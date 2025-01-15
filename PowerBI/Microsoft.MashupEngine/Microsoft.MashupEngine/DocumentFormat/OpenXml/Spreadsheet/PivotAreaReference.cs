using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B8D RID: 11149
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FieldItem))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class PivotAreaReference : OpenXmlCompositeElement
	{
		// Token: 0x17007AC9 RID: 31433
		// (get) Token: 0x06017175 RID: 94581 RVA: 0x00148715 File Offset: 0x00146915
		public override string LocalName
		{
			get
			{
				return "reference";
			}
		}

		// Token: 0x17007ACA RID: 31434
		// (get) Token: 0x06017176 RID: 94582 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007ACB RID: 31435
		// (get) Token: 0x06017177 RID: 94583 RVA: 0x00332A8F File Offset: 0x00330C8F
		internal override int ElementTypeId
		{
			get
			{
				return 11128;
			}
		}

		// Token: 0x06017178 RID: 94584 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007ACC RID: 31436
		// (get) Token: 0x06017179 RID: 94585 RVA: 0x00332A96 File Offset: 0x00330C96
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotAreaReference.attributeTagNames;
			}
		}

		// Token: 0x17007ACD RID: 31437
		// (get) Token: 0x0601717A RID: 94586 RVA: 0x00332A9D File Offset: 0x00330C9D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotAreaReference.attributeNamespaceIds;
			}
		}

		// Token: 0x17007ACE RID: 31438
		// (get) Token: 0x0601717B RID: 94587 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601717C RID: 94588 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "field")]
		public UInt32Value Field
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

		// Token: 0x17007ACF RID: 31439
		// (get) Token: 0x0601717D RID: 94589 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601717E RID: 94590 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x17007AD0 RID: 31440
		// (get) Token: 0x0601717F RID: 94591 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017180 RID: 94592 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "selected")]
		public BooleanValue Selected
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

		// Token: 0x17007AD1 RID: 31441
		// (get) Token: 0x06017181 RID: 94593 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017182 RID: 94594 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "byPosition")]
		public BooleanValue ByPosition
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

		// Token: 0x17007AD2 RID: 31442
		// (get) Token: 0x06017183 RID: 94595 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017184 RID: 94596 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "relative")]
		public BooleanValue Relative
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

		// Token: 0x17007AD3 RID: 31443
		// (get) Token: 0x06017185 RID: 94597 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017186 RID: 94598 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "defaultSubtotal")]
		public BooleanValue DefaultSubtotal
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

		// Token: 0x17007AD4 RID: 31444
		// (get) Token: 0x06017187 RID: 94599 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017188 RID: 94600 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "sumSubtotal")]
		public BooleanValue SumSubtotal
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

		// Token: 0x17007AD5 RID: 31445
		// (get) Token: 0x06017189 RID: 94601 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0601718A RID: 94602 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "countASubtotal")]
		public BooleanValue CountASubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007AD6 RID: 31446
		// (get) Token: 0x0601718B RID: 94603 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0601718C RID: 94604 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "avgSubtotal")]
		public BooleanValue AverageSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007AD7 RID: 31447
		// (get) Token: 0x0601718D RID: 94605 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0601718E RID: 94606 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "maxSubtotal")]
		public BooleanValue MaxSubtotal
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

		// Token: 0x17007AD8 RID: 31448
		// (get) Token: 0x0601718F RID: 94607 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x06017190 RID: 94608 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "minSubtotal")]
		public BooleanValue MinSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17007AD9 RID: 31449
		// (get) Token: 0x06017191 RID: 94609 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x06017192 RID: 94610 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "productSubtotal")]
		public BooleanValue ApplyProductInSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17007ADA RID: 31450
		// (get) Token: 0x06017193 RID: 94611 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x06017194 RID: 94612 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "countSubtotal")]
		public BooleanValue CountSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17007ADB RID: 31451
		// (get) Token: 0x06017195 RID: 94613 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x06017196 RID: 94614 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "stdDevSubtotal")]
		public BooleanValue ApplyStandardDeviationInSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17007ADC RID: 31452
		// (get) Token: 0x06017197 RID: 94615 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x06017198 RID: 94616 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "stdDevPSubtotal")]
		public BooleanValue ApplyStandardDeviationPInSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17007ADD RID: 31453
		// (get) Token: 0x06017199 RID: 94617 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x0601719A RID: 94618 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "varSubtotal")]
		public BooleanValue ApplyVarianceInSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17007ADE RID: 31454
		// (get) Token: 0x0601719B RID: 94619 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0601719C RID: 94620 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "varPSubtotal")]
		public BooleanValue ApplyVariancePInSubtotal
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x0601719D RID: 94621 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotAreaReference()
		{
		}

		// Token: 0x0601719E RID: 94622 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotAreaReference(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601719F RID: 94623 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotAreaReference(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060171A0 RID: 94624 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotAreaReference(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060171A1 RID: 94625 RVA: 0x00332AA4 File Offset: 0x00330CA4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "x" == name)
			{
				return new FieldItem();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x060171A2 RID: 94626 RVA: 0x00332AD8 File Offset: 0x00330CD8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "field" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "selected" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "byPosition" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "relative" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "defaultSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sumSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "countASubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "avgSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "maxSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "minSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "productSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "countSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "stdDevSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "stdDevPSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "varSubtotal" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "varPSubtotal" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060171A3 RID: 94627 RVA: 0x00332C63 File Offset: 0x00330E63
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotAreaReference>(deep);
		}

		// Token: 0x060171A4 RID: 94628 RVA: 0x00332C6C File Offset: 0x00330E6C
		// Note: this type is marked as 'beforefieldinit'.
		static PivotAreaReference()
		{
			byte[] array = new byte[17];
			PivotAreaReference.attributeNamespaceIds = array;
		}

		// Token: 0x04009B08 RID: 39688
		private const string tagName = "reference";

		// Token: 0x04009B09 RID: 39689
		private const byte tagNsId = 22;

		// Token: 0x04009B0A RID: 39690
		internal const int ElementTypeIdConst = 11128;

		// Token: 0x04009B0B RID: 39691
		private static string[] attributeTagNames = new string[]
		{
			"field", "count", "selected", "byPosition", "relative", "defaultSubtotal", "sumSubtotal", "countASubtotal", "avgSubtotal", "maxSubtotal",
			"minSubtotal", "productSubtotal", "countSubtotal", "stdDevSubtotal", "stdDevPSubtotal", "varSubtotal", "varPSubtotal"
		};

		// Token: 0x04009B0C RID: 39692
		private static byte[] attributeNamespaceIds;
	}
}
