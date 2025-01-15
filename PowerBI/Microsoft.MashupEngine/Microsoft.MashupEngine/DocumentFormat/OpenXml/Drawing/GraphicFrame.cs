using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200283B RID: 10299
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualGraphicFrameProperties))]
	[ChildElementInfo(typeof(Graphic))]
	[ChildElementInfo(typeof(Transform2D))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class GraphicFrame : OpenXmlCompositeElement
	{
		// Token: 0x17006651 RID: 26193
		// (get) Token: 0x06014340 RID: 82752 RVA: 0x002EFCFA File Offset: 0x002EDEFA
		public override string LocalName
		{
			get
			{
				return "graphicFrame";
			}
		}

		// Token: 0x17006652 RID: 26194
		// (get) Token: 0x06014341 RID: 82753 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006653 RID: 26195
		// (get) Token: 0x06014342 RID: 82754 RVA: 0x003105A4 File Offset: 0x0030E7A4
		internal override int ElementTypeId
		{
			get
			{
				return 10335;
			}
		}

		// Token: 0x06014343 RID: 82755 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014344 RID: 82756 RVA: 0x00293ECF File Offset: 0x002920CF
		public GraphicFrame()
		{
		}

		// Token: 0x06014345 RID: 82757 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GraphicFrame(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014346 RID: 82758 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GraphicFrame(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014347 RID: 82759 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GraphicFrame(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014348 RID: 82760 RVA: 0x003105AC File Offset: 0x0030E7AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "nvGraphicFramePr" == name)
			{
				return new NonVisualGraphicFrameProperties();
			}
			if (10 == namespaceId && "graphic" == name)
			{
				return new Graphic();
			}
			if (10 == namespaceId && "xfrm" == name)
			{
				return new Transform2D();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006654 RID: 26196
		// (get) Token: 0x06014349 RID: 82761 RVA: 0x0031061A File Offset: 0x0030E81A
		internal override string[] ElementTagNames
		{
			get
			{
				return GraphicFrame.eleTagNames;
			}
		}

		// Token: 0x17006655 RID: 26197
		// (get) Token: 0x0601434A RID: 82762 RVA: 0x00310621 File Offset: 0x0030E821
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GraphicFrame.eleNamespaceIds;
			}
		}

		// Token: 0x17006656 RID: 26198
		// (get) Token: 0x0601434B RID: 82763 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006657 RID: 26199
		// (get) Token: 0x0601434C RID: 82764 RVA: 0x00310628 File Offset: 0x0030E828
		// (set) Token: 0x0601434D RID: 82765 RVA: 0x00310631 File Offset: 0x0030E831
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

		// Token: 0x17006658 RID: 26200
		// (get) Token: 0x0601434E RID: 82766 RVA: 0x0031063B File Offset: 0x0030E83B
		// (set) Token: 0x0601434F RID: 82767 RVA: 0x00310644 File Offset: 0x0030E844
		public Graphic Graphic
		{
			get
			{
				return base.GetElement<Graphic>(1);
			}
			set
			{
				base.SetElement<Graphic>(1, value);
			}
		}

		// Token: 0x17006659 RID: 26201
		// (get) Token: 0x06014350 RID: 82768 RVA: 0x0031064E File Offset: 0x0030E84E
		// (set) Token: 0x06014351 RID: 82769 RVA: 0x00310657 File Offset: 0x0030E857
		public Transform2D Transform2D
		{
			get
			{
				return base.GetElement<Transform2D>(2);
			}
			set
			{
				base.SetElement<Transform2D>(2, value);
			}
		}

		// Token: 0x1700665A RID: 26202
		// (get) Token: 0x06014352 RID: 82770 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06014353 RID: 82771 RVA: 0x002E0C32 File Offset: 0x002DEE32
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x06014354 RID: 82772 RVA: 0x00310661 File Offset: 0x0030E861
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GraphicFrame>(deep);
		}

		// Token: 0x0400897F RID: 35199
		private const string tagName = "graphicFrame";

		// Token: 0x04008980 RID: 35200
		private const byte tagNsId = 10;

		// Token: 0x04008981 RID: 35201
		internal const int ElementTypeIdConst = 10335;

		// Token: 0x04008982 RID: 35202
		private static readonly string[] eleTagNames = new string[] { "nvGraphicFramePr", "graphic", "xfrm", "extLst" };

		// Token: 0x04008983 RID: 35203
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
