using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office.Excel
{
	// Token: 0x0200237E RID: 9086
	[ChildElementInfo(typeof(ColumnSortMap))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RowSortMap))]
	internal class WorksheetSortMap : OpenXmlPartRootElement
	{
		// Token: 0x17004B41 RID: 19265
		// (get) Token: 0x0601064C RID: 67148 RVA: 0x002E32A4 File Offset: 0x002E14A4
		public override string LocalName
		{
			get
			{
				return "worksheetSortMap";
			}
		}

		// Token: 0x17004B42 RID: 19266
		// (get) Token: 0x0601064D RID: 67149 RVA: 0x0022706E File Offset: 0x0022526E
		internal override byte NamespaceId
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17004B43 RID: 19267
		// (get) Token: 0x0601064E RID: 67150 RVA: 0x002E32AB File Offset: 0x002E14AB
		internal override int ElementTypeId
		{
			get
			{
				return 12530;
			}
		}

		// Token: 0x0601064F RID: 67151 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06010650 RID: 67152 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal WorksheetSortMap(WorksheetSortMapPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06010651 RID: 67153 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(WorksheetSortMapPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17004B44 RID: 19268
		// (get) Token: 0x06010652 RID: 67154 RVA: 0x002E32B2 File Offset: 0x002E14B2
		// (set) Token: 0x06010653 RID: 67155 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public WorksheetSortMapPart WorksheetSortMapPart
		{
			get
			{
				return base.OpenXmlPart as WorksheetSortMapPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06010654 RID: 67156 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public WorksheetSortMap(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010655 RID: 67157 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public WorksheetSortMap(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010656 RID: 67158 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public WorksheetSortMap(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010657 RID: 67159 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public WorksheetSortMap()
		{
		}

		// Token: 0x06010658 RID: 67160 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(WorksheetSortMapPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06010659 RID: 67161 RVA: 0x002E32BF File Offset: 0x002E14BF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (32 == namespaceId && "rowSortMap" == name)
			{
				return new RowSortMap();
			}
			if (32 == namespaceId && "colSortMap" == name)
			{
				return new ColumnSortMap();
			}
			return null;
		}

		// Token: 0x17004B45 RID: 19269
		// (get) Token: 0x0601065A RID: 67162 RVA: 0x002E32F2 File Offset: 0x002E14F2
		internal override string[] ElementTagNames
		{
			get
			{
				return WorksheetSortMap.eleTagNames;
			}
		}

		// Token: 0x17004B46 RID: 19270
		// (get) Token: 0x0601065B RID: 67163 RVA: 0x002E32F9 File Offset: 0x002E14F9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WorksheetSortMap.eleNamespaceIds;
			}
		}

		// Token: 0x17004B47 RID: 19271
		// (get) Token: 0x0601065C RID: 67164 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004B48 RID: 19272
		// (get) Token: 0x0601065D RID: 67165 RVA: 0x002E3300 File Offset: 0x002E1500
		// (set) Token: 0x0601065E RID: 67166 RVA: 0x002E3309 File Offset: 0x002E1509
		public RowSortMap RowSortMap
		{
			get
			{
				return base.GetElement<RowSortMap>(0);
			}
			set
			{
				base.SetElement<RowSortMap>(0, value);
			}
		}

		// Token: 0x17004B49 RID: 19273
		// (get) Token: 0x0601065F RID: 67167 RVA: 0x002E3313 File Offset: 0x002E1513
		// (set) Token: 0x06010660 RID: 67168 RVA: 0x002E331C File Offset: 0x002E151C
		public ColumnSortMap ColumnSortMap
		{
			get
			{
				return base.GetElement<ColumnSortMap>(1);
			}
			set
			{
				base.SetElement<ColumnSortMap>(1, value);
			}
		}

		// Token: 0x06010661 RID: 67169 RVA: 0x002E3326 File Offset: 0x002E1526
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorksheetSortMap>(deep);
		}

		// Token: 0x04007469 RID: 29801
		private const string tagName = "worksheetSortMap";

		// Token: 0x0400746A RID: 29802
		private const byte tagNsId = 32;

		// Token: 0x0400746B RID: 29803
		internal const int ElementTypeIdConst = 12530;

		// Token: 0x0400746C RID: 29804
		private static readonly string[] eleTagNames = new string[] { "rowSortMap", "colSortMap" };

		// Token: 0x0400746D RID: 29805
		private static readonly byte[] eleNamespaceIds = new byte[] { 32, 32 };
	}
}
