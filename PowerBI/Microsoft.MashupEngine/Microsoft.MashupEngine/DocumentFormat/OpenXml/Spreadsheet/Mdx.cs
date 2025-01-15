using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BFE RID: 11262
	[ChildElementInfo(typeof(MdxMemberProp))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MdxKpi))]
	[ChildElementInfo(typeof(MdxSet))]
	[ChildElementInfo(typeof(MdxTuple))]
	internal class Mdx : OpenXmlCompositeElement
	{
		// Token: 0x17007F20 RID: 32544
		// (get) Token: 0x06017ABB RID: 96955 RVA: 0x00339B84 File Offset: 0x00337D84
		public override string LocalName
		{
			get
			{
				return "mdx";
			}
		}

		// Token: 0x17007F21 RID: 32545
		// (get) Token: 0x06017ABC RID: 96956 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F22 RID: 32546
		// (get) Token: 0x06017ABD RID: 96957 RVA: 0x00339B8B File Offset: 0x00337D8B
		internal override int ElementTypeId
		{
			get
			{
				return 11241;
			}
		}

		// Token: 0x06017ABE RID: 96958 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F23 RID: 32547
		// (get) Token: 0x06017ABF RID: 96959 RVA: 0x00339B92 File Offset: 0x00337D92
		internal override string[] AttributeTagNames
		{
			get
			{
				return Mdx.attributeTagNames;
			}
		}

		// Token: 0x17007F24 RID: 32548
		// (get) Token: 0x06017AC0 RID: 96960 RVA: 0x00339B99 File Offset: 0x00337D99
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Mdx.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F25 RID: 32549
		// (get) Token: 0x06017AC1 RID: 96961 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017AC2 RID: 96962 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "n")]
		public UInt32Value NameIndex
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007F26 RID: 32550
		// (get) Token: 0x06017AC3 RID: 96963 RVA: 0x00339BA0 File Offset: 0x00337DA0
		// (set) Token: 0x06017AC4 RID: 96964 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "f")]
		public EnumValue<MdxFunctionValues> CubeFunction
		{
			get
			{
				return (EnumValue<MdxFunctionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06017AC5 RID: 96965 RVA: 0x00293ECF File Offset: 0x002920CF
		public Mdx()
		{
		}

		// Token: 0x06017AC6 RID: 96966 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Mdx(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017AC7 RID: 96967 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Mdx(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017AC8 RID: 96968 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Mdx(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017AC9 RID: 96969 RVA: 0x00339BB0 File Offset: 0x00337DB0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "t" == name)
			{
				return new MdxTuple();
			}
			if (22 == namespaceId && "ms" == name)
			{
				return new MdxSet();
			}
			if (22 == namespaceId && "p" == name)
			{
				return new MdxMemberProp();
			}
			if (22 == namespaceId && "k" == name)
			{
				return new MdxKpi();
			}
			return null;
		}

		// Token: 0x17007F27 RID: 32551
		// (get) Token: 0x06017ACA RID: 96970 RVA: 0x00339C1E File Offset: 0x00337E1E
		internal override string[] ElementTagNames
		{
			get
			{
				return Mdx.eleTagNames;
			}
		}

		// Token: 0x17007F28 RID: 32552
		// (get) Token: 0x06017ACB RID: 96971 RVA: 0x00339C25 File Offset: 0x00337E25
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Mdx.eleNamespaceIds;
			}
		}

		// Token: 0x17007F29 RID: 32553
		// (get) Token: 0x06017ACC RID: 96972 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007F2A RID: 32554
		// (get) Token: 0x06017ACD RID: 96973 RVA: 0x00339C2C File Offset: 0x00337E2C
		// (set) Token: 0x06017ACE RID: 96974 RVA: 0x00339C35 File Offset: 0x00337E35
		public MdxTuple MdxTuple
		{
			get
			{
				return base.GetElement<MdxTuple>(0);
			}
			set
			{
				base.SetElement<MdxTuple>(0, value);
			}
		}

		// Token: 0x17007F2B RID: 32555
		// (get) Token: 0x06017ACF RID: 96975 RVA: 0x00339C3F File Offset: 0x00337E3F
		// (set) Token: 0x06017AD0 RID: 96976 RVA: 0x00339C48 File Offset: 0x00337E48
		public MdxSet MdxSet
		{
			get
			{
				return base.GetElement<MdxSet>(1);
			}
			set
			{
				base.SetElement<MdxSet>(1, value);
			}
		}

		// Token: 0x17007F2C RID: 32556
		// (get) Token: 0x06017AD1 RID: 96977 RVA: 0x00339C52 File Offset: 0x00337E52
		// (set) Token: 0x06017AD2 RID: 96978 RVA: 0x00339C5B File Offset: 0x00337E5B
		public MdxMemberProp MdxMemberProp
		{
			get
			{
				return base.GetElement<MdxMemberProp>(2);
			}
			set
			{
				base.SetElement<MdxMemberProp>(2, value);
			}
		}

		// Token: 0x17007F2D RID: 32557
		// (get) Token: 0x06017AD3 RID: 96979 RVA: 0x00339C65 File Offset: 0x00337E65
		// (set) Token: 0x06017AD4 RID: 96980 RVA: 0x00339C6E File Offset: 0x00337E6E
		public MdxKpi MdxKpi
		{
			get
			{
				return base.GetElement<MdxKpi>(3);
			}
			set
			{
				base.SetElement<MdxKpi>(3, value);
			}
		}

		// Token: 0x06017AD5 RID: 96981 RVA: 0x00339C78 File Offset: 0x00337E78
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "n" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "f" == name)
			{
				return new EnumValue<MdxFunctionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017AD6 RID: 96982 RVA: 0x00339CAE File Offset: 0x00337EAE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Mdx>(deep);
		}

		// Token: 0x06017AD7 RID: 96983 RVA: 0x00339CB8 File Offset: 0x00337EB8
		// Note: this type is marked as 'beforefieldinit'.
		static Mdx()
		{
			byte[] array = new byte[2];
			Mdx.attributeNamespaceIds = array;
			Mdx.eleTagNames = new string[] { "t", "ms", "p", "k" };
			Mdx.eleNamespaceIds = new byte[] { 22, 22, 22, 22 };
		}

		// Token: 0x04009D1B RID: 40219
		private const string tagName = "mdx";

		// Token: 0x04009D1C RID: 40220
		private const byte tagNsId = 22;

		// Token: 0x04009D1D RID: 40221
		internal const int ElementTypeIdConst = 11241;

		// Token: 0x04009D1E RID: 40222
		private static string[] attributeTagNames = new string[] { "n", "f" };

		// Token: 0x04009D1F RID: 40223
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009D20 RID: 40224
		private static readonly string[] eleTagNames;

		// Token: 0x04009D21 RID: 40225
		private static readonly byte[] eleNamespaceIds;
	}
}
