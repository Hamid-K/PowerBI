using System;
using System.Collections.Generic;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000136 RID: 310
	internal sealed class TmdlCollectionValue : TmdlValue
	{
		// Token: 0x060014C6 RID: 5318 RVA: 0x0008C842 File Offset: 0x0008AA42
		public TmdlCollectionValue()
			: base(TmdlValueType.Collection, string.Empty, true, false)
		{
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0008C852 File Offset: 0x0008AA52
		public bool IsEmpty
		{
			get
			{
				return this.items == null || this.items.Count == 0;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0008C86C File Offset: 0x0008AA6C
		public ICollection<TmdlProperty[]> Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new List<TmdlProperty[]>();
				}
				return this.items;
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0008C887 File Offset: 0x0008AA87
		private protected override void WriteBody(ITmdlWriter writer)
		{
		}

		// Token: 0x04000360 RID: 864
		private List<TmdlProperty[]> items;
	}
}
