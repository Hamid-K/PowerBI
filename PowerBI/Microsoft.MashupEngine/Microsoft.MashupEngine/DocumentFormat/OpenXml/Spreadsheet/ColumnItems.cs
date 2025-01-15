using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CB2 RID: 11442
	[ChildElementInfo(typeof(RowItem))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnItems : OpenXmlCompositeElement
	{
		// Token: 0x17008494 RID: 33940
		// (get) Token: 0x0601875B RID: 100187 RVA: 0x00341AC7 File Offset: 0x0033FCC7
		public override string LocalName
		{
			get
			{
				return "colItems";
			}
		}

		// Token: 0x17008495 RID: 33941
		// (get) Token: 0x0601875C RID: 100188 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008496 RID: 33942
		// (get) Token: 0x0601875D RID: 100189 RVA: 0x00341ACE File Offset: 0x0033FCCE
		internal override int ElementTypeId
		{
			get
			{
				return 11422;
			}
		}

		// Token: 0x0601875E RID: 100190 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008497 RID: 33943
		// (get) Token: 0x0601875F RID: 100191 RVA: 0x00341AD5 File Offset: 0x0033FCD5
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColumnItems.attributeTagNames;
			}
		}

		// Token: 0x17008498 RID: 33944
		// (get) Token: 0x06018760 RID: 100192 RVA: 0x00341ADC File Offset: 0x0033FCDC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColumnItems.attributeNamespaceIds;
			}
		}

		// Token: 0x17008499 RID: 33945
		// (get) Token: 0x06018761 RID: 100193 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018762 RID: 100194 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06018763 RID: 100195 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColumnItems()
		{
		}

		// Token: 0x06018764 RID: 100196 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColumnItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018765 RID: 100197 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColumnItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018766 RID: 100198 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColumnItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018767 RID: 100199 RVA: 0x00341A1F File Offset: 0x0033FC1F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "i" == name)
			{
				return new RowItem();
			}
			return null;
		}

		// Token: 0x06018768 RID: 100200 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018769 RID: 100201 RVA: 0x00341AE3 File Offset: 0x0033FCE3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnItems>(deep);
		}

		// Token: 0x0601876A RID: 100202 RVA: 0x00341AEC File Offset: 0x0033FCEC
		// Note: this type is marked as 'beforefieldinit'.
		static ColumnItems()
		{
			byte[] array = new byte[1];
			ColumnItems.attributeNamespaceIds = array;
		}

		// Token: 0x0400A05F RID: 41055
		private const string tagName = "colItems";

		// Token: 0x0400A060 RID: 41056
		private const byte tagNsId = 22;

		// Token: 0x0400A061 RID: 41057
		internal const int ElementTypeIdConst = 11422;

		// Token: 0x0400A062 RID: 41058
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A063 RID: 41059
		private static byte[] attributeNamespaceIds;
	}
}
