using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026CE RID: 9934
	[GeneratedCode("DomGen", "2.0")]
	internal class WaveAudioFile : EmbeddedWavAudioFileType
	{
		// Token: 0x17005D74 RID: 23924
		// (get) Token: 0x06012F0E RID: 77582 RVA: 0x003013DC File Offset: 0x002FF5DC
		public override string LocalName
		{
			get
			{
				return "wavAudioFile";
			}
		}

		// Token: 0x17005D75 RID: 23925
		// (get) Token: 0x06012F0F RID: 77583 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005D76 RID: 23926
		// (get) Token: 0x06012F10 RID: 77584 RVA: 0x003013E3 File Offset: 0x002FF5E3
		internal override int ElementTypeId
		{
			get
			{
				return 10002;
			}
		}

		// Token: 0x06012F11 RID: 77585 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012F13 RID: 77587 RVA: 0x003013F2 File Offset: 0x002FF5F2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WaveAudioFile>(deep);
		}

		// Token: 0x040083C5 RID: 33733
		private const string tagName = "wavAudioFile";

		// Token: 0x040083C6 RID: 33734
		private const byte tagNsId = 10;

		// Token: 0x040083C7 RID: 33735
		internal const int ElementTypeIdConst = 10002;
	}
}
