using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200283F RID: 10303
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualPicturePropertiesExtension))]
	internal class NonVisualPicturePropertiesExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700667A RID: 26234
		// (get) Token: 0x0601439B RID: 82843 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700667B RID: 26235
		// (get) Token: 0x0601439C RID: 82844 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700667C RID: 26236
		// (get) Token: 0x0601439D RID: 82845 RVA: 0x00310A6E File Offset: 0x0030EC6E
		internal override int ElementTypeId
		{
			get
			{
				return 10339;
			}
		}

		// Token: 0x0601439E RID: 82846 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601439F RID: 82847 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualPicturePropertiesExtensionList()
		{
		}

		// Token: 0x060143A0 RID: 82848 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualPicturePropertiesExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060143A1 RID: 82849 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualPicturePropertiesExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060143A2 RID: 82850 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualPicturePropertiesExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060143A3 RID: 82851 RVA: 0x00310A75 File Offset: 0x0030EC75
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new NonVisualPicturePropertiesExtension();
			}
			return null;
		}

		// Token: 0x060143A4 RID: 82852 RVA: 0x00310A90 File Offset: 0x0030EC90
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualPicturePropertiesExtensionList>(deep);
		}

		// Token: 0x04008993 RID: 35219
		private const string tagName = "extLst";

		// Token: 0x04008994 RID: 35220
		private const byte tagNsId = 10;

		// Token: 0x04008995 RID: 35221
		internal const int ElementTypeIdConst = 10339;
	}
}
