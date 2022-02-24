using System;
using System.Collections.Generic;
using System.Text;

namespace BuletinKlp01FE.Data
{
    public interface INetworkConnection
    {
        bool IsConnected { get; }

        void CheckNetworkConnection();
    }
}
