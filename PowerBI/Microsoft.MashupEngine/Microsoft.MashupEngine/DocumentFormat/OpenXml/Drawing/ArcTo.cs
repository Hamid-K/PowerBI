using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027CE RID: 10190
	[GeneratedCode("DomGen", "2.0")]
	internal class ArcTo : OpenXmlLeafElement
	{
		// Token: 0x170063C2 RID: 25538
		// (get) Token: 0x06013CFE RID: 81150 RVA: 0x0030BEDC File Offset: 0x0030A0DC
		public override string LocalName
		{
			get
			{
				return "arcTo";
			}
		}

		// Token: 0x170063C3 RID: 25539
		// (get) Token: 0x06013CFF RID: 81151 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063C4 RID: 25540
		// (get) Token: 0x06013D00 RID: 81152 RVA: 0x0030BEE3 File Offset: 0x0030A0E3
		internal override int ElementTypeId
		{
			get
			{
				return 10224;
			}
		}

		// Token: 0x06013D01 RID: 81153 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170063C5 RID: 25541
		// (get) Token: 0x06013D02 RID: 81154 RVA: 0x0030BEEA File Offset: 0x0030A0EA
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArcTo.attributeTagNames;
			}
		}

		// Token: 0x170063C6 RID: 25542
		// (get) Token: 0x06013D03 RID: 81155 RVA: 0x0030BEF1 File Offset: 0x0030A0F1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArcTo.attributeNamespaceIds;
			}
		}

		// Token: 0x170063C7 RID: 25543
		// (get) Token: 0x06013D04 RID: 81156 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013D05 RID: 81157 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "wR")]
		public StringValue WidthRadius
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

		// Token: 0x170063C8 RID: 25544
		// (get) Token: 0x06013D06 RID: 81158 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06013D07 RID: 81159 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "hR")]
		public StringValue HeightRadius
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

		// Token: 0x170063C9 RID: 25545
		// (get) Token: 0x06013D08 RID: 81160 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06013D09 RID: 81161 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "stAng")]
		public StringValue StartAngle
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

		// Token: 0x170063CA RID: 25546
		// (get) Token: 0x06013D0A RID: 81162 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06013D0B RID: 81163 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "swAng")]
		public StringValue SwingAngle
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

		// Token: 0x06013D0D RID: 81165 RVA: 0x0030BEF8 File Offset: 0x0030A0F8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "wR" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hR" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "stAng" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "swAng" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013D0E RID: 81166 RVA: 0x0030BF65 File Offset: 0x0030A165
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArcTo>(deep);
		}

		// Token: 0x06013D0F RID: 81167 RVA: 0x0030BF70 File Offset: 0x0030A170
		// Note: this type is marked as 'beforefieldinit'.
		static ArcTo()
		{
			byte[] array = new byte[4];
			ArcTo.attributeNamespaceIds = array;
		}

		// Token: 0x040087E6 RID: 34790
		private const string tagName = "arcTo";

		// Token: 0x040087E7 RID: 34791
		private const byte tagNsId = 10;

		// Token: 0x040087E8 RID: 34792
		internal const int ElementTypeIdConst = 10224;

		// Token: 0x040087E9 RID: 34793
		private static string[] attributeTagNames = new string[] { "wR", "hR", "stAng", "swAng" };

		// Token: 0x040087EA RID: 34794
		private static byte[] attributeNamespaceIds;
	}
}
