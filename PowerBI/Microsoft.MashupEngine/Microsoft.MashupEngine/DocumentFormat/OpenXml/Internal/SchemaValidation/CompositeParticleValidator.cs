using System;
using System.Diagnostics;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003101 RID: 12545
	internal abstract class CompositeParticleValidator : ParticleValidator
	{
		// Token: 0x1700989F RID: 39071
		// (get) Token: 0x0601B366 RID: 111462 RVA: 0x0037255A File Offset: 0x0037075A
		protected ParticleConstraint ParticleConstraint
		{
			get
			{
				return this._particleConstraint;
			}
		}

		// Token: 0x0601B367 RID: 111463 RVA: 0x00372562 File Offset: 0x00370762
		internal CompositeParticleValidator(ParticleConstraint particleConstraint)
		{
			this._particleConstraint = particleConstraint;
		}

		// Token: 0x0601B368 RID: 111464 RVA: 0x0037257C File Offset: 0x0037077C
		internal override void Validate(ValidationContext validationContext)
		{
			OpenXmlCompositeElement openXmlCompositeElement = validationContext.Element as OpenXmlCompositeElement;
			OpenXmlElement openXmlElement = validationContext.GetFirstChildMc();
			if (openXmlElement == null)
			{
				if (this.ParticleConstraint.MinOccurs == 0)
				{
					return;
				}
				ExpectedChildren requiredElements = base.GetRequiredElements();
				if (requiredElements.Count > 0)
				{
					ValidationErrorInfo validationErrorInfo = validationContext.ComposeSchemaValidationError(openXmlCompositeElement, null, "Sch_IncompleteContentExpectingComplex", new string[] { ParticleValidator.GetExpectedChildrenMessage(openXmlCompositeElement, requiredElements) });
					validationContext.EmitError(validationErrorInfo);
				}
				return;
			}
			else
			{
				if (this._particleMatchInfo == null)
				{
					this._particleMatchInfo = new ParticleMatchInfo(openXmlElement);
				}
				else
				{
					this._particleMatchInfo.Reset(openXmlElement);
				}
				this.TryMatch(this._particleMatchInfo, validationContext);
				switch (this._particleMatchInfo.Match)
				{
				case ParticleMatch.Nomatch:
					this.EmitInvalidElementError(validationContext, this._particleMatchInfo);
					return;
				case ParticleMatch.Partial:
					this.EmitInvalidElementError(validationContext, this._particleMatchInfo);
					return;
				case ParticleMatch.Matched:
					openXmlElement = validationContext.GetNextChildMc(this._particleMatchInfo.LastMatchedElement);
					if (openXmlElement != null)
					{
						this.EmitInvalidElementError(validationContext, this._particleMatchInfo);
					}
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x0601B369 RID: 111465 RVA: 0x00372678 File Offset: 0x00370878
		public override void TryMatch(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
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
				this._childMatchInfo.Reset(openXmlElement);
				this.TryMatchOnce(this._childMatchInfo, validationContext);
				if (this._childMatchInfo.Match == ParticleMatch.Nomatch)
				{
					break;
				}
				if (this._childMatchInfo.Match != ParticleMatch.Matched)
				{
					particleMatchInfo.Match = ParticleMatch.Partial;
					particleMatchInfo.LastMatchedElement = this._childMatchInfo.LastMatchedElement;
					if (validationContext.CollectExpectedChildren)
					{
						particleMatchInfo.SetExpectedChildren(this._childMatchInfo.ExpectedChildren);
					}
					return;
				}
				num++;
				particleMatchInfo.LastMatchedElement = this._childMatchInfo.LastMatchedElement;
				openXmlElement = validationContext.GetNextChildMc(particleMatchInfo.LastMatchedElement);
			}
			if (num == 0)
			{
				particleMatchInfo.Match = ParticleMatch.Nomatch;
				if (validationContext.CollectExpectedChildren)
				{
					particleMatchInfo.SetExpectedChildren(base.GetExpectedElements());
					return;
				}
			}
			else
			{
				if (num >= this.ParticleConstraint.MinOccurs)
				{
					particleMatchInfo.Match = ParticleMatch.Matched;
					return;
				}
				if (this.GetRequiredElements(particleMatchInfo.ExpectedChildren))
				{
					particleMatchInfo.Match = ParticleMatch.Partial;
					return;
				}
				particleMatchInfo.Match = ParticleMatch.Matched;
			}
		}

		// Token: 0x0601B36A RID: 111466 RVA: 0x0037279C File Offset: 0x0037099C
		public override bool GetRequiredElements(ExpectedChildren result)
		{
			bool flag = false;
			if (this._particleConstraint.MinOccurs > 0)
			{
				foreach (ParticleConstraint particleConstraint in this._particleConstraint.ChildrenParticles)
				{
					if (particleConstraint.ParticleValidator.GetRequiredElements(result))
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0601B36B RID: 111467 RVA: 0x003727E8 File Offset: 0x003709E8
		public override bool GetExpectedElements(ExpectedChildren result)
		{
			foreach (ParticleConstraint particleConstraint in this.ParticleConstraint.ChildrenParticles)
			{
				particleConstraint.ParticleValidator.GetExpectedElements(result);
			}
			return true;
		}

		// Token: 0x0601B36C RID: 111468 RVA: 0x00372824 File Offset: 0x00370A24
		protected virtual void EmitInvalidElementError(ValidationContext validationContext, ParticleMatchInfo particleMatchInfo)
		{
			OpenXmlElement openXmlElement;
			if (particleMatchInfo.Match != ParticleMatch.Nomatch)
			{
				openXmlElement = validationContext.GetFirstChildMc();
				validationContext.CollectExpectedChildren = true;
				particleMatchInfo.Reset(openXmlElement);
				particleMatchInfo.InitExpectedChildren();
				this.TryMatch(particleMatchInfo, validationContext);
				validationContext.CollectExpectedChildren = false;
			}
			OpenXmlElement element = validationContext.Element;
			if (particleMatchInfo.LastMatchedElement == null)
			{
				openXmlElement = validationContext.GetFirstChildMc();
			}
			else
			{
				openXmlElement = validationContext.GetNextChildMc(particleMatchInfo.LastMatchedElement);
			}
			string text = null;
			ValidationErrorInfo validationErrorInfo;
			switch (particleMatchInfo.Match)
			{
			case ParticleMatch.Nomatch:
				text = ParticleValidator.GetExpectedChildrenMessage(validationContext.Element, base.GetExpectedElements());
				break;
			case ParticleMatch.Partial:
				if (openXmlElement == null)
				{
					validationErrorInfo = validationContext.ComposeSchemaValidationError(element, null, "Sch_IncompleteContentExpectingComplex", new string[] { ParticleValidator.GetExpectedChildrenMessage(element, particleMatchInfo.ExpectedChildren) });
					validationContext.EmitError(validationErrorInfo);
					return;
				}
				text = ParticleValidator.GetExpectedChildrenMessage(validationContext.Element, particleMatchInfo.ExpectedChildren);
				break;
			case ParticleMatch.Matched:
				if (this.ParticleConstraint.CanOccursMoreThanOne)
				{
					text = ParticleValidator.GetExpectedChildrenMessage(validationContext.Element, base.GetExpectedElements());
				}
				else
				{
					text = ParticleValidator.GetExpectedChildrenMessage(validationContext.Element, particleMatchInfo.ExpectedChildren);
				}
				break;
			}
			if (validationContext.Element.CanContainsChild(openXmlElement))
			{
				validationErrorInfo = validationContext.ComposeSchemaValidationError(element, openXmlElement, "Sch_UnexpectedElementContentExpectingComplex", new string[]
				{
					openXmlElement.XmlQualifiedName.ToString(),
					text
				});
			}
			else if (element.TryCreateValidChild(validationContext.FileFormat, openXmlElement.NamespaceUri, openXmlElement.LocalName) == null)
			{
				validationErrorInfo = validationContext.ComposeSchemaValidationError(element, openXmlElement, "Sch_InvalidElementContentExpectingComplex", new string[]
				{
					openXmlElement.XmlQualifiedName.ToString(),
					text
				});
			}
			else
			{
				validationErrorInfo = validationContext.ComposeSchemaValidationError(element, openXmlElement, "Sch_InvalidElementContentWrongType", new string[]
				{
					openXmlElement.XmlQualifiedName.ToString(),
					openXmlElement.GetType().Name
				});
			}
			validationContext.EmitError(validationErrorInfo);
		}

		// Token: 0x0400B482 RID: 46210
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ParticleConstraint _particleConstraint;

		// Token: 0x0400B483 RID: 46211
		private ParticleMatchInfo _particleMatchInfo;

		// Token: 0x0400B484 RID: 46212
		private ParticleMatchInfo _childMatchInfo = new ParticleMatchInfo();
	}
}
