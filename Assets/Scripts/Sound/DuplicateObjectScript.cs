using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class DuplicateObjectScript
{

    /// <summary>
    /// Code from stack overflow to copy fields
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TU"></typeparam>
    /// <param name="source"></param>
    /// <param name="dest"></param>
    public static void CopyPropertiesTo<T, TU>(this T source, TU dest, List<string> ignoreList = null)
    {
        var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
        var destProps = typeof(TU).GetProperties()
                .Where(x => x.CanWrite)
                .ToList();

        foreach (var sourceProp in sourceProps)
        {
            if (destProps.Any(x => x.Name == sourceProp.Name))
            {
                var p = destProps.First(x => x.Name == sourceProp.Name);
                if (ignoreList != null)
                {
                    if (p.CanWrite && !ignoreList.Contains(p.Name))
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }
                else
                {
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }
            }

        }

    }
}
