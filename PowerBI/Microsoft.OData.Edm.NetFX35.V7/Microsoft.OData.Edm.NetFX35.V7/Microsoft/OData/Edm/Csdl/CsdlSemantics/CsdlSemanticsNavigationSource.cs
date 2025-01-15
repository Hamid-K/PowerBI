using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000166 RID: 358
	internal abstract class CsdlSemanticsNavigationSource : CsdlSemanticsElement, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x0600094B RID: 2379 RVA: 0x00019DC8 File Offset: 0x00017FC8
		public CsdlSemanticsNavigationSource(CsdlSemanticsEntityContainer container, CsdlAbstractNavigationSource navigationSource)
			: base(navigationSource)
		{
			this.container = container;
			this.navigationSource = navigationSource;
			this.path = new EdmPathExpression(this.navigationSource.Name);
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x00019E2C File Offset: 0x0001802C
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.container.Model;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x00019E39 File Offset: 0x00018039
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00019E41 File Offset: 0x00018041
		public override CsdlElement Element
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00019E49 File Offset: 0x00018049
		public string Name
		{
			get
			{
				return this.navigationSource.Name;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00019E56 File Offset: 0x00018056
		public IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000951 RID: 2385
		public abstract IEdmType Type { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000952 RID: 2386
		public abstract EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x00019E5E File Offset: 0x0001805E
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return this.navigationTargetsCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeNavigationTargetsFunc, null);
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00019E74 File Offset: 0x00018074
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property, IEdmPathExpression bindingPath)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			if (!property.ContainsTarget && bindingPath != null)
			{
				using (IEnumerator<IEdmNavigationPropertyBinding> enumerator = this.NavigationPropertyBindings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IEdmNavigationPropertyBinding edmNavigationPropertyBinding = enumerator.Current;
						if (edmNavigationPropertyBinding.NavigationProperty == property && edmNavigationPropertyBinding.Path.Path == bindingPath.Path)
						{
							return edmNavigationPropertyBinding.Target;
						}
					}
					goto IL_008B;
				}
			}
			if (property.ContainsTarget)
			{
				return EdmUtil.DictionaryGetOrUpdate<IEdmNavigationProperty, IEdmContainedEntitySet>(this.containedNavigationPropertyCache, property, (IEdmNavigationProperty navProperty) => new EdmContainedEntitySet(this, navProperty));
			}
			IL_008B:
			return EdmUtil.DictionaryGetOrUpdate<IEdmNavigationProperty, IEdmUnknownEntitySet>(this.unknownNavigationPropertyCache, property, (IEdmNavigationProperty navProperty) => new EdmUnknownEntitySet(this, navProperty));
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00019F38 File Offset: 0x00018138
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty)
		{
			IEdmPathExpression edmPathExpression = ((!this.Type.AsElementType().IsOrInheritsFrom(navigationProperty.DeclaringType)) ? new EdmPathExpression(new string[]
			{
				navigationProperty.DeclaringType.FullTypeName(),
				navigationProperty.Name
			}) : new EdmPathExpression(navigationProperty.Name));
			return this.FindNavigationTarget(navigationProperty, edmPathExpression);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00019F9C File Offset: 0x0001819C
		public IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			if (!navigationProperty.ContainsTarget)
			{
				return Enumerable.ToList<IEdmNavigationPropertyBinding>(Enumerable.Where<IEdmNavigationPropertyBinding>(this.NavigationPropertyBindings, (IEdmNavigationPropertyBinding targetMapping) => targetMapping.NavigationProperty == navigationProperty));
			}
			return null;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00019FE1 File Offset: 0x000181E1
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.container.Context);
		}

		// Token: 0x06000958 RID: 2392
		protected abstract IEdmEntityType ComputeElementType();

		// Token: 0x06000959 RID: 2393 RVA: 0x00019FFA File Offset: 0x000181FA
		private IEnumerable<IEdmNavigationPropertyBinding> ComputeNavigationTargets()
		{
			return Enumerable.ToList<IEdmNavigationPropertyBinding>(Enumerable.Select<CsdlNavigationPropertyBinding, IEdmNavigationPropertyBinding>(this.navigationSource.NavigationPropertyBindings, new Func<CsdlNavigationPropertyBinding, IEdmNavigationPropertyBinding>(this.CreateSemanticMappingForBinding)));
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0001A020 File Offset: 0x00018220
		private IEdmNavigationPropertyBinding CreateSemanticMappingForBinding(CsdlNavigationPropertyBinding binding)
		{
			IEdmNavigationProperty edmNavigationProperty = this.ResolveNavigationPropertyPathForBinding(binding);
			IEdmNavigationSource edmNavigationSource = this.Container.FindEntitySetExtended(binding.Target);
			if (edmNavigationSource == null)
			{
				edmNavigationSource = this.Container.FindSingletonExtended(binding.Target);
				if (edmNavigationSource == null)
				{
					edmNavigationSource = new UnresolvedEntitySet(binding.Target, this.Container, binding.Location);
				}
			}
			return new EdmNavigationPropertyBinding(edmNavigationProperty, edmNavigationSource, new EdmPathExpression(binding.Path));
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001A08C File Offset: 0x0001828C
		private IEdmNavigationProperty ResolveNavigationPropertyPathForBinding(CsdlNavigationPropertyBinding binding)
		{
			string[] array = binding.Path.Split(new char[] { '/' });
			IEdmStructuredType edmStructuredType = this.typeCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeElementTypeFunc, null);
			for (int i = 0; i < array.Length - 1; i++)
			{
				string text = array[i];
				if (text.IndexOf('.') < 0)
				{
					IEdmProperty edmProperty = edmStructuredType.FindProperty(text);
					if (edmProperty == null)
					{
						return new UnresolvedNavigationPropertyPath(edmStructuredType, binding.Path, binding.Location);
					}
					IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
					if (edmNavigationProperty != null && !edmNavigationProperty.ContainsTarget)
					{
						return new UnresolvedNavigationPropertyPath(edmStructuredType, binding.Path, binding.Location);
					}
					edmStructuredType = edmProperty.Type.Definition.AsElementType() as IEdmStructuredType;
					if (edmStructuredType == null)
					{
						return new UnresolvedNavigationPropertyPath(edmStructuredType, binding.Path, binding.Location);
					}
				}
				else
				{
					IEdmStructuredType edmStructuredType2 = this.container.Context.FindType(text) as IEdmStructuredType;
					if (edmStructuredType2 == null || !edmStructuredType2.IsOrInheritsFrom(edmStructuredType))
					{
						return new UnresolvedNavigationPropertyPath(edmStructuredType, binding.Path, binding.Location);
					}
					edmStructuredType = edmStructuredType2;
				}
			}
			return (edmStructuredType.FindProperty(Enumerable.Last<string>(array)) as IEdmNavigationProperty) ?? new UnresolvedNavigationPropertyPath(edmStructuredType, binding.Path, binding.Location);
		}

		// Token: 0x04000596 RID: 1430
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly CsdlAbstractNavigationSource navigationSource;

		// Token: 0x04000597 RID: 1431
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly CsdlSemanticsEntityContainer container;

		// Token: 0x04000598 RID: 1432
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly IEdmPathExpression path;

		// Token: 0x04000599 RID: 1433
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly Cache<CsdlSemanticsNavigationSource, IEdmEntityType> typeCache = new Cache<CsdlSemanticsNavigationSource, IEdmEntityType>();

		// Token: 0x0400059A RID: 1434
		protected static readonly Func<CsdlSemanticsNavigationSource, IEdmEntityType> ComputeElementTypeFunc = (CsdlSemanticsNavigationSource me) => me.ComputeElementType();

		// Token: 0x0400059B RID: 1435
		private readonly Cache<CsdlSemanticsNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> navigationTargetsCache = new Cache<CsdlSemanticsNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>>();

		// Token: 0x0400059C RID: 1436
		private static readonly Func<CsdlSemanticsNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> ComputeNavigationTargetsFunc = (CsdlSemanticsNavigationSource me) => me.ComputeNavigationTargets();

		// Token: 0x0400059D RID: 1437
		private readonly Dictionary<IEdmNavigationProperty, IEdmContainedEntitySet> containedNavigationPropertyCache = new Dictionary<IEdmNavigationProperty, IEdmContainedEntitySet>();

		// Token: 0x0400059E RID: 1438
		private readonly Dictionary<IEdmNavigationProperty, IEdmUnknownEntitySet> unknownNavigationPropertyCache = new Dictionary<IEdmNavigationProperty, IEdmUnknownEntitySet>();
	}
}
