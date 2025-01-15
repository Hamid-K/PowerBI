using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024AD RID: 9389
	[ChildElementInfo(typeof(StyleSet), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class StylisticSets : OpenXmlCompositeElement
	{
		// Token: 0x17005245 RID: 21061
		// (get) Token: 0x0601160E RID: 71182 RVA: 0x002EDEA0 File Offset: 0x002EC0A0
		public override string LocalName
		{
			get
			{
				return "stylisticSets";
			}
		}

		// Token: 0x17005246 RID: 21062
		// (get) Token: 0x0601160F RID: 71183 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005247 RID: 21063
		// (get) Token: 0x06011610 RID: 71184 RVA: 0x002EDEA7 File Offset: 0x002EC0A7
		internal override int ElementTypeId
		{
			get
			{
				return 12863;
			}
		}

		// Token: 0x06011611 RID: 71185 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011612 RID: 71186 RVA: 0x00293ECF File Offset: 0x002920CF
		public StylisticSets()
		{
		}

		// Token: 0x06011613 RID: 71187 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StylisticSets(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011614 RID: 71188 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StylisticSets(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011615 RID: 71189 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StylisticSets(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011616 RID: 71190 RVA: 0x002EDEAE File Offset: 0x002EC0AE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "styleSet" == name)
			{
				return new StyleSet();
			}
			return null;
		}

		// Token: 0x06011617 RID: 71191 RVA: 0x002EDEC9 File Offset: 0x002EC0C9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StylisticSets>(deep);
		}

		// Token: 0x04007989 RID: 31113
		private const string tagName = "stylisticSets";

		// Token: 0x0400798A RID: 31114
		private const byte tagNsId = 52;

		// Token: 0x0400798B RID: 31115
		internal const int ElementTypeIdConst = 12863;
	}
}
