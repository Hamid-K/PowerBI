using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002659 RID: 9817
	[ChildElementInfo(typeof(Extension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17005B69 RID: 23401
		// (get) Token: 0x06012A80 RID: 76416 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17005B6A RID: 23402
		// (get) Token: 0x06012A81 RID: 76417 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B6B RID: 23403
		// (get) Token: 0x06012A82 RID: 76418 RVA: 0x002FDAF0 File Offset: 0x002FBCF0
		internal override int ElementTypeId
		{
			get
			{
				return 10634;
			}
		}

		// Token: 0x06012A83 RID: 76419 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012A84 RID: 76420 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtensionList()
		{
		}

		// Token: 0x06012A85 RID: 76421 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A86 RID: 76422 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A87 RID: 76423 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A88 RID: 76424 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06012A89 RID: 76425 RVA: 0x002FDAF7 File Offset: 0x002FBCF7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtensionList>(deep);
		}

		// Token: 0x04008115 RID: 33045
		private const string tagName = "extLst";

		// Token: 0x04008116 RID: 33046
		private const byte tagNsId = 14;

		// Token: 0x04008117 RID: 33047
		internal const int ElementTypeIdConst = 10634;
	}
}
