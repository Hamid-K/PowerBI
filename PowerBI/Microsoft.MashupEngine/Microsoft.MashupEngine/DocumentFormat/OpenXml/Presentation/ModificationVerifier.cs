using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AB9 RID: 10937
	[GeneratedCode("DomGen", "2.0")]
	internal class ModificationVerifier : OpenXmlLeafElement
	{
		// Token: 0x170074EF RID: 29935
		// (get) Token: 0x0601645D RID: 91229 RVA: 0x0032867B File Offset: 0x0032687B
		public override string LocalName
		{
			get
			{
				return "modifyVerifier";
			}
		}

		// Token: 0x170074F0 RID: 29936
		// (get) Token: 0x0601645E RID: 91230 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074F1 RID: 29937
		// (get) Token: 0x0601645F RID: 91231 RVA: 0x00328682 File Offset: 0x00326882
		internal override int ElementTypeId
		{
			get
			{
				return 12353;
			}
		}

		// Token: 0x06016460 RID: 91232 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170074F2 RID: 29938
		// (get) Token: 0x06016461 RID: 91233 RVA: 0x00328689 File Offset: 0x00326889
		internal override string[] AttributeTagNames
		{
			get
			{
				return ModificationVerifier.attributeTagNames;
			}
		}

		// Token: 0x170074F3 RID: 29939
		// (get) Token: 0x06016462 RID: 91234 RVA: 0x00328690 File Offset: 0x00326890
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ModificationVerifier.attributeNamespaceIds;
			}
		}

		// Token: 0x170074F4 RID: 29940
		// (get) Token: 0x06016463 RID: 91235 RVA: 0x00328697 File Offset: 0x00326897
		// (set) Token: 0x06016464 RID: 91236 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "cryptProviderType")]
		public EnumValue<CryptProviderValues> CryptographicProviderType
		{
			get
			{
				return (EnumValue<CryptProviderValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170074F5 RID: 29941
		// (get) Token: 0x06016465 RID: 91237 RVA: 0x003286A6 File Offset: 0x003268A6
		// (set) Token: 0x06016466 RID: 91238 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cryptAlgorithmClass")]
		public EnumValue<CryptAlgorithmClassValues> CryptographicAlgorithmClass
		{
			get
			{
				return (EnumValue<CryptAlgorithmClassValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170074F6 RID: 29942
		// (get) Token: 0x06016467 RID: 91239 RVA: 0x003286B5 File Offset: 0x003268B5
		// (set) Token: 0x06016468 RID: 91240 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "cryptAlgorithmType")]
		public EnumValue<CryptAlgorithmValues> CryptographicAlgorithmType
		{
			get
			{
				return (EnumValue<CryptAlgorithmValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170074F7 RID: 29943
		// (get) Token: 0x06016469 RID: 91241 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601646A RID: 91242 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "cryptAlgorithmSid")]
		public UInt32Value CryptographicAlgorithmSid
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

		// Token: 0x170074F8 RID: 29944
		// (get) Token: 0x0601646B RID: 91243 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x0601646C RID: 91244 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "spinCount")]
		public UInt32Value SpinCount
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170074F9 RID: 29945
		// (get) Token: 0x0601646D RID: 91245 RVA: 0x003286C4 File Offset: 0x003268C4
		// (set) Token: 0x0601646E RID: 91246 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "saltData")]
		public Base64BinaryValue SaltData
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170074FA RID: 29946
		// (get) Token: 0x0601646F RID: 91247 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06016470 RID: 91248 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "hashData")]
		public StringValue HashData
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

		// Token: 0x170074FB RID: 29947
		// (get) Token: 0x06016471 RID: 91249 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06016472 RID: 91250 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "cryptProvider")]
		public StringValue CryptographicProvider
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

		// Token: 0x170074FC RID: 29948
		// (get) Token: 0x06016473 RID: 91251 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x06016474 RID: 91252 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "algIdExt")]
		public UInt32Value ExtendedCryptographicAlgorithm
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170074FD RID: 29949
		// (get) Token: 0x06016475 RID: 91253 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06016476 RID: 91254 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "algIdExtSource")]
		public StringValue ExtendedCryptographicAlgorithmSource
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

		// Token: 0x170074FE RID: 29950
		// (get) Token: 0x06016477 RID: 91255 RVA: 0x0031EC49 File Offset: 0x0031CE49
		// (set) Token: 0x06016478 RID: 91256 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "cryptProviderTypeExt")]
		public UInt32Value CryptographicProviderTypeExtensibility
		{
			get
			{
				return (UInt32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170074FF RID: 29951
		// (get) Token: 0x06016479 RID: 91257 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0601647A RID: 91258 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "cryptProviderTypeExtSource")]
		public StringValue CryptographicProviderTypeExtensibilitySource
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

		// Token: 0x17007500 RID: 29952
		// (get) Token: 0x0601647B RID: 91259 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0601647C RID: 91260 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "algorithmName")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StringValue AlgorithmName
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

		// Token: 0x17007501 RID: 29953
		// (get) Token: 0x0601647D RID: 91261 RVA: 0x003286D3 File Offset: 0x003268D3
		// (set) Token: 0x0601647E RID: 91262 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "hashValue")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public Base64BinaryValue HashValue
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

		// Token: 0x17007502 RID: 29954
		// (get) Token: 0x0601647F RID: 91263 RVA: 0x003286E3 File Offset: 0x003268E3
		// (set) Token: 0x06016480 RID: 91264 RVA: 0x002BE225 File Offset: 0x002BC425
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(0, "saltValue")]
		public Base64BinaryValue SaltValue
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

		// Token: 0x17007503 RID: 29955
		// (get) Token: 0x06016481 RID: 91265 RVA: 0x002E6F0A File Offset: 0x002E510A
		// (set) Token: 0x06016482 RID: 91266 RVA: 0x002BE241 File Offset: 0x002BC441
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(0, "spinValue")]
		public UInt32Value SpinValue
		{
			get
			{
				return (UInt32Value)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x06016484 RID: 91268 RVA: 0x003286F4 File Offset: 0x003268F4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "cryptProviderType" == name)
			{
				return new EnumValue<CryptProviderValues>();
			}
			if (namespaceId == 0 && "cryptAlgorithmClass" == name)
			{
				return new EnumValue<CryptAlgorithmClassValues>();
			}
			if (namespaceId == 0 && "cryptAlgorithmType" == name)
			{
				return new EnumValue<CryptAlgorithmValues>();
			}
			if (namespaceId == 0 && "cryptAlgorithmSid" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "spinCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "saltData" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "hashData" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cryptProvider" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "algIdExt" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "algIdExtSource" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cryptProviderTypeExt" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "cryptProviderTypeExtSource" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "algorithmName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hashValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "saltValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "spinValue" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016485 RID: 91269 RVA: 0x00328869 File Offset: 0x00326A69
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ModificationVerifier>(deep);
		}

		// Token: 0x06016486 RID: 91270 RVA: 0x00328874 File Offset: 0x00326A74
		// Note: this type is marked as 'beforefieldinit'.
		static ModificationVerifier()
		{
			byte[] array = new byte[16];
			ModificationVerifier.attributeNamespaceIds = array;
		}

		// Token: 0x040096FB RID: 38651
		private const string tagName = "modifyVerifier";

		// Token: 0x040096FC RID: 38652
		private const byte tagNsId = 24;

		// Token: 0x040096FD RID: 38653
		internal const int ElementTypeIdConst = 12353;

		// Token: 0x040096FE RID: 38654
		private static string[] attributeTagNames = new string[]
		{
			"cryptProviderType", "cryptAlgorithmClass", "cryptAlgorithmType", "cryptAlgorithmSid", "spinCount", "saltData", "hashData", "cryptProvider", "algIdExt", "algIdExtSource",
			"cryptProviderTypeExt", "cryptProviderTypeExtSource", "algorithmName", "hashValue", "saltValue", "spinValue"
		};

		// Token: 0x040096FF RID: 38655
		private static byte[] attributeNamespaceIds;
	}
}
