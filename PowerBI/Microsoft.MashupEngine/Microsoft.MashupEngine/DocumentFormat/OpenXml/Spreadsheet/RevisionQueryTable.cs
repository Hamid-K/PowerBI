using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BBB RID: 11195
	[GeneratedCode("DomGen", "2.0")]
	internal class RevisionQueryTable : OpenXmlLeafElement
	{
		// Token: 0x17007C49 RID: 31817
		// (get) Token: 0x060174A6 RID: 95398 RVA: 0x003350AF File Offset: 0x003332AF
		public override string LocalName
		{
			get
			{
				return "rqt";
			}
		}

		// Token: 0x17007C4A RID: 31818
		// (get) Token: 0x060174A7 RID: 95399 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C4B RID: 31819
		// (get) Token: 0x060174A8 RID: 95400 RVA: 0x003350B6 File Offset: 0x003332B6
		internal override int ElementTypeId
		{
			get
			{
				return 11166;
			}
		}

		// Token: 0x060174A9 RID: 95401 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007C4C RID: 31820
		// (get) Token: 0x060174AA RID: 95402 RVA: 0x003350BD File Offset: 0x003332BD
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionQueryTable.attributeTagNames;
			}
		}

		// Token: 0x17007C4D RID: 31821
		// (get) Token: 0x060174AB RID: 95403 RVA: 0x003350C4 File Offset: 0x003332C4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionQueryTable.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C4E RID: 31822
		// (get) Token: 0x060174AC RID: 95404 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060174AD RID: 95405 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007C4F RID: 31823
		// (get) Token: 0x060174AE RID: 95406 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060174AF RID: 95407 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x17007C50 RID: 31824
		// (get) Token: 0x060174B0 RID: 95408 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x060174B1 RID: 95409 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "fieldId")]
		public UInt32Value FieldId
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

		// Token: 0x060174B3 RID: 95411 RVA: 0x003350CC File Offset: 0x003332CC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fieldId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060174B4 RID: 95412 RVA: 0x00335123 File Offset: 0x00333323
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionQueryTable>(deep);
		}

		// Token: 0x060174B5 RID: 95413 RVA: 0x0033512C File Offset: 0x0033332C
		// Note: this type is marked as 'beforefieldinit'.
		static RevisionQueryTable()
		{
			byte[] array = new byte[3];
			RevisionQueryTable.attributeNamespaceIds = array;
		}

		// Token: 0x04009BD0 RID: 39888
		private const string tagName = "rqt";

		// Token: 0x04009BD1 RID: 39889
		private const byte tagNsId = 22;

		// Token: 0x04009BD2 RID: 39890
		internal const int ElementTypeIdConst = 11166;

		// Token: 0x04009BD3 RID: 39891
		private static string[] attributeTagNames = new string[] { "sheetId", "ref", "fieldId" };

		// Token: 0x04009BD4 RID: 39892
		private static byte[] attributeNamespaceIds;
	}
}
