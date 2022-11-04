using UnityEngine;

public class StatisticCounter : MonoBehaviour
{
    [SerializeField]private int _killedEnemies = 0;
    [SerializeField]private int _outgoingDamage = 0;
    [SerializeField]private int _incomingDamage = 0;

    [SerializeField]private int _collectedCoins = 0;
    [SerializeField]private int _collectedExperience = 0;

    public int KilledEnemies { get { return _killedEnemies; } set { if (value >= 0) _killedEnemies = value; } }

    public int OutGoingDamage { get { return _outgoingDamage; } set { if (value >= 0) _outgoingDamage = value; } }

    public int IncomingDamage { get { return _incomingDamage; } set { if (value >= 0) _incomingDamage = value; } }

    public int CollectedCoins { get { return _collectedCoins; } set { if (value >= 0) _collectedCoins = value; } }

    public int CollectedExperiense { get { return _collectedExperience; } set { if (value >= 0) _collectedExperience = value; } }
}