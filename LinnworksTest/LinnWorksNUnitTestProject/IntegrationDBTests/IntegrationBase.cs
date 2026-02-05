namespace LinnWorksNUnitTestProject.IntegrationDBTests
{
    public class IntegrationBase
    {
        protected readonly DatabaseFactory.DbCRUD.DBTestCRUD _dbtest;
        public IntegrationBase()
        {
            _dbtest = new DatabaseFactory.DbCRUD.DBTestCRUD();
        }
    }
}
