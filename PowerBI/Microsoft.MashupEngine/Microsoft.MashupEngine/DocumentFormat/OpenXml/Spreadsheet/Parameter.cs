using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B48 RID: 11080
	[GeneratedCode("DomGen", "2.0")]
	internal class Parameter : OpenXmlLeafElement
	{
		// Token: 0x170077FC RID: 30716
		// (get) Token: 0x06016B61 RID: 93025 RVA: 0x0032E37B File Offset: 0x0032C57B
		public override string LocalName
		{
			get
			{
				return "parameter";
			}
		}

		// Token: 0x170077FD RID: 30717
		// (get) Token: 0x06016B62 RID: 93026 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170077FE RID: 30718
		// (get) Token: 0x06016B63 RID: 93027 RVA: 0x0032E382 File Offset: 0x0032C582
		internal override int ElementTypeId
		{
			get
			{
				return 11063;
			}
		}

		// Token: 0x06016B64 RID: 93028 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170077FF RID: 30719
		// (get) Token: 0x06016B65 RID: 93029 RVA: 0x0032E389 File Offset: 0x0032C589
		internal override string[] AttributeTagNames
		{
			get
			{
				return Parameter.attributeTagNames;
			}
		}

		// Token: 0x17007800 RID: 30720
		// (get) Token: 0x06016B66 RID: 93030 RVA: 0x0032E390 File Offset: 0x0032C590
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Parameter.attributeNamespaceIds;
			}
		}

		// Token: 0x17007801 RID: 30721
		// (get) Token: 0x06016B67 RID: 93031 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016B68 RID: 93032 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007802 RID: 30722
		// (get) Token: 0x06016B69 RID: 93033 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06016B6A RID: 93034 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sqlType")]
		public Int32Value SqlType
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

		// Token: 0x17007803 RID: 30723
		// (get) Token: 0x06016B6B RID: 93035 RVA: 0x0032E397 File Offset: 0x0032C597
		// (set) Token: 0x06016B6C RID: 93036 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "parameterType")]
		public EnumValue<ParameterValues> ParameterType
		{
			get
			{
				return (EnumValue<ParameterValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007804 RID: 30724
		// (get) Token: 0x06016B6D RID: 93037 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06016B6E RID: 93038 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "refreshOnChange")]
		public BooleanValue RefreshOnChange
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

		// Token: 0x17007805 RID: 30725
		// (get) Token: 0x06016B6F RID: 93039 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06016B70 RID: 93040 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "prompt")]
		public StringValue Prompt
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

		// Token: 0x17007806 RID: 30726
		// (get) Token: 0x06016B71 RID: 93041 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06016B72 RID: 93042 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "boolean")]
		public BooleanValue Boolean
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007807 RID: 30727
		// (get) Token: 0x06016B73 RID: 93043 RVA: 0x002FE65A File Offset: 0x002FC85A
		// (set) Token: 0x06016B74 RID: 93044 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "double")]
		public DoubleValue Double
		{
			get
			{
				return (DoubleValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007808 RID: 30728
		// (get) Token: 0x06016B75 RID: 93045 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x06016B76 RID: 93046 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "integer")]
		public Int32Value Integer
		{
			get
			{
				return (Int32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007809 RID: 30729
		// (get) Token: 0x06016B77 RID: 93047 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06016B78 RID: 93048 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "string")]
		public StringValue String
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

		// Token: 0x1700780A RID: 30730
		// (get) Token: 0x06016B79 RID: 93049 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06016B7A RID: 93050 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "cell")]
		public StringValue Cell
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x06016B7C RID: 93052 RVA: 0x0032E3A8 File Offset: 0x0032C5A8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sqlType" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "parameterType" == name)
			{
				return new EnumValue<ParameterValues>();
			}
			if (namespaceId == 0 && "refreshOnChange" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "prompt" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "boolean" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "double" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "integer" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "string" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cell" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016B7D RID: 93053 RVA: 0x0032E499 File Offset: 0x0032C699
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Parameter>(deep);
		}

		// Token: 0x06016B7E RID: 93054 RVA: 0x0032E4A4 File Offset: 0x0032C6A4
		// Note: this type is marked as 'beforefieldinit'.
		static Parameter()
		{
			byte[] array = new byte[10];
			Parameter.attributeNamespaceIds = array;
		}

		// Token: 0x040099A6 RID: 39334
		private const string tagName = "parameter";

		// Token: 0x040099A7 RID: 39335
		private const byte tagNsId = 22;

		// Token: 0x040099A8 RID: 39336
		internal const int ElementTypeIdConst = 11063;

		// Token: 0x040099A9 RID: 39337
		private static string[] attributeTagNames = new string[] { "name", "sqlType", "parameterType", "refreshOnChange", "prompt", "boolean", "double", "integer", "string", "cell" };

		// Token: 0x040099AA RID: 39338
		private static byte[] attributeNamespaceIds;
	}
}
