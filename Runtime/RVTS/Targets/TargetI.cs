namespace RVTS.Targets
{
    public interface TargetI<T>
    {
        public void Action(T args);
    }
}