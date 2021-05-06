using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Linear,
    SineY,
    SineX
}

public static class MoveUtility
{
    public static Vector2 Move(Vector2 dir, MoveData data)
    {
        switch(data.MoveType)
        {
            case MoveType.Linear:
            default:
                return Linear(dir);

            case MoveType.SineY:
                return Sine(dir, data, true);

            case MoveType.SineX:
                return Sine(dir, data, false);
        }
    }

    public static bool Arrived(Vector2 pos, Vector2 dest, MoveData data)
    {
        switch(data.MoveType)
        {
            case MoveType.Linear:
            default:
                return Vector2.Distance(pos, dest) <= 0.05f;

            case MoveType.SineY:
                return Mathf.Abs(pos.x - dest.x) <= 0.05f;

            case MoveType.SineX:
                return Mathf.Abs(pos.y - dest.y) <= 0.05f;
        }
    }

    static Vector2 Linear(Vector2 dir)
    {
        return dir.normalized;
    }

    static Vector2 Sine(Vector2 dir, MoveData data, bool sineY)
    {
        Vector2 offset = sineY?new Vector2(0f, Mathf.Sin(Time.time * 2 * Mathf.PI * data.Frequency)):
                         new Vector2(Mathf.Sin(Time.time * 2 * Mathf.PI * data.Frequency), 0f);
        return dir.normalized + offset * data.Amplitude;
    }


}
