using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingGroup
{
	// Token: 0x020024F5 RID: 9461
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extension))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class OfficeArtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700537A RID: 21370
		// (get) Token: 0x060118CC RID: 71884 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700537B RID: 21371
		// (get) Token: 0x060118CD RID: 71885 RVA: 0x002EF715 File Offset: 0x002ED915
		internal override byte NamespaceId
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x1700537C RID: 21372
		// (get) Token: 0x060118CE RID: 71886 RVA: 0x002EFA93 File Offset: 0x002EDC93
		internal override int ElementTypeId
		{
			get
			{
				return 13126;
			}
		}

		// Token: 0x060118CF RID: 71887 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060118D0 RID: 71888 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeArtExtensionList()
		{
		}

		// Token: 0x060118D1 RID: 71889 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeArtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060118D2 RID: 71890 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeArtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060118D3 RID: 71891 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeArtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060118D4 RID: 71892 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x060118D5 RID: 71893 RVA: 0x002EFA9A File Offset: 0x002EDC9A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeArtExtensionList>(deep);
		}

		// Token: 0x04007B45 RID: 31557
		private const string tagName = "extLst";

		// Token: 0x04007B46 RID: 31558
		private const byte tagNsId = 60;

		// Token: 0x04007B47 RID: 31559
		internal const int ElementTypeIdConst = 13126;
	}
}
