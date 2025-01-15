using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CD9 RID: 11481
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ObjectAnchor), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class CommentProperties : OpenXmlCompositeElement
	{
		// Token: 0x170085BD RID: 34237
		// (get) Token: 0x06018A2B RID: 100907 RVA: 0x003435D2 File Offset: 0x003417D2
		public override string LocalName
		{
			get
			{
				return "commentPr";
			}
		}

		// Token: 0x170085BE RID: 34238
		// (get) Token: 0x06018A2C RID: 100908 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170085BF RID: 34239
		// (get) Token: 0x06018A2D RID: 100909 RVA: 0x003435D9 File Offset: 0x003417D9
		internal override int ElementTypeId
		{
			get
			{
				return 11463;
			}
		}

		// Token: 0x06018A2E RID: 100910 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170085C0 RID: 34240
		// (get) Token: 0x06018A2F RID: 100911 RVA: 0x003435E0 File Offset: 0x003417E0
		internal override string[] AttributeTagNames
		{
			get
			{
				return CommentProperties.attributeTagNames;
			}
		}

		// Token: 0x170085C1 RID: 34241
		// (get) Token: 0x06018A30 RID: 100912 RVA: 0x003435E7 File Offset: 0x003417E7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CommentProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170085C2 RID: 34242
		// (get) Token: 0x06018A31 RID: 100913 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018A32 RID: 100914 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "locked")]
		public BooleanValue Locked
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

		// Token: 0x170085C3 RID: 34243
		// (get) Token: 0x06018A33 RID: 100915 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06018A34 RID: 100916 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "defaultSize")]
		public BooleanValue DefaultSize
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170085C4 RID: 34244
		// (get) Token: 0x06018A35 RID: 100917 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06018A36 RID: 100918 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "print")]
		public BooleanValue Print
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

		// Token: 0x170085C5 RID: 34245
		// (get) Token: 0x06018A37 RID: 100919 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06018A38 RID: 100920 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "disabled")]
		public BooleanValue Disabled
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

		// Token: 0x170085C6 RID: 34246
		// (get) Token: 0x06018A39 RID: 100921 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06018A3A RID: 100922 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "uiObject")]
		public BooleanValue UiObject
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

		// Token: 0x170085C7 RID: 34247
		// (get) Token: 0x06018A3B RID: 100923 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06018A3C RID: 100924 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "autoFill")]
		public BooleanValue AutoFill
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

		// Token: 0x170085C8 RID: 34248
		// (get) Token: 0x06018A3D RID: 100925 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06018A3E RID: 100926 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "autoLine")]
		public BooleanValue AutoLine
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

		// Token: 0x170085C9 RID: 34249
		// (get) Token: 0x06018A3F RID: 100927 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06018A40 RID: 100928 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "altText")]
		public StringValue AltText
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

		// Token: 0x170085CA RID: 34250
		// (get) Token: 0x06018A41 RID: 100929 RVA: 0x003435EE File Offset: 0x003417EE
		// (set) Token: 0x06018A42 RID: 100930 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "textHAlign")]
		public EnumValue<TextHorizontalAlignmentValues> TextHAlign
		{
			get
			{
				return (EnumValue<TextHorizontalAlignmentValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170085CB RID: 34251
		// (get) Token: 0x06018A43 RID: 100931 RVA: 0x003435FD File Offset: 0x003417FD
		// (set) Token: 0x06018A44 RID: 100932 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "textVAlign")]
		public EnumValue<TextVerticalAlignmentValues> TextVAlign
		{
			get
			{
				return (EnumValue<TextVerticalAlignmentValues>)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170085CC RID: 34252
		// (get) Token: 0x06018A45 RID: 100933 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06018A46 RID: 100934 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "lockText")]
		public BooleanValue LockText
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

		// Token: 0x170085CD RID: 34253
		// (get) Token: 0x06018A47 RID: 100935 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06018A48 RID: 100936 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "justLastX")]
		public BooleanValue JustLastX
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

		// Token: 0x170085CE RID: 34254
		// (get) Token: 0x06018A49 RID: 100937 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06018A4A RID: 100938 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "autoScale")]
		public BooleanValue AutoScale
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

		// Token: 0x170085CF RID: 34255
		// (get) Token: 0x06018A4B RID: 100939 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06018A4C RID: 100940 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "rowHidden")]
		public BooleanValue RowHidden
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

		// Token: 0x170085D0 RID: 34256
		// (get) Token: 0x06018A4D RID: 100941 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x06018A4E RID: 100942 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "colHidden")]
		public BooleanValue ColHidden
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

		// Token: 0x06018A4F RID: 100943 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommentProperties()
		{
		}

		// Token: 0x06018A50 RID: 100944 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommentProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018A51 RID: 100945 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommentProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018A52 RID: 100946 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommentProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018A53 RID: 100947 RVA: 0x0033F8F0 File Offset: 0x0033DAF0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "anchor" == name)
			{
				return new ObjectAnchor();
			}
			return null;
		}

		// Token: 0x170085D1 RID: 34257
		// (get) Token: 0x06018A54 RID: 100948 RVA: 0x0034360D File Offset: 0x0034180D
		internal override string[] ElementTagNames
		{
			get
			{
				return CommentProperties.eleTagNames;
			}
		}

		// Token: 0x170085D2 RID: 34258
		// (get) Token: 0x06018A55 RID: 100949 RVA: 0x00343614 File Offset: 0x00341814
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CommentProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170085D3 RID: 34259
		// (get) Token: 0x06018A56 RID: 100950 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170085D4 RID: 34260
		// (get) Token: 0x06018A57 RID: 100951 RVA: 0x0033F919 File Offset: 0x0033DB19
		// (set) Token: 0x06018A58 RID: 100952 RVA: 0x0033F922 File Offset: 0x0033DB22
		public ObjectAnchor ObjectAnchor
		{
			get
			{
				return base.GetElement<ObjectAnchor>(0);
			}
			set
			{
				base.SetElement<ObjectAnchor>(0, value);
			}
		}

		// Token: 0x06018A59 RID: 100953 RVA: 0x0034361C File Offset: 0x0034181C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "locked" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "defaultSize" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "print" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "disabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "uiObject" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoFill" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoLine" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "altText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "textHAlign" == name)
			{
				return new EnumValue<TextHorizontalAlignmentValues>();
			}
			if (namespaceId == 0 && "textVAlign" == name)
			{
				return new EnumValue<TextVerticalAlignmentValues>();
			}
			if (namespaceId == 0 && "lockText" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "justLastX" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoScale" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rowHidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "colHidden" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018A5A RID: 100954 RVA: 0x0034377B File Offset: 0x0034197B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommentProperties>(deep);
		}

		// Token: 0x06018A5B RID: 100955 RVA: 0x00343784 File Offset: 0x00341984
		// Note: this type is marked as 'beforefieldinit'.
		static CommentProperties()
		{
			byte[] array = new byte[15];
			CommentProperties.attributeNamespaceIds = array;
			CommentProperties.eleTagNames = new string[] { "anchor" };
			CommentProperties.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x0400A11C RID: 41244
		private const string tagName = "commentPr";

		// Token: 0x0400A11D RID: 41245
		private const byte tagNsId = 22;

		// Token: 0x0400A11E RID: 41246
		internal const int ElementTypeIdConst = 11463;

		// Token: 0x0400A11F RID: 41247
		private static string[] attributeTagNames = new string[]
		{
			"locked", "defaultSize", "print", "disabled", "uiObject", "autoFill", "autoLine", "altText", "textHAlign", "textVAlign",
			"lockText", "justLastX", "autoScale", "rowHidden", "colHidden"
		};

		// Token: 0x0400A120 RID: 41248
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400A121 RID: 41249
		private static readonly string[] eleTagNames;

		// Token: 0x0400A122 RID: 41250
		private static readonly byte[] eleNamespaceIds;
	}
}
