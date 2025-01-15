using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	internal class ResultColumnDefinition : TSqlFragment
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x0015EEE1 File Offset: 0x0015D0E1
		// (set) Token: 0x06002205 RID: 8709 RVA: 0x0015EEE9 File Offset: 0x0015D0E9
		public ColumnDefinitionBase ColumnDefinition
		{
			get
			{
				return this._columnDefinition;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._columnDefinition = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x0015EEF9 File Offset: 0x0015D0F9
		// (set) Token: 0x06002207 RID: 8711 RVA: 0x0015EF01 File Offset: 0x0015D101
		public NullableConstraintDefinition Nullable
		{
			get
			{
				return this._nullable;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._nullable = value;
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x0015EF11 File Offset: 0x0015D111
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x0015EF1D File Offset: 0x0015D11D
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ColumnDefinition != null)
			{
				this.ColumnDefinition.Accept(visitor);
			}
			if (this.Nullable != null)
			{
				this.Nullable.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A07 RID: 6663
		private ColumnDefinitionBase _columnDefinition;

		// Token: 0x04001A08 RID: 6664
		private NullableConstraintDefinition _nullable;
	}
}
