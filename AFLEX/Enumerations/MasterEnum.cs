namespace AFLEX.Enumerations
{
    public enum SyncDataTypeEnum
    {
        ACCESSCHECK = 1,
        PERMISSION = 2,
        REALTIME_MAINT_SYNC = 3,
        REALTIME_DOC_SYNC = 4,
        API = 5,
        APINAME = 6,
        APIPASS = 7,
        API_TOKEN_VAL = 8
    }

    public enum CategoryEnum
    {
        Debtor = 1,
        Area = 2
    }

    public enum PermissionEnum
    {
        Read = 1,
        Write = 2
    }

    public enum ActionTypeEnum
    {
        Insert = 1,
        Delete = 2,
        Update = 3
    }

    public enum RealTimeStatusEnum
    {
        Yes = 1,
        No = 0
    }

}
