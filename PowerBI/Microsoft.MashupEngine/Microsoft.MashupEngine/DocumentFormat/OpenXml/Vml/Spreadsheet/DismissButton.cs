using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021CA RID: 8650
	[GeneratedCode("DomGen", "2.0")]
	internal class DismissButton : OpenXmlLeafTextElement
	{
		// Token: 0x1700376B RID: 14187
		// (get) Token: 0x0600DC02 RID: 56322 RVA: 0x002BC9B0 File Offset: 0x002BABB0
		public override string LocalName
		{
			get
			{
				return "Dismiss";
			}
		}

		// Token: 0x1700376C RID: 14188
		// (get) Token: 0x0600DC03 RID: 56323 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700376D RID: 14189
		// (get) Token: 0x0600DC04 RID: 56324 RVA: 0x002BC9B7 File Offset: 0x002BABB7
		internal override int ElementTypeId
		{
			get
			{
				return 12456;
			}
		}

		// Token: 0x0600DC05 RID: 56325 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC06 RID: 56326 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DismissButton()
		{
		}

		// Token: 0x0600DC07 RID: 56327 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DismissButton(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC08 RID: 56328 RVA: 0x002BC9C0 File Offset: 0x002BABC0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC09 RID: 56329 RVA: 0x002BC9DB File Offset: 0x002BABDB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DismissButton>(deep);
		}

		// Token: 0x04006C92 RID: 27794
		private const string tagName = "Dismiss";

		// Token: 0x04006C93 RID: 27795
		private const byte tagNsId = 29;

		// Token: 0x04006C94 RID: 27796
		internal const int ElementTypeIdConst = 12456;
	}
}
