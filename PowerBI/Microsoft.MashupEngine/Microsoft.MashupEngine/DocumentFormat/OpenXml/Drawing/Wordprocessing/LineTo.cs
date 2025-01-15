using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028A5 RID: 10405
	[GeneratedCode("DomGen", "2.0")]
	internal class LineTo : Point2DType
	{
		// Token: 0x1700686C RID: 26732
		// (get) Token: 0x060147D5 RID: 83925 RVA: 0x00313F46 File Offset: 0x00312146
		public override string LocalName
		{
			get
			{
				return "lineTo";
			}
		}

		// Token: 0x1700686D RID: 26733
		// (get) Token: 0x060147D6 RID: 83926 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x1700686E RID: 26734
		// (get) Token: 0x060147D7 RID: 83927 RVA: 0x00313F4D File Offset: 0x0031214D
		internal override int ElementTypeId
		{
			get
			{
				return 10702;
			}
		}

		// Token: 0x060147D8 RID: 83928 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060147DA RID: 83930 RVA: 0x00313F54 File Offset: 0x00312154
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineTo>(deep);
		}

		// Token: 0x04008E4F RID: 36431
		private const string tagName = "lineTo";

		// Token: 0x04008E50 RID: 36432
		private const byte tagNsId = 16;

		// Token: 0x04008E51 RID: 36433
		internal const int ElementTypeIdConst = 10702;
	}
}
