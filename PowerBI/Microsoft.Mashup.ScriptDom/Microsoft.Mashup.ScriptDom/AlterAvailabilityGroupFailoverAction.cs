using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000499 RID: 1177
	[Serializable]
	internal class AlterAvailabilityGroupFailoverAction : AlterAvailabilityGroupAction
	{
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600338D RID: 13197 RVA: 0x00171445 File Offset: 0x0016F645
		public IList<AlterAvailabilityGroupFailoverOption> Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x0017144D File Offset: 0x0016F64D
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x0017145C File Offset: 0x0016F65C
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

		// Token: 0x04001EFA RID: 7930
		private List<AlterAvailabilityGroupFailoverOption> _options = new List<AlterAvailabilityGroupFailoverOption>();
	}
}
