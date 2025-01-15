using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200240A RID: 9226
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class BarAxisColor : ColorType
	{
		// Token: 0x17004EC6 RID: 20166
		// (get) Token: 0x06010E15 RID: 69141 RVA: 0x002E84F2 File Offset: 0x002E66F2
		public override string LocalName
		{
			get
			{
				return "axisColor";
			}
		}

		// Token: 0x17004EC7 RID: 20167
		// (get) Token: 0x06010E16 RID: 69142 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EC8 RID: 20168
		// (get) Token: 0x06010E17 RID: 69143 RVA: 0x002E84F9 File Offset: 0x002E66F9
		internal override int ElementTypeId
		{
			get
			{
				return 12971;
			}
		}

		// Token: 0x06010E18 RID: 69144 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010E1A RID: 69146 RVA: 0x002E8500 File Offset: 0x002E6700
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BarAxisColor>(deep);
		}

		// Token: 0x040076AD RID: 30381
		private const string tagName = "axisColor";

		// Token: 0x040076AE RID: 30382
		private const byte tagNsId = 53;

		// Token: 0x040076AF RID: 30383
		internal const int ElementTypeIdConst = 12971;
	}
}
