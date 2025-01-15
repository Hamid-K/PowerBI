using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Pictures
{
	// Token: 0x0200237A RID: 9082
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extension))]
	internal class OfficeArtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17004B26 RID: 19238
		// (get) Token: 0x06010608 RID: 67080 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17004B27 RID: 19239
		// (get) Token: 0x06010609 RID: 67081 RVA: 0x002E2CB4 File Offset: 0x002E0EB4
		internal override byte NamespaceId
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17004B28 RID: 19240
		// (get) Token: 0x0601060A RID: 67082 RVA: 0x002E2D98 File Offset: 0x002E0F98
		internal override int ElementTypeId
		{
			get
			{
				return 12821;
			}
		}

		// Token: 0x0601060B RID: 67083 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601060C RID: 67084 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeArtExtensionList()
		{
		}

		// Token: 0x0601060D RID: 67085 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeArtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601060E RID: 67086 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeArtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601060F RID: 67087 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeArtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010610 RID: 67088 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06010611 RID: 67089 RVA: 0x002E2D9F File Offset: 0x002E0F9F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeArtExtensionList>(deep);
		}

		// Token: 0x04007457 RID: 29783
		private const string tagName = "extLst";

		// Token: 0x04007458 RID: 29784
		private const byte tagNsId = 50;

		// Token: 0x04007459 RID: 29785
		internal const int ElementTypeIdConst = 12821;
	}
}
