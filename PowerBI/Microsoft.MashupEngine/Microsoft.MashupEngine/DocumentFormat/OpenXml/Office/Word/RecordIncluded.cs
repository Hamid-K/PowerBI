using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200247F RID: 9343
	[GeneratedCode("DomGen", "2.0")]
	internal class RecordIncluded : OpenXmlLeafElement
	{
		// Token: 0x17005140 RID: 20800
		// (get) Token: 0x060113DB RID: 70619 RVA: 0x002EBFA8 File Offset: 0x002EA1A8
		public override string LocalName
		{
			get
			{
				return "active";
			}
		}

		// Token: 0x17005141 RID: 20801
		// (get) Token: 0x060113DC RID: 70620 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005142 RID: 20802
		// (get) Token: 0x060113DD RID: 70621 RVA: 0x002EBFAF File Offset: 0x002EA1AF
		internal override int ElementTypeId
		{
			get
			{
				return 12570;
			}
		}

		// Token: 0x060113DE RID: 70622 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005143 RID: 20803
		// (get) Token: 0x060113DF RID: 70623 RVA: 0x002EBFB6 File Offset: 0x002EA1B6
		internal override string[] AttributeTagNames
		{
			get
			{
				return RecordIncluded.attributeTagNames;
			}
		}

		// Token: 0x17005144 RID: 20804
		// (get) Token: 0x060113E0 RID: 70624 RVA: 0x002EBFBD File Offset: 0x002EA1BD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RecordIncluded.attributeNamespaceIds;
			}
		}

		// Token: 0x17005145 RID: 20805
		// (get) Token: 0x060113E1 RID: 70625 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x060113E2 RID: 70626 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(33, "val")]
		public OnOffValue Val
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

		// Token: 0x060113E4 RID: 70628 RVA: 0x002EBFD3 File Offset: 0x002EA1D3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "val" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060113E5 RID: 70629 RVA: 0x002EBFF5 File Offset: 0x002EA1F5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RecordIncluded>(deep);
		}

		// Token: 0x040078D2 RID: 30930
		private const string tagName = "active";

		// Token: 0x040078D3 RID: 30931
		private const byte tagNsId = 33;

		// Token: 0x040078D4 RID: 30932
		internal const int ElementTypeIdConst = 12570;

		// Token: 0x040078D5 RID: 30933
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040078D6 RID: 30934
		private static byte[] attributeNamespaceIds = new byte[] { 33 };
	}
}
