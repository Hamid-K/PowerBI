using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028BA RID: 10426
	[GeneratedCode("DomGen", "2.0")]
	internal class Middle : OpenXmlLeafTextElement
	{
		// Token: 0x170068D6 RID: 26838
		// (get) Token: 0x060148C4 RID: 84164 RVA: 0x0031489C File Offset: 0x00312A9C
		public override string LocalName
		{
			get
			{
				return "Middle";
			}
		}

		// Token: 0x170068D7 RID: 26839
		// (get) Token: 0x060148C5 RID: 84165 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068D8 RID: 26840
		// (get) Token: 0x060148C6 RID: 84166 RVA: 0x003148A3 File Offset: 0x00312AA3
		internal override int ElementTypeId
		{
			get
			{
				return 10762;
			}
		}

		// Token: 0x060148C7 RID: 84167 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060148C8 RID: 84168 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Middle()
		{
		}

		// Token: 0x060148C9 RID: 84169 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Middle(string text)
			: base(text)
		{
		}

		// Token: 0x060148CA RID: 84170 RVA: 0x003148AC File Offset: 0x00312AAC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060148CB RID: 84171 RVA: 0x003148C7 File Offset: 0x00312AC7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Middle>(deep);
		}

		// Token: 0x04008EBA RID: 36538
		private const string tagName = "Middle";

		// Token: 0x04008EBB RID: 36539
		private const byte tagNsId = 9;

		// Token: 0x04008EBC RID: 36540
		internal const int ElementTypeIdConst = 10762;
	}
}
