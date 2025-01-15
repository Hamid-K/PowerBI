using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x0200232D RID: 9005
	[ChildElementInfo(typeof(Extension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class OfficeArtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170048C6 RID: 18630
		// (get) Token: 0x060100EB RID: 65771 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170048C7 RID: 18631
		// (get) Token: 0x060100EC RID: 65772 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x170048C8 RID: 18632
		// (get) Token: 0x060100ED RID: 65773 RVA: 0x002DF2BE File Offset: 0x002DD4BE
		internal override int ElementTypeId
		{
			get
			{
				return 13028;
			}
		}

		// Token: 0x060100EE RID: 65774 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060100EF RID: 65775 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeArtExtensionList()
		{
		}

		// Token: 0x060100F0 RID: 65776 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeArtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060100F1 RID: 65777 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeArtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060100F2 RID: 65778 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeArtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060100F3 RID: 65779 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x060100F4 RID: 65780 RVA: 0x002DF2E0 File Offset: 0x002DD4E0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeArtExtensionList>(deep);
		}

		// Token: 0x040072E5 RID: 29413
		private const string tagName = "extLst";

		// Token: 0x040072E6 RID: 29414
		private const byte tagNsId = 56;

		// Token: 0x040072E7 RID: 29415
		internal const int ElementTypeIdConst = 13028;
	}
}
