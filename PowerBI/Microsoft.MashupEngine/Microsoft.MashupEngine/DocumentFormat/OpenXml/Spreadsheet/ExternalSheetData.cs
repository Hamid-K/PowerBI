using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C1E RID: 11294
	[ChildElementInfo(typeof(ExternalRow))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExternalSheetData : OpenXmlCompositeElement
	{
		// Token: 0x17008055 RID: 32853
		// (get) Token: 0x06017D56 RID: 97622 RVA: 0x0033BA9B File Offset: 0x00339C9B
		public override string LocalName
		{
			get
			{
				return "sheetData";
			}
		}

		// Token: 0x17008056 RID: 32854
		// (get) Token: 0x06017D57 RID: 97623 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008057 RID: 32855
		// (get) Token: 0x06017D58 RID: 97624 RVA: 0x0033BAA2 File Offset: 0x00339CA2
		internal override int ElementTypeId
		{
			get
			{
				return 11275;
			}
		}

		// Token: 0x06017D59 RID: 97625 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008058 RID: 32856
		// (get) Token: 0x06017D5A RID: 97626 RVA: 0x0033BAA9 File Offset: 0x00339CA9
		internal override string[] AttributeTagNames
		{
			get
			{
				return ExternalSheetData.attributeTagNames;
			}
		}

		// Token: 0x17008059 RID: 32857
		// (get) Token: 0x06017D5B RID: 97627 RVA: 0x0033BAB0 File Offset: 0x00339CB0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ExternalSheetData.attributeNamespaceIds;
			}
		}

		// Token: 0x1700805A RID: 32858
		// (get) Token: 0x06017D5C RID: 97628 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017D5D RID: 97629 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "sheetId")]
		public UInt32Value SheetId
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

		// Token: 0x1700805B RID: 32859
		// (get) Token: 0x06017D5E RID: 97630 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017D5F RID: 97631 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "refreshError")]
		public BooleanValue RefreshError
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06017D60 RID: 97632 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExternalSheetData()
		{
		}

		// Token: 0x06017D61 RID: 97633 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExternalSheetData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D62 RID: 97634 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExternalSheetData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D63 RID: 97635 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExternalSheetData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017D64 RID: 97636 RVA: 0x0033BAB7 File Offset: 0x00339CB7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "row" == name)
			{
				return new ExternalRow();
			}
			return null;
		}

		// Token: 0x06017D65 RID: 97637 RVA: 0x0033BAD2 File Offset: 0x00339CD2
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "refreshError" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017D66 RID: 97638 RVA: 0x0033BB08 File Offset: 0x00339D08
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExternalSheetData>(deep);
		}

		// Token: 0x06017D67 RID: 97639 RVA: 0x0033BB14 File Offset: 0x00339D14
		// Note: this type is marked as 'beforefieldinit'.
		static ExternalSheetData()
		{
			byte[] array = new byte[2];
			ExternalSheetData.attributeNamespaceIds = array;
		}

		// Token: 0x04009DC3 RID: 40387
		private const string tagName = "sheetData";

		// Token: 0x04009DC4 RID: 40388
		private const byte tagNsId = 22;

		// Token: 0x04009DC5 RID: 40389
		internal const int ElementTypeIdConst = 11275;

		// Token: 0x04009DC6 RID: 40390
		private static string[] attributeTagNames = new string[] { "sheetId", "refreshError" };

		// Token: 0x04009DC7 RID: 40391
		private static byte[] attributeNamespaceIds;
	}
}
