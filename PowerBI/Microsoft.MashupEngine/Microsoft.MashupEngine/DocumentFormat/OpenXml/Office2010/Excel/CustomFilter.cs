using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002433 RID: 9267
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CustomFilter : OpenXmlLeafElement
	{
		// Token: 0x17005004 RID: 20484
		// (get) Token: 0x060110EC RID: 69868 RVA: 0x002EA2AA File Offset: 0x002E84AA
		public override string LocalName
		{
			get
			{
				return "customFilter";
			}
		}

		// Token: 0x17005005 RID: 20485
		// (get) Token: 0x060110ED RID: 69869 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17005006 RID: 20486
		// (get) Token: 0x060110EE RID: 69870 RVA: 0x002EA2B1 File Offset: 0x002E84B1
		internal override int ElementTypeId
		{
			get
			{
				return 12991;
			}
		}

		// Token: 0x060110EF RID: 69871 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005007 RID: 20487
		// (get) Token: 0x060110F0 RID: 69872 RVA: 0x002EA2B8 File Offset: 0x002E84B8
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomFilter.attributeTagNames;
			}
		}

		// Token: 0x17005008 RID: 20488
		// (get) Token: 0x060110F1 RID: 69873 RVA: 0x002EA2BF File Offset: 0x002E84BF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomFilter.attributeNamespaceIds;
			}
		}

		// Token: 0x17005009 RID: 20489
		// (get) Token: 0x060110F2 RID: 69874 RVA: 0x002EA2C6 File Offset: 0x002E84C6
		// (set) Token: 0x060110F3 RID: 69875 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700500A RID: 20490
		// (get) Token: 0x060110F4 RID: 69876 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060110F5 RID: 69877 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x060110F7 RID: 69879 RVA: 0x002EA2D5 File Offset: 0x002E84D5
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

		// Token: 0x060110F8 RID: 69880 RVA: 0x002EA30B File Offset: 0x002E850B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomFilter>(deep);
		}

		// Token: 0x060110F9 RID: 69881 RVA: 0x002EA314 File Offset: 0x002E8514
		// Note: this type is marked as 'beforefieldinit'.
		static CustomFilter()
		{
			byte[] array = new byte[2];
			CustomFilter.attributeNamespaceIds = array;
		}

		// Token: 0x04007776 RID: 30582
		private const string tagName = "customFilter";

		// Token: 0x04007777 RID: 30583
		private const byte tagNsId = 53;

		// Token: 0x04007778 RID: 30584
		internal const int ElementTypeIdConst = 12991;

		// Token: 0x04007779 RID: 30585
		private static string[] attributeTagNames = new string[] { "operator", "val" };

		// Token: 0x0400777A RID: 30586
		private static byte[] attributeNamespaceIds;
	}
}
