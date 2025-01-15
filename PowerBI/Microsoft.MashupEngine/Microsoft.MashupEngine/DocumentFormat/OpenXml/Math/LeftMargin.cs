using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029B6 RID: 10678
	[GeneratedCode("DomGen", "2.0")]
	internal class LeftMargin : TwipsMeasureType
	{
		// Token: 0x17006D9A RID: 28058
		// (get) Token: 0x06015404 RID: 87044 RVA: 0x0031D4A3 File Offset: 0x0031B6A3
		public override string LocalName
		{
			get
			{
				return "lMargin";
			}
		}

		// Token: 0x17006D9B RID: 28059
		// (get) Token: 0x06015405 RID: 87045 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D9C RID: 28060
		// (get) Token: 0x06015406 RID: 87046 RVA: 0x0031D4AA File Offset: 0x0031B6AA
		internal override int ElementTypeId
		{
			get
			{
				return 10951;
			}
		}

		// Token: 0x06015407 RID: 87047 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015409 RID: 87049 RVA: 0x0031D4B1 File Offset: 0x0031B6B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LeftMargin>(deep);
		}

		// Token: 0x04009252 RID: 37458
		private const string tagName = "lMargin";

		// Token: 0x04009253 RID: 37459
		private const byte tagNsId = 21;

		// Token: 0x04009254 RID: 37460
		internal const int ElementTypeIdConst = 10951;
	}
}
