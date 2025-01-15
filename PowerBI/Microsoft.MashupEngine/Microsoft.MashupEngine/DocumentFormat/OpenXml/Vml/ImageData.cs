using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x0200224A RID: 8778
	[GeneratedCode("DomGen", "2.0")]
	internal class ImageData : OpenXmlLeafElement
	{
		// Token: 0x17003A07 RID: 14855
		// (get) Token: 0x0600E1CC RID: 57804 RVA: 0x002C10A7 File Offset: 0x002BF2A7
		public override string LocalName
		{
			get
			{
				return "imagedata";
			}
		}

		// Token: 0x17003A08 RID: 14856
		// (get) Token: 0x0600E1CD RID: 57805 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003A09 RID: 14857
		// (get) Token: 0x0600E1CE RID: 57806 RVA: 0x002C10AE File Offset: 0x002BF2AE
		internal override int ElementTypeId
		{
			get
			{
				return 12514;
			}
		}

		// Token: 0x0600E1CF RID: 57807 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003A0A RID: 14858
		// (get) Token: 0x0600E1D0 RID: 57808 RVA: 0x002C10B5 File Offset: 0x002BF2B5
		internal override string[] AttributeTagNames
		{
			get
			{
				return ImageData.attributeTagNames;
			}
		}

		// Token: 0x17003A0B RID: 14859
		// (get) Token: 0x0600E1D1 RID: 57809 RVA: 0x002C10BC File Offset: 0x002BF2BC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ImageData.attributeNamespaceIds;
			}
		}

		// Token: 0x17003A0C RID: 14860
		// (get) Token: 0x0600E1D2 RID: 57810 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E1D3 RID: 57811 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003A0D RID: 14861
		// (get) Token: 0x0600E1D4 RID: 57812 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E1D5 RID: 57813 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "chromakey")]
		public StringValue ChromAKey
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

		// Token: 0x17003A0E RID: 14862
		// (get) Token: 0x0600E1D6 RID: 57814 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E1D7 RID: 57815 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "cropleft")]
		public StringValue CropLeft
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

		// Token: 0x17003A0F RID: 14863
		// (get) Token: 0x0600E1D8 RID: 57816 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E1D9 RID: 57817 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "croptop")]
		public StringValue CropTop
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

		// Token: 0x17003A10 RID: 14864
		// (get) Token: 0x0600E1DA RID: 57818 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E1DB RID: 57819 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "cropright")]
		public StringValue CropRight
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

		// Token: 0x17003A11 RID: 14865
		// (get) Token: 0x0600E1DC RID: 57820 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E1DD RID: 57821 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "cropbottom")]
		public StringValue CropBottom
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

		// Token: 0x17003A12 RID: 14866
		// (get) Token: 0x0600E1DE RID: 57822 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E1DF RID: 57823 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "gain")]
		public StringValue Gain
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

		// Token: 0x17003A13 RID: 14867
		// (get) Token: 0x0600E1E0 RID: 57824 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E1E1 RID: 57825 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "blacklevel")]
		public StringValue BlackLevel
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

		// Token: 0x17003A14 RID: 14868
		// (get) Token: 0x0600E1E2 RID: 57826 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E1E3 RID: 57827 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "gamma")]
		public StringValue Gamma
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

		// Token: 0x17003A15 RID: 14869
		// (get) Token: 0x0600E1E4 RID: 57828 RVA: 0x002BEA5B File Offset: 0x002BCC5B
		// (set) Token: 0x0600E1E5 RID: 57829 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "grayscale")]
		public TrueFalseValue Grayscale
		{
			get
			{
				return (TrueFalseValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17003A16 RID: 14870
		// (get) Token: 0x0600E1E6 RID: 57830 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600E1E7 RID: 57831 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "bilevel")]
		public TrueFalseValue BiLevel
		{
			get
			{
				return (TrueFalseValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17003A17 RID: 14871
		// (get) Token: 0x0600E1E8 RID: 57832 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E1E9 RID: 57833 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "embosscolor")]
		public StringValue EmbossColor
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17003A18 RID: 14872
		// (get) Token: 0x0600E1EA RID: 57834 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E1EB RID: 57835 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "recolortarget")]
		public StringValue RecolorTarget
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

		// Token: 0x17003A19 RID: 14873
		// (get) Token: 0x0600E1EC RID: 57836 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E1ED RID: 57837 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(27, "title")]
		public StringValue Title
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17003A1A RID: 14874
		// (get) Token: 0x0600E1EE RID: 57838 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E1EF RID: 57839 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(27, "detectmouseclick")]
		public TrueFalseValue DetectMouseClick
		{
			get
			{
				return (TrueFalseValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17003A1B RID: 14875
		// (get) Token: 0x0600E1F0 RID: 57840 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600E1F1 RID: 57841 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(27, "relid")]
		public StringValue RelId
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17003A1C RID: 14876
		// (get) Token: 0x0600E1F2 RID: 57842 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600E1F3 RID: 57843 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(19, "id")]
		public StringValue RelationshipId
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17003A1D RID: 14877
		// (get) Token: 0x0600E1F4 RID: 57844 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E1F5 RID: 57845 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(19, "pict")]
		public StringValue Picture
		{
			get
			{
				return (StringValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17003A1E RID: 14878
		// (get) Token: 0x0600E1F6 RID: 57846 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600E1F7 RID: 57847 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(19, "href")]
		public StringValue RelHref
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x0600E1F9 RID: 57849 RVA: 0x002C10C4 File Offset: 0x002BF2C4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "chromakey" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cropleft" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "croptop" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cropright" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cropbottom" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "gain" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "blacklevel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "gamma" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "grayscale" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "bilevel" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "embosscolor" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "recolortarget" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "title" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "detectmouseclick" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "relid" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "pict" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "href" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E1FA RID: 57850 RVA: 0x002C1287 File Offset: 0x002BF487
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ImageData>(deep);
		}

		// Token: 0x04006EB8 RID: 28344
		private const string tagName = "imagedata";

		// Token: 0x04006EB9 RID: 28345
		private const byte tagNsId = 26;

		// Token: 0x04006EBA RID: 28346
		internal const int ElementTypeIdConst = 12514;

		// Token: 0x04006EBB RID: 28347
		private static string[] attributeTagNames = new string[]
		{
			"id", "chromakey", "cropleft", "croptop", "cropright", "cropbottom", "gain", "blacklevel", "gamma", "grayscale",
			"bilevel", "embosscolor", "recolortarget", "title", "detectmouseclick", "relid", "id", "pict", "href"
		};

		// Token: 0x04006EBC RID: 28348
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 27, 27, 27, 19, 19, 19
		};
	}
}
