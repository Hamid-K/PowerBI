using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020002FC RID: 764
	[Serializable]
	internal class KillStatement : TSqlStatement
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x001678FD File Offset: 0x00165AFD
		// (set) Token: 0x060029D4 RID: 10708 RVA: 0x00167905 File Offset: 0x00165B05
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

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x00167915 File Offset: 0x00165B15
		// (set) Token: 0x060029D6 RID: 10710 RVA: 0x0016791D File Offset: 0x00165B1D
		public bool WithStatusOnly
		{
			get
			{
				return this._withStatusOnly;
			}
			set
			{
				this._withStatusOnly = value;
			}
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x00167926 File Offset: 0x00165B26
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x00167932 File Offset: 0x00165B32
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			if (this.Parameter != null)
			{
				this.Parameter.Accept(visitor);
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001C3D RID: 7229
		private ScalarExpression _parameter;

		// Token: 0x04001C3E RID: 7230
		private bool _withStatusOnly;
	}
}
