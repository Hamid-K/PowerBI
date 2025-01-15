using System;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003104 RID: 12548
	internal class NsAnyParticleValidator : AnyParticleValidator
	{
		// Token: 0x170098A1 RID: 39073
		// (get) Token: 0x0601B37A RID: 111482 RVA: 0x00372F38 File Offset: 0x00371138
		internal override ParticleConstraint ParticleConstraint
		{
			get
			{
				return this._nsAnyParticleConstraint;
			}
		}

		// Token: 0x0601B37B RID: 111483 RVA: 0x00372F40 File Offset: 0x00371140
		internal NsAnyParticleValidator(NsAnyParticle particleConstraint)
		{
			this._nsAnyParticleConstraint = particleConstraint;
		}

		// Token: 0x0601B37C RID: 111484 RVA: 0x00372F50 File Offset: 0x00371150
		public override void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			OpenXmlElement startElement = particleMatchInfo.StartElement;
			if (startElement.NamespaceUri == NamespaceIdMap.GetNamespaceUri(this._nsAnyParticleConstraint.NamespaceId))
			{
				particleMatchInfo.Match = ParticleMatch.Matched;
				particleMatchInfo.LastMatchedElement = startElement;
				return;
			}
			particleMatchInfo.Match = ParticleMatch.Nomatch;
		}

		// Token: 0x0601B37D RID: 111485 RVA: 0x00372F97 File Offset: 0x00371197
		public override bool GetRequiredElements(ExpectedChildren result)
		{
			if (this.ParticleConstraint.MinOccurs > 0)
			{
				if (result != null)
				{
					result.Add(NamespaceIdMap.GetNamespaceUri(this._nsAnyParticleConstraint.NamespaceId));
				}
				return true;
			}
			return false;
		}

		// Token: 0x0601B37E RID: 111486 RVA: 0x00372FC3 File Offset: 0x003711C3
		public override bool GetExpectedElements(ExpectedChildren result)
		{
			if (result != null)
			{
				result.Add(NamespaceIdMap.GetNamespaceUri(this._nsAnyParticleConstraint.NamespaceId));
			}
			return true;
		}

		// Token: 0x0400B487 RID: 46215
		private NsAnyParticle _nsAnyParticleConstraint;
	}
}
