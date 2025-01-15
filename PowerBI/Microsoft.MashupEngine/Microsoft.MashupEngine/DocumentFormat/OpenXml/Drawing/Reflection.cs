using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002720 RID: 10016
	[GeneratedCode("DomGen", "2.0")]
	internal class Reflection : OpenXmlLeafElement
	{
		// Token: 0x17005F83 RID: 24451
		// (get) Token: 0x06013375 RID: 78709 RVA: 0x002ED530 File Offset: 0x002EB730
		public override string LocalName
		{
			get
			{
				return "reflection";
			}
		}

		// Token: 0x17005F84 RID: 24452
		// (get) Token: 0x06013376 RID: 78710 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F85 RID: 24453
		// (get) Token: 0x06013377 RID: 78711 RVA: 0x00304F72 File Offset: 0x00303172
		internal override int ElementTypeId
		{
			get
			{
				return 10078;
			}
		}

		// Token: 0x06013378 RID: 78712 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005F86 RID: 24454
		// (get) Token: 0x06013379 RID: 78713 RVA: 0x00304F79 File Offset: 0x00303179
		internal override string[] AttributeTagNames
		{
			get
			{
				return Reflection.attributeTagNames;
			}
		}

		// Token: 0x17005F87 RID: 24455
		// (get) Token: 0x0601337A RID: 78714 RVA: 0x00304F80 File Offset: 0x00303180
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Reflection.attributeNamespaceIds;
			}
		}

		// Token: 0x17005F88 RID: 24456
		// (get) Token: 0x0601337B RID: 78715 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x0601337C RID: 78716 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "blurRad")]
		public Int64Value BlurRadius
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005F89 RID: 24457
		// (get) Token: 0x0601337D RID: 78717 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601337E RID: 78718 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "stA")]
		public Int32Value StartOpacity
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005F8A RID: 24458
		// (get) Token: 0x0601337F RID: 78719 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06013380 RID: 78720 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "stPos")]
		public Int32Value StartPosition
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005F8B RID: 24459
		// (get) Token: 0x06013381 RID: 78721 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06013382 RID: 78722 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "endA")]
		public Int32Value EndAlpha
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005F8C RID: 24460
		// (get) Token: 0x06013383 RID: 78723 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x06013384 RID: 78724 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "endPos")]
		public Int32Value EndPosition
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

		// Token: 0x17005F8D RID: 24461
		// (get) Token: 0x06013385 RID: 78725 RVA: 0x002ED54C File Offset: 0x002EB74C
		// (set) Token: 0x06013386 RID: 78726 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "dist")]
		public Int64Value Distance
		{
			get
			{
				return (Int64Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17005F8E RID: 24462
		// (get) Token: 0x06013387 RID: 78727 RVA: 0x002ED380 File Offset: 0x002EB580
		// (set) Token: 0x06013388 RID: 78728 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "dir")]
		public Int32Value Direction
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

		// Token: 0x17005F8F RID: 24463
		// (get) Token: 0x06013389 RID: 78729 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x0601338A RID: 78730 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "fadeDir")]
		public Int32Value FadeDirection
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

		// Token: 0x17005F90 RID: 24464
		// (get) Token: 0x0601338B RID: 78731 RVA: 0x002ED55B File Offset: 0x002EB75B
		// (set) Token: 0x0601338C RID: 78732 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "sx")]
		public Int32Value HorizontalRatio
		{
			get
			{
				return (Int32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17005F91 RID: 24465
		// (get) Token: 0x0601338D RID: 78733 RVA: 0x002D14FA File Offset: 0x002CF6FA
		// (set) Token: 0x0601338E RID: 78734 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "sy")]
		public Int32Value VerticalRatio
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

		// Token: 0x17005F92 RID: 24466
		// (get) Token: 0x0601338F RID: 78735 RVA: 0x002E7730 File Offset: 0x002E5930
		// (set) Token: 0x06013390 RID: 78736 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "kx")]
		public Int32Value HorizontalSkew
		{
			get
			{
				return (Int32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17005F93 RID: 24467
		// (get) Token: 0x06013391 RID: 78737 RVA: 0x002ED56A File Offset: 0x002EB76A
		// (set) Token: 0x06013392 RID: 78738 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "ky")]
		public Int32Value VerticalSkew
		{
			get
			{
				return (Int32Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17005F94 RID: 24468
		// (get) Token: 0x06013393 RID: 78739 RVA: 0x00304F87 File Offset: 0x00303187
		// (set) Token: 0x06013394 RID: 78740 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "algn")]
		public EnumValue<RectangleAlignmentValues> Alignment
		{
			get
			{
				return (EnumValue<RectangleAlignmentValues>)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17005F95 RID: 24469
		// (get) Token: 0x06013395 RID: 78741 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06013396 RID: 78742 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "rotWithShape")]
		public BooleanValue RotateWithShape
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x06013398 RID: 78744 RVA: 0x00304F98 File Offset: 0x00303198
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "blurRad" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "stA" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "stPos" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "endA" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "endPos" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "dist" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "dir" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "fadeDir" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "sx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "sy" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "kx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "ky" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "algn" == name)
			{
				return new EnumValue<RectangleAlignmentValues>();
			}
			if (namespaceId == 0 && "rotWithShape" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013399 RID: 78745 RVA: 0x003050E1 File Offset: 0x003032E1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Reflection>(deep);
		}

		// Token: 0x0601339A RID: 78746 RVA: 0x003050EC File Offset: 0x003032EC
		// Note: this type is marked as 'beforefieldinit'.
		static Reflection()
		{
			byte[] array = new byte[14];
			Reflection.attributeNamespaceIds = array;
		}

		// Token: 0x0400852B RID: 34091
		private const string tagName = "reflection";

		// Token: 0x0400852C RID: 34092
		private const byte tagNsId = 10;

		// Token: 0x0400852D RID: 34093
		internal const int ElementTypeIdConst = 10078;

		// Token: 0x0400852E RID: 34094
		private static string[] attributeTagNames = new string[]
		{
			"blurRad", "stA", "stPos", "endA", "endPos", "dist", "dir", "fadeDir", "sx", "sy",
			"kx", "ky", "algn", "rotWithShape"
		};

		// Token: 0x0400852F RID: 34095
		private static byte[] attributeNamespaceIds;
	}
}
