using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.ReportingServices.Design.Serialization;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200041C RID: 1052
	public sealed class TableHeader : IVoluntarySerializable
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x00080FE2 File Offset: 0x0007F1E2
		// (set) Token: 0x06002175 RID: 8565 RVA: 0x00080FEA File Offset: 0x0007F1EA
		[DefaultValue(false)]
		public bool FixedHeader
		{
			get
			{
				return this.m_fixedHeader;
			}
			set
			{
				this.m_fixedHeader = value;
			}
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x00080FF3 File Offset: 0x0007F1F3
		bool IVoluntarySerializable.ShouldBeSerialized()
		{
			return this.TableRows != null && this.TableRows.Count > 0;
		}

		// Token: 0x04000EAA RID: 3754
		public List<TableRow> TableRows;

		// Token: 0x04000EAB RID: 3755
		private bool m_fixedHeader;

		// Token: 0x04000EAC RID: 3756
		[DefaultValue(false)]
		public bool RepeatOnNewPage;
	}
}
