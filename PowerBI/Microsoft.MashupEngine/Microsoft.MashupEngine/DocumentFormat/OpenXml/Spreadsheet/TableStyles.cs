using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C79 RID: 11385
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableStyle))]
	internal class TableStyles : OpenXmlCompositeElement
	{
		// Token: 0x170082FF RID: 33535
		// (get) Token: 0x0601838F RID: 99215 RVA: 0x002AD473 File Offset: 0x002AB673
		public override string LocalName
		{
			get
			{
				return "tableStyles";
			}
		}

		// Token: 0x17008300 RID: 33536
		// (get) Token: 0x06018390 RID: 99216 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008301 RID: 33537
		// (get) Token: 0x06018391 RID: 99217 RVA: 0x0033F71F File Offset: 0x0033D91F
		internal override int ElementTypeId
		{
			get
			{
				return 11365;
			}
		}

		// Token: 0x06018392 RID: 99218 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008302 RID: 33538
		// (get) Token: 0x06018393 RID: 99219 RVA: 0x0033F726 File Offset: 0x0033D926
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableStyles.attributeTagNames;
			}
		}

		// Token: 0x17008303 RID: 33539
		// (get) Token: 0x06018394 RID: 99220 RVA: 0x0033F72D File Offset: 0x0033D92D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableStyles.attributeNamespaceIds;
			}
		}

		// Token: 0x17008304 RID: 33540
		// (get) Token: 0x06018395 RID: 99221 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018396 RID: 99222 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008305 RID: 33541
		// (get) Token: 0x06018397 RID: 99223 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06018398 RID: 99224 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "defaultTableStyle")]
		public StringValue DefaultTableStyle
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

		// Token: 0x17008306 RID: 33542
		// (get) Token: 0x06018399 RID: 99225 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601839A RID: 99226 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "defaultPivotStyle")]
		public StringValue DefaultPivotStyle
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601839B RID: 99227 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableStyles()
		{
		}

		// Token: 0x0601839C RID: 99228 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableStyles(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601839D RID: 99229 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableStyles(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601839E RID: 99230 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableStyles(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601839F RID: 99231 RVA: 0x0033F734 File Offset: 0x0033D934
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tableStyle" == name)
			{
				return new TableStyle();
			}
			return null;
		}

		// Token: 0x060183A0 RID: 99232 RVA: 0x0033F750 File Offset: 0x0033D950
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "defaultTableStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "defaultPivotStyle" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060183A1 RID: 99233 RVA: 0x0033F7A7 File Offset: 0x0033D9A7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableStyles>(deep);
		}

		// Token: 0x060183A2 RID: 99234 RVA: 0x0033F7B0 File Offset: 0x0033D9B0
		// Note: this type is marked as 'beforefieldinit'.
		static TableStyles()
		{
			byte[] array = new byte[3];
			TableStyles.attributeNamespaceIds = array;
		}

		// Token: 0x04009F69 RID: 40809
		private const string tagName = "tableStyles";

		// Token: 0x04009F6A RID: 40810
		private const byte tagNsId = 22;

		// Token: 0x04009F6B RID: 40811
		internal const int ElementTypeIdConst = 11365;

		// Token: 0x04009F6C RID: 40812
		private static string[] attributeTagNames = new string[] { "count", "defaultTableStyle", "defaultPivotStyle" };

		// Token: 0x04009F6D RID: 40813
		private static byte[] attributeNamespaceIds;
	}
}
