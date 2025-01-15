using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class CacheRefreshPlan
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000C559 File Offset: 0x0000A759
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0000C561 File Offset: 0x0000A761
		public string CacheRefreshPlanID
		{
			get
			{
				return this.cacheRefreshPlanIDField;
			}
			set
			{
				this.cacheRefreshPlanIDField = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000C56A File Offset: 0x0000A76A
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000C572 File Offset: 0x0000A772
		public string ItemPath
		{
			get
			{
				return this.itemPathField;
			}
			set
			{
				this.itemPathField = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000C57B File Offset: 0x0000A77B
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000C583 File Offset: 0x0000A783
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000C58C File Offset: 0x0000A78C
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000C594 File Offset: 0x0000A794
		public CacheRefreshPlanState State
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000C59D File Offset: 0x0000A79D
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000C5A5 File Offset: 0x0000A7A5
		public DateTime LastExecuted
		{
			get
			{
				return this.lastExecutedField;
			}
			set
			{
				this.lastExecutedField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000C5AE File Offset: 0x0000A7AE
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000C5B6 File Offset: 0x0000A7B6
		public DateTime ModifiedDate
		{
			get
			{
				return this.modifiedDateField;
			}
			set
			{
				this.modifiedDateField = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000C5BF File Offset: 0x0000A7BF
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0000C5C7 File Offset: 0x0000A7C7
		public string ModifiedBy
		{
			get
			{
				return this.modifiedByField;
			}
			set
			{
				this.modifiedByField = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		public string LastRunStatus
		{
			get
			{
				return this.lastRunStatusField;
			}
			set
			{
				this.lastRunStatusField = value;
			}
		}

		// Token: 0x0400010D RID: 269
		private string cacheRefreshPlanIDField;

		// Token: 0x0400010E RID: 270
		private string itemPathField;

		// Token: 0x0400010F RID: 271
		private string descriptionField;

		// Token: 0x04000110 RID: 272
		private CacheRefreshPlanState stateField;

		// Token: 0x04000111 RID: 273
		private DateTime lastExecutedField;

		// Token: 0x04000112 RID: 274
		private DateTime modifiedDateField;

		// Token: 0x04000113 RID: 275
		private string modifiedByField;

		// Token: 0x04000114 RID: 276
		private string lastRunStatusField;
	}
}
