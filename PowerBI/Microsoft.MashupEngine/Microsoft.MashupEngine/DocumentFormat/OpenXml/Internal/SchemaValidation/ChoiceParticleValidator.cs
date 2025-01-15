using System;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x0200310F RID: 12559
	internal class ChoiceParticleValidator : CompositeParticleValidator
	{
		// Token: 0x0601B3CF RID: 111567 RVA: 0x00373541 File Offset: 0x00371741
		internal ChoiceParticleValidator(CompositeParticle particleConstraint)
			: base(particleConstraint)
		{
		}

		// Token: 0x0601B3D0 RID: 111568 RVA: 0x00373558 File Offset: 0x00371758
		public override void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			OpenXmlElement startElement = particleMatchInfo.StartElement;
			particleMatchInfo.LastMatchedElement = null;
			particleMatchInfo.Match = ParticleMatch.Nomatch;
			int num = 0;
			int num2 = base.ParticleConstraint.ChildrenParticles.Length;
			while (num < num2 && startElement != null)
			{
				ParticleConstraint particleConstraint = base.ParticleConstraint.ChildrenParticles[num];
				this._childMatchInfo.Reset(startElement);
				particleConstraint.ParticleValidator.TryMatch(this._childMatchInfo, validationContext);
				switch (this._childMatchInfo.Match)
				{
				case ParticleMatch.Nomatch:
					num++;
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
					particleMatchInfo.Match = ParticleMatch.Matched;
					particleMatchInfo.LastMatchedElement = this._childMatchInfo.LastMatchedElement;
					return;
				}
			}
		}

		// Token: 0x0601B3D1 RID: 111569 RVA: 0x00373638 File Offset: 0x00371838
		public override bool GetRequiredElements(ExpectedChildren result)
		{
			bool flag = true;
			ExpectedChildren expectedChildren = new ExpectedChildren();
			if (base.ParticleConstraint.MinOccurs > 0)
			{
				foreach (ParticleConstraint particleConstraint in base.ParticleConstraint.ChildrenParticles)
				{
					if (!particleConstraint.ParticleValidator.GetRequiredElements(expectedChildren))
					{
						flag = false;
					}
				}
				if (flag && result != null)
				{
					result.Add(expectedChildren);
				}
				return flag;
			}
			return false;
		}

		// Token: 0x0400B4B0 RID: 46256
		private ParticleMatchInfo _childMatchInfo = new ParticleMatchInfo();
	}
}
