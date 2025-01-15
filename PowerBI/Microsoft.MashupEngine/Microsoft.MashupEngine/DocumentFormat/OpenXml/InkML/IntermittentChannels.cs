using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200308C RID: 12428
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Channel))]
	internal class IntermittentChannels : OpenXmlCompositeElement
	{
		// Token: 0x17009784 RID: 38788
		// (get) Token: 0x0601B031 RID: 110641 RVA: 0x0036AB5D File Offset: 0x00368D5D
		public override string LocalName
		{
			get
			{
				return "intermittentChannels";
			}
		}

		// Token: 0x17009785 RID: 38789
		// (get) Token: 0x0601B032 RID: 110642 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009786 RID: 38790
		// (get) Token: 0x0601B033 RID: 110643 RVA: 0x0036AB64 File Offset: 0x00368D64
		internal override int ElementTypeId
		{
			get
			{
				return 12649;
			}
		}

		// Token: 0x0601B034 RID: 110644 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601B035 RID: 110645 RVA: 0x00293ECF File Offset: 0x002920CF
		public IntermittentChannels()
		{
		}

		// Token: 0x0601B036 RID: 110646 RVA: 0x00293ED7 File Offset: 0x002920D7
		public IntermittentChannels(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B037 RID: 110647 RVA: 0x00293EE0 File Offset: 0x002920E0
		public IntermittentChannels(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B038 RID: 110648 RVA: 0x00293EE9 File Offset: 0x002920E9
		public IntermittentChannels(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B039 RID: 110649 RVA: 0x0036AB6B File Offset: 0x00368D6B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "channel" == name)
			{
				return new Channel();
			}
			return null;
		}

		// Token: 0x0601B03A RID: 110650 RVA: 0x0036AB86 File Offset: 0x00368D86
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<IntermittentChannels>(deep);
		}

		// Token: 0x0400B28B RID: 45707
		private const string tagName = "intermittentChannels";

		// Token: 0x0400B28C RID: 45708
		private const byte tagNsId = 43;

		// Token: 0x0400B28D RID: 45709
		internal const int ElementTypeIdConst = 12649;
	}
}
