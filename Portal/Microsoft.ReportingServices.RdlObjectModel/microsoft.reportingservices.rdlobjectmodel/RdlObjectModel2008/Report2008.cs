using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2008.Upgrade;
using Microsoft.ReportingServices.RdlObjectModel2010;

namespace Microsoft.ReportingServices.RdlObjectModel2008
{
	// Token: 0x02000070 RID: 112
	[XmlElementClass("Report", Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition")]
	internal class Report2008 : Report2010, IUpgradeable2008
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x00016B5E File Offset: 0x00014D5E
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x00016B72 File Offset: 0x00014D72
		public override Body Body
		{
			get
			{
				return (Body)base.PropertyStore.GetObject(25);
			}
			set
			{
				base.PropertyStore.SetObject(25, value);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00016B82 File Offset: 0x00014D82
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x00016B91 File Offset: 0x00014D91
		public override ReportSize Width
		{
			get
			{
				return base.PropertyStore.GetSize(26);
			}
			set
			{
				base.PropertyStore.SetSize(26, value);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00016BA1 File Offset: 0x00014DA1
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x00016BB5 File Offset: 0x00014DB5
		public override Page Page
		{
			get
			{
				return (Page)base.PropertyStore.GetObject(27);
			}
			set
			{
				base.PropertyStore.SetObject(27, value);
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00016BC5 File Offset: 0x00014DC5
		public Report2008()
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00016BCD File Offset: 0x00014DCD
		public Report2008(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00016BD6 File Offset: 0x00014DD6
		public override void Initialize()
		{
			this.Width = Constants.DefaultZeroSize;
			this.Body = new Body();
			this.Page = new Page();
			base.Initialize();
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00016BFF File Offset: 0x00014DFF
		public void Upgrade(UpgradeImpl2008 upgrader)
		{
			upgrader.UpgradeReport(this);
		}

		// Token: 0x0400010B RID: 267
		public new const string DesignerNamespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner";

		// Token: 0x0200032D RID: 813
		public new class Definition : DefinitionStore<Report2008, Report2008.Definition.Properties>
		{
			// Token: 0x06001761 RID: 5985 RVA: 0x000376D7 File Offset: 0x000358D7
			private Definition()
			{
			}

			// Token: 0x02000455 RID: 1109
			public enum Properties
			{
				// Token: 0x0400090F RID: 2319
				Body = 25,
				// Token: 0x04000910 RID: 2320
				Width,
				// Token: 0x04000911 RID: 2321
				Page,
				// Token: 0x04000912 RID: 2322
				PropertyCount
			}
		}
	}
}
