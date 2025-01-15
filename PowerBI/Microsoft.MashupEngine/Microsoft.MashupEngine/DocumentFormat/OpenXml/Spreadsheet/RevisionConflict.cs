using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BBC RID: 11196
	[GeneratedCode("DomGen", "2.0")]
	internal class RevisionConflict : OpenXmlLeafElement
	{
		// Token: 0x17007C51 RID: 31825
		// (get) Token: 0x060174B6 RID: 95414 RVA: 0x0033516B File Offset: 0x0033336B
		public override string LocalName
		{
			get
			{
				return "rcft";
			}
		}

		// Token: 0x17007C52 RID: 31826
		// (get) Token: 0x060174B7 RID: 95415 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C53 RID: 31827
		// (get) Token: 0x060174B8 RID: 95416 RVA: 0x00335172 File Offset: 0x00333372
		internal override int ElementTypeId
		{
			get
			{
				return 11167;
			}
		}

		// Token: 0x060174B9 RID: 95417 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007C54 RID: 31828
		// (get) Token: 0x060174BA RID: 95418 RVA: 0x00335179 File Offset: 0x00333379
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionConflict.attributeTagNames;
			}
		}

		// Token: 0x17007C55 RID: 31829
		// (get) Token: 0x060174BB RID: 95419 RVA: 0x00335180 File Offset: 0x00333380
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionConflict.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C56 RID: 31830
		// (get) Token: 0x060174BC RID: 95420 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060174BD RID: 95421 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007C57 RID: 31831
		// (get) Token: 0x060174BE RID: 95422 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060174BF RID: 95423 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17007C58 RID: 31832
		// (get) Token: 0x060174C0 RID: 95424 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060174C1 RID: 95425 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17007C59 RID: 31833
		// (get) Token: 0x060174C2 RID: 95426 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x060174C3 RID: 95427 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x060174C5 RID: 95429 RVA: 0x00335188 File Offset: 0x00333388
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060174C6 RID: 95430 RVA: 0x003351F5 File Offset: 0x003333F5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionConflict>(deep);
		}

		// Token: 0x060174C7 RID: 95431 RVA: 0x00335200 File Offset: 0x00333400
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionConflict()
		{
			byte[] array = new byte[4];
			RevisionConflict.attributeNamespaceIds = array;
		}

		// Token: 0x04009BD5 RID: 39893
		private const string tagName = "rcft";

		// Token: 0x04009BD6 RID: 39894
		private const byte tagNsId = 22;

		// Token: 0x04009BD7 RID: 39895
		internal const int ElementTypeIdConst = 11167;

		// Token: 0x04009BD8 RID: 39896
		private static string[] attributeTagNames = new string[] { "rId", "ua", "ra", "sheetId" };

		// Token: 0x04009BD9 RID: 39897
		private static byte[] attributeNamespaceIds;
	}
}
