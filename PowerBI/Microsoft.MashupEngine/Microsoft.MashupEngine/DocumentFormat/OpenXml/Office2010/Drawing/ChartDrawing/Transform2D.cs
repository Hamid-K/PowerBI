using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x0200233E RID: 9022
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Extents))]
	[ChildElementInfo(typeof(Offset))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Transform2D : OpenXmlCompositeElement
	{
		// Token: 0x1700493D RID: 18749
		// (get) Token: 0x060101FF RID: 66047 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x1700493E RID: 18750
		// (get) Token: 0x06010200 RID: 66048 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		internal override byte NamespaceId
		{
			get
			{
				return 47;
			}
		}

		// Token: 0x1700493F RID: 18751
		// (get) Token: 0x06010201 RID: 66049 RVA: 0x002E0032 File Offset: 0x002DE232
		internal override int ElementTypeId
		{
			get
			{
				return 12710;
			}
		}

		// Token: 0x06010202 RID: 66050 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004940 RID: 18752
		// (get) Token: 0x06010203 RID: 66051 RVA: 0x002E0039 File Offset: 0x002DE239
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform2D.attributeTagNames;
			}
		}

		// Token: 0x17004941 RID: 18753
		// (get) Token: 0x06010204 RID: 66052 RVA: 0x002E0040 File Offset: 0x002DE240
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform2D.attributeNamespaceIds;
			}
		}

		// Token: 0x17004942 RID: 18754
		// (get) Token: 0x06010205 RID: 66053 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010206 RID: 66054 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004943 RID: 18755
		// (get) Token: 0x06010207 RID: 66055 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010208 RID: 66056 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004944 RID: 18756
		// (get) Token: 0x06010209 RID: 66057 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601020A RID: 66058 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x0601020B RID: 66059 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform2D()
		{
		}

		// Token: 0x0601020C RID: 66060 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform2D(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601020D RID: 66061 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform2D(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601020E RID: 66062 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform2D(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601020F RID: 66063 RVA: 0x002DF17C File Offset: 0x002DD37C
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

		// Token: 0x17004945 RID: 18757
		// (get) Token: 0x06010210 RID: 66064 RVA: 0x002E0047 File Offset: 0x002DE247
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform2D.eleTagNames;
			}
		}

		// Token: 0x17004946 RID: 18758
		// (get) Token: 0x06010211 RID: 66065 RVA: 0x002E004E File Offset: 0x002DE24E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform2D.eleNamespaceIds;
			}
		}

		// Token: 0x17004947 RID: 18759
		// (get) Token: 0x06010212 RID: 66066 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004948 RID: 18760
		// (get) Token: 0x06010213 RID: 66067 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x06010214 RID: 66068 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
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

		// Token: 0x17004949 RID: 18761
		// (get) Token: 0x06010215 RID: 66069 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x06010216 RID: 66070 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
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

		// Token: 0x06010217 RID: 66071 RVA: 0x002E0058 File Offset: 0x002DE258
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

		// Token: 0x06010218 RID: 66072 RVA: 0x002E00AF File Offset: 0x002DE2AF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform2D>(deep);
		}

		// Token: 0x06010219 RID: 66073 RVA: 0x002E00B8 File Offset: 0x002DE2B8
		// Note: this type is marked as 'beforefieldinit'.
		static Transform2D()
		{
			byte[] array = new byte[3];
			Transform2D.attributeNamespaceIds = array;
			Transform2D.eleTagNames = new string[] { "off", "ext" };
			Transform2D.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x0400732D RID: 29485
		private const string tagName = "xfrm";

		// Token: 0x0400732E RID: 29486
		private const byte tagNsId = 47;

		// Token: 0x0400732F RID: 29487
		internal const int ElementTypeIdConst = 12710;

		// Token: 0x04007330 RID: 29488
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x04007331 RID: 29489
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007332 RID: 29490
		private static readonly string[] eleTagNames;

		// Token: 0x04007333 RID: 29491
		private static readonly byte[] eleNamespaceIds;
	}
}
