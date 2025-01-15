using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200028F RID: 655
	[Serializable]
	internal class DropClusteredConstraintMoveOption : DropClusteredConstraintOption
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x00164FCE File Offset: 0x001631CE
		// (set) Token: 0x06002764 RID: 10084 RVA: 0x00164FD6 File Offset: 0x001631D6
		public FileGroupOrPartitionScheme OptionValue
		{
			get
			{
				return this._optionValue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._optionValue = value;
			}
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x00164FE6 File Offset: 0x001631E6
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002766 RID: 10086 RVA: 0x00164FF2 File Offset: 0x001631F2
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.OptionValue != null)
			{
				this.OptionValue.Accept(visitor);
			}
		}

		// Token: 0x04001B93 RID: 7059
		private FileGroupOrPartitionScheme _optionValue;
	}
}
