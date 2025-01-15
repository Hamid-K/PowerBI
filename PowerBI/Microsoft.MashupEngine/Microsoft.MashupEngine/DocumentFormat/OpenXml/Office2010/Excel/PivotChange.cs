using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002428 RID: 9256
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TupleItems), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotEditValue), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	internal class PivotChange : OpenXmlCompositeElement
	{
		// Token: 0x17004F9D RID: 20381
		// (get) Token: 0x06011009 RID: 69641 RVA: 0x002E98E8 File Offset: 0x002E7AE8
		public override string LocalName
		{
			get
			{
				return "pivotChange";
			}
		}

		// Token: 0x17004F9E RID: 20382
		// (get) Token: 0x0601100A RID: 69642 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F9F RID: 20383
		// (get) Token: 0x0601100B RID: 69643 RVA: 0x002E98EF File Offset: 0x002E7AEF
		internal override int ElementTypeId
		{
			get
			{
				return 12980;
			}
		}

		// Token: 0x0601100C RID: 69644 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004FA0 RID: 20384
		// (get) Token: 0x0601100D RID: 69645 RVA: 0x002E98F6 File Offset: 0x002E7AF6
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotChange.attributeTagNames;
			}
		}

		// Token: 0x17004FA1 RID: 20385
		// (get) Token: 0x0601100E RID: 69646 RVA: 0x002E98FD File Offset: 0x002E7AFD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotChange.attributeNamespaceIds;
			}
		}

		// Token: 0x17004FA2 RID: 20386
		// (get) Token: 0x0601100F RID: 69647 RVA: 0x002E9904 File Offset: 0x002E7B04
		// (set) Token: 0x06011010 RID: 69648 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "allocationMethod")]
		public EnumValue<AllocationMethodValues> AllocationMethod
		{
			get
			{
				return (EnumValue<AllocationMethodValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004FA3 RID: 20387
		// (get) Token: 0x06011011 RID: 69649 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06011012 RID: 69650 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "weightExpression")]
		public StringValue WeightExpression
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

		// Token: 0x06011013 RID: 69651 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotChange()
		{
		}

		// Token: 0x06011014 RID: 69652 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011015 RID: 69653 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011016 RID: 69654 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011017 RID: 69655 RVA: 0x002E9914 File Offset: 0x002E7B14
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "editValue" == name)
			{
				return new PivotEditValue();
			}
			if (53 == namespaceId && "tupleItems" == name)
			{
				return new TupleItems();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004FA4 RID: 20388
		// (get) Token: 0x06011018 RID: 69656 RVA: 0x002E996A File Offset: 0x002E7B6A
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotChange.eleTagNames;
			}
		}

		// Token: 0x17004FA5 RID: 20389
		// (get) Token: 0x06011019 RID: 69657 RVA: 0x002E9971 File Offset: 0x002E7B71
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotChange.eleNamespaceIds;
			}
		}

		// Token: 0x17004FA6 RID: 20390
		// (get) Token: 0x0601101A RID: 69658 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004FA7 RID: 20391
		// (get) Token: 0x0601101B RID: 69659 RVA: 0x002E9978 File Offset: 0x002E7B78
		// (set) Token: 0x0601101C RID: 69660 RVA: 0x002E9981 File Offset: 0x002E7B81
		public PivotEditValue PivotEditValue
		{
			get
			{
				return base.GetElement<PivotEditValue>(0);
			}
			set
			{
				base.SetElement<PivotEditValue>(0, value);
			}
		}

		// Token: 0x17004FA8 RID: 20392
		// (get) Token: 0x0601101D RID: 69661 RVA: 0x002E94D7 File Offset: 0x002E76D7
		// (set) Token: 0x0601101E RID: 69662 RVA: 0x002E94E0 File Offset: 0x002E76E0
		public TupleItems TupleItems
		{
			get
			{
				return base.GetElement<TupleItems>(1);
			}
			set
			{
				base.SetElement<TupleItems>(1, value);
			}
		}

		// Token: 0x17004FA9 RID: 20393
		// (get) Token: 0x0601101F RID: 69663 RVA: 0x002E7546 File Offset: 0x002E5746
		// (set) Token: 0x06011020 RID: 69664 RVA: 0x002E754F File Offset: 0x002E574F
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06011021 RID: 69665 RVA: 0x002E998B File Offset: 0x002E7B8B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "allocationMethod" == name)
			{
				return new EnumValue<AllocationMethodValues>();
			}
			if (namespaceId == 0 && "weightExpression" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011022 RID: 69666 RVA: 0x002E99C1 File Offset: 0x002E7BC1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotChange>(deep);
		}

		// Token: 0x06011023 RID: 69667 RVA: 0x002E99CC File Offset: 0x002E7BCC
		// Note: this type is marked as 'beforefieldinit'.
		static PivotChange()
		{
			byte[] array = new byte[2];
			PivotChange.attributeNamespaceIds = array;
			PivotChange.eleTagNames = new string[] { "editValue", "tupleItems", "extLst" };
			PivotChange.eleNamespaceIds = new byte[] { 53, 53, 53 };
		}

		// Token: 0x04007739 RID: 30521
		private const string tagName = "pivotChange";

		// Token: 0x0400773A RID: 30522
		private const byte tagNsId = 53;

		// Token: 0x0400773B RID: 30523
		internal const int ElementTypeIdConst = 12980;

		// Token: 0x0400773C RID: 30524
		private static string[] attributeTagNames = new string[] { "allocationMethod", "weightExpression" };

		// Token: 0x0400773D RID: 30525
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400773E RID: 30526
		private static readonly string[] eleTagNames;

		// Token: 0x0400773F RID: 30527
		private static readonly byte[] eleNamespaceIds;
	}
}
