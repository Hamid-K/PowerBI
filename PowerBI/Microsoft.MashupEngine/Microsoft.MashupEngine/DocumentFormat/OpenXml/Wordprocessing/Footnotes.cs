using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F13 RID: 12051
	[ChildElementInfo(typeof(Footnote))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Footnotes : OpenXmlPartRootElement
	{
		// Token: 0x17008E44 RID: 36420
		// (get) Token: 0x06019BF2 RID: 105458 RVA: 0x002A5B87 File Offset: 0x002A3D87
		public override string LocalName
		{
			get
			{
				return "footnotes";
			}
		}

		// Token: 0x17008E45 RID: 36421
		// (get) Token: 0x06019BF3 RID: 105459 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E46 RID: 36422
		// (get) Token: 0x06019BF4 RID: 105460 RVA: 0x00354BA3 File Offset: 0x00352DA3
		internal override int ElementTypeId
		{
			get
			{
				return 11693;
			}
		}

		// Token: 0x06019BF5 RID: 105461 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019BF6 RID: 105462 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Footnotes(FootnotesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019BF7 RID: 105463 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(FootnotesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17008E47 RID: 36423
		// (get) Token: 0x06019BF8 RID: 105464 RVA: 0x00354BAA File Offset: 0x00352DAA
		// (set) Token: 0x06019BF9 RID: 105465 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public FootnotesPart FootnotesPart
		{
			get
			{
				return base.OpenXmlPart as FootnotesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06019BFA RID: 105466 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Footnotes(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019BFB RID: 105467 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Footnotes(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019BFC RID: 105468 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Footnotes(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019BFD RID: 105469 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Footnotes()
		{
		}

		// Token: 0x06019BFE RID: 105470 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(FootnotesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06019BFF RID: 105471 RVA: 0x00354BB7 File Offset: 0x00352DB7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "footnote" == name)
			{
				return new Footnote();
			}
			return null;
		}

		// Token: 0x06019C00 RID: 105472 RVA: 0x00354BD2 File Offset: 0x00352DD2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Footnotes>(deep);
		}

		// Token: 0x0400AA65 RID: 43621
		private const string tagName = "footnotes";

		// Token: 0x0400AA66 RID: 43622
		private const byte tagNsId = 23;

		// Token: 0x0400AA67 RID: 43623
		internal const int ElementTypeIdConst = 11693;
	}
}
