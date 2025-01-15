using System;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x020030FF RID: 12543
	internal interface IParticleValidator
	{
		// Token: 0x0601B356 RID: 111446
		void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext);

		// Token: 0x0601B357 RID: 111447
		void TryMatch(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext);

		// Token: 0x0601B358 RID: 111448
		bool GetRequiredElements(ExpectedChildren result);

		// Token: 0x0601B359 RID: 111449
		ExpectedChildren GetRequiredElements();

		// Token: 0x0601B35A RID: 111450
		bool GetExpectedElements(ExpectedChildren result);

		// Token: 0x0601B35B RID: 111451
		ExpectedChildren GetExpectedElements();
	}
}
