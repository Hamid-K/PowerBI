using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002733 RID: 10035
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(EffectList))]
	internal class EffectPropertiesType : OpenXmlCompositeElement
	{
		// Token: 0x17006019 RID: 24601
		// (get) Token: 0x060134B8 RID: 79032 RVA: 0x00303BC8 File Offset: 0x00301DC8
		public override string LocalName
		{
			get
			{
				return "effect";
			}
		}

		// Token: 0x1700601A RID: 24602
		// (get) Token: 0x060134B9 RID: 79033 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700601B RID: 24603
		// (get) Token: 0x060134BA RID: 79034 RVA: 0x00305D46 File Offset: 0x00303F46
		internal override int ElementTypeId
		{
			get
			{
				return 10095;
			}
		}

		// Token: 0x060134BB RID: 79035 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060134BC RID: 79036 RVA: 0x00293ECF File Offset: 0x002920CF
		public EffectPropertiesType()
		{
		}

		// Token: 0x060134BD RID: 79037 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EffectPropertiesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134BE RID: 79038 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EffectPropertiesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134BF RID: 79039 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EffectPropertiesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060134C0 RID: 79040 RVA: 0x002E0AB1 File Offset: 0x002DECB1
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

		// Token: 0x1700601C RID: 24604
		// (get) Token: 0x060134C1 RID: 79041 RVA: 0x00305D4D File Offset: 0x00303F4D
		internal override string[] ElementTagNames
		{
			get
			{
				return EffectPropertiesType.eleTagNames;
			}
		}

		// Token: 0x1700601D RID: 24605
		// (get) Token: 0x060134C2 RID: 79042 RVA: 0x00305D54 File Offset: 0x00303F54
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return EffectPropertiesType.eleNamespaceIds;
			}
		}

		// Token: 0x1700601E RID: 24606
		// (get) Token: 0x060134C3 RID: 79043 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700601F RID: 24607
		// (get) Token: 0x060134C4 RID: 79044 RVA: 0x002E0AF2 File Offset: 0x002DECF2
		// (set) Token: 0x060134C5 RID: 79045 RVA: 0x002E0AFB File Offset: 0x002DECFB
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

		// Token: 0x17006020 RID: 24608
		// (get) Token: 0x060134C6 RID: 79046 RVA: 0x002E0B05 File Offset: 0x002DED05
		// (set) Token: 0x060134C7 RID: 79047 RVA: 0x002E0B0E File Offset: 0x002DED0E
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

		// Token: 0x060134C8 RID: 79048 RVA: 0x00305D5B File Offset: 0x00303F5B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EffectPropertiesType>(deep);
		}

		// Token: 0x04008581 RID: 34177
		private const string tagName = "effect";

		// Token: 0x04008582 RID: 34178
		private const byte tagNsId = 10;

		// Token: 0x04008583 RID: 34179
		internal const int ElementTypeIdConst = 10095;

		// Token: 0x04008584 RID: 34180
		private static readonly string[] eleTagNames = new string[] { "effectLst", "effectDag" };

		// Token: 0x04008585 RID: 34181
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
