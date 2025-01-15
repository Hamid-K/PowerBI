using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002601 RID: 9729
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotTableName))]
	[ChildElementInfo(typeof(FormatId))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class PivotSource : OpenXmlCompositeElement
	{
		// Token: 0x170059A3 RID: 22947
		// (get) Token: 0x06012664 RID: 75364 RVA: 0x002FA937 File Offset: 0x002F8B37
		public override string LocalName
		{
			get
			{
				return "pivotSource";
			}
		}

		// Token: 0x170059A4 RID: 22948
		// (get) Token: 0x06012665 RID: 75365 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170059A5 RID: 22949
		// (get) Token: 0x06012666 RID: 75366 RVA: 0x002FA93E File Offset: 0x002F8B3E
		internal override int ElementTypeId
		{
			get
			{
				return 10576;
			}
		}

		// Token: 0x06012667 RID: 75367 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012668 RID: 75368 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotSource()
		{
		}

		// Token: 0x06012669 RID: 75369 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotSource(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601266A RID: 75370 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotSource(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601266B RID: 75371 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotSource(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601266C RID: 75372 RVA: 0x002FA948 File Offset: 0x002F8B48
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "name" == name)
			{
				return new PivotTableName();
			}
			if (11 == namespaceId && "fmtId" == name)
			{
				return new FormatId();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170059A6 RID: 22950
		// (get) Token: 0x0601266D RID: 75373 RVA: 0x002FA99E File Offset: 0x002F8B9E
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotSource.eleTagNames;
			}
		}

		// Token: 0x170059A7 RID: 22951
		// (get) Token: 0x0601266E RID: 75374 RVA: 0x002FA9A5 File Offset: 0x002F8BA5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotSource.eleNamespaceIds;
			}
		}

		// Token: 0x170059A8 RID: 22952
		// (get) Token: 0x0601266F RID: 75375 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170059A9 RID: 22953
		// (get) Token: 0x06012670 RID: 75376 RVA: 0x002FA9AC File Offset: 0x002F8BAC
		// (set) Token: 0x06012671 RID: 75377 RVA: 0x002FA9B5 File Offset: 0x002F8BB5
		public PivotTableName PivotTableName
		{
			get
			{
				return base.GetElement<PivotTableName>(0);
			}
			set
			{
				base.SetElement<PivotTableName>(0, value);
			}
		}

		// Token: 0x170059AA RID: 22954
		// (get) Token: 0x06012672 RID: 75378 RVA: 0x002FA9BF File Offset: 0x002F8BBF
		// (set) Token: 0x06012673 RID: 75379 RVA: 0x002FA9C8 File Offset: 0x002F8BC8
		public FormatId FormatId
		{
			get
			{
				return base.GetElement<FormatId>(1);
			}
			set
			{
				base.SetElement<FormatId>(1, value);
			}
		}

		// Token: 0x170059AB RID: 22955
		// (get) Token: 0x06012674 RID: 75380 RVA: 0x002F389E File Offset: 0x002F1A9E
		// (set) Token: 0x06012675 RID: 75381 RVA: 0x002F38A7 File Offset: 0x002F1AA7
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06012676 RID: 75382 RVA: 0x002FA9D2 File Offset: 0x002F8BD2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotSource>(deep);
		}

		// Token: 0x04007F6B RID: 32619
		private const string tagName = "pivotSource";

		// Token: 0x04007F6C RID: 32620
		private const byte tagNsId = 11;

		// Token: 0x04007F6D RID: 32621
		internal const int ElementTypeIdConst = 10576;

		// Token: 0x04007F6E RID: 32622
		private static readonly string[] eleTagNames = new string[] { "name", "fmtId", "extLst" };

		// Token: 0x04007F6F RID: 32623
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11 };
	}
}
