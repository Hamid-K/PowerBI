using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023ED RID: 9197
	[ChildElementInfo(typeof(CustomFilter), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class CustomFilters : OpenXmlCompositeElement
	{
		// Token: 0x17004DD9 RID: 19929
		// (get) Token: 0x06010C0C RID: 68620 RVA: 0x002E6B87 File Offset: 0x002E4D87
		public override string LocalName
		{
			get
			{
				return "customFilters";
			}
		}

		// Token: 0x17004DDA RID: 19930
		// (get) Token: 0x06010C0D RID: 68621 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DDB RID: 19931
		// (get) Token: 0x06010C0E RID: 68622 RVA: 0x002E6B8E File Offset: 0x002E4D8E
		internal override int ElementTypeId
		{
			get
			{
				return 12923;
			}
		}

		// Token: 0x06010C0F RID: 68623 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DDC RID: 19932
		// (get) Token: 0x06010C10 RID: 68624 RVA: 0x002E6B95 File Offset: 0x002E4D95
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomFilters.attributeTagNames;
			}
		}

		// Token: 0x17004DDD RID: 19933
		// (get) Token: 0x06010C11 RID: 68625 RVA: 0x002E6B9C File Offset: 0x002E4D9C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomFilters.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DDE RID: 19934
		// (get) Token: 0x06010C12 RID: 68626 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010C13 RID: 68627 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "and")]
		public BooleanValue And
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010C14 RID: 68628 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomFilters()
		{
		}

		// Token: 0x06010C15 RID: 68629 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomFilters(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010C16 RID: 68630 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomFilters(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010C17 RID: 68631 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomFilters(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010C18 RID: 68632 RVA: 0x002E6BA3 File Offset: 0x002E4DA3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "customFilter" == name)
			{
				return new CustomFilter();
			}
			return null;
		}

		// Token: 0x06010C19 RID: 68633 RVA: 0x002E6BBE File Offset: 0x002E4DBE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "and" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010C1A RID: 68634 RVA: 0x002E6BDE File Offset: 0x002E4DDE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomFilters>(deep);
		}

		// Token: 0x06010C1B RID: 68635 RVA: 0x002E6BE8 File Offset: 0x002E4DE8
		// Note: this type is marked as 'beforefieldinit'.
		static CustomFilters()
		{
			byte[] array = new byte[1];
			CustomFilters.attributeNamespaceIds = array;
		}

		// Token: 0x0400763A RID: 30266
		private const string tagName = "customFilters";

		// Token: 0x0400763B RID: 30267
		private const byte tagNsId = 53;

		// Token: 0x0400763C RID: 30268
		internal const int ElementTypeIdConst = 12923;

		// Token: 0x0400763D RID: 30269
		private static string[] attributeTagNames = new string[] { "and" };

		// Token: 0x0400763E RID: 30270
		private static byte[] attributeNamespaceIds;
	}
}
