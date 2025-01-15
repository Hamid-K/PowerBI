using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B72 RID: 11122
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CalculatedMemberExtensionList))]
	internal class CalculatedMember : OpenXmlCompositeElement
	{
		// Token: 0x17007974 RID: 31092
		// (get) Token: 0x06016EA8 RID: 93864 RVA: 0x002E5E23 File Offset: 0x002E4023
		public override string LocalName
		{
			get
			{
				return "calculatedMember";
			}
		}

		// Token: 0x17007975 RID: 31093
		// (get) Token: 0x06016EA9 RID: 93865 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007976 RID: 31094
		// (get) Token: 0x06016EAA RID: 93866 RVA: 0x00330798 File Offset: 0x0032E998
		internal override int ElementTypeId
		{
			get
			{
				return 11102;
			}
		}

		// Token: 0x06016EAB RID: 93867 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007977 RID: 31095
		// (get) Token: 0x06016EAC RID: 93868 RVA: 0x0033079F File Offset: 0x0032E99F
		internal override string[] AttributeTagNames
		{
			get
			{
				return CalculatedMember.attributeTagNames;
			}
		}

		// Token: 0x17007978 RID: 31096
		// (get) Token: 0x06016EAD RID: 93869 RVA: 0x003307A6 File Offset: 0x0032E9A6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CalculatedMember.attributeNamespaceIds;
			}
		}

		// Token: 0x17007979 RID: 31097
		// (get) Token: 0x06016EAE RID: 93870 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016EAF RID: 93871 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700797A RID: 31098
		// (get) Token: 0x06016EB0 RID: 93872 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016EB1 RID: 93873 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "mdx")]
		public StringValue Mdx
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700797B RID: 31099
		// (get) Token: 0x06016EB2 RID: 93874 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016EB3 RID: 93875 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "memberName")]
		public StringValue MemberName
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700797C RID: 31100
		// (get) Token: 0x06016EB4 RID: 93876 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06016EB5 RID: 93877 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "hierarchy")]
		public StringValue Hierarchy
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700797D RID: 31101
		// (get) Token: 0x06016EB6 RID: 93878 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06016EB7 RID: 93879 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "parent")]
		public StringValue ParentName
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700797E RID: 31102
		// (get) Token: 0x06016EB8 RID: 93880 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x06016EB9 RID: 93881 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "solveOrder")]
		public Int32Value SolveOrder
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700797F RID: 31103
		// (get) Token: 0x06016EBA RID: 93882 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06016EBB RID: 93883 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "set")]
		public BooleanValue Set
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x06016EBC RID: 93884 RVA: 0x00293ECF File Offset: 0x002920CF
		public CalculatedMember()
		{
		}

		// Token: 0x06016EBD RID: 93885 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CalculatedMember(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016EBE RID: 93886 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CalculatedMember(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016EBF RID: 93887 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CalculatedMember(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016EC0 RID: 93888 RVA: 0x003307AD File Offset: 0x0032E9AD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new CalculatedMemberExtensionList();
			}
			return null;
		}

		// Token: 0x17007980 RID: 31104
		// (get) Token: 0x06016EC1 RID: 93889 RVA: 0x003307C8 File Offset: 0x0032E9C8
		internal override string[] ElementTagNames
		{
			get
			{
				return CalculatedMember.eleTagNames;
			}
		}

		// Token: 0x17007981 RID: 31105
		// (get) Token: 0x06016EC2 RID: 93890 RVA: 0x003307CF File Offset: 0x0032E9CF
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CalculatedMember.eleNamespaceIds;
			}
		}

		// Token: 0x17007982 RID: 31106
		// (get) Token: 0x06016EC3 RID: 93891 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007983 RID: 31107
		// (get) Token: 0x06016EC4 RID: 93892 RVA: 0x003307D6 File Offset: 0x0032E9D6
		// (set) Token: 0x06016EC5 RID: 93893 RVA: 0x003307DF File Offset: 0x0032E9DF
		public CalculatedMemberExtensionList CalculatedMemberExtensionList
		{
			get
			{
				return base.GetElement<CalculatedMemberExtensionList>(0);
			}
			set
			{
				base.SetElement<CalculatedMemberExtensionList>(0, value);
			}
		}

		// Token: 0x06016EC6 RID: 93894 RVA: 0x003307EC File Offset: 0x0032E9EC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "mdx" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "memberName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hierarchy" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "parent" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "solveOrder" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "set" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016EC7 RID: 93895 RVA: 0x0033089B File Offset: 0x0032EA9B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculatedMember>(deep);
		}

		// Token: 0x06016EC8 RID: 93896 RVA: 0x003308A4 File Offset: 0x0032EAA4
		// Note: this type is marked as 'beforefieldinit'.
		static CalculatedMember()
		{
			byte[] array = new byte[7];
			CalculatedMember.attributeNamespaceIds = array;
			CalculatedMember.eleTagNames = new string[] { "extLst" };
			CalculatedMember.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009A74 RID: 39540
		private const string tagName = "calculatedMember";

		// Token: 0x04009A75 RID: 39541
		private const byte tagNsId = 22;

		// Token: 0x04009A76 RID: 39542
		internal const int ElementTypeIdConst = 11102;

		// Token: 0x04009A77 RID: 39543
		private static string[] attributeTagNames = new string[] { "name", "mdx", "memberName", "hierarchy", "parent", "solveOrder", "set" };

		// Token: 0x04009A78 RID: 39544
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009A79 RID: 39545
		private static readonly string[] eleTagNames;

		// Token: 0x04009A7A RID: 39546
		private static readonly byte[] eleNamespaceIds;
	}
}
