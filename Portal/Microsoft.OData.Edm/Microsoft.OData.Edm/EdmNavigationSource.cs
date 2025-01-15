using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200003B RID: 59
	public abstract class EdmNavigationSource : EdmNamedElement, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00003FFE File Offset: 0x000021FE
		protected EdmNavigationSource(string name)
			: base(name)
		{
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00004028 File Offset: 0x00002228
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return this.navigationTargetsCache.GetValue(this, EdmNavigationSource.ComputeNavigationTargetsFunc, null);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000126 RID: 294
		public abstract IEdmType Type { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000127 RID: 295
		public abstract IEdmPathExpression Path { get; }

		// Token: 0x06000128 RID: 296 RVA: 0x0000403C File Offset: 0x0000223C
		public void AddNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmNavigationSource target)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(target, "navigation target");
			if (navigationProperty.ContainsTarget)
			{
				return;
			}
			string text = navigationProperty.Name;
			if (!this.Type.AsElementType().IsOrInheritsFrom(navigationProperty.DeclaringType))
			{
				text = navigationProperty.DeclaringType.FullTypeName() + "/" + text;
			}
			this.AddNavigationPropertyBinding(navigationProperty, target, new EdmPathExpression(text));
			this.navigationTargetsCache.Clear(null);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000040BC File Offset: 0x000022BC
		public void AddNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmNavigationSource target, IEdmPathExpression bindingPath)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(target, "navigation target");
			EdmUtil.CheckArgumentNull<IEdmPathExpression>(bindingPath, "binding path");
			if (navigationProperty.ContainsTarget)
			{
				return;
			}
			if (navigationProperty.Name != bindingPath.PathSegments.Last<string>())
			{
				throw new ArgumentException(Strings.NavigationPropertyBinding_PathIsNotValid);
			}
			this.AddNavigationPropertyBinding(navigationProperty, target, bindingPath);
			this.navigationTargetsCache.Clear(null);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004130 File Offset: 0x00002330
		public virtual IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			ConcurrentDictionary<string, IEdmNavigationPropertyBinding> concurrentDictionary = EdmUtil.DictionarySafeGet<IEdmNavigationProperty, ConcurrentDictionary<string, IEdmNavigationPropertyBinding>>(this.navigationPropertyMappings, navigationProperty);
			if (concurrentDictionary != null)
			{
				return concurrentDictionary.Select((KeyValuePair<string, IEdmNavigationPropertyBinding> item) => item.Value);
			}
			return null;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004180 File Offset: 0x00002380
		public virtual IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "property");
			IEdmPathExpression edmPathExpression = ((!(navigationProperty.DeclaringType.AsElementType() is IEdmComplexType) && !this.Type.AsElementType().IsOrInheritsFrom(navigationProperty.DeclaringType)) ? new EdmPathExpression(new string[]
			{
				navigationProperty.DeclaringType.FullTypeName(),
				navigationProperty.Name
			}) : new EdmPathExpression(navigationProperty.Name));
			return this.FindNavigationTarget(navigationProperty, edmPathExpression);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004204 File Offset: 0x00002404
		public virtual IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			bindingPath = bindingPath ?? new EdmPathExpression(navigationProperty.Name);
			ConcurrentDictionary<string, IEdmNavigationPropertyBinding> concurrentDictionary = EdmUtil.DictionarySafeGet<IEdmNavigationProperty, ConcurrentDictionary<string, IEdmNavigationPropertyBinding>>(this.navigationPropertyMappings, navigationProperty);
			if (concurrentDictionary != null)
			{
				IEdmNavigationPropertyBinding edmNavigationPropertyBinding = EdmUtil.DictionarySafeGet<string, IEdmNavigationPropertyBinding>(concurrentDictionary, bindingPath.Path);
				if (edmNavigationPropertyBinding != null)
				{
					return edmNavigationPropertyBinding.Target;
				}
			}
			if (navigationProperty.ContainsTarget)
			{
				return this.AddNavigationPropertyBinding(navigationProperty, new EdmContainedEntitySet(this, navigationProperty, bindingPath), bindingPath).Target;
			}
			return EdmUtil.DictionaryGetOrUpdate<IEdmNavigationProperty, IEdmUnknownEntitySet>(this.unknownNavigationPropertyCache, navigationProperty, (IEdmNavigationProperty navProperty) => new EdmUnknownEntitySet(this, navProperty));
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000428C File Offset: 0x0000248C
		private IEdmNavigationPropertyBinding AddNavigationPropertyBinding(IEdmNavigationProperty navigationProperty, IEdmNavigationSource target, IEdmPathExpression bindingPath)
		{
			ConcurrentDictionary<string, IEdmNavigationPropertyBinding> concurrentDictionary = EdmUtil.DictionaryGetOrUpdate<IEdmNavigationProperty, ConcurrentDictionary<string, IEdmNavigationPropertyBinding>>(this.navigationPropertyMappings, navigationProperty, (IEdmNavigationProperty navProperty) => new ConcurrentDictionary<string, IEdmNavigationPropertyBinding>());
			return EdmUtil.DictionaryGetOrUpdate<string, IEdmNavigationPropertyBinding>(concurrentDictionary, bindingPath.Path, (string path) => new EdmNavigationPropertyBinding(navigationProperty, target, new EdmPathExpression(path)));
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000042F8 File Offset: 0x000024F8
		private IEnumerable<IEdmNavigationPropertyBinding> ComputeNavigationTargets()
		{
			List<IEdmNavigationPropertyBinding> list = new List<IEdmNavigationPropertyBinding>();
			foreach (KeyValuePair<IEdmNavigationProperty, ConcurrentDictionary<string, IEdmNavigationPropertyBinding>> keyValuePair in this.navigationPropertyMappings)
			{
				if (!keyValuePair.Key.ContainsTarget)
				{
					foreach (KeyValuePair<string, IEdmNavigationPropertyBinding> keyValuePair2 in keyValuePair.Value)
					{
						list.Add(keyValuePair2.Value);
					}
				}
			}
			return list.OrderBy(delegate(IEdmNavigationPropertyBinding x)
			{
				if (x == null)
				{
					return null;
				}
				IEdmPathExpression path = x.Path;
				if (path == null)
				{
					return null;
				}
				return path.Path;
			});
		}

		// Token: 0x04000069 RID: 105
		private readonly ConcurrentDictionary<IEdmNavigationProperty, ConcurrentDictionary<string, IEdmNavigationPropertyBinding>> navigationPropertyMappings = new ConcurrentDictionary<IEdmNavigationProperty, ConcurrentDictionary<string, IEdmNavigationPropertyBinding>>();

		// Token: 0x0400006A RID: 106
		private readonly ConcurrentDictionary<IEdmNavigationProperty, IEdmUnknownEntitySet> unknownNavigationPropertyCache = new ConcurrentDictionary<IEdmNavigationProperty, IEdmUnknownEntitySet>();

		// Token: 0x0400006B RID: 107
		private readonly Cache<EdmNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> navigationTargetsCache = new Cache<EdmNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>>();

		// Token: 0x0400006C RID: 108
		private static readonly Func<EdmNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> ComputeNavigationTargetsFunc = (EdmNavigationSource me) => me.ComputeNavigationTargets();
	}
}
