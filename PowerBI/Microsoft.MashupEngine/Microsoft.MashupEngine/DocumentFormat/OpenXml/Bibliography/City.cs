using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028C3 RID: 10435
	[GeneratedCode("DomGen", "2.0")]
	internal class City : OpenXmlLeafTextElement
	{
		// Token: 0x170068F1 RID: 26865
		// (get) Token: 0x0601490C RID: 84236 RVA: 0x00314A70 File Offset: 0x00312C70
		public override string LocalName
		{
			get
			{
				return "City";
			}
		}

		// Token: 0x170068F2 RID: 26866
		// (get) Token: 0x0601490D RID: 84237 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068F3 RID: 26867
		// (get) Token: 0x0601490E RID: 84238 RVA: 0x00314A77 File Offset: 0x00312C77
		internal override int ElementTypeId
		{
			get
			{
				return 10789;
			}
		}

		// Token: 0x0601490F RID: 84239 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014910 RID: 84240 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public City()
		{
		}

		// Token: 0x06014911 RID: 84241 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public City(string text)
			: base(text)
		{
		}

		// Token: 0x06014912 RID: 84242 RVA: 0x00314A80 File Offset: 0x00312C80
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014913 RID: 84243 RVA: 0x00314A9B File Offset: 0x00312C9B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<City>(deep);
		}

		// Token: 0x04008ED5 RID: 36565
		private const string tagName = "City";

		// Token: 0x04008ED6 RID: 36566
		private const byte tagNsId = 9;

		// Token: 0x04008ED7 RID: 36567
		internal const int ElementTypeIdConst = 10789;
	}
}
