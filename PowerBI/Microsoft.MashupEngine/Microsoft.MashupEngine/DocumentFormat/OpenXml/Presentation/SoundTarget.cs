using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A0B RID: 10763
	[GeneratedCode("DomGen", "2.0")]
	internal class SoundTarget : EmbeddedWavAudioFileType
	{
		// Token: 0x17006F95 RID: 28565
		// (get) Token: 0x0601586C RID: 88172 RVA: 0x003203A8 File Offset: 0x0031E5A8
		public override string LocalName
		{
			get
			{
				return "sndTgt";
			}
		}

		// Token: 0x17006F96 RID: 28566
		// (get) Token: 0x0601586D RID: 88173 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F97 RID: 28567
		// (get) Token: 0x0601586E RID: 88174 RVA: 0x003203AF File Offset: 0x0031E5AF
		internal override int ElementTypeId
		{
			get
			{
				return 12367;
			}
		}

		// Token: 0x0601586F RID: 88175 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015871 RID: 88177 RVA: 0x003203B6 File Offset: 0x0031E5B6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SoundTarget>(deep);
		}

		// Token: 0x040093B5 RID: 37813
		private const string tagName = "sndTgt";

		// Token: 0x040093B6 RID: 37814
		private const byte tagNsId = 24;

		// Token: 0x040093B7 RID: 37815
		internal const int ElementTypeIdConst = 12367;
	}
}
