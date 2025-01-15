using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002839 RID: 10297
	[ChildElementInfo(typeof(NonVisualConnectionShapeProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(ShapeStyle))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class ConnectionShape : OpenXmlCompositeElement
	{
		// Token: 0x1700663C RID: 26172
		// (get) Token: 0x06014312 RID: 82706 RVA: 0x002FB89A File Offset: 0x002F9A9A
		public override string LocalName
		{
			get
			{
				return "cxnSp";
			}
		}

		// Token: 0x1700663D RID: 26173
		// (get) Token: 0x06014313 RID: 82707 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700663E RID: 26174
		// (get) Token: 0x06014314 RID: 82708 RVA: 0x00310368 File Offset: 0x0030E568
		internal override int ElementTypeId
		{
			get
			{
				return 10333;
			}
		}

		// Token: 0x06014315 RID: 82709 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014316 RID: 82710 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConnectionShape()
		{
		}

		// Token: 0x06014317 RID: 82711 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConnectionShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014318 RID: 82712 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConnectionShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014319 RID: 82713 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConnectionShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601431A RID: 82714 RVA: 0x00310370 File Offset: 0x0030E570
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "nvCxnSpPr" == name)
			{
				return new NonVisualConnectionShapeProperties();
			}
			if (10 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (10 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700663F RID: 26175
		// (get) Token: 0x0601431B RID: 82715 RVA: 0x003103DE File Offset: 0x0030E5DE
		internal override string[] ElementTagNames
		{
			get
			{
				return ConnectionShape.eleTagNames;
			}
		}

		// Token: 0x17006640 RID: 26176
		// (get) Token: 0x0601431C RID: 82716 RVA: 0x003103E5 File Offset: 0x0030E5E5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ConnectionShape.eleNamespaceIds;
			}
		}

		// Token: 0x17006641 RID: 26177
		// (get) Token: 0x0601431D RID: 82717 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006642 RID: 26178
		// (get) Token: 0x0601431E RID: 82718 RVA: 0x003103EC File Offset: 0x0030E5EC
		// (set) Token: 0x0601431F RID: 82719 RVA: 0x003103F5 File Offset: 0x0030E5F5
		public NonVisualConnectionShapeProperties NonVisualConnectionShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualConnectionShapeProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualConnectionShapeProperties>(0, value);
			}
		}

		// Token: 0x17006643 RID: 26179
		// (get) Token: 0x06014320 RID: 82720 RVA: 0x003102DF File Offset: 0x0030E4DF
		// (set) Token: 0x06014321 RID: 82721 RVA: 0x003102E8 File Offset: 0x0030E4E8
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(1);
			}
			set
			{
				base.SetElement<ShapeProperties>(1, value);
			}
		}

		// Token: 0x17006644 RID: 26180
		// (get) Token: 0x06014322 RID: 82722 RVA: 0x003103FF File Offset: 0x0030E5FF
		// (set) Token: 0x06014323 RID: 82723 RVA: 0x00310408 File Offset: 0x0030E608
		public ShapeStyle ShapeStyle
		{
			get
			{
				return base.GetElement<ShapeStyle>(2);
			}
			set
			{
				base.SetElement<ShapeStyle>(2, value);
			}
		}

		// Token: 0x17006645 RID: 26181
		// (get) Token: 0x06014324 RID: 82724 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06014325 RID: 82725 RVA: 0x002E0C32 File Offset: 0x002DEE32
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

		// Token: 0x06014326 RID: 82726 RVA: 0x00310412 File Offset: 0x0030E612
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectionShape>(deep);
		}

		// Token: 0x04008975 RID: 35189
		private const string tagName = "cxnSp";

		// Token: 0x04008976 RID: 35190
		private const byte tagNsId = 10;

		// Token: 0x04008977 RID: 35191
		internal const int ElementTypeIdConst = 10333;

		// Token: 0x04008978 RID: 35192
		private static readonly string[] eleTagNames = new string[] { "nvCxnSpPr", "spPr", "style", "extLst" };

		// Token: 0x04008979 RID: 35193
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
