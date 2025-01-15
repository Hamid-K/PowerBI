using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200278F RID: 10127
	[GeneratedCode("DomGen", "2.0")]
	internal class Extents : PositiveSize2DType
	{
		// Token: 0x170061E6 RID: 25062
		// (get) Token: 0x06013905 RID: 80133 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170061E7 RID: 25063
		// (get) Token: 0x06013906 RID: 80134 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061E8 RID: 25064
		// (get) Token: 0x06013907 RID: 80135 RVA: 0x003084EF File Offset: 0x003066EF
		internal override int ElementTypeId
		{
			get
			{
				return 10162;
			}
		}

		// Token: 0x06013908 RID: 80136 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601390A RID: 80138 RVA: 0x003084FE File Offset: 0x003066FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Extents>(deep);
		}

		// Token: 0x040086C7 RID: 34503
		private const string tagName = "ext";

		// Token: 0x040086C8 RID: 34504
		private const byte tagNsId = 10;

		// Token: 0x040086C9 RID: 34505
		internal const int ElementTypeIdConst = 10162;
	}
}
