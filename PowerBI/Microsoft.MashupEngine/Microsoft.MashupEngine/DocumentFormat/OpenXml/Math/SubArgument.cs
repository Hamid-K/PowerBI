using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002997 RID: 10647
	[GeneratedCode("DomGen", "2.0")]
	internal class SubArgument : OfficeMathArgumentType
	{
		// Token: 0x17006CE1 RID: 27873
		// (get) Token: 0x0601526D RID: 86637 RVA: 0x0031C3AE File Offset: 0x0031A5AE
		public override string LocalName
		{
			get
			{
				return "sub";
			}
		}

		// Token: 0x17006CE2 RID: 27874
		// (get) Token: 0x0601526E RID: 86638 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CE3 RID: 27875
		// (get) Token: 0x0601526F RID: 86639 RVA: 0x0031C3B5 File Offset: 0x0031A5B5
		internal override int ElementTypeId
		{
			get
			{
				return 10927;
			}
		}

		// Token: 0x06015270 RID: 86640 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015271 RID: 86641 RVA: 0x0031C326 File Offset: 0x0031A526
		public SubArgument()
		{
		}

		// Token: 0x06015272 RID: 86642 RVA: 0x0031C32E File Offset: 0x0031A52E
		public SubArgument(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015273 RID: 86643 RVA: 0x0031C337 File Offset: 0x0031A537
		public SubArgument(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015274 RID: 86644 RVA: 0x0031C340 File Offset: 0x0031A540
		public SubArgument(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015275 RID: 86645 RVA: 0x0031C3BC File Offset: 0x0031A5BC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SubArgument>(deep);
		}

		// Token: 0x040091D7 RID: 37335
		private const string tagName = "sub";

		// Token: 0x040091D8 RID: 37336
		private const byte tagNsId = 21;

		// Token: 0x040091D9 RID: 37337
		internal const int ElementTypeIdConst = 10927;
	}
}
