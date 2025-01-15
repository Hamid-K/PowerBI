using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.RdlObjectModel2010.Upgrade;

namespace Microsoft.ReportingServices.RdlObjectModel2008.Upgrade
{
	// Token: 0x02000073 RID: 115
	internal class UpgradeImpl2008 : UpgradeImpl2010
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x00016C08 File Offset: 0x00014E08
		internal UpgradeImpl2008()
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00016C10 File Offset: 0x00014E10
		internal override Type GetReportType()
		{
			return typeof(Report2008);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00016C1C File Offset: 0x00014E1C
		protected override void InitUpgrade()
		{
			this.m_upgradeable = new List<IUpgradeable2008>();
			base.InitUpgrade();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00016C30 File Offset: 0x00014E30
		protected override void Upgrade(Report report)
		{
			foreach (IUpgradeable2008 upgradeable in this.m_upgradeable)
			{
				upgradeable.Upgrade(this);
			}
			base.Upgrade(report);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00016C88 File Offset: 0x00014E88
		protected override RdlSerializerSettings CreateReaderSettings()
		{
			return UpgradeSerializerSettings2008.CreateReaderSettings();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00016C8F File Offset: 0x00014E8F
		protected override RdlSerializerSettings CreateWriterSettings()
		{
			return UpgradeSerializerSettings2008.CreateWriterSettings();
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00016C96 File Offset: 0x00014E96
		protected override void SetupReaderSettings(RdlSerializerSettings settings)
		{
			((SerializerHost2008)settings.Host).Upgradeable2008 = this.m_upgradeable;
			base.SetupReaderSettings(settings);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00016CB8 File Offset: 0x00014EB8
		internal void UpgradeReport(Report2008 report)
		{
			ReportSection reportSection = new ReportSection();
			reportSection.Body = report.Body;
			reportSection.Page = report.Page;
			reportSection.Width = report.Width;
			report.ReportSections = new List<ReportSection>(1);
			report.ReportSections.Add(reportSection);
			report.Body = null;
			report.Page = null;
			report.Width = ReportSize.Empty;
		}

		// Token: 0x0400010C RID: 268
		private List<IUpgradeable2008> m_upgradeable;
	}
}
