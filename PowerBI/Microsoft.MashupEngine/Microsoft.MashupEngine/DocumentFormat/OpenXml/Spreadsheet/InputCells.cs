using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BEA RID: 11242
	[GeneratedCode("DomGen", "2.0")]
	internal class InputCells : OpenXmlLeafElement
	{
		// Token: 0x17007E30 RID: 32304
		// (get) Token: 0x060178B1 RID: 96433 RVA: 0x0033823A File Offset: 0x0033643A
		public override string LocalName
		{
			get
			{
				return "inputCells";
			}
		}

		// Token: 0x17007E31 RID: 32305
		// (get) Token: 0x060178B2 RID: 96434 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E32 RID: 32306
		// (get) Token: 0x060178B3 RID: 96435 RVA: 0x00338241 File Offset: 0x00336441
		internal override int ElementTypeId
		{
			get
			{
				return 11214;
			}
		}

		// Token: 0x060178B4 RID: 96436 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E33 RID: 32307
		// (get) Token: 0x060178B5 RID: 96437 RVA: 0x00338248 File Offset: 0x00336448
		internal override string[] AttributeTagNames
		{
			get
			{
				return InputCells.attributeTagNames;
			}
		}

		// Token: 0x17007E34 RID: 32308
		// (get) Token: 0x060178B6 RID: 96438 RVA: 0x0033824F File Offset: 0x0033644F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return InputCells.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E35 RID: 32309
		// (get) Token: 0x060178B7 RID: 96439 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060178B8 RID: 96440 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007E36 RID: 32310
		// (get) Token: 0x060178B9 RID: 96441 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060178BA RID: 96442 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "deleted")]
		public BooleanValue Deleted
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

		// Token: 0x17007E37 RID: 32311
		// (get) Token: 0x060178BB RID: 96443 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060178BC RID: 96444 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "undone")]
		public BooleanValue Undone
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

		// Token: 0x17007E38 RID: 32312
		// (get) Token: 0x060178BD RID: 96445 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x060178BE RID: 96446 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007E39 RID: 32313
		// (get) Token: 0x060178BF RID: 96447 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x060178C0 RID: 96448 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "numFmtId")]
		public UInt32Value NumberFormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x060178C2 RID: 96450 RVA: 0x00338258 File Offset: 0x00336458
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "deleted" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "undone" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "numFmtId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060178C3 RID: 96451 RVA: 0x003382DB File Offset: 0x003364DB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InputCells>(deep);
		}

		// Token: 0x060178C4 RID: 96452 RVA: 0x003382E4 File Offset: 0x003364E4
		// Note: this type is marked as 'beforefieldinit'.
		static InputCells()
		{
			byte[] array = new byte[5];
			InputCells.attributeNamespaceIds = array;
		}

		// Token: 0x04009CB6 RID: 40118
		private const string tagName = "inputCells";

		// Token: 0x04009CB7 RID: 40119
		private const byte tagNsId = 22;

		// Token: 0x04009CB8 RID: 40120
		internal const int ElementTypeIdConst = 11214;

		// Token: 0x04009CB9 RID: 40121
		private static string[] attributeTagNames = new string[] { "r", "deleted", "undone", "val", "numFmtId" };

		// Token: 0x04009CBA RID: 40122
		private static byte[] attributeNamespaceIds;
	}
}
