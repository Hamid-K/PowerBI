using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000564 RID: 1380
	internal abstract class ExpressionParser
	{
		// Token: 0x06004DB6 RID: 19894 RVA: 0x0013EB04 File Offset: 0x0013CD04
		internal ExpressionParser(ErrorContext errorContext)
		{
			this.m_errorContext = errorContext;
		}

		// Token: 0x06004DB7 RID: 19895
		internal abstract CodeDomProvider GetCodeCompiler();

		// Token: 0x06004DB8 RID: 19896
		internal abstract string GetCompilerArguments();

		// Token: 0x06004DB9 RID: 19897
		internal abstract Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, ExpressionParser.EvaluationMode evaluationMode);

		// Token: 0x06004DBA RID: 19898
		internal abstract Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, ExpressionParser.EvaluationMode evaluationMode, out bool userCollectionReferenced);

		// Token: 0x06004DBB RID: 19899
		internal abstract void ConvertField2ComplexExpr(ref Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expression);

		// Token: 0x06004DBC RID: 19900
		internal abstract Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo CreateScopedFirstAggregate(string fieldName, string dataSetName);

		// Token: 0x06004DBD RID: 19901 RVA: 0x0013EB13 File Offset: 0x0013CD13
		internal void ResetValueReferencedFlag()
		{
			this.m_valueReferenced = false;
		}

		// Token: 0x06004DBE RID: 19902
		internal abstract void ResetPageSectionRefersFlags();

		// Token: 0x06004DBF RID: 19903 RVA: 0x0013EB1C File Offset: 0x0013CD1C
		internal static void ParseRDLConstant(string expression, Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo expressionInfo, DataType constantType, ErrorContext errorContext, ObjectType objectType, string objectName, string propertyName)
		{
			expressionInfo.Type = Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant;
			expressionInfo.ConstantType = constantType;
			if (constantType <= DataType.Integer)
			{
				if (constantType == DataType.Boolean)
				{
					bool flag;
					try
					{
						if (string.Compare(expression, "0", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(expression, "1", StringComparison.OrdinalIgnoreCase) == 0)
						{
							errorContext.Register(ProcessingErrorCode.rsInvalidBooleanConstant, Severity.Warning, objectType, objectName, propertyName, new string[] { expression });
						}
						flag = XmlConvert.ToBoolean(expression);
					}
					catch (Exception ex)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex))
						{
							throw;
						}
						flag = false;
						errorContext.Register(ProcessingErrorCode.rsInvalidBooleanConstant, Severity.Error, objectType, objectName, propertyName, new string[] { expression });
					}
					expressionInfo.BoolValue = flag;
					return;
				}
				if (constantType == DataType.Integer)
				{
					int num;
					try
					{
						num = XmlConvert.ToInt32(expression);
					}
					catch (Exception ex2)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex2))
						{
							throw;
						}
						num = 0;
						errorContext.Register(ProcessingErrorCode.rsInvalidIntegerConstant, Severity.Error, objectType, objectName, propertyName, new string[] { expression });
					}
					expressionInfo.IntValue = num;
					return;
				}
			}
			else
			{
				if (constantType == DataType.Float)
				{
					double num2;
					try
					{
						num2 = XmlConvert.ToDouble(expression);
					}
					catch (Exception ex3)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex3))
						{
							throw;
						}
						num2 = 0.0;
						errorContext.Register(ProcessingErrorCode.rsInvalidFloatConstant, Severity.Error, objectType, objectName, propertyName, new string[] { expression });
					}
					expressionInfo.FloatValue = num2;
					return;
				}
				if (constantType != DataType.DateTime)
				{
					if (constantType == DataType.String)
					{
						expressionInfo.StringValue = expression;
						return;
					}
				}
				else
				{
					DateTimeOffset dateTimeOffset;
					bool flag2;
					if (!Microsoft.ReportingServices.Common.DateTimeUtil.TryParseDateTime(expression, CultureInfo.InvariantCulture, out dateTimeOffset, out flag2))
					{
						errorContext.Register(ProcessingErrorCode.rsInvalidDateTimeConstant, Severity.Error, objectType, objectName, propertyName, new string[] { expression });
						return;
					}
					if (flag2)
					{
						expressionInfo.SetDateTimeValue(dateTimeOffset);
						return;
					}
					expressionInfo.SetDateTimeValue(dateTimeOffset.DateTime);
					return;
				}
			}
			Global.Tracer.Assert(false);
			throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
		}

		// Token: 0x17001E03 RID: 7683
		// (get) Token: 0x06004DC0 RID: 19904
		internal abstract bool BodyRefersToReportItems { get; }

		// Token: 0x17001E04 RID: 7684
		// (get) Token: 0x06004DC1 RID: 19905
		internal abstract bool PageSectionRefersToReportItems { get; }

		// Token: 0x17001E05 RID: 7685
		// (get) Token: 0x06004DC2 RID: 19906
		internal abstract bool PageSectionRefersToOverallTotalPages { get; }

		// Token: 0x17001E06 RID: 7686
		// (get) Token: 0x06004DC3 RID: 19907
		internal abstract bool PageSectionRefersToTotalPages { get; }

		// Token: 0x17001E07 RID: 7687
		// (get) Token: 0x06004DC4 RID: 19908
		internal abstract int NumberOfAggregates { get; }

		// Token: 0x17001E08 RID: 7688
		// (get) Token: 0x06004DC5 RID: 19909
		internal abstract int LastID { get; }

		// Token: 0x17001E09 RID: 7689
		// (get) Token: 0x06004DC6 RID: 19910
		internal abstract int LastLookupID { get; }

		// Token: 0x17001E0A RID: 7690
		// (get) Token: 0x06004DC7 RID: 19911
		internal abstract bool PreviousAggregateUsed { get; }

		// Token: 0x17001E0B RID: 7691
		// (get) Token: 0x06004DC8 RID: 19912
		internal abstract bool AggregateOfAggregatesUsed { get; }

		// Token: 0x17001E0C RID: 7692
		// (get) Token: 0x06004DC9 RID: 19913
		internal abstract bool AggregateOfAggregatesUsedInUserSort { get; }

		// Token: 0x17001E0D RID: 7693
		// (get) Token: 0x06004DCA RID: 19914 RVA: 0x0013ECDC File Offset: 0x0013CEDC
		internal bool ValueReferenced
		{
			get
			{
				return this.m_valueReferenced;
			}
		}

		// Token: 0x17001E0E RID: 7694
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x0013ECE4 File Offset: 0x0013CEE4
		internal bool ValueReferencedGlobal
		{
			get
			{
				return this.m_valueReferencedGlobal;
			}
		}

		// Token: 0x06004DCC RID: 19916 RVA: 0x0013ECEC File Offset: 0x0013CEEC
		protected static ExpressionParser.Restrictions ExpressionType2Restrictions(ExpressionParser.ExpressionType expressionType)
		{
			switch (expressionType)
			{
			case ExpressionParser.ExpressionType.General:
				return ExpressionParser.Restrictions.None;
			case ExpressionParser.ExpressionType.ReportParameter:
				return ExpressionParser.Restrictions.ReportParameter;
			case ExpressionParser.ExpressionType.ReportLanguage:
				return ExpressionParser.Restrictions.ReportParameter;
			case ExpressionParser.ExpressionType.QueryParameter:
				return ExpressionParser.Restrictions.ReportParameter;
			case ExpressionParser.ExpressionType.GroupExpression:
				return ExpressionParser.Restrictions.GroupExpression;
			case ExpressionParser.ExpressionType.SortExpression:
			case ExpressionParser.ExpressionType.UserSortExpression:
				return ExpressionParser.Restrictions.SortExpression;
			case ExpressionParser.ExpressionType.DataRegionSortExpression:
				return ExpressionParser.Restrictions.DataRowSortExpression;
			case ExpressionParser.ExpressionType.DataSetFilters:
				return ExpressionParser.Restrictions.DataSetFilters;
			case ExpressionParser.ExpressionType.DataRegionFilters:
				return ExpressionParser.Restrictions.DataRegionFilters;
			case ExpressionParser.ExpressionType.GroupingFilters:
				return ExpressionParser.Restrictions.GroupingFilters;
			case ExpressionParser.ExpressionType.FieldValue:
				return ExpressionParser.Restrictions.FieldValue;
			case ExpressionParser.ExpressionType.VariableValue:
				return ExpressionParser.Restrictions.VariableValue;
			case ExpressionParser.ExpressionType.SubReportParameter:
				return ExpressionParser.Restrictions.SubReportParameter;
			case ExpressionParser.ExpressionType.GroupVariableValue:
				return ExpressionParser.Restrictions.GroupVariableValue;
			case ExpressionParser.ExpressionType.JoinExpression:
				return ExpressionParser.Restrictions.GroupExpression;
			case ExpressionParser.ExpressionType.ScopeValue:
				return ExpressionParser.Restrictions.SortExpression;
			case ExpressionParser.ExpressionType.Calculation:
				return ExpressionParser.Restrictions.None;
			default:
				Global.Tracer.Assert(false);
				return ExpressionParser.Restrictions.None;
			}
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x0013EDB3 File Offset: 0x0013CFB3
		protected void SetValueReferenced()
		{
			this.m_valueReferenced = true;
			this.m_valueReferencedGlobal = true;
		}

		// Token: 0x0400286F RID: 10351
		protected ErrorContext m_errorContext;

		// Token: 0x04002870 RID: 10352
		private bool m_valueReferenced;

		// Token: 0x04002871 RID: 10353
		private bool m_valueReferencedGlobal;

		// Token: 0x04002872 RID: 10354
		internal const int UnrestrictedMaxExprLength = -1;

		// Token: 0x020009A7 RID: 2471
		internal enum ExpressionType
		{
			// Token: 0x040044E3 RID: 17635
			General,
			// Token: 0x040044E4 RID: 17636
			ReportParameter,
			// Token: 0x040044E5 RID: 17637
			ReportLanguage,
			// Token: 0x040044E6 RID: 17638
			QueryParameter,
			// Token: 0x040044E7 RID: 17639
			GroupExpression,
			// Token: 0x040044E8 RID: 17640
			SortExpression,
			// Token: 0x040044E9 RID: 17641
			DataRegionSortExpression,
			// Token: 0x040044EA RID: 17642
			DataSetFilters,
			// Token: 0x040044EB RID: 17643
			DataRegionFilters,
			// Token: 0x040044EC RID: 17644
			GroupingFilters,
			// Token: 0x040044ED RID: 17645
			FieldValue,
			// Token: 0x040044EE RID: 17646
			VariableValue,
			// Token: 0x040044EF RID: 17647
			SubReportParameter,
			// Token: 0x040044F0 RID: 17648
			GroupVariableValue,
			// Token: 0x040044F1 RID: 17649
			UserSortExpression,
			// Token: 0x040044F2 RID: 17650
			JoinExpression,
			// Token: 0x040044F3 RID: 17651
			ScopeValue,
			// Token: 0x040044F4 RID: 17652
			Calculation
		}

		// Token: 0x020009A8 RID: 2472
		internal enum RecursiveFlags
		{
			// Token: 0x040044F6 RID: 17654
			Simple,
			// Token: 0x040044F7 RID: 17655
			Recursive
		}

		// Token: 0x020009A9 RID: 2473
		internal enum EvaluationMode
		{
			// Token: 0x040044F9 RID: 17657
			Auto,
			// Token: 0x040044FA RID: 17658
			Constant
		}

		// Token: 0x020009AA RID: 2474
		internal struct ExpressionContext
		{
			// Token: 0x06008139 RID: 33081 RVA: 0x00214094 File Offset: 0x00212294
			internal ExpressionContext(ExpressionParser.ExpressionType expressionType, DataType constantType, Microsoft.ReportingServices.ReportPublishing.LocationFlags location, ObjectType objectType, string objectName, string propertyName, string dataSetName, int maxExpressionLength, PublishingContextBase publishingContext)
			{
				this.m_expressionType = expressionType;
				this.m_constantType = constantType;
				this.m_location = location;
				this.m_objectType = objectType;
				this.m_objectName = objectName;
				this.m_propertyName = propertyName;
				this.m_dataSetName = dataSetName;
				this.m_inPrevious = false;
				this.m_inLookup = false;
				this.m_maxExpressionLength = maxExpressionLength;
				this.m_outerAggregate = null;
				this.m_publishingContext = publishingContext;
			}

			// Token: 0x170029B6 RID: 10678
			// (get) Token: 0x0600813A RID: 33082 RVA: 0x002140FB File Offset: 0x002122FB
			// (set) Token: 0x0600813B RID: 33083 RVA: 0x00214103 File Offset: 0x00212303
			internal bool InPrevious
			{
				get
				{
					return this.m_inPrevious;
				}
				set
				{
					this.m_inPrevious = value;
				}
			}

			// Token: 0x170029B7 RID: 10679
			// (get) Token: 0x0600813C RID: 33084 RVA: 0x0021410C File Offset: 0x0021230C
			// (set) Token: 0x0600813D RID: 33085 RVA: 0x00214114 File Offset: 0x00212314
			internal bool InLookup
			{
				get
				{
					return this.m_inLookup;
				}
				set
				{
					this.m_inLookup = value;
				}
			}

			// Token: 0x170029B8 RID: 10680
			// (get) Token: 0x0600813E RID: 33086 RVA: 0x0021411D File Offset: 0x0021231D
			internal ExpressionParser.ExpressionType ExpressionType
			{
				get
				{
					return this.m_expressionType;
				}
			}

			// Token: 0x170029B9 RID: 10681
			// (get) Token: 0x0600813F RID: 33087 RVA: 0x00214125 File Offset: 0x00212325
			internal DataType ConstantType
			{
				get
				{
					return this.m_constantType;
				}
			}

			// Token: 0x170029BA RID: 10682
			// (get) Token: 0x06008140 RID: 33088 RVA: 0x0021412D File Offset: 0x0021232D
			internal Microsoft.ReportingServices.ReportPublishing.LocationFlags Location
			{
				get
				{
					return this.m_location;
				}
			}

			// Token: 0x170029BB RID: 10683
			// (get) Token: 0x06008141 RID: 33089 RVA: 0x00214135 File Offset: 0x00212335
			internal ObjectType ObjectType
			{
				get
				{
					return this.m_objectType;
				}
			}

			// Token: 0x170029BC RID: 10684
			// (get) Token: 0x06008142 RID: 33090 RVA: 0x0021413D File Offset: 0x0021233D
			internal string ObjectName
			{
				get
				{
					return this.m_objectName;
				}
			}

			// Token: 0x170029BD RID: 10685
			// (get) Token: 0x06008143 RID: 33091 RVA: 0x00214145 File Offset: 0x00212345
			internal string PropertyName
			{
				get
				{
					return this.m_propertyName;
				}
			}

			// Token: 0x170029BE RID: 10686
			// (get) Token: 0x06008144 RID: 33092 RVA: 0x0021414D File Offset: 0x0021234D
			internal string DataSetName
			{
				get
				{
					return this.m_dataSetName;
				}
			}

			// Token: 0x170029BF RID: 10687
			// (get) Token: 0x06008145 RID: 33093 RVA: 0x00214155 File Offset: 0x00212355
			internal int MaxExpressionLength
			{
				get
				{
					return this.m_maxExpressionLength;
				}
			}

			// Token: 0x170029C0 RID: 10688
			// (get) Token: 0x06008146 RID: 33094 RVA: 0x0021415D File Offset: 0x0021235D
			// (set) Token: 0x06008147 RID: 33095 RVA: 0x00214165 File Offset: 0x00212365
			internal Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo OuterAggregate
			{
				get
				{
					return this.m_outerAggregate;
				}
				set
				{
					this.m_outerAggregate = value;
				}
			}

			// Token: 0x170029C1 RID: 10689
			// (get) Token: 0x06008148 RID: 33096 RVA: 0x0021416E File Offset: 0x0021236E
			internal bool InAggregate
			{
				get
				{
					return this.m_outerAggregate != null;
				}
			}

			// Token: 0x170029C2 RID: 10690
			// (get) Token: 0x06008149 RID: 33097 RVA: 0x00214179 File Offset: 0x00212379
			internal PublishingVersioning PublishingVersioning
			{
				get
				{
					return this.m_publishingContext.PublishingVersioning;
				}
			}

			// Token: 0x040044FB RID: 17659
			private ExpressionParser.ExpressionType m_expressionType;

			// Token: 0x040044FC RID: 17660
			private DataType m_constantType;

			// Token: 0x040044FD RID: 17661
			private Microsoft.ReportingServices.ReportPublishing.LocationFlags m_location;

			// Token: 0x040044FE RID: 17662
			private ObjectType m_objectType;

			// Token: 0x040044FF RID: 17663
			private string m_objectName;

			// Token: 0x04004500 RID: 17664
			private string m_propertyName;

			// Token: 0x04004501 RID: 17665
			private string m_dataSetName;

			// Token: 0x04004502 RID: 17666
			private bool m_inPrevious;

			// Token: 0x04004503 RID: 17667
			private bool m_inLookup;

			// Token: 0x04004504 RID: 17668
			private int m_maxExpressionLength;

			// Token: 0x04004505 RID: 17669
			private Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo m_outerAggregate;

			// Token: 0x04004506 RID: 17670
			private readonly PublishingContextBase m_publishingContext;
		}

		// Token: 0x020009AB RID: 2475
		[Flags]
		protected enum GrammarFlags
		{
			// Token: 0x04004508 RID: 17672
			DenyAggregates = 1,
			// Token: 0x04004509 RID: 17673
			DenyRunningValue = 2,
			// Token: 0x0400450A RID: 17674
			DenyRowNumber = 4,
			// Token: 0x0400450B RID: 17675
			DenyFields = 8,
			// Token: 0x0400450C RID: 17676
			DenyReportItems = 16,
			// Token: 0x0400450D RID: 17677
			DenyOverallPageGlobals = 32,
			// Token: 0x0400450E RID: 17678
			DenyPostSortAggregate = 64,
			// Token: 0x0400450F RID: 17679
			DenyPrevious = 128,
			// Token: 0x04004510 RID: 17680
			DenyDataSets = 256,
			// Token: 0x04004511 RID: 17681
			DenyDataSources = 512,
			// Token: 0x04004512 RID: 17682
			DenyVariables = 1024,
			// Token: 0x04004513 RID: 17683
			DenyMeDotValue = 2048,
			// Token: 0x04004514 RID: 17684
			DenyLookups = 4096,
			// Token: 0x04004515 RID: 17685
			DenyAggregatesOfAggregates = 8192,
			// Token: 0x04004516 RID: 17686
			DenyPageGlobals = 16384,
			// Token: 0x04004517 RID: 17687
			DenyRenderFormatAll = 32768,
			// Token: 0x04004518 RID: 17688
			DenyScopes = 65536
		}

		// Token: 0x020009AC RID: 2476
		[Flags]
		protected enum Restrictions
		{
			// Token: 0x0400451A RID: 17690
			None = 0,
			// Token: 0x0400451B RID: 17691
			InPageSection = 65670,
			// Token: 0x0400451C RID: 17692
			InBody = 16416,
			// Token: 0x0400451D RID: 17693
			AggregateParameterInPageSection = 66759,
			// Token: 0x0400451E RID: 17694
			AggregateParameterInBody = 66774,
			// Token: 0x0400451F RID: 17695
			PreviousAggregateParameterInBody = 66708,
			// Token: 0x04004520 RID: 17696
			ReportParameter = 104351,
			// Token: 0x04004521 RID: 17697
			ReportLanguage = 104351,
			// Token: 0x04004522 RID: 17698
			QueryParameter = 104351,
			// Token: 0x04004523 RID: 17699
			GroupExpression = 99475,
			// Token: 0x04004524 RID: 17700
			SortExpression = 98518,
			// Token: 0x04004525 RID: 17701
			DataRowSortExpression = 99543,
			// Token: 0x04004526 RID: 17702
			DataSetFilters = 103575,
			// Token: 0x04004527 RID: 17703
			DataRegionFilters = 99479,
			// Token: 0x04004528 RID: 17704
			GroupingFilters = 106710,
			// Token: 0x04004529 RID: 17705
			FieldValue = 119991,
			// Token: 0x0400452A RID: 17706
			VariableValue = 114934,
			// Token: 0x0400452B RID: 17707
			GroupVariableValue = 123094,
			// Token: 0x0400452C RID: 17708
			LookupSourceExpression = 37888,
			// Token: 0x0400452D RID: 17709
			SubReportParameter = 98304,
			// Token: 0x0400452E RID: 17710
			JoinExpression = 99475,
			// Token: 0x0400452F RID: 17711
			ScopeValue = 98518
		}
	}
}
