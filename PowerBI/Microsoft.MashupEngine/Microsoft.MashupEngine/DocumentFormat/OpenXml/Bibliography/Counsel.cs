using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028F5 RID: 10485
	[GeneratedCode("DomGen", "2.0")]
	internal class Counsel : NameType
	{
		// Token: 0x17006988 RID: 27016
		// (get) Token: 0x06014AA6 RID: 84646 RVA: 0x0031545E File Offset: 0x0031365E
		public override string LocalName
		{
			get
			{
				return "Counsel";
			}
		}

		// Token: 0x17006989 RID: 27017
		// (get) Token: 0x06014AA7 RID: 84647 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700698A RID: 27018
		// (get) Token: 0x06014AA8 RID: 84648 RVA: 0x00315465 File Offset: 0x00313665
		internal override int ElementTypeId
		{
			get
			{
				return 10771;
			}
		}

		// Token: 0x06014AA9 RID: 84649 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014AAA RID: 84650 RVA: 0x003153D6 File Offset: 0x003135D6
		public Counsel()
		{
		}

		// Token: 0x06014AAB RID: 84651 RVA: 0x003153DE File Offset: 0x003135DE
		public Counsel(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AAC RID: 84652 RVA: 0x003153E7 File Offset: 0x003135E7
		public Counsel(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AAD RID: 84653 RVA: 0x003153F0 File Offset: 0x003135F0
		public Counsel(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014AAE RID: 84654 RVA: 0x0031546C File Offset: 0x0031366C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Counsel>(deep);
		}

		// Token: 0x04008F6A RID: 36714
		private const string tagName = "Counsel";

		// Token: 0x04008F6B RID: 36715
		private const byte tagNsId = 9;

		// Token: 0x04008F6C RID: 36716
		internal const int ElementTypeIdConst = 10771;
	}
}
