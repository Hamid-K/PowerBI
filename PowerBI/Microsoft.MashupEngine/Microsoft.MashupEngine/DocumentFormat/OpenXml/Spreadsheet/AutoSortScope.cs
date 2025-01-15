using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CAB RID: 11435
	[ChildElementInfo(typeof(PivotArea))]
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoSortScope : OpenXmlCompositeElement
	{
		// Token: 0x17008467 RID: 33895
		// (get) Token: 0x060186EB RID: 100075 RVA: 0x00341787 File Offset: 0x0033F987
		public override string LocalName
		{
			get
			{
				return "autoSortScope";
			}
		}

		// Token: 0x17008468 RID: 33896
		// (get) Token: 0x060186EC RID: 100076 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008469 RID: 33897
		// (get) Token: 0x060186ED RID: 100077 RVA: 0x0034178E File Offset: 0x0033F98E
		internal override int ElementTypeId
		{
			get
			{
				return 11415;
			}
		}

		// Token: 0x060186EE RID: 100078 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060186EF RID: 100079 RVA: 0x00293ECF File Offset: 0x002920CF
		public AutoSortScope()
		{
		}

		// Token: 0x060186F0 RID: 100080 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AutoSortScope(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186F1 RID: 100081 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AutoSortScope(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186F2 RID: 100082 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AutoSortScope(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060186F3 RID: 100083 RVA: 0x002E9D93 File Offset: 0x002E7F93
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotArea" == name)
			{
				return new PivotArea();
			}
			return null;
		}

		// Token: 0x1700846A RID: 33898
		// (get) Token: 0x060186F4 RID: 100084 RVA: 0x00341795 File Offset: 0x0033F995
		internal override string[] ElementTagNames
		{
			get
			{
				return AutoSortScope.eleTagNames;
			}
		}

		// Token: 0x1700846B RID: 33899
		// (get) Token: 0x060186F5 RID: 100085 RVA: 0x0034179C File Offset: 0x0033F99C
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AutoSortScope.eleNamespaceIds;
			}
		}

		// Token: 0x1700846C RID: 33900
		// (get) Token: 0x060186F6 RID: 100086 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700846D RID: 33901
		// (get) Token: 0x060186F7 RID: 100087 RVA: 0x003304CB File Offset: 0x0032E6CB
		// (set) Token: 0x060186F8 RID: 100088 RVA: 0x003304D4 File Offset: 0x0032E6D4
		public PivotArea PivotArea
		{
			get
			{
				return base.GetElement<PivotArea>(0);
			}
			set
			{
				base.SetElement<PivotArea>(0, value);
			}
		}

		// Token: 0x060186F9 RID: 100089 RVA: 0x003417A3 File Offset: 0x0033F9A3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoSortScope>(deep);
		}

		// Token: 0x0400A03E RID: 41022
		private const string tagName = "autoSortScope";

		// Token: 0x0400A03F RID: 41023
		private const byte tagNsId = 22;

		// Token: 0x0400A040 RID: 41024
		internal const int ElementTypeIdConst = 11415;

		// Token: 0x0400A041 RID: 41025
		private static readonly string[] eleTagNames = new string[] { "pivotArea" };

		// Token: 0x0400A042 RID: 41026
		private static readonly byte[] eleNamespaceIds = new byte[] { 22 };
	}
}
