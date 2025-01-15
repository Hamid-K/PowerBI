using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CC5 RID: 11461
	[ChildElementInfo(typeof(NumberItem))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BooleanItem))]
	[ChildElementInfo(typeof(MissingItem))]
	[ChildElementInfo(typeof(ErrorItem))]
	[ChildElementInfo(typeof(StringItem))]
	[ChildElementInfo(typeof(DateTimeItem))]
	internal class SharedItems : OpenXmlCompositeElement
	{
		// Token: 0x17008507 RID: 34055
		// (get) Token: 0x06018882 RID: 100482 RVA: 0x0034239A File Offset: 0x0034059A
		public override string LocalName
		{
			get
			{
				return "sharedItems";
			}
		}

		// Token: 0x17008508 RID: 34056
		// (get) Token: 0x06018883 RID: 100483 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008509 RID: 34057
		// (get) Token: 0x06018884 RID: 100484 RVA: 0x003423A1 File Offset: 0x003405A1
		internal override int ElementTypeId
		{
			get
			{
				return 11441;
			}
		}

		// Token: 0x06018885 RID: 100485 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700850A RID: 34058
		// (get) Token: 0x06018886 RID: 100486 RVA: 0x003423A8 File Offset: 0x003405A8
		internal override string[] AttributeTagNames
		{
			get
			{
				return SharedItems.attributeTagNames;
			}
		}

		// Token: 0x1700850B RID: 34059
		// (get) Token: 0x06018887 RID: 100487 RVA: 0x003423AF File Offset: 0x003405AF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SharedItems.attributeNamespaceIds;
			}
		}

		// Token: 0x1700850C RID: 34060
		// (get) Token: 0x06018888 RID: 100488 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018889 RID: 100489 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "containsSemiMixedTypes")]
		public BooleanValue ContainsSemiMixedTypes
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

		// Token: 0x1700850D RID: 34061
		// (get) Token: 0x0601888A RID: 100490 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601888B RID: 100491 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "containsNonDate")]
		public BooleanValue ContainsNonDate
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

		// Token: 0x1700850E RID: 34062
		// (get) Token: 0x0601888C RID: 100492 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601888D RID: 100493 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "containsDate")]
		public BooleanValue ContainsDate
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

		// Token: 0x1700850F RID: 34063
		// (get) Token: 0x0601888E RID: 100494 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601888F RID: 100495 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "containsString")]
		public BooleanValue ContainsString
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

		// Token: 0x17008510 RID: 34064
		// (get) Token: 0x06018890 RID: 100496 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06018891 RID: 100497 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "containsBlank")]
		public BooleanValue ContainsBlank
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

		// Token: 0x17008511 RID: 34065
		// (get) Token: 0x06018892 RID: 100498 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06018893 RID: 100499 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "containsMixedTypes")]
		public BooleanValue ContainsMixedTypes
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

		// Token: 0x17008512 RID: 34066
		// (get) Token: 0x06018894 RID: 100500 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06018895 RID: 100501 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "containsNumber")]
		public BooleanValue ContainsNumber
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

		// Token: 0x17008513 RID: 34067
		// (get) Token: 0x06018896 RID: 100502 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06018897 RID: 100503 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "containsInteger")]
		public BooleanValue ContainsInteger
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

		// Token: 0x17008514 RID: 34068
		// (get) Token: 0x06018898 RID: 100504 RVA: 0x0032999B File Offset: 0x00327B9B
		// (set) Token: 0x06018899 RID: 100505 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "minValue")]
		public DoubleValue MinValue
		{
			get
			{
				return (DoubleValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17008515 RID: 34069
		// (get) Token: 0x0601889A RID: 100506 RVA: 0x002FE455 File Offset: 0x002FC655
		// (set) Token: 0x0601889B RID: 100507 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "maxValue")]
		public DoubleValue MaxValue
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

		// Token: 0x17008516 RID: 34070
		// (get) Token: 0x0601889C RID: 100508 RVA: 0x003423B6 File Offset: 0x003405B6
		// (set) Token: 0x0601889D RID: 100509 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "minDate")]
		public DateTimeValue MinDate
		{
			get
			{
				return (DateTimeValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17008517 RID: 34071
		// (get) Token: 0x0601889E RID: 100510 RVA: 0x003423C6 File Offset: 0x003405C6
		// (set) Token: 0x0601889F RID: 100511 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "maxDate")]
		public DateTimeValue MaxDate
		{
			get
			{
				return (DateTimeValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17008518 RID: 34072
		// (get) Token: 0x060188A0 RID: 100512 RVA: 0x002E6EFA File Offset: 0x002E50FA
		// (set) Token: 0x060188A1 RID: 100513 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17008519 RID: 34073
		// (get) Token: 0x060188A2 RID: 100514 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x060188A3 RID: 100515 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "longText")]
		public BooleanValue LongText
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

		// Token: 0x060188A4 RID: 100516 RVA: 0x00293ECF File Offset: 0x002920CF
		public SharedItems()
		{
		}

		// Token: 0x060188A5 RID: 100517 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SharedItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188A6 RID: 100518 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SharedItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188A7 RID: 100519 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SharedItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060188A8 RID: 100520 RVA: 0x003423D8 File Offset: 0x003405D8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "m" == name)
			{
				return new MissingItem();
			}
			if (22 == namespaceId && "n" == name)
			{
				return new NumberItem();
			}
			if (22 == namespaceId && "b" == name)
			{
				return new BooleanItem();
			}
			if (22 == namespaceId && "e" == name)
			{
				return new ErrorItem();
			}
			if (22 == namespaceId && "s" == name)
			{
				return new StringItem();
			}
			if (22 == namespaceId && "d" == name)
			{
				return new DateTimeItem();
			}
			return null;
		}

		// Token: 0x060188A9 RID: 100521 RVA: 0x00342478 File Offset: 0x00340678
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "containsSemiMixedTypes" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "containsNonDate" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "containsDate" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "containsString" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "containsBlank" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "containsMixedTypes" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "containsNumber" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "containsInteger" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "minValue" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "maxValue" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "minDate" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "maxDate" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "longText" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060188AA RID: 100522 RVA: 0x003425C1 File Offset: 0x003407C1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SharedItems>(deep);
		}

		// Token: 0x060188AB RID: 100523 RVA: 0x003425CC File Offset: 0x003407CC
		// Note: this type is marked as 'beforefieldinit'.
		static SharedItems()
		{
			byte[] array = new byte[14];
			SharedItems.attributeNamespaceIds = array;
		}

		// Token: 0x0400A0B8 RID: 41144
		private const string tagName = "sharedItems";

		// Token: 0x0400A0B9 RID: 41145
		private const byte tagNsId = 22;

		// Token: 0x0400A0BA RID: 41146
		internal const int ElementTypeIdConst = 11441;

		// Token: 0x0400A0BB RID: 41147
		private static string[] attributeTagNames = new string[]
		{
			"containsSemiMixedTypes", "containsNonDate", "containsDate", "containsString", "containsBlank", "containsMixedTypes", "containsNumber", "containsInteger", "minValue", "maxValue",
			"minDate", "maxDate", "count", "longText"
		};

		// Token: 0x0400A0BC RID: 41148
		private static byte[] attributeNamespaceIds;
	}
}
