using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Slicer
{
	// Token: 0x0200237C RID: 9084
	[ChildElementInfo(typeof(Extension))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class OfficeArtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17004B33 RID: 19251
		// (get) Token: 0x06010627 RID: 67111 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17004B34 RID: 19252
		// (get) Token: 0x06010628 RID: 67112 RVA: 0x002E2DA8 File Offset: 0x002E0FA8
		internal override byte NamespaceId
		{
			get
			{
				return 62;
			}
		}

		// Token: 0x17004B35 RID: 19253
		// (get) Token: 0x06010629 RID: 67113 RVA: 0x002E2E5E File Offset: 0x002E105E
		internal override int ElementTypeId
		{
			get
			{
				return 13142;
			}
		}

		// Token: 0x0601062A RID: 67114 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601062B RID: 67115 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeArtExtensionList()
		{
		}

		// Token: 0x0601062C RID: 67116 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeArtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601062D RID: 67117 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeArtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601062E RID: 67118 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeArtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601062F RID: 67119 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06010630 RID: 67120 RVA: 0x002E2E65 File Offset: 0x002E1065
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeArtExtensionList>(deep);
		}

		// Token: 0x04007461 RID: 29793
		private const string tagName = "extLst";

		// Token: 0x04007462 RID: 29794
		private const byte tagNsId = 62;

		// Token: 0x04007463 RID: 29795
		internal const int ElementTypeIdConst = 13142;
	}
}
