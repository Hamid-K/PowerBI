using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200020D RID: 525
	[Serializable]
	internal class SchemaDeclarationItem : TSqlFragment
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x00161AEB File Offset: 0x0015FCEB
		// (set) Token: 0x06002460 RID: 9312 RVA: 0x00161AF3 File Offset: 0x0015FCF3
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

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x00161B03 File Offset: 0x0015FD03
		// (set) Token: 0x06002462 RID: 9314 RVA: 0x00161B0B File Offset: 0x0015FD0B
		public ValueExpression Mapping
		{
			get
			{
				return this._mapping;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._mapping = value;
			}
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00161B1B File Offset: 0x0015FD1B
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002464 RID: 9316 RVA: 0x00161B27 File Offset: 0x0015FD27
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.ColumnDefinition != null)
			{
				this.ColumnDefinition.Accept(visitor);
			}
			if (this.Mapping != null)
			{
				this.Mapping.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001ABD RID: 6845
		private ColumnDefinitionBase _columnDefinition;

		// Token: 0x04001ABE RID: 6846
		private ValueExpression _mapping;
	}
}
