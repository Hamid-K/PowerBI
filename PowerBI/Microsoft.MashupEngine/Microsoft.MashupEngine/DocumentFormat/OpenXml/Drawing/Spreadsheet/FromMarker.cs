using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x0200288E RID: 10382
	[GeneratedCode("DomGen", "2.0")]
	internal class FromMarker : MarkerType
	{
		// Token: 0x17006799 RID: 26521
		// (get) Token: 0x06014616 RID: 83478 RVA: 0x002FCA49 File Offset: 0x002FAC49
		public override string LocalName
		{
			get
			{
				return "from";
			}
		}

		// Token: 0x1700679A RID: 26522
		// (get) Token: 0x06014617 RID: 83479 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x1700679B RID: 26523
		// (get) Token: 0x06014618 RID: 83480 RVA: 0x00312B40 File Offset: 0x00310D40
		internal override int ElementTypeId
		{
			get
			{
				return 10743;
			}
		}

		// Token: 0x06014619 RID: 83481 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601461A RID: 83482 RVA: 0x00312B47 File Offset: 0x00310D47
		public FromMarker()
		{
		}

		// Token: 0x0601461B RID: 83483 RVA: 0x00312B4F File Offset: 0x00310D4F
		public FromMarker(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601461C RID: 83484 RVA: 0x00312B58 File Offset: 0x00310D58
		public FromMarker(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601461D RID: 83485 RVA: 0x00312B61 File Offset: 0x00310D61
		public FromMarker(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601461E RID: 83486 RVA: 0x00312B6A File Offset: 0x00310D6A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FromMarker>(deep);
		}

		// Token: 0x04008DD4 RID: 36308
		private const string tagName = "from";

		// Token: 0x04008DD5 RID: 36309
		private const byte tagNsId = 18;

		// Token: 0x04008DD6 RID: 36310
		internal const int ElementTypeIdConst = 10743;
	}
}
