using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027CD RID: 10189
	[ChildElementInfo(typeof(Point))]
	[GeneratedCode("DomGen", "2.0")]
	internal class LineTo : OpenXmlCompositeElement
	{
		// Token: 0x170063BB RID: 25531
		// (get) Token: 0x06013CEE RID: 81134 RVA: 0x0030BE80 File Offset: 0x0030A080
		public override string LocalName
		{
			get
			{
				return "lnTo";
			}
		}

		// Token: 0x170063BC RID: 25532
		// (get) Token: 0x06013CEF RID: 81135 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170063BD RID: 25533
		// (get) Token: 0x06013CF0 RID: 81136 RVA: 0x0030BE87 File Offset: 0x0030A087
		internal override int ElementTypeId
		{
			get
			{
				return 10223;
			}
		}

		// Token: 0x06013CF1 RID: 81137 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013CF2 RID: 81138 RVA: 0x00293ECF File Offset: 0x002920CF
		public LineTo()
		{
		}

		// Token: 0x06013CF3 RID: 81139 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LineTo(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013CF4 RID: 81140 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LineTo(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013CF5 RID: 81141 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LineTo(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013CF6 RID: 81142 RVA: 0x0030BE07 File Offset: 0x0030A007
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "pt" == name)
			{
				return new Point();
			}
			return null;
		}

		// Token: 0x170063BE RID: 25534
		// (get) Token: 0x06013CF7 RID: 81143 RVA: 0x0030BE8E File Offset: 0x0030A08E
		internal override string[] ElementTagNames
		{
			get
			{
				return LineTo.eleTagNames;
			}
		}

		// Token: 0x170063BF RID: 25535
		// (get) Token: 0x06013CF8 RID: 81144 RVA: 0x0030BE95 File Offset: 0x0030A095
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LineTo.eleNamespaceIds;
			}
		}

		// Token: 0x170063C0 RID: 25536
		// (get) Token: 0x06013CF9 RID: 81145 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170063C1 RID: 25537
		// (get) Token: 0x06013CFA RID: 81146 RVA: 0x0030BE30 File Offset: 0x0030A030
		// (set) Token: 0x06013CFB RID: 81147 RVA: 0x0030BE39 File Offset: 0x0030A039
		public Point Point
		{
			get
			{
				return base.GetElement<Point>(0);
			}
			set
			{
				base.SetElement<Point>(0, value);
			}
		}

		// Token: 0x06013CFC RID: 81148 RVA: 0x0030BE9C File Offset: 0x0030A09C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineTo>(deep);
		}

		// Token: 0x040087E1 RID: 34785
		private const string tagName = "lnTo";

		// Token: 0x040087E2 RID: 34786
		private const byte tagNsId = 10;

		// Token: 0x040087E3 RID: 34787
		internal const int ElementTypeIdConst = 10223;

		// Token: 0x040087E4 RID: 34788
		private static readonly string[] eleTagNames = new string[] { "pt" };

		// Token: 0x040087E5 RID: 34789
		private static readonly byte[] eleNamespaceIds = new byte[] { 10 };
	}
}
