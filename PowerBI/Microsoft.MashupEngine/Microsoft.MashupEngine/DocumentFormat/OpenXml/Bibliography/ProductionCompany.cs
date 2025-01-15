using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028DA RID: 10458
	[GeneratedCode("DomGen", "2.0")]
	internal class ProductionCompany : OpenXmlLeafTextElement
	{
		// Token: 0x17006936 RID: 26934
		// (get) Token: 0x060149C4 RID: 84420 RVA: 0x00314F1C File Offset: 0x0031311C
		public override string LocalName
		{
			get
			{
				return "ProductionCompany";
			}
		}

		// Token: 0x17006937 RID: 26935
		// (get) Token: 0x060149C5 RID: 84421 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006938 RID: 26936
		// (get) Token: 0x060149C6 RID: 84422 RVA: 0x00314F23 File Offset: 0x00313123
		internal override int ElementTypeId
		{
			get
			{
				return 10812;
			}
		}

		// Token: 0x060149C7 RID: 84423 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149C8 RID: 84424 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ProductionCompany()
		{
		}

		// Token: 0x060149C9 RID: 84425 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ProductionCompany(string text)
			: base(text)
		{
		}

		// Token: 0x060149CA RID: 84426 RVA: 0x00314F2C File Offset: 0x0031312C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149CB RID: 84427 RVA: 0x00314F47 File Offset: 0x00313147
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ProductionCompany>(deep);
		}

		// Token: 0x04008F1A RID: 36634
		private const string tagName = "ProductionCompany";

		// Token: 0x04008F1B RID: 36635
		private const byte tagNsId = 9;

		// Token: 0x04008F1C RID: 36636
		internal const int ElementTypeIdConst = 10812;
	}
}
