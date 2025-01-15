using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000144 RID: 324
	[Serializable]
	internal sealed class TransactionQuotaConfig : ConfigurationElement, ISerializable
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x0002011F File Offset: 0x0001E31F
		public TransactionQuotaConfig()
		{
			this.InitializeDefault();
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00020B5E File Offset: 0x0001ED5E
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x00020B70 File Offset: 0x0001ED70
		[ConfigurationProperty("transactionCount", DefaultValue = 810000L, IsRequired = false)]
		public long TransactionCount
		{
			get
			{
				return (long)base["transactionCount"];
			}
			set
			{
				base["transactionCount"] = value;
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00020B83 File Offset: 0x0001ED83
		public TransactionQuotaConfig(SerializationInfo info, StreamingContext context)
		{
			this.TransactionCount = info.GetInt64("transactionCount");
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00020B9C File Offset: 0x0001ED9C
		public TransactionQuotaConfig(long transCnt)
		{
			this.TransactionCount = transCnt;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00020BAB File Offset: 0x0001EDAB
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("transactionCount", base["transactionCount"]);
		}

		// Token: 0x040006EB RID: 1771
		private const string TRANSACTION = "transactionCount";

		// Token: 0x040006EC RID: 1772
		internal const long DEFAULT_TRANSACTIONS_DEFCACHE = 810000L;
	}
}
