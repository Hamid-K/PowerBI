using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F77 RID: 12151
	[GeneratedCode("DomGen", "2.0")]
	internal class DocumentVariable : OpenXmlLeafElement
	{
		// Token: 0x1700911F RID: 37151
		// (get) Token: 0x0601A261 RID: 107105 RVA: 0x0035E0E0 File Offset: 0x0035C2E0
		public override string LocalName
		{
			get
			{
				return "docVar";
			}
		}

		// Token: 0x17009120 RID: 37152
		// (get) Token: 0x0601A262 RID: 107106 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009121 RID: 37153
		// (get) Token: 0x0601A263 RID: 107107 RVA: 0x0035E0E7 File Offset: 0x0035C2E7
		internal override int ElementTypeId
		{
			get
			{
				return 11828;
			}
		}

		// Token: 0x0601A264 RID: 107108 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009122 RID: 37154
		// (get) Token: 0x0601A265 RID: 107109 RVA: 0x0035E0EE File Offset: 0x0035C2EE
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocumentVariable.attributeTagNames;
			}
		}

		// Token: 0x17009123 RID: 37155
		// (get) Token: 0x0601A266 RID: 107110 RVA: 0x0035E0F5 File Offset: 0x0035C2F5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocumentVariable.attributeNamespaceIds;
			}
		}

		// Token: 0x17009124 RID: 37156
		// (get) Token: 0x0601A267 RID: 107111 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A268 RID: 107112 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "name")]
		public StringValue Name
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

		// Token: 0x17009125 RID: 37157
		// (get) Token: 0x0601A269 RID: 107113 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601A26A RID: 107114 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x0601A26C RID: 107116 RVA: 0x0035E0FC File Offset: 0x0035C2FC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A26D RID: 107117 RVA: 0x0035E136 File Offset: 0x0035C336
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocumentVariable>(deep);
		}

		// Token: 0x0400AC08 RID: 44040
		private const string tagName = "docVar";

		// Token: 0x0400AC09 RID: 44041
		private const byte tagNsId = 23;

		// Token: 0x0400AC0A RID: 44042
		internal const int ElementTypeIdConst = 11828;

		// Token: 0x0400AC0B RID: 44043
		private static string[] attributeTagNames = new string[] { "name", "val" };

		// Token: 0x0400AC0C RID: 44044
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
