using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FE2 RID: 12258
	[GeneratedCode("DomGen", "2.0")]
	internal class RevisionView : OpenXmlLeafElement
	{
		// Token: 0x170094E4 RID: 38116
		// (get) Token: 0x0601AA65 RID: 109157 RVA: 0x003657A5 File Offset: 0x003639A5
		public override string LocalName
		{
			get
			{
				return "revisionView";
			}
		}

		// Token: 0x170094E5 RID: 38117
		// (get) Token: 0x0601AA66 RID: 109158 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170094E6 RID: 38118
		// (get) Token: 0x0601AA67 RID: 109159 RVA: 0x003657AC File Offset: 0x003639AC
		internal override int ElementTypeId
		{
			get
			{
				return 11988;
			}
		}

		// Token: 0x0601AA68 RID: 109160 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170094E7 RID: 38119
		// (get) Token: 0x0601AA69 RID: 109161 RVA: 0x003657B3 File Offset: 0x003639B3
		internal override string[] AttributeTagNames
		{
			get
			{
				return RevisionView.attributeTagNames;
			}
		}

		// Token: 0x170094E8 RID: 38120
		// (get) Token: 0x0601AA6A RID: 109162 RVA: 0x003657BA File Offset: 0x003639BA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RevisionView.attributeNamespaceIds;
			}
		}

		// Token: 0x170094E9 RID: 38121
		// (get) Token: 0x0601AA6B RID: 109163 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x0601AA6C RID: 109164 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "markup")]
		public OnOffValue Markup
		{
			get
			{
				return (OnOffValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170094EA RID: 38122
		// (get) Token: 0x0601AA6D RID: 109165 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601AA6E RID: 109166 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "comments")]
		public OnOffValue Comments
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170094EB RID: 38123
		// (get) Token: 0x0601AA6F RID: 109167 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x0601AA70 RID: 109168 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "insDel")]
		public OnOffValue DisplayRevision
		{
			get
			{
				return (OnOffValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170094EC RID: 38124
		// (get) Token: 0x0601AA71 RID: 109169 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x0601AA72 RID: 109170 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "formatting")]
		public OnOffValue Formatting
		{
			get
			{
				return (OnOffValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170094ED RID: 38125
		// (get) Token: 0x0601AA73 RID: 109171 RVA: 0x002EB443 File Offset: 0x002E9643
		// (set) Token: 0x0601AA74 RID: 109172 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "inkAnnotations")]
		public OnOffValue InkAnnotations
		{
			get
			{
				return (OnOffValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x0601AA76 RID: 109174 RVA: 0x003657C4 File Offset: 0x003639C4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "markup" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "comments" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "insDel" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "formatting" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "inkAnnotations" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AA77 RID: 109175 RVA: 0x00365851 File Offset: 0x00363A51
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RevisionView>(deep);
		}

		// Token: 0x0400ADD9 RID: 44505
		private const string tagName = "revisionView";

		// Token: 0x0400ADDA RID: 44506
		private const byte tagNsId = 23;

		// Token: 0x0400ADDB RID: 44507
		internal const int ElementTypeIdConst = 11988;

		// Token: 0x0400ADDC RID: 44508
		private static string[] attributeTagNames = new string[] { "markup", "comments", "insDel", "formatting", "inkAnnotations" };

		// Token: 0x0400ADDD RID: 44509
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23 };
	}
}
