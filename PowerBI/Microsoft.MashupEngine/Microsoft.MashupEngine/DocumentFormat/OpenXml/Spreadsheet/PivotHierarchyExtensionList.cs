using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CA8 RID: 11432
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotHierarchyExtension))]
	internal class PivotHierarchyExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700845B RID: 33883
		// (get) Token: 0x060186C7 RID: 100039 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700845C RID: 33884
		// (get) Token: 0x060186C8 RID: 100040 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700845D RID: 33885
		// (get) Token: 0x060186C9 RID: 100041 RVA: 0x003416C7 File Offset: 0x0033F8C7
		internal override int ElementTypeId
		{
			get
			{
				return 11412;
			}
		}

		// Token: 0x060186CA RID: 100042 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060186CB RID: 100043 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotHierarchyExtensionList()
		{
		}

		// Token: 0x060186CC RID: 100044 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotHierarchyExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186CD RID: 100045 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotHierarchyExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186CE RID: 100046 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotHierarchyExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060186CF RID: 100047 RVA: 0x003416CE File Offset: 0x0033F8CE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new PivotHierarchyExtension();
			}
			return null;
		}

		// Token: 0x060186D0 RID: 100048 RVA: 0x003416E9 File Offset: 0x0033F8E9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotHierarchyExtensionList>(deep);
		}

		// Token: 0x0400A033 RID: 41011
		private const string tagName = "extLst";

		// Token: 0x0400A034 RID: 41012
		private const byte tagNsId = 22;

		// Token: 0x0400A035 RID: 41013
		internal const int ElementTypeIdConst = 11412;
	}
}
