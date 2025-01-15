using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027B7 RID: 10167
	[GeneratedCode("DomGen", "2.0")]
	internal class Normal : Vector3DType
	{
		// Token: 0x17006340 RID: 25408
		// (get) Token: 0x06013BEB RID: 80875 RVA: 0x0030B553 File Offset: 0x00309753
		public override string LocalName
		{
			get
			{
				return "norm";
			}
		}

		// Token: 0x17006341 RID: 25409
		// (get) Token: 0x06013BEC RID: 80876 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006342 RID: 25410
		// (get) Token: 0x06013BED RID: 80877 RVA: 0x0030B55A File Offset: 0x0030975A
		internal override int ElementTypeId
		{
			get
			{
				return 10199;
			}
		}

		// Token: 0x06013BEE RID: 80878 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013BF0 RID: 80880 RVA: 0x0030B569 File Offset: 0x00309769
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Normal>(deep);
		}

		// Token: 0x0400878E RID: 34702
		private const string tagName = "norm";

		// Token: 0x0400878F RID: 34703
		private const byte tagNsId = 10;

		// Token: 0x04008790 RID: 34704
		internal const int ElementTypeIdConst = 10199;
	}
}
