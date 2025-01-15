using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023C0 RID: 9152
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extension))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17004CCA RID: 19658
		// (get) Token: 0x060109AC RID: 68012 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17004CCB RID: 19659
		// (get) Token: 0x060109AD RID: 68013 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CCC RID: 19660
		// (get) Token: 0x060109AE RID: 68014 RVA: 0x002E53F1 File Offset: 0x002E35F1
		internal override int ElementTypeId
		{
			get
			{
				return 12806;
			}
		}

		// Token: 0x060109AF RID: 68015 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060109B0 RID: 68016 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtensionList()
		{
		}

		// Token: 0x060109B1 RID: 68017 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060109B2 RID: 68018 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060109B3 RID: 68019 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060109B4 RID: 68020 RVA: 0x002E3E40 File Offset: 0x002E2040
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x060109B5 RID: 68021 RVA: 0x002E53F8 File Offset: 0x002E35F8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtensionList>(deep);
		}

		// Token: 0x04007577 RID: 30071
		private const string tagName = "extLst";

		// Token: 0x04007578 RID: 30072
		private const byte tagNsId = 49;

		// Token: 0x04007579 RID: 30073
		internal const int ElementTypeIdConst = 12806;
	}
}
