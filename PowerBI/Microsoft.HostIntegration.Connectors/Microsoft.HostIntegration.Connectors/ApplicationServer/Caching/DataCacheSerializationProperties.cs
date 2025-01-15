using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001E2 RID: 482
	public class DataCacheSerializationProperties
	{
		// Token: 0x06000FA2 RID: 4002 RVA: 0x00035B5E File Offset: 0x00033D5E
		internal DataCacheSerializationProperties()
			: this(DataCacheObjectSerializerType.NetDataContractSerializer, null)
		{
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00035B68 File Offset: 0x00033D68
		internal DataCacheSerializationProperties(DataCacheObjectSerializerType cacheObjectSerializerType, string customCacheObjectSerializerTypeName)
		{
			IDataCacheObjectSerializer dataCacheObjectSerializer = null;
			if (cacheObjectSerializerType == DataCacheObjectSerializerType.CustomSerializer)
			{
				if (string.IsNullOrEmpty(customCacheObjectSerializerTypeName))
				{
					throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "CustomSerializerNotSpecified"));
				}
				Type type = Type.GetType(customCacheObjectSerializerTypeName, false);
				if (type == null)
				{
					throw new ArgumentNullException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "CustomSerializerNotSpecified"));
				}
				dataCacheObjectSerializer = Activator.CreateInstance(type) as IDataCacheObjectSerializer;
			}
			this.Initialize(cacheObjectSerializerType, dataCacheObjectSerializer);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00035BD8 File Offset: 0x00033DD8
		public DataCacheSerializationProperties(DataCacheObjectSerializerType cacheObjectSerializerType, IDataCacheObjectSerializer customCacheObjectSerializer)
		{
			this.Initialize(cacheObjectSerializerType, customCacheObjectSerializer);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x00035BE8 File Offset: 0x00033DE8
		private void Initialize(DataCacheObjectSerializerType cacheObjectSerializerType, IDataCacheObjectSerializer customCacheObjectSerializer)
		{
			this.CacheObjectSerializerType = cacheObjectSerializerType;
			if (cacheObjectSerializerType == DataCacheObjectSerializerType.CustomSerializer)
			{
				if (customCacheObjectSerializer == null)
				{
					throw new ArgumentNullException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "CustomSerializerNotSpecified"));
				}
				this.CustomCacheObjectSerializer = customCacheObjectSerializer;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00035C14 File Offset: 0x00033E14
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x00035C1C File Offset: 0x00033E1C
		public DataCacheObjectSerializerType CacheObjectSerializerType { get; private set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00035C25 File Offset: 0x00033E25
		// (set) Token: 0x06000FA9 RID: 4009 RVA: 0x00035C2D File Offset: 0x00033E2D
		public IDataCacheObjectSerializer CustomCacheObjectSerializer { get; private set; }
	}
}
