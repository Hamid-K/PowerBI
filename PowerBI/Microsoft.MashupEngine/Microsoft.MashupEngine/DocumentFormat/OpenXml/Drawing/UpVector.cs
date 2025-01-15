using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027B8 RID: 10168
	[GeneratedCode("DomGen", "2.0")]
	internal class UpVector : Vector3DType
	{
		// Token: 0x17006343 RID: 25411
		// (get) Token: 0x06013BF1 RID: 80881 RVA: 0x0030B572 File Offset: 0x00309772
		public override string LocalName
		{
			get
			{
				return "up";
			}
		}

		// Token: 0x17006344 RID: 25412
		// (get) Token: 0x06013BF2 RID: 80882 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006345 RID: 25413
		// (get) Token: 0x06013BF3 RID: 80883 RVA: 0x0030B579 File Offset: 0x00309779
		internal override int ElementTypeId
		{
			get
			{
				return 10200;
			}
		}

		// Token: 0x06013BF4 RID: 80884 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013BF6 RID: 80886 RVA: 0x0030B580 File Offset: 0x00309780
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UpVector>(deep);
		}

		// Token: 0x04008791 RID: 34705
		private const string tagName = "up";

		// Token: 0x04008792 RID: 34706
		private const byte tagNsId = 10;

		// Token: 0x04008793 RID: 34707
		internal const int ElementTypeIdConst = 10200;
	}
}
