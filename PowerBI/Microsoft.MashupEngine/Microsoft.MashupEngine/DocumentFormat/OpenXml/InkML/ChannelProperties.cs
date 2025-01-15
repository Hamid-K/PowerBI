using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003093 RID: 12435
	[ChildElementInfo(typeof(ChannelProperty))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ChannelProperties : OpenXmlCompositeElement
	{
		// Token: 0x170097B4 RID: 38836
		// (get) Token: 0x0601B099 RID: 110745 RVA: 0x0036AFA7 File Offset: 0x003691A7
		public override string LocalName
		{
			get
			{
				return "channelProperties";
			}
		}

		// Token: 0x170097B5 RID: 38837
		// (get) Token: 0x0601B09A RID: 110746 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x170097B6 RID: 38838
		// (get) Token: 0x0601B09B RID: 110747 RVA: 0x0036AFAE File Offset: 0x003691AE
		internal override int ElementTypeId
		{
			get
			{
				return 12656;
			}
		}

		// Token: 0x0601B09C RID: 110748 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601B09D RID: 110749 RVA: 0x00293ECF File Offset: 0x002920CF
		public ChannelProperties()
		{
		}

		// Token: 0x0601B09E RID: 110750 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ChannelProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B09F RID: 110751 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ChannelProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B0A0 RID: 110752 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ChannelProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B0A1 RID: 110753 RVA: 0x0036AFB5 File Offset: 0x003691B5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "channelProperty" == name)
			{
				return new ChannelProperty();
			}
			return null;
		}

		// Token: 0x0601B0A2 RID: 110754 RVA: 0x0036AFD0 File Offset: 0x003691D0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ChannelProperties>(deep);
		}

		// Token: 0x0400B2AC RID: 45740
		private const string tagName = "channelProperties";

		// Token: 0x0400B2AD RID: 45741
		private const byte tagNsId = 43;

		// Token: 0x0400B2AE RID: 45742
		internal const int ElementTypeIdConst = 12656;
	}
}
