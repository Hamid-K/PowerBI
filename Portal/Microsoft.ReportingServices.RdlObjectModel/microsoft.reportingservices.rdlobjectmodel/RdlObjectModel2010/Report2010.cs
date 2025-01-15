using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2010.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2010
{
	// Token: 0x02000068 RID: 104
	[XmlElementClass("Report", Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition")]
	internal class Report2010 : Report, IUpgradeable2010
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0001642B File Offset: 0x0001462B
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0001643F File Offset: 0x0001463F
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

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0001644F File Offset: 0x0001464F
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0001645E File Offset: 0x0001465E
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

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0001646E File Offset: 0x0001466E
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00016482 File Offset: 0x00014682
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

		// Token: 0x060003E9 RID: 1001 RVA: 0x00016492 File Offset: 0x00014692
		public Report2010()
		{
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001649A File Offset: 0x0001469A
		public Report2010(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000164A3 File Offset: 0x000146A3
		public override void Initialize()
		{
			this.Width = Constants.DefaultZeroSize;
			this.Body = new Body();
			this.Page = new Page();
			base.Initialize();
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000164CC File Offset: 0x000146CC
		public void Upgrade(UpgradeImpl2010 upgrader)
		{
			upgrader.UpgradeReport(this);
		}

		// Token: 0x04000103 RID: 259
		public const string DesignerNamespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner";

		// Token: 0x0200032B RID: 811
		public new class Definition : DefinitionStore<Report2010, Report2010.Definition.Properties>
		{
			// Token: 0x0600175F RID: 5983 RVA: 0x000376C7 File Offset: 0x000358C7
			private Definition()
			{
			}

			// Token: 0x02000454 RID: 1108
			public enum Properties
			{
				// Token: 0x0400090A RID: 2314
				Body = 25,
				// Token: 0x0400090B RID: 2315
				Width,
				// Token: 0x0400090C RID: 2316
				Page,
				// Token: 0x0400090D RID: 2317
				PropertyCount
			}
		}
	}
}
