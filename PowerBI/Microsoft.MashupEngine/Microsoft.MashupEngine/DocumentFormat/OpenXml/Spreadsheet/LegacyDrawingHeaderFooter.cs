using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C85 RID: 11397
	[GeneratedCode("DomGen", "2.0")]
	internal class LegacyDrawingHeaderFooter : LegacyDrawingType
	{
		// Token: 0x17008368 RID: 33640
		// (get) Token: 0x06018477 RID: 99447 RVA: 0x002F3334 File Offset: 0x002F1534
		public override string LocalName
		{
			get
			{
				return "legacyDrawingHF";
			}
		}

		// Token: 0x17008369 RID: 33641
		// (get) Token: 0x06018478 RID: 99448 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700836A RID: 33642
		// (get) Token: 0x06018479 RID: 99449 RVA: 0x003400CC File Offset: 0x0033E2CC
		internal override int ElementTypeId
		{
			get
			{
				return 11376;
			}
		}

		// Token: 0x0601847A RID: 99450 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601847C RID: 99452 RVA: 0x003400D3 File Offset: 0x0033E2D3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LegacyDrawingHeaderFooter>(deep);
		}

		// Token: 0x04009FA0 RID: 40864
		private const string tagName = "legacyDrawingHF";

		// Token: 0x04009FA1 RID: 40865
		private const byte tagNsId = 22;

		// Token: 0x04009FA2 RID: 40866
		internal const int ElementTypeIdConst = 11376;
	}
}
