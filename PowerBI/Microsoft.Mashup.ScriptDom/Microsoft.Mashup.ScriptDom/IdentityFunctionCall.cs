using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003BF RID: 959
	[Serializable]
	internal class IdentityFunctionCall : ScalarExpression
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x0016CA63 File Offset: 0x0016AC63
		// (set) Token: 0x06002EC1 RID: 11969 RVA: 0x0016CA6B File Offset: 0x0016AC6B
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

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x0016CA7B File Offset: 0x0016AC7B
		// (set) Token: 0x06002EC3 RID: 11971 RVA: 0x0016CA83 File Offset: 0x0016AC83
		public ScalarExpression Seed
		{
			get
			{
				return this._seed;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._seed = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x0016CA93 File Offset: 0x0016AC93
		// (set) Token: 0x06002EC5 RID: 11973 RVA: 0x0016CA9B File Offset: 0x0016AC9B
		public ScalarExpression Increment
		{
			get
			{
				return this._increment;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._increment = value;
			}
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x0016CAAB File Offset: 0x0016ACAB
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x0016CAB8 File Offset: 0x0016ACB8
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.DataType != null)
			{
				this.DataType.Accept(visitor);
			}
			if (this.Seed != null)
			{
				this.Seed.Accept(visitor);
			}
			if (this.Increment != null)
			{
				this.Increment.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001DB7 RID: 7607
		private DataTypeReference _dataType;

		// Token: 0x04001DB8 RID: 7608
		private ScalarExpression _seed;

		// Token: 0x04001DB9 RID: 7609
		private ScalarExpression _increment;
	}
}
