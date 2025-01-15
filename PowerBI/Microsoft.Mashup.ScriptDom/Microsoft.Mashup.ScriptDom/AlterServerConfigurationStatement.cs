using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200048A RID: 1162
	[Serializable]
	internal class AlterServerConfigurationStatement : TSqlStatement
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x00170FE1 File Offset: 0x0016F1E1
		// (set) Token: 0x06003343 RID: 13123 RVA: 0x00170FE9 File Offset: 0x0016F1E9
		public ProcessAffinityType ProcessAffinity
		{
			get
			{
				return this._processAffinity;
			}
			set
			{
				this._processAffinity = value;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x00170FF2 File Offset: 0x0016F1F2
		public IList<ProcessAffinityRange> ProcessAffinityRanges
		{
			get
			{
				return this._processAffinityRanges;
			}
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x00170FFA File Offset: 0x0016F1FA
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x00171008 File Offset: 0x0016F208
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			int i = 0;
			int count = this.ProcessAffinityRanges.Count;
			while (i < count)
			{
				this.ProcessAffinityRanges[i].Accept(visitor);
				i++;
			}
			base.AcceptChildren(visitor);
		}

		// Token: 0x04001EE7 RID: 7911
		private ProcessAffinityType _processAffinity;

		// Token: 0x04001EE8 RID: 7912
		private List<ProcessAffinityRange> _processAffinityRanges = new List<ProcessAffinityRange>();
	}
}
