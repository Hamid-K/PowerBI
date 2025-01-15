using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E41 RID: 11841
	[ChildElementInfo(typeof(EndnotePosition))]
	[ChildElementInfo(typeof(NumberingRestart))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(NumberingStart))]
	internal class EndnoteProperties : OpenXmlCompositeElement
	{
		// Token: 0x170089C0 RID: 35264
		// (get) Token: 0x0601924D RID: 102989 RVA: 0x00346C34 File Offset: 0x00344E34
		public override string LocalName
		{
			get
			{
				return "endnotePr";
			}
		}

		// Token: 0x170089C1 RID: 35265
		// (get) Token: 0x0601924E RID: 102990 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089C2 RID: 35266
		// (get) Token: 0x0601924F RID: 102991 RVA: 0x00346C3B File Offset: 0x00344E3B
		internal override int ElementTypeId
		{
			get
			{
				return 11527;
			}
		}

		// Token: 0x06019250 RID: 102992 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019251 RID: 102993 RVA: 0x00293ECF File Offset: 0x002920CF
		public EndnoteProperties()
		{
		}

		// Token: 0x06019252 RID: 102994 RVA: 0x00293ED7 File Offset: 0x002920D7
		public EndnoteProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019253 RID: 102995 RVA: 0x00293EE0 File Offset: 0x002920E0
		public EndnoteProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019254 RID: 102996 RVA: 0x00293EE9 File Offset: 0x002920E9
		public EndnoteProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019255 RID: 102997 RVA: 0x00346C44 File Offset: 0x00344E44
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
			return null;
		}

		// Token: 0x170089C3 RID: 35267
		// (get) Token: 0x06019256 RID: 102998 RVA: 0x00346CB2 File Offset: 0x00344EB2
		internal override string[] ElementTagNames
		{
			get
			{
				return EndnoteProperties.eleTagNames;
			}
		}

		// Token: 0x170089C4 RID: 35268
		// (get) Token: 0x06019257 RID: 102999 RVA: 0x00346CB9 File Offset: 0x00344EB9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return EndnoteProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170089C5 RID: 35269
		// (get) Token: 0x06019258 RID: 103000 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170089C6 RID: 35270
		// (get) Token: 0x06019259 RID: 103001 RVA: 0x00346CC0 File Offset: 0x00344EC0
		// (set) Token: 0x0601925A RID: 103002 RVA: 0x00346CC9 File Offset: 0x00344EC9
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

		// Token: 0x170089C7 RID: 35271
		// (get) Token: 0x0601925B RID: 103003 RVA: 0x00346B9F File Offset: 0x00344D9F
		// (set) Token: 0x0601925C RID: 103004 RVA: 0x00346BA8 File Offset: 0x00344DA8
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

		// Token: 0x170089C8 RID: 35272
		// (get) Token: 0x0601925D RID: 103005 RVA: 0x00346BB2 File Offset: 0x00344DB2
		// (set) Token: 0x0601925E RID: 103006 RVA: 0x00346BBB File Offset: 0x00344DBB
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

		// Token: 0x170089C9 RID: 35273
		// (get) Token: 0x0601925F RID: 103007 RVA: 0x00346BC5 File Offset: 0x00344DC5
		// (set) Token: 0x06019260 RID: 103008 RVA: 0x00346BCE File Offset: 0x00344DCE
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

		// Token: 0x06019261 RID: 103009 RVA: 0x00346CD3 File Offset: 0x00344ED3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndnoteProperties>(deep);
		}

		// Token: 0x0400A746 RID: 42822
		private const string tagName = "endnotePr";

		// Token: 0x0400A747 RID: 42823
		private const byte tagNsId = 23;

		// Token: 0x0400A748 RID: 42824
		internal const int ElementTypeIdConst = 11527;

		// Token: 0x0400A749 RID: 42825
		private static readonly string[] eleTagNames = new string[] { "pos", "numFmt", "numStart", "numRestart" };

		// Token: 0x0400A74A RID: 42826
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
