using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200027F RID: 639
	[Serializable]
	internal class AlterTableRebuildStatement : AlterTableStatement
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x00164A5E File Offset: 0x00162C5E
		// (set) Token: 0x06002709 RID: 9993 RVA: 0x00164A66 File Offset: 0x00162C66
		public PartitionSpecifier Partition
		{
			get
			{
				return this._partition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._partition = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x00164A76 File Offset: 0x00162C76
		public IList<IndexOption> IndexOptions
		{
			get
			{
				return this._indexOptions;
			}
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x00164A7E File Offset: 0x00162C7E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x00164A8C File Offset: 0x00162C8C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (base.SchemaObjectName != null)
			{
				base.SchemaObjectName.Accept(visitor);
			}
			if (this.Partition != null)
			{
				this.Partition.Accept(visitor);
			}
			int i = 0;
			int count = this.IndexOptions.Count;
			while (i < count)
			{
				this.IndexOptions[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001B7B RID: 7035
		private PartitionSpecifier _partition;

		// Token: 0x04001B7C RID: 7036
		private List<IndexOption> _indexOptions = new List<IndexOption>();
	}
}
