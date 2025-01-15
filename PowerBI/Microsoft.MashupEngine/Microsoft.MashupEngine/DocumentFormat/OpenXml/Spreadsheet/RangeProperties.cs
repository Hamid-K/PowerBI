using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B88 RID: 11144
	[GeneratedCode("DomGen", "2.0")]
	internal class RangeProperties : OpenXmlLeafElement
	{
		// Token: 0x17007A9C RID: 31388
		// (get) Token: 0x0601710E RID: 94478 RVA: 0x003325CD File Offset: 0x003307CD
		public override string LocalName
		{
			get
			{
				return "rangePr";
			}
		}

		// Token: 0x17007A9D RID: 31389
		// (get) Token: 0x0601710F RID: 94479 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A9E RID: 31390
		// (get) Token: 0x06017110 RID: 94480 RVA: 0x003325D4 File Offset: 0x003307D4
		internal override int ElementTypeId
		{
			get
			{
				return 11122;
			}
		}

		// Token: 0x06017111 RID: 94481 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A9F RID: 31391
		// (get) Token: 0x06017112 RID: 94482 RVA: 0x003325DB File Offset: 0x003307DB
		internal override string[] AttributeTagNames
		{
			get
			{
				return RangeProperties.attributeTagNames;
			}
		}

		// Token: 0x17007AA0 RID: 31392
		// (get) Token: 0x06017113 RID: 94483 RVA: 0x003325E2 File Offset: 0x003307E2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RangeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007AA1 RID: 31393
		// (get) Token: 0x06017114 RID: 94484 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017115 RID: 94485 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "autoStart")]
		public BooleanValue AutoStart
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

		// Token: 0x17007AA2 RID: 31394
		// (get) Token: 0x06017116 RID: 94486 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017117 RID: 94487 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "autoEnd")]
		public BooleanValue AutoEnd
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

		// Token: 0x17007AA3 RID: 31395
		// (get) Token: 0x06017118 RID: 94488 RVA: 0x003325E9 File Offset: 0x003307E9
		// (set) Token: 0x06017119 RID: 94489 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "groupBy")]
		public EnumValue<GroupByValues> GroupBy
		{
			get
			{
				return (EnumValue<GroupByValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007AA4 RID: 31396
		// (get) Token: 0x0601711A RID: 94490 RVA: 0x002F66C2 File Offset: 0x002F48C2
		// (set) Token: 0x0601711B RID: 94491 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "startNum")]
		public DoubleValue StartNumber
		{
			get
			{
				return (DoubleValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007AA5 RID: 31397
		// (get) Token: 0x0601711C RID: 94492 RVA: 0x002E82DC File Offset: 0x002E64DC
		// (set) Token: 0x0601711D RID: 94493 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "endNum")]
		public DoubleValue EndNum
		{
			get
			{
				return (DoubleValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007AA6 RID: 31398
		// (get) Token: 0x0601711E RID: 94494 RVA: 0x003325F8 File Offset: 0x003307F8
		// (set) Token: 0x0601711F RID: 94495 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "startDate")]
		public DateTimeValue StartDate
		{
			get
			{
				return (DateTimeValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007AA7 RID: 31399
		// (get) Token: 0x06017120 RID: 94496 RVA: 0x00332607 File Offset: 0x00330807
		// (set) Token: 0x06017121 RID: 94497 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "endDate")]
		public DateTimeValue EndDate
		{
			get
			{
				return (DateTimeValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007AA8 RID: 31400
		// (get) Token: 0x06017122 RID: 94498 RVA: 0x00332616 File Offset: 0x00330816
		// (set) Token: 0x06017123 RID: 94499 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "groupInterval")]
		public DoubleValue GroupInterval
		{
			get
			{
				return (DoubleValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x06017125 RID: 94501 RVA: 0x00332628 File Offset: 0x00330828
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "autoStart" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "autoEnd" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "groupBy" == name)
			{
				return new EnumValue<GroupByValues>();
			}
			if (namespaceId == 0 && "startNum" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "endNum" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "startDate" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "endDate" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "groupInterval" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017126 RID: 94502 RVA: 0x003326ED File Offset: 0x003308ED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RangeProperties>(deep);
		}

		// Token: 0x06017127 RID: 94503 RVA: 0x003326F8 File Offset: 0x003308F8
		// Note: this type is marked as 'beforefieldinit'.
		static RangeProperties()
		{
			byte[] array = new byte[8];
			RangeProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009AED RID: 39661
		private const string tagName = "rangePr";

		// Token: 0x04009AEE RID: 39662
		private const byte tagNsId = 22;

		// Token: 0x04009AEF RID: 39663
		internal const int ElementTypeIdConst = 11122;

		// Token: 0x04009AF0 RID: 39664
		private static string[] attributeTagNames = new string[] { "autoStart", "autoEnd", "groupBy", "startNum", "endNum", "startDate", "endDate", "groupInterval" };

		// Token: 0x04009AF1 RID: 39665
		private static byte[] attributeNamespaceIds;
	}
}
