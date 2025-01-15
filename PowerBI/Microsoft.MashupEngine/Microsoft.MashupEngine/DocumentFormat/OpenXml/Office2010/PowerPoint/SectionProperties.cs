using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023AD RID: 9133
	[ChildElementInfo(typeof(SectionOld), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SectionProperties : OpenXmlCompositeElement
	{
		// Token: 0x17004C49 RID: 19529
		// (get) Token: 0x06010892 RID: 67730 RVA: 0x002E47FF File Offset: 0x002E29FF
		public override string LocalName
		{
			get
			{
				return "sectionPr";
			}
		}

		// Token: 0x17004C4A RID: 19530
		// (get) Token: 0x06010893 RID: 67731 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C4B RID: 19531
		// (get) Token: 0x06010894 RID: 67732 RVA: 0x002E4806 File Offset: 0x002E2A06
		internal override int ElementTypeId
		{
			get
			{
				return 12788;
			}
		}

		// Token: 0x06010895 RID: 67733 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010896 RID: 67734 RVA: 0x00293ECF File Offset: 0x002920CF
		public SectionProperties()
		{
		}

		// Token: 0x06010897 RID: 67735 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SectionProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010898 RID: 67736 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SectionProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010899 RID: 67737 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SectionProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601089A RID: 67738 RVA: 0x002E480D File Offset: 0x002E2A0D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "section" == name)
			{
				return new SectionOld();
			}
			return null;
		}

		// Token: 0x0601089B RID: 67739 RVA: 0x002E4828 File Offset: 0x002E2A28
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SectionProperties>(deep);
		}

		// Token: 0x04007521 RID: 29985
		private const string tagName = "sectionPr";

		// Token: 0x04007522 RID: 29986
		private const byte tagNsId = 49;

		// Token: 0x04007523 RID: 29987
		internal const int ElementTypeIdConst = 12788;
	}
}
