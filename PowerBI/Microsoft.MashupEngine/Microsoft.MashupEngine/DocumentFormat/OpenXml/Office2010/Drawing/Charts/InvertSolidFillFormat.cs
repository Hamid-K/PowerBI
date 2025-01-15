using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x02002319 RID: 8985
	[ChildElementInfo(typeof(ShapeProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class InvertSolidFillFormat : OpenXmlCompositeElement
	{
		// Token: 0x17004839 RID: 18489
		// (get) Token: 0x0600FFBE RID: 65470 RVA: 0x002DE2E5 File Offset: 0x002DC4E5
		public override string LocalName
		{
			get
			{
				return "invertSolidFillFmt";
			}
		}

		// Token: 0x1700483A RID: 18490
		// (get) Token: 0x0600FFBF RID: 65471 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x1700483B RID: 18491
		// (get) Token: 0x0600FFC0 RID: 65472 RVA: 0x002DE2EC File Offset: 0x002DC4EC
		internal override int ElementTypeId
		{
			get
			{
				return 12693;
			}
		}

		// Token: 0x0600FFC1 RID: 65473 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FFC2 RID: 65474 RVA: 0x00293ECF File Offset: 0x002920CF
		public InvertSolidFillFormat()
		{
		}

		// Token: 0x0600FFC3 RID: 65475 RVA: 0x00293ED7 File Offset: 0x002920D7
		public InvertSolidFillFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FFC4 RID: 65476 RVA: 0x00293EE0 File Offset: 0x002920E0
		public InvertSolidFillFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FFC5 RID: 65477 RVA: 0x00293EE9 File Offset: 0x002920E9
		public InvertSolidFillFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FFC6 RID: 65478 RVA: 0x002DE2F3 File Offset: 0x002DC4F3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (46 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			return null;
		}

		// Token: 0x1700483C RID: 18492
		// (get) Token: 0x0600FFC7 RID: 65479 RVA: 0x002DE30E File Offset: 0x002DC50E
		internal override string[] ElementTagNames
		{
			get
			{
				return InvertSolidFillFormat.eleTagNames;
			}
		}

		// Token: 0x1700483D RID: 18493
		// (get) Token: 0x0600FFC8 RID: 65480 RVA: 0x002DE315 File Offset: 0x002DC515
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return InvertSolidFillFormat.eleNamespaceIds;
			}
		}

		// Token: 0x1700483E RID: 18494
		// (get) Token: 0x0600FFC9 RID: 65481 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700483F RID: 18495
		// (get) Token: 0x0600FFCA RID: 65482 RVA: 0x002DE31C File Offset: 0x002DC51C
		// (set) Token: 0x0600FFCB RID: 65483 RVA: 0x002DE325 File Offset: 0x002DC525
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(0);
			}
			set
			{
				base.SetElement<ShapeProperties>(0, value);
			}
		}

		// Token: 0x0600FFCC RID: 65484 RVA: 0x002DE32F File Offset: 0x002DC52F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InvertSolidFillFormat>(deep);
		}

		// Token: 0x04007286 RID: 29318
		private const string tagName = "invertSolidFillFmt";

		// Token: 0x04007287 RID: 29319
		private const byte tagNsId = 46;

		// Token: 0x04007288 RID: 29320
		internal const int ElementTypeIdConst = 12693;

		// Token: 0x04007289 RID: 29321
		private static readonly string[] eleTagNames = new string[] { "spPr" };

		// Token: 0x0400728A RID: 29322
		private static readonly byte[] eleNamespaceIds = new byte[] { 46 };
	}
}
