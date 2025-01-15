using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D53 RID: 11603
	[GeneratedCode("DomGen", "2.0")]
	internal class DocPartGallery : StringType
	{
		// Token: 0x17008695 RID: 34453
		// (get) Token: 0x06018BE9 RID: 101353 RVA: 0x003447A2 File Offset: 0x003429A2
		public override string LocalName
		{
			get
			{
				return "docPartGallery";
			}
		}

		// Token: 0x17008696 RID: 34454
		// (get) Token: 0x06018BEA RID: 101354 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008697 RID: 34455
		// (get) Token: 0x06018BEB RID: 101355 RVA: 0x003447A9 File Offset: 0x003429A9
		internal override int ElementTypeId
		{
			get
			{
				return 11765;
			}
		}

		// Token: 0x06018BEC RID: 101356 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BEE RID: 101358 RVA: 0x003447B0 File Offset: 0x003429B0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPartGallery>(deep);
		}

		// Token: 0x0400A465 RID: 42085
		private const string tagName = "docPartGallery";

		// Token: 0x0400A466 RID: 42086
		private const byte tagNsId = 23;

		// Token: 0x0400A467 RID: 42087
		internal const int ElementTypeIdConst = 11765;
	}
}
