using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A13 RID: 10771
	[GeneratedCode("DomGen", "2.0")]
	internal class Condition : TimeListConditionalType
	{
		// Token: 0x17006FCF RID: 28623
		// (get) Token: 0x060158E5 RID: 88293 RVA: 0x003208D6 File Offset: 0x0031EAD6
		public override string LocalName
		{
			get
			{
				return "cond";
			}
		}

		// Token: 0x17006FD0 RID: 28624
		// (get) Token: 0x060158E6 RID: 88294 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FD1 RID: 28625
		// (get) Token: 0x060158E7 RID: 88295 RVA: 0x003208DD File Offset: 0x0031EADD
		internal override int ElementTypeId
		{
			get
			{
				return 12198;
			}
		}

		// Token: 0x060158E8 RID: 88296 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060158E9 RID: 88297 RVA: 0x003208E4 File Offset: 0x0031EAE4
		public Condition()
		{
		}

		// Token: 0x060158EA RID: 88298 RVA: 0x003208EC File Offset: 0x0031EAEC
		public Condition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060158EB RID: 88299 RVA: 0x003208F5 File Offset: 0x0031EAF5
		public Condition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060158EC RID: 88300 RVA: 0x003208FE File Offset: 0x0031EAFE
		public Condition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060158ED RID: 88301 RVA: 0x00320907 File Offset: 0x0031EB07
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Condition>(deep);
		}

		// Token: 0x040093DC RID: 37852
		private const string tagName = "cond";

		// Token: 0x040093DD RID: 37853
		private const byte tagNsId = 24;

		// Token: 0x040093DE RID: 37854
		internal const int ElementTypeIdConst = 12198;
	}
}
