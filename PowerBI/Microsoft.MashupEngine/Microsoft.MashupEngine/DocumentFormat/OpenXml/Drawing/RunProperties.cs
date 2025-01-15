using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002820 RID: 10272
	[GeneratedCode("DomGen", "2.0")]
	internal class RunProperties : TextCharacterPropertiesType
	{
		// Token: 0x170065B5 RID: 26037
		// (get) Token: 0x060141C1 RID: 82369 RVA: 0x0030F747 File Offset: 0x0030D947
		public override string LocalName
		{
			get
			{
				return "rPr";
			}
		}

		// Token: 0x170065B6 RID: 26038
		// (get) Token: 0x060141C2 RID: 82370 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065B7 RID: 26039
		// (get) Token: 0x060141C3 RID: 82371 RVA: 0x0030F74E File Offset: 0x0030D94E
		internal override int ElementTypeId
		{
			get
			{
				return 10308;
			}
		}

		// Token: 0x060141C4 RID: 82372 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060141C5 RID: 82373 RVA: 0x0030F71B File Offset: 0x0030D91B
		public RunProperties()
		{
		}

		// Token: 0x060141C6 RID: 82374 RVA: 0x0030F723 File Offset: 0x0030D923
		public RunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060141C7 RID: 82375 RVA: 0x0030F72C File Offset: 0x0030D92C
		public RunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060141C8 RID: 82376 RVA: 0x0030F735 File Offset: 0x0030D935
		public RunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060141C9 RID: 82377 RVA: 0x0030F755 File Offset: 0x0030D955
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunProperties>(deep);
		}

		// Token: 0x0400890B RID: 35083
		private const string tagName = "rPr";

		// Token: 0x0400890C RID: 35084
		private const byte tagNsId = 10;

		// Token: 0x0400890D RID: 35085
		internal const int ElementTypeIdConst = 10308;
	}
}
