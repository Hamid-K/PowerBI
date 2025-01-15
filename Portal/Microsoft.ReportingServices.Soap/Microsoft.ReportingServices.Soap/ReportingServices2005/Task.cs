using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005
{
	// Token: 0x020001FA RID: 506
	[GeneratedCode("wsdl", "2.0.50727.1433")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[Serializable]
	public class Task
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x00021675 File Offset: 0x0001F875
		// (set) Token: 0x06001399 RID: 5017 RVA: 0x0002167D File Offset: 0x0001F87D
		public string TaskID
		{
			get
			{
				return this.taskIDField;
			}
			set
			{
				this.taskIDField = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00021686 File Offset: 0x0001F886
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x0002168E File Offset: 0x0001F88E
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

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x00021697 File Offset: 0x0001F897
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x0002169F File Offset: 0x0001F89F
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

		// Token: 0x04000599 RID: 1433
		private string taskIDField;

		// Token: 0x0400059A RID: 1434
		private string nameField;

		// Token: 0x0400059B RID: 1435
		private string descriptionField;
	}
}
