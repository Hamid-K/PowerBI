using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028DB RID: 10459
	[GeneratedCode("DomGen", "2.0")]
	internal class PublicationTitle : OpenXmlLeafTextElement
	{
		// Token: 0x17006939 RID: 26937
		// (get) Token: 0x060149CC RID: 84428 RVA: 0x00314F50 File Offset: 0x00313150
		public override string LocalName
		{
			get
			{
				return "PublicationTitle";
			}
		}

		// Token: 0x1700693A RID: 26938
		// (get) Token: 0x060149CD RID: 84429 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700693B RID: 26939
		// (get) Token: 0x060149CE RID: 84430 RVA: 0x00314F57 File Offset: 0x00313157
		internal override int ElementTypeId
		{
			get
			{
				return 10813;
			}
		}

		// Token: 0x060149CF RID: 84431 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149D0 RID: 84432 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PublicationTitle()
		{
		}

		// Token: 0x060149D1 RID: 84433 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PublicationTitle(string text)
			: base(text)
		{
		}

		// Token: 0x060149D2 RID: 84434 RVA: 0x00314F60 File Offset: 0x00313160
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149D3 RID: 84435 RVA: 0x00314F7B File Offset: 0x0031317B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PublicationTitle>(deep);
		}

		// Token: 0x04008F1D RID: 36637
		private const string tagName = "PublicationTitle";

		// Token: 0x04008F1E RID: 36638
		private const byte tagNsId = 9;

		// Token: 0x04008F1F RID: 36639
		internal const int ElementTypeIdConst = 10813;
	}
}
