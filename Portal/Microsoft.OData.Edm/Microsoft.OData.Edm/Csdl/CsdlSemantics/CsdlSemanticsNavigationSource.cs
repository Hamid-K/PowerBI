using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000175 RID: 373
	internal abstract class CsdlSemanticsNavigationSource : CsdlSemanticsElement, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000A06 RID: 2566 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
		public CsdlSemanticsNavigationSource(CsdlSemanticsEntityContainer container, CsdlAbstractNavigationSource navigationSource)
			: base(navigationSource)
		{
			this.container = container;
			this.navigationSource = navigationSource;
			this.path = new EdmPathExpression(this.navigationSource.Name);
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0001BF2C File Offset: 0x0001A12C
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.container.Model;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0001BF39 File Offset: 0x0001A139
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0001BF41 File Offset: 0x0001A141
		public override CsdlElement Element
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0001BF49 File Offset: 0x0001A149
		public string Name
		{
			get
			{
				return this.navigationSource.Name;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0001BF56 File Offset: 0x0001A156
		public IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000A0C RID: 2572
		public abstract IEdmType Type { get; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000A0D RID: 2573
		public abstract EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0001BF5E File Offset: 0x0001A15E
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return this.navigationTargetsCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeNavigationTargetsFunc, null);
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001BF74 File Offset: 0x0001A174
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

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001C038 File Offset: 0x0001A238
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty)
		{
			IEdmPathExpression edmPathExpression = ((!this.Type.AsElementType().IsOrInheritsFrom(navigationProperty.DeclaringType)) ? new EdmPathExpression(new string[]
			{
				navigationProperty.DeclaringType.FullTypeName(),
				navigationProperty.Name
			}) : new EdmPathExpression(navigationProperty.Name));
			return this.FindNavigationTarget(navigationProperty, edmPathExpression);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001C09C File Offset: 0x0001A29C
		public IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			if (!navigationProperty.ContainsTarget)
			{
				return this.NavigationPropertyBindings.Where((IEdmNavigationPropertyBinding targetMapping) => targetMapping.NavigationProperty == navigationProperty).ToList<IEdmNavigationPropertyBinding>();
			}
			return null;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001C0E1 File Offset: 0x0001A2E1
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.container.Context);
		}

		// Token: 0x06000A13 RID: 2579
		protected abstract IEdmEntityType ComputeElementType();

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001C0FA File Offset: 0x0001A2FA
		private IEnumerable<IEdmNavigationPropertyBinding> ComputeNavigationTargets()
		{
			return this.navigationSource.NavigationPropertyBindings.Select(new Func<CsdlNavigationPropertyBinding, IEdmNavigationPropertyBinding>(this.CreateSemanticMappingForBinding)).ToList<IEdmNavigationPropertyBinding>();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001C120 File Offset: 0x0001A320
		private IEdmNavigationPropertyBinding CreateSemanticMappingForBinding(CsdlNavigationPropertyBinding binding)
		{
			IEdmNavigationProperty edmNavigationProperty = this.ResolveNavigationPropertyPathForBinding(binding);
			IEdmNavigationSource edmNavigationSource = this.Container.FindNavigationSourceExtended(binding.Target);
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

		// Token: 0x06000A16 RID: 2582 RVA: 0x0001C18C File Offset: 0x0001A38C
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
			return (edmStructuredType.FindProperty(array.Last<string>()) as IEdmNavigationProperty) ?? new UnresolvedNavigationPropertyPath(edmStructuredType, binding.Path, binding.Location);
		}

		// Token: 0x04000611 RID: 1553
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly CsdlAbstractNavigationSource navigationSource;

		// Token: 0x04000612 RID: 1554
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly CsdlSemanticsEntityContainer container;

		// Token: 0x04000613 RID: 1555
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly IEdmPathExpression path;

		// Token: 0x04000614 RID: 1556
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly Cache<CsdlSemanticsNavigationSource, IEdmEntityType> typeCache = new Cache<CsdlSemanticsNavigationSource, IEdmEntityType>();

		// Token: 0x04000615 RID: 1557
		protected static readonly Func<CsdlSemanticsNavigationSource, IEdmEntityType> ComputeElementTypeFunc = (CsdlSemanticsNavigationSource me) => me.ComputeElementType();

		// Token: 0x04000616 RID: 1558
		private readonly Cache<CsdlSemanticsNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> navigationTargetsCache = new Cache<CsdlSemanticsNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>>();

		// Token: 0x04000617 RID: 1559
		private static readonly Func<CsdlSemanticsNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> ComputeNavigationTargetsFunc = (CsdlSemanticsNavigationSource me) => me.ComputeNavigationTargets();

		// Token: 0x04000618 RID: 1560
		private readonly ConcurrentDictionary<IEdmNavigationProperty, IEdmContainedEntitySet> containedNavigationPropertyCache = new ConcurrentDictionary<IEdmNavigationProperty, IEdmContainedEntitySet>();

		// Token: 0x04000619 RID: 1561
		private readonly ConcurrentDictionary<IEdmNavigationProperty, IEdmUnknownEntitySet> unknownNavigationPropertyCache = new ConcurrentDictionary<IEdmNavigationProperty, IEdmUnknownEntitySet>();
	}
}
