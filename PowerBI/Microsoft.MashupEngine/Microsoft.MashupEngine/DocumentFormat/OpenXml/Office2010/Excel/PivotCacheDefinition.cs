using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023E2 RID: 9186
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotCacheDefinition : OpenXmlLeafElement
	{
		// Token: 0x17004D88 RID: 19848
		// (get) Token: 0x06010B5E RID: 68446 RVA: 0x002A82F8 File Offset: 0x002A64F8
		public override string LocalName
		{
			get
			{
				return "pivotCacheDefinition";
			}
		}

		// Token: 0x17004D89 RID: 19849
		// (get) Token: 0x06010B5F RID: 68447 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D8A RID: 19850
		// (get) Token: 0x06010B60 RID: 68448 RVA: 0x002E649D File Offset: 0x002E469D
		internal override int ElementTypeId
		{
			get
			{
				return 12912;
			}
		}

		// Token: 0x06010B61 RID: 68449 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D8B RID: 19851
		// (get) Token: 0x06010B62 RID: 68450 RVA: 0x002E64A4 File Offset: 0x002E46A4
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotCacheDefinition.attributeTagNames;
			}
		}

		// Token: 0x17004D8C RID: 19852
		// (get) Token: 0x06010B63 RID: 68451 RVA: 0x002E64AB File Offset: 0x002E46AB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotCacheDefinition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D8D RID: 19853
		// (get) Token: 0x06010B64 RID: 68452 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010B65 RID: 68453 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "slicerData")]
		public BooleanValue SlicerData
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004D8E RID: 19854
		// (get) Token: 0x06010B66 RID: 68454 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06010B67 RID: 68455 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pivotCacheId")]
		public UInt32Value PivotCacheId
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

		// Token: 0x17004D8F RID: 19855
		// (get) Token: 0x06010B68 RID: 68456 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010B69 RID: 68457 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "supportSubqueryNonVisual")]
		public BooleanValue SupportSubqueryNonVisual
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004D90 RID: 19856
		// (get) Token: 0x06010B6A RID: 68458 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010B6B RID: 68459 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "supportSubqueryCalcMem")]
		public BooleanValue SupportSubqueryCalcMem
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004D91 RID: 19857
		// (get) Token: 0x06010B6C RID: 68460 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06010B6D RID: 68461 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "supportAddCalcMems")]
		public BooleanValue SupportAddCalcMems
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06010B6F RID: 68463 RVA: 0x002E64B4 File Offset: 0x002E46B4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "slicerData" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pivotCacheId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "supportSubqueryNonVisual" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "supportSubqueryCalcMem" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "supportAddCalcMems" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010B70 RID: 68464 RVA: 0x002E6537 File Offset: 0x002E4737
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotCacheDefinition>(deep);
		}

		// Token: 0x06010B71 RID: 68465 RVA: 0x002E6540 File Offset: 0x002E4740
		// Note: this type is marked as 'beforefieldinit'.
		static PivotCacheDefinition()
		{
			byte[] array = new byte[5];
			PivotCacheDefinition.attributeNamespaceIds = array;
		}

		// Token: 0x04007601 RID: 30209
		private const string tagName = "pivotCacheDefinition";

		// Token: 0x04007602 RID: 30210
		private const byte tagNsId = 53;

		// Token: 0x04007603 RID: 30211
		internal const int ElementTypeIdConst = 12912;

		// Token: 0x04007604 RID: 30212
		private static string[] attributeTagNames = new string[] { "slicerData", "pivotCacheId", "supportSubqueryNonVisual", "supportSubqueryCalcMem", "supportAddCalcMems" };

		// Token: 0x04007605 RID: 30213
		private static byte[] attributeNamespaceIds;
	}
}
