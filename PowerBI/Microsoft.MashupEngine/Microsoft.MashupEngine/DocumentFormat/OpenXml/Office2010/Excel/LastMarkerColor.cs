using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002402 RID: 9218
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class LastMarkerColor : ColorType
	{
		// Token: 0x17004EAE RID: 20142
		// (get) Token: 0x06010DE5 RID: 69093 RVA: 0x002E843A File Offset: 0x002E663A
		public override string LocalName
		{
			get
			{
				return "colorLast";
			}
		}

		// Token: 0x17004EAF RID: 20143
		// (get) Token: 0x06010DE6 RID: 69094 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EB0 RID: 20144
		// (get) Token: 0x06010DE7 RID: 69095 RVA: 0x002E8441 File Offset: 0x002E6641
		internal override int ElementTypeId
		{
			get
			{
				return 12942;
			}
		}

		// Token: 0x06010DE8 RID: 69096 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010DEA RID: 69098 RVA: 0x002E8448 File Offset: 0x002E6648
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LastMarkerColor>(deep);
		}

		// Token: 0x04007695 RID: 30357
		private const string tagName = "colorLast";

		// Token: 0x04007696 RID: 30358
		private const byte tagNsId = 53;

		// Token: 0x04007697 RID: 30359
		internal const int ElementTypeIdConst = 12942;
	}
}
