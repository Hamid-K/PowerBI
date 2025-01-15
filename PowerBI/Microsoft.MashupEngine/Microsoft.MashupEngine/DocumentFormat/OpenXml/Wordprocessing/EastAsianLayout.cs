using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EB0 RID: 11952
	[GeneratedCode("DomGen", "2.0")]
	internal class EastAsianLayout : OpenXmlLeafElement
	{
		// Token: 0x17008BB7 RID: 35767
		// (get) Token: 0x06019661 RID: 104033 RVA: 0x0034938F File Offset: 0x0034758F
		public override string LocalName
		{
			get
			{
				return "eastAsianLayout";
			}
		}

		// Token: 0x17008BB8 RID: 35768
		// (get) Token: 0x06019662 RID: 104034 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008BB9 RID: 35769
		// (get) Token: 0x06019663 RID: 104035 RVA: 0x00349396 File Offset: 0x00347596
		internal override int ElementTypeId
		{
			get
			{
				return 11609;
			}
		}

		// Token: 0x06019664 RID: 104036 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008BBA RID: 35770
		// (get) Token: 0x06019665 RID: 104037 RVA: 0x0034939D File Offset: 0x0034759D
		internal override string[] AttributeTagNames
		{
			get
			{
				return EastAsianLayout.attributeTagNames;
			}
		}

		// Token: 0x17008BBB RID: 35771
		// (get) Token: 0x06019666 RID: 104038 RVA: 0x003493A4 File Offset: 0x003475A4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EastAsianLayout.attributeNamespaceIds;
			}
		}

		// Token: 0x17008BBC RID: 35772
		// (get) Token: 0x06019667 RID: 104039 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06019668 RID: 104040 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "id")]
		public Int32Value Id
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008BBD RID: 35773
		// (get) Token: 0x06019669 RID: 104041 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601966A RID: 104042 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "combine")]
		public OnOffValue Combine
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

		// Token: 0x17008BBE RID: 35774
		// (get) Token: 0x0601966B RID: 104043 RVA: 0x003493AB File Offset: 0x003475AB
		// (set) Token: 0x0601966C RID: 104044 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "combineBrackets")]
		public EnumValue<CombineBracketValues> CombineBrackets
		{
			get
			{
				return (EnumValue<CombineBracketValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008BBF RID: 35775
		// (get) Token: 0x0601966D RID: 104045 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x0601966E RID: 104046 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "vert")]
		public OnOffValue Vertical
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

		// Token: 0x17008BC0 RID: 35776
		// (get) Token: 0x0601966F RID: 104047 RVA: 0x002EB443 File Offset: 0x002E9643
		// (set) Token: 0x06019670 RID: 104048 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "vertCompress")]
		public OnOffValue VerticalCompress
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

		// Token: 0x06019672 RID: 104050 RVA: 0x003493BC File Offset: 0x003475BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "id" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "combine" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "combineBrackets" == name)
			{
				return new EnumValue<CombineBracketValues>();
			}
			if (23 == namespaceId && "vert" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "vertCompress" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019673 RID: 104051 RVA: 0x00349449 File Offset: 0x00347649
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EastAsianLayout>(deep);
		}

		// Token: 0x0400A8CB RID: 43211
		private const string tagName = "eastAsianLayout";

		// Token: 0x0400A8CC RID: 43212
		private const byte tagNsId = 23;

		// Token: 0x0400A8CD RID: 43213
		internal const int ElementTypeIdConst = 11609;

		// Token: 0x0400A8CE RID: 43214
		private static string[] attributeTagNames = new string[] { "id", "combine", "combineBrackets", "vert", "vertCompress" };

		// Token: 0x0400A8CF RID: 43215
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23 };
	}
}
