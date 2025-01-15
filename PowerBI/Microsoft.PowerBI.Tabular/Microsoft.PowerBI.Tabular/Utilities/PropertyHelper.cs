using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular.Utilities
{
	// Token: 0x02000199 RID: 409
	internal static class PropertyHelper
	{
		// Token: 0x060018C1 RID: 6337 RVA: 0x000A7956 File Offset: 0x000A5B56
		internal static bool AreValuesIdentical(MetadataObject oldVal, MetadataObject newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x000A795C File Offset: 0x000A5B5C
		internal static string ConvertIDToXml(ObjectId value)
		{
			return value.ToString();
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x000A796B File Offset: 0x000A5B6B
		internal static bool AreValuesIdentical(ObjectId oldVal, ObjectId newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x000A7974 File Offset: 0x000A5B74
		internal static string ConvertStringToXml(string value)
		{
			return value;
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x000A7977 File Offset: 0x000A5B77
		internal static bool AreValuesIdentical(string oldVal, string newVal)
		{
			if (oldVal == null)
			{
				oldVal = "";
			}
			if (newVal == null)
			{
				newVal = "";
			}
			return oldVal.Equals(newVal, StringComparison.Ordinal);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x000A7995 File Offset: 0x000A5B95
		internal static string ConvertBooleanToXml(bool value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x000A799D File Offset: 0x000A5B9D
		internal static bool AreValuesIdentical(bool oldVal, bool newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x000A79A3 File Offset: 0x000A5BA3
		internal static string ConvertInt32ToXml(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000A79AB File Offset: 0x000A5BAB
		internal static bool AreValuesIdentical(int oldVal, int newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000A79B1 File Offset: 0x000A5BB1
		internal static string ConvertInt64ToXml(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000A79B9 File Offset: 0x000A5BB9
		internal static bool AreValuesIdentical(long oldVal, long newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x000A79BF File Offset: 0x000A5BBF
		internal static string ConvertUInt32ToXml(uint value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x000A79C7 File Offset: 0x000A5BC7
		internal static bool AreValuesIdentical(uint oldVal, uint newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x000A79CD File Offset: 0x000A5BCD
		internal static string ConvertUInt64ToXml(ulong value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x000A79D5 File Offset: 0x000A5BD5
		internal static bool AreValuesIdentical(ulong oldVal, ulong newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x000A79DB File Offset: 0x000A5BDB
		internal static string ConvertDateTimeToXml(DateTime value)
		{
			return XmlConvert.ToString(value, XmlDateTimeSerializationMode.Utc);
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000A79E4 File Offset: 0x000A5BE4
		internal static bool AreValuesIdentical(DateTime oldVal, DateTime newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000A79ED File Offset: 0x000A5BED
		internal static DateTime StartOfYear(DateTime datetime)
		{
			return new DateTime(datetime.Year, 1, 1);
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000A7A00 File Offset: 0x000A5C00
		internal static DateTime StartOfQuarter(DateTime datetime)
		{
			switch (datetime.Month)
			{
			case 1:
			case 2:
			case 3:
				return new DateTime(datetime.Year, 1, 1);
			case 4:
			case 5:
			case 6:
				return new DateTime(datetime.Year, 4, 1);
			case 7:
			case 8:
			case 9:
				return new DateTime(datetime.Year, 7, 1);
			default:
				return new DateTime(datetime.Year, 10, 1);
			}
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x000A7A7F File Offset: 0x000A5C7F
		internal static DateTime StartOfMonth(DateTime datetime)
		{
			return new DateTime(datetime.Year, datetime.Month, 1);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000A7A95 File Offset: 0x000A5C95
		internal static DateTime StartOfDay(DateTime datetime)
		{
			return new DateTime(datetime.Year, datetime.Month, datetime.Day);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x000A7AB4 File Offset: 0x000A5CB4
		internal static DateTime AddGranularity(DateTime sourceTime, RefreshGranularityType granularity)
		{
			switch (granularity)
			{
			case RefreshGranularityType.Day:
				return PropertyHelper.StartOfDay(sourceTime.AddDays(1.0));
			case RefreshGranularityType.Month:
				return PropertyHelper.StartOfMonth(sourceTime.AddMonths(1));
			case RefreshGranularityType.Quarter:
				return PropertyHelper.StartOfQuarter(sourceTime.AddMonths(3));
			case RefreshGranularityType.Year:
				return PropertyHelper.StartOfYear(sourceTime.AddYears(1));
			default:
				throw TomInternalException.Create("Invalid RefreshGranularityType {0} provided.", new object[] { granularity });
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x000A7B34 File Offset: 0x000A5D34
		internal static DateTime SubtractGranularity(DateTime sourceTime, RefreshGranularityType granularity)
		{
			switch (granularity)
			{
			case RefreshGranularityType.Day:
				return PropertyHelper.StartOfDay(sourceTime.AddDays(-1.0));
			case RefreshGranularityType.Month:
				return PropertyHelper.StartOfMonth(sourceTime.AddMonths(-1));
			case RefreshGranularityType.Quarter:
				return PropertyHelper.StartOfQuarter(sourceTime.AddMonths(-3));
			case RefreshGranularityType.Year:
				return PropertyHelper.StartOfYear(sourceTime.AddYears(-1));
			default:
				throw TomInternalException.Create("Invalid RefreshGranularityType {0} provided.", new object[] { granularity });
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000A7BB3 File Offset: 0x000A5DB3
		internal static RefreshGranularityType DeGranularity(RefreshGranularityType granularity)
		{
			switch (granularity)
			{
			case RefreshGranularityType.Quarter:
				return RefreshGranularityType.Month;
			case RefreshGranularityType.Year:
				return RefreshGranularityType.Quarter;
			}
			return RefreshGranularityType.Day;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x000A7BD0 File Offset: 0x000A5DD0
		internal static bool IsValidCompatibilityMode(CompatibilityMode mode, bool isUnknownModeValid = false)
		{
			switch (mode)
			{
			case CompatibilityMode.Unknown:
				return isUnknownModeValid;
			case CompatibilityMode.AnalysisServices:
			case CompatibilityMode.PowerBI:
			case CompatibilityMode.Excel:
				return true;
			}
			return false;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x000A7BF3 File Offset: 0x000A5DF3
		internal static IEnumerable<TEnum> GetEnumCompatibleValues<TEnum>(CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return PropertyHelper.GetEnumCompatibleValues(typeof(TEnum), mode, dbCompatibilityLevel).Cast<TEnum>();
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x000A7C0C File Offset: 0x000A5E0C
		internal static IEnumerable<object> GetEnumCompatibleValues(Type enumType, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (enumType == typeof(ObjectState))
			{
				return PropertyHelper.GetObjectStateCompatibleValues(mode, dbCompatibilityLevel).Cast<object>();
			}
			if (enumType == typeof(PartitionSourceType))
			{
				return PropertyHelper.GetPartitionSourceTypeCompatibleValues(mode, dbCompatibilityLevel).Cast<object>();
			}
			if (enumType == typeof(SecurityFilteringBehavior))
			{
				return PropertyHelper.GetSecurityFilteringBehaviorCompatibleValues(mode, dbCompatibilityLevel).Cast<object>();
			}
			if (enumType == typeof(DataSourceType))
			{
				return PropertyHelper.GetDataSourceTypeCompatibleValues(mode, dbCompatibilityLevel).Cast<object>();
			}
			if (enumType == typeof(ModeType))
			{
				return PropertyHelper.GetModeTypeCompatibleValues(mode, dbCompatibilityLevel).Cast<object>();
			}
			if (enumType == typeof(PowerBIDataSourceVersion))
			{
				return PropertyHelper.GetPowerBIDataSourceVersionCompatibleValues(mode, dbCompatibilityLevel).Cast<object>();
			}
			return PropertyHelper.GetEnumCompatibleValuesImpl(enumType);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x000A7CDC File Offset: 0x000A5EDC
		internal static string GetEnumDescription(Type enumType)
		{
			if (enumType == typeof(ObjectState))
			{
				return "An enumeration of possible values for object state.";
			}
			if (enumType == typeof(ImpersonationMode))
			{
				return "Determines how credentials are obtained for an impersonated connection to a data source during data import or refresh.";
			}
			if (enumType == typeof(Alignment))
			{
				return "An enumeration of possible values for aligning data in a cell. ";
			}
			if (enumType == typeof(AggregateFunction))
			{
				return "Specifies the aggregate function to be used by reporting tools to summarize column values.";
			}
			if (enumType == typeof(DatasourceIsolation))
			{
				return "Controls the locking behavior of the SQL statements when executing commands against the data source. ";
			}
			if (enumType == typeof(ColumnType))
			{
				return "An enumeration of possible values for a column type. ";
			}
			if (enumType == typeof(PartitionSourceType))
			{
				return "An enumeration of possible values for a partition source.";
			}
			if (enumType == typeof(EvaluationBehavior))
			{
				return "Evaluation behavior for calculated column.";
			}
			if (enumType == typeof(DataType))
			{
				return "Describes the type of data contained in the column. ";
			}
			if (enumType == typeof(RefreshType))
			{
				return "An enumeration of possible values for a refresh type.";
			}
			if (enumType == typeof(RelationshipEndCardinality))
			{
				return "An enumeration of possible values for defining cardinality on either side of a table relationship.";
			}
			if (enumType == typeof(CrossFilteringBehavior))
			{
				return "Indicates how relationships influence filtering of data. The enumeration defines the possible behaviors.";
			}
			if (enumType == typeof(SecurityFilteringBehavior))
			{
				return "Indicates how relationships influence filtering of data when evaluating row-level security expressions. The enumeration defines the possible behaviors.";
			}
			if (enumType == typeof(TranslatedProperty))
			{
				return "Specifies which property of the object is being translated.";
			}
			if (enumType == typeof(DataSourceType))
			{
				return "The type of DataSource. Currently, the only possible value is Provider.";
			}
			if (enumType == typeof(RelationshipType))
			{
				return "The type of relationship. Currently, the only possible value is SingleColumn.";
			}
			if (enumType == typeof(DateTimeRelationshipBehavior))
			{
				return "When joining two date time columns, indicates whether to join on date and time parts or on date part only.";
			}
			if (enumType == typeof(ModeType))
			{
				return "Defines the method for making data available in the partition.";
			}
			if (enumType == typeof(DataViewType))
			{
				return "Determines which partitions are to be selected to run queries against the model.";
			}
			if (enumType == typeof(DirectLakeBehavior))
			{
				return "Fallback behavior for Direct Lake models.";
			}
			if (enumType == typeof(ModelPermission))
			{
				return "An enumeration of possible model permissions that can be used in a Role object.";
			}
			if (enumType == typeof(RoleMemberType))
			{
				return "Indicates whether the particular member of a security role is an individual user or a group of users, or if the member is automatically detected.";
			}
			if (enumType == typeof(ExtendedPropertyType))
			{
				return "An enumeration of possible values for the type of value stored in extended property.";
			}
			if (enumType == typeof(HierarchyHideMembersType))
			{
				return "Ragged/unbalanced hierarchies can be enabled by hiding members.";
			}
			if (enumType == typeof(ExpressionKind))
			{
				return "Indicates the dialect of the query expression.";
			}
			if (enumType == typeof(MetadataPermission))
			{
				return "Access control to a data defined by a metadata object.";
			}
			if (enumType == typeof(EncodingHintType))
			{
				return "Encoding hint to suggest whether a column should use hash encoding.";
			}
			if (enumType == typeof(SummarizationType))
			{
				return "Specifies the Summarization type to be used by alternative sources' columns.";
			}
			if (enumType == typeof(RefreshGranularityType))
			{
				return "Specifies the granularity of the refresh policy for auto partitioning";
			}
			if (enumType == typeof(RefreshPolicyType))
			{
				return "Specifies the refresh policy type of a table";
			}
			if (enumType == typeof(PowerBIDataSourceVersion))
			{
				return "DataSource format version in Power BI Service.";
			}
			if (enumType == typeof(ContentType))
			{
				return "The type of the content of a string. E.g. XML or JSON.";
			}
			if (enumType == typeof(DataSourceVariablesOverrideBehaviorType))
			{
				return "Data source edit varaibles override behaviour type. E.g. Disallow or Allow.";
			}
			if (enumType == typeof(RefreshPolicyMode))
			{
				return "Mode of a refresh policy.";
			}
			if (enumType == typeof(TimeUnit))
			{
				return "Various options for units that describe time information.";
			}
			if (enumType == typeof(CalculationGroupSelectionMode))
			{
				return "Options for selections on calculation groups which do not invoke a calculation item.";
			}
			if (enumType == typeof(ValueFilterBehaviorType))
			{
				return "Determines value filter behavior for SummarizeColumns";
			}
			if (enumType == typeof(BindingInfoType))
			{
				return "The type of a BindingInfo metadata element.";
			}
			return null;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x000A807A File Offset: 0x000A627A
		private static IEnumerable<object> GetEnumCompatibleValuesImpl(Type enumType)
		{
			foreach (object obj in Enum.GetValues(enumType))
			{
				yield return obj;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000A808C File Offset: 0x000A628C
		internal static string ConvertObjectStateToXml(ObjectState value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000A80A3 File Offset: 0x000A62A3
		internal static bool AreValuesIdentical(ObjectState oldVal, ObjectState newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000A80A9 File Offset: 0x000A62A9
		internal static CompatibilityRestrictionSet GetObjectStateCompatibilityRestrictions(ObjectState value)
		{
			if (value == ObjectState.ForceCalculationNeeded)
			{
				return CompatibilityRestrictions.ObjectState_ForceCalculationNeeded;
			}
			return CompatibilityRestrictionSet.Empty;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x000A80BB File Offset: 0x000A62BB
		internal static bool IsObjectStateValueCompatible(ObjectState value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return value != ObjectState.ForceCalculationNeeded || CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000A80D0 File Offset: 0x000A62D0
		internal static IEnumerable<ObjectState> GetObjectStateCompatibleValues(CompatibilityMode mode, int dbCompatibilityLevel)
		{
			yield return ObjectState.Ready;
			yield return ObjectState.NoData;
			yield return ObjectState.CalculationNeeded;
			yield return ObjectState.SemanticError;
			yield return ObjectState.EvaluationError;
			yield return ObjectState.DependencyError;
			yield return ObjectState.Incomplete;
			if (CompatibilityRestrictions.ObjectState_ForceCalculationNeeded.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return ObjectState.ForceCalculationNeeded;
			}
			yield break;
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000A80E8 File Offset: 0x000A62E8
		internal static string ConvertImpersonationModeToXml(ImpersonationMode value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000A80FF File Offset: 0x000A62FF
		internal static bool AreValuesIdentical(ImpersonationMode oldVal, ImpersonationMode newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000A8108 File Offset: 0x000A6308
		internal static string ConvertAlignmentToXml(Alignment value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x000A811F File Offset: 0x000A631F
		internal static bool AreValuesIdentical(Alignment oldVal, Alignment newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x000A8128 File Offset: 0x000A6328
		internal static string ConvertAggregateFunctionToXml(AggregateFunction value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x000A813F File Offset: 0x000A633F
		internal static bool AreValuesIdentical(AggregateFunction oldVal, AggregateFunction newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x000A8148 File Offset: 0x000A6348
		internal static string ConvertDatasourceIsolationToXml(DatasourceIsolation value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x000A815F File Offset: 0x000A635F
		internal static bool AreValuesIdentical(DatasourceIsolation oldVal, DatasourceIsolation newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x000A8168 File Offset: 0x000A6368
		internal static string ConvertColumnTypeToXml(ColumnType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000A817F File Offset: 0x000A637F
		internal static bool AreValuesIdentical(ColumnType oldVal, ColumnType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x000A8188 File Offset: 0x000A6388
		internal static string ConvertPartitionSourceTypeToXml(PartitionSourceType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x000A819F File Offset: 0x000A639F
		internal static bool AreValuesIdentical(PartitionSourceType oldVal, PartitionSourceType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x000A81A8 File Offset: 0x000A63A8
		internal static CompatibilityRestrictionSet GetPartitionSourceTypeCompatibilityRestrictions(PartitionSourceType value)
		{
			switch (value)
			{
			case PartitionSourceType.M:
				return CompatibilityRestrictions.PartitionSourceType_M;
			case PartitionSourceType.Entity:
				return CompatibilityRestrictions.PartitionSourceType_Entity;
			case PartitionSourceType.PolicyRange:
				return CompatibilityRestrictions.PartitionSourceType_PolicyRange;
			case PartitionSourceType.CalculationGroup:
				return CompatibilityRestrictions.PartitionSourceType_CalculationGroup;
			case PartitionSourceType.Inferred:
				return CompatibilityRestrictions.PartitionSourceType_Inferred;
			case PartitionSourceType.Parquet:
				return CompatibilityRestrictions.PartitionSourceType_Parquet;
			default:
				return CompatibilityRestrictionSet.Empty;
			}
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x000A8200 File Offset: 0x000A6400
		internal static bool IsPartitionSourceTypeValueCompatible(PartitionSourceType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			switch (value)
			{
			case PartitionSourceType.M:
				return CompatibilityRestrictions.PartitionSourceType_M.IsCompatible(mode, dbCompatibilityLevel);
			case PartitionSourceType.Entity:
				return CompatibilityRestrictions.PartitionSourceType_Entity.IsCompatible(mode, dbCompatibilityLevel);
			case PartitionSourceType.PolicyRange:
				return CompatibilityRestrictions.PartitionSourceType_PolicyRange.IsCompatible(mode, dbCompatibilityLevel);
			case PartitionSourceType.CalculationGroup:
				return CompatibilityRestrictions.PartitionSourceType_CalculationGroup.IsCompatible(mode, dbCompatibilityLevel);
			case PartitionSourceType.Inferred:
				return CompatibilityRestrictions.PartitionSourceType_Inferred.IsCompatible(mode, dbCompatibilityLevel);
			case PartitionSourceType.Parquet:
				return CompatibilityRestrictions.PartitionSourceType_Parquet.IsCompatible(mode, dbCompatibilityLevel);
			default:
				return true;
			}
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x000A827E File Offset: 0x000A647E
		internal static IEnumerable<PartitionSourceType> GetPartitionSourceTypeCompatibleValues(CompatibilityMode mode, int dbCompatibilityLevel)
		{
			yield return PartitionSourceType.Query;
			yield return PartitionSourceType.Calculated;
			yield return PartitionSourceType.None;
			if (CompatibilityRestrictions.PartitionSourceType_M.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return PartitionSourceType.M;
			}
			if (CompatibilityRestrictions.PartitionSourceType_Entity.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return PartitionSourceType.Entity;
			}
			if (CompatibilityRestrictions.PartitionSourceType_PolicyRange.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return PartitionSourceType.PolicyRange;
			}
			if (CompatibilityRestrictions.PartitionSourceType_CalculationGroup.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return PartitionSourceType.CalculationGroup;
			}
			if (CompatibilityRestrictions.PartitionSourceType_Inferred.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return PartitionSourceType.Inferred;
			}
			if (CompatibilityRestrictions.PartitionSourceType_Parquet.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return PartitionSourceType.Parquet;
			}
			yield break;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x000A8298 File Offset: 0x000A6498
		internal static string ConvertEvaluationBehaviorToXml(EvaluationBehavior value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x000A82AF File Offset: 0x000A64AF
		internal static bool AreValuesIdentical(EvaluationBehavior oldVal, EvaluationBehavior newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x000A82B5 File Offset: 0x000A64B5
		internal static CompatibilityRestrictionSet GetEvaluationBehaviorCompatibilityRestrictions(EvaluationBehavior value)
		{
			return CompatibilityRestrictions.EvaluationBehavior;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000A82BC File Offset: 0x000A64BC
		internal static bool IsEvaluationBehaviorValueCompatible(EvaluationBehavior value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.EvaluationBehavior.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000A82CC File Offset: 0x000A64CC
		internal static string ConvertDataTypeToXml(DataType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000A82E3 File Offset: 0x000A64E3
		internal static bool AreValuesIdentical(DataType oldVal, DataType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000A82EC File Offset: 0x000A64EC
		internal static string ConvertRefreshTypeToXml(RefreshType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x000A8303 File Offset: 0x000A6503
		internal static bool AreValuesIdentical(RefreshType oldVal, RefreshType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x000A830C File Offset: 0x000A650C
		internal static string ConvertRelationshipEndCardinalityToXml(RelationshipEndCardinality value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018FB RID: 6395 RVA: 0x000A8323 File Offset: 0x000A6523
		internal static bool AreValuesIdentical(RelationshipEndCardinality oldVal, RelationshipEndCardinality newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x000A832C File Offset: 0x000A652C
		internal static string ConvertCrossFilteringBehaviorToXml(CrossFilteringBehavior value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x000A8343 File Offset: 0x000A6543
		internal static bool AreValuesIdentical(CrossFilteringBehavior oldVal, CrossFilteringBehavior newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x000A834C File Offset: 0x000A654C
		internal static string ConvertSecurityFilteringBehaviorToXml(SecurityFilteringBehavior value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000A8363 File Offset: 0x000A6563
		internal static bool AreValuesIdentical(SecurityFilteringBehavior oldVal, SecurityFilteringBehavior newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000A8369 File Offset: 0x000A6569
		internal static CompatibilityRestrictionSet GetSecurityFilteringBehaviorCompatibilityRestrictions(SecurityFilteringBehavior value)
		{
			if (value == SecurityFilteringBehavior.None)
			{
				return CompatibilityRestrictions.SecurityFilteringBehavior_None;
			}
			return CompatibilityRestrictionSet.Empty;
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000A837A File Offset: 0x000A657A
		internal static bool IsSecurityFilteringBehaviorValueCompatible(SecurityFilteringBehavior value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return value != SecurityFilteringBehavior.None || CompatibilityRestrictions.SecurityFilteringBehavior_None.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x000A838E File Offset: 0x000A658E
		internal static IEnumerable<SecurityFilteringBehavior> GetSecurityFilteringBehaviorCompatibleValues(CompatibilityMode mode, int dbCompatibilityLevel)
		{
			yield return SecurityFilteringBehavior.OneDirection;
			yield return SecurityFilteringBehavior.BothDirections;
			if (CompatibilityRestrictions.SecurityFilteringBehavior_None.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return SecurityFilteringBehavior.None;
			}
			yield break;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x000A83A8 File Offset: 0x000A65A8
		internal static string ConvertTranslatedPropertyToXml(TranslatedProperty value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000A83BF File Offset: 0x000A65BF
		internal static bool AreValuesIdentical(TranslatedProperty oldVal, TranslatedProperty newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x000A83C8 File Offset: 0x000A65C8
		internal static string ConvertDataSourceTypeToXml(DataSourceType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000A83DF File Offset: 0x000A65DF
		internal static bool AreValuesIdentical(DataSourceType oldVal, DataSourceType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000A83E5 File Offset: 0x000A65E5
		internal static CompatibilityRestrictionSet GetDataSourceTypeCompatibilityRestrictions(DataSourceType value)
		{
			if (value == DataSourceType.Structured)
			{
				return CompatibilityRestrictions.DataSourceType_Structured;
			}
			return CompatibilityRestrictionSet.Empty;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x000A83F6 File Offset: 0x000A65F6
		internal static bool IsDataSourceTypeValueCompatible(DataSourceType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return value != DataSourceType.Structured || CompatibilityRestrictions.DataSourceType_Structured.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x000A840A File Offset: 0x000A660A
		internal static IEnumerable<DataSourceType> GetDataSourceTypeCompatibleValues(CompatibilityMode mode, int dbCompatibilityLevel)
		{
			yield return DataSourceType.Provider;
			if (CompatibilityRestrictions.DataSourceType_Structured.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return DataSourceType.Structured;
			}
			yield break;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x000A8424 File Offset: 0x000A6624
		internal static string ConvertRelationshipTypeToXml(RelationshipType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x000A843B File Offset: 0x000A663B
		internal static bool AreValuesIdentical(RelationshipType oldVal, RelationshipType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000A8444 File Offset: 0x000A6644
		internal static string ConvertDateTimeRelationshipBehaviorToXml(DateTimeRelationshipBehavior value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000A845B File Offset: 0x000A665B
		internal static bool AreValuesIdentical(DateTimeRelationshipBehavior oldVal, DateTimeRelationshipBehavior newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000A8464 File Offset: 0x000A6664
		internal static string ConvertModeTypeToXml(ModeType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000A847B File Offset: 0x000A667B
		internal static bool AreValuesIdentical(ModeType oldVal, ModeType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x000A8481 File Offset: 0x000A6681
		internal static CompatibilityRestrictionSet GetModeTypeCompatibilityRestrictions(ModeType value)
		{
			switch (value)
			{
			case ModeType.Push:
				return CompatibilityRestrictions.ModeType_Push;
			case ModeType.Dual:
				return CompatibilityRestrictions.ModeType_Dual;
			case ModeType.DirectLake:
				return CompatibilityRestrictions.ModeType_DirectLake;
			default:
				return CompatibilityRestrictionSet.Empty;
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000A84B0 File Offset: 0x000A66B0
		internal static bool IsModeTypeValueCompatible(ModeType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			switch (value)
			{
			case ModeType.Push:
				return CompatibilityRestrictions.ModeType_Push.IsCompatible(mode, dbCompatibilityLevel);
			case ModeType.Dual:
				return CompatibilityRestrictions.ModeType_Dual.IsCompatible(mode, dbCompatibilityLevel);
			case ModeType.DirectLake:
				return CompatibilityRestrictions.ModeType_DirectLake.IsCompatible(mode, dbCompatibilityLevel);
			default:
				return true;
			}
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x000A84F0 File Offset: 0x000A66F0
		internal static IEnumerable<ModeType> GetModeTypeCompatibleValues(CompatibilityMode mode, int dbCompatibilityLevel)
		{
			yield return ModeType.Import;
			yield return ModeType.DirectQuery;
			yield return ModeType.Default;
			if (CompatibilityRestrictions.ModeType_Push.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return ModeType.Push;
			}
			if (CompatibilityRestrictions.ModeType_Dual.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return ModeType.Dual;
			}
			if (CompatibilityRestrictions.ModeType_DirectLake.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return ModeType.DirectLake;
			}
			yield break;
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x000A8508 File Offset: 0x000A6708
		internal static string ConvertDataViewTypeToXml(DataViewType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x000A851F File Offset: 0x000A671F
		internal static bool AreValuesIdentical(DataViewType oldVal, DataViewType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x000A8528 File Offset: 0x000A6728
		internal static string ConvertDirectLakeBehaviorToXml(DirectLakeBehavior value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x000A853F File Offset: 0x000A673F
		internal static bool AreValuesIdentical(DirectLakeBehavior oldVal, DirectLakeBehavior newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x000A8545 File Offset: 0x000A6745
		internal static CompatibilityRestrictionSet GetDirectLakeBehaviorCompatibilityRestrictions(DirectLakeBehavior value)
		{
			return CompatibilityRestrictions.DirectLakeBehavior;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000A854C File Offset: 0x000A674C
		internal static bool IsDirectLakeBehaviorValueCompatible(DirectLakeBehavior value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.DirectLakeBehavior.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000A855C File Offset: 0x000A675C
		internal static string ConvertModelPermissionToXml(ModelPermission value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000A8573 File Offset: 0x000A6773
		internal static bool AreValuesIdentical(ModelPermission oldVal, ModelPermission newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x000A857C File Offset: 0x000A677C
		internal static string ConvertRoleMemberTypeToXml(RoleMemberType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x000A8593 File Offset: 0x000A6793
		internal static bool AreValuesIdentical(RoleMemberType oldVal, RoleMemberType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000A859C File Offset: 0x000A679C
		internal static string ConvertExtendedPropertyTypeToXml(ExtendedPropertyType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000A85B3 File Offset: 0x000A67B3
		internal static bool AreValuesIdentical(ExtendedPropertyType oldVal, ExtendedPropertyType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x000A85B9 File Offset: 0x000A67B9
		internal static CompatibilityRestrictionSet GetExtendedPropertyTypeCompatibilityRestrictions(ExtendedPropertyType value)
		{
			return CompatibilityRestrictions.ExtendedPropertyType;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x000A85C0 File Offset: 0x000A67C0
		internal static bool IsExtendedPropertyTypeValueCompatible(ExtendedPropertyType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.ExtendedPropertyType.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x000A85D0 File Offset: 0x000A67D0
		internal static string ConvertHierarchyHideMembersTypeToXml(HierarchyHideMembersType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x000A85E7 File Offset: 0x000A67E7
		internal static bool AreValuesIdentical(HierarchyHideMembersType oldVal, HierarchyHideMembersType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000A85ED File Offset: 0x000A67ED
		internal static CompatibilityRestrictionSet GetHierarchyHideMembersTypeCompatibilityRestrictions(HierarchyHideMembersType value)
		{
			return CompatibilityRestrictions.HierarchyHideMembersType;
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x000A85F4 File Offset: 0x000A67F4
		internal static bool IsHierarchyHideMembersTypeValueCompatible(HierarchyHideMembersType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.HierarchyHideMembersType.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000A8604 File Offset: 0x000A6804
		internal static string ConvertExpressionKindToXml(ExpressionKind value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x000A861B File Offset: 0x000A681B
		internal static bool AreValuesIdentical(ExpressionKind oldVal, ExpressionKind newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x000A8621 File Offset: 0x000A6821
		internal static CompatibilityRestrictionSet GetExpressionKindCompatibilityRestrictions(ExpressionKind value)
		{
			return CompatibilityRestrictions.ExpressionKind;
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000A8628 File Offset: 0x000A6828
		internal static bool IsExpressionKindValueCompatible(ExpressionKind value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.ExpressionKind.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000A8638 File Offset: 0x000A6838
		internal static string ConvertMetadataPermissionToXml(MetadataPermission value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x000A864F File Offset: 0x000A684F
		internal static bool AreValuesIdentical(MetadataPermission oldVal, MetadataPermission newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x000A8655 File Offset: 0x000A6855
		internal static CompatibilityRestrictionSet GetMetadataPermissionCompatibilityRestrictions(MetadataPermission value)
		{
			return CompatibilityRestrictions.MetadataPermission;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x000A865C File Offset: 0x000A685C
		internal static bool IsMetadataPermissionValueCompatible(MetadataPermission value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.MetadataPermission.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x000A866C File Offset: 0x000A686C
		internal static string ConvertEncodingHintTypeToXml(EncodingHintType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x000A8683 File Offset: 0x000A6883
		internal static bool AreValuesIdentical(EncodingHintType oldVal, EncodingHintType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x000A8689 File Offset: 0x000A6889
		internal static CompatibilityRestrictionSet GetEncodingHintTypeCompatibilityRestrictions(EncodingHintType value)
		{
			return CompatibilityRestrictions.EncodingHintType;
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x000A8690 File Offset: 0x000A6890
		internal static bool IsEncodingHintTypeValueCompatible(EncodingHintType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.EncodingHintType.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x000A86A0 File Offset: 0x000A68A0
		internal static string ConvertSummarizationTypeToXml(SummarizationType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x000A86B7 File Offset: 0x000A68B7
		internal static bool AreValuesIdentical(SummarizationType oldVal, SummarizationType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x000A86BD File Offset: 0x000A68BD
		internal static CompatibilityRestrictionSet GetSummarizationTypeCompatibilityRestrictions(SummarizationType value)
		{
			return CompatibilityRestrictions.SummarizationType;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x000A86C4 File Offset: 0x000A68C4
		internal static bool IsSummarizationTypeValueCompatible(SummarizationType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.SummarizationType.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000A86D4 File Offset: 0x000A68D4
		internal static string ConvertRefreshGranularityTypeToXml(RefreshGranularityType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x000A86EB File Offset: 0x000A68EB
		internal static bool AreValuesIdentical(RefreshGranularityType oldVal, RefreshGranularityType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x000A86F1 File Offset: 0x000A68F1
		internal static CompatibilityRestrictionSet GetRefreshGranularityTypeCompatibilityRestrictions(RefreshGranularityType value)
		{
			return CompatibilityRestrictions.RefreshGranularityType;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x000A86F8 File Offset: 0x000A68F8
		internal static bool IsRefreshGranularityTypeValueCompatible(RefreshGranularityType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.RefreshGranularityType.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000A8708 File Offset: 0x000A6908
		internal static string ConvertRefreshPolicyTypeToXml(RefreshPolicyType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000A871F File Offset: 0x000A691F
		internal static bool AreValuesIdentical(RefreshPolicyType oldVal, RefreshPolicyType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x000A8725 File Offset: 0x000A6925
		internal static CompatibilityRestrictionSet GetRefreshPolicyTypeCompatibilityRestrictions(RefreshPolicyType value)
		{
			return CompatibilityRestrictions.RefreshPolicyType;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x000A872C File Offset: 0x000A692C
		internal static bool IsRefreshPolicyTypeValueCompatible(RefreshPolicyType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.RefreshPolicyType.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x000A873C File Offset: 0x000A693C
		internal static string ConvertPowerBIDataSourceVersionToXml(PowerBIDataSourceVersion value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x000A8753 File Offset: 0x000A6953
		internal static bool AreValuesIdentical(PowerBIDataSourceVersion oldVal, PowerBIDataSourceVersion newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x000A8759 File Offset: 0x000A6959
		internal static CompatibilityRestrictionSet GetPowerBIDataSourceVersionCompatibilityRestrictions(PowerBIDataSourceVersion value)
		{
			if (value == PowerBIDataSourceVersion.PowerBI_V3)
			{
				return CompatibilityRestrictions.PowerBIDataSourceVersion.Merge(CompatibilityRestrictions.PowerBIDataSourceVersion_PowerBI_V3);
			}
			return CompatibilityRestrictions.PowerBIDataSourceVersion;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000A8774 File Offset: 0x000A6974
		internal static bool IsPowerBIDataSourceVersionValueCompatible(PowerBIDataSourceVersion value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.PowerBIDataSourceVersion.IsCompatible(mode, dbCompatibilityLevel) && (value != PowerBIDataSourceVersion.PowerBI_V3 || CompatibilityRestrictions.PowerBIDataSourceVersion_PowerBI_V3.IsCompatible(mode, dbCompatibilityLevel));
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x000A8798 File Offset: 0x000A6998
		internal static IEnumerable<PowerBIDataSourceVersion> GetPowerBIDataSourceVersionCompatibleValues(CompatibilityMode mode, int dbCompatibilityLevel)
		{
			yield return PowerBIDataSourceVersion.PowerBI_V1;
			yield return PowerBIDataSourceVersion.PowerBI_V2;
			if (CompatibilityRestrictions.PowerBIDataSourceVersion_PowerBI_V3.IsCompatible(mode, dbCompatibilityLevel))
			{
				yield return PowerBIDataSourceVersion.PowerBI_V3;
			}
			yield break;
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x000A87B0 File Offset: 0x000A69B0
		internal static string ConvertContentTypeToXml(ContentType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x000A87C7 File Offset: 0x000A69C7
		internal static bool AreValuesIdentical(ContentType oldVal, ContentType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x000A87CD File Offset: 0x000A69CD
		internal static CompatibilityRestrictionSet GetContentTypeCompatibilityRestrictions(ContentType value)
		{
			return CompatibilityRestrictions.ContentType;
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x000A87D4 File Offset: 0x000A69D4
		internal static bool IsContentTypeValueCompatible(ContentType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.ContentType.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x000A87E4 File Offset: 0x000A69E4
		internal static string ConvertDataSourceVariablesOverrideBehaviorTypeToXml(DataSourceVariablesOverrideBehaviorType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x000A87FB File Offset: 0x000A69FB
		internal static bool AreValuesIdentical(DataSourceVariablesOverrideBehaviorType oldVal, DataSourceVariablesOverrideBehaviorType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x000A8801 File Offset: 0x000A6A01
		internal static CompatibilityRestrictionSet GetDataSourceVariablesOverrideBehaviorTypeCompatibilityRestrictions(DataSourceVariablesOverrideBehaviorType value)
		{
			return CompatibilityRestrictions.DataSourceVariablesOverrideBehaviorType;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x000A8808 File Offset: 0x000A6A08
		internal static bool IsDataSourceVariablesOverrideBehaviorTypeValueCompatible(DataSourceVariablesOverrideBehaviorType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.DataSourceVariablesOverrideBehaviorType.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x000A8818 File Offset: 0x000A6A18
		internal static string ConvertRefreshPolicyModeToXml(RefreshPolicyMode value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x000A882F File Offset: 0x000A6A2F
		internal static bool AreValuesIdentical(RefreshPolicyMode oldVal, RefreshPolicyMode newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000A8835 File Offset: 0x000A6A35
		internal static CompatibilityRestrictionSet GetRefreshPolicyModeCompatibilityRestrictions(RefreshPolicyMode value)
		{
			return CompatibilityRestrictions.RefreshPolicyMode;
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x000A883C File Offset: 0x000A6A3C
		internal static bool IsRefreshPolicyModeValueCompatible(RefreshPolicyMode value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.RefreshPolicyMode.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x000A884C File Offset: 0x000A6A4C
		internal static string ConvertTimeUnitToXml(TimeUnit value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x000A8863 File Offset: 0x000A6A63
		internal static bool AreValuesIdentical(TimeUnit oldVal, TimeUnit newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x000A8869 File Offset: 0x000A6A69
		internal static CompatibilityRestrictionSet GetTimeUnitCompatibilityRestrictions(TimeUnit value)
		{
			return CompatibilityRestrictions.TimeUnit;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x000A8870 File Offset: 0x000A6A70
		internal static bool IsTimeUnitValueCompatible(TimeUnit value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.TimeUnit.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x000A8880 File Offset: 0x000A6A80
		internal static string ConvertCalculationGroupSelectionModeToXml(CalculationGroupSelectionMode value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x000A8897 File Offset: 0x000A6A97
		internal static bool AreValuesIdentical(CalculationGroupSelectionMode oldVal, CalculationGroupSelectionMode newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000A889D File Offset: 0x000A6A9D
		internal static CompatibilityRestrictionSet GetCalculationGroupSelectionModeCompatibilityRestrictions(CalculationGroupSelectionMode value)
		{
			return CompatibilityRestrictions.CalculationGroupSelectionMode;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000A88A4 File Offset: 0x000A6AA4
		internal static bool IsCalculationGroupSelectionModeValueCompatible(CalculationGroupSelectionMode value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.CalculationGroupSelectionMode.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000A88B4 File Offset: 0x000A6AB4
		internal static string ConvertValueFilterBehaviorTypeToXml(ValueFilterBehaviorType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000A88CB File Offset: 0x000A6ACB
		internal static bool AreValuesIdentical(ValueFilterBehaviorType oldVal, ValueFilterBehaviorType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000A88D1 File Offset: 0x000A6AD1
		internal static CompatibilityRestrictionSet GetValueFilterBehaviorTypeCompatibilityRestrictions(ValueFilterBehaviorType value)
		{
			return CompatibilityRestrictions.ValueFilterBehaviorType;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000A88D8 File Offset: 0x000A6AD8
		internal static bool IsValueFilterBehaviorTypeValueCompatible(ValueFilterBehaviorType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.ValueFilterBehaviorType.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000A88E8 File Offset: 0x000A6AE8
		internal static string ConvertBindingInfoTypeToXml(BindingInfoType value)
		{
			return ((long)value).ToString();
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x000A88FF File Offset: 0x000A6AFF
		internal static bool AreValuesIdentical(BindingInfoType oldVal, BindingInfoType newVal)
		{
			return oldVal == newVal;
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000A8905 File Offset: 0x000A6B05
		internal static CompatibilityRestrictionSet GetBindingInfoTypeCompatibilityRestrictions(BindingInfoType value)
		{
			return CompatibilityRestrictions.BindingInfoType;
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x000A890C File Offset: 0x000A6B0C
		internal static bool IsBindingInfoTypeValueCompatible(BindingInfoType value, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return CompatibilityRestrictions.BindingInfoType.IsCompatible(mode, dbCompatibilityLevel);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000A891A File Offset: 0x000A6B1A
		internal static string GetCuratedValueForPassword(string password)
		{
			return "********";
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000A8924 File Offset: 0x000A6B24
		internal static string GetCuratedValueForConnectionString(string connectionString)
		{
			string text;
			try
			{
				DbConnectionStringBuilder dbConnectionStringBuilder = new DbConnectionStringBuilder
				{
					ConnectionString = connectionString
				};
				if (PropertyHelper.RemovePasswordFromConnectionString(dbConnectionStringBuilder, false, string.Empty))
				{
					text = dbConnectionStringBuilder.ConnectionString;
				}
				else
				{
					text = connectionString;
				}
			}
			catch (Exception)
			{
				text = "********";
			}
			return text;
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000A8974 File Offset: 0x000A6B74
		internal static string GetCuratedValueForCredential(string credential)
		{
			if (string.IsNullOrEmpty(credential))
			{
				return credential;
			}
			string text;
			try
			{
				text = JsonPropertyHelper.ConvertJsonValueToString(PropertyHelper.GetCuratedValueForCredential(JsonPropertyHelper.ConvertStringToJsonObject(credential, "Credential")));
			}
			catch (Exception)
			{
				text = "********";
			}
			return text;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000A89C0 File Offset: 0x000A6BC0
		internal static JToken GetCuratedValueForPassword(JToken connectionString)
		{
			return new JValue("********");
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000A89CC File Offset: 0x000A6BCC
		internal static JToken GetCuratedValueForConnectionString(JToken connectionString)
		{
			return new JValue(PropertyHelper.GetCuratedValueForConnectionString(JsonPropertyHelper.ConvertJsonValueToString(connectionString)));
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000A89E0 File Offset: 0x000A6BE0
		internal static JToken GetCuratedValueForCredential(JToken credential)
		{
			if (credential.Type == 1)
			{
				JObject jobject = (JObject)credential.DeepClone();
				foreach (JProperty jproperty in jobject.Properties())
				{
					if (string.Equals(jproperty.Name, "connectionstring"))
					{
						jproperty.Value = new JValue(PropertyHelper.GetCuratedValueForConnectionString(jproperty.Value));
					}
					else if (!PropertyHelper.AllowListedCredentialProperties.Contains(jproperty.Name))
					{
						jproperty.Value = new JValue("********");
					}
				}
				return jobject;
			}
			return JToken.FromObject("");
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x000A8A98 File Offset: 0x000A6C98
		private static bool RemovePasswordFromConnectionString(DbConnectionStringBuilder builder, bool useOdbcRules, string provider)
		{
			bool flag = false;
			object obj;
			if (builder.TryGetValue("Provider", out obj))
			{
				provider = (string)obj;
			}
			foreach (string text in PropertyHelper.ConnectionString_RestrictedElementKeys)
			{
				if (builder.ContainsKey(text))
				{
					builder[text] = "********";
					flag = true;
				}
			}
			object obj2;
			if (builder.TryGetValue("Extended Properties", out obj2) && PropertyHelper.ConnectionString_ProvidersWithExtPropsToCurate.Contains(provider, StringComparer.OrdinalIgnoreCase))
			{
				bool flag2 = useOdbcRules;
				if (!flag2 && string.Compare(provider, "MSDASQL", StringComparison.OrdinalIgnoreCase) == 0)
				{
					flag2 = true;
				}
				DbConnectionStringBuilder dbConnectionStringBuilder;
				if (PropertyHelper.GetConnectionStringBuilder(PropertyHelper.NormalizeConnectionStringValue((string)obj2, useOdbcRules), flag2, out dbConnectionStringBuilder) && PropertyHelper.RemovePasswordFromConnectionString(dbConnectionStringBuilder, flag2, provider))
				{
					builder["Extended Properties"] = dbConnectionStringBuilder.ConnectionString;
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000A8B66 File Offset: 0x000A6D66
		private static bool GetConnectionStringBuilder(string connectionString, bool useOdbcRules, out DbConnectionStringBuilder builder)
		{
			if (connectionString.ToString().IndexOf('=') == -1)
			{
				builder = null;
				return false;
			}
			builder = new DbConnectionStringBuilder(useOdbcRules)
			{
				ConnectionString = connectionString
			};
			return true;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000A8B90 File Offset: 0x000A6D90
		private static string NormalizeConnectionStringValue(string value, bool useOdbcRules)
		{
			value = value.Trim();
			bool flag = false;
			if (useOdbcRules)
			{
				if (value.Length >= 2 && value[0] == '{' && value[value.Length - 1] == '}')
				{
					value = value.Substring(1, value.Length - 2);
					flag = true;
				}
			}
			else if (value.Length >= 2 && value[0] == '"' && value[value.Length - 1] == '"')
			{
				value = value.Substring(1, value.Length - 2);
				flag = true;
			}
			if (flag)
			{
				char[] array;
				if (useOdbcRules)
				{
					array = new char[] { '\\', '{', '}' };
				}
				else
				{
					array = new char[] { '\\', '"', '\'' };
				}
				StringBuilder stringBuilder = new StringBuilder();
				int i = 0;
				while (i < value.Length)
				{
					int num = value.IndexOfAny(array, i);
					if (num == -1)
					{
						stringBuilder.Append(value, i, value.Length - i);
						i = value.Length;
					}
					else if (num < value.Length - 1)
					{
						if (value[num] == '\\' && ((useOdbcRules && (value[num + 1] == '{' || value[num + 1] == '}')) || (!useOdbcRules && (value[num + 1] == '"' || value[num + 1] == '\''))))
						{
							stringBuilder.Append(value, i, num - i);
							stringBuilder.Append(value[num + 1]);
							i = num + 2;
						}
						else if (value[num] == value[num + 1])
						{
							stringBuilder.Append(value, i, num - i + 1);
							i = num + 2;
						}
						else
						{
							stringBuilder.Append(value, i, num - i + 1);
							i = num + 1;
						}
					}
					else
					{
						stringBuilder.Append(value, i, num - i + 1);
						i = num + 1;
					}
				}
				value = stringBuilder.ToString();
			}
			return value;
		}

		// Token: 0x040004B5 RID: 1205
		internal const string SensitiveInfoRemovalMarker = "********";

		// Token: 0x040004B6 RID: 1206
		private const string ConnectionString_ExtPropsKey = "Extended Properties";

		// Token: 0x040004B7 RID: 1207
		private const string ConnectionString_ProviderKey = "Provider";

		// Token: 0x040004B8 RID: 1208
		private static readonly string[] ConnectionString_RestrictedElementKeys = new string[] { "Password", "PWD" };

		// Token: 0x040004B9 RID: 1209
		private static readonly string[] ConnectionString_ProvidersWithExtPropsToCurate = new string[] { "MSDASQL" };

		// Token: 0x040004BA RID: 1210
		private const string Credential_ConnectionString = "connectionstring";

		// Token: 0x040004BB RID: 1211
		private static readonly HashSet<string> AllowListedCredentialProperties = new HashSet<string>(new string[] { "AuthenticationKind", "IdentitySource", "path", "kind", "Username", "EncryptConnection", "Expires", "PrivacySetting", "ProviderType" }, StringComparer.InvariantCultureIgnoreCase);
	}
}
