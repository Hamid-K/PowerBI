using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200249F RID: 9375
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SchemeColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RgbColorModelHex), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SolidColorFillProperties : OpenXmlCompositeElement
	{
		// Token: 0x170051C0 RID: 20928
		// (get) Token: 0x060114F5 RID: 70901 RVA: 0x002ECFFB File Offset: 0x002EB1FB
		public override string LocalName
		{
			get
			{
				return "solidFill";
			}
		}

		// Token: 0x170051C1 RID: 20929
		// (get) Token: 0x060114F6 RID: 70902 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051C2 RID: 20930
		// (get) Token: 0x060114F7 RID: 70903 RVA: 0x002ED002 File Offset: 0x002EB202
		internal override int ElementTypeId
		{
			get
			{
				return 12847;
			}
		}

		// Token: 0x060114F8 RID: 70904 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060114F9 RID: 70905 RVA: 0x00293ECF File Offset: 0x002920CF
		public SolidColorFillProperties()
		{
		}

		// Token: 0x060114FA RID: 70906 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SolidColorFillProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060114FB RID: 70907 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SolidColorFillProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060114FC RID: 70908 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SolidColorFillProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060114FD RID: 70909 RVA: 0x002ED009 File Offset: 0x002EB209
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "srgbClr" == name)
			{
				return new RgbColorModelHex();
			}
			if (52 == namespaceId && "schemeClr" == name)
			{
				return new SchemeColor();
			}
			return null;
		}

		// Token: 0x170051C3 RID: 20931
		// (get) Token: 0x060114FE RID: 70910 RVA: 0x002ED03C File Offset: 0x002EB23C
		internal override string[] ElementTagNames
		{
			get
			{
				return SolidColorFillProperties.eleTagNames;
			}
		}

		// Token: 0x170051C4 RID: 20932
		// (get) Token: 0x060114FF RID: 70911 RVA: 0x002ED043 File Offset: 0x002EB243
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SolidColorFillProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170051C5 RID: 20933
		// (get) Token: 0x06011500 RID: 70912 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170051C6 RID: 20934
		// (get) Token: 0x06011501 RID: 70913 RVA: 0x002ED04A File Offset: 0x002EB24A
		// (set) Token: 0x06011502 RID: 70914 RVA: 0x002ED053 File Offset: 0x002EB253
		public RgbColorModelHex RgbColorModelHex
		{
			get
			{
				return base.GetElement<RgbColorModelHex>(0);
			}
			set
			{
				base.SetElement<RgbColorModelHex>(0, value);
			}
		}

		// Token: 0x170051C7 RID: 20935
		// (get) Token: 0x06011503 RID: 70915 RVA: 0x002ED05D File Offset: 0x002EB25D
		// (set) Token: 0x06011504 RID: 70916 RVA: 0x002ED066 File Offset: 0x002EB266
		public SchemeColor SchemeColor
		{
			get
			{
				return base.GetElement<SchemeColor>(1);
			}
			set
			{
				base.SetElement<SchemeColor>(1, value);
			}
		}

		// Token: 0x06011505 RID: 70917 RVA: 0x002ED070 File Offset: 0x002EB270
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SolidColorFillProperties>(deep);
		}

		// Token: 0x0400793D RID: 31037
		private const string tagName = "solidFill";

		// Token: 0x0400793E RID: 31038
		private const byte tagNsId = 52;

		// Token: 0x0400793F RID: 31039
		internal const int ElementTypeIdConst = 12847;

		// Token: 0x04007940 RID: 31040
		private static readonly string[] eleTagNames = new string[] { "srgbClr", "schemeClr" };

		// Token: 0x04007941 RID: 31041
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52 };
	}
}
