using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AC5 RID: 10949
	[ChildElementInfo(typeof(BackgroundAnimation))]
	[ChildElementInfo(typeof(GraphicElement))]
	[ChildElementInfo(typeof(SubShape))]
	[ChildElementInfo(typeof(OleChartElement))]
	[ChildElementInfo(typeof(TextElement))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeTarget : OpenXmlCompositeElement
	{
		// Token: 0x1700752C RID: 29996
		// (get) Token: 0x060164EC RID: 91372 RVA: 0x00328D23 File Offset: 0x00326F23
		public override string LocalName
		{
			get
			{
				return "spTgt";
			}
		}

		// Token: 0x1700752D RID: 29997
		// (get) Token: 0x060164ED RID: 91373 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700752E RID: 29998
		// (get) Token: 0x060164EE RID: 91374 RVA: 0x00328D2A File Offset: 0x00326F2A
		internal override int ElementTypeId
		{
			get
			{
				return 12368;
			}
		}

		// Token: 0x060164EF RID: 91375 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700752F RID: 29999
		// (get) Token: 0x060164F0 RID: 91376 RVA: 0x00328D31 File Offset: 0x00326F31
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeTarget.attributeTagNames;
			}
		}

		// Token: 0x17007530 RID: 30000
		// (get) Token: 0x060164F1 RID: 91377 RVA: 0x00328D38 File Offset: 0x00326F38
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeTarget.attributeNamespaceIds;
			}
		}

		// Token: 0x17007531 RID: 30001
		// (get) Token: 0x060164F2 RID: 91378 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060164F3 RID: 91379 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spid")]
		public StringValue ShapeId
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060164F4 RID: 91380 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeTarget()
		{
		}

		// Token: 0x060164F5 RID: 91381 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeTarget(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164F6 RID: 91382 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeTarget(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164F7 RID: 91383 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeTarget(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060164F8 RID: 91384 RVA: 0x00328D40 File Offset: 0x00326F40
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "bg" == name)
			{
				return new BackgroundAnimation();
			}
			if (24 == namespaceId && "subSp" == name)
			{
				return new SubShape();
			}
			if (24 == namespaceId && "oleChartEl" == name)
			{
				return new OleChartElement();
			}
			if (24 == namespaceId && "txEl" == name)
			{
				return new TextElement();
			}
			if (24 == namespaceId && "graphicEl" == name)
			{
				return new GraphicElement();
			}
			return null;
		}

		// Token: 0x17007532 RID: 30002
		// (get) Token: 0x060164F9 RID: 91385 RVA: 0x00328DC6 File Offset: 0x00326FC6
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeTarget.eleTagNames;
			}
		}

		// Token: 0x17007533 RID: 30003
		// (get) Token: 0x060164FA RID: 91386 RVA: 0x00328DCD File Offset: 0x00326FCD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeTarget.eleNamespaceIds;
			}
		}

		// Token: 0x17007534 RID: 30004
		// (get) Token: 0x060164FB RID: 91387 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007535 RID: 30005
		// (get) Token: 0x060164FC RID: 91388 RVA: 0x00328DD4 File Offset: 0x00326FD4
		// (set) Token: 0x060164FD RID: 91389 RVA: 0x00328DDD File Offset: 0x00326FDD
		public BackgroundAnimation BackgroundAnimation
		{
			get
			{
				return base.GetElement<BackgroundAnimation>(0);
			}
			set
			{
				base.SetElement<BackgroundAnimation>(0, value);
			}
		}

		// Token: 0x17007536 RID: 30006
		// (get) Token: 0x060164FE RID: 91390 RVA: 0x00328DE7 File Offset: 0x00326FE7
		// (set) Token: 0x060164FF RID: 91391 RVA: 0x00328DF0 File Offset: 0x00326FF0
		public SubShape SubShape
		{
			get
			{
				return base.GetElement<SubShape>(1);
			}
			set
			{
				base.SetElement<SubShape>(1, value);
			}
		}

		// Token: 0x17007537 RID: 30007
		// (get) Token: 0x06016500 RID: 91392 RVA: 0x00328DFA File Offset: 0x00326FFA
		// (set) Token: 0x06016501 RID: 91393 RVA: 0x00328E03 File Offset: 0x00327003
		public OleChartElement OleChartElement
		{
			get
			{
				return base.GetElement<OleChartElement>(2);
			}
			set
			{
				base.SetElement<OleChartElement>(2, value);
			}
		}

		// Token: 0x17007538 RID: 30008
		// (get) Token: 0x06016502 RID: 91394 RVA: 0x00328E0D File Offset: 0x0032700D
		// (set) Token: 0x06016503 RID: 91395 RVA: 0x00328E16 File Offset: 0x00327016
		public TextElement TextElement
		{
			get
			{
				return base.GetElement<TextElement>(3);
			}
			set
			{
				base.SetElement<TextElement>(3, value);
			}
		}

		// Token: 0x17007539 RID: 30009
		// (get) Token: 0x06016504 RID: 91396 RVA: 0x00328E20 File Offset: 0x00327020
		// (set) Token: 0x06016505 RID: 91397 RVA: 0x00328E29 File Offset: 0x00327029
		public GraphicElement GraphicElement
		{
			get
			{
				return base.GetElement<GraphicElement>(4);
			}
			set
			{
				base.SetElement<GraphicElement>(4, value);
			}
		}

		// Token: 0x06016506 RID: 91398 RVA: 0x002E015B File Offset: 0x002DE35B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spid" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016507 RID: 91399 RVA: 0x00328E33 File Offset: 0x00327033
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeTarget>(deep);
		}

		// Token: 0x06016508 RID: 91400 RVA: 0x00328E3C File Offset: 0x0032703C
		// Note: this type is marked as 'beforefieldinit'.
		static ShapeTarget()
		{
			byte[] array = new byte[1];
			ShapeTarget.attributeNamespaceIds = array;
			ShapeTarget.eleTagNames = new string[] { "bg", "subSp", "oleChartEl", "txEl", "graphicEl" };
			ShapeTarget.eleNamespaceIds = new byte[] { 24, 24, 24, 24, 24 };
		}

		// Token: 0x04009721 RID: 38689
		private const string tagName = "spTgt";

		// Token: 0x04009722 RID: 38690
		private const byte tagNsId = 24;

		// Token: 0x04009723 RID: 38691
		internal const int ElementTypeIdConst = 12368;

		// Token: 0x04009724 RID: 38692
		private static string[] attributeTagNames = new string[] { "spid" };

		// Token: 0x04009725 RID: 38693
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009726 RID: 38694
		private static readonly string[] eleTagNames;

		// Token: 0x04009727 RID: 38695
		private static readonly byte[] eleNamespaceIds;
	}
}
