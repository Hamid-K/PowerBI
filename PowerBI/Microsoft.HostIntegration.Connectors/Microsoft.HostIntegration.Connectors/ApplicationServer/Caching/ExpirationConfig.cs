using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000137 RID: 311
	[Serializable]
	internal class ExpirationConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x06000947 RID: 2375 RVA: 0x00015607 File Offset: 0x00013807
		public ExpirationConfig()
		{
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0001FF89 File Offset: 0x0001E189
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x0001FF9B File Offset: 0x0001E19B
		[LongValidator(MinValue = 0L)]
		[ConfigurationProperty("defaultTTL", DefaultValue = 10L, IsRequired = true)]
		public long DefaultTTL
		{
			get
			{
				return (long)base["defaultTTL"];
			}
			set
			{
				base["defaultTTL"] = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0001FFAE File Offset: 0x0001E1AE
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		[ConfigurationProperty("isExpirable", DefaultValue = true, IsRequired = true)]
		public bool IsExpirable
		{
			get
			{
				return (bool)base["isExpirable"];
			}
			set
			{
				base["isExpirable"] = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x0001FFD3 File Offset: 0x0001E1D3
		// (set) Token: 0x0600094D RID: 2381 RVA: 0x0001FFE5 File Offset: 0x0001E1E5
		[ConfigurationProperty("type", DefaultValue = ExpirationType.NotProvided, IsRequired = false)]
		public ExpirationType Type
		{
			get
			{
				return (ExpirationType)base["type"];
			}
			set
			{
				base["type"] = value;
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001FFF8 File Offset: 0x0001E1F8
		protected ExpirationConfig(SerializationInfo info, StreamingContext context)
		{
			this.IsExpirable = info.GetBoolean("isExpirable");
			this.DefaultTTL = (long)info.GetDouble("defaultTTL");
			try
			{
				this.Type = (ExpirationType)info.GetInt32("type");
			}
			catch (SerializationException)
			{
				this.Type = (this.IsExpirable ? ExpirationType.AbsoluteExpiration : ExpirationType.None);
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00020068 File Offset: 0x0001E268
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("defaultTTL", this.DefaultTTL);
			info.AddValue("isExpirable", this.IsExpirable);
			info.AddValue("type", (int)this.Type);
		}

		// Token: 0x040006C4 RID: 1732
		internal const string DEFAULT_TTL = "defaultTTL";

		// Token: 0x040006C5 RID: 1733
		internal const string IS_EXPIRABLE = "isExpirable";

		// Token: 0x040006C6 RID: 1734
		internal const string TYPE = "type";

		// Token: 0x040006C7 RID: 1735
		internal const long DEFAULT_TTL_VALUE = 10L;
	}
}
