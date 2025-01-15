using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F04 RID: 12036
	[GeneratedCode("DomGen", "2.0")]
	internal class TableOverlap : OpenXmlLeafElement
	{
		// Token: 0x17008DE2 RID: 36322
		// (get) Token: 0x06019B16 RID: 105238 RVA: 0x00353FD3 File Offset: 0x003521D3
		public override string LocalName
		{
			get
			{
				return "tblOverlap";
			}
		}

		// Token: 0x17008DE3 RID: 36323
		// (get) Token: 0x06019B17 RID: 105239 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DE4 RID: 36324
		// (get) Token: 0x06019B18 RID: 105240 RVA: 0x00353FDA File Offset: 0x003521DA
		internal override int ElementTypeId
		{
			get
			{
				return 11673;
			}
		}

		// Token: 0x06019B19 RID: 105241 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008DE5 RID: 36325
		// (get) Token: 0x06019B1A RID: 105242 RVA: 0x00353FE1 File Offset: 0x003521E1
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableOverlap.attributeTagNames;
			}
		}

		// Token: 0x17008DE6 RID: 36326
		// (get) Token: 0x06019B1B RID: 105243 RVA: 0x00353FE8 File Offset: 0x003521E8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableOverlap.attributeNamespaceIds;
			}
		}

		// Token: 0x17008DE7 RID: 36327
		// (get) Token: 0x06019B1C RID: 105244 RVA: 0x00353FEF File Offset: 0x003521EF
		// (set) Token: 0x06019B1D RID: 105245 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<TableOverlapValues> Val
		{
			get
			{
				return (EnumValue<TableOverlapValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019B1F RID: 105247 RVA: 0x00353FFE File Offset: 0x003521FE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<TableOverlapValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019B20 RID: 105248 RVA: 0x00354020 File Offset: 0x00352220
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableOverlap>(deep);
		}

		// Token: 0x0400AA25 RID: 43557
		private const string tagName = "tblOverlap";

		// Token: 0x0400AA26 RID: 43558
		private const byte tagNsId = 23;

		// Token: 0x0400AA27 RID: 43559
		internal const int ElementTypeIdConst = 11673;

		// Token: 0x0400AA28 RID: 43560
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AA29 RID: 43561
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
