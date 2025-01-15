using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002975 RID: 10613
	[GeneratedCode("DomGen", "2.0")]
	internal class StrikeBottomLeftToTopRight : OnOffType
	{
		// Token: 0x17006C6A RID: 27754
		// (get) Token: 0x06015162 RID: 86370 RVA: 0x0031B4E8 File Offset: 0x003196E8
		public override string LocalName
		{
			get
			{
				return "strikeBLTR";
			}
		}

		// Token: 0x17006C6B RID: 27755
		// (get) Token: 0x06015163 RID: 86371 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C6C RID: 27756
		// (get) Token: 0x06015164 RID: 86372 RVA: 0x0031B4EF File Offset: 0x003196EF
		internal override int ElementTypeId
		{
			get
			{
				return 10886;
			}
		}

		// Token: 0x06015165 RID: 86373 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015167 RID: 86375 RVA: 0x0031B4F6 File Offset: 0x003196F6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StrikeBottomLeftToTopRight>(deep);
		}

		// Token: 0x0400916B RID: 37227
		private const string tagName = "strikeBLTR";

		// Token: 0x0400916C RID: 37228
		private const byte tagNsId = 21;

		// Token: 0x0400916D RID: 37229
		internal const int ElementTypeIdConst = 10886;
	}
}
