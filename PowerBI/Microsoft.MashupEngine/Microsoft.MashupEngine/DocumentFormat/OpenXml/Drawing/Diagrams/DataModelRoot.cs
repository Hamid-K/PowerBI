using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002649 RID: 9801
	[ChildElementInfo(typeof(Whole))]
	[ChildElementInfo(typeof(Background))]
	[ChildElementInfo(typeof(PointList))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(ConnectionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DataModelRoot : OpenXmlPartRootElement
	{
		// Token: 0x17005B0F RID: 23311
		// (get) Token: 0x06012992 RID: 76178 RVA: 0x002FD09D File Offset: 0x002FB29D
		public override string LocalName
		{
			get
			{
				return "dataModel";
			}
		}

		// Token: 0x17005B10 RID: 23312
		// (get) Token: 0x06012993 RID: 76179 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B11 RID: 23313
		// (get) Token: 0x06012994 RID: 76180 RVA: 0x002FD0A4 File Offset: 0x002FB2A4
		internal override int ElementTypeId
		{
			get
			{
				return 10619;
			}
		}

		// Token: 0x06012995 RID: 76181 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012996 RID: 76182 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal DataModelRoot(DiagramDataPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06012997 RID: 76183 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(DiagramDataPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17005B12 RID: 23314
		// (get) Token: 0x06012998 RID: 76184 RVA: 0x002FD0AB File Offset: 0x002FB2AB
		// (set) Token: 0x06012999 RID: 76185 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public DiagramDataPart DiagramDataPart
		{
			get
			{
				return base.OpenXmlPart as DiagramDataPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x0601299A RID: 76186 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public DataModelRoot(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601299B RID: 76187 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public DataModelRoot(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601299C RID: 76188 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public DataModelRoot(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601299D RID: 76189 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public DataModelRoot()
		{
		}

		// Token: 0x0601299E RID: 76190 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(DiagramDataPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601299F RID: 76191 RVA: 0x002FD0B8 File Offset: 0x002FB2B8
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
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005B13 RID: 23315
		// (get) Token: 0x060129A0 RID: 76192 RVA: 0x002FD13E File Offset: 0x002FB33E
		internal override string[] ElementTagNames
		{
			get
			{
				return DataModelRoot.eleTagNames;
			}
		}

		// Token: 0x17005B14 RID: 23316
		// (get) Token: 0x060129A1 RID: 76193 RVA: 0x002FD145 File Offset: 0x002FB345
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataModelRoot.eleNamespaceIds;
			}
		}

		// Token: 0x17005B15 RID: 23317
		// (get) Token: 0x060129A2 RID: 76194 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005B16 RID: 23318
		// (get) Token: 0x060129A3 RID: 76195 RVA: 0x002FD14C File Offset: 0x002FB34C
		// (set) Token: 0x060129A4 RID: 76196 RVA: 0x002FD155 File Offset: 0x002FB355
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

		// Token: 0x17005B17 RID: 23319
		// (get) Token: 0x060129A5 RID: 76197 RVA: 0x002FD15F File Offset: 0x002FB35F
		// (set) Token: 0x060129A6 RID: 76198 RVA: 0x002FD168 File Offset: 0x002FB368
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

		// Token: 0x17005B18 RID: 23320
		// (get) Token: 0x060129A7 RID: 76199 RVA: 0x002FD172 File Offset: 0x002FB372
		// (set) Token: 0x060129A8 RID: 76200 RVA: 0x002FD17B File Offset: 0x002FB37B
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

		// Token: 0x17005B19 RID: 23321
		// (get) Token: 0x060129A9 RID: 76201 RVA: 0x002FD185 File Offset: 0x002FB385
		// (set) Token: 0x060129AA RID: 76202 RVA: 0x002FD18E File Offset: 0x002FB38E
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

		// Token: 0x17005B1A RID: 23322
		// (get) Token: 0x060129AB RID: 76203 RVA: 0x002FD198 File Offset: 0x002FB398
		// (set) Token: 0x060129AC RID: 76204 RVA: 0x002FD1A1 File Offset: 0x002FB3A1
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x060129AD RID: 76205 RVA: 0x002FD1AB File Offset: 0x002FB3AB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataModelRoot>(deep);
		}

		// Token: 0x040080D8 RID: 32984
		private const string tagName = "dataModel";

		// Token: 0x040080D9 RID: 32985
		private const byte tagNsId = 14;

		// Token: 0x040080DA RID: 32986
		internal const int ElementTypeIdConst = 10619;

		// Token: 0x040080DB RID: 32987
		private static readonly string[] eleTagNames = new string[] { "ptLst", "cxnLst", "bg", "whole", "extLst" };

		// Token: 0x040080DC RID: 32988
		private static readonly byte[] eleNamespaceIds = new byte[] { 14, 14, 14, 14, 14 };
	}
}
