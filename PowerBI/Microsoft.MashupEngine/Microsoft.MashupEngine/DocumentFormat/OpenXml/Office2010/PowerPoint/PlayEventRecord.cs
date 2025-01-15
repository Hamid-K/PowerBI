using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023C9 RID: 9161
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PlayEventRecord : MediaPlaybackEventRecordType
	{
		// Token: 0x17004D05 RID: 19717
		// (get) Token: 0x06010A31 RID: 68145 RVA: 0x002E593F File Offset: 0x002E3B3F
		public override string LocalName
		{
			get
			{
				return "playEvt";
			}
		}

		// Token: 0x17004D06 RID: 19718
		// (get) Token: 0x06010A32 RID: 68146 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004D07 RID: 19719
		// (get) Token: 0x06010A33 RID: 68147 RVA: 0x002E5946 File Offset: 0x002E3B46
		internal override int ElementTypeId
		{
			get
			{
				return 12814;
			}
		}

		// Token: 0x06010A34 RID: 68148 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010A36 RID: 68150 RVA: 0x002E5955 File Offset: 0x002E3B55
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PlayEventRecord>(deep);
		}

		// Token: 0x0400759F RID: 30111
		private const string tagName = "playEvt";

		// Token: 0x040075A0 RID: 30112
		private const byte tagNsId = 49;

		// Token: 0x040075A1 RID: 30113
		internal const int ElementTypeIdConst = 12814;
	}
}
