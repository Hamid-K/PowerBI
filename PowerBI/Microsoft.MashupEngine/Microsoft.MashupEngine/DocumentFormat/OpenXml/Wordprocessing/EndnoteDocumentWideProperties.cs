using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FF4 RID: 12276
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EndnoteSpecialReference))]
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(NumberingRestart))]
	[ChildElementInfo(typeof(EndnotePosition))]
	[ChildElementInfo(typeof(NumberingStart))]
	internal class EndnoteDocumentWideProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700954F RID: 38223
		// (get) Token: 0x0601AB4A RID: 109386 RVA: 0x00346C34 File Offset: 0x00344E34
		public override string LocalName
		{
			get
			{
				return "endnotePr";
			}
		}

		// Token: 0x17009550 RID: 38224
		// (get) Token: 0x0601AB4B RID: 109387 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009551 RID: 38225
		// (get) Token: 0x0601AB4C RID: 109388 RVA: 0x00366148 File Offset: 0x00364348
		internal override int ElementTypeId
		{
			get
			{
				return 12037;
			}
		}

		// Token: 0x0601AB4D RID: 109389 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AB4E RID: 109390 RVA: 0x00293ECF File Offset: 0x002920CF
		public EndnoteDocumentWideProperties()
		{
		}

		// Token: 0x0601AB4F RID: 109391 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EndnoteDocumentWideProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB50 RID: 109392 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EndnoteDocumentWideProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB51 RID: 109393 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EndnoteDocumentWideProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AB52 RID: 109394 RVA: 0x00366150 File Offset: 0x00364350
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "pos" == name)
			{
				return new EndnotePosition();
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
			if (23 == namespaceId && "endnote" == name)
			{
				return new EndnoteSpecialReference();
			}
			return null;
		}

		// Token: 0x17009552 RID: 38226
		// (get) Token: 0x0601AB53 RID: 109395 RVA: 0x003661D6 File Offset: 0x003643D6
		internal override string[] ElementTagNames
		{
			get
			{
				return EndnoteDocumentWideProperties.eleTagNames;
			}
		}

		// Token: 0x17009553 RID: 38227
		// (get) Token: 0x0601AB54 RID: 109396 RVA: 0x003661DD File Offset: 0x003643DD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return EndnoteDocumentWideProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17009554 RID: 38228
		// (get) Token: 0x0601AB55 RID: 109397 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009555 RID: 38229
		// (get) Token: 0x0601AB56 RID: 109398 RVA: 0x00346CC0 File Offset: 0x00344EC0
		// (set) Token: 0x0601AB57 RID: 109399 RVA: 0x00346CC9 File Offset: 0x00344EC9
		public EndnotePosition EndnotePosition
		{
			get
			{
				return base.GetElement<EndnotePosition>(0);
			}
			set
			{
				base.SetElement<EndnotePosition>(0, value);
			}
		}

		// Token: 0x17009556 RID: 38230
		// (get) Token: 0x0601AB58 RID: 109400 RVA: 0x00346B9F File Offset: 0x00344D9F
		// (set) Token: 0x0601AB59 RID: 109401 RVA: 0x00346BA8 File Offset: 0x00344DA8
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

		// Token: 0x17009557 RID: 38231
		// (get) Token: 0x0601AB5A RID: 109402 RVA: 0x00346BB2 File Offset: 0x00344DB2
		// (set) Token: 0x0601AB5B RID: 109403 RVA: 0x00346BBB File Offset: 0x00344DBB
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

		// Token: 0x17009558 RID: 38232
		// (get) Token: 0x0601AB5C RID: 109404 RVA: 0x00346BC5 File Offset: 0x00344DC5
		// (set) Token: 0x0601AB5D RID: 109405 RVA: 0x00346BCE File Offset: 0x00344DCE
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

		// Token: 0x0601AB5E RID: 109406 RVA: 0x003661E4 File Offset: 0x003643E4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndnoteDocumentWideProperties>(deep);
		}

		// Token: 0x0400AE1C RID: 44572
		private const string tagName = "endnotePr";

		// Token: 0x0400AE1D RID: 44573
		private const byte tagNsId = 23;

		// Token: 0x0400AE1E RID: 44574
		internal const int ElementTypeIdConst = 12037;

		// Token: 0x0400AE1F RID: 44575
		private static readonly string[] eleTagNames = new string[] { "pos", "numFmt", "numStart", "numRestart", "endnote" };

		// Token: 0x0400AE20 RID: 44576
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23 };
	}
}
