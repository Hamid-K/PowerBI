using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002999 RID: 10649
	[GeneratedCode("DomGen", "2.0")]
	internal class Degree : OfficeMathArgumentType
	{
		// Token: 0x17006CE7 RID: 27879
		// (get) Token: 0x0601527F RID: 86655 RVA: 0x0031C3DC File Offset: 0x0031A5DC
		public override string LocalName
		{
			get
			{
				return "deg";
			}
		}

		// Token: 0x17006CE8 RID: 27880
		// (get) Token: 0x06015280 RID: 86656 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CE9 RID: 27881
		// (get) Token: 0x06015281 RID: 86657 RVA: 0x0031C3E3 File Offset: 0x0031A5E3
		internal override int ElementTypeId
		{
			get
			{
				return 10937;
			}
		}

		// Token: 0x06015282 RID: 86658 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015283 RID: 86659 RVA: 0x0031C326 File Offset: 0x0031A526
		public Degree()
		{
		}

		// Token: 0x06015284 RID: 86660 RVA: 0x0031C32E File Offset: 0x0031A52E
		public Degree(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015285 RID: 86661 RVA: 0x0031C337 File Offset: 0x0031A537
		public Degree(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015286 RID: 86662 RVA: 0x0031C340 File Offset: 0x0031A540
		public Degree(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015287 RID: 86663 RVA: 0x0031C3EA File Offset: 0x0031A5EA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Degree>(deep);
		}

		// Token: 0x040091DD RID: 37341
		private const string tagName = "deg";

		// Token: 0x040091DE RID: 37342
		private const byte tagNsId = 21;

		// Token: 0x040091DF RID: 37343
		internal const int ElementTypeIdConst = 10937;
	}
}
