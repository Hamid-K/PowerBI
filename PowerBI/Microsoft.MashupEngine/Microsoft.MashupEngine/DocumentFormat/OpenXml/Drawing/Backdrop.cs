using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027B4 RID: 10164
	[ChildElementInfo(typeof(Normal))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Anchor))]
	[ChildElementInfo(typeof(UpVector))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Backdrop : OpenXmlCompositeElement
	{
		// Token: 0x17006329 RID: 25385
		// (get) Token: 0x06013BBA RID: 80826 RVA: 0x0030B2D2 File Offset: 0x003094D2
		public override string LocalName
		{
			get
			{
				return "backdrop";
			}
		}

		// Token: 0x1700632A RID: 25386
		// (get) Token: 0x06013BBB RID: 80827 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700632B RID: 25387
		// (get) Token: 0x06013BBC RID: 80828 RVA: 0x0030B2D9 File Offset: 0x003094D9
		internal override int ElementTypeId
		{
			get
			{
				return 10197;
			}
		}

		// Token: 0x06013BBD RID: 80829 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013BBE RID: 80830 RVA: 0x00293ECF File Offset: 0x002920CF
		public Backdrop()
		{
		}

		// Token: 0x06013BBF RID: 80831 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Backdrop(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013BC0 RID: 80832 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Backdrop(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013BC1 RID: 80833 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Backdrop(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013BC2 RID: 80834 RVA: 0x0030B2E0 File Offset: 0x003094E0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "anchor" == name)
			{
				return new Anchor();
			}
			if (10 == namespaceId && "norm" == name)
			{
				return new Normal();
			}
			if (10 == namespaceId && "up" == name)
			{
				return new UpVector();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700632C RID: 25388
		// (get) Token: 0x06013BC3 RID: 80835 RVA: 0x0030B34E File Offset: 0x0030954E
		internal override string[] ElementTagNames
		{
			get
			{
				return Backdrop.eleTagNames;
			}
		}

		// Token: 0x1700632D RID: 25389
		// (get) Token: 0x06013BC4 RID: 80836 RVA: 0x0030B355 File Offset: 0x00309555
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Backdrop.eleNamespaceIds;
			}
		}

		// Token: 0x1700632E RID: 25390
		// (get) Token: 0x06013BC5 RID: 80837 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700632F RID: 25391
		// (get) Token: 0x06013BC6 RID: 80838 RVA: 0x0030B35C File Offset: 0x0030955C
		// (set) Token: 0x06013BC7 RID: 80839 RVA: 0x0030B365 File Offset: 0x00309565
		public Anchor Anchor
		{
			get
			{
				return base.GetElement<Anchor>(0);
			}
			set
			{
				base.SetElement<Anchor>(0, value);
			}
		}

		// Token: 0x17006330 RID: 25392
		// (get) Token: 0x06013BC8 RID: 80840 RVA: 0x0030B36F File Offset: 0x0030956F
		// (set) Token: 0x06013BC9 RID: 80841 RVA: 0x0030B378 File Offset: 0x00309578
		public Normal Normal
		{
			get
			{
				return base.GetElement<Normal>(1);
			}
			set
			{
				base.SetElement<Normal>(1, value);
			}
		}

		// Token: 0x17006331 RID: 25393
		// (get) Token: 0x06013BCA RID: 80842 RVA: 0x0030B382 File Offset: 0x00309582
		// (set) Token: 0x06013BCB RID: 80843 RVA: 0x0030B38B File Offset: 0x0030958B
		public UpVector UpVector
		{
			get
			{
				return base.GetElement<UpVector>(2);
			}
			set
			{
				base.SetElement<UpVector>(2, value);
			}
		}

		// Token: 0x17006332 RID: 25394
		// (get) Token: 0x06013BCC RID: 80844 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06013BCD RID: 80845 RVA: 0x002E0C32 File Offset: 0x002DEE32
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x06013BCE RID: 80846 RVA: 0x0030B395 File Offset: 0x00309595
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Backdrop>(deep);
		}

		// Token: 0x04008782 RID: 34690
		private const string tagName = "backdrop";

		// Token: 0x04008783 RID: 34691
		private const byte tagNsId = 10;

		// Token: 0x04008784 RID: 34692
		internal const int ElementTypeIdConst = 10197;

		// Token: 0x04008785 RID: 34693
		private static readonly string[] eleTagNames = new string[] { "anchor", "norm", "up", "extLst" };

		// Token: 0x04008786 RID: 34694
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
