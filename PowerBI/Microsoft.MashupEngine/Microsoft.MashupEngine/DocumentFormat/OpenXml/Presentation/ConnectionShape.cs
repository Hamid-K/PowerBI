using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AA6 RID: 10918
	[ChildElementInfo(typeof(ShapeProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualConnectionShapeProperties))]
	[ChildElementInfo(typeof(ShapeStyle))]
	[ChildElementInfo(typeof(ExtensionListWithModification))]
	internal class ConnectionShape : OpenXmlCompositeElement
	{
		// Token: 0x17007467 RID: 29799
		// (get) Token: 0x06016320 RID: 90912 RVA: 0x002FB89A File Offset: 0x002F9A9A
		public override string LocalName
		{
			get
			{
				return "cxnSp";
			}
		}

		// Token: 0x17007468 RID: 29800
		// (get) Token: 0x06016321 RID: 90913 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007469 RID: 29801
		// (get) Token: 0x06016322 RID: 90914 RVA: 0x00327900 File Offset: 0x00325B00
		internal override int ElementTypeId
		{
			get
			{
				return 12332;
			}
		}

		// Token: 0x06016323 RID: 90915 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016324 RID: 90916 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConnectionShape()
		{
		}

		// Token: 0x06016325 RID: 90917 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConnectionShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016326 RID: 90918 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConnectionShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016327 RID: 90919 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConnectionShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016328 RID: 90920 RVA: 0x00327908 File Offset: 0x00325B08
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "nvCxnSpPr" == name)
			{
				return new NonVisualConnectionShapeProperties();
			}
			if (24 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (24 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionListWithModification();
			}
			return null;
		}

		// Token: 0x1700746A RID: 29802
		// (get) Token: 0x06016329 RID: 90921 RVA: 0x00327976 File Offset: 0x00325B76
		internal override string[] ElementTagNames
		{
			get
			{
				return ConnectionShape.eleTagNames;
			}
		}

		// Token: 0x1700746B RID: 29803
		// (get) Token: 0x0601632A RID: 90922 RVA: 0x0032797D File Offset: 0x00325B7D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ConnectionShape.eleNamespaceIds;
			}
		}

		// Token: 0x1700746C RID: 29804
		// (get) Token: 0x0601632B RID: 90923 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700746D RID: 29805
		// (get) Token: 0x0601632C RID: 90924 RVA: 0x00327984 File Offset: 0x00325B84
		// (set) Token: 0x0601632D RID: 90925 RVA: 0x0032798D File Offset: 0x00325B8D
		public NonVisualConnectionShapeProperties NonVisualConnectionShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualConnectionShapeProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualConnectionShapeProperties>(0, value);
			}
		}

		// Token: 0x1700746E RID: 29806
		// (get) Token: 0x0601632E RID: 90926 RVA: 0x0032771B File Offset: 0x0032591B
		// (set) Token: 0x0601632F RID: 90927 RVA: 0x00327724 File Offset: 0x00325924
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(1);
			}
			set
			{
				base.SetElement<ShapeProperties>(1, value);
			}
		}

		// Token: 0x1700746F RID: 29807
		// (get) Token: 0x06016330 RID: 90928 RVA: 0x0032772E File Offset: 0x0032592E
		// (set) Token: 0x06016331 RID: 90929 RVA: 0x00327737 File Offset: 0x00325937
		public ShapeStyle ShapeStyle
		{
			get
			{
				return base.GetElement<ShapeStyle>(2);
			}
			set
			{
				base.SetElement<ShapeStyle>(2, value);
			}
		}

		// Token: 0x17007470 RID: 29808
		// (get) Token: 0x06016332 RID: 90930 RVA: 0x0031FA77 File Offset: 0x0031DC77
		// (set) Token: 0x06016333 RID: 90931 RVA: 0x0031FA80 File Offset: 0x0031DC80
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

		// Token: 0x06016334 RID: 90932 RVA: 0x00327997 File Offset: 0x00325B97
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectionShape>(deep);
		}

		// Token: 0x040096A4 RID: 38564
		private const string tagName = "cxnSp";

		// Token: 0x040096A5 RID: 38565
		private const byte tagNsId = 24;

		// Token: 0x040096A6 RID: 38566
		internal const int ElementTypeIdConst = 12332;

		// Token: 0x040096A7 RID: 38567
		private static readonly string[] eleTagNames = new string[] { "nvCxnSpPr", "spPr", "style", "extLst" };

		// Token: 0x040096A8 RID: 38568
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24 };
	}
}
