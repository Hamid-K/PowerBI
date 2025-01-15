using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027F6 RID: 10230
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(TableCell))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class TableRow : OpenXmlCompositeElement
	{
		// Token: 0x170064F6 RID: 25846
		// (get) Token: 0x06013FC6 RID: 81862 RVA: 0x0030E261 File Offset: 0x0030C461
		public override string LocalName
		{
			get
			{
				return "tr";
			}
		}

		// Token: 0x170064F7 RID: 25847
		// (get) Token: 0x06013FC7 RID: 81863 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170064F8 RID: 25848
		// (get) Token: 0x06013FC8 RID: 81864 RVA: 0x0030E268 File Offset: 0x0030C468
		internal override int ElementTypeId
		{
			get
			{
				return 10266;
			}
		}

		// Token: 0x06013FC9 RID: 81865 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170064F9 RID: 25849
		// (get) Token: 0x06013FCA RID: 81866 RVA: 0x0030E26F File Offset: 0x0030C46F
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableRow.attributeTagNames;
			}
		}

		// Token: 0x170064FA RID: 25850
		// (get) Token: 0x06013FCB RID: 81867 RVA: 0x0030E276 File Offset: 0x0030C476
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableRow.attributeNamespaceIds;
			}
		}

		// Token: 0x170064FB RID: 25851
		// (get) Token: 0x06013FCC RID: 81868 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06013FCD RID: 81869 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "h")]
		public Int64Value Height
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06013FCE RID: 81870 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableRow()
		{
		}

		// Token: 0x06013FCF RID: 81871 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableRow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FD0 RID: 81872 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableRow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013FD1 RID: 81873 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableRow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013FD2 RID: 81874 RVA: 0x0030E27D File Offset: 0x0030C47D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "tc" == name)
			{
				return new TableCell();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06013FD3 RID: 81875 RVA: 0x0030E2B0 File Offset: 0x0030C4B0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "h" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013FD4 RID: 81876 RVA: 0x0030E2D0 File Offset: 0x0030C4D0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableRow>(deep);
		}

		// Token: 0x06013FD5 RID: 81877 RVA: 0x0030E2DC File Offset: 0x0030C4DC
		// Note: this type is marked as 'beforefieldinit'.
		static TableRow()
		{
			byte[] array = new byte[1];
			TableRow.attributeNamespaceIds = array;
		}

		// Token: 0x04008885 RID: 34949
		private const string tagName = "tr";

		// Token: 0x04008886 RID: 34950
		private const byte tagNsId = 10;

		// Token: 0x04008887 RID: 34951
		internal const int ElementTypeIdConst = 10266;

		// Token: 0x04008888 RID: 34952
		private static string[] attributeTagNames = new string[] { "h" };

		// Token: 0x04008889 RID: 34953
		private static byte[] attributeNamespaceIds;
	}
}
