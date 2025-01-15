using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E40 RID: 11840
	[ChildElementInfo(typeof(NumberingFormat))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FootnotePosition))]
	[ChildElementInfo(typeof(NumberingStart))]
	[ChildElementInfo(typeof(NumberingRestart))]
	internal class FootnoteProperties : OpenXmlCompositeElement
	{
		// Token: 0x170089B6 RID: 35254
		// (get) Token: 0x06019237 RID: 102967 RVA: 0x00346AFF File Offset: 0x00344CFF
		public override string LocalName
		{
			get
			{
				return "footnotePr";
			}
		}

		// Token: 0x170089B7 RID: 35255
		// (get) Token: 0x06019238 RID: 102968 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089B8 RID: 35256
		// (get) Token: 0x06019239 RID: 102969 RVA: 0x00346B06 File Offset: 0x00344D06
		internal override int ElementTypeId
		{
			get
			{
				return 11526;
			}
		}

		// Token: 0x0601923A RID: 102970 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601923B RID: 102971 RVA: 0x00293ECF File Offset: 0x002920CF
		public FootnoteProperties()
		{
		}

		// Token: 0x0601923C RID: 102972 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FootnoteProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601923D RID: 102973 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FootnoteProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601923E RID: 102974 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FootnoteProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601923F RID: 102975 RVA: 0x00346B10 File Offset: 0x00344D10
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
			return null;
		}

		// Token: 0x170089B9 RID: 35257
		// (get) Token: 0x06019240 RID: 102976 RVA: 0x00346B7E File Offset: 0x00344D7E
		internal override string[] ElementTagNames
		{
			get
			{
				return FootnoteProperties.eleTagNames;
			}
		}

		// Token: 0x170089BA RID: 35258
		// (get) Token: 0x06019241 RID: 102977 RVA: 0x00346B85 File Offset: 0x00344D85
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FootnoteProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170089BB RID: 35259
		// (get) Token: 0x06019242 RID: 102978 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170089BC RID: 35260
		// (get) Token: 0x06019243 RID: 102979 RVA: 0x00346B8C File Offset: 0x00344D8C
		// (set) Token: 0x06019244 RID: 102980 RVA: 0x00346B95 File Offset: 0x00344D95
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

		// Token: 0x170089BD RID: 35261
		// (get) Token: 0x06019245 RID: 102981 RVA: 0x00346B9F File Offset: 0x00344D9F
		// (set) Token: 0x06019246 RID: 102982 RVA: 0x00346BA8 File Offset: 0x00344DA8
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

		// Token: 0x170089BE RID: 35262
		// (get) Token: 0x06019247 RID: 102983 RVA: 0x00346BB2 File Offset: 0x00344DB2
		// (set) Token: 0x06019248 RID: 102984 RVA: 0x00346BBB File Offset: 0x00344DBB
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

		// Token: 0x170089BF RID: 35263
		// (get) Token: 0x06019249 RID: 102985 RVA: 0x00346BC5 File Offset: 0x00344DC5
		// (set) Token: 0x0601924A RID: 102986 RVA: 0x00346BCE File Offset: 0x00344DCE
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

		// Token: 0x0601924B RID: 102987 RVA: 0x00346BD8 File Offset: 0x00344DD8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FootnoteProperties>(deep);
		}

		// Token: 0x0400A741 RID: 42817
		private const string tagName = "footnotePr";

		// Token: 0x0400A742 RID: 42818
		private const byte tagNsId = 23;

		// Token: 0x0400A743 RID: 42819
		internal const int ElementTypeIdConst = 11526;

		// Token: 0x0400A744 RID: 42820
		private static readonly string[] eleTagNames = new string[] { "pos", "numFmt", "numStart", "numRestart" };

		// Token: 0x0400A745 RID: 42821
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
