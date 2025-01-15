using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002798 RID: 10136
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HyperlinkSound))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal abstract class HyperlinkType : OpenXmlCompositeElement
	{
		// Token: 0x1700623C RID: 25148
		// (get) Token: 0x060139B6 RID: 80310 RVA: 0x00308C32 File Offset: 0x00306E32
		internal override string[] AttributeTagNames
		{
			get
			{
				return HyperlinkType.attributeTagNames;
			}
		}

		// Token: 0x1700623D RID: 25149
		// (get) Token: 0x060139B7 RID: 80311 RVA: 0x00308C39 File Offset: 0x00306E39
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HyperlinkType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700623E RID: 25150
		// (get) Token: 0x060139B8 RID: 80312 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060139B9 RID: 80313 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x1700623F RID: 25151
		// (get) Token: 0x060139BA RID: 80314 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060139BB RID: 80315 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "invalidUrl")]
		public StringValue InvalidUrl
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006240 RID: 25152
		// (get) Token: 0x060139BC RID: 80316 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060139BD RID: 80317 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "action")]
		public StringValue Action
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17006241 RID: 25153
		// (get) Token: 0x060139BE RID: 80318 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x060139BF RID: 80319 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "tgtFrame")]
		public StringValue TargetFrame
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17006242 RID: 25154
		// (get) Token: 0x060139C0 RID: 80320 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060139C1 RID: 80321 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "tooltip")]
		public StringValue Tooltip
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17006243 RID: 25155
		// (get) Token: 0x060139C2 RID: 80322 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060139C3 RID: 80323 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "history")]
		public BooleanValue History
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

		// Token: 0x17006244 RID: 25156
		// (get) Token: 0x060139C4 RID: 80324 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060139C5 RID: 80325 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "highlightClick")]
		public BooleanValue HighlightClick
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17006245 RID: 25157
		// (get) Token: 0x060139C6 RID: 80326 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060139C7 RID: 80327 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "endSnd")]
		public BooleanValue EndSound
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x060139C8 RID: 80328 RVA: 0x00308C40 File Offset: 0x00306E40
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "snd" == name)
			{
				return new HyperlinkSound();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006246 RID: 25158
		// (get) Token: 0x060139C9 RID: 80329 RVA: 0x00308C73 File Offset: 0x00306E73
		internal override string[] ElementTagNames
		{
			get
			{
				return HyperlinkType.eleTagNames;
			}
		}

		// Token: 0x17006247 RID: 25159
		// (get) Token: 0x060139CA RID: 80330 RVA: 0x00308C7A File Offset: 0x00306E7A
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return HyperlinkType.eleNamespaceIds;
			}
		}

		// Token: 0x17006248 RID: 25160
		// (get) Token: 0x060139CB RID: 80331 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006249 RID: 25161
		// (get) Token: 0x060139CC RID: 80332 RVA: 0x00308C81 File Offset: 0x00306E81
		// (set) Token: 0x060139CD RID: 80333 RVA: 0x00308C8A File Offset: 0x00306E8A
		public HyperlinkSound HyperlinkSound
		{
			get
			{
				return base.GetElement<HyperlinkSound>(0);
			}
			set
			{
				base.SetElement<HyperlinkSound>(0, value);
			}
		}

		// Token: 0x1700624A RID: 25162
		// (get) Token: 0x060139CE RID: 80334 RVA: 0x002DEB6A File Offset: 0x002DCD6A
		// (set) Token: 0x060139CF RID: 80335 RVA: 0x002DEB73 File Offset: 0x002DCD73
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

		// Token: 0x060139D0 RID: 80336 RVA: 0x00308C94 File Offset: 0x00306E94
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "invalidUrl" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "action" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tgtFrame" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tooltip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "history" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "highlightClick" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "endSnd" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060139D1 RID: 80337 RVA: 0x00293ECF File Offset: 0x002920CF
		protected HyperlinkType()
		{
		}

		// Token: 0x060139D2 RID: 80338 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected HyperlinkType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139D3 RID: 80339 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected HyperlinkType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060139D4 RID: 80340 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected HyperlinkType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060139D5 RID: 80341 RVA: 0x00308D5C File Offset: 0x00306F5C
		// Note: this type is marked as 'beforefieldinit'.
		static HyperlinkType()
		{
			byte[] array = new byte[8];
			array[0] = 19;
			HyperlinkType.attributeNamespaceIds = array;
			HyperlinkType.eleTagNames = new string[] { "snd", "extLst" };
			HyperlinkType.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x040086F1 RID: 34545
		private static string[] attributeTagNames = new string[] { "id", "invalidUrl", "action", "tgtFrame", "tooltip", "history", "highlightClick", "endSnd" };

		// Token: 0x040086F2 RID: 34546
		private static byte[] attributeNamespaceIds;

		// Token: 0x040086F3 RID: 34547
		private static readonly string[] eleTagNames;

		// Token: 0x040086F4 RID: 34548
		private static readonly byte[] eleNamespaceIds;
	}
}
