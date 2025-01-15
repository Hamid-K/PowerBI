using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002676 RID: 9846
	[ChildElementInfo(typeof(Background))]
	[ChildElementInfo(typeof(PointList))]
	[ChildElementInfo(typeof(ConnectionList))]
	[ChildElementInfo(typeof(Whole))]
	[ChildElementInfo(typeof(DataModelExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataModel : OpenXmlCompositeElement
	{
		// Token: 0x17005C64 RID: 23652
		// (get) Token: 0x06012CC0 RID: 76992 RVA: 0x002FD09D File Offset: 0x002FB29D
		public override string LocalName
		{
			get
			{
				return "dataModel";
			}
		}

		// Token: 0x17005C65 RID: 23653
		// (get) Token: 0x06012CC1 RID: 76993 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C66 RID: 23654
		// (get) Token: 0x06012CC2 RID: 76994 RVA: 0x002FF983 File Offset: 0x002FDB83
		internal override int ElementTypeId
		{
			get
			{
				return 10661;
			}
		}

		// Token: 0x06012CC3 RID: 76995 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012CC4 RID: 76996 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataModel()
		{
		}

		// Token: 0x06012CC5 RID: 76997 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataModel(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012CC6 RID: 76998 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataModel(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012CC7 RID: 76999 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataModel(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012CC8 RID: 77000 RVA: 0x002FF98C File Offset: 0x002FDB8C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "ptLst" == name)
			{
				return new PointList();
			}
			if (14 == namespaceId && "cxnLst" == name)
			{
				return new ConnectionList();
			}
			if (14 == namespaceId && "bg" == name)
			{
				return new Background();
			}
			if (14 == namespaceId && "whole" == name)
			{
				return new Whole();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new DataModelExtensionList();
			}
			return null;
		}

		// Token: 0x17005C67 RID: 23655
		// (get) Token: 0x06012CC9 RID: 77001 RVA: 0x002FFA12 File Offset: 0x002FDC12
		internal override string[] ElementTagNames
		{
			get
			{
				return DataModel.eleTagNames;
			}
		}

		// Token: 0x17005C68 RID: 23656
		// (get) Token: 0x06012CCA RID: 77002 RVA: 0x002FFA19 File Offset: 0x002FDC19
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataModel.eleNamespaceIds;
			}
		}

		// Token: 0x17005C69 RID: 23657
		// (get) Token: 0x06012CCB RID: 77003 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005C6A RID: 23658
		// (get) Token: 0x06012CCC RID: 77004 RVA: 0x002FD14C File Offset: 0x002FB34C
		// (set) Token: 0x06012CCD RID: 77005 RVA: 0x002FD155 File Offset: 0x002FB355
		public PointList PointList
		{
			get
			{
				return base.GetElement<PointList>(0);
			}
			set
			{
				base.SetElement<PointList>(0, value);
			}
		}

		// Token: 0x17005C6B RID: 23659
		// (get) Token: 0x06012CCE RID: 77006 RVA: 0x002FD15F File Offset: 0x002FB35F
		// (set) Token: 0x06012CCF RID: 77007 RVA: 0x002FD168 File Offset: 0x002FB368
		public ConnectionList ConnectionList
		{
			get
			{
				return base.GetElement<ConnectionList>(1);
			}
			set
			{
				base.SetElement<ConnectionList>(1, value);
			}
		}

		// Token: 0x17005C6C RID: 23660
		// (get) Token: 0x06012CD0 RID: 77008 RVA: 0x002FD172 File Offset: 0x002FB372
		// (set) Token: 0x06012CD1 RID: 77009 RVA: 0x002FD17B File Offset: 0x002FB37B
		public Background Background
		{
			get
			{
				return base.GetElement<Background>(2);
			}
			set
			{
				base.SetElement<Background>(2, value);
			}
		}

		// Token: 0x17005C6D RID: 23661
		// (get) Token: 0x06012CD2 RID: 77010 RVA: 0x002FD185 File Offset: 0x002FB385
		// (set) Token: 0x06012CD3 RID: 77011 RVA: 0x002FD18E File Offset: 0x002FB38E
		public Whole Whole
		{
			get
			{
				return base.GetElement<Whole>(3);
			}
			set
			{
				base.SetElement<Whole>(3, value);
			}
		}

		// Token: 0x17005C6E RID: 23662
		// (get) Token: 0x06012CD4 RID: 77012 RVA: 0x002FFA20 File Offset: 0x002FDC20
		// (set) Token: 0x06012CD5 RID: 77013 RVA: 0x002FFA29 File Offset: 0x002FDC29
		public DataModelExtensionList DataModelExtensionList
		{
			get
			{
				return base.GetElement<DataModelExtensionList>(4);
			}
			set
			{
				base.SetElement<DataModelExtensionList>(4, value);
			}
		}

		// Token: 0x06012CD6 RID: 77014 RVA: 0x002FFA33 File Offset: 0x002FDC33
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataModel>(deep);
		}

		// Token: 0x0400819D RID: 33181
		private const string tagName = "dataModel";

		// Token: 0x0400819E RID: 33182
		private const byte tagNsId = 14;

		// Token: 0x0400819F RID: 33183
		internal const int ElementTypeIdConst = 10661;

		// Token: 0x040081A0 RID: 33184
		private static readonly string[] eleTagNames = new string[] { "ptLst", "cxnLst", "bg", "whole", "extLst" };

		// Token: 0x040081A1 RID: 33185
		private static readonly byte[] eleNamespaceIds = new byte[] { 14, 14, 14, 14, 14 };
	}
}
