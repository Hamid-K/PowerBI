using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Excel;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002431 RID: 9265
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ReferenceSequence))]
	[GeneratedCode("DomGen", "2.0")]
	internal class IgnoredError : OpenXmlCompositeElement
	{
		// Token: 0x17004FE2 RID: 20450
		// (get) Token: 0x060110A6 RID: 69798 RVA: 0x002E9F3F File Offset: 0x002E813F
		public override string LocalName
		{
			get
			{
				return "ignoredError";
			}
		}

		// Token: 0x17004FE3 RID: 20451
		// (get) Token: 0x060110A7 RID: 69799 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004FE4 RID: 20452
		// (get) Token: 0x060110A8 RID: 69800 RVA: 0x002E9F46 File Offset: 0x002E8146
		internal override int ElementTypeId
		{
			get
			{
				return 12989;
			}
		}

		// Token: 0x060110A9 RID: 69801 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004FE5 RID: 20453
		// (get) Token: 0x060110AA RID: 69802 RVA: 0x002E9F4D File Offset: 0x002E814D
		internal override string[] AttributeTagNames
		{
			get
			{
				return IgnoredError.attributeTagNames;
			}
		}

		// Token: 0x17004FE6 RID: 20454
		// (get) Token: 0x060110AB RID: 69803 RVA: 0x002E9F54 File Offset: 0x002E8154
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return IgnoredError.attributeNamespaceIds;
			}
		}

		// Token: 0x17004FE7 RID: 20455
		// (get) Token: 0x060110AC RID: 69804 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060110AD RID: 69805 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "evalError")]
		public BooleanValue EvalError
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

		// Token: 0x17004FE8 RID: 20456
		// (get) Token: 0x060110AE RID: 69806 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060110AF RID: 69807 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "twoDigitTextYear")]
		public BooleanValue TwoDigitTextYear
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

		// Token: 0x17004FE9 RID: 20457
		// (get) Token: 0x060110B0 RID: 69808 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060110B1 RID: 69809 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "numberStoredAsText")]
		public BooleanValue NumberStoredAsText
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

		// Token: 0x17004FEA RID: 20458
		// (get) Token: 0x060110B2 RID: 69810 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060110B3 RID: 69811 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "formula")]
		public BooleanValue Formula
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

		// Token: 0x17004FEB RID: 20459
		// (get) Token: 0x060110B4 RID: 69812 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060110B5 RID: 69813 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "formulaRange")]
		public BooleanValue FormulaRange
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

		// Token: 0x17004FEC RID: 20460
		// (get) Token: 0x060110B6 RID: 69814 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060110B7 RID: 69815 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "unlockedFormula")]
		public BooleanValue UnlockedFormula
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

		// Token: 0x17004FED RID: 20461
		// (get) Token: 0x060110B8 RID: 69816 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060110B9 RID: 69817 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "emptyCellReference")]
		public BooleanValue EmptyCellReference
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

		// Token: 0x17004FEE RID: 20462
		// (get) Token: 0x060110BA RID: 69818 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060110BB RID: 69819 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "listDataValidation")]
		public BooleanValue ListDataValidation
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

		// Token: 0x17004FEF RID: 20463
		// (get) Token: 0x060110BC RID: 69820 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060110BD RID: 69821 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "calculatedColumn")]
		public BooleanValue CalculatedColumn
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

		// Token: 0x060110BE RID: 69822 RVA: 0x00293ECF File Offset: 0x002920CF
		public IgnoredError()
		{
		}

		// Token: 0x060110BF RID: 69823 RVA: 0x00293ED7 File Offset: 0x002920D7
		public IgnoredError(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060110C0 RID: 69824 RVA: 0x00293EE0 File Offset: 0x002920E0
		public IgnoredError(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060110C1 RID: 69825 RVA: 0x00293EE9 File Offset: 0x002920E9
		public IgnoredError(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060110C2 RID: 69826 RVA: 0x002E9F5B File Offset: 0x002E815B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (32 == namespaceId && "sqref" == name)
			{
				return new ReferenceSequence();
			}
			return null;
		}

		// Token: 0x17004FF0 RID: 20464
		// (get) Token: 0x060110C3 RID: 69827 RVA: 0x002E9F76 File Offset: 0x002E8176
		internal override string[] ElementTagNames
		{
			get
			{
				return IgnoredError.eleTagNames;
			}
		}

		// Token: 0x17004FF1 RID: 20465
		// (get) Token: 0x060110C4 RID: 69828 RVA: 0x002E9F7D File Offset: 0x002E817D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return IgnoredError.eleNamespaceIds;
			}
		}

		// Token: 0x17004FF2 RID: 20466
		// (get) Token: 0x060110C5 RID: 69829 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004FF3 RID: 20467
		// (get) Token: 0x060110C6 RID: 69830 RVA: 0x002E9F84 File Offset: 0x002E8184
		// (set) Token: 0x060110C7 RID: 69831 RVA: 0x002E9F8D File Offset: 0x002E818D
		public ReferenceSequence ReferenceSequence
		{
			get
			{
				return base.GetElement<ReferenceSequence>(0);
			}
			set
			{
				base.SetElement<ReferenceSequence>(0, value);
			}
		}

		// Token: 0x060110C8 RID: 69832 RVA: 0x002E9F98 File Offset: 0x002E8198
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "evalError" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "twoDigitTextYear" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "numberStoredAsText" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "formula" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "formulaRange" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "unlockedFormula" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "emptyCellReference" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "listDataValidation" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "calculatedColumn" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060110C9 RID: 69833 RVA: 0x002EA073 File Offset: 0x002E8273
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IgnoredError>(deep);
		}

		// Token: 0x060110CA RID: 69834 RVA: 0x002EA07C File Offset: 0x002E827C
		// Note: this type is marked as 'beforefieldinit'.
		static IgnoredError()
		{
			byte[] array = new byte[9];
			IgnoredError.attributeNamespaceIds = array;
			IgnoredError.eleTagNames = new string[] { "sqref" };
			IgnoredError.eleNamespaceIds = new byte[] { 32 };
		}

		// Token: 0x04007768 RID: 30568
		private const string tagName = "ignoredError";

		// Token: 0x04007769 RID: 30569
		private const byte tagNsId = 53;

		// Token: 0x0400776A RID: 30570
		internal const int ElementTypeIdConst = 12989;

		// Token: 0x0400776B RID: 30571
		private static string[] attributeTagNames = new string[] { "evalError", "twoDigitTextYear", "numberStoredAsText", "formula", "formulaRange", "unlockedFormula", "emptyCellReference", "listDataValidation", "calculatedColumn" };

		// Token: 0x0400776C RID: 30572
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400776D RID: 30573
		private static readonly string[] eleTagNames;

		// Token: 0x0400776E RID: 30574
		private static readonly byte[] eleNamespaceIds;
	}
}
