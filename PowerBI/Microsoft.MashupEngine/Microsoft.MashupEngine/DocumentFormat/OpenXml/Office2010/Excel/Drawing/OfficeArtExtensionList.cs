using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x02002390 RID: 9104
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extension))]
	internal class OfficeArtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17004BB5 RID: 19381
		// (get) Token: 0x0601075A RID: 67418 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17004BB6 RID: 19382
		// (get) Token: 0x0601075B RID: 67419 RVA: 0x002E35B9 File Offset: 0x002E17B9
		internal override byte NamespaceId
		{
			get
			{
				return 54;
			}
		}

		// Token: 0x17004BB7 RID: 19383
		// (get) Token: 0x0601075C RID: 67420 RVA: 0x002E3C27 File Offset: 0x002E1E27
		internal override int ElementTypeId
		{
			get
			{
				return 13018;
			}
		}

		// Token: 0x0601075D RID: 67421 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601075E RID: 67422 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeArtExtensionList()
		{
		}

		// Token: 0x0601075F RID: 67423 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeArtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010760 RID: 67424 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeArtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010761 RID: 67425 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeArtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010762 RID: 67426 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06010763 RID: 67427 RVA: 0x002E3C2E File Offset: 0x002E1E2E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeArtExtensionList>(deep);
		}

		// Token: 0x040074B2 RID: 29874
		private const string tagName = "extLst";

		// Token: 0x040074B3 RID: 29875
		private const byte tagNsId = 54;

		// Token: 0x040074B4 RID: 29876
		internal const int ElementTypeIdConst = 13018;
	}
}
