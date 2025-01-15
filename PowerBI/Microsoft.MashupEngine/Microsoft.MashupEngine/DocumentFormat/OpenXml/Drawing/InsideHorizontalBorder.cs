using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027FC RID: 10236
	[GeneratedCode("DomGen", "2.0")]
	internal class InsideHorizontalBorder : ThemeableLineStyleType
	{
		// Token: 0x1700650D RID: 25869
		// (get) Token: 0x06014007 RID: 81927 RVA: 0x0030E404 File Offset: 0x0030C604
		public override string LocalName
		{
			get
			{
				return "insideH";
			}
		}

		// Token: 0x1700650E RID: 25870
		// (get) Token: 0x06014008 RID: 81928 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700650F RID: 25871
		// (get) Token: 0x06014009 RID: 81929 RVA: 0x0030E40B File Offset: 0x0030C60B
		internal override int ElementTypeId
		{
			get
			{
				return 10272;
			}
		}

		// Token: 0x0601400A RID: 81930 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601400B RID: 81931 RVA: 0x0030E3A8 File Offset: 0x0030C5A8
		public InsideHorizontalBorder()
		{
		}

		// Token: 0x0601400C RID: 81932 RVA: 0x0030E3B0 File Offset: 0x0030C5B0
		public InsideHorizontalBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601400D RID: 81933 RVA: 0x0030E3B9 File Offset: 0x0030C5B9
		public InsideHorizontalBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601400E RID: 81934 RVA: 0x0030E3C2 File Offset: 0x0030C5C2
		public InsideHorizontalBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601400F RID: 81935 RVA: 0x0030E412 File Offset: 0x0030C612
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InsideHorizontalBorder>(deep);
		}

		// Token: 0x04008898 RID: 34968
		private const string tagName = "insideH";

		// Token: 0x04008899 RID: 34969
		private const byte tagNsId = 10;

		// Token: 0x0400889A RID: 34970
		internal const int ElementTypeIdConst = 10272;
	}
}
