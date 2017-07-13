namespace ViewModel.Shared
{
    using System;

    public interface IClosable
    {
        event EventHandler CloseEvent;
    }
}
