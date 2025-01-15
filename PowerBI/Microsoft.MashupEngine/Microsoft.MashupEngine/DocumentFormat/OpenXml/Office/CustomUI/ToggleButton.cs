using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomUI
{
	// Token: 0x0200227C RID: 8828
	[GeneratedCode("DomGen", "2.0")]
	internal class ToggleButton : OpenXmlLeafElement
	{
		// Token: 0x17003E96 RID: 16022
		// (get) Token: 0x0600EB32 RID: 60210 RVA: 0x002C99C0 File Offset: 0x002C7BC0
		public override string LocalName
		{
			get
			{
				return "toggleButton";
			}
		}

		// Token: 0x17003E97 RID: 16023
		// (get) Token: 0x0600EB33 RID: 60211 RVA: 0x002C8749 File Offset: 0x002C6949
		internal override byte NamespaceId
		{
			get
			{
				return 34;
			}
		}

		// Token: 0x17003E98 RID: 16024
		// (get) Token: 0x0600EB34 RID: 60212 RVA: 0x002CBE18 File Offset: 0x002CA018
		internal override int ElementTypeId
		{
			get
			{
				return 12587;
			}
		}

		// Token: 0x0600EB35 RID: 60213 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003E99 RID: 16025
		// (get) Token: 0x0600EB36 RID: 60214 RVA: 0x002CBE1F File Offset: 0x002CA01F
		internal override string[] AttributeTagNames
		{
			get
			{
				return ToggleButton.attributeTagNames;
			}
		}

		// Token: 0x17003E9A RID: 16026
		// (get) Token: 0x0600EB37 RID: 60215 RVA: 0x002CBE26 File Offset: 0x002CA026
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ToggleButton.attributeNamespaceIds;
			}
		}

		// Token: 0x17003E9B RID: 16027
		// (get) Token: 0x0600EB38 RID: 60216 RVA: 0x002CB2F7 File Offset: 0x002C94F7
		// (set) Token: 0x0600EB39 RID: 60217 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003E9C RID: 16028
		// (get) Token: 0x0600EB3A RID: 60218 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600EB3B RID: 60219 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17003E9D RID: 16029
		// (get) Token: 0x0600EB3C RID: 60220 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600EB3D RID: 60221 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getPressed")]
		public StringValue GetPressed
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

		// Token: 0x17003E9E RID: 16030
		// (get) Token: 0x0600EB3E RID: 60222 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600EB3F RID: 60223 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17003E9F RID: 16031
		// (get) Token: 0x0600EB40 RID: 60224 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0600EB41 RID: 60225 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17003EA0 RID: 16032
		// (get) Token: 0x0600EB42 RID: 60226 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600EB43 RID: 60227 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17003EA1 RID: 16033
		// (get) Token: 0x0600EB44 RID: 60228 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600EB45 RID: 60229 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17003EA2 RID: 16034
		// (get) Token: 0x0600EB46 RID: 60230 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600EB47 RID: 60231 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17003EA3 RID: 16035
		// (get) Token: 0x0600EB48 RID: 60232 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600EB49 RID: 60233 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17003EA4 RID: 16036
		// (get) Token: 0x0600EB4A RID: 60234 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600EB4B RID: 60235 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17003EA5 RID: 16037
		// (get) Token: 0x0600EB4C RID: 60236 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600EB4D RID: 60237 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17003EA6 RID: 16038
		// (get) Token: 0x0600EB4E RID: 60238 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600EB4F RID: 60239 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17003EA7 RID: 16039
		// (get) Token: 0x0600EB50 RID: 60240 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600EB51 RID: 60241 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "idQ")]
		public StringValue IdQ
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

		// Token: 0x17003EA8 RID: 16040
		// (get) Token: 0x0600EB52 RID: 60242 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600EB53 RID: 60243 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17003EA9 RID: 16041
		// (get) Token: 0x0600EB54 RID: 60244 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600EB55 RID: 60245 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17003EAA RID: 16042
		// (get) Token: 0x0600EB56 RID: 60246 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600EB57 RID: 60247 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17003EAB RID: 16043
		// (get) Token: 0x0600EB58 RID: 60248 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600EB59 RID: 60249 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17003EAC RID: 16044
		// (get) Token: 0x0600EB5A RID: 60250 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600EB5B RID: 60251 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17003EAD RID: 16045
		// (get) Token: 0x0600EB5C RID: 60252 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600EB5D RID: 60253 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17003EAE RID: 16046
		// (get) Token: 0x0600EB5E RID: 60254 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600EB5F RID: 60255 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17003EAF RID: 16047
		// (get) Token: 0x0600EB60 RID: 60256 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600EB61 RID: 60257 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17003EB0 RID: 16048
		// (get) Token: 0x0600EB62 RID: 60258 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600EB63 RID: 60259 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17003EB1 RID: 16049
		// (get) Token: 0x0600EB64 RID: 60260 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600EB65 RID: 60261 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17003EB2 RID: 16050
		// (get) Token: 0x0600EB66 RID: 60262 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600EB67 RID: 60263 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQ
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

		// Token: 0x17003EB3 RID: 16051
		// (get) Token: 0x0600EB68 RID: 60264 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600EB69 RID: 60265 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQ
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

		// Token: 0x17003EB4 RID: 16052
		// (get) Token: 0x0600EB6A RID: 60266 RVA: 0x002CBE3C File Offset: 0x002CA03C
		// (set) Token: 0x0600EB6B RID: 60267 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17003EB5 RID: 16053
		// (get) Token: 0x0600EB6C RID: 60268 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600EB6D RID: 60269 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17003EB6 RID: 16054
		// (get) Token: 0x0600EB6E RID: 60270 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600EB6F RID: 60271 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17003EB7 RID: 16055
		// (get) Token: 0x0600EB70 RID: 60272 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600EB71 RID: 60273 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17003EB8 RID: 16056
		// (get) Token: 0x0600EB72 RID: 60274 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x0600EB73 RID: 60275 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17003EB9 RID: 16057
		// (get) Token: 0x0600EB74 RID: 60276 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600EB75 RID: 60277 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17003EBA RID: 16058
		// (get) Token: 0x0600EB76 RID: 60278 RVA: 0x002CBE4C File Offset: 0x002CA04C
		// (set) Token: 0x0600EB77 RID: 60279 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
		{
			get
			{
				return (BooleanValue)base.Attributes[31];
			}
			set
			{
				base.Attributes[31] = value;
			}
		}

		// Token: 0x17003EBB RID: 16059
		// (get) Token: 0x0600EB78 RID: 60280 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600EB79 RID: 60281 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[32];
			}
			set
			{
				base.Attributes[32] = value;
			}
		}

		// Token: 0x0600EB7B RID: 60283 RVA: 0x002CBE5C File Offset: 0x002CA05C
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
			if (namespaceId == 0 && "getPressed" == name)
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

		// Token: 0x0600EB7C RID: 60284 RVA: 0x002CC147 File Offset: 0x002CA347
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ToggleButton>(deep);
		}

		// Token: 0x0600EB7D RID: 60285 RVA: 0x002CC150 File Offset: 0x002CA350
		// Note: this type is marked as 'beforefieldinit'.
		static ToggleButton()
		{
			byte[] array = new byte[33];
			ToggleButton.attributeNamespaceIds = array;
		}

		// Token: 0x04006FCB RID: 28619
		private const string tagName = "toggleButton";

		// Token: 0x04006FCC RID: 28620
		private const byte tagNsId = 34;

		// Token: 0x04006FCD RID: 28621
		internal const int ElementTypeIdConst = 12587;

		// Token: 0x04006FCE RID: 28622
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "getPressed", "onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso",
			"getImage", "id", "idQ", "idMso", "tag", "screentip", "getScreentip", "supertip", "getSupertip", "label",
			"getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel",
			"getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04006FCF RID: 28623
		private static byte[] attributeNamespaceIds;
	}
}
