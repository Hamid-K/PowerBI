using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000212 RID: 530
	[Serializable]
	internal class CastCall : PrimaryExpression
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x00161E10 File Offset: 0x00160010
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x00161E18 File Offset: 0x00160018
		public DataTypeReference DataType
		{
			get
			{
				return this._dataType;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._dataType = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x00161E28 File Offset: 0x00160028
		// (set) Token: 0x0600248D RID: 9357 RVA: 0x00161E30 File Offset: 0x00160030
		public ScalarExpression Parameter
		{
			get
			{
				return this._parameter;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._parameter = value;
			}
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x00161E40 File Offset: 0x00160040
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x00161E4C File Offset: 0x0016004C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.DataType != null)
			{
				this.DataType.Accept(visitor);
			}
			if (this.Parameter != null)
			{
				this.Parameter.Accept(visitor);
			}
		}

		// Token: 0x04001ACB RID: 6859
		private DataTypeReference _dataType;

		// Token: 0x04001ACC RID: 6860
		private ScalarExpression _parameter;
	}
}
