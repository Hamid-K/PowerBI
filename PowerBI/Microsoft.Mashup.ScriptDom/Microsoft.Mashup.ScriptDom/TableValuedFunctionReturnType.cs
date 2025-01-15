using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001D2 RID: 466
	[Serializable]
	internal class TableValuedFunctionReturnType : FunctionReturnType
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x0015FFF6 File Offset: 0x0015E1F6
		// (set) Token: 0x060022F0 RID: 8944 RVA: 0x0015FFFE File Offset: 0x0015E1FE
		public DeclareTableVariableBody DeclareTableVariableBody
		{
			get
			{
				return this._declareTableVariableBody;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._declareTableVariableBody = value;
			}
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0016000E File Offset: 0x0015E20E
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0016001A File Offset: 0x0015E21A
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.DeclareTableVariableBody != null)
			{
				this.DeclareTableVariableBody.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001A4C RID: 6732
		private DeclareTableVariableBody _declareTableVariableBody;
	}
}
