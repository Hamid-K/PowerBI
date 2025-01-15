using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C91 RID: 11409
	[GeneratedCode("DomGen", "2.0")]
	internal class SheetFormatProperties : OpenXmlLeafElement
	{
		// Token: 0x170083C9 RID: 33737
		// (get) Token: 0x06018556 RID: 99670 RVA: 0x003409A6 File Offset: 0x0033EBA6
		public override string LocalName
		{
			get
			{
				return "sheetFormatPr";
			}
		}

		// Token: 0x170083CA RID: 33738
		// (get) Token: 0x06018557 RID: 99671 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170083CB RID: 33739
		// (get) Token: 0x06018558 RID: 99672 RVA: 0x003409AD File Offset: 0x0033EBAD
		internal override int ElementTypeId
		{
			get
			{
				return 11389;
			}
		}

		// Token: 0x06018559 RID: 99673 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170083CC RID: 33740
		// (get) Token: 0x0601855A RID: 99674 RVA: 0x003409B4 File Offset: 0x0033EBB4
		internal override string[] AttributeTagNames
		{
			get
			{
				return SheetFormatProperties.attributeTagNames;
			}
		}

		// Token: 0x170083CD RID: 33741
		// (get) Token: 0x0601855B RID: 99675 RVA: 0x003409BB File Offset: 0x0033EBBB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SheetFormatProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170083CE RID: 33742
		// (get) Token: 0x0601855C RID: 99676 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601855D RID: 99677 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "baseColWidth")]
		public UInt32Value BaseColumnWidth
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

		// Token: 0x170083CF RID: 33743
		// (get) Token: 0x0601855E RID: 99678 RVA: 0x002E7DD4 File Offset: 0x002E5FD4
		// (set) Token: 0x0601855F RID: 99679 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "defaultColWidth")]
		public DoubleValue DefaultColumnWidth
		{
			get
			{
				return (DoubleValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170083D0 RID: 33744
		// (get) Token: 0x06018560 RID: 99680 RVA: 0x002E7DE3 File Offset: 0x002E5FE3
		// (set) Token: 0x06018561 RID: 99681 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "defaultRowHeight")]
		public DoubleValue DefaultRowHeight
		{
			get
			{
				return (DoubleValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170083D1 RID: 33745
		// (get) Token: 0x06018562 RID: 99682 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06018563 RID: 99683 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "customHeight")]
		public BooleanValue CustomHeight
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

		// Token: 0x170083D2 RID: 33746
		// (get) Token: 0x06018564 RID: 99684 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06018565 RID: 99685 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "zeroHeight")]
		public BooleanValue ZeroHeight
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

		// Token: 0x170083D3 RID: 33747
		// (get) Token: 0x06018566 RID: 99686 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06018567 RID: 99687 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "thickTop")]
		public BooleanValue ThickTop
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

		// Token: 0x170083D4 RID: 33748
		// (get) Token: 0x06018568 RID: 99688 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06018569 RID: 99689 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "thickBottom")]
		public BooleanValue ThickBottom
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

		// Token: 0x170083D5 RID: 33749
		// (get) Token: 0x0601856A RID: 99690 RVA: 0x00335EDA File Offset: 0x003340DA
		// (set) Token: 0x0601856B RID: 99691 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "outlineLevelRow")]
		public ByteValue OutlineLevelRow
		{
			get
			{
				return (ByteValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170083D6 RID: 33750
		// (get) Token: 0x0601856C RID: 99692 RVA: 0x00334AF1 File Offset: 0x00332CF1
		// (set) Token: 0x0601856D RID: 99693 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "outlineLevelCol")]
		public ByteValue OutlineLevelColumn
		{
			get
			{
				return (ByteValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170083D7 RID: 33751
		// (get) Token: 0x0601856E RID: 99694 RVA: 0x002FE455 File Offset: 0x002FC655
		// (set) Token: 0x0601856F RID: 99695 RVA: 0x002BD54B File Offset: 0x002BB74B
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(55, "dyDescent")]
		public DoubleValue DyDescent
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

		// Token: 0x06018571 RID: 99697 RVA: 0x003409C4 File Offset: 0x0033EBC4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "baseColWidth" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "defaultColWidth" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "defaultRowHeight" == name)
			{
				return new DoubleValue();
			}
			if (namespaceId == 0 && "customHeight" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "zeroHeight" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "thickTop" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "thickBottom" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "outlineLevelRow" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "outlineLevelCol" == name)
			{
				return new ByteValue();
			}
			if (55 == namespaceId && "dyDescent" == name)
			{
				return new DoubleValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018572 RID: 99698 RVA: 0x00340AB7 File Offset: 0x0033ECB7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SheetFormatProperties>(deep);
		}

		// Token: 0x04009FD6 RID: 40918
		private const string tagName = "sheetFormatPr";

		// Token: 0x04009FD7 RID: 40919
		private const byte tagNsId = 22;

		// Token: 0x04009FD8 RID: 40920
		internal const int ElementTypeIdConst = 11389;

		// Token: 0x04009FD9 RID: 40921
		private static string[] attributeTagNames = new string[] { "baseColWidth", "defaultColWidth", "defaultRowHeight", "customHeight", "zeroHeight", "thickTop", "thickBottom", "outlineLevelRow", "outlineLevelCol", "dyDescent" };

		// Token: 0x04009FDA RID: 40922
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 55 };
	}
}
