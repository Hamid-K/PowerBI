using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200010F RID: 271
	[ImmutableObject(true)]
	public sealed class TermBindingComparer : IEqualityComparer<TermBinding>
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x00009A1C File Offset: 0x00007C1C
		public TermBindingComparer(StringComparer objectNameComparer, StringComparer valueComparer, bool ignoreText = false)
		{
			this._objectNameComparer = objectNameComparer;
			this._valueComparer = valueComparer;
			this._ignoreText = ignoreText;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00009A39 File Offset: 0x00007C39
		public void SetTermComparer(IEqualityComparer<Term> termComparer)
		{
			this._termComparer = termComparer;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00009A44 File Offset: 0x00007C44
		public bool Equals(TermBinding x, TermBinding y)
		{
			bool? flag = Util.AreEqual<TermBinding>(x, y);
			if (flag != null)
			{
				return flag.Value;
			}
			if (x.GetType() != y.GetType())
			{
				return false;
			}
			if (!this.ShouldIgnoreText(x) && !this._valueComparer.Equals(x.Text, y.Text))
			{
				return false;
			}
			if (x is CoreTermBinding)
			{
				return true;
			}
			VisualizationTypeTermBinding visualizationTypeTermBinding = x as VisualizationTypeTermBinding;
			if (visualizationTypeTermBinding != null)
			{
				VisualizationTypeTermBinding visualizationTypeTermBinding2 = (VisualizationTypeTermBinding)y;
				return visualizationTypeTermBinding.Type == visualizationTypeTermBinding2.Type;
			}
			PhrasingTermBinding phrasingTermBinding = x as PhrasingTermBinding;
			if (phrasingTermBinding != null)
			{
				PhrasingTermBinding phrasingTermBinding2 = (PhrasingTermBinding)y;
				return phrasingTermBinding.Text == phrasingTermBinding2.Text;
			}
			TableTermBinding tableTermBinding = x as TableTermBinding;
			if (tableTermBinding != null)
			{
				TableTermBinding tableTermBinding2 = (TableTermBinding)y;
				return this._objectNameComparer.Equals(tableTermBinding.ConceptualSchema ?? string.Empty, tableTermBinding2.ConceptualSchema ?? string.Empty) && this._objectNameComparer.Equals(tableTermBinding.ConceptualEntity, tableTermBinding2.ConceptualEntity);
			}
			PodTermBinding podTermBinding = x as PodTermBinding;
			if (podTermBinding != null)
			{
				PodTermBinding podTermBinding2 = (PodTermBinding)y;
				return this._objectNameComparer.Equals(podTermBinding.ConceptualSchema ?? string.Empty, podTermBinding2.ConceptualSchema ?? string.Empty) && this._objectNameComparer.Equals(podTermBinding.ConceptualEntity, podTermBinding2.ConceptualEntity);
			}
			TextualEntityTermBinding textualEntityTermBinding = x as TextualEntityTermBinding;
			if (textualEntityTermBinding != null)
			{
				TextualEntityTermBinding textualEntityTermBinding2 = (TextualEntityTermBinding)y;
				return this._objectNameComparer.Equals(textualEntityTermBinding.TextualEntityName, textualEntityTermBinding2.TextualEntityName);
			}
			PropertyTermBinding propertyTermBinding = x as PropertyTermBinding;
			if (propertyTermBinding != null)
			{
				PropertyTermBinding propertyTermBinding2 = (PropertyTermBinding)y;
				return this._objectNameComparer.Equals(propertyTermBinding.ConceptualSchema ?? string.Empty, propertyTermBinding2.ConceptualSchema ?? string.Empty) && this._objectNameComparer.Equals(propertyTermBinding.ConceptualEntity, propertyTermBinding2.ConceptualEntity) && this._objectNameComparer.Equals(propertyTermBinding.ConceptualProperty, propertyTermBinding2.ConceptualProperty) && this._objectNameComparer.Equals(propertyTermBinding.ConceptualHierarchy, propertyTermBinding2.ConceptualHierarchy) && this._objectNameComparer.Equals(propertyTermBinding.ConceptualHierarchyLevel, propertyTermBinding2.ConceptualHierarchyLevel) && this._objectNameComparer.Equals(propertyTermBinding.ConceptualVariationSource, propertyTermBinding2.ConceptualVariationSource);
			}
			ValueTermBinding valueTermBinding = x as ValueTermBinding;
			if (valueTermBinding != null)
			{
				ValueTermBinding valueTermBinding2 = (ValueTermBinding)y;
				return (this._ignoreText || this._objectNameComparer.Equals(valueTermBinding.EntityText, valueTermBinding2.EntityText)) && this._objectNameComparer.Equals(valueTermBinding.ConceptualSchema ?? string.Empty, valueTermBinding2.ConceptualSchema ?? string.Empty) && this._objectNameComparer.Equals(valueTermBinding.ConceptualEntity, valueTermBinding2.ConceptualEntity) && this._objectNameComparer.Equals(valueTermBinding.ConceptualProperty, valueTermBinding2.ConceptualProperty) && this._objectNameComparer.Equals(valueTermBinding.ConceptualHierarchy, valueTermBinding2.ConceptualHierarchy) && this._objectNameComparer.Equals(valueTermBinding.ConceptualHierarchyLevel, valueTermBinding2.ConceptualHierarchyLevel);
			}
			RangeTermBinding rangeTermBinding = x as RangeTermBinding;
			if (rangeTermBinding != null)
			{
				RangeTermBinding rangeTermBinding2 = (RangeTermBinding)y;
				return (this._ignoreText || this._objectNameComparer.Equals(rangeTermBinding.EntityText, rangeTermBinding2.EntityText)) && this._objectNameComparer.Equals(rangeTermBinding.ConceptualSchema ?? string.Empty, rangeTermBinding2.ConceptualSchema ?? string.Empty) && this._objectNameComparer.Equals(rangeTermBinding.ConceptualEntity, rangeTermBinding2.ConceptualEntity) && this._objectNameComparer.Equals(rangeTermBinding.ConceptualProperty, rangeTermBinding2.ConceptualProperty) && this._objectNameComparer.Equals(rangeTermBinding.ConceptualHierarchy, rangeTermBinding2.ConceptualHierarchy) && this._objectNameComparer.Equals(rangeTermBinding.ConceptualHierarchyLevel, rangeTermBinding2.ConceptualHierarchyLevel) && this._termComparer.Equals(rangeTermBinding.LowerBoundTerm, rangeTermBinding2.LowerBoundTerm) && this._termComparer.Equals(rangeTermBinding.UpperBoundTerm, rangeTermBinding2.UpperBoundTerm);
			}
			LiteralTermBinding literalTermBinding = x as LiteralTermBinding;
			if (literalTermBinding != null)
			{
				LiteralTermBinding literalTermBinding2 = (LiteralTermBinding)y;
				return object.Equals(literalTermBinding.DataValue, literalTermBinding2.DataValue) && (this._ignoreText || this._objectNameComparer.Equals(literalTermBinding.Text, literalTermBinding2.Text)) && literalTermBinding.DataType == literalTermBinding2.DataType;
			}
			CompositeTermBinding compositeTermBinding = x as CompositeTermBinding;
			if (compositeTermBinding != null)
			{
				CompositeTermBinding compositeTermBinding2 = (CompositeTermBinding)y;
				return compositeTermBinding.Terms.SequenceEqual(compositeTermBinding2.Terms, this._termComparer);
			}
			InferredTermBinding inferredTermBinding = x as InferredTermBinding;
			if (inferredTermBinding != null)
			{
				InferredTermBinding inferredTermBinding2 = (InferredTermBinding)y;
				return (this._ignoreText || this._objectNameComparer.Equals(inferredTermBinding.Text, inferredTermBinding.Text)) && inferredTermBinding.Type == inferredTermBinding.Type && this._valueComparer.Equals(inferredTermBinding.DefinitionPrompt ?? string.Empty, inferredTermBinding.DefinitionPrompt ?? string.Empty) && this._valueComparer.Equals(inferredTermBinding.DefinitionText ?? string.Empty, inferredTermBinding.DefinitionText ?? string.Empty) && this._valueComparer.Equals(inferredTermBinding.PrefixText ?? string.Empty, inferredTermBinding.PrefixText ?? string.Empty) && this._valueComparer.Equals(inferredTermBinding.SuffixText ?? string.Empty, inferredTermBinding.SuffixText ?? string.Empty) && this._valueComparer.Equals(inferredTermBinding.HintText ?? string.Empty, inferredTermBinding.HintText ?? string.Empty);
			}
			string text = "Unsupported binding of type ";
			Type type = x.GetType();
			throw Contract.ExceptNotSupported(text + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000A074 File Offset: 0x00008274
		public int GetHashCode(TermBinding obj)
		{
			int num = (this.ShouldIgnoreText(obj) ? obj.GetType().GetHashCode() : Hashing.CombineHash(this._valueComparer.GetHashCode(obj.Text), obj.GetType().GetHashCode()));
			if (obj is CoreTermBinding)
			{
				return num;
			}
			VisualizationTypeTermBinding visualizationTypeTermBinding = obj as VisualizationTypeTermBinding;
			if (visualizationTypeTermBinding != null)
			{
				return Hashing.CombineHash(num, visualizationTypeTermBinding.Type.GetHashCode());
			}
			PhrasingTermBinding phrasingTermBinding = obj as PhrasingTermBinding;
			if (phrasingTermBinding != null)
			{
				return Hashing.CombineHash(num, phrasingTermBinding.Text.GetHashCode());
			}
			EntityTermBinding entityTermBinding = obj as EntityTermBinding;
			if (entityTermBinding != null)
			{
				if (!this._ignoreText)
				{
					num = Hashing.CombineHash(num, this._valueComparer.GetHashCode(entityTermBinding.EntityText));
				}
				TextualEntityTermBinding textualEntityTermBinding = entityTermBinding as TextualEntityTermBinding;
				if (textualEntityTermBinding != null)
				{
					num = Hashing.CombineHash(num, this._valueComparer.GetHashCode(textualEntityTermBinding.TextualEntityName));
				}
				else
				{
					ModelEntityTermBinding modelEntityTermBinding = entityTermBinding as ModelEntityTermBinding;
					if (modelEntityTermBinding != null)
					{
						num = Hashing.CombineHash(num, this._objectNameComparer.GetHashCode(modelEntityTermBinding.ConceptualSchema ?? string.Empty), this._objectNameComparer.GetHashCode(modelEntityTermBinding.ConceptualEntity));
						PropertyTermBaseBinding propertyTermBaseBinding;
						if ((propertyTermBaseBinding = modelEntityTermBinding as PropertyTermBaseBinding) != null)
						{
							if (!string.IsNullOrEmpty(propertyTermBaseBinding.ConceptualProperty))
							{
								num = Hashing.CombineHash(num, this._objectNameComparer.GetHashCode(propertyTermBaseBinding.ConceptualProperty));
							}
							if (!string.IsNullOrEmpty(propertyTermBaseBinding.ConceptualHierarchy))
							{
								num = Hashing.CombineHash(num, this._objectNameComparer.GetHashCode(propertyTermBaseBinding.ConceptualHierarchy));
							}
							if (!string.IsNullOrEmpty(propertyTermBaseBinding.ConceptualHierarchyLevel))
							{
								num = Hashing.CombineHash(num, this._objectNameComparer.GetHashCode(propertyTermBaseBinding.ConceptualHierarchyLevel));
							}
						}
					}
				}
				return num;
			}
			RangeTermBinding rangeTermBinding = obj as RangeTermBinding;
			if (rangeTermBinding != null)
			{
				return Hashing.CombineHash(num, (rangeTermBinding.LowerBoundTerm == null) ? (-1) : this._termComparer.GetHashCode(rangeTermBinding.LowerBoundTerm), (rangeTermBinding.UpperBoundTerm == null) ? (-1) : this._termComparer.GetHashCode(rangeTermBinding.UpperBoundTerm));
			}
			LiteralTermBinding literalTermBinding = obj as LiteralTermBinding;
			if (literalTermBinding != null)
			{
				return Hashing.CombineHash(num, literalTermBinding.DataType.GetHashCode(), Hashing.GetHashCode<DataValue>(literalTermBinding.DataValue, null));
			}
			CompositeTermBinding compositeTermBinding = obj as CompositeTermBinding;
			if (compositeTermBinding != null)
			{
				IList<Term> terms = compositeTermBinding.Terms;
				num = Hashing.CombineHash(num, terms.Count);
				for (int i = 0; i < terms.Count; i++)
				{
					num = Hashing.CombineHash(num, this._termComparer.GetHashCode(terms[i]));
				}
				return num;
			}
			InferredTermBinding inferredTermBinding = obj as InferredTermBinding;
			if (inferredTermBinding != null)
			{
				num = Hashing.CombineHash(num, inferredTermBinding.Type.GetHashCode());
				if (!string.IsNullOrEmpty(inferredTermBinding.DefinitionPrompt))
				{
					num = Hashing.CombineHash(num, this._valueComparer.GetHashCode(inferredTermBinding.DefinitionPrompt));
				}
				if (!string.IsNullOrEmpty(inferredTermBinding.DefinitionText))
				{
					num = Hashing.CombineHash(num, this._valueComparer.GetHashCode(inferredTermBinding.DefinitionText));
				}
				if (!string.IsNullOrEmpty(inferredTermBinding.PrefixText))
				{
					num = Hashing.CombineHash(num, this._valueComparer.GetHashCode(inferredTermBinding.PrefixText));
				}
				if (!string.IsNullOrEmpty(inferredTermBinding.SuffixText))
				{
					num = Hashing.CombineHash(num, this._valueComparer.GetHashCode(inferredTermBinding.SuffixText));
				}
				if (!string.IsNullOrEmpty(inferredTermBinding.HintText))
				{
					num = Hashing.CombineHash(num, this._valueComparer.GetHashCode(inferredTermBinding.HintText));
				}
				return num;
			}
			string text = "Unsupported binding of type ";
			Type type = obj.GetType();
			throw Contract.ExceptNotSupported(text + ((type != null) ? type.ToString() : null));
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000A410 File Offset: 0x00008610
		public static TermBindingComparer Create(StringComparer objectNameComparer, StringComparer valueComparer, TermComparer termComparer = null, bool ignoreText = false)
		{
			termComparer = termComparer ?? new TermComparer();
			TermBindingComparer termBindingComparer = new TermBindingComparer(objectNameComparer, valueComparer, ignoreText);
			termBindingComparer.SetTermComparer(termComparer);
			termComparer.SetBindingComparer(termBindingComparer);
			return termBindingComparer;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000A441 File Offset: 0x00008641
		private bool ShouldIgnoreText(TermBinding term)
		{
			return this._ignoreText || term is TableTermBinding || term is PodTermBinding || term is PropertyTermBinding;
		}

		// Token: 0x040005BE RID: 1470
		private readonly StringComparer _objectNameComparer;

		// Token: 0x040005BF RID: 1471
		private readonly StringComparer _valueComparer;

		// Token: 0x040005C0 RID: 1472
		private readonly bool _ignoreText;

		// Token: 0x040005C1 RID: 1473
		private IEqualityComparer<Term> _termComparer;
	}
}
