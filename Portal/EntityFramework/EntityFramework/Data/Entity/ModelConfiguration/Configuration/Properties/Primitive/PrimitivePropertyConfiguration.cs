using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive
{
	// Token: 0x02000206 RID: 518
	internal class PrimitivePropertyConfiguration : PropertyConfiguration
	{
		// Token: 0x06001B57 RID: 6999 RVA: 0x0004AEE0 File Offset: 0x000490E0
		public PrimitivePropertyConfiguration()
		{
			this.OverridableConfigurationParts = OverridableConfigurationParts.OverridableInCSpace | OverridableConfigurationParts.OverridableInSSpace;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0004AEFC File Offset: 0x000490FC
		protected PrimitivePropertyConfiguration(PrimitivePropertyConfiguration source)
		{
			Check.NotNull<PrimitivePropertyConfiguration>(source, "source");
			this.TypeConfiguration = source.TypeConfiguration;
			this.IsNullable = source.IsNullable;
			this.ConcurrencyMode = source.ConcurrencyMode;
			this.DatabaseGeneratedOption = source.DatabaseGeneratedOption;
			this.ColumnType = source.ColumnType;
			this.ColumnName = source.ColumnName;
			this.ParameterName = source.ParameterName;
			this.ColumnOrder = source.ColumnOrder;
			this.OverridableConfigurationParts = source.OverridableConfigurationParts;
			foreach (KeyValuePair<string, object> keyValuePair in source._annotations)
			{
				this._annotations.Add(keyValuePair);
			}
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0004AFD8 File Offset: 0x000491D8
		internal virtual PrimitivePropertyConfiguration Clone()
		{
			return new PrimitivePropertyConfiguration(this);
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0004AFE0 File Offset: 0x000491E0
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x0004AFE8 File Offset: 0x000491E8
		public bool? IsNullable { get; set; }

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0004AFF1 File Offset: 0x000491F1
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x0004AFF9 File Offset: 0x000491F9
		public ConcurrencyMode? ConcurrencyMode { get; set; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x0004B002 File Offset: 0x00049202
		// (set) Token: 0x06001B5F RID: 7007 RVA: 0x0004B00A File Offset: 0x0004920A
		public DatabaseGeneratedOption? DatabaseGeneratedOption { get; set; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0004B013 File Offset: 0x00049213
		// (set) Token: 0x06001B61 RID: 7009 RVA: 0x0004B01B File Offset: 0x0004921B
		public string ColumnType { get; set; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0004B024 File Offset: 0x00049224
		// (set) Token: 0x06001B63 RID: 7011 RVA: 0x0004B02C File Offset: 0x0004922C
		public string ColumnName { get; set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0004B035 File Offset: 0x00049235
		public IDictionary<string, object> Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0004B03D File Offset: 0x0004923D
		public virtual void SetAnnotation(string name, object value)
		{
			if (!name.IsValidUndottedName())
			{
				throw new ArgumentException(Strings.BadAnnotationName(name));
			}
			this._annotations[name] = value;
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0004B060 File Offset: 0x00049260
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x0004B068 File Offset: 0x00049268
		public string ParameterName { get; set; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0004B071 File Offset: 0x00049271
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x0004B079 File Offset: 0x00049279
		public int? ColumnOrder { get; set; }

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0004B082 File Offset: 0x00049282
		// (set) Token: 0x06001B6B RID: 7019 RVA: 0x0004B08A File Offset: 0x0004928A
		internal OverridableConfigurationParts OverridableConfigurationParts { get; set; }

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0004B093 File Offset: 0x00049293
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x0004B09B File Offset: 0x0004929B
		internal StructuralTypeConfiguration TypeConfiguration { get; set; }

		// Token: 0x06001B6E RID: 7022 RVA: 0x0004B0A4 File Offset: 0x000492A4
		internal virtual void Configure(EdmProperty property)
		{
			this.Clone().MergeWithExistingConfiguration(property, delegate(string errorMessage)
			{
				PropertyInfo clrPropertyInfo = property.GetClrPropertyInfo();
				string text = ((clrPropertyInfo == null) ? string.Empty : ObjectContextTypeCache.GetObjectType(clrPropertyInfo.DeclaringType).FullNameWithNesting());
				return Error.ConflictingPropertyConfiguration(property.Name, text, errorMessage);
			}, true, false).ConfigureProperty(property);
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0004B0E8 File Offset: 0x000492E8
		private PrimitivePropertyConfiguration MergeWithExistingConfiguration(EdmProperty property, Func<string, Exception> getConflictException, bool inCSpace, bool fillFromExistingConfiguration)
		{
			PrimitivePropertyConfiguration primitivePropertyConfiguration = property.GetConfiguration() as PrimitivePropertyConfiguration;
			if (primitivePropertyConfiguration == null)
			{
				return this;
			}
			OverridableConfigurationParts overridableConfigurationParts = (inCSpace ? OverridableConfigurationParts.OverridableInCSpace : OverridableConfigurationParts.OverridableInSSpace);
			if (primitivePropertyConfiguration.OverridableConfigurationParts.HasFlag(overridableConfigurationParts) || fillFromExistingConfiguration)
			{
				return primitivePropertyConfiguration.OverrideFrom(this, inCSpace);
			}
			string text;
			if (this.OverridableConfigurationParts.HasFlag(overridableConfigurationParts) || primitivePropertyConfiguration.IsCompatible(this, inCSpace, out text))
			{
				return this.OverrideFrom(primitivePropertyConfiguration, inCSpace);
			}
			throw getConflictException(text);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0004B166 File Offset: 0x00049366
		private PrimitivePropertyConfiguration OverrideFrom(PrimitivePropertyConfiguration overridingConfiguration, bool inCSpace)
		{
			if (overridingConfiguration.GetType().IsAssignableFrom(base.GetType()))
			{
				this.MakeCompatibleWith(overridingConfiguration, inCSpace);
				this.FillFrom(overridingConfiguration, inCSpace);
				return this;
			}
			overridingConfiguration.FillFrom(this, inCSpace);
			return overridingConfiguration;
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0004B198 File Offset: 0x00049398
		protected virtual void ConfigureProperty(EdmProperty property)
		{
			if (this.IsNullable != null)
			{
				property.Nullable = this.IsNullable.Value;
			}
			if (this.ConcurrencyMode != null)
			{
				property.ConcurrencyMode = this.ConcurrencyMode.Value;
			}
			if (this.DatabaseGeneratedOption != null)
			{
				property.SetStoreGeneratedPattern((StoreGeneratedPattern)this.DatabaseGeneratedOption.Value);
				if (this.DatabaseGeneratedOption.Value == global::System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)
				{
					property.Nullable = false;
				}
			}
			property.SetConfiguration(this);
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0004B230 File Offset: 0x00049430
		internal void Configure(IEnumerable<Tuple<ColumnMappingBuilder, EntityType>> propertyMappings, DbProviderManifest providerManifest, bool allowOverride = false, bool fillFromExistingConfiguration = false)
		{
			propertyMappings.Each(delegate(Tuple<ColumnMappingBuilder, EntityType> pm)
			{
				this.Configure(pm.Item1.ColumnProperty, pm.Item2, providerManifest, allowOverride, fillFromExistingConfiguration);
			});
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0004B272 File Offset: 0x00049472
		internal void ConfigureFunctionParameters(IEnumerable<FunctionParameter> parameters)
		{
			parameters.Each(new Action<FunctionParameter>(this.ConfigureParameterName));
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0004B288 File Offset: 0x00049488
		private void ConfigureParameterName(FunctionParameter parameter)
		{
			if (string.IsNullOrWhiteSpace(this.ParameterName) || string.Equals(this.ParameterName, parameter.Name, StringComparison.Ordinal))
			{
				return;
			}
			parameter.Name = this.ParameterName;
			IEnumerable<FunctionParameter> enumerable = from p in parameter.DeclaringFunction.Parameters
				let configuration = p.GetConfiguration() as PrimitivePropertyConfiguration
				where p != parameter && string.Equals(this.ParameterName, p.Name, StringComparison.Ordinal) && (configuration == null || configuration.ParameterName == null)
				select p;
			List<FunctionParameter> renamedParameters = new List<FunctionParameter> { parameter };
			enumerable.Each(delegate(FunctionParameter c)
			{
				c.Name = renamedParameters.UniquifyName(this.ParameterName);
				renamedParameters.Add(c);
			});
			parameter.SetConfiguration(this);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0004B380 File Offset: 0x00049580
		internal void Configure(EdmProperty column, EntityType table, DbProviderManifest providerManifest, bool allowOverride = false, bool fillFromExistingConfiguration = false)
		{
			PrimitivePropertyConfiguration primitivePropertyConfiguration = this.Clone();
			if (allowOverride)
			{
				primitivePropertyConfiguration.OverridableConfigurationParts |= OverridableConfigurationParts.OverridableInSSpace;
			}
			primitivePropertyConfiguration.MergeWithExistingConfiguration(column, (string errorMessage) => Error.ConflictingColumnConfiguration(column.Name, table.Name, errorMessage), false, fillFromExistingConfiguration).ConfigureColumn(column, table, providerManifest);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0004B3E8 File Offset: 0x000495E8
		protected virtual void ConfigureColumn(EdmProperty column, EntityType table, DbProviderManifest providerManifest)
		{
			this.ConfigureColumnName(column, table);
			this.ConfigureAnnotations(column);
			if (!string.IsNullOrWhiteSpace(this.ColumnType))
			{
				column.PrimitiveType = providerManifest.GetStoreTypeFromName(this.ColumnType);
			}
			if (this.ColumnOrder != null)
			{
				column.SetOrder(this.ColumnOrder.Value);
			}
			PrimitiveType primitiveType = providerManifest.GetStoreTypes().SingleOrDefault((PrimitiveType t) => t.Name.Equals(column.TypeName, StringComparison.OrdinalIgnoreCase));
			if (primitiveType != null)
			{
				primitiveType.FacetDescriptions.Each(delegate(FacetDescription f)
				{
					this.Configure(column, f);
				});
			}
			column.SetConfiguration(this);
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0004B4B0 File Offset: 0x000496B0
		private void ConfigureColumnName(EdmProperty column, EntityType table)
		{
			if (string.IsNullOrWhiteSpace(this.ColumnName) || string.Equals(this.ColumnName, column.Name, StringComparison.Ordinal))
			{
				return;
			}
			column.Name = this.ColumnName;
			IEnumerable<EdmProperty> enumerable = from c in table.Properties
				let configuration = c.GetConfiguration() as PrimitivePropertyConfiguration
				where c != column && string.Equals(this.ColumnName, c.GetPreferredName(), StringComparison.Ordinal) && (configuration == null || configuration.ColumnName == null)
				select c;
			List<EdmProperty> renamedColumns = new List<EdmProperty> { column };
			enumerable.Each(delegate(EdmProperty c)
			{
				c.Name = renamedColumns.UniquifyName(this.ColumnName);
				renamedColumns.Add(c);
			});
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0004B590 File Offset: 0x00049790
		private void ConfigureAnnotations(EdmProperty column)
		{
			foreach (KeyValuePair<string, object> keyValuePair in this._annotations)
			{
				column.AddAnnotation("http://schemas.microsoft.com/ado/2013/11/edm/customannotation:" + keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0004B5F4 File Offset: 0x000497F4
		internal virtual void Configure(EdmProperty column, FacetDescription facetDescription)
		{
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0004B5F8 File Offset: 0x000497F8
		internal virtual void CopyFrom(PrimitivePropertyConfiguration other)
		{
			if (this == other)
			{
				return;
			}
			this.ColumnName = other.ColumnName;
			this.ParameterName = other.ParameterName;
			this.ColumnOrder = other.ColumnOrder;
			this.ColumnType = other.ColumnType;
			this.ConcurrencyMode = other.ConcurrencyMode;
			this.DatabaseGeneratedOption = other.DatabaseGeneratedOption;
			this.IsNullable = other.IsNullable;
			this.OverridableConfigurationParts = other.OverridableConfigurationParts;
			this._annotations.Clear();
			foreach (KeyValuePair<string, object> keyValuePair in other._annotations)
			{
				this._annotations[keyValuePair.Key] = keyValuePair.Value;
			}
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0004B6C8 File Offset: 0x000498C8
		internal virtual void FillFrom(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			if (this == other)
			{
				return;
			}
			if (inCSpace)
			{
				if (this.ConcurrencyMode == null)
				{
					this.ConcurrencyMode = other.ConcurrencyMode;
				}
				if (this.DatabaseGeneratedOption == null)
				{
					this.DatabaseGeneratedOption = other.DatabaseGeneratedOption;
				}
				if (this.IsNullable == null)
				{
					this.IsNullable = other.IsNullable;
				}
				if (!other.OverridableConfigurationParts.HasFlag(OverridableConfigurationParts.OverridableInCSpace))
				{
					this.OverridableConfigurationParts &= ~OverridableConfigurationParts.OverridableInCSpace;
					return;
				}
			}
			else
			{
				if (this.ColumnName == null)
				{
					this.ColumnName = other.ColumnName;
				}
				if (this.ParameterName == null)
				{
					this.ParameterName = other.ParameterName;
				}
				if (this.ColumnOrder == null)
				{
					this.ColumnOrder = other.ColumnOrder;
				}
				if (this.ColumnType == null)
				{
					this.ColumnType = other.ColumnType;
				}
				foreach (KeyValuePair<string, object> keyValuePair in other._annotations)
				{
					if (this._annotations.ContainsKey(keyValuePair.Key))
					{
						IMergeableAnnotation mergeableAnnotation = this._annotations[keyValuePair.Key] as IMergeableAnnotation;
						if (mergeableAnnotation != null)
						{
							this._annotations[keyValuePair.Key] = mergeableAnnotation.MergeWith(keyValuePair.Value);
						}
					}
					else
					{
						this._annotations[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				if (!other.OverridableConfigurationParts.HasFlag(OverridableConfigurationParts.OverridableInSSpace))
				{
					this.OverridableConfigurationParts &= ~OverridableConfigurationParts.OverridableInSSpace;
				}
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0004B888 File Offset: 0x00049A88
		internal virtual void MakeCompatibleWith(PrimitivePropertyConfiguration other, bool inCSpace)
		{
			if (this == other)
			{
				return;
			}
			if (inCSpace)
			{
				if (other.ConcurrencyMode != null)
				{
					this.ConcurrencyMode = null;
				}
				if (other.DatabaseGeneratedOption != null)
				{
					this.DatabaseGeneratedOption = null;
				}
				if (other.IsNullable != null)
				{
					this.IsNullable = null;
					return;
				}
			}
			else
			{
				if (other.ColumnName != null)
				{
					this.ColumnName = null;
				}
				if (other.ParameterName != null)
				{
					this.ParameterName = null;
				}
				if (other.ColumnOrder != null)
				{
					this.ColumnOrder = null;
				}
				if (other.ColumnType != null)
				{
					this.ColumnType = null;
				}
				foreach (string text in other._annotations.Keys)
				{
					if (this._annotations.ContainsKey(text))
					{
						IMergeableAnnotation mergeableAnnotation = this._annotations[text] as IMergeableAnnotation;
						if (mergeableAnnotation == null || !mergeableAnnotation.IsCompatibleWith(other._annotations[text]))
						{
							this._annotations.Remove(text);
						}
					}
				}
			}
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0004B9E0 File Offset: 0x00049BE0
		internal virtual bool IsCompatible(PrimitivePropertyConfiguration other, bool inCSpace, out string errorMessage)
		{
			errorMessage = string.Empty;
			if (other == null || this == other)
			{
				return true;
			}
			bool flag = !inCSpace || this.IsCompatible<bool, PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.IsNullable, other, ref errorMessage);
			bool flag2 = !inCSpace || this.IsCompatible<ConcurrencyMode, PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.ConcurrencyMode, other, ref errorMessage);
			bool flag3 = !inCSpace || this.IsCompatible<DatabaseGeneratedOption, PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.DatabaseGeneratedOption, other, ref errorMessage);
			bool flag4 = inCSpace || this.IsCompatible<PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.ColumnName, other, ref errorMessage);
			bool flag5 = inCSpace || this.IsCompatible<PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.ParameterName, other, ref errorMessage);
			bool flag6 = inCSpace || this.IsCompatible<int, PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.ColumnOrder, other, ref errorMessage);
			bool flag7 = inCSpace || this.IsCompatible<PrimitivePropertyConfiguration>((PrimitivePropertyConfiguration c) => c.ColumnType, other, ref errorMessage);
			bool flag8 = inCSpace || this.AnnotationsAreCompatible(other, ref errorMessage);
			return flag && flag2 && flag3 && flag4 && flag5 && flag6 && flag7 && flag8;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0004BC30 File Offset: 0x00049E30
		private bool AnnotationsAreCompatible(PrimitivePropertyConfiguration other, ref string errorMessage)
		{
			bool flag = true;
			foreach (KeyValuePair<string, object> keyValuePair in this.Annotations)
			{
				if (other.Annotations.ContainsKey(keyValuePair.Key))
				{
					object value = keyValuePair.Value;
					object obj = other.Annotations[keyValuePair.Key];
					IMergeableAnnotation mergeableAnnotation = value as IMergeableAnnotation;
					if (mergeableAnnotation != null)
					{
						CompatibilityResult compatibilityResult = mergeableAnnotation.IsCompatibleWith(obj);
						if (!compatibilityResult)
						{
							flag = false;
							errorMessage = errorMessage + Environment.NewLine + "\t" + compatibilityResult.ErrorMessage;
						}
					}
					else if (!object.Equals(value, obj))
					{
						flag = false;
						errorMessage = errorMessage + Environment.NewLine + "\t" + Strings.ConflictingAnnotationValue(keyValuePair.Key, value.ToString(), obj.ToString());
					}
				}
			}
			return flag;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0004BD28 File Offset: 0x00049F28
		protected bool IsCompatible<TProperty, TConfiguration>(Expression<Func<TConfiguration, TProperty?>> propertyExpression, TConfiguration other, ref string errorMessage) where TProperty : struct where TConfiguration : PrimitivePropertyConfiguration
		{
			Check.NotNull<Expression<Func<TConfiguration, TProperty?>>>(propertyExpression, "propertyExpression");
			Check.NotNull<TConfiguration>(other, "other");
			PropertyInfo propertyInfo = propertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			TProperty? tproperty = (TProperty?)propertyInfo.GetValue(this, null);
			TProperty? tproperty2 = (TProperty?)propertyInfo.GetValue(other, null);
			if (PrimitivePropertyConfiguration.IsCompatible<TProperty>(tproperty, tproperty2))
			{
				return true;
			}
			errorMessage = errorMessage + Environment.NewLine + "\t" + Strings.ConflictingConfigurationValue(propertyInfo.Name, tproperty, propertyInfo.Name, tproperty2);
			return false;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0004BDB8 File Offset: 0x00049FB8
		protected bool IsCompatible<TConfiguration>(Expression<Func<TConfiguration, string>> propertyExpression, TConfiguration other, ref string errorMessage) where TConfiguration : PrimitivePropertyConfiguration
		{
			Check.NotNull<Expression<Func<TConfiguration, string>>>(propertyExpression, "propertyExpression");
			Check.NotNull<TConfiguration>(other, "other");
			PropertyInfo propertyInfo = propertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			string text = (string)propertyInfo.GetValue(this, null);
			string text2 = (string)propertyInfo.GetValue(other, null);
			if (PrimitivePropertyConfiguration.IsCompatible(text, text2))
			{
				return true;
			}
			errorMessage = errorMessage + Environment.NewLine + "\t" + Strings.ConflictingConfigurationValue(propertyInfo.Name, text, propertyInfo.Name, text2);
			return false;
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0004BE3C File Offset: 0x0004A03C
		protected static bool IsCompatible<T>(T? thisConfiguration, T? other) where T : struct
		{
			return thisConfiguration == null || other == null || object.Equals(thisConfiguration.Value, other.Value);
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0004BE71 File Offset: 0x0004A071
		protected static bool IsCompatible(string thisConfiguration, string other)
		{
			return thisConfiguration == null || other == null || object.Equals(thisConfiguration, other);
		}

		// Token: 0x04000ABE RID: 2750
		private readonly IDictionary<string, object> _annotations = new Dictionary<string, object>();
	}
}
