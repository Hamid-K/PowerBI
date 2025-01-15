using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002797 RID: 10135
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class GraphicFrameLocks : OpenXmlCompositeElement
	{
		// Token: 0x1700622D RID: 25133
		// (get) Token: 0x06013997 RID: 80279 RVA: 0x00308AE6 File Offset: 0x00306CE6
		public override string LocalName
		{
			get
			{
				return "graphicFrameLocks";
			}
		}

		// Token: 0x1700622E RID: 25134
		// (get) Token: 0x06013998 RID: 80280 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700622F RID: 25135
		// (get) Token: 0x06013999 RID: 80281 RVA: 0x00308AED File Offset: 0x00306CED
		internal override int ElementTypeId
		{
			get
			{
				return 10171;
			}
		}

		// Token: 0x0601399A RID: 80282 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006230 RID: 25136
		// (get) Token: 0x0601399B RID: 80283 RVA: 0x00308AF4 File Offset: 0x00306CF4
		internal override string[] AttributeTagNames
		{
			get
			{
				return GraphicFrameLocks.attributeTagNames;
			}
		}

		// Token: 0x17006231 RID: 25137
		// (get) Token: 0x0601399C RID: 80284 RVA: 0x00308AFB File Offset: 0x00306CFB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GraphicFrameLocks.attributeNamespaceIds;
			}
		}

		// Token: 0x17006232 RID: 25138
		// (get) Token: 0x0601399D RID: 80285 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601399E RID: 80286 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "noGrp")]
		public BooleanValue NoGrouping
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006233 RID: 25139
		// (get) Token: 0x0601399F RID: 80287 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060139A0 RID: 80288 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "noDrilldown")]
		public BooleanValue NoDrilldown
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006234 RID: 25140
		// (get) Token: 0x060139A1 RID: 80289 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060139A2 RID: 80290 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "noSelect")]
		public BooleanValue NoSelection
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17006235 RID: 25141
		// (get) Token: 0x060139A3 RID: 80291 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060139A4 RID: 80292 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "noChangeAspect")]
		public BooleanValue NoChangeAspect
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006236 RID: 25142
		// (get) Token: 0x060139A5 RID: 80293 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x060139A6 RID: 80294 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "noMove")]
		public BooleanValue NoMove
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17006237 RID: 25143
		// (get) Token: 0x060139A7 RID: 80295 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060139A8 RID: 80296 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "noResize")]
		public BooleanValue NoResize
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x060139A9 RID: 80297 RVA: 0x00293ECF File Offset: 0x002920CF
		public GraphicFrameLocks()
		{
		}

		// Token: 0x060139AA RID: 80298 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GraphicFrameLocks(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139AB RID: 80299 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GraphicFrameLocks(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139AC RID: 80300 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GraphicFrameLocks(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060139AD RID: 80301 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006238 RID: 25144
		// (get) Token: 0x060139AE RID: 80302 RVA: 0x00308B02 File Offset: 0x00306D02
		internal override string[] ElementTagNames
		{
			get
			{
				return GraphicFrameLocks.eleTagNames;
			}
		}

		// Token: 0x17006239 RID: 25145
		// (get) Token: 0x060139AF RID: 80303 RVA: 0x00308B09 File Offset: 0x00306D09
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GraphicFrameLocks.eleNamespaceIds;
			}
		}

		// Token: 0x1700623A RID: 25146
		// (get) Token: 0x060139B0 RID: 80304 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700623B RID: 25147
		// (get) Token: 0x060139B1 RID: 80305 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x060139B2 RID: 80306 RVA: 0x002FA750 File Offset: 0x002F8950
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x060139B3 RID: 80307 RVA: 0x00308B10 File Offset: 0x00306D10
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "noGrp" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noDrilldown" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noSelect" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noChangeAspect" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noMove" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noResize" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060139B4 RID: 80308 RVA: 0x00308BA9 File Offset: 0x00306DA9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GraphicFrameLocks>(deep);
		}

		// Token: 0x060139B5 RID: 80309 RVA: 0x00308BB4 File Offset: 0x00306DB4
		// Note: this type is marked as 'beforefieldinit'.
		static GraphicFrameLocks()
		{
			byte[] array = new byte[6];
			GraphicFrameLocks.attributeNamespaceIds = array;
			GraphicFrameLocks.eleTagNames = new string[] { "extLst" };
			GraphicFrameLocks.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x040086EA RID: 34538
		private const string tagName = "graphicFrameLocks";

		// Token: 0x040086EB RID: 34539
		private const byte tagNsId = 10;

		// Token: 0x040086EC RID: 34540
		internal const int ElementTypeIdConst = 10171;

		// Token: 0x040086ED RID: 34541
		private static string[] attributeTagNames = new string[] { "noGrp", "noDrilldown", "noSelect", "noChangeAspect", "noMove", "noResize" };

		// Token: 0x040086EE RID: 34542
		private static byte[] attributeNamespaceIds;

		// Token: 0x040086EF RID: 34543
		private static readonly string[] eleTagNames;

		// Token: 0x040086F0 RID: 34544
		private static readonly byte[] eleNamespaceIds;
	}
}
