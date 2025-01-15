using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200020E RID: 526
	[Serializable]
	internal class ConvertCall : PrimaryExpression
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x00161B60 File Offset: 0x0015FD60
		// (set) Token: 0x06002467 RID: 9319 RVA: 0x00161B68 File Offset: 0x0015FD68
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

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x00161B78 File Offset: 0x0015FD78
		// (set) Token: 0x06002469 RID: 9321 RVA: 0x00161B80 File Offset: 0x0015FD80
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

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x00161B90 File Offset: 0x0015FD90
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x00161B98 File Offset: 0x0015FD98
		public ScalarExpression Style
		{
			get
			{
				return this._style;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._style = value;
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00161BA8 File Offset: 0x0015FDA8
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x00161BB4 File Offset: 0x0015FDB4
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
			if (this.Style != null)
			{
				this.Style.Accept(visitor);
			}
		}

		// Token: 0x04001ABF RID: 6847
		private DataTypeReference _dataType;

		// Token: 0x04001AC0 RID: 6848
		private ScalarExpression _parameter;

		// Token: 0x04001AC1 RID: 6849
		private ScalarExpression _style;
	}
}
