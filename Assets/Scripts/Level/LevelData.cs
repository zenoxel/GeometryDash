using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public List<BodyData>  body;
    public List<ImageData> image;
}

[System.Serializable]
public class BodyData
{
    public string            name;
    public PositionData      position;
    public float             angle;
    public List<CustomProperty> customProperties;
    public List<FixtureData>    fixture;
}

[System.Serializable]
public class PositionData { public float x; public float y; }

[System.Serializable]
public class CustomProperty
{
    public string name;
    public string @string;
}

[System.Serializable]
public class FixtureData  { public PolygonData polygon; }

[System.Serializable]
public class PolygonData  { public VerticesData vertices; }

[System.Serializable]
public class VerticesData
{
    public List<float> x;
    public List<float> y;
}

[System.Serializable]
public class ImageData
{
    public string     file;
    public int        body;
    public CenterData center;
    public float      scale;
    public float      opacity;
}

[System.Serializable]
public class CenterData { public float x; public float y; }