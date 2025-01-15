using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023CA RID: 9162
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class StopEventRecord : MediaPlaybackEventRecordType
	{
		// Token: 0x17004D08 RID: 19720
		// (get) Token: 0x06010A37 RID: 68151 RVA: 0x002E595E File Offset: 0x002E3B5E
		public override string LocalName
		{
			get
			{
				return "stopEvt";
			}
		}

		// Token: 0x17004D09 RID: 19721
		// (get) Token: 0x06010A38 RID: 68152 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004D0A RID: 19722
		// (get) Token: 0x06010A39 RID: 68153 RVA: 0x002E5965 File Offset: 0x002E3B65
		internal override int ElementTypeId
		{
			get
			{
				return 12815;
			}
		}

		// Token: 0x06010A3A RID: 68154 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010A3C RID: 68156 RVA: 0x002E596C File Offset: 0x002E3B6C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StopEventRecord>(deep);
		}

		// Token: 0x040075A2 RID: 30114
		private const string tagName = "stopEvt";

		// Token: 0x040075A3 RID: 30115
		private const byte tagNsId = 49;

		// Token: 0x040075A4 RID: 30116
		internal const int ElementTypeIdConst = 12815;
	}
}
