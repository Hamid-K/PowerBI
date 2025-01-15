using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002209 RID: 8713
	[GeneratedCode("DomGen", "2.0")]
	internal class Lock : OpenXmlLeafElement
	{
		// Token: 0x1700389E RID: 14494
		// (get) Token: 0x0600DED3 RID: 57043 RVA: 0x002BEA3F File Offset: 0x002BCC3F
		public override string LocalName
		{
			get
			{
				return "lock";
			}
		}

		// Token: 0x1700389F RID: 14495
		// (get) Token: 0x0600DED4 RID: 57044 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170038A0 RID: 14496
		// (get) Token: 0x0600DED5 RID: 57045 RVA: 0x002BEA46 File Offset: 0x002BCC46
		internal override int ElementTypeId
		{
			get
			{
				return 12407;
			}
		}

		// Token: 0x0600DED6 RID: 57046 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170038A1 RID: 14497
		// (get) Token: 0x0600DED7 RID: 57047 RVA: 0x002BEA4D File Offset: 0x002BCC4D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Lock.attributeTagNames;
			}
		}

		// Token: 0x170038A2 RID: 14498
		// (get) Token: 0x0600DED8 RID: 57048 RVA: 0x002BEA54 File Offset: 0x002BCC54
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Lock.attributeNamespaceIds;
			}
		}

		// Token: 0x170038A3 RID: 14499
		// (get) Token: 0x0600DED9 RID: 57049 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DEDA RID: 57050 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170038A4 RID: 14500
		// (get) Token: 0x0600DEDB RID: 57051 RVA: 0x002BDACB File Offset: 0x002BBCCB
		// (set) Token: 0x0600DEDC RID: 57052 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "position")]
		public TrueFalseValue Position
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

		// Token: 0x170038A5 RID: 14501
		// (get) Token: 0x0600DEDD RID: 57053 RVA: 0x002BDE2B File Offset: 0x002BC02B
		// (set) Token: 0x0600DEDE RID: 57054 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "selection")]
		public TrueFalseValue Selection
		{
			get
			{
				return (TrueFalseValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170038A6 RID: 14502
		// (get) Token: 0x0600DEDF RID: 57055 RVA: 0x002BD49F File Offset: 0x002BB69F
		// (set) Token: 0x0600DEE0 RID: 57056 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "grouping")]
		public TrueFalseValue Grouping
		{
			get
			{
				return (TrueFalseValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170038A7 RID: 14503
		// (get) Token: 0x0600DEE1 RID: 57057 RVA: 0x002BDAE9 File Offset: 0x002BBCE9
		// (set) Token: 0x0600DEE2 RID: 57058 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "ungrouping")]
		public TrueFalseValue Ungrouping
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

		// Token: 0x170038A8 RID: 14504
		// (get) Token: 0x0600DEE3 RID: 57059 RVA: 0x002BD4D3 File Offset: 0x002BB6D3
		// (set) Token: 0x0600DEE4 RID: 57060 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "rotation")]
		public TrueFalseValue Rotation
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

		// Token: 0x170038A9 RID: 14505
		// (get) Token: 0x0600DEE5 RID: 57061 RVA: 0x002BDAF8 File Offset: 0x002BBCF8
		// (set) Token: 0x0600DEE6 RID: 57062 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "cropping")]
		public TrueFalseValue Cropping
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

		// Token: 0x170038AA RID: 14506
		// (get) Token: 0x0600DEE7 RID: 57063 RVA: 0x002BD507 File Offset: 0x002BB707
		// (set) Token: 0x0600DEE8 RID: 57064 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "verticies")]
		public TrueFalseValue Verticies
		{
			get
			{
				return (TrueFalseValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170038AB RID: 14507
		// (get) Token: 0x0600DEE9 RID: 57065 RVA: 0x002BD521 File Offset: 0x002BB721
		// (set) Token: 0x0600DEEA RID: 57066 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "adjusthandles")]
		public TrueFalseValue AdjustHandles
		{
			get
			{
				return (TrueFalseValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170038AC RID: 14508
		// (get) Token: 0x0600DEEB RID: 57067 RVA: 0x002BEA5B File Offset: 0x002BCC5B
		// (set) Token: 0x0600DEEC RID: 57068 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "text")]
		public TrueFalseValue TextLock
		{
			get
			{
				return (TrueFalseValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170038AD RID: 14509
		// (get) Token: 0x0600DEED RID: 57069 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600DEEE RID: 57070 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "aspectratio")]
		public TrueFalseValue AspectRatio
		{
			get
			{
				return (TrueFalseValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170038AE RID: 14510
		// (get) Token: 0x0600DEEF RID: 57071 RVA: 0x002BE837 File Offset: 0x002BCA37
		// (set) Token: 0x0600DEF0 RID: 57072 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "shapetype")]
		public TrueFalseValue ShapeType
		{
			get
			{
				return (TrueFalseValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x0600DEF2 RID: 57074 RVA: 0x002BEA6C File Offset: 0x002BCC6C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "position" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "selection" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "grouping" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "ungrouping" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "rotation" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "cropping" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "verticies" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "adjusthandles" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "text" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "aspectratio" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "shapetype" == name)
			{
				return new TrueFalseValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DEF3 RID: 57075 RVA: 0x002BEB8B File Offset: 0x002BCD8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Lock>(deep);
		}

		// Token: 0x0600DEF4 RID: 57076 RVA: 0x002BEB94 File Offset: 0x002BCD94
		// Note: this type is marked as 'beforefieldinit'.
		static Lock()
		{
			byte[] array = new byte[12];
			array[0] = 26;
			Lock.attributeNamespaceIds = array;
		}

		// Token: 0x04006D7C RID: 28028
		private const string tagName = "lock";

		// Token: 0x04006D7D RID: 28029
		private const byte tagNsId = 27;

		// Token: 0x04006D7E RID: 28030
		internal const int ElementTypeIdConst = 12407;

		// Token: 0x04006D7F RID: 28031
		private static string[] attributeTagNames = new string[]
		{
			"ext", "position", "selection", "grouping", "ungrouping", "rotation", "cropping", "verticies", "adjusthandles", "text",
			"aspectratio", "shapetype"
		};

		// Token: 0x04006D80 RID: 28032
		private static byte[] attributeNamespaceIds;
	}
}
