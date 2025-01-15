using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BBA RID: 11194
	[GeneratedCode("DomGen", "2.0")]
	internal class RevisionComment : OpenXmlLeafElement
	{
		// Token: 0x17007C39 RID: 31801
		// (get) Token: 0x06017486 RID: 95366 RVA: 0x00334EEF File Offset: 0x003330EF
		public override string LocalName
		{
			get
			{
				return "rcmt";
			}
		}

		// Token: 0x17007C3A RID: 31802
		// (get) Token: 0x06017487 RID: 95367 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C3B RID: 31803
		// (get) Token: 0x06017488 RID: 95368 RVA: 0x00334EF6 File Offset: 0x003330F6
		internal override int ElementTypeId
		{
			get
			{
				return 11165;
			}
		}

		// Token: 0x06017489 RID: 95369 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007C3C RID: 31804
		// (get) Token: 0x0601748A RID: 95370 RVA: 0x00334EFD File Offset: 0x003330FD
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionComment.attributeTagNames;
			}
		}

		// Token: 0x17007C3D RID: 31805
		// (get) Token: 0x0601748B RID: 95371 RVA: 0x00334F04 File Offset: 0x00333104
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionComment.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C3E RID: 31806
		// (get) Token: 0x0601748C RID: 95372 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601748D RID: 95373 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "sheetId")]
		public UInt32Value SheetId
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

		// Token: 0x17007C3F RID: 31807
		// (get) Token: 0x0601748E RID: 95374 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601748F RID: 95375 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cell")]
		public StringValue Cell
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

		// Token: 0x17007C40 RID: 31808
		// (get) Token: 0x06017490 RID: 95376 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06017491 RID: 95377 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "guid")]
		public StringValue Guid
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007C41 RID: 31809
		// (get) Token: 0x06017492 RID: 95378 RVA: 0x00334F0B File Offset: 0x0033310B
		// (set) Token: 0x06017493 RID: 95379 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "action")]
		public EnumValue<RevisionActionValues> Action
		{
			get
			{
				return (EnumValue<RevisionActionValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007C42 RID: 31810
		// (get) Token: 0x06017494 RID: 95380 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06017495 RID: 95381 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "alwaysShow")]
		public BooleanValue AlwaysShow
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

		// Token: 0x17007C43 RID: 31811
		// (get) Token: 0x06017496 RID: 95382 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06017497 RID: 95383 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "old")]
		public BooleanValue Old
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

		// Token: 0x17007C44 RID: 31812
		// (get) Token: 0x06017498 RID: 95384 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06017499 RID: 95385 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "hiddenRow")]
		public BooleanValue HiddenRow
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

		// Token: 0x17007C45 RID: 31813
		// (get) Token: 0x0601749A RID: 95386 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0601749B RID: 95387 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "hiddenColumn")]
		public BooleanValue HiddenColumn
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

		// Token: 0x17007C46 RID: 31814
		// (get) Token: 0x0601749C RID: 95388 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601749D RID: 95389 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "author")]
		public StringValue Author
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

		// Token: 0x17007C47 RID: 31815
		// (get) Token: 0x0601749E RID: 95390 RVA: 0x002E7720 File Offset: 0x002E5920
		// (set) Token: 0x0601749F RID: 95391 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "oldLength")]
		public UInt32Value OldLength
		{
			get
			{
				return (UInt32Value)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17007C48 RID: 31816
		// (get) Token: 0x060174A0 RID: 95392 RVA: 0x0031EC49 File Offset: 0x0031CE49
		// (set) Token: 0x060174A1 RID: 95393 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "newLength")]
		public UInt32Value NewLength
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

		// Token: 0x060174A3 RID: 95395 RVA: 0x00334F1C File Offset: 0x0033311C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "cell" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "guid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "action" == name)
			{
				return new EnumValue<RevisionActionValues>();
			}
			if (namespaceId == 0 && "alwaysShow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "old" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hiddenRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "hiddenColumn" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "author" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "oldLength" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "newLength" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060174A4 RID: 95396 RVA: 0x00335023 File Offset: 0x00333223
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionComment>(deep);
		}

		// Token: 0x060174A5 RID: 95397 RVA: 0x0033502C File Offset: 0x0033322C
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionComment()
		{
			byte[] array = new byte[11];
			RevisionComment.attributeNamespaceIds = array;
		}

		// Token: 0x04009BCB RID: 39883
		private const string tagName = "rcmt";

		// Token: 0x04009BCC RID: 39884
		private const byte tagNsId = 22;

		// Token: 0x04009BCD RID: 39885
		internal const int ElementTypeIdConst = 11165;

		// Token: 0x04009BCE RID: 39886
		private static string[] attributeTagNames = new string[]
		{
			"sheetId", "cell", "guid", "action", "alwaysShow", "old", "hiddenRow", "hiddenColumn", "author", "oldLength",
			"newLength"
		};

		// Token: 0x04009BCF RID: 39887
		private static byte[] attributeNamespaceIds;
	}
}
