﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace HighlightPlus {


    public partial class HighlightEffect : MonoBehaviour {

        static List<HighlightSeeThroughOccluder> occluders = new List<HighlightSeeThroughOccluder>();
        static Dictionary<Camera, int> occludersFrameCount = new Dictionary<Camera, int>();
        static CommandBuffer cbOccluder;
        static Material fxMatOccluder;
        static RaycastHit[] hits;
        static Collider[] colliders;

        bool cancelSeeThroughThisFrame;
        public static void RegisterOccluder(HighlightSeeThroughOccluder occluder) {
            if (!occluders.Contains(occluder)) {
                occluders.Add(occluder);
            }
        }

        public static void UnregisterOccluder(HighlightSeeThroughOccluder occluder) {
            if (occluders.Contains(occluder)) {
                occluders.Remove(occluder);
            }
        }

        public void RenderOccluders(CommandBuffer cb, Camera cam) {

            int occludersCount = occluders.Count;
            if (occludersCount == 0 || rmsCount == 0) return;

            Vector3 camPos = cam.transform.position;

            bool useRayCastCheck = false;
            // Check if raycast method is needed
            for (int k = 0; k < occludersCount; k++) {
                HighlightSeeThroughOccluder occluder = occluders[k];
                if (occluder == null || !occluder.isActiveAndEnabled) continue;
                if (occluder.detectionMethod == DetectionMethod.RayCast) {
                    useRayCastCheck = true;
                    break;
                }
            }
            if (useRayCastCheck) {
                // Compute bounds
                Bounds bounds = new Bounds();
                for (int r = 0; r < rms.Length; r++) {
                    if (rms[r].renderer != null) {
                        if (bounds.size.x == 0) {
                            bounds = rms[r].renderer.bounds;
                        } else {
                            bounds.Encapsulate(rms[r].renderer.bounds);
                        }
                    }
                }
                Vector3 pos = bounds.center;
                Vector3 offset = pos - camPos;
                float maxDistance = Vector3.Distance(pos, camPos);
                if (hits == null || hits.Length == 0) {
                    hits = new RaycastHit[64];
                }
                int hitCount = Physics.BoxCastNonAlloc(pos - offset, bounds.extents * 0.9f, offset.normalized, hits, Quaternion.identity, maxDistance);
                for (int k = 0; k < hitCount; k++) {
                    for (int j = 0; j < occludersCount; j++) {
                        if (hits[k].collider.transform == occluders[j].transform) {
                            cancelSeeThroughThisFrame = true;
                            return;
                        }
                    }
                }
            }

            int lastFrameCount;
            occludersFrameCount.TryGetValue(cam, out lastFrameCount);
            int currentFrameCount = Time.frameCount;
            if (currentFrameCount == lastFrameCount) return;
            occludersFrameCount[cam] = currentFrameCount;

            if (cbOccluder == null) {
                cbOccluder = new CommandBuffer();
                cbOccluder.name = "Occluder";
            }

            if (fxMatOccluder == null) {
                InitMaterial(ref fxMatOccluder, "HighlightPlus/Geometry/SeeThroughOccluder");
                if (fxMatOccluder == null) return;
            }


            for (int k = 0; k < occludersCount; k++) {
                HighlightSeeThroughOccluder occluder = occluders[k];
                if (occluder == null || !occluder.isActiveAndEnabled) continue;
                if (occluder.detectionMethod == DetectionMethod.Stencil) {
                    if (occluder.meshData == null || occluder.meshData.Length == 0) continue;
                    // Per renderer
                    for (int m = 0; m < occluder.meshData.Length; m++) {
                        // Per submesh
                        Renderer renderer = occluder.meshData[m].renderer;
                        if (renderer.isVisible) {
                            for (int s = 0; s < occluder.meshData[m].subMeshCount; s++) {
                                cb.DrawRenderer(renderer, fxMatOccluder, s);
                            }
                        }
                    }
                }
            }
        }

        bool CheckOcclusion(Camera cam) {

            float now = Time.time;
            int frameCount = Time.frameCount; // ensure all cameras are checked this frame

            if (Time.time - occlusionCheckLastTime < seeThroughOccluderCheckInterval && Application.isPlaying && occlusionRenderFrame != frameCount) return lastOcclusionTestResult;
            occlusionCheckLastTime = now;
            occlusionRenderFrame = frameCount;

            if (rms.Length == 0 || rms[0].renderer == null) return false;

            Vector3 camPos = cam.transform.position;
            Quaternion quaternionIdentity = Quaternion.identity;
            if (colliders == null || colliders.Length == 0) {
                colliders = new Collider[1];
            }

            if (seeThroughOccluderCheckIndividualObjects) {
                for (int r = 0; r < rms.Length; r++) {
                    if (rms[r].renderer != null) {
                        Bounds bounds = rms[r].renderer.bounds;
                        Vector3 pos = bounds.center;
                        float maxDistance = Vector3.Distance(pos, camPos);
                        Vector3 extents = bounds.extents * seeThroughOccluderThreshold;
                        if (Physics.OverlapBoxNonAlloc(pos, extents, colliders, quaternionIdentity, seeThroughOccluderMask) > 0) {
                            lastOcclusionTestResult = true;
                            return true;
                        }
                        if (Physics.BoxCast(pos, extents, (camPos - pos).normalized, quaternionIdentity, maxDistance, seeThroughOccluderMask)) {
                            lastOcclusionTestResult = true;
                            return true;
                        }
                    }
                }
                lastOcclusionTestResult = false;
                return false;
            } else {
                // Compute combined bounds
                Bounds bounds = rms[0].renderer.bounds;
                for (int r = 1; r < rms.Length; r++) {
                    if (rms[r].renderer != null) {
                        bounds.Encapsulate(rms[r].renderer.bounds);
                    }
                }
                Vector3 pos = bounds.center;
                Vector3 extents = bounds.extents * seeThroughOccluderThreshold;
                if (Physics.OverlapBoxNonAlloc(pos, extents, colliders, quaternionIdentity, seeThroughOccluderMask) > 0) {
                    lastOcclusionTestResult = true;
                    return true;
                }
                float maxDistance = Vector3.Distance(pos, camPos);
                lastOcclusionTestResult = Physics.BoxCast(pos, extents, (camPos - pos).normalized, quaternionIdentity, maxDistance, seeThroughOccluderMask);
                return lastOcclusionTestResult;
            }
        }


        const int MAX_OCCLUDER_HITS = 50;
        static RaycastHit[] occluderHits;
        readonly Dictionary<Camera, List<Renderer>> cachedOccludersPerCamera = new Dictionary<Camera, List<Renderer>>();

        void CheckOcclusionAccurate(CommandBuffer cbuf, Camera cam) {

            List<Renderer> occluderRenderers;
            if (!cachedOccludersPerCamera.TryGetValue(cam, out occluderRenderers)) {
                occluderRenderers = new List<Renderer>();
                cachedOccludersPerCamera[cam] = occluderRenderers;
            }

            float now = Time.time;
            int frameCount = Time.frameCount; // ensure all cameras are checked this frame
            bool reuse = Time.time - occlusionCheckLastTime < seeThroughOccluderCheckInterval && Application.isPlaying && occlusionRenderFrame != frameCount;

            if (!reuse) {
                if (rms.Length == 0 || rms[0].renderer == null) return;

                occlusionCheckLastTime = now;
                occlusionRenderFrame = frameCount;
                Quaternion quaternionIdentity = Quaternion.identity;
                Vector3 camPos = cam.transform.position;

                occluderRenderers.Clear();

                if (occluderHits == null || occluderHits.Length < MAX_OCCLUDER_HITS) {
                    occluderHits = new RaycastHit[MAX_OCCLUDER_HITS];
                }

                if (seeThroughOccluderCheckIndividualObjects) {
                    for (int r = 0; r < rms.Length; r++) {
                        if (rms[r].renderer != null) {
                            Bounds bounds = rms[r].renderer.bounds;
                            Vector3 pos = bounds.center;
                            float maxDistance = Vector3.Distance(pos, camPos);
                            int numOccluderHits = Physics.BoxCastNonAlloc(pos, bounds.extents * seeThroughOccluderThreshold, (camPos - pos).normalized, occluderHits, quaternionIdentity, maxDistance, seeThroughOccluderMask);
                            for (int k = 0; k < numOccluderHits; k++) {
                                Renderer rr = occluderHits[k].collider.GetComponentInChildren<Renderer>();
                                if (rr != null && !occluderRenderers.Contains(rr)) {
                                    occluderRenderers.Add(rr);
                                }
                            }
                        }
                    }
                } else {
                    // Compute combined bounds
                    Bounds bounds = rms[0].renderer.bounds;
                for (int r = 1; r < rms.Length; r++) {
                    if (rms[r].renderer != null) {
                        bounds.Encapsulate(rms[r].renderer.bounds);
                    }
                }
                    Vector3 pos = bounds.center;
                    float maxDistance = Vector3.Distance(pos, camPos);
                    int numOccluderHits = Physics.BoxCastNonAlloc(pos, bounds.extents * seeThroughOccluderThreshold, (camPos - pos).normalized, occluderHits, quaternionIdentity, maxDistance, seeThroughOccluderMask);
                    for (int k = 0; k < numOccluderHits; k++) {
                        Renderer rr = occluderHits[k].collider.GetComponentInChildren<Renderer>();
                        if (rr != null) {
                            occluderRenderers.Add(rr);
                        }
                    }
                }
            }

            // render occluders
            int occluderRenderersCount = occluderRenderers.Count;
            if (occluderRenderersCount > 0) {
                for (int k = 0; k < occluderRenderersCount; k++) {
                    Renderer r = occluderRenderers[k];
                    if (r != null) {
                        cbuf.DrawRenderer(r, fxMatSeeThroughMask);
                    }
                }
            }
        }

        public List<Renderer> GetOccluders(Camera camera) {
            List<Renderer> occluders = null;
            if (cachedOccludersPerCamera != null) {
                cachedOccludersPerCamera.TryGetValue(camera, out occluders);
            }
            return occluders;
        }
    }
}
