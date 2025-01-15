using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000267 RID: 615
	[Serializable]
	internal class SchemaObjectNameOrValueExpression : TSqlFragment
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x001641B5 File Offset: 0x001623B5
		// (set) Token: 0x06002687 RID: 9863 RVA: 0x001641BD File Offset: 0x001623BD
		public SchemaObjectName SchemaObjectName
		{
			get
			{
				return this._schemaObjectName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._schemaObjectName = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x001641CD File Offset: 0x001623CD
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x001641D5 File Offset: 0x001623D5
		public ValueExpression ValueExpression
		{
			get
			{
				return this._valueExpression;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._valueExpression = value;
			}
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x001641E5 File Offset: 0x001623E5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x001641F1 File Offset: 0x001623F1
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.SchemaObjectName != null)
			{
				this.SchemaObjectName.Accept(visitor);
			}
			if (this.ValueExpression != null)
			{
				this.ValueExpression.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B59 RID: 7001
		private SchemaObjectName _schemaObjectName;

		// Token: 0x04001B5A RID: 7002
		private ValueExpression _valueExpression;
	}
}
