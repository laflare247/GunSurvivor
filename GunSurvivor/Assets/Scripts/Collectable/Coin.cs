public class Coin : CollectableItem
{
    protected override void PlayerCollect()
    {
        base.PlayerCollect();
        _player.AddCoin((int)_value);
    }
}
