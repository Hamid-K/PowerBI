using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x0200013D RID: 317
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class Job
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00017179 File Offset: 0x00015379
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x00017181 File Offset: 0x00015381
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

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x0001718A File Offset: 0x0001538A
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00017192 File Offset: 0x00015392
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

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x0001719B File Offset: 0x0001539B
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x000171A3 File Offset: 0x000153A3
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

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x000171AC File Offset: 0x000153AC
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x000171B4 File Offset: 0x000153B4
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

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x000171BD File Offset: 0x000153BD
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x000171C5 File Offset: 0x000153C5
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

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x000171CE File Offset: 0x000153CE
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x000171D6 File Offset: 0x000153D6
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

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x000171DF File Offset: 0x000153DF
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x000171E7 File Offset: 0x000153E7
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

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x000171F0 File Offset: 0x000153F0
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x000171F8 File Offset: 0x000153F8
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

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x00017201 File Offset: 0x00015401
		// (set) Token: 0x06000D55 RID: 3413 RVA: 0x00017209 File Offset: 0x00015409
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

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x00017212 File Offset: 0x00015412
		// (set) Token: 0x06000D57 RID: 3415 RVA: 0x0001721A File Offset: 0x0001541A
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

		// Token: 0x040003ED RID: 1005
		private string jobIDField;

		// Token: 0x040003EE RID: 1006
		private string nameField;

		// Token: 0x040003EF RID: 1007
		private string pathField;

		// Token: 0x040003F0 RID: 1008
		private string descriptionField;

		// Token: 0x040003F1 RID: 1009
		private string machineField;

		// Token: 0x040003F2 RID: 1010
		private string userField;

		// Token: 0x040003F3 RID: 1011
		private DateTime startDateTimeField;

		// Token: 0x040003F4 RID: 1012
		private JobActionEnum actionField;

		// Token: 0x040003F5 RID: 1013
		private JobTypeEnum typeField;

		// Token: 0x040003F6 RID: 1014
		private JobStatusEnum statusField;
	}
}
