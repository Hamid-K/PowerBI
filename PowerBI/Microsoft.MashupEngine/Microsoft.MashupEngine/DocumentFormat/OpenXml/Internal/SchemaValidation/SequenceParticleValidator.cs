using System;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003124 RID: 12580
	internal class SequenceParticleValidator : CompositeParticleValidator
	{
		// Token: 0x0601B4A8 RID: 111784 RVA: 0x003758BA File Offset: 0x00373ABA
		internal SequenceParticleValidator(CompositeParticle particleConstraint)
			: base(particleConstraint)
		{
		}

		// Token: 0x0601B4A9 RID: 111785 RVA: 0x003758D0 File Offset: 0x00373AD0
		public override void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			OpenXmlElement openXmlElement = particleMatchInfo.StartElement;
			particleMatchInfo.LastMatchedElement = null;
			particleMatchInfo.Match = ParticleMatch.Nomatch;
			int i = 0;
			int num = base.ParticleConstraint.ChildrenParticles.Length;
			while (i < num && openXmlElement != null)
			{
				ParticleConstraint particleConstraint = base.ParticleConstraint.ChildrenParticles[i];
				this._childMatchInfo.Reset(openXmlElement);
				particleConstraint.ParticleValidator.TryMatch(this._childMatchInfo, validationContext);
				switch (this._childMatchInfo.Match)
				{
				case ParticleMatch.Nomatch:
					if (particleConstraint.ParticleValidator.GetRequiredElements(null))
					{
						if (validationContext.CollectExpectedChildren)
						{
							if (particleMatchInfo.ExpectedChildren == null)
							{
								particleMatchInfo.SetExpectedChildren(particleConstraint.ParticleValidator.GetRequiredElements());
							}
							else
							{
								particleMatchInfo.ExpectedChildren.Clear();
								particleConstraint.ParticleValidator.GetRequiredElements(particleMatchInfo.ExpectedChildren);
							}
						}
						if (openXmlElement == particleMatchInfo.StartElement)
						{
							particleMatchInfo.Match = ParticleMatch.Nomatch;
							particleMatchInfo.LastMatchedElement = null;
							return;
						}
						particleMatchInfo.Match = ParticleMatch.Partial;
						return;
					}
					else
					{
						i++;
					}
					break;
				case ParticleMatch.Partial:
					particleMatchInfo.Match = ParticleMatch.Partial;
					particleMatchInfo.LastMatchedElement = this._childMatchInfo.LastMatchedElement;
					if (validationContext.CollectExpectedChildren)
					{
						particleMatchInfo.SetExpectedChildren(this._childMatchInfo.ExpectedChildren);
					}
					return;
				case ParticleMatch.Matched:
					particleMatchInfo.LastMatchedElement = this._childMatchInfo.LastMatchedElement;
					openXmlElement = validationContext.GetNextChildMc(particleMatchInfo.LastMatchedElement);
					i++;
					break;
				}
			}
			if (i != num)
			{
				while (i < num)
				{
					if (base.ParticleConstraint.ChildrenParticles[i].ParticleValidator.GetRequiredElements(null))
					{
						if (validationContext.CollectExpectedChildren)
						{
							if (particleMatchInfo.ExpectedChildren == null)
							{
								particleMatchInfo.InitExpectedChildren();
							}
							base.ParticleConstraint.ChildrenParticles[i].ParticleValidator.GetRequiredElements(particleMatchInfo.ExpectedChildren);
						}
						particleMatchInfo.Match = ParticleMatch.Partial;
						return;
					}
					i++;
				}
				particleMatchInfo.Match = ParticleMatch.Matched;
				return;
			}
			if (particleMatchInfo.LastMatchedElement != null)
			{
				particleMatchInfo.Match = ParticleMatch.Matched;
				return;
			}
			particleMatchInfo.Match = ParticleMatch.Nomatch;
		}

		// Token: 0x0601B4AA RID: 111786 RVA: 0x00375AB0 File Offset: 0x00373CB0
		public override bool GetRequiredElements(ExpectedChildren result)
		{
			bool flag = false;
			if (base.ParticleConstraint.MinOccurs > 0)
			{
				foreach (ParticleConstraint particleConstraint in base.ParticleConstraint.ChildrenParticles)
				{
					if (particleConstraint.ParticleValidator.GetRequiredElements(result))
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0601B4AB RID: 111787 RVA: 0x00375AFE File Offset: 0x00373CFE
		public override bool GetExpectedElements(ExpectedChildren result)
		{
			if (base.ParticleConstraint.ChildrenParticles.Length > 0)
			{
				base.ParticleConstraint.ChildrenParticles[0].ParticleValidator.GetExpectedElements(result);
				return true;
			}
			return false;
		}

		// Token: 0x0400B4DC RID: 46300
		private ParticleMatchInfo _childMatchInfo = new ParticleMatchInfo();
	}
}
