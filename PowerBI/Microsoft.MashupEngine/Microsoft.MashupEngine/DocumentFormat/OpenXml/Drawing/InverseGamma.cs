using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026F1 RID: 9969
	[GeneratedCode("DomGen", "2.0")]
	internal class InverseGamma : OpenXmlLeafElement
	{
		// Token: 0x17005DFB RID: 24059
		// (get) Token: 0x06013022 RID: 77858 RVA: 0x0030199D File Offset: 0x002FFB9D
		public override string LocalName
		{
			get
			{
				return "invGamma";
			}
		}

		// Token: 0x17005DFC RID: 24060
		// (get) Token: 0x06013023 RID: 77859 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DFD RID: 24061
		// (get) Token: 0x06013024 RID: 77860 RVA: 0x003019A4 File Offset: 0x002FFBA4
		internal override int ElementTypeId
		{
			get
			{
				return 10033;
			}
		}

		// Token: 0x06013025 RID: 77861 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013027 RID: 77863 RVA: 0x003019AB File Offset: 0x002FFBAB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InverseGamma>(deep);
		}

		// Token: 0x0400843D RID: 33853
		private const string tagName = "invGamma";

		// Token: 0x0400843E RID: 33854
		private const byte tagNsId = 10;

		// Token: 0x0400843F RID: 33855
		internal const int ElementTypeIdConst = 10033;
	}
}
