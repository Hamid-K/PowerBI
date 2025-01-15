using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingCanvas
{
	// Token: 0x020024E6 RID: 9446
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extension))]
	internal class OfficeArtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17005326 RID: 21286
		// (get) Token: 0x06011809 RID: 71689 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17005327 RID: 21287
		// (get) Token: 0x0601180A RID: 71690 RVA: 0x002EEF8A File Offset: 0x002ED18A
		internal override byte NamespaceId
		{
			get
			{
				return 59;
			}
		}

		// Token: 0x17005328 RID: 21288
		// (get) Token: 0x0601180B RID: 71691 RVA: 0x002EF2B4 File Offset: 0x002ED4B4
		internal override int ElementTypeId
		{
			get
			{
				return 13121;
			}
		}

		// Token: 0x0601180C RID: 71692 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601180D RID: 71693 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeArtExtensionList()
		{
		}

		// Token: 0x0601180E RID: 71694 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeArtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601180F RID: 71695 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeArtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011810 RID: 71696 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeArtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011811 RID: 71697 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06011812 RID: 71698 RVA: 0x002EF2BB File Offset: 0x002ED4BB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeArtExtensionList>(deep);
		}

		// Token: 0x04007AFF RID: 31487
		private const string tagName = "extLst";

		// Token: 0x04007B00 RID: 31488
		private const byte tagNsId = 59;

		// Token: 0x04007B01 RID: 31489
		internal const int ElementTypeIdConst = 13121;
	}
}
