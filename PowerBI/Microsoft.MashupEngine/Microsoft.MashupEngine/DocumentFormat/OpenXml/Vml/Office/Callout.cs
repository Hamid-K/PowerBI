using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002208 RID: 8712
	[GeneratedCode("DomGen", "2.0")]
	internal class Callout : OpenXmlLeafElement
	{
		// Token: 0x1700388B RID: 14475
		// (get) Token: 0x0600DEAD RID: 57005 RVA: 0x002BE7FC File Offset: 0x002BC9FC
		public override string LocalName
		{
			get
			{
				return "callout";
			}
		}

		// Token: 0x1700388C RID: 14476
		// (get) Token: 0x0600DEAE RID: 57006 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x1700388D RID: 14477
		// (get) Token: 0x0600DEAF RID: 57007 RVA: 0x002BE803 File Offset: 0x002BCA03
		internal override int ElementTypeId
		{
			get
			{
				return 12406;
			}
		}

		// Token: 0x0600DEB0 RID: 57008 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700388E RID: 14478
		// (get) Token: 0x0600DEB1 RID: 57009 RVA: 0x002BE80A File Offset: 0x002BCA0A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Callout.attributeTagNames;
			}
		}

		// Token: 0x1700388F RID: 14479
		// (get) Token: 0x0600DEB2 RID: 57010 RVA: 0x002BE811 File Offset: 0x002BCA11
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Callout.attributeNamespaceIds;
			}
		}

		// Token: 0x17003890 RID: 14480
		// (get) Token: 0x0600DEB3 RID: 57011 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DEB4 RID: 57012 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003891 RID: 14481
		// (get) Token: 0x0600DEB5 RID: 57013 RVA: 0x002BDACB File Offset: 0x002BBCCB
		// (set) Token: 0x0600DEB6 RID: 57014 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "on")]
		public TrueFalseValue On
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

		// Token: 0x17003892 RID: 14482
		// (get) Token: 0x0600DEB7 RID: 57015 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600DEB8 RID: 57016 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "type")]
		public StringValue Type
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

		// Token: 0x17003893 RID: 14483
		// (get) Token: 0x0600DEB9 RID: 57017 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600DEBA RID: 57018 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "gap")]
		public StringValue Gap
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

		// Token: 0x17003894 RID: 14484
		// (get) Token: 0x0600DEBB RID: 57019 RVA: 0x002BE818 File Offset: 0x002BCA18
		// (set) Token: 0x0600DEBC RID: 57020 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "angle")]
		public EnumValue<AngleValues> Angle
		{
			get
			{
				return (EnumValue<AngleValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003895 RID: 14485
		// (get) Token: 0x0600DEBD RID: 57021 RVA: 0x002BD4D3 File Offset: 0x002BB6D3
		// (set) Token: 0x0600DEBE RID: 57022 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "dropauto")]
		public TrueFalseValue DropAuto
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

		// Token: 0x17003896 RID: 14486
		// (get) Token: 0x0600DEBF RID: 57023 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600DEC0 RID: 57024 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "drop")]
		public StringValue Drop
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

		// Token: 0x17003897 RID: 14487
		// (get) Token: 0x0600DEC1 RID: 57025 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600DEC2 RID: 57026 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "distance")]
		public StringValue Distance
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

		// Token: 0x17003898 RID: 14488
		// (get) Token: 0x0600DEC3 RID: 57027 RVA: 0x002BD521 File Offset: 0x002BB721
		// (set) Token: 0x0600DEC4 RID: 57028 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "lengthspecified")]
		public TrueFalseValue LengthSpecified
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

		// Token: 0x17003899 RID: 14489
		// (get) Token: 0x0600DEC5 RID: 57029 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600DEC6 RID: 57030 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "length")]
		public StringValue Length
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

		// Token: 0x1700389A RID: 14490
		// (get) Token: 0x0600DEC7 RID: 57031 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600DEC8 RID: 57032 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "accentbar")]
		public TrueFalseValue AccentBar
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

		// Token: 0x1700389B RID: 14491
		// (get) Token: 0x0600DEC9 RID: 57033 RVA: 0x002BE837 File Offset: 0x002BCA37
		// (set) Token: 0x0600DECA RID: 57034 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "textborder")]
		public TrueFalseValue TextBorder
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

		// Token: 0x1700389C RID: 14492
		// (get) Token: 0x0600DECB RID: 57035 RVA: 0x002BE1E9 File Offset: 0x002BC3E9
		// (set) Token: 0x0600DECC RID: 57036 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "minusx")]
		public TrueFalseValue MinusX
		{
			get
			{
				return (TrueFalseValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x1700389D RID: 14493
		// (get) Token: 0x0600DECD RID: 57037 RVA: 0x002BE1F9 File Offset: 0x002BC3F9
		// (set) Token: 0x0600DECE RID: 57038 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "minusy")]
		public TrueFalseValue MinusY
		{
			get
			{
				return (TrueFalseValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x0600DED0 RID: 57040 RVA: 0x002BE848 File Offset: 0x002BCA48
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			if (namespaceId == 0 && "on" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "gap" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "angle" == name)
			{
				return new EnumValue<AngleValues>();
			}
			if (namespaceId == 0 && "dropauto" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "drop" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "distance" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "lengthspecified" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "length" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "accentbar" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "textborder" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "minusx" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "minusy" == name)
			{
				return new TrueFalseValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DED1 RID: 57041 RVA: 0x002BE993 File Offset: 0x002BCB93
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Callout>(deep);
		}

		// Token: 0x0600DED2 RID: 57042 RVA: 0x002BE99C File Offset: 0x002BCB9C
		// Note: this type is marked as 'beforefieldinit'.
		static Callout()
		{
			byte[] array = new byte[14];
			array[0] = 26;
			Callout.attributeNamespaceIds = array;
		}

		// Token: 0x04006D77 RID: 28023
		private const string tagName = "callout";

		// Token: 0x04006D78 RID: 28024
		private const byte tagNsId = 27;

		// Token: 0x04006D79 RID: 28025
		internal const int ElementTypeIdConst = 12406;

		// Token: 0x04006D7A RID: 28026
		private static string[] attributeTagNames = new string[]
		{
			"ext", "on", "type", "gap", "angle", "dropauto", "drop", "distance", "lengthspecified", "length",
			"accentbar", "textborder", "minusx", "minusy"
		};

		// Token: 0x04006D7B RID: 28027
		private static byte[] attributeNamespaceIds;
	}
}
