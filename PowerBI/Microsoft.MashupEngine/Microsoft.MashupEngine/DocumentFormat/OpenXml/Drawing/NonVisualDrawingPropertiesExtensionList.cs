using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002840 RID: 10304
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualDrawingPropertiesExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700667D RID: 26237
		// (get) Token: 0x060143A5 RID: 82853 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700667E RID: 26238
		// (get) Token: 0x060143A6 RID: 82854 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700667F RID: 26239
		// (get) Token: 0x060143A7 RID: 82855 RVA: 0x00310A99 File Offset: 0x0030EC99
		internal override int ElementTypeId
		{
			get
			{
				return 10341;
			}
		}

		// Token: 0x060143A8 RID: 82856 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060143A9 RID: 82857 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingPropertiesExtensionList()
		{
		}

		// Token: 0x060143AA RID: 82858 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingPropertiesExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060143AB RID: 82859 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingPropertiesExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060143AC RID: 82860 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingPropertiesExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060143AD RID: 82861 RVA: 0x00310AA0 File Offset: 0x0030ECA0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new NonVisualDrawingPropertiesExtension();
			}
			return null;
		}

		// Token: 0x060143AE RID: 82862 RVA: 0x00310ABB File Offset: 0x0030ECBB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingPropertiesExtensionList>(deep);
		}

		// Token: 0x04008996 RID: 35222
		private const string tagName = "extLst";

		// Token: 0x04008997 RID: 35223
		private const byte tagNsId = 10;

		// Token: 0x04008998 RID: 35224
		internal const int ElementTypeIdConst = 10341;
	}
}
