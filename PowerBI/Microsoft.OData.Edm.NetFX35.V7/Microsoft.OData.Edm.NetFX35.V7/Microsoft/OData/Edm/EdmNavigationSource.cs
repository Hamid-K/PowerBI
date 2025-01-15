using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000069 RID: 105
	public abstract class EdmNavigationSource : EdmNamedElement, IEdmNavigationSource, IEdmNamedElement, IEdmElement
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000B8F6 File Offset: 0x00009AF6
		protected EdmNavigationSource(string name)
			: base(name)
		{
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000B92B File Offset: 0x00009B2B
		public IEnumerable<IEdmNavigationPropertyBinding> NavigationPropertyBindings
		{
			get
			{
				return this.navigationTargetsCache.GetValue(this, EdmNavigationSource.ComputeNavigationTargetsFunc, null);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003C5 RID: 965
		public abstract IEdmType Type { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003C6 RID: 966
		public abstract IEdmPathExpression Path { get; }

		// Token: 0x060003C7 RID: 967 RVA: 0x0000B940 File Offset: 0x00009B40
		public void AddNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmNavigationSource target)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(target, "navigation target");
			string text = navigationProperty.Name;
			if (!this.Type.AsElementType().IsOrInheritsFrom(navigationProperty.DeclaringType))
			{
				text = navigationProperty.DeclaringType.FullTypeName() + "/" + text;
			}
			if (!this.navigationPropertyMappings.ContainsKey(navigationProperty))
			{
				this.navigationPropertyMappings[navigationProperty] = new Dictionary<string, IEdmNavigationPropertyBinding>();
			}
			this.navigationPropertyMappings[navigationProperty][text] = new EdmNavigationPropertyBinding(navigationProperty, target, new EdmPathExpression(text));
			this.navigationTargetsCache.Clear(null);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		public void AddNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmNavigationSource target, IEdmPathExpression bindingPath)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			EdmUtil.CheckArgumentNull<IEdmNavigationSource>(target, "navigation target");
			EdmUtil.CheckArgumentNull<IEdmPathExpression>(bindingPath, "binding path");
			if (navigationProperty.Name != Enumerable.Last<string>(bindingPath.PathSegments))
			{
				throw new ArgumentException(Strings.NavigationPropertyBinding_PathIsNotValid);
			}
			if (!this.navigationPropertyMappings.ContainsKey(navigationProperty))
			{
				this.navigationPropertyMappings[navigationProperty] = new Dictionary<string, IEdmNavigationPropertyBinding>();
			}
			this.navigationPropertyMappings[navigationProperty][bindingPath.Path] = new EdmNavigationPropertyBinding(navigationProperty, target, bindingPath);
			this.navigationTargetsCache.Clear(null);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000BA88 File Offset: 0x00009C88
		public virtual IEnumerable<IEdmNavigationPropertyBinding> FindNavigationPropertyBindings(IEdmNavigationProperty navigationProperty)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			Dictionary<string, IEdmNavigationPropertyBinding> dictionary;
			if (this.navigationPropertyMappings.TryGetValue(navigationProperty, ref dictionary))
			{
				return Enumerable.Select<KeyValuePair<string, IEdmNavigationPropertyBinding>, IEdmNavigationPropertyBinding>(dictionary, (KeyValuePair<string, IEdmNavigationPropertyBinding> item) => item.Value);
			}
			return null;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public virtual IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "property");
			IEdmPathExpression edmPathExpression = ((!this.Type.AsElementType().IsOrInheritsFrom(navigationProperty.DeclaringType)) ? new EdmPathExpression(new string[]
			{
				navigationProperty.DeclaringType.FullTypeName(),
				navigationProperty.Name
			}) : new EdmPathExpression(navigationProperty.Name));
			return this.FindNavigationTarget(navigationProperty, edmPathExpression);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000BB48 File Offset: 0x00009D48
		public virtual IEdmNavigationSource FindNavigationTarget(IEdmNavigationProperty navigationProperty, IEdmPathExpression bindingPath)
		{
			EdmUtil.CheckArgumentNull<IEdmNavigationProperty>(navigationProperty, "navigationProperty");
			if (!navigationProperty.ContainsTarget && bindingPath != null)
			{
				Dictionary<string, IEdmNavigationPropertyBinding> dictionary;
				IEdmNavigationPropertyBinding edmNavigationPropertyBinding;
				if (this.navigationPropertyMappings.TryGetValue(navigationProperty, ref dictionary) && dictionary.TryGetValue(bindingPath.Path, ref edmNavigationPropertyBinding))
				{
					return edmNavigationPropertyBinding.Target;
				}
			}
			else if (navigationProperty.ContainsTarget)
			{
				return EdmUtil.DictionaryGetOrUpdate<IEdmNavigationProperty, IEdmContainedEntitySet>(this.containedNavigationPropertyCache, navigationProperty, (IEdmNavigationProperty navProperty) => new EdmContainedEntitySet(this, navProperty));
			}
			return EdmUtil.DictionaryGetOrUpdate<IEdmNavigationProperty, IEdmUnknownEntitySet>(this.unknownNavigationPropertyCache, navigationProperty, (IEdmNavigationProperty navProperty) => new EdmUnknownEntitySet(this, navProperty));
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000BBCC File Offset: 0x00009DCC
		private IEnumerable<IEdmNavigationPropertyBinding> ComputeNavigationTargets()
		{
			List<IEdmNavigationPropertyBinding> list = new List<IEdmNavigationPropertyBinding>();
			foreach (KeyValuePair<IEdmNavigationProperty, Dictionary<string, IEdmNavigationPropertyBinding>> keyValuePair in this.navigationPropertyMappings)
			{
				foreach (KeyValuePair<string, IEdmNavigationPropertyBinding> keyValuePair2 in keyValuePair.Value)
				{
					list.Add(keyValuePair2.Value);
				}
			}
			return list;
		}

		// Token: 0x040000E5 RID: 229
		private readonly Dictionary<IEdmNavigationProperty, Dictionary<string, IEdmNavigationPropertyBinding>> navigationPropertyMappings = new Dictionary<IEdmNavigationProperty, Dictionary<string, IEdmNavigationPropertyBinding>>();

		// Token: 0x040000E6 RID: 230
		private readonly Dictionary<IEdmNavigationProperty, IEdmContainedEntitySet> containedNavigationPropertyCache = new Dictionary<IEdmNavigationProperty, IEdmContainedEntitySet>();

		// Token: 0x040000E7 RID: 231
		private readonly Dictionary<IEdmNavigationProperty, IEdmUnknownEntitySet> unknownNavigationPropertyCache = new Dictionary<IEdmNavigationProperty, IEdmUnknownEntitySet>();

		// Token: 0x040000E8 RID: 232
		private readonly Cache<EdmNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> navigationTargetsCache = new Cache<EdmNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>>();

		// Token: 0x040000E9 RID: 233
		private static readonly Func<EdmNavigationSource, IEnumerable<IEdmNavigationPropertyBinding>> ComputeNavigationTargetsFunc = (EdmNavigationSource me) => me.ComputeNavigationTargets();
	}
}
