using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002639 RID: 9785
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(GraphicFrameLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NonVisualGraphicFrameDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005AA7 RID: 23207
		// (get) Token: 0x0601289C RID: 75932 RVA: 0x002FC6DE File Offset: 0x002FA8DE
		public override string LocalName
		{
			get
			{
				return "cNvGraphicFramePr";
			}
		}

		// Token: 0x17005AA8 RID: 23208
		// (get) Token: 0x0601289D RID: 75933 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005AA9 RID: 23209
		// (get) Token: 0x0601289E RID: 75934 RVA: 0x002FC6E5 File Offset: 0x002FA8E5
		internal override int ElementTypeId
		{
			get
			{
				return 10604;
			}
		}

		// Token: 0x0601289F RID: 75935 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060128A0 RID: 75936 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGraphicFrameDrawingProperties()
		{
		}

		// Token: 0x060128A1 RID: 75937 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGraphicFrameDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060128A2 RID: 75938 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGraphicFrameDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060128A3 RID: 75939 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGraphicFrameDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060128A4 RID: 75940 RVA: 0x002EF8FC File Offset: 0x002EDAFC
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

		// Token: 0x17005AAA RID: 23210
		// (get) Token: 0x060128A5 RID: 75941 RVA: 0x002FC6EC File Offset: 0x002FA8EC
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGraphicFrameDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17005AAB RID: 23211
		// (get) Token: 0x060128A6 RID: 75942 RVA: 0x002FC6F3 File Offset: 0x002FA8F3
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGraphicFrameDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005AAC RID: 23212
		// (get) Token: 0x060128A7 RID: 75943 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005AAD RID: 23213
		// (get) Token: 0x060128A8 RID: 75944 RVA: 0x002EF93D File Offset: 0x002EDB3D
		// (set) Token: 0x060128A9 RID: 75945 RVA: 0x002EF946 File Offset: 0x002EDB46
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

		// Token: 0x17005AAE RID: 23214
		// (get) Token: 0x060128AA RID: 75946 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x060128AB RID: 75947 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x060128AC RID: 75948 RVA: 0x002FC6FA File Offset: 0x002FA8FA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGraphicFrameDrawingProperties>(deep);
		}

		// Token: 0x04008091 RID: 32913
		private const string tagName = "cNvGraphicFramePr";

		// Token: 0x04008092 RID: 32914
		private const byte tagNsId = 12;

		// Token: 0x04008093 RID: 32915
		internal const int ElementTypeIdConst = 10604;

		// Token: 0x04008094 RID: 32916
		private static readonly string[] eleTagNames = new string[] { "graphicFrameLocks", "extLst" };

		// Token: 0x04008095 RID: 32917
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
