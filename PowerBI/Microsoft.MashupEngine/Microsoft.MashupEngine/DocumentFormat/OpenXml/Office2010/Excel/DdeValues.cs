using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200242C RID: 9260
	[ChildElementInfo(typeof(Value))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DdeValues : OpenXmlCompositeElement
	{
		// Token: 0x17004FB6 RID: 20406
		// (get) Token: 0x06011044 RID: 69700 RVA: 0x002E9B46 File Offset: 0x002E7D46
		public override string LocalName
		{
			get
			{
				return "values";
			}
		}

		// Token: 0x17004FB7 RID: 20407
		// (get) Token: 0x06011045 RID: 69701 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004FB8 RID: 20408
		// (get) Token: 0x06011046 RID: 69702 RVA: 0x002E9B4D File Offset: 0x002E7D4D
		internal override int ElementTypeId
		{
			get
			{
				return 12984;
			}
		}

		// Token: 0x06011047 RID: 69703 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004FB9 RID: 20409
		// (get) Token: 0x06011048 RID: 69704 RVA: 0x002E9B54 File Offset: 0x002E7D54
		internal override string[] AttributeTagNames
		{
			get
			{
				return DdeValues.attributeTagNames;
			}
		}

		// Token: 0x17004FBA RID: 20410
		// (get) Token: 0x06011049 RID: 69705 RVA: 0x002E9B5B File Offset: 0x002E7D5B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DdeValues.attributeNamespaceIds;
			}
		}

		// Token: 0x17004FBB RID: 20411
		// (get) Token: 0x0601104A RID: 69706 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601104B RID: 69707 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rows")]
		public UInt32Value Rows
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

		// Token: 0x17004FBC RID: 20412
		// (get) Token: 0x0601104C RID: 69708 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601104D RID: 69709 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cols")]
		public UInt32Value Columns
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601104E RID: 69710 RVA: 0x00293ECF File Offset: 0x002920CF
		public DdeValues()
		{
		}

		// Token: 0x0601104F RID: 69711 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DdeValues(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011050 RID: 69712 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DdeValues(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011051 RID: 69713 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DdeValues(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011052 RID: 69714 RVA: 0x002E9B62 File Offset: 0x002E7D62
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "value" == name)
			{
				return new Value();
			}
			return null;
		}

		// Token: 0x06011053 RID: 69715 RVA: 0x002E9B7D File Offset: 0x002E7D7D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rows" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "cols" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011054 RID: 69716 RVA: 0x002E9BB3 File Offset: 0x002E7DB3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DdeValues>(deep);
		}

		// Token: 0x06011055 RID: 69717 RVA: 0x002E9BBC File Offset: 0x002E7DBC
		// Note: this type is marked as 'beforefieldinit'.
		static DdeValues()
		{
			byte[] array = new byte[2];
			DdeValues.attributeNamespaceIds = array;
		}

		// Token: 0x0400774B RID: 30539
		private const string tagName = "values";

		// Token: 0x0400774C RID: 30540
		private const byte tagNsId = 53;

		// Token: 0x0400774D RID: 30541
		internal const int ElementTypeIdConst = 12984;

		// Token: 0x0400774E RID: 30542
		private static string[] attributeTagNames = new string[] { "rows", "cols" };

		// Token: 0x0400774F RID: 30543
		private static byte[] attributeNamespaceIds;
	}
}
