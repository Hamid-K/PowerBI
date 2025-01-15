using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026D0 RID: 9936
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class AudioFromFile : OpenXmlCompositeElement
	{
		// Token: 0x17005D7A RID: 23930
		// (get) Token: 0x06012F1A RID: 77594 RVA: 0x00301412 File Offset: 0x002FF612
		public override string LocalName
		{
			get
			{
				return "audioFile";
			}
		}

		// Token: 0x17005D7B RID: 23931
		// (get) Token: 0x06012F1B RID: 77595 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005D7C RID: 23932
		// (get) Token: 0x06012F1C RID: 77596 RVA: 0x00301419 File Offset: 0x002FF619
		internal override int ElementTypeId
		{
			get
			{
				return 10003;
			}
		}

		// Token: 0x06012F1D RID: 77597 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005D7D RID: 23933
		// (get) Token: 0x06012F1E RID: 77598 RVA: 0x00301420 File Offset: 0x002FF620
		internal override string[] AttributeTagNames
		{
			get
			{
				return AudioFromFile.attributeTagNames;
			}
		}

		// Token: 0x17005D7E RID: 23934
		// (get) Token: 0x06012F1F RID: 77599 RVA: 0x00301427 File Offset: 0x002FF627
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AudioFromFile.attributeNamespaceIds;
			}
		}

		// Token: 0x17005D7F RID: 23935
		// (get) Token: 0x06012F20 RID: 77600 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012F21 RID: 77601 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012F22 RID: 77602 RVA: 0x00293ECF File Offset: 0x002920CF
		public AudioFromFile()
		{
		}

		// Token: 0x06012F23 RID: 77603 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AudioFromFile(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012F24 RID: 77604 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AudioFromFile(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012F25 RID: 77605 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AudioFromFile(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012F26 RID: 77606 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005D80 RID: 23936
		// (get) Token: 0x06012F27 RID: 77607 RVA: 0x0030142E File Offset: 0x002FF62E
		internal override string[] ElementTagNames
		{
			get
			{
				return AudioFromFile.eleTagNames;
			}
		}

		// Token: 0x17005D81 RID: 23937
		// (get) Token: 0x06012F28 RID: 77608 RVA: 0x00301435 File Offset: 0x002FF635
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AudioFromFile.eleNamespaceIds;
			}
		}

		// Token: 0x17005D82 RID: 23938
		// (get) Token: 0x06012F29 RID: 77609 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005D83 RID: 23939
		// (get) Token: 0x06012F2A RID: 77610 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x06012F2B RID: 77611 RVA: 0x002FA750 File Offset: 0x002F8950
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

		// Token: 0x06012F2C RID: 77612 RVA: 0x0030143C File Offset: 0x002FF63C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "link" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012F2D RID: 77613 RVA: 0x0030145E File Offset: 0x002FF65E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AudioFromFile>(deep);
		}

		// Token: 0x040083CB RID: 33739
		private const string tagName = "audioFile";

		// Token: 0x040083CC RID: 33740
		private const byte tagNsId = 10;

		// Token: 0x040083CD RID: 33741
		internal const int ElementTypeIdConst = 10003;

		// Token: 0x040083CE RID: 33742
		private static string[] attributeTagNames = new string[] { "link" };

		// Token: 0x040083CF RID: 33743
		private static byte[] attributeNamespaceIds = new byte[] { 19 };

		// Token: 0x040083D0 RID: 33744
		private static readonly string[] eleTagNames = new string[] { "extLst" };

		// Token: 0x040083D1 RID: 33745
		private static readonly byte[] eleNamespaceIds = new byte[] { 10 };
	}
}
