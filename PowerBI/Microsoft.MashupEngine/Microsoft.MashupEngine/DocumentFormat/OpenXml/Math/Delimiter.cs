using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002956 RID: 10582
	[ChildElementInfo(typeof(Base))]
	[ChildElementInfo(typeof(DelimiterProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Delimiter : OpenXmlCompositeElement
	{
		// Token: 0x17006BA5 RID: 27557
		// (get) Token: 0x06014FB1 RID: 85937 RVA: 0x003198B5 File Offset: 0x00317AB5
		public override string LocalName
		{
			get
			{
				return "d";
			}
		}

		// Token: 0x17006BA6 RID: 27558
		// (get) Token: 0x06014FB2 RID: 85938 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BA7 RID: 27559
		// (get) Token: 0x06014FB3 RID: 85939 RVA: 0x003198BC File Offset: 0x00317ABC
		internal override int ElementTypeId
		{
			get
			{
				return 10846;
			}
		}

		// Token: 0x06014FB4 RID: 85940 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014FB5 RID: 85941 RVA: 0x00293ECF File Offset: 0x002920CF
		public Delimiter()
		{
		}

		// Token: 0x06014FB6 RID: 85942 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Delimiter(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FB7 RID: 85943 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Delimiter(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014FB8 RID: 85944 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Delimiter(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014FB9 RID: 85945 RVA: 0x003198C3 File Offset: 0x00317AC3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "dPr" == name)
			{
				return new DelimiterProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x17006BA8 RID: 27560
		// (get) Token: 0x06014FBA RID: 85946 RVA: 0x003198F6 File Offset: 0x00317AF6
		internal override string[] ElementTagNames
		{
			get
			{
				return Delimiter.eleTagNames;
			}
		}

		// Token: 0x17006BA9 RID: 27561
		// (get) Token: 0x06014FBB RID: 85947 RVA: 0x003198FD File Offset: 0x00317AFD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Delimiter.eleNamespaceIds;
			}
		}

		// Token: 0x17006BAA RID: 27562
		// (get) Token: 0x06014FBC RID: 85948 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BAB RID: 27563
		// (get) Token: 0x06014FBD RID: 85949 RVA: 0x00319904 File Offset: 0x00317B04
		// (set) Token: 0x06014FBE RID: 85950 RVA: 0x0031990D File Offset: 0x00317B0D
		public DelimiterProperties DelimiterProperties
		{
			get
			{
				return base.GetElement<DelimiterProperties>(0);
			}
			set
			{
				base.SetElement<DelimiterProperties>(0, value);
			}
		}

		// Token: 0x06014FBF RID: 85951 RVA: 0x00319917 File Offset: 0x00317B17
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Delimiter>(deep);
		}

		// Token: 0x040090ED RID: 37101
		private const string tagName = "d";

		// Token: 0x040090EE RID: 37102
		private const byte tagNsId = 21;

		// Token: 0x040090EF RID: 37103
		internal const int ElementTypeIdConst = 10846;

		// Token: 0x040090F0 RID: 37104
		private static readonly string[] eleTagNames = new string[] { "dPr", "e" };

		// Token: 0x040090F1 RID: 37105
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
