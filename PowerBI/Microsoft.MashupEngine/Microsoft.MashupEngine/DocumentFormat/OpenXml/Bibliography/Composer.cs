using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028F3 RID: 10483
	[GeneratedCode("DomGen", "2.0")]
	internal class Composer : NameType
	{
		// Token: 0x17006982 RID: 27010
		// (get) Token: 0x06014A94 RID: 84628 RVA: 0x00315430 File Offset: 0x00313630
		public override string LocalName
		{
			get
			{
				return "Composer";
			}
		}

		// Token: 0x17006983 RID: 27011
		// (get) Token: 0x06014A95 RID: 84629 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006984 RID: 27012
		// (get) Token: 0x06014A96 RID: 84630 RVA: 0x00315437 File Offset: 0x00313637
		internal override int ElementTypeId
		{
			get
			{
				return 10769;
			}
		}

		// Token: 0x06014A97 RID: 84631 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A98 RID: 84632 RVA: 0x003153D6 File Offset: 0x003135D6
		public Composer()
		{
		}

		// Token: 0x06014A99 RID: 84633 RVA: 0x003153DE File Offset: 0x003135DE
		public Composer(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A9A RID: 84634 RVA: 0x003153E7 File Offset: 0x003135E7
		public Composer(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014A9B RID: 84635 RVA: 0x003153F0 File Offset: 0x003135F0
		public Composer(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014A9C RID: 84636 RVA: 0x0031543E File Offset: 0x0031363E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Composer>(deep);
		}

		// Token: 0x04008F64 RID: 36708
		private const string tagName = "Composer";

		// Token: 0x04008F65 RID: 36709
		private const byte tagNsId = 9;

		// Token: 0x04008F66 RID: 36710
		internal const int ElementTypeIdConst = 10769;
	}
}
