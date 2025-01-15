using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002662 RID: 9826
	[ChildElementInfo(typeof(EffectDag))]
	[ChildElementInfo(typeof(GroupFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(EffectList))]
	internal class Background : OpenXmlCompositeElement
	{
		// Token: 0x17005BB4 RID: 23476
		// (get) Token: 0x06012B29 RID: 76585 RVA: 0x002EF0F4 File Offset: 0x002ED2F4
		public override string LocalName
		{
			get
			{
				return "bg";
			}
		}

		// Token: 0x17005BB5 RID: 23477
		// (get) Token: 0x06012B2A RID: 76586 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BB6 RID: 23478
		// (get) Token: 0x06012B2B RID: 76587 RVA: 0x002FE230 File Offset: 0x002FC430
		internal override int ElementTypeId
		{
			get
			{
				return 10643;
			}
		}

		// Token: 0x06012B2C RID: 76588 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012B2D RID: 76589 RVA: 0x00293ECF File Offset: 0x002920CF
		public Background()
		{
		}

		// Token: 0x06012B2E RID: 76590 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Background(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B2F RID: 76591 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Background(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012B30 RID: 76592 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Background(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012B31 RID: 76593 RVA: 0x002FE238 File Offset: 0x002FC438
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "grpFill" == name)
			{
				return new GroupFill();
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

		// Token: 0x06012B32 RID: 76594 RVA: 0x002FE306 File Offset: 0x002FC506
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Background>(deep);
		}

		// Token: 0x04008140 RID: 33088
		private const string tagName = "bg";

		// Token: 0x04008141 RID: 33089
		private const byte tagNsId = 14;

		// Token: 0x04008142 RID: 33090
		internal const int ElementTypeIdConst = 10643;
	}
}
