using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022BB RID: 8891
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ButtonRegular : OpenXmlLeafElement
	{
		// Token: 0x170041A8 RID: 16808
		// (get) Token: 0x0600F1DD RID: 61917 RVA: 0x002C8B1A File Offset: 0x002C6D1A
		public override string LocalName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x170041A9 RID: 16809
		// (get) Token: 0x0600F1DE RID: 61918 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170041AA RID: 16810
		// (get) Token: 0x0600F1DF RID: 61919 RVA: 0x002D1AF6 File Offset: 0x002CFCF6
		internal override int ElementTypeId
		{
			get
			{
				return 13036;
			}
		}

		// Token: 0x0600F1E0 RID: 61920 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170041AB RID: 16811
		// (get) Token: 0x0600F1E1 RID: 61921 RVA: 0x002D1AFD File Offset: 0x002CFCFD
		internal override string[] AttributeTagNames
		{
			get
			{
				return ButtonRegular.attributeTagNames;
			}
		}

		// Token: 0x170041AC RID: 16812
		// (get) Token: 0x0600F1E2 RID: 61922 RVA: 0x002D1B04 File Offset: 0x002CFD04
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ButtonRegular.attributeNamespaceIds;
			}
		}

		// Token: 0x170041AD RID: 16813
		// (get) Token: 0x0600F1E3 RID: 61923 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F1E4 RID: 61924 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x170041AE RID: 16814
		// (get) Token: 0x0600F1E5 RID: 61925 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0600F1E6 RID: 61926 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x170041AF RID: 16815
		// (get) Token: 0x0600F1E7 RID: 61927 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F1E8 RID: 61928 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170041B0 RID: 16816
		// (get) Token: 0x0600F1E9 RID: 61929 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F1EA RID: 61930 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170041B1 RID: 16817
		// (get) Token: 0x0600F1EB RID: 61931 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F1EC RID: 61932 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x170041B2 RID: 16818
		// (get) Token: 0x0600F1ED RID: 61933 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F1EE RID: 61934 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x170041B3 RID: 16819
		// (get) Token: 0x0600F1EF RID: 61935 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F1F0 RID: 61936 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x170041B4 RID: 16820
		// (get) Token: 0x0600F1F1 RID: 61937 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F1F2 RID: 61938 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x170041B5 RID: 16821
		// (get) Token: 0x0600F1F3 RID: 61939 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F1F4 RID: 61940 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170041B6 RID: 16822
		// (get) Token: 0x0600F1F5 RID: 61941 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F1F6 RID: 61942 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170041B7 RID: 16823
		// (get) Token: 0x0600F1F7 RID: 61943 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F1F8 RID: 61944 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x170041B8 RID: 16824
		// (get) Token: 0x0600F1F9 RID: 61945 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F1FA RID: 61946 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x170041B9 RID: 16825
		// (get) Token: 0x0600F1FB RID: 61947 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F1FC RID: 61948 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x170041BA RID: 16826
		// (get) Token: 0x0600F1FD RID: 61949 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F1FE RID: 61950 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x170041BB RID: 16827
		// (get) Token: 0x0600F1FF RID: 61951 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F200 RID: 61952 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x170041BC RID: 16828
		// (get) Token: 0x0600F201 RID: 61953 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F202 RID: 61954 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x170041BD RID: 16829
		// (get) Token: 0x0600F203 RID: 61955 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F204 RID: 61956 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x170041BE RID: 16830
		// (get) Token: 0x0600F205 RID: 61957 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F206 RID: 61958 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x170041BF RID: 16831
		// (get) Token: 0x0600F207 RID: 61959 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F208 RID: 61960 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x170041C0 RID: 16832
		// (get) Token: 0x0600F209 RID: 61961 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F20A RID: 61962 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x170041C1 RID: 16833
		// (get) Token: 0x0600F20B RID: 61963 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F20C RID: 61964 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x170041C2 RID: 16834
		// (get) Token: 0x0600F20D RID: 61965 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F20E RID: 61966 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x170041C3 RID: 16835
		// (get) Token: 0x0600F20F RID: 61967 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600F210 RID: 61968 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x170041C4 RID: 16836
		// (get) Token: 0x0600F211 RID: 61969 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F212 RID: 61970 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x170041C5 RID: 16837
		// (get) Token: 0x0600F213 RID: 61971 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F214 RID: 61972 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x170041C6 RID: 16838
		// (get) Token: 0x0600F215 RID: 61973 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F216 RID: 61974 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x170041C7 RID: 16839
		// (get) Token: 0x0600F217 RID: 61975 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600F218 RID: 61976 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x170041C8 RID: 16840
		// (get) Token: 0x0600F219 RID: 61977 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F21A RID: 61978 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x170041C9 RID: 16841
		// (get) Token: 0x0600F21B RID: 61979 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600F21C RID: 61980 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x170041CA RID: 16842
		// (get) Token: 0x0600F21D RID: 61981 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600F21E RID: 61982 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x0600F220 RID: 61984 RVA: 0x002D1B0C File Offset: 0x002CFD0C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "onAction" == name)
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
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
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
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
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

		// Token: 0x0600F221 RID: 61985 RVA: 0x002D1DB5 File Offset: 0x002CFFB5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ButtonRegular>(deep);
		}

		// Token: 0x0600F222 RID: 61986 RVA: 0x002D1DC0 File Offset: 0x002CFFC0
		// Note: this type is marked as 'beforefieldinit'.
		static ButtonRegular()
		{
			byte[] array = new byte[30];
			ButtonRegular.attributeNamespaceIds = array;
		}

		// Token: 0x040070D4 RID: 28884
		private const string tagName = "button";

		// Token: 0x040070D5 RID: 28885
		private const byte tagNsId = 57;

		// Token: 0x040070D6 RID: 28886
		internal const int ElementTypeIdConst = 13036;

		// Token: 0x040070D7 RID: 28887
		private static string[] attributeTagNames = new string[]
		{
			"onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage", "id", "idQ",
			"tag", "idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x040070D8 RID: 28888
		private static byte[] attributeNamespaceIds;
	}
}
