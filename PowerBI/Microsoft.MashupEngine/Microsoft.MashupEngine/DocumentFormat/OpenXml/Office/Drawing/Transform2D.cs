using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x0200232C RID: 9004
	[ChildElementInfo(typeof(Extents))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Offset))]
	internal class Transform2D : OpenXmlCompositeElement
	{
		// Token: 0x170048B9 RID: 18617
		// (get) Token: 0x060100D0 RID: 65744 RVA: 0x002DF160 File Offset: 0x002DD360
		public override string LocalName
		{
			get
			{
				return "txXfrm";
			}
		}

		// Token: 0x170048BA RID: 18618
		// (get) Token: 0x060100D1 RID: 65745 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x170048BB RID: 18619
		// (get) Token: 0x060100D2 RID: 65746 RVA: 0x002DF167 File Offset: 0x002DD367
		internal override int ElementTypeId
		{
			get
			{
				return 13027;
			}
		}

		// Token: 0x060100D3 RID: 65747 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170048BC RID: 18620
		// (get) Token: 0x060100D4 RID: 65748 RVA: 0x002DF16E File Offset: 0x002DD36E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform2D.attributeTagNames;
			}
		}

		// Token: 0x170048BD RID: 18621
		// (get) Token: 0x060100D5 RID: 65749 RVA: 0x002DF175 File Offset: 0x002DD375
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform2D.attributeNamespaceIds;
			}
		}

		// Token: 0x170048BE RID: 18622
		// (get) Token: 0x060100D6 RID: 65750 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060100D7 RID: 65751 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170048BF RID: 18623
		// (get) Token: 0x060100D8 RID: 65752 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060100D9 RID: 65753 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170048C0 RID: 18624
		// (get) Token: 0x060100DA RID: 65754 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060100DB RID: 65755 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x060100DC RID: 65756 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform2D()
		{
		}

		// Token: 0x060100DD RID: 65757 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform2D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060100DE RID: 65758 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform2D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060100DF RID: 65759 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform2D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060100E0 RID: 65760 RVA: 0x002DF17C File Offset: 0x002DD37C
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

		// Token: 0x170048C1 RID: 18625
		// (get) Token: 0x060100E1 RID: 65761 RVA: 0x002DF1AF File Offset: 0x002DD3AF
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform2D.eleTagNames;
			}
		}

		// Token: 0x170048C2 RID: 18626
		// (get) Token: 0x060100E2 RID: 65762 RVA: 0x002DF1B6 File Offset: 0x002DD3B6
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform2D.eleNamespaceIds;
			}
		}

		// Token: 0x170048C3 RID: 18627
		// (get) Token: 0x060100E3 RID: 65763 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170048C4 RID: 18628
		// (get) Token: 0x060100E4 RID: 65764 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x060100E5 RID: 65765 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
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

		// Token: 0x170048C5 RID: 18629
		// (get) Token: 0x060100E6 RID: 65766 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x060100E7 RID: 65767 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
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

		// Token: 0x060100E8 RID: 65768 RVA: 0x002DF1E4 File Offset: 0x002DD3E4
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

		// Token: 0x060100E9 RID: 65769 RVA: 0x002DF23B File Offset: 0x002DD43B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform2D>(deep);
		}

		// Token: 0x060100EA RID: 65770 RVA: 0x002DF244 File Offset: 0x002DD444
		// Note: this type is marked as 'beforefieldinit'.
		static Transform2D()
		{
			byte[] array = new byte[3];
			Transform2D.attributeNamespaceIds = array;
			Transform2D.eleTagNames = new string[] { "off", "ext" };
			Transform2D.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x040072DE RID: 29406
		private const string tagName = "txXfrm";

		// Token: 0x040072DF RID: 29407
		private const byte tagNsId = 56;

		// Token: 0x040072E0 RID: 29408
		internal const int ElementTypeIdConst = 13027;

		// Token: 0x040072E1 RID: 29409
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x040072E2 RID: 29410
		private static byte[] attributeNamespaceIds;

		// Token: 0x040072E3 RID: 29411
		private static readonly string[] eleTagNames;

		// Token: 0x040072E4 RID: 29412
		private static readonly byte[] eleNamespaceIds;
	}
}
