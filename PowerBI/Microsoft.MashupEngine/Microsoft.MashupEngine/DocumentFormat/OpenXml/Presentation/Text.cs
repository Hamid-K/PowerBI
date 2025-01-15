using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A29 RID: 10793
	[GeneratedCode("DomGen", "2.0")]
	internal class Text : OpenXmlLeafTextElement
	{
		// Token: 0x170070A5 RID: 28837
		// (get) Token: 0x06015ABB RID: 88763 RVA: 0x00321E60 File Offset: 0x00320060
		public override string LocalName
		{
			get
			{
				return "text";
			}
		}

		// Token: 0x170070A6 RID: 28838
		// (get) Token: 0x06015ABC RID: 88764 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070A7 RID: 28839
		// (get) Token: 0x06015ABD RID: 88765 RVA: 0x00321E67 File Offset: 0x00320067
		internal override int ElementTypeId
		{
			get
			{
				return 12315;
			}
		}

		// Token: 0x06015ABE RID: 88766 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015ABF RID: 88767 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Text()
		{
		}

		// Token: 0x06015AC0 RID: 88768 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Text(string text)
			: base(text)
		{
		}

		// Token: 0x06015AC1 RID: 88769 RVA: 0x00321E70 File Offset: 0x00320070
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06015AC2 RID: 88770 RVA: 0x00321E8B File Offset: 0x0032008B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Text>(deep);
		}

		// Token: 0x0400944D RID: 37965
		private const string tagName = "text";

		// Token: 0x0400944E RID: 37966
		private const byte tagNsId = 24;

		// Token: 0x0400944F RID: 37967
		internal const int ElementTypeIdConst = 12315;
	}
}
