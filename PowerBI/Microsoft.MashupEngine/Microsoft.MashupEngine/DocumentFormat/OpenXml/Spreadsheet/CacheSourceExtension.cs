using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C4C RID: 11340
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SourceConnection), FileFormatVersions.Office2010)]
	internal class CacheSourceExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081DF RID: 33247
		// (get) Token: 0x060180D2 RID: 98514 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081E0 RID: 33248
		// (get) Token: 0x060180D3 RID: 98515 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081E1 RID: 33249
		// (get) Token: 0x060180D4 RID: 98516 RVA: 0x0033DF63 File Offset: 0x0033C163
		internal override int ElementTypeId
		{
			get
			{
				return 11321;
			}
		}

		// Token: 0x060180D5 RID: 98517 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081E2 RID: 33250
		// (get) Token: 0x060180D6 RID: 98518 RVA: 0x0033DF6A File Offset: 0x0033C16A
		internal override string[] AttributeTagNames
		{
			get
			{
				return CacheSourceExtension.attributeTagNames;
			}
		}

		// Token: 0x170081E3 RID: 33251
		// (get) Token: 0x060180D7 RID: 98519 RVA: 0x0033DF71 File Offset: 0x0033C171
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CacheSourceExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081E4 RID: 33252
		// (get) Token: 0x060180D8 RID: 98520 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060180D9 RID: 98521 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
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

		// Token: 0x060180DA RID: 98522 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheSourceExtension()
		{
		}

		// Token: 0x060180DB RID: 98523 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheSourceExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180DC RID: 98524 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheSourceExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180DD RID: 98525 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheSourceExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060180DE RID: 98526 RVA: 0x0033DF78 File Offset: 0x0033C178
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "sourceConnection" == name)
			{
				return new SourceConnection();
			}
			return null;
		}

		// Token: 0x060180DF RID: 98527 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060180E0 RID: 98528 RVA: 0x0033DF93 File Offset: 0x0033C193
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheSourceExtension>(deep);
		}

		// Token: 0x060180E1 RID: 98529 RVA: 0x0033DF9C File Offset: 0x0033C19C
		// Note: this type is marked as 'beforefieldinit'.
		static CacheSourceExtension()
		{
			byte[] array = new byte[1];
			CacheSourceExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009EAD RID: 40621
		private const string tagName = "ext";

		// Token: 0x04009EAE RID: 40622
		private const byte tagNsId = 22;

		// Token: 0x04009EAF RID: 40623
		internal const int ElementTypeIdConst = 11321;

		// Token: 0x04009EB0 RID: 40624
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009EB1 RID: 40625
		private static byte[] attributeNamespaceIds;
	}
}
