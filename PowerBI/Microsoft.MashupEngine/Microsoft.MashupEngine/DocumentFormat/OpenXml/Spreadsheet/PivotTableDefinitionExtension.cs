using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C49 RID: 11337
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotTableDefinition), FileFormatVersions.Office2010)]
	internal class PivotTableDefinitionExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081CD RID: 33229
		// (get) Token: 0x060180A2 RID: 98466 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081CE RID: 33230
		// (get) Token: 0x060180A3 RID: 98467 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081CF RID: 33231
		// (get) Token: 0x060180A4 RID: 98468 RVA: 0x0033DE2B File Offset: 0x0033C02B
		internal override int ElementTypeId
		{
			get
			{
				return 11318;
			}
		}

		// Token: 0x060180A5 RID: 98469 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081D0 RID: 33232
		// (get) Token: 0x060180A6 RID: 98470 RVA: 0x0033DE32 File Offset: 0x0033C032
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotTableDefinitionExtension.attributeTagNames;
			}
		}

		// Token: 0x170081D1 RID: 33233
		// (get) Token: 0x060180A7 RID: 98471 RVA: 0x0033DE39 File Offset: 0x0033C039
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotTableDefinitionExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081D2 RID: 33234
		// (get) Token: 0x060180A8 RID: 98472 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060180A9 RID: 98473 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060180AA RID: 98474 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotTableDefinitionExtension()
		{
		}

		// Token: 0x060180AB RID: 98475 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotTableDefinitionExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180AC RID: 98476 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotTableDefinitionExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060180AD RID: 98477 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotTableDefinitionExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060180AE RID: 98478 RVA: 0x0033DE40 File Offset: 0x0033C040
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "pivotTableDefinition" == name)
			{
				return new PivotTableDefinition();
			}
			return null;
		}

		// Token: 0x060180AF RID: 98479 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060180B0 RID: 98480 RVA: 0x0033DE5B File Offset: 0x0033C05B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotTableDefinitionExtension>(deep);
		}

		// Token: 0x060180B1 RID: 98481 RVA: 0x0033DE64 File Offset: 0x0033C064
		// Note: this type is marked as 'beforefieldinit'.
		static PivotTableDefinitionExtension()
		{
			byte[] array = new byte[1];
			PivotTableDefinitionExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009E9E RID: 40606
		private const string tagName = "ext";

		// Token: 0x04009E9F RID: 40607
		private const byte tagNsId = 22;

		// Token: 0x04009EA0 RID: 40608
		internal const int ElementTypeIdConst = 11318;

		// Token: 0x04009EA1 RID: 40609
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009EA2 RID: 40610
		private static byte[] attributeNamespaceIds;
	}
}
