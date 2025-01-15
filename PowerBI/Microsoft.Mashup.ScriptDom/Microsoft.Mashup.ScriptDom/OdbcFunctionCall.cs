using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200021F RID: 543
	[Serializable]
	internal class OdbcFunctionCall : PrimaryExpression
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060024D9 RID: 9433 RVA: 0x001623D4 File Offset: 0x001605D4
		// (set) Token: 0x060024DA RID: 9434 RVA: 0x001623DC File Offset: 0x001605DC
		public Identifier Name
		{
			get
			{
				return this._name;
			}
			set
			{
				base.UpdateTokenInfo(value);
				this._name = value;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060024DB RID: 9435 RVA: 0x001623EC File Offset: 0x001605EC
		// (set) Token: 0x060024DC RID: 9436 RVA: 0x001623F4 File Offset: 0x001605F4
		public bool ParametersUsed
		{
			get
			{
				return this._parametersUsed;
			}
			set
			{
				this._parametersUsed = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060024DD RID: 9437 RVA: 0x001623FD File Offset: 0x001605FD
		public IList<ScalarExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x00162405 File Offset: 0x00160605
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x00162414 File Offset: 0x00160614
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			if (this.Name != null)
			{
				this.Name.Accept(visitor);
			}
			int i = 0;
			int count = this.Parameters.Count;
			while (i < count)
			{
				this.Parameters[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001AE2 RID: 6882
		private Identifier _name;

		// Token: 0x04001AE3 RID: 6883
		private bool _parametersUsed;

		// Token: 0x04001AE4 RID: 6884
		private List<ScalarExpression> _parameters = new List<ScalarExpression>();
	}
}
