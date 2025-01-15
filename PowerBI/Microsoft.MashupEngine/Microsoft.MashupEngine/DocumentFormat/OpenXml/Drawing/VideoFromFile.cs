using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026D1 RID: 9937
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class VideoFromFile : OpenXmlCompositeElement
	{
		// Token: 0x17005D84 RID: 23940
		// (get) Token: 0x06012F2F RID: 77615 RVA: 0x003014C3 File Offset: 0x002FF6C3
		public override string LocalName
		{
			get
			{
				return "videoFile";
			}
		}

		// Token: 0x17005D85 RID: 23941
		// (get) Token: 0x06012F30 RID: 77616 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005D86 RID: 23942
		// (get) Token: 0x06012F31 RID: 77617 RVA: 0x003014CA File Offset: 0x002FF6CA
		internal override int ElementTypeId
		{
			get
			{
				return 10004;
			}
		}

		// Token: 0x06012F32 RID: 77618 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005D87 RID: 23943
		// (get) Token: 0x06012F33 RID: 77619 RVA: 0x003014D1 File Offset: 0x002FF6D1
		internal override string[] AttributeTagNames
		{
			get
			{
				return VideoFromFile.attributeTagNames;
			}
		}

		// Token: 0x17005D88 RID: 23944
		// (get) Token: 0x06012F34 RID: 77620 RVA: 0x003014D8 File Offset: 0x002FF6D8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VideoFromFile.attributeNamespaceIds;
			}
		}

		// Token: 0x17005D89 RID: 23945
		// (get) Token: 0x06012F35 RID: 77621 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012F36 RID: 77622 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012F37 RID: 77623 RVA: 0x00293ECF File Offset: 0x002920CF
		public VideoFromFile()
		{
		}

		// Token: 0x06012F38 RID: 77624 RVA: 0x00293ED7 File Offset: 0x002920D7
		public VideoFromFile(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012F39 RID: 77625 RVA: 0x00293EE0 File Offset: 0x002920E0
		public VideoFromFile(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012F3A RID: 77626 RVA: 0x00293EE9 File Offset: 0x002920E9
		public VideoFromFile(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012F3B RID: 77627 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005D8A RID: 23946
		// (get) Token: 0x06012F3C RID: 77628 RVA: 0x003014DF File Offset: 0x002FF6DF
		internal override string[] ElementTagNames
		{
			get
			{
				return VideoFromFile.eleTagNames;
			}
		}

		// Token: 0x17005D8B RID: 23947
		// (get) Token: 0x06012F3D RID: 77629 RVA: 0x003014E6 File Offset: 0x002FF6E6
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return VideoFromFile.eleNamespaceIds;
			}
		}

		// Token: 0x17005D8C RID: 23948
		// (get) Token: 0x06012F3E RID: 77630 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005D8D RID: 23949
		// (get) Token: 0x06012F3F RID: 77631 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x06012F40 RID: 77632 RVA: 0x002FA750 File Offset: 0x002F8950
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

		// Token: 0x06012F41 RID: 77633 RVA: 0x0030143C File Offset: 0x002FF63C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "link" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012F42 RID: 77634 RVA: 0x003014ED File Offset: 0x002FF6ED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VideoFromFile>(deep);
		}

		// Token: 0x040083D2 RID: 33746
		private const string tagName = "videoFile";

		// Token: 0x040083D3 RID: 33747
		private const byte tagNsId = 10;

		// Token: 0x040083D4 RID: 33748
		internal const int ElementTypeIdConst = 10004;

		// Token: 0x040083D5 RID: 33749
		private static string[] attributeTagNames = new string[] { "link" };

		// Token: 0x040083D6 RID: 33750
		private static byte[] attributeNamespaceIds = new byte[] { 19 };

		// Token: 0x040083D7 RID: 33751
		private static readonly string[] eleTagNames = new string[] { "extLst" };

		// Token: 0x040083D8 RID: 33752
		private static readonly byte[] eleNamespaceIds = new byte[] { 10 };
	}
}
