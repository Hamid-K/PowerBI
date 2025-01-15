using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200298E RID: 10638
	[GeneratedCode("DomGen", "2.0")]
	internal class EndChar : CharType
	{
		// Token: 0x17006CC0 RID: 27840
		// (get) Token: 0x06015213 RID: 86547 RVA: 0x0031B9A9 File Offset: 0x00319BA9
		public override string LocalName
		{
			get
			{
				return "endChr";
			}
		}

		// Token: 0x17006CC1 RID: 27841
		// (get) Token: 0x06015214 RID: 86548 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CC2 RID: 27842
		// (get) Token: 0x06015215 RID: 86549 RVA: 0x0031B9B0 File Offset: 0x00319BB0
		internal override int ElementTypeId
		{
			get
			{
				return 10891;
			}
		}

		// Token: 0x06015216 RID: 86550 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015218 RID: 86552 RVA: 0x0031B9B7 File Offset: 0x00319BB7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndChar>(deep);
		}

		// Token: 0x040091BB RID: 37307
		private const string tagName = "endChr";

		// Token: 0x040091BC RID: 37308
		private const byte tagNsId = 21;

		// Token: 0x040091BD RID: 37309
		internal const int ElementTypeIdConst = 10891;
	}
}
