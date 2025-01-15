using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021DB RID: 8667
	[GeneratedCode("DomGen", "2.0")]
	internal class UIObject : OpenXmlLeafTextElement
	{
		// Token: 0x1700379E RID: 14238
		// (get) Token: 0x0600DC8A RID: 56458 RVA: 0x002BCD24 File Offset: 0x002BAF24
		public override string LocalName
		{
			get
			{
				return "UIObj";
			}
		}

		// Token: 0x1700379F RID: 14239
		// (get) Token: 0x0600DC8B RID: 56459 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037A0 RID: 14240
		// (get) Token: 0x0600DC8C RID: 56460 RVA: 0x002BCD2B File Offset: 0x002BAF2B
		internal override int ElementTypeId
		{
			get
			{
				return 12498;
			}
		}

		// Token: 0x0600DC8D RID: 56461 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DC8E RID: 56462 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public UIObject()
		{
		}

		// Token: 0x0600DC8F RID: 56463 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public UIObject(string text)
			: base(text)
		{
		}

		// Token: 0x0600DC90 RID: 56464 RVA: 0x002BCD34 File Offset: 0x002BAF34
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new EnumValue<BooleanEntryWithBlankValues>
			{
				InnerText = text
			};
		}

		// Token: 0x0600DC91 RID: 56465 RVA: 0x002BCD4F File Offset: 0x002BAF4F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UIObject>(deep);
		}

		// Token: 0x04006CC5 RID: 27845
		private const string tagName = "UIObj";

		// Token: 0x04006CC6 RID: 27846
		private const byte tagNsId = 29;

		// Token: 0x04006CC7 RID: 27847
		internal const int ElementTypeIdConst = 12498;
	}
}
