using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000403 RID: 1027
	[Serializable]
	internal class AlterLoginOptionsStatement : AlterLoginStatement
	{
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x0016E28C File Offset: 0x0016C48C
		public IList<PrincipalOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x0016E294 File Offset: 0x0016C494
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x0016E2A0 File Offset: 0x0016C4A0
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001E21 RID: 7713
		private List<PrincipalOption> _options = new List<PrincipalOption>();
	}
}
