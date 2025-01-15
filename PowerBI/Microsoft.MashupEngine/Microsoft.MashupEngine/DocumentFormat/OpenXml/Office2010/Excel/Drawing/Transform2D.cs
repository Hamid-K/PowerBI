using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x0200238F RID: 9103
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extents))]
	[ChildElementInfo(typeof(Offset))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Transform2D : OpenXmlCompositeElement
	{
		// Token: 0x17004BA8 RID: 19368
		// (get) Token: 0x0601073F RID: 67391 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x17004BA9 RID: 19369
		// (get) Token: 0x06010740 RID: 67392 RVA: 0x002E35B9 File Offset: 0x002E17B9
		internal override byte NamespaceId
		{
			get
			{
				return 54;
			}
		}

		// Token: 0x17004BAA RID: 19370
		// (get) Token: 0x06010741 RID: 67393 RVA: 0x002E3B2F File Offset: 0x002E1D2F
		internal override int ElementTypeId
		{
			get
			{
				return 13017;
			}
		}

		// Token: 0x06010742 RID: 67394 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004BAB RID: 19371
		// (get) Token: 0x06010743 RID: 67395 RVA: 0x002E3B36 File Offset: 0x002E1D36
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform2D.attributeTagNames;
			}
		}

		// Token: 0x17004BAC RID: 19372
		// (get) Token: 0x06010744 RID: 67396 RVA: 0x002E3B3D File Offset: 0x002E1D3D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform2D.attributeNamespaceIds;
			}
		}

		// Token: 0x17004BAD RID: 19373
		// (get) Token: 0x06010745 RID: 67397 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010746 RID: 67398 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004BAE RID: 19374
		// (get) Token: 0x06010747 RID: 67399 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010748 RID: 67400 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004BAF RID: 19375
		// (get) Token: 0x06010749 RID: 67401 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601074A RID: 67402 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x0601074B RID: 67403 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform2D()
		{
		}

		// Token: 0x0601074C RID: 67404 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform2D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601074D RID: 67405 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform2D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601074E RID: 67406 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform2D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601074F RID: 67407 RVA: 0x002DF17C File Offset: 0x002DD37C
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

		// Token: 0x17004BB0 RID: 19376
		// (get) Token: 0x06010750 RID: 67408 RVA: 0x002E3B44 File Offset: 0x002E1D44
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform2D.eleTagNames;
			}
		}

		// Token: 0x17004BB1 RID: 19377
		// (get) Token: 0x06010751 RID: 67409 RVA: 0x002E3B4B File Offset: 0x002E1D4B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform2D.eleNamespaceIds;
			}
		}

		// Token: 0x17004BB2 RID: 19378
		// (get) Token: 0x06010752 RID: 67410 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004BB3 RID: 19379
		// (get) Token: 0x06010753 RID: 67411 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x06010754 RID: 67412 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
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

		// Token: 0x17004BB4 RID: 19380
		// (get) Token: 0x06010755 RID: 67413 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x06010756 RID: 67414 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
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

		// Token: 0x06010757 RID: 67415 RVA: 0x002E3B54 File Offset: 0x002E1D54
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

		// Token: 0x06010758 RID: 67416 RVA: 0x002E3BAB File Offset: 0x002E1DAB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform2D>(deep);
		}

		// Token: 0x06010759 RID: 67417 RVA: 0x002E3BB4 File Offset: 0x002E1DB4
		// Note: this type is marked as 'beforefieldinit'.
		static Transform2D()
		{
			byte[] array = new byte[3];
			Transform2D.attributeNamespaceIds = array;
			Transform2D.eleTagNames = new string[] { "off", "ext" };
			Transform2D.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x040074AB RID: 29867
		private const string tagName = "xfrm";

		// Token: 0x040074AC RID: 29868
		private const byte tagNsId = 54;

		// Token: 0x040074AD RID: 29869
		internal const int ElementTypeIdConst = 13017;

		// Token: 0x040074AE RID: 29870
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x040074AF RID: 29871
		private static byte[] attributeNamespaceIds;

		// Token: 0x040074B0 RID: 29872
		private static readonly string[] eleTagNames;

		// Token: 0x040074B1 RID: 29873
		private static readonly byte[] eleNamespaceIds;
	}
}
