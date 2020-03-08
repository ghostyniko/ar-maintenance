using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPose 
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }
   
    public double SX { get; set; }

    public double SY { get; set; }
    public double SZ { get; set; }
    public string Type { get; set; }
    public ObjectPose(double x, double y, double z, double a, double b, double c, double sX, double sY, double sZ,string type)
    {
        X = x;
        Y = y;
        Z = z;
        A = a;
        B = b;
        C = c;
        SX = sX;
        SY = sY;
        SZ = sZ;
        Type = type;
    }
}
