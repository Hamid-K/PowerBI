using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022BA RID: 8890
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ControlCloneRegular : OpenXmlLeafElement
	{
		// Token: 0x17004189 RID: 16777
		// (get) Token: 0x0600F19F RID: 61855 RVA: 0x002AD773 File Offset: 0x002AB973
		public override string LocalName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x1700418A RID: 16778
		// (get) Token: 0x0600F1A0 RID: 61856 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700418B RID: 16779
		// (get) Token: 0x0600F1A1 RID: 61857 RVA: 0x002D176D File Offset: 0x002CF96D
		internal override int ElementTypeId
		{
			get
			{
				return 13035;
			}
		}

		// Token: 0x0600F1A2 RID: 61858 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700418C RID: 16780
		// (get) Token: 0x0600F1A3 RID: 61859 RVA: 0x002D177F File Offset: 0x002CF97F
		internal override string[] AttributeTagNames
		{
			get
			{
				return ControlCloneRegular.attributeTagNames;
			}
		}

		// Token: 0x1700418D RID: 16781
		// (get) Token: 0x0600F1A4 RID: 61860 RVA: 0x002D1786 File Offset: 0x002CF986
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ControlCloneRegular.attributeNamespaceIds;
			}
		}

		// Token: 0x1700418E RID: 16782
		// (get) Token: 0x0600F1A5 RID: 61861 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F1A6 RID: 61862 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700418F RID: 16783
		// (get) Token: 0x0600F1A7 RID: 61863 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F1A8 RID: 61864 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17004190 RID: 16784
		// (get) Token: 0x0600F1A9 RID: 61865 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F1AA RID: 61866 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17004191 RID: 16785
		// (get) Token: 0x0600F1AB RID: 61867 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F1AC RID: 61868 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004192 RID: 16786
		// (get) Token: 0x0600F1AD RID: 61869 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F1AE RID: 61870 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17004193 RID: 16787
		// (get) Token: 0x0600F1AF RID: 61871 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F1B0 RID: 61872 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17004194 RID: 16788
		// (get) Token: 0x0600F1B1 RID: 61873 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F1B2 RID: 61874 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004195 RID: 16789
		// (get) Token: 0x0600F1B3 RID: 61875 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F1B4 RID: 61876 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17004196 RID: 16790
		// (get) Token: 0x0600F1B5 RID: 61877 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F1B6 RID: 61878 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17004197 RID: 16791
		// (get) Token: 0x0600F1B7 RID: 61879 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F1B8 RID: 61880 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004198 RID: 16792
		// (get) Token: 0x0600F1B9 RID: 61881 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600F1BA RID: 61882 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17004199 RID: 16793
		// (get) Token: 0x0600F1BB RID: 61883 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F1BC RID: 61884 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x1700419A RID: 16794
		// (get) Token: 0x0600F1BD RID: 61885 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F1BE RID: 61886 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x1700419B RID: 16795
		// (get) Token: 0x0600F1BF RID: 61887 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F1C0 RID: 61888 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x1700419C RID: 16796
		// (get) Token: 0x0600F1C1 RID: 61889 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F1C2 RID: 61890 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x1700419D RID: 16797
		// (get) Token: 0x0600F1C3 RID: 61891 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F1C4 RID: 61892 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x1700419E RID: 16798
		// (get) Token: 0x0600F1C5 RID: 61893 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F1C6 RID: 61894 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x1700419F RID: 16799
		// (get) Token: 0x0600F1C7 RID: 61895 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F1C8 RID: 61896 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x170041A0 RID: 16800
		// (get) Token: 0x0600F1C9 RID: 61897 RVA: 0x002C8772 File Offset: 0x002C6972
		// (set) Token: 0x0600F1CA RID: 61898 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x170041A1 RID: 16801
		// (get) Token: 0x0600F1CB RID: 61899 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F1CC RID: 61900 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x170041A2 RID: 16802
		// (get) Token: 0x0600F1CD RID: 61901 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F1CE RID: 61902 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x170041A3 RID: 16803
		// (get) Token: 0x0600F1CF RID: 61903 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F1D0 RID: 61904 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x170041A4 RID: 16804
		// (get) Token: 0x0600F1D1 RID: 61905 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600F1D2 RID: 61906 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x170041A5 RID: 16805
		// (get) Token: 0x0600F1D3 RID: 61907 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F1D4 RID: 61908 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x170041A6 RID: 16806
		// (get) Token: 0x0600F1D5 RID: 61909 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x0600F1D6 RID: 61910 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x170041A7 RID: 16807
		// (get) Token: 0x0600F1D7 RID: 61911 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F1D8 RID: 61912 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600F1DA RID: 61914 RVA: 0x002D1790 File Offset: 0x002CF990
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
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

		// Token: 0x0600F1DB RID: 61915 RVA: 0x002D19E1 File Offset: 0x002CFBE1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ControlCloneRegular>(deep);
		}

		// Token: 0x0600F1DC RID: 61916 RVA: 0x002D19EC File Offset: 0x002CFBEC
		// Note: this type is marked as 'beforefieldinit'.
		static ControlCloneRegular()
		{
			byte[] array = new byte[26];
			ControlCloneRegular.attributeNamespaceIds = array;
		}

		// Token: 0x040070CF RID: 28879
		private const string tagName = "control";

		// Token: 0x040070D0 RID: 28880
		private const byte tagNsId = 57;

		// Token: 0x040070D1 RID: 28881
		internal const int ElementTypeIdConst = 13035;

		// Token: 0x040070D2 RID: 28882
		private static string[] attributeTagNames = new string[]
		{
			"idQ", "tag", "idMso", "image", "imageMso", "getImage", "screentip", "getScreentip", "supertip", "getSupertip",
			"enabled", "getEnabled", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible",
			"keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x040070D3 RID: 28883
		private static byte[] attributeNamespaceIds;
	}
}
