using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001D9 RID: 473
	internal class ModificationStoredProcedureConfiguration
	{
		// Token: 0x060018C3 RID: 6339 RVA: 0x00042AE5 File Offset: 0x00040CE5
		public ModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00042B04 File Offset: 0x00040D04
		private ModificationStoredProcedureConfiguration(ModificationStoredProcedureConfiguration source)
		{
			this._name = source._name;
			this._schema = source._schema;
			this._rowsAffectedParameter = source._rowsAffectedParameter;
			source._parameterNames.Each(delegate(KeyValuePair<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>> c)
			{
				this._parameterNames.Add(c.Key, Tuple.Create<string, string>(c.Value.Item1, c.Value.Item2));
			});
			source._resultBindings.Each(delegate(KeyValuePair<PropertyInfo, string> r)
			{
				this._resultBindings.Add(r.Key, r.Value);
			});
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x00042B7F File Offset: 0x00040D7F
		public virtual ModificationStoredProcedureConfiguration Clone()
		{
			return new ModificationStoredProcedureConfiguration(this);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x00042B88 File Offset: 0x00040D88
		public void HasName(string name)
		{
			DatabaseName databaseName = DatabaseName.Parse(name);
			this._name = databaseName.Name;
			this._schema = databaseName.Schema;
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x00042BB4 File Offset: 0x00040DB4
		public void HasName(string name, string schema)
		{
			this._name = name;
			this._schema = schema;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00042BC4 File Offset: 0x00040DC4
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00042BCC File Offset: 0x00040DCC
		public string Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x00042BD4 File Offset: 0x00040DD4
		public void RowsAffectedParameter(string name)
		{
			this._rowsAffectedParameter = name;
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x00042BDD File Offset: 0x00040DDD
		public string RowsAffectedParameterName
		{
			get
			{
				return this._rowsAffectedParameter;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x00042BE5 File Offset: 0x00040DE5
		public IEnumerable<Tuple<string, string>> ParameterNames
		{
			get
			{
				return this._parameterNames.Values;
			}
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00042BF2 File Offset: 0x00040DF2
		public void ClearParameterNames()
		{
			this._parameterNames.Clear();
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00042BFF File Offset: 0x00040DFF
		public Dictionary<PropertyInfo, string> ResultBindings
		{
			get
			{
				return this._resultBindings;
			}
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x00042C07 File Offset: 0x00040E07
		public void Parameter(PropertyPath propertyPath, string parameterName, string originalValueParameterName = null, bool rightKey = false)
		{
			this._parameterNames[new ModificationStoredProcedureConfiguration.ParameterKey(propertyPath, rightKey)] = Tuple.Create<string, string>(parameterName, originalValueParameterName);
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00042C23 File Offset: 0x00040E23
		public void Result(PropertyPath propertyPath, string columnName)
		{
			this._resultBindings[propertyPath.Single<PropertyInfo>()] = columnName;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00042C37 File Offset: 0x00040E37
		public virtual void Configure(ModificationFunctionMapping modificationStoredProcedureMapping, DbProviderManifest providerManifest)
		{
			this._configuredParameters = new List<FunctionParameter>();
			this.ConfigureName(modificationStoredProcedureMapping);
			this.ConfigureSchema(modificationStoredProcedureMapping);
			this.ConfigureRowsAffectedParameter(modificationStoredProcedureMapping, providerManifest);
			this.ConfigureParameters(modificationStoredProcedureMapping);
			this.ConfigureResultBindings(modificationStoredProcedureMapping);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00042C68 File Offset: 0x00040E68
		private void ConfigureName(ModificationFunctionMapping modificationStoredProcedureMapping)
		{
			if (!string.IsNullOrWhiteSpace(this._name))
			{
				modificationStoredProcedureMapping.Function.StoreFunctionNameAttribute = this._name;
			}
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00042C88 File Offset: 0x00040E88
		private void ConfigureSchema(ModificationFunctionMapping modificationStoredProcedureMapping)
		{
			if (!string.IsNullOrWhiteSpace(this._schema))
			{
				modificationStoredProcedureMapping.Function.Schema = this._schema;
			}
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00042CA8 File Offset: 0x00040EA8
		private void ConfigureRowsAffectedParameter(ModificationFunctionMapping modificationStoredProcedureMapping, DbProviderManifest providerManifest)
		{
			if (!string.IsNullOrWhiteSpace(this._rowsAffectedParameter))
			{
				if (modificationStoredProcedureMapping.RowsAffectedParameter == null)
				{
					FunctionParameter functionParameter = new FunctionParameter("_RowsAffected_", providerManifest.GetStoreType(TypeUsage.CreateDefaultTypeUsage(PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32))), ParameterMode.Out);
					modificationStoredProcedureMapping.Function.AddParameter(functionParameter);
					modificationStoredProcedureMapping.RowsAffectedParameter = functionParameter;
				}
				modificationStoredProcedureMapping.RowsAffectedParameter.Name = this._rowsAffectedParameter;
				this._configuredParameters.Add(modificationStoredProcedureMapping.RowsAffectedParameter);
			}
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00042D20 File Offset: 0x00040F20
		private void ConfigureParameters(ModificationFunctionMapping modificationStoredProcedureMapping)
		{
			foreach (KeyValuePair<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>> keyValuePair in this._parameterNames)
			{
				PropertyPath propertyPath = keyValuePair.Key.PropertyPath;
				string item = keyValuePair.Value.Item1;
				string item2 = keyValuePair.Value.Item2;
				Func<PropertyInfo, bool> <>9__4;
				List<ModificationFunctionParameterBinding> list = modificationStoredProcedureMapping.ParameterBindings.Where(delegate(ModificationFunctionParameterBinding pb)
				{
					if (pb.MemberPath.AssociationSetEnd == null || pb.MemberPath.AssociationSetEnd.ParentAssociationSet.ElementType.IsManyToMany())
					{
						if (propertyPath.Equals(new PropertyPath(from m in pb.MemberPath.Members.OfType<EdmProperty>()
							select m.GetClrPropertyInfo())))
						{
							return true;
						}
					}
					if (propertyPath.Count == 2 && pb.MemberPath.AssociationSetEnd != null && pb.MemberPath.Members.First<EdmMember>().GetClrPropertyInfo().IsSameAs(propertyPath.Last<PropertyInfo>()))
					{
						IEnumerable<PropertyInfo> enumerable = from ae in pb.MemberPath.AssociationSetEnd.ParentAssociationSet.AssociationSetEnds
							select ae.CorrespondingAssociationEndMember.GetClrPropertyInfo() into pi
							where pi != null
							select pi;
						Func<PropertyInfo, bool> func;
						if ((func = <>9__4) == null)
						{
							func = (<>9__4 = (PropertyInfo pi) => pi.IsSameAs(propertyPath.First<PropertyInfo>()));
						}
						return enumerable.Any(func);
					}
					return false;
				}).ToList<ModificationFunctionParameterBinding>();
				if (list.Count == 1)
				{
					ModificationFunctionParameterBinding modificationFunctionParameterBinding = list.Single<ModificationFunctionParameterBinding>();
					if (!string.IsNullOrWhiteSpace(item2) && modificationFunctionParameterBinding.IsCurrent)
					{
						throw Error.ModificationFunctionParameterNotFoundOriginal(propertyPath, modificationStoredProcedureMapping.Function.FunctionName);
					}
					modificationFunctionParameterBinding.Parameter.Name = item;
					this._configuredParameters.Add(modificationFunctionParameterBinding.Parameter);
				}
				else
				{
					if (list.Count == 2)
					{
						if (list.Select((ModificationFunctionParameterBinding pb) => pb.IsCurrent).Distinct<bool>().Count<bool>() != 1)
						{
							goto IL_0132;
						}
						if (!list.All((ModificationFunctionParameterBinding pb) => pb.MemberPath.AssociationSetEnd != null))
						{
							goto IL_0132;
						}
						ModificationFunctionParameterBinding modificationFunctionParameterBinding2 = ((!keyValuePair.Key.IsRightKey) ? list.First<ModificationFunctionParameterBinding>() : list.Last<ModificationFunctionParameterBinding>());
						IL_0178:
						ModificationFunctionParameterBinding modificationFunctionParameterBinding3 = modificationFunctionParameterBinding2;
						modificationFunctionParameterBinding3.Parameter.Name = item;
						this._configuredParameters.Add(modificationFunctionParameterBinding3.Parameter);
						if (!string.IsNullOrWhiteSpace(item2))
						{
							modificationFunctionParameterBinding3 = list.Single((ModificationFunctionParameterBinding pb) => !pb.IsCurrent);
							modificationFunctionParameterBinding3.Parameter.Name = item2;
							this._configuredParameters.Add(modificationFunctionParameterBinding3.Parameter);
							continue;
						}
						continue;
						IL_0132:
						modificationFunctionParameterBinding2 = list.Single((ModificationFunctionParameterBinding pb) => pb.IsCurrent);
						goto IL_0178;
					}
					throw Error.ModificationFunctionParameterNotFound(propertyPath, modificationStoredProcedureMapping.Function.FunctionName);
				}
			}
			foreach (FunctionParameter functionParameter in modificationStoredProcedureMapping.Function.Parameters.Except(this._configuredParameters))
			{
				functionParameter.Name = modificationStoredProcedureMapping.Function.Parameters.Except(new FunctionParameter[] { functionParameter }).UniquifyName(functionParameter.Name);
			}
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00042FF0 File Offset: 0x000411F0
		private void ConfigureResultBindings(ModificationFunctionMapping modificationStoredProcedureMapping)
		{
			foreach (KeyValuePair<PropertyInfo, string> keyValuePair in this._resultBindings)
			{
				PropertyInfo propertyInfo = keyValuePair.Key;
				string value = keyValuePair.Value;
				IEnumerable<ModificationFunctionResultBinding> resultBindings = modificationStoredProcedureMapping.ResultBindings;
				ModificationFunctionResultBinding modificationFunctionResultBinding = (resultBindings ?? Enumerable.Empty<ModificationFunctionResultBinding>()).SingleOrDefault((ModificationFunctionResultBinding rb) => propertyInfo.IsSameAs(rb.Property.GetClrPropertyInfo()));
				if (modificationFunctionResultBinding == null)
				{
					throw Error.ResultBindingNotFound(propertyInfo.Name, modificationStoredProcedureMapping.Function.FunctionName);
				}
				modificationFunctionResultBinding.ColumnName = value;
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000430A0 File Offset: 0x000412A0
		public bool IsCompatibleWith(ModificationStoredProcedureConfiguration other)
		{
			if (this._name != null && other._name != null && !string.Equals(this._name, other._name, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			if (this._schema != null && other._schema != null && !string.Equals(this._schema, other._schema, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			return !(from kv1 in this._parameterNames
				join kv2 in other._parameterNames on kv1.Key equals kv2.Key
				select !object.Equals(kv1.Value, kv2.Value)).Any((bool j) => j);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x00043190 File Offset: 0x00041390
		public void Merge(ModificationStoredProcedureConfiguration modificationStoredProcedureConfiguration, bool allowOverride)
		{
			if (allowOverride || string.IsNullOrWhiteSpace(this._name))
			{
				this._name = modificationStoredProcedureConfiguration.Name ?? this._name;
			}
			if (allowOverride || string.IsNullOrWhiteSpace(this._schema))
			{
				this._schema = modificationStoredProcedureConfiguration.Schema ?? this._schema;
			}
			if (allowOverride || string.IsNullOrWhiteSpace(this._rowsAffectedParameter))
			{
				this._rowsAffectedParameter = modificationStoredProcedureConfiguration.RowsAffectedParameterName ?? this._rowsAffectedParameter;
			}
			IEnumerable<KeyValuePair<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>>> parameterNames = modificationStoredProcedureConfiguration._parameterNames;
			Func<KeyValuePair<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>>, bool> <>9__0;
			Func<KeyValuePair<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>>, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (KeyValuePair<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>> parameterName) => allowOverride || !this._parameterNames.ContainsKey(parameterName.Key));
			}
			foreach (KeyValuePair<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>> keyValuePair in parameterNames.Where(func))
			{
				this._parameterNames[keyValuePair.Key] = keyValuePair.Value;
			}
			IEnumerable<KeyValuePair<PropertyInfo, string>> resultBindings = modificationStoredProcedureConfiguration.ResultBindings;
			Func<KeyValuePair<PropertyInfo, string>, bool> <>9__1;
			Func<KeyValuePair<PropertyInfo, string>, bool> func2;
			if ((func2 = <>9__1) == null)
			{
				func2 = (<>9__1 = (KeyValuePair<PropertyInfo, string> resultBinding) => allowOverride || !this._resultBindings.ContainsKey(resultBinding.Key));
			}
			foreach (KeyValuePair<PropertyInfo, string> keyValuePair2 in resultBindings.Where(func2))
			{
				this._resultBindings[keyValuePair2.Key] = keyValuePair2.Value;
			}
		}

		// Token: 0x04000A69 RID: 2665
		private readonly Dictionary<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>> _parameterNames = new Dictionary<ModificationStoredProcedureConfiguration.ParameterKey, Tuple<string, string>>();

		// Token: 0x04000A6A RID: 2666
		private readonly Dictionary<PropertyInfo, string> _resultBindings = new Dictionary<PropertyInfo, string>();

		// Token: 0x04000A6B RID: 2667
		private string _name;

		// Token: 0x04000A6C RID: 2668
		private string _schema;

		// Token: 0x04000A6D RID: 2669
		private string _rowsAffectedParameter;

		// Token: 0x04000A6E RID: 2670
		private List<FunctionParameter> _configuredParameters;

		// Token: 0x020008AB RID: 2219
		private sealed class ParameterKey
		{
			// Token: 0x06005B8C RID: 23436 RVA: 0x0013E2AE File Offset: 0x0013C4AE
			public ParameterKey(PropertyPath propertyPath, bool rightKey)
			{
				this._propertyPath = propertyPath;
				this._rightKey = rightKey;
			}

			// Token: 0x1700106B RID: 4203
			// (get) Token: 0x06005B8D RID: 23437 RVA: 0x0013E2C4 File Offset: 0x0013C4C4
			public PropertyPath PropertyPath
			{
				get
				{
					return this._propertyPath;
				}
			}

			// Token: 0x1700106C RID: 4204
			// (get) Token: 0x06005B8E RID: 23438 RVA: 0x0013E2CC File Offset: 0x0013C4CC
			public bool IsRightKey
			{
				get
				{
					return this._rightKey;
				}
			}

			// Token: 0x06005B8F RID: 23439 RVA: 0x0013E2D4 File Offset: 0x0013C4D4
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (this == obj)
				{
					return true;
				}
				ModificationStoredProcedureConfiguration.ParameterKey parameterKey = (ModificationStoredProcedureConfiguration.ParameterKey)obj;
				return this._propertyPath.Equals(parameterKey._propertyPath) && this._rightKey.Equals(parameterKey._rightKey);
			}

			// Token: 0x06005B90 RID: 23440 RVA: 0x0013E31C File Offset: 0x0013C51C
			public override int GetHashCode()
			{
				return (this._propertyPath.GetHashCode() * 397) ^ this._rightKey.GetHashCode();
			}

			// Token: 0x040023D3 RID: 9171
			private readonly PropertyPath _propertyPath;

			// Token: 0x040023D4 RID: 9172
			private readonly bool _rightKey;
		}
	}
}
