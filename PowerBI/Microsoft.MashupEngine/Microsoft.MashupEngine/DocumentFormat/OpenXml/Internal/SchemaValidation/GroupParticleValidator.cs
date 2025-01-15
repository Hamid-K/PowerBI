using System;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003110 RID: 12560
	internal class GroupParticleValidator : CompositeParticleValidator
	{
		// Token: 0x0601B3D2 RID: 111570 RVA: 0x0037369E File Offset: 0x0037189E
		internal GroupParticleValidator(CompositeParticle particleConstraint)
			: base(particleConstraint)
		{
		}

		// Token: 0x0601B3D3 RID: 111571 RVA: 0x003736A8 File Offset: 0x003718A8
		public override void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			ParticleConstraint particleConstraint = base.ParticleConstraint.ChildrenParticles[0];
			particleConstraint.ParticleValidator.TryMatch(particleMatchInfo, validationContext);
		}
	}
}
