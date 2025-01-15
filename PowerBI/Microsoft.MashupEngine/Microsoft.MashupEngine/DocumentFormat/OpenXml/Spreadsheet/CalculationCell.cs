using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B2F RID: 11055
	[GeneratedCode("DomGen", "2.0")]
	internal class CalculationCell : OpenXmlLeafElement
	{
		// Token: 0x17007761 RID: 30561
		// (get) Token: 0x060169F3 RID: 92659 RVA: 0x0032D4D3 File Offset: 0x0032B6D3
		public override string LocalName
		{
			get
			{
				return "c";
			}
		}

		// Token: 0x17007762 RID: 30562
		// (get) Token: 0x060169F4 RID: 92660 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007763 RID: 30563
		// (get) Token: 0x060169F5 RID: 92661 RVA: 0x0032D4DA File Offset: 0x0032B6DA
		internal override int ElementTypeId
		{
			get
			{
				return 11053;
			}
		}

		// Token: 0x060169F6 RID: 92662 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007764 RID: 30564
		// (get) Token: 0x060169F7 RID: 92663 RVA: 0x0032D4E1 File Offset: 0x0032B6E1
		internal override string[] AttributeTagNames
		{
			get
			{
				return CalculationCell.attributeTagNames;
			}
		}

		// Token: 0x17007765 RID: 30565
		// (get) Token: 0x060169F8 RID: 92664 RVA: 0x0032D4E8 File Offset: 0x0032B6E8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CalculationCell.attributeNamespaceIds;
			}
		}

		// Token: 0x17007766 RID: 30566
		// (get) Token: 0x060169F9 RID: 92665 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060169FA RID: 92666 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "r")]
		public StringValue CellReference
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007767 RID: 30567
		// (get) Token: 0x060169FB RID: 92667 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060169FC RID: 92668 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "i")]
		public Int32Value SheetId
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007768 RID: 30568
		// (get) Token: 0x060169FD RID: 92669 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060169FE RID: 92670 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "s")]
		public BooleanValue InChildChain
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

		// Token: 0x17007769 RID: 30569
		// (get) Token: 0x060169FF RID: 92671 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06016A00 RID: 92672 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "l")]
		public BooleanValue NewLevel
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

		// Token: 0x1700776A RID: 30570
		// (get) Token: 0x06016A01 RID: 92673 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06016A02 RID: 92674 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "t")]
		public BooleanValue NewThread
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

		// Token: 0x1700776B RID: 30571
		// (get) Token: 0x06016A03 RID: 92675 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06016A04 RID: 92676 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "a")]
		public BooleanValue Array
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

		// Token: 0x06016A06 RID: 92678 RVA: 0x0032D4F0 File Offset: 0x0032B6F0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "i" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "s" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "l" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "t" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "a" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016A07 RID: 92679 RVA: 0x0032D589 File Offset: 0x0032B789
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculationCell>(deep);
		}

		// Token: 0x06016A08 RID: 92680 RVA: 0x0032D594 File Offset: 0x0032B794
		// Note: this type is marked as 'beforefieldinit'.
		static CalculationCell()
		{
			byte[] array = new byte[6];
			CalculationCell.attributeNamespaceIds = array;
		}

		// Token: 0x04009948 RID: 39240
		private const string tagName = "c";

		// Token: 0x04009949 RID: 39241
		private const byte tagNsId = 22;

		// Token: 0x0400994A RID: 39242
		internal const int ElementTypeIdConst = 11053;

		// Token: 0x0400994B RID: 39243
		private static string[] attributeTagNames = new string[] { "r", "i", "s", "l", "t", "a" };

		// Token: 0x0400994C RID: 39244
		private static byte[] attributeNamespaceIds;
	}
}
