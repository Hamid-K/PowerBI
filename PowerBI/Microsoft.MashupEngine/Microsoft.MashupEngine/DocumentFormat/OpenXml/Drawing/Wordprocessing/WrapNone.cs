using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x0200289C RID: 10396
	[GeneratedCode("DomGen", "2.0")]
	internal class WrapNone : OpenXmlLeafElement
	{
		// Token: 0x17006804 RID: 26628
		// (get) Token: 0x060146FE RID: 83710 RVA: 0x00313372 File Offset: 0x00311572
		public override string LocalName
		{
			get
			{
				return "wrapNone";
			}
		}

		// Token: 0x17006805 RID: 26629
		// (get) Token: 0x060146FF RID: 83711 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17006806 RID: 26630
		// (get) Token: 0x06014700 RID: 83712 RVA: 0x00313379 File Offset: 0x00311579
		internal override int ElementTypeId
		{
			get
			{
				return 10694;
			}
		}

		// Token: 0x06014701 RID: 83713 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014703 RID: 83715 RVA: 0x00313380 File Offset: 0x00311580
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WrapNone>(deep);
		}

		// Token: 0x04008E1D RID: 36381
		private const string tagName = "wrapNone";

		// Token: 0x04008E1E RID: 36382
		private const byte tagNsId = 16;

		// Token: 0x04008E1F RID: 36383
		internal const int ElementTypeIdConst = 10694;
	}
}
