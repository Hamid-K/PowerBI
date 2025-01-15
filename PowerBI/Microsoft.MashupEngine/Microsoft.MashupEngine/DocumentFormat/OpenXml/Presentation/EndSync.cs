using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A14 RID: 10772
	[GeneratedCode("DomGen", "2.0")]
	internal class EndSync : TimeListConditionalType
	{
		// Token: 0x17006FD2 RID: 28626
		// (get) Token: 0x060158EE RID: 88302 RVA: 0x00320910 File Offset: 0x0031EB10
		public override string LocalName
		{
			get
			{
				return "endSync";
			}
		}

		// Token: 0x17006FD3 RID: 28627
		// (get) Token: 0x060158EF RID: 88303 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FD4 RID: 28628
		// (get) Token: 0x060158F0 RID: 88304 RVA: 0x00320917 File Offset: 0x0031EB17
		internal override int ElementTypeId
		{
			get
			{
				return 12362;
			}
		}

		// Token: 0x060158F1 RID: 88305 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060158F2 RID: 88306 RVA: 0x003208E4 File Offset: 0x0031EAE4
		public EndSync()
		{
		}

		// Token: 0x060158F3 RID: 88307 RVA: 0x003208EC File Offset: 0x0031EAEC
		public EndSync(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060158F4 RID: 88308 RVA: 0x003208F5 File Offset: 0x0031EAF5
		public EndSync(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060158F5 RID: 88309 RVA: 0x003208FE File Offset: 0x0031EAFE
		public EndSync(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060158F6 RID: 88310 RVA: 0x0032091E File Offset: 0x0031EB1E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndSync>(deep);
		}

		// Token: 0x040093DF RID: 37855
		private const string tagName = "endSync";

		// Token: 0x040093E0 RID: 37856
		private const byte tagNsId = 24;

		// Token: 0x040093E1 RID: 37857
		internal const int ElementTypeIdConst = 12362;
	}
}
