using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020001FA RID: 506
	[Serializable]
	internal class OptimizeForOptimizerHint : OptimizerHint
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x00160F59 File Offset: 0x0015F159
		public IList<VariableValuePair> Pairs
		{
			get
			{
				return this._pairs;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x00160F61 File Offset: 0x0015F161
		// (set) Token: 0x060023CD RID: 9165 RVA: 0x00160F69 File Offset: 0x0015F169
		public bool IsForUnknown
		{
			get
			{
				return this._isForUnknown;
			}
			set
			{
				this._isForUnknown = value;
			}
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x00160F72 File Offset: 0x0015F172
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x00160F80 File Offset: 0x0015F180
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
			int i = 0;
			int count = this.Pairs.Count;
			while (i < count)
			{
				this.Pairs[i].Accept(visitor);
				i++;
			}
		}

		// Token: 0x04001A8A RID: 6794
		private List<VariableValuePair> _pairs = new List<VariableValuePair>();

		// Token: 0x04001A8B RID: 6795
		private bool _isForUnknown;
	}
}
