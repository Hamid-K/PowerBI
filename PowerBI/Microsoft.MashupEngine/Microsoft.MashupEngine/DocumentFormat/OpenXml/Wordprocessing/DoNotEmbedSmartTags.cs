using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DE0 RID: 11744
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotEmbedSmartTags : OnOffType
	{
		// Token: 0x1700883C RID: 34876
		// (get) Token: 0x06018F38 RID: 102200 RVA: 0x0034542D File Offset: 0x0034362D
		public override string LocalName
		{
			get
			{
				return "doNotEmbedSmartTags";
			}
		}

		// Token: 0x1700883D RID: 34877
		// (get) Token: 0x06018F39 RID: 102201 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700883E RID: 34878
		// (get) Token: 0x06018F3A RID: 102202 RVA: 0x00345434 File Offset: 0x00343634
		internal override int ElementTypeId
		{
			get
			{
				return 12052;
			}
		}

		// Token: 0x06018F3B RID: 102203 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F3D RID: 102205 RVA: 0x0034543B File Offset: 0x0034363B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotEmbedSmartTags>(deep);
		}

		// Token: 0x0400A60B RID: 42507
		private const string tagName = "doNotEmbedSmartTags";

		// Token: 0x0400A60C RID: 42508
		private const byte tagNsId = 23;

		// Token: 0x0400A60D RID: 42509
		internal const int ElementTypeIdConst = 12052;
	}
}
