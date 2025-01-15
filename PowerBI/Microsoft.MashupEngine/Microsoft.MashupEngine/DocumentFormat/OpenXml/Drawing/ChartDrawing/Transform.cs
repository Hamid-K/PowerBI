using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x0200263B RID: 9787
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Offset))]
	[ChildElementInfo(typeof(Extents))]
	internal class Transform : OpenXmlCompositeElement
	{
		// Token: 0x17005AB7 RID: 23223
		// (get) Token: 0x060128C0 RID: 75968 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x17005AB8 RID: 23224
		// (get) Token: 0x060128C1 RID: 75969 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005AB9 RID: 23225
		// (get) Token: 0x060128C2 RID: 75970 RVA: 0x002FC7F1 File Offset: 0x002FA9F1
		internal override int ElementTypeId
		{
			get
			{
				return 10606;
			}
		}

		// Token: 0x060128C3 RID: 75971 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005ABA RID: 23226
		// (get) Token: 0x060128C4 RID: 75972 RVA: 0x002FC7F8 File Offset: 0x002FA9F8
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform.attributeTagNames;
			}
		}

		// Token: 0x17005ABB RID: 23227
		// (get) Token: 0x060128C5 RID: 75973 RVA: 0x002FC7FF File Offset: 0x002FA9FF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform.attributeNamespaceIds;
			}
		}

		// Token: 0x17005ABC RID: 23228
		// (get) Token: 0x060128C6 RID: 75974 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060128C7 RID: 75975 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005ABD RID: 23229
		// (get) Token: 0x060128C8 RID: 75976 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060128C9 RID: 75977 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17005ABE RID: 23230
		// (get) Token: 0x060128CA RID: 75978 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060128CB RID: 75979 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x060128CC RID: 75980 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform()
		{
		}

		// Token: 0x060128CD RID: 75981 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060128CE RID: 75982 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060128CF RID: 75983 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060128D0 RID: 75984 RVA: 0x002DF17C File Offset: 0x002DD37C
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

		// Token: 0x17005ABF RID: 23231
		// (get) Token: 0x060128D1 RID: 75985 RVA: 0x002FC806 File Offset: 0x002FAA06
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform.eleTagNames;
			}
		}

		// Token: 0x17005AC0 RID: 23232
		// (get) Token: 0x060128D2 RID: 75986 RVA: 0x002FC80D File Offset: 0x002FAA0D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform.eleNamespaceIds;
			}
		}

		// Token: 0x17005AC1 RID: 23233
		// (get) Token: 0x060128D3 RID: 75987 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005AC2 RID: 23234
		// (get) Token: 0x060128D4 RID: 75988 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x060128D5 RID: 75989 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
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

		// Token: 0x17005AC3 RID: 23235
		// (get) Token: 0x060128D6 RID: 75990 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x060128D7 RID: 75991 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
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

		// Token: 0x060128D8 RID: 75992 RVA: 0x002FC814 File Offset: 0x002FAA14
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

		// Token: 0x060128D9 RID: 75993 RVA: 0x002FC86B File Offset: 0x002FAA6B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform>(deep);
		}

		// Token: 0x060128DA RID: 75994 RVA: 0x002FC874 File Offset: 0x002FAA74
		// Note: this type is marked as 'beforefieldinit'.
		static Transform()
		{
			byte[] array = new byte[3];
			Transform.attributeNamespaceIds = array;
			Transform.eleTagNames = new string[] { "off", "ext" };
			Transform.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x0400809B RID: 32923
		private const string tagName = "xfrm";

		// Token: 0x0400809C RID: 32924
		private const byte tagNsId = 12;

		// Token: 0x0400809D RID: 32925
		internal const int ElementTypeIdConst = 10606;

		// Token: 0x0400809E RID: 32926
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x0400809F RID: 32927
		private static byte[] attributeNamespaceIds;

		// Token: 0x040080A0 RID: 32928
		private static readonly string[] eleTagNames;

		// Token: 0x040080A1 RID: 32929
		private static readonly byte[] eleNamespaceIds;
	}
}
