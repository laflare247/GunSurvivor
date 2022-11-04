public class Experience : CollectableItem
{
    protected override void PlayerCollect()
    {
        base.PlayerCollect();
        _player.AddExperience(_value);
    }
}
