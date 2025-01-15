using System;
using Microsoft.ReportingServices.Portal.Interfaces.Models;

namespace Microsoft.ReportingServices.Portal.Services.Models
{
	// Token: 0x02000054 RID: 84
	internal sealed class DataSetParameter : IDataSetParameter
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00012872 File Offset: 0x00010A72
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0001287A File Offset: 0x00010A7A
		public string Name { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00012883 File Offset: 0x00010A83
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0001288B File Offset: 0x00010A8B
		public string DefaultValue { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00012894 File Offset: 0x00010A94
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0001289C File Offset: 0x00010A9C
		public bool Nullable { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000128A5 File Offset: 0x00010AA5
		// (set) Token: 0x060002BA RID: 698 RVA: 0x000128AD File Offset: 0x00010AAD
		public string DataType { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000128B6 File Offset: 0x00010AB6
		// (set) Token: 0x060002BC RID: 700 RVA: 0x000128BE File Offset: 0x00010ABE
		public bool IsExpression { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000128C7 File Offset: 0x00010AC7
		// (set) Token: 0x060002BE RID: 702 RVA: 0x000128CF File Offset: 0x00010ACF
		public bool IsMultiValued { get; set; }
	}
}
