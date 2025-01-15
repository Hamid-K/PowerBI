using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x0200300A RID: 12298
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtContentDocPartObject : SdtDocPartType
	{
		// Token: 0x17009658 RID: 38488
		// (get) Token: 0x0601AD7E RID: 109950 RVA: 0x00368630 File Offset: 0x00366830
		public override string LocalName
		{
			get
			{
				return "docPartObj";
			}
		}

		// Token: 0x17009659 RID: 38489
		// (get) Token: 0x0601AD7F RID: 109951 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700965A RID: 38490
		// (get) Token: 0x0601AD80 RID: 109952 RVA: 0x00368637 File Offset: 0x00366837
		internal override int ElementTypeId
		{
			get
			{
				return 12150;
			}
		}

		// Token: 0x0601AD81 RID: 109953 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AD82 RID: 109954 RVA: 0x0036863E File Offset: 0x0036683E
		public SdtContentDocPartObject()
		{
		}

		// Token: 0x0601AD83 RID: 109955 RVA: 0x00368646 File Offset: 0x00366846
		public SdtContentDocPartObject(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD84 RID: 109956 RVA: 0x0036864F File Offset: 0x0036684F
		public SdtContentDocPartObject(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD85 RID: 109957 RVA: 0x00368658 File Offset: 0x00366858
		public SdtContentDocPartObject(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AD86 RID: 109958 RVA: 0x00368661 File Offset: 0x00366861
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentDocPartObject>(deep);
		}

		// Token: 0x0400AE7E RID: 44670
		private const string tagName = "docPartObj";

		// Token: 0x0400AE7F RID: 44671
		private const byte tagNsId = 23;

		// Token: 0x0400AE80 RID: 44672
		internal const int ElementTypeIdConst = 12150;
	}
}
