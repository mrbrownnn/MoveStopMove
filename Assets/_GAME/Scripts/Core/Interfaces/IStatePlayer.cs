public interface IStatePlayer
{
    void OnEnter(PlayerController player);
    void OnExecute(PlayerController player);
    void OnExit(PlayerController player);
}
