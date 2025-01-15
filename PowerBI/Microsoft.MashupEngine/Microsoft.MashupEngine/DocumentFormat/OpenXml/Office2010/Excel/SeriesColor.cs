using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023FD RID: 9213
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SeriesColor : ColorType
	{
		// Token: 0x17004E9F RID: 20127
		// (get) Token: 0x06010DC7 RID: 69063 RVA: 0x002E83BF File Offset: 0x002E65BF
		public override string LocalName
		{
			get
			{
				return "colorSeries";
			}
		}

		// Token: 0x17004EA0 RID: 20128
		// (get) Token: 0x06010DC8 RID: 69064 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EA1 RID: 20129
		// (get) Token: 0x06010DC9 RID: 69065 RVA: 0x002E83C6 File Offset: 0x002E65C6
		internal override int ElementTypeId
		{
			get
			{
				return 12937;
			}
		}

		// Token: 0x06010DCA RID: 69066 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010DCC RID: 69068 RVA: 0x002E83D5 File Offset: 0x002E65D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SeriesColor>(deep);
		}

		// Token: 0x04007686 RID: 30342
		private const string tagName = "colorSeries";

		// Token: 0x04007687 RID: 30343
		private const byte tagNsId = 53;

		// Token: 0x04007688 RID: 30344
		internal const int ElementTypeIdConst = 12937;
	}
}
