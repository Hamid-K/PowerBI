using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BB5 RID: 11189
	[GeneratedCode("DomGen", "2.0")]
	internal class RevisionInsertSheet : OpenXmlLeafElement
	{
		// Token: 0x17007BD2 RID: 31698
		// (get) Token: 0x060173B5 RID: 95157 RVA: 0x0033434E File Offset: 0x0033254E
		public override string LocalName
		{
			get
			{
				return "ris";
			}
		}

		// Token: 0x17007BD3 RID: 31699
		// (get) Token: 0x060173B6 RID: 95158 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007BD4 RID: 31700
		// (get) Token: 0x060173B7 RID: 95159 RVA: 0x00334355 File Offset: 0x00332555
		internal override int ElementTypeId
		{
			get
			{
				return 11160;
			}
		}

		// Token: 0x060173B8 RID: 95160 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007BD5 RID: 31701
		// (get) Token: 0x060173B9 RID: 95161 RVA: 0x0033435C File Offset: 0x0033255C
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionInsertSheet.attributeTagNames;
			}
		}

		// Token: 0x17007BD6 RID: 31702
		// (get) Token: 0x060173BA RID: 95162 RVA: 0x00334363 File Offset: 0x00332563
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionInsertSheet.attributeNamespaceIds;
			}
		}

		// Token: 0x17007BD7 RID: 31703
		// (get) Token: 0x060173BB RID: 95163 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060173BC RID: 95164 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007BD8 RID: 31704
		// (get) Token: 0x060173BD RID: 95165 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060173BE RID: 95166 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17007BD9 RID: 31705
		// (get) Token: 0x060173BF RID: 95167 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060173C0 RID: 95168 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17007BDA RID: 31706
		// (get) Token: 0x060173C1 RID: 95169 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x060173C2 RID: 95170 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "sheetId")]
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

		// Token: 0x17007BDB RID: 31707
		// (get) Token: 0x060173C3 RID: 95171 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060173C4 RID: 95172 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17007BDC RID: 31708
		// (get) Token: 0x060173C5 RID: 95173 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x060173C6 RID: 95174 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "sheetPosition")]
		public UInt32Value SheetPosition
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x060173C8 RID: 95176 RVA: 0x0033436C File Offset: 0x0033256C
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
			if (namespaceId == 0 && "sheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sheetPosition" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060173C9 RID: 95177 RVA: 0x00334405 File Offset: 0x00332605
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionInsertSheet>(deep);
		}

		// Token: 0x060173CA RID: 95178 RVA: 0x00334410 File Offset: 0x00332610
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionInsertSheet()
		{
			byte[] array = new byte[6];
			RevisionInsertSheet.attributeNamespaceIds = array;
		}

		// Token: 0x04009BAC RID: 39852
		private const string tagName = "ris";

		// Token: 0x04009BAD RID: 39853
		private const byte tagNsId = 22;

		// Token: 0x04009BAE RID: 39854
		internal const int ElementTypeIdConst = 11160;

		// Token: 0x04009BAF RID: 39855
		private static string[] attributeTagNames = new string[] { "rId", "ua", "ra", "sheetId", "name", "sheetPosition" };

		// Token: 0x04009BB0 RID: 39856
		private static byte[] attributeNamespaceIds;
	}
}
