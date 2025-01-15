using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200241D RID: 9245
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(Font))]
	[ChildElementInfo(typeof(Fill))]
	[ChildElementInfo(typeof(Alignment))]
	[ChildElementInfo(typeof(Border))]
	[ChildElementInfo(typeof(Protection))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DifferentialType : OpenXmlCompositeElement
	{
		// Token: 0x17004F40 RID: 20288
		// (get) Token: 0x06010F33 RID: 69427 RVA: 0x002E8FB3 File Offset: 0x002E71B3
		public override string LocalName
		{
			get
			{
				return "dxf";
			}
		}

		// Token: 0x17004F41 RID: 20289
		// (get) Token: 0x06010F34 RID: 69428 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F42 RID: 20290
		// (get) Token: 0x06010F35 RID: 69429 RVA: 0x002E8FBA File Offset: 0x002E71BA
		internal override int ElementTypeId
		{
			get
			{
				return 12963;
			}
		}

		// Token: 0x06010F36 RID: 69430 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010F37 RID: 69431 RVA: 0x00293ECF File Offset: 0x002920CF
		public DifferentialType()
		{
		}

		// Token: 0x06010F38 RID: 69432 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DifferentialType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F39 RID: 69433 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DifferentialType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F3A RID: 69434 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DifferentialType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010F3B RID: 69435 RVA: 0x002E8FC4 File Offset: 0x002E71C4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "font" == name)
			{
				return new Font();
			}
			if (22 == namespaceId && "numFmt" == name)
			{
				return new NumberingFormat();
			}
			if (22 == namespaceId && "fill" == name)
			{
				return new Fill();
			}
			if (22 == namespaceId && "alignment" == name)
			{
				return new Alignment();
			}
			if (22 == namespaceId && "border" == name)
			{
				return new Border();
			}
			if (22 == namespaceId && "protection" == name)
			{
				return new Protection();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004F43 RID: 20291
		// (get) Token: 0x06010F3C RID: 69436 RVA: 0x002E907A File Offset: 0x002E727A
		internal override string[] ElementTagNames
		{
			get
			{
				return DifferentialType.eleTagNames;
			}
		}

		// Token: 0x17004F44 RID: 20292
		// (get) Token: 0x06010F3D RID: 69437 RVA: 0x002E9081 File Offset: 0x002E7281
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DifferentialType.eleNamespaceIds;
			}
		}

		// Token: 0x17004F45 RID: 20293
		// (get) Token: 0x06010F3E RID: 69438 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004F46 RID: 20294
		// (get) Token: 0x06010F3F RID: 69439 RVA: 0x002E9088 File Offset: 0x002E7288
		// (set) Token: 0x06010F40 RID: 69440 RVA: 0x002E9091 File Offset: 0x002E7291
		public Font Font
		{
			get
			{
				return base.GetElement<Font>(0);
			}
			set
			{
				base.SetElement<Font>(0, value);
			}
		}

		// Token: 0x17004F47 RID: 20295
		// (get) Token: 0x06010F41 RID: 69441 RVA: 0x002E909B File Offset: 0x002E729B
		// (set) Token: 0x06010F42 RID: 69442 RVA: 0x002E90A4 File Offset: 0x002E72A4
		public NumberingFormat NumberingFormat
		{
			get
			{
				return base.GetElement<NumberingFormat>(1);
			}
			set
			{
				base.SetElement<NumberingFormat>(1, value);
			}
		}

		// Token: 0x17004F48 RID: 20296
		// (get) Token: 0x06010F43 RID: 69443 RVA: 0x002E90AE File Offset: 0x002E72AE
		// (set) Token: 0x06010F44 RID: 69444 RVA: 0x002E90B7 File Offset: 0x002E72B7
		public Fill Fill
		{
			get
			{
				return base.GetElement<Fill>(2);
			}
			set
			{
				base.SetElement<Fill>(2, value);
			}
		}

		// Token: 0x17004F49 RID: 20297
		// (get) Token: 0x06010F45 RID: 69445 RVA: 0x002E90C1 File Offset: 0x002E72C1
		// (set) Token: 0x06010F46 RID: 69446 RVA: 0x002E90CA File Offset: 0x002E72CA
		public Alignment Alignment
		{
			get
			{
				return base.GetElement<Alignment>(3);
			}
			set
			{
				base.SetElement<Alignment>(3, value);
			}
		}

		// Token: 0x17004F4A RID: 20298
		// (get) Token: 0x06010F47 RID: 69447 RVA: 0x002E90D4 File Offset: 0x002E72D4
		// (set) Token: 0x06010F48 RID: 69448 RVA: 0x002E90DD File Offset: 0x002E72DD
		public Border Border
		{
			get
			{
				return base.GetElement<Border>(4);
			}
			set
			{
				base.SetElement<Border>(4, value);
			}
		}

		// Token: 0x17004F4B RID: 20299
		// (get) Token: 0x06010F49 RID: 69449 RVA: 0x002E90E7 File Offset: 0x002E72E7
		// (set) Token: 0x06010F4A RID: 69450 RVA: 0x002E90F0 File Offset: 0x002E72F0
		public Protection Protection
		{
			get
			{
				return base.GetElement<Protection>(5);
			}
			set
			{
				base.SetElement<Protection>(5, value);
			}
		}

		// Token: 0x17004F4C RID: 20300
		// (get) Token: 0x06010F4B RID: 69451 RVA: 0x002E90FA File Offset: 0x002E72FA
		// (set) Token: 0x06010F4C RID: 69452 RVA: 0x002E9103 File Offset: 0x002E7303
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(6);
			}
			set
			{
				base.SetElement<ExtensionList>(6, value);
			}
		}

		// Token: 0x06010F4D RID: 69453 RVA: 0x002E910D File Offset: 0x002E730D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DifferentialType>(deep);
		}

		// Token: 0x04007704 RID: 30468
		private const string tagName = "dxf";

		// Token: 0x04007705 RID: 30469
		private const byte tagNsId = 53;

		// Token: 0x04007706 RID: 30470
		internal const int ElementTypeIdConst = 12963;

		// Token: 0x04007707 RID: 30471
		private static readonly string[] eleTagNames = new string[] { "font", "numFmt", "fill", "alignment", "border", "protection", "extLst" };

		// Token: 0x04007708 RID: 30472
		private static readonly byte[] eleNamespaceIds = new byte[] { 22, 22, 22, 22, 22, 22, 22 };
	}
}
