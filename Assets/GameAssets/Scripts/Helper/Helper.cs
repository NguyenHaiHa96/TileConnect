using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Helper  
{
    private static readonly Dictionary<float, WaitForSeconds> WFSDictionary = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWaitForSeconds(float time)
    {
        if (WFSDictionary.TryGetValue(time, out var wait)) return wait;

        WFSDictionary[time] = new WaitForSeconds(time);
        return WFSDictionary[time];
    }

    public static IEnumerator WaitForFrames(int frameCount)
    {
        if (frameCount <= 0) throw new ArgumentOutOfRangeException("frameCount", "Cannot wait for less that 1 frame");
        while (frameCount > 0)
        {
            frameCount--;
            yield return null;
        }
    }

    #region Vector3
    public static Vector3 GetDirection(Transform a, Transform b)
    {
        return (b.position - a.position).normalized;
    }

    public static bool CheckDistance(Vector3 start, Vector3 end, float threshold)
    {
        return Vector3.Distance(start, end) <= threshold;
    }

    public static Vector3 GetRandomPoint(List<Transform> transforms, float randomRange1, float randomRange2)
    {
        Vector3 position = transforms[Random.Range(0, transforms.Count)].position;
        Vector3 addition = new Vector3(Random.Range(randomRange1, randomRange2), 0f, Random.Range(randomRange1, randomRange2));
        return position + addition;
    }

    public static Vector3 GetDirection(Vector3 a, Vector3 b)
    {
        return (b - a).normalized;
    }

    public static Vector3 GetSpecificDirection(Transform a, Transform b)
    {
        return new Vector3(b.position.x - a.position.x, 0, b.position.z - a.position.z).normalized;
    }

    public static Quaternion GetQuaternion(Vector3 targetPosition, Transform transform)
    {
        Vector3 lookAtPosition = new Vector3(targetPosition.x - transform.position.x,
                    transform.position.y,
                    targetPosition.z - transform.position.z);
        return Quaternion.LookRotation(lookAtPosition);
    }

    public static void ClampValue(ref int value, int min, int max)
    {
        if (value > max)
        {
            value = min;
        }
    }

    public static Vector3 GetSpecificDirection(Vector3 a, Vector3 b)
    {
        return new Vector3(b.x - a.x, 0, b.z - a.z).normalized;
    }

    public static Vector3 GetRandomVector(Vector3 origin, float minRange, float maxRange)
    {
        return origin + new Vector3(Random.Range(minRange, maxRange), origin.y, Random.Range(minRange, maxRange));
    }

    public static Vector3 GetRandomVector(Vector3 origin, float maxRange)
    {
        return origin + new Vector3(Random.Range(0, maxRange), origin.y, Random.Range(0, maxRange));
    }

    public static Vector3 GetRandomVectorXAxis(Vector3 origin, float maxRange, float maxZ)
    {
        return origin + new Vector3(Random.Range(0, maxRange), origin.y, maxZ);
    }

    public static Vector3 GetRandomVector(Vector3 origin, Vector3 range)
    {
        return origin + new Vector3(Random.Range(-range.x, range.x), 0f, Random.Range(0, range.z));
    }
    #endregion

    #region Object
    public static Quaternion RandomRatation()
    {
        Vector3 rotation = new Vector3(0f, Random.Range(0, 360), 0);
        return Quaternion.Euler(rotation);
    }

    public static Transform GetObjectTransform<T>(T obj)
    {
        return (obj as MonoBehaviour).GetComponent<Transform>();
    }

    public static List<Transform> GetObjectTransforms<T>(List<T> list)
    {
        List<Transform> transforms = new List<Transform>();
        for (int i = 0; i < list.Count; i++)
        {
            Transform cachedTransform = (list[i] as MonoBehaviour).GetComponent<Transform>();
            transforms.Add(cachedTransform);
        }
        return transforms;
    }

    public static Transform GetRandomTransform(List<Transform> transforms)
    {
        int randomIndex = Random.Range(0, transforms.Count);
        return transforms[randomIndex];
    }


    public static Vector3 GetObjectPosition<T>(T obj)
    {
        return (obj as MonoBehaviour).GetComponent<Transform>().position;
    }

    public static List<Vector3> GetObjectPositions<T>(List<T> list)
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < list.Count; i++)
        {
            Vector3 position = (list[i] as MonoBehaviour).GetComponent<Transform>().position;
            positions.Add(position);
        }
        return positions;
    }

    public static List<Quaternion> GetObjectRotations<T>(List<T> list)
    {
        List<Quaternion> quaternions = new List<Quaternion>();
        for (int i = 0; i < list.Count; i++)
        {
            Quaternion quaternion = (list[i] as MonoBehaviour).GetComponent<Transform>().rotation;
            quaternions.Add(quaternion);
        }
        return quaternions;
    }

    public static void SetObjectPosition(MonoBehaviour monoObject, Vector3 position)
    {
        monoObject.transform.position = position;
    }

    public static void SetObjectPosition(MonoBehaviour monoObject, Transform transform, float height)
    {
        monoObject.transform.position = new Vector3(transform.position.x,
                transform.position.y + height,
                transform.position.z);
    }

    public static void SetObjectPositions<T>(List<T> objects, List<Transform> points, float height)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Transform objTransform = (objects[i] as MonoBehaviour).transform;
            objTransform.position = new Vector3(points[i].position.x,
                points[i].position.y + height,
                points[i].position.z);
        }
    }

    public static void SetObjectRandomPositions<T>(List<T> objects, List<Transform> points, float height)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            Transform randomTransform = GetRandomTransform(points);
            Transform objTransform = (objects[i] as MonoBehaviour).transform;
            objTransform.position = new Vector3(randomTransform.position.x,
                randomTransform.position.y + height,
                randomTransform.position.z);
            points.Remove(randomTransform);
        }
    }

    public static void SetObjectPositions(List<Transform> objectTransforms, List<Transform> points, float height)
    {
        for (int i = 0; i < objectTransforms.Count; i++)
        {
            objectTransforms[i].position = new Vector3(points[i].position.x,
                points[i].position.y + height,
                points[i].position.z);
        }
    }

    public static void SetObjectRotations(List<Transform> objectTransforms, List<Quaternion> quaternions)
    {
        for (int i = 0; i < objectTransforms.Count; i++)
        {
            objectTransforms[i].rotation = quaternions[i];  
        }
    }
    #endregion

    #region Color
    public static string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    public static void ChangeColorAlpha(Color color1, Color color2)
    {
        Color tempColor = color1;
        tempColor.a = 0;
        color2 = tempColor;
    }

    public static Color HexToColor(string hex)
    {
        hex = hex.Replace("0x", ""); //in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", ""); //in case the string is formatted #FFFFFF
        byte a = 255; //assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }

    public static Color HexToColorWithAlpha(string hex, byte alpha)
    {
        hex = hex.Replace("0x", ""); //in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", ""); //in case the string is formatted #FFFFFF
        byte a = alpha; //assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }
    #endregion

    #region Extensions
    public static void AddElements<T>(this List<T> list, params T[] elements)
    {
        list.AddRange(elements);
    }

    public static void AddElements<T>(this HashSet<T> hashSet, params T[] elements)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            hashSet.Add(elements[i]);
        }
    }
    #endregion

    #region Angle
    public static float GetAngleFromTwoPositions(Transform from, Transform to)
    {
        if (from == null || to == null)
        {
            return 0f;
        }
        float xDistance = to.position.x - from.position.x;
        float zDistance = to.position.z - from.position.z;
        float angle = (Mathf.Atan2(zDistance, xDistance) * Mathf.Rad2Deg) - 90f;
        angle = GetNormalizedAngle(angle);
        return angle;
    }

    public static void SetEulerAnglesZAxis(this Transform self, float z)
    {
        Vector3 selfAngles = self.eulerAngles;
        self.rotation = Quaternion.Euler(selfAngles.x, selfAngles.y, z);
    }

    public static void SetRotationFromTwoTransforms(this Transform self, Transform from, Transform to)
    {
        Vector3 direction = from.position - to.position;
        self.rotation = Quaternion.LookRotation(direction);
    }

    public static float GetNormalizedAngle(float angle)
    {
        while (angle < 0f)
        {
            angle += 360f;
        }
        while (360f < angle)
        {
            angle -= 360f;
        }
        return angle;
    }
    #endregion

    #region String
    public static string[] SplitString(string input, string spliter)
    {
        return input.Split(new string[] { spliter }, System.StringSplitOptions.None);
    } 
    #endregion
}
