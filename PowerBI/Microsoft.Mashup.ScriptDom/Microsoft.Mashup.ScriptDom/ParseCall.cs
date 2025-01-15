using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000210 RID: 528
	[Serializable]
	internal class ParseCall : PrimaryExpression
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x00161CB8 File Offset: 0x0015FEB8
		// (set) Token: 0x06002479 RID: 9337 RVA: 0x00161CC0 File Offset: 0x0015FEC0
		public ScalarExpression StringValue
		{
			get
			{
				return this._stringValue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._stringValue = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x00161CD0 File Offset: 0x0015FED0
		// (set) Token: 0x0600247B RID: 9339 RVA: 0x00161CD8 File Offset: 0x0015FED8
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

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x00161CE8 File Offset: 0x0015FEE8
		// (set) Token: 0x0600247D RID: 9341 RVA: 0x00161CF0 File Offset: 0x0015FEF0
		public ScalarExpression Culture
		{
			get
			{
				return this._culture;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._culture = value;
			}
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x00161D00 File Offset: 0x0015FF00
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00161D0C File Offset: 0x0015FF0C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.StringValue != null)
			{
				this.StringValue.Accept(visitor);
			}
			if (this.DataType != null)
			{
				this.DataType.Accept(visitor);
			}
			if (this.Culture != null)
			{
				this.Culture.Accept(visitor);
			}
		}

		// Token: 0x04001AC5 RID: 6853
		private ScalarExpression _stringValue;

		// Token: 0x04001AC6 RID: 6854
		private DataTypeReference _dataType;

		// Token: 0x04001AC7 RID: 6855
		private ScalarExpression _culture;
	}
}
