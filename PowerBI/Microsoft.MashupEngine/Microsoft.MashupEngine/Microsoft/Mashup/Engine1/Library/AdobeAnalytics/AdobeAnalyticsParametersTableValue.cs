using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F74 RID: 3956
	internal static class AdobeAnalyticsParametersTableValue
	{
		// Token: 0x06006840 RID: 26688 RVA: 0x00166594 File Offset: 0x00164794
		public static TableValue New(CubeValue cubeValue, AdobeAnalyticsCube adobeCube)
		{
			IValueReference[] array = new IValueReference[3];
			TypeValue[] array2 = new TypeValue[]
			{
				AdobeAnalyticsParametersTableValue.NewDateTypeValue(),
				AdobeAnalyticsParametersTableValue.NewDateTypeValue()
			};
			array[0] = AdobeAnalyticsParametersTableValue.CreateParameterRecord(cubeValue, "DateRange", "Date Range", Strings.AdobeDateRangeParameterDescription, array2, AdobeAnalyticsParametersTableValue.dateIntervalParameterNames, 2);
			TypeValue[] array3 = new TypeValue[] { ListTypeValue.New(AdobeAnalyticsParametersTableValue.NewSegmentTypeValue(() => ListValue.New(adobeCube.Segments.Select((AdobeAnalyticsSegment segment) => TextValue.New(segment.Id).NewMeta(RecordValue.New(AdobeAnalyticsParametersTableValue.captionMetadataKeys, new Value[] { TextValue.New(segment.Name) }))).ToArray<Value>()))) };
			array[1] = AdobeAnalyticsParametersTableValue.CreateParameterRecord(cubeValue, "Segment", "Segment", Strings.AdobeSegmentParameterDescription, array3, AdobeAnalyticsParametersTableValue.segmentParameterNames, 1);
			TypeValue[] array4 = new TypeValue[]
			{
				AdobeAnalyticsParametersTableValue.NewTopTypeValue(),
				AdobeAnalyticsParametersTableValue.NewDimensionTypeValue(() => ListValue.New(AdobeAnalyticsParametersTableValue.GetDimensionSampleValues(adobeCube).ToArray<Value>()))
			};
			array[2] = AdobeAnalyticsParametersTableValue.CreateParameterRecord(cubeValue, "Top", "Top", Strings.AdobeTopParameterDescription, array4, AdobeAnalyticsParametersTableValue.topParameterNames, 1);
			return ListValue.New(array).ToTable(AdobeAnalyticsParametersTableValue.tableType);
		}

		// Token: 0x06006841 RID: 26689 RVA: 0x0016668D File Offset: 0x0016488D
		private static IEnumerable<Value> GetDimensionSampleValues(AdobeAnalyticsCube adobeCube)
		{
			yield return TextValue.Empty.NewMeta(RecordValue.New(AdobeAnalyticsParametersTableValue.captionMetadataKeys, new Value[] { TextValue.New(Strings.AdobeTopDimensionParameterAllDimensions) }));
			foreach (AdobeAnalyticsDimension adobeAnalyticsDimension in adobeCube.Dimensions)
			{
				yield return TextValue.New(adobeAnalyticsDimension.Id).NewMeta(RecordValue.New(AdobeAnalyticsParametersTableValue.captionMetadataKeys, new Value[] { TextValue.New(adobeAnalyticsDimension.Name) }));
			}
			IEnumerator<AdobeAnalyticsDimension> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06006842 RID: 26690 RVA: 0x001666A0 File Offset: 0x001648A0
		private static RecordValue CreateParameterRecord(CubeValue cube, string id, string name, string description, TypeValue[] types, string[] parameterNames, int minRequired)
		{
			IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(id);
			ParameterValue parameterValue = new ParameterValue(cube, identifierCubeExpression, minRequired, parameterNames, types);
			return RecordValue.New(AdobeAnalyticsParametersTableValue.columns, new Value[]
			{
				TextValue.New(id),
				TextValue.NewOrNull(name),
				LogicalValue.New(true),
				parameterValue,
				TextValue.NewOrNull(description)
			});
		}

		// Token: 0x06006843 RID: 26691 RVA: 0x001666FC File Offset: 0x001648FC
		private static TypeValue NewDateTypeValue()
		{
			TypeValue any = TypeValue.Any;
			RecordValue recordValue = RecordValue.New(AdobeAnalyticsParametersTableValue.defaultValueMetadataKeys, new Value[] { DateValue.New(DateTime.Now) });
			return any.NewMeta(recordValue).AsType;
		}

		// Token: 0x06006844 RID: 26692 RVA: 0x0016673C File Offset: 0x0016493C
		private static TypeValue NewTopTypeValue()
		{
			TypeValue number = TypeValue.Number;
			RecordValue asRecord = RecordValue.New(AdobeAnalyticsParametersTableValue.defaultValueMetadataKeys, new Value[] { Value.Null }).Concatenate(RecordValue.New(AdobeAnalyticsParametersTableValue.allowedValuesMetaType, (int index) => ListValue.New(new Value[]
			{
				NumberValue.New(10),
				NumberValue.New(20),
				NumberValue.New(50),
				NumberValue.New(100)
			}))).Concatenate(NavigationTableServices.NewAllowedValuesIsOpenSetMetadata(true))
				.AsRecord;
			return number.NewMeta(asRecord).AsType;
		}

		// Token: 0x06006845 RID: 26693 RVA: 0x001667B4 File Offset: 0x001649B4
		private static TypeValue NewDimensionTypeValue(Func<Value> getDimensions)
		{
			TypeValue nullable = TypeValue.Text.Nullable;
			RecordValue asRecord = RecordValue.New(AdobeAnalyticsParametersTableValue.defaultValueMetadataKeys, new Value[] { Value.Null }).Concatenate(RecordValue.New(AdobeAnalyticsParametersTableValue.allowedValuesMetaType, (int index) => getDimensions())).AsRecord;
			return nullable.NewMeta(asRecord).AsType;
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x00166820 File Offset: 0x00164A20
		private static TypeValue NewSegmentTypeValue(Func<Value> getAllowedValues = null)
		{
			TypeValue nullable = TypeValue.Text.Nullable;
			RecordValue recordValue = RecordValue.New(AdobeAnalyticsParametersTableValue.defaultValueMetadataKeys, new Value[] { TextValue.Empty });
			if (getAllowedValues != null)
			{
				recordValue = recordValue.Concatenate(RecordValue.New(AdobeAnalyticsParametersTableValue.allowedValuesMetaType, (int index) => getAllowedValues())).AsRecord;
			}
			return nullable.NewMeta(recordValue).AsType;
		}

		// Token: 0x0400395E RID: 14686
		public const string DateRangeId = "DateRange";

		// Token: 0x0400395F RID: 14687
		public const string SegmentId = "Segment";

		// Token: 0x04003960 RID: 14688
		public const string TopId = "Top";

		// Token: 0x04003961 RID: 14689
		private const string DateRangeName = "Date Range";

		// Token: 0x04003962 RID: 14690
		private const string SegmentName = "Segment";

		// Token: 0x04003963 RID: 14691
		private const string TopName = "Top";

		// Token: 0x04003964 RID: 14692
		private static readonly Keys defaultValueMetadataKeys = Keys.New("Documentation.DefaultValue");

		// Token: 0x04003965 RID: 14693
		private static readonly Keys allowedValuesMetadataKeys = Keys.New("Documentation.AllowedValues");

		// Token: 0x04003966 RID: 14694
		private static readonly Keys captionMetadataKeys = Keys.New("Documentation.Caption");

		// Token: 0x04003967 RID: 14695
		private static readonly TypeValue delayedNullableListType = PreviewServices.ConvertToDelayedValue(TypeValue.List.Nullable, "Value");

		// Token: 0x04003968 RID: 14696
		private static readonly RecordTypeValue allowedValuesMetaType = RecordTypeValue.New(RecordValue.New(new NamedValue[]
		{
			new NamedValue("Documentation.AllowedValues", RecordTypeAlgebra.NewField(AdobeAnalyticsParametersTableValue.delayedNullableListType, false))
		}), false);

		// Token: 0x04003969 RID: 14697
		private static readonly string[] dateIntervalParameterNames = new string[] { "Start", "End" };

		// Token: 0x0400396A RID: 14698
		private static readonly string[] topParameterNames = new string[] { "Top", "Dimension" };

		// Token: 0x0400396B RID: 14699
		private static readonly string[] segmentParameterNames = new string[] { "Segment" };

		// Token: 0x0400396C RID: 14700
		private static readonly Keys columns = Keys.New(new string[]
		{
			"Id",
			"Name",
			CubeParametersTableValue.IsOptional.AsString,
			"Data",
			"Description"
		});

		// Token: 0x0400396D RID: 14701
		private static readonly TableTypeValue tableType = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(AdobeAnalyticsParametersTableValue.columns, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Logical, false),
			RecordTypeAlgebra.NewField(TypeValue.Function, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false)
		})), new TableKey[]
		{
			new TableKey(new int[1], true)
		});
	}
}
