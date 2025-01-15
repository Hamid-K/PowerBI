using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024C2 RID: 9410
	[ChildElementInfo(typeof(Extension))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class OfficeArtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170052BA RID: 21178
		// (get) Token: 0x06011714 RID: 71444 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170052BB RID: 21179
		// (get) Token: 0x06011715 RID: 71445 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052BC RID: 21180
		// (get) Token: 0x06011716 RID: 71446 RVA: 0x002EE817 File Offset: 0x002ECA17
		internal override int ElementTypeId
		{
			get
			{
				return 12882;
			}
		}

		// Token: 0x06011717 RID: 71447 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011718 RID: 71448 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeArtExtensionList()
		{
		}

		// Token: 0x06011719 RID: 71449 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeArtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601171A RID: 71450 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeArtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601171B RID: 71451 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeArtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601171C RID: 71452 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x0601171D RID: 71453 RVA: 0x002EE81E File Offset: 0x002ECA1E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeArtExtensionList>(deep);
		}

		// Token: 0x040079D8 RID: 31192
		private const string tagName = "extLst";

		// Token: 0x040079D9 RID: 31193
		private const byte tagNsId = 52;

		// Token: 0x040079DA RID: 31194
		internal const int ElementTypeIdConst = 12882;
	}
}
