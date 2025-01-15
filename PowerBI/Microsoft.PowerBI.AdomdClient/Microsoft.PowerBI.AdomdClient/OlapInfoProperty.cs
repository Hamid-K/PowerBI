using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E6 RID: 230
	public sealed class OlapInfoProperty
	{
		// Token: 0x06000C93 RID: 3219 RVA: 0x0002F18C File Offset: 0x0002D38C
		internal OlapInfoProperty(DataColumn propertyColumn)
		{
			this.propertyColumn = propertyColumn;
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0002F19B File Offset: 0x0002D39B
		public string Name
		{
			get
			{
				return this.propertyColumn.ExtendedProperties["MemberPropertyUnqualifiedName"] as string;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0002F1B7 File Offset: 0x0002D3B7
		public string Namespace
		{
			get
			{
				return this.propertyColumn.Namespace;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0002F1C4 File Offset: 0x0002D3C4
		public Type Type
		{
			get
			{
				return FormattersHelpers.GetColumnType(this.propertyColumn);
			}
		}

		// Token: 0x04000804 RID: 2052
		private DataColumn propertyColumn;
	}
}
