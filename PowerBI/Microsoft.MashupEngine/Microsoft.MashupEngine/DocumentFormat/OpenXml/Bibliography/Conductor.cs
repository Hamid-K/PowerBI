using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028F4 RID: 10484
	[GeneratedCode("DomGen", "2.0")]
	internal class Conductor : NameType
	{
		// Token: 0x17006985 RID: 27013
		// (get) Token: 0x06014A9D RID: 84637 RVA: 0x00315447 File Offset: 0x00313647
		public override string LocalName
		{
			get
			{
				return "Conductor";
			}
		}

		// Token: 0x17006986 RID: 27014
		// (get) Token: 0x06014A9E RID: 84638 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006987 RID: 27015
		// (get) Token: 0x06014A9F RID: 84639 RVA: 0x0031544E File Offset: 0x0031364E
		internal override int ElementTypeId
		{
			get
			{
				return 10770;
			}
		}

		// Token: 0x06014AA0 RID: 84640 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014AA1 RID: 84641 RVA: 0x003153D6 File Offset: 0x003135D6
		public Conductor()
		{
		}

		// Token: 0x06014AA2 RID: 84642 RVA: 0x003153DE File Offset: 0x003135DE
		public Conductor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AA3 RID: 84643 RVA: 0x003153E7 File Offset: 0x003135E7
		public Conductor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AA4 RID: 84644 RVA: 0x003153F0 File Offset: 0x003135F0
		public Conductor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014AA5 RID: 84645 RVA: 0x00315455 File Offset: 0x00313655
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Conductor>(deep);
		}

		// Token: 0x04008F67 RID: 36711
		private const string tagName = "Conductor";

		// Token: 0x04008F68 RID: 36712
		private const byte tagNsId = 9;

		// Token: 0x04008F69 RID: 36713
		internal const int ElementTypeIdConst = 10770;
	}
}
