using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C89 RID: 11401
	[ChildElementInfo(typeof(Color))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConditionalFormatValueObject))]
	internal class ColorScale : OpenXmlCompositeElement
	{
		// Token: 0x1700838F RID: 33679
		// (get) Token: 0x060184C9 RID: 99529 RVA: 0x002E8C0F File Offset: 0x002E6E0F
		public override string LocalName
		{
			get
			{
				return "colorScale";
			}
		}

		// Token: 0x17008390 RID: 33680
		// (get) Token: 0x060184CA RID: 99530 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008391 RID: 33681
		// (get) Token: 0x060184CB RID: 99531 RVA: 0x0034044F File Offset: 0x0033E64F
		internal override int ElementTypeId
		{
			get
			{
				return 11380;
			}
		}

		// Token: 0x060184CC RID: 99532 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060184CD RID: 99533 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorScale()
		{
		}

		// Token: 0x060184CE RID: 99534 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorScale(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060184CF RID: 99535 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorScale(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060184D0 RID: 99536 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorScale(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060184D1 RID: 99537 RVA: 0x00340456 File Offset: 0x0033E656
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cfvo" == name)
			{
				return new ConditionalFormatValueObject();
			}
			if (22 == namespaceId && "color" == name)
			{
				return new Color();
			}
			return null;
		}

		// Token: 0x060184D2 RID: 99538 RVA: 0x00340489 File Offset: 0x0033E689
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorScale>(deep);
		}

		// Token: 0x04009FB2 RID: 40882
		private const string tagName = "colorScale";

		// Token: 0x04009FB3 RID: 40883
		private const byte tagNsId = 22;

		// Token: 0x04009FB4 RID: 40884
		internal const int ElementTypeIdConst = 11380;
	}
}
