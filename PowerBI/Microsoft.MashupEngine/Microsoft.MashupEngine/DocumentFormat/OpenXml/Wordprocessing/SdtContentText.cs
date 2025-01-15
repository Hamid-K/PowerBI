using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x0200300D RID: 12301
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtContentText : OpenXmlLeafElement
	{
		// Token: 0x17009664 RID: 38500
		// (get) Token: 0x0601ADA0 RID: 109984 RVA: 0x00321E60 File Offset: 0x00320060
		public override string LocalName
		{
			get
			{
				return "text";
			}
		}

		// Token: 0x17009665 RID: 38501
		// (get) Token: 0x0601ADA1 RID: 109985 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009666 RID: 38502
		// (get) Token: 0x0601ADA2 RID: 109986 RVA: 0x003686DC File Offset: 0x003668DC
		internal override int ElementTypeId
		{
			get
			{
				return 12155;
			}
		}

		// Token: 0x0601ADA3 RID: 109987 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009667 RID: 38503
		// (get) Token: 0x0601ADA4 RID: 109988 RVA: 0x003686E3 File Offset: 0x003668E3
		internal override string[] AttributeTagNames
		{
			get
			{
				return SdtContentText.attributeTagNames;
			}
		}

		// Token: 0x17009668 RID: 38504
		// (get) Token: 0x0601ADA5 RID: 109989 RVA: 0x003686EA File Offset: 0x003668EA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SdtContentText.attributeNamespaceIds;
			}
		}

		// Token: 0x17009669 RID: 38505
		// (get) Token: 0x0601ADA6 RID: 109990 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x0601ADA7 RID: 109991 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "multiLine")]
		public OnOffValue MultiLine
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

		// Token: 0x0601ADA9 RID: 109993 RVA: 0x003686F1 File Offset: 0x003668F1
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "multiLine" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601ADAA RID: 109994 RVA: 0x00368713 File Offset: 0x00366913
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentText>(deep);
		}

		// Token: 0x0400AE89 RID: 44681
		private const string tagName = "text";

		// Token: 0x0400AE8A RID: 44682
		private const byte tagNsId = 23;

		// Token: 0x0400AE8B RID: 44683
		internal const int ElementTypeIdConst = 12155;

		// Token: 0x0400AE8C RID: 44684
		private static string[] attributeTagNames = new string[] { "multiLine" };

		// Token: 0x0400AE8D RID: 44685
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
