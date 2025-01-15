using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027FD RID: 10237
	[GeneratedCode("DomGen", "2.0")]
	internal class InsideVerticalBorder : ThemeableLineStyleType
	{
		// Token: 0x17006510 RID: 25872
		// (get) Token: 0x06014010 RID: 81936 RVA: 0x0030E41B File Offset: 0x0030C61B
		public override string LocalName
		{
			get
			{
				return "insideV";
			}
		}

		// Token: 0x17006511 RID: 25873
		// (get) Token: 0x06014011 RID: 81937 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006512 RID: 25874
		// (get) Token: 0x06014012 RID: 81938 RVA: 0x0030E422 File Offset: 0x0030C622
		internal override int ElementTypeId
		{
			get
			{
				return 10273;
			}
		}

		// Token: 0x06014013 RID: 81939 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014014 RID: 81940 RVA: 0x0030E3A8 File Offset: 0x0030C5A8
		public InsideVerticalBorder()
		{
		}

		// Token: 0x06014015 RID: 81941 RVA: 0x0030E3B0 File Offset: 0x0030C5B0
		public InsideVerticalBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014016 RID: 81942 RVA: 0x0030E3B9 File Offset: 0x0030C5B9
		public InsideVerticalBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014017 RID: 81943 RVA: 0x0030E3C2 File Offset: 0x0030C5C2
		public InsideVerticalBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014018 RID: 81944 RVA: 0x0030E429 File Offset: 0x0030C629
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InsideVerticalBorder>(deep);
		}

		// Token: 0x0400889B RID: 34971
		private const string tagName = "insideV";

		// Token: 0x0400889C RID: 34972
		private const byte tagNsId = 10;

		// Token: 0x0400889D RID: 34973
		internal const int ElementTypeIdConst = 10273;
	}
}
