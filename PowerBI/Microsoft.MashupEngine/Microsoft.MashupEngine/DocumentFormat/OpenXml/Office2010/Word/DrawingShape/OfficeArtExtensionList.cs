using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingShape
{
	// Token: 0x020024FA RID: 9466
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extension))]
	internal class OfficeArtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170053A4 RID: 21412
		// (get) Token: 0x0601192A RID: 71978 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170053A5 RID: 21413
		// (get) Token: 0x0601192B RID: 71979 RVA: 0x002EFE53 File Offset: 0x002EE053
		internal override byte NamespaceId
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170053A6 RID: 21414
		// (get) Token: 0x0601192C RID: 71980 RVA: 0x002F0038 File Offset: 0x002EE238
		internal override int ElementTypeId
		{
			get
			{
				return 13132;
			}
		}

		// Token: 0x0601192D RID: 71981 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601192E RID: 71982 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeArtExtensionList()
		{
		}

		// Token: 0x0601192F RID: 71983 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeArtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011930 RID: 71984 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeArtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011931 RID: 71985 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeArtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011932 RID: 71986 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06011933 RID: 71987 RVA: 0x002F003F File Offset: 0x002EE23F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeArtExtensionList>(deep);
		}

		// Token: 0x04007B60 RID: 31584
		private const string tagName = "extLst";

		// Token: 0x04007B61 RID: 31585
		private const byte tagNsId = 61;

		// Token: 0x04007B62 RID: 31586
		internal const int ElementTypeIdConst = 13132;
	}
}
