using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002917 RID: 10519
	[GeneratedCode("DomGen", "2.0")]
	internal class VTNull : OpenXmlLeafElement
	{
		// Token: 0x17006AA0 RID: 27296
		// (get) Token: 0x06014D2D RID: 85293 RVA: 0x001D17D1 File Offset: 0x001CF9D1
		public override string LocalName
		{
			get
			{
				return "null";
			}
		}

		// Token: 0x17006AA1 RID: 27297
		// (get) Token: 0x06014D2E RID: 85294 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AA2 RID: 27298
		// (get) Token: 0x06014D2F RID: 85295 RVA: 0x00317DFF File Offset: 0x00315FFF
		internal override int ElementTypeId
		{
			get
			{
				return 10969;
			}
		}

		// Token: 0x06014D30 RID: 85296 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D32 RID: 85298 RVA: 0x00317E06 File Offset: 0x00316006
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTNull>(deep);
		}

		// Token: 0x04008FF7 RID: 36855
		private const string tagName = "null";

		// Token: 0x04008FF8 RID: 36856
		private const byte tagNsId = 5;

		// Token: 0x04008FF9 RID: 36857
		internal const int ElementTypeIdConst = 10969;
	}
}
