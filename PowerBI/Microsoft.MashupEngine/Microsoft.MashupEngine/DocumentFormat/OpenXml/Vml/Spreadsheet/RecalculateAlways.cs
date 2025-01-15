using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021D8 RID: 8664
	[GeneratedCode("DomGen", "2.0")]
	internal class RecalculateAlways : OpenXmlLeafTextElement
	{
		// Token: 0x17003795 RID: 14229
		// (get) Token: 0x0600DC72 RID: 56434 RVA: 0x002BCC88 File Offset: 0x002BAE88
		public override string LocalName
		{
			get
			{
				return "RecalcAlways";
			}
		}

		// Token: 0x17003796 RID: 14230
		// (get) Token: 0x0600DC73 RID: 56435 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003797 RID: 14231
		// (get) Token: 0x0600DC74 RID: 56436 RVA: 0x002BCC8F File Offset: 0x002BAE8F
		internal override int ElementTypeId
		{
			get
			{
				return 12495;
			}
		}

		// Token: 0x0600DC75 RID: 56437 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC76 RID: 56438 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public RecalculateAlways()
		{
		}

		// Token: 0x0600DC77 RID: 56439 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public RecalculateAlways(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC78 RID: 56440 RVA: 0x002BCC98 File Offset: 0x002BAE98
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC79 RID: 56441 RVA: 0x002BCCB3 File Offset: 0x002BAEB3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RecalculateAlways>(deep);
		}

		// Token: 0x04006CBC RID: 27836
		private const string tagName = "RecalcAlways";

		// Token: 0x04006CBD RID: 27837
		private const byte tagNsId = 29;

		// Token: 0x04006CBE RID: 27838
		internal const int ElementTypeIdConst = 12495;
	}
}
