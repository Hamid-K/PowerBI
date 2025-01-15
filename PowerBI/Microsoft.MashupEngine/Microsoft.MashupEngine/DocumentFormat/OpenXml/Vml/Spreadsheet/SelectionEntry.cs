using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021F2 RID: 8690
	[GeneratedCode("DomGen", "2.0")]
	internal class SelectionEntry : OpenXmlLeafTextElement
	{
		// Token: 0x170037E3 RID: 14307
		// (get) Token: 0x0600DD42 RID: 56642 RVA: 0x002BD1D0 File Offset: 0x002BB3D0
		public override string LocalName
		{
			get
			{
				return "Sel";
			}
		}

		// Token: 0x170037E4 RID: 14308
		// (get) Token: 0x0600DD43 RID: 56643 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037E5 RID: 14309
		// (get) Token: 0x0600DD44 RID: 56644 RVA: 0x002BD1D7 File Offset: 0x002BB3D7
		internal override int ElementTypeId
		{
			get
			{
				return 12470;
			}
		}

		// Token: 0x0600DD45 RID: 56645 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD46 RID: 56646 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public SelectionEntry()
		{
		}

		// Token: 0x0600DD47 RID: 56647 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public SelectionEntry(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD48 RID: 56648 RVA: 0x002BD1E0 File Offset: 0x002BB3E0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new IntegerValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD49 RID: 56649 RVA: 0x002BD1FB File Offset: 0x002BB3FB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SelectionEntry>(deep);
		}

		// Token: 0x04006D0A RID: 27914
		private const string tagName = "Sel";

		// Token: 0x04006D0B RID: 27915
		private const byte tagNsId = 29;

		// Token: 0x04006D0C RID: 27916
		internal const int ElementTypeIdConst = 12470;
	}
}
