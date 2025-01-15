using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001FB RID: 507
	[Serializable]
	internal class VariableValuePair : TSqlFragment
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060023D1 RID: 9169 RVA: 0x00160FD1 File Offset: 0x0015F1D1
		// (set) Token: 0x060023D2 RID: 9170 RVA: 0x00160FD9 File Offset: 0x0015F1D9
		public VariableReference Variable
		{
			get
			{
				return this._variable;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._variable = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x00160FE9 File Offset: 0x0015F1E9
		// (set) Token: 0x060023D4 RID: 9172 RVA: 0x00160FF1 File Offset: 0x0015F1F1
		public ScalarExpression Value
		{
			get
			{
				return this._value;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._value = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x00161001 File Offset: 0x0015F201
		// (set) Token: 0x060023D6 RID: 9174 RVA: 0x00161009 File Offset: 0x0015F209
		public bool IsForUnknown
		{
			get
			{
				return this._isForUnknown;
			}
			set
			{
				this._isForUnknown = value;
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00161012 File Offset: 0x0015F212
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023D8 RID: 9176 RVA: 0x0016101E File Offset: 0x0015F21E
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Variable != null)
			{
				this.Variable.Accept(visitor);
			}
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A8C RID: 6796
		private VariableReference _variable;

		// Token: 0x04001A8D RID: 6797
		private ScalarExpression _value;

		// Token: 0x04001A8E RID: 6798
		private bool _isForUnknown;
	}
}
