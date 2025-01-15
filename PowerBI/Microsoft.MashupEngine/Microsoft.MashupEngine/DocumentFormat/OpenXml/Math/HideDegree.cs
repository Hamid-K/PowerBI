using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002982 RID: 10626
	[GeneratedCode("DomGen", "2.0")]
	internal class HideDegree : OnOffType
	{
		// Token: 0x17006C91 RID: 27793
		// (get) Token: 0x060151B0 RID: 86448 RVA: 0x0031B613 File Offset: 0x00319813
		public override string LocalName
		{
			get
			{
				return "degHide";
			}
		}

		// Token: 0x17006C92 RID: 27794
		// (get) Token: 0x060151B1 RID: 86449 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C93 RID: 27795
		// (get) Token: 0x060151B2 RID: 86450 RVA: 0x0031B61A File Offset: 0x0031981A
		internal override int ElementTypeId
		{
			get
			{
				return 10935;
			}
		}

		// Token: 0x060151B3 RID: 86451 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060151B5 RID: 86453 RVA: 0x0031B621 File Offset: 0x00319821
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HideDegree>(deep);
		}

		// Token: 0x04009192 RID: 37266
		private const string tagName = "degHide";

		// Token: 0x04009193 RID: 37267
		private const byte tagNsId = 21;

		// Token: 0x04009194 RID: 37268
		internal const int ElementTypeIdConst = 10935;
	}
}
