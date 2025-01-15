using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200243B RID: 9275
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SlicerCachePivotTable : OpenXmlLeafElement
	{
		// Token: 0x1700504D RID: 20557
		// (get) Token: 0x0601118B RID: 70027 RVA: 0x002A81F0 File Offset: 0x002A63F0
		public override string LocalName
		{
			get
			{
				return "pivotTable";
			}
		}

		// Token: 0x1700504E RID: 20558
		// (get) Token: 0x0601118C RID: 70028 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x1700504F RID: 20559
		// (get) Token: 0x0601118D RID: 70029 RVA: 0x002EA96B File Offset: 0x002E8B6B
		internal override int ElementTypeId
		{
			get
			{
				return 12999;
			}
		}

		// Token: 0x0601118E RID: 70030 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005050 RID: 20560
		// (get) Token: 0x0601118F RID: 70031 RVA: 0x002EA972 File Offset: 0x002E8B72
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlicerCachePivotTable.attributeTagNames;
			}
		}

		// Token: 0x17005051 RID: 20561
		// (get) Token: 0x06011190 RID: 70032 RVA: 0x002EA979 File Offset: 0x002E8B79
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlicerCachePivotTable.attributeNamespaceIds;
			}
		}

		// Token: 0x17005052 RID: 20562
		// (get) Token: 0x06011191 RID: 70033 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06011192 RID: 70034 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "tabId")]
		public UInt32Value TabId
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

		// Token: 0x17005053 RID: 20563
		// (get) Token: 0x06011193 RID: 70035 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06011194 RID: 70036 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x06011196 RID: 70038 RVA: 0x002EA980 File Offset: 0x002E8B80
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "tabId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011197 RID: 70039 RVA: 0x002EA9B6 File Offset: 0x002E8BB6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerCachePivotTable>(deep);
		}

		// Token: 0x06011198 RID: 70040 RVA: 0x002EA9C0 File Offset: 0x002E8BC0
		// Note: this type is marked as 'beforefieldinit'.
		static SlicerCachePivotTable()
		{
			byte[] array = new byte[2];
			SlicerCachePivotTable.attributeNamespaceIds = array;
		}

		// Token: 0x040077A0 RID: 30624
		private const string tagName = "pivotTable";

		// Token: 0x040077A1 RID: 30625
		private const byte tagNsId = 53;

		// Token: 0x040077A2 RID: 30626
		internal const int ElementTypeIdConst = 12999;

		// Token: 0x040077A3 RID: 30627
		private static string[] attributeTagNames = new string[] { "tabId", "name" };

		// Token: 0x040077A4 RID: 30628
		private static byte[] attributeNamespaceIds;
	}
}
