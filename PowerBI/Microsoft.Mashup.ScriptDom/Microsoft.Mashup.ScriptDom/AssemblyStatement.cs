using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000273 RID: 627
	[Serializable]
	internal abstract class AssemblyStatement : TSqlStatement
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0016454A File Offset: 0x0016274A
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x00164552 File Offset: 0x00162752
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

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x00164562 File Offset: 0x00162762
		public IList<ScalarExpression> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x0016456A File Offset: 0x0016276A
		public IList<AssemblyOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x00164574 File Offset: 0x00162774
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
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
			int j = 0;
			int count2 = this.Options.Count;
			while (j < count2)
			{
				this.Options[j].Accept(visitor);
				j++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001B67 RID: 7015
		private Identifier _name;

		// Token: 0x04001B68 RID: 7016
		private List<ScalarExpression> _parameters = new List<ScalarExpression>();

		// Token: 0x04001B69 RID: 7017
		private List<AssemblyOption> _options = new List<AssemblyOption>();
	}
}
