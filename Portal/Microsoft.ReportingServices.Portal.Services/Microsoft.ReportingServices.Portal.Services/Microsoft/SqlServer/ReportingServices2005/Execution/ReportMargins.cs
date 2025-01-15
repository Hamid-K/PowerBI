using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.ReportingServices2005.Execution
{
	// Token: 0x02000081 RID: 129
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	public class ReportMargins
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0001B476 File Offset: 0x00019676
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0001B47E File Offset: 0x0001967E
		public double Top
		{
			get
			{
				return this.topField;
			}
			set
			{
				this.topField = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001B487 File Offset: 0x00019687
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0001B48F File Offset: 0x0001968F
		public double Bottom
		{
			get
			{
				return this.bottomField;
			}
			set
			{
				this.bottomField = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001B498 File Offset: 0x00019698
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0001B4A0 File Offset: 0x000196A0
		public double Left
		{
			get
			{
				return this.leftField;
			}
			set
			{
				this.leftField = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001B4A9 File Offset: 0x000196A9
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0001B4B1 File Offset: 0x000196B1
		public double Right
		{
			get
			{
				return this.rightField;
			}
			set
			{
				this.rightField = value;
			}
		}

		// Token: 0x0400017E RID: 382
		private double topField;

		// Token: 0x0400017F RID: 383
		private double bottomField;

		// Token: 0x04000180 RID: 384
		private double leftField;

		// Token: 0x04000181 RID: 385
		private double rightField;
	}
}
