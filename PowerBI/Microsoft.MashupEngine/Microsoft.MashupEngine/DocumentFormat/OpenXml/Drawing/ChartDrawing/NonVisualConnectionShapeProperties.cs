using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002634 RID: 9780
	[ChildElementInfo(typeof(EndConnection))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConnectionShapeLocks))]
	[ChildElementInfo(typeof(StartConnection))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class NonVisualConnectionShapeProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005A76 RID: 23158
		// (get) Token: 0x06012832 RID: 75826 RVA: 0x002FC20C File Offset: 0x002FA40C
		public override string LocalName
		{
			get
			{
				return "cNvCxnSpPr";
			}
		}

		// Token: 0x17005A77 RID: 23159
		// (get) Token: 0x06012833 RID: 75827 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005A78 RID: 23160
		// (get) Token: 0x06012834 RID: 75828 RVA: 0x002FC213 File Offset: 0x002FA413
		internal override int ElementTypeId
		{
			get
			{
				return 10599;
			}
		}

		// Token: 0x06012835 RID: 75829 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012836 RID: 75830 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualConnectionShapeProperties()
		{
		}

		// Token: 0x06012837 RID: 75831 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualConnectionShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012838 RID: 75832 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualConnectionShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012839 RID: 75833 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualConnectionShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601283A RID: 75834 RVA: 0x002FC21C File Offset: 0x002FA41C
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

		// Token: 0x17005A79 RID: 23161
		// (get) Token: 0x0601283B RID: 75835 RVA: 0x002FC28A File Offset: 0x002FA48A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualConnectionShapeProperties.eleTagNames;
			}
		}

		// Token: 0x17005A7A RID: 23162
		// (get) Token: 0x0601283C RID: 75836 RVA: 0x002FC291 File Offset: 0x002FA491
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualConnectionShapeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005A7B RID: 23163
		// (get) Token: 0x0601283D RID: 75837 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005A7C RID: 23164
		// (get) Token: 0x0601283E RID: 75838 RVA: 0x002F02F8 File Offset: 0x002EE4F8
		// (set) Token: 0x0601283F RID: 75839 RVA: 0x002F0301 File Offset: 0x002EE501
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

		// Token: 0x17005A7D RID: 23165
		// (get) Token: 0x06012840 RID: 75840 RVA: 0x002F030B File Offset: 0x002EE50B
		// (set) Token: 0x06012841 RID: 75841 RVA: 0x002F0314 File Offset: 0x002EE514
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

		// Token: 0x17005A7E RID: 23166
		// (get) Token: 0x06012842 RID: 75842 RVA: 0x002F031E File Offset: 0x002EE51E
		// (set) Token: 0x06012843 RID: 75843 RVA: 0x002F0327 File Offset: 0x002EE527
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

		// Token: 0x17005A7F RID: 23167
		// (get) Token: 0x06012844 RID: 75844 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06012845 RID: 75845 RVA: 0x002E0C32 File Offset: 0x002DEE32
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

		// Token: 0x06012846 RID: 75846 RVA: 0x002FC298 File Offset: 0x002FA498
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualConnectionShapeProperties>(deep);
		}

		// Token: 0x04008074 RID: 32884
		private const string tagName = "cNvCxnSpPr";

		// Token: 0x04008075 RID: 32885
		private const byte tagNsId = 12;

		// Token: 0x04008076 RID: 32886
		internal const int ElementTypeIdConst = 10599;

		// Token: 0x04008077 RID: 32887
		private static readonly string[] eleTagNames = new string[] { "cxnSpLocks", "stCxn", "endCxn", "extLst" };

		// Token: 0x04008078 RID: 32888
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
