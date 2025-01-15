using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200296A RID: 10602
	[GeneratedCode("DomGen", "2.0")]
	internal class NormalText : OnOffType
	{
		// Token: 0x17006C49 RID: 27721
		// (get) Token: 0x06015120 RID: 86304 RVA: 0x0031B3EB File Offset: 0x003195EB
		public override string LocalName
		{
			get
			{
				return "nor";
			}
		}

		// Token: 0x17006C4A RID: 27722
		// (get) Token: 0x06015121 RID: 86305 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C4B RID: 27723
		// (get) Token: 0x06015122 RID: 86306 RVA: 0x0031B3F2 File Offset: 0x003195F2
		internal override int ElementTypeId
		{
			get
			{
				return 10865;
			}
		}

		// Token: 0x06015123 RID: 86307 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015125 RID: 86309 RVA: 0x0031B3F9 File Offset: 0x003195F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NormalText>(deep);
		}

		// Token: 0x0400914A RID: 37194
		private const string tagName = "nor";

		// Token: 0x0400914B RID: 37195
		private const byte tagNsId = 21;

		// Token: 0x0400914C RID: 37196
		internal const int ElementTypeIdConst = 10865;
	}
}
