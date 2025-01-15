using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026D2 RID: 9938
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class QuickTimeFromFile : OpenXmlCompositeElement
	{
		// Token: 0x17005D8E RID: 23950
		// (get) Token: 0x06012F44 RID: 77636 RVA: 0x00301553 File Offset: 0x002FF753
		public override string LocalName
		{
			get
			{
				return "quickTimeFile";
			}
		}

		// Token: 0x17005D8F RID: 23951
		// (get) Token: 0x06012F45 RID: 77637 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005D90 RID: 23952
		// (get) Token: 0x06012F46 RID: 77638 RVA: 0x0030155A File Offset: 0x002FF75A
		internal override int ElementTypeId
		{
			get
			{
				return 10005;
			}
		}

		// Token: 0x06012F47 RID: 77639 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005D91 RID: 23953
		// (get) Token: 0x06012F48 RID: 77640 RVA: 0x00301561 File Offset: 0x002FF761
		internal override string[] AttributeTagNames
		{
			get
			{
				return QuickTimeFromFile.attributeTagNames;
			}
		}

		// Token: 0x17005D92 RID: 23954
		// (get) Token: 0x06012F49 RID: 77641 RVA: 0x00301568 File Offset: 0x002FF768
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return QuickTimeFromFile.attributeNamespaceIds;
			}
		}

		// Token: 0x17005D93 RID: 23955
		// (get) Token: 0x06012F4A RID: 77642 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012F4B RID: 77643 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "link")]
		public StringValue Link
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

		// Token: 0x06012F4C RID: 77644 RVA: 0x00293ECF File Offset: 0x002920CF
		public QuickTimeFromFile()
		{
		}

		// Token: 0x06012F4D RID: 77645 RVA: 0x00293ED7 File Offset: 0x002920D7
		public QuickTimeFromFile(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012F4E RID: 77646 RVA: 0x00293EE0 File Offset: 0x002920E0
		public QuickTimeFromFile(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012F4F RID: 77647 RVA: 0x00293EE9 File Offset: 0x002920E9
		public QuickTimeFromFile(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012F50 RID: 77648 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005D94 RID: 23956
		// (get) Token: 0x06012F51 RID: 77649 RVA: 0x0030156F File Offset: 0x002FF76F
		internal override string[] ElementTagNames
		{
			get
			{
				return QuickTimeFromFile.eleTagNames;
			}
		}

		// Token: 0x17005D95 RID: 23957
		// (get) Token: 0x06012F52 RID: 77650 RVA: 0x00301576 File Offset: 0x002FF776
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return QuickTimeFromFile.eleNamespaceIds;
			}
		}

		// Token: 0x17005D96 RID: 23958
		// (get) Token: 0x06012F53 RID: 77651 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005D97 RID: 23959
		// (get) Token: 0x06012F54 RID: 77652 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x06012F55 RID: 77653 RVA: 0x002FA750 File Offset: 0x002F8950
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06012F56 RID: 77654 RVA: 0x0030143C File Offset: 0x002FF63C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "link" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012F57 RID: 77655 RVA: 0x0030157D File Offset: 0x002FF77D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QuickTimeFromFile>(deep);
		}

		// Token: 0x040083D9 RID: 33753
		private const string tagName = "quickTimeFile";

		// Token: 0x040083DA RID: 33754
		private const byte tagNsId = 10;

		// Token: 0x040083DB RID: 33755
		internal const int ElementTypeIdConst = 10005;

		// Token: 0x040083DC RID: 33756
		private static string[] attributeTagNames = new string[] { "link" };

		// Token: 0x040083DD RID: 33757
		private static byte[] attributeNamespaceIds = new byte[] { 19 };

		// Token: 0x040083DE RID: 33758
		private static readonly string[] eleTagNames = new string[] { "extLst" };

		// Token: 0x040083DF RID: 33759
		private static readonly byte[] eleNamespaceIds = new byte[] { 10 };
	}
}
