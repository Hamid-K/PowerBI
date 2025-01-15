using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AA5 RID: 10917
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualGraphicFrameProperties))]
	[ChildElementInfo(typeof(Transform))]
	[ChildElementInfo(typeof(Graphic))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	internal class GraphicFrame : OpenXmlCompositeElement
	{
		// Token: 0x1700745D RID: 29789
		// (get) Token: 0x0601630A RID: 90890 RVA: 0x002EFCFA File Offset: 0x002EDEFA
		public override string LocalName
		{
			get
			{
				return "graphicFrame";
			}
		}

		// Token: 0x1700745E RID: 29790
		// (get) Token: 0x0601630B RID: 90891 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700745F RID: 29791
		// (get) Token: 0x0601630C RID: 90892 RVA: 0x003277FA File Offset: 0x003259FA
		internal override int ElementTypeId
		{
			get
			{
				return 12331;
			}
		}

		// Token: 0x0601630D RID: 90893 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601630E RID: 90894 RVA: 0x00293ECF File Offset: 0x002920CF
		public GraphicFrame()
		{
		}

		// Token: 0x0601630F RID: 90895 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GraphicFrame(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016310 RID: 90896 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GraphicFrame(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016311 RID: 90897 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GraphicFrame(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016312 RID: 90898 RVA: 0x00327804 File Offset: 0x00325A04
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "nvGraphicFramePr" == name)
			{
				return new NonVisualGraphicFrameProperties();
			}
			if (24 == namespaceId && "xfrm" == name)
			{
				return new Transform();
			}
			if (10 == namespaceId && "graphic" == name)
			{
				return new Graphic();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x17007460 RID: 29792
		// (get) Token: 0x06016313 RID: 90899 RVA: 0x00327872 File Offset: 0x00325A72
		internal override string[] ElementTagNames
		{
			get
			{
				return GraphicFrame.eleTagNames;
			}
		}

		// Token: 0x17007461 RID: 29793
		// (get) Token: 0x06016314 RID: 90900 RVA: 0x00327879 File Offset: 0x00325A79
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GraphicFrame.eleNamespaceIds;
			}
		}

		// Token: 0x17007462 RID: 29794
		// (get) Token: 0x06016315 RID: 90901 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007463 RID: 29795
		// (get) Token: 0x06016316 RID: 90902 RVA: 0x00327880 File Offset: 0x00325A80
		// (set) Token: 0x06016317 RID: 90903 RVA: 0x00327889 File Offset: 0x00325A89
		public NonVisualGraphicFrameProperties NonVisualGraphicFrameProperties
		{
			get
			{
				return base.GetElement<NonVisualGraphicFrameProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualGraphicFrameProperties>(0, value);
			}
		}

		// Token: 0x17007464 RID: 29796
		// (get) Token: 0x06016318 RID: 90904 RVA: 0x00327893 File Offset: 0x00325A93
		// (set) Token: 0x06016319 RID: 90905 RVA: 0x0032789C File Offset: 0x00325A9C
		public Transform Transform
		{
			get
			{
				return base.GetElement<Transform>(1);
			}
			set
			{
				base.SetElement<Transform>(1, value);
			}
		}

		// Token: 0x17007465 RID: 29797
		// (get) Token: 0x0601631A RID: 90906 RVA: 0x002FB80A File Offset: 0x002F9A0A
		// (set) Token: 0x0601631B RID: 90907 RVA: 0x002FB813 File Offset: 0x002F9A13
		public Graphic Graphic
		{
			get
			{
				return base.GetElement<Graphic>(2);
			}
			set
			{
				base.SetElement<Graphic>(2, value);
			}
		}

		// Token: 0x17007466 RID: 29798
		// (get) Token: 0x0601631C RID: 90908 RVA: 0x0031FA77 File Offset: 0x0031DC77
		// (set) Token: 0x0601631D RID: 90909 RVA: 0x0031FA80 File Offset: 0x0031DC80
		public ExtensionListWithModification ExtensionListWithModification
		{
			get
			{
				return base.GetElement<ExtensionListWithModification>(3);
			}
			set
			{
				base.SetElement<ExtensionListWithModification>(3, value);
			}
		}

		// Token: 0x0601631E RID: 90910 RVA: 0x003278A6 File Offset: 0x00325AA6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GraphicFrame>(deep);
		}

		// Token: 0x0400969F RID: 38559
		private const string tagName = "graphicFrame";

		// Token: 0x040096A0 RID: 38560
		private const byte tagNsId = 24;

		// Token: 0x040096A1 RID: 38561
		internal const int ElementTypeIdConst = 12331;

		// Token: 0x040096A2 RID: 38562
		private static readonly string[] eleTagNames = new string[] { "nvGraphicFramePr", "xfrm", "graphic", "extLst" };

		// Token: 0x040096A3 RID: 38563
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 10, 24 };
	}
}
