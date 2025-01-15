using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BD1 RID: 11217
	[GeneratedCode("DomGen", "2.0")]
	internal class Selection : OpenXmlLeafElement
	{
		// Token: 0x17007D16 RID: 32022
		// (get) Token: 0x06017664 RID: 95844 RVA: 0x002EAE4F File Offset: 0x002E904F
		public override string LocalName
		{
			get
			{
				return "selection";
			}
		}

		// Token: 0x17007D17 RID: 32023
		// (get) Token: 0x06017665 RID: 95845 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D18 RID: 32024
		// (get) Token: 0x06017666 RID: 95846 RVA: 0x0033651B File Offset: 0x0033471B
		internal override int ElementTypeId
		{
			get
			{
				return 11190;
			}
		}

		// Token: 0x06017667 RID: 95847 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D19 RID: 32025
		// (get) Token: 0x06017668 RID: 95848 RVA: 0x00336522 File Offset: 0x00334722
		internal override string[] AttributeTagNames
		{
			get
			{
				return Selection.attributeTagNames;
			}
		}

		// Token: 0x17007D1A RID: 32026
		// (get) Token: 0x06017669 RID: 95849 RVA: 0x00336529 File Offset: 0x00334729
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Selection.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D1B RID: 32027
		// (get) Token: 0x0601766A RID: 95850 RVA: 0x00336530 File Offset: 0x00334730
		// (set) Token: 0x0601766B RID: 95851 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "pane")]
		public EnumValue<PaneValues> Pane
		{
			get
			{
				return (EnumValue<PaneValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007D1C RID: 32028
		// (get) Token: 0x0601766C RID: 95852 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601766D RID: 95853 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "activeCell")]
		public StringValue ActiveCell
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007D1D RID: 32029
		// (get) Token: 0x0601766E RID: 95854 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x0601766F RID: 95855 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "activeCellId")]
		public UInt32Value ActiveCellId
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007D1E RID: 32030
		// (get) Token: 0x06017670 RID: 95856 RVA: 0x003347D1 File Offset: 0x003329D1
		// (set) Token: 0x06017671 RID: 95857 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sqref")]
		public ListValue<StringValue> SequenceOfReferences
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06017673 RID: 95859 RVA: 0x00336540 File Offset: 0x00334740
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "pane" == name)
			{
				return new EnumValue<PaneValues>();
			}
			if (namespaceId == 0 && "activeCell" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "activeCellId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "sqref" == name)
			{
				return new ListValue<StringValue>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017674 RID: 95860 RVA: 0x003365AD File Offset: 0x003347AD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Selection>(deep);
		}

		// Token: 0x06017675 RID: 95861 RVA: 0x003365B8 File Offset: 0x003347B8
		// Note: this type is marked as 'beforefieldinit'.
		static Selection()
		{
			byte[] array = new byte[4];
			Selection.attributeNamespaceIds = array;
		}

		// Token: 0x04009C34 RID: 39988
		private const string tagName = "selection";

		// Token: 0x04009C35 RID: 39989
		private const byte tagNsId = 22;

		// Token: 0x04009C36 RID: 39990
		internal const int ElementTypeIdConst = 11190;

		// Token: 0x04009C37 RID: 39991
		private static string[] attributeTagNames = new string[] { "pane", "activeCell", "activeCellId", "sqref" };

		// Token: 0x04009C38 RID: 39992
		private static byte[] attributeNamespaceIds;
	}
}
