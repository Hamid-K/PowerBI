using System;
using Microsoft.ReportingServices.Portal.Interfaces.Models;

namespace Microsoft.ReportingServices.Portal.Services.Models
{
	// Token: 0x02000055 RID: 85
	internal sealed class DataSetField : IDataSetField
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x000128D8 File Offset: 0x00010AD8
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x000128E0 File Offset: 0x00010AE0
		public string Name { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x000128E9 File Offset: 0x00010AE9
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x000128F1 File Offset: 0x00010AF1
		public string DataType { get; set; }
	}
}
