using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C48 RID: 11336
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotField), FileFormatVersions.Office2010)]
	internal class PivotFieldExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081C7 RID: 33223
		// (get) Token: 0x06018092 RID: 98450 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081C8 RID: 33224
		// (get) Token: 0x06018093 RID: 98451 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081C9 RID: 33225
		// (get) Token: 0x06018094 RID: 98452 RVA: 0x0033DDC3 File Offset: 0x0033BFC3
		internal override int ElementTypeId
		{
			get
			{
				return 11317;
			}
		}

		// Token: 0x06018095 RID: 98453 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081CA RID: 33226
		// (get) Token: 0x06018096 RID: 98454 RVA: 0x0033DDCA File Offset: 0x0033BFCA
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotFieldExtension.attributeTagNames;
			}
		}

		// Token: 0x170081CB RID: 33227
		// (get) Token: 0x06018097 RID: 98455 RVA: 0x0033DDD1 File Offset: 0x0033BFD1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotFieldExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081CC RID: 33228
		// (get) Token: 0x06018098 RID: 98456 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018099 RID: 98457 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601809A RID: 98458 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotFieldExtension()
		{
		}

		// Token: 0x0601809B RID: 98459 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotFieldExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601809C RID: 98460 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotFieldExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601809D RID: 98461 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotFieldExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601809E RID: 98462 RVA: 0x0033DDD8 File Offset: 0x0033BFD8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "pivotField" == name)
			{
				return new PivotField();
			}
			return null;
		}

		// Token: 0x0601809F RID: 98463 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060180A0 RID: 98464 RVA: 0x0033DDF3 File Offset: 0x0033BFF3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotFieldExtension>(deep);
		}

		// Token: 0x060180A1 RID: 98465 RVA: 0x0033DDFC File Offset: 0x0033BFFC
		// Note: this type is marked as 'beforefieldinit'.
		static PivotFieldExtension()
		{
			byte[] array = new byte[1];
			PivotFieldExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009E99 RID: 40601
		private const string tagName = "ext";

		// Token: 0x04009E9A RID: 40602
		private const byte tagNsId = 22;

		// Token: 0x04009E9B RID: 40603
		internal const int ElementTypeIdConst = 11317;

		// Token: 0x04009E9C RID: 40604
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009E9D RID: 40605
		private static byte[] attributeNamespaceIds;
	}
}
