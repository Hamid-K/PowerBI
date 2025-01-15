using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B6E RID: 11118
	[ChildElementInfo(typeof(SortByTuple))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Tuples))]
	internal class TupleSet : OpenXmlCompositeElement
	{
		// Token: 0x1700793E RID: 31038
		// (get) Token: 0x06016E35 RID: 93749 RVA: 0x0032162E File Offset: 0x0031F82E
		public override string LocalName
		{
			get
			{
				return "set";
			}
		}

		// Token: 0x1700793F RID: 31039
		// (get) Token: 0x06016E36 RID: 93750 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007940 RID: 31040
		// (get) Token: 0x06016E37 RID: 93751 RVA: 0x00330273 File Offset: 0x0032E473
		internal override int ElementTypeId
		{
			get
			{
				return 11097;
			}
		}

		// Token: 0x06016E38 RID: 93752 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007941 RID: 31041
		// (get) Token: 0x06016E39 RID: 93753 RVA: 0x0033027A File Offset: 0x0032E47A
		internal override string[] AttributeTagNames
		{
			get
			{
				return TupleSet.attributeTagNames;
			}
		}

		// Token: 0x17007942 RID: 31042
		// (get) Token: 0x06016E3A RID: 93754 RVA: 0x00330281 File Offset: 0x0032E481
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TupleSet.attributeNamespaceIds;
			}
		}

		// Token: 0x17007943 RID: 31043
		// (get) Token: 0x06016E3B RID: 93755 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016E3C RID: 93756 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x17007944 RID: 31044
		// (get) Token: 0x06016E3D RID: 93757 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06016E3E RID: 93758 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "maxRank")]
		public Int32Value MaxRank
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007945 RID: 31045
		// (get) Token: 0x06016E3F RID: 93759 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06016E40 RID: 93760 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "setDefinition")]
		public StringValue SetDefinition
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

		// Token: 0x17007946 RID: 31046
		// (get) Token: 0x06016E41 RID: 93761 RVA: 0x00330288 File Offset: 0x0032E488
		// (set) Token: 0x06016E42 RID: 93762 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sortType")]
		public EnumValue<SortValues> SortType
		{
			get
			{
				return (EnumValue<SortValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007947 RID: 31047
		// (get) Token: 0x06016E43 RID: 93763 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06016E44 RID: 93764 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "queryFailed")]
		public BooleanValue QueryFailed
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06016E45 RID: 93765 RVA: 0x00293ECF File Offset: 0x002920CF
		public TupleSet()
		{
		}

		// Token: 0x06016E46 RID: 93766 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TupleSet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E47 RID: 93767 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TupleSet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E48 RID: 93768 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TupleSet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016E49 RID: 93769 RVA: 0x00330297 File Offset: 0x0032E497
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tpls" == name)
			{
				return new Tuples();
			}
			if (22 == namespaceId && "sortByTuple" == name)
			{
				return new SortByTuple();
			}
			return null;
		}

		// Token: 0x06016E4A RID: 93770 RVA: 0x003302CC File Offset: 0x0032E4CC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "maxRank" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "setDefinition" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sortType" == name)
			{
				return new EnumValue<SortValues>();
			}
			if (namespaceId == 0 && "queryFailed" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016E4B RID: 93771 RVA: 0x0033034F File Offset: 0x0032E54F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TupleSet>(deep);
		}

		// Token: 0x06016E4C RID: 93772 RVA: 0x00330358 File Offset: 0x0032E558
		// Note: this type is marked as 'beforefieldinit'.
		static TupleSet()
		{
			byte[] array = new byte[5];
			TupleSet.attributeNamespaceIds = array;
		}

		// Token: 0x04009A5A RID: 39514
		private const string tagName = "set";

		// Token: 0x04009A5B RID: 39515
		private const byte tagNsId = 22;

		// Token: 0x04009A5C RID: 39516
		internal const int ElementTypeIdConst = 11097;

		// Token: 0x04009A5D RID: 39517
		private static string[] attributeTagNames = new string[] { "count", "maxRank", "setDefinition", "sortType", "queryFailed" };

		// Token: 0x04009A5E RID: 39518
		private static byte[] attributeNamespaceIds;
	}
}
