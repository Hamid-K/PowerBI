using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A9A RID: 10906
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ApplicationNonVisualDrawingPropertiesExtension))]
	internal class ApplicationNonVisualDrawingPropertiesExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700741A RID: 29722
		// (get) Token: 0x06016266 RID: 90726 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700741B RID: 29723
		// (get) Token: 0x06016267 RID: 90727 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700741C RID: 29724
		// (get) Token: 0x06016268 RID: 90728 RVA: 0x00327020 File Offset: 0x00325220
		internal override int ElementTypeId
		{
			get
			{
				return 12321;
			}
		}

		// Token: 0x06016269 RID: 90729 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601626A RID: 90730 RVA: 0x00293ECF File Offset: 0x002920CF
		public ApplicationNonVisualDrawingPropertiesExtensionList()
		{
		}

		// Token: 0x0601626B RID: 90731 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ApplicationNonVisualDrawingPropertiesExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601626C RID: 90732 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ApplicationNonVisualDrawingPropertiesExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601626D RID: 90733 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ApplicationNonVisualDrawingPropertiesExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601626E RID: 90734 RVA: 0x00327027 File Offset: 0x00325227
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ext" == name)
			{
				return new ApplicationNonVisualDrawingPropertiesExtension();
			}
			return null;
		}

		// Token: 0x0601626F RID: 90735 RVA: 0x00327042 File Offset: 0x00325242
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ApplicationNonVisualDrawingPropertiesExtensionList>(deep);
		}

		// Token: 0x04009671 RID: 38513
		private const string tagName = "extLst";

		// Token: 0x04009672 RID: 38514
		private const byte tagNsId = 24;

		// Token: 0x04009673 RID: 38515
		internal const int ElementTypeIdConst = 12321;
	}
}
