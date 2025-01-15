using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FEA RID: 12266
	[GeneratedCode("DomGen", "2.0")]
	internal class DisplayHorizontalDrawingGrid : UnsignedInt7Type
	{
		// Token: 0x1700951E RID: 38174
		// (get) Token: 0x0601AADB RID: 109275 RVA: 0x00365D5C File Offset: 0x00363F5C
		public override string LocalName
		{
			get
			{
				return "displayHorizontalDrawingGridEvery";
			}
		}

		// Token: 0x1700951F RID: 38175
		// (get) Token: 0x0601AADC RID: 109276 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009520 RID: 38176
		// (get) Token: 0x0601AADD RID: 109277 RVA: 0x00365D63 File Offset: 0x00363F63
		internal override int ElementTypeId
		{
			get
			{
				return 12011;
			}
		}

		// Token: 0x0601AADE RID: 109278 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AAE0 RID: 109280 RVA: 0x00365D72 File Offset: 0x00363F72
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DisplayHorizontalDrawingGrid>(deep);
		}

		// Token: 0x0400ADF7 RID: 44535
		private const string tagName = "displayHorizontalDrawingGridEvery";

		// Token: 0x0400ADF8 RID: 44536
		private const byte tagNsId = 23;

		// Token: 0x0400ADF9 RID: 44537
		internal const int ElementTypeIdConst = 12011;
	}
}
