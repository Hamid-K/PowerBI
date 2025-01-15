using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002953 RID: 10579
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BarProperties))]
	[ChildElementInfo(typeof(Base))]
	internal class Bar : OpenXmlCompositeElement
	{
		// Token: 0x17006B8D RID: 27533
		// (get) Token: 0x06014F7B RID: 85883 RVA: 0x003196B5 File Offset: 0x003178B5
		public override string LocalName
		{
			get
			{
				return "bar";
			}
		}

		// Token: 0x17006B8E RID: 27534
		// (get) Token: 0x06014F7C RID: 85884 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006B8F RID: 27535
		// (get) Token: 0x06014F7D RID: 85885 RVA: 0x003196BC File Offset: 0x003178BC
		internal override int ElementTypeId
		{
			get
			{
				return 10843;
			}
		}

		// Token: 0x06014F7E RID: 85886 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014F7F RID: 85887 RVA: 0x00293ECF File Offset: 0x002920CF
		public Bar()
		{
		}

		// Token: 0x06014F80 RID: 85888 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Bar(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F81 RID: 85889 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Bar(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F82 RID: 85890 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Bar(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014F83 RID: 85891 RVA: 0x003196C3 File Offset: 0x003178C3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "barPr" == name)
			{
				return new BarProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006B90 RID: 27536
		// (get) Token: 0x06014F84 RID: 85892 RVA: 0x003196F6 File Offset: 0x003178F6
		internal override string[] ElementTagNames
		{
			get
			{
				return Bar.eleTagNames;
			}
		}

		// Token: 0x17006B91 RID: 27537
		// (get) Token: 0x06014F85 RID: 85893 RVA: 0x003196FD File Offset: 0x003178FD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Bar.eleNamespaceIds;
			}
		}

		// Token: 0x17006B92 RID: 27538
		// (get) Token: 0x06014F86 RID: 85894 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006B93 RID: 27539
		// (get) Token: 0x06014F87 RID: 85895 RVA: 0x00319704 File Offset: 0x00317904
		// (set) Token: 0x06014F88 RID: 85896 RVA: 0x0031970D File Offset: 0x0031790D
		public BarProperties BarProperties
		{
			get
			{
				return base.GetElement<BarProperties>(0);
			}
			set
			{
				base.SetElement<BarProperties>(0, value);
			}
		}

		// Token: 0x17006B94 RID: 27540
		// (get) Token: 0x06014F89 RID: 85897 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x06014F8A RID: 85898 RVA: 0x0031965F File Offset: 0x0031785F
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(1);
			}
			set
			{
				base.SetElement<Base>(1, value);
			}
		}

		// Token: 0x06014F8B RID: 85899 RVA: 0x00319717 File Offset: 0x00317917
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Bar>(deep);
		}

		// Token: 0x040090DE RID: 37086
		private const string tagName = "bar";

		// Token: 0x040090DF RID: 37087
		private const byte tagNsId = 21;

		// Token: 0x040090E0 RID: 37088
		internal const int ElementTypeIdConst = 10843;

		// Token: 0x040090E1 RID: 37089
		private static readonly string[] eleTagNames = new string[] { "barPr", "e" };

		// Token: 0x040090E2 RID: 37090
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
