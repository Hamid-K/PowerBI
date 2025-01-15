using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BAC RID: 11180
	[ChildElementInfo(typeof(Extend))]
	[ChildElementInfo(typeof(Color))]
	[ChildElementInfo(typeof(VerticalTextAlignment))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Italic))]
	[ChildElementInfo(typeof(Strike))]
	[ChildElementInfo(typeof(Condense))]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(Shadow))]
	[ChildElementInfo(typeof(Underline))]
	[ChildElementInfo(typeof(Bold))]
	[ChildElementInfo(typeof(FontSize))]
	[ChildElementInfo(typeof(RunFont))]
	[ChildElementInfo(typeof(FontFamily))]
	[ChildElementInfo(typeof(RunPropertyCharSet))]
	[ChildElementInfo(typeof(FontScheme))]
	internal class RunProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007B73 RID: 31603
		// (get) Token: 0x060172E6 RID: 94950 RVA: 0x0030F747 File Offset: 0x0030D947
		public override string LocalName
		{
			get
			{
				return "rPr";
			}
		}

		// Token: 0x17007B74 RID: 31604
		// (get) Token: 0x060172E7 RID: 94951 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B75 RID: 31605
		// (get) Token: 0x060172E8 RID: 94952 RVA: 0x00333817 File Offset: 0x00331A17
		internal override int ElementTypeId
		{
			get
			{
				return 11151;
			}
		}

		// Token: 0x060172E9 RID: 94953 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060172EA RID: 94954 RVA: 0x00293ECF File Offset: 0x002920CF
		public RunProperties()
		{
		}

		// Token: 0x060172EB RID: 94955 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RunProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060172EC RID: 94956 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RunProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060172ED RID: 94957 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RunProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060172EE RID: 94958 RVA: 0x00333820 File Offset: 0x00331A20
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "b" == name)
			{
				return new Bold();
			}
			if (22 == namespaceId && "i" == name)
			{
				return new Italic();
			}
			if (22 == namespaceId && "strike" == name)
			{
				return new Strike();
			}
			if (22 == namespaceId && "condense" == name)
			{
				return new Condense();
			}
			if (22 == namespaceId && "extend" == name)
			{
				return new Extend();
			}
			if (22 == namespaceId && "outline" == name)
			{
				return new Outline();
			}
			if (22 == namespaceId && "shadow" == name)
			{
				return new Shadow();
			}
			if (22 == namespaceId && "u" == name)
			{
				return new Underline();
			}
			if (22 == namespaceId && "vertAlign" == name)
			{
				return new VerticalTextAlignment();
			}
			if (22 == namespaceId && "sz" == name)
			{
				return new FontSize();
			}
			if (22 == namespaceId && "color" == name)
			{
				return new Color();
			}
			if (22 == namespaceId && "rFont" == name)
			{
				return new RunFont();
			}
			if (22 == namespaceId && "family" == name)
			{
				return new FontFamily();
			}
			if (22 == namespaceId && "charset" == name)
			{
				return new RunPropertyCharSet();
			}
			if (22 == namespaceId && "scheme" == name)
			{
				return new FontScheme();
			}
			return null;
		}

		// Token: 0x060172EF RID: 94959 RVA: 0x00333996 File Offset: 0x00331B96
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunProperties>(deep);
		}

		// Token: 0x04009B7B RID: 39803
		private const string tagName = "rPr";

		// Token: 0x04009B7C RID: 39804
		private const byte tagNsId = 22;

		// Token: 0x04009B7D RID: 39805
		internal const int ElementTypeIdConst = 11151;
	}
}
