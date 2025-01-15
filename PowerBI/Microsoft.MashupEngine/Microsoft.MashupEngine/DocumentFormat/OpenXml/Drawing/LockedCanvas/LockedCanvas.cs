using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Drawing.LockedCanvas
{
	// Token: 0x020026CB RID: 9931
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualGroupShapeProperties))]
	[ChildElementInfo(typeof(VisualGroupShapeProperties))]
	[ChildElementInfo(typeof(TextShape))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(ConnectionShape))]
	[ChildElementInfo(typeof(Picture))]
	[ChildElementInfo(typeof(GvmlContentPart), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GraphicFrame))]
	[ChildElementInfo(typeof(GroupShape))]
	[ChildElementInfo(typeof(GvmlGroupShapeExtensionList))]
	internal class LockedCanvas : OpenXmlCompositeElement
	{
		// Token: 0x17005D5E RID: 23902
		// (get) Token: 0x06012EDD RID: 77533 RVA: 0x0030105B File Offset: 0x002FF25B
		public override string LocalName
		{
			get
			{
				return "lockedCanvas";
			}
		}

		// Token: 0x17005D5F RID: 23903
		// (get) Token: 0x06012EDE RID: 77534 RVA: 0x001AA8D9 File Offset: 0x001A8AD9
		internal override byte NamespaceId
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17005D60 RID: 23904
		// (get) Token: 0x06012EDF RID: 77535 RVA: 0x00301062 File Offset: 0x002FF262
		internal override int ElementTypeId
		{
			get
			{
				return 10693;
			}
		}

		// Token: 0x06012EE0 RID: 77536 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012EE1 RID: 77537 RVA: 0x00293ECF File Offset: 0x002920CF
		public LockedCanvas()
		{
		}

		// Token: 0x06012EE2 RID: 77538 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LockedCanvas(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012EE3 RID: 77539 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LockedCanvas(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012EE4 RID: 77540 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LockedCanvas(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012EE5 RID: 77541 RVA: 0x0030106C File Offset: 0x002FF26C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "nvGrpSpPr" == name)
			{
				return new NonVisualGroupShapeProperties();
			}
			if (10 == namespaceId && "grpSpPr" == name)
			{
				return new VisualGroupShapeProperties();
			}
			if (10 == namespaceId && "txSp" == name)
			{
				return new TextShape();
			}
			if (10 == namespaceId && "sp" == name)
			{
				return new Shape();
			}
			if (10 == namespaceId && "cxnSp" == name)
			{
				return new ConnectionShape();
			}
			if (10 == namespaceId && "pic" == name)
			{
				return new Picture();
			}
			if (48 == namespaceId && "contentPart" == name)
			{
				return new GvmlContentPart();
			}
			if (10 == namespaceId && "graphicFrame" == name)
			{
				return new GraphicFrame();
			}
			if (10 == namespaceId && "grpSp" == name)
			{
				return new GroupShape();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new GvmlGroupShapeExtensionList();
			}
			return null;
		}

		// Token: 0x17005D61 RID: 23905
		// (get) Token: 0x06012EE6 RID: 77542 RVA: 0x0030116A File Offset: 0x002FF36A
		internal override string[] ElementTagNames
		{
			get
			{
				return LockedCanvas.eleTagNames;
			}
		}

		// Token: 0x17005D62 RID: 23906
		// (get) Token: 0x06012EE7 RID: 77543 RVA: 0x00301171 File Offset: 0x002FF371
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LockedCanvas.eleNamespaceIds;
			}
		}

		// Token: 0x17005D63 RID: 23907
		// (get) Token: 0x06012EE8 RID: 77544 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005D64 RID: 23908
		// (get) Token: 0x06012EE9 RID: 77545 RVA: 0x00301178 File Offset: 0x002FF378
		// (set) Token: 0x06012EEA RID: 77546 RVA: 0x00301181 File Offset: 0x002FF381
		public NonVisualGroupShapeProperties NonVisualGroupShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualGroupShapeProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualGroupShapeProperties>(0, value);
			}
		}

		// Token: 0x17005D65 RID: 23909
		// (get) Token: 0x06012EEB RID: 77547 RVA: 0x0030118B File Offset: 0x002FF38B
		// (set) Token: 0x06012EEC RID: 77548 RVA: 0x00301194 File Offset: 0x002FF394
		public VisualGroupShapeProperties VisualGroupShapeProperties
		{
			get
			{
				return base.GetElement<VisualGroupShapeProperties>(1);
			}
			set
			{
				base.SetElement<VisualGroupShapeProperties>(1, value);
			}
		}

		// Token: 0x06012EED RID: 77549 RVA: 0x0030119E File Offset: 0x002FF39E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LockedCanvas>(deep);
		}

		// Token: 0x040083B9 RID: 33721
		private const string tagName = "lockedCanvas";

		// Token: 0x040083BA RID: 33722
		private const byte tagNsId = 15;

		// Token: 0x040083BB RID: 33723
		internal const int ElementTypeIdConst = 10693;

		// Token: 0x040083BC RID: 33724
		private static readonly string[] eleTagNames = new string[] { "nvGrpSpPr", "grpSpPr", "txSp", "sp", "cxnSp", "pic", "contentPart", "graphicFrame", "grpSp", "extLst" };

		// Token: 0x040083BD RID: 33725
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10, 48, 10, 10, 10 };
	}
}
