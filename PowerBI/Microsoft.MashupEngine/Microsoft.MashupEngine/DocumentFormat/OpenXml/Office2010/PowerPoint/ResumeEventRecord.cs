using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023CC RID: 9164
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ResumeEventRecord : MediaPlaybackEventRecordType
	{
		// Token: 0x17004D0E RID: 19726
		// (get) Token: 0x06010A43 RID: 68163 RVA: 0x002E598C File Offset: 0x002E3B8C
		public override string LocalName
		{
			get
			{
				return "resumeEvt";
			}
		}

		// Token: 0x17004D0F RID: 19727
		// (get) Token: 0x06010A44 RID: 68164 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004D10 RID: 19728
		// (get) Token: 0x06010A45 RID: 68165 RVA: 0x002E5993 File Offset: 0x002E3B93
		internal override int ElementTypeId
		{
			get
			{
				return 12817;
			}
		}

		// Token: 0x06010A46 RID: 68166 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010A48 RID: 68168 RVA: 0x002E599A File Offset: 0x002E3B9A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ResumeEventRecord>(deep);
		}

		// Token: 0x040075A8 RID: 30120
		private const string tagName = "resumeEvt";

		// Token: 0x040075A9 RID: 30121
		private const byte tagNsId = 49;

		// Token: 0x040075AA RID: 30122
		internal const int ElementTypeIdConst = 12817;
	}
}
