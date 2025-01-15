using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D78 RID: 11640
	[GeneratedCode("DomGen", "2.0")]
	internal class SnapToGrid : OnOffType
	{
		// Token: 0x17008704 RID: 34564
		// (get) Token: 0x06018CC8 RID: 101576 RVA: 0x00344B1B File Offset: 0x00342D1B
		public override string LocalName
		{
			get
			{
				return "snapToGrid";
			}
		}

		// Token: 0x17008705 RID: 34565
		// (get) Token: 0x06018CC9 RID: 101577 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008706 RID: 34566
		// (get) Token: 0x06018CCA RID: 101578 RVA: 0x00344B22 File Offset: 0x00342D22
		internal override int ElementTypeId
		{
			get
			{
				return 11512;
			}
		}

		// Token: 0x06018CCB RID: 101579 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018CCD RID: 101581 RVA: 0x00344B29 File Offset: 0x00342D29
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SnapToGrid>(deep);
		}

		// Token: 0x0400A4D3 RID: 42195
		private const string tagName = "snapToGrid";

		// Token: 0x0400A4D4 RID: 42196
		private const byte tagNsId = 23;

		// Token: 0x0400A4D5 RID: 42197
		internal const int ElementTypeIdConst = 11512;
	}
}
