using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200277E RID: 10110
	[GeneratedCode("DomGen", "2.0")]
	internal class Dark2Color : Color2Type
	{
		// Token: 0x170061B0 RID: 25008
		// (get) Token: 0x06013878 RID: 79992 RVA: 0x0030825D File Offset: 0x0030645D
		public override string LocalName
		{
			get
			{
				return "dk2";
			}
		}

		// Token: 0x170061B1 RID: 25009
		// (get) Token: 0x06013879 RID: 79993 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061B2 RID: 25010
		// (get) Token: 0x0601387A RID: 79994 RVA: 0x00308264 File Offset: 0x00306464
		internal override int ElementTypeId
		{
			get
			{
				return 10149;
			}
		}

		// Token: 0x0601387B RID: 79995 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601387C RID: 79996 RVA: 0x0030821A File Offset: 0x0030641A
		public Dark2Color()
		{
		}

		// Token: 0x0601387D RID: 79997 RVA: 0x00308222 File Offset: 0x00306422
		public Dark2Color(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601387E RID: 79998 RVA: 0x0030822B File Offset: 0x0030642B
		public Dark2Color(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601387F RID: 79999 RVA: 0x00308234 File Offset: 0x00306434
		public Dark2Color(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013880 RID: 80000 RVA: 0x0030826B File Offset: 0x0030646B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Dark2Color>(deep);
		}

		// Token: 0x04008697 RID: 34455
		private const string tagName = "dk2";

		// Token: 0x04008698 RID: 34456
		private const byte tagNsId = 10;

		// Token: 0x04008699 RID: 34457
		internal const int ElementTypeIdConst = 10149;
	}
}
