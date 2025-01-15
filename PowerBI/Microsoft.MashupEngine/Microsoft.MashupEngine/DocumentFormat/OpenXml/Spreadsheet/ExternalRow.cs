using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C1F RID: 11295
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExternalCell))]
	internal class ExternalRow : OpenXmlCompositeElement
	{
		// Token: 0x1700805C RID: 32860
		// (get) Token: 0x06017D68 RID: 97640 RVA: 0x002E3583 File Offset: 0x002E1783
		public override string LocalName
		{
			get
			{
				return "row";
			}
		}

		// Token: 0x1700805D RID: 32861
		// (get) Token: 0x06017D69 RID: 97641 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700805E RID: 32862
		// (get) Token: 0x06017D6A RID: 97642 RVA: 0x0033BB4B File Offset: 0x00339D4B
		internal override int ElementTypeId
		{
			get
			{
				return 11276;
			}
		}

		// Token: 0x06017D6B RID: 97643 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700805F RID: 32863
		// (get) Token: 0x06017D6C RID: 97644 RVA: 0x0033BB52 File Offset: 0x00339D52
		internal override string[] AttributeTagNames
		{
			get
			{
				return ExternalRow.attributeTagNames;
			}
		}

		// Token: 0x17008060 RID: 32864
		// (get) Token: 0x06017D6D RID: 97645 RVA: 0x0033BB59 File Offset: 0x00339D59
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ExternalRow.attributeNamespaceIds;
			}
		}

		// Token: 0x17008061 RID: 32865
		// (get) Token: 0x06017D6E RID: 97646 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017D6F RID: 97647 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public UInt32Value RowIndex
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

		// Token: 0x06017D70 RID: 97648 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExternalRow()
		{
		}

		// Token: 0x06017D71 RID: 97649 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExternalRow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D72 RID: 97650 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExternalRow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D73 RID: 97651 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExternalRow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017D74 RID: 97652 RVA: 0x0033BB60 File Offset: 0x00339D60
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cell" == name)
			{
				return new ExternalCell();
			}
			return null;
		}

		// Token: 0x06017D75 RID: 97653 RVA: 0x0033BB7B File Offset: 0x00339D7B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017D76 RID: 97654 RVA: 0x0033BB9B File Offset: 0x00339D9B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExternalRow>(deep);
		}

		// Token: 0x06017D77 RID: 97655 RVA: 0x0033BBA4 File Offset: 0x00339DA4
		// Note: this type is marked as 'beforefieldinit'.
		static ExternalRow()
		{
			byte[] array = new byte[1];
			ExternalRow.attributeNamespaceIds = array;
		}

		// Token: 0x04009DC8 RID: 40392
		private const string tagName = "row";

		// Token: 0x04009DC9 RID: 40393
		private const byte tagNsId = 22;

		// Token: 0x04009DCA RID: 40394
		internal const int ElementTypeIdConst = 11276;

		// Token: 0x04009DCB RID: 40395
		private static string[] attributeTagNames = new string[] { "r" };

		// Token: 0x04009DCC RID: 40396
		private static byte[] attributeNamespaceIds;
	}
}
