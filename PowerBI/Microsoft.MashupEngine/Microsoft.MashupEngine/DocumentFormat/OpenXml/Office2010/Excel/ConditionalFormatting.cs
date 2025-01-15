using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Excel;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023F4 RID: 9204
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ConditionalFormattingRule), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ReferenceSequence))]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConditionalFormatting : OpenXmlCompositeElement
	{
		// Token: 0x17004E38 RID: 20024
		// (get) Token: 0x06010CE1 RID: 68833 RVA: 0x002E760A File Offset: 0x002E580A
		public override string LocalName
		{
			get
			{
				return "conditionalFormatting";
			}
		}

		// Token: 0x17004E39 RID: 20025
		// (get) Token: 0x06010CE2 RID: 68834 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004E3A RID: 20026
		// (get) Token: 0x06010CE3 RID: 68835 RVA: 0x002E7611 File Offset: 0x002E5811
		internal override int ElementTypeId
		{
			get
			{
				return 12930;
			}
		}

		// Token: 0x06010CE4 RID: 68836 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004E3B RID: 20027
		// (get) Token: 0x06010CE5 RID: 68837 RVA: 0x002E7618 File Offset: 0x002E5818
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormatting.attributeTagNames;
			}
		}

		// Token: 0x17004E3C RID: 20028
		// (get) Token: 0x06010CE6 RID: 68838 RVA: 0x002E761F File Offset: 0x002E581F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormatting.attributeNamespaceIds;
			}
		}

		// Token: 0x17004E3D RID: 20029
		// (get) Token: 0x06010CE7 RID: 68839 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010CE8 RID: 68840 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "pivot")]
		public BooleanValue Pivot
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

		// Token: 0x06010CE9 RID: 68841 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormatting()
		{
		}

		// Token: 0x06010CEA RID: 68842 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormatting(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010CEB RID: 68843 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormatting(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010CEC RID: 68844 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormatting(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010CED RID: 68845 RVA: 0x002E7628 File Offset: 0x002E5828
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "cfRule" == name)
			{
				return new ConditionalFormattingRule();
			}
			if (32 == namespaceId && "sqref" == name)
			{
				return new ReferenceSequence();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06010CEE RID: 68846 RVA: 0x002E767E File Offset: 0x002E587E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "pivot" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010CEF RID: 68847 RVA: 0x002E769E File Offset: 0x002E589E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormatting>(deep);
		}

		// Token: 0x06010CF0 RID: 68848 RVA: 0x002E76A8 File Offset: 0x002E58A8
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormatting()
		{
			byte[] array = new byte[1];
			ConditionalFormatting.attributeNamespaceIds = array;
		}

		// Token: 0x04007661 RID: 30305
		private const string tagName = "conditionalFormatting";

		// Token: 0x04007662 RID: 30306
		private const byte tagNsId = 53;

		// Token: 0x04007663 RID: 30307
		internal const int ElementTypeIdConst = 12930;

		// Token: 0x04007664 RID: 30308
		private static string[] attributeTagNames = new string[] { "pivot" };

		// Token: 0x04007665 RID: 30309
		private static byte[] attributeNamespaceIds;
	}
}
