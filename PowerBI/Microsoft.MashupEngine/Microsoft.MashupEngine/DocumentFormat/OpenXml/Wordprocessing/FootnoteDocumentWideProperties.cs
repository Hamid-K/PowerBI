using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FF3 RID: 12275
	[ChildElementInfo(typeof(FootnotePosition))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NumberingStart))]
	[ChildElementInfo(typeof(FootnoteSpecialReference))]
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(NumberingRestart))]
	internal class FootnoteDocumentWideProperties : OpenXmlCompositeElement
	{
		// Token: 0x17009545 RID: 38213
		// (get) Token: 0x0601AB34 RID: 109364 RVA: 0x00346AFF File Offset: 0x00344CFF
		public override string LocalName
		{
			get
			{
				return "footnotePr";
			}
		}

		// Token: 0x17009546 RID: 38214
		// (get) Token: 0x0601AB35 RID: 109365 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009547 RID: 38215
		// (get) Token: 0x0601AB36 RID: 109366 RVA: 0x00366049 File Offset: 0x00364249
		internal override int ElementTypeId
		{
			get
			{
				return 12036;
			}
		}

		// Token: 0x0601AB37 RID: 109367 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AB38 RID: 109368 RVA: 0x00293ECF File Offset: 0x002920CF
		public FootnoteDocumentWideProperties()
		{
		}

		// Token: 0x0601AB39 RID: 109369 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FootnoteDocumentWideProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB3A RID: 109370 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FootnoteDocumentWideProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB3B RID: 109371 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FootnoteDocumentWideProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AB3C RID: 109372 RVA: 0x00366050 File Offset: 0x00364250
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "pos" == name)
			{
				return new FootnotePosition();
			}
			if (23 == namespaceId && "numFmt" == name)
			{
				return new NumberingFormat();
			}
			if (23 == namespaceId && "numStart" == name)
			{
				return new NumberingStart();
			}
			if (23 == namespaceId && "numRestart" == name)
			{
				return new NumberingRestart();
			}
			if (23 == namespaceId && "footnote" == name)
			{
				return new FootnoteSpecialReference();
			}
			return null;
		}

		// Token: 0x17009548 RID: 38216
		// (get) Token: 0x0601AB3D RID: 109373 RVA: 0x003660D6 File Offset: 0x003642D6
		internal override string[] ElementTagNames
		{
			get
			{
				return FootnoteDocumentWideProperties.eleTagNames;
			}
		}

		// Token: 0x17009549 RID: 38217
		// (get) Token: 0x0601AB3E RID: 109374 RVA: 0x003660DD File Offset: 0x003642DD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FootnoteDocumentWideProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700954A RID: 38218
		// (get) Token: 0x0601AB3F RID: 109375 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700954B RID: 38219
		// (get) Token: 0x0601AB40 RID: 109376 RVA: 0x00346B8C File Offset: 0x00344D8C
		// (set) Token: 0x0601AB41 RID: 109377 RVA: 0x00346B95 File Offset: 0x00344D95
		public FootnotePosition FootnotePosition
		{
			get
			{
				return base.GetElement<FootnotePosition>(0);
			}
			set
			{
				base.SetElement<FootnotePosition>(0, value);
			}
		}

		// Token: 0x1700954C RID: 38220
		// (get) Token: 0x0601AB42 RID: 109378 RVA: 0x00346B9F File Offset: 0x00344D9F
		// (set) Token: 0x0601AB43 RID: 109379 RVA: 0x00346BA8 File Offset: 0x00344DA8
		public NumberingFormat NumberingFormat
		{
			get
			{
				return base.GetElement<NumberingFormat>(1);
			}
			set
			{
				base.SetElement<NumberingFormat>(1, value);
			}
		}

		// Token: 0x1700954D RID: 38221
		// (get) Token: 0x0601AB44 RID: 109380 RVA: 0x00346BB2 File Offset: 0x00344DB2
		// (set) Token: 0x0601AB45 RID: 109381 RVA: 0x00346BBB File Offset: 0x00344DBB
		public NumberingStart NumberingStart
		{
			get
			{
				return base.GetElement<NumberingStart>(2);
			}
			set
			{
				base.SetElement<NumberingStart>(2, value);
			}
		}

		// Token: 0x1700954E RID: 38222
		// (get) Token: 0x0601AB46 RID: 109382 RVA: 0x00346BC5 File Offset: 0x00344DC5
		// (set) Token: 0x0601AB47 RID: 109383 RVA: 0x00346BCE File Offset: 0x00344DCE
		public NumberingRestart NumberingRestart
		{
			get
			{
				return base.GetElement<NumberingRestart>(3);
			}
			set
			{
				base.SetElement<NumberingRestart>(3, value);
			}
		}

		// Token: 0x0601AB48 RID: 109384 RVA: 0x003660E4 File Offset: 0x003642E4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FootnoteDocumentWideProperties>(deep);
		}

		// Token: 0x0400AE17 RID: 44567
		private const string tagName = "footnotePr";

		// Token: 0x0400AE18 RID: 44568
		private const byte tagNsId = 23;

		// Token: 0x0400AE19 RID: 44569
		internal const int ElementTypeIdConst = 12036;

		// Token: 0x0400AE1A RID: 44570
		private static readonly string[] eleTagNames = new string[] { "pos", "numFmt", "numStart", "numRestart", "footnote" };

		// Token: 0x0400AE1B RID: 44571
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23 };
	}
}
