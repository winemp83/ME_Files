using System;

namespace ME_SDApp.Model
{
    public interface IDataService
    {
        void GetData(Action<DataItem, Exception> callback);
    }
}