using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026CF RID: 9935
	[GeneratedCode("DomGen", "2.0")]
	internal class HyperlinkSound : EmbeddedWavAudioFileType
	{
		// Token: 0x17005D77 RID: 23927
		// (get) Token: 0x06012F14 RID: 77588 RVA: 0x003013FB File Offset: 0x002FF5FB
		public override string LocalName
		{
			get
			{
				return "snd";
			}
		}

		// Token: 0x17005D78 RID: 23928
		// (get) Token: 0x06012F15 RID: 77589 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005D79 RID: 23929
		// (get) Token: 0x06012F16 RID: 77590 RVA: 0x00301402 File Offset: 0x002FF602
		internal override int ElementTypeId
		{
			get
			{
				return 10165;
			}
		}

		// Token: 0x06012F17 RID: 77591 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012F19 RID: 77593 RVA: 0x00301409 File Offset: 0x002FF609
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HyperlinkSound>(deep);
		}

		// Token: 0x040083C8 RID: 33736
		private const string tagName = "snd";

		// Token: 0x040083C9 RID: 33737
		private const byte tagNsId = 10;

		// Token: 0x040083CA RID: 33738
		internal const int ElementTypeIdConst = 10165;
	}
}
