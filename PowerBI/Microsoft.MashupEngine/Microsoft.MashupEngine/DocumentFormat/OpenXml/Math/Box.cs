using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002954 RID: 10580
	[ChildElementInfo(typeof(BoxProperties))]
	[ChildElementInfo(typeof(Base))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Box : OpenXmlCompositeElement
	{
		// Token: 0x17006B95 RID: 27541
		// (get) Token: 0x06014F8D RID: 85901 RVA: 0x002CE46E File Offset: 0x002CC66E
		public override string LocalName
		{
			get
			{
				return "box";
			}
		}

		// Token: 0x17006B96 RID: 27542
		// (get) Token: 0x06014F8E RID: 85902 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006B97 RID: 27543
		// (get) Token: 0x06014F8F RID: 85903 RVA: 0x00319761 File Offset: 0x00317961
		internal override int ElementTypeId
		{
			get
			{
				return 10844;
			}
		}

		// Token: 0x06014F90 RID: 85904 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014F91 RID: 85905 RVA: 0x00293ECF File Offset: 0x002920CF
		public Box()
		{
		}

		// Token: 0x06014F92 RID: 85906 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Box(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F93 RID: 85907 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Box(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014F94 RID: 85908 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Box(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014F95 RID: 85909 RVA: 0x00319768 File Offset: 0x00317968
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "boxPr" == name)
			{
				return new BoxProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006B98 RID: 27544
		// (get) Token: 0x06014F96 RID: 85910 RVA: 0x0031979B File Offset: 0x0031799B
		internal override string[] ElementTagNames
		{
			get
			{
				return Box.eleTagNames;
			}
		}

		// Token: 0x17006B99 RID: 27545
		// (get) Token: 0x06014F97 RID: 85911 RVA: 0x003197A2 File Offset: 0x003179A2
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Box.eleNamespaceIds;
			}
		}

		// Token: 0x17006B9A RID: 27546
		// (get) Token: 0x06014F98 RID: 85912 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006B9B RID: 27547
		// (get) Token: 0x06014F99 RID: 85913 RVA: 0x003197A9 File Offset: 0x003179A9
		// (set) Token: 0x06014F9A RID: 85914 RVA: 0x003197B2 File Offset: 0x003179B2
		public BoxProperties BoxProperties
		{
			get
			{
				return base.GetElement<BoxProperties>(0);
			}
			set
			{
				base.SetElement<BoxProperties>(0, value);
			}
		}

		// Token: 0x17006B9C RID: 27548
		// (get) Token: 0x06014F9B RID: 85915 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x06014F9C RID: 85916 RVA: 0x0031965F File Offset: 0x0031785F
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

		// Token: 0x06014F9D RID: 85917 RVA: 0x003197BC File Offset: 0x003179BC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Box>(deep);
		}

		// Token: 0x040090E3 RID: 37091
		private const string tagName = "box";

		// Token: 0x040090E4 RID: 37092
		private const byte tagNsId = 21;

		// Token: 0x040090E5 RID: 37093
		internal const int ElementTypeIdConst = 10844;

		// Token: 0x040090E6 RID: 37094
		private static readonly string[] eleTagNames = new string[] { "boxPr", "e" };

		// Token: 0x040090E7 RID: 37095
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
