using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D77 RID: 11639
	[GeneratedCode("DomGen", "2.0")]
	internal class AdjustRightIndent : OnOffType
	{
		// Token: 0x17008701 RID: 34561
		// (get) Token: 0x06018CC2 RID: 101570 RVA: 0x00344B04 File Offset: 0x00342D04
		public override string LocalName
		{
			get
			{
				return "adjustRightInd";
			}
		}

		// Token: 0x17008702 RID: 34562
		// (get) Token: 0x06018CC3 RID: 101571 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008703 RID: 34563
		// (get) Token: 0x06018CC4 RID: 101572 RVA: 0x00344B0B File Offset: 0x00342D0B
		internal override int ElementTypeId
		{
			get
			{
				return 11511;
			}
		}

		// Token: 0x06018CC5 RID: 101573 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CC7 RID: 101575 RVA: 0x00344B12 File Offset: 0x00342D12
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AdjustRightIndent>(deep);
		}

		// Token: 0x0400A4D0 RID: 42192
		private const string tagName = "adjustRightInd";

		// Token: 0x0400A4D1 RID: 42193
		private const byte tagNsId = 23;

		// Token: 0x0400A4D2 RID: 42194
		internal const int ElementTypeIdConst = 11511;
	}
}
