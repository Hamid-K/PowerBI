using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000234 RID: 564
	[Serializable]
	internal class ProcedureParameter : DeclareVariableElement
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x00162BBE File Offset: 0x00160DBE
		// (set) Token: 0x06002548 RID: 9544 RVA: 0x00162BC6 File Offset: 0x00160DC6
		public bool IsVarying
		{
			get
			{
				return this._isVarying;
			}
			set
			{
				this._isVarying = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x00162BCF File Offset: 0x00160DCF
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x00162BD7 File Offset: 0x00160DD7
		public ParameterModifier Modifier
		{
			get
			{
				return this._modifier;
			}
			set
			{
				this._modifier = value;
			}
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x00162BE0 File Offset: 0x00160DE0
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600254C RID: 9548 RVA: 0x00162BEC File Offset: 0x00160DEC
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001AFB RID: 6907
		private bool _isVarying;

		// Token: 0x04001AFC RID: 6908
		private ParameterModifier _modifier;
	}
}
