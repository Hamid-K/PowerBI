using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C4D RID: 11341
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CacheField), FileFormatVersions.Office2010)]
	internal class CacheFieldExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081E5 RID: 33253
		// (get) Token: 0x060180E2 RID: 98530 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081E6 RID: 33254
		// (get) Token: 0x060180E3 RID: 98531 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081E7 RID: 33255
		// (get) Token: 0x060180E4 RID: 98532 RVA: 0x0033DFCB File Offset: 0x0033C1CB
		internal override int ElementTypeId
		{
			get
			{
				return 11322;
			}
		}

		// Token: 0x060180E5 RID: 98533 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081E8 RID: 33256
		// (get) Token: 0x060180E6 RID: 98534 RVA: 0x0033DFD2 File Offset: 0x0033C1D2
		internal override string[] AttributeTagNames
		{
			get
			{
				return CacheFieldExtension.attributeTagNames;
			}
		}

		// Token: 0x170081E9 RID: 33257
		// (get) Token: 0x060180E7 RID: 98535 RVA: 0x0033DFD9 File Offset: 0x0033C1D9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CacheFieldExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081EA RID: 33258
		// (get) Token: 0x060180E8 RID: 98536 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060180E9 RID: 98537 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060180EA RID: 98538 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheFieldExtension()
		{
		}

		// Token: 0x060180EB RID: 98539 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheFieldExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180EC RID: 98540 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheFieldExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180ED RID: 98541 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheFieldExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060180EE RID: 98542 RVA: 0x0033DFE0 File Offset: 0x0033C1E0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "cacheField" == name)
			{
				return new CacheField();
			}
			return null;
		}

		// Token: 0x060180EF RID: 98543 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060180F0 RID: 98544 RVA: 0x0033DFFB File Offset: 0x0033C1FB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheFieldExtension>(deep);
		}

		// Token: 0x060180F1 RID: 98545 RVA: 0x0033E004 File Offset: 0x0033C204
		// Note: this type is marked as 'beforefieldinit'.
		static CacheFieldExtension()
		{
			byte[] array = new byte[1];
			CacheFieldExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009EB2 RID: 40626
		private const string tagName = "ext";

		// Token: 0x04009EB3 RID: 40627
		private const byte tagNsId = 22;

		// Token: 0x04009EB4 RID: 40628
		internal const int ElementTypeIdConst = 11322;

		// Token: 0x04009EB5 RID: 40629
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009EB6 RID: 40630
		private static byte[] attributeNamespaceIds;
	}
}
