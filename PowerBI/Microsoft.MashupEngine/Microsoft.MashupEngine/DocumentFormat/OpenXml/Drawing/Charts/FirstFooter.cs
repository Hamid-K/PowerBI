using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002576 RID: 9590
	[GeneratedCode("DomGen", "2.0")]
	internal class FirstFooter : OpenXmlLeafTextElement
	{
		// Token: 0x170055E9 RID: 21993
		// (get) Token: 0x06011E26 RID: 73254 RVA: 0x002F353C File Offset: 0x002F173C
		public override string LocalName
		{
			get
			{
				return "firstFooter";
			}
		}

		// Token: 0x170055EA RID: 21994
		// (get) Token: 0x06011E27 RID: 73255 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055EB RID: 21995
		// (get) Token: 0x06011E28 RID: 73256 RVA: 0x002F3543 File Offset: 0x002F1743
		internal override int ElementTypeId
		{
			get
			{
				return 10515;
			}
		}

		// Token: 0x06011E29 RID: 73257 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E2A RID: 73258 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FirstFooter()
		{
		}

		// Token: 0x06011E2B RID: 73259 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FirstFooter(string text)
			: base(text)
		{
		}

		// Token: 0x06011E2C RID: 73260 RVA: 0x002F354C File Offset: 0x002F174C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011E2D RID: 73261 RVA: 0x002F3567 File Offset: 0x002F1767
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FirstFooter>(deep);
		}

		// Token: 0x04007D15 RID: 32021
		private const string tagName = "firstFooter";

		// Token: 0x04007D16 RID: 32022
		private const byte tagNsId = 11;

		// Token: 0x04007D17 RID: 32023
		internal const int ElementTypeIdConst = 10515;
	}
}
