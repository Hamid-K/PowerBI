using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021E3 RID: 8675
	[GeneratedCode("DomGen", "2.0")]
	internal class ListItem : OpenXmlLeafTextElement
	{
		// Token: 0x170037B6 RID: 14262
		// (get) Token: 0x0600DCCA RID: 56522 RVA: 0x002BCEC4 File Offset: 0x002BB0C4
		public override string LocalName
		{
			get
			{
				return "ListItem";
			}
		}

		// Token: 0x170037B7 RID: 14263
		// (get) Token: 0x0600DCCB RID: 56523 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037B8 RID: 14264
		// (get) Token: 0x0600DCCC RID: 56524 RVA: 0x002BCECB File Offset: 0x002BB0CB
		internal override int ElementTypeId
		{
			get
			{
				return 12475;
			}
		}

		// Token: 0x0600DCCD RID: 56525 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCCE RID: 56526 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ListItem()
		{
		}

		// Token: 0x0600DCCF RID: 56527 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ListItem(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCD0 RID: 56528 RVA: 0x002BCED4 File Offset: 0x002BB0D4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCD1 RID: 56529 RVA: 0x002BCEEF File Offset: 0x002BB0EF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ListItem>(deep);
		}

		// Token: 0x04006CDD RID: 27869
		private const string tagName = "ListItem";

		// Token: 0x04006CDE RID: 27870
		private const byte tagNsId = 29;

		// Token: 0x04006CDF RID: 27871
		internal const int ElementTypeIdConst = 12475;
	}
}
