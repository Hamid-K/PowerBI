using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003102 RID: 12546
	internal class AllParticleValidator : CompositeParticleValidator
	{
		// Token: 0x0601B36D RID: 111469 RVA: 0x00372A00 File Offset: 0x00370C00
		internal AllParticleValidator(CompositeParticle particleConstraint)
			: base(particleConstraint)
		{
			this._childrenParticles = new Dictionary<int, bool>(base.ParticleConstraint.ChildrenParticles.Length);
			foreach (ParticleConstraint particleConstraint2 in base.ParticleConstraint.ChildrenParticles)
			{
				this._childrenParticles.Add(particleConstraint2.ElementId, false);
			}
		}

		// Token: 0x0601B36E RID: 111470 RVA: 0x00372A5C File Offset: 0x00370C5C
		public override void TryMatch(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			this.TryMatchOnce(particleMatchInfo, validationContext);
		}

		// Token: 0x0601B36F RID: 111471 RVA: 0x00372A68 File Offset: 0x00370C68
		public override void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
		{
			OpenXmlElement openXmlElement = particleMatchInfo.StartElement;
			particleMatchInfo.LastMatchedElement = null;
			particleMatchInfo.Match = ParticleMatch.Nomatch;
			foreach (ParticleConstraint particleConstraint in base.ParticleConstraint.ChildrenParticles)
			{
				this._childrenParticles[particleConstraint.ElementId] = false;
			}
			bool flag;
			while (openXmlElement != null && this._childrenParticles.TryGetValue(openXmlElement.ElementTypeId, out flag) && !flag)
			{
				this._childrenParticles[openXmlElement.ElementTypeId] = true;
				particleMatchInfo.LastMatchedElement = openXmlElement;
				openXmlElement = validationContext.GetNextChildMc(openXmlElement);
			}
			if (particleMatchInfo.ExpectedChildren == null)
			{
				particleMatchInfo.InitExpectedChildren();
			}
			if (particleMatchInfo.LastMatchedElement == null)
			{
				particleMatchInfo.Match = ParticleMatch.Nomatch;
				foreach (ParticleConstraint particleConstraint2 in base.ParticleConstraint.ChildrenParticles)
				{
					particleMatchInfo.ExpectedChildren.Add(particleConstraint2.ElementId);
				}
				return;
			}
			particleMatchInfo.Match = ParticleMatch.Matched;
			foreach (ParticleConstraint particleConstraint3 in base.ParticleConstraint.ChildrenParticles)
			{
				if (!this._childrenParticles[particleConstraint3.ElementId] && particleConstraint3.MinOccurs == 1)
				{
					particleMatchInfo.Match = ParticleMatch.Partial;
				}
			}
			foreach (ParticleConstraint particleConstraint4 in base.ParticleConstraint.ChildrenParticles)
			{
				if (!this._childrenParticles[particleConstraint4.ElementId])
				{
					particleMatchInfo.ExpectedChildren.Add(particleConstraint4.ElementId);
				}
			}
		}

		// Token: 0x0601B370 RID: 111472 RVA: 0x00372BF8 File Offset: 0x00370DF8
		protected override void EmitInvalidElementError(ValidationContext validationContext, ParticleMatchInfo particleMatchInfo)
		{
			OpenXmlElement element = validationContext.Element;
			OpenXmlElement openXmlElement;
			if (particleMatchInfo.LastMatchedElement == null)
			{
				openXmlElement = validationContext.GetFirstChildMc();
			}
			else
			{
				openXmlElement = validationContext.GetNextChildMc(particleMatchInfo.LastMatchedElement);
			}
			switch (particleMatchInfo.Match)
			{
			case ParticleMatch.Nomatch:
			{
				string text = ParticleValidator.GetExpectedChildrenMessage(validationContext.Element, base.GetExpectedElements());
				ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(element, openXmlElement, "Sch_InvalidElementContentExpectingComplex", new string[]
				{
					openXmlElement.XmlQualifiedName.ToString(),
					text
				});
				validationContext.EmitError(validationErrorInfo);
				return;
			}
			case ParticleMatch.Partial:
			case ParticleMatch.Matched:
			{
				ValidationErrorInfo validationErrorInfo;
				if (this._childrenParticles.ContainsKey(openXmlElement.ElementTypeId))
				{
					validationErrorInfo = validationContext.ComposeSchemaValidationError(element, openXmlElement, "Sch_AllElement", new string[] { openXmlElement.XmlQualifiedName.ToString() });
					validationContext.EmitError(validationErrorInfo);
					return;
				}
				string text = ParticleValidator.GetExpectedChildrenMessage(validationContext.Element, particleMatchInfo.ExpectedChildren);
				validationErrorInfo = validationContext.ComposeSchemaValidationError(element, openXmlElement, "Sch_InvalidElementContentExpectingComplex", new string[]
				{
					openXmlElement.XmlQualifiedName.ToString(),
					text
				});
				validationContext.EmitError(validationErrorInfo);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0400B485 RID: 46213
		private Dictionary<int, bool> _childrenParticles;
	}
}
