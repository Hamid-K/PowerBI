using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BC5 RID: 11205
	[ChildElementInfo(typeof(Font))]
	[ChildElementInfo(typeof(Fill))]
	[ChildElementInfo(typeof(Alignment))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NumberingFormat))]
	[ChildElementInfo(typeof(Border))]
	[ChildElementInfo(typeof(Protection))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal abstract class DifferentialFormatType : OpenXmlCompositeElement
	{
		// Token: 0x0601756B RID: 95595 RVA: 0x00335988 File Offset: 0x00333B88
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

		// Token: 0x17007CA3 RID: 31907
		// (get) Token: 0x0601756C RID: 95596 RVA: 0x00335A3E File Offset: 0x00333C3E
		internal override string[] ElementTagNames
		{
			get
			{
				return DifferentialFormatType.eleTagNames;
			}
		}

		// Token: 0x17007CA4 RID: 31908
		// (get) Token: 0x0601756D RID: 95597 RVA: 0x00335A45 File Offset: 0x00333C45
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DifferentialFormatType.eleNamespaceIds;
			}
		}

		// Token: 0x17007CA5 RID: 31909
		// (get) Token: 0x0601756E RID: 95598 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007CA6 RID: 31910
		// (get) Token: 0x0601756F RID: 95599 RVA: 0x002E9088 File Offset: 0x002E7288
		// (set) Token: 0x06017570 RID: 95600 RVA: 0x002E9091 File Offset: 0x002E7291
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

		// Token: 0x17007CA7 RID: 31911
		// (get) Token: 0x06017571 RID: 95601 RVA: 0x002E909B File Offset: 0x002E729B
		// (set) Token: 0x06017572 RID: 95602 RVA: 0x002E90A4 File Offset: 0x002E72A4
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

		// Token: 0x17007CA8 RID: 31912
		// (get) Token: 0x06017573 RID: 95603 RVA: 0x002E90AE File Offset: 0x002E72AE
		// (set) Token: 0x06017574 RID: 95604 RVA: 0x002E90B7 File Offset: 0x002E72B7
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

		// Token: 0x17007CA9 RID: 31913
		// (get) Token: 0x06017575 RID: 95605 RVA: 0x002E90C1 File Offset: 0x002E72C1
		// (set) Token: 0x06017576 RID: 95606 RVA: 0x002E90CA File Offset: 0x002E72CA
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

		// Token: 0x17007CAA RID: 31914
		// (get) Token: 0x06017577 RID: 95607 RVA: 0x002E90D4 File Offset: 0x002E72D4
		// (set) Token: 0x06017578 RID: 95608 RVA: 0x002E90DD File Offset: 0x002E72DD
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

		// Token: 0x17007CAB RID: 31915
		// (get) Token: 0x06017579 RID: 95609 RVA: 0x002E90E7 File Offset: 0x002E72E7
		// (set) Token: 0x0601757A RID: 95610 RVA: 0x002E90F0 File Offset: 0x002E72F0
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

		// Token: 0x17007CAC RID: 31916
		// (get) Token: 0x0601757B RID: 95611 RVA: 0x002E90FA File Offset: 0x002E72FA
		// (set) Token: 0x0601757C RID: 95612 RVA: 0x002E9103 File Offset: 0x002E7303
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

		// Token: 0x0601757D RID: 95613 RVA: 0x00293ECF File Offset: 0x002920CF
		protected DifferentialFormatType()
		{
		}

		// Token: 0x0601757E RID: 95614 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected DifferentialFormatType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601757F RID: 95615 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected DifferentialFormatType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017580 RID: 95616 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected DifferentialFormatType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04009BFF RID: 39935
		private static readonly string[] eleTagNames = new string[] { "font", "numFmt", "fill", "alignment", "border", "protection", "extLst" };

		// Token: 0x04009C00 RID: 39936
		private static readonly byte[] eleNamespaceIds = new byte[] { 22, 22, 22, 22, 22, 22, 22 };
	}
}
