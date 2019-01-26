using UnityEngine;

public class BaseTrick : BaseEntity
{
    protected bool m_unlocked;

    public bool Unlocked
    {
        get { return m_unlocked; }
        set
        {
            SetUnlocked(value);
        }
    }

    protected virtual void SetUnlocked(bool v)
    {
        m_unlocked = v;
    }
}