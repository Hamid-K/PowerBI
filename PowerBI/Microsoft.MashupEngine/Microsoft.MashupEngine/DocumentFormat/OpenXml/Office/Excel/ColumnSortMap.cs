using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Excel
{
	// Token: 0x02002382 RID: 9090
	[ChildElementInfo(typeof(ColumnSortMapItem))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnSortMap : OpenXmlCompositeElement
	{
		// Token: 0x17004B57 RID: 19287
		// (get) Token: 0x06010685 RID: 67205 RVA: 0x002E348F File Offset: 0x002E168F
		public override string LocalName
		{
			get
			{
				return "colSortMap";
			}
		}

		// Token: 0x17004B58 RID: 19288
		// (get) Token: 0x06010686 RID: 67206 RVA: 0x0022706E File Offset: 0x0022526E
		internal override byte NamespaceId
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17004B59 RID: 19289
		// (get) Token: 0x06010687 RID: 67207 RVA: 0x002E3496 File Offset: 0x002E1696
		internal override int ElementTypeId
		{
			get
			{
				return 12534;
			}
		}

		// Token: 0x06010688 RID: 67208 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004B5A RID: 19290
		// (get) Token: 0x06010689 RID: 67209 RVA: 0x002E349D File Offset: 0x002E169D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColumnSortMap.attributeTagNames;
			}
		}

		// Token: 0x17004B5B RID: 19291
		// (get) Token: 0x0601068A RID: 67210 RVA: 0x002E34A4 File Offset: 0x002E16A4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColumnSortMap.attributeNamespaceIds;
			}
		}

		// Token: 0x17004B5C RID: 19292
		// (get) Token: 0x0601068B RID: 67211 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601068C RID: 67212 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Ref
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

		// Token: 0x17004B5D RID: 19293
		// (get) Token: 0x0601068D RID: 67213 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601068E RID: 67214 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x0601068F RID: 67215 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColumnSortMap()
		{
		}

		// Token: 0x06010690 RID: 67216 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColumnSortMap(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010691 RID: 67217 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColumnSortMap(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010692 RID: 67218 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColumnSortMap(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010693 RID: 67219 RVA: 0x002E34AB File Offset: 0x002E16AB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (32 == namespaceId && "col" == name)
			{
				return new ColumnSortMapItem();
			}
			return null;
		}

		// Token: 0x06010694 RID: 67220 RVA: 0x002E3416 File Offset: 0x002E1616
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010695 RID: 67221 RVA: 0x002E34C6 File Offset: 0x002E16C6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnSortMap>(deep);
		}

		// Token: 0x06010696 RID: 67222 RVA: 0x002E34D0 File Offset: 0x002E16D0
		// Note: this type is marked as 'beforefieldinit'.
		static ColumnSortMap()
		{
			byte[] array = new byte[2];
			ColumnSortMap.attributeNamespaceIds = array;
		}

		// Token: 0x04007479 RID: 29817
		private const string tagName = "colSortMap";

		// Token: 0x0400747A RID: 29818
		private const byte tagNsId = 32;

		// Token: 0x0400747B RID: 29819
		internal const int ElementTypeIdConst = 12534;

		// Token: 0x0400747C RID: 29820
		private static string[] attributeTagNames = new string[] { "ref", "count" };

		// Token: 0x0400747D RID: 29821
		private static byte[] attributeNamespaceIds;
	}
}
