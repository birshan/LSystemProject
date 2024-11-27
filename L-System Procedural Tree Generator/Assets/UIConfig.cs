using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum TreeType 
{
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H
}
public static class UIConfig
{
    public static string Axiom {get; set;} = "F";
    public static int Iterations {get; set;} = 5;
    public static float Angle {get; set;} = 22.5f;  
    public static Dictionary<char, string> Rules {get; set;} = new Dictionary<char, string>();
    public static float InitialThickness {get; set;} = 5f;
    public static float ContractionRatio {get; set;} = 0.707f;
    public static int RandomAngle3D {get; set;} = 20;
    public static int RandomAngle3DChange {get; set;} = 5;

    public static bool Is3D {get; set;} = true;
    public static bool DoSmoothBranching {get; set;} = false;
    public static bool DoChaoticBranching {get; set;} = false;
    public static bool DoTwistedHorrorBranching {get; set;} = false;

    public static Vector3 cameraPosition{get; set;} = new Vector3(0, 275, -175);
    public static int cameraVerticalOffset{get; set;} = 300;

    // Predefined tree configurations 
    public static Dictionary<TreeType, (string axiom, int interations, float angle, Dictionary<char, string> rules, float initialThickness, float contractionRatio, 
    bool is3d, bool doSmoothBranching, bool  doHorrorBranching, bool DoTwistedHorrorBranching)> 
    TreeConfigs {get; set;}= new Dictionary<TreeType, (string axiom, int interations, float angle, Dictionary<char, string> rules, float initialThickness, float contractionRatio, 
    bool is3d, bool doSmoothBranching, bool  doHorrorBranching, bool DoTwistedHorrorBranching)>
    {
        {TreeType.A, ("F", 5, 25.7f, new Dictionary<char, string>{{'F', "F[+F]F[-F]F"}}, 5f, 0.707f, false, false, false, false)},
        {TreeType.B, ("F", 5, 20f, new Dictionary<char, string>{{'F', "F[+F]F[-F][F]"}}, 5f, 0.707f, false, false, false, false)},
        {TreeType.C, ("F", 4, 22.5f, new Dictionary<char, string>{{'F', "FF-[-F+F+F]+[+F-F-F]"}}, 5f, 0.707f, false, false, false, false)},
        {TreeType.D, ("X", 7, 20f, new Dictionary<char, string>{{'X', "F[+X]F[-X]+X"},{'F', "FF"}}, 5f, 0.707f, false, false, false, false)},
        {TreeType.E, ("X", 7, 25.7f, new Dictionary<char, string>{{'X', "F[+X][-X]FX"},{'F', "FF"}}, 5f, 0.707f, false, false, false, false)},
        {TreeType.F, ("X", 5, 22.5f, new Dictionary<char, string>{{'X', "F-[[X]+X]+F[+FX]-X"},{'F', "FF"}}, 5f, 0.707f, false, false, false, false)},
        {TreeType.G, ("Y", 5, 25.7f, new Dictionary<char, string>{{'X', "X[-FFF][+FFF]FX"},{'Y', "YFX[+Y][-Y]"}}, 5f, 0.707f, false, false, false, false)},
        {TreeType.H, ("F", 4, 35f, new Dictionary<char, string>{{'F', "F[+FF][-FF]F[-F][+F]F"}}, 5f, 0.707f, false, false, false, false)}
    };
    
}
