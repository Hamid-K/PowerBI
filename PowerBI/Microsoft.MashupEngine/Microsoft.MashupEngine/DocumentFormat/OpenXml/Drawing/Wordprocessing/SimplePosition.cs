using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028A6 RID: 10406
	[GeneratedCode("DomGen", "2.0")]
	internal class SimplePosition : Point2DType
	{
		// Token: 0x1700686F RID: 26735
		// (get) Token: 0x060147DB RID: 83931 RVA: 0x00313F5D File Offset: 0x0031215D
		public override string LocalName
		{
			get
			{
				return "simplePos";
			}
		}

		// Token: 0x17006870 RID: 26736
		// (get) Token: 0x060147DC RID: 83932 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17006871 RID: 26737
		// (get) Token: 0x060147DD RID: 83933 RVA: 0x00313F64 File Offset: 0x00312164
		internal override int ElementTypeId
		{
			get
			{
				return 10705;
			}
		}

		// Token: 0x060147DE RID: 83934 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060147E0 RID: 83936 RVA: 0x00313F6B File Offset: 0x0031216B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SimplePosition>(deep);
		}

		// Token: 0x04008E52 RID: 36434
		private const string tagName = "simplePos";

		// Token: 0x04008E53 RID: 36435
		private const byte tagNsId = 16;

		// Token: 0x04008E54 RID: 36436
		internal const int ElementTypeIdConst = 10705;
	}
}
