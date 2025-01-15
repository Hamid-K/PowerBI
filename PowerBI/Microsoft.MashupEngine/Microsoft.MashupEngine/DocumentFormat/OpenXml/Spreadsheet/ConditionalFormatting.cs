using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C9B RID: 11419
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(ConditionalFormattingRule))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ConditionalFormatting : OpenXmlCompositeElement
	{
		// Token: 0x17008420 RID: 33824
		// (get) Token: 0x0601861D RID: 99869 RVA: 0x002E760A File Offset: 0x002E580A
		public override string LocalName
		{
			get
			{
				return "conditionalFormatting";
			}
		}

		// Token: 0x17008421 RID: 33825
		// (get) Token: 0x0601861E RID: 99870 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008422 RID: 33826
		// (get) Token: 0x0601861F RID: 99871 RVA: 0x003411FB File Offset: 0x0033F3FB
		internal override int ElementTypeId
		{
			get
			{
				return 11399;
			}
		}

		// Token: 0x06018620 RID: 99872 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008423 RID: 33827
		// (get) Token: 0x06018621 RID: 99873 RVA: 0x00341202 File Offset: 0x0033F402
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormatting.attributeTagNames;
			}
		}

		// Token: 0x17008424 RID: 33828
		// (get) Token: 0x06018622 RID: 99874 RVA: 0x00341209 File Offset: 0x0033F409
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormatting.attributeNamespaceIds;
			}
		}

		// Token: 0x17008425 RID: 33829
		// (get) Token: 0x06018623 RID: 99875 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018624 RID: 99876 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008426 RID: 33830
		// (get) Token: 0x06018625 RID: 99877 RVA: 0x00335ECB File Offset: 0x003340CB
		// (set) Token: 0x06018626 RID: 99878 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sqref")]
		public ListValue<StringValue> SequenceOfReferences
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06018627 RID: 99879 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormatting()
		{
		}

		// Token: 0x06018628 RID: 99880 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormatting(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018629 RID: 99881 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormatting(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601862A RID: 99882 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormatting(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601862B RID: 99883 RVA: 0x00341210 File Offset: 0x0033F410
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cfRule" == name)
			{
				return new ConditionalFormattingRule();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x0601862C RID: 99884 RVA: 0x00341243 File Offset: 0x0033F443
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "pivot" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sqref" == name)
			{
				return new ListValue<StringValue>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601862D RID: 99885 RVA: 0x00341279 File Offset: 0x0033F479
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormatting>(deep);
		}

		// Token: 0x0601862E RID: 99886 RVA: 0x00341284 File Offset: 0x0033F484
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormatting()
		{
			byte[] array = new byte[2];
			ConditionalFormatting.attributeNamespaceIds = array;
		}

		// Token: 0x0400A002 RID: 40962
		private const string tagName = "conditionalFormatting";

		// Token: 0x0400A003 RID: 40963
		private const byte tagNsId = 22;

		// Token: 0x0400A004 RID: 40964
		internal const int ElementTypeIdConst = 11399;

		// Token: 0x0400A005 RID: 40965
		private static string[] attributeTagNames = new string[] { "pivot", "sqref" };

		// Token: 0x0400A006 RID: 40966
		private static byte[] attributeNamespaceIds;
	}
}
