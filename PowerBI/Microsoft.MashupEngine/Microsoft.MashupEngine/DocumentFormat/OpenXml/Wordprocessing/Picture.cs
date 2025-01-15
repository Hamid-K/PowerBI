using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Vml.Office;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E78 RID: 11896
	[ChildElementInfo(typeof(PolyLine))]
	[ChildElementInfo(typeof(Control))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MovieReference))]
	[ChildElementInfo(typeof(OleObject))]
	[ChildElementInfo(typeof(Curve))]
	[ChildElementInfo(typeof(Arc))]
	[ChildElementInfo(typeof(Oval))]
	[ChildElementInfo(typeof(Shapetype))]
	[ChildElementInfo(typeof(ImageFile))]
	[ChildElementInfo(typeof(Line))]
	[ChildElementInfo(typeof(Group))]
	[ChildElementInfo(typeof(Rectangle))]
	[ChildElementInfo(typeof(RoundRectangle))]
	[ChildElementInfo(typeof(Shape))]
	internal class Picture : OpenXmlCompositeElement
	{
		// Token: 0x17008AB2 RID: 35506
		// (get) Token: 0x06019449 RID: 103497 RVA: 0x00347EE8 File Offset: 0x003460E8
		public override string LocalName
		{
			get
			{
				return "pict";
			}
		}

		// Token: 0x17008AB3 RID: 35507
		// (get) Token: 0x0601944A RID: 103498 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008AB4 RID: 35508
		// (get) Token: 0x0601944B RID: 103499 RVA: 0x00347EEF File Offset: 0x003460EF
		internal override int ElementTypeId
		{
			get
			{
				return 11566;
			}
		}

		// Token: 0x0601944C RID: 103500 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008AB5 RID: 35509
		// (get) Token: 0x0601944D RID: 103501 RVA: 0x00347EF6 File Offset: 0x003460F6
		internal override string[] AttributeTagNames
		{
			get
			{
				return Picture.attributeTagNames;
			}
		}

		// Token: 0x17008AB6 RID: 35510
		// (get) Token: 0x0601944E RID: 103502 RVA: 0x00347EFD File Offset: 0x003460FD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Picture.attributeNamespaceIds;
			}
		}

		// Token: 0x17008AB7 RID: 35511
		// (get) Token: 0x0601944F RID: 103503 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x06019450 RID: 103504 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "anchorId")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public HexBinaryValue AnchorId
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019451 RID: 103505 RVA: 0x00293ECF File Offset: 0x002920CF
		public Picture()
		{
		}

		// Token: 0x06019452 RID: 103506 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Picture(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019453 RID: 103507 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Picture(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019454 RID: 103508 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Picture(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019455 RID: 103509 RVA: 0x00347F04 File Offset: 0x00346104
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
			if (23 == namespaceId && "movie" == name)
			{
				return new MovieReference();
			}
			if (23 == namespaceId && "control" == name)
			{
				return new Control();
			}
			return null;
		}

		// Token: 0x06019456 RID: 103510 RVA: 0x00348062 File Offset: 0x00346262
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "anchorId" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019457 RID: 103511 RVA: 0x00348084 File Offset: 0x00346284
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Picture>(deep);
		}

		// Token: 0x0400A804 RID: 43012
		private const string tagName = "pict";

		// Token: 0x0400A805 RID: 43013
		private const byte tagNsId = 23;

		// Token: 0x0400A806 RID: 43014
		internal const int ElementTypeIdConst = 11566;

		// Token: 0x0400A807 RID: 43015
		private static string[] attributeTagNames = new string[] { "anchorId" };

		// Token: 0x0400A808 RID: 43016
		private static byte[] attributeNamespaceIds = new byte[] { 52 };
	}
}
