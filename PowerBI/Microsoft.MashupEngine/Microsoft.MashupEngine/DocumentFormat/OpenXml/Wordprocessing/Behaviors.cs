using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FD5 RID: 12245
	[ChildElementInfo(typeof(Behavior))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Behaviors : OpenXmlCompositeElement
	{
		// Token: 0x1700945A RID: 37978
		// (get) Token: 0x0601A947 RID: 108871 RVA: 0x00364740 File Offset: 0x00362940
		public override string LocalName
		{
			get
			{
				return "behaviors";
			}
		}

		// Token: 0x1700945B RID: 37979
		// (get) Token: 0x0601A948 RID: 108872 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700945C RID: 37980
		// (get) Token: 0x0601A949 RID: 108873 RVA: 0x00364747 File Offset: 0x00362947
		internal override int ElementTypeId
		{
			get
			{
				return 11952;
			}
		}

		// Token: 0x0601A94A RID: 108874 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A94B RID: 108875 RVA: 0x00293ECF File Offset: 0x002920CF
		public Behaviors()
		{
		}

		// Token: 0x0601A94C RID: 108876 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Behaviors(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A94D RID: 108877 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Behaviors(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A94E RID: 108878 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Behaviors(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A94F RID: 108879 RVA: 0x0036474E File Offset: 0x0036294E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "behavior" == name)
			{
				return new Behavior();
			}
			return null;
		}

		// Token: 0x0601A950 RID: 108880 RVA: 0x00364769 File Offset: 0x00362969
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Behaviors>(deep);
		}

		// Token: 0x0400AD9A RID: 44442
		private const string tagName = "behaviors";

		// Token: 0x0400AD9B RID: 44443
		private const byte tagNsId = 23;

		// Token: 0x0400AD9C RID: 44444
		internal const int ElementTypeIdConst = 11952;
	}
}
