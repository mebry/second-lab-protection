namespace first_l_s2.DI
{
    internal interface IData<T>
    {
        void Add(T data);
        void Remove(int index);
        IEnumerable<T> ReadAll();
    }
}
