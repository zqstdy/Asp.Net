using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace SqlSugar
{
    public class ConnectionConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public dynamic ConfigId { get; set; }
        /// <summary>
        ///DbType.SqlServer Or Other
        /// </summary>
        public DbType DbType { get; set; }
        /// <summary>
        ///Database Connection string
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// true does not need to close the connection
        /// </summary>
        public bool IsAutoCloseConnection { get; set; }
        /// <summary>
        /// Default SystemTable,If you do not have system table permissions, use attribute
        /// </summary>
        public InitKeyType InitKeyType = InitKeyType.SystemTable;
        /// <summary>
        ///如果为true，则同一连接字符串中同一线程中只有一个连接实例
        /// </summary>
        public bool IsShardSameThread { get; set; }
        /// <summary>
        /// 配置外部服务替换默认服务，例如Redis存储
        /// </summary>
        [JsonIgnore]
        public ConfigureExternalServices ConfigureExternalServices = new ConfigureExternalServices();
        /// <summary>
        /// If SlaveConnectionStrings has value,ConnectionString is write operation, SlaveConnectionStrings is read operation.
        /// All operations within a transaction is ConnectionString
        /// </summary>
        public List<SlaveConnectionConfig> SlaveConnectionConfigs { get; set; }
        /// <summary>
        /// 更多戈壁设置
        /// </summary>
        public ConnMoreSettings MoreSettings { get; set; }
        /// <summary>
        /// 用于调试对性能有影响的错误或错误，用于调试
        /// </summary>
        public SugarDebugger Debugger { get; set; }

        [JsonIgnore]
        public AopEvents AopEvents { get;set; }
    }
    public class AopEvents
    {
        public Action<DiffLogModel> OnDiffLogEvent { get; set; }
        public Action<SqlSugarException> OnError { get; set; }
        public Action<string, SugarParameter[]> OnLogExecuting { get; set; }
        public Action<string, SugarParameter[]> OnLogExecuted { get; set; }
        public Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> OnExecutingChangeSql { get; set; }
    }
    public class ConfigureExternalServices
    {

        private ISerializeService _SerializeService;
        private ICacheService _ReflectionInoCache;
        private ICacheService _DataInfoCache;
        private IRazorService _RazorService;

        public IRazorService RazorService
        {
            get
            {
                if (_RazorService == null)
                    return _RazorService;
                else
                    return _RazorService;
            }
            set { _RazorService = value; }
        }

        public ISerializeService SerializeService
        {
            get
            {
                if (_SerializeService == null)
                    return DefaultServices.Serialize;
                else
                    return _SerializeService;
            }
            set{ _SerializeService = value;}
        }

        public ICacheService ReflectionInoCacheService
        {
            get
            {
                if (_ReflectionInoCache == null)
                    return DefaultServices.ReflectionInoCache;
                else
                    return _ReflectionInoCache;
            }
            set{_ReflectionInoCache = value;}
        }

        public ICacheService DataInfoCacheService
        {
            get
            {
                if (_DataInfoCache == null)
                    return DefaultServices.DataInoCache;
                else
                    return _DataInfoCache;
            }
            set { _DataInfoCache = value; }
        }

        public List<SqlFuncExternal> SqlFuncServices { get; set; }
        public List<KeyValuePair<string, CSharpDataType>> AppendDataReaderTypeMappings { get;  set; }


        public Action<PropertyInfo, EntityColumnInfo> EntityService{ get; set; }
        public Action<Type,EntityInfo> EntityNameService { get; set; }
    }
}
