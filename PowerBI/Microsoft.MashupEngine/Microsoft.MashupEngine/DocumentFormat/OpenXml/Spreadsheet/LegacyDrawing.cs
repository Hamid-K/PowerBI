using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C84 RID: 11396
	[GeneratedCode("DomGen", "2.0")]
	internal class LegacyDrawing : LegacyDrawingType
	{
		// Token: 0x17008365 RID: 33637
		// (get) Token: 0x06018471 RID: 99441 RVA: 0x002FCDC2 File Offset: 0x002FAFC2
		public override string LocalName
		{
			get
			{
				return "legacyDrawing";
			}
		}

		// Token: 0x17008366 RID: 33638
		// (get) Token: 0x06018472 RID: 99442 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008367 RID: 33639
		// (get) Token: 0x06018473 RID: 99443 RVA: 0x003400B4 File Offset: 0x0033E2B4
		internal override int ElementTypeId
		{
			get
			{
				return 11375;
			}
		}

		// Token: 0x06018474 RID: 99444 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018476 RID: 99446 RVA: 0x003400C3 File Offset: 0x0033E2C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LegacyDrawing>(deep);
		}

		// Token: 0x04009F9D RID: 40861
		private const string tagName = "legacyDrawing";

		// Token: 0x04009F9E RID: 40862
		private const byte tagNsId = 22;

		// Token: 0x04009F9F RID: 40863
		internal const int ElementTypeIdConst = 11375;
	}
}
