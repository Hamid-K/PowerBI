using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FF1 RID: 12273
	[GeneratedCode("DomGen", "2.0")]
	internal class HeaderShapeDefaults : ShapeDefaultsType
	{
		// Token: 0x1700953F RID: 38207
		// (get) Token: 0x0601AB22 RID: 109346 RVA: 0x00365FF8 File Offset: 0x003641F8
		public override string LocalName
		{
			get
			{
				return "hdrShapeDefaults";
			}
		}

		// Token: 0x17009540 RID: 38208
		// (get) Token: 0x0601AB23 RID: 109347 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009541 RID: 38209
		// (get) Token: 0x0601AB24 RID: 109348 RVA: 0x00365FFF File Offset: 0x003641FF
		internal override int ElementTypeId
		{
			get
			{
				return 12035;
			}
		}

		// Token: 0x0601AB25 RID: 109349 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AB26 RID: 109350 RVA: 0x00366006 File Offset: 0x00364206
		public HeaderShapeDefaults()
		{
		}

		// Token: 0x0601AB27 RID: 109351 RVA: 0x0036600E File Offset: 0x0036420E
		public HeaderShapeDefaults(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB28 RID: 109352 RVA: 0x00366017 File Offset: 0x00364217
		public HeaderShapeDefaults(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB29 RID: 109353 RVA: 0x00366020 File Offset: 0x00364220
		public HeaderShapeDefaults(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AB2A RID: 109354 RVA: 0x00366029 File Offset: 0x00364229
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HeaderShapeDefaults>(deep);
		}

		// Token: 0x0400AE11 RID: 44561
		private const string tagName = "hdrShapeDefaults";

		// Token: 0x0400AE12 RID: 44562
		private const byte tagNsId = 23;

		// Token: 0x0400AE13 RID: 44563
		internal const int ElementTypeIdConst = 12035;
	}
}
