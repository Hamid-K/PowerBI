using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingShape
{
	// Token: 0x020024FD RID: 9469
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(StartConnection))]
	[ChildElementInfo(typeof(EndConnection))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConnectionShapeLocks))]
	internal class NonVisualConnectorProperties : OpenXmlCompositeElement
	{
		// Token: 0x170053C2 RID: 21442
		// (get) Token: 0x0601196C RID: 72044 RVA: 0x002F026B File Offset: 0x002EE46B
		public override string LocalName
		{
			get
			{
				return "cNvCnPr";
			}
		}

		// Token: 0x170053C3 RID: 21443
		// (get) Token: 0x0601196D RID: 72045 RVA: 0x002EFE53 File Offset: 0x002EE053
		internal override byte NamespaceId
		{
			get
			{
				return 61;
			}
		}

		// Token: 0x170053C4 RID: 21444
		// (get) Token: 0x0601196E RID: 72046 RVA: 0x002F0272 File Offset: 0x002EE472
		internal override int ElementTypeId
		{
			get
			{
				return 13135;
			}
		}

		// Token: 0x0601196F RID: 72047 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011970 RID: 72048 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualConnectorProperties()
		{
		}

		// Token: 0x06011971 RID: 72049 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualConnectorProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011972 RID: 72050 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualConnectorProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011973 RID: 72051 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualConnectorProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011974 RID: 72052 RVA: 0x002F027C File Offset: 0x002EE47C
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

		// Token: 0x170053C5 RID: 21445
		// (get) Token: 0x06011975 RID: 72053 RVA: 0x002F02EA File Offset: 0x002EE4EA
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualConnectorProperties.eleTagNames;
			}
		}

		// Token: 0x170053C6 RID: 21446
		// (get) Token: 0x06011976 RID: 72054 RVA: 0x002F02F1 File Offset: 0x002EE4F1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualConnectorProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170053C7 RID: 21447
		// (get) Token: 0x06011977 RID: 72055 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170053C8 RID: 21448
		// (get) Token: 0x06011978 RID: 72056 RVA: 0x002F02F8 File Offset: 0x002EE4F8
		// (set) Token: 0x06011979 RID: 72057 RVA: 0x002F0301 File Offset: 0x002EE501
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

		// Token: 0x170053C9 RID: 21449
		// (get) Token: 0x0601197A RID: 72058 RVA: 0x002F030B File Offset: 0x002EE50B
		// (set) Token: 0x0601197B RID: 72059 RVA: 0x002F0314 File Offset: 0x002EE514
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

		// Token: 0x170053CA RID: 21450
		// (get) Token: 0x0601197C RID: 72060 RVA: 0x002F031E File Offset: 0x002EE51E
		// (set) Token: 0x0601197D RID: 72061 RVA: 0x002F0327 File Offset: 0x002EE527
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

		// Token: 0x170053CB RID: 21451
		// (get) Token: 0x0601197E RID: 72062 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x0601197F RID: 72063 RVA: 0x002E0C32 File Offset: 0x002DEE32
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

		// Token: 0x06011980 RID: 72064 RVA: 0x002F0331 File Offset: 0x002EE531
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualConnectorProperties>(deep);
		}

		// Token: 0x04007B71 RID: 31601
		private const string tagName = "cNvCnPr";

		// Token: 0x04007B72 RID: 31602
		private const byte tagNsId = 61;

		// Token: 0x04007B73 RID: 31603
		internal const int ElementTypeIdConst = 13135;

		// Token: 0x04007B74 RID: 31604
		private static readonly string[] eleTagNames = new string[] { "cxnSpLocks", "stCxn", "endCxn", "extLst" };

		// Token: 0x04007B75 RID: 31605
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
