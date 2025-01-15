using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028AD RID: 10413
	[ChildElementInfo(typeof(GraphicFrameLocks))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualGraphicFrameDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170068B4 RID: 26804
		// (get) Token: 0x06014869 RID: 84073 RVA: 0x002FC6DE File Offset: 0x002FA8DE
		public override string LocalName
		{
			get
			{
				return "cNvGraphicFramePr";
			}
		}

		// Token: 0x170068B5 RID: 26805
		// (get) Token: 0x0601486A RID: 84074 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x170068B6 RID: 26806
		// (get) Token: 0x0601486B RID: 84075 RVA: 0x003145FA File Offset: 0x003127FA
		internal override int ElementTypeId
		{
			get
			{
				return 10710;
			}
		}

		// Token: 0x0601486C RID: 84076 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601486D RID: 84077 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualGraphicFrameDrawingProperties()
		{
		}

		// Token: 0x0601486E RID: 84078 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualGraphicFrameDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601486F RID: 84079 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualGraphicFrameDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014870 RID: 84080 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualGraphicFrameDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014871 RID: 84081 RVA: 0x002EF8FC File Offset: 0x002EDAFC
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

		// Token: 0x170068B7 RID: 26807
		// (get) Token: 0x06014872 RID: 84082 RVA: 0x00314601 File Offset: 0x00312801
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualGraphicFrameDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170068B8 RID: 26808
		// (get) Token: 0x06014873 RID: 84083 RVA: 0x00314608 File Offset: 0x00312808
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualGraphicFrameDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170068B9 RID: 26809
		// (get) Token: 0x06014874 RID: 84084 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170068BA RID: 26810
		// (get) Token: 0x06014875 RID: 84085 RVA: 0x002EF93D File Offset: 0x002EDB3D
		// (set) Token: 0x06014876 RID: 84086 RVA: 0x002EF946 File Offset: 0x002EDB46
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

		// Token: 0x170068BB RID: 26811
		// (get) Token: 0x06014877 RID: 84087 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x06014878 RID: 84088 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x06014879 RID: 84089 RVA: 0x0031460F File Offset: 0x0031280F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualGraphicFrameDrawingProperties>(deep);
		}

		// Token: 0x04008E7B RID: 36475
		private const string tagName = "cNvGraphicFramePr";

		// Token: 0x04008E7C RID: 36476
		private const byte tagNsId = 16;

		// Token: 0x04008E7D RID: 36477
		internal const int ElementTypeIdConst = 10710;

		// Token: 0x04008E7E RID: 36478
		private static readonly string[] eleTagNames = new string[] { "graphicFrameLocks", "extLst" };

		// Token: 0x04008E7F RID: 36479
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
