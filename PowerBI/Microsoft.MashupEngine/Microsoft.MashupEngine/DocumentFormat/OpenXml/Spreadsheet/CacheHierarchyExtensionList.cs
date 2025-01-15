using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CC1 RID: 11457
	[ChildElementInfo(typeof(CacheHierarchyExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CacheHierarchyExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170084ED RID: 34029
		// (get) Token: 0x06018845 RID: 100421 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170084EE RID: 34030
		// (get) Token: 0x06018846 RID: 100422 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084EF RID: 34031
		// (get) Token: 0x06018847 RID: 100423 RVA: 0x00342153 File Offset: 0x00340353
		internal override int ElementTypeId
		{
			get
			{
				return 11437;
			}
		}

		// Token: 0x06018848 RID: 100424 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018849 RID: 100425 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheHierarchyExtensionList()
		{
		}

		// Token: 0x0601884A RID: 100426 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheHierarchyExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601884B RID: 100427 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheHierarchyExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601884C RID: 100428 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheHierarchyExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601884D RID: 100429 RVA: 0x0034215A File Offset: 0x0034035A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new CacheHierarchyExtension();
			}
			return null;
		}

		// Token: 0x0601884E RID: 100430 RVA: 0x00342175 File Offset: 0x00340375
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheHierarchyExtensionList>(deep);
		}

		// Token: 0x0400A0A6 RID: 41126
		private const string tagName = "extLst";

		// Token: 0x0400A0A7 RID: 41127
		private const byte tagNsId = 22;

		// Token: 0x0400A0A8 RID: 41128
		internal const int ElementTypeIdConst = 11437;
	}
}
