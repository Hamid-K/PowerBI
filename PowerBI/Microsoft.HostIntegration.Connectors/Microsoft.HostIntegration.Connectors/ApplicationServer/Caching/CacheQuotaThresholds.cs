using System;
using System.Configuration;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000141 RID: 321
	[Serializable]
	internal sealed class CacheQuotaThresholds : ConfigurationElement, ISerializable
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x0002011F File Offset: 0x0001E31F
		public CacheQuotaThresholds()
		{
			this.InitializeDefault();
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0002088F File Offset: 0x0001EA8F
		public CacheQuotaThresholds(int dataTransfer, int transaction, int splitFactor)
		{
			this.PerNodeTransferLimit = dataTransfer;
			this.PerNodeTransactionLimit = transaction;
			this.NodeSplitFactor = splitFactor;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x000208AC File Offset: 0x0001EAAC
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x000208BE File Offset: 0x0001EABE
		[ConfigurationProperty("perNodeTransactionLimit", DefaultValue = 4500, IsRequired = false)]
		public int PerNodeTransactionLimit
		{
			get
			{
				return (int)base["perNodeTransactionLimit"];
			}
			set
			{
				base["perNodeTransactionLimit"] = value;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x000208D1 File Offset: 0x0001EAD1
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x000208E3 File Offset: 0x0001EAE3
		[ConfigurationProperty("perNodeTransferLimit", DefaultValue = 16, IsRequired = false)]
		public int PerNodeTransferLimit
		{
			get
			{
				return (int)base["perNodeTransferLimit"];
			}
			set
			{
				base["perNodeTransferLimit"] = value;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x000208F6 File Offset: 0x0001EAF6
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x00020908 File Offset: 0x0001EB08
		[ConfigurationProperty("nodeSplitFactor", DefaultValue = 5, IsRequired = false)]
		public int NodeSplitFactor
		{
			get
			{
				return (int)base["nodeSplitFactor"];
			}
			set
			{
				base["nodeSplitFactor"] = value;
			}
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0002091B File Offset: 0x0001EB1B
		public CacheQuotaThresholds(SerializationInfo info, StreamingContext context)
		{
			this.PerNodeTransactionLimit = info.GetInt32("perNodeTransactionLimit");
			this.PerNodeTransferLimit = info.GetInt32("perNodeTransferLimit");
			this.NodeSplitFactor = info.GetInt32("nodeSplitFactor");
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00020958 File Offset: 0x0001EB58
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("perNodeTransactionLimit", base["perNodeTransactionLimit"]);
			info.AddValue("perNodeTransferLimit", base["perNodeTransferLimit"]);
			info.AddValue("nodeSplitFactor", base["nodeSplitFactor"]);
		}

		// Token: 0x040006E3 RID: 1763
		internal const string PER_NODE_TRANSFER_LIMIT = "perNodeTransferLimit";

		// Token: 0x040006E4 RID: 1764
		internal const string PER_NODE_TRANSACTION_LIMIT = "perNodeTransactionLimit";

		// Token: 0x040006E5 RID: 1765
		internal const string NODE_SPLIT_FACTOR = "nodeSplitFactor";
	}
}
