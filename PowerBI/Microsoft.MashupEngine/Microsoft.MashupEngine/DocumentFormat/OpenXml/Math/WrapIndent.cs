using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029BC RID: 10684
	[GeneratedCode("DomGen", "2.0")]
	internal class WrapIndent : TwipsMeasureType
	{
		// Token: 0x17006DAC RID: 28076
		// (get) Token: 0x06015428 RID: 87080 RVA: 0x0031D52D File Offset: 0x0031B72D
		public override string LocalName
		{
			get
			{
				return "wrapIndent";
			}
		}

		// Token: 0x17006DAD RID: 28077
		// (get) Token: 0x06015429 RID: 87081 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DAE RID: 28078
		// (get) Token: 0x0601542A RID: 87082 RVA: 0x0031D534 File Offset: 0x0031B734
		internal override int ElementTypeId
		{
			get
			{
				return 10958;
			}
		}

		// Token: 0x0601542B RID: 87083 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601542D RID: 87085 RVA: 0x0031D53B File Offset: 0x0031B73B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WrapIndent>(deep);
		}

		// Token: 0x04009264 RID: 37476
		private const string tagName = "wrapIndent";

		// Token: 0x04009265 RID: 37477
		private const byte tagNsId = 21;

		// Token: 0x04009266 RID: 37478
		internal const int ElementTypeIdConst = 10958;
	}
}
