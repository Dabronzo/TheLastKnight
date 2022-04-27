//This is the interface that allows call the Cancel method on Combat and Movement by the ActionScheduler
//without creating cycles of dependencies
namespace RPG.GameCore
{
    public interface IAction
    {
        void Cancel();
    }
}