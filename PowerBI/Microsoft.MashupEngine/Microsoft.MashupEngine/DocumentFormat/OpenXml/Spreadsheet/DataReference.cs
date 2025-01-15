using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BD4 RID: 11220
	[GeneratedCode("DomGen", "2.0")]
	internal class DataReference : OpenXmlLeafElement
	{
		// Token: 0x17007D43 RID: 32067
		// (get) Token: 0x060176BF RID: 95935 RVA: 0x003369A7 File Offset: 0x00334BA7
		public override string LocalName
		{
			get
			{
				return "dataRef";
			}
		}

		// Token: 0x17007D44 RID: 32068
		// (get) Token: 0x060176C0 RID: 95936 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007D45 RID: 32069
		// (get) Token: 0x060176C1 RID: 95937 RVA: 0x003369AE File Offset: 0x00334BAE
		internal override int ElementTypeId
		{
			get
			{
				return 11193;
			}
		}

		// Token: 0x060176C2 RID: 95938 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007D46 RID: 32070
		// (get) Token: 0x060176C3 RID: 95939 RVA: 0x003369B5 File Offset: 0x00334BB5
		internal override string[] AttributeTagNames
		{
			get
			{
				return DataReference.attributeTagNames;
			}
		}

		// Token: 0x17007D47 RID: 32071
		// (get) Token: 0x060176C4 RID: 95940 RVA: 0x003369BC File Offset: 0x00334BBC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DataReference.attributeNamespaceIds;
			}
		}

		// Token: 0x17007D48 RID: 32072
		// (get) Token: 0x060176C5 RID: 95941 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060176C6 RID: 95942 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x17007D49 RID: 32073
		// (get) Token: 0x060176C7 RID: 95943 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060176C8 RID: 95944 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x17007D4A RID: 32074
		// (get) Token: 0x060176C9 RID: 95945 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060176CA RID: 95946 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "sheet")]
		public StringValue Sheet
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

		// Token: 0x17007D4B RID: 32075
		// (get) Token: 0x060176CB RID: 95947 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x060176CC RID: 95948 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x060176CE RID: 95950 RVA: 0x003369C4 File Offset: 0x00334BC4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sheet" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060176CF RID: 95951 RVA: 0x00336A33 File Offset: 0x00334C33
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataReference>(deep);
		}

		// Token: 0x04009C45 RID: 40005
		private const string tagName = "dataRef";

		// Token: 0x04009C46 RID: 40006
		private const byte tagNsId = 22;

		// Token: 0x04009C47 RID: 40007
		internal const int ElementTypeIdConst = 11193;

		// Token: 0x04009C48 RID: 40008
		private static string[] attributeTagNames = new string[] { "ref", "name", "sheet", "id" };

		// Token: 0x04009C49 RID: 40009
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 19 };
	}
}
