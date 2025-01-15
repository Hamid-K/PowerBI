using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002403 RID: 9219
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class HighMarkerColor : ColorType
	{
		// Token: 0x17004EB1 RID: 20145
		// (get) Token: 0x06010DEB RID: 69099 RVA: 0x002E8451 File Offset: 0x002E6651
		public override string LocalName
		{
			get
			{
				return "colorHigh";
			}
		}

		// Token: 0x17004EB2 RID: 20146
		// (get) Token: 0x06010DEC RID: 69100 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EB3 RID: 20147
		// (get) Token: 0x06010DED RID: 69101 RVA: 0x002E8458 File Offset: 0x002E6658
		internal override int ElementTypeId
		{
			get
			{
				return 12943;
			}
		}

		// Token: 0x06010DEE RID: 69102 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010DF0 RID: 69104 RVA: 0x002E845F File Offset: 0x002E665F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HighMarkerColor>(deep);
		}

		// Token: 0x04007698 RID: 30360
		private const string tagName = "colorHigh";

		// Token: 0x04007699 RID: 30361
		private const byte tagNsId = 53;

		// Token: 0x0400769A RID: 30362
		internal const int ElementTypeIdConst = 12943;
	}
}
