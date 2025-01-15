using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000115 RID: 277
	public abstract class NavigationSourceConfiguration
	{
		// Token: 0x0600096E RID: 2414 RVA: 0x00027A03 File Offset: 0x00025C03
		protected NavigationSourceConfiguration()
		{
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00027A16 File Offset: 0x00025C16
		protected NavigationSourceConfiguration(ODataModelBuilder modelBuilder, Type entityClrType, string name)
			: this(modelBuilder, new EntityTypeConfiguration(modelBuilder, entityClrType), name)
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00027A28 File Offset: 0x00025C28
		protected NavigationSourceConfiguration(ODataModelBuilder modelBuilder, EntityTypeConfiguration entityType, string name)
		{
			if (modelBuilder == null)
			{
				throw Error.ArgumentNull("modelBuilder");
			}
			if (entityType == null)
			{
				throw Error.ArgumentNull("entityType");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw Error.ArgumentNullOrEmpty("name");
			}
			this._modelBuilder = modelBuilder;
			this.Name = name;
			this.EntityType = entityType;
			this.ClrType = entityType.ClrType;
			this._url = this.Name;
			this._editLinkBuilder = null;
			this._readLinkBuilder = null;
			this._navigationPropertyLinkBuilders = new Dictionary<NavigationPropertyConfiguration, NavigationLinkBuilder>();
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x00027ABB File Offset: 0x00025CBB
		public IEnumerable<NavigationPropertyBindingConfiguration> Bindings
		{
			get
			{
				return this._navigationPropertyBindings.Values.SelectMany((Dictionary<string, NavigationPropertyBindingConfiguration> e) => e.Values);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00027AEC File Offset: 0x00025CEC
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x00027AF4 File Offset: 0x00025CF4
		public virtual EntityTypeConfiguration EntityType { get; private set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00027AFD File Offset: 0x00025CFD
		// (set) Token: 0x06000975 RID: 2421 RVA: 0x00027B05 File Offset: 0x00025D05
		public Type ClrType { get; private set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00027B0E File Offset: 0x00025D0E
		// (set) Token: 0x06000977 RID: 2423 RVA: 0x00027B16 File Offset: 0x00025D16
		public string Name { get; private set; }

		// Token: 0x06000978 RID: 2424 RVA: 0x00027B1F File Offset: 0x00025D1F
		public virtual NavigationSourceConfiguration HasUrl(string url)
		{
			this._url = url;
			return this;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00027B29 File Offset: 0x00025D29
		public virtual NavigationSourceConfiguration HasEditLink(SelfLinkBuilder<Uri> editLinkBuilder)
		{
			if (editLinkBuilder == null)
			{
				throw Error.ArgumentNull("editLinkBuilder");
			}
			this._editLinkBuilder = editLinkBuilder;
			return this;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00027B41 File Offset: 0x00025D41
		public virtual NavigationSourceConfiguration HasReadLink(SelfLinkBuilder<Uri> readLinkBuilder)
		{
			if (readLinkBuilder == null)
			{
				throw Error.ArgumentNull("readLinkBuilder");
			}
			this._readLinkBuilder = readLinkBuilder;
			return this;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00027B59 File Offset: 0x00025D59
		public virtual NavigationSourceConfiguration HasIdLink(SelfLinkBuilder<Uri> idLinkBuilder)
		{
			if (idLinkBuilder == null)
			{
				throw Error.ArgumentNull("idLinkBuilder");
			}
			this._idLinkBuilder = idLinkBuilder;
			return this;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00027B74 File Offset: 0x00025D74
		public virtual NavigationSourceConfiguration HasNavigationPropertyLink(NavigationPropertyConfiguration navigationProperty, NavigationLinkBuilder navigationLinkBuilder)
		{
			if (navigationProperty == null)
			{
				throw Error.ArgumentNull("navigationProperty");
			}
			if (navigationLinkBuilder == null)
			{
				throw Error.ArgumentNull("navigationLinkBuilder");
			}
			StructuralTypeConfiguration declaringType = navigationProperty.DeclaringType;
			if (!declaringType.IsAssignableFrom(this.EntityType) && !this.EntityType.IsAssignableFrom(declaringType))
			{
				throw Error.Argument("navigationProperty", SRResources.NavigationPropertyNotInHierarchy, new object[]
				{
					declaringType.FullName,
					this.EntityType.FullName,
					this.Name
				});
			}
			this._navigationPropertyLinkBuilders[navigationProperty] = navigationLinkBuilder;
			return this;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00027C04 File Offset: 0x00025E04
		public virtual NavigationSourceConfiguration HasNavigationPropertiesLink(IEnumerable<NavigationPropertyConfiguration> navigationProperties, NavigationLinkBuilder navigationLinkBuilder)
		{
			if (navigationProperties == null)
			{
				throw Error.ArgumentNull("navigationProperties");
			}
			if (navigationLinkBuilder == null)
			{
				throw Error.ArgumentNull("navigationLinkBuilder");
			}
			foreach (NavigationPropertyConfiguration navigationPropertyConfiguration in navigationProperties)
			{
				this.HasNavigationPropertyLink(navigationPropertyConfiguration, navigationLinkBuilder);
			}
			return this;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00027C6C File Offset: 0x00025E6C
		public virtual NavigationPropertyBindingConfiguration AddBinding(NavigationPropertyConfiguration navigationConfiguration, NavigationSourceConfiguration targetNavigationSource)
		{
			if (navigationConfiguration == null)
			{
				throw Error.ArgumentNull("navigationConfiguration");
			}
			if (targetNavigationSource == null)
			{
				throw Error.ArgumentNull("targetNavigationSource");
			}
			IList<MemberInfo> list = new List<MemberInfo> { navigationConfiguration.PropertyInfo };
			if (navigationConfiguration.DeclaringType != this.EntityType)
			{
				list.Insert(0, TypeHelper.AsMemberInfo(navigationConfiguration.DeclaringType.ClrType));
			}
			return this.AddBinding(navigationConfiguration, targetNavigationSource, list);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00027CD8 File Offset: 0x00025ED8
		public virtual NavigationPropertyBindingConfiguration AddBinding(NavigationPropertyConfiguration navigationConfiguration, NavigationSourceConfiguration targetNavigationSource, IList<MemberInfo> bindingPath)
		{
			if (navigationConfiguration == null)
			{
				throw Error.ArgumentNull("navigationConfiguration");
			}
			if (targetNavigationSource == null)
			{
				throw Error.ArgumentNull("targetNavigationSource");
			}
			if (bindingPath == null || !bindingPath.Any<MemberInfo>())
			{
				throw Error.ArgumentNull("bindingPath");
			}
			this.VerifyBindingPath(navigationConfiguration, bindingPath);
			string text = bindingPath.ConvertBindingPath();
			Dictionary<string, NavigationPropertyBindingConfiguration> dictionary;
			NavigationPropertyBindingConfiguration navigationPropertyBindingConfiguration;
			if (this._navigationPropertyBindings.TryGetValue(navigationConfiguration, out dictionary))
			{
				if (dictionary.TryGetValue(text, out navigationPropertyBindingConfiguration))
				{
					if (navigationPropertyBindingConfiguration.TargetNavigationSource != targetNavigationSource)
					{
						throw Error.NotSupported(SRResources.RebindingNotSupported, new object[0]);
					}
				}
				else
				{
					navigationPropertyBindingConfiguration = new NavigationPropertyBindingConfiguration(navigationConfiguration, targetNavigationSource, bindingPath);
					this._navigationPropertyBindings[navigationConfiguration][text] = navigationPropertyBindingConfiguration;
				}
			}
			else
			{
				this._navigationPropertyBindings[navigationConfiguration] = new Dictionary<string, NavigationPropertyBindingConfiguration>();
				navigationPropertyBindingConfiguration = new NavigationPropertyBindingConfiguration(navigationConfiguration, targetNavigationSource, bindingPath);
				this._navigationPropertyBindings[navigationConfiguration][text] = navigationPropertyBindingConfiguration;
			}
			return navigationPropertyBindingConfiguration;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00027DA7 File Offset: 0x00025FA7
		public virtual void RemoveBinding(NavigationPropertyConfiguration navigationConfiguration)
		{
			if (navigationConfiguration == null)
			{
				throw Error.ArgumentNull("navigationConfiguration");
			}
			this._navigationPropertyBindings.Remove(navigationConfiguration);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00027DC4 File Offset: 0x00025FC4
		public virtual void RemoveBinding(NavigationPropertyConfiguration navigationConfiguration, string bindingPath)
		{
			if (navigationConfiguration == null)
			{
				throw Error.ArgumentNull("navigationConfiguration");
			}
			Dictionary<string, NavigationPropertyBindingConfiguration> dictionary;
			if (this._navigationPropertyBindings.TryGetValue(navigationConfiguration, out dictionary))
			{
				dictionary.Remove(bindingPath);
				if (!dictionary.Any<KeyValuePair<string, NavigationPropertyBindingConfiguration>>())
				{
					this._navigationPropertyBindings.Remove(navigationConfiguration);
				}
			}
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00027E0C File Offset: 0x0002600C
		public virtual IEnumerable<NavigationPropertyBindingConfiguration> FindBinding(NavigationPropertyConfiguration navigationConfiguration)
		{
			if (navigationConfiguration == null)
			{
				throw Error.ArgumentNull("navigationConfiguration");
			}
			Dictionary<string, NavigationPropertyBindingConfiguration> dictionary;
			if (this._navigationPropertyBindings.TryGetValue(navigationConfiguration, out dictionary))
			{
				return dictionary.Values;
			}
			return null;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00027E40 File Offset: 0x00026040
		public virtual NavigationPropertyBindingConfiguration FindBinding(NavigationPropertyConfiguration navigationConfiguration, IList<MemberInfo> bindingPath)
		{
			if (navigationConfiguration == null)
			{
				throw Error.ArgumentNull("navigationConfiguration");
			}
			if (bindingPath == null)
			{
				throw Error.ArgumentNullOrEmpty("bindingPath");
			}
			string text = bindingPath.ConvertBindingPath();
			Dictionary<string, NavigationPropertyBindingConfiguration> dictionary;
			NavigationPropertyBindingConfiguration navigationPropertyBindingConfiguration;
			if (this._navigationPropertyBindings.TryGetValue(navigationConfiguration, out dictionary) && dictionary.TryGetValue(text, out navigationPropertyBindingConfiguration))
			{
				return navigationPropertyBindingConfiguration;
			}
			if (this._modelBuilder.BindingOptions == NavigationPropertyBindingOption.None)
			{
				return null;
			}
			bool flag = navigationConfiguration.PropertyInfo.GetCustomAttributes<SingletonAttribute>().Any<SingletonAttribute>();
			Type entityType = navigationConfiguration.RelatedClrType;
			NavigationSourceConfiguration[] array2;
			if (flag)
			{
				NavigationSourceConfiguration[] array = this._modelBuilder.Singletons.Where((SingletonConfiguration es) => es.EntityType.ClrType == entityType).ToArray<SingletonConfiguration>();
				array2 = array;
			}
			else
			{
				NavigationSourceConfiguration[] array = this._modelBuilder.EntitySets.Where((EntitySetConfiguration es) => es.EntityType.ClrType == entityType).ToArray<EntitySetConfiguration>();
				array2 = array;
			}
			if (array2.Length < 1)
			{
				return null;
			}
			if (array2.Length == 1 || this._modelBuilder.BindingOptions == NavigationPropertyBindingOption.Auto)
			{
				return this.AddBinding(navigationConfiguration, array2[0], bindingPath);
			}
			string cannotAutoCreateMultipleCandidates = SRResources.CannotAutoCreateMultipleCandidates;
			object[] array3 = new object[4];
			array3[0] = text;
			array3[1] = navigationConfiguration.DeclaringType.FullName;
			array3[2] = this.Name;
			array3[3] = string.Join(", ", array2.Select((NavigationSourceConfiguration s) => s.Name));
			throw Error.NotSupported(cannotAutoCreateMultipleCandidates, array3);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00027F94 File Offset: 0x00026194
		public virtual IEnumerable<NavigationPropertyBindingConfiguration> FindBindings(string propertyName)
		{
			foreach (KeyValuePair<NavigationPropertyConfiguration, Dictionary<string, NavigationPropertyBindingConfiguration>> keyValuePair in this._navigationPropertyBindings)
			{
				if (keyValuePair.Key.Name == propertyName)
				{
					return keyValuePair.Value.Values;
				}
			}
			return Enumerable.Empty<NavigationPropertyBindingConfiguration>();
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0002800C File Offset: 0x0002620C
		public virtual string GetUrl()
		{
			return this._url;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00028014 File Offset: 0x00026214
		public virtual SelfLinkBuilder<Uri> GetEditLink()
		{
			return this._editLinkBuilder;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0002801C File Offset: 0x0002621C
		public virtual SelfLinkBuilder<Uri> GetReadLink()
		{
			return this._readLinkBuilder;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00028024 File Offset: 0x00026224
		public virtual SelfLinkBuilder<Uri> GetIdLink()
		{
			return this._idLinkBuilder;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0002802C File Offset: 0x0002622C
		public virtual NavigationLinkBuilder GetNavigationPropertyLink(NavigationPropertyConfiguration navigationProperty)
		{
			if (navigationProperty == null)
			{
				throw Error.ArgumentNull("navigationProperty");
			}
			NavigationLinkBuilder navigationLinkBuilder;
			this._navigationPropertyLinkBuilders.TryGetValue(navigationProperty, out navigationLinkBuilder);
			return navigationLinkBuilder;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00028058 File Offset: 0x00026258
		private void VerifyBindingPath(NavigationPropertyConfiguration navigationConfiguration, IList<MemberInfo> bindingPath)
		{
			PropertyInfo propertyInfo = bindingPath.Last<MemberInfo>() as PropertyInfo;
			if (propertyInfo == null || propertyInfo != navigationConfiguration.PropertyInfo)
			{
				throw Error.Argument("navigationConfiguration", SRResources.NavigationPropertyBindingPathIsNotValid, new object[]
				{
					bindingPath.ConvertBindingPath(),
					navigationConfiguration.Name
				});
			}
			bindingPath.Aggregate(this.EntityType.ClrType, new Func<Type, MemberInfo, Type>(NavigationSourceConfiguration.VerifyBindingSegment));
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x000280D0 File Offset: 0x000262D0
		private static Type VerifyBindingSegment(Type current, MemberInfo info)
		{
			TypeInfo typeInfo = info as TypeInfo;
			if (typeInfo != null)
			{
				if (!typeInfo.IsAssignableFrom(current) && !current.IsAssignableFrom(typeInfo.BaseType))
				{
					throw Error.InvalidOperation(SRResources.NavigationPropertyBindingPathNotInHierarchy, new object[] { typeInfo.FullName, info.Name, current.FullName });
				}
				return typeInfo.BaseType;
			}
			else
			{
				PropertyInfo propertyInfo = info as PropertyInfo;
				if (propertyInfo == null)
				{
					throw Error.NotSupported(SRResources.NavigationPropertyBindingPathNotSupported, new object[] { info.Name, info.MemberType });
				}
				Type declaringType = propertyInfo.DeclaringType;
				if (declaringType == null || (!declaringType.IsAssignableFrom(current) && !current.IsAssignableFrom(declaringType)))
				{
					throw Error.InvalidOperation(SRResources.NavigationPropertyBindingPathNotInHierarchy, new object[]
					{
						(declaringType == null) ? "Unknown Type" : declaringType.FullName,
						info.Name,
						current.FullName
					});
				}
				Type type;
				if (TypeHelper.IsCollection(propertyInfo.PropertyType, out type))
				{
					return type;
				}
				return propertyInfo.PropertyType;
			}
		}

		// Token: 0x04000302 RID: 770
		private readonly ODataModelBuilder _modelBuilder;

		// Token: 0x04000303 RID: 771
		private string _url;

		// Token: 0x04000304 RID: 772
		private SelfLinkBuilder<Uri> _editLinkBuilder;

		// Token: 0x04000305 RID: 773
		private SelfLinkBuilder<Uri> _readLinkBuilder;

		// Token: 0x04000306 RID: 774
		private SelfLinkBuilder<Uri> _idLinkBuilder;

		// Token: 0x04000307 RID: 775
		private readonly Dictionary<NavigationPropertyConfiguration, Dictionary<string, NavigationPropertyBindingConfiguration>> _navigationPropertyBindings = new Dictionary<NavigationPropertyConfiguration, Dictionary<string, NavigationPropertyBindingConfiguration>>();

		// Token: 0x04000308 RID: 776
		private readonly Dictionary<NavigationPropertyConfiguration, NavigationLinkBuilder> _navigationPropertyLinkBuilders;
	}
}
