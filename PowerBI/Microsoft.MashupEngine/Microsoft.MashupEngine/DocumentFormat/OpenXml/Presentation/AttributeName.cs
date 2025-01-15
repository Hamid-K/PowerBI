using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A28 RID: 10792
	[GeneratedCode("DomGen", "2.0")]
	internal class AttributeName : OpenXmlLeafTextElement
	{
		// Token: 0x170070A2 RID: 28834
		// (get) Token: 0x06015AB3 RID: 88755 RVA: 0x00321E2B File Offset: 0x0032002B
		public override string LocalName
		{
			get
			{
				return "attrName";
			}
		}

		// Token: 0x170070A3 RID: 28835
		// (get) Token: 0x06015AB4 RID: 88756 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070A4 RID: 28836
		// (get) Token: 0x06015AB5 RID: 88757 RVA: 0x00321E32 File Offset: 0x00320032
		internal override int ElementTypeId
		{
			get
			{
				return 12215;
			}
		}

		// Token: 0x06015AB6 RID: 88758 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015AB7 RID: 88759 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public AttributeName()
		{
		}

		// Token: 0x06015AB8 RID: 88760 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public AttributeName(string text)
			: base(text)
		{
		}

		// Token: 0x06015AB9 RID: 88761 RVA: 0x00321E3C File Offset: 0x0032003C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06015ABA RID: 88762 RVA: 0x00321E57 File Offset: 0x00320057
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AttributeName>(deep);
		}

		// Token: 0x0400944A RID: 37962
		private const string tagName = "attrName";

		// Token: 0x0400944B RID: 37963
		private const byte tagNsId = 24;

		// Token: 0x0400944C RID: 37964
		internal const int ElementTypeIdConst = 12215;
	}
}
