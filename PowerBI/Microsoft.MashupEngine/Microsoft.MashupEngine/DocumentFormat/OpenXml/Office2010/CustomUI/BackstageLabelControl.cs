using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022DE RID: 8926
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackstageLabelControl : OpenXmlLeafElement
	{
		// Token: 0x170045B2 RID: 17842
		// (get) Token: 0x0600FA31 RID: 64049 RVA: 0x002CB6EA File Offset: 0x002C98EA
		public override string LocalName
		{
			get
			{
				return "labelControl";
			}
		}

		// Token: 0x170045B3 RID: 17843
		// (get) Token: 0x0600FA32 RID: 64050 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170045B4 RID: 17844
		// (get) Token: 0x0600FA33 RID: 64051 RVA: 0x002D966B File Offset: 0x002D786B
		internal override int ElementTypeId
		{
			get
			{
				return 13071;
			}
		}

		// Token: 0x0600FA34 RID: 64052 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170045B5 RID: 17845
		// (get) Token: 0x0600FA35 RID: 64053 RVA: 0x002D9672 File Offset: 0x002D7872
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageLabelControl.attributeTagNames;
			}
		}

		// Token: 0x170045B6 RID: 17846
		// (get) Token: 0x0600FA36 RID: 64054 RVA: 0x002D9679 File Offset: 0x002D7879
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageLabelControl.attributeNamespaceIds;
			}
		}

		// Token: 0x170045B7 RID: 17847
		// (get) Token: 0x0600FA37 RID: 64055 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FA38 RID: 64056 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170045B8 RID: 17848
		// (get) Token: 0x0600FA39 RID: 64057 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FA3A RID: 64058 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170045B9 RID: 17849
		// (get) Token: 0x0600FA3B RID: 64059 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FA3C RID: 64060 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x170045BA RID: 17850
		// (get) Token: 0x0600FA3D RID: 64061 RVA: 0x002D8849 File Offset: 0x002D6A49
		// (set) Token: 0x0600FA3E RID: 64062 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "alignLabel")]
		public EnumValue<ExpandValues> AlignLabel
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170045BB RID: 17851
		// (get) Token: 0x0600FA3F RID: 64063 RVA: 0x002D8858 File Offset: 0x002D6A58
		// (set) Token: 0x0600FA40 RID: 64064 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "expand")]
		public EnumValue<ExpandValues> Expand
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170045BC RID: 17852
		// (get) Token: 0x0600FA41 RID: 64065 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0600FA42 RID: 64066 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170045BD RID: 17853
		// (get) Token: 0x0600FA43 RID: 64067 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FA44 RID: 64068 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170045BE RID: 17854
		// (get) Token: 0x0600FA45 RID: 64069 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FA46 RID: 64070 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x170045BF RID: 17855
		// (get) Token: 0x0600FA47 RID: 64071 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FA48 RID: 64072 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x170045C0 RID: 17856
		// (get) Token: 0x0600FA49 RID: 64073 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600FA4A RID: 64074 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170045C1 RID: 17857
		// (get) Token: 0x0600FA4B RID: 64075 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FA4C RID: 64076 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x170045C2 RID: 17858
		// (get) Token: 0x0600FA4D RID: 64077 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0600FA4E RID: 64078 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "noWrap")]
		public BooleanValue NoWrap
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x0600FA50 RID: 64080 RVA: 0x002D9680 File Offset: 0x002D7880
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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
			if (namespaceId == 0 && "alignLabel" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "expand" == name)
			{
				return new EnumValue<ExpandValues>();
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
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "noWrap" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FA51 RID: 64081 RVA: 0x002D979D File Offset: 0x002D799D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageLabelControl>(deep);
		}

		// Token: 0x0600FA52 RID: 64082 RVA: 0x002D97A8 File Offset: 0x002D79A8
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageLabelControl()
		{
			byte[] array = new byte[12];
			BackstageLabelControl.attributeNamespaceIds = array;
		}

		// Token: 0x04007183 RID: 29059
		private const string tagName = "labelControl";

		// Token: 0x04007184 RID: 29060
		private const byte tagNsId = 57;

		// Token: 0x04007185 RID: 29061
		internal const int ElementTypeIdConst = 13071;

		// Token: 0x04007186 RID: 29062
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "alignLabel", "expand", "enabled", "getEnabled", "label", "getLabel", "visible",
			"getVisible", "noWrap"
		};

		// Token: 0x04007187 RID: 29063
		private static byte[] attributeNamespaceIds;
	}
}
