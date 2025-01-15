using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002684 RID: 9860
	[GeneratedCode("DomGen", "2.0")]
	internal class Direction : OpenXmlLeafElement
	{
		// Token: 0x17005CB9 RID: 23737
		// (get) Token: 0x06012D7D RID: 77181 RVA: 0x002FFF23 File Offset: 0x002FE123
		public override string LocalName
		{
			get
			{
				return "dir";
			}
		}

		// Token: 0x17005CBA RID: 23738
		// (get) Token: 0x06012D7E RID: 77182 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CBB RID: 23739
		// (get) Token: 0x06012D7F RID: 77183 RVA: 0x002FFF2A File Offset: 0x002FE12A
		internal override int ElementTypeId
		{
			get
			{
				return 10675;
			}
		}

		// Token: 0x06012D80 RID: 77184 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CBC RID: 23740
		// (get) Token: 0x06012D81 RID: 77185 RVA: 0x002FFF31 File Offset: 0x002FE131
		internal override string[] AttributeTagNames
		{
			get
			{
				return Direction.attributeTagNames;
			}
		}

		// Token: 0x17005CBD RID: 23741
		// (get) Token: 0x06012D82 RID: 77186 RVA: 0x002FFF38 File Offset: 0x002FE138
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Direction.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CBE RID: 23742
		// (get) Token: 0x06012D83 RID: 77187 RVA: 0x002FFF3F File Offset: 0x002FE13F
		// (set) Token: 0x06012D84 RID: 77188 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<DirectionValues> Val
		{
			get
			{
				return (EnumValue<DirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012D86 RID: 77190 RVA: 0x002FFF4E File Offset: 0x002FE14E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<DirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012D87 RID: 77191 RVA: 0x002FFF6E File Offset: 0x002FE16E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Direction>(deep);
		}

		// Token: 0x06012D88 RID: 77192 RVA: 0x002FFF78 File Offset: 0x002FE178
		// Note: this type is marked as 'beforefieldinit'.
		static Direction()
		{
			byte[] array = new byte[1];
			Direction.attributeNamespaceIds = array;
		}

		// Token: 0x040081DA RID: 33242
		private const string tagName = "dir";

		// Token: 0x040081DB RID: 33243
		private const byte tagNsId = 14;

		// Token: 0x040081DC RID: 33244
		internal const int ElementTypeIdConst = 10675;

		// Token: 0x040081DD RID: 33245
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040081DE RID: 33246
		private static byte[] attributeNamespaceIds;
	}
}
