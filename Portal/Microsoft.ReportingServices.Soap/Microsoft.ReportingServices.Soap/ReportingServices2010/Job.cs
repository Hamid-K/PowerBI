using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2010
{
	// Token: 0x02000008 RID: 8
	[GeneratedCode("wsdl", "2.0.50727.3038")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer")]
	[Serializable]
	public class Job
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000C635 File Offset: 0x0000A835
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000C63D File Offset: 0x0000A83D
		public string JobID
		{
			get
			{
				return this.jobIDField;
			}
			set
			{
				this.jobIDField = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000C646 File Offset: 0x0000A846
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000C64E File Offset: 0x0000A84E
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000C657 File Offset: 0x0000A857
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000C65F File Offset: 0x0000A85F
		public string Path
		{
			get
			{
				return this.pathField;
			}
			set
			{
				this.pathField = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000C668 File Offset: 0x0000A868
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000C670 File Offset: 0x0000A870
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000C679 File Offset: 0x0000A879
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000C681 File Offset: 0x0000A881
		public string Machine
		{
			get
			{
				return this.machineField;
			}
			set
			{
				this.machineField = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000C68A File Offset: 0x0000A88A
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000C692 File Offset: 0x0000A892
		public string User
		{
			get
			{
				return this.userField;
			}
			set
			{
				this.userField = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000C69B File Offset: 0x0000A89B
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000C6A3 File Offset: 0x0000A8A3
		public DateTime StartDateTime
		{
			get
			{
				return this.startDateTimeField;
			}
			set
			{
				this.startDateTimeField = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000C6B4 File Offset: 0x0000A8B4
		public string JobActionName
		{
			get
			{
				return this.jobActionNameField;
			}
			set
			{
				this.jobActionNameField = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000C6BD File Offset: 0x0000A8BD
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000C6C5 File Offset: 0x0000A8C5
		public string JobTypeName
		{
			get
			{
				return this.jobTypeNameField;
			}
			set
			{
				this.jobTypeNameField = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000C6CE File Offset: 0x0000A8CE
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000C6D6 File Offset: 0x0000A8D6
		public string JobStatusName
		{
			get
			{
				return this.jobStatusNameField;
			}
			set
			{
				this.jobStatusNameField = value;
			}
		}

		// Token: 0x04000119 RID: 281
		private string jobIDField;

		// Token: 0x0400011A RID: 282
		private string nameField;

		// Token: 0x0400011B RID: 283
		private string pathField;

		// Token: 0x0400011C RID: 284
		private string descriptionField;

		// Token: 0x0400011D RID: 285
		private string machineField;

		// Token: 0x0400011E RID: 286
		private string userField;

		// Token: 0x0400011F RID: 287
		private DateTime startDateTimeField;

		// Token: 0x04000120 RID: 288
		private string jobActionNameField;

		// Token: 0x04000121 RID: 289
		private string jobTypeNameField;

		// Token: 0x04000122 RID: 290
		private string jobStatusNameField;
	}
}
