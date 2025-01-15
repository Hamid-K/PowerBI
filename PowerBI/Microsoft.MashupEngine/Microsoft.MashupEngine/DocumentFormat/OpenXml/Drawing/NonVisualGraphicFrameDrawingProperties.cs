using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027AE RID: 10158
	[ChildElementInfo(typeof(GraphicFrameLocks))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NonVisualGraphicFrameDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170062F2 RID: 25330
		// (get) Token: 0x06013B44 RID: 80708 RVA: 0x002FC6DE File Offset: 0x002FA8DE
		public override string LocalName
		{
			get
			{
				return "cNvGraphicFramePr";
			}
		}

		// Token: 0x170062F3 RID: 25331
		// (get) Token: 0x06013B45 RID: 80709 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062F4 RID: 25332
		// (get) Token: 0x06013B46 RID: 80710 RVA: 0x0030AEAD File Offset: 0x003090AD
		internal override int ElementTypeId
		{
			get
			{
				return 10191;
			}
		}

		// Token: 0x06013B47 RID: 80711 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013B48 RID: 80712 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGraphicFrameDrawingProperties()
		{
		}

		// Token: 0x06013B49 RID: 80713 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGraphicFrameDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B4A RID: 80714 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGraphicFrameDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013B4B RID: 80715 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGraphicFrameDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013B4C RID: 80716 RVA: 0x002EF8FC File Offset: 0x002EDAFC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "graphicFrameLocks" == name)
			{
				return new GraphicFrameLocks();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170062F5 RID: 25333
		// (get) Token: 0x06013B4D RID: 80717 RVA: 0x0030AEB4 File Offset: 0x003090B4
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGraphicFrameDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170062F6 RID: 25334
		// (get) Token: 0x06013B4E RID: 80718 RVA: 0x0030AEBB File Offset: 0x003090BB
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGraphicFrameDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170062F7 RID: 25335
		// (get) Token: 0x06013B4F RID: 80719 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170062F8 RID: 25336
		// (get) Token: 0x06013B50 RID: 80720 RVA: 0x002EF93D File Offset: 0x002EDB3D
		// (set) Token: 0x06013B51 RID: 80721 RVA: 0x002EF946 File Offset: 0x002EDB46
		public GraphicFrameLocks GraphicFrameLocks
		{
			get
			{
				return base.GetElement<GraphicFrameLocks>(0);
			}
			set
			{
				base.SetElement<GraphicFrameLocks>(0, value);
			}
		}

		// Token: 0x170062F9 RID: 25337
		// (get) Token: 0x06013B52 RID: 80722 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x06013B53 RID: 80723 RVA: 0x002DEB73 File Offset: 0x002DCD73
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06013B54 RID: 80724 RVA: 0x0030AEC2 File Offset: 0x003090C2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGraphicFrameDrawingProperties>(deep);
		}

		// Token: 0x04008760 RID: 34656
		private const string tagName = "cNvGraphicFramePr";

		// Token: 0x04008761 RID: 34657
		private const byte tagNsId = 10;

		// Token: 0x04008762 RID: 34658
		internal const int ElementTypeIdConst = 10191;

		// Token: 0x04008763 RID: 34659
		private static readonly string[] eleTagNames = new string[] { "graphicFrameLocks", "extLst" };

		// Token: 0x04008764 RID: 34660
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
