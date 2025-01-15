using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F5E RID: 12126
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PreviousTableGrid))]
	internal class TableGridChange : OpenXmlCompositeElement
	{
		// Token: 0x1700905B RID: 36955
		// (get) Token: 0x0601A0C5 RID: 106693 RVA: 0x0035CD1C File Offset: 0x0035AF1C
		public override string LocalName
		{
			get
			{
				return "tblGridChange";
			}
		}

		// Token: 0x1700905C RID: 36956
		// (get) Token: 0x0601A0C6 RID: 106694 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700905D RID: 36957
		// (get) Token: 0x0601A0C7 RID: 106695 RVA: 0x0035CD23 File Offset: 0x0035AF23
		internal override int ElementTypeId
		{
			get
			{
				return 11782;
			}
		}

		// Token: 0x0601A0C8 RID: 106696 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700905E RID: 36958
		// (get) Token: 0x0601A0C9 RID: 106697 RVA: 0x0035CD2A File Offset: 0x0035AF2A
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableGridChange.attributeTagNames;
			}
		}

		// Token: 0x1700905F RID: 36959
		// (get) Token: 0x0601A0CA RID: 106698 RVA: 0x0035CD31 File Offset: 0x0035AF31
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableGridChange.attributeNamespaceIds;
			}
		}

		// Token: 0x17009060 RID: 36960
		// (get) Token: 0x0601A0CB RID: 106699 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A0CC RID: 106700 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "id")]
		public StringValue Id
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

		// Token: 0x0601A0CD RID: 106701 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableGridChange()
		{
		}

		// Token: 0x0601A0CE RID: 106702 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableGridChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A0CF RID: 106703 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableGridChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A0D0 RID: 106704 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableGridChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A0D1 RID: 106705 RVA: 0x0035CD38 File Offset: 0x0035AF38
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tblGrid" == name)
			{
				return new PreviousTableGrid();
			}
			return null;
		}

		// Token: 0x17009061 RID: 36961
		// (get) Token: 0x0601A0D2 RID: 106706 RVA: 0x0035CD53 File Offset: 0x0035AF53
		internal override string[] ElementTagNames
		{
			get
			{
				return TableGridChange.eleTagNames;
			}
		}

		// Token: 0x17009062 RID: 36962
		// (get) Token: 0x0601A0D3 RID: 106707 RVA: 0x0035CD5A File Offset: 0x0035AF5A
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableGridChange.eleNamespaceIds;
			}
		}

		// Token: 0x17009063 RID: 36963
		// (get) Token: 0x0601A0D4 RID: 106708 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009064 RID: 36964
		// (get) Token: 0x0601A0D5 RID: 106709 RVA: 0x0035CD61 File Offset: 0x0035AF61
		// (set) Token: 0x0601A0D6 RID: 106710 RVA: 0x0035CD6A File Offset: 0x0035AF6A
		public PreviousTableGrid PreviousTableGrid
		{
			get
			{
				return base.GetElement<PreviousTableGrid>(0);
			}
			set
			{
				base.SetElement<PreviousTableGrid>(0, value);
			}
		}

		// Token: 0x0601A0D7 RID: 106711 RVA: 0x002EE1BE File Offset: 0x002EC3BE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A0D8 RID: 106712 RVA: 0x0035CD74 File Offset: 0x0035AF74
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableGridChange>(deep);
		}

		// Token: 0x0400AB93 RID: 43923
		private const string tagName = "tblGridChange";

		// Token: 0x0400AB94 RID: 43924
		private const byte tagNsId = 23;

		// Token: 0x0400AB95 RID: 43925
		internal const int ElementTypeIdConst = 11782;

		// Token: 0x0400AB96 RID: 43926
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400AB97 RID: 43927
		private static byte[] attributeNamespaceIds = new byte[] { 23 };

		// Token: 0x0400AB98 RID: 43928
		private static readonly string[] eleTagNames = new string[] { "tblGrid" };

		// Token: 0x0400AB99 RID: 43929
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
