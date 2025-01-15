using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B70 RID: 11120
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotArea))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class CalculatedItem : OpenXmlCompositeElement
	{
		// Token: 0x17007952 RID: 31058
		// (get) Token: 0x06016E62 RID: 93794 RVA: 0x0033046E File Offset: 0x0032E66E
		public override string LocalName
		{
			get
			{
				return "calculatedItem";
			}
		}

		// Token: 0x17007953 RID: 31059
		// (get) Token: 0x06016E63 RID: 93795 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007954 RID: 31060
		// (get) Token: 0x06016E64 RID: 93796 RVA: 0x00330475 File Offset: 0x0032E675
		internal override int ElementTypeId
		{
			get
			{
				return 11100;
			}
		}

		// Token: 0x06016E65 RID: 93797 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007955 RID: 31061
		// (get) Token: 0x06016E66 RID: 93798 RVA: 0x0033047C File Offset: 0x0032E67C
		internal override string[] AttributeTagNames
		{
			get
			{
				return CalculatedItem.attributeTagNames;
			}
		}

		// Token: 0x17007956 RID: 31062
		// (get) Token: 0x06016E67 RID: 93799 RVA: 0x00330483 File Offset: 0x0032E683
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CalculatedItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17007957 RID: 31063
		// (get) Token: 0x06016E68 RID: 93800 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016E69 RID: 93801 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "field")]
		public UInt32Value Field
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

		// Token: 0x17007958 RID: 31064
		// (get) Token: 0x06016E6A RID: 93802 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06016E6B RID: 93803 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "formula")]
		public StringValue Formula
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06016E6C RID: 93804 RVA: 0x00293ECF File Offset: 0x002920CF
		public CalculatedItem()
		{
		}

		// Token: 0x06016E6D RID: 93805 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CalculatedItem(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E6E RID: 93806 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CalculatedItem(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E6F RID: 93807 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CalculatedItem(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016E70 RID: 93808 RVA: 0x0033048A File Offset: 0x0032E68A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotArea" == name)
			{
				return new PivotArea();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007959 RID: 31065
		// (get) Token: 0x06016E71 RID: 93809 RVA: 0x003304BD File Offset: 0x0032E6BD
		internal override string[] ElementTagNames
		{
			get
			{
				return CalculatedItem.eleTagNames;
			}
		}

		// Token: 0x1700795A RID: 31066
		// (get) Token: 0x06016E72 RID: 93810 RVA: 0x003304C4 File Offset: 0x0032E6C4
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CalculatedItem.eleNamespaceIds;
			}
		}

		// Token: 0x1700795B RID: 31067
		// (get) Token: 0x06016E73 RID: 93811 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700795C RID: 31068
		// (get) Token: 0x06016E74 RID: 93812 RVA: 0x003304CB File Offset: 0x0032E6CB
		// (set) Token: 0x06016E75 RID: 93813 RVA: 0x003304D4 File Offset: 0x0032E6D4
		public PivotArea PivotArea
		{
			get
			{
				return base.GetElement<PivotArea>(0);
			}
			set
			{
				base.SetElement<PivotArea>(0, value);
			}
		}

		// Token: 0x1700795D RID: 31069
		// (get) Token: 0x06016E76 RID: 93814 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x06016E77 RID: 93815 RVA: 0x002E96F3 File Offset: 0x002E78F3
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06016E78 RID: 93816 RVA: 0x003304DE File Offset: 0x0032E6DE
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "field" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "formula" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016E79 RID: 93817 RVA: 0x00330514 File Offset: 0x0032E714
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculatedItem>(deep);
		}

		// Token: 0x06016E7A RID: 93818 RVA: 0x00330520 File Offset: 0x0032E720
		// Note: this type is marked as 'beforefieldinit'.
		static CalculatedItem()
		{
			byte[] array = new byte[2];
			CalculatedItem.attributeNamespaceIds = array;
			CalculatedItem.eleTagNames = new string[] { "pivotArea", "extLst" };
			CalculatedItem.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009A66 RID: 39526
		private const string tagName = "calculatedItem";

		// Token: 0x04009A67 RID: 39527
		private const byte tagNsId = 22;

		// Token: 0x04009A68 RID: 39528
		internal const int ElementTypeIdConst = 11100;

		// Token: 0x04009A69 RID: 39529
		private static string[] attributeTagNames = new string[] { "field", "formula" };

		// Token: 0x04009A6A RID: 39530
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009A6B RID: 39531
		private static readonly string[] eleTagNames;

		// Token: 0x04009A6C RID: 39532
		private static readonly byte[] eleNamespaceIds;
	}
}
