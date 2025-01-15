using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F73 RID: 12147
	[GeneratedCode("DomGen", "2.0")]
	internal class MainDocumentType : OpenXmlLeafElement
	{
		// Token: 0x17009101 RID: 37121
		// (get) Token: 0x0601A223 RID: 107043 RVA: 0x0035DD64 File Offset: 0x0035BF64
		public override string LocalName
		{
			get
			{
				return "mainDocumentType";
			}
		}

		// Token: 0x17009102 RID: 37122
		// (get) Token: 0x0601A224 RID: 107044 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009103 RID: 37123
		// (get) Token: 0x0601A225 RID: 107045 RVA: 0x0035DD6B File Offset: 0x0035BF6B
		internal override int ElementTypeId
		{
			get
			{
				return 11812;
			}
		}

		// Token: 0x0601A226 RID: 107046 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009104 RID: 37124
		// (get) Token: 0x0601A227 RID: 107047 RVA: 0x0035DD72 File Offset: 0x0035BF72
		internal override string[] AttributeTagNames
		{
			get
			{
				return MainDocumentType.attributeTagNames;
			}
		}

		// Token: 0x17009105 RID: 37125
		// (get) Token: 0x0601A228 RID: 107048 RVA: 0x0035DD79 File Offset: 0x0035BF79
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MainDocumentType.attributeNamespaceIds;
			}
		}

		// Token: 0x17009106 RID: 37126
		// (get) Token: 0x0601A229 RID: 107049 RVA: 0x0035DD80 File Offset: 0x0035BF80
		// (set) Token: 0x0601A22A RID: 107050 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<MailMergeDocumentValues> Val
		{
			get
			{
				return (EnumValue<MailMergeDocumentValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A22C RID: 107052 RVA: 0x0035DD8F File Offset: 0x0035BF8F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<MailMergeDocumentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A22D RID: 107053 RVA: 0x0035DDB1 File Offset: 0x0035BFB1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MainDocumentType>(deep);
		}

		// Token: 0x0400ABF4 RID: 44020
		private const string tagName = "mainDocumentType";

		// Token: 0x0400ABF5 RID: 44021
		private const byte tagNsId = 23;

		// Token: 0x0400ABF6 RID: 44022
		internal const int ElementTypeIdConst = 11812;

		// Token: 0x0400ABF7 RID: 44023
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ABF8 RID: 44024
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
