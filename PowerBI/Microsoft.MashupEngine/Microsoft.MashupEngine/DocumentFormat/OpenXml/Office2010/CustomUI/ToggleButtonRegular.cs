using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022BE RID: 8894
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ToggleButtonRegular : OpenXmlLeafElement
	{
		// Token: 0x1700421E RID: 16926
		// (get) Token: 0x0600F2CD RID: 62157 RVA: 0x002C99C0 File Offset: 0x002C7BC0
		public override string LocalName
		{
			get
			{
				return "toggleButton";
			}
		}

		// Token: 0x1700421F RID: 16927
		// (get) Token: 0x0600F2CE RID: 62158 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004220 RID: 16928
		// (get) Token: 0x0600F2CF RID: 62159 RVA: 0x002D28E1 File Offset: 0x002D0AE1
		internal override int ElementTypeId
		{
			get
			{
				return 13039;
			}
		}

		// Token: 0x0600F2D0 RID: 62160 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004221 RID: 16929
		// (get) Token: 0x0600F2D1 RID: 62161 RVA: 0x002D28E8 File Offset: 0x002D0AE8
		internal override string[] AttributeTagNames
		{
			get
			{
				return ToggleButtonRegular.attributeTagNames;
			}
		}

		// Token: 0x17004222 RID: 16930
		// (get) Token: 0x0600F2D2 RID: 62162 RVA: 0x002D28EF File Offset: 0x002D0AEF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ToggleButtonRegular.attributeNamespaceIds;
			}
		}

		// Token: 0x17004223 RID: 16931
		// (get) Token: 0x0600F2D3 RID: 62163 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F2D4 RID: 62164 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "getPressed")]
		public StringValue GetPressed
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

		// Token: 0x17004224 RID: 16932
		// (get) Token: 0x0600F2D5 RID: 62165 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F2D6 RID: 62166 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17004225 RID: 16933
		// (get) Token: 0x0600F2D7 RID: 62167 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600F2D8 RID: 62168 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x17004226 RID: 16934
		// (get) Token: 0x0600F2D9 RID: 62169 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F2DA RID: 62170 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004227 RID: 16935
		// (get) Token: 0x0600F2DB RID: 62171 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F2DC RID: 62172 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17004228 RID: 16936
		// (get) Token: 0x0600F2DD RID: 62173 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F2DE RID: 62174 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17004229 RID: 16937
		// (get) Token: 0x0600F2DF RID: 62175 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F2E0 RID: 62176 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x1700422A RID: 16938
		// (get) Token: 0x0600F2E1 RID: 62177 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F2E2 RID: 62178 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x1700422B RID: 16939
		// (get) Token: 0x0600F2E3 RID: 62179 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F2E4 RID: 62180 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x1700422C RID: 16940
		// (get) Token: 0x0600F2E5 RID: 62181 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F2E6 RID: 62182 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x1700422D RID: 16941
		// (get) Token: 0x0600F2E7 RID: 62183 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F2E8 RID: 62184 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700422E RID: 16942
		// (get) Token: 0x0600F2E9 RID: 62185 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F2EA RID: 62186 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x1700422F RID: 16943
		// (get) Token: 0x0600F2EB RID: 62187 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F2EC RID: 62188 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17004230 RID: 16944
		// (get) Token: 0x0600F2ED RID: 62189 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F2EE RID: 62190 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004231 RID: 16945
		// (get) Token: 0x0600F2EF RID: 62191 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F2F0 RID: 62192 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17004232 RID: 16946
		// (get) Token: 0x0600F2F1 RID: 62193 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F2F2 RID: 62194 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17004233 RID: 16947
		// (get) Token: 0x0600F2F3 RID: 62195 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F2F4 RID: 62196 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004234 RID: 16948
		// (get) Token: 0x0600F2F5 RID: 62197 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F2F6 RID: 62198 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x17004235 RID: 16949
		// (get) Token: 0x0600F2F7 RID: 62199 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F2F8 RID: 62200 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x17004236 RID: 16950
		// (get) Token: 0x0600F2F9 RID: 62201 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F2FA RID: 62202 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x17004237 RID: 16951
		// (get) Token: 0x0600F2FB RID: 62203 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F2FC RID: 62204 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x17004238 RID: 16952
		// (get) Token: 0x0600F2FD RID: 62205 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F2FE RID: 62206 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x17004239 RID: 16953
		// (get) Token: 0x0600F2FF RID: 62207 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F300 RID: 62208 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x1700423A RID: 16954
		// (get) Token: 0x0600F301 RID: 62209 RVA: 0x002C99DC File Offset: 0x002C7BDC
		// (set) Token: 0x0600F302 RID: 62210 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x1700423B RID: 16955
		// (get) Token: 0x0600F303 RID: 62211 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F304 RID: 62212 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x1700423C RID: 16956
		// (get) Token: 0x0600F305 RID: 62213 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F306 RID: 62214 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x1700423D RID: 16957
		// (get) Token: 0x0600F307 RID: 62215 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600F308 RID: 62216 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x1700423E RID: 16958
		// (get) Token: 0x0600F309 RID: 62217 RVA: 0x002C99EC File Offset: 0x002C7BEC
		// (set) Token: 0x0600F30A RID: 62218 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x1700423F RID: 16959
		// (get) Token: 0x0600F30B RID: 62219 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600F30C RID: 62220 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x17004240 RID: 16960
		// (get) Token: 0x0600F30D RID: 62221 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x0600F30E RID: 62222 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x17004241 RID: 16961
		// (get) Token: 0x0600F30F RID: 62223 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600F310 RID: 62224 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x0600F312 RID: 62226 RVA: 0x002D28F8 File Offset: 0x002D0AF8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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

		// Token: 0x0600F313 RID: 62227 RVA: 0x002D2BB7 File Offset: 0x002D0DB7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ToggleButtonRegular>(deep);
		}

		// Token: 0x0600F314 RID: 62228 RVA: 0x002D2BC0 File Offset: 0x002D0DC0
		// Note: this type is marked as 'beforefieldinit'.
		static ToggleButtonRegular()
		{
			byte[] array = new byte[31];
			ToggleButtonRegular.attributeNamespaceIds = array;
		}

		// Token: 0x040070E3 RID: 28899
		private const string tagName = "toggleButton";

		// Token: 0x040070E4 RID: 28900
		private const byte tagNsId = 57;

		// Token: 0x040070E5 RID: 28901
		internal const int ElementTypeIdConst = 13039;

		// Token: 0x040070E6 RID: 28902
		private static string[] attributeTagNames = new string[]
		{
			"getPressed", "onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage", "id",
			"idQ", "tag", "idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso",
			"insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage",
			"getShowImage"
		};

		// Token: 0x040070E7 RID: 28903
		private static byte[] attributeNamespaceIds;
	}
}
