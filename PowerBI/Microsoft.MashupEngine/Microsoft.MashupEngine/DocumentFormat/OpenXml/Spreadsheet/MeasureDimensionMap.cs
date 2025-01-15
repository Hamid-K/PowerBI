using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B84 RID: 11140
	[GeneratedCode("DomGen", "2.0")]
	internal class MeasureDimensionMap : OpenXmlLeafElement
	{
		// Token: 0x17007A59 RID: 31321
		// (get) Token: 0x06017082 RID: 94338 RVA: 0x00331E77 File Offset: 0x00330077
		public override string LocalName
		{
			get
			{
				return "map";
			}
		}

		// Token: 0x17007A5A RID: 31322
		// (get) Token: 0x06017083 RID: 94339 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A5B RID: 31323
		// (get) Token: 0x06017084 RID: 94340 RVA: 0x00331E7E File Offset: 0x0033007E
		internal override int ElementTypeId
		{
			get
			{
				return 11118;
			}
		}

		// Token: 0x06017085 RID: 94341 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A5C RID: 31324
		// (get) Token: 0x06017086 RID: 94342 RVA: 0x00331E85 File Offset: 0x00330085
		internal override string[] AttributeTagNames
		{
			get
			{
				return MeasureDimensionMap.attributeTagNames;
			}
		}

		// Token: 0x17007A5D RID: 31325
		// (get) Token: 0x06017087 RID: 94343 RVA: 0x00331E8C File Offset: 0x0033008C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MeasureDimensionMap.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A5E RID: 31326
		// (get) Token: 0x06017088 RID: 94344 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017089 RID: 94345 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "measureGroup")]
		public UInt32Value MeasureGroup
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007A5F RID: 31327
		// (get) Token: 0x0601708A RID: 94346 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601708B RID: 94347 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dimension")]
		public UInt32Value Dimension
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601708D RID: 94349 RVA: 0x00331E93 File Offset: 0x00330093
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "measureGroup" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "dimension" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601708E RID: 94350 RVA: 0x00331EC9 File Offset: 0x003300C9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MeasureDimensionMap>(deep);
		}

		// Token: 0x0601708F RID: 94351 RVA: 0x00331ED4 File Offset: 0x003300D4
		// Note: this type is marked as 'beforefieldinit'.
		static MeasureDimensionMap()
		{
			byte[] array = new byte[2];
			MeasureDimensionMap.attributeNamespaceIds = array;
		}

		// Token: 0x04009AD5 RID: 39637
		private const string tagName = "map";

		// Token: 0x04009AD6 RID: 39638
		private const byte tagNsId = 22;

		// Token: 0x04009AD7 RID: 39639
		internal const int ElementTypeIdConst = 11118;

		// Token: 0x04009AD8 RID: 39640
		private static string[] attributeTagNames = new string[] { "measureGroup", "dimension" };

		// Token: 0x04009AD9 RID: 39641
		private static byte[] attributeNamespaceIds;
	}
}
