using UnityEngine;

public class BaseTrick : MonoBehaviour
{
    protected bool m_unlocked;

    public bool Unlocked
    {
        get { return m_unlocked; }
        set
        {
            SetUnlocked(m_unlocked);
        }
    }

    private void SetUnlocked(bool v)
    {
        m_unlocked = v;
    }
}