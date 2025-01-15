using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002AA RID: 682
	[Serializable]
	internal class FileGroupOrPartitionScheme : TSqlFragment
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06002807 RID: 10247 RVA: 0x00165B7F File Offset: 0x00163D7F
		// (set) Token: 0x06002808 RID: 10248 RVA: 0x00165B87 File Offset: 0x00163D87
		public IdentifierOrValueExpression Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x00165B97 File Offset: 0x00163D97
		public IList<Identifier> PartitionSchemeColumns
		{
			get
			{
				return this._partitionSchemeColumns;
			}
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x00165B9F File Offset: 0x00163D9F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x00165BAC File Offset: 0x00163DAC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.PartitionSchemeColumns.Count;
			while (i < count)
			{
				this.PartitionSchemeColumns[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001BC2 RID: 7106
		private IdentifierOrValueExpression _name;

		// Token: 0x04001BC3 RID: 7107
		private List<Identifier> _partitionSchemeColumns = new List<Identifier>();
	}
}
