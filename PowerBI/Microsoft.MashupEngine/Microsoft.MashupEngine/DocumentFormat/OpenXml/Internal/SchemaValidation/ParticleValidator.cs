using System;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003100 RID: 12544
	internal abstract class ParticleValidator : IParticleValidator
	{
		// Token: 0x0601B35C RID: 111452 RVA: 0x000020FD File Offset: 0x000002FD
		internal ParticleValidator()
		{
		}

		// Token: 0x0601B35D RID: 111453
		internal abstract void Validate(ValidationContext validationContext);

		// Token: 0x0601B35E RID: 111454 RVA: 0x003724A9 File Offset: 0x003706A9
		public virtual void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			particleMatchInfo.Match = ParticleMatch.Nomatch;
		}

		// Token: 0x0601B35F RID: 111455 RVA: 0x003724A9 File Offset: 0x003706A9
		public virtual void TryMatch(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			particleMatchInfo.Match = ParticleMatch.Nomatch;
		}

		// Token: 0x0601B360 RID: 111456 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool GetRequiredElements(ExpectedChildren result)
		{
			return false;
		}

		// Token: 0x0601B361 RID: 111457 RVA: 0x003724B4 File Offset: 0x003706B4
		public ExpectedChildren GetRequiredElements()
		{
			ExpectedChildren expectedChildren = new ExpectedChildren();
			this.GetRequiredElements(expectedChildren);
			return expectedChildren;
		}

		// Token: 0x0601B362 RID: 111458 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool GetExpectedElements(ExpectedChildren result)
		{
			return false;
		}

		// Token: 0x0601B363 RID: 111459 RVA: 0x003724D0 File Offset: 0x003706D0
		public ExpectedChildren GetExpectedElements()
		{
			ExpectedChildren expectedChildren = new ExpectedChildren();
			this.GetExpectedElements(expectedChildren);
			return expectedChildren;
		}

		// Token: 0x0601B364 RID: 111460 RVA: 0x003724EC File Offset: 0x003706EC
		internal static ParticleValidator CreateParticleValidator(ParticleConstraint particleConstraint)
		{
			CompositeParticle compositeParticle = particleConstraint as CompositeParticle;
			switch (particleConstraint.ParticleType)
			{
			case ParticleType.All:
				return new AllParticleValidator(compositeParticle);
			case ParticleType.Choice:
				return new ChoiceParticleValidator(compositeParticle);
			case ParticleType.Group:
				return new GroupParticleValidator(compositeParticle);
			case ParticleType.Sequence:
				return new SequenceParticleValidator(compositeParticle);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0601B365 RID: 111461 RVA: 0x00372548 File Offset: 0x00370748
		internal static string GetExpectedChildrenMessage(OpenXmlElement parent, ExpectedChildren expectedChildrenIds)
		{
			if (expectedChildrenIds != null)
			{
				return expectedChildrenIds.GetExpectedChildrenMessage(parent);
			}
			return string.Empty;
		}
	}
}
