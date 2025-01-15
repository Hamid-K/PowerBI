using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200294B RID: 10571
	[GeneratedCode("DomGen", "2.0")]
	internal class HeadingPairs : VectorVariantType
	{
		// Token: 0x17006B5D RID: 27485
		// (get) Token: 0x06014F0D RID: 85773 RVA: 0x00318EA7 File Offset: 0x003170A7
		public override string LocalName
		{
			get
			{
				return "HeadingPairs";
			}
		}

		// Token: 0x17006B5E RID: 27486
		// (get) Token: 0x06014F0E RID: 85774 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B5F RID: 27487
		// (get) Token: 0x06014F0F RID: 85775 RVA: 0x00318EAE File Offset: 0x003170AE
		internal override int ElementTypeId
		{
			get
			{
				return 11014;
			}
		}

		// Token: 0x06014F10 RID: 85776 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014F11 RID: 85777 RVA: 0x00318EB5 File Offset: 0x003170B5
		public HeadingPairs()
		{
		}

		// Token: 0x06014F12 RID: 85778 RVA: 0x00318EBD File Offset: 0x003170BD
		public HeadingPairs(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F13 RID: 85779 RVA: 0x00318EC6 File Offset: 0x003170C6
		public HeadingPairs(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F14 RID: 85780 RVA: 0x00318ECF File Offset: 0x003170CF
		public HeadingPairs(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014F15 RID: 85781 RVA: 0x00318ED8 File Offset: 0x003170D8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HeadingPairs>(deep);
		}

		// Token: 0x040090BA RID: 37050
		private const string tagName = "HeadingPairs";

		// Token: 0x040090BB RID: 37051
		private const byte tagNsId = 3;

		// Token: 0x040090BC RID: 37052
		internal const int ElementTypeIdConst = 11014;
	}
}
