using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Vml.Office;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E77 RID: 11895
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Rectangle))]
	[ChildElementInfo(typeof(Group))]
	[ChildElementInfo(typeof(ImageFile))]
	[ChildElementInfo(typeof(Line))]
	[ChildElementInfo(typeof(Oval))]
	[ChildElementInfo(typeof(PolyLine))]
	[ChildElementInfo(typeof(Shapetype))]
	[ChildElementInfo(typeof(RoundRectangle))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(Curve))]
	[ChildElementInfo(typeof(Control))]
	[ChildElementInfo(typeof(Arc))]
	[ChildElementInfo(typeof(OleObject))]
	internal class EmbeddedObject : OpenXmlCompositeElement
	{
		// Token: 0x17008AAA RID: 35498
		// (get) Token: 0x06019435 RID: 103477 RVA: 0x00347CD1 File Offset: 0x00345ED1
		public override string LocalName
		{
			get
			{
				return "object";
			}
		}

		// Token: 0x17008AAB RID: 35499
		// (get) Token: 0x06019436 RID: 103478 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AAC RID: 35500
		// (get) Token: 0x06019437 RID: 103479 RVA: 0x00347CD8 File Offset: 0x00345ED8
		internal override int ElementTypeId
		{
			get
			{
				return 11565;
			}
		}

		// Token: 0x06019438 RID: 103480 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008AAD RID: 35501
		// (get) Token: 0x06019439 RID: 103481 RVA: 0x00347CDF File Offset: 0x00345EDF
		internal override string[] AttributeTagNames
		{
			get
			{
				return EmbeddedObject.attributeTagNames;
			}
		}

		// Token: 0x17008AAE RID: 35502
		// (get) Token: 0x0601943A RID: 103482 RVA: 0x00347CE6 File Offset: 0x00345EE6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EmbeddedObject.attributeNamespaceIds;
			}
		}

		// Token: 0x17008AAF RID: 35503
		// (get) Token: 0x0601943B RID: 103483 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601943C RID: 103484 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "dxaOrig")]
		public StringValue DxaOriginal
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008AB0 RID: 35504
		// (get) Token: 0x0601943D RID: 103485 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601943E RID: 103486 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "dyaOrig")]
		public StringValue DyaOriginal
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008AB1 RID: 35505
		// (get) Token: 0x0601943F RID: 103487 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x06019440 RID: 103488 RVA: 0x002BD494 File Offset: 0x002BB694
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(52, "anchorId")]
		public HexBinaryValue AnchorId
		{
			get
			{
				return (HexBinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06019441 RID: 103489 RVA: 0x00293ECF File Offset: 0x002920CF
		public EmbeddedObject()
		{
		}

		// Token: 0x06019442 RID: 103490 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EmbeddedObject(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019443 RID: 103491 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EmbeddedObject(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019444 RID: 103492 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EmbeddedObject(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019445 RID: 103493 RVA: 0x00347CF0 File Offset: 0x00345EF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "group" == name)
			{
				return new Group();
			}
			if (26 == namespaceId && "image" == name)
			{
				return new ImageFile();
			}
			if (26 == namespaceId && "line" == name)
			{
				return new Line();
			}
			if (26 == namespaceId && "oval" == name)
			{
				return new Oval();
			}
			if (26 == namespaceId && "polyline" == name)
			{
				return new PolyLine();
			}
			if (26 == namespaceId && "rect" == name)
			{
				return new Rectangle();
			}
			if (26 == namespaceId && "roundrect" == name)
			{
				return new RoundRectangle();
			}
			if (26 == namespaceId && "shape" == name)
			{
				return new Shape();
			}
			if (26 == namespaceId && "shapetype" == name)
			{
				return new Shapetype();
			}
			if (26 == namespaceId && "arc" == name)
			{
				return new Arc();
			}
			if (26 == namespaceId && "curve" == name)
			{
				return new Curve();
			}
			if (27 == namespaceId && "OLEObject" == name)
			{
				return new OleObject();
			}
			if (23 == namespaceId && "control" == name)
			{
				return new Control();
			}
			return null;
		}

		// Token: 0x06019446 RID: 103494 RVA: 0x00347E38 File Offset: 0x00346038
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "dxaOrig" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "dyaOrig" == name)
			{
				return new StringValue();
			}
			if (52 == namespaceId && "anchorId" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019447 RID: 103495 RVA: 0x00347E95 File Offset: 0x00346095
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EmbeddedObject>(deep);
		}

		// Token: 0x0400A7FF RID: 43007
		private const string tagName = "object";

		// Token: 0x0400A800 RID: 43008
		private const byte tagNsId = 23;

		// Token: 0x0400A801 RID: 43009
		internal const int ElementTypeIdConst = 11565;

		// Token: 0x0400A802 RID: 43010
		private static string[] attributeTagNames = new string[] { "dxaOrig", "dyaOrig", "anchorId" };

		// Token: 0x0400A803 RID: 43011
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 52 };
	}
}
