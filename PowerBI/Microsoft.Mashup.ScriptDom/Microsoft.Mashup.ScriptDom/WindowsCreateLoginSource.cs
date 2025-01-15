using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020003FE RID: 1022
	[Serializable]
	internal class WindowsCreateLoginSource : CreateLoginSource
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x0016E057 File Offset: 0x0016C257
		public IList<PrincipalOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x0016E05F File Offset: 0x0016C25F
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x0016E06C File Offset: 0x0016C26C
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.Options.Count;
			while (i < count)
			{
				this.Options[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001E16 RID: 7702
		private List<PrincipalOption> _options = new List<PrincipalOption>();
	}
}
