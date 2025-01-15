using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024A5 RID: 9381
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Reflection : OpenXmlLeafElement
	{
		// Token: 0x170051F8 RID: 20984
		// (get) Token: 0x0601156B RID: 71019 RVA: 0x002ED530 File Offset: 0x002EB730
		public override string LocalName
		{
			get
			{
				return "reflection";
			}
		}

		// Token: 0x170051F9 RID: 20985
		// (get) Token: 0x0601156C RID: 71020 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051FA RID: 20986
		// (get) Token: 0x0601156D RID: 71021 RVA: 0x002ED537 File Offset: 0x002EB737
		internal override int ElementTypeId
		{
			get
			{
				return 12855;
			}
		}

		// Token: 0x0601156E RID: 71022 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170051FB RID: 20987
		// (get) Token: 0x0601156F RID: 71023 RVA: 0x002ED53E File Offset: 0x002EB73E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Reflection.attributeTagNames;
			}
		}

		// Token: 0x170051FC RID: 20988
		// (get) Token: 0x06011570 RID: 71024 RVA: 0x002ED545 File Offset: 0x002EB745
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Reflection.attributeNamespaceIds;
			}
		}

		// Token: 0x170051FD RID: 20989
		// (get) Token: 0x06011571 RID: 71025 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06011572 RID: 71026 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "blurRad")]
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

		// Token: 0x170051FE RID: 20990
		// (get) Token: 0x06011573 RID: 71027 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06011574 RID: 71028 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "stA")]
		public Int32Value StartingOpacity
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

		// Token: 0x170051FF RID: 20991
		// (get) Token: 0x06011575 RID: 71029 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06011576 RID: 71030 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(52, "stPos")]
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

		// Token: 0x17005200 RID: 20992
		// (get) Token: 0x06011577 RID: 71031 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06011578 RID: 71032 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(52, "endA")]
		public Int32Value EndingOpacity
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

		// Token: 0x17005201 RID: 20993
		// (get) Token: 0x06011579 RID: 71033 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x0601157A RID: 71034 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(52, "endPos")]
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

		// Token: 0x17005202 RID: 20994
		// (get) Token: 0x0601157B RID: 71035 RVA: 0x002ED54C File Offset: 0x002EB74C
		// (set) Token: 0x0601157C RID: 71036 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(52, "dist")]
		public Int64Value DistanceFromText
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

		// Token: 0x17005203 RID: 20995
		// (get) Token: 0x0601157D RID: 71037 RVA: 0x002ED380 File Offset: 0x002EB580
		// (set) Token: 0x0601157E RID: 71038 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(52, "dir")]
		public Int32Value DirectionAngle
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

		// Token: 0x17005204 RID: 20996
		// (get) Token: 0x0601157F RID: 71039 RVA: 0x002D14EB File Offset: 0x002CF6EB
		// (set) Token: 0x06011580 RID: 71040 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(52, "fadeDir")]
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

		// Token: 0x17005205 RID: 20997
		// (get) Token: 0x06011581 RID: 71041 RVA: 0x002ED55B File Offset: 0x002EB75B
		// (set) Token: 0x06011582 RID: 71042 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(52, "sx")]
		public Int32Value HorizontalScalingFactor
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

		// Token: 0x17005206 RID: 20998
		// (get) Token: 0x06011583 RID: 71043 RVA: 0x002D14FA File Offset: 0x002CF6FA
		// (set) Token: 0x06011584 RID: 71044 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(52, "sy")]
		public Int32Value VerticalScalingFactor
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

		// Token: 0x17005207 RID: 20999
		// (get) Token: 0x06011585 RID: 71045 RVA: 0x002E7730 File Offset: 0x002E5930
		// (set) Token: 0x06011586 RID: 71046 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(52, "kx")]
		public Int32Value HorizontalSkewAngle
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

		// Token: 0x17005208 RID: 21000
		// (get) Token: 0x06011587 RID: 71047 RVA: 0x002ED56A File Offset: 0x002EB76A
		// (set) Token: 0x06011588 RID: 71048 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(52, "ky")]
		public Int32Value VerticalSkewAngle
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

		// Token: 0x17005209 RID: 21001
		// (get) Token: 0x06011589 RID: 71049 RVA: 0x002ED57A File Offset: 0x002EB77A
		// (set) Token: 0x0601158A RID: 71050 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(52, "algn")]
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

		// Token: 0x0601158C RID: 71052 RVA: 0x002ED58C File Offset: 0x002EB78C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "blurRad" == name)
			{
				return new Int64Value();
			}
			if (52 == namespaceId && "stA" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "stPos" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "endA" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "endPos" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "dist" == name)
			{
				return new Int64Value();
			}
			if (52 == namespaceId && "dir" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "fadeDir" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "sx" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "sy" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "kx" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "ky" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "algn" == name)
			{
				return new EnumValue<RectangleAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601158D RID: 71053 RVA: 0x002ED6D9 File Offset: 0x002EB8D9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Reflection>(deep);
		}

		// Token: 0x0400795F RID: 31071
		private const string tagName = "reflection";

		// Token: 0x04007960 RID: 31072
		private const byte tagNsId = 52;

		// Token: 0x04007961 RID: 31073
		internal const int ElementTypeIdConst = 12855;

		// Token: 0x04007962 RID: 31074
		private static string[] attributeTagNames = new string[]
		{
			"blurRad", "stA", "stPos", "endA", "endPos", "dist", "dir", "fadeDir", "sx", "sy",
			"kx", "ky", "algn"
		};

		// Token: 0x04007963 RID: 31075
		private static byte[] attributeNamespaceIds = new byte[]
		{
			52, 52, 52, 52, 52, 52, 52, 52, 52, 52,
			52, 52, 52
		};
	}
}
