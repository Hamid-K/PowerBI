using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FE3 RID: 12259
	[GeneratedCode("DomGen", "2.0")]
	internal class DocumentProtection : OpenXmlLeafElement
	{
		// Token: 0x170094EE RID: 38126
		// (get) Token: 0x0601AA79 RID: 109177 RVA: 0x003658B4 File Offset: 0x00363AB4
		public override string LocalName
		{
			get
			{
				return "documentProtection";
			}
		}

		// Token: 0x170094EF RID: 38127
		// (get) Token: 0x0601AA7A RID: 109178 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170094F0 RID: 38128
		// (get) Token: 0x0601AA7B RID: 109179 RVA: 0x003658BB File Offset: 0x00363ABB
		internal override int ElementTypeId
		{
			get
			{
				return 11992;
			}
		}

		// Token: 0x0601AA7C RID: 109180 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170094F1 RID: 38129
		// (get) Token: 0x0601AA7D RID: 109181 RVA: 0x003658C2 File Offset: 0x00363AC2
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocumentProtection.attributeTagNames;
			}
		}

		// Token: 0x170094F2 RID: 38130
		// (get) Token: 0x0601AA7E RID: 109182 RVA: 0x003658C9 File Offset: 0x00363AC9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocumentProtection.attributeNamespaceIds;
			}
		}

		// Token: 0x170094F3 RID: 38131
		// (get) Token: 0x0601AA7F RID: 109183 RVA: 0x003658D0 File Offset: 0x00363AD0
		// (set) Token: 0x0601AA80 RID: 109184 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "edit")]
		public EnumValue<DocumentProtectionValues> Edit
		{
			get
			{
				return (EnumValue<DocumentProtectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170094F4 RID: 38132
		// (get) Token: 0x0601AA81 RID: 109185 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601AA82 RID: 109186 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "formatting")]
		public OnOffValue Formatting
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170094F5 RID: 38133
		// (get) Token: 0x0601AA83 RID: 109187 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x0601AA84 RID: 109188 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "enforcement")]
		public OnOffValue Enforcement
		{
			get
			{
				return (OnOffValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170094F6 RID: 38134
		// (get) Token: 0x0601AA85 RID: 109189 RVA: 0x003658DF File Offset: 0x00363ADF
		// (set) Token: 0x0601AA86 RID: 109190 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "cryptProviderType")]
		public EnumValue<CryptProviderValues> CryptographicProviderType
		{
			get
			{
				return (EnumValue<CryptProviderValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170094F7 RID: 38135
		// (get) Token: 0x0601AA87 RID: 109191 RVA: 0x003658EE File Offset: 0x00363AEE
		// (set) Token: 0x0601AA88 RID: 109192 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "cryptAlgorithmClass")]
		public EnumValue<CryptAlgorithmClassValues> CryptographicAlgorithmClass
		{
			get
			{
				return (EnumValue<CryptAlgorithmClassValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170094F8 RID: 38136
		// (get) Token: 0x0601AA89 RID: 109193 RVA: 0x003658FD File Offset: 0x00363AFD
		// (set) Token: 0x0601AA8A RID: 109194 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "cryptAlgorithmType")]
		public EnumValue<CryptAlgorithmValues> CryptographicAlgorithmType
		{
			get
			{
				return (EnumValue<CryptAlgorithmValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170094F9 RID: 38137
		// (get) Token: 0x0601AA8B RID: 109195 RVA: 0x002ED380 File Offset: 0x002EB580
		// (set) Token: 0x0601AA8C RID: 109196 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "cryptAlgorithmSid")]
		public Int32Value CryptographicAlgorithmSid
		{
			get
			{
				return (Int32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170094FA RID: 38138
		// (get) Token: 0x0601AA8D RID: 109197 RVA: 0x0032B268 File Offset: 0x00329468
		// (set) Token: 0x0601AA8E RID: 109198 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(23, "cryptSpinCount")]
		public UInt32Value CryptographicSpinCount
		{
			get
			{
				return (UInt32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170094FB RID: 38139
		// (get) Token: 0x0601AA8F RID: 109199 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601AA90 RID: 109200 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(23, "cryptProvider")]
		public StringValue CryptographicProvider
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

		// Token: 0x170094FC RID: 38140
		// (get) Token: 0x0601AA91 RID: 109201 RVA: 0x00364A9A File Offset: 0x00362C9A
		// (set) Token: 0x0601AA92 RID: 109202 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(23, "algIdExt")]
		public HexBinaryValue AlgorithmIdExtensibility
		{
			get
			{
				return (HexBinaryValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170094FD RID: 38141
		// (get) Token: 0x0601AA93 RID: 109203 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0601AA94 RID: 109204 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(23, "algIdExtSource")]
		public StringValue AlgorithmIdExtensibilitySource
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

		// Token: 0x170094FE RID: 38142
		// (get) Token: 0x0601AA95 RID: 109205 RVA: 0x00313A55 File Offset: 0x00311C55
		// (set) Token: 0x0601AA96 RID: 109206 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(23, "cryptProviderTypeExt")]
		public HexBinaryValue CryptographicProviderTypeExtensibility
		{
			get
			{
				return (HexBinaryValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x170094FF RID: 38143
		// (get) Token: 0x0601AA97 RID: 109207 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0601AA98 RID: 109208 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(23, "cryptProviderTypeExtSource")]
		public StringValue CryptographicProviderTypeExtSource
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

		// Token: 0x17009500 RID: 38144
		// (get) Token: 0x0601AA99 RID: 109209 RVA: 0x003286D3 File Offset: 0x003268D3
		// (set) Token: 0x0601AA9A RID: 109210 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(23, "hash")]
		public Base64BinaryValue Hash
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17009501 RID: 38145
		// (get) Token: 0x0601AA9B RID: 109211 RVA: 0x003286E3 File Offset: 0x003268E3
		// (set) Token: 0x0601AA9C RID: 109212 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(23, "salt")]
		public Base64BinaryValue Salt
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17009502 RID: 38146
		// (get) Token: 0x0601AA9D RID: 109213 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0601AA9E RID: 109214 RVA: 0x002BE241 File Offset: 0x002BC441
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "algorithmName")]
		public StringValue AlgorithmName
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

		// Token: 0x17009503 RID: 38147
		// (get) Token: 0x0601AA9F RID: 109215 RVA: 0x0036590C File Offset: 0x00363B0C
		// (set) Token: 0x0601AAA0 RID: 109216 RVA: 0x002BE25D File Offset: 0x002BC45D
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "hashValue")]
		public Base64BinaryValue HashValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17009504 RID: 38148
		// (get) Token: 0x0601AAA1 RID: 109217 RVA: 0x0036591C File Offset: 0x00363B1C
		// (set) Token: 0x0601AAA2 RID: 109218 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(23, "saltValue")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Base64BinaryValue SaltValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17009505 RID: 38149
		// (get) Token: 0x0601AAA3 RID: 109219 RVA: 0x00300821 File Offset: 0x002FEA21
		// (set) Token: 0x0601AAA4 RID: 109220 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(23, "spinCount")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Int32Value SpinCount
		{
			get
			{
				return (Int32Value)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x0601AAA6 RID: 109222 RVA: 0x0036592C File Offset: 0x00363B2C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "edit" == name)
			{
				return new EnumValue<DocumentProtectionValues>();
			}
			if (23 == namespaceId && "formatting" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "enforcement" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "cryptProviderType" == name)
			{
				return new EnumValue<CryptProviderValues>();
			}
			if (23 == namespaceId && "cryptAlgorithmClass" == name)
			{
				return new EnumValue<CryptAlgorithmClassValues>();
			}
			if (23 == namespaceId && "cryptAlgorithmType" == name)
			{
				return new EnumValue<CryptAlgorithmValues>();
			}
			if (23 == namespaceId && "cryptAlgorithmSid" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "cryptSpinCount" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "cryptProvider" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "algIdExt" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "algIdExtSource" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "cryptProviderTypeExt" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "cryptProviderTypeExtSource" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "hash" == name)
			{
				return new Base64BinaryValue();
			}
			if (23 == namespaceId && "salt" == name)
			{
				return new Base64BinaryValue();
			}
			if (23 == namespaceId && "algorithmName" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "hashValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (23 == namespaceId && "saltValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (23 == namespaceId && "spinCount" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AAA7 RID: 109223 RVA: 0x00365B09 File Offset: 0x00363D09
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocumentProtection>(deep);
		}

		// Token: 0x0400ADDE RID: 44510
		private const string tagName = "documentProtection";

		// Token: 0x0400ADDF RID: 44511
		private const byte tagNsId = 23;

		// Token: 0x0400ADE0 RID: 44512
		internal const int ElementTypeIdConst = 11992;

		// Token: 0x0400ADE1 RID: 44513
		private static string[] attributeTagNames = new string[]
		{
			"edit", "formatting", "enforcement", "cryptProviderType", "cryptAlgorithmClass", "cryptAlgorithmType", "cryptAlgorithmSid", "cryptSpinCount", "cryptProvider", "algIdExt",
			"algIdExtSource", "cryptProviderTypeExt", "cryptProviderTypeExtSource", "hash", "salt", "algorithmName", "hashValue", "saltValue", "spinCount"
		};

		// Token: 0x0400ADE2 RID: 44514
		private static byte[] attributeNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23, 23, 23
		};
	}
}
