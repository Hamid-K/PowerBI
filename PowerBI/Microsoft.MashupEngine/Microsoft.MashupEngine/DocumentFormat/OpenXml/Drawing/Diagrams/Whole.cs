using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002663 RID: 9827
	[ChildElementInfo(typeof(EffectList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Outline))]
	[ChildElementInfo(typeof(EffectDag))]
	internal class Whole : OpenXmlCompositeElement
	{
		// Token: 0x17005BB7 RID: 23479
		// (get) Token: 0x06012B33 RID: 76595 RVA: 0x002EF1DB File Offset: 0x002ED3DB
		public override string LocalName
		{
			get
			{
				return "whole";
			}
		}

		// Token: 0x17005BB8 RID: 23480
		// (get) Token: 0x06012B34 RID: 76596 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BB9 RID: 23481
		// (get) Token: 0x06012B35 RID: 76597 RVA: 0x002FE30F File Offset: 0x002FC50F
		internal override int ElementTypeId
		{
			get
			{
				return 10644;
			}
		}

		// Token: 0x06012B36 RID: 76598 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012B37 RID: 76599 RVA: 0x00293ECF File Offset: 0x002920CF
		public Whole()
		{
		}

		// Token: 0x06012B38 RID: 76600 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Whole(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B39 RID: 76601 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Whole(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B3A RID: 76602 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Whole(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012B3B RID: 76603 RVA: 0x002FE318 File Offset: 0x002FC518
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

		// Token: 0x17005BBA RID: 23482
		// (get) Token: 0x06012B3C RID: 76604 RVA: 0x002FE36E File Offset: 0x002FC56E
		internal override string[] ElementTagNames
		{
			get
			{
				return Whole.eleTagNames;
			}
		}

		// Token: 0x17005BBB RID: 23483
		// (get) Token: 0x06012B3D RID: 76605 RVA: 0x002FE375 File Offset: 0x002FC575
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Whole.eleNamespaceIds;
			}
		}

		// Token: 0x17005BBC RID: 23484
		// (get) Token: 0x06012B3E RID: 76606 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005BBD RID: 23485
		// (get) Token: 0x06012B3F RID: 76607 RVA: 0x002EF250 File Offset: 0x002ED450
		// (set) Token: 0x06012B40 RID: 76608 RVA: 0x002EF259 File Offset: 0x002ED459
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

		// Token: 0x06012B41 RID: 76609 RVA: 0x002FE37C File Offset: 0x002FC57C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Whole>(deep);
		}

		// Token: 0x04008143 RID: 33091
		private const string tagName = "whole";

		// Token: 0x04008144 RID: 33092
		private const byte tagNsId = 14;

		// Token: 0x04008145 RID: 33093
		internal const int ElementTypeIdConst = 10644;

		// Token: 0x04008146 RID: 33094
		private static readonly string[] eleTagNames = new string[] { "ln", "effectLst", "effectDag" };

		// Token: 0x04008147 RID: 33095
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
