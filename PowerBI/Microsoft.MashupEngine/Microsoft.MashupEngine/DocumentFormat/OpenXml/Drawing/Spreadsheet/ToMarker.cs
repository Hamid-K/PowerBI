using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200288F RID: 10383
	[GeneratedCode("DomGen", "2.0")]
	internal class ToMarker : MarkerType
	{
		// Token: 0x1700679C RID: 26524
		// (get) Token: 0x0601461F RID: 83487 RVA: 0x002FCA83 File Offset: 0x002FAC83
		public override string LocalName
		{
			get
			{
				return "to";
			}
		}

		// Token: 0x1700679D RID: 26525
		// (get) Token: 0x06014620 RID: 83488 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x1700679E RID: 26526
		// (get) Token: 0x06014621 RID: 83489 RVA: 0x00312B73 File Offset: 0x00310D73
		internal override int ElementTypeId
		{
			get
			{
				return 10744;
			}
		}

		// Token: 0x06014622 RID: 83490 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014623 RID: 83491 RVA: 0x00312B47 File Offset: 0x00310D47
		public ToMarker()
		{
		}

		// Token: 0x06014624 RID: 83492 RVA: 0x00312B4F File Offset: 0x00310D4F
		public ToMarker(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014625 RID: 83493 RVA: 0x00312B58 File Offset: 0x00310D58
		public ToMarker(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014626 RID: 83494 RVA: 0x00312B61 File Offset: 0x00310D61
		public ToMarker(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014627 RID: 83495 RVA: 0x00312B7A File Offset: 0x00310D7A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ToMarker>(deep);
		}

		// Token: 0x04008DD7 RID: 36311
		private const string tagName = "to";

		// Token: 0x04008DD8 RID: 36312
		private const byte tagNsId = 18;

		// Token: 0x04008DD9 RID: 36313
		internal const int ElementTypeIdConst = 10744;
	}
}
