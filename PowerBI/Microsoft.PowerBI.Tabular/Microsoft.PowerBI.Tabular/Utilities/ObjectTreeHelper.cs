using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Utilities
{
	// Token: 0x02000198 RID: 408
	internal static class ObjectTreeHelper
	{
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x000A528C File Offset: 0x000A348C
		private static List<ObjectType> AllObjectTypesInTopologicalOrder
		{
			get
			{
				if (ObjectTreeHelper.allTypesInTopologicalOrder == null)
				{
					ObjectTreeHelper.allTypesInTopologicalOrder = new List<ObjectType>(49)
					{
						ObjectType.Database,
						ObjectType.Model,
						ObjectType.Table,
						ObjectType.Relationship,
						ObjectType.DataSource,
						ObjectType.Perspective,
						ObjectType.Culture,
						ObjectType.Role,
						ObjectType.Expression,
						ObjectType.QueryGroup,
						ObjectType.AnalyticsAIMetadata,
						ObjectType.Function,
						ObjectType.BindingInfo,
						ObjectType.Column,
						ObjectType.Partition,
						ObjectType.Measure,
						ObjectType.Hierarchy,
						ObjectType.Set,
						ObjectType.RefreshPolicy,
						ObjectType.DetailRowsDefinition,
						ObjectType.ExcludedArtifact,
						ObjectType.CalculationGroup,
						ObjectType.Calendar,
						ObjectType.PerspectiveTable,
						ObjectType.ObjectTranslation,
						ObjectType.LinguisticMetadata,
						ObjectType.RoleMembership,
						ObjectType.TablePermission,
						ObjectType.AttributeHierarchy,
						ObjectType.Variation,
						ObjectType.RelatedColumnDetails,
						ObjectType.AlternateOf,
						ObjectType.DataCoverageDefinition,
						ObjectType.KPI,
						ObjectType.Level,
						ObjectType.ChangedProperty,
						ObjectType.CalculationItem,
						ObjectType.CalculationExpression,
						ObjectType.TimeUnitColumnAssociation,
						ObjectType.PerspectiveColumn,
						ObjectType.PerspectiveMeasure,
						ObjectType.PerspectiveHierarchy,
						ObjectType.PerspectiveSet,
						ObjectType.ColumnPermission,
						ObjectType.Annotation,
						ObjectType.ExtendedProperty,
						ObjectType.GroupByColumn,
						ObjectType.FormatStringDefinition,
						ObjectType.CalendarColumnReference
					};
				}
				return ObjectTreeHelper.allTypesInTopologicalOrder;
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x000A5437 File Offset: 0x000A3637
		public static int GetObjectTypeTopologicalOrder(ObjectType t)
		{
			return ObjectTreeHelper.AllObjectTypesInTopologicalOrder.IndexOf(t);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x000A5444 File Offset: 0x000A3644
		public static bool IsObjectComplientWithCompatibilityRestriction(ObjectType type, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			switch (type)
			{
			case ObjectType.Model:
			case ObjectType.DataSource:
			case ObjectType.Table:
			case ObjectType.Column:
			case ObjectType.AttributeHierarchy:
			case ObjectType.Partition:
			case ObjectType.Relationship:
			case ObjectType.Measure:
			case ObjectType.Hierarchy:
			case ObjectType.Level:
			case ObjectType.Annotation:
			case ObjectType.KPI:
			case ObjectType.Culture:
			case ObjectType.ObjectTranslation:
			case ObjectType.LinguisticMetadata:
			case ObjectType.Perspective:
			case ObjectType.PerspectiveTable:
			case ObjectType.PerspectiveColumn:
			case ObjectType.PerspectiveHierarchy:
			case ObjectType.PerspectiveMeasure:
			case ObjectType.Role:
			case ObjectType.RoleMembership:
			case ObjectType.TablePermission:
				return true;
			case (ObjectType)16:
			case (ObjectType)17:
			case (ObjectType)18:
			case (ObjectType)19:
			case (ObjectType)20:
			case (ObjectType)21:
			case (ObjectType)22:
			case (ObjectType)23:
			case (ObjectType)24:
			case (ObjectType)25:
			case (ObjectType)26:
			case (ObjectType)27:
			case (ObjectType)28:
			case (ObjectType)55:
			case (ObjectType)56:
			case (ObjectType)57:
				break;
			case ObjectType.Variation:
				return CompatibilityRestrictions.Variation.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.Set:
				return CompatibilityRestrictions.Set.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.PerspectiveSet:
				return CompatibilityRestrictions.PerspectiveSet.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.ExtendedProperty:
				return CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.Expression:
				return CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.ColumnPermission:
				return CompatibilityRestrictions.ColumnPermission.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.DetailRowsDefinition:
				return CompatibilityRestrictions.DetailRowsDefinition.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.RelatedColumnDetails:
				return CompatibilityRestrictions.RelatedColumnDetails.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.GroupByColumn:
				return CompatibilityRestrictions.GroupByColumn.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.CalculationGroup:
				return CompatibilityRestrictions.CalculationGroup.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.CalculationItem:
				return CompatibilityRestrictions.CalculationItem.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.AlternateOf:
				return CompatibilityRestrictions.AlternateOf.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.RefreshPolicy:
				return CompatibilityRestrictions.RefreshPolicy.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.FormatStringDefinition:
				return CompatibilityRestrictions.FormatStringDefinition.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.QueryGroup:
				return CompatibilityRestrictions.QueryGroup.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.AnalyticsAIMetadata:
				return CompatibilityRestrictions.AnalyticsAIMetadata.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.ChangedProperty:
				return CompatibilityRestrictions.ChangedProperty.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.ExcludedArtifact:
				return CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.DataCoverageDefinition:
				return CompatibilityRestrictions.DataCoverageDefinition.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.CalculationExpression:
				return CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.Calendar:
				return CompatibilityRestrictions.Calendar.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.TimeUnitColumnAssociation:
				return CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.CalendarColumnReference:
				return CompatibilityRestrictions.CalendarColumnReference.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.Function:
				return CompatibilityRestrictions.Function.IsCompatible(mode, dbCompatibilityLevel);
			case ObjectType.BindingInfo:
				return CompatibilityRestrictions.BindingInfo.IsCompatible(mode, dbCompatibilityLevel);
			default:
				if (type == ObjectType.Database)
				{
					return true;
				}
				break;
			}
			throw new TomInternalException("Unknown object type");
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000A56B7 File Offset: 0x000A38B7
		public static bool IsInferredObject(ObjectType type)
		{
			return type == ObjectType.AttributeHierarchy;
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000A56C0 File Offset: 0x000A38C0
		public static bool IsInferredObject(MetadataObject obj)
		{
			ObjectType objectType = obj.ObjectType;
			if (objectType != ObjectType.Column)
			{
				if (objectType == ObjectType.AttributeHierarchy)
				{
					return true;
				}
			}
			else if (((Column)obj).Type == ColumnType.RowNumber)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x000A56F0 File Offset: 0x000A38F0
		public static bool HasTranslatableDescendants(ObjectType type)
		{
			if (type <= ObjectType.Set)
			{
				if (type <= ObjectType.Perspective)
				{
					switch (type)
					{
					case ObjectType.Model:
					case ObjectType.Table:
					case ObjectType.Column:
					case ObjectType.Measure:
					case ObjectType.Hierarchy:
					case ObjectType.Level:
					case ObjectType.KPI:
						break;
					case ObjectType.DataSource:
					case ObjectType.AttributeHierarchy:
					case ObjectType.Partition:
					case ObjectType.Relationship:
					case ObjectType.Annotation:
						return false;
					default:
						if (type != ObjectType.Perspective)
						{
							return false;
						}
						break;
					}
				}
				else if (type != ObjectType.Role && type - ObjectType.Variation > 1)
				{
					return false;
				}
			}
			else if (type <= ObjectType.QueryGroup)
			{
				if (type != ObjectType.Expression && type != ObjectType.QueryGroup)
				{
					return false;
				}
			}
			else if (type != ObjectType.Calendar && type - ObjectType.Function > 1)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x000A5774 File Offset: 0x000A3974
		public static IEnumerable<TranslatablePropertyInfo> GetTranslatedProperties(ObjectType type, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (type <= ObjectType.Set)
			{
				switch (type)
				{
				case ObjectType.Model:
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Caption,
						IsMultiline = false
					};
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Description,
						IsMultiline = true
					};
					break;
				case ObjectType.DataSource:
				case ObjectType.AttributeHierarchy:
				case ObjectType.Partition:
				case ObjectType.Relationship:
				case ObjectType.Annotation:
					break;
				case ObjectType.Table:
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Caption,
						IsMultiline = false
					};
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Description,
						IsMultiline = true
					};
					break;
				case ObjectType.Column:
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Caption,
						IsMultiline = false
					};
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Description,
						IsMultiline = true
					};
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.DisplayFolder,
						IsMultiline = false
					};
					break;
				case ObjectType.Measure:
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Caption,
						IsMultiline = false
					};
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Description,
						IsMultiline = true
					};
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.DisplayFolder,
						IsMultiline = false
					};
					break;
				case ObjectType.Hierarchy:
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Caption,
						IsMultiline = false
					};
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Description,
						IsMultiline = true
					};
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.DisplayFolder,
						IsMultiline = false
					};
					break;
				case ObjectType.Level:
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Caption,
						IsMultiline = false
					};
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Description,
						IsMultiline = true
					};
					break;
				case ObjectType.KPI:
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Description,
						IsMultiline = true
					};
					break;
				default:
					if (type != ObjectType.Perspective)
					{
						switch (type)
						{
						case ObjectType.Role:
							yield return new TranslatablePropertyInfo
							{
								Property = TranslatedProperty.Description,
								IsMultiline = true
							};
							break;
						case ObjectType.Variation:
							yield return new TranslatablePropertyInfo
							{
								Property = TranslatedProperty.Caption,
								IsMultiline = false
							};
							yield return new TranslatablePropertyInfo
							{
								Property = TranslatedProperty.Description,
								IsMultiline = true
							};
							break;
						case ObjectType.Set:
							yield return new TranslatablePropertyInfo
							{
								Property = TranslatedProperty.Caption,
								IsMultiline = false
							};
							yield return new TranslatablePropertyInfo
							{
								Property = TranslatedProperty.Description,
								IsMultiline = true
							};
							yield return new TranslatablePropertyInfo
							{
								Property = TranslatedProperty.DisplayFolder,
								IsMultiline = false
							};
							break;
						}
					}
					else
					{
						yield return new TranslatablePropertyInfo
						{
							Property = TranslatedProperty.Caption,
							IsMultiline = false
						};
						yield return new TranslatablePropertyInfo
						{
							Property = TranslatedProperty.Description,
							IsMultiline = true
						};
					}
					break;
				}
			}
			else if (type != ObjectType.Expression)
			{
				if (type != ObjectType.QueryGroup)
				{
					switch (type)
					{
					case ObjectType.Calendar:
						yield return new TranslatablePropertyInfo
						{
							Property = TranslatedProperty.Caption,
							IsMultiline = false
						};
						yield return new TranslatablePropertyInfo
						{
							Property = TranslatedProperty.Description,
							IsMultiline = true
						};
						break;
					case ObjectType.Function:
						yield return new TranslatablePropertyInfo
						{
							Property = TranslatedProperty.Caption,
							IsMultiline = false
						};
						yield return new TranslatablePropertyInfo
						{
							Property = TranslatedProperty.Description,
							IsMultiline = true
						};
						break;
					case ObjectType.BindingInfo:
						yield return new TranslatablePropertyInfo
						{
							Property = TranslatedProperty.Description,
							IsMultiline = true
						};
						break;
					}
				}
				else
				{
					yield return new TranslatablePropertyInfo
					{
						Property = TranslatedProperty.Description,
						IsMultiline = true
					};
				}
			}
			else
			{
				yield return new TranslatablePropertyInfo
				{
					Property = TranslatedProperty.Caption,
					IsMultiline = false
				};
				yield return new TranslatablePropertyInfo
				{
					Property = TranslatedProperty.Description,
					IsMultiline = true
				};
			}
			yield break;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x000A5784 File Offset: 0x000A3984
		public static bool IsNamedObject(ObjectType type)
		{
			switch (type)
			{
			case ObjectType.Model:
			case ObjectType.DataSource:
			case ObjectType.Table:
			case ObjectType.Column:
			case ObjectType.Partition:
			case ObjectType.Relationship:
			case ObjectType.Measure:
			case ObjectType.Hierarchy:
			case ObjectType.Level:
			case ObjectType.Annotation:
			case ObjectType.Culture:
			case ObjectType.Perspective:
			case ObjectType.PerspectiveTable:
			case ObjectType.PerspectiveColumn:
			case ObjectType.PerspectiveHierarchy:
			case ObjectType.PerspectiveMeasure:
			case ObjectType.Role:
			case ObjectType.RoleMembership:
			case ObjectType.TablePermission:
			case ObjectType.Variation:
			case ObjectType.Set:
			case ObjectType.PerspectiveSet:
			case ObjectType.ExtendedProperty:
			case ObjectType.Expression:
			case ObjectType.ColumnPermission:
			case ObjectType.CalculationItem:
			case ObjectType.QueryGroup:
			case ObjectType.AnalyticsAIMetadata:
				break;
			case ObjectType.AttributeHierarchy:
			case ObjectType.KPI:
			case ObjectType.ObjectTranslation:
			case ObjectType.LinguisticMetadata:
			case (ObjectType)16:
			case (ObjectType)17:
			case (ObjectType)18:
			case (ObjectType)19:
			case (ObjectType)20:
			case (ObjectType)21:
			case (ObjectType)22:
			case (ObjectType)23:
			case (ObjectType)24:
			case (ObjectType)25:
			case (ObjectType)26:
			case (ObjectType)27:
			case (ObjectType)28:
			case ObjectType.DetailRowsDefinition:
			case ObjectType.RelatedColumnDetails:
			case ObjectType.GroupByColumn:
			case ObjectType.CalculationGroup:
			case ObjectType.AlternateOf:
			case ObjectType.RefreshPolicy:
			case ObjectType.FormatStringDefinition:
				return false;
			default:
				if (type != ObjectType.Calendar && type - ObjectType.CalendarColumnReference > 2)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x000A5878 File Offset: 0x000A3A78
		public static bool IsKeyedObject(ObjectType type)
		{
			return type == ObjectType.CalculationExpression || type == ObjectType.TimeUnitColumnAssociation;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x000A5887 File Offset: 0x000A3A87
		public static string GetKeyedObjectKeyPropertyName(ObjectType type)
		{
			if (type == ObjectType.CalculationExpression)
			{
				return "SelectionMode";
			}
			if (type != ObjectType.TimeUnitColumnAssociation)
			{
				throw TomInternalException.Create("ObjectType.{0} is not a type of a keyed-object!", new object[] { type.ToString() });
			}
			return "TimeUnit";
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x000A58C4 File Offset: 0x000A3AC4
		public static bool IsChildObject(ObjectType parent, ObjectType child, bool isLogicalStructure, out bool isSingleChild)
		{
			switch (parent)
			{
			case ObjectType.Null:
				if (child == ObjectType.Model)
				{
					isSingleChild = true;
					return true;
				}
				break;
			case ObjectType.Model:
				if (child <= ObjectType.Perspective)
				{
					if (child <= ObjectType.Relationship)
					{
						if (child - ObjectType.DataSource > 1 && child != ObjectType.Relationship)
						{
							break;
						}
					}
					else if (child != ObjectType.Annotation && child != ObjectType.Culture && child != ObjectType.Perspective)
					{
						break;
					}
				}
				else if (child <= ObjectType.Expression)
				{
					if (child != ObjectType.Role && child - ObjectType.ExtendedProperty > 1)
					{
						break;
					}
				}
				else if (child - ObjectType.QueryGroup > 1 && child != ObjectType.ExcludedArtifact && child - ObjectType.Function > 1)
				{
					break;
				}
				isSingleChild = false;
				return true;
			case ObjectType.DataSource:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Table:
				if (child <= ObjectType.DetailRowsDefinition)
				{
					if (child <= ObjectType.Set)
					{
						switch (child)
						{
						case ObjectType.Column:
						case ObjectType.Partition:
						case ObjectType.Measure:
						case ObjectType.Hierarchy:
						case ObjectType.Annotation:
							goto IL_01FF;
						case ObjectType.AttributeHierarchy:
						case ObjectType.Relationship:
						case ObjectType.Level:
							goto IL_04E8;
						default:
							if (child != ObjectType.Set)
							{
								goto IL_04E8;
							}
							goto IL_01FF;
						}
					}
					else
					{
						if (child == ObjectType.ExtendedProperty)
						{
							goto IL_01FF;
						}
						if (child != ObjectType.DetailRowsDefinition)
						{
							break;
						}
					}
				}
				else if (child <= ObjectType.RefreshPolicy)
				{
					if (child != ObjectType.CalculationGroup && child != ObjectType.RefreshPolicy)
					{
						break;
					}
				}
				else
				{
					if (child - ObjectType.ChangedProperty > 1 && child != ObjectType.Calendar)
					{
						break;
					}
					goto IL_01FF;
				}
				isSingleChild = true;
				return true;
				IL_01FF:
				isSingleChild = false;
				return true;
			case ObjectType.Column:
				if (child <= ObjectType.Variation)
				{
					if (child != ObjectType.AttributeHierarchy)
					{
						if (child != ObjectType.Annotation && child != ObjectType.Variation)
						{
							break;
						}
						goto IL_0244;
					}
				}
				else if (child <= ObjectType.RelatedColumnDetails)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						goto IL_0244;
					}
					if (child != ObjectType.RelatedColumnDetails)
					{
						break;
					}
				}
				else if (child != ObjectType.AlternateOf)
				{
					if (child != ObjectType.ChangedProperty)
					{
						break;
					}
					goto IL_0244;
				}
				isSingleChild = true;
				return true;
				IL_0244:
				isSingleChild = false;
				return true;
			case ObjectType.AttributeHierarchy:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Partition:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				if (child == ObjectType.DataCoverageDefinition)
				{
					isSingleChild = true;
					return true;
				}
				break;
			case ObjectType.Relationship:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty || child == ObjectType.ChangedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Measure:
				if (child <= ObjectType.ExtendedProperty)
				{
					if (child == ObjectType.Annotation)
					{
						goto IL_02C0;
					}
					if (child != ObjectType.KPI)
					{
						if (child != ObjectType.ExtendedProperty)
						{
							break;
						}
						goto IL_02C0;
					}
				}
				else if (child != ObjectType.DetailRowsDefinition && child != ObjectType.FormatStringDefinition)
				{
					if (child != ObjectType.ChangedProperty)
					{
						break;
					}
					goto IL_02C0;
				}
				isSingleChild = true;
				return true;
				IL_02C0:
				isSingleChild = false;
				return true;
			case ObjectType.Hierarchy:
				if (child - ObjectType.Level <= 1 || child == ObjectType.ExtendedProperty || child - ObjectType.ChangedProperty <= 1)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Level:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty || child == ObjectType.ChangedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.KPI:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Culture:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.LinguisticMetadata)
					{
						isSingleChild = true;
						return true;
					}
					if (child != ObjectType.ExtendedProperty)
					{
						break;
					}
				}
				isSingleChild = false;
				return true;
			case ObjectType.LinguisticMetadata:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Perspective:
				if (child == ObjectType.Annotation || child == ObjectType.PerspectiveTable || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.PerspectiveTable:
				if (child == ObjectType.Annotation || child - ObjectType.PerspectiveColumn <= 2 || child - ObjectType.PerspectiveSet <= 1)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.PerspectiveColumn:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.PerspectiveHierarchy:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.PerspectiveMeasure:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Role:
				if (child == ObjectType.Annotation || child - ObjectType.RoleMembership <= 1 || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.RoleMembership:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.TablePermission:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty || child == ObjectType.ColumnPermission)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Variation:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Set:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.PerspectiveSet:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Expression:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty || child == ObjectType.ExcludedArtifact)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.ColumnPermission:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.RelatedColumnDetails:
				if (child == ObjectType.GroupByColumn)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.CalculationGroup:
				if (child == ObjectType.Annotation || child == ObjectType.CalculationItem)
				{
					isSingleChild = false;
					return true;
				}
				if (child == ObjectType.CalculationExpression)
				{
					isSingleChild = isLogicalStructure;
					return true;
				}
				break;
			case ObjectType.CalculationItem:
				if (child == ObjectType.FormatStringDefinition)
				{
					isSingleChild = true;
					return true;
				}
				break;
			case ObjectType.AlternateOf:
				if (child == ObjectType.Annotation)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.RefreshPolicy:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.QueryGroup:
				if (child == ObjectType.Annotation)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.DataCoverageDefinition:
				if (child == ObjectType.Annotation)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.CalculationExpression:
				if (child == ObjectType.FormatStringDefinition)
				{
					isSingleChild = true;
					return true;
				}
				break;
			case ObjectType.Calendar:
				if (child == ObjectType.TimeUnitColumnAssociation)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.TimeUnitColumnAssociation:
				if (child == ObjectType.CalendarColumnReference)
				{
					isSingleChild = false;
					return !isLogicalStructure;
				}
				break;
			case ObjectType.Function:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty || child == ObjectType.ChangedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.BindingInfo:
				if (child == ObjectType.Annotation || child == ObjectType.ExtendedProperty)
				{
					isSingleChild = false;
					return true;
				}
				break;
			}
			IL_04E8:
			isSingleChild = false;
			return false;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x000A5DC0 File Offset: 0x000A3FC0
		public static string GetChildJsonPropertyName(ObjectType parent, ObjectType child)
		{
			string text;
			if (!ObjectTreeHelper.TryGetChildJsonPropertyName(parent, child, out text))
			{
				throw TomInternalException.Create("Invalid request for a child property name - ObjectType.{0} is not a valid type of a child of ObjectType.{1}", new object[]
				{
					child.ToString(),
					parent.ToString()
				});
			}
			return text;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000A5E0C File Offset: 0x000A400C
		public static bool TryGetChildJsonPropertyName(ObjectType parent, ObjectType child, out string jsonPropertyName)
		{
			jsonPropertyName = null;
			if (parent <= ObjectType.Culture)
			{
				switch (parent)
				{
				case ObjectType.Null:
					break;
				case ObjectType.Model:
				case ObjectType.DataSource:
				case ObjectType.AttributeHierarchy:
				case ObjectType.Relationship:
					goto IL_0132;
				case ObjectType.Table:
					if (child == ObjectType.DetailRowsDefinition)
					{
						jsonPropertyName = "defaultDetailRowsDefinition";
						goto IL_0132;
					}
					if (child == ObjectType.CalculationGroup)
					{
						jsonPropertyName = "calculationGroup";
						goto IL_0132;
					}
					if (child != ObjectType.RefreshPolicy)
					{
						goto IL_0132;
					}
					jsonPropertyName = "refreshPolicy";
					goto IL_0132;
				case ObjectType.Column:
					if (child == ObjectType.AttributeHierarchy)
					{
						jsonPropertyName = "attributeHierarchy";
						goto IL_0132;
					}
					if (child == ObjectType.RelatedColumnDetails)
					{
						jsonPropertyName = "relatedColumnDetails";
						goto IL_0132;
					}
					if (child != ObjectType.AlternateOf)
					{
						goto IL_0132;
					}
					jsonPropertyName = "alternateOf";
					goto IL_0132;
				case ObjectType.Partition:
					if (child == ObjectType.DataCoverageDefinition)
					{
						jsonPropertyName = "dataCoverageDefinition";
						goto IL_0132;
					}
					goto IL_0132;
				case ObjectType.Measure:
					if (child == ObjectType.KPI)
					{
						jsonPropertyName = "kpi";
						goto IL_0132;
					}
					if (child == ObjectType.DetailRowsDefinition)
					{
						jsonPropertyName = "detailRowsDefinition";
						goto IL_0132;
					}
					if (child != ObjectType.FormatStringDefinition)
					{
						goto IL_0132;
					}
					jsonPropertyName = "formatStringDefinition";
					goto IL_0132;
				default:
					if (parent != ObjectType.Culture)
					{
						goto IL_0132;
					}
					if (child == ObjectType.LinguisticMetadata)
					{
						jsonPropertyName = "linguisticMetadata";
						goto IL_0132;
					}
					goto IL_0132;
				}
			}
			else if (parent != ObjectType.CalculationItem)
			{
				if (parent != ObjectType.CalculationExpression)
				{
					if (parent != ObjectType.Database)
					{
						goto IL_0132;
					}
				}
				else
				{
					if (child == ObjectType.FormatStringDefinition)
					{
						jsonPropertyName = "formatStringDefinition";
						goto IL_0132;
					}
					goto IL_0132;
				}
			}
			else
			{
				if (child == ObjectType.FormatStringDefinition)
				{
					jsonPropertyName = "formatStringDefinition";
					goto IL_0132;
				}
				goto IL_0132;
			}
			if (child == ObjectType.Model)
			{
				jsonPropertyName = "model";
			}
			IL_0132:
			return !string.IsNullOrEmpty(jsonPropertyName);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000A5F58 File Offset: 0x000A4158
		public static string GetChildCollectionJsonPropertyName(ObjectType parent, ObjectType child)
		{
			string text;
			if (!ObjectTreeHelper.TryGetChildCollectionJsonPropertyName(parent, child, out text))
			{
				throw TomInternalException.Create("Invalid request for a child collection name - ObjectType.{0} is not a valid type of a child of ObjectType.{1}", new object[]
				{
					child.ToString(),
					parent.ToString()
				});
			}
			return text;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x000A5FA4 File Offset: 0x000A41A4
		public static bool TryGetChildCollectionJsonPropertyName(ObjectType parent, ObjectType child, out string jsonPropertyName)
		{
			jsonPropertyName = null;
			switch (parent)
			{
			case ObjectType.Model:
				if (child <= ObjectType.Perspective)
				{
					if (child <= ObjectType.Relationship)
					{
						if (child != ObjectType.DataSource)
						{
							if (child != ObjectType.Table)
							{
								if (child == ObjectType.Relationship)
								{
									jsonPropertyName = "relationships";
								}
							}
							else
							{
								jsonPropertyName = "tables";
							}
						}
						else
						{
							jsonPropertyName = "dataSources";
						}
					}
					else if (child != ObjectType.Annotation)
					{
						if (child != ObjectType.Culture)
						{
							if (child == ObjectType.Perspective)
							{
								jsonPropertyName = "perspectives";
							}
						}
						else
						{
							jsonPropertyName = "cultures";
						}
					}
					else
					{
						jsonPropertyName = "annotations";
					}
				}
				else if (child <= ObjectType.Expression)
				{
					if (child != ObjectType.Role)
					{
						if (child != ObjectType.ExtendedProperty)
						{
							if (child == ObjectType.Expression)
							{
								jsonPropertyName = "expressions";
							}
						}
						else
						{
							jsonPropertyName = "extendedProperties";
						}
					}
					else
					{
						jsonPropertyName = "roles";
					}
				}
				else
				{
					switch (child)
					{
					case ObjectType.QueryGroup:
						jsonPropertyName = "queryGroups";
						break;
					case ObjectType.AnalyticsAIMetadata:
						jsonPropertyName = "analyticsAIMetadata";
						break;
					case ObjectType.ChangedProperty:
						break;
					case ObjectType.ExcludedArtifact:
						jsonPropertyName = "excludedArtifacts";
						break;
					default:
						if (child != ObjectType.Function)
						{
							if (child == ObjectType.BindingInfo)
							{
								jsonPropertyName = "bindingInfoCollection";
							}
						}
						else
						{
							jsonPropertyName = "functions";
						}
						break;
					}
				}
				break;
			case ObjectType.DataSource:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Table:
				if (child <= ObjectType.ExtendedProperty)
				{
					switch (child)
					{
					case ObjectType.Column:
						jsonPropertyName = "columns";
						break;
					case ObjectType.AttributeHierarchy:
					case ObjectType.Relationship:
					case ObjectType.Level:
						break;
					case ObjectType.Partition:
						jsonPropertyName = "partitions";
						break;
					case ObjectType.Measure:
						jsonPropertyName = "measures";
						break;
					case ObjectType.Hierarchy:
						jsonPropertyName = "hierarchies";
						break;
					case ObjectType.Annotation:
						jsonPropertyName = "annotations";
						break;
					default:
						if (child != ObjectType.Set)
						{
							if (child == ObjectType.ExtendedProperty)
							{
								jsonPropertyName = "extendedProperties";
							}
						}
						else
						{
							jsonPropertyName = "sets";
						}
						break;
					}
				}
				else if (child != ObjectType.ChangedProperty)
				{
					if (child != ObjectType.ExcludedArtifact)
					{
						if (child == ObjectType.Calendar)
						{
							jsonPropertyName = "calendars";
						}
					}
					else
					{
						jsonPropertyName = "excludedArtifacts";
					}
				}
				else
				{
					jsonPropertyName = "changedProperties";
				}
				break;
			case ObjectType.Column:
				if (child <= ObjectType.Variation)
				{
					if (child != ObjectType.Annotation)
					{
						if (child == ObjectType.Variation)
						{
							jsonPropertyName = "variations";
						}
					}
					else
					{
						jsonPropertyName = "annotations";
					}
				}
				else if (child != ObjectType.ExtendedProperty)
				{
					if (child == ObjectType.ChangedProperty)
					{
						jsonPropertyName = "changedProperties";
					}
				}
				else
				{
					jsonPropertyName = "extendedProperties";
				}
				break;
			case ObjectType.AttributeHierarchy:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Partition:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Relationship:
				if (child != ObjectType.Annotation)
				{
					if (child != ObjectType.ExtendedProperty)
					{
						if (child == ObjectType.ChangedProperty)
						{
							jsonPropertyName = "changedProperties";
						}
					}
					else
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Measure:
				if (child != ObjectType.Annotation)
				{
					if (child != ObjectType.ExtendedProperty)
					{
						if (child == ObjectType.ChangedProperty)
						{
							jsonPropertyName = "changedProperties";
						}
					}
					else
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Hierarchy:
				if (child <= ObjectType.Annotation)
				{
					if (child != ObjectType.Level)
					{
						if (child == ObjectType.Annotation)
						{
							jsonPropertyName = "annotations";
						}
					}
					else
					{
						jsonPropertyName = "levels";
					}
				}
				else if (child != ObjectType.ExtendedProperty)
				{
					if (child != ObjectType.ChangedProperty)
					{
						if (child == ObjectType.ExcludedArtifact)
						{
							jsonPropertyName = "excludedArtifacts";
						}
					}
					else
					{
						jsonPropertyName = "changedProperties";
					}
				}
				else
				{
					jsonPropertyName = "extendedProperties";
				}
				break;
			case ObjectType.Level:
				if (child != ObjectType.Annotation)
				{
					if (child != ObjectType.ExtendedProperty)
					{
						if (child == ObjectType.ChangedProperty)
						{
							jsonPropertyName = "changedProperties";
						}
					}
					else
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.KPI:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Culture:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.LinguisticMetadata:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Perspective:
				if (child != ObjectType.Annotation)
				{
					if (child != ObjectType.PerspectiveTable)
					{
						if (child == ObjectType.ExtendedProperty)
						{
							jsonPropertyName = "extendedProperties";
						}
					}
					else
					{
						jsonPropertyName = "tables";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.PerspectiveTable:
				if (child <= ObjectType.PerspectiveMeasure)
				{
					if (child != ObjectType.Annotation)
					{
						switch (child)
						{
						case ObjectType.PerspectiveColumn:
							jsonPropertyName = "columns";
							break;
						case ObjectType.PerspectiveHierarchy:
							jsonPropertyName = "hierarchies";
							break;
						case ObjectType.PerspectiveMeasure:
							jsonPropertyName = "measures";
							break;
						}
					}
					else
					{
						jsonPropertyName = "annotations";
					}
				}
				else if (child != ObjectType.PerspectiveSet)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "sets";
				}
				break;
			case ObjectType.PerspectiveColumn:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.PerspectiveHierarchy:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.PerspectiveMeasure:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Role:
				if (child <= ObjectType.RoleMembership)
				{
					if (child != ObjectType.Annotation)
					{
						if (child == ObjectType.RoleMembership)
						{
							jsonPropertyName = "members";
						}
					}
					else
					{
						jsonPropertyName = "annotations";
					}
				}
				else if (child != ObjectType.TablePermission)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "tablePermissions";
				}
				break;
			case ObjectType.RoleMembership:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.TablePermission:
				if (child != ObjectType.Annotation)
				{
					if (child != ObjectType.ExtendedProperty)
					{
						if (child == ObjectType.ColumnPermission)
						{
							jsonPropertyName = "columnPermissions";
						}
					}
					else
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Variation:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Set:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.PerspectiveSet:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Expression:
				if (child != ObjectType.Annotation)
				{
					if (child != ObjectType.ExtendedProperty)
					{
						if (child == ObjectType.ExcludedArtifact)
						{
							jsonPropertyName = "excludedArtifacts";
						}
					}
					else
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.ColumnPermission:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.RelatedColumnDetails:
				if (child == ObjectType.GroupByColumn)
				{
					jsonPropertyName = "groupByColumns";
				}
				break;
			case ObjectType.CalculationGroup:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.CalculationItem)
					{
						jsonPropertyName = "calculationItems";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.AlternateOf:
				if (child == ObjectType.Annotation)
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.RefreshPolicy:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.QueryGroup:
				if (child == ObjectType.Annotation)
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.DataCoverageDefinition:
				if (child == ObjectType.Annotation)
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.Calendar:
				if (child == ObjectType.TimeUnitColumnAssociation)
				{
					jsonPropertyName = "timeUnitColumnAssociations";
				}
				break;
			case ObjectType.Function:
				if (child != ObjectType.Annotation)
				{
					if (child != ObjectType.ExtendedProperty)
					{
						if (child == ObjectType.ChangedProperty)
						{
							jsonPropertyName = "changedProperties";
						}
					}
					else
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			case ObjectType.BindingInfo:
				if (child != ObjectType.Annotation)
				{
					if (child == ObjectType.ExtendedProperty)
					{
						jsonPropertyName = "extendedProperties";
					}
				}
				else
				{
					jsonPropertyName = "annotations";
				}
				break;
			}
			return !string.IsNullOrEmpty(jsonPropertyName);
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x000A688C File Offset: 0x000A4A8C
		public static bool IsChildJsonPropertyName(ObjectType parent, string propertyName, out ObjectType child, out bool isSingleChild)
		{
			switch (parent)
			{
			case ObjectType.Null:
				if (propertyName == "model")
				{
					child = ObjectType.Model;
					isSingleChild = true;
					return true;
				}
				break;
			case ObjectType.Model:
				if (propertyName != null)
				{
					switch (propertyName.Length)
					{
					case 5:
						if (propertyName == "roles")
						{
							child = ObjectType.Role;
							isSingleChild = false;
							return true;
						}
						break;
					case 6:
						if (propertyName == "tables")
						{
							child = ObjectType.Table;
							isSingleChild = false;
							return true;
						}
						break;
					case 8:
						if (propertyName == "cultures")
						{
							child = ObjectType.Culture;
							isSingleChild = false;
							return true;
						}
						break;
					case 9:
						if (propertyName == "functions")
						{
							child = ObjectType.Function;
							isSingleChild = false;
							return true;
						}
						break;
					case 11:
					{
						char c = propertyName[0];
						switch (c)
						{
						case 'a':
							if (propertyName == "annotations")
							{
								child = ObjectType.Annotation;
								isSingleChild = false;
								return true;
							}
							break;
						case 'b':
						case 'c':
							break;
						case 'd':
							if (propertyName == "dataSources")
							{
								child = ObjectType.DataSource;
								isSingleChild = false;
								return true;
							}
							break;
						case 'e':
							if (propertyName == "expressions")
							{
								child = ObjectType.Expression;
								isSingleChild = false;
								return true;
							}
							break;
						default:
							if (c == 'q')
							{
								if (propertyName == "queryGroups")
								{
									child = ObjectType.QueryGroup;
									isSingleChild = false;
									return true;
								}
							}
							break;
						}
						break;
					}
					case 12:
						if (propertyName == "perspectives")
						{
							child = ObjectType.Perspective;
							isSingleChild = false;
							return true;
						}
						break;
					case 13:
						if (propertyName == "relationships")
						{
							child = ObjectType.Relationship;
							isSingleChild = false;
							return true;
						}
						break;
					case 17:
						if (propertyName == "excludedArtifacts")
						{
							child = ObjectType.ExcludedArtifact;
							isSingleChild = false;
							return true;
						}
						break;
					case 18:
						if (propertyName == "extendedProperties")
						{
							child = ObjectType.ExtendedProperty;
							isSingleChild = false;
							return true;
						}
						break;
					case 19:
						if (propertyName == "analyticsAIMetadata")
						{
							child = ObjectType.AnalyticsAIMetadata;
							isSingleChild = false;
							return true;
						}
						break;
					case 21:
						if (propertyName == "bindingInfoCollection")
						{
							child = ObjectType.BindingInfo;
							isSingleChild = false;
							return true;
						}
						break;
					}
				}
				break;
			case ObjectType.DataSource:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Table:
				if (propertyName != null)
				{
					int num = propertyName.Length;
					switch (num)
					{
					case 4:
						if (propertyName == "sets")
						{
							child = ObjectType.Set;
							isSingleChild = false;
							return true;
						}
						break;
					case 5:
					case 6:
					case 12:
					case 14:
					case 15:
						break;
					case 7:
						if (propertyName == "columns")
						{
							child = ObjectType.Column;
							isSingleChild = false;
							return true;
						}
						break;
					case 8:
						if (propertyName == "measures")
						{
							child = ObjectType.Measure;
							isSingleChild = false;
							return true;
						}
						break;
					case 9:
						if (propertyName == "calendars")
						{
							child = ObjectType.Calendar;
							isSingleChild = false;
							return true;
						}
						break;
					case 10:
						if (propertyName == "partitions")
						{
							child = ObjectType.Partition;
							isSingleChild = false;
							return true;
						}
						break;
					case 11:
					{
						char c = propertyName[0];
						if (c != 'a')
						{
							if (c == 'h')
							{
								if (propertyName == "hierarchies")
								{
									child = ObjectType.Hierarchy;
									isSingleChild = false;
									return true;
								}
							}
						}
						else if (propertyName == "annotations")
						{
							child = ObjectType.Annotation;
							isSingleChild = false;
							return true;
						}
						break;
					}
					case 13:
						if (propertyName == "refreshPolicy")
						{
							child = ObjectType.RefreshPolicy;
							isSingleChild = true;
							return true;
						}
						break;
					case 16:
						if (propertyName == "calculationGroup")
						{
							child = ObjectType.CalculationGroup;
							isSingleChild = true;
							return true;
						}
						break;
					case 17:
					{
						char c = propertyName[0];
						if (c != 'c')
						{
							if (c == 'e')
							{
								if (propertyName == "excludedArtifacts")
								{
									child = ObjectType.ExcludedArtifact;
									isSingleChild = false;
									return true;
								}
							}
						}
						else if (propertyName == "changedProperties")
						{
							child = ObjectType.ChangedProperty;
							isSingleChild = false;
							return true;
						}
						break;
					}
					case 18:
						if (propertyName == "extendedProperties")
						{
							child = ObjectType.ExtendedProperty;
							isSingleChild = false;
							return true;
						}
						break;
					default:
						if (num == 27)
						{
							if (propertyName == "defaultDetailRowsDefinition")
							{
								child = ObjectType.DetailRowsDefinition;
								isSingleChild = true;
								return true;
							}
						}
						break;
					}
				}
				break;
			case ObjectType.Column:
				if (propertyName != null)
				{
					int num = propertyName.Length;
					if (num != 10)
					{
						if (num != 11)
						{
							switch (num)
							{
							case 17:
								if (propertyName == "changedProperties")
								{
									child = ObjectType.ChangedProperty;
									isSingleChild = false;
									return true;
								}
								break;
							case 18:
							{
								char c = propertyName[0];
								if (c != 'a')
								{
									if (c == 'e')
									{
										if (propertyName == "extendedProperties")
										{
											child = ObjectType.ExtendedProperty;
											isSingleChild = false;
											return true;
										}
									}
								}
								else if (propertyName == "attributeHierarchy")
								{
									child = ObjectType.AttributeHierarchy;
									isSingleChild = true;
									return true;
								}
								break;
							}
							case 20:
								if (propertyName == "relatedColumnDetails")
								{
									child = ObjectType.RelatedColumnDetails;
									isSingleChild = true;
									return true;
								}
								break;
							}
						}
						else
						{
							char c = propertyName[1];
							if (c != 'l')
							{
								if (c == 'n')
								{
									if (propertyName == "annotations")
									{
										child = ObjectType.Annotation;
										isSingleChild = false;
										return true;
									}
								}
							}
							else if (propertyName == "alternateOf")
							{
								child = ObjectType.AlternateOf;
								isSingleChild = true;
								return true;
							}
						}
					}
					else if (propertyName == "variations")
					{
						child = ObjectType.Variation;
						isSingleChild = false;
						return true;
					}
				}
				break;
			case ObjectType.AttributeHierarchy:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Partition:
				if (propertyName == "dataCoverageDefinition")
				{
					child = ObjectType.DataCoverageDefinition;
					isSingleChild = true;
					return true;
				}
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Relationship:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "changedProperties")
				{
					child = ObjectType.ChangedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Measure:
				if (propertyName == "kpi")
				{
					child = ObjectType.KPI;
					isSingleChild = true;
					return true;
				}
				if (propertyName == "detailRowsDefinition")
				{
					child = ObjectType.DetailRowsDefinition;
					isSingleChild = true;
					return true;
				}
				if (propertyName == "formatStringDefinition")
				{
					child = ObjectType.FormatStringDefinition;
					isSingleChild = true;
					return true;
				}
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "changedProperties")
				{
					child = ObjectType.ChangedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Hierarchy:
				if (propertyName == "levels")
				{
					child = ObjectType.Level;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "excludedArtifacts")
				{
					child = ObjectType.ExcludedArtifact;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "changedProperties")
				{
					child = ObjectType.ChangedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Level:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "changedProperties")
				{
					child = ObjectType.ChangedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.KPI:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Culture:
				if (propertyName == "linguisticMetadata")
				{
					child = ObjectType.LinguisticMetadata;
					isSingleChild = true;
					return true;
				}
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.LinguisticMetadata:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Perspective:
				if (propertyName == "tables")
				{
					child = ObjectType.PerspectiveTable;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.PerspectiveTable:
				if (propertyName == "columns")
				{
					child = ObjectType.PerspectiveColumn;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "measures")
				{
					child = ObjectType.PerspectiveMeasure;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "hierarchies")
				{
					child = ObjectType.PerspectiveHierarchy;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "sets")
				{
					child = ObjectType.PerspectiveSet;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.PerspectiveColumn:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.PerspectiveHierarchy:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.PerspectiveMeasure:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Role:
				if (propertyName == "members")
				{
					child = ObjectType.RoleMembership;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "tablePermissions")
				{
					child = ObjectType.TablePermission;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.RoleMembership:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.TablePermission:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "columnPermissions")
				{
					child = ObjectType.ColumnPermission;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Variation:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Set:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.PerspectiveSet:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Expression:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "excludedArtifacts")
				{
					child = ObjectType.ExcludedArtifact;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.ColumnPermission:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.RelatedColumnDetails:
				if (propertyName == "groupByColumns")
				{
					child = ObjectType.GroupByColumn;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.CalculationGroup:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "calculationItems")
				{
					child = ObjectType.CalculationItem;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.CalculationItem:
				if (propertyName == "formatStringDefinition")
				{
					child = ObjectType.FormatStringDefinition;
					isSingleChild = true;
					return true;
				}
				break;
			case ObjectType.AlternateOf:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.RefreshPolicy:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.QueryGroup:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.DataCoverageDefinition:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.CalculationExpression:
				if (propertyName == "formatStringDefinition")
				{
					child = ObjectType.FormatStringDefinition;
					isSingleChild = true;
					return true;
				}
				break;
			case ObjectType.Calendar:
				if (propertyName == "timeUnitColumnAssociations")
				{
					child = ObjectType.TimeUnitColumnAssociation;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.Function:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "changedProperties")
				{
					child = ObjectType.ChangedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			case ObjectType.BindingInfo:
				if (propertyName == "annotations")
				{
					child = ObjectType.Annotation;
					isSingleChild = false;
					return true;
				}
				if (propertyName == "extendedProperties")
				{
					child = ObjectType.ExtendedProperty;
					isSingleChild = false;
					return true;
				}
				break;
			}
			child = ObjectType.Null;
			isSingleChild = false;
			return false;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000A76C8 File Offset: 0x000A58C8
		public static bool SupportsScriptOut(ObjectType type)
		{
			if (type <= ObjectType.Partition)
			{
				if (type - ObjectType.DataSource > 1 && type != ObjectType.Partition)
				{
					return false;
				}
			}
			else if (type != ObjectType.Role)
			{
				if (type == ObjectType.Database)
				{
					return true;
				}
				return false;
			}
			return true;
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x000A76EC File Offset: 0x000A58EC
		public static bool SupportsSerialization(ObjectType type)
		{
			return type != ObjectType.ObjectTranslation && type != ObjectType.CalendarColumnReference;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x000A76FB File Offset: 0x000A58FB
		public static bool SupportsRefresh(ObjectType type)
		{
			if (type == ObjectType.Database)
			{
				type = ObjectType.Model;
			}
			return type == ObjectType.Model || type == ObjectType.Table || type == ObjectType.Partition;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x000A7717 File Offset: 0x000A5917
		public static bool SupportsExport(ObjectType type)
		{
			if (type == ObjectType.Database)
			{
				type = ObjectType.Model;
			}
			return type == ObjectType.Model;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000A772B File Offset: 0x000A592B
		public static bool CanHaveMoreThanOneTypeOfParent(ObjectType type)
		{
			if (type <= ObjectType.ExtendedProperty)
			{
				if (type != ObjectType.Annotation && type != ObjectType.ExtendedProperty)
				{
					return false;
				}
			}
			else if (type != ObjectType.DetailRowsDefinition && type != ObjectType.FormatStringDefinition && type - ObjectType.ChangedProperty > 1)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000A7752 File Offset: 0x000A5952
		public static MetadataObject LocateObjectByPath(ObjectPath path, MetadataObject root)
		{
			if (path == null || path.IsEmpty || root == null)
			{
				return root;
			}
			return ObjectTreeHelper.LocateObjectByPathImpl(path, root, false);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x000A776C File Offset: 0x000A596C
		internal static MetadataObject LocateObjectByPathImpl(ObjectPath path, MetadataObject root, bool skipRoot)
		{
			if (skipRoot)
			{
				Utils.Verify(path[0].Key == root.ObjectType);
				bool flag;
				if (!string.IsNullOrEmpty(path[0].Value))
				{
					NamedMetadataObject namedMetadataObject = root as NamedMetadataObject;
					flag = namedMetadataObject != null && string.Compare(path[0].Value, namedMetadataObject.Name, StringComparison.OrdinalIgnoreCase) == 0;
				}
				else
				{
					flag = true;
				}
				Utils.Verify(flag);
			}
			else if (path[0].Key == ObjectType.Database)
			{
				Utils.Verify(root.Model != null && root.Model.Database != null && string.Compare(path[0].Value, root.Model.Database.Name, StringComparison.OrdinalIgnoreCase) == 0);
				skipRoot = true;
			}
			for (int i = ((skipRoot > false) ? 1 : 0); i < path.Count; i++)
			{
				ObjectType objType = path[i].Key;
				string objName = path[i].Value;
				if (string.IsNullOrEmpty(objName))
				{
					root = (from c in root.GetDirectChildren(false)
						where c.ObjectType == objType
						select c).SingleOrDefault<MetadataObject>();
				}
				else if (ObjectTreeHelper.IsNamedObject(objType))
				{
					IMetadataObjectCollection metadataObjectCollection = (from c in root.GetChildrenCollections(false)
						where c.ItemType == objType
						select c).SingleOrDefault<IMetadataObjectCollection>();
					if (metadataObjectCollection == null)
					{
						return null;
					}
					root = ((INamedMetadataObjectCollection)metadataObjectCollection).Find(objName);
				}
				else if (ObjectTreeHelper.IsKeyedObject(objType))
				{
					IMetadataObjectCollection metadataObjectCollection2 = (from c in root.GetChildrenCollections(false)
						where c.ItemType == objType
						select c).SingleOrDefault<IMetadataObjectCollection>();
					if (metadataObjectCollection2 == null)
					{
						return null;
					}
					root = metadataObjectCollection2.GetObjects().FirstOrDefault((MetadataObject o) => ((IKeyedMetadataObject)o).LogicalPathElement == objName);
				}
				if (root == null)
				{
					return null;
				}
			}
			return root;
		}

		// Token: 0x040004B4 RID: 1204
		private static List<ObjectType> allTypesInTopologicalOrder;
	}
}
