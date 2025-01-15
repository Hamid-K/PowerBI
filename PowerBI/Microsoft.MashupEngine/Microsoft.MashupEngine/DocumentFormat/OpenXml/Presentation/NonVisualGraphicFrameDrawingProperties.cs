using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A69 RID: 10857
	[ChildElementInfo(typeof(GraphicFrameLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualGraphicFrameDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170072AC RID: 29356
		// (get) Token: 0x06015F29 RID: 89897 RVA: 0x002FC6DE File Offset: 0x002FA8DE
		public override string LocalName
		{
			get
			{
				return "cNvGraphicFramePr";
			}
		}

		// Token: 0x170072AD RID: 29357
		// (get) Token: 0x06015F2A RID: 89898 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170072AE RID: 29358
		// (get) Token: 0x06015F2B RID: 89899 RVA: 0x00324D4A File Offset: 0x00322F4A
		internal override int ElementTypeId
		{
			get
			{
				return 12275;
			}
		}

		// Token: 0x06015F2C RID: 89900 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015F2D RID: 89901 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGraphicFrameDrawingProperties()
		{
		}

		// Token: 0x06015F2E RID: 89902 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGraphicFrameDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F2F RID: 89903 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGraphicFrameDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F30 RID: 89904 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGraphicFrameDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015F31 RID: 89905 RVA: 0x002EF8FC File Offset: 0x002EDAFC
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

		// Token: 0x170072AF RID: 29359
		// (get) Token: 0x06015F32 RID: 89906 RVA: 0x00324D51 File Offset: 0x00322F51
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGraphicFrameDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170072B0 RID: 29360
		// (get) Token: 0x06015F33 RID: 89907 RVA: 0x00324D58 File Offset: 0x00322F58
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGraphicFrameDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170072B1 RID: 29361
		// (get) Token: 0x06015F34 RID: 89908 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170072B2 RID: 29362
		// (get) Token: 0x06015F35 RID: 89909 RVA: 0x002EF93D File Offset: 0x002EDB3D
		// (set) Token: 0x06015F36 RID: 89910 RVA: 0x002EF946 File Offset: 0x002EDB46
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

		// Token: 0x170072B3 RID: 29363
		// (get) Token: 0x06015F37 RID: 89911 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x06015F38 RID: 89912 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x06015F39 RID: 89913 RVA: 0x00324D5F File Offset: 0x00322F5F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGraphicFrameDrawingProperties>(deep);
		}

		// Token: 0x04009589 RID: 38281
		private const string tagName = "cNvGraphicFramePr";

		// Token: 0x0400958A RID: 38282
		private const byte tagNsId = 24;

		// Token: 0x0400958B RID: 38283
		internal const int ElementTypeIdConst = 12275;

		// Token: 0x0400958C RID: 38284
		private static readonly string[] eleTagNames = new string[] { "graphicFrameLocks", "extLst" };

		// Token: 0x0400958D RID: 38285
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
