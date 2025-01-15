using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028C4 RID: 10436
	[GeneratedCode("DomGen", "2.0")]
	internal class Comments : OpenXmlLeafTextElement
	{
		// Token: 0x170068F4 RID: 26868
		// (get) Token: 0x06014914 RID: 84244 RVA: 0x00314AA4 File Offset: 0x00312CA4
		public override string LocalName
		{
			get
			{
				return "Comments";
			}
		}

		// Token: 0x170068F5 RID: 26869
		// (get) Token: 0x06014915 RID: 84245 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170068F6 RID: 26870
		// (get) Token: 0x06014916 RID: 84246 RVA: 0x00314AAB File Offset: 0x00312CAB
		internal override int ElementTypeId
		{
			get
			{
				return 10790;
			}
		}

		// Token: 0x06014917 RID: 84247 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014918 RID: 84248 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Comments()
		{
		}

		// Token: 0x06014919 RID: 84249 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Comments(string text)
			: base(text)
		{
		}

		// Token: 0x0601491A RID: 84250 RVA: 0x00314AB4 File Offset: 0x00312CB4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601491B RID: 84251 RVA: 0x00314ACF File Offset: 0x00312CCF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Comments>(deep);
		}

		// Token: 0x04008ED8 RID: 36568
		private const string tagName = "Comments";

		// Token: 0x04008ED9 RID: 36569
		private const byte tagNsId = 9;

		// Token: 0x04008EDA RID: 36570
		internal const int ElementTypeIdConst = 10790;
	}
}
