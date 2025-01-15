using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200041D RID: 1053
	public sealed class GroupHeader : IVoluntarySerializable
	{
		// Token: 0x06002178 RID: 8568 RVA: 0x000025F4 File Offset: 0x000007F4
		public GroupHeader()
		{
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0008100D File Offset: 0x0007F20D
		internal GroupHeader(List<TableRow> headerRows, bool repeatOnNewPage)
		{
			this.TableRows = headerRows;
			this.RepeatOnNewPage = repeatOnNewPage;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x00081023 File Offset: 0x0007F223
		bool IVoluntarySerializable.ShouldBeSerialized()
		{
			return this.TableRows != null && this.TableRows.Count > 0;
		}

		// Token: 0x04000EAD RID: 3757
		public List<TableRow> TableRows;

		// Token: 0x04000EAE RID: 3758
		[DefaultValue(false)]
		public bool RepeatOnNewPage;
	}
}
