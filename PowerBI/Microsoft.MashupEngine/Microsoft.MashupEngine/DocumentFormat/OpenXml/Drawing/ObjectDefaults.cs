using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027E9 RID: 10217
	[ChildElementInfo(typeof(ShapeDefault))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LineDefault))]
	[ChildElementInfo(typeof(TextDefault))]
	internal class ObjectDefaults : OpenXmlCompositeElement
	{
		// Token: 0x1700647C RID: 25724
		// (get) Token: 0x06013EB2 RID: 81586 RVA: 0x0030D29C File Offset: 0x0030B49C
		public override string LocalName
		{
			get
			{
				return "objectDefaults";
			}
		}

		// Token: 0x1700647D RID: 25725
		// (get) Token: 0x06013EB3 RID: 81587 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700647E RID: 25726
		// (get) Token: 0x06013EB4 RID: 81588 RVA: 0x0030D2A3 File Offset: 0x0030B4A3
		internal override int ElementTypeId
		{
			get
			{
				return 10249;
			}
		}

		// Token: 0x06013EB5 RID: 81589 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013EB6 RID: 81590 RVA: 0x00293ECF File Offset: 0x002920CF
		public ObjectDefaults()
		{
		}

		// Token: 0x06013EB7 RID: 81591 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ObjectDefaults(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013EB8 RID: 81592 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ObjectDefaults(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013EB9 RID: 81593 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ObjectDefaults(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013EBA RID: 81594 RVA: 0x0030D2AC File Offset: 0x0030B4AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "spDef" == name)
			{
				return new ShapeDefault();
			}
			if (10 == namespaceId && "lnDef" == name)
			{
				return new LineDefault();
			}
			if (10 == namespaceId && "txDef" == name)
			{
				return new TextDefault();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700647F RID: 25727
		// (get) Token: 0x06013EBB RID: 81595 RVA: 0x0030D31A File Offset: 0x0030B51A
		internal override string[] ElementTagNames
		{
			get
			{
				return ObjectDefaults.eleTagNames;
			}
		}

		// Token: 0x17006480 RID: 25728
		// (get) Token: 0x06013EBC RID: 81596 RVA: 0x0030D321 File Offset: 0x0030B521
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ObjectDefaults.eleNamespaceIds;
			}
		}

		// Token: 0x17006481 RID: 25729
		// (get) Token: 0x06013EBD RID: 81597 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006482 RID: 25730
		// (get) Token: 0x06013EBE RID: 81598 RVA: 0x0030D328 File Offset: 0x0030B528
		// (set) Token: 0x06013EBF RID: 81599 RVA: 0x0030D331 File Offset: 0x0030B531
		public ShapeDefault ShapeDefault
		{
			get
			{
				return base.GetElement<ShapeDefault>(0);
			}
			set
			{
				base.SetElement<ShapeDefault>(0, value);
			}
		}

		// Token: 0x17006483 RID: 25731
		// (get) Token: 0x06013EC0 RID: 81600 RVA: 0x0030D33B File Offset: 0x0030B53B
		// (set) Token: 0x06013EC1 RID: 81601 RVA: 0x0030D344 File Offset: 0x0030B544
		public LineDefault LineDefault
		{
			get
			{
				return base.GetElement<LineDefault>(1);
			}
			set
			{
				base.SetElement<LineDefault>(1, value);
			}
		}

		// Token: 0x17006484 RID: 25732
		// (get) Token: 0x06013EC2 RID: 81602 RVA: 0x0030D34E File Offset: 0x0030B54E
		// (set) Token: 0x06013EC3 RID: 81603 RVA: 0x0030D357 File Offset: 0x0030B557
		public TextDefault TextDefault
		{
			get
			{
				return base.GetElement<TextDefault>(2);
			}
			set
			{
				base.SetElement<TextDefault>(2, value);
			}
		}

		// Token: 0x17006485 RID: 25733
		// (get) Token: 0x06013EC4 RID: 81604 RVA: 0x002E0C29 File Offset: 0x002DEE29
		// (set) Token: 0x06013EC5 RID: 81605 RVA: 0x002E0C32 File Offset: 0x002DEE32
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

		// Token: 0x06013EC6 RID: 81606 RVA: 0x0030D361 File Offset: 0x0030B561
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ObjectDefaults>(deep);
		}

		// Token: 0x04008849 RID: 34889
		private const string tagName = "objectDefaults";

		// Token: 0x0400884A RID: 34890
		private const byte tagNsId = 10;

		// Token: 0x0400884B RID: 34891
		internal const int ElementTypeIdConst = 10249;

		// Token: 0x0400884C RID: 34892
		private static readonly string[] eleTagNames = new string[] { "spDef", "lnDef", "txDef", "extLst" };

		// Token: 0x0400884D RID: 34893
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
