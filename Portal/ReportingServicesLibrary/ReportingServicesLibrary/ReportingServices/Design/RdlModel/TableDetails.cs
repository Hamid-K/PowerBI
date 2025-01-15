using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200041F RID: 1055
	public sealed class TableDetails : IVoluntarySerializable
	{
		// Token: 0x06002183 RID: 8579 RVA: 0x000810CE File Offset: 0x0007F2CE
		bool IVoluntarySerializable.ShouldBeSerialized()
		{
			return this.TableRows != null && this.TableRows.Count > 0;
		}

		// Token: 0x04000EB2 RID: 3762
		public List<TableRow> TableRows = new List<TableRow>();

		// Token: 0x04000EB3 RID: 3763
		public Grouping Grouping;

		// Token: 0x04000EB4 RID: 3764
		public Sorting Sorting;

		// Token: 0x04000EB5 RID: 3765
		public Visibility Visibility;
	}
}
