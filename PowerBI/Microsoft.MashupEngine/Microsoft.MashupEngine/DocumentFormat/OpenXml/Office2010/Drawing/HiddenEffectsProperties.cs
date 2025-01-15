using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200234A RID: 9034
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[GeneratedCode("DomGen", "2.0")]
	internal class HiddenEffectsProperties : OpenXmlCompositeElement
	{
		// Token: 0x170049A5 RID: 18853
		// (get) Token: 0x060102DC RID: 66268 RVA: 0x002E0AA3 File Offset: 0x002DECA3
		public override string LocalName
		{
			get
			{
				return "hiddenEffects";
			}
		}

		// Token: 0x170049A6 RID: 18854
		// (get) Token: 0x060102DD RID: 66269 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x170049A7 RID: 18855
		// (get) Token: 0x060102DE RID: 66270 RVA: 0x002E0AAA File Offset: 0x002DECAA
		internal override int ElementTypeId
		{
			get
			{
				return 12719;
			}
		}

		// Token: 0x060102DF RID: 66271 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060102E0 RID: 66272 RVA: 0x00293ECF File Offset: 0x002920CF
		public HiddenEffectsProperties()
		{
		}

		// Token: 0x060102E1 RID: 66273 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HiddenEffectsProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060102E2 RID: 66274 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HiddenEffectsProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060102E3 RID: 66275 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HiddenEffectsProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060102E4 RID: 66276 RVA: 0x002E0AB1 File Offset: 0x002DECB1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
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

		// Token: 0x170049A8 RID: 18856
		// (get) Token: 0x060102E5 RID: 66277 RVA: 0x002E0AE4 File Offset: 0x002DECE4
		internal override string[] ElementTagNames
		{
			get
			{
				return HiddenEffectsProperties.eleTagNames;
			}
		}

		// Token: 0x170049A9 RID: 18857
		// (get) Token: 0x060102E6 RID: 66278 RVA: 0x002E0AEB File Offset: 0x002DECEB
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HiddenEffectsProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170049AA RID: 18858
		// (get) Token: 0x060102E7 RID: 66279 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170049AB RID: 18859
		// (get) Token: 0x060102E8 RID: 66280 RVA: 0x002E0AF2 File Offset: 0x002DECF2
		// (set) Token: 0x060102E9 RID: 66281 RVA: 0x002E0AFB File Offset: 0x002DECFB
		public EffectList EffectList
		{
			get
			{
				return base.GetElement<EffectList>(0);
			}
			set
			{
				base.SetElement<EffectList>(0, value);
			}
		}

		// Token: 0x170049AC RID: 18860
		// (get) Token: 0x060102EA RID: 66282 RVA: 0x002E0B05 File Offset: 0x002DED05
		// (set) Token: 0x060102EB RID: 66283 RVA: 0x002E0B0E File Offset: 0x002DED0E
		public EffectDag EffectDag
		{
			get
			{
				return base.GetElement<EffectDag>(1);
			}
			set
			{
				base.SetElement<EffectDag>(1, value);
			}
		}

		// Token: 0x060102EC RID: 66284 RVA: 0x002E0B18 File Offset: 0x002DED18
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HiddenEffectsProperties>(deep);
		}

		// Token: 0x0400736D RID: 29549
		private const string tagName = "hiddenEffects";

		// Token: 0x0400736E RID: 29550
		private const byte tagNsId = 48;

		// Token: 0x0400736F RID: 29551
		internal const int ElementTypeIdConst = 12719;

		// Token: 0x04007370 RID: 29552
		private static readonly string[] eleTagNames = new string[] { "effectLst", "effectDag" };

		// Token: 0x04007371 RID: 29553
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
