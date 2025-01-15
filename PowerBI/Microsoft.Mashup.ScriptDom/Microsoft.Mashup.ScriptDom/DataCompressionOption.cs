using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000343 RID: 835
	[Serializable]
	internal class DataCompressionOption : IndexOption
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x00169363 File Offset: 0x00167563
		// (set) Token: 0x06002BA1 RID: 11169 RVA: 0x0016936B File Offset: 0x0016756B
		public DataCompressionLevel CompressionLevel
		{
			get
			{
				return this._compressionLevel;
			}
			set
			{
				this._compressionLevel = value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x00169374 File Offset: 0x00167574
		public IList<CompressionPartitionRange> PartitionRanges
		{
			get
			{
				return this._partitionRanges;
			}
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0016937C File Offset: 0x0016757C
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x00169388 File Offset: 0x00167588
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.PartitionRanges.Count;
			while (i < count)
			{
				this.PartitionRanges[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001CC1 RID: 7361
		private DataCompressionLevel _compressionLevel;

		// Token: 0x04001CC2 RID: 7362
		private List<CompressionPartitionRange> _partitionRanges = new List<CompressionPartitionRange>();
	}
}
