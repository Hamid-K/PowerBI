using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027F3 RID: 10227
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class GridColumn : OpenXmlCompositeElement
	{
		// Token: 0x170064DD RID: 25821
		// (get) Token: 0x06013F8B RID: 81803 RVA: 0x0030DF38 File Offset: 0x0030C138
		public override string LocalName
		{
			get
			{
				return "gridCol";
			}
		}

		// Token: 0x170064DE RID: 25822
		// (get) Token: 0x06013F8C RID: 81804 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170064DF RID: 25823
		// (get) Token: 0x06013F8D RID: 81805 RVA: 0x0030DF3F File Offset: 0x0030C13F
		internal override int ElementTypeId
		{
			get
			{
				return 10263;
			}
		}

		// Token: 0x06013F8E RID: 81806 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170064E0 RID: 25824
		// (get) Token: 0x06013F8F RID: 81807 RVA: 0x0030DF46 File Offset: 0x0030C146
		internal override string[] AttributeTagNames
		{
			get
			{
				return GridColumn.attributeTagNames;
			}
		}

		// Token: 0x170064E1 RID: 25825
		// (get) Token: 0x06013F90 RID: 81808 RVA: 0x0030DF4D File Offset: 0x0030C14D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GridColumn.attributeNamespaceIds;
			}
		}

		// Token: 0x170064E2 RID: 25826
		// (get) Token: 0x06013F91 RID: 81809 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x06013F92 RID: 81810 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "w")]
		public Int64Value Width
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

		// Token: 0x06013F93 RID: 81811 RVA: 0x00293ECF File Offset: 0x002920CF
		public GridColumn()
		{
		}

		// Token: 0x06013F94 RID: 81812 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GridColumn(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F95 RID: 81813 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GridColumn(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013F96 RID: 81814 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GridColumn(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013F97 RID: 81815 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170064E3 RID: 25827
		// (get) Token: 0x06013F98 RID: 81816 RVA: 0x0030DF54 File Offset: 0x0030C154
		internal override string[] ElementTagNames
		{
			get
			{
				return GridColumn.eleTagNames;
			}
		}

		// Token: 0x170064E4 RID: 25828
		// (get) Token: 0x06013F99 RID: 81817 RVA: 0x0030DF5B File Offset: 0x0030C15B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GridColumn.eleNamespaceIds;
			}
		}

		// Token: 0x170064E5 RID: 25829
		// (get) Token: 0x06013F9A RID: 81818 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170064E6 RID: 25830
		// (get) Token: 0x06013F9B RID: 81819 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x06013F9C RID: 81820 RVA: 0x002FA750 File Offset: 0x002F8950
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06013F9D RID: 81821 RVA: 0x0030DF62 File Offset: 0x0030C162
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "w" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013F9E RID: 81822 RVA: 0x0030DF82 File Offset: 0x0030C182
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GridColumn>(deep);
		}

		// Token: 0x06013F9F RID: 81823 RVA: 0x0030DF8C File Offset: 0x0030C18C
		// Note: this type is marked as 'beforefieldinit'.
		static GridColumn()
		{
			byte[] array = new byte[1];
			GridColumn.attributeNamespaceIds = array;
			GridColumn.eleTagNames = new string[] { "extLst" };
			GridColumn.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x04008876 RID: 34934
		private const string tagName = "gridCol";

		// Token: 0x04008877 RID: 34935
		private const byte tagNsId = 10;

		// Token: 0x04008878 RID: 34936
		internal const int ElementTypeIdConst = 10263;

		// Token: 0x04008879 RID: 34937
		private static string[] attributeTagNames = new string[] { "w" };

		// Token: 0x0400887A RID: 34938
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400887B RID: 34939
		private static readonly string[] eleTagNames;

		// Token: 0x0400887C RID: 34940
		private static readonly byte[] eleNamespaceIds;
	}
}
