using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BB1 RID: 11185
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RevisionCellChange))]
	[ChildElementInfo(typeof(RevisionFormat))]
	[ChildElementInfo(typeof(Undo))]
	internal class RevisionRowColumn : OpenXmlCompositeElement
	{
		// Token: 0x17007BA3 RID: 31651
		// (get) Token: 0x0601734E RID: 95054 RVA: 0x00333E13 File Offset: 0x00332013
		public override string LocalName
		{
			get
			{
				return "rrc";
			}
		}

		// Token: 0x17007BA4 RID: 31652
		// (get) Token: 0x0601734F RID: 95055 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007BA5 RID: 31653
		// (get) Token: 0x06017350 RID: 95056 RVA: 0x00333E1A File Offset: 0x0033201A
		internal override int ElementTypeId
		{
			get
			{
				return 11156;
			}
		}

		// Token: 0x06017351 RID: 95057 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007BA6 RID: 31654
		// (get) Token: 0x06017352 RID: 95058 RVA: 0x00333E21 File Offset: 0x00332021
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionRowColumn.attributeTagNames;
			}
		}

		// Token: 0x17007BA7 RID: 31655
		// (get) Token: 0x06017353 RID: 95059 RVA: 0x00333E28 File Offset: 0x00332028
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionRowColumn.attributeNamespaceIds;
			}
		}

		// Token: 0x17007BA8 RID: 31656
		// (get) Token: 0x06017354 RID: 95060 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017355 RID: 95061 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rId")]
		public UInt32Value RevisionId
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

		// Token: 0x17007BA9 RID: 31657
		// (get) Token: 0x06017356 RID: 95062 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06017357 RID: 95063 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ua")]
		public BooleanValue Ua
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

		// Token: 0x17007BAA RID: 31658
		// (get) Token: 0x06017358 RID: 95064 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06017359 RID: 95065 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ra")]
		public BooleanValue Ra
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

		// Token: 0x17007BAB RID: 31659
		// (get) Token: 0x0601735A RID: 95066 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x0601735B RID: 95067 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sId")]
		public UInt32Value SheetId
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007BAC RID: 31660
		// (get) Token: 0x0601735C RID: 95068 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601735D RID: 95069 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "eol")]
		public BooleanValue EndOfList
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

		// Token: 0x17007BAD RID: 31661
		// (get) Token: 0x0601735E RID: 95070 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0601735F RID: 95071 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "ref")]
		public StringValue Reference
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007BAE RID: 31662
		// (get) Token: 0x06017360 RID: 95072 RVA: 0x00333E2F File Offset: 0x0033202F
		// (set) Token: 0x06017361 RID: 95073 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "action")]
		public EnumValue<RowColumnActionValues> Action
		{
			get
			{
				return (EnumValue<RowColumnActionValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007BAF RID: 31663
		// (get) Token: 0x06017362 RID: 95074 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06017363 RID: 95075 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "edge")]
		public BooleanValue Edge
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

		// Token: 0x06017364 RID: 95076 RVA: 0x00293ECF File Offset: 0x002920CF
		public RevisionRowColumn()
		{
		}

		// Token: 0x06017365 RID: 95077 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RevisionRowColumn(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017366 RID: 95078 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RevisionRowColumn(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017367 RID: 95079 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RevisionRowColumn(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017368 RID: 95080 RVA: 0x00333E40 File Offset: 0x00332040
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "undo" == name)
			{
				return new Undo();
			}
			if (22 == namespaceId && "rcc" == name)
			{
				return new RevisionCellChange();
			}
			if (22 == namespaceId && "rfmt" == name)
			{
				return new RevisionFormat();
			}
			return null;
		}

		// Token: 0x06017369 RID: 95081 RVA: 0x00333E98 File Offset: 0x00332098
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "ua" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ra" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "eol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "action" == name)
			{
				return new EnumValue<RowColumnActionValues>();
			}
			if (namespaceId == 0 && "edge" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601736A RID: 95082 RVA: 0x00333F5D File Offset: 0x0033215D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionRowColumn>(deep);
		}

		// Token: 0x0601736B RID: 95083 RVA: 0x00333F68 File Offset: 0x00332168
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionRowColumn()
		{
			byte[] array = new byte[8];
			RevisionRowColumn.attributeNamespaceIds = array;
		}

		// Token: 0x04009B96 RID: 39830
		private const string tagName = "rrc";

		// Token: 0x04009B97 RID: 39831
		private const byte tagNsId = 22;

		// Token: 0x04009B98 RID: 39832
		internal const int ElementTypeIdConst = 11156;

		// Token: 0x04009B99 RID: 39833
		private static string[] attributeTagNames = new string[] { "rId", "ua", "ra", "sId", "eol", "ref", "action", "edge" };

		// Token: 0x04009B9A RID: 39834
		private static byte[] attributeNamespaceIds;
	}
}
