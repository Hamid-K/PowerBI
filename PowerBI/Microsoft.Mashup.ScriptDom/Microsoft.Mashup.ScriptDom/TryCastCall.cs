using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000213 RID: 531
	[Serializable]
	internal class TryCastCall : PrimaryExpression
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06002491 RID: 9361 RVA: 0x00161E85 File Offset: 0x00160085
		// (set) Token: 0x06002492 RID: 9362 RVA: 0x00161E8D File Offset: 0x0016008D
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

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06002493 RID: 9363 RVA: 0x00161E9D File Offset: 0x0016009D
		// (set) Token: 0x06002494 RID: 9364 RVA: 0x00161EA5 File Offset: 0x001600A5
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

		// Token: 0x06002495 RID: 9365 RVA: 0x00161EB5 File Offset: 0x001600B5
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00161EC1 File Offset: 0x001600C1
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

		// Token: 0x04001ACD RID: 6861
		private DataTypeReference _dataType;

		// Token: 0x04001ACE RID: 6862
		private ScalarExpression _parameter;
	}
}
