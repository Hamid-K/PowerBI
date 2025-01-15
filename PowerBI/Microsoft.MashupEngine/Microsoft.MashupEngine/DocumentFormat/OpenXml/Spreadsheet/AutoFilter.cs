using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B86 RID: 11142
	[ChildElementInfo(typeof(FilterColumn))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SortState))]
	internal class AutoFilter : OpenXmlCompositeElement
	{
		// Token: 0x17007A75 RID: 31349
		// (get) Token: 0x060170BB RID: 94395 RVA: 0x00332137 File Offset: 0x00330337
		public override string LocalName
		{
			get
			{
				return "autoFilter";
			}
		}

		// Token: 0x17007A76 RID: 31350
		// (get) Token: 0x060170BC RID: 94396 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A77 RID: 31351
		// (get) Token: 0x060170BD RID: 94397 RVA: 0x0033213E File Offset: 0x0033033E
		internal override int ElementTypeId
		{
			get
			{
				return 11120;
			}
		}

		// Token: 0x060170BE RID: 94398 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007A78 RID: 31352
		// (get) Token: 0x060170BF RID: 94399 RVA: 0x00332145 File Offset: 0x00330345
		internal override string[] AttributeTagNames
		{
			get
			{
				return AutoFilter.attributeTagNames;
			}
		}

		// Token: 0x17007A79 RID: 31353
		// (get) Token: 0x060170C0 RID: 94400 RVA: 0x0033214C File Offset: 0x0033034C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AutoFilter.attributeNamespaceIds;
			}
		}

		// Token: 0x17007A7A RID: 31354
		// (get) Token: 0x060170C1 RID: 94401 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060170C2 RID: 94402 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x060170C3 RID: 94403 RVA: 0x00293ECF File Offset: 0x002920CF
		public AutoFilter()
		{
		}

		// Token: 0x060170C4 RID: 94404 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AutoFilter(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060170C5 RID: 94405 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AutoFilter(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060170C6 RID: 94406 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AutoFilter(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060170C7 RID: 94407 RVA: 0x00332154 File Offset: 0x00330354
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "filterColumn" == name)
			{
				return new FilterColumn();
			}
			if (22 == namespaceId && "sortState" == name)
			{
				return new SortState();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x060170C8 RID: 94408 RVA: 0x00303BE4 File Offset: 0x00301DE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060170C9 RID: 94409 RVA: 0x003321AA File Offset: 0x003303AA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoFilter>(deep);
		}

		// Token: 0x060170CA RID: 94410 RVA: 0x003321B4 File Offset: 0x003303B4
		// Note: this type is marked as 'beforefieldinit'.
		static AutoFilter()
		{
			byte[] array = new byte[1];
			AutoFilter.attributeNamespaceIds = array;
		}

		// Token: 0x04009AE1 RID: 39649
		private const string tagName = "autoFilter";

		// Token: 0x04009AE2 RID: 39650
		private const byte tagNsId = 22;

		// Token: 0x04009AE3 RID: 39651
		internal const int ElementTypeIdConst = 11120;

		// Token: 0x04009AE4 RID: 39652
		private static string[] attributeTagNames = new string[] { "ref" };

		// Token: 0x04009AE5 RID: 39653
		private static byte[] attributeNamespaceIds;
	}
}
