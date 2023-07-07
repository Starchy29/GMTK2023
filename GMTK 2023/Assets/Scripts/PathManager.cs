using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager
{
    private static List<PathMapping> pathMap = new List<PathMapping>();

    public PathManager()
    {

    }

    private static bool containsPath(string pathName)
    {
        foreach (PathMapping m in pathMap)
        {
            if (m.name.Equals(pathName))
            {
                return true;
            }
        }
        return false;
    }

    public static bool addPath(string pathName, PathPointScript startingPoint)
    {
        if (containsPath(pathName))
        {
            return false;
        }

        pathMap.Add(new PathMapping(pathName, startingPoint));
        return true;
    }

    public static PathPointScript getPathStartingPoint(string pathName)
    {
        foreach (PathMapping m in pathMap)
        {
            if (m.name.Equals(pathName))
            {
                return m.startingPoint;
            }
        }
        return null;
    }

    private class PathMapping
    {
        public string name;
        public PathPointScript startingPoint;

        public PathMapping(string name, PathPointScript startingPoint)
        {
            this.name = name;
            this.startingPoint = startingPoint;
        }
    }

}
