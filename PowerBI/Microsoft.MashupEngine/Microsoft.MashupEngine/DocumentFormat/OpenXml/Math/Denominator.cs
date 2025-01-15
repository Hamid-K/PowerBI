using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002994 RID: 10644
	[GeneratedCode("DomGen", "2.0")]
	internal class Denominator : OfficeMathArgumentType
	{
		// Token: 0x17006CD8 RID: 27864
		// (get) Token: 0x06015252 RID: 86610 RVA: 0x0031C369 File Offset: 0x0031A569
		public override string LocalName
		{
			get
			{
				return "den";
			}
		}

		// Token: 0x17006CD9 RID: 27865
		// (get) Token: 0x06015253 RID: 86611 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CDA RID: 27866
		// (get) Token: 0x06015254 RID: 86612 RVA: 0x0031C370 File Offset: 0x0031A570
		internal override int ElementTypeId
		{
			get
			{
				return 10904;
			}
		}

		// Token: 0x06015255 RID: 86613 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015256 RID: 86614 RVA: 0x0031C326 File Offset: 0x0031A526
		public Denominator()
		{
		}

		// Token: 0x06015257 RID: 86615 RVA: 0x0031C32E File Offset: 0x0031A52E
		public Denominator(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015258 RID: 86616 RVA: 0x0031C337 File Offset: 0x0031A537
		public Denominator(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015259 RID: 86617 RVA: 0x0031C340 File Offset: 0x0031A540
		public Denominator(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601525A RID: 86618 RVA: 0x0031C377 File Offset: 0x0031A577
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Denominator>(deep);
		}

		// Token: 0x040091CE RID: 37326
		private const string tagName = "den";

		// Token: 0x040091CF RID: 37327
		private const byte tagNsId = 21;

		// Token: 0x040091D0 RID: 37328
		internal const int ElementTypeIdConst = 10904;
	}
}
