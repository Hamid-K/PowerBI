using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021CC RID: 8652
	[GeneratedCode("DomGen", "2.0")]
	internal class RowHidden : OpenXmlLeafTextElement
	{
		// Token: 0x17003771 RID: 14193
		// (get) Token: 0x0600DC12 RID: 56338 RVA: 0x002BCA18 File Offset: 0x002BAC18
		public override string LocalName
		{
			get
			{
				return "RowHidden";
			}
		}

		// Token: 0x17003772 RID: 14194
		// (get) Token: 0x0600DC13 RID: 56339 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x17003773 RID: 14195
		// (get) Token: 0x0600DC14 RID: 56340 RVA: 0x002BCA1F File Offset: 0x002BAC1F
		internal override int ElementTypeId
		{
			get
			{
				return 12462;
			}
		}

		// Token: 0x0600DC15 RID: 56341 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC16 RID: 56342 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public RowHidden()
		{
		}

		// Token: 0x0600DC17 RID: 56343 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public RowHidden(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC18 RID: 56344 RVA: 0x002BCA28 File Offset: 0x002BAC28
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC19 RID: 56345 RVA: 0x002BCA43 File Offset: 0x002BAC43
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowHidden>(deep);
		}

		// Token: 0x04006C98 RID: 27800
		private const string tagName = "RowHidden";

		// Token: 0x04006C99 RID: 27801
		private const byte tagNsId = 29;

		// Token: 0x04006C9A RID: 27802
		internal const int ElementTypeIdConst = 12462;
	}
}
