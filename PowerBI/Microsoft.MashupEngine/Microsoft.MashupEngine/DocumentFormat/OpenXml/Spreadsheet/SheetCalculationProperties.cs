using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C94 RID: 11412
	[GeneratedCode("DomGen", "2.0")]
	internal class SheetCalculationProperties : OpenXmlLeafElement
	{
		// Token: 0x170083DE RID: 33758
		// (get) Token: 0x06018588 RID: 99720 RVA: 0x00340B9D File Offset: 0x0033ED9D
		public override string LocalName
		{
			get
			{
				return "sheetCalcPr";
			}
		}

		// Token: 0x170083DF RID: 33759
		// (get) Token: 0x06018589 RID: 99721 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170083E0 RID: 33760
		// (get) Token: 0x0601858A RID: 99722 RVA: 0x00340BA4 File Offset: 0x0033EDA4
		internal override int ElementTypeId
		{
			get
			{
				return 11392;
			}
		}

		// Token: 0x0601858B RID: 99723 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170083E1 RID: 33761
		// (get) Token: 0x0601858C RID: 99724 RVA: 0x00340BAB File Offset: 0x0033EDAB
		internal override string[] AttributeTagNames
		{
			get
			{
				return SheetCalculationProperties.attributeTagNames;
			}
		}

		// Token: 0x170083E2 RID: 33762
		// (get) Token: 0x0601858D RID: 99725 RVA: 0x00340BB2 File Offset: 0x0033EDB2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SheetCalculationProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170083E3 RID: 33763
		// (get) Token: 0x0601858E RID: 99726 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601858F RID: 99727 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fullCalcOnLoad")]
		public BooleanValue FullCalculationOnLoad
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06018591 RID: 99729 RVA: 0x00340BB9 File Offset: 0x0033EDB9
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fullCalcOnLoad" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018592 RID: 99730 RVA: 0x00340BD9 File Offset: 0x0033EDD9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetCalculationProperties>(deep);
		}

		// Token: 0x06018593 RID: 99731 RVA: 0x00340BE4 File Offset: 0x0033EDE4
		// Note: this type is marked as 'beforefieldinit'.
		static SheetCalculationProperties()
		{
			byte[] array = new byte[1];
			SheetCalculationProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009FE1 RID: 40929
		private const string tagName = "sheetCalcPr";

		// Token: 0x04009FE2 RID: 40930
		private const byte tagNsId = 22;

		// Token: 0x04009FE3 RID: 40931
		internal const int ElementTypeIdConst = 11392;

		// Token: 0x04009FE4 RID: 40932
		private static string[] attributeTagNames = new string[] { "fullCalcOnLoad" };

		// Token: 0x04009FE5 RID: 40933
		private static byte[] attributeNamespaceIds;
	}
}
