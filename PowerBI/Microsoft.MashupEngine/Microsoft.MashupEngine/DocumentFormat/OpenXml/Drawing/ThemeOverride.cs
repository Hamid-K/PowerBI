using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002766 RID: 10086
	[ChildElementInfo(typeof(FontScheme))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ColorScheme))]
	[ChildElementInfo(typeof(FormatScheme))]
	internal class ThemeOverride : OpenXmlPartRootElement
	{
		// Token: 0x17006118 RID: 24856
		// (get) Token: 0x06013711 RID: 79633 RVA: 0x003072A2 File Offset: 0x003054A2
		public override string LocalName
		{
			get
			{
				return "themeOverride";
			}
		}

		// Token: 0x17006119 RID: 24857
		// (get) Token: 0x06013712 RID: 79634 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700611A RID: 24858
		// (get) Token: 0x06013713 RID: 79635 RVA: 0x003072A9 File Offset: 0x003054A9
		internal override int ElementTypeId
		{
			get
			{
				return 10123;
			}
		}

		// Token: 0x06013714 RID: 79636 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013715 RID: 79637 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal ThemeOverride(ThemeOverridePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06013716 RID: 79638 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(ThemeOverridePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x1700611B RID: 24859
		// (get) Token: 0x06013717 RID: 79639 RVA: 0x003072B0 File Offset: 0x003054B0
		// (set) Token: 0x06013718 RID: 79640 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public ThemeOverridePart ThemeOverridePart
		{
			get
			{
				return base.OpenXmlPart as ThemeOverridePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06013719 RID: 79641 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public ThemeOverride(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601371A RID: 79642 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public ThemeOverride(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601371B RID: 79643 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public ThemeOverride(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601371C RID: 79644 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public ThemeOverride()
		{
		}

		// Token: 0x0601371D RID: 79645 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(ThemeOverridePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601371E RID: 79646 RVA: 0x003072C0 File Offset: 0x003054C0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "clrScheme" == name)
			{
				return new ColorScheme();
			}
			if (10 == namespaceId && "fontScheme" == name)
			{
				return new FontScheme();
			}
			if (10 == namespaceId && "fmtScheme" == name)
			{
				return new FormatScheme();
			}
			return null;
		}

		// Token: 0x1700611C RID: 24860
		// (get) Token: 0x0601371F RID: 79647 RVA: 0x00307316 File Offset: 0x00305516
		internal override string[] ElementTagNames
		{
			get
			{
				return ThemeOverride.eleTagNames;
			}
		}

		// Token: 0x1700611D RID: 24861
		// (get) Token: 0x06013720 RID: 79648 RVA: 0x0030731D File Offset: 0x0030551D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ThemeOverride.eleNamespaceIds;
			}
		}

		// Token: 0x1700611E RID: 24862
		// (get) Token: 0x06013721 RID: 79649 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700611F RID: 24863
		// (get) Token: 0x06013722 RID: 79650 RVA: 0x00307324 File Offset: 0x00305524
		// (set) Token: 0x06013723 RID: 79651 RVA: 0x0030732D File Offset: 0x0030552D
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

		// Token: 0x17006120 RID: 24864
		// (get) Token: 0x06013724 RID: 79652 RVA: 0x00307337 File Offset: 0x00305537
		// (set) Token: 0x06013725 RID: 79653 RVA: 0x00307340 File Offset: 0x00305540
		public FontScheme FontScheme
		{
			get
			{
				return base.GetElement<FontScheme>(1);
			}
			set
			{
				base.SetElement<FontScheme>(1, value);
			}
		}

		// Token: 0x17006121 RID: 24865
		// (get) Token: 0x06013726 RID: 79654 RVA: 0x0030734A File Offset: 0x0030554A
		// (set) Token: 0x06013727 RID: 79655 RVA: 0x00307353 File Offset: 0x00305553
		public FormatScheme FormatScheme
		{
			get
			{
				return base.GetElement<FormatScheme>(2);
			}
			set
			{
				base.SetElement<FormatScheme>(2, value);
			}
		}

		// Token: 0x06013728 RID: 79656 RVA: 0x0030735D File Offset: 0x0030555D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ThemeOverride>(deep);
		}

		// Token: 0x0400863A RID: 34362
		private const string tagName = "themeOverride";

		// Token: 0x0400863B RID: 34363
		private const byte tagNsId = 10;

		// Token: 0x0400863C RID: 34364
		internal const int ElementTypeIdConst = 10123;

		// Token: 0x0400863D RID: 34365
		private static readonly string[] eleTagNames = new string[] { "clrScheme", "fontScheme", "fmtScheme" };

		// Token: 0x0400863E RID: 34366
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
