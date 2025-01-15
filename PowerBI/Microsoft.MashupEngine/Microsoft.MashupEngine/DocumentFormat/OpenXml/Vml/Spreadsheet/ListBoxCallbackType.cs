using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021E2 RID: 8674
	[GeneratedCode("DomGen", "2.0")]
	internal class ListBoxCallbackType : OpenXmlLeafTextElement
	{
		// Token: 0x170037B3 RID: 14259
		// (get) Token: 0x0600DCC2 RID: 56514 RVA: 0x002BCE90 File Offset: 0x002BB090
		public override string LocalName
		{
			get
			{
				return "LCT";
			}
		}

		// Token: 0x170037B4 RID: 14260
		// (get) Token: 0x0600DCC3 RID: 56515 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037B5 RID: 14261
		// (get) Token: 0x0600DCC4 RID: 56516 RVA: 0x002BCE97 File Offset: 0x002BB097
		internal override int ElementTypeId
		{
			get
			{
				return 12474;
			}
		}

		// Token: 0x0600DCC5 RID: 56517 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DCC6 RID: 56518 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ListBoxCallbackType()
		{
		}

		// Token: 0x0600DCC7 RID: 56519 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ListBoxCallbackType(string text)
			: base(text)
		{
		}

		// Token: 0x0600DCC8 RID: 56520 RVA: 0x002BCEA0 File Offset: 0x002BB0A0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DCC9 RID: 56521 RVA: 0x002BCEBB File Offset: 0x002BB0BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ListBoxCallbackType>(deep);
		}

		// Token: 0x04006CDA RID: 27866
		private const string tagName = "LCT";

		// Token: 0x04006CDB RID: 27867
		private const byte tagNsId = 29;

		// Token: 0x04006CDC RID: 27868
		internal const int ElementTypeIdConst = 12474;
	}
}
