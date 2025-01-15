using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021DA RID: 8666
	[GeneratedCode("DomGen", "2.0")]
	internal class DdeObject : OpenXmlLeafTextElement
	{
		// Token: 0x1700379B RID: 14235
		// (get) Token: 0x0600DC82 RID: 56450 RVA: 0x002BCCF0 File Offset: 0x002BAEF0
		public override string LocalName
		{
			get
			{
				return "DDE";
			}
		}

		// Token: 0x1700379C RID: 14236
		// (get) Token: 0x0600DC83 RID: 56451 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700379D RID: 14237
		// (get) Token: 0x0600DC84 RID: 56452 RVA: 0x002BCCF7 File Offset: 0x002BAEF7
		internal override int ElementTypeId
		{
			get
			{
				return 12497;
			}
		}

		// Token: 0x0600DC85 RID: 56453 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC86 RID: 56454 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DdeObject()
		{
		}

		// Token: 0x0600DC87 RID: 56455 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DdeObject(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC88 RID: 56456 RVA: 0x002BCD00 File Offset: 0x002BAF00
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC89 RID: 56457 RVA: 0x002BCD1B File Offset: 0x002BAF1B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DdeObject>(deep);
		}

		// Token: 0x04006CC2 RID: 27842
		private const string tagName = "DDE";

		// Token: 0x04006CC3 RID: 27843
		private const byte tagNsId = 29;

		// Token: 0x04006CC4 RID: 27844
		internal const int ElementTypeIdConst = 12497;
	}
}
