using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CAD RID: 11437
	[GeneratedCode("DomGen", "2.0")]
	internal class Location : OpenXmlLeafElement
	{
		// Token: 0x17008471 RID: 33905
		// (get) Token: 0x06018705 RID: 100101 RVA: 0x0034180B File Offset: 0x0033FA0B
		public override string LocalName
		{
			get
			{
				return "location";
			}
		}

		// Token: 0x17008472 RID: 33906
		// (get) Token: 0x06018706 RID: 100102 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008473 RID: 33907
		// (get) Token: 0x06018707 RID: 100103 RVA: 0x00341812 File Offset: 0x0033FA12
		internal override int ElementTypeId
		{
			get
			{
				return 11417;
			}
		}

		// Token: 0x06018708 RID: 100104 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008474 RID: 33908
		// (get) Token: 0x06018709 RID: 100105 RVA: 0x00341819 File Offset: 0x0033FA19
		internal override string[] AttributeTagNames
		{
			get
			{
				return Location.attributeTagNames;
			}
		}

		// Token: 0x17008475 RID: 33909
		// (get) Token: 0x0601870A RID: 100106 RVA: 0x00341820 File Offset: 0x0033FA20
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Location.attributeNamespaceIds;
			}
		}

		// Token: 0x17008476 RID: 33910
		// (get) Token: 0x0601870B RID: 100107 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601870C RID: 100108 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x17008477 RID: 33911
		// (get) Token: 0x0601870D RID: 100109 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601870E RID: 100110 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "firstHeaderRow")]
		public UInt32Value FirstHeaderRow
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008478 RID: 33912
		// (get) Token: 0x0601870F RID: 100111 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06018710 RID: 100112 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "firstDataRow")]
		public UInt32Value FirstDataRow
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008479 RID: 33913
		// (get) Token: 0x06018711 RID: 100113 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x06018712 RID: 100114 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "firstDataCol")]
		public UInt32Value FirstDataColumn
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700847A RID: 33914
		// (get) Token: 0x06018713 RID: 100115 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06018714 RID: 100116 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "rowPageCount")]
		public UInt32Value RowPageCount
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700847B RID: 33915
		// (get) Token: 0x06018715 RID: 100117 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x06018716 RID: 100118 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "colPageCount")]
		public UInt32Value ColumnsPerPage
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x06018718 RID: 100120 RVA: 0x00341828 File Offset: 0x0033FA28
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "firstHeaderRow" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "firstDataRow" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "firstDataCol" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "rowPageCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "colPageCount" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018719 RID: 100121 RVA: 0x003418C1 File Offset: 0x0033FAC1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Location>(deep);
		}

		// Token: 0x0601871A RID: 100122 RVA: 0x003418CC File Offset: 0x0033FACC
		// Note: this type is marked as 'beforefieldinit'.
		static Location()
		{
			byte[] array = new byte[6];
			Location.attributeNamespaceIds = array;
		}

		// Token: 0x0400A046 RID: 41030
		private const string tagName = "location";

		// Token: 0x0400A047 RID: 41031
		private const byte tagNsId = 22;

		// Token: 0x0400A048 RID: 41032
		internal const int ElementTypeIdConst = 11417;

		// Token: 0x0400A049 RID: 41033
		private static string[] attributeTagNames = new string[] { "ref", "firstHeaderRow", "firstDataRow", "firstDataCol", "rowPageCount", "colPageCount" };

		// Token: 0x0400A04A RID: 41034
		private static byte[] attributeNamespaceIds;
	}
}
