using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F5D RID: 12125
	[GeneratedCode("DomGen", "2.0")]
	internal class GridColumn : OpenXmlLeafElement
	{
		// Token: 0x17009055 RID: 36949
		// (get) Token: 0x0601A0B9 RID: 106681 RVA: 0x0030DF38 File Offset: 0x0030C138
		public override string LocalName
		{
			get
			{
				return "gridCol";
			}
		}

		// Token: 0x17009056 RID: 36950
		// (get) Token: 0x0601A0BA RID: 106682 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009057 RID: 36951
		// (get) Token: 0x0601A0BB RID: 106683 RVA: 0x0035CCA8 File Offset: 0x0035AEA8
		internal override int ElementTypeId
		{
			get
			{
				return 11781;
			}
		}

		// Token: 0x0601A0BC RID: 106684 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009058 RID: 36952
		// (get) Token: 0x0601A0BD RID: 106685 RVA: 0x0035CCAF File Offset: 0x0035AEAF
		internal override string[] AttributeTagNames
		{
			get
			{
				return GridColumn.attributeTagNames;
			}
		}

		// Token: 0x17009059 RID: 36953
		// (get) Token: 0x0601A0BE RID: 106686 RVA: 0x0035CCB6 File Offset: 0x0035AEB6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GridColumn.attributeNamespaceIds;
			}
		}

		// Token: 0x1700905A RID: 36954
		// (get) Token: 0x0601A0BF RID: 106687 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A0C0 RID: 106688 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "w")]
		public StringValue Width
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

		// Token: 0x0601A0C2 RID: 106690 RVA: 0x0035CCBD File Offset: 0x0035AEBD
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "w" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A0C3 RID: 106691 RVA: 0x0035CCDF File Offset: 0x0035AEDF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GridColumn>(deep);
		}

		// Token: 0x0400AB8E RID: 43918
		private const string tagName = "gridCol";

		// Token: 0x0400AB8F RID: 43919
		private const byte tagNsId = 23;

		// Token: 0x0400AB90 RID: 43920
		internal const int ElementTypeIdConst = 11781;

		// Token: 0x0400AB91 RID: 43921
		private static string[] attributeTagNames = new string[] { "w" };

		// Token: 0x0400AB92 RID: 43922
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
