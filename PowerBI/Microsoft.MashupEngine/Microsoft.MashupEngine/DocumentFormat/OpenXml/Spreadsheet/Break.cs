using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BD3 RID: 11219
	[GeneratedCode("DomGen", "2.0")]
	internal class Break : OpenXmlLeafElement
	{
		// Token: 0x17007D39 RID: 32057
		// (get) Token: 0x060176AB RID: 95915 RVA: 0x0031B686 File Offset: 0x00319886
		public override string LocalName
		{
			get
			{
				return "brk";
			}
		}

		// Token: 0x17007D3A RID: 32058
		// (get) Token: 0x060176AC RID: 95916 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D3B RID: 32059
		// (get) Token: 0x060176AD RID: 95917 RVA: 0x003368B6 File Offset: 0x00334AB6
		internal override int ElementTypeId
		{
			get
			{
				return 11192;
			}
		}

		// Token: 0x060176AE RID: 95918 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D3C RID: 32060
		// (get) Token: 0x060176AF RID: 95919 RVA: 0x003368BD File Offset: 0x00334ABD
		internal override string[] AttributeTagNames
		{
			get
			{
				return Break.attributeTagNames;
			}
		}

		// Token: 0x17007D3D RID: 32061
		// (get) Token: 0x060176B0 RID: 95920 RVA: 0x003368C4 File Offset: 0x00334AC4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Break.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D3E RID: 32062
		// (get) Token: 0x060176B1 RID: 95921 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060176B2 RID: 95922 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
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

		// Token: 0x17007D3F RID: 32063
		// (get) Token: 0x060176B3 RID: 95923 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060176B4 RID: 95924 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "min")]
		public UInt32Value Min
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

		// Token: 0x17007D40 RID: 32064
		// (get) Token: 0x060176B5 RID: 95925 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x060176B6 RID: 95926 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "max")]
		public UInt32Value Max
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

		// Token: 0x17007D41 RID: 32065
		// (get) Token: 0x060176B7 RID: 95927 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060176B8 RID: 95928 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "man")]
		public BooleanValue ManualPageBreak
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007D42 RID: 32066
		// (get) Token: 0x060176B9 RID: 95929 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060176BA RID: 95930 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "pt")]
		public BooleanValue PivotTablePageBreak
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

		// Token: 0x060176BC RID: 95932 RVA: 0x003368CC File Offset: 0x00334ACC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "min" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "max" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "man" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pt" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060176BD RID: 95933 RVA: 0x0033694F File Offset: 0x00334B4F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Break>(deep);
		}

		// Token: 0x060176BE RID: 95934 RVA: 0x00336958 File Offset: 0x00334B58
		// Note: this type is marked as 'beforefieldinit'.
		static Break()
		{
			byte[] array = new byte[5];
			Break.attributeNamespaceIds = array;
		}

		// Token: 0x04009C40 RID: 40000
		private const string tagName = "brk";

		// Token: 0x04009C41 RID: 40001
		private const byte tagNsId = 22;

		// Token: 0x04009C42 RID: 40002
		internal const int ElementTypeIdConst = 11192;

		// Token: 0x04009C43 RID: 40003
		private static string[] attributeTagNames = new string[] { "id", "min", "max", "man", "pt" };

		// Token: 0x04009C44 RID: 40004
		private static byte[] attributeNamespaceIds;
	}
}
