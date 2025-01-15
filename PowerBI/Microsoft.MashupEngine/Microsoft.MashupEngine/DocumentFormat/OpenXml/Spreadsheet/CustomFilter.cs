using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B2E RID: 11054
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomFilter : OpenXmlLeafElement
	{
		// Token: 0x1700775A RID: 30554
		// (get) Token: 0x060169E5 RID: 92645 RVA: 0x002EA2AA File Offset: 0x002E84AA
		public override string LocalName
		{
			get
			{
				return "customFilter";
			}
		}

		// Token: 0x1700775B RID: 30555
		// (get) Token: 0x060169E6 RID: 92646 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700775C RID: 30556
		// (get) Token: 0x060169E7 RID: 92647 RVA: 0x0032D47B File Offset: 0x0032B67B
		internal override int ElementTypeId
		{
			get
			{
				return 11052;
			}
		}

		// Token: 0x060169E8 RID: 92648 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700775D RID: 30557
		// (get) Token: 0x060169E9 RID: 92649 RVA: 0x0032D482 File Offset: 0x0032B682
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomFilter.attributeTagNames;
			}
		}

		// Token: 0x1700775E RID: 30558
		// (get) Token: 0x060169EA RID: 92650 RVA: 0x0032D489 File Offset: 0x0032B689
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomFilter.attributeNamespaceIds;
			}
		}

		// Token: 0x1700775F RID: 30559
		// (get) Token: 0x060169EB RID: 92651 RVA: 0x002EA2C6 File Offset: 0x002E84C6
		// (set) Token: 0x060169EC RID: 92652 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "operator")]
		public EnumValue<FilterOperatorValues> Operator
		{
			get
			{
				return (EnumValue<FilterOperatorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007760 RID: 30560
		// (get) Token: 0x060169ED RID: 92653 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060169EE RID: 92654 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "val")]
		public StringValue Val
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

		// Token: 0x060169F0 RID: 92656 RVA: 0x002EA2D5 File Offset: 0x002E84D5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "operator" == name)
			{
				return new EnumValue<FilterOperatorValues>();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060169F1 RID: 92657 RVA: 0x0032D490 File Offset: 0x0032B690
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomFilter>(deep);
		}

		// Token: 0x060169F2 RID: 92658 RVA: 0x0032D49C File Offset: 0x0032B69C
		// Note: this type is marked as 'beforefieldinit'.
		static CustomFilter()
		{
			byte[] array = new byte[2];
			CustomFilter.attributeNamespaceIds = array;
		}

		// Token: 0x04009943 RID: 39235
		private const string tagName = "customFilter";

		// Token: 0x04009944 RID: 39236
		private const byte tagNsId = 22;

		// Token: 0x04009945 RID: 39237
		internal const int ElementTypeIdConst = 11052;

		// Token: 0x04009946 RID: 39238
		private static string[] attributeTagNames = new string[] { "operator", "val" };

		// Token: 0x04009947 RID: 39239
		private static byte[] attributeNamespaceIds;
	}
}
