using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029CE RID: 10702
	[GeneratedCode("DomGen", "2.0")]
	internal class Justification : OfficeMathJustificationType
	{
		// Token: 0x17006E1D RID: 28189
		// (get) Token: 0x06015526 RID: 87334 RVA: 0x0031DF1C File Offset: 0x0031C11C
		public override string LocalName
		{
			get
			{
				return "jc";
			}
		}

		// Token: 0x17006E1E RID: 28190
		// (get) Token: 0x06015527 RID: 87335 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006E1F RID: 28191
		// (get) Token: 0x06015528 RID: 87336 RVA: 0x0031DF23 File Offset: 0x0031C123
		internal override int ElementTypeId
		{
			get
			{
				return 10945;
			}
		}

		// Token: 0x06015529 RID: 87337 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601552B RID: 87339 RVA: 0x0031DF32 File Offset: 0x0031C132
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Justification>(deep);
		}

		// Token: 0x040092AC RID: 37548
		private const string tagName = "jc";

		// Token: 0x040092AD RID: 37549
		private const byte tagNsId = 21;

		// Token: 0x040092AE RID: 37550
		internal const int ElementTypeIdConst = 10945;
	}
}
