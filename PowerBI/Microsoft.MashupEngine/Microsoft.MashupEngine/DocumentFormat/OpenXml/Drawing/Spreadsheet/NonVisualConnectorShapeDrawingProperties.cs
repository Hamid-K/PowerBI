using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002895 RID: 10389
	[ChildElementInfo(typeof(StartConnection))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConnectionShapeLocks))]
	[ChildElementInfo(typeof(EndConnection))]
	internal class NonVisualConnectorShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170067CD RID: 26573
		// (get) Token: 0x06014686 RID: 83590 RVA: 0x002FC20C File Offset: 0x002FA40C
		public override string LocalName
		{
			get
			{
				return "cNvCxnSpPr";
			}
		}

		// Token: 0x170067CE RID: 26574
		// (get) Token: 0x06014687 RID: 83591 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170067CF RID: 26575
		// (get) Token: 0x06014688 RID: 83592 RVA: 0x00312EA3 File Offset: 0x003110A3
		internal override int ElementTypeId
		{
			get
			{
				return 10750;
			}
		}

		// Token: 0x06014689 RID: 83593 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601468A RID: 83594 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualConnectorShapeDrawingProperties()
		{
		}

		// Token: 0x0601468B RID: 83595 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualConnectorShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601468C RID: 83596 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualConnectorShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601468D RID: 83597 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualConnectorShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601468E RID: 83598 RVA: 0x00312EAC File Offset: 0x003110AC
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

		// Token: 0x170067D0 RID: 26576
		// (get) Token: 0x0601468F RID: 83599 RVA: 0x00312F1A File Offset: 0x0031111A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualConnectorShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170067D1 RID: 26577
		// (get) Token: 0x06014690 RID: 83600 RVA: 0x00312F21 File Offset: 0x00311121
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualConnectorShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170067D2 RID: 26578
		// (get) Token: 0x06014691 RID: 83601 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170067D3 RID: 26579
		// (get) Token: 0x06014692 RID: 83602 RVA: 0x002F02F8 File Offset: 0x002EE4F8
		// (set) Token: 0x06014693 RID: 83603 RVA: 0x002F0301 File Offset: 0x002EE501
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

		// Token: 0x170067D4 RID: 26580
		// (get) Token: 0x06014694 RID: 83604 RVA: 0x002F030B File Offset: 0x002EE50B
		// (set) Token: 0x06014695 RID: 83605 RVA: 0x002F0314 File Offset: 0x002EE514
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

		// Token: 0x170067D5 RID: 26581
		// (get) Token: 0x06014696 RID: 83606 RVA: 0x002F031E File Offset: 0x002EE51E
		// (set) Token: 0x06014697 RID: 83607 RVA: 0x002F0327 File Offset: 0x002EE527
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

		// Token: 0x170067D6 RID: 26582
		// (get) Token: 0x06014698 RID: 83608 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06014699 RID: 83609 RVA: 0x002E0C32 File Offset: 0x002DEE32
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

		// Token: 0x0601469A RID: 83610 RVA: 0x00312F28 File Offset: 0x00311128
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualConnectorShapeDrawingProperties>(deep);
		}

		// Token: 0x04008DF7 RID: 36343
		private const string tagName = "cNvCxnSpPr";

		// Token: 0x04008DF8 RID: 36344
		private const byte tagNsId = 18;

		// Token: 0x04008DF9 RID: 36345
		internal const int ElementTypeIdConst = 10750;

		// Token: 0x04008DFA RID: 36346
		private static readonly string[] eleTagNames = new string[] { "cxnSpLocks", "stCxn", "endCxn", "extLst" };

		// Token: 0x04008DFB RID: 36347
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
