using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023E1 RID: 9185
	[ChildElementInfo(typeof(PivotEdits), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PivotChanges), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ConditionalFormats), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotTableDefinition : OpenXmlCompositeElement
	{
		// Token: 0x17004D73 RID: 19827
		// (get) Token: 0x06010B33 RID: 68403 RVA: 0x002E621F File Offset: 0x002E441F
		public override string LocalName
		{
			get
			{
				return "pivotTableDefinition";
			}
		}

		// Token: 0x17004D74 RID: 19828
		// (get) Token: 0x06010B34 RID: 68404 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D75 RID: 19829
		// (get) Token: 0x06010B35 RID: 68405 RVA: 0x002E6226 File Offset: 0x002E4426
		internal override int ElementTypeId
		{
			get
			{
				return 12911;
			}
		}

		// Token: 0x06010B36 RID: 68406 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004D76 RID: 19830
		// (get) Token: 0x06010B37 RID: 68407 RVA: 0x002E622D File Offset: 0x002E442D
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotTableDefinition.attributeTagNames;
			}
		}

		// Token: 0x17004D77 RID: 19831
		// (get) Token: 0x06010B38 RID: 68408 RVA: 0x002E6234 File Offset: 0x002E4434
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotTableDefinition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004D78 RID: 19832
		// (get) Token: 0x06010B39 RID: 68409 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010B3A RID: 68410 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "fillDownLabelsDefault")]
		public BooleanValue FillDownLabelsDefault
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

		// Token: 0x17004D79 RID: 19833
		// (get) Token: 0x06010B3B RID: 68411 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010B3C RID: 68412 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "visualTotalsForSets")]
		public BooleanValue VisualTotalsForSets
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004D7A RID: 19834
		// (get) Token: 0x06010B3D RID: 68413 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06010B3E RID: 68414 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "calculatedMembersInFilters")]
		public BooleanValue CalculatedMembersInFilters
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

		// Token: 0x17004D7B RID: 19835
		// (get) Token: 0x06010B3F RID: 68415 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06010B40 RID: 68416 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "altText")]
		public StringValue AltText
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004D7C RID: 19836
		// (get) Token: 0x06010B41 RID: 68417 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06010B42 RID: 68418 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "altTextSummary")]
		public StringValue AltTextSummary
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17004D7D RID: 19837
		// (get) Token: 0x06010B43 RID: 68419 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06010B44 RID: 68420 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "enableEdit")]
		public BooleanValue EnableEdit
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004D7E RID: 19838
		// (get) Token: 0x06010B45 RID: 68421 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06010B46 RID: 68422 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "autoApply")]
		public BooleanValue AutoApply
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17004D7F RID: 19839
		// (get) Token: 0x06010B47 RID: 68423 RVA: 0x002E623B File Offset: 0x002E443B
		// (set) Token: 0x06010B48 RID: 68424 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "allocationMethod")]
		public EnumValue<AllocationMethodValues> AllocationMethod
		{
			get
			{
				return (EnumValue<AllocationMethodValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17004D80 RID: 19840
		// (get) Token: 0x06010B49 RID: 68425 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x06010B4A RID: 68426 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "weightExpression")]
		public StringValue WeightExpression
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17004D81 RID: 19841
		// (get) Token: 0x06010B4B RID: 68427 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x06010B4C RID: 68428 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "hideValuesRow")]
		public BooleanValue HideValuesRow
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x06010B4D RID: 68429 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotTableDefinition()
		{
		}

		// Token: 0x06010B4E RID: 68430 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotTableDefinition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010B4F RID: 68431 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotTableDefinition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010B50 RID: 68432 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotTableDefinition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010B51 RID: 68433 RVA: 0x002E624C File Offset: 0x002E444C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "pivotEdits" == name)
			{
				return new PivotEdits();
			}
			if (53 == namespaceId && "pivotChanges" == name)
			{
				return new PivotChanges();
			}
			if (53 == namespaceId && "conditionalFormats" == name)
			{
				return new ConditionalFormats();
			}
			return null;
		}

		// Token: 0x17004D82 RID: 19842
		// (get) Token: 0x06010B52 RID: 68434 RVA: 0x002E62A2 File Offset: 0x002E44A2
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotTableDefinition.eleTagNames;
			}
		}

		// Token: 0x17004D83 RID: 19843
		// (get) Token: 0x06010B53 RID: 68435 RVA: 0x002E62A9 File Offset: 0x002E44A9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotTableDefinition.eleNamespaceIds;
			}
		}

		// Token: 0x17004D84 RID: 19844
		// (get) Token: 0x06010B54 RID: 68436 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004D85 RID: 19845
		// (get) Token: 0x06010B55 RID: 68437 RVA: 0x002E62B0 File Offset: 0x002E44B0
		// (set) Token: 0x06010B56 RID: 68438 RVA: 0x002E62B9 File Offset: 0x002E44B9
		public PivotEdits PivotEdits
		{
			get
			{
				return base.GetElement<PivotEdits>(0);
			}
			set
			{
				base.SetElement<PivotEdits>(0, value);
			}
		}

		// Token: 0x17004D86 RID: 19846
		// (get) Token: 0x06010B57 RID: 68439 RVA: 0x002E62C3 File Offset: 0x002E44C3
		// (set) Token: 0x06010B58 RID: 68440 RVA: 0x002E62CC File Offset: 0x002E44CC
		public PivotChanges PivotChanges
		{
			get
			{
				return base.GetElement<PivotChanges>(1);
			}
			set
			{
				base.SetElement<PivotChanges>(1, value);
			}
		}

		// Token: 0x17004D87 RID: 19847
		// (get) Token: 0x06010B59 RID: 68441 RVA: 0x002E62D6 File Offset: 0x002E44D6
		// (set) Token: 0x06010B5A RID: 68442 RVA: 0x002E62DF File Offset: 0x002E44DF
		public ConditionalFormats ConditionalFormats
		{
			get
			{
				return base.GetElement<ConditionalFormats>(2);
			}
			set
			{
				base.SetElement<ConditionalFormats>(2, value);
			}
		}

		// Token: 0x06010B5B RID: 68443 RVA: 0x002E62EC File Offset: 0x002E44EC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "fillDownLabelsDefault" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "visualTotalsForSets" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "calculatedMembersInFilters" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "altText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "altTextSummary" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "enableEdit" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoApply" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "allocationMethod" == name)
			{
				return new EnumValue<AllocationMethodValues>();
			}
			if (namespaceId == 0 && "weightExpression" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hideValuesRow" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010B5C RID: 68444 RVA: 0x002E63DD File Offset: 0x002E45DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotTableDefinition>(deep);
		}

		// Token: 0x06010B5D RID: 68445 RVA: 0x002E63E8 File Offset: 0x002E45E8
		// Note: this type is marked as 'beforefieldinit'.
		static PivotTableDefinition()
		{
			byte[] array = new byte[10];
			PivotTableDefinition.attributeNamespaceIds = array;
			PivotTableDefinition.eleTagNames = new string[] { "pivotEdits", "pivotChanges", "conditionalFormats" };
			PivotTableDefinition.eleNamespaceIds = new byte[] { 53, 53, 53 };
		}

		// Token: 0x040075FA RID: 30202
		private const string tagName = "pivotTableDefinition";

		// Token: 0x040075FB RID: 30203
		private const byte tagNsId = 53;

		// Token: 0x040075FC RID: 30204
		internal const int ElementTypeIdConst = 12911;

		// Token: 0x040075FD RID: 30205
		private static string[] attributeTagNames = new string[] { "fillDownLabelsDefault", "visualTotalsForSets", "calculatedMembersInFilters", "altText", "altTextSummary", "enableEdit", "autoApply", "allocationMethod", "weightExpression", "hideValuesRow" };

		// Token: 0x040075FE RID: 30206
		private static byte[] attributeNamespaceIds;

		// Token: 0x040075FF RID: 30207
		private static readonly string[] eleTagNames;

		// Token: 0x04007600 RID: 30208
		private static readonly byte[] eleNamespaceIds;
	}
}
