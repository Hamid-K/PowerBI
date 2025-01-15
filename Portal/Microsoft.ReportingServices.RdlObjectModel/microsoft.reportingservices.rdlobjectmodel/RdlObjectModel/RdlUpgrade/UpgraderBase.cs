using System;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlObjectModel.RdlUpgrade
{
	// Token: 0x0200020F RID: 527
	internal abstract class UpgraderBase
	{
		// Token: 0x060011B8 RID: 4536 RVA: 0x000282BA File Offset: 0x000264BA
		internal UpgraderBase()
		{
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x000282C2 File Offset: 0x000264C2
		internal RDLUpgradeResult UpgradeResults
		{
			get
			{
				return this.m_upgradeResults;
			}
		}

		// Token: 0x060011BA RID: 4538
		internal abstract Type GetReportType();

		// Token: 0x060011BB RID: 4539 RVA: 0x000282CC File Offset: 0x000264CC
		public void Upgrade(XmlReader xmlReader, Stream outStream)
		{
			this.InitUpgrade();
			RdlSerializerSettings rdlSerializerSettings = this.CreateReaderSettings();
			this.SetupReaderSettings(rdlSerializerSettings);
			Report report = (Report)new RdlSerializer(rdlSerializerSettings).Deserialize(xmlReader, this.GetReportType());
			this.Upgrade(report);
			new RdlSerializer(this.CreateWriterSettings()).Serialize(outStream, report);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0002831E File Offset: 0x0002651E
		protected virtual void InitUpgrade()
		{
			this.m_upgradeResults = new RDLUpgradeResult();
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0002832B File Offset: 0x0002652B
		protected virtual void Upgrade(Report report)
		{
		}

		// Token: 0x060011BE RID: 4542
		protected abstract RdlSerializerSettings CreateReaderSettings();

		// Token: 0x060011BF RID: 4543 RVA: 0x0002832D File Offset: 0x0002652D
		protected virtual void SetupReaderSettings(RdlSerializerSettings settings)
		{
		}

		// Token: 0x060011C0 RID: 4544
		protected abstract RdlSerializerSettings CreateWriterSettings();

		// Token: 0x040005B6 RID: 1462
		protected RDLUpgradeResult m_upgradeResults;
	}
}
