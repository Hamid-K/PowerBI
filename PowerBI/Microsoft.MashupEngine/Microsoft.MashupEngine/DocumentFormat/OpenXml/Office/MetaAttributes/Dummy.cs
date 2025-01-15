using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.MetaAttributes
{
	// Token: 0x020022B8 RID: 8888
	[GeneratedCode("DomGen", "2.0")]
	internal class Dummy : OpenXmlLeafElement
	{
		// Token: 0x17004173 RID: 16755
		// (get) Token: 0x0600F173 RID: 61811 RVA: 0x002D14CB File Offset: 0x002CF6CB
		public override string LocalName
		{
			get
			{
				return "DummyContentTypeElement";
			}
		}

		// Token: 0x17004174 RID: 16756
		// (get) Token: 0x0600F174 RID: 61812 RVA: 0x002D14D2 File Offset: 0x002CF6D2
		internal override byte NamespaceId
		{
			get
			{
				return 41;
			}
		}

		// Token: 0x17004175 RID: 16757
		// (get) Token: 0x0600F175 RID: 61813 RVA: 0x002D14D6 File Offset: 0x002CF6D6
		internal override int ElementTypeId
		{
			get
			{
				return 12642;
			}
		}

		// Token: 0x0600F176 RID: 61814 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004176 RID: 16758
		// (get) Token: 0x0600F177 RID: 61815 RVA: 0x002D14DD File Offset: 0x002CF6DD
		internal override string[] AttributeTagNames
		{
			get
			{
				return Dummy.attributeTagNames;
			}
		}

		// Token: 0x17004177 RID: 16759
		// (get) Token: 0x0600F178 RID: 61816 RVA: 0x002D14E4 File Offset: 0x002CF6E4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Dummy.attributeNamespaceIds;
			}
		}

		// Token: 0x17004178 RID: 16760
		// (get) Token: 0x0600F179 RID: 61817 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F17A RID: 61818 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "decimals")]
		public StringValue Decimals
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

		// Token: 0x17004179 RID: 16761
		// (get) Token: 0x0600F17B RID: 61819 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F17C RID: 61820 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "default")]
		public StringValue Default
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

		// Token: 0x1700417A RID: 16762
		// (get) Token: 0x0600F17D RID: 61821 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F17E RID: 61822 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x1700417B RID: 16763
		// (get) Token: 0x0600F17F RID: 61823 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F180 RID: 61824 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "displayName")]
		public StringValue DisplayName
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

		// Token: 0x1700417C RID: 16764
		// (get) Token: 0x0600F181 RID: 61825 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F182 RID: 61826 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "fieldsID")]
		public StringValue FieldsID
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

		// Token: 0x1700417D RID: 16765
		// (get) Token: 0x0600F183 RID: 61827 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F184 RID: 61828 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "format")]
		public StringValue Format
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

		// Token: 0x1700417E RID: 16766
		// (get) Token: 0x0600F185 RID: 61829 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F186 RID: 61830 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "hidden")]
		public StringValue Hidden
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

		// Token: 0x1700417F RID: 16767
		// (get) Token: 0x0600F187 RID: 61831 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x0600F188 RID: 61832 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "index")]
		public Int32Value Index
		{
			get
			{
				return (Int32Value)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004180 RID: 16768
		// (get) Token: 0x0600F189 RID: 61833 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F18A RID: 61834 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "internalName")]
		public StringValue InternalName
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

		// Token: 0x17004181 RID: 16769
		// (get) Token: 0x0600F18B RID: 61835 RVA: 0x002D14FA File Offset: 0x002CF6FA
		// (set) Token: 0x0600F18C RID: 61836 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "LCID")]
		public Int32Value LCID
		{
			get
			{
				return (Int32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17004182 RID: 16770
		// (get) Token: 0x0600F18D RID: 61837 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F18E RID: 61838 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "list")]
		public StringValue List
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

		// Token: 0x17004183 RID: 16771
		// (get) Token: 0x0600F18F RID: 61839 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F190 RID: 61840 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "percentage")]
		public StringValue Percentage
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

		// Token: 0x17004184 RID: 16772
		// (get) Token: 0x0600F191 RID: 61841 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F192 RID: 61842 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "readOnly")]
		public StringValue ReadOnly
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

		// Token: 0x17004185 RID: 16773
		// (get) Token: 0x0600F193 RID: 61843 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F194 RID: 61844 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "requiredMultiChoice")]
		public StringValue RequiredMultiChoice
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

		// Token: 0x17004186 RID: 16774
		// (get) Token: 0x0600F195 RID: 61845 RVA: 0x002D150A File Offset: 0x002CF70A
		// (set) Token: 0x0600F196 RID: 61846 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "root")]
		public EnumValue<TrueOnlyValues> Root
		{
			get
			{
				return (EnumValue<TrueOnlyValues>)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17004187 RID: 16775
		// (get) Token: 0x0600F197 RID: 61847 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F198 RID: 61848 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "showField")]
		public StringValue ShowField
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

		// Token: 0x17004188 RID: 16776
		// (get) Token: 0x0600F199 RID: 61849 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F19A RID: 61850 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "web")]
		public StringValue Web
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

		// Token: 0x0600F19C RID: 61852 RVA: 0x002D151C File Offset: 0x002CF71C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "decimals" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "default" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "displayName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fieldsID" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "format" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "index" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "internalName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "LCID" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "list" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "percentage" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "readOnly" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "requiredMultiChoice" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "root" == name)
			{
				return new EnumValue<TrueOnlyValues>();
			}
			if (namespaceId == 0 && "showField" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "web" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F19D RID: 61853 RVA: 0x002D16A7 File Offset: 0x002CF8A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Dummy>(deep);
		}

		// Token: 0x0600F19E RID: 61854 RVA: 0x002D16B0 File Offset: 0x002CF8B0
		// Note: this type is marked as 'beforefieldinit'.
		static Dummy()
		{
			byte[] array = new byte[17];
			Dummy.attributeNamespaceIds = array;
		}

		// Token: 0x040070C8 RID: 28872
		private const string tagName = "DummyContentTypeElement";

		// Token: 0x040070C9 RID: 28873
		private const byte tagNsId = 41;

		// Token: 0x040070CA RID: 28874
		internal const int ElementTypeIdConst = 12642;

		// Token: 0x040070CB RID: 28875
		private static string[] attributeTagNames = new string[]
		{
			"decimals", "default", "description", "displayName", "fieldsID", "format", "hidden", "index", "internalName", "LCID",
			"list", "percentage", "readOnly", "requiredMultiChoice", "root", "showField", "web"
		};

		// Token: 0x040070CC RID: 28876
		private static byte[] attributeNamespaceIds;
	}
}
