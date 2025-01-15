using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028C6 RID: 10438
	[GeneratedCode("DomGen", "2.0")]
	internal class CountryRegion : OpenXmlLeafTextElement
	{
		// Token: 0x170068FA RID: 26874
		// (get) Token: 0x06014924 RID: 84260 RVA: 0x00314B0C File Offset: 0x00312D0C
		public override string LocalName
		{
			get
			{
				return "CountryRegion";
			}
		}

		// Token: 0x170068FB RID: 26875
		// (get) Token: 0x06014925 RID: 84261 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068FC RID: 26876
		// (get) Token: 0x06014926 RID: 84262 RVA: 0x00314B13 File Offset: 0x00312D13
		internal override int ElementTypeId
		{
			get
			{
				return 10792;
			}
		}

		// Token: 0x06014927 RID: 84263 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014928 RID: 84264 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public CountryRegion()
		{
		}

		// Token: 0x06014929 RID: 84265 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public CountryRegion(string text)
			: base(text)
		{
		}

		// Token: 0x0601492A RID: 84266 RVA: 0x00314B1C File Offset: 0x00312D1C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601492B RID: 84267 RVA: 0x00314B37 File Offset: 0x00312D37
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CountryRegion>(deep);
		}

		// Token: 0x04008EDE RID: 36574
		private const string tagName = "CountryRegion";

		// Token: 0x04008EDF RID: 36575
		private const byte tagNsId = 9;

		// Token: 0x04008EE0 RID: 36576
		internal const int ElementTypeIdConst = 10792;
	}
}
