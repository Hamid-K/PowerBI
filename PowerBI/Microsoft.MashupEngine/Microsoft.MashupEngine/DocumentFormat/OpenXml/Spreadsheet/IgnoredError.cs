using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BEC RID: 11244
	[GeneratedCode("DomGen", "2.0")]
	internal class IgnoredError : OpenXmlLeafElement
	{
		// Token: 0x17007E46 RID: 32326
		// (get) Token: 0x060178DE RID: 96478 RVA: 0x002E9F3F File Offset: 0x002E813F
		public override string LocalName
		{
			get
			{
				return "ignoredError";
			}
		}

		// Token: 0x17007E47 RID: 32327
		// (get) Token: 0x060178DF RID: 96479 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E48 RID: 32328
		// (get) Token: 0x060178E0 RID: 96480 RVA: 0x00338453 File Offset: 0x00336653
		internal override int ElementTypeId
		{
			get
			{
				return 11216;
			}
		}

		// Token: 0x060178E1 RID: 96481 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E49 RID: 32329
		// (get) Token: 0x060178E2 RID: 96482 RVA: 0x0033845A File Offset: 0x0033665A
		internal override string[] AttributeTagNames
		{
			get
			{
				return IgnoredError.attributeTagNames;
			}
		}

		// Token: 0x17007E4A RID: 32330
		// (get) Token: 0x060178E3 RID: 96483 RVA: 0x00338461 File Offset: 0x00336661
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return IgnoredError.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E4B RID: 32331
		// (get) Token: 0x060178E4 RID: 96484 RVA: 0x00338468 File Offset: 0x00336668
		// (set) Token: 0x060178E5 RID: 96485 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "sqref")]
		public ListValue<StringValue> SequenceOfReferences
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007E4C RID: 32332
		// (get) Token: 0x060178E6 RID: 96486 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060178E7 RID: 96487 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "evalError")]
		public BooleanValue EvalError
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

		// Token: 0x17007E4D RID: 32333
		// (get) Token: 0x060178E8 RID: 96488 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060178E9 RID: 96489 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "twoDigitTextYear")]
		public BooleanValue TwoDigitTextYear
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

		// Token: 0x17007E4E RID: 32334
		// (get) Token: 0x060178EA RID: 96490 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060178EB RID: 96491 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "numberStoredAsText")]
		public BooleanValue NumberStoredAsText
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

		// Token: 0x17007E4F RID: 32335
		// (get) Token: 0x060178EC RID: 96492 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060178ED RID: 96493 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "formula")]
		public BooleanValue Formula
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

		// Token: 0x17007E50 RID: 32336
		// (get) Token: 0x060178EE RID: 96494 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060178EF RID: 96495 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "formulaRange")]
		public BooleanValue FormulaRange
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

		// Token: 0x17007E51 RID: 32337
		// (get) Token: 0x060178F0 RID: 96496 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060178F1 RID: 96497 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "unlockedFormula")]
		public BooleanValue UnlockedFormula
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

		// Token: 0x17007E52 RID: 32338
		// (get) Token: 0x060178F2 RID: 96498 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060178F3 RID: 96499 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "emptyCellReference")]
		public BooleanValue EmptyCellReference
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

		// Token: 0x17007E53 RID: 32339
		// (get) Token: 0x060178F4 RID: 96500 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060178F5 RID: 96501 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "listDataValidation")]
		public BooleanValue ListDataValidation
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

		// Token: 0x17007E54 RID: 32340
		// (get) Token: 0x060178F6 RID: 96502 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x060178F7 RID: 96503 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "calculatedColumn")]
		public BooleanValue CalculatedColumn
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

		// Token: 0x060178F9 RID: 96505 RVA: 0x00338478 File Offset: 0x00336678
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sqref" == name)
			{
				return new ListValue<StringValue>();
			}
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

		// Token: 0x060178FA RID: 96506 RVA: 0x00338569 File Offset: 0x00336769
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IgnoredError>(deep);
		}

		// Token: 0x060178FB RID: 96507 RVA: 0x00338574 File Offset: 0x00336774
		// Note: this type is marked as 'beforefieldinit'.
		static IgnoredError()
		{
			byte[] array = new byte[10];
			IgnoredError.attributeNamespaceIds = array;
		}

		// Token: 0x04009CC2 RID: 40130
		private const string tagName = "ignoredError";

		// Token: 0x04009CC3 RID: 40131
		private const byte tagNsId = 22;

		// Token: 0x04009CC4 RID: 40132
		internal const int ElementTypeIdConst = 11216;

		// Token: 0x04009CC5 RID: 40133
		private static string[] attributeTagNames = new string[] { "sqref", "evalError", "twoDigitTextYear", "numberStoredAsText", "formula", "formulaRange", "unlockedFormula", "emptyCellReference", "listDataValidation", "calculatedColumn" };

		// Token: 0x04009CC6 RID: 40134
		private static byte[] attributeNamespaceIds;
	}
}
