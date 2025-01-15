using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000101 RID: 257
	public abstract class EdmNavigationSource : EdmNamedElement, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0000D2C9 File Offset: 0x0000B4C9
		protected EdmNavigationSource(string name)
			: base(name)
		{
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000D2FE File Offset: 0x0000B4FE
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return this.navigationTargetsCache.GetValue(this, EdmNavigationSource.ComputeNavigationTargetsFunc, null);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600050B RID: 1291
		public abstract IEdmType Type { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600050C RID: 1292
		public abstract IEdmPathExpression Path { get; }

		// Token: 0x0600050D RID: 1293 RVA: 0x0000D312 File Offset: 0x0000B512
		public void AddNavigationTarget(IEdmNavigationProperty property, IEdmNavigationSource target)
		{
			this.navigationPropertyMappings[property] = target;
			this.navigationTargetsCache.Clear(null);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000D340 File Offset: 0x0000B540
		public virtual IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty property)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(property, "property");
			if (property.ContainsTarget)
			{
				return EdmUtil.DictionaryGetOrUpdate<IEdmNavigationProperty, IEdmContainedEntitySet>(this.containedNavigationPropertyCache, property, (IEdmNavigationProperty navProperty) => new EdmContainedEntitySet(this, navProperty));
			}
			IEdmNavigationSource edmNavigationSource;
			if (this.navigationPropertyMappings.TryGetValue(property, ref edmNavigationSource))
			{
				return edmNavigationSource;
			}
			return EdmUtil.DictionaryGetOrUpdate<IEdmNavigationProperty, IEdmUnknownEntitySet>(this.unknownNavigationPropertyCache, property, (IEdmNavigationProperty navProperty) => new EdmUnknownEntitySet(this, navProperty));
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000D3AC File Offset: 0x0000B5AC
		private IEnumerable<IEdmNavigationPropertyBinding> ComputeNavigationTargets()
		{
			List<IEdmNavigationPropertyBinding> list = new List<IEdmNavigationPropertyBinding>();
			foreach (KeyValuePair<IEdmNavigationProperty, IEdmNavigationSource> keyValuePair in this.navigationPropertyMappings)
			{
				list.Add(new EdmNavigationPropertyBinding(keyValuePair.Key, keyValuePair.Value));
			}
			return list;
		}

		// Token: 0x040001E4 RID: 484
		private readonly Dictionary<IEdmNavigationProperty, IEdmNavigationSource> navigationPropertyMappings = new Dictionary<IEdmNavigationProperty, IEdmNavigationSource>();

		// Token: 0x040001E5 RID: 485
		private readonly Dictionary<IEdmNavigationProperty, IEdmContainedEntitySet> containedNavigationPropertyCache = new Dictionary<IEdmNavigationProperty, IEdmContainedEntitySet>();

		// Token: 0x040001E6 RID: 486
		private readonly Dictionary<IEdmNavigationProperty, IEdmUnknownEntitySet> unknownNavigationPropertyCache = new Dictionary<IEdmNavigationProperty, IEdmUnknownEntitySet>();

		// Token: 0x040001E7 RID: 487
		private readonly Cache<EdmNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> navigationTargetsCache = new Cache<EdmNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>>();

		// Token: 0x040001E8 RID: 488
		private static readonly Func<EdmNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> ComputeNavigationTargetsFunc = (EdmNavigationSource me) => me.ComputeNavigationTargets();
	}
}
