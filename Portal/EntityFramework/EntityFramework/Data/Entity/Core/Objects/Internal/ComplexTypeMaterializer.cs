using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000438 RID: 1080
	internal class ComplexTypeMaterializer
	{
		// Token: 0x060034B3 RID: 13491 RVA: 0x000A93F1 File Offset: 0x000A75F1
		internal ComplexTypeMaterializer(MetadataWorkspace workspace)
		{
			this._workspace = workspace;
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x000A9400 File Offset: 0x000A7600
		internal object CreateComplex(IExtendedDataRecord record, DataRecordInfo recordInfo, object result)
		{
			ComplexTypeMaterializer.Plan plan = this.GetPlan(recordInfo);
			if (result == null)
			{
				result = plan.ClrType();
			}
			this.SetProperties(record, result, plan.Properties);
			return result;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x000A9434 File Offset: 0x000A7634
		private void SetProperties(IExtendedDataRecord record, object result, ComplexTypeMaterializer.PlanEdmProperty[] properties)
		{
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i].GetExistingComplex != null)
				{
					object obj = properties[i].GetExistingComplex(result);
					object obj2 = this.CreateComplexRecursive(record.GetValue(properties[i].Ordinal), obj);
					if (obj == null)
					{
						properties[i].ClrProperty(result, obj2);
					}
				}
				else
				{
					properties[i].ClrProperty(result, ComplexTypeMaterializer.ConvertDBNull(record.GetValue(properties[i].Ordinal)));
				}
			}
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x000A94D1 File Offset: 0x000A76D1
		private static object ConvertDBNull(object value)
		{
			if (DBNull.Value == value)
			{
				return null;
			}
			return value;
		}

		// Token: 0x060034B7 RID: 13495 RVA: 0x000A94DE File Offset: 0x000A76DE
		private object CreateComplexRecursive(object record, object existing)
		{
			if (DBNull.Value == record)
			{
				return existing;
			}
			return this.CreateComplexRecursive((IExtendedDataRecord)record, existing);
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000A94F7 File Offset: 0x000A76F7
		private object CreateComplexRecursive(IExtendedDataRecord record, object existing)
		{
			return this.CreateComplex(record, record.DataRecordInfo, existing);
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000A9508 File Offset: 0x000A7708
		private ComplexTypeMaterializer.Plan GetPlan(DataRecordInfo recordInfo)
		{
			ComplexTypeMaterializer.Plan[] array;
			if ((array = this._lastPlans) == null)
			{
				array = (this._lastPlans = new ComplexTypeMaterializer.Plan[4]);
			}
			ComplexTypeMaterializer.Plan[] array2 = array;
			int num = this._lastPlanIndex - 1;
			for (int i = 0; i < 4; i++)
			{
				num = (num + 1) % 4;
				if (array2[num] == null)
				{
					break;
				}
				if (array2[num].Key == recordInfo.RecordType)
				{
					this._lastPlanIndex = num;
					return array2[num];
				}
			}
			ObjectTypeMapping objectMapping = Util.GetObjectMapping(recordInfo.RecordType.EdmType, this._workspace);
			this._lastPlanIndex = num;
			array2[num] = new ComplexTypeMaterializer.Plan(recordInfo.RecordType, objectMapping, recordInfo.FieldMetadata);
			return array2[num];
		}

		// Token: 0x040010F8 RID: 4344
		private readonly MetadataWorkspace _workspace;

		// Token: 0x040010F9 RID: 4345
		private const int MaxPlanCount = 4;

		// Token: 0x040010FA RID: 4346
		private ComplexTypeMaterializer.Plan[] _lastPlans;

		// Token: 0x040010FB RID: 4347
		private int _lastPlanIndex;

		// Token: 0x02000A41 RID: 2625
		private sealed class Plan
		{
			// Token: 0x06006155 RID: 24917 RVA: 0x0014F49C File Offset: 0x0014D69C
			internal Plan(TypeUsage key, ObjectTypeMapping mapping, ReadOnlyCollection<FieldMetadata> fields)
			{
				this.Key = key;
				this.ClrType = DelegateFactory.GetConstructorDelegateForType((ClrComplexType)mapping.ClrType);
				this.Properties = new ComplexTypeMaterializer.PlanEdmProperty[fields.Count];
				for (int i = 0; i < this.Properties.Length; i++)
				{
					FieldMetadata fieldMetadata = fields[i];
					int ordinal = fieldMetadata.Ordinal;
					this.Properties[i] = new ComplexTypeMaterializer.PlanEdmProperty(ordinal, mapping.GetPropertyMap(fieldMetadata.FieldType.Name).ClrProperty);
				}
			}

			// Token: 0x04002A2F RID: 10799
			internal readonly TypeUsage Key;

			// Token: 0x04002A30 RID: 10800
			internal readonly Func<object> ClrType;

			// Token: 0x04002A31 RID: 10801
			internal readonly ComplexTypeMaterializer.PlanEdmProperty[] Properties;
		}

		// Token: 0x02000A42 RID: 2626
		private struct PlanEdmProperty
		{
			// Token: 0x06006156 RID: 24918 RVA: 0x0014F52B File Offset: 0x0014D72B
			internal PlanEdmProperty(int ordinal, EdmProperty property)
			{
				this.Ordinal = ordinal;
				this.GetExistingComplex = (Helper.IsComplexType(property.TypeUsage.EdmType) ? DelegateFactory.GetGetterDelegateForProperty(property) : null);
				this.ClrProperty = DelegateFactory.GetSetterDelegateForProperty(property);
			}

			// Token: 0x04002A32 RID: 10802
			internal readonly int Ordinal;

			// Token: 0x04002A33 RID: 10803
			internal readonly Func<object, object> GetExistingComplex;

			// Token: 0x04002A34 RID: 10804
			internal readonly Action<object, object> ClrProperty;
		}
	}
}
