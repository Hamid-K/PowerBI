using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025B0 RID: 9648
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(Index))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BandFormat : OpenXmlCompositeElement
	{
		// Token: 0x17005728 RID: 22312
		// (get) Token: 0x060120F9 RID: 73977 RVA: 0x002F524F File Offset: 0x002F344F
		public override string LocalName
		{
			get
			{
				return "bandFmt";
			}
		}

		// Token: 0x17005729 RID: 22313
		// (get) Token: 0x060120FA RID: 73978 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700572A RID: 22314
		// (get) Token: 0x060120FB RID: 73979 RVA: 0x002F5256 File Offset: 0x002F3456
		internal override int ElementTypeId
		{
			get
			{
				return 10468;
			}
		}

		// Token: 0x060120FC RID: 73980 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060120FD RID: 73981 RVA: 0x00293ECF File Offset: 0x002920CF
		public BandFormat()
		{
		}

		// Token: 0x060120FE RID: 73982 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BandFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060120FF RID: 73983 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BandFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012100 RID: 73984 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BandFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012101 RID: 73985 RVA: 0x002F525D File Offset: 0x002F345D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "idx" == name)
			{
				return new Index();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			return null;
		}

		// Token: 0x1700572B RID: 22315
		// (get) Token: 0x06012102 RID: 73986 RVA: 0x002F5290 File Offset: 0x002F3490
		internal override string[] ElementTagNames
		{
			get
			{
				return BandFormat.eleTagNames;
			}
		}

		// Token: 0x1700572C RID: 22316
		// (get) Token: 0x06012103 RID: 73987 RVA: 0x002F5297 File Offset: 0x002F3497
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BandFormat.eleNamespaceIds;
			}
		}

		// Token: 0x1700572D RID: 22317
		// (get) Token: 0x06012104 RID: 73988 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700572E RID: 22318
		// (get) Token: 0x06012105 RID: 73989 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x06012106 RID: 73990 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
		public Index Index
		{
			get
			{
				return base.GetElement<Index>(0);
			}
			set
			{
				base.SetElement<Index>(0, value);
			}
		}

		// Token: 0x1700572F RID: 22319
		// (get) Token: 0x06012107 RID: 73991 RVA: 0x002F529E File Offset: 0x002F349E
		// (set) Token: 0x06012108 RID: 73992 RVA: 0x002F52A7 File Offset: 0x002F34A7
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(1);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(1, value);
			}
		}

		// Token: 0x06012109 RID: 73993 RVA: 0x002F52B1 File Offset: 0x002F34B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BandFormat>(deep);
		}

		// Token: 0x04007DFD RID: 32253
		private const string tagName = "bandFmt";

		// Token: 0x04007DFE RID: 32254
		private const byte tagNsId = 11;

		// Token: 0x04007DFF RID: 32255
		internal const int ElementTypeIdConst = 10468;

		// Token: 0x04007E00 RID: 32256
		private static readonly string[] eleTagNames = new string[] { "idx", "spPr" };

		// Token: 0x04007E01 RID: 32257
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11 };
	}
}
