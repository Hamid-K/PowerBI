using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CBD RID: 11453
	[ChildElementInfo(typeof(PivotTableDefinitionExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotTableDefinitionExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170084DB RID: 34011
		// (get) Token: 0x06018811 RID: 100369 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170084DC RID: 34012
		// (get) Token: 0x06018812 RID: 100370 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084DD RID: 34013
		// (get) Token: 0x06018813 RID: 100371 RVA: 0x0034201B File Offset: 0x0034021B
		internal override int ElementTypeId
		{
			get
			{
				return 11433;
			}
		}

		// Token: 0x06018814 RID: 100372 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018815 RID: 100373 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotTableDefinitionExtensionList()
		{
		}

		// Token: 0x06018816 RID: 100374 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotTableDefinitionExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018817 RID: 100375 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotTableDefinitionExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018818 RID: 100376 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotTableDefinitionExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018819 RID: 100377 RVA: 0x00342022 File Offset: 0x00340222
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new PivotTableDefinitionExtension();
			}
			return null;
		}

		// Token: 0x0601881A RID: 100378 RVA: 0x0034203D File Offset: 0x0034023D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotTableDefinitionExtensionList>(deep);
		}

		// Token: 0x0400A096 RID: 41110
		private const string tagName = "extLst";

		// Token: 0x0400A097 RID: 41111
		private const byte tagNsId = 22;

		// Token: 0x0400A098 RID: 41112
		internal const int ElementTypeIdConst = 11433;
	}
}
