using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CA4 RID: 11428
	[ChildElementInfo(typeof(TablePart))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableParts : OpenXmlCompositeElement
	{
		// Token: 0x17008445 RID: 33861
		// (get) Token: 0x0601868B RID: 99979 RVA: 0x0034150C File Offset: 0x0033F70C
		public override string LocalName
		{
			get
			{
				return "tableParts";
			}
		}

		// Token: 0x17008446 RID: 33862
		// (get) Token: 0x0601868C RID: 99980 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008447 RID: 33863
		// (get) Token: 0x0601868D RID: 99981 RVA: 0x00341513 File Offset: 0x0033F713
		internal override int ElementTypeId
		{
			get
			{
				return 11408;
			}
		}

		// Token: 0x0601868E RID: 99982 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008448 RID: 33864
		// (get) Token: 0x0601868F RID: 99983 RVA: 0x0034151A File Offset: 0x0033F71A
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableParts.attributeTagNames;
			}
		}

		// Token: 0x17008449 RID: 33865
		// (get) Token: 0x06018690 RID: 99984 RVA: 0x00341521 File Offset: 0x0033F721
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableParts.attributeNamespaceIds;
			}
		}

		// Token: 0x1700844A RID: 33866
		// (get) Token: 0x06018691 RID: 99985 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018692 RID: 99986 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018693 RID: 99987 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableParts()
		{
		}

		// Token: 0x06018694 RID: 99988 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableParts(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018695 RID: 99989 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableParts(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018696 RID: 99990 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableParts(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018697 RID: 99991 RVA: 0x00341528 File Offset: 0x0033F728
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tablePart" == name)
			{
				return new TablePart();
			}
			return null;
		}

		// Token: 0x06018698 RID: 99992 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018699 RID: 99993 RVA: 0x00341543 File Offset: 0x0033F743
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableParts>(deep);
		}

		// Token: 0x0601869A RID: 99994 RVA: 0x0034154C File Offset: 0x0033F74C
		// Note: this type is marked as 'beforefieldinit'.
		static TableParts()
		{
			byte[] array = new byte[1];
			TableParts.attributeNamespaceIds = array;
		}

		// Token: 0x0400A021 RID: 40993
		private const string tagName = "tableParts";

		// Token: 0x0400A022 RID: 40994
		private const byte tagNsId = 22;

		// Token: 0x0400A023 RID: 40995
		internal const int ElementTypeIdConst = 11408;

		// Token: 0x0400A024 RID: 40996
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A025 RID: 40997
		private static byte[] attributeNamespaceIds;
	}
}
