using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021CD RID: 8653
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnHidden : OpenXmlLeafTextElement
	{
		// Token: 0x17003774 RID: 14196
		// (get) Token: 0x0600DC1A RID: 56346 RVA: 0x002BCA4C File Offset: 0x002BAC4C
		public override string LocalName
		{
			get
			{
				return "ColHidden";
			}
		}

		// Token: 0x17003775 RID: 14197
		// (get) Token: 0x0600DC1B RID: 56347 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003776 RID: 14198
		// (get) Token: 0x0600DC1C RID: 56348 RVA: 0x002BCA53 File Offset: 0x002BAC53
		internal override int ElementTypeId
		{
			get
			{
				return 12463;
			}
		}

		// Token: 0x0600DC1D RID: 56349 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC1E RID: 56350 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ColumnHidden()
		{
		}

		// Token: 0x0600DC1F RID: 56351 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ColumnHidden(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC20 RID: 56352 RVA: 0x002BCA5C File Offset: 0x002BAC5C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC21 RID: 56353 RVA: 0x002BCA77 File Offset: 0x002BAC77
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnHidden>(deep);
		}

		// Token: 0x04006C9B RID: 27803
		private const string tagName = "ColHidden";

		// Token: 0x04006C9C RID: 27804
		private const byte tagNsId = 29;

		// Token: 0x04006C9D RID: 27805
		internal const int ElementTypeIdConst = 12463;
	}
}
