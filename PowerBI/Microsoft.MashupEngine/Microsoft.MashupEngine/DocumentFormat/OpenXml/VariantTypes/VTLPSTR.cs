using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002925 RID: 10533
	[GeneratedCode("DomGen", "2.0")]
	internal class VTLPSTR : OpenXmlLeafTextElement
	{
		// Token: 0x17006ACA RID: 27338
		// (get) Token: 0x06014D9B RID: 85403 RVA: 0x003180B4 File Offset: 0x003162B4
		public override string LocalName
		{
			get
			{
				return "lpstr";
			}
		}

		// Token: 0x17006ACB RID: 27339
		// (get) Token: 0x06014D9C RID: 85404 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006ACC RID: 27340
		// (get) Token: 0x06014D9D RID: 85405 RVA: 0x003180BB File Offset: 0x003162BB
		internal override int ElementTypeId
		{
			get
			{
				return 10983;
			}
		}

		// Token: 0x06014D9E RID: 85406 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D9F RID: 85407 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTLPSTR()
		{
		}

		// Token: 0x06014DA0 RID: 85408 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTLPSTR(string text)
			: base(text)
		{
		}

		// Token: 0x06014DA1 RID: 85409 RVA: 0x003180C4 File Offset: 0x003162C4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DA2 RID: 85410 RVA: 0x003180DF File Offset: 0x003162DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTLPSTR>(deep);
		}

		// Token: 0x04009021 RID: 36897
		private const string tagName = "lpstr";

		// Token: 0x04009022 RID: 36898
		private const byte tagNsId = 5;

		// Token: 0x04009023 RID: 36899
		internal const int ElementTypeIdConst = 10983;
	}
}
