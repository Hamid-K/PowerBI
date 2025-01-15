using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002203 RID: 8707
	[GeneratedCode("DomGen", "2.0")]
	internal class SignatureLine : OpenXmlLeafElement
	{
		// Token: 0x17003830 RID: 14384
		// (get) Token: 0x0600DDF6 RID: 56822 RVA: 0x002BDAAF File Offset: 0x002BBCAF
		public override string LocalName
		{
			get
			{
				return "signatureline";
			}
		}

		// Token: 0x17003831 RID: 14385
		// (get) Token: 0x0600DDF7 RID: 56823 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x17003832 RID: 14386
		// (get) Token: 0x0600DDF8 RID: 56824 RVA: 0x002BDAB6 File Offset: 0x002BBCB6
		internal override int ElementTypeId
		{
			get
			{
				return 12401;
			}
		}

		// Token: 0x0600DDF9 RID: 56825 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003833 RID: 14387
		// (get) Token: 0x0600DDFA RID: 56826 RVA: 0x002BDABD File Offset: 0x002BBCBD
		internal override string[] AttributeTagNames
		{
			get
			{
				return SignatureLine.attributeTagNames;
			}
		}

		// Token: 0x17003834 RID: 14388
		// (get) Token: 0x0600DDFB RID: 56827 RVA: 0x002BDAC4 File Offset: 0x002BBCC4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SignatureLine.attributeNamespaceIds;
			}
		}

		// Token: 0x17003835 RID: 14389
		// (get) Token: 0x0600DDFC RID: 56828 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DDFD RID: 56829 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003836 RID: 14390
		// (get) Token: 0x0600DDFE RID: 56830 RVA: 0x002BDACB File Offset: 0x002BBCCB
		// (set) Token: 0x0600DDFF RID: 56831 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "issignatureline")]
		public TrueFalseValue IsSignatureLine
		{
			get
			{
				return (TrueFalseValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003837 RID: 14391
		// (get) Token: 0x0600DE00 RID: 56832 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600DE01 RID: 56833 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17003838 RID: 14392
		// (get) Token: 0x0600DE02 RID: 56834 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600DE03 RID: 56835 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "provid")]
		public StringValue ProviderId
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

		// Token: 0x17003839 RID: 14393
		// (get) Token: 0x0600DE04 RID: 56836 RVA: 0x002BDAE9 File Offset: 0x002BBCE9
		// (set) Token: 0x0600DE05 RID: 56837 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "signinginstructionsset")]
		public TrueFalseValue SigningInstructionsSet
		{
			get
			{
				return (TrueFalseValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700383A RID: 14394
		// (get) Token: 0x0600DE06 RID: 56838 RVA: 0x002BD4D3 File Offset: 0x002BB6D3
		// (set) Token: 0x0600DE07 RID: 56839 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "allowcomments")]
		public TrueFalseValue AllowComments
		{
			get
			{
				return (TrueFalseValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x1700383B RID: 14395
		// (get) Token: 0x0600DE08 RID: 56840 RVA: 0x002BDAF8 File Offset: 0x002BBCF8
		// (set) Token: 0x0600DE09 RID: 56841 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "showsigndate")]
		public TrueFalseValue ShowSignDate
		{
			get
			{
				return (TrueFalseValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700383C RID: 14396
		// (get) Token: 0x0600DE0A RID: 56842 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600DE0B RID: 56843 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(27, "suggestedsigner")]
		public StringValue SuggestedSigner
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

		// Token: 0x1700383D RID: 14397
		// (get) Token: 0x0600DE0C RID: 56844 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600DE0D RID: 56845 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(27, "suggestedsigner2")]
		public StringValue SuggestedSigner2
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

		// Token: 0x1700383E RID: 14398
		// (get) Token: 0x0600DE0E RID: 56846 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600DE0F RID: 56847 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(27, "suggestedsigneremail")]
		public StringValue SuggestedSignerEmail
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

		// Token: 0x1700383F RID: 14399
		// (get) Token: 0x0600DE10 RID: 56848 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600DE11 RID: 56849 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "signinginstructions")]
		public StringValue SigningInstructions
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

		// Token: 0x17003840 RID: 14400
		// (get) Token: 0x0600DE12 RID: 56850 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600DE13 RID: 56851 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "addlxml")]
		public StringValue AdditionalXml
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

		// Token: 0x17003841 RID: 14401
		// (get) Token: 0x0600DE14 RID: 56852 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600DE15 RID: 56853 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "sigprovurl")]
		public StringValue SignatureProviderUrl
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

		// Token: 0x0600DE17 RID: 56855 RVA: 0x002BDB8C File Offset: 0x002BBD8C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "issignatureline" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "provid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "signinginstructionsset" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "allowcomments" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "showsigndate" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "suggestedsigner" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "suggestedsigner2" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "suggestedsigneremail" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "signinginstructions" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "addlxml" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sigprovurl" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DE18 RID: 56856 RVA: 0x002BDCC7 File Offset: 0x002BBEC7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SignatureLine>(deep);
		}

		// Token: 0x04006D5C RID: 27996
		private const string tagName = "signatureline";

		// Token: 0x04006D5D RID: 27997
		private const byte tagNsId = 27;

		// Token: 0x04006D5E RID: 27998
		internal const int ElementTypeIdConst = 12401;

		// Token: 0x04006D5F RID: 27999
		private static string[] attributeTagNames = new string[]
		{
			"ext", "issignatureline", "id", "provid", "signinginstructionsset", "allowcomments", "showsigndate", "suggestedsigner", "suggestedsigner2", "suggestedsigneremail",
			"signinginstructions", "addlxml", "sigprovurl"
		};

		// Token: 0x04006D60 RID: 28000
		private static byte[] attributeNamespaceIds = new byte[]
		{
			26, 0, 0, 0, 0, 0, 0, 27, 27, 27,
			0, 0, 0
		};
	}
}
