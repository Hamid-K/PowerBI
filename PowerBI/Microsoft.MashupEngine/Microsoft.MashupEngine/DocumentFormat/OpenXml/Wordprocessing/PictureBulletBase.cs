using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Vml;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F95 RID: 12181
	[ChildElementInfo(typeof(Group))]
	[ChildElementInfo(typeof(Rectangle))]
	[ChildElementInfo(typeof(PolyLine))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Line))]
	[ChildElementInfo(typeof(Oval))]
	[ChildElementInfo(typeof(ImageFile))]
	[ChildElementInfo(typeof(RoundRectangle))]
	[ChildElementInfo(typeof(Shape))]
	[ChildElementInfo(typeof(Shapetype))]
	internal class PictureBulletBase : OpenXmlCompositeElement
	{
		// Token: 0x170091EC RID: 37356
		// (get) Token: 0x0601A417 RID: 107543 RVA: 0x00347EE8 File Offset: 0x003460E8
		public override string LocalName
		{
			get
			{
				return "pict";
			}
		}

		// Token: 0x170091ED RID: 37357
		// (get) Token: 0x0601A418 RID: 107544 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091EE RID: 37358
		// (get) Token: 0x0601A419 RID: 107545 RVA: 0x0035F978 File Offset: 0x0035DB78
		internal override int ElementTypeId
		{
			get
			{
				return 11862;
			}
		}

		// Token: 0x0601A41A RID: 107546 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A41B RID: 107547 RVA: 0x00293ECF File Offset: 0x002920CF
		public PictureBulletBase()
		{
		}

		// Token: 0x0601A41C RID: 107548 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PictureBulletBase(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A41D RID: 107549 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PictureBulletBase(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A41E RID: 107550 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PictureBulletBase(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A41F RID: 107551 RVA: 0x0035F980 File Offset: 0x0035DB80
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
			return null;
		}

		// Token: 0x0601A420 RID: 107552 RVA: 0x0035FA66 File Offset: 0x0035DC66
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PictureBulletBase>(deep);
		}

		// Token: 0x0400AC74 RID: 44148
		private const string tagName = "pict";

		// Token: 0x0400AC75 RID: 44149
		private const byte tagNsId = 23;

		// Token: 0x0400AC76 RID: 44150
		internal const int ElementTypeIdConst = 11862;
	}
}
