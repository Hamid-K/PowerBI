using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200020F RID: 527
	[Serializable]
	internal class TryConvertCall : PrimaryExpression
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x00161C0C File Offset: 0x0015FE0C
		// (set) Token: 0x06002470 RID: 9328 RVA: 0x00161C14 File Offset: 0x0015FE14
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

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x00161C24 File Offset: 0x0015FE24
		// (set) Token: 0x06002472 RID: 9330 RVA: 0x00161C2C File Offset: 0x0015FE2C
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x00161C3C File Offset: 0x0015FE3C
		// (set) Token: 0x06002474 RID: 9332 RVA: 0x00161C44 File Offset: 0x0015FE44
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

		// Token: 0x06002475 RID: 9333 RVA: 0x00161C54 File Offset: 0x0015FE54
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x00161C60 File Offset: 0x0015FE60
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

		// Token: 0x04001AC2 RID: 6850
		private DataTypeReference _dataType;

		// Token: 0x04001AC3 RID: 6851
		private ScalarExpression _parameter;

		// Token: 0x04001AC4 RID: 6852
		private ScalarExpression _style;
	}
}
