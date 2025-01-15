using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.FormatParsing
{
	// Token: 0x0200077F RID: 1919
	public class FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser> where TFormatPart : IFormatPart<TPartialParse, TFullParse, TSubstring, TFormatPart> where TPartialParse : IPartialParse<TFullParse, TFormatPart, TSubstring, TPartialParse>, IEquatable<TPartialParse> where TSubstring : ISubstring<TSubstring>, IEquatable<TSubstring> where TSpacerParser : SpacerFormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>
	{
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x000744F4 File Offset: 0x000726F4
		private int UniqueIdentifier
		{
			get
			{
				int nextUniqueIdentifier = this._nextUniqueIdentifier;
				this._nextUniqueIdentifier = nextUniqueIdentifier + 1;
				return nextUniqueIdentifier;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x00074512 File Offset: 0x00072712
		public Func<TSubstring, TPartialParse> EmptyPartialParseFactory { get; }

		// Token: 0x0600290B RID: 10507 RVA: 0x0007451A File Offset: 0x0007271A
		private string GetNewName()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}", new object[] { "_anonymous_component_", this.UniqueIdentifier }));
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00074547 File Offset: 0x00072747
		public FormatParserBuilder(Func<TSubstring, TPartialParse> emptyPartialParseFactory)
		{
			this.EmptyPartialParseFactory = emptyPartialParseFactory;
			this._components = new List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>();
			this._componentNamesToIndices = new Dictionary<string, int>();
			this._directionalConstraints = new List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.DirectionalConstraint>();
			this._groupConstraints = new List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.GroupConstraint>();
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00074584 File Offset: 0x00072784
		private FormatParserBuilder(Func<TSubstring, TPartialParse> emptyPartialParseFactory, IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> components, IReadOnlyDictionary<string, int> componentNamesToIndices, IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.DirectionalConstraint> directionalConstraints, IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.GroupConstraint> groupConstraints)
		{
			this.EmptyPartialParseFactory = emptyPartialParseFactory;
			this._components = components.ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>();
			this._componentNamesToIndices = componentNamesToIndices.ToDictionary<string, int>();
			this._directionalConstraints = directionalConstraints.ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.DirectionalConstraint>();
			this._groupConstraints = groupConstraints.ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.GroupConstraint>();
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x000745D0 File Offset: 0x000727D0
		internal FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser> Clone()
		{
			FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser> formatParserBuilder = new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>(this.EmptyPartialParseFactory, this._components.Select((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c) => c.Clone()), this._componentNamesToIndices, this._directionalConstraints, this._groupConstraints);
			formatParserBuilder.FixupReferences();
			return formatParserBuilder;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x0007462C File Offset: 0x0007282C
		private void FixupDirectionalConstraint(FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.DirectionalConstraint directionalConstraint)
		{
			FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent constrainedComponent = this.Resolve(directionalConstraint.ConstrainedComponent);
			directionalConstraint.IndependentComponents.Select(new Func<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>(this.Resolve)).ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>().ForEach(delegate(FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c)
			{
				c.Dependents.Add(constrainedComponent);
			});
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x00074680 File Offset: 0x00072880
		private void FixupGroupConstraint(FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.GroupConstraint groupConstraint)
		{
			List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> list = groupConstraint.ConstrainedComponents.Select(new Func<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>(this.Resolve)).ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>();
			using (List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent component = enumerator.Current;
					component.Dependents.AddRange(list.Where((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c) => c != component));
				}
			}
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x00074714 File Offset: 0x00072914
		private void FixupReferences()
		{
			foreach (FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.DirectionalConstraint directionalConstraint in this._directionalConstraints)
			{
				this.FixupDirectionalConstraint(directionalConstraint);
			}
			foreach (FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.GroupConstraint groupConstraint in this._groupConstraints)
			{
				this.FixupGroupConstraint(groupConstraint);
			}
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x000747AC File Offset: 0x000729AC
		private void CheckName(string name)
		{
			if (name.Contains("."))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Component names may not contain periods: \"{0}\"", new object[] { name })));
			}
			if (this._componentNamesToIndices.ContainsKey(name))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Redefinition of component named \"{0}\"", new object[] { name })));
			}
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x00074814 File Offset: 0x00072A14
		private FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent ResolveLocal(string name, out int index)
		{
			index = -1;
			int? indexForName = this.GetIndexForName(name);
			if (indexForName == null)
			{
				return null;
			}
			index = indexForName.Value;
			return this._components[indexForName.Value];
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x00074854 File Offset: 0x00072A54
		private int? GetIndexForName(string name)
		{
			int num;
			if (this._componentNamesToIndices.TryGetValue(name, out num))
			{
				return new int?(num);
			}
			return null;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x00074884 File Offset: 0x00072A84
		private FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution Resolve(string name)
		{
			string[] array = name.Split(new char[] { '.' });
			if (array.Length != 1)
			{
				return this.Resolve(name, array, 0, new List<int>(), new List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>());
			}
			int num;
			FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent formatParserBuilderComponent = this.ResolveLocal(name, out num);
			if (formatParserBuilderComponent == null)
			{
				return null;
			}
			return new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution(name, formatParserBuilderComponent, new int[] { num }, new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent[] { formatParserBuilderComponent });
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x000748E8 File Offset: 0x00072AE8
		private FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution Resolve(string fullName, string[] nameParts, int currentIndex, List<int> accumulatedIndices, List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> accumulatedComponents)
		{
			int num;
			FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent formatParserBuilderComponent = this.ResolveLocal(nameParts[currentIndex], out num);
			if (formatParserBuilderComponent == null)
			{
				return null;
			}
			accumulatedIndices.Add(num);
			accumulatedComponents.Add(formatParserBuilderComponent);
			if (currentIndex == nameParts.Length - 1)
			{
				return new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution(fullName, formatParserBuilderComponent, accumulatedIndices, accumulatedComponents);
			}
			FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.BuilderBasedComponent builderBasedComponent = formatParserBuilderComponent as FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.BuilderBasedComponent;
			if (builderBasedComponent != null)
			{
				return builderBasedComponent.ContainedBuilder.Resolve(fullName, nameParts, currentIndex + 1, accumulatedIndices, accumulatedComponents);
			}
			return null;
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x0007494C File Offset: 0x00072B4C
		private FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent Resolve(IReadOnlyList<int> indices)
		{
			if (indices[0] >= this._components.Count)
			{
				return null;
			}
			FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent formatParserBuilderComponent = this._components[indices[0]];
			for (int i = 1; i < indices.Count; i++)
			{
				FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.BuilderBasedComponent builderBasedComponent = formatParserBuilderComponent as FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.BuilderBasedComponent;
				if (builderBasedComponent == null || indices[i] >= builderBasedComponent.ContainedBuilder._components.Count)
				{
					return null;
				}
				formatParserBuilderComponent = builderBasedComponent.ContainedBuilder._components[indices[i]];
			}
			return formatParserBuilderComponent;
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x000749D3 File Offset: 0x00072BD3
		private string CheckOrCreateName(string name)
		{
			if (name != null)
			{
				this.CheckName(name);
				return name;
			}
			return this.GetNewName();
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x000749E8 File Offset: 0x00072BE8
		private void Append(FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent component)
		{
			int count = this._components.Count;
			this._componentNamesToIndices[component.Name] = count;
			this._components.Add(component);
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x00074A20 File Offset: 0x00072C20
		public void Append(TFormatPart formatPart, string componentName = null, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null, bool isPermutable = false, int minRepetitions = 1, int maxRepetitions = 1)
		{
			this.Append(new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBasedComponent(this.EmptyPartialParseFactory, this.CheckOrCreateName(componentName), new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.AtomicFormatParser(this.EmptyPartialParseFactory, formatPart, filterPredicate, null, null, 1, 1, null), isPermutable, minRepetitions, maxRepetitions));
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x00074A68 File Offset: 0x00072C68
		public void Append(FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser> builder, string componentName = null, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null, bool isPermutable = false, int minRepetitions = 1, int maxRepetitions = 1)
		{
			if (builder.EndsWithSpacer)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("The builder to be appended ends with a spacer. This is forbidden.", Array.Empty<object>())));
			}
			this.Append(new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.BuilderBasedComponent(this.EmptyPartialParseFactory, this.CheckOrCreateName(componentName), builder, filterPredicate, isPermutable, minRepetitions, maxRepetitions));
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x00074AB8 File Offset: 0x00072CB8
		public void AppendUnion(IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>> formatParsersToUnion, IEnumerable<TFormatPart> formatPartsToUnion, string componentName = null, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null, bool isPermutable = false, int minRepetitions = 1, int maxRepetitions = 1)
		{
			this.Append(new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.UnionComponent(this.EmptyPartialParseFactory, this.CheckOrCreateName(componentName), formatPartsToUnion.Select((TFormatPart p) => new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBasedComponent(this.EmptyPartialParseFactory, this.GetNewName(), new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.AtomicFormatParser(this.EmptyPartialParseFactory, p, null, null, null, 1, 1, null), false, 1, 1)).Cast<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>().Concat(formatParsersToUnion.Select((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser> builder) => new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.BuilderBasedComponent(this.EmptyPartialParseFactory, this.GetNewName(), builder, null, false, 1, 1))), filterPredicate, isPermutable, minRepetitions, maxRepetitions));
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x00074B13 File Offset: 0x00072D13
		public void AppendUnion(IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>> formatParsersToUnion, string componentName = null, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null, bool isPermutable = false, int minRepetitions = 1, int maxRepetitions = 1)
		{
			this.AppendUnion(formatParsersToUnion, Enumerable.Empty<TFormatPart>(), componentName, filterPredicate, isPermutable, minRepetitions, maxRepetitions);
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x00074B29 File Offset: 0x00072D29
		public void AppendUnion(IEnumerable<TFormatPart> formatPartsToUnion, string componentName = null, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null, bool isPermutable = false, int minRepetitions = 1, int maxRepetitions = 1)
		{
			this.AppendUnion(Enumerable.Empty<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>>(), formatPartsToUnion, componentName, filterPredicate, isPermutable, minRepetitions, maxRepetitions);
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x00074B3F File Offset: 0x00072D3F
		public bool EndsWithSpacer
		{
			get
			{
				return this._components.Count > 0 && this._components.Last<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>() is FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.SpacerComponent;
			}
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x00074B64 File Offset: 0x00072D64
		public void AppendSpacer(string componentName, TSpacerParser spacerParser, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate = null)
		{
			if (this._components.Count == 0)
			{
				throw new InvalidOperationException("A spacer cannot be the first component in a FormatParser.");
			}
			if (this.EndsWithSpacer)
			{
				throw new InvalidOperationException("Cannot have two spacers in succession.");
			}
			this.Append(new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.SpacerComponent(this.EmptyPartialParseFactory, this.CheckOrCreateName(componentName), spacerParser, filterPredicate));
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x00074BB6 File Offset: 0x00072DB6
		public void AssertDirectionalConstraint(string dependentComponentName, Func<TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> dependencyConstraintChecker, params string[] independentComponents)
		{
			this.AssertDirectionalConstraint(dependentComponentName, dependencyConstraintChecker, independentComponents);
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x00074BC1 File Offset: 0x00072DC1
		public void AssertGroupConstraint(Func<int, TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> dependencyConstraintChecker, params string[] componentNames)
		{
			this.AssertGroupConstraint(dependencyConstraintChecker, componentNames);
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x00074BCC File Offset: 0x00072DCC
		private bool AreComponentsNested(IEnumerable<string> componentNames)
		{
			foreach (Record<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution> record in ((componentNames as IReadOnlyList<string>) ?? componentNames.ToList<string>()).Select(new Func<string, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution>(this.Resolve)).ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution>().UnorderedPairs(false))
			{
				FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution item = record.Item1;
				FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution item2 = record.Item2;
				Optional<Discrepancy<int>> optional = DiscrepancyUtil.FirstDiscrepancy<int>(item.RelativePath, item2.RelativePath, null);
				if (optional.HasValue && (optional.Value.LeftListIsPrefixOfRight || optional.Value.RightListIsPrefixOfLeft))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x00074C88 File Offset: 0x00072E88
		private bool AreComponentsNested(string dependentComponentName, IEnumerable<string> independentComponentNames)
		{
			IEnumerable<string> enumerable = (independentComponentNames as IReadOnlyList<string>) ?? independentComponentNames.ToList<string>();
			FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution nameResolution = this.Resolve(dependentComponentName);
			foreach (FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution nameResolution2 in enumerable.Select(new Func<string, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution>(this.Resolve)).ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution>())
			{
				Optional<Discrepancy<int>> optional = DiscrepancyUtil.FirstDiscrepancy<int>(nameResolution.RelativePath, nameResolution2.RelativePath, null);
				if (optional.HasValue && (optional.Value.LeftListIsPrefixOfRight || optional.Value.RightListIsPrefixOfLeft))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x00074D40 File Offset: 0x00072F40
		public void AssertDirectionalConstraint(string dependentComponentName, Func<TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> dependencyConstraintChecker, IEnumerable<string> independentComponents)
		{
			List<string> list = independentComponents.ToList<string>();
			if (!list.Any<string>())
			{
				throw new ArgumentException("Cannot have empty list of independent component names for constraint");
			}
			if (this.AreComponentsNested(dependentComponentName, list))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Constraints cannot be asserted between nested components.", Array.Empty<object>())));
			}
			FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution dependentComponentResolution = this.Resolve(dependentComponentName);
			List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution> list2 = list.Select(new Func<string, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution>(this.Resolve)).ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution>();
			if (dependentComponentResolution == null)
			{
				FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.ThrowResolutionError(dependentComponentName);
				return;
			}
			int num = list2.FindIndex((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution r) => r == null);
			if (num >= 0)
			{
				FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.ThrowResolutionError(list[num]);
			}
			list2.ForEach(delegate(FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution c)
			{
				c.ResolvedComponent.Dependents.Add(dependentComponentResolution.ResolvedComponent);
			});
			FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.DirectionalConstraint directionalConstraint = new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.DirectionalConstraint(dependentComponentResolution.RelativePath, dependencyConstraintChecker, list2.Select((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution c) => c.RelativePath));
			this._directionalConstraints.Add(directionalConstraint);
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x00074E52 File Offset: 0x00073052
		private static void ThrowResolutionError(string name)
		{
			throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Could not resolve name \"{0}\"", new object[] { name })));
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x00074E74 File Offset: 0x00073074
		public void AssertGroupConstraint(Func<int, TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> dependencyConstraintChecker, IEnumerable<string> componentNames)
		{
			List<string> list = componentNames.ToList<string>();
			if (!list.Any<string>())
			{
				throw new ArgumentException("Cannot have empty list of component names for constraint.");
			}
			if (this.AreComponentsNested(list))
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Constraints cannot be asserted between nested components.", Array.Empty<object>())));
			}
			List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution> list2 = list.Select(new Func<string, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution>(this.Resolve)).ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution>();
			int num = list2.FindIndex((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution r) => r == null);
			if (num >= 0)
			{
				FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.ThrowResolutionError(list[num]);
			}
			using (List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution>.Enumerator enumerator = list2.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution resolvedComponent = enumerator.Current;
					resolvedComponent.ResolvedComponent.Dependents.AddRange(from c in list2
						where c != resolvedComponent
						select c.ResolvedComponent);
				}
			}
			FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.GroupConstraint groupConstraint = new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.GroupConstraint(list2.Select((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.NameResolution c) => c.RelativePath), dependencyConstraintChecker);
			this._groupConstraints.Add(groupConstraint);
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x00074FDC File Offset: 0x000731DC
		private IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> AllComponents
		{
			get
			{
				return this._components.SelectMany((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c) => c.AllComponents);
			}
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x00075008 File Offset: 0x00073208
		public FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> Build()
		{
			int num = 0;
			this.AssignMatchStoreIndices(ref num);
			return this.BuildFormatParser();
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x00075028 File Offset: 0x00073228
		private FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> BuildForSequence(IReadOnlyList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> sequence)
		{
			List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>> list = new List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>>();
			foreach (FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent formatParserBuilderComponent in sequence)
			{
				list.Add(formatParserBuilderComponent.BuildFormatSet());
			}
			return new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.SequenceFormatParser(this.EmptyPartialParseFactory, list, null, null, null, 1, 1, null);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00075098 File Offset: 0x00073298
		private FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> ExtendPermutationSequence(int currentIndex, int indexOfFirstComponent, ImmutableList<int> remaningPermutableIndices)
		{
			List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> list = new List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> { this._components[indexOfFirstComponent] };
			int num = currentIndex + 1;
			while (num < this._components.Count && !this._components[num].IsPermutable)
			{
				list.Add(this._components[num]);
				num++;
			}
			if (remaningPermutableIndices.Count == 1)
			{
				list.Add(this._components[remaningPermutableIndices.Single<int>()]);
				list.AddRange(this._components.Skip(num + 1));
			}
			else if (num < this._components.Count)
			{
				list.Add(new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBasedComponent(this.EmptyPartialParseFactory, this.GetNewName(), this.BuildUnion(num, remaningPermutableIndices), false, 1, 1));
			}
			return this.BuildForSequence(list);
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x00075168 File Offset: 0x00073368
		private FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> BuildUnion(int firstPermutableIndex, ImmutableList<int> remaningPermutableIndices)
		{
			List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>> list = new List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>>();
			foreach (int num in remaningPermutableIndices)
			{
				list.Add(this.ExtendPermutationSequence(firstPermutableIndex, num, remaningPermutableIndices.Remove(num)));
			}
			return new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.UnionFormatParser(this.EmptyPartialParseFactory, list, null, null, null, 1, 1, null);
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x000751E4 File Offset: 0x000733E4
		internal FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> BuildFormatParser()
		{
			this.AssertConstraintsInComponents();
			if (this._components.Count == 0)
			{
				throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("{0} was called on an empty {1}", new object[]
				{
					"BuildFormatParser",
					base.GetType()
				})));
			}
			if (this._components.Count((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c) => c.IsPermutable) <= 1)
			{
				return this.BuildForSequence(this._components);
			}
			ImmutableList<int> immutableList = (from idx in Enumerable.Range(0, this._components.Count)
				where this._components[idx].IsPermutable
				select idx).ToImmutableList<int>();
			if (!this._components[0].IsPermutable)
			{
				return this.ExtendPermutationSequence(0, 0, immutableList);
			}
			return this.BuildUnion(0, immutableList);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x000752B8 File Offset: 0x000734B8
		private void AssignMatchStoreIndices(ref int nextId)
		{
			foreach (FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent formatParserBuilderComponent in this._components)
			{
				formatParserBuilderComponent.AssignMatchStoreIndices(ref nextId);
			}
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x0007530C File Offset: 0x0007350C
		private void AssertConstraintsInComponents()
		{
			foreach (FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.DirectionalConstraint directionalConstraint in this._directionalConstraints)
			{
				FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent formatParserBuilderComponent = this.Resolve(directionalConstraint.ConstrainedComponent);
				List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> list = directionalConstraint.IndependentComponents.Select(new Func<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>(this.Resolve)).ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>();
				formatParserBuilderComponent.DirectionalConstraints.Add(new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint(list.Select(delegate(FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c)
				{
					int? matchStoreIndex = c.MatchStoreIndex;
					if (matchStoreIndex == null)
					{
						throw new InvalidOperationException();
					}
					return matchStoreIndex.GetValueOrDefault();
				}), directionalConstraint.ConstraintChecker));
			}
			foreach (FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.GroupConstraint groupConstraint in this._groupConstraints)
			{
				List<int> list2 = groupConstraint.ConstrainedComponents.Select(new Func<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>(this.Resolve)).Select(delegate(FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c)
				{
					int? matchStoreIndex2 = c.MatchStoreIndex;
					if (matchStoreIndex2 == null)
					{
						throw new InvalidOperationException();
					}
					return matchStoreIndex2.GetValueOrDefault();
				}).ToList<int>();
				foreach (FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath relativePath in groupConstraint.ConstrainedComponents)
				{
					this.Resolve(relativePath).GroupConstraints.Add(new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint(list2, groupConstraint.ConstraintChecker));
				}
			}
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x0007549C File Offset: 0x0007369C
		public void Clear()
		{
			this._components.Clear();
			this._componentNamesToIndices.Clear();
			this._directionalConstraints.Clear();
			this._groupConstraints.Clear();
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000754CC File Offset: 0x000736CC
		public override string ToString()
		{
			string text = "Builder({0})";
			object[] array = new object[1];
			array[0] = string.Join(", ", this._components.Select((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c) => c.ToString()));
			return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
		}

		// Token: 0x04001406 RID: 5126
		private readonly List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> _components;

		// Token: 0x04001407 RID: 5127
		private readonly Dictionary<string, int> _componentNamesToIndices;

		// Token: 0x04001408 RID: 5128
		private readonly List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.DirectionalConstraint> _directionalConstraints;

		// Token: 0x04001409 RID: 5129
		private readonly List<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.GroupConstraint> _groupConstraints;

		// Token: 0x0400140A RID: 5130
		private int _nextUniqueIdentifier;

		// Token: 0x0400140B RID: 5131
		private const string DefaultNamePrefix = "_anonymous_component_";

		// Token: 0x02000780 RID: 1920
		internal abstract class FormatParserBuilderComponent
		{
			// Token: 0x17000725 RID: 1829
			// (get) Token: 0x06002935 RID: 10549 RVA: 0x0007558E File Offset: 0x0007378E
			public string Name { get; }

			// Token: 0x17000726 RID: 1830
			// (get) Token: 0x06002936 RID: 10550 RVA: 0x00075596 File Offset: 0x00073796
			public bool IsPermutable { get; }

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x06002937 RID: 10551 RVA: 0x0007559E File Offset: 0x0007379E
			// (set) Token: 0x06002938 RID: 10552 RVA: 0x000755A6 File Offset: 0x000737A6
			public int? MatchStoreIndex { get; private set; }

			// Token: 0x17000728 RID: 1832
			// (get) Token: 0x06002939 RID: 10553 RVA: 0x000755AF File Offset: 0x000737AF
			public int MinRepetitions { get; }

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x0600293A RID: 10554 RVA: 0x000755B7 File Offset: 0x000737B7
			public int MaxRepetitions { get; }

			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x0600293B RID: 10555 RVA: 0x000755BF File Offset: 0x000737BF
			public HashSet<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> Dependents { get; }

			// Token: 0x1700072B RID: 1835
			// (get) Token: 0x0600293C RID: 10556 RVA: 0x000755C7 File Offset: 0x000737C7
			public Func<TSubstring, TPartialParse> EmptyPartialParseFactory { get; }

			// Token: 0x1700072C RID: 1836
			// (get) Token: 0x0600293D RID: 10557 RVA: 0x000755CF File Offset: 0x000737CF
			internal List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint> DirectionalConstraints { get; }

			// Token: 0x1700072D RID: 1837
			// (get) Token: 0x0600293E RID: 10558 RVA: 0x000755D7 File Offset: 0x000737D7
			internal List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint> GroupConstraints { get; }

			// Token: 0x1700072E RID: 1838
			// (get) Token: 0x0600293F RID: 10559 RVA: 0x000755DF File Offset: 0x000737DF
			public Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> FilterPredicate { get; }

			// Token: 0x06002940 RID: 10560 RVA: 0x000755E8 File Offset: 0x000737E8
			protected FormatParserBuilderComponent(Func<TSubstring, TPartialParse> emptyPartialParseFactory, string name, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate, bool isPermutable, int minRepetitions, int maxRepetitions)
			{
				if (minRepetitions > maxRepetitions || minRepetitions < 0 || maxRepetitions < 0)
				{
					throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("{0} <= {1}, and neither of them can be negative.", new object[] { "minRepetitions", "maxRepetitions" })));
				}
				this.EmptyPartialParseFactory = emptyPartialParseFactory;
				this.Name = name;
				this.FilterPredicate = filterPredicate;
				this.IsPermutable = isPermutable;
				this.Dependents = new HashSet<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>(IdentityEquality.Comparer);
				this.MinRepetitions = minRepetitions;
				this.MaxRepetitions = maxRepetitions;
				this.DirectionalConstraints = new List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.DirectionalConstraint>();
				this.GroupConstraints = new List<FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.GroupConstraint>();
			}

			// Token: 0x06002941 RID: 10561
			public abstract FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent Clone();

			// Token: 0x1700072F RID: 1839
			// (get) Token: 0x06002942 RID: 10562
			public abstract IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> AllComponents { get; }

			// Token: 0x06002943 RID: 10563 RVA: 0x0007568C File Offset: 0x0007388C
			public virtual void AssignMatchStoreIndices(ref int nextId)
			{
				if (this.Dependents.Any<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>())
				{
					int num = nextId;
					nextId = num + 1;
					this.MatchStoreIndex = new int?(num);
				}
			}

			// Token: 0x06002944 RID: 10564
			public abstract FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> BuildFormatSet();

			// Token: 0x06002945 RID: 10565
			public abstract override string ToString();
		}

		// Token: 0x02000781 RID: 1921
		private class FormatParserBasedComponent : FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent
		{
			// Token: 0x17000730 RID: 1840
			// (get) Token: 0x06002946 RID: 10566 RVA: 0x000756BA File Offset: 0x000738BA
			public FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> FormatParser { get; }

			// Token: 0x06002947 RID: 10567 RVA: 0x000756C2 File Offset: 0x000738C2
			public FormatParserBasedComponent(Func<TSubstring, TPartialParse> emptyPartialParseFactory, string name, FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> formatParser, bool isPermutable, int minRepetitions, int maxRepetitions)
				: base(emptyPartialParseFactory, name, formatParser.FilterPredicate, isPermutable, minRepetitions, maxRepetitions)
			{
				this.FormatParser = formatParser;
			}

			// Token: 0x06002948 RID: 10568 RVA: 0x000756DF File Offset: 0x000738DF
			public override FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent Clone()
			{
				return new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBasedComponent(base.EmptyPartialParseFactory, base.Name, this.FormatParser, base.IsPermutable, base.MinRepetitions, base.MaxRepetitions);
			}

			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x06002949 RID: 10569 RVA: 0x0007570A File Offset: 0x0007390A
			public override IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> AllComponents
			{
				get
				{
					return this.Yield<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBasedComponent>();
				}
			}

			// Token: 0x0600294A RID: 10570 RVA: 0x00075712 File Offset: 0x00073912
			public override FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> BuildFormatSet()
			{
				return this.FormatParser.Clone(base.MatchStoreIndex, base.FilterPredicate, base.DirectionalConstraints, base.GroupConstraints, base.MinRepetitions, base.MaxRepetitions);
			}

			// Token: 0x0600294B RID: 10571 RVA: 0x00075743 File Offset: 0x00073943
			public override string ToString()
			{
				return this.FormatParser.ToString();
			}
		}

		// Token: 0x02000782 RID: 1922
		private class BuilderBasedComponent : FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent
		{
			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x0600294C RID: 10572 RVA: 0x00075750 File Offset: 0x00073950
			public FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser> ContainedBuilder { get; }

			// Token: 0x0600294D RID: 10573 RVA: 0x00075758 File Offset: 0x00073958
			public BuilderBasedComponent(Func<TSubstring, TPartialParse> emptyPartialParseFactory, string name, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser> containedBuilder, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate, bool isPermutable, int minRepetitions, int maxRepetitions)
				: base(emptyPartialParseFactory, name, filterPredicate, isPermutable, minRepetitions, maxRepetitions)
			{
				this.ContainedBuilder = containedBuilder.Clone();
			}

			// Token: 0x0600294E RID: 10574 RVA: 0x00075776 File Offset: 0x00073976
			public override FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent Clone()
			{
				return new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.BuilderBasedComponent(base.EmptyPartialParseFactory, base.Name, this.ContainedBuilder, base.FilterPredicate, base.IsPermutable, base.MinRepetitions, base.MaxRepetitions);
			}

			// Token: 0x17000733 RID: 1843
			// (get) Token: 0x0600294F RID: 10575 RVA: 0x000757A7 File Offset: 0x000739A7
			public override IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> AllComponents
			{
				get
				{
					return this.ContainedBuilder.AllComponents;
				}
			}

			// Token: 0x06002950 RID: 10576 RVA: 0x000757B4 File Offset: 0x000739B4
			public override FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> BuildFormatSet()
			{
				return this.ContainedBuilder.BuildFormatParser().Clone(base.MatchStoreIndex, base.FilterPredicate, base.DirectionalConstraints, base.GroupConstraints, base.MinRepetitions, base.MaxRepetitions);
			}

			// Token: 0x06002951 RID: 10577 RVA: 0x000757EA File Offset: 0x000739EA
			public override void AssignMatchStoreIndices(ref int nextId)
			{
				base.AssignMatchStoreIndices(ref nextId);
				this.ContainedBuilder.AssignMatchStoreIndices(ref nextId);
			}

			// Token: 0x06002952 RID: 10578 RVA: 0x000757FF File Offset: 0x000739FF
			public override string ToString()
			{
				return this.ContainedBuilder.ToString();
			}
		}

		// Token: 0x02000783 RID: 1923
		private class UnionComponent : FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent
		{
			// Token: 0x17000734 RID: 1844
			// (get) Token: 0x06002953 RID: 10579 RVA: 0x0007580C File Offset: 0x00073A0C
			public IReadOnlyList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> UnionedComponents { get; }

			// Token: 0x06002954 RID: 10580 RVA: 0x00075814 File Offset: 0x00073A14
			public UnionComponent(Func<TSubstring, TPartialParse> emptyPartialParseFactory, string name, IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> components, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate, bool isPermutable, int minRepetitions, int maxRepetitions)
				: base(emptyPartialParseFactory, name, filterPredicate, isPermutable, minRepetitions, maxRepetitions)
			{
				this.UnionedComponents = components.ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent>();
			}

			// Token: 0x17000735 RID: 1845
			// (get) Token: 0x06002955 RID: 10581 RVA: 0x00075832 File Offset: 0x00073A32
			public override IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> AllComponents
			{
				get
				{
					return this.UnionedComponents.SelectMany((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c) => c.AllComponents);
				}
			}

			// Token: 0x06002956 RID: 10582 RVA: 0x00075860 File Offset: 0x00073A60
			public override FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent Clone()
			{
				return new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.UnionComponent(base.EmptyPartialParseFactory, base.Name, this.UnionedComponents.Select((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c) => c.Clone()), base.FilterPredicate, base.IsPermutable, base.MinRepetitions, base.MaxRepetitions);
			}

			// Token: 0x06002957 RID: 10583 RVA: 0x000758C0 File Offset: 0x00073AC0
			public override FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> BuildFormatSet()
			{
				return new FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring>.UnionFormatParser(base.EmptyPartialParseFactory, this.UnionedComponents.Select((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c) => c.BuildFormatSet()), base.FilterPredicate, base.DirectionalConstraints, base.GroupConstraints, base.MinRepetitions, base.MaxRepetitions, base.MatchStoreIndex);
			}

			// Token: 0x06002958 RID: 10584 RVA: 0x00075928 File Offset: 0x00073B28
			public override void AssignMatchStoreIndices(ref int nextId)
			{
				base.AssignMatchStoreIndices(ref nextId);
				foreach (FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent formatParserBuilderComponent in this.UnionedComponents)
				{
					formatParserBuilderComponent.AssignMatchStoreIndices(ref nextId);
				}
			}

			// Token: 0x06002959 RID: 10585 RVA: 0x0007597C File Offset: 0x00073B7C
			public override string ToString()
			{
				string text = "Union({0})";
				object[] array = new object[1];
				array[0] = string.Join(", ", this.UnionedComponents.Select((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent c) => c.ToString()));
				return FormattableString.Invariant(FormattableStringFactory.Create(text, array));
			}
		}

		// Token: 0x02000785 RID: 1925
		private class SpacerComponent : FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent
		{
			// Token: 0x17000736 RID: 1846
			// (get) Token: 0x06002960 RID: 10592 RVA: 0x000759F9 File Offset: 0x00073BF9
			public TSpacerParser SpacerParser { get; }

			// Token: 0x06002961 RID: 10593 RVA: 0x00075A01 File Offset: 0x00073C01
			public SpacerComponent(Func<TSubstring, TPartialParse> emptyPartialParseFactory, string name, TSpacerParser spacerParser, Predicate<DeltaFormatMatchState<TPartialParse, TFullParse, TFormatPart, TSubstring>> filterPredicate)
				: base(emptyPartialParseFactory, name, filterPredicate, false, 1, 1)
			{
				this.SpacerParser = spacerParser;
			}

			// Token: 0x06002962 RID: 10594 RVA: 0x00075A18 File Offset: 0x00073C18
			public override FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent Clone()
			{
				return new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.SpacerComponent(base.EmptyPartialParseFactory, base.Name, (TSpacerParser)((object)this.SpacerParser.Clone(null, null, null, null, 1, 1)), base.FilterPredicate);
			}

			// Token: 0x17000737 RID: 1847
			// (get) Token: 0x06002963 RID: 10595 RVA: 0x00075A5F File Offset: 0x00073C5F
			public override IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> AllComponents
			{
				get
				{
					return this.Yield<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.SpacerComponent>();
				}
			}

			// Token: 0x06002964 RID: 10596 RVA: 0x00075A67 File Offset: 0x00073C67
			public override FormatParser<TFormatPart, TPartialParse, TFullParse, TSubstring> BuildFormatSet()
			{
				return this.SpacerParser.Clone(base.MatchStoreIndex, base.FilterPredicate, base.DirectionalConstraints, base.GroupConstraints, base.MinRepetitions, base.MaxRepetitions);
			}

			// Token: 0x06002965 RID: 10597 RVA: 0x00073F2F File Offset: 0x0007212F
			public override string ToString()
			{
				return "Spacer";
			}
		}

		// Token: 0x02000786 RID: 1926
		private class RelativePath : IReadOnlyList<int>, IReadOnlyCollection<int>, IEnumerable<int>, IEnumerable, IEquatable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath>
		{
			// Token: 0x06002966 RID: 10598 RVA: 0x00075A9D File Offset: 0x00073C9D
			public RelativePath(IEnumerable<int> indices)
			{
				this._indices = indices.ToList<int>();
				this._hashCode = null;
			}

			// Token: 0x06002967 RID: 10599 RVA: 0x00075ABD File Offset: 0x00073CBD
			public bool Equals(FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath other)
			{
				return other == this || (other != null && this._indices.SequenceEqual(other._indices));
			}

			// Token: 0x06002968 RID: 10600 RVA: 0x00075ADB File Offset: 0x00073CDB
			public IEnumerator<int> GetEnumerator()
			{
				return this._indices.GetEnumerator();
			}

			// Token: 0x06002969 RID: 10601 RVA: 0x00075AE8 File Offset: 0x00073CE8
			public override bool Equals(object obj)
			{
				return obj == this || (obj != null && !(obj.GetType() != base.GetType()) && this.Equals((FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath)obj));
			}

			// Token: 0x0600296A RID: 10602 RVA: 0x00075B16 File Offset: 0x00073D16
			public override int GetHashCode()
			{
				if (this._hashCode == null)
				{
					this._hashCode = new int?((this.OrderDependentHashCode<int>() * 89303) ^ base.GetType().GetHashCode());
				}
				return this._hashCode.Value;
			}

			// Token: 0x0600296B RID: 10603 RVA: 0x00075B53 File Offset: 0x00073D53
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x17000738 RID: 1848
			public int this[int key]
			{
				get
				{
					return this._indices[key];
				}
			}

			// Token: 0x17000739 RID: 1849
			// (get) Token: 0x0600296D RID: 10605 RVA: 0x00075B69 File Offset: 0x00073D69
			public int Count
			{
				get
				{
					return this._indices.Count;
				}
			}

			// Token: 0x04001420 RID: 5152
			private int? _hashCode;

			// Token: 0x04001421 RID: 5153
			private readonly IReadOnlyList<int> _indices;
		}

		// Token: 0x02000787 RID: 1927
		private class NameResolution
		{
			// Token: 0x1700073A RID: 1850
			// (get) Token: 0x0600296E RID: 10606 RVA: 0x00075B76 File Offset: 0x00073D76
			public string ResolvedName { get; }

			// Token: 0x1700073B RID: 1851
			// (get) Token: 0x0600296F RID: 10607 RVA: 0x00075B7E File Offset: 0x00073D7E
			public FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent ResolvedComponent { get; }

			// Token: 0x1700073C RID: 1852
			// (get) Token: 0x06002970 RID: 10608 RVA: 0x00075B86 File Offset: 0x00073D86
			public FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath RelativePath { get; }

			// Token: 0x1700073D RID: 1853
			// (get) Token: 0x06002971 RID: 10609 RVA: 0x00075B8E File Offset: 0x00073D8E
			public IReadOnlyList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> ComponentsTraversed { get; }

			// Token: 0x06002972 RID: 10610 RVA: 0x00075B96 File Offset: 0x00073D96
			public NameResolution(string resolvedName, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent resolvedComponent, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath relativePath, IReadOnlyList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> componentsTraversed)
			{
				this.ResolvedName = resolvedName;
				this.ResolvedComponent = resolvedComponent;
				this.RelativePath = relativePath;
				this.ComponentsTraversed = componentsTraversed;
			}

			// Token: 0x06002973 RID: 10611 RVA: 0x00075BBB File Offset: 0x00073DBB
			public NameResolution(string resolvedName, FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent resolvedComponent, IEnumerable<int> indicesTraversed, IReadOnlyList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.FormatParserBuilderComponent> componentsTraversed)
				: this(resolvedName, resolvedComponent, new FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath(indicesTraversed), componentsTraversed)
			{
			}
		}

		// Token: 0x02000788 RID: 1928
		private class DirectionalConstraint
		{
			// Token: 0x1700073E RID: 1854
			// (get) Token: 0x06002974 RID: 10612 RVA: 0x00075BCD File Offset: 0x00073DCD
			public FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath ConstrainedComponent { get; }

			// Token: 0x1700073F RID: 1855
			// (get) Token: 0x06002975 RID: 10613 RVA: 0x00075BD5 File Offset: 0x00073DD5
			public IReadOnlyList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath> IndependentComponents { get; }

			// Token: 0x17000740 RID: 1856
			// (get) Token: 0x06002976 RID: 10614 RVA: 0x00075BDD File Offset: 0x00073DDD
			public Func<TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> ConstraintChecker { get; }

			// Token: 0x06002977 RID: 10615 RVA: 0x00075BE5 File Offset: 0x00073DE5
			public DirectionalConstraint(FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath constrainedComponent, Func<TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> constraintChecker, IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath> independentComponents)
			{
				this.ConstrainedComponent = constrainedComponent;
				this.ConstraintChecker = constraintChecker;
				this.IndependentComponents = independentComponents.ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath>();
			}
		}

		// Token: 0x02000789 RID: 1929
		private class GroupConstraint
		{
			// Token: 0x17000741 RID: 1857
			// (get) Token: 0x06002978 RID: 10616 RVA: 0x00075C07 File Offset: 0x00073E07
			public IReadOnlyList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath> ConstrainedComponents { get; }

			// Token: 0x17000742 RID: 1858
			// (get) Token: 0x06002979 RID: 10617 RVA: 0x00075C0F File Offset: 0x00073E0F
			public Func<int, TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> ConstraintChecker { get; }

			// Token: 0x0600297A RID: 10618 RVA: 0x00075C17 File Offset: 0x00073E17
			public GroupConstraint(IEnumerable<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath> constrainedComponents, Func<int, TPartialParse, IReadOnlyList<Optional<TPartialParse>>, bool> constraintChecker)
			{
				this.ConstrainedComponents = constrainedComponents.ToList<FormatParserBuilder<TFormatPart, TPartialParse, TFullParse, TSubstring, TSpacerParser>.RelativePath>();
				this.ConstraintChecker = constraintChecker;
			}
		}
	}
}
