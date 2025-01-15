using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200242D RID: 9261
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PivotAreas), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ConditionalFormat : OpenXmlCompositeElement
	{
		// Token: 0x17004FBD RID: 20413
		// (get) Token: 0x06011056 RID: 69718 RVA: 0x002E9BF3 File Offset: 0x002E7DF3
		public override string LocalName
		{
			get
			{
				return "conditionalFormat";
			}
		}

		// Token: 0x17004FBE RID: 20414
		// (get) Token: 0x06011057 RID: 69719 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004FBF RID: 20415
		// (get) Token: 0x06011058 RID: 69720 RVA: 0x002E9BFA File Offset: 0x002E7DFA
		internal override int ElementTypeId
		{
			get
			{
				return 12985;
			}
		}

		// Token: 0x06011059 RID: 69721 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004FC0 RID: 20416
		// (get) Token: 0x0601105A RID: 69722 RVA: 0x002E9C01 File Offset: 0x002E7E01
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormat.attributeTagNames;
			}
		}

		// Token: 0x17004FC1 RID: 20417
		// (get) Token: 0x0601105B RID: 69723 RVA: 0x002E9C08 File Offset: 0x002E7E08
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x17004FC2 RID: 20418
		// (get) Token: 0x0601105C RID: 69724 RVA: 0x002E9C0F File Offset: 0x002E7E0F
		// (set) Token: 0x0601105D RID: 69725 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "scope")]
		public EnumValue<ScopeValues> Scope
		{
			get
			{
				return (EnumValue<ScopeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004FC3 RID: 20419
		// (get) Token: 0x0601105E RID: 69726 RVA: 0x002E9C1E File Offset: 0x002E7E1E
		// (set) Token: 0x0601105F RID: 69727 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public EnumValue<RuleValues> Type
		{
			get
			{
				return (EnumValue<RuleValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004FC4 RID: 20420
		// (get) Token: 0x06011060 RID: 69728 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06011061 RID: 69729 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "priority")]
		public UInt32Value Priority
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004FC5 RID: 20421
		// (get) Token: 0x06011062 RID: 69730 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06011063 RID: 69731 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x06011064 RID: 69732 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormat()
		{
		}

		// Token: 0x06011065 RID: 69733 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011066 RID: 69734 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011067 RID: 69735 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011068 RID: 69736 RVA: 0x002E9C2D File Offset: 0x002E7E2D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "pivotAreas" == name)
			{
				return new PivotAreas();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004FC6 RID: 20422
		// (get) Token: 0x06011069 RID: 69737 RVA: 0x002E9C60 File Offset: 0x002E7E60
		internal override string[] ElementTagNames
		{
			get
			{
				return ConditionalFormat.eleTagNames;
			}
		}

		// Token: 0x17004FC7 RID: 20423
		// (get) Token: 0x0601106A RID: 69738 RVA: 0x002E9C67 File Offset: 0x002E7E67
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ConditionalFormat.eleNamespaceIds;
			}
		}

		// Token: 0x17004FC8 RID: 20424
		// (get) Token: 0x0601106B RID: 69739 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004FC9 RID: 20425
		// (get) Token: 0x0601106C RID: 69740 RVA: 0x002E9C6E File Offset: 0x002E7E6E
		// (set) Token: 0x0601106D RID: 69741 RVA: 0x002E9C77 File Offset: 0x002E7E77
		public PivotAreas PivotAreas
		{
			get
			{
				return base.GetElement<PivotAreas>(0);
			}
			set
			{
				base.SetElement<PivotAreas>(0, value);
			}
		}

		// Token: 0x17004FCA RID: 20426
		// (get) Token: 0x0601106E RID: 69742 RVA: 0x002E700B File Offset: 0x002E520B
		// (set) Token: 0x0601106F RID: 69743 RVA: 0x002E7014 File Offset: 0x002E5214
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06011070 RID: 69744 RVA: 0x002E9C84 File Offset: 0x002E7E84
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "scope" == name)
			{
				return new EnumValue<ScopeValues>();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<RuleValues>();
			}
			if (namespaceId == 0 && "priority" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011071 RID: 69745 RVA: 0x002E9CF1 File Offset: 0x002E7EF1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormat>(deep);
		}

		// Token: 0x06011072 RID: 69746 RVA: 0x002E9CFC File Offset: 0x002E7EFC
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormat()
		{
			byte[] array = new byte[4];
			ConditionalFormat.attributeNamespaceIds = array;
			ConditionalFormat.eleTagNames = new string[] { "pivotAreas", "extLst" };
			ConditionalFormat.eleNamespaceIds = new byte[] { 53, 53 };
		}

		// Token: 0x04007750 RID: 30544
		private const string tagName = "conditionalFormat";

		// Token: 0x04007751 RID: 30545
		private const byte tagNsId = 53;

		// Token: 0x04007752 RID: 30546
		internal const int ElementTypeIdConst = 12985;

		// Token: 0x04007753 RID: 30547
		private static string[] attributeTagNames = new string[] { "scope", "type", "priority", "id" };

		// Token: 0x04007754 RID: 30548
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007755 RID: 30549
		private static readonly string[] eleTagNames;

		// Token: 0x04007756 RID: 30550
		private static readonly byte[] eleNamespaceIds;
	}
}
