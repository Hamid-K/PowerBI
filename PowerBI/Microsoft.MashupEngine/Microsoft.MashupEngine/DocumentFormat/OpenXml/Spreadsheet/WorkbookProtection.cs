using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C53 RID: 11347
	[GeneratedCode("DomGen", "2.0")]
	internal class WorkbookProtection : OpenXmlLeafElement
	{
		// Token: 0x17008225 RID: 33317
		// (get) Token: 0x0601816E RID: 98670 RVA: 0x0033E5FF File Offset: 0x0033C7FF
		public override string LocalName
		{
			get
			{
				return "workbookProtection";
			}
		}

		// Token: 0x17008226 RID: 33318
		// (get) Token: 0x0601816F RID: 98671 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008227 RID: 33319
		// (get) Token: 0x06018170 RID: 98672 RVA: 0x0033E606 File Offset: 0x0033C806
		internal override int ElementTypeId
		{
			get
			{
				return 11328;
			}
		}

		// Token: 0x06018171 RID: 98673 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008228 RID: 33320
		// (get) Token: 0x06018172 RID: 98674 RVA: 0x0033E60D File Offset: 0x0033C80D
		internal override string[] AttributeTagNames
		{
			get
			{
				return WorkbookProtection.attributeTagNames;
			}
		}

		// Token: 0x17008229 RID: 33321
		// (get) Token: 0x06018173 RID: 98675 RVA: 0x0033E614 File Offset: 0x0033C814
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WorkbookProtection.attributeNamespaceIds;
			}
		}

		// Token: 0x1700822A RID: 33322
		// (get) Token: 0x06018174 RID: 98676 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x06018175 RID: 98677 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "workbookPassword")]
		public HexBinaryValue WorkbookPassword
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700822B RID: 33323
		// (get) Token: 0x06018176 RID: 98678 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x06018177 RID: 98679 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "revisionsPassword")]
		public HexBinaryValue RevisionsPassword
		{
			get
			{
				return (HexBinaryValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700822C RID: 33324
		// (get) Token: 0x06018178 RID: 98680 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06018179 RID: 98681 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "lockStructure")]
		public BooleanValue LockStructure
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

		// Token: 0x1700822D RID: 33325
		// (get) Token: 0x0601817A RID: 98682 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601817B RID: 98683 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "lockWindows")]
		public BooleanValue LockWindows
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

		// Token: 0x1700822E RID: 33326
		// (get) Token: 0x0601817C RID: 98684 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0601817D RID: 98685 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "lockRevision")]
		public BooleanValue LockRevision
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

		// Token: 0x1700822F RID: 33327
		// (get) Token: 0x0601817E RID: 98686 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0601817F RID: 98687 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "revisionsAlgorithmName")]
		public StringValue RevisionsAlgorithmName
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

		// Token: 0x17008230 RID: 33328
		// (get) Token: 0x06018180 RID: 98688 RVA: 0x0033E61B File Offset: 0x0033C81B
		// (set) Token: 0x06018181 RID: 98689 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "revisionsHashValue")]
		public Base64BinaryValue RevisionsHashValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008231 RID: 33329
		// (get) Token: 0x06018182 RID: 98690 RVA: 0x0033E62A File Offset: 0x0033C82A
		// (set) Token: 0x06018183 RID: 98691 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "revisionsSaltValue")]
		public Base64BinaryValue RevisionsSaltValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17008232 RID: 33330
		// (get) Token: 0x06018184 RID: 98692 RVA: 0x002F6806 File Offset: 0x002F4A06
		// (set) Token: 0x06018185 RID: 98693 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "revisionsSpinCount")]
		public UInt32Value RevisionsSpinCount
		{
			get
			{
				return (UInt32Value)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17008233 RID: 33331
		// (get) Token: 0x06018186 RID: 98694 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x06018187 RID: 98695 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "workbookAlgorithmName")]
		public StringValue WorkbookAlgorithmName
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

		// Token: 0x17008234 RID: 33332
		// (get) Token: 0x06018188 RID: 98696 RVA: 0x0033E639 File Offset: 0x0033C839
		// (set) Token: 0x06018189 RID: 98697 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "workbookHashValue")]
		public Base64BinaryValue WorkbookHashValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17008235 RID: 33333
		// (get) Token: 0x0601818A RID: 98698 RVA: 0x0033E649 File Offset: 0x0033C849
		// (set) Token: 0x0601818B RID: 98699 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "workbookSaltValue")]
		public Base64BinaryValue WorkbookSaltValue
		{
			get
			{
				return (Base64BinaryValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17008236 RID: 33334
		// (get) Token: 0x0601818C RID: 98700 RVA: 0x002E6EFA File Offset: 0x002E50FA
		// (set) Token: 0x0601818D RID: 98701 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "workbookSpinCount")]
		public UInt32Value WorkbookSpinCount
		{
			get
			{
				return (UInt32Value)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x0601818F RID: 98703 RVA: 0x0033E65C File Offset: 0x0033C85C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "workbookPassword" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "revisionsPassword" == name)
			{
				return new HexBinaryValue();
			}
			if (namespaceId == 0 && "lockStructure" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "lockWindows" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "lockRevision" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "revisionsAlgorithmName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "revisionsHashValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "revisionsSaltValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "revisionsSpinCount" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "workbookAlgorithmName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "workbookHashValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "workbookSaltValue" == name)
			{
				return new Base64BinaryValue();
			}
			if (namespaceId == 0 && "workbookSpinCount" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018190 RID: 98704 RVA: 0x0033E78F File Offset: 0x0033C98F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorkbookProtection>(deep);
		}

		// Token: 0x06018191 RID: 98705 RVA: 0x0033E798 File Offset: 0x0033C998
		// Note: this type is marked as 'beforefieldinit'.
		static WorkbookProtection()
		{
			byte[] array = new byte[13];
			WorkbookProtection.attributeNamespaceIds = array;
		}

		// Token: 0x04009ED0 RID: 40656
		private const string tagName = "workbookProtection";

		// Token: 0x04009ED1 RID: 40657
		private const byte tagNsId = 22;

		// Token: 0x04009ED2 RID: 40658
		internal const int ElementTypeIdConst = 11328;

		// Token: 0x04009ED3 RID: 40659
		private static string[] attributeTagNames = new string[]
		{
			"workbookPassword", "revisionsPassword", "lockStructure", "lockWindows", "lockRevision", "revisionsAlgorithmName", "revisionsHashValue", "revisionsSaltValue", "revisionsSpinCount", "workbookAlgorithmName",
			"workbookHashValue", "workbookSaltValue", "workbookSpinCount"
		};

		// Token: 0x04009ED4 RID: 40660
		private static byte[] attributeNamespaceIds;
	}
}
