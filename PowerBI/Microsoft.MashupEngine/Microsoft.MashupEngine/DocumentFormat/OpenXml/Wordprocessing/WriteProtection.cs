using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FD9 RID: 12249
	[GeneratedCode("DomGen", "2.0")]
	internal class WriteProtection : OpenXmlLeafElement
	{
		// Token: 0x17009478 RID: 38008
		// (get) Token: 0x0601A98B RID: 108939 RVA: 0x00364A51 File Offset: 0x00362C51
		public override string LocalName
		{
			get
			{
				return "writeProtection";
			}
		}

		// Token: 0x17009479 RID: 38009
		// (get) Token: 0x0601A98C RID: 108940 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700947A RID: 38010
		// (get) Token: 0x0601A98D RID: 108941 RVA: 0x00364A58 File Offset: 0x00362C58
		internal override int ElementTypeId
		{
			get
			{
				return 11958;
			}
		}

		// Token: 0x0601A98E RID: 108942 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700947B RID: 38011
		// (get) Token: 0x0601A98F RID: 108943 RVA: 0x00364A5F File Offset: 0x00362C5F
		internal override string[] AttributeTagNames
		{
			get
			{
				return WriteProtection.attributeTagNames;
			}
		}

		// Token: 0x1700947C RID: 38012
		// (get) Token: 0x0601A990 RID: 108944 RVA: 0x00364A66 File Offset: 0x00362C66
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WriteProtection.attributeNamespaceIds;
			}
		}

		// Token: 0x1700947D RID: 38013
		// (get) Token: 0x0601A991 RID: 108945 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x0601A992 RID: 108946 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "recommended")]
		public OnOffValue Recommended
		{
			get
			{
				return (OnOffValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700947E RID: 38014
		// (get) Token: 0x0601A993 RID: 108947 RVA: 0x00364A6D File Offset: 0x00362C6D
		// (set) Token: 0x0601A994 RID: 108948 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "cryptProviderType")]
		public EnumValue<CryptProviderValues> CryptographicProviderType
		{
			get
			{
				return (EnumValue<CryptProviderValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700947F RID: 38015
		// (get) Token: 0x0601A995 RID: 108949 RVA: 0x00364A7C File Offset: 0x00362C7C
		// (set) Token: 0x0601A996 RID: 108950 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "cryptAlgorithmClass")]
		public EnumValue<CryptAlgorithmClassValues> CryptographicAlgorithmClass
		{
			get
			{
				return (EnumValue<CryptAlgorithmClassValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17009480 RID: 38016
		// (get) Token: 0x0601A997 RID: 108951 RVA: 0x00364A8B File Offset: 0x00362C8B
		// (set) Token: 0x0601A998 RID: 108952 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "cryptAlgorithmType")]
		public EnumValue<CryptAlgorithmValues> CryptographicAlgorithmType
		{
			get
			{
				return (EnumValue<CryptAlgorithmValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17009481 RID: 38017
		// (get) Token: 0x0601A999 RID: 108953 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x0601A99A RID: 108954 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "cryptAlgorithmSid")]
		public Int32Value CryptographicAlgorithmSid
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17009482 RID: 38018
		// (get) Token: 0x0601A99B RID: 108955 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x0601A99C RID: 108956 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "cryptSpinCount")]
		public UInt32Value CryptographicSpinCount
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17009483 RID: 38019
		// (get) Token: 0x0601A99D RID: 108957 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0601A99E RID: 108958 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "cryptProvider")]
		public StringValue CryptographicProvider
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

		// Token: 0x17009484 RID: 38020
		// (get) Token: 0x0601A99F RID: 108959 RVA: 0x0032EEF7 File Offset: 0x0032D0F7
		// (set) Token: 0x0601A9A0 RID: 108960 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(23, "algIdExt")]
		public HexBinaryValue AlgorithmIdExtensibility
		{
			get
			{
				return (HexBinaryValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17009485 RID: 38021
		// (get) Token: 0x0601A9A1 RID: 108961 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601A9A2 RID: 108962 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(23, "algIdExtSource")]
		public StringValue AlgorithmIdExtensibilitySource
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

		// Token: 0x17009486 RID: 38022
		// (get) Token: 0x0601A9A3 RID: 108963 RVA: 0x00364A9A File Offset: 0x00362C9A
		// (set) Token: 0x0601A9A4 RID: 108964 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(23, "cryptProviderTypeExt")]
		public HexBinaryValue CryptographicProviderTypeExtensibility
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

		// Token: 0x17009487 RID: 38023
		// (get) Token: 0x0601A9A5 RID: 108965 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0601A9A6 RID: 108966 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(23, "cryptProviderTypeExtSource")]
		public StringValue CryptographicProviderTypeExtSource
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

		// Token: 0x17009488 RID: 38024
		// (get) Token: 0x0601A9A7 RID: 108967 RVA: 0x0033E649 File Offset: 0x0033C849
		// (set) Token: 0x0601A9A8 RID: 108968 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(23, "hash")]
		public Base64BinaryValue Hash
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17009489 RID: 38025
		// (get) Token: 0x0601A9A9 RID: 108969 RVA: 0x00364AAA File Offset: 0x00362CAA
		// (set) Token: 0x0601A9AA RID: 108970 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(23, "salt")]
		public Base64BinaryValue Salt
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x1700948A RID: 38026
		// (get) Token: 0x0601A9AB RID: 108971 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0601A9AC RID: 108972 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(23, "algorithmName")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public StringValue AlgorithmName
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

		// Token: 0x1700948B RID: 38027
		// (get) Token: 0x0601A9AD RID: 108973 RVA: 0x003286E3 File Offset: 0x003268E3
		// (set) Token: 0x0601A9AE RID: 108974 RVA: 0x002BE225 File Offset: 0x002BC425
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "hashValue")]
		public Base64BinaryValue HashValue
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

		// Token: 0x1700948C RID: 38028
		// (get) Token: 0x0601A9AF RID: 108975 RVA: 0x00364ABA File Offset: 0x00362CBA
		// (set) Token: 0x0601A9B0 RID: 108976 RVA: 0x002BE241 File Offset: 0x002BC441
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "saltValue")]
		public Base64BinaryValue SaltValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x1700948D RID: 38029
		// (get) Token: 0x0601A9B1 RID: 108977 RVA: 0x00364ACA File Offset: 0x00362CCA
		// (set) Token: 0x0601A9B2 RID: 108978 RVA: 0x002BE25D File Offset: 0x002BC45D
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "spinCount")]
		public Int32Value SpinCount
		{
			get
			{
				return (Int32Value)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x0601A9B4 RID: 108980 RVA: 0x00364ADC File Offset: 0x00362CDC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "recommended" == name)
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

		// Token: 0x0601A9B5 RID: 108981 RVA: 0x00364C89 File Offset: 0x00362E89
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WriteProtection>(deep);
		}

		// Token: 0x0400ADAC RID: 44460
		private const string tagName = "writeProtection";

		// Token: 0x0400ADAD RID: 44461
		private const byte tagNsId = 23;

		// Token: 0x0400ADAE RID: 44462
		internal const int ElementTypeIdConst = 11958;

		// Token: 0x0400ADAF RID: 44463
		private static string[] attributeTagNames = new string[]
		{
			"recommended", "cryptProviderType", "cryptAlgorithmClass", "cryptAlgorithmType", "cryptAlgorithmSid", "cryptSpinCount", "cryptProvider", "algIdExt", "algIdExtSource", "cryptProviderTypeExt",
			"cryptProviderTypeExtSource", "hash", "salt", "algorithmName", "hashValue", "saltValue", "spinCount"
		};

		// Token: 0x0400ADB0 RID: 44464
		private static byte[] attributeNamespaceIds = new byte[]
		{
			23, 23, 23, 23, 23, 23, 23, 23, 23, 23,
			23, 23, 23, 23, 23, 23, 23
		};
	}
}
