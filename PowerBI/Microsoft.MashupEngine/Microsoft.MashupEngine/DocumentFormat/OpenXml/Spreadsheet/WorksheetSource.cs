using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CC2 RID: 11458
	[GeneratedCode("DomGen", "2.0")]
	internal class WorksheetSource : OpenXmlLeafElement
	{
		// Token: 0x170084F0 RID: 34032
		// (get) Token: 0x0601884F RID: 100431 RVA: 0x0034217E File Offset: 0x0034037E
		public override string LocalName
		{
			get
			{
				return "worksheetSource";
			}
		}

		// Token: 0x170084F1 RID: 34033
		// (get) Token: 0x06018850 RID: 100432 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084F2 RID: 34034
		// (get) Token: 0x06018851 RID: 100433 RVA: 0x00342185 File Offset: 0x00340385
		internal override int ElementTypeId
		{
			get
			{
				return 11438;
			}
		}

		// Token: 0x06018852 RID: 100434 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084F3 RID: 34035
		// (get) Token: 0x06018853 RID: 100435 RVA: 0x0034218C File Offset: 0x0034038C
		internal override string[] AttributeTagNames
		{
			get
			{
				return WorksheetSource.attributeTagNames;
			}
		}

		// Token: 0x170084F4 RID: 34036
		// (get) Token: 0x06018854 RID: 100436 RVA: 0x00342193 File Offset: 0x00340393
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WorksheetSource.attributeNamespaceIds;
			}
		}

		// Token: 0x170084F5 RID: 34037
		// (get) Token: 0x06018855 RID: 100437 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018856 RID: 100438 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170084F6 RID: 34038
		// (get) Token: 0x06018857 RID: 100439 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06018858 RID: 100440 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170084F7 RID: 34039
		// (get) Token: 0x06018859 RID: 100441 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601885A RID: 100442 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170084F8 RID: 34040
		// (get) Token: 0x0601885B RID: 100443 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0601885C RID: 100444 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x0601885E RID: 100446 RVA: 0x0034219C File Offset: 0x0034039C
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

		// Token: 0x0601885F RID: 100447 RVA: 0x0034220B File Offset: 0x0034040B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorksheetSource>(deep);
		}

		// Token: 0x0400A0A9 RID: 41129
		private const string tagName = "worksheetSource";

		// Token: 0x0400A0AA RID: 41130
		private const byte tagNsId = 22;

		// Token: 0x0400A0AB RID: 41131
		internal const int ElementTypeIdConst = 11438;

		// Token: 0x0400A0AC RID: 41132
		private static string[] attributeTagNames = new string[] { "ref", "name", "sheet", "id" };

		// Token: 0x0400A0AD RID: 41133
		private static byte[] attributeNamespaceIds = new byte[] { 0, 0, 0, 19 };
	}
}
