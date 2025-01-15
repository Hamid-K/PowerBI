using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BDB RID: 11227
	[GeneratedCode("DomGen", "2.0")]
	internal class PrintOptions : OpenXmlLeafElement
	{
		// Token: 0x17007D72 RID: 32114
		// (get) Token: 0x06017729 RID: 96041 RVA: 0x00336DF3 File Offset: 0x00334FF3
		public override string LocalName
		{
			get
			{
				return "printOptions";
			}
		}

		// Token: 0x17007D73 RID: 32115
		// (get) Token: 0x0601772A RID: 96042 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D74 RID: 32116
		// (get) Token: 0x0601772B RID: 96043 RVA: 0x00336DFA File Offset: 0x00334FFA
		internal override int ElementTypeId
		{
			get
			{
				return 11199;
			}
		}

		// Token: 0x0601772C RID: 96044 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D75 RID: 32117
		// (get) Token: 0x0601772D RID: 96045 RVA: 0x00336E01 File Offset: 0x00335001
		internal override string[] AttributeTagNames
		{
			get
			{
				return PrintOptions.attributeTagNames;
			}
		}

		// Token: 0x17007D76 RID: 32118
		// (get) Token: 0x0601772E RID: 96046 RVA: 0x00336E08 File Offset: 0x00335008
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PrintOptions.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D77 RID: 32119
		// (get) Token: 0x0601772F RID: 96047 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017730 RID: 96048 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "horizontalCentered")]
		public BooleanValue HorizontalCentered
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

		// Token: 0x17007D78 RID: 32120
		// (get) Token: 0x06017731 RID: 96049 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017732 RID: 96050 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "verticalCentered")]
		public BooleanValue VerticalCentered
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

		// Token: 0x17007D79 RID: 32121
		// (get) Token: 0x06017733 RID: 96051 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017734 RID: 96052 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "headings")]
		public BooleanValue Headings
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

		// Token: 0x17007D7A RID: 32122
		// (get) Token: 0x06017735 RID: 96053 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017736 RID: 96054 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "gridLines")]
		public BooleanValue GridLines
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

		// Token: 0x17007D7B RID: 32123
		// (get) Token: 0x06017737 RID: 96055 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017738 RID: 96056 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "gridLinesSet")]
		public BooleanValue GridLinesSet
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

		// Token: 0x0601773A RID: 96058 RVA: 0x00336E10 File Offset: 0x00335010
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "horizontalCentered" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "verticalCentered" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "headings" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "gridLines" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "gridLinesSet" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601773B RID: 96059 RVA: 0x00336E93 File Offset: 0x00335093
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrintOptions>(deep);
		}

		// Token: 0x0601773C RID: 96060 RVA: 0x00336E9C File Offset: 0x0033509C
		// Note: this type is marked as 'beforefieldinit'.
		static PrintOptions()
		{
			byte[] array = new byte[5];
			PrintOptions.attributeNamespaceIds = array;
		}

		// Token: 0x04009C63 RID: 40035
		private const string tagName = "printOptions";

		// Token: 0x04009C64 RID: 40036
		private const byte tagNsId = 22;

		// Token: 0x04009C65 RID: 40037
		internal const int ElementTypeIdConst = 11199;

		// Token: 0x04009C66 RID: 40038
		private static string[] attributeTagNames = new string[] { "horizontalCentered", "verticalCentered", "headings", "gridLines", "gridLinesSet" };

		// Token: 0x04009C67 RID: 40039
		private static byte[] attributeNamespaceIds;
	}
}
