using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x02000222 RID: 546
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Job
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x000220FB File Offset: 0x000202FB
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x00022103 File Offset: 0x00020303
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

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x0002210C File Offset: 0x0002030C
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x00022114 File Offset: 0x00020314
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

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x0002211D File Offset: 0x0002031D
		// (set) Token: 0x060014DC RID: 5340 RVA: 0x00022125 File Offset: 0x00020325
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

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x0002212E File Offset: 0x0002032E
		// (set) Token: 0x060014DE RID: 5342 RVA: 0x00022136 File Offset: 0x00020336
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

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x0002213F File Offset: 0x0002033F
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x00022147 File Offset: 0x00020347
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

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00022150 File Offset: 0x00020350
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x00022158 File Offset: 0x00020358
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

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x00022161 File Offset: 0x00020361
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x00022169 File Offset: 0x00020369
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

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x00022172 File Offset: 0x00020372
		// (set) Token: 0x060014E6 RID: 5350 RVA: 0x0002217A File Offset: 0x0002037A
		public JobActionEnum Action
		{
			get
			{
				return this.actionField;
			}
			set
			{
				this.actionField = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00022183 File Offset: 0x00020383
		// (set) Token: 0x060014E8 RID: 5352 RVA: 0x0002218B File Offset: 0x0002038B
		public JobTypeEnum Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00022194 File Offset: 0x00020394
		// (set) Token: 0x060014EA RID: 5354 RVA: 0x0002219C File Offset: 0x0002039C
		public JobStatusEnum Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x04000641 RID: 1601
		private string jobIDField;

		// Token: 0x04000642 RID: 1602
		private string nameField;

		// Token: 0x04000643 RID: 1603
		private string pathField;

		// Token: 0x04000644 RID: 1604
		private string descriptionField;

		// Token: 0x04000645 RID: 1605
		private string machineField;

		// Token: 0x04000646 RID: 1606
		private string userField;

		// Token: 0x04000647 RID: 1607
		private DateTime startDateTimeField;

		// Token: 0x04000648 RID: 1608
		private JobActionEnum actionField;

		// Token: 0x04000649 RID: 1609
		private JobTypeEnum typeField;

		// Token: 0x0400064A RID: 1610
		private JobStatusEnum statusField;
	}
}
