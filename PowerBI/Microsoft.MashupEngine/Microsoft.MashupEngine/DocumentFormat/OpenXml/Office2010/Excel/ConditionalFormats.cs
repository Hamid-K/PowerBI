using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002422 RID: 9250
	[ChildElementInfo(typeof(ConditionalFormat), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConditionalFormats : OpenXmlCompositeElement
	{
		// Token: 0x17004F66 RID: 20326
		// (get) Token: 0x06010F8A RID: 69514 RVA: 0x002E9357 File Offset: 0x002E7557
		public override string LocalName
		{
			get
			{
				return "conditionalFormats";
			}
		}

		// Token: 0x17004F67 RID: 20327
		// (get) Token: 0x06010F8B RID: 69515 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F68 RID: 20328
		// (get) Token: 0x06010F8C RID: 69516 RVA: 0x002E935E File Offset: 0x002E755E
		internal override int ElementTypeId
		{
			get
			{
				return 12974;
			}
		}

		// Token: 0x06010F8D RID: 69517 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F69 RID: 20329
		// (get) Token: 0x06010F8E RID: 69518 RVA: 0x002E9365 File Offset: 0x002E7565
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormats.attributeTagNames;
			}
		}

		// Token: 0x17004F6A RID: 20330
		// (get) Token: 0x06010F8F RID: 69519 RVA: 0x002E936C File Offset: 0x002E756C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormats.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F6B RID: 20331
		// (get) Token: 0x06010F90 RID: 69520 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010F91 RID: 69521 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010F92 RID: 69522 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormats()
		{
		}

		// Token: 0x06010F93 RID: 69523 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F94 RID: 69524 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F95 RID: 69525 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010F96 RID: 69526 RVA: 0x002E9373 File Offset: 0x002E7573
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "conditionalFormat" == name)
			{
				return new ConditionalFormat();
			}
			return null;
		}

		// Token: 0x06010F97 RID: 69527 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010F98 RID: 69528 RVA: 0x002E938E File Offset: 0x002E758E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormats>(deep);
		}

		// Token: 0x06010F99 RID: 69529 RVA: 0x002E9398 File Offset: 0x002E7598
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormats()
		{
			byte[] array = new byte[1];
			ConditionalFormats.attributeNamespaceIds = array;
		}

		// Token: 0x0400771B RID: 30491
		private const string tagName = "conditionalFormats";

		// Token: 0x0400771C RID: 30492
		private const byte tagNsId = 53;

		// Token: 0x0400771D RID: 30493
		internal const int ElementTypeIdConst = 12974;

		// Token: 0x0400771E RID: 30494
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400771F RID: 30495
		private static byte[] attributeNamespaceIds;
	}
}
