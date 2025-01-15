using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002595 RID: 9621
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(Symbol))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Size))]
	internal class Marker : OpenXmlCompositeElement
	{
		// Token: 0x17005697 RID: 22167
		// (get) Token: 0x06011FAF RID: 73647 RVA: 0x002F13F2 File Offset: 0x002EF5F2
		public override string LocalName
		{
			get
			{
				return "marker";
			}
		}

		// Token: 0x17005698 RID: 22168
		// (get) Token: 0x06011FB0 RID: 73648 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005699 RID: 22169
		// (get) Token: 0x06011FB1 RID: 73649 RVA: 0x002F4663 File Offset: 0x002F2863
		internal override int ElementTypeId
		{
			get
			{
				return 10432;
			}
		}

		// Token: 0x06011FB2 RID: 73650 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011FB3 RID: 73651 RVA: 0x00293ECF File Offset: 0x002920CF
		public Marker()
		{
		}

		// Token: 0x06011FB4 RID: 73652 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Marker(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011FB5 RID: 73653 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Marker(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011FB6 RID: 73654 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Marker(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011FB7 RID: 73655 RVA: 0x002F466C File Offset: 0x002F286C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "symbol" == name)
			{
				return new Symbol();
			}
			if (11 == namespaceId && "size" == name)
			{
				return new Size();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700569A RID: 22170
		// (get) Token: 0x06011FB8 RID: 73656 RVA: 0x002F46DA File Offset: 0x002F28DA
		internal override string[] ElementTagNames
		{
			get
			{
				return Marker.eleTagNames;
			}
		}

		// Token: 0x1700569B RID: 22171
		// (get) Token: 0x06011FB9 RID: 73657 RVA: 0x002F46E1 File Offset: 0x002F28E1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Marker.eleNamespaceIds;
			}
		}

		// Token: 0x1700569C RID: 22172
		// (get) Token: 0x06011FBA RID: 73658 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700569D RID: 22173
		// (get) Token: 0x06011FBB RID: 73659 RVA: 0x002F46E8 File Offset: 0x002F28E8
		// (set) Token: 0x06011FBC RID: 73660 RVA: 0x002F46F1 File Offset: 0x002F28F1
		public Symbol Symbol
		{
			get
			{
				return base.GetElement<Symbol>(0);
			}
			set
			{
				base.SetElement<Symbol>(0, value);
			}
		}

		// Token: 0x1700569E RID: 22174
		// (get) Token: 0x06011FBD RID: 73661 RVA: 0x002F46FB File Offset: 0x002F28FB
		// (set) Token: 0x06011FBE RID: 73662 RVA: 0x002F4704 File Offset: 0x002F2904
		public Size Size
		{
			get
			{
				return base.GetElement<Size>(1);
			}
			set
			{
				base.SetElement<Size>(1, value);
			}
		}

		// Token: 0x1700569F RID: 22175
		// (get) Token: 0x06011FBF RID: 73663 RVA: 0x002F470E File Offset: 0x002F290E
		// (set) Token: 0x06011FC0 RID: 73664 RVA: 0x002F4717 File Offset: 0x002F2917
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(2);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(2, value);
			}
		}

		// Token: 0x170056A0 RID: 22176
		// (get) Token: 0x06011FC1 RID: 73665 RVA: 0x002F4721 File Offset: 0x002F2921
		// (set) Token: 0x06011FC2 RID: 73666 RVA: 0x002F472A File Offset: 0x002F292A
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

		// Token: 0x06011FC3 RID: 73667 RVA: 0x002F4734 File Offset: 0x002F2934
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Marker>(deep);
		}

		// Token: 0x04007D93 RID: 32147
		private const string tagName = "marker";

		// Token: 0x04007D94 RID: 32148
		private const byte tagNsId = 11;

		// Token: 0x04007D95 RID: 32149
		internal const int ElementTypeIdConst = 10432;

		// Token: 0x04007D96 RID: 32150
		private static readonly string[] eleTagNames = new string[] { "symbol", "size", "spPr", "extLst" };

		// Token: 0x04007D97 RID: 32151
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11 };
	}
}
