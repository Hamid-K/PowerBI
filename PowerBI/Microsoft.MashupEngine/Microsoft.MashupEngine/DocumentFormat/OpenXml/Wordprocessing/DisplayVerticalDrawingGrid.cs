using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FEB RID: 12267
	[GeneratedCode("DomGen", "2.0")]
	internal class DisplayVerticalDrawingGrid : UnsignedInt7Type
	{
		// Token: 0x17009521 RID: 38177
		// (get) Token: 0x0601AAE1 RID: 109281 RVA: 0x00365D7B File Offset: 0x00363F7B
		public override string LocalName
		{
			get
			{
				return "displayVerticalDrawingGridEvery";
			}
		}

		// Token: 0x17009522 RID: 38178
		// (get) Token: 0x0601AAE2 RID: 109282 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009523 RID: 38179
		// (get) Token: 0x0601AAE3 RID: 109283 RVA: 0x00365D82 File Offset: 0x00363F82
		internal override int ElementTypeId
		{
			get
			{
				return 12012;
			}
		}

		// Token: 0x0601AAE4 RID: 109284 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AAE6 RID: 109286 RVA: 0x00365D89 File Offset: 0x00363F89
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DisplayVerticalDrawingGrid>(deep);
		}

		// Token: 0x0400ADFA RID: 44538
		private const string tagName = "displayVerticalDrawingGridEvery";

		// Token: 0x0400ADFB RID: 44539
		private const byte tagNsId = 23;

		// Token: 0x0400ADFC RID: 44540
		internal const int ElementTypeIdConst = 12012;
	}
}
