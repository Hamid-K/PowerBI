using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002408 RID: 9224
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NegativeFillColor : ColorType
	{
		// Token: 0x17004EC0 RID: 20160
		// (get) Token: 0x06010E09 RID: 69129 RVA: 0x002E84C4 File Offset: 0x002E66C4
		public override string LocalName
		{
			get
			{
				return "negativeFillColor";
			}
		}

		// Token: 0x17004EC1 RID: 20161
		// (get) Token: 0x06010E0A RID: 69130 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004EC2 RID: 20162
		// (get) Token: 0x06010E0B RID: 69131 RVA: 0x002E84CB File Offset: 0x002E66CB
		internal override int ElementTypeId
		{
			get
			{
				return 12969;
			}
		}

		// Token: 0x06010E0C RID: 69132 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010E0E RID: 69134 RVA: 0x002E84D2 File Offset: 0x002E66D2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NegativeFillColor>(deep);
		}

		// Token: 0x040076A7 RID: 30375
		private const string tagName = "negativeFillColor";

		// Token: 0x040076A8 RID: 30376
		private const byte tagNsId = 53;

		// Token: 0x040076A9 RID: 30377
		internal const int ElementTypeIdConst = 12969;
	}
}
