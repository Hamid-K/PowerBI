using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029F7 RID: 10743
	[ChildElementInfo(typeof(OverrideColorMapping))]
	[ChildElementInfo(typeof(MasterColorMapping))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorMapOverride : OpenXmlCompositeElement
	{
		// Token: 0x17006E9D RID: 28317
		// (get) Token: 0x0601562F RID: 87599 RVA: 0x002FA64B File Offset: 0x002F884B
		public override string LocalName
		{
			get
			{
				return "clrMapOvr";
			}
		}

		// Token: 0x17006E9E RID: 28318
		// (get) Token: 0x06015630 RID: 87600 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E9F RID: 28319
		// (get) Token: 0x06015631 RID: 87601 RVA: 0x0031E6CB File Offset: 0x0031C8CB
		internal override int ElementTypeId
		{
			get
			{
				return 12170;
			}
		}

		// Token: 0x06015632 RID: 87602 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015633 RID: 87603 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorMapOverride()
		{
		}

		// Token: 0x06015634 RID: 87604 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorMapOverride(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015635 RID: 87605 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorMapOverride(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015636 RID: 87606 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorMapOverride(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015637 RID: 87607 RVA: 0x0031E6D2 File Offset: 0x0031C8D2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "masterClrMapping" == name)
			{
				return new MasterColorMapping();
			}
			if (10 == namespaceId && "overrideClrMapping" == name)
			{
				return new OverrideColorMapping();
			}
			return null;
		}

		// Token: 0x17006EA0 RID: 28320
		// (get) Token: 0x06015638 RID: 87608 RVA: 0x0031E705 File Offset: 0x0031C905
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorMapOverride.eleTagNames;
			}
		}

		// Token: 0x17006EA1 RID: 28321
		// (get) Token: 0x06015639 RID: 87609 RVA: 0x0031E70C File Offset: 0x0031C90C
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorMapOverride.eleNamespaceIds;
			}
		}

		// Token: 0x17006EA2 RID: 28322
		// (get) Token: 0x0601563A RID: 87610 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17006EA3 RID: 28323
		// (get) Token: 0x0601563B RID: 87611 RVA: 0x0031E713 File Offset: 0x0031C913
		// (set) Token: 0x0601563C RID: 87612 RVA: 0x0031E71C File Offset: 0x0031C91C
		public MasterColorMapping MasterColorMapping
		{
			get
			{
				return base.GetElement<MasterColorMapping>(0);
			}
			set
			{
				base.SetElement<MasterColorMapping>(0, value);
			}
		}

		// Token: 0x17006EA4 RID: 28324
		// (get) Token: 0x0601563D RID: 87613 RVA: 0x0031E726 File Offset: 0x0031C926
		// (set) Token: 0x0601563E RID: 87614 RVA: 0x0031E72F File Offset: 0x0031C92F
		public OverrideColorMapping OverrideColorMapping
		{
			get
			{
				return base.GetElement<OverrideColorMapping>(1);
			}
			set
			{
				base.SetElement<OverrideColorMapping>(1, value);
			}
		}

		// Token: 0x0601563F RID: 87615 RVA: 0x0031E739 File Offset: 0x0031C939
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorMapOverride>(deep);
		}

		// Token: 0x0400934A RID: 37706
		private const string tagName = "clrMapOvr";

		// Token: 0x0400934B RID: 37707
		private const byte tagNsId = 24;

		// Token: 0x0400934C RID: 37708
		internal const int ElementTypeIdConst = 12170;

		// Token: 0x0400934D RID: 37709
		private static readonly string[] eleTagNames = new string[] { "masterClrMapping", "overrideClrMapping" };

		// Token: 0x0400934E RID: 37710
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
