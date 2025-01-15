using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000238 RID: 568
	[Serializable]
	internal abstract class DataModificationSpecification : TSqlFragment
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x00162CCC File Offset: 0x00160ECC
		// (set) Token: 0x0600255D RID: 9565 RVA: 0x00162CD4 File Offset: 0x00160ED4
		public TableReference Target
		{
			get
			{
				return this._target;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._target = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600255E RID: 9566 RVA: 0x00162CE4 File Offset: 0x00160EE4
		// (set) Token: 0x0600255F RID: 9567 RVA: 0x00162CEC File Offset: 0x00160EEC
		public TopRowFilter TopRowFilter
		{
			get
			{
				return this._topRowFilter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._topRowFilter = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06002560 RID: 9568 RVA: 0x00162CFC File Offset: 0x00160EFC
		// (set) Token: 0x06002561 RID: 9569 RVA: 0x00162D04 File Offset: 0x00160F04
		public OutputIntoClause OutputIntoClause
		{
			get
			{
				return this._outputIntoClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._outputIntoClause = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06002562 RID: 9570 RVA: 0x00162D14 File Offset: 0x00160F14
		// (set) Token: 0x06002563 RID: 9571 RVA: 0x00162D1C File Offset: 0x00160F1C
		public OutputClause OutputClause
		{
			get
			{
				return this._outputClause;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._outputClause = value;
			}
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00162D2C File Offset: 0x00160F2C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Target != null)
			{
				this.Target.Accept(visitor);
			}
			if (this.TopRowFilter != null)
			{
				this.TopRowFilter.Accept(visitor);
			}
			if (this.OutputIntoClause != null)
			{
				this.OutputIntoClause.Accept(visitor);
			}
			if (this.OutputClause != null)
			{
				this.OutputClause.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B00 RID: 6912
		private TableReference _target;

		// Token: 0x04001B01 RID: 6913
		private TopRowFilter _topRowFilter;

		// Token: 0x04001B02 RID: 6914
		private OutputIntoClause _outputIntoClause;

		// Token: 0x04001B03 RID: 6915
		private OutputClause _outputClause;
	}
}
