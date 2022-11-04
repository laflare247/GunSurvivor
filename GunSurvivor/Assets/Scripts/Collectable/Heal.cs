using UnityEngine;

public class Heal : CollectableItem
{
    protected override void PlayerCollect()
    {
        base.PlayerCollect();
        _player.AddHealth(_player.MaxHealth / 20 + Random.Range(_player.MaxHealth / -200, _player.MaxHealth / 200));
    }
}
