using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C59 RID: 11353
	[GeneratedCode("DomGen", "2.0")]
	internal class CalculationProperties : OpenXmlLeafElement
	{
		// Token: 0x17008249 RID: 33353
		// (get) Token: 0x060181CA RID: 98762 RVA: 0x0033E979 File Offset: 0x0033CB79
		public override string LocalName
		{
			get
			{
				return "calcPr";
			}
		}

		// Token: 0x1700824A RID: 33354
		// (get) Token: 0x060181CB RID: 98763 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700824B RID: 33355
		// (get) Token: 0x060181CC RID: 98764 RVA: 0x0033E980 File Offset: 0x0033CB80
		internal override int ElementTypeId
		{
			get
			{
				return 11334;
			}
		}

		// Token: 0x060181CD RID: 98765 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700824C RID: 33356
		// (get) Token: 0x060181CE RID: 98766 RVA: 0x0033E987 File Offset: 0x0033CB87
		internal override string[] AttributeTagNames
		{
			get
			{
				return CalculationProperties.attributeTagNames;
			}
		}

		// Token: 0x1700824D RID: 33357
		// (get) Token: 0x060181CF RID: 98767 RVA: 0x0033E98E File Offset: 0x0033CB8E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CalculationProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700824E RID: 33358
		// (get) Token: 0x060181D0 RID: 98768 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060181D1 RID: 98769 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "calcId")]
		public UInt32Value CalculationId
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

		// Token: 0x1700824F RID: 33359
		// (get) Token: 0x060181D2 RID: 98770 RVA: 0x0033E995 File Offset: 0x0033CB95
		// (set) Token: 0x060181D3 RID: 98771 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "calcMode")]
		public EnumValue<CalculateModeValues> CalculationMode
		{
			get
			{
				return (EnumValue<CalculateModeValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008250 RID: 33360
		// (get) Token: 0x060181D4 RID: 98772 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060181D5 RID: 98773 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "fullCalcOnLoad")]
		public BooleanValue FullCalculationOnLoad
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

		// Token: 0x17008251 RID: 33361
		// (get) Token: 0x060181D6 RID: 98774 RVA: 0x0033E9A4 File Offset: 0x0033CBA4
		// (set) Token: 0x060181D7 RID: 98775 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "refMode")]
		public EnumValue<ReferenceModeValues> ReferenceMode
		{
			get
			{
				return (EnumValue<ReferenceModeValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008252 RID: 33362
		// (get) Token: 0x060181D8 RID: 98776 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060181D9 RID: 98777 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "iterate")]
		public BooleanValue Iterate
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

		// Token: 0x17008253 RID: 33363
		// (get) Token: 0x060181DA RID: 98778 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x060181DB RID: 98779 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "iterateCount")]
		public UInt32Value IterateCount
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008254 RID: 33364
		// (get) Token: 0x060181DC RID: 98780 RVA: 0x002FE65A File Offset: 0x002FC85A
		// (set) Token: 0x060181DD RID: 98781 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "iterateDelta")]
		public DoubleValue IterateDelta
		{
			get
			{
				return (DoubleValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008255 RID: 33365
		// (get) Token: 0x060181DE RID: 98782 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060181DF RID: 98783 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "fullPrecision")]
		public BooleanValue FullPrecision
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

		// Token: 0x17008256 RID: 33366
		// (get) Token: 0x060181E0 RID: 98784 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060181E1 RID: 98785 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "calcCompleted")]
		public BooleanValue CalculationCompleted
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

		// Token: 0x17008257 RID: 33367
		// (get) Token: 0x060181E2 RID: 98786 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x060181E3 RID: 98787 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "calcOnSave")]
		public BooleanValue CalculationOnSave
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

		// Token: 0x17008258 RID: 33368
		// (get) Token: 0x060181E4 RID: 98788 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x060181E5 RID: 98789 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "concurrentCalc")]
		public BooleanValue ConcurrentCalculation
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

		// Token: 0x17008259 RID: 33369
		// (get) Token: 0x060181E6 RID: 98790 RVA: 0x002E9686 File Offset: 0x002E7886
		// (set) Token: 0x060181E7 RID: 98791 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "concurrentManualCount")]
		public UInt32Value ConcurrentManualCount
		{
			get
			{
				return (UInt32Value)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x1700825A RID: 33370
		// (get) Token: 0x060181E8 RID: 98792 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x060181E9 RID: 98793 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "forceFullCalc")]
		public BooleanValue ForceFullCalculation
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

		// Token: 0x060181EB RID: 98795 RVA: 0x0033E9B4 File Offset: 0x0033CBB4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "calcId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "calcMode" == name)
			{
				return new EnumValue<CalculateModeValues>();
			}
			if (namespaceId == 0 && "fullCalcOnLoad" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "refMode" == name)
			{
				return new EnumValue<ReferenceModeValues>();
			}
			if (namespaceId == 0 && "iterate" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "iterateCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "iterateDelta" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "fullPrecision" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "calcCompleted" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "calcOnSave" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "concurrentCalc" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "concurrentManualCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "forceFullCalc" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060181EC RID: 98796 RVA: 0x0033EAE7 File Offset: 0x0033CCE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculationProperties>(deep);
		}

		// Token: 0x060181ED RID: 98797 RVA: 0x0033EAF0 File Offset: 0x0033CCF0
		// Note: this type is marked as 'beforefieldinit'.
		static CalculationProperties()
		{
			byte[] array = new byte[13];
			CalculationProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009EE6 RID: 40678
		private const string tagName = "calcPr";

		// Token: 0x04009EE7 RID: 40679
		private const byte tagNsId = 22;

		// Token: 0x04009EE8 RID: 40680
		internal const int ElementTypeIdConst = 11334;

		// Token: 0x04009EE9 RID: 40681
		private static string[] attributeTagNames = new string[]
		{
			"calcId", "calcMode", "fullCalcOnLoad", "refMode", "iterate", "iterateCount", "iterateDelta", "fullPrecision", "calcCompleted", "calcOnSave",
			"concurrentCalc", "concurrentManualCount", "forceFullCalc"
		};

		// Token: 0x04009EEA RID: 40682
		private static byte[] attributeNamespaceIds;
	}
}
