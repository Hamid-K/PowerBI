using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Library.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000081 RID: 129
	internal abstract class CsdlSemanticsNavigationSource : CsdlSemanticsElement, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x0600020A RID: 522 RVA: 0x000058E4 File Offset: 0x00003AE4
		public CsdlSemanticsNavigationSource(CsdlSemanticsEntityContainer container, CsdlAbstractNavigationSource navigationSource)
			: base(navigationSource)
		{
			this.container = container;
			this.navigationSource = navigationSource;
			this.path = new EdmPathExpression(this.navigationSource.Name);
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00005948 File Offset: 0x00003B48
		public override CsdlSemanticsModel Model
		{
			get
			{
				return this.container.Model;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00005955 File Offset: 0x00003B55
		public IEdmEntityContainer Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000595D File Offset: 0x00003B5D
		public override CsdlElement Element
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00005965 File Offset: 0x00003B65
		public string Name
		{
			get
			{
				return this.navigationSource.Name;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00005972 File Offset: 0x00003B72
		public IEdmPathExpression Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000210 RID: 528
		public abstract IEdmType Type { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000211 RID: 529
		public abstract EdmContainerElementKind ContainerElementKind { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000597A File Offset: 0x00003B7A
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return this.navigationTargetsCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeNavigationTargetsFunc, null);
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000059A0 File Offset: 0x00003BA0
		public IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			if (!property.ContainsTarget)
			{
				using (IEnumerator<IEdmNavigationPropertyBinding> enumerator = this.NavigationPropertyBindings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IEdmNavigationPropertyBinding edmNavigationPropertyBinding = enumerator.Current;
						if (edmNavigationPropertyBinding.NavigationProperty == property)
						{
							return edmNavigationPropertyBinding.Target;
						}
					}
					goto IL_006F;
				}
				goto IL_0051;
				IL_006F:
				return EdmUtil.DictionaryGetOrUpdate<IEdmNavigationProperty, IEdmUnknownEntitySet>(this.unknownNavigationPropertyCache, property, (IEdmNavigationProperty navProperty) => new EdmUnknownEntitySet(this, navProperty));
			}
			IL_0051:
			return EdmUtil.DictionaryGetOrUpdate<IEdmNavigationProperty, IEdmContainedEntitySet>(this.containedNavigationPropertyCache, property, (IEdmNavigationProperty navProperty) => new EdmContainedEntitySet(this, navProperty));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00005A48 File Offset: 0x00003C48
		protected override IEnumerable<IEdmVocabularyAnnotation> ComputeInlineVocabularyAnnotations()
		{
			return this.Model.WrapInlineVocabularyAnnotations(this, this.container.Context);
		}

		// Token: 0x06000215 RID: 533
		protected abstract IEdmEntityType ComputeElementType();

		// Token: 0x06000216 RID: 534 RVA: 0x00005A61 File Offset: 0x00003C61
		private IEnumerable<IEdmNavigationPropertyBinding> ComputeNavigationTargets()
		{
			return Enumerable.ToList<IEdmNavigationPropertyBinding>(Enumerable.Select<CsdlNavigationPropertyBinding, IEdmNavigationPropertyBinding>(this.navigationSource.NavigationPropertyBindings, new Func<CsdlNavigationPropertyBinding, IEdmNavigationPropertyBinding>(this.CreateSemanticMappingForBinding)));
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00005A84 File Offset: 0x00003C84
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
			return new EdmNavigationPropertyBinding(edmNavigationProperty, edmNavigationSource);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00005AE4 File Offset: 0x00003CE4
		private IEdmNavigationProperty ResolveNavigationPropertyPathForBinding(CsdlNavigationPropertyBinding binding)
		{
			string text = binding.Path;
			IEdmEntityType edmEntityType = this.typeCache.GetValue(this, CsdlSemanticsNavigationSource.ComputeElementTypeFunc, null);
			if (text.Length == 0 || text.get_Chars(text.Length - 1) == '/')
			{
				return new UnresolvedNavigationPropertyPath(edmEntityType, text, binding.Location);
			}
			string[] array = text.Split(new char[] { '/' });
			IEdmNavigationProperty edmNavigationProperty;
			for (int i = 0; i < array.Length - 1; i++)
			{
				string text2 = array[i];
				if (text2.Length == 0)
				{
					return new UnresolvedNavigationPropertyPath(edmEntityType, text, binding.Location);
				}
				IEdmEntityType edmEntityType2 = this.container.Context.FindType(text2) as IEdmEntityType;
				if (edmEntityType2 == null)
				{
					IEdmProperty edmProperty = edmEntityType.FindProperty(text2);
					edmNavigationProperty = edmProperty as IEdmNavigationProperty;
					if (edmNavigationProperty == null)
					{
						return new UnresolvedNavigationPropertyPath(edmEntityType, text, binding.Location);
					}
					edmEntityType = edmNavigationProperty.ToEntityType();
				}
				else
				{
					edmEntityType = edmEntityType2;
				}
			}
			edmNavigationProperty = edmEntityType.FindProperty(Enumerable.Last<string>(array)) as IEdmNavigationProperty;
			if (edmNavigationProperty == null)
			{
				edmNavigationProperty = new UnresolvedNavigationPropertyPath(edmEntityType, text, binding.Location);
			}
			return edmNavigationProperty;
		}

		// Token: 0x040000BC RID: 188
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly CsdlAbstractNavigationSource navigationSource;

		// Token: 0x040000BD RID: 189
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly CsdlSemanticsEntityContainer container;

		// Token: 0x040000BE RID: 190
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly IEdmPathExpression path;

		// Token: 0x040000BF RID: 191
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "protected field in internal class.")]
		protected readonly Cache<CsdlSemanticsNavigationSource, IEdmEntityType> typeCache = new Cache<CsdlSemanticsNavigationSource, IEdmEntityType>();

		// Token: 0x040000C0 RID: 192
		protected static readonly Func<CsdlSemanticsNavigationSource, IEdmEntityType> ComputeElementTypeFunc = (CsdlSemanticsNavigationSource me) => me.ComputeElementType();

		// Token: 0x040000C1 RID: 193
		private readonly Cache<CsdlSemanticsNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> navigationTargetsCache = new Cache<CsdlSemanticsNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>>();

		// Token: 0x040000C2 RID: 194
		private static readonly Func<CsdlSemanticsNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> ComputeNavigationTargetsFunc = (CsdlSemanticsNavigationSource me) => me.ComputeNavigationTargets();

		// Token: 0x040000C3 RID: 195
		private readonly Dictionary<IEdmNavigationProperty, IEdmContainedEntitySet> containedNavigationPropertyCache = new Dictionary<IEdmNavigationProperty, IEdmContainedEntitySet>();

		// Token: 0x040000C4 RID: 196
		private readonly Dictionary<IEdmNavigationProperty, IEdmUnknownEntitySet> unknownNavigationPropertyCache = new Dictionary<IEdmNavigationProperty, IEdmUnknownEntitySet>();
	}
}
