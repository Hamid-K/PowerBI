using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AC2 RID: 10946
	[ChildElementInfo(typeof(ExclusiveTimeNode))]
	[ChildElementInfo(typeof(AnimateEffect))]
	[ChildElementInfo(typeof(AnimateMotion))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ParallelTimeNode))]
	[ChildElementInfo(typeof(SequenceTimeNode))]
	[ChildElementInfo(typeof(Animate))]
	[ChildElementInfo(typeof(AnimateColor))]
	[ChildElementInfo(typeof(AnimateRotation))]
	[ChildElementInfo(typeof(AnimateScale))]
	[ChildElementInfo(typeof(Command))]
	[ChildElementInfo(typeof(SetBehavior))]
	[ChildElementInfo(typeof(Audio))]
	[ChildElementInfo(typeof(Video))]
	internal abstract class TimeTypeListType : OpenXmlCompositeElement
	{
		// Token: 0x060164D5 RID: 91349 RVA: 0x00328B8C File Offset: 0x00326D8C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "par" == name)
			{
				return new ParallelTimeNode();
			}
			if (24 == namespaceId && "seq" == name)
			{
				return new SequenceTimeNode();
			}
			if (24 == namespaceId && "excl" == name)
			{
				return new ExclusiveTimeNode();
			}
			if (24 == namespaceId && "anim" == name)
			{
				return new Animate();
			}
			if (24 == namespaceId && "animClr" == name)
			{
				return new AnimateColor();
			}
			if (24 == namespaceId && "animEffect" == name)
			{
				return new AnimateEffect();
			}
			if (24 == namespaceId && "animMotion" == name)
			{
				return new AnimateMotion();
			}
			if (24 == namespaceId && "animRot" == name)
			{
				return new AnimateRotation();
			}
			if (24 == namespaceId && "animScale" == name)
			{
				return new AnimateScale();
			}
			if (24 == namespaceId && "cmd" == name)
			{
				return new Command();
			}
			if (24 == namespaceId && "set" == name)
			{
				return new SetBehavior();
			}
			if (24 == namespaceId && "audio" == name)
			{
				return new Audio();
			}
			if (24 == namespaceId && "video" == name)
			{
				return new Video();
			}
			return null;
		}

		// Token: 0x060164D6 RID: 91350 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TimeTypeListType()
		{
		}

		// Token: 0x060164D7 RID: 91351 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TimeTypeListType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164D8 RID: 91352 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TimeTypeListType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060164D9 RID: 91353 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TimeTypeListType(string outerXml)
			: base(outerXml)
		{
		}
	}
}
