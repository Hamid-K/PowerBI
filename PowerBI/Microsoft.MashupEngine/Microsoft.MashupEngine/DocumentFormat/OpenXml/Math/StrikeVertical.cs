using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002974 RID: 10612
	[GeneratedCode("DomGen", "2.0")]
	internal class StrikeVertical : OnOffType
	{
		// Token: 0x17006C67 RID: 27751
		// (get) Token: 0x0601515C RID: 86364 RVA: 0x0031B4D1 File Offset: 0x003196D1
		public override string LocalName
		{
			get
			{
				return "strikeV";
			}
		}

		// Token: 0x17006C68 RID: 27752
		// (get) Token: 0x0601515D RID: 86365 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C69 RID: 27753
		// (get) Token: 0x0601515E RID: 86366 RVA: 0x0031B4D8 File Offset: 0x003196D8
		internal override int ElementTypeId
		{
			get
			{
				return 10885;
			}
		}

		// Token: 0x0601515F RID: 86367 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015161 RID: 86369 RVA: 0x0031B4DF File Offset: 0x003196DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StrikeVertical>(deep);
		}

		// Token: 0x04009168 RID: 37224
		private const string tagName = "strikeV";

		// Token: 0x04009169 RID: 37225
		private const byte tagNsId = 21;

		// Token: 0x0400916A RID: 37226
		internal const int ElementTypeIdConst = 10885;
	}
}
