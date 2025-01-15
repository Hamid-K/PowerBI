using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x02002276 RID: 8822
	[GeneratedCode("DomGen", "2.0")]
	internal class UnsizedDynamicMenu : OpenXmlLeafElement
	{
		// Token: 0x17003DD9 RID: 15833
		// (get) Token: 0x0600E9B0 RID: 59824 RVA: 0x002CA71A File Offset: 0x002C891A
		public override string LocalName
		{
			get
			{
				return "dynamicMenu";
			}
		}

		// Token: 0x17003DDA RID: 15834
		// (get) Token: 0x0600E9B1 RID: 59825 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003DDB RID: 15835
		// (get) Token: 0x0600E9B2 RID: 59826 RVA: 0x002CA721 File Offset: 0x002C8921
		internal override int ElementTypeId
		{
			get
			{
				return 12581;
			}
		}

		// Token: 0x0600E9B3 RID: 59827 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003DDC RID: 15836
		// (get) Token: 0x0600E9B4 RID: 59828 RVA: 0x002CA728 File Offset: 0x002C8928
		internal override string[] AttributeTagNames
		{
			get
			{
				return UnsizedDynamicMenu.attributeTagNames;
			}
		}

		// Token: 0x17003DDD RID: 15837
		// (get) Token: 0x0600E9B5 RID: 59829 RVA: 0x002CA72F File Offset: 0x002C892F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return UnsizedDynamicMenu.attributeNamespaceIds;
			}
		}

		// Token: 0x17003DDE RID: 15838
		// (get) Token: 0x0600E9B6 RID: 59830 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E9B7 RID: 59831 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17003DDF RID: 15839
		// (get) Token: 0x0600E9B8 RID: 59832 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E9B9 RID: 59833 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17003DE0 RID: 15840
		// (get) Token: 0x0600E9BA RID: 59834 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E9BB RID: 59835 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003DE1 RID: 15841
		// (get) Token: 0x0600E9BC RID: 59836 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E9BD RID: 59837 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003DE2 RID: 15842
		// (get) Token: 0x0600E9BE RID: 59838 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600E9BF RID: 59839 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003DE3 RID: 15843
		// (get) Token: 0x0600E9C0 RID: 59840 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E9C1 RID: 59841 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003DE4 RID: 15844
		// (get) Token: 0x0600E9C2 RID: 59842 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E9C3 RID: 59843 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getContent")]
		public StringValue GetContent
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

		// Token: 0x17003DE5 RID: 15845
		// (get) Token: 0x0600E9C4 RID: 59844 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0600E9C5 RID: 59845 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "invalidateContentOnDrop")]
		public BooleanValue InvalidateContentOnDrop
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

		// Token: 0x17003DE6 RID: 15846
		// (get) Token: 0x0600E9C6 RID: 59846 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E9C7 RID: 59847 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17003DE7 RID: 15847
		// (get) Token: 0x0600E9C8 RID: 59848 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E9C9 RID: 59849 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17003DE8 RID: 15848
		// (get) Token: 0x0600E9CA RID: 59850 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E9CB RID: 59851 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17003DE9 RID: 15849
		// (get) Token: 0x0600E9CC RID: 59852 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E9CD RID: 59853 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17003DEA RID: 15850
		// (get) Token: 0x0600E9CE RID: 59854 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E9CF RID: 59855 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17003DEB RID: 15851
		// (get) Token: 0x0600E9D0 RID: 59856 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E9D1 RID: 59857 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17003DEC RID: 15852
		// (get) Token: 0x0600E9D2 RID: 59858 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600E9D3 RID: 59859 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17003DED RID: 15853
		// (get) Token: 0x0600E9D4 RID: 59860 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x0600E9D5 RID: 59861 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17003DEE RID: 15854
		// (get) Token: 0x0600E9D6 RID: 59862 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600E9D7 RID: 59863 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003DEF RID: 15855
		// (get) Token: 0x0600E9D8 RID: 59864 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E9D9 RID: 59865 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17003DF0 RID: 15856
		// (get) Token: 0x0600E9DA RID: 59866 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600E9DB RID: 59867 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17003DF1 RID: 15857
		// (get) Token: 0x0600E9DC RID: 59868 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600E9DD RID: 59869 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17003DF2 RID: 15858
		// (get) Token: 0x0600E9DE RID: 59870 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600E9DF RID: 59871 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
		{
			get
			{
				return (StringValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17003DF3 RID: 15859
		// (get) Token: 0x0600E9E0 RID: 59872 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600E9E1 RID: 59873 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
		{
			get
			{
				return (StringValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17003DF4 RID: 15860
		// (get) Token: 0x0600E9E2 RID: 59874 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600E9E3 RID: 59875 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
		{
			get
			{
				return (StringValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17003DF5 RID: 15861
		// (get) Token: 0x0600E9E4 RID: 59876 RVA: 0x002C99DC File Offset: 0x002C7BDC
		// (set) Token: 0x0600E9E5 RID: 59877 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17003DF6 RID: 15862
		// (get) Token: 0x0600E9E6 RID: 59878 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600E9E7 RID: 59879 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17003DF7 RID: 15863
		// (get) Token: 0x0600E9E8 RID: 59880 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600E9E9 RID: 59881 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x17003DF8 RID: 15864
		// (get) Token: 0x0600E9EA RID: 59882 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600E9EB RID: 59883 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17003DF9 RID: 15865
		// (get) Token: 0x0600E9EC RID: 59884 RVA: 0x002C99EC File Offset: 0x002C7BEC
		// (set) Token: 0x0600E9ED RID: 59885 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17003DFA RID: 15866
		// (get) Token: 0x0600E9EE RID: 59886 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600E9EF RID: 59887 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x17003DFB RID: 15867
		// (get) Token: 0x0600E9F0 RID: 59888 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x0600E9F1 RID: 59889 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x17003DFC RID: 15868
		// (get) Token: 0x0600E9F2 RID: 59890 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600E9F3 RID: 59891 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x0600E9F5 RID: 59893 RVA: 0x002CA758 File Offset: 0x002C8958
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getContent" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "invalidateContentOnDrop" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "image" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imageMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getImage" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "screentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getScreentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "supertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getSupertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "enabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getEnabled" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showImage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowImage" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E9F6 RID: 59894 RVA: 0x002CAA17 File Offset: 0x002C8C17
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnsizedDynamicMenu>(deep);
		}

		// Token: 0x0600E9F7 RID: 59895 RVA: 0x002CAA20 File Offset: 0x002C8C20
		// Note: this type is marked as 'beforefieldinit'.
		static UnsizedDynamicMenu()
		{
			byte[] array = new byte[31];
			UnsizedDynamicMenu.attributeNamespaceIds = array;
		}

		// Token: 0x04006FAD RID: 28589
		private const string tagName = "dynamicMenu";

		// Token: 0x04006FAE RID: 28590
		private const byte tagNsId = 34;

		// Token: 0x04006FAF RID: 28591
		internal const int ElementTypeIdConst = 12581;

		// Token: 0x04006FB0 RID: 28592
		private static string[] attributeTagNames = new string[]
		{
			"description", "getDescription", "id", "idQ", "idMso", "tag", "getContent", "invalidateContentOnDrop", "image", "imageMso",
			"getImage", "screentip", "getScreentip", "supertip", "getSupertip", "enabled", "getEnabled", "label", "getLabel", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage",
			"getShowImage"
		};

		// Token: 0x04006FB1 RID: 28593
		private static byte[] attributeNamespaceIds;
	}
}
