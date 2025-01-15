using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DF8 RID: 11768
	[GeneratedCode("DomGen", "2.0")]
	internal class SwapBordersFacingPages : OnOffType
	{
		// Token: 0x17008884 RID: 34948
		// (get) Token: 0x06018FC8 RID: 102344 RVA: 0x00345655 File Offset: 0x00343855
		public override string LocalName
		{
			get
			{
				return "swapBordersFacingPages";
			}
		}

		// Token: 0x17008885 RID: 34949
		// (get) Token: 0x06018FC9 RID: 102345 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008886 RID: 34950
		// (get) Token: 0x06018FCA RID: 102346 RVA: 0x0034565C File Offset: 0x0034385C
		internal override int ElementTypeId
		{
			get
			{
				return 12078;
			}
		}

		// Token: 0x06018FCB RID: 102347 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FCD RID: 102349 RVA: 0x00345663 File Offset: 0x00343863
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SwapBordersFacingPages>(deep);
		}

		// Token: 0x0400A653 RID: 42579
		private const string tagName = "swapBordersFacingPages";

		// Token: 0x0400A654 RID: 42580
		private const byte tagNsId = 23;

		// Token: 0x0400A655 RID: 42581
		internal const int ElementTypeIdConst = 12078;
	}
}
