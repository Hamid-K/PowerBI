using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2005.Upgrade;
using Microsoft.ReportingServices.RdlObjectModel2008;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000034 RID: 52
	[XmlElementClass("Report", Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
	internal class Report2005 : Report2008, IUpgradeable
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00003A47 File Offset: 0x00001C47
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00003A54 File Offset: 0x00001C54
		public PageSection PageHeader
		{
			get
			{
				return this.Page.PageHeader;
			}
			set
			{
				this.Page.PageHeader = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00003A62 File Offset: 0x00001C62
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00003A6F File Offset: 0x00001C6F
		public PageSection PageFooter
		{
			get
			{
				return this.Page.PageFooter;
			}
			set
			{
				this.Page.PageFooter = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00003A7D File Offset: 0x00001C7D
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00003A8A File Offset: 0x00001C8A
		public ReportSize PageHeight
		{
			get
			{
				return this.Page.PageHeight;
			}
			set
			{
				this.Page.PageHeight = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00003A98 File Offset: 0x00001C98
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00003AA5 File Offset: 0x00001CA5
		public ReportSize PageWidth
		{
			get
			{
				return this.Page.PageWidth;
			}
			set
			{
				this.Page.PageWidth = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00003AB3 File Offset: 0x00001CB3
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public ReportSize InteractiveHeight
		{
			get
			{
				return this.Page.InteractiveHeight;
			}
			set
			{
				this.Page.InteractiveHeight = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00003ACE File Offset: 0x00001CCE
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00003ADB File Offset: 0x00001CDB
		public ReportSize InteractiveWidth
		{
			get
			{
				return this.Page.InteractiveWidth;
			}
			set
			{
				this.Page.InteractiveWidth = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00003AE9 File Offset: 0x00001CE9
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00003AF6 File Offset: 0x00001CF6
		public ReportSize LeftMargin
		{
			get
			{
				return this.Page.LeftMargin;
			}
			set
			{
				this.Page.LeftMargin = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00003B04 File Offset: 0x00001D04
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00003B11 File Offset: 0x00001D11
		public ReportSize RightMargin
		{
			get
			{
				return this.Page.RightMargin;
			}
			set
			{
				this.Page.RightMargin = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00003B1F File Offset: 0x00001D1F
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00003B2C File Offset: 0x00001D2C
		public ReportSize TopMargin
		{
			get
			{
				return this.Page.TopMargin;
			}
			set
			{
				this.Page.TopMargin = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00003B3A File Offset: 0x00001D3A
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00003B47 File Offset: 0x00001D47
		public ReportSize BottomMargin
		{
			get
			{
				return this.Page.BottomMargin;
			}
			set
			{
				this.Page.BottomMargin = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00003B55 File Offset: 0x00001D55
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00003B5D File Offset: 0x00001D5D
		public new DataElementStyles2005 DataElementStyle
		{
			get
			{
				return (DataElementStyles2005)base.DataElementStyle;
			}
			set
			{
				base.DataElementStyle = (DataElementStyles)value;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00003B66 File Offset: 0x00001D66
		public Report2005()
		{
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00003B6E File Offset: 0x00001D6E
		public Report2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00003B77 File Offset: 0x00001D77
		public void Upgrade(UpgradeImpl2005 upgrader)
		{
			upgrader.UpgradeReport(this);
		}

		// Token: 0x04000030 RID: 48
		public new const string DesignerNamespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner";

		// Token: 0x0200030E RID: 782
		internal new class Definition : DefinitionStore<Report2005, Report2005.Definition.Properties>
		{
			// Token: 0x0600170A RID: 5898 RVA: 0x000364EA File Offset: 0x000346EA
			private Definition()
			{
			}

			// Token: 0x02000442 RID: 1090
			public enum Properties
			{
				// Token: 0x040008AB RID: 2219
				PropertyCount = 28
			}
		}
	}
}
