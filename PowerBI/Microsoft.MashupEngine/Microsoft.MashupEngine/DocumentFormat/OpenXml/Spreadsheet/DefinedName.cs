using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C3B RID: 11323
	[GeneratedCode("DomGen", "2.0")]
	internal class DefinedName : OpenXmlLeafTextElement
	{
		// Token: 0x17008169 RID: 33129
		// (get) Token: 0x06017FAE RID: 98222 RVA: 0x002E8690 File Offset: 0x002E6890
		public override string LocalName
		{
			get
			{
				return "definedName";
			}
		}

		// Token: 0x1700816A RID: 33130
		// (get) Token: 0x06017FAF RID: 98223 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700816B RID: 33131
		// (get) Token: 0x06017FB0 RID: 98224 RVA: 0x0033D504 File Offset: 0x0033B704
		internal override int ElementTypeId
		{
			get
			{
				return 11305;
			}
		}

		// Token: 0x06017FB1 RID: 98225 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700816C RID: 33132
		// (get) Token: 0x06017FB2 RID: 98226 RVA: 0x0033D50B File Offset: 0x0033B70B
		internal override string[] AttributeTagNames
		{
			get
			{
				return DefinedName.attributeTagNames;
			}
		}

		// Token: 0x1700816D RID: 33133
		// (get) Token: 0x06017FB3 RID: 98227 RVA: 0x0033D512 File Offset: 0x0033B712
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DefinedName.attributeNamespaceIds;
			}
		}

		// Token: 0x1700816E RID: 33134
		// (get) Token: 0x06017FB4 RID: 98228 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017FB5 RID: 98229 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700816F RID: 33135
		// (get) Token: 0x06017FB6 RID: 98230 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017FB7 RID: 98231 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "comment")]
		public StringValue Comment
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

		// Token: 0x17008170 RID: 33136
		// (get) Token: 0x06017FB8 RID: 98232 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06017FB9 RID: 98233 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "customMenu")]
		public StringValue CustomMenu
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

		// Token: 0x17008171 RID: 33137
		// (get) Token: 0x06017FBA RID: 98234 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06017FBB RID: 98235 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17008172 RID: 33138
		// (get) Token: 0x06017FBC RID: 98236 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06017FBD RID: 98237 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "help")]
		public StringValue Help
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

		// Token: 0x17008173 RID: 33139
		// (get) Token: 0x06017FBE RID: 98238 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06017FBF RID: 98239 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "statusBar")]
		public StringValue StatusBar
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008174 RID: 33140
		// (get) Token: 0x06017FC0 RID: 98240 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06017FC1 RID: 98241 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "localSheetId")]
		public UInt32Value LocalSheetId
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008175 RID: 33141
		// (get) Token: 0x06017FC2 RID: 98242 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017FC3 RID: 98243 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
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

		// Token: 0x17008176 RID: 33142
		// (get) Token: 0x06017FC4 RID: 98244 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06017FC5 RID: 98245 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "function")]
		public BooleanValue Function
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

		// Token: 0x17008177 RID: 33143
		// (get) Token: 0x06017FC6 RID: 98246 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06017FC7 RID: 98247 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "vbProcedure")]
		public BooleanValue VbProcedure
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

		// Token: 0x17008178 RID: 33144
		// (get) Token: 0x06017FC8 RID: 98248 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06017FC9 RID: 98249 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "xlm")]
		public BooleanValue Xlm
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

		// Token: 0x17008179 RID: 33145
		// (get) Token: 0x06017FCA RID: 98250 RVA: 0x002E9686 File Offset: 0x002E7886
		// (set) Token: 0x06017FCB RID: 98251 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "functionGroupId")]
		public UInt32Value FunctionGroupId
		{
			get
			{
				return (UInt32Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x1700817A RID: 33146
		// (get) Token: 0x06017FCC RID: 98252 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x06017FCD RID: 98253 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "shortcutKey")]
		public StringValue ShortcutKey
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x1700817B RID: 33147
		// (get) Token: 0x06017FCE RID: 98254 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06017FCF RID: 98255 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "publishToServer")]
		public BooleanValue PublishToServer
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

		// Token: 0x1700817C RID: 33148
		// (get) Token: 0x06017FD0 RID: 98256 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x06017FD1 RID: 98257 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "workbookParameter")]
		public BooleanValue WorkbookParameter
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

		// Token: 0x06017FD2 RID: 98258 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DefinedName()
		{
		}

		// Token: 0x06017FD3 RID: 98259 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DefinedName(string text)
			: base(text)
		{
		}

		// Token: 0x06017FD4 RID: 98260 RVA: 0x0033D51C File Offset: 0x0033B71C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06017FD5 RID: 98261 RVA: 0x0033D538 File Offset: 0x0033B738
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "comment" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "customMenu" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "help" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "statusBar" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "localSheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "function" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "vbProcedure" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "xlm" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "functionGroupId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "shortcutKey" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "publishToServer" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "workbookParameter" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017FD6 RID: 98262 RVA: 0x0033D697 File Offset: 0x0033B897
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DefinedName>(deep);
		}

		// Token: 0x06017FD7 RID: 98263 RVA: 0x0033D6A0 File Offset: 0x0033B8A0
		// Note: this type is marked as 'beforefieldinit'.
		static DefinedName()
		{
			byte[] array = new byte[15];
			DefinedName.attributeNamespaceIds = array;
		}

		// Token: 0x04009E5D RID: 40541
		private const string tagName = "definedName";

		// Token: 0x04009E5E RID: 40542
		private const byte tagNsId = 22;

		// Token: 0x04009E5F RID: 40543
		internal const int ElementTypeIdConst = 11305;

		// Token: 0x04009E60 RID: 40544
		private static string[] attributeTagNames = new string[]
		{
			"name", "comment", "customMenu", "description", "help", "statusBar", "localSheetId", "hidden", "function", "vbProcedure",
			"xlm", "functionGroupId", "shortcutKey", "publishToServer", "workbookParameter"
		};

		// Token: 0x04009E61 RID: 40545
		private static byte[] attributeNamespaceIds;
	}
}
