using System;
using System.Diagnostics;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003112 RID: 12562
	[DebuggerDisplay("ElementId={ElementId}")]
	internal class ElementParticle : ParticleConstraint, IParticleValidator
	{
		// Token: 0x0601B3E5 RID: 111589 RVA: 0x003737EC File Offset: 0x003719EC
		internal ElementParticle()
		{
		}

		// Token: 0x170098CA RID: 39114
		// (get) Token: 0x0601B3E6 RID: 111590 RVA: 0x00002105 File Offset: 0x00000305
		// (set) Token: 0x0601B3E7 RID: 111591 RVA: 0x0000336E File Offset: 0x0000156E
		internal override ParticleType ParticleType
		{
			get
			{
				return ParticleType.Element;
			}
			set
			{
			}
		}

		// Token: 0x170098CB RID: 39115
		// (get) Token: 0x0601B3E8 RID: 111592 RVA: 0x003737F4 File Offset: 0x003719F4
		// (set) Token: 0x0601B3E9 RID: 111593 RVA: 0x003737FC File Offset: 0x003719FC
		internal override int ElementId
		{
			get
			{
				return this._elementId;
			}
			set
			{
				this._elementId = value;
			}
		}

		// Token: 0x170098CC RID: 39116
		// (get) Token: 0x0601B3EA RID: 111594 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		internal override IParticleValidator ParticleValidator
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0601B3EB RID: 111595 RVA: 0x00373805 File Offset: 0x00371A05
		public void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			if (particleMatchInfo.StartElement.ElementTypeId == this.ElementId)
			{
				particleMatchInfo.Match = ParticleMatch.Matched;
				particleMatchInfo.LastMatchedElement = particleMatchInfo.StartElement;
				return;
			}
			particleMatchInfo.Match = ParticleMatch.Nomatch;
		}

		// Token: 0x0601B3EC RID: 111596 RVA: 0x00373838 File Offset: 0x00371A38
		public void TryMatch(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			if (this.ElementId != particleMatchInfo.StartElement.ElementTypeId)
			{
				particleMatchInfo.Match = ParticleMatch.Nomatch;
				return;
			}
			if (base.MaxOccurs == 1)
			{
				particleMatchInfo.Match = ParticleMatch.Matched;
				particleMatchInfo.LastMatchedElement = particleMatchInfo.StartElement;
				return;
			}
			OpenXmlElement openXmlElement = particleMatchInfo.StartElement;
			int num = 0;
			while (openXmlElement != null && base.MaxOccursGreaterThan(num) && openXmlElement.ElementTypeId == this.ElementId)
			{
				num++;
				particleMatchInfo.LastMatchedElement = openXmlElement;
				openXmlElement = validationContext.GetNextChildMc(openXmlElement);
			}
			if (num >= base.MinOccurs)
			{
				particleMatchInfo.Match = ParticleMatch.Matched;
				return;
			}
			particleMatchInfo.Match = ParticleMatch.Partial;
			if (validationContext.CollectExpectedChildren)
			{
				if (particleMatchInfo.ExpectedChildren == null)
				{
					particleMatchInfo.InitExpectedChildren();
				}
				particleMatchInfo.ExpectedChildren.Add(this.ElementId);
			}
		}

		// Token: 0x0601B3ED RID: 111597 RVA: 0x003738F4 File Offset: 0x00371AF4
		public bool GetRequiredElements(ExpectedChildren result)
		{
			if (base.MinOccurs > 0)
			{
				if (result != null)
				{
					result.Add(this.ElementId);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0601B3EE RID: 111598 RVA: 0x00373914 File Offset: 0x00371B14
		public ExpectedChildren GetRequiredElements()
		{
			ExpectedChildren expectedChildren = new ExpectedChildren();
			if (base.MinOccurs > 0)
			{
				expectedChildren.Add(this.ElementId);
			}
			return expectedChildren;
		}

		// Token: 0x0601B3EF RID: 111599 RVA: 0x0037393D File Offset: 0x00371B3D
		public bool GetExpectedElements(ExpectedChildren result)
		{
			result.Add(this.ElementId);
			return true;
		}

		// Token: 0x0601B3F0 RID: 111600 RVA: 0x0037394C File Offset: 0x00371B4C
		public ExpectedChildren GetExpectedElements()
		{
			ExpectedChildren expectedChildren = new ExpectedChildren();
			expectedChildren.Add(this.ElementId);
			return expectedChildren;
		}

		// Token: 0x0400B4B3 RID: 46259
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int _elementId;
	}
}
