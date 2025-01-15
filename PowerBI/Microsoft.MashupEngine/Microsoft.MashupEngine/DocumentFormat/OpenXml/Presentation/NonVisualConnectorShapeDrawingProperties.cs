using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A64 RID: 10852
	[ChildElementInfo(typeof(EndConnection))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StartConnection))]
	[ChildElementInfo(typeof(ConnectionShapeLocks))]
	internal class NonVisualConnectorShapeDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007279 RID: 29305
		// (get) Token: 0x06015EBB RID: 89787 RVA: 0x002FC20C File Offset: 0x002FA40C
		public override string LocalName
		{
			get
			{
				return "cNvCxnSpPr";
			}
		}

		// Token: 0x1700727A RID: 29306
		// (get) Token: 0x06015EBC RID: 89788 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700727B RID: 29307
		// (get) Token: 0x06015EBD RID: 89789 RVA: 0x00324920 File Offset: 0x00322B20
		internal override int ElementTypeId
		{
			get
			{
				return 12270;
			}
		}

		// Token: 0x06015EBE RID: 89790 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015EBF RID: 89791 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualConnectorShapeDrawingProperties()
		{
		}

		// Token: 0x06015EC0 RID: 89792 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualConnectorShapeDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015EC1 RID: 89793 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualConnectorShapeDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015EC2 RID: 89794 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualConnectorShapeDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015EC3 RID: 89795 RVA: 0x00324928 File Offset: 0x00322B28
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

		// Token: 0x1700727C RID: 29308
		// (get) Token: 0x06015EC4 RID: 89796 RVA: 0x00324996 File Offset: 0x00322B96
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualConnectorShapeDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x1700727D RID: 29309
		// (get) Token: 0x06015EC5 RID: 89797 RVA: 0x0032499D File Offset: 0x00322B9D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualConnectorShapeDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700727E RID: 29310
		// (get) Token: 0x06015EC6 RID: 89798 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700727F RID: 29311
		// (get) Token: 0x06015EC7 RID: 89799 RVA: 0x002F02F8 File Offset: 0x002EE4F8
		// (set) Token: 0x06015EC8 RID: 89800 RVA: 0x002F0301 File Offset: 0x002EE501
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

		// Token: 0x17007280 RID: 29312
		// (get) Token: 0x06015EC9 RID: 89801 RVA: 0x002F030B File Offset: 0x002EE50B
		// (set) Token: 0x06015ECA RID: 89802 RVA: 0x002F0314 File Offset: 0x002EE514
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

		// Token: 0x17007281 RID: 29313
		// (get) Token: 0x06015ECB RID: 89803 RVA: 0x002F031E File Offset: 0x002EE51E
		// (set) Token: 0x06015ECC RID: 89804 RVA: 0x002F0327 File Offset: 0x002EE527
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

		// Token: 0x17007282 RID: 29314
		// (get) Token: 0x06015ECD RID: 89805 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06015ECE RID: 89806 RVA: 0x002E0C32 File Offset: 0x002DEE32
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

		// Token: 0x06015ECF RID: 89807 RVA: 0x003249A4 File Offset: 0x00322BA4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualConnectorShapeDrawingProperties>(deep);
		}

		// Token: 0x0400956C RID: 38252
		private const string tagName = "cNvCxnSpPr";

		// Token: 0x0400956D RID: 38253
		private const byte tagNsId = 24;

		// Token: 0x0400956E RID: 38254
		internal const int ElementTypeIdConst = 12270;

		// Token: 0x0400956F RID: 38255
		private static readonly string[] eleTagNames = new string[] { "cxnSpLocks", "stCxn", "endCxn", "extLst" };

		// Token: 0x04009570 RID: 38256
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
