using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x0200233F RID: 9023
	[ChildElementInfo(typeof(Extension))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class OfficeArtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700494A RID: 18762
		// (get) Token: 0x0601021A RID: 66074 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700494B RID: 18763
		// (get) Token: 0x0601021B RID: 66075 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		internal override byte NamespaceId
		{
			get
			{
				return 47;
			}
		}

		// Token: 0x1700494C RID: 18764
		// (get) Token: 0x0601021C RID: 66076 RVA: 0x002E012B File Offset: 0x002DE32B
		internal override int ElementTypeId
		{
			get
			{
				return 12711;
			}
		}

		// Token: 0x0601021D RID: 66077 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601021E RID: 66078 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeArtExtensionList()
		{
		}

		// Token: 0x0601021F RID: 66079 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeArtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010220 RID: 66080 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeArtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010221 RID: 66081 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeArtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010222 RID: 66082 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06010223 RID: 66083 RVA: 0x002E0132 File Offset: 0x002DE332
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeArtExtensionList>(deep);
		}

		// Token: 0x04007334 RID: 29492
		private const string tagName = "extLst";

		// Token: 0x04007335 RID: 29493
		private const byte tagNsId = 47;

		// Token: 0x04007336 RID: 29494
		internal const int ElementTypeIdConst = 12711;
	}
}
