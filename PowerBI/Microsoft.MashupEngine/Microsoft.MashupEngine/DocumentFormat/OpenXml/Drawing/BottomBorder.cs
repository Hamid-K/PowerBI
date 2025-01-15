using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027FB RID: 10235
	[GeneratedCode("DomGen", "2.0")]
	internal class BottomBorder : ThemeableLineStyleType
	{
		// Token: 0x1700650A RID: 25866
		// (get) Token: 0x06013FFE RID: 81918 RVA: 0x002BF3AD File Offset: 0x002BD5AD
		public override string LocalName
		{
			get
			{
				return "bottom";
			}
		}

		// Token: 0x1700650B RID: 25867
		// (get) Token: 0x06013FFF RID: 81919 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700650C RID: 25868
		// (get) Token: 0x06014000 RID: 81920 RVA: 0x0030E3F4 File Offset: 0x0030C5F4
		internal override int ElementTypeId
		{
			get
			{
				return 10271;
			}
		}

		// Token: 0x06014001 RID: 81921 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014002 RID: 81922 RVA: 0x0030E3A8 File Offset: 0x0030C5A8
		public BottomBorder()
		{
		}

		// Token: 0x06014003 RID: 81923 RVA: 0x0030E3B0 File Offset: 0x0030C5B0
		public BottomBorder(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014004 RID: 81924 RVA: 0x0030E3B9 File Offset: 0x0030C5B9
		public BottomBorder(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014005 RID: 81925 RVA: 0x0030E3C2 File Offset: 0x0030C5C2
		public BottomBorder(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014006 RID: 81926 RVA: 0x0030E3FB File Offset: 0x0030C5FB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BottomBorder>(deep);
		}

		// Token: 0x04008895 RID: 34965
		private const string tagName = "bottom";

		// Token: 0x04008896 RID: 34966
		private const byte tagNsId = 10;

		// Token: 0x04008897 RID: 34967
		internal const int ElementTypeIdConst = 10271;
	}
}
