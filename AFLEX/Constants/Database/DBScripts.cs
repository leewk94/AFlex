namespace AFLEX.Constants.Database
{
    public class DBScripts
    {
        public const string MASTER_CHECKEXISTENCE = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id=OBJECT_ID(N'{0}') AND type in (N'U')) ";

        public const string MASTER_GETSERVICESYNCDATA_BYDATATYPE = "SELECT DocType,ServiceValue,ServiceVal2 FROM dbo.Flex_ServiceSyncData WHERE DocType='{0}'";

        public const string TRIGGERMAINTENANCE_GETLIST_BYMAINTENANCEVALUE = "SELECT DocType,Value,Status FROM dbo.Flex_AFlex_TriggerMaintenance WHERE DocType='{0}'  AND Value='{1}' AND Status='{2}'";
        public const string TRIGGERMAINTENANCE_UPDATE = "UPDATE Flex_AFlex_TriggerMaintenance SET ModifiedDate={0} WHERE DocType={1} AND Value={2} AND Status={3} ";
        public const string TRIGGERMAINTENANCE_INSERT = "INSERT INTO Flex_AFlex_TriggerMaintenance (DocType,Value,Status,CreationDate,ModifiedDate) VALUES ({0},{1},{2},{3},{4})";
        public const string TRIGGERMAINTENANCE_DELETE_BYMAINTENANCEVALUE = "DELETE FROM dbo.Flex_AFlex_TriggerMaintenance WHERE DocType={0} AND Value={1} ";

    }
}
