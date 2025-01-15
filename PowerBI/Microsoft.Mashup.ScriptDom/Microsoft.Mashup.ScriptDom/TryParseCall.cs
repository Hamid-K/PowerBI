using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000211 RID: 529
	[Serializable]
	internal class TryParseCall : PrimaryExpression
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x00161D64 File Offset: 0x0015FF64
		// (set) Token: 0x06002482 RID: 9346 RVA: 0x00161D6C File Offset: 0x0015FF6C
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

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06002483 RID: 9347 RVA: 0x00161D7C File Offset: 0x0015FF7C
		// (set) Token: 0x06002484 RID: 9348 RVA: 0x00161D84 File Offset: 0x0015FF84
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

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06002485 RID: 9349 RVA: 0x00161D94 File Offset: 0x0015FF94
		// (set) Token: 0x06002486 RID: 9350 RVA: 0x00161D9C File Offset: 0x0015FF9C
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

		// Token: 0x06002487 RID: 9351 RVA: 0x00161DAC File Offset: 0x0015FFAC
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x00161DB8 File Offset: 0x0015FFB8
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

		// Token: 0x04001AC8 RID: 6856
		private ScalarExpression _stringValue;

		// Token: 0x04001AC9 RID: 6857
		private DataTypeReference _dataType;

		// Token: 0x04001ACA RID: 6858
		private ScalarExpression _culture;
	}
}
