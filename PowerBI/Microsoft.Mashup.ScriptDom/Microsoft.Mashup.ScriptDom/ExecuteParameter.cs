using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001AF RID: 431
	[Serializable]
	internal class ExecuteParameter : TSqlFragment
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06002222 RID: 8738 RVA: 0x0015F0D2 File Offset: 0x0015D2D2
		// (set) Token: 0x06002223 RID: 8739 RVA: 0x0015F0DA File Offset: 0x0015D2DA
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

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x0015F0EA File Offset: 0x0015D2EA
		// (set) Token: 0x06002225 RID: 8741 RVA: 0x0015F0F2 File Offset: 0x0015D2F2
		public ScalarExpression ParameterValue
		{
			get
			{
				return this._parameterValue;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._parameterValue = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x0015F102 File Offset: 0x0015D302
		// (set) Token: 0x06002227 RID: 8743 RVA: 0x0015F10A File Offset: 0x0015D30A
		public bool IsOutput
		{
			get
			{
				return this._isOutput;
			}
			set
			{
				this._isOutput = value;
			}
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0015F113 File Offset: 0x0015D313
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x0015F11F File Offset: 0x0015D31F
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Variable != null)
			{
				this.Variable.Accept(visitor);
			}
			if (this.ParameterValue != null)
			{
				this.ParameterValue.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A10 RID: 6672
		private VariableReference _variable;

		// Token: 0x04001A11 RID: 6673
		private ScalarExpression _parameterValue;

		// Token: 0x04001A12 RID: 6674
		private bool _isOutput;
	}
}
