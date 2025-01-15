using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002972 RID: 10610
	[GeneratedCode("DomGen", "2.0")]
	internal class HideRight : OnOffType
	{
		// Token: 0x17006C61 RID: 27745
		// (get) Token: 0x06015150 RID: 86352 RVA: 0x0031B4A3 File Offset: 0x003196A3
		public override string LocalName
		{
			get
			{
				return "hideRight";
			}
		}

		// Token: 0x17006C62 RID: 27746
		// (get) Token: 0x06015151 RID: 86353 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C63 RID: 27747
		// (get) Token: 0x06015152 RID: 86354 RVA: 0x0031B4AA File Offset: 0x003196AA
		internal override int ElementTypeId
		{
			get
			{
				return 10883;
			}
		}

		// Token: 0x06015153 RID: 86355 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015155 RID: 86357 RVA: 0x0031B4B1 File Offset: 0x003196B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HideRight>(deep);
		}

		// Token: 0x04009162 RID: 37218
		private const string tagName = "hideRight";

		// Token: 0x04009163 RID: 37219
		private const byte tagNsId = 21;

		// Token: 0x04009164 RID: 37220
		internal const int ElementTypeIdConst = 10883;
	}
}
