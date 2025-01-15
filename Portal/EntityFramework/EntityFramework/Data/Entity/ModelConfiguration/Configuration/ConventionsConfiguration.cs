using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Configuration.Properties;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Conventions.Sets;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C5 RID: 453
	public class ConventionsConfiguration
	{
		// Token: 0x060017EC RID: 6124 RVA: 0x00040C50 File Offset: 0x0003EE50
		internal ConventionsConfiguration()
			: this(V2ConventionSet.Conventions)
		{
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00040C60 File Offset: 0x0003EE60
		internal ConventionsConfiguration(ConventionSet conventionSet)
		{
			this._configurationConventions = new List<IConvention>();
			this._conceptualModelConventions = new List<IConvention>();
			this._conceptualToStoreMappingConventions = new List<IConvention>();
			this._storeModelConventions = new List<IConvention>();
			base..ctor();
			this._configurationConventions.AddRange(conventionSet.ConfigurationConventions);
			this._conceptualModelConventions.AddRange(conventionSet.ConceptualModelConventions);
			this._conceptualToStoreMappingConventions.AddRange(conventionSet.ConceptualToStoreMappingConventions);
			this._storeModelConventions.AddRange(conventionSet.StoreModelConventions);
			this._initialConventionSet = conventionSet;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00040CEC File Offset: 0x0003EEEC
		private ConventionsConfiguration(ConventionsConfiguration source)
		{
			this._configurationConventions = new List<IConvention>();
			this._conceptualModelConventions = new List<IConvention>();
			this._conceptualToStoreMappingConventions = new List<IConvention>();
			this._storeModelConventions = new List<IConvention>();
			base..ctor();
			this._configurationConventions.AddRange(source._configurationConventions);
			this._conceptualModelConventions.AddRange(source._conceptualModelConventions);
			this._conceptualToStoreMappingConventions.AddRange(source._conceptualToStoreMappingConventions);
			this._storeModelConventions.AddRange(source._storeModelConventions);
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x00040D6F File Offset: 0x0003EF6F
		internal IEnumerable<IConvention> ConfigurationConventions
		{
			get
			{
				return this._configurationConventions;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00040D77 File Offset: 0x0003EF77
		internal IEnumerable<IConvention> ConceptualModelConventions
		{
			get
			{
				return this._conceptualModelConventions;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x00040D7F File Offset: 0x0003EF7F
		internal IEnumerable<IConvention> ConceptualToStoreMappingConventions
		{
			get
			{
				return this._conceptualToStoreMappingConventions;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00040D87 File Offset: 0x0003EF87
		internal IEnumerable<IConvention> StoreModelConventions
		{
			get
			{
				return this._storeModelConventions;
			}
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00040D8F File Offset: 0x0003EF8F
		internal virtual ConventionsConfiguration Clone()
		{
			return new ConventionsConfiguration(this);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00040D98 File Offset: 0x0003EF98
		public void AddFromAssembly(Assembly assembly)
		{
			Check.NotNull<Assembly>(assembly, "assembly");
			IOrderedEnumerable<Type> orderedEnumerable = from type in assembly.GetAccessibleTypes()
				orderby type.Name
				select type;
			new ConventionsTypeFinder().AddConventions(orderedEnumerable, delegate(IConvention convention)
			{
				this.Add(new IConvention[] { convention });
			});
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00040DF4 File Offset: 0x0003EFF4
		public void Add(params IConvention[] conventions)
		{
			Check.NotNull<IConvention[]>(conventions, "conventions");
			foreach (IConvention convention in conventions)
			{
				bool flag = true;
				if (ConventionsTypeFilter.IsConfigurationConvention(convention.GetType()))
				{
					flag = false;
					int num = this._configurationConventions.FindIndex((IConvention initialConvention) => this._initialConventionSet.ConfigurationConventions.Contains(initialConvention));
					num = ((num == -1) ? this._configurationConventions.Count : num);
					this._configurationConventions.Insert(num, convention);
				}
				if (ConventionsTypeFilter.IsConceptualModelConvention(convention.GetType()))
				{
					flag = false;
					this._conceptualModelConventions.Add(convention);
				}
				if (ConventionsTypeFilter.IsStoreModelConvention(convention.GetType()))
				{
					flag = false;
					this._storeModelConventions.Add(convention);
				}
				if (ConventionsTypeFilter.IsConceptualToStoreMappingConvention(convention.GetType()))
				{
					flag = false;
					this._conceptualToStoreMappingConventions.Add(convention);
				}
				if (flag)
				{
					throw new InvalidOperationException(Strings.ConventionsConfiguration_InvalidConventionType(convention.GetType()));
				}
			}
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00040EDA File Offset: 0x0003F0DA
		public void Add<TConvention>() where TConvention : IConvention, new()
		{
			this.Add(new IConvention[]
			{
				new TConvention()
			});
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00040EF8 File Offset: 0x0003F0F8
		public void AddAfter<TExistingConvention>(IConvention newConvention) where TExistingConvention : IConvention
		{
			Check.NotNull<IConvention>(newConvention, "newConvention");
			bool flag = true;
			if (ConventionsTypeFilter.IsConfigurationConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConfigurationConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 1, newConvention, this._configurationConventions);
			}
			if (ConventionsTypeFilter.IsConceptualModelConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConceptualModelConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 1, newConvention, this._conceptualModelConventions);
			}
			if (ConventionsTypeFilter.IsStoreModelConvention(newConvention.GetType()) && ConventionsTypeFilter.IsStoreModelConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 1, newConvention, this._storeModelConventions);
			}
			if (ConventionsTypeFilter.IsConceptualToStoreMappingConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConceptualToStoreMappingConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 1, newConvention, this._conceptualToStoreMappingConventions);
			}
			if (flag)
			{
				throw new InvalidOperationException(Strings.ConventionsConfiguration_ConventionTypeMissmatch(newConvention.GetType(), typeof(TExistingConvention)));
			}
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00041010 File Offset: 0x0003F210
		public void AddBefore<TExistingConvention>(IConvention newConvention) where TExistingConvention : IConvention
		{
			Check.NotNull<IConvention>(newConvention, "newConvention");
			bool flag = true;
			if (ConventionsTypeFilter.IsConfigurationConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConfigurationConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 0, newConvention, this._configurationConventions);
			}
			if (ConventionsTypeFilter.IsConceptualModelConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConceptualModelConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 0, newConvention, this._conceptualModelConventions);
			}
			if (ConventionsTypeFilter.IsStoreModelConvention(newConvention.GetType()) && ConventionsTypeFilter.IsStoreModelConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 0, newConvention, this._storeModelConventions);
			}
			if (ConventionsTypeFilter.IsConceptualToStoreMappingConvention(newConvention.GetType()) && ConventionsTypeFilter.IsConceptualToStoreMappingConvention(typeof(TExistingConvention)))
			{
				flag = false;
				ConventionsConfiguration.Insert(typeof(TExistingConvention), 0, newConvention, this._conceptualToStoreMappingConventions);
			}
			if (flag)
			{
				throw new InvalidOperationException(Strings.ConventionsConfiguration_ConventionTypeMissmatch(newConvention.GetType(), typeof(TExistingConvention)));
			}
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00041128 File Offset: 0x0003F328
		private static void Insert(Type existingConventionType, int offset, IConvention newConvention, IList<IConvention> conventions)
		{
			int num = ConventionsConfiguration.IndexOf(existingConventionType, conventions);
			if (num < 0)
			{
				throw Error.ConventionNotFound(newConvention.GetType(), existingConventionType);
			}
			conventions.Insert(num + offset, newConvention);
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x00041158 File Offset: 0x0003F358
		private static int IndexOf(Type existingConventionType, IList<IConvention> conventions)
		{
			int num = 0;
			using (IEnumerator<IConvention> enumerator = conventions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType() == existingConventionType)
					{
						return num;
					}
					num++;
				}
			}
			return -1;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x000411B4 File Offset: 0x0003F3B4
		public void Remove(params IConvention[] conventions)
		{
			Check.NotNull<IConvention[]>(conventions, "conventions");
			Check.NotNull<IConvention[]>(conventions, "conventions");
			foreach (IConvention convention in conventions)
			{
				if (ConventionsTypeFilter.IsConfigurationConvention(convention.GetType()))
				{
					this._configurationConventions.Remove(convention);
				}
				if (ConventionsTypeFilter.IsConceptualModelConvention(convention.GetType()))
				{
					this._conceptualModelConventions.Remove(convention);
				}
				if (ConventionsTypeFilter.IsStoreModelConvention(convention.GetType()))
				{
					this._storeModelConventions.Remove(convention);
				}
				if (ConventionsTypeFilter.IsConceptualToStoreMappingConvention(convention.GetType()))
				{
					this._conceptualToStoreMappingConventions.Remove(convention);
				}
			}
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x00041258 File Offset: 0x0003F458
		public void Remove<TConvention>() where TConvention : IConvention
		{
			if (ConventionsTypeFilter.IsConfigurationConvention(typeof(TConvention)))
			{
				this._configurationConventions.RemoveAll((IConvention c) => c.GetType() == typeof(TConvention));
			}
			if (ConventionsTypeFilter.IsConceptualModelConvention(typeof(TConvention)))
			{
				this._conceptualModelConventions.RemoveAll((IConvention c) => c.GetType() == typeof(TConvention));
			}
			if (ConventionsTypeFilter.IsStoreModelConvention(typeof(TConvention)))
			{
				this._storeModelConventions.RemoveAll((IConvention c) => c.GetType() == typeof(TConvention));
			}
			if (ConventionsTypeFilter.IsConceptualToStoreMappingConvention(typeof(TConvention)))
			{
				this._conceptualToStoreMappingConventions.RemoveAll((IConvention c) => c.GetType() == typeof(TConvention));
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00041358 File Offset: 0x0003F558
		internal void ApplyConceptualModel(DbModel model)
		{
			foreach (IConvention convention in this._conceptualModelConventions)
			{
				new ConventionsConfiguration.ModelConventionDispatcher(convention, model, DataSpace.CSpace).Dispatch();
			}
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x000413B0 File Offset: 0x0003F5B0
		internal void ApplyStoreModel(DbModel model)
		{
			foreach (IConvention convention in this._storeModelConventions)
			{
				new ConventionsConfiguration.ModelConventionDispatcher(convention, model, DataSpace.SSpace).Dispatch();
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00041408 File Offset: 0x0003F608
		internal void ApplyPluralizingTableNameConvention(DbModel model)
		{
			foreach (IConvention convention in this._storeModelConventions.Where((IConvention c) => c is PluralizingTableNameConvention))
			{
				new ConventionsConfiguration.ModelConventionDispatcher(convention, model, DataSpace.SSpace).Dispatch();
			}
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00041480 File Offset: 0x0003F680
		internal void ApplyMapping(DbDatabaseMapping databaseMapping)
		{
			foreach (IConvention convention in this._conceptualToStoreMappingConventions)
			{
				IDbMappingConvention dbMappingConvention = convention as IDbMappingConvention;
				if (dbMappingConvention != null)
				{
					dbMappingConvention.Apply(databaseMapping);
				}
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000414DC File Offset: 0x0003F6DC
		internal virtual void ApplyModelConfiguration(ModelConfiguration modelConfiguration)
		{
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				IConfigurationConvention configurationConvention = convention as IConfigurationConvention;
				if (configurationConvention != null)
				{
					configurationConvention.Apply(modelConfiguration);
				}
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyModelConfiguration(modelConfiguration);
				}
			}
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00041530 File Offset: 0x0003F730
		internal virtual void ApplyModelConfiguration(Type type, ModelConfiguration modelConfiguration)
		{
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				IConfigurationConvention<Type> configurationConvention = convention as IConfigurationConvention<Type>;
				if (configurationConvention != null)
				{
					configurationConvention.Apply(type, modelConfiguration);
				}
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyModelConfiguration(type, modelConfiguration);
				}
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00041584 File Offset: 0x0003F784
		internal virtual void ApplyTypeConfiguration<TStructuralTypeConfiguration>(Type type, Func<TStructuralTypeConfiguration> structuralTypeConfiguration, ModelConfiguration modelConfiguration) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				IConfigurationConvention<Type, TStructuralTypeConfiguration> configurationConvention = convention as IConfigurationConvention<Type, TStructuralTypeConfiguration>;
				if (configurationConvention != null)
				{
					configurationConvention.Apply(type, structuralTypeConfiguration, modelConfiguration);
				}
				IConfigurationConvention<Type, StructuralTypeConfiguration> configurationConvention2 = convention as IConfigurationConvention<Type, StructuralTypeConfiguration>;
				if (configurationConvention2 != null)
				{
					configurationConvention2.Apply(type, structuralTypeConfiguration, modelConfiguration);
				}
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyTypeConfiguration<TStructuralTypeConfiguration>(type, structuralTypeConfiguration, modelConfiguration);
				}
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000415F0 File Offset: 0x0003F7F0
		internal virtual void ApplyPropertyConfiguration(PropertyInfo propertyInfo, ModelConfiguration modelConfiguration)
		{
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				IConfigurationConvention<PropertyInfo> configurationConvention = convention as IConfigurationConvention<PropertyInfo>;
				if (configurationConvention != null)
				{
					configurationConvention.Apply(propertyInfo, modelConfiguration);
				}
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyPropertyConfiguration(propertyInfo, modelConfiguration);
				}
			}
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00041644 File Offset: 0x0003F844
		internal virtual void ApplyPropertyConfiguration(PropertyInfo propertyInfo, Func<PropertyConfiguration> propertyConfiguration, ModelConfiguration modelConfiguration)
		{
			Type propertyConfigurationType = StructuralTypeConfiguration.GetPropertyConfigurationType(propertyInfo.PropertyType);
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				new ConventionsConfiguration.PropertyConfigurationConventionDispatcher(convention, propertyConfigurationType, propertyInfo, propertyConfiguration, modelConfiguration).Dispatch();
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyPropertyConfiguration(propertyInfo, propertyConfiguration, modelConfiguration);
				}
			}
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000416A4 File Offset: 0x0003F8A4
		internal virtual void ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(PropertyInfo propertyInfo, Func<TStructuralTypeConfiguration> structuralTypeConfiguration, ModelConfiguration modelConfiguration) where TStructuralTypeConfiguration : StructuralTypeConfiguration
		{
			for (int i = this._configurationConventions.Count - 1; i >= 0; i--)
			{
				IConvention convention = this._configurationConventions[i];
				IConfigurationConvention<PropertyInfo, TStructuralTypeConfiguration> configurationConvention = convention as IConfigurationConvention<PropertyInfo, TStructuralTypeConfiguration>;
				if (configurationConvention != null)
				{
					configurationConvention.Apply(propertyInfo, structuralTypeConfiguration, modelConfiguration);
				}
				IConfigurationConvention<PropertyInfo, StructuralTypeConfiguration> configurationConvention2 = convention as IConfigurationConvention<PropertyInfo, StructuralTypeConfiguration>;
				if (configurationConvention2 != null)
				{
					configurationConvention2.Apply(propertyInfo, structuralTypeConfiguration, modelConfiguration);
				}
				Convention convention2 = convention as Convention;
				if (convention2 != null)
				{
					convention2.ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(propertyInfo, structuralTypeConfiguration, modelConfiguration);
				}
			}
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0004170D File Offset: 0x0003F90D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00041715 File Offset: 0x0003F915
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0004171E File Offset: 0x0003F91E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00041726 File Offset: 0x0003F926
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A4A RID: 2634
		private readonly List<IConvention> _configurationConventions;

		// Token: 0x04000A4B RID: 2635
		private readonly List<IConvention> _conceptualModelConventions;

		// Token: 0x04000A4C RID: 2636
		private readonly List<IConvention> _conceptualToStoreMappingConventions;

		// Token: 0x04000A4D RID: 2637
		private readonly List<IConvention> _storeModelConventions;

		// Token: 0x04000A4E RID: 2638
		private readonly ConventionSet _initialConventionSet;

		// Token: 0x020008A7 RID: 2215
		private class ModelConventionDispatcher : EdmModelVisitor
		{
			// Token: 0x06005B6E RID: 23406 RVA: 0x0013DF7A File Offset: 0x0013C17A
			public ModelConventionDispatcher(IConvention convention, DbModel model, DataSpace dataSpace)
			{
				Check.NotNull<IConvention>(convention, "convention");
				Check.NotNull<DbModel>(model, "model");
				this._convention = convention;
				this._model = model;
				this._dataSpace = dataSpace;
			}

			// Token: 0x06005B6F RID: 23407 RVA: 0x0013DFAF File Offset: 0x0013C1AF
			public void Dispatch()
			{
				this.VisitEdmModel((this._dataSpace == DataSpace.CSpace) ? this._model.ConceptualModel : this._model.StoreModel);
			}

			// Token: 0x06005B70 RID: 23408 RVA: 0x0013DFD8 File Offset: 0x0013C1D8
			private void Dispatch<T>(T item) where T : MetadataItem
			{
				if (this._dataSpace == DataSpace.CSpace)
				{
					IConceptualModelConvention<T> conceptualModelConvention = this._convention as IConceptualModelConvention<T>;
					if (conceptualModelConvention != null)
					{
						conceptualModelConvention.Apply(item, this._model);
						return;
					}
				}
				else
				{
					IStoreModelConvention<T> storeModelConvention = this._convention as IStoreModelConvention<T>;
					if (storeModelConvention != null)
					{
						storeModelConvention.Apply(item, this._model);
					}
				}
			}

			// Token: 0x06005B71 RID: 23409 RVA: 0x0013E027 File Offset: 0x0013C227
			protected internal override void VisitEdmModel(EdmModel item)
			{
				this.Dispatch<EdmModel>(item);
				base.VisitEdmModel(item);
			}

			// Token: 0x06005B72 RID: 23410 RVA: 0x0013E037 File Offset: 0x0013C237
			protected override void VisitEdmNavigationProperty(NavigationProperty item)
			{
				this.Dispatch<NavigationProperty>(item);
				base.VisitEdmNavigationProperty(item);
			}

			// Token: 0x06005B73 RID: 23411 RVA: 0x0013E047 File Offset: 0x0013C247
			protected override void VisitEdmAssociationConstraint(ReferentialConstraint item)
			{
				this.Dispatch<ReferentialConstraint>(item);
				if (item != null)
				{
					this.VisitMetadataItem(item);
				}
			}

			// Token: 0x06005B74 RID: 23412 RVA: 0x0013E05A File Offset: 0x0013C25A
			protected override void VisitEdmAssociationEnd(RelationshipEndMember item)
			{
				this.Dispatch<RelationshipEndMember>(item);
				base.VisitEdmAssociationEnd(item);
			}

			// Token: 0x06005B75 RID: 23413 RVA: 0x0013E06A File Offset: 0x0013C26A
			protected internal override void VisitEdmProperty(EdmProperty item)
			{
				this.Dispatch<EdmProperty>(item);
				base.VisitEdmProperty(item);
			}

			// Token: 0x06005B76 RID: 23414 RVA: 0x0013E07A File Offset: 0x0013C27A
			protected internal override void VisitMetadataItem(MetadataItem item)
			{
				this.Dispatch<MetadataItem>(item);
				base.VisitMetadataItem(item);
			}

			// Token: 0x06005B77 RID: 23415 RVA: 0x0013E08A File Offset: 0x0013C28A
			protected override void VisitEdmEntityContainer(EntityContainer item)
			{
				this.Dispatch<EntityContainer>(item);
				base.VisitEdmEntityContainer(item);
			}

			// Token: 0x06005B78 RID: 23416 RVA: 0x0013E09A File Offset: 0x0013C29A
			protected internal override void VisitEdmEntitySet(EntitySet item)
			{
				this.Dispatch<EntitySet>(item);
				base.VisitEdmEntitySet(item);
			}

			// Token: 0x06005B79 RID: 23417 RVA: 0x0013E0AA File Offset: 0x0013C2AA
			protected override void VisitEdmAssociationSet(AssociationSet item)
			{
				this.Dispatch<AssociationSet>(item);
				base.VisitEdmAssociationSet(item);
			}

			// Token: 0x06005B7A RID: 23418 RVA: 0x0013E0BA File Offset: 0x0013C2BA
			protected override void VisitEdmAssociationSetEnd(EntitySet item)
			{
				this.Dispatch<EntitySet>(item);
				base.VisitEdmAssociationSetEnd(item);
			}

			// Token: 0x06005B7B RID: 23419 RVA: 0x0013E0CA File Offset: 0x0013C2CA
			protected override void VisitComplexType(ComplexType item)
			{
				this.Dispatch<ComplexType>(item);
				base.VisitComplexType(item);
			}

			// Token: 0x06005B7C RID: 23420 RVA: 0x0013E0DA File Offset: 0x0013C2DA
			protected internal override void VisitEdmEntityType(EntityType item)
			{
				this.Dispatch<EntityType>(item);
				this.VisitMetadataItem(item);
				if (item != null)
				{
					this.VisitDeclaredProperties(item, item.DeclaredProperties);
					this.VisitDeclaredNavigationProperties(item, item.DeclaredNavigationProperties);
				}
			}

			// Token: 0x06005B7D RID: 23421 RVA: 0x0013E107 File Offset: 0x0013C307
			protected internal override void VisitEdmAssociationType(AssociationType item)
			{
				this.Dispatch<AssociationType>(item);
				base.VisitEdmAssociationType(item);
			}

			// Token: 0x040023C3 RID: 9155
			private readonly IConvention _convention;

			// Token: 0x040023C4 RID: 9156
			private readonly DbModel _model;

			// Token: 0x040023C5 RID: 9157
			private readonly DataSpace _dataSpace;
		}

		// Token: 0x020008A8 RID: 2216
		private class PropertyConfigurationConventionDispatcher
		{
			// Token: 0x06005B7E RID: 23422 RVA: 0x0013E118 File Offset: 0x0013C318
			public PropertyConfigurationConventionDispatcher(IConvention convention, Type propertyConfigurationType, PropertyInfo propertyInfo, Func<PropertyConfiguration> propertyConfiguration, ModelConfiguration modelConfiguration)
			{
				Check.NotNull<IConvention>(convention, "convention");
				Check.NotNull<Type>(propertyConfigurationType, "propertyConfigurationType");
				Check.NotNull<PropertyInfo>(propertyInfo, "propertyInfo");
				Check.NotNull<Func<PropertyConfiguration>>(propertyConfiguration, "propertyConfiguration");
				this._convention = convention;
				this._propertyConfigurationType = propertyConfigurationType;
				this._propertyInfo = propertyInfo;
				this._propertyConfiguration = propertyConfiguration;
				this._modelConfiguration = modelConfiguration;
			}

			// Token: 0x06005B7F RID: 23423 RVA: 0x0013E181 File Offset: 0x0013C381
			public void Dispatch()
			{
				this.Dispatch<PropertyConfiguration>();
				this.Dispatch<PrimitivePropertyConfiguration>();
				this.Dispatch<LengthPropertyConfiguration>();
				this.Dispatch<DateTimePropertyConfiguration>();
				this.Dispatch<DecimalPropertyConfiguration>();
				this.Dispatch<StringPropertyConfiguration>();
				this.Dispatch<BinaryPropertyConfiguration>();
				this.Dispatch<NavigationPropertyConfiguration>();
			}

			// Token: 0x06005B80 RID: 23424 RVA: 0x0013E1B4 File Offset: 0x0013C3B4
			private void Dispatch<TPropertyConfiguration>() where TPropertyConfiguration : PropertyConfiguration
			{
				IConfigurationConvention<PropertyInfo, TPropertyConfiguration> configurationConvention = this._convention as IConfigurationConvention<PropertyInfo, TPropertyConfiguration>;
				if (configurationConvention != null && typeof(TPropertyConfiguration).IsAssignableFrom(this._propertyConfigurationType))
				{
					configurationConvention.Apply(this._propertyInfo, () => (TPropertyConfiguration)((object)this._propertyConfiguration()), this._modelConfiguration);
				}
			}

			// Token: 0x040023C6 RID: 9158
			private readonly IConvention _convention;

			// Token: 0x040023C7 RID: 9159
			private readonly Type _propertyConfigurationType;

			// Token: 0x040023C8 RID: 9160
			private readonly PropertyInfo _propertyInfo;

			// Token: 0x040023C9 RID: 9161
			private readonly Func<PropertyConfiguration> _propertyConfiguration;

			// Token: 0x040023CA RID: 9162
			private readonly ModelConfiguration _modelConfiguration;
		}
	}
}
