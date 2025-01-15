using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2006
{
	// Token: 0x02000148 RID: 328
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2006/03/15/reporting/reportingservices")]
	[Serializable]
	public class ServerConfigInfo
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00017440 File Offset: 0x00015640
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x00017448 File Offset: 0x00015648
		public string MachineName
		{
			get
			{
				return this.machineNameField;
			}
			set
			{
				this.machineNameField = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00017451 File Offset: 0x00015651
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x00017459 File Offset: 0x00015659
		public string InstanceName
		{
			get
			{
				return this.instanceNameField;
			}
			set
			{
				this.instanceNameField = value;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x00017462 File Offset: 0x00015662
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x0001746A File Offset: 0x0001566A
		public string ServiceAccountName
		{
			get
			{
				return this.serviceAccountNameField;
			}
			set
			{
				this.serviceAccountNameField = value;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x00017473 File Offset: 0x00015673
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0001747B File Offset: 0x0001567B
		public string ReportServerUrlItem
		{
			get
			{
				return this.reportServerUrlItemField;
			}
			set
			{
				this.reportServerUrlItemField = value;
			}
		}

		// Token: 0x0400042C RID: 1068
		private string machineNameField;

		// Token: 0x0400042D RID: 1069
		private string instanceNameField;

		// Token: 0x0400042E RID: 1070
		private string serviceAccountNameField;

		// Token: 0x0400042F RID: 1071
		private string reportServerUrlItemField;
	}
}
