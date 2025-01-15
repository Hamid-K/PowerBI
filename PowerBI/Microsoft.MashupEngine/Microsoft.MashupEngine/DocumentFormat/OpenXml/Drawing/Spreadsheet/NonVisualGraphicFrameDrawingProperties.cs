using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002897 RID: 10391
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GraphicFrameLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NonVisualGraphicFrameDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170067E2 RID: 26594
		// (get) Token: 0x060146B3 RID: 83635 RVA: 0x002FC6DE File Offset: 0x002FA8DE
		public override string LocalName
		{
			get
			{
				return "cNvGraphicFramePr";
			}
		}

		// Token: 0x170067E3 RID: 26595
		// (get) Token: 0x060146B4 RID: 83636 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067E4 RID: 26596
		// (get) Token: 0x060146B5 RID: 83637 RVA: 0x00313013 File Offset: 0x00311213
		internal override int ElementTypeId
		{
			get
			{
				return 10752;
			}
		}

		// Token: 0x060146B6 RID: 83638 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060146B7 RID: 83639 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGraphicFrameDrawingProperties()
		{
		}

		// Token: 0x060146B8 RID: 83640 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGraphicFrameDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060146B9 RID: 83641 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGraphicFrameDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060146BA RID: 83642 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGraphicFrameDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060146BB RID: 83643 RVA: 0x002EF8FC File Offset: 0x002EDAFC
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

		// Token: 0x170067E5 RID: 26597
		// (get) Token: 0x060146BC RID: 83644 RVA: 0x0031301A File Offset: 0x0031121A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGraphicFrameDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170067E6 RID: 26598
		// (get) Token: 0x060146BD RID: 83645 RVA: 0x00313021 File Offset: 0x00311221
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGraphicFrameDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170067E7 RID: 26599
		// (get) Token: 0x060146BE RID: 83646 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170067E8 RID: 26600
		// (get) Token: 0x060146BF RID: 83647 RVA: 0x002EF93D File Offset: 0x002EDB3D
		// (set) Token: 0x060146C0 RID: 83648 RVA: 0x002EF946 File Offset: 0x002EDB46
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

		// Token: 0x170067E9 RID: 26601
		// (get) Token: 0x060146C1 RID: 83649 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x060146C2 RID: 83650 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x060146C3 RID: 83651 RVA: 0x00313028 File Offset: 0x00311228
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGraphicFrameDrawingProperties>(deep);
		}

		// Token: 0x04008E03 RID: 36355
		private const string tagName = "cNvGraphicFramePr";

		// Token: 0x04008E04 RID: 36356
		private const byte tagNsId = 18;

		// Token: 0x04008E05 RID: 36357
		internal const int ElementTypeIdConst = 10752;

		// Token: 0x04008E06 RID: 36358
		private static readonly string[] eleTagNames = new string[] { "graphicFrameLocks", "extLst" };

		// Token: 0x04008E07 RID: 36359
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
