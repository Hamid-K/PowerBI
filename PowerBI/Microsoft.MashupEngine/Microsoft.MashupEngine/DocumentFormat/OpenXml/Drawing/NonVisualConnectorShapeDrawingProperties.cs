using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027AA RID: 10154
	[ChildElementInfo(typeof(StartConnection))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConnectionShapeLocks))]
	[ChildElementInfo(typeof(EndConnection))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NonVisualConnectorShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170062CD RID: 25293
		// (get) Token: 0x06013AF3 RID: 80627 RVA: 0x002FC20C File Offset: 0x002FA40C
		public override string LocalName
		{
			get
			{
				return "cNvCxnSpPr";
			}
		}

		// Token: 0x170062CE RID: 25294
		// (get) Token: 0x06013AF4 RID: 80628 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170062CF RID: 25295
		// (get) Token: 0x06013AF5 RID: 80629 RVA: 0x0030ABF0 File Offset: 0x00308DF0
		internal override int ElementTypeId
		{
			get
			{
				return 10187;
			}
		}

		// Token: 0x06013AF6 RID: 80630 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013AF7 RID: 80631 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualConnectorShapeDrawingProperties()
		{
		}

		// Token: 0x06013AF8 RID: 80632 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualConnectorShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013AF9 RID: 80633 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualConnectorShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013AFA RID: 80634 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualConnectorShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013AFB RID: 80635 RVA: 0x0030ABF8 File Offset: 0x00308DF8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "cxnSpLocks" == name)
			{
				return new ConnectionShapeLocks();
			}
			if (10 == namespaceId && "stCxn" == name)
			{
				return new StartConnection();
			}
			if (10 == namespaceId && "endCxn" == name)
			{
				return new EndConnection();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170062D0 RID: 25296
		// (get) Token: 0x06013AFC RID: 80636 RVA: 0x0030AC66 File Offset: 0x00308E66
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualConnectorShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170062D1 RID: 25297
		// (get) Token: 0x06013AFD RID: 80637 RVA: 0x0030AC6D File Offset: 0x00308E6D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualConnectorShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170062D2 RID: 25298
		// (get) Token: 0x06013AFE RID: 80638 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170062D3 RID: 25299
		// (get) Token: 0x06013AFF RID: 80639 RVA: 0x002F02F8 File Offset: 0x002EE4F8
		// (set) Token: 0x06013B00 RID: 80640 RVA: 0x002F0301 File Offset: 0x002EE501
		public ConnectionShapeLocks ConnectionShapeLocks
		{
			get
			{
				return base.GetElement<ConnectionShapeLocks>(0);
			}
			set
			{
				base.SetElement<ConnectionShapeLocks>(0, value);
			}
		}

		// Token: 0x170062D4 RID: 25300
		// (get) Token: 0x06013B01 RID: 80641 RVA: 0x002F030B File Offset: 0x002EE50B
		// (set) Token: 0x06013B02 RID: 80642 RVA: 0x002F0314 File Offset: 0x002EE514
		public StartConnection StartConnection
		{
			get
			{
				return base.GetElement<StartConnection>(1);
			}
			set
			{
				base.SetElement<StartConnection>(1, value);
			}
		}

		// Token: 0x170062D5 RID: 25301
		// (get) Token: 0x06013B03 RID: 80643 RVA: 0x002F031E File Offset: 0x002EE51E
		// (set) Token: 0x06013B04 RID: 80644 RVA: 0x002F0327 File Offset: 0x002EE527
		public EndConnection EndConnection
		{
			get
			{
				return base.GetElement<EndConnection>(2);
			}
			set
			{
				base.SetElement<EndConnection>(2, value);
			}
		}

		// Token: 0x170062D6 RID: 25302
		// (get) Token: 0x06013B05 RID: 80645 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06013B06 RID: 80646 RVA: 0x002E0C32 File Offset: 0x002DEE32
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x06013B07 RID: 80647 RVA: 0x0030AC74 File Offset: 0x00308E74
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualConnectorShapeDrawingProperties>(deep);
		}

		// Token: 0x0400874A RID: 34634
		private const string tagName = "cNvCxnSpPr";

		// Token: 0x0400874B RID: 34635
		private const byte tagNsId = 10;

		// Token: 0x0400874C RID: 34636
		internal const int ElementTypeIdConst = 10187;

		// Token: 0x0400874D RID: 34637
		private static readonly string[] eleTagNames = new string[] { "cxnSpLocks", "stCxn", "endCxn", "extLst" };

		// Token: 0x0400874E RID: 34638
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
