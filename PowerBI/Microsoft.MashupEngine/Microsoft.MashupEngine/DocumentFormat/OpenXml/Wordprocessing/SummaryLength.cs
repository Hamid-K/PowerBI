using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FE8 RID: 12264
	[GeneratedCode("DomGen", "2.0")]
	internal class SummaryLength : OpenXmlLeafElement
	{
		// Token: 0x17009515 RID: 38165
		// (get) Token: 0x0601AAC8 RID: 109256 RVA: 0x00365CBC File Offset: 0x00363EBC
		public override string LocalName
		{
			get
			{
				return "summaryLength";
			}
		}

		// Token: 0x17009516 RID: 38166
		// (get) Token: 0x0601AAC9 RID: 109257 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009517 RID: 38167
		// (get) Token: 0x0601AACA RID: 109258 RVA: 0x00365CC3 File Offset: 0x00363EC3
		internal override int ElementTypeId
		{
			get
			{
				return 12002;
			}
		}

		// Token: 0x0601AACB RID: 109259 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009518 RID: 38168
		// (get) Token: 0x0601AACC RID: 109260 RVA: 0x00365CCA File Offset: 0x00363ECA
		internal override string[] AttributeTagNames
		{
			get
			{
				return SummaryLength.attributeTagNames;
			}
		}

		// Token: 0x17009519 RID: 38169
		// (get) Token: 0x0601AACD RID: 109261 RVA: 0x00365CD1 File Offset: 0x00363ED1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SummaryLength.attributeNamespaceIds;
			}
		}

		// Token: 0x1700951A RID: 38170
		// (get) Token: 0x0601AACE RID: 109262 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601AACF RID: 109263 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Int32Value Val
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

		// Token: 0x0601AAD1 RID: 109265 RVA: 0x00346792 File Offset: 0x00344992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AAD2 RID: 109266 RVA: 0x00365CD8 File Offset: 0x00363ED8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SummaryLength>(deep);
		}

		// Token: 0x0400ADF0 RID: 44528
		private const string tagName = "summaryLength";

		// Token: 0x0400ADF1 RID: 44529
		private const byte tagNsId = 23;

		// Token: 0x0400ADF2 RID: 44530
		internal const int ElementTypeIdConst = 12002;

		// Token: 0x0400ADF3 RID: 44531
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ADF4 RID: 44532
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
