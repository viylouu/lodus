using System.Numerics;

public class math {
    /// <summary>half the circumference of a circle with a radius of 1 or 180 degrees in radians</summary>
    public const float pi = 3.14159265358979323846264f;
    /// <summary>rounds a number</summary>
    public static float round(float a) => MathF.Round(a);
    /// <summary>calculates the ceiling of a number</summary>
    public static float ceil(float a) => MathF.Ceiling(a);
    /// <summary>calculates the floor of a number</summary>
    public static float floor(float a) => MathF.Floor(a);
    /// <summary>calculates the absolute value of a number</summary>
    public static float abs(float a) => MathF.Abs(a);
    /// <summary>calculates the cosine of a number</summary>
    public static float cos(float a) => MathF.Cos(a);
    /// <summary>calculates the sine of a number</summary>
    public static float sin(float a) => MathF.Sin(a);
    /// <summary>calculates a number to the power of another number</summary>
    public static float pow(float a, float b) => MathF.Pow(a,b);
    /// <summary>clamps a number between two values (b and c)</summary>
    public static float clamp(float a, float b, float c) => Math.Clamp(a,b,c);
    /// <summary>calculates the square root of a number</summary>
    public static float sqrt(float a) => MathF.Sqrt(a);
    /// <summary>calculates the cube root of a number</summary>
    public static float cbrt(float a) => MathF.Pow(a,.3333333333333333333333333333333333f);
    /// <summary>calculates a number to its nth root</summary>
    public static float nroot(float a, float n) => MathF.Pow(a,1f/n);
    /// <summary>calculates a number to the power of 2</summary>
    public static float sqr(float a) => a*a;
    /// <summary>calculates a number to the power of 3</summary>
    public static float cbe(float a) => a*a*a;
    /// <summary>calculates the distance between two points</summary>
    public static float dist(Vector2 a, Vector2 b) => sqrt(sqr(b.X-a.X)+sqr(b.Y-a.Y));
    public static float dist(Vector2 a, float x, float y) => sqrt(sqr(x - a.X) + sqr(y - a.Y));
    public static float dist(float u, float v, float x, float y) => sqrt(sqr(x - u) + sqr(y - v));
    /// <summary>calculates the squared distance between two points</summary>
    public static float sqrdist(Vector2 a, Vector2 b) => sqr(b.X - a.X) + sqr(b.Y - a.Y);
    public static float sqrdist(Vector2 a, float x, float y) => sqr(x - a.X) + sqr(y - a.Y);
    public static float sqrdist(float u, float v, float x, float y) => sqr(x - u) + sqr(y - v);
    ///<summary>returns the biggest number between 2 numbers</summary>
    public static float max(float a, float b) => MathF.Max(a, b);
    ///<summary>returns the smallest number between 2 numbers</summary>
    public static float min(float a, float b) => MathF.Min(a, b);
}