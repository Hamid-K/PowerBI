using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200022F RID: 559
	[Serializable]
	internal class DeclareVariableElement : TSqlFragment
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x00162986 File Offset: 0x00160B86
		// (set) Token: 0x06002528 RID: 9512 RVA: 0x0016298E File Offset: 0x00160B8E
		public Identifier VariableName
		{
			get
			{
				return this._variableName;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._variableName = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x0016299E File Offset: 0x00160B9E
		// (set) Token: 0x0600252A RID: 9514 RVA: 0x001629A6 File Offset: 0x00160BA6
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

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600252B RID: 9515 RVA: 0x001629B6 File Offset: 0x00160BB6
		// (set) Token: 0x0600252C RID: 9516 RVA: 0x001629BE File Offset: 0x00160BBE
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

		// Token: 0x0600252D RID: 9517 RVA: 0x001629CE File Offset: 0x00160BCE
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x001629DC File Offset: 0x00160BDC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.VariableName != null)
			{
				this.VariableName.Accept(visitor);
			}
			if (this.DataType != null)
			{
				this.DataType.Accept(visitor);
			}
			if (this.Value != null)
			{
				this.Value.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AF2 RID: 6898
		private Identifier _variableName;

		// Token: 0x04001AF3 RID: 6899
		private DataTypeReference _dataType;

		// Token: 0x04001AF4 RID: 6900
		private ScalarExpression _value;
	}
}
