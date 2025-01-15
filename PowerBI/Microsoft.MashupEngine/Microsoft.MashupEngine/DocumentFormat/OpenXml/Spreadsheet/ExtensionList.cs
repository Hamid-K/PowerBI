using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B2D RID: 11053
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extension))]
	internal class ExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17007757 RID: 30551
		// (get) Token: 0x060169DB RID: 92635 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17007758 RID: 30552
		// (get) Token: 0x060169DC RID: 92636 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007759 RID: 30553
		// (get) Token: 0x060169DD RID: 92637 RVA: 0x0032D46B File Offset: 0x0032B66B
		internal override int ElementTypeId
		{
			get
			{
				return 11051;
			}
		}

		// Token: 0x060169DE RID: 92638 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060169DF RID: 92639 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtensionList()
		{
		}

		// Token: 0x060169E0 RID: 92640 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060169E1 RID: 92641 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060169E2 RID: 92642 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060169E3 RID: 92643 RVA: 0x002E79D9 File Offset: 0x002E5BD9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x060169E4 RID: 92644 RVA: 0x0032D472 File Offset: 0x0032B672
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtensionList>(deep);
		}

		// Token: 0x04009940 RID: 39232
		private const string tagName = "extLst";

		// Token: 0x04009941 RID: 39233
		private const byte tagNsId = 22;

		// Token: 0x04009942 RID: 39234
		internal const int ElementTypeIdConst = 11051;
	}
}
