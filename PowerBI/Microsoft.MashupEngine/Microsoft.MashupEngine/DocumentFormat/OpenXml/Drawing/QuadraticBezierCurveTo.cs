using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027CF RID: 10191
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Point))]
	internal class QuadraticBezierCurveTo : OpenXmlCompositeElement
	{
		// Token: 0x170063CB RID: 25547
		// (get) Token: 0x06013D10 RID: 81168 RVA: 0x0030BFB7 File Offset: 0x0030A1B7
		public override string LocalName
		{
			get
			{
				return "quadBezTo";
			}
		}

		// Token: 0x170063CC RID: 25548
		// (get) Token: 0x06013D11 RID: 81169 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063CD RID: 25549
		// (get) Token: 0x06013D12 RID: 81170 RVA: 0x0030BFBE File Offset: 0x0030A1BE
		internal override int ElementTypeId
		{
			get
			{
				return 10225;
			}
		}

		// Token: 0x06013D13 RID: 81171 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013D14 RID: 81172 RVA: 0x00293ECF File Offset: 0x002920CF
		public QuadraticBezierCurveTo()
		{
		}

		// Token: 0x06013D15 RID: 81173 RVA: 0x00293ED7 File Offset: 0x002920D7
		public QuadraticBezierCurveTo(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D16 RID: 81174 RVA: 0x00293EE0 File Offset: 0x002920E0
		public QuadraticBezierCurveTo(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013D17 RID: 81175 RVA: 0x00293EE9 File Offset: 0x002920E9
		public QuadraticBezierCurveTo(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013D18 RID: 81176 RVA: 0x0030BE07 File Offset: 0x0030A007
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "pt" == name)
			{
				return new Point();
			}
			return null;
		}

		// Token: 0x06013D19 RID: 81177 RVA: 0x0030BFC5 File Offset: 0x0030A1C5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<QuadraticBezierCurveTo>(deep);
		}

		// Token: 0x040087EB RID: 34795
		private const string tagName = "quadBezTo";

		// Token: 0x040087EC RID: 34796
		private const byte tagNsId = 10;

		// Token: 0x040087ED RID: 34797
		internal const int ElementTypeIdConst = 10225;
	}
}
