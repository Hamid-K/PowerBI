using System;

namespace Microsoft.MetaAnalytics.RDataSupport
{
	// Token: 0x0200016E RID: 366
	[Serializable]
	public class RObject<T> : RObject
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x0000B61B File Offset: 0x0000981B
		public RObject(T[] values, bool hasMissing = true, bool isRaw = false)
		{
			this.TypedValues = values;
			this.HasMissings = hasMissing;
			base.IsRaw = isRaw;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0000B638 File Offset: 0x00009838
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0000B640 File Offset: 0x00009840
		public T[] TypedValues { get; private set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0000B649 File Offset: 0x00009849
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0000B651 File Offset: 0x00009851
		public bool HasMissings { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0000B65A File Offset: 0x0000985A
		public override Array Values
		{
			get
			{
				return this.TypedValues;
			}
		}
	}
}
