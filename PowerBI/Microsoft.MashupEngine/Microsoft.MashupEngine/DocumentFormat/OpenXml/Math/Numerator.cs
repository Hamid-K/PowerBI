using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002993 RID: 10643
	[GeneratedCode("DomGen", "2.0")]
	internal class Numerator : OfficeMathArgumentType
	{
		// Token: 0x17006CD5 RID: 27861
		// (get) Token: 0x06015249 RID: 86601 RVA: 0x0031C352 File Offset: 0x0031A552
		public override string LocalName
		{
			get
			{
				return "num";
			}
		}

		// Token: 0x17006CD6 RID: 27862
		// (get) Token: 0x0601524A RID: 86602 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CD7 RID: 27863
		// (get) Token: 0x0601524B RID: 86603 RVA: 0x0031C359 File Offset: 0x0031A559
		internal override int ElementTypeId
		{
			get
			{
				return 10903;
			}
		}

		// Token: 0x0601524C RID: 86604 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601524D RID: 86605 RVA: 0x0031C326 File Offset: 0x0031A526
		public Numerator()
		{
		}

		// Token: 0x0601524E RID: 86606 RVA: 0x0031C32E File Offset: 0x0031A52E
		public Numerator(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601524F RID: 86607 RVA: 0x0031C337 File Offset: 0x0031A537
		public Numerator(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015250 RID: 86608 RVA: 0x0031C340 File Offset: 0x0031A540
		public Numerator(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015251 RID: 86609 RVA: 0x0031C360 File Offset: 0x0031A560
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Numerator>(deep);
		}

		// Token: 0x040091CB RID: 37323
		private const string tagName = "num";

		// Token: 0x040091CC RID: 37324
		private const byte tagNsId = 21;

		// Token: 0x040091CD RID: 37325
		internal const int ElementTypeIdConst = 10903;
	}
}
