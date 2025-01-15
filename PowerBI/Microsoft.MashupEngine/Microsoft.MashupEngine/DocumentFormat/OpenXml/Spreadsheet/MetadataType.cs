using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BFA RID: 11258
	[GeneratedCode("DomGen", "2.0")]
	internal class MetadataType : OpenXmlLeafElement
	{
		// Token: 0x17007EEE RID: 32494
		// (get) Token: 0x06017A51 RID: 96849 RVA: 0x003396A8 File Offset: 0x003378A8
		public override string LocalName
		{
			get
			{
				return "metadataType";
			}
		}

		// Token: 0x17007EEF RID: 32495
		// (get) Token: 0x06017A52 RID: 96850 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007EF0 RID: 32496
		// (get) Token: 0x06017A53 RID: 96851 RVA: 0x003396AF File Offset: 0x003378AF
		internal override int ElementTypeId
		{
			get
			{
				return 11237;
			}
		}

		// Token: 0x06017A54 RID: 96852 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007EF1 RID: 32497
		// (get) Token: 0x06017A55 RID: 96853 RVA: 0x003396B6 File Offset: 0x003378B6
		internal override string[] AttributeTagNames
		{
			get
			{
				return MetadataType.attributeTagNames;
			}
		}

		// Token: 0x17007EF2 RID: 32498
		// (get) Token: 0x06017A56 RID: 96854 RVA: 0x003396BD File Offset: 0x003378BD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MetadataType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007EF3 RID: 32499
		// (get) Token: 0x06017A57 RID: 96855 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017A58 RID: 96856 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007EF4 RID: 32500
		// (get) Token: 0x06017A59 RID: 96857 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06017A5A RID: 96858 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "minSupportedVersion")]
		public UInt32Value MinSupportedVersion
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

		// Token: 0x17007EF5 RID: 32501
		// (get) Token: 0x06017A5B RID: 96859 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017A5C RID: 96860 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ghostRow")]
		public BooleanValue GhostRow
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

		// Token: 0x17007EF6 RID: 32502
		// (get) Token: 0x06017A5D RID: 96861 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017A5E RID: 96862 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "ghostCol")]
		public BooleanValue GhostColumn
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

		// Token: 0x17007EF7 RID: 32503
		// (get) Token: 0x06017A5F RID: 96863 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017A60 RID: 96864 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "edit")]
		public BooleanValue Edit
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

		// Token: 0x17007EF8 RID: 32504
		// (get) Token: 0x06017A61 RID: 96865 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017A62 RID: 96866 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "delete")]
		public BooleanValue Delete
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

		// Token: 0x17007EF9 RID: 32505
		// (get) Token: 0x06017A63 RID: 96867 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017A64 RID: 96868 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "copy")]
		public BooleanValue Copy
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

		// Token: 0x17007EFA RID: 32506
		// (get) Token: 0x06017A65 RID: 96869 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017A66 RID: 96870 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "pasteAll")]
		public BooleanValue PasteAll
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007EFB RID: 32507
		// (get) Token: 0x06017A67 RID: 96871 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06017A68 RID: 96872 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "pasteFormulas")]
		public BooleanValue PasteFormulas
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007EFC RID: 32508
		// (get) Token: 0x06017A69 RID: 96873 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06017A6A RID: 96874 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "pasteValues")]
		public BooleanValue PasteValues
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17007EFD RID: 32509
		// (get) Token: 0x06017A6B RID: 96875 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06017A6C RID: 96876 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "pasteFormats")]
		public BooleanValue PasteFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17007EFE RID: 32510
		// (get) Token: 0x06017A6D RID: 96877 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06017A6E RID: 96878 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "pasteComments")]
		public BooleanValue PasteComments
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17007EFF RID: 32511
		// (get) Token: 0x06017A6F RID: 96879 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06017A70 RID: 96880 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "pasteDataValidation")]
		public BooleanValue PasteDataValidation
		{
			get
			{
				return (BooleanValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17007F00 RID: 32512
		// (get) Token: 0x06017A71 RID: 96881 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06017A72 RID: 96882 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "pasteBorders")]
		public BooleanValue PasteBorders
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17007F01 RID: 32513
		// (get) Token: 0x06017A73 RID: 96883 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x06017A74 RID: 96884 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "pasteColWidths")]
		public BooleanValue PasteColWidths
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17007F02 RID: 32514
		// (get) Token: 0x06017A75 RID: 96885 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x06017A76 RID: 96886 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "pasteNumberFormats")]
		public BooleanValue PasteNumberFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17007F03 RID: 32515
		// (get) Token: 0x06017A77 RID: 96887 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x06017A78 RID: 96888 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "merge")]
		public BooleanValue Merge
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17007F04 RID: 32516
		// (get) Token: 0x06017A79 RID: 96889 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x06017A7A RID: 96890 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "splitFirst")]
		public BooleanValue SplitFirst
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17007F05 RID: 32517
		// (get) Token: 0x06017A7B RID: 96891 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x06017A7C RID: 96892 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "splitAll")]
		public BooleanValue SplitAll
		{
			get
			{
				return (BooleanValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17007F06 RID: 32518
		// (get) Token: 0x06017A7D RID: 96893 RVA: 0x002D6080 File Offset: 0x002D4280
		// (set) Token: 0x06017A7E RID: 96894 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "rowColShift")]
		public BooleanValue RowColumnShift
		{
			get
			{
				return (BooleanValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17007F07 RID: 32519
		// (get) Token: 0x06017A7F RID: 96895 RVA: 0x002C8F75 File Offset: 0x002C7175
		// (set) Token: 0x06017A80 RID: 96896 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "clearAll")]
		public BooleanValue ClearAll
		{
			get
			{
				return (BooleanValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17007F08 RID: 32520
		// (get) Token: 0x06017A81 RID: 96897 RVA: 0x002DB1B1 File Offset: 0x002D93B1
		// (set) Token: 0x06017A82 RID: 96898 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "clearFormats")]
		public BooleanValue ClearFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17007F09 RID: 32521
		// (get) Token: 0x06017A83 RID: 96899 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x06017A84 RID: 96900 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "clearContents")]
		public BooleanValue ClearContents
		{
			get
			{
				return (BooleanValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17007F0A RID: 32522
		// (get) Token: 0x06017A85 RID: 96901 RVA: 0x002C99DC File Offset: 0x002C7BDC
		// (set) Token: 0x06017A86 RID: 96902 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "clearComments")]
		public BooleanValue ClearComments
		{
			get
			{
				return (BooleanValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x17007F0B RID: 32523
		// (get) Token: 0x06017A87 RID: 96903 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x06017A88 RID: 96904 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "assign")]
		public BooleanValue Assign
		{
			get
			{
				return (BooleanValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17007F0C RID: 32524
		// (get) Token: 0x06017A89 RID: 96905 RVA: 0x002CBE3C File Offset: 0x002CA03C
		// (set) Token: 0x06017A8A RID: 96906 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "coerce")]
		public BooleanValue Coerce
		{
			get
			{
				return (BooleanValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x17007F0D RID: 32525
		// (get) Token: 0x06017A8B RID: 96907 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x06017A8C RID: 96908 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "adjust")]
		public BooleanValue Adjust
		{
			get
			{
				return (BooleanValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17007F0E RID: 32526
		// (get) Token: 0x06017A8D RID: 96909 RVA: 0x002C99EC File Offset: 0x002C7BEC
		// (set) Token: 0x06017A8E RID: 96910 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "cellMeta")]
		public BooleanValue CellMeta
		{
			get
			{
				return (BooleanValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x06017A90 RID: 96912 RVA: 0x003396C4 File Offset: 0x003378C4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "minSupportedVersion" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "ghostRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ghostCol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "edit" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "delete" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "copy" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pasteAll" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pasteFormulas" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pasteValues" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pasteFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pasteComments" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pasteDataValidation" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pasteBorders" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pasteColWidths" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pasteNumberFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "merge" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "splitFirst" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "splitAll" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rowColShift" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "clearAll" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "clearFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "clearContents" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "clearComments" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "assign" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "coerce" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "adjust" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "cellMeta" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017A91 RID: 96913 RVA: 0x00339941 File Offset: 0x00337B41
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MetadataType>(deep);
		}

		// Token: 0x06017A92 RID: 96914 RVA: 0x0033994C File Offset: 0x00337B4C
		// Note: this type is marked as 'beforefieldinit'.
		static MetadataType()
		{
			byte[] array = new byte[28];
			MetadataType.attributeNamespaceIds = array;
		}

		// Token: 0x04009D09 RID: 40201
		private const string tagName = "metadataType";

		// Token: 0x04009D0A RID: 40202
		private const byte tagNsId = 22;

		// Token: 0x04009D0B RID: 40203
		internal const int ElementTypeIdConst = 11237;

		// Token: 0x04009D0C RID: 40204
		private static string[] attributeTagNames = new string[]
		{
			"name", "minSupportedVersion", "ghostRow", "ghostCol", "edit", "delete", "copy", "pasteAll", "pasteFormulas", "pasteValues",
			"pasteFormats", "pasteComments", "pasteDataValidation", "pasteBorders", "pasteColWidths", "pasteNumberFormats", "merge", "splitFirst", "splitAll", "rowColShift",
			"clearAll", "clearFormats", "clearContents", "clearComments", "assign", "coerce", "adjust", "cellMeta"
		};

		// Token: 0x04009D0D RID: 40205
		private static byte[] attributeNamespaceIds;
	}
}
