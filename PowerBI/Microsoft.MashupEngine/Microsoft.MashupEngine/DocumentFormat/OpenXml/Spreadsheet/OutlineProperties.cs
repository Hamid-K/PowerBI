using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BCE RID: 11214
	[GeneratedCode("DomGen", "2.0")]
	internal class OutlineProperties : OpenXmlLeafElement
	{
		// Token: 0x17007CFC RID: 31996
		// (get) Token: 0x06017630 RID: 95792 RVA: 0x00336292 File Offset: 0x00334492
		public override string LocalName
		{
			get
			{
				return "outlinePr";
			}
		}

		// Token: 0x17007CFD RID: 31997
		// (get) Token: 0x06017631 RID: 95793 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007CFE RID: 31998
		// (get) Token: 0x06017632 RID: 95794 RVA: 0x00336299 File Offset: 0x00334499
		internal override int ElementTypeId
		{
			get
			{
				return 11187;
			}
		}

		// Token: 0x06017633 RID: 95795 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007CFF RID: 31999
		// (get) Token: 0x06017634 RID: 95796 RVA: 0x003362A0 File Offset: 0x003344A0
		internal override string[] AttributeTagNames
		{
			get
			{
				return OutlineProperties.attributeTagNames;
			}
		}

		// Token: 0x17007D00 RID: 32000
		// (get) Token: 0x06017635 RID: 95797 RVA: 0x003362A7 File Offset: 0x003344A7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OutlineProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D01 RID: 32001
		// (get) Token: 0x06017636 RID: 95798 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017637 RID: 95799 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "applyStyles")]
		public BooleanValue ApplyStyles
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

		// Token: 0x17007D02 RID: 32002
		// (get) Token: 0x06017638 RID: 95800 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017639 RID: 95801 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "summaryBelow")]
		public BooleanValue SummaryBelow
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

		// Token: 0x17007D03 RID: 32003
		// (get) Token: 0x0601763A RID: 95802 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601763B RID: 95803 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "summaryRight")]
		public BooleanValue SummaryRight
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

		// Token: 0x17007D04 RID: 32004
		// (get) Token: 0x0601763C RID: 95804 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601763D RID: 95805 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "showOutlineSymbols")]
		public BooleanValue ShowOutlineSymbols
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

		// Token: 0x0601763F RID: 95807 RVA: 0x003362B0 File Offset: 0x003344B0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "applyStyles" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "summaryBelow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "summaryRight" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showOutlineSymbols" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017640 RID: 95808 RVA: 0x0033631D File Offset: 0x0033451D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OutlineProperties>(deep);
		}

		// Token: 0x06017641 RID: 95809 RVA: 0x00336328 File Offset: 0x00334528
		// Note: this type is marked as 'beforefieldinit'.
		static OutlineProperties()
		{
			byte[] array = new byte[4];
			OutlineProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009C25 RID: 39973
		private const string tagName = "outlinePr";

		// Token: 0x04009C26 RID: 39974
		private const byte tagNsId = 22;

		// Token: 0x04009C27 RID: 39975
		internal const int ElementTypeIdConst = 11187;

		// Token: 0x04009C28 RID: 39976
		private static string[] attributeTagNames = new string[] { "applyStyles", "summaryBelow", "summaryRight", "showOutlineSymbols" };

		// Token: 0x04009C29 RID: 39977
		private static byte[] attributeNamespaceIds;
	}
}
