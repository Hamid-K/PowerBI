using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Excel;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200240C RID: 9228
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Formula))]
	[ChildElementInfo(typeof(ReferenceSequence))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Sparkline : OpenXmlCompositeElement
	{
		// Token: 0x17004ECC RID: 20172
		// (get) Token: 0x06010E25 RID: 69157 RVA: 0x002E853B File Offset: 0x002E673B
		public override string LocalName
		{
			get
			{
				return "sparkline";
			}
		}

		// Token: 0x17004ECD RID: 20173
		// (get) Token: 0x06010E26 RID: 69158 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004ECE RID: 20174
		// (get) Token: 0x06010E27 RID: 69159 RVA: 0x002E8542 File Offset: 0x002E6742
		internal override int ElementTypeId
		{
			get
			{
				return 12946;
			}
		}

		// Token: 0x06010E28 RID: 69160 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010E29 RID: 69161 RVA: 0x00293ECF File Offset: 0x002920CF
		public Sparkline()
		{
		}

		// Token: 0x06010E2A RID: 69162 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Sparkline(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010E2B RID: 69163 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Sparkline(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010E2C RID: 69164 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Sparkline(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010E2D RID: 69165 RVA: 0x002E8549 File Offset: 0x002E6749
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (32 == namespaceId && "f" == name)
			{
				return new Formula();
			}
			if (32 == namespaceId && "sqref" == name)
			{
				return new ReferenceSequence();
			}
			return null;
		}

		// Token: 0x17004ECF RID: 20175
		// (get) Token: 0x06010E2E RID: 69166 RVA: 0x002E857C File Offset: 0x002E677C
		internal override string[] ElementTagNames
		{
			get
			{
				return Sparkline.eleTagNames;
			}
		}

		// Token: 0x17004ED0 RID: 20176
		// (get) Token: 0x06010E2F RID: 69167 RVA: 0x002E8583 File Offset: 0x002E6783
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Sparkline.eleNamespaceIds;
			}
		}

		// Token: 0x17004ED1 RID: 20177
		// (get) Token: 0x06010E30 RID: 69168 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004ED2 RID: 20178
		// (get) Token: 0x06010E31 RID: 69169 RVA: 0x002E7D10 File Offset: 0x002E5F10
		// (set) Token: 0x06010E32 RID: 69170 RVA: 0x002E7D19 File Offset: 0x002E5F19
		public Formula Formula
		{
			get
			{
				return base.GetElement<Formula>(0);
			}
			set
			{
				base.SetElement<Formula>(0, value);
			}
		}

		// Token: 0x17004ED3 RID: 20179
		// (get) Token: 0x06010E33 RID: 69171 RVA: 0x002E858A File Offset: 0x002E678A
		// (set) Token: 0x06010E34 RID: 69172 RVA: 0x002E8593 File Offset: 0x002E6793
		public ReferenceSequence ReferenceSequence
		{
			get
			{
				return base.GetElement<ReferenceSequence>(1);
			}
			set
			{
				base.SetElement<ReferenceSequence>(1, value);
			}
		}

		// Token: 0x06010E35 RID: 69173 RVA: 0x002E859D File Offset: 0x002E679D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Sparkline>(deep);
		}

		// Token: 0x040076B3 RID: 30387
		private const string tagName = "sparkline";

		// Token: 0x040076B4 RID: 30388
		private const byte tagNsId = 53;

		// Token: 0x040076B5 RID: 30389
		internal const int ElementTypeIdConst = 12946;

		// Token: 0x040076B6 RID: 30390
		private static readonly string[] eleTagNames = new string[] { "f", "sqref" };

		// Token: 0x040076B7 RID: 30391
		private static readonly byte[] eleNamespaceIds = new byte[] { 32, 32 };
	}
}
