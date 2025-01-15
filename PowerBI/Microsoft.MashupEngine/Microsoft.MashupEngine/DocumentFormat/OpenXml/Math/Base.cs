using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002992 RID: 10642
	[GeneratedCode("DomGen", "2.0")]
	internal class Base : OfficeMathArgumentType
	{
		// Token: 0x17006CD2 RID: 27858
		// (get) Token: 0x06015240 RID: 86592 RVA: 0x0031C318 File Offset: 0x0031A518
		public override string LocalName
		{
			get
			{
				return "e";
			}
		}

		// Token: 0x17006CD3 RID: 27859
		// (get) Token: 0x06015241 RID: 86593 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CD4 RID: 27860
		// (get) Token: 0x06015242 RID: 86594 RVA: 0x0031C31F File Offset: 0x0031A51F
		internal override int ElementTypeId
		{
			get
			{
				return 10873;
			}
		}

		// Token: 0x06015243 RID: 86595 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015244 RID: 86596 RVA: 0x0031C326 File Offset: 0x0031A526
		public Base()
		{
		}

		// Token: 0x06015245 RID: 86597 RVA: 0x0031C32E File Offset: 0x0031A52E
		public Base(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015246 RID: 86598 RVA: 0x0031C337 File Offset: 0x0031A537
		public Base(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015247 RID: 86599 RVA: 0x0031C340 File Offset: 0x0031A540
		public Base(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015248 RID: 86600 RVA: 0x0031C349 File Offset: 0x0031A549
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Base>(deep);
		}

		// Token: 0x040091C8 RID: 37320
		private const string tagName = "e";

		// Token: 0x040091C9 RID: 37321
		private const byte tagNsId = 21;

		// Token: 0x040091CA RID: 37322
		internal const int ElementTypeIdConst = 10873;
	}
}
