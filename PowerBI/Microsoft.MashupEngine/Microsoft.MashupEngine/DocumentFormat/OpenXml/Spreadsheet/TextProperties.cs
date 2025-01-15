using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CD6 RID: 11478
	[ChildElementInfo(typeof(TextFields))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TextProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700859C RID: 34204
		// (get) Token: 0x060189E0 RID: 100832 RVA: 0x00343288 File Offset: 0x00341488
		public override string LocalName
		{
			get
			{
				return "textPr";
			}
		}

		// Token: 0x1700859D RID: 34205
		// (get) Token: 0x060189E1 RID: 100833 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700859E RID: 34206
		// (get) Token: 0x060189E2 RID: 100834 RVA: 0x0034328F File Offset: 0x0034148F
		internal override int ElementTypeId
		{
			get
			{
				return 11459;
			}
		}

		// Token: 0x060189E3 RID: 100835 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700859F RID: 34207
		// (get) Token: 0x060189E4 RID: 100836 RVA: 0x00343296 File Offset: 0x00341496
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextProperties.attributeTagNames;
			}
		}

		// Token: 0x170085A0 RID: 34208
		// (get) Token: 0x060189E5 RID: 100837 RVA: 0x0034329D File Offset: 0x0034149D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170085A1 RID: 34209
		// (get) Token: 0x060189E6 RID: 100838 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060189E7 RID: 100839 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "prompt")]
		public BooleanValue Prompt
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170085A2 RID: 34210
		// (get) Token: 0x060189E8 RID: 100840 RVA: 0x003432A4 File Offset: 0x003414A4
		// (set) Token: 0x060189E9 RID: 100841 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fileType")]
		public EnumValue<FileTypeValues> FileType
		{
			get
			{
				return (EnumValue<FileTypeValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170085A3 RID: 34211
		// (get) Token: 0x060189EA RID: 100842 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x060189EB RID: 100843 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "codePage")]
		public UInt32Value CodePage
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

		// Token: 0x170085A4 RID: 34212
		// (get) Token: 0x060189EC RID: 100844 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x060189ED RID: 100845 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "firstRow")]
		public UInt32Value FirstRow
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

		// Token: 0x170085A5 RID: 34213
		// (get) Token: 0x060189EE RID: 100846 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060189EF RID: 100847 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "sourceFile")]
		public StringValue SourceFile
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

		// Token: 0x170085A6 RID: 34214
		// (get) Token: 0x060189F0 RID: 100848 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060189F1 RID: 100849 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "delimited")]
		public BooleanValue Delimited
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

		// Token: 0x170085A7 RID: 34215
		// (get) Token: 0x060189F2 RID: 100850 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x060189F3 RID: 100851 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "decimal")]
		public StringValue Decimal
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170085A8 RID: 34216
		// (get) Token: 0x060189F4 RID: 100852 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x060189F5 RID: 100853 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "thousands")]
		public StringValue Thousands
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170085A9 RID: 34217
		// (get) Token: 0x060189F6 RID: 100854 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060189F7 RID: 100855 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "tab")]
		public BooleanValue TabAsDelimiter
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

		// Token: 0x170085AA RID: 34218
		// (get) Token: 0x060189F8 RID: 100856 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x060189F9 RID: 100857 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "space")]
		public BooleanValue Space
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

		// Token: 0x170085AB RID: 34219
		// (get) Token: 0x060189FA RID: 100858 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x060189FB RID: 100859 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "comma")]
		public BooleanValue Comma
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

		// Token: 0x170085AC RID: 34220
		// (get) Token: 0x060189FC RID: 100860 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x060189FD RID: 100861 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "semicolon")]
		public BooleanValue Semicolon
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

		// Token: 0x170085AD RID: 34221
		// (get) Token: 0x060189FE RID: 100862 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x060189FF RID: 100863 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "consecutive")]
		public BooleanValue Consecutive
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

		// Token: 0x170085AE RID: 34222
		// (get) Token: 0x06018A00 RID: 100864 RVA: 0x003432B3 File Offset: 0x003414B3
		// (set) Token: 0x06018A01 RID: 100865 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "qualifier")]
		public EnumValue<QualifierValues> Qualifier
		{
			get
			{
				return (EnumValue<QualifierValues>)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x170085AF RID: 34223
		// (get) Token: 0x06018A02 RID: 100866 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x06018A03 RID: 100867 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "delimiter")]
		public StringValue Delimiter
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x06018A04 RID: 100868 RVA: 0x00293ECF File Offset: 0x002920CF
		public TextProperties()
		{
		}

		// Token: 0x06018A05 RID: 100869 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TextProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018A06 RID: 100870 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TextProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018A07 RID: 100871 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TextProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018A08 RID: 100872 RVA: 0x003432C3 File Offset: 0x003414C3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "textFields" == name)
			{
				return new TextFields();
			}
			return null;
		}

		// Token: 0x170085B0 RID: 34224
		// (get) Token: 0x06018A09 RID: 100873 RVA: 0x003432DE File Offset: 0x003414DE
		internal override string[] ElementTagNames
		{
			get
			{
				return TextProperties.eleTagNames;
			}
		}

		// Token: 0x170085B1 RID: 34225
		// (get) Token: 0x06018A0A RID: 100874 RVA: 0x003432E5 File Offset: 0x003414E5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170085B2 RID: 34226
		// (get) Token: 0x06018A0B RID: 100875 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170085B3 RID: 34227
		// (get) Token: 0x06018A0C RID: 100876 RVA: 0x003432EC File Offset: 0x003414EC
		// (set) Token: 0x06018A0D RID: 100877 RVA: 0x003432F5 File Offset: 0x003414F5
		public TextFields TextFields
		{
			get
			{
				return base.GetElement<TextFields>(0);
			}
			set
			{
				base.SetElement<TextFields>(0, value);
			}
		}

		// Token: 0x06018A0E RID: 100878 RVA: 0x00343300 File Offset: 0x00341500
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "prompt" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fileType" == name)
			{
				return new EnumValue<FileTypeValues>();
			}
			if (namespaceId == 0 && "codePage" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "firstRow" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "sourceFile" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "delimited" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "decimal" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "thousands" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tab" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "space" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "comma" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "semicolon" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "consecutive" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "qualifier" == name)
			{
				return new EnumValue<QualifierValues>();
			}
			if (namespaceId == 0 && "delimiter" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018A0F RID: 100879 RVA: 0x0034345F File Offset: 0x0034165F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TextProperties>(deep);
		}

		// Token: 0x06018A10 RID: 100880 RVA: 0x00343468 File Offset: 0x00341668
		// Note: this type is marked as 'beforefieldinit'.
		static TextProperties()
		{
			byte[] array = new byte[15];
			TextProperties.attributeNamespaceIds = array;
			TextProperties.eleTagNames = new string[] { "textFields" };
			TextProperties.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x0400A10D RID: 41229
		private const string tagName = "textPr";

		// Token: 0x0400A10E RID: 41230
		private const byte tagNsId = 22;

		// Token: 0x0400A10F RID: 41231
		internal const int ElementTypeIdConst = 11459;

		// Token: 0x0400A110 RID: 41232
		private static string[] attributeTagNames = new string[]
		{
			"prompt", "fileType", "codePage", "firstRow", "sourceFile", "delimited", "decimal", "thousands", "tab", "space",
			"comma", "semicolon", "consecutive", "qualifier", "delimiter"
		};

		// Token: 0x0400A111 RID: 41233
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400A112 RID: 41234
		private static readonly string[] eleTagNames;

		// Token: 0x0400A113 RID: 41235
		private static readonly byte[] eleNamespaceIds;
	}
}
