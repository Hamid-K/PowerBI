using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002960 RID: 10592
	[ChildElementInfo(typeof(Base))]
	[ChildElementInfo(typeof(RadicalProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Degree))]
	internal class Radical : OpenXmlCompositeElement
	{
		// Token: 0x17006BF8 RID: 27640
		// (get) Token: 0x0601506B RID: 86123 RVA: 0x0031A0F9 File Offset: 0x003182F9
		public override string LocalName
		{
			get
			{
				return "rad";
			}
		}

		// Token: 0x17006BF9 RID: 27641
		// (get) Token: 0x0601506C RID: 86124 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BFA RID: 27642
		// (get) Token: 0x0601506D RID: 86125 RVA: 0x0031A100 File Offset: 0x00318300
		internal override int ElementTypeId
		{
			get
			{
				return 10856;
			}
		}

		// Token: 0x0601506E RID: 86126 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601506F RID: 86127 RVA: 0x00293ECF File Offset: 0x002920CF
		public Radical()
		{
		}

		// Token: 0x06015070 RID: 86128 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Radical(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015071 RID: 86129 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Radical(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015072 RID: 86130 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Radical(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015073 RID: 86131 RVA: 0x0031A108 File Offset: 0x00318308
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "radPr" == name)
			{
				return new RadicalProperties();
			}
			if (21 == namespaceId && "deg" == name)
			{
				return new Degree();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006BFB RID: 27643
		// (get) Token: 0x06015074 RID: 86132 RVA: 0x0031A15E File Offset: 0x0031835E
		internal override string[] ElementTagNames
		{
			get
			{
				return Radical.eleTagNames;
			}
		}

		// Token: 0x17006BFC RID: 27644
		// (get) Token: 0x06015075 RID: 86133 RVA: 0x0031A165 File Offset: 0x00318365
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Radical.eleNamespaceIds;
			}
		}

		// Token: 0x17006BFD RID: 27645
		// (get) Token: 0x06015076 RID: 86134 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BFE RID: 27646
		// (get) Token: 0x06015077 RID: 86135 RVA: 0x0031A16C File Offset: 0x0031836C
		// (set) Token: 0x06015078 RID: 86136 RVA: 0x0031A175 File Offset: 0x00318375
		public RadicalProperties RadicalProperties
		{
			get
			{
				return base.GetElement<RadicalProperties>(0);
			}
			set
			{
				base.SetElement<RadicalProperties>(0, value);
			}
		}

		// Token: 0x17006BFF RID: 27647
		// (get) Token: 0x06015079 RID: 86137 RVA: 0x0031A17F File Offset: 0x0031837F
		// (set) Token: 0x0601507A RID: 86138 RVA: 0x0031A188 File Offset: 0x00318388
		public Degree Degree
		{
			get
			{
				return base.GetElement<Degree>(1);
			}
			set
			{
				base.SetElement<Degree>(1, value);
			}
		}

		// Token: 0x17006C00 RID: 27648
		// (get) Token: 0x0601507B RID: 86139 RVA: 0x00319B9E File Offset: 0x00317D9E
		// (set) Token: 0x0601507C RID: 86140 RVA: 0x00319BA7 File Offset: 0x00317DA7
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(2);
			}
			set
			{
				base.SetElement<Base>(2, value);
			}
		}

		// Token: 0x0601507D RID: 86141 RVA: 0x0031A192 File Offset: 0x00318392
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Radical>(deep);
		}

		// Token: 0x0400911F RID: 37151
		private const string tagName = "rad";

		// Token: 0x04009120 RID: 37152
		private const byte tagNsId = 21;

		// Token: 0x04009121 RID: 37153
		internal const int ElementTypeIdConst = 10856;

		// Token: 0x04009122 RID: 37154
		private static readonly string[] eleTagNames = new string[] { "radPr", "deg", "e" };

		// Token: 0x04009123 RID: 37155
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21 };
	}
}
