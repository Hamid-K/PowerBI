using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C8F RID: 11407
	[GeneratedCode("DomGen", "2.0")]
	internal class SheetDimension : OpenXmlLeafElement
	{
		// Token: 0x170083C0 RID: 33728
		// (get) Token: 0x06018540 RID: 99648 RVA: 0x00331D07 File Offset: 0x0032FF07
		public override string LocalName
		{
			get
			{
				return "dimension";
			}
		}

		// Token: 0x170083C1 RID: 33729
		// (get) Token: 0x06018541 RID: 99649 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170083C2 RID: 33730
		// (get) Token: 0x06018542 RID: 99650 RVA: 0x00340914 File Offset: 0x0033EB14
		internal override int ElementTypeId
		{
			get
			{
				return 11387;
			}
		}

		// Token: 0x06018543 RID: 99651 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170083C3 RID: 33731
		// (get) Token: 0x06018544 RID: 99652 RVA: 0x0034091B File Offset: 0x0033EB1B
		internal override string[] AttributeTagNames
		{
			get
			{
				return SheetDimension.attributeTagNames;
			}
		}

		// Token: 0x170083C4 RID: 33732
		// (get) Token: 0x06018545 RID: 99653 RVA: 0x00340922 File Offset: 0x0033EB22
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SheetDimension.attributeNamespaceIds;
			}
		}

		// Token: 0x170083C5 RID: 33733
		// (get) Token: 0x06018546 RID: 99654 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018547 RID: 99655 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x06018549 RID: 99657 RVA: 0x00303BE4 File Offset: 0x00301DE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601854A RID: 99658 RVA: 0x00340929 File Offset: 0x0033EB29
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetDimension>(deep);
		}

		// Token: 0x0601854B RID: 99659 RVA: 0x00340934 File Offset: 0x0033EB34
		// Note: this type is marked as 'beforefieldinit'.
		static SheetDimension()
		{
			byte[] array = new byte[1];
			SheetDimension.attributeNamespaceIds = array;
		}

		// Token: 0x04009FCE RID: 40910
		private const string tagName = "dimension";

		// Token: 0x04009FCF RID: 40911
		private const byte tagNsId = 22;

		// Token: 0x04009FD0 RID: 40912
		internal const int ElementTypeIdConst = 11387;

		// Token: 0x04009FD1 RID: 40913
		private static string[] attributeTagNames = new string[] { "ref" };

		// Token: 0x04009FD2 RID: 40914
		private static byte[] attributeNamespaceIds;
	}
}
