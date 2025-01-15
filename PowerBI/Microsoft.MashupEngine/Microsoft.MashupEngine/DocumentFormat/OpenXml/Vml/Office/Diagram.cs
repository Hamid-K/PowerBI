using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002205 RID: 8709
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RelationTable))]
	internal class Diagram : OpenXmlCompositeElement
	{
		// Token: 0x17003849 RID: 14409
		// (get) Token: 0x0600DE28 RID: 56872 RVA: 0x002BDE0F File Offset: 0x002BC00F
		public override string LocalName
		{
			get
			{
				return "diagram";
			}
		}

		// Token: 0x1700384A RID: 14410
		// (get) Token: 0x0600DE29 RID: 56873 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x1700384B RID: 14411
		// (get) Token: 0x0600DE2A RID: 56874 RVA: 0x002BDE16 File Offset: 0x002BC016
		internal override int ElementTypeId
		{
			get
			{
				return 12403;
			}
		}

		// Token: 0x0600DE2B RID: 56875 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700384C RID: 14412
		// (get) Token: 0x0600DE2C RID: 56876 RVA: 0x002BDE1D File Offset: 0x002BC01D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Diagram.attributeTagNames;
			}
		}

		// Token: 0x1700384D RID: 14413
		// (get) Token: 0x0600DE2D RID: 56877 RVA: 0x002BDE24 File Offset: 0x002BC024
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Diagram.attributeNamespaceIds;
			}
		}

		// Token: 0x1700384E RID: 14414
		// (get) Token: 0x0600DE2E RID: 56878 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DE2F RID: 56879 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700384F RID: 14415
		// (get) Token: 0x0600DE30 RID: 56880 RVA: 0x002BD46B File Offset: 0x002BB66B
		// (set) Token: 0x0600DE31 RID: 56881 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dgmstyle")]
		public IntegerValue Style
		{
			get
			{
				return (IntegerValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003850 RID: 14416
		// (get) Token: 0x0600DE32 RID: 56882 RVA: 0x002BDE2B File Offset: 0x002BC02B
		// (set) Token: 0x0600DE33 RID: 56883 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "autoformat")]
		public TrueFalseValue AutoFormat
		{
			get
			{
				return (TrueFalseValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003851 RID: 14417
		// (get) Token: 0x0600DE34 RID: 56884 RVA: 0x002BD49F File Offset: 0x002BB69F
		// (set) Token: 0x0600DE35 RID: 56885 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "reverse")]
		public TrueFalseValue Reverse
		{
			get
			{
				return (TrueFalseValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003852 RID: 14418
		// (get) Token: 0x0600DE36 RID: 56886 RVA: 0x002BDAE9 File Offset: 0x002BBCE9
		// (set) Token: 0x0600DE37 RID: 56887 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "autolayout")]
		public TrueFalseValue AutoLayout
		{
			get
			{
				return (TrueFalseValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003853 RID: 14419
		// (get) Token: 0x0600DE38 RID: 56888 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0600DE39 RID: 56889 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "dgmscalex")]
		public IntegerValue ScaleX
		{
			get
			{
				return (IntegerValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17003854 RID: 14420
		// (get) Token: 0x0600DE3A RID: 56890 RVA: 0x002BDE49 File Offset: 0x002BC049
		// (set) Token: 0x0600DE3B RID: 56891 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "dgmscaley")]
		public IntegerValue ScaleY
		{
			get
			{
				return (IntegerValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17003855 RID: 14421
		// (get) Token: 0x0600DE3C RID: 56892 RVA: 0x002BDE58 File Offset: 0x002BC058
		// (set) Token: 0x0600DE3D RID: 56893 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "dgmfontsize")]
		public IntegerValue FontSize
		{
			get
			{
				return (IntegerValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17003856 RID: 14422
		// (get) Token: 0x0600DE3E RID: 56894 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600DE3F RID: 56895 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "constrainbounds")]
		public StringValue ConstrainBounds
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17003857 RID: 14423
		// (get) Token: 0x0600DE40 RID: 56896 RVA: 0x002BDE67 File Offset: 0x002BC067
		// (set) Token: 0x0600DE41 RID: 56897 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "dgmbasetextscale")]
		public IntegerValue BaseTextScale
		{
			get
			{
				return (IntegerValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x0600DE42 RID: 56898 RVA: 0x00293ECF File Offset: 0x002920CF
		public Diagram()
		{
		}

		// Token: 0x0600DE43 RID: 56899 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Diagram(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DE44 RID: 56900 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Diagram(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DE45 RID: 56901 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Diagram(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600DE46 RID: 56902 RVA: 0x002BDE77 File Offset: 0x002BC077
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "relationtable" == name)
			{
				return new RelationTable();
			}
			return null;
		}

		// Token: 0x17003858 RID: 14424
		// (get) Token: 0x0600DE47 RID: 56903 RVA: 0x002BDE92 File Offset: 0x002BC092
		internal override string[] ElementTagNames
		{
			get
			{
				return Diagram.eleTagNames;
			}
		}

		// Token: 0x17003859 RID: 14425
		// (get) Token: 0x0600DE48 RID: 56904 RVA: 0x002BDE99 File Offset: 0x002BC099
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Diagram.eleNamespaceIds;
			}
		}

		// Token: 0x1700385A RID: 14426
		// (get) Token: 0x0600DE49 RID: 56905 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700385B RID: 14427
		// (get) Token: 0x0600DE4A RID: 56906 RVA: 0x002BDEA0 File Offset: 0x002BC0A0
		// (set) Token: 0x0600DE4B RID: 56907 RVA: 0x002BDEA9 File Offset: 0x002BC0A9
		public RelationTable RelationTable
		{
			get
			{
				return base.GetElement<RelationTable>(0);
			}
			set
			{
				base.SetElement<RelationTable>(0, value);
			}
		}

		// Token: 0x0600DE4C RID: 56908 RVA: 0x002BDEB4 File Offset: 0x002BC0B4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "dgmstyle" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "autoformat" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "reverse" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "autolayout" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "dgmscalex" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "dgmscaley" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "dgmfontsize" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "constrainbounds" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dgmbasetextscale" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DE4D RID: 56909 RVA: 0x002BDFA7 File Offset: 0x002BC1A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Diagram>(deep);
		}

		// Token: 0x0600DE4E RID: 56910 RVA: 0x002BDFB0 File Offset: 0x002BC1B0
		// Note: this type is marked as 'beforefieldinit'.
		static Diagram()
		{
			byte[] array = new byte[10];
			array[0] = 26;
			Diagram.attributeNamespaceIds = array;
			Diagram.eleTagNames = new string[] { "relationtable" };
			Diagram.eleNamespaceIds = new byte[] { 27 };
		}

		// Token: 0x04006D66 RID: 28006
		private const string tagName = "diagram";

		// Token: 0x04006D67 RID: 28007
		private const byte tagNsId = 27;

		// Token: 0x04006D68 RID: 28008
		internal const int ElementTypeIdConst = 12403;

		// Token: 0x04006D69 RID: 28009
		private static string[] attributeTagNames = new string[] { "ext", "dgmstyle", "autoformat", "reverse", "autolayout", "dgmscalex", "dgmscaley", "dgmfontsize", "constrainbounds", "dgmbasetextscale" };

		// Token: 0x04006D6A RID: 28010
		private static byte[] attributeNamespaceIds;

		// Token: 0x04006D6B RID: 28011
		private static readonly string[] eleTagNames;

		// Token: 0x04006D6C RID: 28012
		private static readonly byte[] eleNamespaceIds;
	}
}
