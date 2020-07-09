using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.DapperConnection
{
    public interface IFactoryConnection
    {
        void CloseConnection();
        IDbConnection GetConnection();
    }
}
