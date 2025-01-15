using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000538 RID: 1336
	internal class FunctionImportMappingComposableHelper
	{
		// Token: 0x060041BC RID: 16828 RVA: 0x000DE1FC File Offset: 0x000DC3FC
		internal FunctionImportMappingComposableHelper(EntityContainerMapping entityContainerMapping, string sourceLocation, List<EdmSchemaError> parsingErrors)
		{
			this._entityContainerMapping = entityContainerMapping;
			this.m_sourceLocation = sourceLocation;
			this.m_parsingErrors = parsingErrors;
		}

		// Token: 0x060041BD RID: 16829 RVA: 0x000DE21C File Offset: 0x000DC41C
		internal bool TryCreateFunctionImportMappingComposableWithStructuralResult(EdmFunction functionImport, EdmFunction cTypeTargetFunction, List<FunctionImportStructuralTypeMapping> typeMappings, RowType cTypeTvfElementType, RowType sTypeTvfElementType, IXmlLineInfo lineInfo, out FunctionImportMappingComposable mapping)
		{
			mapping = null;
			StructuralType structuralType;
			if (typeMappings.Count == 0 && MetadataHelper.TryGetFunctionImportReturnType<StructuralType>(functionImport, 0, out structuralType))
			{
				if (structuralType.Abstract)
				{
					FunctionImportMappingComposableHelper.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_FunctionImport_ImplicitMappingForAbstractReturnType), structuralType.FullName, functionImport.Identity, MappingErrorCode.MappingOfAbstractType, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return false;
				}
				if (structuralType.BuiltInTypeKind == BuiltInTypeKind.EntityType)
				{
					typeMappings.Add(new FunctionImportEntityTypeMapping(Enumerable.Empty<EntityType>(), new EntityType[] { (EntityType)structuralType }, Enumerable.Empty<FunctionImportEntityTypeMappingCondition>(), new Collection<FunctionImportReturnTypePropertyMapping>(), new LineInfo(lineInfo)));
				}
				else
				{
					typeMappings.Add(new FunctionImportComplexTypeMapping((ComplexType)structuralType, new Collection<FunctionImportReturnTypePropertyMapping>(), new LineInfo(lineInfo)));
				}
			}
			EdmItemCollection edmItemCollection = ((this._entityContainerMapping.StorageMappingItemCollection != null) ? this._entityContainerMapping.StorageMappingItemCollection.EdmItemCollection : new EdmItemCollection(new EdmModel(DataSpace.CSpace, 3.0)));
			FunctionImportStructuralTypeMappingKB functionImportStructuralTypeMappingKB = new FunctionImportStructuralTypeMappingKB(typeMappings, edmItemCollection);
			List<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>> list = new List<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>>();
			EdmProperty[] array = null;
			ComplexType complexType;
			if (functionImportStructuralTypeMappingKB.MappedEntityTypes.Count > 0)
			{
				if (!functionImportStructuralTypeMappingKB.ValidateTypeConditions(true, this.m_parsingErrors, this.m_sourceLocation))
				{
					return false;
				}
				for (int i = 0; i < functionImportStructuralTypeMappingKB.MappedEntityTypes.Count; i++)
				{
					List<ConditionPropertyMapping> list2;
					List<PropertyMapping> list3;
					if (this.TryConvertToEntityTypeConditionsAndPropertyMappings(functionImport, functionImportStructuralTypeMappingKB, i, cTypeTvfElementType, sTypeTvfElementType, lineInfo, out list2, out list3))
					{
						list.Add(Tuple.Create<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>(functionImportStructuralTypeMappingKB.MappedEntityTypes[i], list2, list3));
					}
				}
				if (list.Count < functionImportStructuralTypeMappingKB.MappedEntityTypes.Count)
				{
					return false;
				}
				if (!FunctionImportMappingComposableHelper.TryInferTVFKeys(list, out array))
				{
					FunctionImportMappingComposableHelper.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_FunctionImport_CannotInferTargetFunctionKeys), functionImport.Identity, MappingErrorCode.MappingFunctionImportCannotInferTargetFunctionKeys, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
					return false;
				}
			}
			else if (MetadataHelper.TryGetFunctionImportReturnType<ComplexType>(functionImport, 0, out complexType))
			{
				List<PropertyMapping> list4;
				if (!this.TryConvertToPropertyMappings(complexType, cTypeTvfElementType, sTypeTvfElementType, functionImport, functionImportStructuralTypeMappingKB, lineInfo, out list4))
				{
					return false;
				}
				list.Add(Tuple.Create<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>(complexType, new List<ConditionPropertyMapping>(), list4));
			}
			mapping = new FunctionImportMappingComposable(functionImport, cTypeTargetFunction, list, array, this._entityContainerMapping);
			return true;
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x000DE42C File Offset: 0x000DC62C
		internal bool TryCreateFunctionImportMappingComposableWithScalarResult(EdmFunction functionImport, EdmFunction cTypeTargetFunction, EdmFunction sTypeTargetFunction, EdmType scalarResultType, RowType cTypeTvfElementType, IXmlLineInfo lineInfo, out FunctionImportMappingComposable mapping)
		{
			mapping = null;
			if (cTypeTvfElementType.Properties.Count > 1)
			{
				FunctionImportMappingComposableHelper.AddToSchemaErrors(Strings.Mapping_FunctionImport_ScalarMappingToMulticolumnTVF(functionImport.Identity, sTypeTargetFunction.Identity), MappingErrorCode.MappingFunctionImportScalarMappingToMulticolumnTVF, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			if (!FunctionImportMappingComposableHelper.ValidateFunctionImportMappingResultTypeCompatibility(TypeUsage.Create(scalarResultType), cTypeTvfElementType.Properties[0].TypeUsage))
			{
				FunctionImportMappingComposableHelper.AddToSchemaErrors(Strings.Mapping_FunctionImport_ScalarMappingTypeMismatch(functionImport.ReturnParameter.TypeUsage.EdmType.FullName, functionImport.Identity, sTypeTargetFunction.ReturnParameter.TypeUsage.EdmType.FullName, sTypeTargetFunction.Identity), MappingErrorCode.MappingFunctionImportScalarMappingTypeMismatch, this.m_sourceLocation, lineInfo, this.m_parsingErrors);
				return false;
			}
			mapping = new FunctionImportMappingComposable(functionImport, cTypeTargetFunction, null, null, this._entityContainerMapping);
			return true;
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x000DE500 File Offset: 0x000DC700
		private bool TryConvertToEntityTypeConditionsAndPropertyMappings(EdmFunction functionImport, FunctionImportStructuralTypeMappingKB functionImportKB, int typeID, RowType cTypeTvfElementType, RowType sTypeTvfElementType, IXmlLineInfo navLineInfo, out List<ConditionPropertyMapping> typeConditions, out List<PropertyMapping> propertyMappings)
		{
			FunctionImportMappingComposableHelper.<>c__DisplayClass6_0 CS$<>8__locals1 = new FunctionImportMappingComposableHelper.<>c__DisplayClass6_0();
			CS$<>8__locals1.typeID = typeID;
			CS$<>8__locals1.<>4__this = this;
			EntityType entityType = functionImportKB.MappedEntityTypes[CS$<>8__locals1.typeID];
			typeConditions = new List<ConditionPropertyMapping>();
			bool flag = false;
			IEnumerable<FunctionImportNormalizedEntityTypeMapping> normalizedEntityTypeMappings = functionImportKB.NormalizedEntityTypeMappings;
			Func<FunctionImportNormalizedEntityTypeMapping, bool> func;
			if ((func = CS$<>8__locals1.<>9__0) == null)
			{
				func = (CS$<>8__locals1.<>9__0 = (FunctionImportNormalizedEntityTypeMapping f) => f.ImpliedEntityTypes[CS$<>8__locals1.typeID]);
			}
			foreach (FunctionImportNormalizedEntityTypeMapping functionImportNormalizedEntityTypeMapping in normalizedEntityTypeMappings.Where(func))
			{
				using (IEnumerator<FunctionImportEntityTypeMappingCondition> enumerator2 = functionImportNormalizedEntityTypeMapping.ColumnConditions.Where((FunctionImportEntityTypeMappingCondition c) => c != null).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						FunctionImportMappingComposableHelper.<>c__DisplayClass6_1 CS$<>8__locals2 = new FunctionImportMappingComposableHelper.<>c__DisplayClass6_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.condition = enumerator2.Current;
						EdmProperty column;
						if (sTypeTvfElementType.Properties.TryGetValue(CS$<>8__locals2.condition.ColumnName, false, out column))
						{
							object obj;
							bool? flag2;
							if (CS$<>8__locals2.condition.ConditionValue.IsSentinel)
							{
								obj = null;
								if (CS$<>8__locals2.condition.ConditionValue == ValueCondition.IsNull)
								{
									flag2 = new bool?(true);
								}
								else
								{
									flag2 = new bool?(false);
								}
							}
							else
							{
								PrimitiveType primitiveType = (PrimitiveType)cTypeTvfElementType.Properties[column.Name].TypeUsage.EdmType;
								obj = ((FunctionImportEntityTypeMappingConditionValue)CS$<>8__locals2.condition).GetConditionValue(primitiveType.ClrEquivalentType, delegate
								{
									FunctionImportMappingComposableHelper.AddToSchemaErrorWithMemberAndStructure(new Func<object, object, string>(Strings.Mapping_InvalidContent_ConditionMapping_InvalidPrimitiveTypeKind), column.Name, column.TypeUsage.EdmType.FullName, MappingErrorCode.ConditionError, CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_sourceLocation, CS$<>8__locals2.condition.LineInfo, CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_parsingErrors);
								}, delegate
								{
									FunctionImportMappingComposableHelper.AddToSchemaErrors(Strings.Mapping_ConditionValueTypeMismatch, MappingErrorCode.ConditionError, CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_sourceLocation, CS$<>8__locals2.condition.LineInfo, CS$<>8__locals2.CS$<>8__locals1.<>4__this.m_parsingErrors);
								});
								if (obj == null)
								{
									flag = true;
									continue;
								}
								flag2 = null;
							}
							typeConditions.Add((obj != null) ? new ValueConditionMapping(column, obj) : new IsNullConditionMapping(column, flag2.Value));
						}
						else
						{
							FunctionImportMappingComposableHelper.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Column), CS$<>8__locals2.condition.ColumnName, MappingErrorCode.InvalidStorageMember, this.m_sourceLocation, CS$<>8__locals2.condition.LineInfo, this.m_parsingErrors);
						}
					}
				}
			}
			flag |= !this.TryConvertToPropertyMappings(entityType, cTypeTvfElementType, sTypeTvfElementType, functionImport, functionImportKB, navLineInfo, out propertyMappings);
			return !flag;
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x000DE7BC File Offset: 0x000DC9BC
		private bool TryConvertToPropertyMappings(StructuralType structuralType, RowType cTypeTvfElementType, RowType sTypeTvfElementType, EdmFunction functionImport, FunctionImportStructuralTypeMappingKB functionImportKB, IXmlLineInfo navLineInfo, out List<PropertyMapping> propertyMappings)
		{
			propertyMappings = new List<PropertyMapping>();
			bool flag = false;
			foreach (object obj in TypeHelpers.GetAllStructuralMembers(structuralType))
			{
				EdmProperty edmProperty = (EdmProperty)obj;
				if (!Helper.IsScalarType(edmProperty.TypeUsage.EdmType))
				{
					EdmSchemaError edmSchemaError = new EdmSchemaError(Strings.Mapping_Invalid_CSide_ScalarProperty(edmProperty.Name), 2085, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, navLineInfo.LineNumber, navLineInfo.LinePosition);
					this.m_parsingErrors.Add(edmSchemaError);
					flag = true;
				}
				else
				{
					IXmlLineInfo xmlLineInfo = null;
					FunctionImportReturnTypeStructuralTypeColumnRenameMapping functionImportReturnTypeStructuralTypeColumnRenameMapping;
					bool flag2;
					string text;
					if (functionImportKB.ReturnTypeColumnsRenameMapping.TryGetValue(edmProperty.Name, out functionImportReturnTypeStructuralTypeColumnRenameMapping))
					{
						flag2 = true;
						text = functionImportReturnTypeStructuralTypeColumnRenameMapping.GetRename(structuralType, out xmlLineInfo);
					}
					else
					{
						flag2 = false;
						text = edmProperty.Name;
					}
					xmlLineInfo = ((xmlLineInfo != null && xmlLineInfo.HasLineInfo()) ? xmlLineInfo : navLineInfo);
					EdmProperty edmProperty2;
					if (sTypeTvfElementType.Properties.TryGetValue(text, false, out edmProperty2))
					{
						EdmProperty edmProperty3 = cTypeTvfElementType.Properties[text];
						if (FunctionImportMappingComposableHelper.ValidateFunctionImportMappingResultTypeCompatibility(edmProperty.TypeUsage, edmProperty3.TypeUsage))
						{
							propertyMappings.Add(new ScalarPropertyMapping(edmProperty, edmProperty2));
						}
						else
						{
							EdmSchemaError edmSchemaError2 = new EdmSchemaError(FunctionImportMappingComposableHelper.GetInvalidMemberMappingErrorMessage(edmProperty, edmProperty2), 2019, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
							this.m_parsingErrors.Add(edmSchemaError2);
							flag = true;
						}
					}
					else if (flag2)
					{
						FunctionImportMappingComposableHelper.AddToSchemaErrorsWithMemberInfo(new Func<object, string>(Strings.Mapping_InvalidContent_Column), text, MappingErrorCode.InvalidStorageMember, this.m_sourceLocation, xmlLineInfo, this.m_parsingErrors);
						flag = true;
					}
					else
					{
						EdmSchemaError edmSchemaError3 = new EdmSchemaError(Strings.Mapping_FunctionImport_PropertyNotMapped(edmProperty.Name, structuralType.FullName, functionImport.Identity), 2104, EdmSchemaErrorSeverity.Error, this.m_sourceLocation, xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
						this.m_parsingErrors.Add(edmSchemaError3);
						flag = true;
					}
				}
			}
			return !flag;
		}

		// Token: 0x060041C1 RID: 16833 RVA: 0x000DE9C4 File Offset: 0x000DCBC4
		private static bool TryInferTVFKeys(List<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>> structuralTypeMappings, out EdmProperty[] keys)
		{
			keys = null;
			foreach (Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>> tuple in structuralTypeMappings)
			{
				EdmProperty[] array;
				if (!FunctionImportMappingComposableHelper.TryInferTVFKeysForEntityType((EntityType)tuple.Item1, tuple.Item3, out array))
				{
					keys = null;
					return false;
				}
				if (keys == null)
				{
					keys = array;
				}
				else
				{
					for (int i = 0; i < keys.Length; i++)
					{
						if (!keys[i].EdmEquals(array[i]))
						{
							keys = null;
							return false;
						}
					}
				}
			}
			for (int j = 0; j < keys.Length; j++)
			{
				if (keys[j].Nullable)
				{
					keys = null;
					return false;
				}
			}
			return true;
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x000DEA8C File Offset: 0x000DCC8C
		private static bool TryInferTVFKeysForEntityType(EntityType entityType, List<PropertyMapping> propertyMappings, out EdmProperty[] keys)
		{
			keys = new EdmProperty[entityType.KeyMembers.Count];
			for (int i = 0; i < keys.Length; i++)
			{
				ScalarPropertyMapping scalarPropertyMapping = propertyMappings[entityType.Properties.IndexOf((EdmProperty)entityType.KeyMembers[i])] as ScalarPropertyMapping;
				if (scalarPropertyMapping == null)
				{
					keys = null;
					return false;
				}
				keys[i] = scalarPropertyMapping.Column;
			}
			return true;
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x000DEAF8 File Offset: 0x000DCCF8
		private static bool ValidateFunctionImportMappingResultTypeCompatibility(TypeUsage cSpaceMemberType, TypeUsage sSpaceMemberType)
		{
			TypeUsage typeUsage = FunctionImportMappingComposableHelper.ResolveTypeUsageForEnums(cSpaceMemberType);
			bool flag = TypeSemantics.IsStructurallyEqualOrPromotableTo(sSpaceMemberType, typeUsage);
			bool flag2 = TypeSemantics.IsStructurallyEqualOrPromotableTo(typeUsage, sSpaceMemberType);
			return flag || flag2;
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x000DEB1F File Offset: 0x000DCD1F
		private static TypeUsage ResolveTypeUsageForEnums(TypeUsage typeUsage)
		{
			return MappingItemLoader.ResolveTypeUsageForEnums(typeUsage);
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x000DEB27 File Offset: 0x000DCD27
		private static void AddToSchemaErrors(string message, MappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			MappingItemLoader.AddToSchemaErrors(message, errorCode, location, lineInfo, parsingErrors);
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x000DEB34 File Offset: 0x000DCD34
		private static void AddToSchemaErrorsWithMemberInfo(Func<object, string> messageFormat, string errorMember, MappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			MappingItemLoader.AddToSchemaErrorsWithMemberInfo(messageFormat, errorMember, errorCode, location, lineInfo, parsingErrors);
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x000DEB44 File Offset: 0x000DCD44
		private static void AddToSchemaErrorWithMemberAndStructure(Func<object, object, string> messageFormat, string errorMember, string errorStructure, MappingErrorCode errorCode, string location, IXmlLineInfo lineInfo, IList<EdmSchemaError> parsingErrors)
		{
			MappingItemLoader.AddToSchemaErrorWithMemberAndStructure(messageFormat, errorMember, errorStructure, errorCode, location, lineInfo, parsingErrors);
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x000DEB55 File Offset: 0x000DCD55
		private static string GetInvalidMemberMappingErrorMessage(EdmMember cSpaceMember, EdmMember sSpaceMember)
		{
			return MappingItemLoader.GetInvalidMemberMappingErrorMessage(cSpaceMember, sSpaceMember);
		}

		// Token: 0x040016CD RID: 5837
		private readonly EntityContainerMapping _entityContainerMapping;

		// Token: 0x040016CE RID: 5838
		private readonly string m_sourceLocation;

		// Token: 0x040016CF RID: 5839
		private readonly List<EdmSchemaError> m_parsingErrors;
	}
}
