using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200270D RID: 9997
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EffectContainer))]
	internal class AlphaModulationEffect : OpenXmlCompositeElement
	{
		// Token: 0x17005EC4 RID: 24260
		// (get) Token: 0x060131E6 RID: 78310 RVA: 0x00301737 File Offset: 0x002FF937
		public override string LocalName
		{
			get
			{
				return "alphaMod";
			}
		}

		// Token: 0x17005EC5 RID: 24261
		// (get) Token: 0x060131E7 RID: 78311 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EC6 RID: 24262
		// (get) Token: 0x060131E8 RID: 78312 RVA: 0x00303E08 File Offset: 0x00302008
		internal override int ElementTypeId
		{
			get
			{
				return 10059;
			}
		}

		// Token: 0x060131E9 RID: 78313 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060131EA RID: 78314 RVA: 0x00293ECF File Offset: 0x002920CF
		public AlphaModulationEffect()
		{
		}

		// Token: 0x060131EB RID: 78315 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AlphaModulationEffect(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060131EC RID: 78316 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AlphaModulationEffect(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060131ED RID: 78317 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AlphaModulationEffect(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060131EE RID: 78318 RVA: 0x00303E0F File Offset: 0x0030200F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "cont" == name)
			{
				return new EffectContainer();
			}
			return null;
		}

		// Token: 0x17005EC7 RID: 24263
		// (get) Token: 0x060131EF RID: 78319 RVA: 0x00303E2A File Offset: 0x0030202A
		internal override string[] ElementTagNames
		{
			get
			{
				return AlphaModulationEffect.eleTagNames;
			}
		}

		// Token: 0x17005EC8 RID: 24264
		// (get) Token: 0x060131F0 RID: 78320 RVA: 0x00303E31 File Offset: 0x00302031
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AlphaModulationEffect.eleNamespaceIds;
			}
		}

		// Token: 0x17005EC9 RID: 24265
		// (get) Token: 0x060131F1 RID: 78321 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005ECA RID: 24266
		// (get) Token: 0x060131F2 RID: 78322 RVA: 0x00303E38 File Offset: 0x00302038
		// (set) Token: 0x060131F3 RID: 78323 RVA: 0x00303E41 File Offset: 0x00302041
		public EffectContainer EffectContainer
		{
			get
			{
				return base.GetElement<EffectContainer>(0);
			}
			set
			{
				base.SetElement<EffectContainer>(0, value);
			}
		}

		// Token: 0x060131F4 RID: 78324 RVA: 0x00303E4B File Offset: 0x0030204B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlphaModulationEffect>(deep);
		}

		// Token: 0x040084C2 RID: 33986
		private const string tagName = "alphaMod";

		// Token: 0x040084C3 RID: 33987
		private const byte tagNsId = 10;

		// Token: 0x040084C4 RID: 33988
		internal const int ElementTypeIdConst = 10059;

		// Token: 0x040084C5 RID: 33989
		private static readonly string[] eleTagNames = new string[] { "cont" };

		// Token: 0x040084C6 RID: 33990
		private static readonly byte[] eleNamespaceIds = new byte[] { 10 };
	}
}
