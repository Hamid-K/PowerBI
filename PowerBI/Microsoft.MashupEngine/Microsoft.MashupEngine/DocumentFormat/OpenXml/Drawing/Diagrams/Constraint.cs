using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002664 RID: 9828
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Constraint : OpenXmlCompositeElement
	{
		// Token: 0x17005BBE RID: 23486
		// (get) Token: 0x06012B43 RID: 76611 RVA: 0x002FE3D0 File Offset: 0x002FC5D0
		public override string LocalName
		{
			get
			{
				return "constr";
			}
		}

		// Token: 0x17005BBF RID: 23487
		// (get) Token: 0x06012B44 RID: 76612 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BC0 RID: 23488
		// (get) Token: 0x06012B45 RID: 76613 RVA: 0x002FE3D7 File Offset: 0x002FC5D7
		internal override int ElementTypeId
		{
			get
			{
				return 10645;
			}
		}

		// Token: 0x06012B46 RID: 76614 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005BC1 RID: 23489
		// (get) Token: 0x06012B47 RID: 76615 RVA: 0x002FE3DE File Offset: 0x002FC5DE
		internal override string[] AttributeTagNames
		{
			get
			{
				return Constraint.attributeTagNames;
			}
		}

		// Token: 0x17005BC2 RID: 23490
		// (get) Token: 0x06012B48 RID: 76616 RVA: 0x002FE3E5 File Offset: 0x002FC5E5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Constraint.attributeNamespaceIds;
			}
		}

		// Token: 0x17005BC3 RID: 23491
		// (get) Token: 0x06012B49 RID: 76617 RVA: 0x002FE3EC File Offset: 0x002FC5EC
		// (set) Token: 0x06012B4A RID: 76618 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<ConstraintValues> Type
		{
			get
			{
				return (EnumValue<ConstraintValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005BC4 RID: 23492
		// (get) Token: 0x06012B4B RID: 76619 RVA: 0x002FE3FB File Offset: 0x002FC5FB
		// (set) Token: 0x06012B4C RID: 76620 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "for")]
		public EnumValue<ConstraintRelationshipValues> For
		{
			get
			{
				return (EnumValue<ConstraintRelationshipValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17005BC5 RID: 23493
		// (get) Token: 0x06012B4D RID: 76621 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06012B4E RID: 76622 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "forName")]
		public StringValue ForName
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005BC6 RID: 23494
		// (get) Token: 0x06012B4F RID: 76623 RVA: 0x002FE40A File Offset: 0x002FC60A
		// (set) Token: 0x06012B50 RID: 76624 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "ptType")]
		public EnumValue<ElementValues> PointType
		{
			get
			{
				return (EnumValue<ElementValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17005BC7 RID: 23495
		// (get) Token: 0x06012B51 RID: 76625 RVA: 0x002FE419 File Offset: 0x002FC619
		// (set) Token: 0x06012B52 RID: 76626 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "refType")]
		public EnumValue<ConstraintValues> ReferenceType
		{
			get
			{
				return (EnumValue<ConstraintValues>)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17005BC8 RID: 23496
		// (get) Token: 0x06012B53 RID: 76627 RVA: 0x002FE428 File Offset: 0x002FC628
		// (set) Token: 0x06012B54 RID: 76628 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "refFor")]
		public EnumValue<ConstraintRelationshipValues> ReferenceFor
		{
			get
			{
				return (EnumValue<ConstraintRelationshipValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17005BC9 RID: 23497
		// (get) Token: 0x06012B55 RID: 76629 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06012B56 RID: 76630 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "refForName")]
		public StringValue ReferenceForName
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17005BCA RID: 23498
		// (get) Token: 0x06012B57 RID: 76631 RVA: 0x002FE437 File Offset: 0x002FC637
		// (set) Token: 0x06012B58 RID: 76632 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "refPtType")]
		public EnumValue<ElementValues> ReferencePointType
		{
			get
			{
				return (EnumValue<ElementValues>)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17005BCB RID: 23499
		// (get) Token: 0x06012B59 RID: 76633 RVA: 0x002FE446 File Offset: 0x002FC646
		// (set) Token: 0x06012B5A RID: 76634 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "op")]
		public EnumValue<BoolOperatorValues> Operator
		{
			get
			{
				return (EnumValue<BoolOperatorValues>)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17005BCC RID: 23500
		// (get) Token: 0x06012B5B RID: 76635 RVA: 0x002FE455 File Offset: 0x002FC655
		// (set) Token: 0x06012B5C RID: 76636 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "val")]
		public DoubleValue Val
		{
			get
			{
				return (DoubleValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17005BCD RID: 23501
		// (get) Token: 0x06012B5D RID: 76637 RVA: 0x002FE465 File Offset: 0x002FC665
		// (set) Token: 0x06012B5E RID: 76638 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "fact")]
		public DoubleValue Fact
		{
			get
			{
				return (DoubleValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x06012B5F RID: 76639 RVA: 0x00293ECF File Offset: 0x002920CF
		public Constraint()
		{
		}

		// Token: 0x06012B60 RID: 76640 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Constraint(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B61 RID: 76641 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Constraint(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B62 RID: 76642 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Constraint(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012B63 RID: 76643 RVA: 0x002FE011 File Offset: 0x002FC211
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005BCE RID: 23502
		// (get) Token: 0x06012B64 RID: 76644 RVA: 0x002FE475 File Offset: 0x002FC675
		internal override string[] ElementTagNames
		{
			get
			{
				return Constraint.eleTagNames;
			}
		}

		// Token: 0x17005BCF RID: 23503
		// (get) Token: 0x06012B65 RID: 76645 RVA: 0x002FE47C File Offset: 0x002FC67C
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Constraint.eleNamespaceIds;
			}
		}

		// Token: 0x17005BD0 RID: 23504
		// (get) Token: 0x06012B66 RID: 76646 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005BD1 RID: 23505
		// (get) Token: 0x06012B67 RID: 76647 RVA: 0x002FE03A File Offset: 0x002FC23A
		// (set) Token: 0x06012B68 RID: 76648 RVA: 0x002FE043 File Offset: 0x002FC243
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

		// Token: 0x06012B69 RID: 76649 RVA: 0x002FE484 File Offset: 0x002FC684
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ConstraintValues>();
			}
			if (namespaceId == 0 && "for" == name)
			{
				return new EnumValue<ConstraintRelationshipValues>();
			}
			if (namespaceId == 0 && "forName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "ptType" == name)
			{
				return new EnumValue<ElementValues>();
			}
			if (namespaceId == 0 && "refType" == name)
			{
				return new EnumValue<ConstraintValues>();
			}
			if (namespaceId == 0 && "refFor" == name)
			{
				return new EnumValue<ConstraintRelationshipValues>();
			}
			if (namespaceId == 0 && "refForName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "refPtType" == name)
			{
				return new EnumValue<ElementValues>();
			}
			if (namespaceId == 0 && "op" == name)
			{
				return new EnumValue<BoolOperatorValues>();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "fact" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012B6A RID: 76650 RVA: 0x002FE58B File Offset: 0x002FC78B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Constraint>(deep);
		}

		// Token: 0x06012B6B RID: 76651 RVA: 0x002FE594 File Offset: 0x002FC794
		// Note: this type is marked as 'beforefieldinit'.
		static Constraint()
		{
			byte[] array = new byte[11];
			Constraint.attributeNamespaceIds = array;
			Constraint.eleTagNames = new string[] { "extLst" };
			Constraint.eleNamespaceIds = new byte[] { 14 };
		}

		// Token: 0x04008148 RID: 33096
		private const string tagName = "constr";

		// Token: 0x04008149 RID: 33097
		private const byte tagNsId = 14;

		// Token: 0x0400814A RID: 33098
		internal const int ElementTypeIdConst = 10645;

		// Token: 0x0400814B RID: 33099
		private static string[] attributeTagNames = new string[]
		{
			"type", "for", "forName", "ptType", "refType", "refFor", "refForName", "refPtType", "op", "val",
			"fact"
		};

		// Token: 0x0400814C RID: 33100
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400814D RID: 33101
		private static readonly string[] eleTagNames;

		// Token: 0x0400814E RID: 33102
		private static readonly byte[] eleNamespaceIds;
	}
}
