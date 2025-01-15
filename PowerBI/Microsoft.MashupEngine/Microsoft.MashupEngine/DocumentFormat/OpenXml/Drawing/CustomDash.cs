using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200272D RID: 10029
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DashStop))]
	internal class CustomDash : OpenXmlCompositeElement
	{
		// Token: 0x17005FF5 RID: 24565
		// (get) Token: 0x0601345F RID: 78943 RVA: 0x00305A4F File Offset: 0x00303C4F
		public override string LocalName
		{
			get
			{
				return "custDash";
			}
		}

		// Token: 0x17005FF6 RID: 24566
		// (get) Token: 0x06013460 RID: 78944 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FF7 RID: 24567
		// (get) Token: 0x06013461 RID: 78945 RVA: 0x00305A56 File Offset: 0x00303C56
		internal override int ElementTypeId
		{
			get
			{
				return 10092;
			}
		}

		// Token: 0x06013462 RID: 78946 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013463 RID: 78947 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomDash()
		{
		}

		// Token: 0x06013464 RID: 78948 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomDash(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013465 RID: 78949 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomDash(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013466 RID: 78950 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomDash(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013467 RID: 78951 RVA: 0x00305A5D File Offset: 0x00303C5D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ds" == name)
			{
				return new DashStop();
			}
			return null;
		}

		// Token: 0x06013468 RID: 78952 RVA: 0x00305A78 File Offset: 0x00303C78
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomDash>(deep);
		}

		// Token: 0x0400856C RID: 34156
		private const string tagName = "custDash";

		// Token: 0x0400856D RID: 34157
		private const byte tagNsId = 10;

		// Token: 0x0400856E RID: 34158
		internal const int ElementTypeIdConst = 10092;
	}
}
