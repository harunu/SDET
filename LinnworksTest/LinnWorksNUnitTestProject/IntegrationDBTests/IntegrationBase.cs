using System;
using System.Collections.Generic;
using System.Text;

namespace LinnWorksNUnitTestProject.IntegrationDBTests
{
    public  class IntegrationBase
    {
        protected readonly DatabaseFactory.DbCRUD.DBTestCRUD _dbtest;

        public  IntegrationBase()
        {
            _dbtest = new DatabaseFactory.DbCRUD.DBTestCRUD();
        }
    }
}
