using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002952 RID: 10578
	[ChildElementInfo(typeof(AccentProperties))]
	[ChildElementInfo(typeof(Base))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Accent : OpenXmlCompositeElement
	{
		// Token: 0x17006B85 RID: 27525
		// (get) Token: 0x06014F69 RID: 85865 RVA: 0x003195F4 File Offset: 0x003177F4
		public override string LocalName
		{
			get
			{
				return "acc";
			}
		}

		// Token: 0x17006B86 RID: 27526
		// (get) Token: 0x06014F6A RID: 85866 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006B87 RID: 27527
		// (get) Token: 0x06014F6B RID: 85867 RVA: 0x003195FB File Offset: 0x003177FB
		internal override int ElementTypeId
		{
			get
			{
				return 10842;
			}
		}

		// Token: 0x06014F6C RID: 85868 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014F6D RID: 85869 RVA: 0x00293ECF File Offset: 0x002920CF
		public Accent()
		{
		}

		// Token: 0x06014F6E RID: 85870 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Accent(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F6F RID: 85871 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Accent(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F70 RID: 85872 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Accent(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014F71 RID: 85873 RVA: 0x00319602 File Offset: 0x00317802
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "accPr" == name)
			{
				return new AccentProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006B88 RID: 27528
		// (get) Token: 0x06014F72 RID: 85874 RVA: 0x00319635 File Offset: 0x00317835
		internal override string[] ElementTagNames
		{
			get
			{
				return Accent.eleTagNames;
			}
		}

		// Token: 0x17006B89 RID: 27529
		// (get) Token: 0x06014F73 RID: 85875 RVA: 0x0031963C File Offset: 0x0031783C
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Accent.eleNamespaceIds;
			}
		}

		// Token: 0x17006B8A RID: 27530
		// (get) Token: 0x06014F74 RID: 85876 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006B8B RID: 27531
		// (get) Token: 0x06014F75 RID: 85877 RVA: 0x00319643 File Offset: 0x00317843
		// (set) Token: 0x06014F76 RID: 85878 RVA: 0x0031964C File Offset: 0x0031784C
		public AccentProperties AccentProperties
		{
			get
			{
				return base.GetElement<AccentProperties>(0);
			}
			set
			{
				base.SetElement<AccentProperties>(0, value);
			}
		}

		// Token: 0x17006B8C RID: 27532
		// (get) Token: 0x06014F77 RID: 85879 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x06014F78 RID: 85880 RVA: 0x0031965F File Offset: 0x0031785F
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

		// Token: 0x06014F79 RID: 85881 RVA: 0x00319669 File Offset: 0x00317869
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Accent>(deep);
		}

		// Token: 0x040090D9 RID: 37081
		private const string tagName = "acc";

		// Token: 0x040090DA RID: 37082
		private const byte tagNsId = 21;

		// Token: 0x040090DB RID: 37083
		internal const int ElementTypeIdConst = 10842;

		// Token: 0x040090DC RID: 37084
		private static readonly string[] eleTagNames = new string[] { "accPr", "e" };

		// Token: 0x040090DD RID: 37085
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
