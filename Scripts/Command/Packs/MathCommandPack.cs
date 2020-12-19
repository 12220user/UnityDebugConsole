using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityDebugConsole.CommandTool;

[CommandPackage("Math")]
public static class MathCommandPack
{
    [Command("=" , "command supported all math operation" , "x | operation (+/-/...) | y")]
    public static string Math(float x , string operation , float y){
        switch(operation){
            case "+":
                return Sum(x , y);
            case "-":
                return Subtraction(x , y);
            case "*":
                return Multiply(x , y);
            case "/":
                return Division(x , y);
            case "^":
                return Pow(x , y);
            default:
                return null;
        }
    }


    [Command("sum" , "x + y" , "x | y")]
    public static string Sum(float x , float y){
        return (x + y).ToString();
    }

    [Command("subtract" , "x - y" , "x | y")]
    public static string Subtraction(float x , float y){
        return (x - y).ToString();
    }

    [Command("multiply" , "x * y" , "x | y")]
    public static string Multiply(float x, float y){
        return (x * y).ToString();
    }
    
    [Command("division" , "x / y" , "x | y")]
    public static string Division(float x, float y){
        return (x / y).ToString();
    }

    [Command("pow" , "x^y" , "x | y")]
    public static string Pow(float x, float y){
        return (System.Math.Pow(x , y)).ToString();
    }

    [Command("sqrt" , "x^(1/y)" , "x | y")]
    public static string Sqrt(float x, float y){
        return (System.Math.Pow(x , (1/y))).ToString();
    }

    [Command("delta" , "x^(1/y)" , "x | y | delta")]
    public static string Lerp(float x, float y , float delta){
        return (x + ( (y-x) * delta)).ToString();
    }

    [Command("sin" , "sinus" , "x")]
    public static string Sin(float x){
        return System.Math.Sin(x).ToString();
    }

    [Command("cos" , "cosinus" , "x")]
    public static string Cos(float x){
        return System.Math.Cos(x).ToString();
    }
}
