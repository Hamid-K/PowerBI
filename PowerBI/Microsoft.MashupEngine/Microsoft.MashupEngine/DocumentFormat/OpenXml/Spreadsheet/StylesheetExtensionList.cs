using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C7B RID: 11387
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StylesheetExtension))]
	internal class StylesheetExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700830F RID: 33551
		// (get) Token: 0x060183B5 RID: 99253 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17008310 RID: 33552
		// (get) Token: 0x060183B6 RID: 99254 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008311 RID: 33553
		// (get) Token: 0x060183B7 RID: 99255 RVA: 0x0033F8A9 File Offset: 0x0033DAA9
		internal override int ElementTypeId
		{
			get
			{
				return 11367;
			}
		}

		// Token: 0x060183B8 RID: 99256 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060183B9 RID: 99257 RVA: 0x00293ECF File Offset: 0x002920CF
		public StylesheetExtensionList()
		{
		}

		// Token: 0x060183BA RID: 99258 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StylesheetExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060183BB RID: 99259 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StylesheetExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060183BC RID: 99260 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StylesheetExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060183BD RID: 99261 RVA: 0x0033F8B0 File Offset: 0x0033DAB0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new StylesheetExtension();
			}
			return null;
		}

		// Token: 0x060183BE RID: 99262 RVA: 0x0033F8CB File Offset: 0x0033DACB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StylesheetExtensionList>(deep);
		}

		// Token: 0x04009F73 RID: 40819
		private const string tagName = "extLst";

		// Token: 0x04009F74 RID: 40820
		private const byte tagNsId = 22;

		// Token: 0x04009F75 RID: 40821
		internal const int ElementTypeIdConst = 11367;
	}
}
