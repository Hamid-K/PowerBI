using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000145 RID: 325
	[Serializable]
	internal sealed class DataTransferQuotaConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x0002011F File Offset: 0x0001E31F
		public DataTransferQuotaConfig()
		{
			this.InitializeDefault();
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00020BC3 File Offset: 0x0001EDC3
		public DataTransferQuotaConfig(long dataLmt)
		{
			this.DataTransferCount = dataLmt;
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x00020BD2 File Offset: 0x0001EDD2
		// (set) Token: 0x060009C2 RID: 2498 RVA: 0x00020BE4 File Offset: 0x0001EDE4
		[ConfigurationProperty("dataTransferCount", DefaultValue = 2880L, IsRequired = false)]
		public long DataTransferCount
		{
			get
			{
				return (long)base["dataTransferCount"];
			}
			set
			{
				base["dataTransferCount"] = value;
			}
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00020BF7 File Offset: 0x0001EDF7
		public DataTransferQuotaConfig(SerializationInfo info, StreamingContext context)
		{
			this.DataTransferCount = info.GetInt64("dataTransferCount");
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00020C10 File Offset: 0x0001EE10
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("dataTransferCount", base["dataTransferCount"]);
		}

		// Token: 0x040006ED RID: 1773
		private const string DATATRANSFER = "dataTransferCount";

		// Token: 0x040006EE RID: 1774
		internal const long DEFAULT_DATATRANSFER = 2880L;
	}
}
