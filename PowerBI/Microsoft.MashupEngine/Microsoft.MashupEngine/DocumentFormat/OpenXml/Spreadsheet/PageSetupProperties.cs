using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BCF RID: 11215
	[GeneratedCode("DomGen", "2.0")]
	internal class PageSetupProperties : OpenXmlLeafElement
	{
		// Token: 0x17007D05 RID: 32005
		// (get) Token: 0x06017642 RID: 95810 RVA: 0x0033636F File Offset: 0x0033456F
		public override string LocalName
		{
			get
			{
				return "pageSetUpPr";
			}
		}

		// Token: 0x17007D06 RID: 32006
		// (get) Token: 0x06017643 RID: 95811 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D07 RID: 32007
		// (get) Token: 0x06017644 RID: 95812 RVA: 0x00336376 File Offset: 0x00334576
		internal override int ElementTypeId
		{
			get
			{
				return 11188;
			}
		}

		// Token: 0x06017645 RID: 95813 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D08 RID: 32008
		// (get) Token: 0x06017646 RID: 95814 RVA: 0x0033637D File Offset: 0x0033457D
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageSetupProperties.attributeTagNames;
			}
		}

		// Token: 0x17007D09 RID: 32009
		// (get) Token: 0x06017647 RID: 95815 RVA: 0x00336384 File Offset: 0x00334584
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageSetupProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D0A RID: 32010
		// (get) Token: 0x06017648 RID: 95816 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017649 RID: 95817 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "autoPageBreaks")]
		public BooleanValue AutoPageBreaks
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

		// Token: 0x17007D0B RID: 32011
		// (get) Token: 0x0601764A RID: 95818 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601764B RID: 95819 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fitToPage")]
		public BooleanValue FitToPage
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

		// Token: 0x0601764D RID: 95821 RVA: 0x0033638B File Offset: 0x0033458B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "autoPageBreaks" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fitToPage" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601764E RID: 95822 RVA: 0x003363C1 File Offset: 0x003345C1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageSetupProperties>(deep);
		}

		// Token: 0x0601764F RID: 95823 RVA: 0x003363CC File Offset: 0x003345CC
		// Note: this type is marked as 'beforefieldinit'.
		static PageSetupProperties()
		{
			byte[] array = new byte[2];
			PageSetupProperties.attributeNamespaceIds = array;
		}

		// Token: 0x04009C2A RID: 39978
		private const string tagName = "pageSetUpPr";

		// Token: 0x04009C2B RID: 39979
		private const byte tagNsId = 22;

		// Token: 0x04009C2C RID: 39980
		internal const int ElementTypeIdConst = 11188;

		// Token: 0x04009C2D RID: 39981
		private static string[] attributeTagNames = new string[] { "autoPageBreaks", "fitToPage" };

		// Token: 0x04009C2E RID: 39982
		private static byte[] attributeNamespaceIds;
	}
}
