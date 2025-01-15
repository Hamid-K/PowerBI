using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027A3 RID: 10147
	[ChildElementInfo(typeof(Extents))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Offset))]
	internal class Transform2D : OpenXmlCompositeElement
	{
		// Token: 0x17006282 RID: 25218
		// (get) Token: 0x06013A53 RID: 80467 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x17006283 RID: 25219
		// (get) Token: 0x06013A54 RID: 80468 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006284 RID: 25220
		// (get) Token: 0x06013A55 RID: 80469 RVA: 0x0030A3CF File Offset: 0x003085CF
		internal override int ElementTypeId
		{
			get
			{
				return 10180;
			}
		}

		// Token: 0x06013A56 RID: 80470 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006285 RID: 25221
		// (get) Token: 0x06013A57 RID: 80471 RVA: 0x0030A3D6 File Offset: 0x003085D6
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform2D.attributeTagNames;
			}
		}

		// Token: 0x17006286 RID: 25222
		// (get) Token: 0x06013A58 RID: 80472 RVA: 0x0030A3DD File Offset: 0x003085DD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform2D.attributeNamespaceIds;
			}
		}

		// Token: 0x17006287 RID: 25223
		// (get) Token: 0x06013A59 RID: 80473 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013A5A RID: 80474 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rot")]
		public Int32Value Rotation
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006288 RID: 25224
		// (get) Token: 0x06013A5B RID: 80475 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06013A5C RID: 80476 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "flipH")]
		public BooleanValue HorizontalFlip
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006289 RID: 25225
		// (get) Token: 0x06013A5D RID: 80477 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06013A5E RID: 80478 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "flipV")]
		public BooleanValue VerticalFlip
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06013A5F RID: 80479 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform2D()
		{
		}

		// Token: 0x06013A60 RID: 80480 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform2D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013A61 RID: 80481 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform2D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013A62 RID: 80482 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform2D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013A63 RID: 80483 RVA: 0x002DF17C File Offset: 0x002DD37C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "off" == name)
			{
				return new Offset();
			}
			if (10 == namespaceId && "ext" == name)
			{
				return new Extents();
			}
			return null;
		}

		// Token: 0x1700628A RID: 25226
		// (get) Token: 0x06013A64 RID: 80484 RVA: 0x0030A3E4 File Offset: 0x003085E4
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform2D.eleTagNames;
			}
		}

		// Token: 0x1700628B RID: 25227
		// (get) Token: 0x06013A65 RID: 80485 RVA: 0x0030A3EB File Offset: 0x003085EB
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform2D.eleNamespaceIds;
			}
		}

		// Token: 0x1700628C RID: 25228
		// (get) Token: 0x06013A66 RID: 80486 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700628D RID: 25229
		// (get) Token: 0x06013A67 RID: 80487 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x06013A68 RID: 80488 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
		public Offset Offset
		{
			get
			{
				return base.GetElement<Offset>(0);
			}
			set
			{
				base.SetElement<Offset>(0, value);
			}
		}

		// Token: 0x1700628E RID: 25230
		// (get) Token: 0x06013A69 RID: 80489 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x06013A6A RID: 80490 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
		public Extents Extents
		{
			get
			{
				return base.GetElement<Extents>(1);
			}
			set
			{
				base.SetElement<Extents>(1, value);
			}
		}

		// Token: 0x06013A6B RID: 80491 RVA: 0x0030A3F4 File Offset: 0x003085F4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rot" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "flipH" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "flipV" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013A6C RID: 80492 RVA: 0x0030A44B File Offset: 0x0030864B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform2D>(deep);
		}

		// Token: 0x06013A6D RID: 80493 RVA: 0x0030A454 File Offset: 0x00308654
		// Note: this type is marked as 'beforefieldinit'.
		static Transform2D()
		{
			byte[] array = new byte[3];
			Transform2D.attributeNamespaceIds = array;
			Transform2D.eleTagNames = new string[] { "off", "ext" };
			Transform2D.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x0400871F RID: 34591
		private const string tagName = "xfrm";

		// Token: 0x04008720 RID: 34592
		private const byte tagNsId = 10;

		// Token: 0x04008721 RID: 34593
		internal const int ElementTypeIdConst = 10180;

		// Token: 0x04008722 RID: 34594
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x04008723 RID: 34595
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008724 RID: 34596
		private static readonly string[] eleTagNames;

		// Token: 0x04008725 RID: 34597
		private static readonly byte[] eleNamespaceIds;
	}
}
