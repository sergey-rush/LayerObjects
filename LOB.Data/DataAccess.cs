using System.Configuration;
using System.Data;
using System.Data.Common;

namespace LOB.Data
{
    public abstract class DataAccess
    {
        protected string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SqlServer"].ToString(); }
        }
        protected virtual string LayerObjectsConnection
        {
            get { return ConfigurationManager.ConnectionStrings["LayerObjects1"].ToString(); }
        }
        public static RoleManager Roles
        {
            get { return RoleManager.Instance; }
        }
        public static LayerManager Layers
        {
            get { return LayerManager.Instance; }
        }
        public static DrawingTypeManager DrawingTypes
        {
            get { return DrawingTypeManager.Instance; }
        }
        public static AttributeManager Attributes
        {
            get { return AttributeManager.Instance; }
        }
        public static ElementTypeManager ElementTypes
        {
            get { return ElementTypeManager.Instance; }
        }
        public static ElementTypeAttributeManager ElementTypeAttributes
        {
            get { return ElementTypeAttributeManager.Instance; }
        }
        public static ContactManager Contacts
        {
            get { return ContactManager.Instance; }
        }
        public static LogManager Logs
        {
            get { return LogManager.Instance; }
        }

        public static UserManager Users
        {
            get { return UserManager.Instance; }
        }

        protected int ExecuteNonQuery(DbCommand cmd)
        {
            return cmd.ExecuteNonQuery();
        }

        protected IDataReader ExecuteReader(DbCommand dbCommand)
        {
            return ExecuteReader(dbCommand, CommandBehavior.Default);
        }

        protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
        {
            return cmd.ExecuteReader(behavior);
        }

        protected object ExecuteScalar(DbCommand cmd)
        {
            return cmd.ExecuteScalar();
        }
    }
}