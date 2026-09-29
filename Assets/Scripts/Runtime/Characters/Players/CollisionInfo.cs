using UnityEngine;

public struct CollisionInfo
{
    public bool m_above;
    public bool m_below;
    public bool m_left;
    public bool m_right;

    public void Reset()
    {
        m_above = false;
        m_below = false;
        m_left = false;
        m_right = false;
    }
}
