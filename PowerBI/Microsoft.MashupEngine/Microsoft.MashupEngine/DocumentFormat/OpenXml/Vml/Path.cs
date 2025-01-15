using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Vml.Office;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002242 RID: 8770
	[GeneratedCode("DomGen", "2.0")]
	internal class Path : OpenXmlLeafElement
	{
		// Token: 0x1700397B RID: 14715
		// (get) Token: 0x0600E0A6 RID: 57510 RVA: 0x002BFFB6 File Offset: 0x002BE1B6
		public override string LocalName
		{
			get
			{
				return "path";
			}
		}

		// Token: 0x1700397C RID: 14716
		// (get) Token: 0x0600E0A7 RID: 57511 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x1700397D RID: 14717
		// (get) Token: 0x0600E0A8 RID: 57512 RVA: 0x002BFFBD File Offset: 0x002BE1BD
		internal override int ElementTypeId
		{
			get
			{
				return 12506;
			}
		}

		// Token: 0x0600E0A9 RID: 57513 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700397E RID: 14718
		// (get) Token: 0x0600E0AA RID: 57514 RVA: 0x002BFFC4 File Offset: 0x002BE1C4
		internal override string[] AttributeTagNames
		{
			get
			{
				return Path.attributeTagNames;
			}
		}

		// Token: 0x1700397F RID: 14719
		// (get) Token: 0x0600E0AB RID: 57515 RVA: 0x002BFFCB File Offset: 0x002BE1CB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Path.attributeNamespaceIds;
			}
		}

		// Token: 0x17003980 RID: 14720
		// (get) Token: 0x0600E0AC RID: 57516 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E0AD RID: 57517 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17003981 RID: 14721
		// (get) Token: 0x0600E0AE RID: 57518 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E0AF RID: 57519 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "v")]
		public StringValue Value
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

		// Token: 0x17003982 RID: 14722
		// (get) Token: 0x0600E0B0 RID: 57520 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E0B1 RID: 57521 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "limo")]
		public StringValue Limo
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

		// Token: 0x17003983 RID: 14723
		// (get) Token: 0x0600E0B2 RID: 57522 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600E0B3 RID: 57523 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "textboxrect")]
		public StringValue TextboxRectangle
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

		// Token: 0x17003984 RID: 14724
		// (get) Token: 0x0600E0B4 RID: 57524 RVA: 0x002BDAE9 File Offset: 0x002BBCE9
		// (set) Token: 0x0600E0B5 RID: 57525 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "fillok")]
		public TrueFalseValue AllowFill
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

		// Token: 0x17003985 RID: 14725
		// (get) Token: 0x0600E0B6 RID: 57526 RVA: 0x002BD4D3 File Offset: 0x002BB6D3
		// (set) Token: 0x0600E0B7 RID: 57527 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "strokeok")]
		public TrueFalseValue AllowStroke
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

		// Token: 0x17003986 RID: 14726
		// (get) Token: 0x0600E0B8 RID: 57528 RVA: 0x002BDAF8 File Offset: 0x002BBCF8
		// (set) Token: 0x0600E0B9 RID: 57529 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "shadowok")]
		public TrueFalseValue AllowShading
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

		// Token: 0x17003987 RID: 14727
		// (get) Token: 0x0600E0BA RID: 57530 RVA: 0x002BD507 File Offset: 0x002BB707
		// (set) Token: 0x0600E0BB RID: 57531 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "arrowok")]
		public TrueFalseValue ShowArrowhead
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

		// Token: 0x17003988 RID: 14728
		// (get) Token: 0x0600E0BC RID: 57532 RVA: 0x002BD521 File Offset: 0x002BB721
		// (set) Token: 0x0600E0BD RID: 57533 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "gradientshapeok")]
		public TrueFalseValue AllowGradientShape
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

		// Token: 0x17003989 RID: 14729
		// (get) Token: 0x0600E0BE RID: 57534 RVA: 0x002BEA5B File Offset: 0x002BCC5B
		// (set) Token: 0x0600E0BF RID: 57535 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "textpathok")]
		public TrueFalseValue AllowTextPath
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

		// Token: 0x1700398A RID: 14730
		// (get) Token: 0x0600E0C0 RID: 57536 RVA: 0x002BE827 File Offset: 0x002BCA27
		// (set) Token: 0x0600E0C1 RID: 57537 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "insetpenok")]
		public TrueFalseValue AllowInsetPen
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

		// Token: 0x1700398B RID: 14731
		// (get) Token: 0x0600E0C2 RID: 57538 RVA: 0x002BFFD2 File Offset: 0x002BE1D2
		// (set) Token: 0x0600E0C3 RID: 57539 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(27, "connecttype")]
		public EnumValue<ConnectValues> ConnectionPointType
		{
			get
			{
				return (EnumValue<ConnectValues>)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x1700398C RID: 14732
		// (get) Token: 0x0600E0C4 RID: 57540 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E0C5 RID: 57541 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(27, "connectlocs")]
		public StringValue ConnectionPoints
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

		// Token: 0x1700398D RID: 14733
		// (get) Token: 0x0600E0C6 RID: 57542 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E0C7 RID: 57543 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(27, "connectangles")]
		public StringValue ConnectAngles
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

		// Token: 0x1700398E RID: 14734
		// (get) Token: 0x0600E0C8 RID: 57544 RVA: 0x002BFFE2 File Offset: 0x002BE1E2
		// (set) Token: 0x0600E0C9 RID: 57545 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(27, "extrusionok")]
		public TrueFalseValue AllowExtrusion
		{
			get
			{
				return (TrueFalseValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x0600E0CB RID: 57547 RVA: 0x002BFFF4 File Offset: 0x002BE1F4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "v" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "limo" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "textboxrect" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fillok" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "strokeok" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "shadowok" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "arrowok" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "gradientshapeok" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "textpathok" == name)
			{
				return new TrueFalseValue();
			}
			if (namespaceId == 0 && "insetpenok" == name)
			{
				return new TrueFalseValue();
			}
			if (27 == namespaceId && "connecttype" == name)
			{
				return new EnumValue<ConnectValues>();
			}
			if (27 == namespaceId && "connectlocs" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "connectangles" == name)
			{
				return new StringValue();
			}
			if (27 == namespaceId && "extrusionok" == name)
			{
				return new TrueFalseValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E0CC RID: 57548 RVA: 0x002C015B File Offset: 0x002BE35B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Path>(deep);
		}

		// Token: 0x04006E90 RID: 28304
		private const string tagName = "path";

		// Token: 0x04006E91 RID: 28305
		private const byte tagNsId = 26;

		// Token: 0x04006E92 RID: 28306
		internal const int ElementTypeIdConst = 12506;

		// Token: 0x04006E93 RID: 28307
		private static string[] attributeTagNames = new string[]
		{
			"id", "v", "limo", "textboxrect", "fillok", "strokeok", "shadowok", "arrowok", "gradientshapeok", "textpathok",
			"insetpenok", "connecttype", "connectlocs", "connectangles", "extrusionok"
		};

		// Token: 0x04006E94 RID: 28308
		private static byte[] attributeNamespaceIds = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 27, 27, 27, 27
		};
	}
}
