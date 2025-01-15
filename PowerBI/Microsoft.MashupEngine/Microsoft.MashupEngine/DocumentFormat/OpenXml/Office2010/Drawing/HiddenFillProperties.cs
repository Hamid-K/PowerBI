using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002348 RID: 9032
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(PatternFill))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(GroupFill))]
	internal class HiddenFillProperties : OpenXmlCompositeElement
	{
		// Token: 0x17004990 RID: 18832
		// (get) Token: 0x060102AC RID: 66220 RVA: 0x002E06CF File Offset: 0x002DE8CF
		public override string LocalName
		{
			get
			{
				return "hiddenFill";
			}
		}

		// Token: 0x17004991 RID: 18833
		// (get) Token: 0x060102AD RID: 66221 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004992 RID: 18834
		// (get) Token: 0x060102AE RID: 66222 RVA: 0x002E06D6 File Offset: 0x002DE8D6
		internal override int ElementTypeId
		{
			get
			{
				return 12717;
			}
		}

		// Token: 0x060102AF RID: 66223 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060102B0 RID: 66224 RVA: 0x00293ECF File Offset: 0x002920CF
		public HiddenFillProperties()
		{
		}

		// Token: 0x060102B1 RID: 66225 RVA: 0x00293ED7 File Offset: 0x002920D7
		public HiddenFillProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060102B2 RID: 66226 RVA: 0x00293EE0 File Offset: 0x002920E0
		public HiddenFillProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060102B3 RID: 66227 RVA: 0x00293EE9 File Offset: 0x002920E9
		public HiddenFillProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060102B4 RID: 66228 RVA: 0x002E06E0 File Offset: 0x002DE8E0
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
			return null;
		}

		// Token: 0x17004993 RID: 18835
		// (get) Token: 0x060102B5 RID: 66229 RVA: 0x002E077E File Offset: 0x002DE97E
		internal override string[] ElementTagNames
		{
			get
			{
				return HiddenFillProperties.eleTagNames;
			}
		}

		// Token: 0x17004994 RID: 18836
		// (get) Token: 0x060102B6 RID: 66230 RVA: 0x002E0785 File Offset: 0x002DE985
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HiddenFillProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004995 RID: 18837
		// (get) Token: 0x060102B7 RID: 66231 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17004996 RID: 18838
		// (get) Token: 0x060102B8 RID: 66232 RVA: 0x002E078C File Offset: 0x002DE98C
		// (set) Token: 0x060102B9 RID: 66233 RVA: 0x002E0795 File Offset: 0x002DE995
		public NoFill NoFill
		{
			get
			{
				return base.GetElement<NoFill>(0);
			}
			set
			{
				base.SetElement<NoFill>(0, value);
			}
		}

		// Token: 0x17004997 RID: 18839
		// (get) Token: 0x060102BA RID: 66234 RVA: 0x002E079F File Offset: 0x002DE99F
		// (set) Token: 0x060102BB RID: 66235 RVA: 0x002E07A8 File Offset: 0x002DE9A8
		public SolidFill SolidFill
		{
			get
			{
				return base.GetElement<SolidFill>(1);
			}
			set
			{
				base.SetElement<SolidFill>(1, value);
			}
		}

		// Token: 0x17004998 RID: 18840
		// (get) Token: 0x060102BC RID: 66236 RVA: 0x002E07B2 File Offset: 0x002DE9B2
		// (set) Token: 0x060102BD RID: 66237 RVA: 0x002E07BB File Offset: 0x002DE9BB
		public GradientFill GradientFill
		{
			get
			{
				return base.GetElement<GradientFill>(2);
			}
			set
			{
				base.SetElement<GradientFill>(2, value);
			}
		}

		// Token: 0x17004999 RID: 18841
		// (get) Token: 0x060102BE RID: 66238 RVA: 0x002E07C5 File Offset: 0x002DE9C5
		// (set) Token: 0x060102BF RID: 66239 RVA: 0x002E07CE File Offset: 0x002DE9CE
		public BlipFill BlipFill
		{
			get
			{
				return base.GetElement<BlipFill>(3);
			}
			set
			{
				base.SetElement<BlipFill>(3, value);
			}
		}

		// Token: 0x1700499A RID: 18842
		// (get) Token: 0x060102C0 RID: 66240 RVA: 0x002E07D8 File Offset: 0x002DE9D8
		// (set) Token: 0x060102C1 RID: 66241 RVA: 0x002E07E1 File Offset: 0x002DE9E1
		public PatternFill PatternFill
		{
			get
			{
				return base.GetElement<PatternFill>(4);
			}
			set
			{
				base.SetElement<PatternFill>(4, value);
			}
		}

		// Token: 0x1700499B RID: 18843
		// (get) Token: 0x060102C2 RID: 66242 RVA: 0x002E07EB File Offset: 0x002DE9EB
		// (set) Token: 0x060102C3 RID: 66243 RVA: 0x002E07F4 File Offset: 0x002DE9F4
		public GroupFill GroupFill
		{
			get
			{
				return base.GetElement<GroupFill>(5);
			}
			set
			{
				base.SetElement<GroupFill>(5, value);
			}
		}

		// Token: 0x060102C4 RID: 66244 RVA: 0x002E07FE File Offset: 0x002DE9FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HiddenFillProperties>(deep);
		}

		// Token: 0x04007363 RID: 29539
		private const string tagName = "hiddenFill";

		// Token: 0x04007364 RID: 29540
		private const byte tagNsId = 48;

		// Token: 0x04007365 RID: 29541
		internal const int ElementTypeIdConst = 12717;

		// Token: 0x04007366 RID: 29542
		private static readonly string[] eleTagNames = new string[] { "noFill", "solidFill", "gradFill", "blipFill", "pattFill", "grpFill" };

		// Token: 0x04007367 RID: 29543
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
