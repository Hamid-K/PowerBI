using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E6 RID: 230
	public sealed class OlapInfoProperty
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002F4BC File Offset: 0x0002D6BC
		internal OlapInfoProperty(DataColumn propertyColumn)
		{
			this.propertyColumn = propertyColumn;
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0002F4CB File Offset: 0x0002D6CB
		public string Name
		{
			get
			{
				return this.propertyColumn.ExtendedProperties["MemberPropertyUnqualifiedName"] as string;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002F4E7 File Offset: 0x0002D6E7
		public string Namespace
		{
			get
			{
				return this.propertyColumn.Namespace;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0002F4F4 File Offset: 0x0002D6F4
		public Type Type
		{
			get
			{
				return FormattersHelpers.GetColumnType(this.propertyColumn);
			}
		}

		// Token: 0x04000811 RID: 2065
		private DataColumn propertyColumn;
	}
}
