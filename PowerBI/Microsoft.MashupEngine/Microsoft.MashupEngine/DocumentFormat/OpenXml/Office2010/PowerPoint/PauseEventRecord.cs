using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023CB RID: 9163
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class PauseEventRecord : MediaPlaybackEventRecordType
	{
		// Token: 0x17004D0B RID: 19723
		// (get) Token: 0x06010A3D RID: 68157 RVA: 0x002E5975 File Offset: 0x002E3B75
		public override string LocalName
		{
			get
			{
				return "pauseEvt";
			}
		}

		// Token: 0x17004D0C RID: 19724
		// (get) Token: 0x06010A3E RID: 68158 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004D0D RID: 19725
		// (get) Token: 0x06010A3F RID: 68159 RVA: 0x002E597C File Offset: 0x002E3B7C
		internal override int ElementTypeId
		{
			get
			{
				return 12816;
			}
		}

		// Token: 0x06010A40 RID: 68160 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010A42 RID: 68162 RVA: 0x002E5983 File Offset: 0x002E3B83
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PauseEventRecord>(deep);
		}

		// Token: 0x040075A5 RID: 30117
		private const string tagName = "pauseEvt";

		// Token: 0x040075A6 RID: 30118
		private const byte tagNsId = 49;

		// Token: 0x040075A7 RID: 30119
		internal const int ElementTypeIdConst = 12816;
	}
}
