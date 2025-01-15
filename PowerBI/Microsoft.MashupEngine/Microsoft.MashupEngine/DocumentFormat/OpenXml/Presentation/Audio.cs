using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A20 RID: 10784
	[ChildElementInfo(typeof(CommonMediaNode))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Audio : OpenXmlCompositeElement
	{
		// Token: 0x1700705C RID: 28764
		// (get) Token: 0x06015A13 RID: 88595 RVA: 0x003216DD File Offset: 0x0031F8DD
		public override string LocalName
		{
			get
			{
				return "audio";
			}
		}

		// Token: 0x1700705D RID: 28765
		// (get) Token: 0x06015A14 RID: 88596 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700705E RID: 28766
		// (get) Token: 0x06015A15 RID: 88597 RVA: 0x003216E4 File Offset: 0x0031F8E4
		internal override int ElementTypeId
		{
			get
			{
				return 12210;
			}
		}

		// Token: 0x06015A16 RID: 88598 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700705F RID: 28767
		// (get) Token: 0x06015A17 RID: 88599 RVA: 0x003216EB File Offset: 0x0031F8EB
		internal override string[] AttributeTagNames
		{
			get
			{
				return Audio.attributeTagNames;
			}
		}

		// Token: 0x17007060 RID: 28768
		// (get) Token: 0x06015A18 RID: 88600 RVA: 0x003216F2 File Offset: 0x0031F8F2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Audio.attributeNamespaceIds;
			}
		}

		// Token: 0x17007061 RID: 28769
		// (get) Token: 0x06015A19 RID: 88601 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015A1A RID: 88602 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "isNarration")]
		public BooleanValue IsNarration
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

		// Token: 0x06015A1B RID: 88603 RVA: 0x00293ECF File Offset: 0x002920CF
		public Audio()
		{
		}

		// Token: 0x06015A1C RID: 88604 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Audio(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A1D RID: 88605 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Audio(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A1E RID: 88606 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Audio(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015A1F RID: 88607 RVA: 0x003216F9 File Offset: 0x0031F8F9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cMediaNode" == name)
			{
				return new CommonMediaNode();
			}
			return null;
		}

		// Token: 0x17007062 RID: 28770
		// (get) Token: 0x06015A20 RID: 88608 RVA: 0x00321714 File Offset: 0x0031F914
		internal override string[] ElementTagNames
		{
			get
			{
				return Audio.eleTagNames;
			}
		}

		// Token: 0x17007063 RID: 28771
		// (get) Token: 0x06015A21 RID: 88609 RVA: 0x0032171B File Offset: 0x0031F91B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Audio.eleNamespaceIds;
			}
		}

		// Token: 0x17007064 RID: 28772
		// (get) Token: 0x06015A22 RID: 88610 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007065 RID: 28773
		// (get) Token: 0x06015A23 RID: 88611 RVA: 0x00321722 File Offset: 0x0031F922
		// (set) Token: 0x06015A24 RID: 88612 RVA: 0x0032172B File Offset: 0x0031F92B
		public CommonMediaNode CommonMediaNode
		{
			get
			{
				return base.GetElement<CommonMediaNode>(0);
			}
			set
			{
				base.SetElement<CommonMediaNode>(0, value);
			}
		}

		// Token: 0x06015A25 RID: 88613 RVA: 0x00321735 File Offset: 0x0031F935
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "isNarration" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015A26 RID: 88614 RVA: 0x00321755 File Offset: 0x0031F955
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Audio>(deep);
		}

		// Token: 0x06015A27 RID: 88615 RVA: 0x00321760 File Offset: 0x0031F960
		// Note: this type is marked as 'beforefieldinit'.
		static Audio()
		{
			byte[] array = new byte[1];
			Audio.attributeNamespaceIds = array;
			Audio.eleTagNames = new string[] { "cMediaNode" };
			Audio.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x04009429 RID: 37929
		private const string tagName = "audio";

		// Token: 0x0400942A RID: 37930
		private const byte tagNsId = 24;

		// Token: 0x0400942B RID: 37931
		internal const int ElementTypeIdConst = 12210;

		// Token: 0x0400942C RID: 37932
		private static string[] attributeTagNames = new string[] { "isNarration" };

		// Token: 0x0400942D RID: 37933
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400942E RID: 37934
		private static readonly string[] eleTagNames;

		// Token: 0x0400942F RID: 37935
		private static readonly byte[] eleNamespaceIds;
	}
}
