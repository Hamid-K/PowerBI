using System;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003103 RID: 12547
	internal class AnyParticleValidator : IParticleValidator
	{
		// Token: 0x170098A0 RID: 39072
		// (get) Token: 0x0601B371 RID: 111473 RVA: 0x00372D13 File Offset: 0x00370F13
		internal virtual ParticleConstraint ParticleConstraint
		{
			get
			{
				return this._particleConstraint;
			}
		}

		// Token: 0x0601B372 RID: 111474 RVA: 0x000020FD File Offset: 0x000002FD
		protected AnyParticleValidator()
		{
		}

		// Token: 0x0601B373 RID: 111475 RVA: 0x00372D1B File Offset: 0x00370F1B
		internal AnyParticleValidator(AnyParticle particleConstraint)
		{
			this._particleConstraint = particleConstraint;
		}

		// Token: 0x0601B374 RID: 111476 RVA: 0x00372D2C File Offset: 0x00370F2C
		public virtual void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			OpenXmlElement startElement = particleMatchInfo.StartElement;
			switch (this._particleConstraint.NamespaceValue)
			{
			case 0:
				particleMatchInfo.Match = ParticleMatch.Matched;
				particleMatchInfo.LastMatchedElement = startElement;
				return;
			case 1:
				if (string.IsNullOrEmpty(startElement.NamespaceUri) || (startElement.Parent != null && startElement.NamespaceUri != startElement.Parent.NamespaceUri))
				{
					particleMatchInfo.Match = ParticleMatch.Matched;
					particleMatchInfo.LastMatchedElement = startElement;
					return;
				}
				particleMatchInfo.Match = ParticleMatch.Nomatch;
				return;
			case 2:
				if (string.IsNullOrEmpty(startElement.NamespaceUri))
				{
					particleMatchInfo.Match = ParticleMatch.Matched;
					particleMatchInfo.LastMatchedElement = startElement;
					return;
				}
				particleMatchInfo.Match = ParticleMatch.Nomatch;
				return;
			case 3:
				if (startElement.Parent != null && startElement.NamespaceUri == startElement.Parent.NamespaceUri)
				{
					particleMatchInfo.Match = ParticleMatch.Matched;
					particleMatchInfo.LastMatchedElement = startElement;
					return;
				}
				particleMatchInfo.Match = ParticleMatch.Nomatch;
				return;
			default:
				return;
			}
		}

		// Token: 0x0601B375 RID: 111477 RVA: 0x00372E10 File Offset: 0x00371010
		public void TryMatch(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			if (this.ParticleConstraint.MaxOccurs == 1)
			{
				this.TryMatchOnce(particleMatchInfo, validationContext);
				return;
			}
			int num = 0;
			OpenXmlElement openXmlElement = particleMatchInfo.StartElement;
			while (openXmlElement != null && this.ParticleConstraint.MaxOccursGreaterThan(num))
			{
				ParticleMatchInfo particleMatchInfo2 = new ParticleMatchInfo(openXmlElement);
				this.TryMatchOnce(particleMatchInfo2, validationContext);
				if (particleMatchInfo2.Match == ParticleMatch.Nomatch)
				{
					break;
				}
				if (particleMatchInfo2.Match == ParticleMatch.Matched)
				{
					num++;
					particleMatchInfo.LastMatchedElement = particleMatchInfo2.LastMatchedElement;
					openXmlElement = validationContext.GetNextChildMc(particleMatchInfo.LastMatchedElement);
				}
			}
			if (num == 0)
			{
				particleMatchInfo.Match = ParticleMatch.Nomatch;
				return;
			}
			if (num >= this.ParticleConstraint.MinOccurs)
			{
				particleMatchInfo.Match = ParticleMatch.Matched;
				return;
			}
			particleMatchInfo.Match = ParticleMatch.Partial;
		}

		// Token: 0x0601B376 RID: 111478 RVA: 0x00372EB5 File Offset: 0x003710B5
		public virtual bool GetRequiredElements(ExpectedChildren result)
		{
			if (this.ParticleConstraint.MinOccurs > 0)
			{
				if (result != null)
				{
					result.Add(XsdAnyPrefidefinedValue.GetNamespaceString(this._particleConstraint.NamespaceValue));
				}
				return true;
			}
			return false;
		}

		// Token: 0x0601B377 RID: 111479 RVA: 0x00372EE4 File Offset: 0x003710E4
		public ExpectedChildren GetRequiredElements()
		{
			ExpectedChildren expectedChildren = new ExpectedChildren();
			this.GetRequiredElements(expectedChildren);
			return expectedChildren;
		}

		// Token: 0x0601B378 RID: 111480 RVA: 0x00372F00 File Offset: 0x00371100
		public virtual bool GetExpectedElements(ExpectedChildren result)
		{
			if (result != null)
			{
				result.Add(XsdAnyPrefidefinedValue.GetNamespaceString(this._particleConstraint.NamespaceValue));
			}
			return true;
		}

		// Token: 0x0601B379 RID: 111481 RVA: 0x00372F1C File Offset: 0x0037111C
		public ExpectedChildren GetExpectedElements()
		{
			ExpectedChildren expectedChildren = new ExpectedChildren();
			this.GetExpectedElements(expectedChildren);
			return expectedChildren;
		}

		// Token: 0x0400B486 RID: 46214
		private AnyParticle _particleConstraint;
	}
}
