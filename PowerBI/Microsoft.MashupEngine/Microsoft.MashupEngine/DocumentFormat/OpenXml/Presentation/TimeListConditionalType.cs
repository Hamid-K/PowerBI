using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A12 RID: 10770
	[ChildElementInfo(typeof(TargetElement))]
	[ChildElementInfo(typeof(TimeNode))]
	[ChildElementInfo(typeof(RuntimeNodeTrigger))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TimeListConditionalType : OpenXmlCompositeElement
	{
		// Token: 0x17006FC5 RID: 28613
		// (get) Token: 0x060158CF RID: 88271 RVA: 0x0032077F File Offset: 0x0031E97F
		internal override string[] AttributeTagNames
		{
			get
			{
				return TimeListConditionalType.attributeTagNames;
			}
		}

		// Token: 0x17006FC6 RID: 28614
		// (get) Token: 0x060158D0 RID: 88272 RVA: 0x00320786 File Offset: 0x0031E986
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TimeListConditionalType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006FC7 RID: 28615
		// (get) Token: 0x060158D1 RID: 88273 RVA: 0x002E5805 File Offset: 0x002E3A05
		// (set) Token: 0x060158D2 RID: 88274 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "evt")]
		public EnumValue<TriggerEventValues> Event
		{
			get
			{
				return (EnumValue<TriggerEventValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006FC8 RID: 28616
		// (get) Token: 0x060158D3 RID: 88275 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060158D4 RID: 88276 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "delay")]
		public StringValue Delay
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

		// Token: 0x060158D5 RID: 88277 RVA: 0x00320790 File Offset: 0x0031E990
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "tgtEl" == name)
			{
				return new TargetElement();
			}
			if (24 == namespaceId && "tn" == name)
			{
				return new TimeNode();
			}
			if (24 == namespaceId && "rtn" == name)
			{
				return new RuntimeNodeTrigger();
			}
			return null;
		}

		// Token: 0x17006FC9 RID: 28617
		// (get) Token: 0x060158D6 RID: 88278 RVA: 0x003207E6 File Offset: 0x0031E9E6
		internal override string[] ElementTagNames
		{
			get
			{
				return TimeListConditionalType.eleTagNames;
			}
		}

		// Token: 0x17006FCA RID: 28618
		// (get) Token: 0x060158D7 RID: 88279 RVA: 0x003207ED File Offset: 0x0031E9ED
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TimeListConditionalType.eleNamespaceIds;
			}
		}

		// Token: 0x17006FCB RID: 28619
		// (get) Token: 0x060158D8 RID: 88280 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17006FCC RID: 28620
		// (get) Token: 0x060158D9 RID: 88281 RVA: 0x003207F4 File Offset: 0x0031E9F4
		// (set) Token: 0x060158DA RID: 88282 RVA: 0x003207FD File Offset: 0x0031E9FD
		public TargetElement TargetElement
		{
			get
			{
				return base.GetElement<TargetElement>(0);
			}
			set
			{
				base.SetElement<TargetElement>(0, value);
			}
		}

		// Token: 0x17006FCD RID: 28621
		// (get) Token: 0x060158DB RID: 88283 RVA: 0x00320807 File Offset: 0x0031EA07
		// (set) Token: 0x060158DC RID: 88284 RVA: 0x00320810 File Offset: 0x0031EA10
		public TimeNode TimeNode
		{
			get
			{
				return base.GetElement<TimeNode>(1);
			}
			set
			{
				base.SetElement<TimeNode>(1, value);
			}
		}

		// Token: 0x17006FCE RID: 28622
		// (get) Token: 0x060158DD RID: 88285 RVA: 0x0032081A File Offset: 0x0031EA1A
		// (set) Token: 0x060158DE RID: 88286 RVA: 0x00320823 File Offset: 0x0031EA23
		public RuntimeNodeTrigger RuntimeNodeTrigger
		{
			get
			{
				return base.GetElement<RuntimeNodeTrigger>(2);
			}
			set
			{
				base.SetElement<RuntimeNodeTrigger>(2, value);
			}
		}

		// Token: 0x060158DF RID: 88287 RVA: 0x0032082D File Offset: 0x0031EA2D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "evt" == name)
			{
				return new EnumValue<TriggerEventValues>();
			}
			if (namespaceId == 0 && "delay" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060158E0 RID: 88288 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TimeListConditionalType()
		{
		}

		// Token: 0x060158E1 RID: 88289 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TimeListConditionalType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060158E2 RID: 88290 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TimeListConditionalType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060158E3 RID: 88291 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TimeListConditionalType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060158E4 RID: 88292 RVA: 0x00320864 File Offset: 0x0031EA64
		// Note: this type is marked as 'beforefieldinit'.
		static TimeListConditionalType()
		{
			byte[] array = new byte[2];
			TimeListConditionalType.attributeNamespaceIds = array;
			TimeListConditionalType.eleTagNames = new string[] { "tgtEl", "tn", "rtn" };
			TimeListConditionalType.eleNamespaceIds = new byte[] { 24, 24, 24 };
		}

		// Token: 0x040093D8 RID: 37848
		private static string[] attributeTagNames = new string[] { "evt", "delay" };

		// Token: 0x040093D9 RID: 37849
		private static byte[] attributeNamespaceIds;

		// Token: 0x040093DA RID: 37850
		private static readonly string[] eleTagNames;

		// Token: 0x040093DB RID: 37851
		private static readonly byte[] eleNamespaceIds;
	}
}
