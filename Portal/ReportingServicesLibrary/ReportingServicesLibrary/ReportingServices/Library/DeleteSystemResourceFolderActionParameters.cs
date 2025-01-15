using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000254 RID: 596
	internal sealed class DeleteSystemResourceFolderActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0005657F File Offset: 0x0005477F
		// (set) Token: 0x060015C4 RID: 5572 RVA: 0x00056587 File Offset: 0x00054787
		public string ItemPath { get; set; }

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00056590 File Offset: 0x00054790
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00056598 File Offset: 0x00054798
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException("ItemPath");
			}
		}
	}
}
