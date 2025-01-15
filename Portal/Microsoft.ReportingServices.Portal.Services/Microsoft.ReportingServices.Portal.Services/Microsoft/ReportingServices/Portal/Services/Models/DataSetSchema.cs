using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.Portal.Interfaces.Models;

namespace Microsoft.ReportingServices.Portal.Services.Models
{
	// Token: 0x02000056 RID: 86
	internal sealed class DataSetSchema : IDataSetSchema
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x000128FA File Offset: 0x00010AFA
		internal DataSetSchema()
		{
			this.Fields = Enumerable.Empty<IDataSetField>();
			this.Parameters = Enumerable.Empty<IDataSetParameter>();
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00012918 File Offset: 0x00010B18
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00012920 File Offset: 0x00010B20
		public string Name { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00012929 File Offset: 0x00010B29
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00012931 File Offset: 0x00010B31
		public IEnumerable<IDataSetField> Fields { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0001293A File Offset: 0x00010B3A
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00012942 File Offset: 0x00010B42
		public IEnumerable<IDataSetParameter> Parameters { get; set; }
	}
}
