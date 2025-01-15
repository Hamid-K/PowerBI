using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C4E RID: 11342
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotCacheDefinition), FileFormatVersions.Office2010)]
	internal class PivotCacheDefinitionExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081EB RID: 33259
		// (get) Token: 0x060180F2 RID: 98546 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081EC RID: 33260
		// (get) Token: 0x060180F3 RID: 98547 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081ED RID: 33261
		// (get) Token: 0x060180F4 RID: 98548 RVA: 0x0033E033 File Offset: 0x0033C233
		internal override int ElementTypeId
		{
			get
			{
				return 11323;
			}
		}

		// Token: 0x060180F5 RID: 98549 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081EE RID: 33262
		// (get) Token: 0x060180F6 RID: 98550 RVA: 0x0033E03A File Offset: 0x0033C23A
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotCacheDefinitionExtension.attributeTagNames;
			}
		}

		// Token: 0x170081EF RID: 33263
		// (get) Token: 0x060180F7 RID: 98551 RVA: 0x0033E041 File Offset: 0x0033C241
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotCacheDefinitionExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081F0 RID: 33264
		// (get) Token: 0x060180F8 RID: 98552 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060180F9 RID: 98553 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060180FA RID: 98554 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotCacheDefinitionExtension()
		{
		}

		// Token: 0x060180FB RID: 98555 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotCacheDefinitionExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180FC RID: 98556 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotCacheDefinitionExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180FD RID: 98557 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotCacheDefinitionExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060180FE RID: 98558 RVA: 0x0033E048 File Offset: 0x0033C248
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "pivotCacheDefinition" == name)
			{
				return new PivotCacheDefinition();
			}
			return null;
		}

		// Token: 0x060180FF RID: 98559 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018100 RID: 98560 RVA: 0x0033E063 File Offset: 0x0033C263
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotCacheDefinitionExtension>(deep);
		}

		// Token: 0x06018101 RID: 98561 RVA: 0x0033E06C File Offset: 0x0033C26C
		// Note: this type is marked as 'beforefieldinit'.
		static PivotCacheDefinitionExtension()
		{
			byte[] array = new byte[1];
			PivotCacheDefinitionExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009EB7 RID: 40631
		private const string tagName = "ext";

		// Token: 0x04009EB8 RID: 40632
		private const byte tagNsId = 22;

		// Token: 0x04009EB9 RID: 40633
		internal const int ElementTypeIdConst = 11323;

		// Token: 0x04009EBA RID: 40634
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009EBB RID: 40635
		private static byte[] attributeNamespaceIds;
	}
}
