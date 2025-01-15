using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027E7 RID: 10215
	[ChildElementInfo(typeof(ColorScheme))]
	[ChildElementInfo(typeof(ColorMap))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExtraColorScheme : OpenXmlCompositeElement
	{
		// Token: 0x1700646A RID: 25706
		// (get) Token: 0x06013E8A RID: 81546 RVA: 0x0030D108 File Offset: 0x0030B308
		public override string LocalName
		{
			get
			{
				return "extraClrScheme";
			}
		}

		// Token: 0x1700646B RID: 25707
		// (get) Token: 0x06013E8B RID: 81547 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700646C RID: 25708
		// (get) Token: 0x06013E8C RID: 81548 RVA: 0x0030D10F File Offset: 0x0030B30F
		internal override int ElementTypeId
		{
			get
			{
				return 10247;
			}
		}

		// Token: 0x06013E8D RID: 81549 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013E8E RID: 81550 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtraColorScheme()
		{
		}

		// Token: 0x06013E8F RID: 81551 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtraColorScheme(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E90 RID: 81552 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtraColorScheme(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013E91 RID: 81553 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtraColorScheme(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013E92 RID: 81554 RVA: 0x0030D116 File Offset: 0x0030B316
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "clrScheme" == name)
			{
				return new ColorScheme();
			}
			if (10 == namespaceId && "clrMap" == name)
			{
				return new ColorMap();
			}
			return null;
		}

		// Token: 0x1700646D RID: 25709
		// (get) Token: 0x06013E93 RID: 81555 RVA: 0x0030D149 File Offset: 0x0030B349
		internal override string[] ElementTagNames
		{
			get
			{
				return ExtraColorScheme.eleTagNames;
			}
		}

		// Token: 0x1700646E RID: 25710
		// (get) Token: 0x06013E94 RID: 81556 RVA: 0x0030D150 File Offset: 0x0030B350
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ExtraColorScheme.eleNamespaceIds;
			}
		}

		// Token: 0x1700646F RID: 25711
		// (get) Token: 0x06013E95 RID: 81557 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006470 RID: 25712
		// (get) Token: 0x06013E96 RID: 81558 RVA: 0x00307324 File Offset: 0x00305524
		// (set) Token: 0x06013E97 RID: 81559 RVA: 0x0030732D File Offset: 0x0030552D
		public ColorScheme ColorScheme
		{
			get
			{
				return base.GetElement<ColorScheme>(0);
			}
			set
			{
				base.SetElement<ColorScheme>(0, value);
			}
		}

		// Token: 0x17006471 RID: 25713
		// (get) Token: 0x06013E98 RID: 81560 RVA: 0x0030D157 File Offset: 0x0030B357
		// (set) Token: 0x06013E99 RID: 81561 RVA: 0x0030D160 File Offset: 0x0030B360
		public ColorMap ColorMap
		{
			get
			{
				return base.GetElement<ColorMap>(1);
			}
			set
			{
				base.SetElement<ColorMap>(1, value);
			}
		}

		// Token: 0x06013E9A RID: 81562 RVA: 0x0030D16A File Offset: 0x0030B36A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtraColorScheme>(deep);
		}

		// Token: 0x0400883F RID: 34879
		private const string tagName = "extraClrScheme";

		// Token: 0x04008840 RID: 34880
		private const byte tagNsId = 10;

		// Token: 0x04008841 RID: 34881
		internal const int ElementTypeIdConst = 10247;

		// Token: 0x04008842 RID: 34882
		private static readonly string[] eleTagNames = new string[] { "clrScheme", "clrMap" };

		// Token: 0x04008843 RID: 34883
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
