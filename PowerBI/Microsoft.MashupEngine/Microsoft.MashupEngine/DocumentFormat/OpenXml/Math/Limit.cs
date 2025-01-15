using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002996 RID: 10646
	[GeneratedCode("DomGen", "2.0")]
	internal class Limit : OfficeMathArgumentType
	{
		// Token: 0x17006CDE RID: 27870
		// (get) Token: 0x06015264 RID: 86628 RVA: 0x0031C397 File Offset: 0x0031A597
		public override string LocalName
		{
			get
			{
				return "lim";
			}
		}

		// Token: 0x17006CDF RID: 27871
		// (get) Token: 0x06015265 RID: 86629 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CE0 RID: 27872
		// (get) Token: 0x06015266 RID: 86630 RVA: 0x0031C39E File Offset: 0x0031A59E
		internal override int ElementTypeId
		{
			get
			{
				return 10910;
			}
		}

		// Token: 0x06015267 RID: 86631 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015268 RID: 86632 RVA: 0x0031C326 File Offset: 0x0031A526
		public Limit()
		{
		}

		// Token: 0x06015269 RID: 86633 RVA: 0x0031C32E File Offset: 0x0031A52E
		public Limit(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601526A RID: 86634 RVA: 0x0031C337 File Offset: 0x0031A537
		public Limit(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601526B RID: 86635 RVA: 0x0031C340 File Offset: 0x0031A540
		public Limit(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601526C RID: 86636 RVA: 0x0031C3A5 File Offset: 0x0031A5A5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Limit>(deep);
		}

		// Token: 0x040091D4 RID: 37332
		private const string tagName = "lim";

		// Token: 0x040091D5 RID: 37333
		private const byte tagNsId = 21;

		// Token: 0x040091D6 RID: 37334
		internal const int ElementTypeIdConst = 10910;
	}
}
