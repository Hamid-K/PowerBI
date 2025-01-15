using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002400 RID: 9216
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class MarkersColor : ColorType
	{
		// Token: 0x17004EA8 RID: 20136
		// (get) Token: 0x06010DD9 RID: 69081 RVA: 0x002E840C File Offset: 0x002E660C
		public override string LocalName
		{
			get
			{
				return "colorMarkers";
			}
		}

		// Token: 0x17004EA9 RID: 20137
		// (get) Token: 0x06010DDA RID: 69082 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EAA RID: 20138
		// (get) Token: 0x06010DDB RID: 69083 RVA: 0x002E8413 File Offset: 0x002E6613
		internal override int ElementTypeId
		{
			get
			{
				return 12940;
			}
		}

		// Token: 0x06010DDC RID: 69084 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010DDE RID: 69086 RVA: 0x002E841A File Offset: 0x002E661A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MarkersColor>(deep);
		}

		// Token: 0x0400768F RID: 30351
		private const string tagName = "colorMarkers";

		// Token: 0x04007690 RID: 30352
		private const byte tagNsId = 53;

		// Token: 0x04007691 RID: 30353
		internal const int ElementTypeIdConst = 12940;
	}
}
