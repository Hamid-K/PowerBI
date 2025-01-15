using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002775 RID: 10101
	[ChildElementInfo(typeof(Outline))]
	[GeneratedCode("DomGen", "2.0")]
	internal class LineStyleList : OpenXmlCompositeElement
	{
		// Token: 0x1700616A RID: 24938
		// (get) Token: 0x060137D4 RID: 79828 RVA: 0x00307A37 File Offset: 0x00305C37
		public override string LocalName
		{
			get
			{
				return "lnStyleLst";
			}
		}

		// Token: 0x1700616B RID: 24939
		// (get) Token: 0x060137D5 RID: 79829 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700616C RID: 24940
		// (get) Token: 0x060137D6 RID: 79830 RVA: 0x00307A3E File Offset: 0x00305C3E
		internal override int ElementTypeId
		{
			get
			{
				return 10141;
			}
		}

		// Token: 0x060137D7 RID: 79831 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060137D8 RID: 79832 RVA: 0x00293ECF File Offset: 0x002920CF
		public LineStyleList()
		{
		}

		// Token: 0x060137D9 RID: 79833 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LineStyleList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137DA RID: 79834 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LineStyleList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060137DB RID: 79835 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LineStyleList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060137DC RID: 79836 RVA: 0x00307A45 File Offset: 0x00305C45
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ln" == name)
			{
				return new Outline();
			}
			return null;
		}

		// Token: 0x060137DD RID: 79837 RVA: 0x00307A60 File Offset: 0x00305C60
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineStyleList>(deep);
		}

		// Token: 0x04008671 RID: 34417
		private const string tagName = "lnStyleLst";

		// Token: 0x04008672 RID: 34418
		private const byte tagNsId = 10;

		// Token: 0x04008673 RID: 34419
		internal const int ElementTypeIdConst = 10141;
	}
}
