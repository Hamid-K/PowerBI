using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002359 RID: 9049
	[ChildElementInfo(typeof(Offset))]
	[ChildElementInfo(typeof(Extents))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Transform2D : OpenXmlCompositeElement
	{
		// Token: 0x17004A18 RID: 18968
		// (get) Token: 0x060103E0 RID: 66528 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x17004A19 RID: 18969
		// (get) Token: 0x060103E1 RID: 66529 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A1A RID: 18970
		// (get) Token: 0x060103E2 RID: 66530 RVA: 0x002E156F File Offset: 0x002DF76F
		internal override int ElementTypeId
		{
			get
			{
				return 12732;
			}
		}

		// Token: 0x060103E3 RID: 66531 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A1B RID: 18971
		// (get) Token: 0x060103E4 RID: 66532 RVA: 0x002E1576 File Offset: 0x002DF776
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform2D.attributeTagNames;
			}
		}

		// Token: 0x17004A1C RID: 18972
		// (get) Token: 0x060103E5 RID: 66533 RVA: 0x002E157D File Offset: 0x002DF77D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform2D.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A1D RID: 18973
		// (get) Token: 0x060103E6 RID: 66534 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060103E7 RID: 66535 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A1E RID: 18974
		// (get) Token: 0x060103E8 RID: 66536 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060103E9 RID: 66537 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004A1F RID: 18975
		// (get) Token: 0x060103EA RID: 66538 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060103EB RID: 66539 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x060103EC RID: 66540 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform2D()
		{
		}

		// Token: 0x060103ED RID: 66541 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform2D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103EE RID: 66542 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform2D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103EF RID: 66543 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform2D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060103F0 RID: 66544 RVA: 0x002DF17C File Offset: 0x002DD37C
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

		// Token: 0x17004A20 RID: 18976
		// (get) Token: 0x060103F1 RID: 66545 RVA: 0x002E1584 File Offset: 0x002DF784
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform2D.eleTagNames;
			}
		}

		// Token: 0x17004A21 RID: 18977
		// (get) Token: 0x060103F2 RID: 66546 RVA: 0x002E158B File Offset: 0x002DF78B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform2D.eleNamespaceIds;
			}
		}

		// Token: 0x17004A22 RID: 18978
		// (get) Token: 0x060103F3 RID: 66547 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004A23 RID: 18979
		// (get) Token: 0x060103F4 RID: 66548 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x060103F5 RID: 66549 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
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

		// Token: 0x17004A24 RID: 18980
		// (get) Token: 0x060103F6 RID: 66550 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x060103F7 RID: 66551 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
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

		// Token: 0x060103F8 RID: 66552 RVA: 0x002E1594 File Offset: 0x002DF794
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

		// Token: 0x060103F9 RID: 66553 RVA: 0x002E15EB File Offset: 0x002DF7EB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform2D>(deep);
		}

		// Token: 0x060103FA RID: 66554 RVA: 0x002E15F4 File Offset: 0x002DF7F4
		// Note: this type is marked as 'beforefieldinit'.
		static Transform2D()
		{
			byte[] array = new byte[3];
			Transform2D.attributeNamespaceIds = array;
			Transform2D.eleTagNames = new string[] { "off", "ext" };
			Transform2D.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x040073AE RID: 29614
		private const string tagName = "xfrm";

		// Token: 0x040073AF RID: 29615
		private const byte tagNsId = 48;

		// Token: 0x040073B0 RID: 29616
		internal const int ElementTypeIdConst = 12732;

		// Token: 0x040073B1 RID: 29617
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x040073B2 RID: 29618
		private static byte[] attributeNamespaceIds;

		// Token: 0x040073B3 RID: 29619
		private static readonly string[] eleTagNames;

		// Token: 0x040073B4 RID: 29620
		private static readonly byte[] eleNamespaceIds;
	}
}
