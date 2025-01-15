using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A0F RID: 10767
	[ChildElementInfo(typeof(ShapeTarget))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BookmarkTarget), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SlideTarget))]
	[ChildElementInfo(typeof(SoundTarget))]
	[ChildElementInfo(typeof(InkTarget))]
	internal class TargetElement : OpenXmlCompositeElement
	{
		// Token: 0x17006FAE RID: 28590
		// (get) Token: 0x0601589F RID: 88223 RVA: 0x0032053F File Offset: 0x0031E73F
		public override string LocalName
		{
			get
			{
				return "tgtEl";
			}
		}

		// Token: 0x17006FAF RID: 28591
		// (get) Token: 0x060158A0 RID: 88224 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006FB0 RID: 28592
		// (get) Token: 0x060158A1 RID: 88225 RVA: 0x00320546 File Offset: 0x0031E746
		internal override int ElementTypeId
		{
			get
			{
				return 12195;
			}
		}

		// Token: 0x060158A2 RID: 88226 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060158A3 RID: 88227 RVA: 0x00293ECF File Offset: 0x002920CF
		public TargetElement()
		{
		}

		// Token: 0x060158A4 RID: 88228 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TargetElement(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060158A5 RID: 88229 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TargetElement(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060158A6 RID: 88230 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TargetElement(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060158A7 RID: 88231 RVA: 0x00320550 File Offset: 0x0031E750
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "sldTgt" == name)
			{
				return new SlideTarget();
			}
			if (24 == namespaceId && "sndTgt" == name)
			{
				return new SoundTarget();
			}
			if (24 == namespaceId && "spTgt" == name)
			{
				return new ShapeTarget();
			}
			if (24 == namespaceId && "inkTgt" == name)
			{
				return new InkTarget();
			}
			if (49 == namespaceId && "bmkTgt" == name)
			{
				return new BookmarkTarget();
			}
			return null;
		}

		// Token: 0x17006FB1 RID: 28593
		// (get) Token: 0x060158A8 RID: 88232 RVA: 0x003205D6 File Offset: 0x0031E7D6
		internal override string[] ElementTagNames
		{
			get
			{
				return TargetElement.eleTagNames;
			}
		}

		// Token: 0x17006FB2 RID: 28594
		// (get) Token: 0x060158A9 RID: 88233 RVA: 0x003205DD File Offset: 0x0031E7DD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TargetElement.eleNamespaceIds;
			}
		}

		// Token: 0x17006FB3 RID: 28595
		// (get) Token: 0x060158AA RID: 88234 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17006FB4 RID: 28596
		// (get) Token: 0x060158AB RID: 88235 RVA: 0x003205E4 File Offset: 0x0031E7E4
		// (set) Token: 0x060158AC RID: 88236 RVA: 0x003205ED File Offset: 0x0031E7ED
		public SlideTarget SlideTarget
		{
			get
			{
				return base.GetElement<SlideTarget>(0);
			}
			set
			{
				base.SetElement<SlideTarget>(0, value);
			}
		}

		// Token: 0x17006FB5 RID: 28597
		// (get) Token: 0x060158AD RID: 88237 RVA: 0x003205F7 File Offset: 0x0031E7F7
		// (set) Token: 0x060158AE RID: 88238 RVA: 0x00320600 File Offset: 0x0031E800
		public SoundTarget SoundTarget
		{
			get
			{
				return base.GetElement<SoundTarget>(1);
			}
			set
			{
				base.SetElement<SoundTarget>(1, value);
			}
		}

		// Token: 0x17006FB6 RID: 28598
		// (get) Token: 0x060158AF RID: 88239 RVA: 0x0032060A File Offset: 0x0031E80A
		// (set) Token: 0x060158B0 RID: 88240 RVA: 0x00320613 File Offset: 0x0031E813
		public ShapeTarget ShapeTarget
		{
			get
			{
				return base.GetElement<ShapeTarget>(2);
			}
			set
			{
				base.SetElement<ShapeTarget>(2, value);
			}
		}

		// Token: 0x17006FB7 RID: 28599
		// (get) Token: 0x060158B1 RID: 88241 RVA: 0x0032061D File Offset: 0x0031E81D
		// (set) Token: 0x060158B2 RID: 88242 RVA: 0x00320626 File Offset: 0x0031E826
		public InkTarget InkTarget
		{
			get
			{
				return base.GetElement<InkTarget>(3);
			}
			set
			{
				base.SetElement<InkTarget>(3, value);
			}
		}

		// Token: 0x17006FB8 RID: 28600
		// (get) Token: 0x060158B3 RID: 88243 RVA: 0x00320630 File Offset: 0x0031E830
		// (set) Token: 0x060158B4 RID: 88244 RVA: 0x00320639 File Offset: 0x0031E839
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public BookmarkTarget BookmarkTarget
		{
			get
			{
				return base.GetElement<BookmarkTarget>(4);
			}
			set
			{
				base.SetElement<BookmarkTarget>(4, value);
			}
		}

		// Token: 0x060158B5 RID: 88245 RVA: 0x00320643 File Offset: 0x0031E843
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TargetElement>(deep);
		}

		// Token: 0x040093C9 RID: 37833
		private const string tagName = "tgtEl";

		// Token: 0x040093CA RID: 37834
		private const byte tagNsId = 24;

		// Token: 0x040093CB RID: 37835
		internal const int ElementTypeIdConst = 12195;

		// Token: 0x040093CC RID: 37836
		private static readonly string[] eleTagNames = new string[] { "sldTgt", "sndTgt", "spTgt", "inkTgt", "bmkTgt" };

		// Token: 0x040093CD RID: 37837
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24, 24, 24, 49 };
	}
}
