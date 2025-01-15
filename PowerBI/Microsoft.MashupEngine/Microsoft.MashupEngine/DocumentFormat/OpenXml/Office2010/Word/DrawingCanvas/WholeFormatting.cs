using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingCanvas
{
	// Token: 0x020024E5 RID: 9445
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class WholeFormatting : OpenXmlCompositeElement
	{
		// Token: 0x1700531F RID: 21279
		// (get) Token: 0x060117F9 RID: 71673 RVA: 0x002EF1DB File Offset: 0x002ED3DB
		public override string LocalName
		{
			get
			{
				return "whole";
			}
		}

		// Token: 0x17005320 RID: 21280
		// (get) Token: 0x060117FA RID: 71674 RVA: 0x002EEF8A File Offset: 0x002ED18A
		internal override byte NamespaceId
		{
			get
			{
				return 59;
			}
		}

		// Token: 0x17005321 RID: 21281
		// (get) Token: 0x060117FB RID: 71675 RVA: 0x002EF1E2 File Offset: 0x002ED3E2
		internal override int ElementTypeId
		{
			get
			{
				return 13120;
			}
		}

		// Token: 0x060117FC RID: 71676 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060117FD RID: 71677 RVA: 0x00293ECF File Offset: 0x002920CF
		public WholeFormatting()
		{
		}

		// Token: 0x060117FE RID: 71678 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WholeFormatting(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117FF RID: 71679 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WholeFormatting(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011800 RID: 71680 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WholeFormatting(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011801 RID: 71681 RVA: 0x002EF1EC File Offset: 0x002ED3EC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ln" == name)
			{
				return new Outline();
			}
			if (10 == namespaceId && "effectLst" == name)
			{
				return new EffectList();
			}
			if (10 == namespaceId && "effectDag" == name)
			{
				return new EffectDag();
			}
			return null;
		}

		// Token: 0x17005322 RID: 21282
		// (get) Token: 0x06011802 RID: 71682 RVA: 0x002EF242 File Offset: 0x002ED442
		internal override string[] ElementTagNames
		{
			get
			{
				return WholeFormatting.eleTagNames;
			}
		}

		// Token: 0x17005323 RID: 21283
		// (get) Token: 0x06011803 RID: 71683 RVA: 0x002EF249 File Offset: 0x002ED449
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WholeFormatting.eleNamespaceIds;
			}
		}

		// Token: 0x17005324 RID: 21284
		// (get) Token: 0x06011804 RID: 71684 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005325 RID: 21285
		// (get) Token: 0x06011805 RID: 71685 RVA: 0x002EF250 File Offset: 0x002ED450
		// (set) Token: 0x06011806 RID: 71686 RVA: 0x002EF259 File Offset: 0x002ED459
		public Outline Outline
		{
			get
			{
				return base.GetElement<Outline>(0);
			}
			set
			{
				base.SetElement<Outline>(0, value);
			}
		}

		// Token: 0x06011807 RID: 71687 RVA: 0x002EF263 File Offset: 0x002ED463
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WholeFormatting>(deep);
		}

		// Token: 0x04007AFA RID: 31482
		private const string tagName = "whole";

		// Token: 0x04007AFB RID: 31483
		private const byte tagNsId = 59;

		// Token: 0x04007AFC RID: 31484
		internal const int ElementTypeIdConst = 13120;

		// Token: 0x04007AFD RID: 31485
		private static readonly string[] eleTagNames = new string[] { "ln", "effectLst", "effectDag" };

		// Token: 0x04007AFE RID: 31486
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
