using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Excel
{
	// Token: 0x02002381 RID: 9089
	[ChildElementInfo(typeof(RowSortMapItem))]
	[GeneratedCode("DomGen", "2.0")]
	internal class RowSortMap : OpenXmlCompositeElement
	{
		// Token: 0x17004B50 RID: 19280
		// (get) Token: 0x06010673 RID: 67187 RVA: 0x002E33D0 File Offset: 0x002E15D0
		public override string LocalName
		{
			get
			{
				return "rowSortMap";
			}
		}

		// Token: 0x17004B51 RID: 19281
		// (get) Token: 0x06010674 RID: 67188 RVA: 0x0022706E File Offset: 0x0022526E
		internal override byte NamespaceId
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17004B52 RID: 19282
		// (get) Token: 0x06010675 RID: 67189 RVA: 0x002E33D7 File Offset: 0x002E15D7
		internal override int ElementTypeId
		{
			get
			{
				return 12533;
			}
		}

		// Token: 0x06010676 RID: 67190 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17004B53 RID: 19283
		// (get) Token: 0x06010677 RID: 67191 RVA: 0x002E33DE File Offset: 0x002E15DE
		internal override string[] AttributeTagNames
		{
			get
			{
				return RowSortMap.attributeTagNames;
			}
		}

		// Token: 0x17004B54 RID: 19284
		// (get) Token: 0x06010678 RID: 67192 RVA: 0x002E33E5 File Offset: 0x002E15E5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RowSortMap.attributeNamespaceIds;
			}
		}

		// Token: 0x17004B55 RID: 19285
		// (get) Token: 0x06010679 RID: 67193 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601067A RID: 67194 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004B56 RID: 19286
		// (get) Token: 0x0601067B RID: 67195 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601067C RID: 67196 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x0601067D RID: 67197 RVA: 0x00293ECF File Offset: 0x002920CF
		public RowSortMap()
		{
		}

		// Token: 0x0601067E RID: 67198 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RowSortMap(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601067F RID: 67199 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RowSortMap(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010680 RID: 67200 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RowSortMap(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010681 RID: 67201 RVA: 0x002E33FB File Offset: 0x002E15FB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (32 == namespaceId && "row" == name)
			{
				return new RowSortMapItem();
			}
			return null;
		}

		// Token: 0x06010682 RID: 67202 RVA: 0x002E3416 File Offset: 0x002E1616
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

		// Token: 0x06010683 RID: 67203 RVA: 0x002E344C File Offset: 0x002E164C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowSortMap>(deep);
		}

		// Token: 0x06010684 RID: 67204 RVA: 0x002E3458 File Offset: 0x002E1658
		// Note: this type is marked as 'beforefieldinit'.
		static RowSortMap()
		{
			byte[] array = new byte[2];
			RowSortMap.attributeNamespaceIds = array;
		}

		// Token: 0x04007474 RID: 29812
		private const string tagName = "rowSortMap";

		// Token: 0x04007475 RID: 29813
		private const byte tagNsId = 32;

		// Token: 0x04007476 RID: 29814
		internal const int ElementTypeIdConst = 12533;

		// Token: 0x04007477 RID: 29815
		private static string[] attributeTagNames = new string[] { "ref", "count" };

		// Token: 0x04007478 RID: 29816
		private static byte[] attributeNamespaceIds;
	}
}
