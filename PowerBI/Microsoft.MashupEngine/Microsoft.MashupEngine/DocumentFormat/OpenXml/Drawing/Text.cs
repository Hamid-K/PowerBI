using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200282B RID: 10283
	[GeneratedCode("DomGen", "2.0")]
	internal class Text : OpenXmlLeafTextElement
	{
		// Token: 0x170065E6 RID: 26086
		// (get) Token: 0x0601423B RID: 82491 RVA: 0x00300F6A File Offset: 0x002FF16A
		public override string LocalName
		{
			get
			{
				return "t";
			}
		}

		// Token: 0x170065E7 RID: 26087
		// (get) Token: 0x0601423C RID: 82492 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065E8 RID: 26088
		// (get) Token: 0x0601423D RID: 82493 RVA: 0x0030FB2B File Offset: 0x0030DD2B
		internal override int ElementTypeId
		{
			get
			{
				return 10316;
			}
		}

		// Token: 0x0601423E RID: 82494 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601423F RID: 82495 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Text()
		{
		}

		// Token: 0x06014240 RID: 82496 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Text(string text)
			: base(text)
		{
		}

		// Token: 0x06014241 RID: 82497 RVA: 0x0030FB34 File Offset: 0x0030DD34
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014242 RID: 82498 RVA: 0x0030FB4F File Offset: 0x0030DD4F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Text>(deep);
		}

		// Token: 0x04008933 RID: 35123
		private const string tagName = "t";

		// Token: 0x04008934 RID: 35124
		private const byte tagNsId = 10;

		// Token: 0x04008935 RID: 35125
		internal const int ElementTypeIdConst = 10316;
	}
}
