using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A0A RID: 10762
	[GeneratedCode("DomGen", "2.0")]
	internal class Sound : EmbeddedWavAudioFileType
	{
		// Token: 0x17006F92 RID: 28562
		// (get) Token: 0x06015866 RID: 88166 RVA: 0x003013FB File Offset: 0x002FF5FB
		public override string LocalName
		{
			get
			{
				return "snd";
			}
		}

		// Token: 0x17006F93 RID: 28563
		// (get) Token: 0x06015867 RID: 88167 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F94 RID: 28564
		// (get) Token: 0x06015868 RID: 88168 RVA: 0x00320390 File Offset: 0x0031E590
		internal override int ElementTypeId
		{
			get
			{
				return 12188;
			}
		}

		// Token: 0x06015869 RID: 88169 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601586B RID: 88171 RVA: 0x0032039F File Offset: 0x0031E59F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Sound>(deep);
		}

		// Token: 0x040093B2 RID: 37810
		private const string tagName = "snd";

		// Token: 0x040093B3 RID: 37811
		private const byte tagNsId = 24;

		// Token: 0x040093B4 RID: 37812
		internal const int ElementTypeIdConst = 12188;
	}
}
