using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BC0 RID: 11200
	[GeneratedCode("DomGen", "2.0")]
	internal class Undo : OpenXmlLeafElement
	{
		// Token: 0x17007C6C RID: 31852
		// (get) Token: 0x060174F4 RID: 95476 RVA: 0x0033539B File Offset: 0x0033359B
		public override string LocalName
		{
			get
			{
				return "undo";
			}
		}

		// Token: 0x17007C6D RID: 31853
		// (get) Token: 0x060174F5 RID: 95477 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C6E RID: 31854
		// (get) Token: 0x060174F6 RID: 95478 RVA: 0x003353A2 File Offset: 0x003335A2
		internal override int ElementTypeId
		{
			get
			{
				return 11171;
			}
		}

		// Token: 0x060174F7 RID: 95479 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007C6F RID: 31855
		// (get) Token: 0x060174F8 RID: 95480 RVA: 0x003353A9 File Offset: 0x003335A9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Undo.attributeTagNames;
			}
		}

		// Token: 0x17007C70 RID: 31856
		// (get) Token: 0x060174F9 RID: 95481 RVA: 0x003353B0 File Offset: 0x003335B0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Undo.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C71 RID: 31857
		// (get) Token: 0x060174FA RID: 95482 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060174FB RID: 95483 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "index")]
		public UInt32Value Index
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

		// Token: 0x17007C72 RID: 31858
		// (get) Token: 0x060174FC RID: 95484 RVA: 0x003353B7 File Offset: 0x003335B7
		// (set) Token: 0x060174FD RID: 95485 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "exp")]
		public EnumValue<FormulaExpressionValues> Expression
		{
			get
			{
				return (EnumValue<FormulaExpressionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007C73 RID: 31859
		// (get) Token: 0x060174FE RID: 95486 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060174FF RID: 95487 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ref3D")]
		public BooleanValue Reference3D
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

		// Token: 0x17007C74 RID: 31860
		// (get) Token: 0x06017500 RID: 95488 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06017501 RID: 95489 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "array")]
		public BooleanValue Array
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

		// Token: 0x17007C75 RID: 31861
		// (get) Token: 0x06017502 RID: 95490 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017503 RID: 95491 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "v")]
		public BooleanValue Val
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

		// Token: 0x17007C76 RID: 31862
		// (get) Token: 0x06017504 RID: 95492 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017505 RID: 95493 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "nf")]
		public BooleanValue DefinedNameFormula
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

		// Token: 0x17007C77 RID: 31863
		// (get) Token: 0x06017506 RID: 95494 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017507 RID: 95495 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "cs")]
		public BooleanValue CrossSheetMove
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

		// Token: 0x17007C78 RID: 31864
		// (get) Token: 0x06017508 RID: 95496 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x06017509 RID: 95497 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "dr")]
		public StringValue DeletedRange
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17007C79 RID: 31865
		// (get) Token: 0x0601750A RID: 95498 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601750B RID: 95499 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "dn")]
		public StringValue DefinedName
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17007C7A RID: 31866
		// (get) Token: 0x0601750C RID: 95500 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0601750D RID: 95501 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "r")]
		public StringValue CellReference
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17007C7B RID: 31867
		// (get) Token: 0x0601750E RID: 95502 RVA: 0x0031EC49 File Offset: 0x0031CE49
		// (set) Token: 0x0601750F RID: 95503 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "sId")]
		public UInt32Value SheetId
		{
			get
			{
				return (UInt32Value)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x06017511 RID: 95505 RVA: 0x003353C8 File Offset: 0x003335C8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "index" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "exp" == name)
			{
				return new EnumValue<FormulaExpressionValues>();
			}
			if (namespaceId == 0 && "ref3D" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "array" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "v" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "nf" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "cs" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dr" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dn" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "r" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017512 RID: 95506 RVA: 0x003354CF File Offset: 0x003336CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Undo>(deep);
		}

		// Token: 0x06017513 RID: 95507 RVA: 0x003354D8 File Offset: 0x003336D8
		// Note: this type is marked as 'beforefieldinit'.
		static Undo()
		{
			byte[] array = new byte[11];
			Undo.attributeNamespaceIds = array;
		}

		// Token: 0x04009BE9 RID: 39913
		private const string tagName = "undo";

		// Token: 0x04009BEA RID: 39914
		private const byte tagNsId = 22;

		// Token: 0x04009BEB RID: 39915
		internal const int ElementTypeIdConst = 11171;

		// Token: 0x04009BEC RID: 39916
		private static string[] attributeTagNames = new string[]
		{
			"index", "exp", "ref3D", "array", "v", "nf", "cs", "dr", "dn", "r",
			"sId"
		};

		// Token: 0x04009BED RID: 39917
		private static byte[] attributeNamespaceIds;
	}
}
