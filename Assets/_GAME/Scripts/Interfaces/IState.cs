public interface IState 
{
    void OnEnter(EnemyController enemy);
    void OnExecute(EnemyController enemy);
    void OnExit(EnemyController enemy);
}
