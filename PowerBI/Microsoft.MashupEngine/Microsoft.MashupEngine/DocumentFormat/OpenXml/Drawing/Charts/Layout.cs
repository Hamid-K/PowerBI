using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002535 RID: 9525
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(ManualLayout))]
	internal class Layout : OpenXmlCompositeElement
	{
		// Token: 0x170054B8 RID: 21688
		// (get) Token: 0x06011B71 RID: 72561 RVA: 0x002AD00B File Offset: 0x002AB20B
		public override string LocalName
		{
			get
			{
				return "layout";
			}
		}

		// Token: 0x170054B9 RID: 21689
		// (get) Token: 0x06011B72 RID: 72562 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054BA RID: 21690
		// (get) Token: 0x06011B73 RID: 72563 RVA: 0x002F1624 File Offset: 0x002EF824
		internal override int ElementTypeId
		{
			get
			{
				return 10353;
			}
		}

		// Token: 0x06011B74 RID: 72564 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B75 RID: 72565 RVA: 0x00293ECF File Offset: 0x002920CF
		public Layout()
		{
		}

		// Token: 0x06011B76 RID: 72566 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Layout(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011B77 RID: 72567 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Layout(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011B78 RID: 72568 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Layout(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011B79 RID: 72569 RVA: 0x002F162B File Offset: 0x002EF82B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "manualLayout" == name)
			{
				return new ManualLayout();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170054BB RID: 21691
		// (get) Token: 0x06011B7A RID: 72570 RVA: 0x002F165E File Offset: 0x002EF85E
		internal override string[] ElementTagNames
		{
			get
			{
				return Layout.eleTagNames;
			}
		}

		// Token: 0x170054BC RID: 21692
		// (get) Token: 0x06011B7B RID: 72571 RVA: 0x002F1665 File Offset: 0x002EF865
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Layout.eleNamespaceIds;
			}
		}

		// Token: 0x170054BD RID: 21693
		// (get) Token: 0x06011B7C RID: 72572 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170054BE RID: 21694
		// (get) Token: 0x06011B7D RID: 72573 RVA: 0x002F166C File Offset: 0x002EF86C
		// (set) Token: 0x06011B7E RID: 72574 RVA: 0x002F1675 File Offset: 0x002EF875
		public ManualLayout ManualLayout
		{
			get
			{
				return base.GetElement<ManualLayout>(0);
			}
			set
			{
				base.SetElement<ManualLayout>(0, value);
			}
		}

		// Token: 0x170054BF RID: 21695
		// (get) Token: 0x06011B7F RID: 72575 RVA: 0x002F167F File Offset: 0x002EF87F
		// (set) Token: 0x06011B80 RID: 72576 RVA: 0x002F1688 File Offset: 0x002EF888
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06011B81 RID: 72577 RVA: 0x002F1692 File Offset: 0x002EF892
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Layout>(deep);
		}

		// Token: 0x04007C33 RID: 31795
		private const string tagName = "layout";

		// Token: 0x04007C34 RID: 31796
		private const byte tagNsId = 11;

		// Token: 0x04007C35 RID: 31797
		internal const int ElementTypeIdConst = 10353;

		// Token: 0x04007C36 RID: 31798
		private static readonly string[] eleTagNames = new string[] { "manualLayout", "extLst" };

		// Token: 0x04007C37 RID: 31799
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11 };
	}
}
