using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026FD RID: 9981
	[GeneratedCode("DomGen", "2.0")]
	internal class Tile : OpenXmlLeafElement
	{
		// Token: 0x17005E56 RID: 24150
		// (get) Token: 0x060130F6 RID: 78070 RVA: 0x00303162 File Offset: 0x00301362
		public override string LocalName
		{
			get
			{
				return "tile";
			}
		}

		// Token: 0x17005E57 RID: 24151
		// (get) Token: 0x060130F7 RID: 78071 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E58 RID: 24152
		// (get) Token: 0x060130F8 RID: 78072 RVA: 0x00303169 File Offset: 0x00301369
		internal override int ElementTypeId
		{
			get
			{
				return 10045;
			}
		}

		// Token: 0x060130F9 RID: 78073 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E59 RID: 24153
		// (get) Token: 0x060130FA RID: 78074 RVA: 0x00303170 File Offset: 0x00301370
		internal override string[] AttributeTagNames
		{
			get
			{
				return Tile.attributeTagNames;
			}
		}

		// Token: 0x17005E5A RID: 24154
		// (get) Token: 0x060130FB RID: 78075 RVA: 0x00303177 File Offset: 0x00301377
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Tile.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E5B RID: 24155
		// (get) Token: 0x060130FC RID: 78076 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060130FD RID: 78077 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "tx")]
		public Int64Value HorizontalOffset
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

		// Token: 0x17005E5C RID: 24156
		// (get) Token: 0x060130FE RID: 78078 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x060130FF RID: 78079 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ty")]
		public Int64Value VerticalOffset
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005E5D RID: 24157
		// (get) Token: 0x06013100 RID: 78080 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06013101 RID: 78081 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "sx")]
		public Int32Value HorizontalRatio
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

		// Token: 0x17005E5E RID: 24158
		// (get) Token: 0x06013102 RID: 78082 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06013103 RID: 78083 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sy")]
		public Int32Value VerticalRatio
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

		// Token: 0x17005E5F RID: 24159
		// (get) Token: 0x06013104 RID: 78084 RVA: 0x0030317E File Offset: 0x0030137E
		// (set) Token: 0x06013105 RID: 78085 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "flip")]
		public EnumValue<TileFlipValues> Flip
		{
			get
			{
				return (EnumValue<TileFlipValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005E60 RID: 24160
		// (get) Token: 0x06013106 RID: 78086 RVA: 0x0030318D File Offset: 0x0030138D
		// (set) Token: 0x06013107 RID: 78087 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "algn")]
		public EnumValue<RectangleAlignmentValues> Alignment
		{
			get
			{
				return (EnumValue<RectangleAlignmentValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x06013109 RID: 78089 RVA: 0x0030319C File Offset: 0x0030139C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "tx" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "ty" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "sx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "sy" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "flip" == name)
			{
				return new EnumValue<TileFlipValues>();
			}
			if (namespaceId == 0 && "algn" == name)
			{
				return new EnumValue<RectangleAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601310A RID: 78090 RVA: 0x00303235 File Offset: 0x00301435
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tile>(deep);
		}

		// Token: 0x0601310B RID: 78091 RVA: 0x00303240 File Offset: 0x00301440
		// Note: this type is marked as 'beforefieldinit'.
		static Tile()
		{
			byte[] array = new byte[6];
			Tile.attributeNamespaceIds = array;
		}

		// Token: 0x0400847B RID: 33915
		private const string tagName = "tile";

		// Token: 0x0400847C RID: 33916
		private const byte tagNsId = 10;

		// Token: 0x0400847D RID: 33917
		internal const int ElementTypeIdConst = 10045;

		// Token: 0x0400847E RID: 33918
		private static string[] attributeTagNames = new string[] { "tx", "ty", "sx", "sy", "flip", "algn" };

		// Token: 0x0400847F RID: 33919
		private static byte[] attributeNamespaceIds;
	}
}
