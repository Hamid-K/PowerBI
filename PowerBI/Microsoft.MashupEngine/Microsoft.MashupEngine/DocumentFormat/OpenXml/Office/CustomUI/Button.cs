using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200227B RID: 8827
	[GeneratedCode("DomGen", "2.0")]
	internal class Button : OpenXmlLeafElement
	{
		// Token: 0x17003E71 RID: 15985
		// (get) Token: 0x0600EAE8 RID: 60136 RVA: 0x002C8B1A File Offset: 0x002C6D1A
		public override string LocalName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x17003E72 RID: 15986
		// (get) Token: 0x0600EAE9 RID: 60137 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003E73 RID: 15987
		// (get) Token: 0x0600EAEA RID: 60138 RVA: 0x002CB9C4 File Offset: 0x002C9BC4
		internal override int ElementTypeId
		{
			get
			{
				return 12586;
			}
		}

		// Token: 0x0600EAEB RID: 60139 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003E74 RID: 15988
		// (get) Token: 0x0600EAEC RID: 60140 RVA: 0x002CB9CB File Offset: 0x002C9BCB
		internal override string[] AttributeTagNames
		{
			get
			{
				return Button.attributeTagNames;
			}
		}

		// Token: 0x17003E75 RID: 15989
		// (get) Token: 0x0600EAED RID: 60141 RVA: 0x002CB9D2 File Offset: 0x002C9BD2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Button.attributeNamespaceIds;
			}
		}

		// Token: 0x17003E76 RID: 15990
		// (get) Token: 0x0600EAEE RID: 60142 RVA: 0x002CB2F7 File Offset: 0x002C94F7
		// (set) Token: 0x0600EAEF RID: 60143 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "size")]
		public EnumValue<SizeValues> Size
		{
			get
			{
				return (EnumValue<SizeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003E77 RID: 15991
		// (get) Token: 0x0600EAF0 RID: 60144 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EAF1 RID: 60145 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getSize")]
		public StringValue GetSize
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

		// Token: 0x17003E78 RID: 15992
		// (get) Token: 0x0600EAF2 RID: 60146 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EAF3 RID: 60147 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17003E79 RID: 15993
		// (get) Token: 0x0600EAF4 RID: 60148 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0600EAF5 RID: 60149 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17003E7A RID: 15994
		// (get) Token: 0x0600EAF6 RID: 60150 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600EAF7 RID: 60151 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003E7B RID: 15995
		// (get) Token: 0x0600EAF8 RID: 60152 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EAF9 RID: 60153 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17003E7C RID: 15996
		// (get) Token: 0x0600EAFA RID: 60154 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EAFB RID: 60155 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17003E7D RID: 15997
		// (get) Token: 0x0600EAFC RID: 60156 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EAFD RID: 60157 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17003E7E RID: 15998
		// (get) Token: 0x0600EAFE RID: 60158 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EAFF RID: 60159 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17003E7F RID: 15999
		// (get) Token: 0x0600EB00 RID: 60160 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EB01 RID: 60161 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17003E80 RID: 16000
		// (get) Token: 0x0600EB02 RID: 60162 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EB03 RID: 60163 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003E81 RID: 16001
		// (get) Token: 0x0600EB04 RID: 60164 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EB05 RID: 60165 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003E82 RID: 16002
		// (get) Token: 0x0600EB06 RID: 60166 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EB07 RID: 60167 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003E83 RID: 16003
		// (get) Token: 0x0600EB08 RID: 60168 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EB09 RID: 60169 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003E84 RID: 16004
		// (get) Token: 0x0600EB0A RID: 60170 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600EB0B RID: 60171 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17003E85 RID: 16005
		// (get) Token: 0x0600EB0C RID: 60172 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EB0D RID: 60173 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17003E86 RID: 16006
		// (get) Token: 0x0600EB0E RID: 60174 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600EB0F RID: 60175 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17003E87 RID: 16007
		// (get) Token: 0x0600EB10 RID: 60176 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EB11 RID: 60177 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17003E88 RID: 16008
		// (get) Token: 0x0600EB12 RID: 60178 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EB13 RID: 60179 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17003E89 RID: 16009
		// (get) Token: 0x0600EB14 RID: 60180 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EB15 RID: 60181 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17003E8A RID: 16010
		// (get) Token: 0x0600EB16 RID: 60182 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EB17 RID: 60183 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003E8B RID: 16011
		// (get) Token: 0x0600EB18 RID: 60184 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600EB19 RID: 60185 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003E8C RID: 16012
		// (get) Token: 0x0600EB1A RID: 60186 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600EB1B RID: 60187 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003E8D RID: 16013
		// (get) Token: 0x0600EB1C RID: 60188 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600EB1D RID: 60189 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003E8E RID: 16014
		// (get) Token: 0x0600EB1E RID: 60190 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x0600EB1F RID: 60191 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17003E8F RID: 16015
		// (get) Token: 0x0600EB20 RID: 60192 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600EB21 RID: 60193 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003E90 RID: 16016
		// (get) Token: 0x0600EB22 RID: 60194 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600EB23 RID: 60195 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003E91 RID: 16017
		// (get) Token: 0x0600EB24 RID: 60196 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600EB25 RID: 60197 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003E92 RID: 16018
		// (get) Token: 0x0600EB26 RID: 60198 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600EB27 RID: 60199 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17003E93 RID: 16019
		// (get) Token: 0x0600EB28 RID: 60200 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600EB29 RID: 60201 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17003E94 RID: 16020
		// (get) Token: 0x0600EB2A RID: 60202 RVA: 0x002CB9E8 File Offset: 0x002C9BE8
		// (set) Token: 0x0600EB2B RID: 60203 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x17003E95 RID: 16021
		// (get) Token: 0x0600EB2C RID: 60204 RVA: 0x002C2A6B File Offset: 0x002C0C6B
		// (set) Token: 0x0600EB2D RID: 60205 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[31];
			}
			set
			{
				base.Attributes[31] = value;
			}
		}

		// Token: 0x0600EB2F RID: 60207 RVA: 0x002CB9F8 File Offset: 0x002C9BF8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "size" == name)
			{
				return new EnumValue<SizeValues>();
			}
			if (namespaceId == 0 && "getSize" == name)
			{
				return new StringValue();
			}
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
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
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

		// Token: 0x0600EB30 RID: 60208 RVA: 0x002CBCCD File Offset: 0x002C9ECD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Button>(deep);
		}

		// Token: 0x0600EB31 RID: 60209 RVA: 0x002CBCD8 File Offset: 0x002C9ED8
		// Note: this type is marked as 'beforefieldinit'.
		static Button()
		{
			byte[] array = new byte[32];
			Button.attributeNamespaceIds = array;
		}

		// Token: 0x04006FC6 RID: 28614
		private const string tagName = "button";

		// Token: 0x04006FC7 RID: 28615
		private const byte tagNsId = 34;

		// Token: 0x04006FC8 RID: 28616
		internal const int ElementTypeIdConst = 12586;

		// Token: 0x04006FC9 RID: 28617
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage",
			"id", "idQ", "idMso", "tag", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel",
			"insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel",
			"showImage", "getShowImage"
		};

		// Token: 0x04006FCA RID: 28618
		private static byte[] attributeNamespaceIds;
	}
}
