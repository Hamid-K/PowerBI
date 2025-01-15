using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Mapping.Update.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x02000176 RID: 374
	internal class FunctionParameterMappingGenerator : StructuralTypeMappingGenerator
	{
		// Token: 0x060016CC RID: 5836 RVA: 0x0003C56A File Offset: 0x0003A76A
		public FunctionParameterMappingGenerator(DbProviderManifest providerManifest)
			: base(providerManifest)
		{
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0003C573 File Offset: 0x0003A773
		public IEnumerable<ModificationFunctionParameterBinding> Generate(ModificationOperator modificationOperator, IEnumerable<EdmProperty> properties, IList<ColumnMappingBuilder> columnMappings, IList<EdmProperty> propertyPath, bool useOriginalValues = false)
		{
			using (IEnumerator<EdmProperty> enumerator = properties.GetEnumerator())
			{
				Func<ColumnMappingBuilder, bool> <>9__1;
				while (enumerator.MoveNext())
				{
					FunctionParameterMappingGenerator.<>c__DisplayClass1_1 CS$<>8__locals2 = new FunctionParameterMappingGenerator.<>c__DisplayClass1_1();
					CS$<>8__locals2.property = enumerator.Current;
					if (CS$<>8__locals2.property.IsComplexType && propertyPath.Any((EdmProperty p) => p.IsComplexType && p.ComplexType == CS$<>8__locals2.property.ComplexType))
					{
						throw Error.CircularComplexTypeHierarchy();
					}
					propertyPath.Add(CS$<>8__locals2.property);
					if (CS$<>8__locals2.property.IsComplexType)
					{
						foreach (ModificationFunctionParameterBinding modificationFunctionParameterBinding in this.Generate(modificationOperator, CS$<>8__locals2.property.ComplexType.Properties, columnMappings, propertyPath, useOriginalValues))
						{
							yield return modificationFunctionParameterBinding;
						}
						IEnumerator<ModificationFunctionParameterBinding> enumerator2 = null;
					}
					else
					{
						StoreGeneratedPattern? storeGeneratedPattern = CS$<>8__locals2.property.GetStoreGeneratedPattern();
						StoreGeneratedPattern storeGeneratedPattern2 = StoreGeneratedPattern.Identity;
						if (!((storeGeneratedPattern.GetValueOrDefault() == storeGeneratedPattern2) & (storeGeneratedPattern != null)) || modificationOperator != ModificationOperator.Insert)
						{
							Func<ColumnMappingBuilder, bool> func;
							if ((func = <>9__1) == null)
							{
								func = (<>9__1 = (ColumnMappingBuilder cm) => cm.PropertyPath.SequenceEqual(propertyPath));
							}
							EdmProperty columnProperty = columnMappings.First(func).ColumnProperty;
							storeGeneratedPattern = CS$<>8__locals2.property.GetStoreGeneratedPattern();
							storeGeneratedPattern2 = StoreGeneratedPattern.Computed;
							if (!((storeGeneratedPattern.GetValueOrDefault() == storeGeneratedPattern2) & (storeGeneratedPattern != null)) && (modificationOperator != ModificationOperator.Delete || CS$<>8__locals2.property.IsKeyMember))
							{
								yield return new ModificationFunctionParameterBinding(new FunctionParameter(columnProperty.Name, columnProperty.TypeUsage, ParameterMode.In), new ModificationFunctionMemberPath(propertyPath, null), !useOriginalValues);
							}
							if (modificationOperator != ModificationOperator.Insert && CS$<>8__locals2.property.ConcurrencyMode == ConcurrencyMode.Fixed)
							{
								yield return new ModificationFunctionParameterBinding(new FunctionParameter(columnProperty.Name + "_Original", columnProperty.TypeUsage, ParameterMode.In), new ModificationFunctionMemberPath(propertyPath, null), false);
							}
							columnProperty = null;
						}
					}
					propertyPath.Remove(CS$<>8__locals2.property);
					CS$<>8__locals2 = null;
				}
			}
			IEnumerator<EdmProperty> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0003C5A8 File Offset: 0x0003A7A8
		public IEnumerable<ModificationFunctionParameterBinding> Generate(IEnumerable<Tuple<ModificationFunctionMemberPath, EdmProperty>> iaFkProperties, bool useOriginalValues = false)
		{
			return from iaFkProperty in iaFkProperties
				let functionParameter = new FunctionParameter(iaFkProperty.Item2.Name, iaFkProperty.Item2.TypeUsage, ParameterMode.In)
				select new ModificationFunctionParameterBinding(functionParameter, iaFkProperty.Item1, !useOriginalValues);
		}
	}
}
