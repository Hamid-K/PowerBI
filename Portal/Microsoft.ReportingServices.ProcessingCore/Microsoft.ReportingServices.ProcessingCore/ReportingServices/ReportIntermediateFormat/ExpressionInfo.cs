using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004C5 RID: 1221
	[Serializable]
	public sealed class ExpressionInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IStaticReferenceable
	{
		// Token: 0x06003DAD RID: 15789 RVA: 0x00107CC7 File Offset: 0x00105EC7
		internal ExpressionInfo()
		{
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x00107CF0 File Offset: 0x00105EF0
		internal static ExpressionInfo CreateConstExpression(string value)
		{
			return new ExpressionInfo
			{
				Type = ExpressionInfo.Types.Constant,
				ConstantType = DataType.String,
				OriginalText = value,
				StringValue = value
			};
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x00107D14 File Offset: 0x00105F14
		internal static ExpressionInfo CreateConstExpression(bool value)
		{
			return new ExpressionInfo
			{
				Type = ExpressionInfo.Types.Constant,
				ConstantType = DataType.Boolean,
				OriginalText = value.ToString(CultureInfo.InvariantCulture),
				BoolValue = value
			};
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x00107D42 File Offset: 0x00105F42
		internal static ExpressionInfo CreateConstExpression(int value)
		{
			return new ExpressionInfo
			{
				Type = ExpressionInfo.Types.Constant,
				ConstantType = DataType.Integer,
				OriginalText = value.ToString(CultureInfo.InvariantCulture),
				IntValue = value
			};
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x00107D71 File Offset: 0x00105F71
		internal static ExpressionInfo CreateEmptyExpression()
		{
			return new ExpressionInfo
			{
				Type = ExpressionInfo.Types.Expression,
				OriginalText = null,
				StringValue = null
			};
		}

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x06003DB2 RID: 15794 RVA: 0x00107D8D File Offset: 0x00105F8D
		internal bool IsExpression
		{
			get
			{
				return this.m_type != ExpressionInfo.Types.Constant;
			}
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x00107D9B File Offset: 0x00105F9B
		// (set) Token: 0x06003DB4 RID: 15796 RVA: 0x00107DA3 File Offset: 0x00105FA3
		internal DataType ConstantType
		{
			get
			{
				return this.m_constantType;
			}
			set
			{
				this.m_constantType = value;
			}
		}

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x06003DB5 RID: 15797 RVA: 0x00107DAC File Offset: 0x00105FAC
		internal TypeCode ConstantTypeCode
		{
			get
			{
				if (!this.IsExpression)
				{
					DataType constantType = this.m_constantType;
					if (constantType <= DataType.Integer)
					{
						if (constantType == DataType.Boolean)
						{
							return TypeCode.Boolean;
						}
						if (constantType == DataType.Integer)
						{
							return TypeCode.Int32;
						}
					}
					else
					{
						if (constantType == DataType.Float)
						{
							return TypeCode.Double;
						}
						if (constantType == DataType.DateTime)
						{
							return TypeCode.DateTime;
						}
						if (constantType != DataType.String)
						{
						}
					}
					return TypeCode.String;
				}
				return TypeCode.Object;
			}
		}

		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x06003DB6 RID: 15798 RVA: 0x00107DF8 File Offset: 0x00105FF8
		// (set) Token: 0x06003DB7 RID: 15799 RVA: 0x00107E00 File Offset: 0x00106000
		internal ExpressionInfo.Types Type
		{
			get
			{
				return this.m_type;
			}
			set
			{
				this.m_type = value;
			}
		}

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x06003DB8 RID: 15800 RVA: 0x00107E09 File Offset: 0x00106009
		// (set) Token: 0x06003DB9 RID: 15801 RVA: 0x00107E11 File Offset: 0x00106011
		internal string StringValue
		{
			get
			{
				return this.m_stringValue;
			}
			set
			{
				this.m_stringValue = value;
			}
		}

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x00107E1C File Offset: 0x0010601C
		internal object Value
		{
			get
			{
				if (!this.IsExpression)
				{
					DataType constantType = this.m_constantType;
					if (constantType <= DataType.Integer)
					{
						if (constantType == DataType.Boolean)
						{
							return this.m_boolValue;
						}
						if (constantType == DataType.Integer)
						{
							return this.m_intValue;
						}
					}
					else
					{
						if (constantType == DataType.Float)
						{
							return this.m_floatValue;
						}
						if (constantType == DataType.DateTime)
						{
							return this.GetDateTimeValue();
						}
						if (constantType == DataType.String)
						{
							return this.m_stringValue;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x06003DBB RID: 15803 RVA: 0x00107E8C File Offset: 0x0010608C
		// (set) Token: 0x06003DBC RID: 15804 RVA: 0x00107E94 File Offset: 0x00106094
		internal bool BoolValue
		{
			get
			{
				return this.m_boolValue;
			}
			set
			{
				this.m_boolValue = value;
			}
		}

		// Token: 0x17001A4F RID: 6735
		// (get) Token: 0x06003DBD RID: 15805 RVA: 0x00107E9D File Offset: 0x0010609D
		// (set) Token: 0x06003DBE RID: 15806 RVA: 0x00107EA5 File Offset: 0x001060A5
		internal int IntValue
		{
			get
			{
				return this.m_intValue;
			}
			set
			{
				this.m_intValue = value;
			}
		}

		// Token: 0x17001A50 RID: 6736
		// (get) Token: 0x06003DBF RID: 15807 RVA: 0x00107EAE File Offset: 0x001060AE
		// (set) Token: 0x06003DC0 RID: 15808 RVA: 0x00107EB6 File Offset: 0x001060B6
		internal double FloatValue
		{
			get
			{
				return this.m_floatValue;
			}
			set
			{
				this.m_floatValue = value;
			}
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x00107EBF File Offset: 0x001060BF
		internal object GetDateTimeValue()
		{
			if (this.m_dateTimeOffsetValue != null)
			{
				return this.m_dateTimeOffsetValue;
			}
			return this.m_dateTimeValue;
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x00107EE5 File Offset: 0x001060E5
		internal void SetDateTimeValue(DateTime dateTime)
		{
			this.m_dateTimeValue = dateTime;
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x00107EEE File Offset: 0x001060EE
		internal void SetDateTimeValue(DateTimeOffset dateTimeOffset)
		{
			this.m_dateTimeOffsetValue = new DateTimeOffset?(dateTimeOffset);
		}

		// Token: 0x17001A51 RID: 6737
		// (get) Token: 0x06003DC4 RID: 15812 RVA: 0x00107EFC File Offset: 0x001060FC
		// (set) Token: 0x06003DC5 RID: 15813 RVA: 0x00107F04 File Offset: 0x00106104
		internal string OriginalText
		{
			get
			{
				return this.m_originalText;
			}
			set
			{
				this.m_originalText = value;
			}
		}

		// Token: 0x17001A52 RID: 6738
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x00107F0D File Offset: 0x0010610D
		// (set) Token: 0x06003DC7 RID: 15815 RVA: 0x00107F15 File Offset: 0x00106115
		internal string TransformedExpression
		{
			get
			{
				return this.m_transformedExpression;
			}
			set
			{
				this.m_transformedExpression = value;
			}
		}

		// Token: 0x17001A53 RID: 6739
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x00107F1E File Offset: 0x0010611E
		// (set) Token: 0x06003DC9 RID: 15817 RVA: 0x00107F26 File Offset: 0x00106126
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17001A54 RID: 6740
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x00107F2F File Offset: 0x0010612F
		// (set) Token: 0x06003DCB RID: 15819 RVA: 0x00107F37 File Offset: 0x00106137
		internal int CompileTimeID
		{
			get
			{
				return this.m_compileTimeID;
			}
			set
			{
				this.m_compileTimeID = value;
			}
		}

		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x06003DCC RID: 15820 RVA: 0x00107F40 File Offset: 0x00106140
		internal List<DataAggregateInfo> Aggregates
		{
			get
			{
				return this.m_aggregates;
			}
		}

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x06003DCD RID: 15821 RVA: 0x00107F48 File Offset: 0x00106148
		internal List<RunningValueInfo> RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
		}

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x06003DCE RID: 15822 RVA: 0x00107F50 File Offset: 0x00106150
		internal List<LookupInfo> Lookups
		{
			get
			{
				return this.m_lookups;
			}
		}

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x06003DCF RID: 15823 RVA: 0x00107F58 File Offset: 0x00106158
		// (set) Token: 0x06003DD0 RID: 15824 RVA: 0x00107F60 File Offset: 0x00106160
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

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x06003DD1 RID: 15825 RVA: 0x00107F69 File Offset: 0x00106169
		internal Hashtable ReferencedFieldProperties
		{
			get
			{
				return this.m_referencedFieldProperties;
			}
		}

		// Token: 0x17001A5A RID: 6746
		// (get) Token: 0x06003DD2 RID: 15826 RVA: 0x00107F71 File Offset: 0x00106171
		internal List<string> ReferencedReportItems
		{
			get
			{
				return this.m_referencedReportItems;
			}
		}

		// Token: 0x17001A5B RID: 6747
		// (get) Token: 0x06003DD3 RID: 15827 RVA: 0x00107F79 File Offset: 0x00106179
		// (set) Token: 0x06003DD4 RID: 15828 RVA: 0x00107F81 File Offset: 0x00106181
		internal bool DynamicFieldReferences
		{
			get
			{
				return this.m_dynamicFieldReferences;
			}
			set
			{
				this.m_dynamicFieldReferences = value;
			}
		}

		// Token: 0x17001A5C RID: 6748
		// (get) Token: 0x06003DD5 RID: 15829 RVA: 0x00107F8A File Offset: 0x0010618A
		// (set) Token: 0x06003DD6 RID: 15830 RVA: 0x00107F92 File Offset: 0x00106192
		internal bool ReferencedOverallPageGlobals
		{
			get
			{
				return this.m_referencedOverallPageGlobals;
			}
			set
			{
				this.m_referencedOverallPageGlobals = value;
			}
		}

		// Token: 0x17001A5D RID: 6749
		// (get) Token: 0x06003DD7 RID: 15831 RVA: 0x00107F9B File Offset: 0x0010619B
		// (set) Token: 0x06003DD8 RID: 15832 RVA: 0x00107FA3 File Offset: 0x001061A3
		internal bool ReferencedPageGlobals
		{
			get
			{
				return this.m_referencedPageGlobals;
			}
			set
			{
				this.m_referencedPageGlobals = value;
			}
		}

		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x06003DD9 RID: 15833 RVA: 0x00107FAC File Offset: 0x001061AC
		internal bool MeDotValueDetected
		{
			get
			{
				return (this.m_meDotValuePositionsInOriginalText != null && this.m_meDotValuePositionsInOriginalText.Count > 0) || (this.m_meDotValuePositionsInTranformedExpr != null && this.m_meDotValuePositionsInTranformedExpr.Count > 0);
			}
		}

		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x06003DDA RID: 15834 RVA: 0x00107FDE File Offset: 0x001061DE
		// (set) Token: 0x06003DDB RID: 15835 RVA: 0x00107FE6 File Offset: 0x001061E6
		internal bool NullLevelDetected
		{
			get
			{
				return this.m_nullLevelInExpr;
			}
			set
			{
				this.m_nullLevelInExpr = value;
			}
		}

		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x06003DDC RID: 15836 RVA: 0x00107FEF File Offset: 0x001061EF
		internal bool HasDirectFieldReferences
		{
			get
			{
				return this.m_referencedFields != null && this.m_referencedFields.Count > 0;
			}
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x06003DDD RID: 15837 RVA: 0x0010800C File Offset: 0x0010620C
		// (set) Token: 0x06003DDE RID: 15838 RVA: 0x0010807C File Offset: 0x0010627C
		internal bool HasAnyFieldReferences
		{
			get
			{
				if (this.m_hasAnyFieldReferences)
				{
					return true;
				}
				if (this.m_rdlFunctionInfo != null)
				{
					using (List<ExpressionInfo>.Enumerator enumerator = this.m_rdlFunctionInfo.Expressions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.HasAnyFieldReferences)
							{
								return true;
							}
						}
					}
					return false;
				}
				return false;
			}
			set
			{
				this.m_hasAnyFieldReferences = value;
			}
		}

		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x06003DDF RID: 15839 RVA: 0x00108085 File Offset: 0x00106285
		// (set) Token: 0x06003DE0 RID: 15840 RVA: 0x0010808D File Offset: 0x0010628D
		internal string SimpleParameterName
		{
			get
			{
				return this.m_simpleParameterName;
			}
			set
			{
				this.m_simpleParameterName = value;
			}
		}

		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x06003DE1 RID: 15841 RVA: 0x00108096 File Offset: 0x00106296
		// (set) Token: 0x06003DE2 RID: 15842 RVA: 0x0010809E File Offset: 0x0010629E
		internal bool HasDynamicParameterReference
		{
			get
			{
				return this.m_hasDynamicParameterReference;
			}
			set
			{
				this.m_hasDynamicParameterReference = value;
			}
		}

		// Token: 0x17001A64 RID: 6756
		// (get) Token: 0x06003DE3 RID: 15843 RVA: 0x001080A7 File Offset: 0x001062A7
		internal List<string> ReferencedParameters
		{
			get
			{
				return this.m_referencedParameters;
			}
		}

		// Token: 0x17001A65 RID: 6757
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x001080AF File Offset: 0x001062AF
		internal int FieldIndex
		{
			get
			{
				return this.IntValue;
			}
		}

		// Token: 0x17001A66 RID: 6758
		// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x001080B7 File Offset: 0x001062B7
		// (set) Token: 0x06003DE6 RID: 15846 RVA: 0x001080BF File Offset: 0x001062BF
		internal RdlFunctionInfo RdlFunctionInfo
		{
			get
			{
				return this.m_rdlFunctionInfo;
			}
			set
			{
				this.m_rdlFunctionInfo = value;
			}
		}

		// Token: 0x17001A67 RID: 6759
		// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x001080C8 File Offset: 0x001062C8
		// (set) Token: 0x06003DE8 RID: 15848 RVA: 0x001080D0 File Offset: 0x001062D0
		internal ScopedFieldInfo ScopedFieldInfo
		{
			get
			{
				return this.m_scopedFieldInfo;
			}
			set
			{
				this.m_scopedFieldInfo = value;
			}
		}

		// Token: 0x17001A68 RID: 6760
		// (get) Token: 0x06003DE9 RID: 15849 RVA: 0x001080D9 File Offset: 0x001062D9
		// (set) Token: 0x06003DEA RID: 15850 RVA: 0x001080E1 File Offset: 0x001062E1
		internal LiteralInfo LiteralInfo
		{
			get
			{
				return this.m_literalInfo;
			}
			set
			{
				this.m_literalInfo = value;
			}
		}

		// Token: 0x17001A69 RID: 6761
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x001080EA File Offset: 0x001062EA
		// (set) Token: 0x06003DEC RID: 15852 RVA: 0x001080F2 File Offset: 0x001062F2
		internal ExpressionStructureInfo ExpressionStructureInfo
		{
			get
			{
				return this.m_expressionStructureInformation;
			}
			set
			{
				this.m_expressionStructureInformation = value;
			}
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x001080FB File Offset: 0x001062FB
		internal void Initialize(string propertyName, InitializationContext context)
		{
			this.Initialize(propertyName, context, true);
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x00108108 File Offset: 0x00106308
		internal void Initialize(string propertyName, InitializationContext context, bool initializeDataOnError)
		{
			context.EnforceRdlSandboxContentRestrictions(this, propertyName);
			context.CheckVariableReferences(this.m_referencedVariables, propertyName);
			context.CheckFieldReferences(this.m_referencedFields, propertyName);
			context.CheckReportItemReferences(this.m_referencedReportItems, propertyName);
			context.CheckReportParameterReferences(this.m_referencedParameters, propertyName);
			context.CheckDataSetReference(this.m_referencedDataSets, propertyName);
			context.CheckDataSourceReference(this.m_referencedDataSources, propertyName);
			context.CheckScopeReferences(this.m_referencedScopes, propertyName);
			if (this.Type == ExpressionInfo.Types.ScopedFieldReference)
			{
				this.ScopedFieldInfo.FieldIndex = context.ResolveScopedFieldReferenceToIndex(this.StringValue, this.ScopedFieldInfo.FieldName);
			}
			if (!context.ErrorContext.HasError || initializeDataOnError)
			{
				context.FillInFieldIndex(this);
				context.TransferAggregates(this.m_aggregates, propertyName);
				context.TransferRunningValues(this.m_runningValues, propertyName);
				context.TransferLookups(this.m_lookups, propertyName);
				context.FillInTokenIndex(this);
			}
			this.m_referencedFieldProperties = null;
			if (this.m_nullLevelInExpr && context.InRecursiveHierarchyRows && context.InRecursiveHierarchyColumns)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsLevelCallRecursiveHierarchyBothDimensions, Severity.Warning, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
			}
			if (this.m_rdlFunctionInfo != null)
			{
				this.m_rdlFunctionInfo.Initialize(propertyName, context, initializeDataOnError);
			}
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x00108258 File Offset: 0x00106458
		internal bool InitializeAxisExpression(string propertyName, InitializationContext context)
		{
			bool hasError = context.ErrorContext.HasError;
			context.ErrorContext.HasError = false;
			this.Initialize(propertyName, context, false);
			int hasError2 = (context.ErrorContext.HasError ? 1 : 0);
			context.ErrorContext.HasError = hasError;
			return hasError2 == 0;
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x001082A4 File Offset: 0x001064A4
		internal void AggregateInitialize(string dataSetName, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, InitializationContext context)
		{
			this.SpecialFunctionArgInitialize(dataSetName, objectType, objectName, propertyName, context, false);
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x001082B4 File Offset: 0x001064B4
		private void SpecialFunctionArgInitialize(string dataSetName, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, InitializationContext context, bool isLookup)
		{
			context.EnforceRdlSandboxContentRestrictions(this, objectType, objectName, propertyName);
			context.AggregateCheckVariableReferences(this.m_referencedVariables, objectType, objectName, propertyName);
			context.AggregateCheckFieldReferences(this.m_referencedFields, dataSetName, objectType, objectName, propertyName);
			context.AggregateCheckReportItemReferences(this.m_referencedReportItems, objectType, objectName, propertyName);
			context.AggregateCheckDataSetReference(this.m_referencedDataSets, objectType, objectName, propertyName);
			context.AggregateCheckDataSourceReference(this.m_referencedDataSources, objectType, objectName, propertyName);
			context.FillInFieldIndex(this, dataSetName);
			if (!isLookup)
			{
				context.ExprHostBuilder.AggregateParamExprAdd(this);
			}
			if (this.m_inPrevious || isLookup)
			{
				context.TransferAggregates(this.m_aggregates, propertyName);
				context.TransferRunningValues(this.m_runningValues, propertyName);
			}
			else
			{
				context.TransferNestedAggregates(this.m_aggregates, propertyName);
			}
			if (!isLookup)
			{
				context.TransferLookups(this.m_lookups, objectType, objectName, propertyName, dataSetName);
			}
			if (this.m_rdlFunctionInfo != null)
			{
				this.m_rdlFunctionInfo.Initialize(propertyName, context, false);
			}
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x001083A5 File Offset: 0x001065A5
		internal void LookupInitialize(string dataSetName, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, InitializationContext context)
		{
			this.SpecialFunctionArgInitialize(dataSetName, objectType, objectName, propertyName, context, true);
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x001083B8 File Offset: 0x001065B8
		internal bool HasRecursiveAggregates()
		{
			if (this.m_aggregates != null)
			{
				for (int i = 0; i < this.m_aggregates.Count; i++)
				{
					if (this.m_aggregates[i].Recursive)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x001083FC File Offset: 0x001065FC
		internal void GroupExpressionInitialize(InitializationContext context)
		{
			context.CheckVariableReferences(this.m_referencedVariables, "Group");
			context.CheckFieldReferences(this.m_referencedFields, "Group");
			context.CheckReportItemReferences(this.m_referencedReportItems, "Group");
			context.CheckReportParameterReferences(this.m_referencedParameters, "Group");
			context.CheckDataSetReference(this.m_referencedDataSets, "Group");
			context.CheckDataSourceReference(this.m_referencedDataSources, "Group");
			context.FillInFieldIndex(this);
			context.TransferGroupExpressionRowNumbers(this.m_runningValues);
			context.TransferLookups(this.m_lookups, "Group");
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x0010849C File Offset: 0x0010669C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Type, Token.Enum),
				new MemberInfo(MemberName.StringValue, Token.String),
				new MemberInfo(MemberName.BoolValue, Token.Boolean),
				new MemberInfo(MemberName.IntValue, Token.Int32),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.OriginalValue, Token.String),
				new MemberInfo(MemberName.InPrevious, Token.Boolean),
				new MemberInfo(MemberName.DateTimeValue, Token.DateTime),
				new MemberInfo(MemberName.FloatValue, Token.Double),
				new MemberInfo(MemberName.ConstantType, Token.Enum),
				new MemberInfo(MemberName.DateTimeOffsetValue, Token.Object),
				new MemberInfo(MemberName.RdlFunctionInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RdlFunctionInfo),
				new MemberInfo(MemberName.ScopedFieldInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopedFieldInfo, Lifetime.AddedIn(200)),
				new MemberInfo(MemberName.TransformedExpression, Token.String)
			});
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x001085E0 File Offset: 0x001067E0
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(ExpressionInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.InPrevious)
				{
					if (memberName <= MemberName.OriginalValue)
					{
						switch (memberName)
						{
						case MemberName.StringValue:
							writer.Write(this.m_stringValue);
							continue;
						case MemberName.BoolValue:
							writer.Write(this.m_boolValue);
							continue;
						case MemberName.IntValue:
							writer.Write(this.m_intValue);
							continue;
						case MemberName.Hidden:
						case MemberName.Toggle:
							break;
						case MemberName.Type:
							writer.WriteEnum((int)this.m_type);
							continue;
						default:
							if (memberName == MemberName.OriginalValue)
							{
								writer.Write(this.m_originalText);
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
						if (memberName == MemberName.InPrevious)
						{
							writer.Write(this.m_inPrevious);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.DateTimeOffsetValue)
				{
					switch (memberName)
					{
					case MemberName.FloatValue:
						writer.Write(this.m_floatValue);
						continue;
					case MemberName.DateTimeValue:
						writer.Write(this.m_dateTimeValue);
						continue;
					case MemberName.ConstantType:
						writer.WriteEnum((int)this.m_constantType);
						continue;
					default:
						if (memberName == MemberName.DateTimeOffsetValue)
						{
							writer.Write((this.m_dateTimeOffsetValue != null) ? this.m_dateTimeOffsetValue : null);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.RdlFunctionInfo)
					{
						writer.Write(this.m_rdlFunctionInfo);
						continue;
					}
					if (memberName == MemberName.ScopedFieldInfo)
					{
						writer.Write(this.m_scopedFieldInfo);
						continue;
					}
					if (memberName == MemberName.TransformedExpression)
					{
						if (this.ExpressionWasTransformed())
						{
							writer.Write(this.m_transformedExpression);
							continue;
						}
						writer.Write(null);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x001087E8 File Offset: 0x001069E8
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(ExpressionInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.InPrevious)
				{
					if (memberName <= MemberName.OriginalValue)
					{
						switch (memberName)
						{
						case MemberName.StringValue:
							this.m_stringValue = reader.ReadString();
							continue;
						case MemberName.BoolValue:
							this.m_boolValue = reader.ReadBoolean();
							continue;
						case MemberName.IntValue:
							this.m_intValue = reader.ReadInt32();
							continue;
						case MemberName.Hidden:
						case MemberName.Toggle:
							break;
						case MemberName.Type:
							this.m_type = (ExpressionInfo.Types)reader.ReadEnum();
							continue;
						default:
							if (memberName == MemberName.OriginalValue)
							{
								this.m_originalText = reader.ReadString();
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.InPrevious)
						{
							this.m_inPrevious = reader.ReadBoolean();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.DateTimeOffsetValue)
				{
					switch (memberName)
					{
					case MemberName.FloatValue:
						this.m_floatValue = reader.ReadDouble();
						continue;
					case MemberName.DateTimeValue:
						this.m_dateTimeValue = reader.ReadDateTime();
						continue;
					case MemberName.ConstantType:
						this.m_constantType = (DataType)reader.ReadEnum();
						continue;
					default:
						if (memberName == MemberName.DateTimeOffsetValue)
						{
							object obj = reader.ReadVariant();
							if (obj != null)
							{
								this.m_dateTimeOffsetValue = new DateTimeOffset?((DateTimeOffset)obj);
								continue;
							}
							this.m_dateTimeOffsetValue = null;
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.RdlFunctionInfo)
					{
						this.m_rdlFunctionInfo = reader.ReadRIFObject<RdlFunctionInfo>();
						continue;
					}
					if (memberName == MemberName.ScopedFieldInfo)
					{
						this.m_scopedFieldInfo = (ScopedFieldInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.TransformedExpression)
					{
						this.m_transformedExpression = reader.ReadString();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x001089E8 File Offset: 0x00106BE8
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x001089F5 File Offset: 0x00106BF5
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo;
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x001089FC File Offset: 0x00106BFC
		internal void SetAsSimpleFieldReference(string fieldName)
		{
			this.AddReferencedField(fieldName);
			this.HasAnyFieldReferences = true;
			this.Type = ExpressionInfo.Types.Field;
			this.StringValue = fieldName;
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x00108A1C File Offset: 0x00106C1C
		internal void SetAsScopedFieldReference(string scopeName, string fieldName)
		{
			this.AddReferencedScope(new ScopeReference(scopeName, fieldName));
			ScopedFieldInfo scopedFieldInfo = new ScopedFieldInfo();
			scopedFieldInfo.FieldName = fieldName;
			this.Type = ExpressionInfo.Types.ScopedFieldReference;
			this.StringValue = scopeName;
			this.ScopedFieldInfo = scopedFieldInfo;
		}

		// Token: 0x06003DFC RID: 15868 RVA: 0x00108A58 File Offset: 0x00106C58
		internal void SetAsLiteral(LiteralInfo literal)
		{
			this.Type = ExpressionInfo.Types.Literal;
			this.LiteralInfo = literal;
		}

		// Token: 0x06003DFD RID: 15869 RVA: 0x00108A69 File Offset: 0x00106C69
		internal void SetAsRdlFunction(RdlFunctionInfo function)
		{
			this.Type = ExpressionInfo.Types.RdlFunction;
			this.RdlFunctionInfo = function;
		}

		// Token: 0x06003DFE RID: 15870 RVA: 0x00108A79 File Offset: 0x00106C79
		internal void AddReferencedField(string fieldName)
		{
			if (this.m_referencedFields == null)
			{
				this.m_referencedFields = new List<string>();
			}
			this.m_referencedFields.Add(fieldName);
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x00108A9A File Offset: 0x00106C9A
		internal void AddReferencedReportItemInOriginalText(string reportItemName, int index)
		{
			if (this.m_referencedReportItemPositionsInOriginalText == null)
			{
				this.m_referencedReportItemPositionsInOriginalText = new List<int>();
			}
			this.m_referencedReportItemPositionsInOriginalText.Add(index);
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x00108ABB File Offset: 0x00106CBB
		internal void AddReferencedReportItemInTransformedExpression(string reportItemName, int index)
		{
			if (this.m_referencedReportItems == null)
			{
				this.m_referencedReportItems = new List<string>();
			}
			if (this.m_referencedReportItemPositionsInTransformedExpression == null)
			{
				this.m_referencedReportItemPositionsInTransformedExpression = new List<int>();
			}
			this.m_referencedReportItemPositionsInTransformedExpression.Add(index);
			this.m_referencedReportItems.Add(reportItemName);
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x00108AFB File Offset: 0x00106CFB
		internal void AddMeDotValueInOriginalText(int index)
		{
			if (this.m_meDotValuePositionsInOriginalText == null)
			{
				this.m_meDotValuePositionsInOriginalText = new List<int>();
			}
			this.m_meDotValuePositionsInOriginalText.Add(index);
		}

		// Token: 0x06003E02 RID: 15874 RVA: 0x00108B1C File Offset: 0x00106D1C
		internal void AddMeDotValueInTransformedExpression(int index)
		{
			if (this.m_meDotValuePositionsInTranformedExpr == null)
			{
				this.m_meDotValuePositionsInTranformedExpr = new List<int>();
			}
			this.m_meDotValuePositionsInTranformedExpr.Add(index);
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x00108B3D File Offset: 0x00106D3D
		internal void AddReferencedVariable(string variableName, int index)
		{
			if (this.m_referencedVariables == null)
			{
				this.m_referencedVariables = new List<string>();
			}
			if (this.m_referencedVariablePositions == null)
			{
				this.m_referencedVariablePositions = new List<int>();
			}
			this.m_referencedVariablePositions.Add(index);
			this.m_referencedVariables.Add(variableName);
		}

		// Token: 0x06003E04 RID: 15876 RVA: 0x00108B7D File Offset: 0x00106D7D
		internal void AddReferencedParameter(string parameterName)
		{
			if (this.m_referencedParameters == null)
			{
				this.m_referencedParameters = new List<string>();
			}
			this.m_referencedParameters.Add(parameterName);
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x00108B9E File Offset: 0x00106D9E
		internal void AddReferencedDataSet(string dataSetName)
		{
			if (this.m_referencedDataSets == null)
			{
				this.m_referencedDataSets = new List<string>();
			}
			this.m_referencedDataSets.Add(dataSetName);
		}

		// Token: 0x06003E06 RID: 15878 RVA: 0x00108BBF File Offset: 0x00106DBF
		internal void AddReferencedDataSource(string dataSourceName)
		{
			if (this.m_referencedDataSources == null)
			{
				this.m_referencedDataSources = new List<string>();
			}
			this.m_referencedDataSources.Add(dataSourceName);
		}

		// Token: 0x06003E07 RID: 15879 RVA: 0x00108BE0 File Offset: 0x00106DE0
		internal void AddReferencedScope(ScopeReference scopeReference)
		{
			if (this.m_referencedScopes == null)
			{
				this.m_referencedScopes = new List<ScopeReference>();
			}
			this.m_referencedScopes.Add(scopeReference);
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x00108C01 File Offset: 0x00106E01
		internal void AddAggregate(DataAggregateInfo aggregate)
		{
			if (this.m_aggregates == null)
			{
				this.m_aggregates = new List<DataAggregateInfo>();
			}
			this.m_aggregates.Add(aggregate);
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x00108C22 File Offset: 0x00106E22
		internal void AddRunningValue(RunningValueInfo runningValue)
		{
			if (this.m_runningValues == null)
			{
				this.m_runningValues = new List<RunningValueInfo>();
			}
			this.m_runningValues.Add(runningValue);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x00108C43 File Offset: 0x00106E43
		internal void AddLookup(LookupInfo lookup)
		{
			if (this.m_lookups == null)
			{
				this.m_lookups = new List<LookupInfo>();
			}
			this.m_lookups.Add(lookup);
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x00108C64 File Offset: 0x00106E64
		internal DataAggregateInfo GetSumAggregateWithoutScope()
		{
			if (ExpressionInfo.Types.Aggregate == this.m_type && this.m_aggregates != null)
			{
				Global.Tracer.Assert(1 == this.m_aggregates.Count);
				DataAggregateInfo dataAggregateInfo = this.m_aggregates[0];
				string text;
				if (DataAggregateInfo.AggregateTypes.Sum == dataAggregateInfo.AggregateType && !dataAggregateInfo.GetScope(out text))
				{
					return dataAggregateInfo;
				}
			}
			return null;
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x00108CC0 File Offset: 0x00106EC0
		internal void AddDynamicPropertyReference(string fieldName)
		{
			Global.Tracer.Assert(fieldName != null, "(null != fieldName)");
			if (this.m_referencedFieldProperties == null)
			{
				this.m_referencedFieldProperties = new Hashtable();
			}
			else if (this.m_referencedFieldProperties.ContainsKey(fieldName))
			{
				this.m_referencedFieldProperties.Remove(fieldName);
			}
			this.m_referencedFieldProperties.Add(fieldName, null);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x00108D1C File Offset: 0x00106F1C
		internal void AddStaticPropertyReference(string fieldName, string propertyName)
		{
			Global.Tracer.Assert(fieldName != null && propertyName != null, "(null != fieldName && null != propertyName)");
			if (this.m_referencedFieldProperties == null)
			{
				this.m_referencedFieldProperties = new Hashtable();
			}
			if (this.m_referencedFieldProperties.ContainsKey(fieldName))
			{
				Hashtable hashtable = this.m_referencedFieldProperties[fieldName] as Hashtable;
				if (hashtable != null)
				{
					hashtable[propertyName] = null;
					return;
				}
			}
			else
			{
				Hashtable hashtable2 = new Hashtable();
				hashtable2.Add(propertyName, null);
				this.m_referencedFieldProperties.Add(fieldName, hashtable2);
			}
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x00108D9C File Offset: 0x00106F9C
		internal void AddTransformedExpressionAggregateInfo(int position, string aggregateID, bool isRunningValue)
		{
			int num;
			ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType specialFunctionType;
			if (isRunningValue)
			{
				num = this.m_runningValues.Count - 1;
				specialFunctionType = ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType.RunningValue;
			}
			else
			{
				num = this.m_aggregates.Count - 1;
				specialFunctionType = ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType.Aggregate;
			}
			this.AddTransformedSpecialFunctionInfo(position, aggregateID, specialFunctionType, num);
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x00108DDA File Offset: 0x00106FDA
		private void AddTransformedSpecialFunctionInfo(int position, string specialFunctionID, ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType funcType, int index)
		{
			if (this.m_transformedExprAggregateInfos == null)
			{
				this.m_transformedExprAggregateInfos = new List<ExpressionInfo.TransformedExprSpecialFunctionInfo>();
			}
			this.m_transformedExprAggregateInfos.Add(new ExpressionInfo.TransformedExprSpecialFunctionInfo(position, specialFunctionID, funcType, index));
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x00108E04 File Offset: 0x00107004
		internal void AddTransformedExpressionLookupInfo(int position, string lookupID)
		{
			this.AddTransformedSpecialFunctionInfo(position, lookupID, ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType.Lookup, this.m_lookups.Count - 1);
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x00108E1C File Offset: 0x0010701C
		private int UpdateReferencedItemsCollection(ExpressionInfo meDotValueExpression, int referencedIndex, int meDotValuePositionInOriginalText, int meDotValuePositionInTransformedExpression, List<int> positionsInTransformedExpression, List<int> positionsInOriginalText, List<string> referencedValues, List<int> positionsInMeDotValueTransformedExpression, List<int> positionsInMeDotValueOriginalText, List<string> referencedMeDotValueValues)
		{
			int num = 8;
			for (int i = referencedIndex; i < positionsInTransformedExpression.Count; i++)
			{
				if (positionsInOriginalText != null && meDotValuePositionInOriginalText < positionsInOriginalText[i])
				{
					int num2 = i;
					positionsInOriginalText[num2] += meDotValueExpression.OriginalText.Length - num;
				}
				if (meDotValuePositionInTransformedExpression < positionsInTransformedExpression[i])
				{
					int num2 = i;
					positionsInTransformedExpression[num2] += meDotValueExpression.TransformedExpression.Length - num;
				}
				else
				{
					referencedIndex++;
				}
			}
			if (referencedMeDotValueValues != null)
			{
				for (int j = 0; j < referencedMeDotValueValues.Count; j++)
				{
					if (positionsInMeDotValueOriginalText != null)
					{
						int num3 = positionsInMeDotValueOriginalText[j];
						positionsInOriginalText.Insert(referencedIndex, meDotValuePositionInOriginalText + num3);
					}
					string text = referencedMeDotValueValues[j];
					int num4 = positionsInMeDotValueTransformedExpression[j];
					positionsInTransformedExpression.Insert(referencedIndex, meDotValuePositionInTransformedExpression + num4);
					referencedValues.Insert(referencedIndex, text);
					referencedIndex++;
				}
			}
			return referencedIndex;
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x00108F0C File Offset: 0x0010710C
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			ExpressionInfo expressionInfo = (ExpressionInfo)base.MemberwiseClone();
			if (this.RdlFunctionInfo != null)
			{
				expressionInfo.m_rdlFunctionInfo = (RdlFunctionInfo)this.m_rdlFunctionInfo.PublishClone(context);
				Global.Tracer.Assert(this.m_aggregates == null, "this.m_aggregates == null");
				Global.Tracer.Assert(this.m_runningValues == null, "this.m_aggregates == null");
				Global.Tracer.Assert(this.m_lookups == null, "this.m_aggregates == null");
				Global.Tracer.Assert(this.m_referencedReportItems == null, "this.m_aggregates == null");
				Global.Tracer.Assert(this.m_referencedReportItemPositionsInOriginalText == null, "this.m_aggregates == null");
				Global.Tracer.Assert(this.m_meDotValuePositionsInOriginalText == null, "this.m_aggregates == null");
				Global.Tracer.Assert(this.m_transformedExpression == null, "this.m_aggregates == null");
				return expressionInfo;
			}
			if (this.m_aggregates != null)
			{
				expressionInfo.m_aggregates = new List<DataAggregateInfo>(this.m_aggregates.Count);
				foreach (DataAggregateInfo dataAggregateInfo in this.m_aggregates)
				{
					expressionInfo.m_aggregates.Add((DataAggregateInfo)dataAggregateInfo.PublishClone(context));
				}
			}
			if (this.m_runningValues != null)
			{
				expressionInfo.m_runningValues = new List<RunningValueInfo>(this.m_runningValues.Count);
				foreach (RunningValueInfo runningValueInfo in this.m_runningValues)
				{
					expressionInfo.m_runningValues.Add((RunningValueInfo)runningValueInfo.PublishClone(context));
				}
			}
			if (this.m_lookups != null)
			{
				expressionInfo.m_lookups = new List<LookupInfo>(this.m_lookups.Count);
				foreach (LookupInfo lookupInfo in this.m_lookups)
				{
					expressionInfo.m_lookups.Add((LookupInfo)lookupInfo.PublishClone(context));
				}
			}
			if (this.m_referencedReportItems != null)
			{
				expressionInfo.m_referencedReportItems = new List<string>(this.m_referencedReportItems.Count);
				foreach (string text in this.m_referencedReportItems)
				{
					expressionInfo.m_referencedReportItems.Add((string)text.Clone());
				}
				context.AddExpressionThatReferencesReportItems(expressionInfo);
			}
			if (this.m_referencedReportItemPositionsInOriginalText != null)
			{
				expressionInfo.m_referencedReportItemPositionsInOriginalText = new List<int>(this.m_referencedReportItemPositionsInOriginalText.Count);
				foreach (int num in this.m_referencedReportItemPositionsInOriginalText)
				{
					expressionInfo.m_referencedReportItemPositionsInOriginalText.Add(num);
				}
			}
			if (this.m_meDotValuePositionsInOriginalText != null)
			{
				expressionInfo.m_meDotValuePositionsInOriginalText = new List<int>(this.m_meDotValuePositionsInOriginalText.Count);
				foreach (int num2 in this.m_meDotValuePositionsInOriginalText)
				{
					expressionInfo.m_meDotValuePositionsInOriginalText.Add(num2);
				}
			}
			if (this.m_transformedExpression != null)
			{
				StringBuilder stringBuilder = new StringBuilder(this.m_transformedExpression);
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
				bool flag = false;
				bool flag2 = false;
				if (this.m_transformedExprAggregateInfos != null)
				{
					expressionInfo.m_transformedExprAggregateInfos = new List<ExpressionInfo.TransformedExprSpecialFunctionInfo>(this.m_transformedExprAggregateInfos.Count);
					foreach (ExpressionInfo.TransformedExprSpecialFunctionInfo transformedExprSpecialFunctionInfo in this.m_transformedExprAggregateInfos)
					{
						expressionInfo.m_transformedExprAggregateInfos.Add((ExpressionInfo.TransformedExprSpecialFunctionInfo)transformedExprSpecialFunctionInfo.PublishClone(context));
					}
					flag = true;
					num5 += this.m_transformedExprAggregateInfos.Count;
				}
				if (this.m_referencedVariablePositions != null)
				{
					expressionInfo.m_referencedVariablePositions = new List<int>(this.m_referencedVariablePositions.Count);
					foreach (int num6 in this.m_referencedVariablePositions)
					{
						expressionInfo.m_referencedVariablePositions.Add(num6);
					}
					flag2 = true;
					num5 += this.m_referencedVariablePositions.Count;
				}
				if (this.m_meDotValuePositionsInTranformedExpr != null)
				{
					expressionInfo.m_meDotValuePositionsInTranformedExpr = new List<int>(this.m_meDotValuePositionsInTranformedExpr.Count);
					foreach (int num7 in this.m_meDotValuePositionsInTranformedExpr)
					{
						expressionInfo.m_meDotValuePositionsInTranformedExpr.Add(num7);
					}
				}
				if (this.m_referencedReportItemPositionsInTransformedExpression != null)
				{
					expressionInfo.m_referencedReportItemPositionsInTransformedExpression = new List<int>(this.m_referencedReportItemPositionsInTransformedExpression.Count);
					foreach (int num8 in this.m_referencedReportItemPositionsInTransformedExpression)
					{
						expressionInfo.m_referencedReportItemPositionsInTransformedExpression.Add(num8);
					}
				}
				int num9 = 11;
				int num10 = 8;
				int num11 = 0;
				int num12 = 0;
				int num13 = 0;
				int num14 = 0;
				int num15 = 0;
				int num16 = 0;
				int num17 = 0;
				int num18 = 0;
				int num19 = 0;
				int num20 = 0;
				StringBuilder stringBuilder2;
				if (flag || flag2)
				{
					stringBuilder2 = new StringBuilder();
					stringBuilder2.Append("=");
					for (int i = 0; i < num5; i++)
					{
						Global.Tracer.Assert(!flag || !flag2 || num19 >= this.m_transformedExprAggregateInfos.Count || num20 >= this.m_referencedVariablePositions.Count || this.m_transformedExprAggregateInfos[num19].Position != this.m_referencedVariablePositions[num20]);
						int num21 = int.MaxValue;
						int num22 = int.MaxValue;
						if (flag && num19 < this.m_transformedExprAggregateInfos.Count)
						{
							num21 = this.m_transformedExprAggregateInfos[num19].Position;
						}
						if (flag2 && num20 < this.m_referencedVariablePositions.Count)
						{
							num22 = this.m_referencedVariablePositions[num20];
						}
						if (num21 < num22)
						{
							num15 = this.m_transformedExprAggregateInfos[num19].Position;
							ExpressionInfo.TransformedExprSpecialFunctionInfo transformedExprSpecialFunctionInfo2 = this.m_transformedExprAggregateInfos[num19];
							string functionID = transformedExprSpecialFunctionInfo2.FunctionID;
							string text2;
							int num23;
							if (transformedExprSpecialFunctionInfo2.FunctionType == ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType.Lookup)
							{
								text2 = context.GetNewLookupID(functionID);
								num23 = num10;
							}
							else
							{
								text2 = context.GetNewAggregateID(functionID);
								num23 = num9;
							}
							ExpressionInfo.TransformedExprSpecialFunctionInfo transformedExprSpecialFunctionInfo3 = expressionInfo.m_transformedExprAggregateInfos[num19];
							transformedExprSpecialFunctionInfo3.FunctionID = text2;
							transformedExprSpecialFunctionInfo3.Position = num15 + num12;
							stringBuilder.Replace(functionID, text2, num15 + num23 + num12, functionID.Length);
							stringBuilder2.Append(this.m_transformedExpression.Substring(num14, num15 - num14));
							num16 = stringBuilder2.Length;
							int indexIntoCollection = transformedExprSpecialFunctionInfo2.IndexIntoCollection;
							string text3 = null;
							string text4 = null;
							switch (transformedExprSpecialFunctionInfo2.FunctionType)
							{
							case ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType.Aggregate:
								text3 = this.m_aggregates[indexIntoCollection].GetAsString();
								text4 = expressionInfo.m_aggregates[indexIntoCollection].GetAsString();
								break;
							case ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType.RunningValue:
								text3 = this.m_runningValues[indexIntoCollection].GetAsString();
								text4 = expressionInfo.m_runningValues[indexIntoCollection].GetAsString();
								break;
							case ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType.Lookup:
								text3 = this.m_lookups[indexIntoCollection].GetAsString();
								text4 = expressionInfo.m_lookups[indexIntoCollection].GetAsString();
								break;
							default:
								Global.Tracer.Assert(false, "Unknown transformed item function type: {0}", new object[] { transformedExprSpecialFunctionInfo2.FunctionType });
								break;
							}
							stringBuilder2.Append(text4);
							num14 = num15 + num23 + functionID.Length;
							Global.Tracer.Assert(text2.Length >= functionID.Length, "(newName.Length >= oldName.Length)");
							num11 = text2.Length - functionID.Length;
							num13 = text4.Length - text3.Length;
							num12 += num11;
							num19++;
						}
						else if (num22 != 2147483647)
						{
							num15 = this.m_referencedVariablePositions[num20];
							string text5 = this.m_referencedVariables[num20];
							string newVariableName = context.GetNewVariableName(text5);
							expressionInfo.m_referencedVariablePositions[num20] = num15 + num12;
							stringBuilder.Replace(text5, newVariableName, num15 + num12, text5.Length);
							stringBuilder2.Append(this.m_transformedExpression.Substring(num14, num15 - num14));
							num16 = stringBuilder2.Length;
							stringBuilder2.Append(newVariableName);
							num14 = num15 + text5.Length;
							Global.Tracer.Assert(newVariableName.Length >= text5.Length, "(newName.Length >= oldName.Length)");
							num11 = newVariableName.Length - text5.Length;
							num13 = num11;
							num12 += num11;
							num20++;
						}
						if (num11 != 0)
						{
							if (this.m_meDotValuePositionsInTranformedExpr != null)
							{
								for (int j = num18; j < this.m_meDotValuePositionsInTranformedExpr.Count; j++)
								{
									if (num16 > this.m_meDotValuePositionsInTranformedExpr[j])
									{
										num18++;
									}
									else
									{
										int num24 = this.m_meDotValuePositionsInTranformedExpr[j];
										expressionInfo.m_meDotValuePositionsInTranformedExpr[j] = num24 + num11;
									}
								}
							}
							if (this.m_referencedReportItemPositionsInTransformedExpression != null)
							{
								for (int k = num4; k < this.m_referencedReportItemPositionsInTransformedExpression.Count; k++)
								{
									if (num15 > this.m_referencedReportItemPositionsInTransformedExpression[k])
									{
										num4++;
									}
									else
									{
										int num25 = this.m_referencedReportItemPositionsInTransformedExpression[k];
										expressionInfo.m_referencedReportItemPositionsInTransformedExpression[k] = num25 + num11;
									}
								}
							}
						}
						if (num13 != 0)
						{
							if (this.m_meDotValuePositionsInOriginalText != null)
							{
								for (int l = num17; l < this.m_meDotValuePositionsInOriginalText.Count; l++)
								{
									if (num16 > this.m_meDotValuePositionsInOriginalText[l])
									{
										num17++;
									}
									else
									{
										int num26 = this.m_meDotValuePositionsInOriginalText[l];
										expressionInfo.m_meDotValuePositionsInOriginalText[l] = num26 + num13;
									}
								}
							}
							if (this.m_referencedReportItemPositionsInOriginalText != null)
							{
								for (int m = num3; m < this.m_referencedReportItemPositionsInOriginalText.Count; m++)
								{
									if (num16 > this.m_referencedReportItemPositionsInOriginalText[m])
									{
										num3++;
									}
									else
									{
										int num27 = this.m_referencedReportItemPositionsInOriginalText[m];
										expressionInfo.m_referencedReportItemPositionsInOriginalText[m] = num27 + num13;
									}
								}
							}
						}
					}
					stringBuilder2.Append(this.m_transformedExpression.Substring(num14));
					Global.Tracer.Assert(num19 + num20 == num5, "((currentAggIDIndex + currentVariableIndex) == potentialChangeCount)");
				}
				else
				{
					stringBuilder2 = new StringBuilder(this.m_originalText);
				}
				expressionInfo.m_originalText = stringBuilder2.ToString();
				expressionInfo.m_transformedExpression = stringBuilder.ToString();
			}
			else if (expressionInfo.m_aggregates != null && expressionInfo.m_aggregates.Count > 0)
			{
				expressionInfo.m_stringValue = expressionInfo.m_aggregates[0].Name;
				expressionInfo.m_originalText = expressionInfo.m_aggregates[0].GetAsString();
			}
			else if (expressionInfo.m_runningValues != null && expressionInfo.m_runningValues.Count > 0)
			{
				expressionInfo.m_stringValue = expressionInfo.m_runningValues[0].Name;
				expressionInfo.m_originalText = expressionInfo.m_runningValues[0].GetAsString();
			}
			else if (expressionInfo.m_lookups != null && expressionInfo.m_lookups.Count > 0)
			{
				expressionInfo.m_stringValue = expressionInfo.m_lookups[0].Name;
				expressionInfo.m_originalText = expressionInfo.m_lookups[0].GetAsString();
			}
			return expressionInfo;
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x00109B20 File Offset: 0x00107D20
		internal void UpdateReportItemReferences(AutomaticSubtotalContext context)
		{
			StringBuilder stringBuilder = new StringBuilder(this.m_transformedExpression);
			StringBuilder stringBuilder2 = new StringBuilder(this.m_originalText);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < this.m_referencedReportItemPositionsInTransformedExpression.Count; i++)
			{
				string text = this.m_referencedReportItems[i];
				string newReportItemName = context.GetNewReportItemName(text);
				if (newReportItemName != text)
				{
					this.m_referencedReportItems[i] = newReportItemName;
					if (this.m_transformedExpression != null)
					{
						num2 = this.m_referencedReportItemPositionsInTransformedExpression[i];
						stringBuilder.Replace(text, newReportItemName, num2 + num, text.Length);
						this.m_referencedReportItemPositionsInTransformedExpression[i] = num2 + num;
					}
					int num7 = this.m_referencedReportItemPositionsInOriginalText[i];
					stringBuilder2.Replace(text, newReportItemName, num7 + num, text.Length);
					this.m_referencedReportItemPositionsInOriginalText[i] = num7 + num;
					int num8 = newReportItemName.Length - text.Length;
					num += num8;
					if (num8 != 0)
					{
						if (this.m_transformedExpression != null && this.m_transformedExprAggregateInfos != null)
						{
							for (int j = num3; j < this.m_transformedExprAggregateInfos.Count; j++)
							{
								if (num2 > this.m_transformedExprAggregateInfos[j].Position)
								{
									num3++;
								}
								else
								{
									int position = this.m_transformedExprAggregateInfos[j].Position;
									this.m_transformedExprAggregateInfos[j].Position = position + num8;
								}
							}
						}
						if (this.m_referencedVariablePositions != null)
						{
							for (int k = num4; k < this.m_referencedVariablePositions.Count; k++)
							{
								if (num2 > this.m_referencedVariablePositions[k])
								{
									num4++;
								}
								else
								{
									int num9 = this.m_referencedVariablePositions[k];
									this.m_referencedVariablePositions[k] = num9 + num8;
								}
							}
						}
						if (this.m_meDotValuePositionsInOriginalText != null)
						{
							for (int l = num6; l < this.m_meDotValuePositionsInOriginalText.Count; l++)
							{
								if (num7 > this.m_meDotValuePositionsInOriginalText[l])
								{
									num6++;
								}
								else
								{
									int num10 = this.m_meDotValuePositionsInOriginalText[l];
									this.m_meDotValuePositionsInOriginalText[l] = num10 + num8;
								}
							}
							for (int m = num5; m < this.m_meDotValuePositionsInTranformedExpr.Count; m++)
							{
								if (num2 > this.m_meDotValuePositionsInTranformedExpr[m])
								{
									num5++;
								}
								else
								{
									int num11 = this.m_meDotValuePositionsInTranformedExpr[m];
									this.m_meDotValuePositionsInTranformedExpr[m] = num11 + num8;
								}
							}
						}
					}
				}
			}
			this.m_transformedExpression = stringBuilder.ToString();
			this.m_originalText = stringBuilder2.ToString();
		}

		// Token: 0x06003E14 RID: 15892 RVA: 0x00109DDC File Offset: 0x00107FDC
		internal bool ExpressionWasTransformed()
		{
			if (this.m_type == ExpressionInfo.Types.Expression && this.m_originalText != null && this.m_transformedExpression != null)
			{
				string text = this.m_originalText.Trim();
				if (text.StartsWith("="))
				{
					text = text.Substring(1);
				}
				return !text.Equals(this.m_transformedExpression.Trim());
			}
			return false;
		}

		// Token: 0x17001A6A RID: 6762
		// (get) Token: 0x06003E15 RID: 15893 RVA: 0x00109E38 File Offset: 0x00108038
		public int ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x00109E40 File Offset: 0x00108040
		public void SetID(int id)
		{
			this.m_id = id;
		}

		// Token: 0x04001CC9 RID: 7369
		private ExpressionInfo.Types m_type;

		// Token: 0x04001CCA RID: 7370
		private DataType m_constantType = DataType.String;

		// Token: 0x04001CCB RID: 7371
		private string m_stringValue;

		// Token: 0x04001CCC RID: 7372
		private bool m_boolValue;

		// Token: 0x04001CCD RID: 7373
		private int m_intValue;

		// Token: 0x04001CCE RID: 7374
		private DateTime m_dateTimeValue;

		// Token: 0x04001CCF RID: 7375
		private DateTimeOffset? m_dateTimeOffsetValue;

		// Token: 0x04001CD0 RID: 7376
		private double m_floatValue;

		// Token: 0x04001CD1 RID: 7377
		private int m_exprHostID = -1;

		// Token: 0x04001CD2 RID: 7378
		private string m_originalText;

		// Token: 0x04001CD3 RID: 7379
		private bool m_inPrevious;

		// Token: 0x04001CD4 RID: 7380
		private RdlFunctionInfo m_rdlFunctionInfo;

		// Token: 0x04001CD5 RID: 7381
		private ScopedFieldInfo m_scopedFieldInfo;

		// Token: 0x04001CD6 RID: 7382
		private string m_transformedExpression;

		// Token: 0x04001CD7 RID: 7383
		[NonSerialized]
		private LiteralInfo m_literalInfo;

		// Token: 0x04001CD8 RID: 7384
		[NonSerialized]
		private List<ExpressionInfo.TransformedExprSpecialFunctionInfo> m_transformedExprAggregateInfos;

		// Token: 0x04001CD9 RID: 7385
		[NonSerialized]
		private List<string> m_referencedFields;

		// Token: 0x04001CDA RID: 7386
		[NonSerialized]
		private List<string> m_referencedReportItems;

		// Token: 0x04001CDB RID: 7387
		[NonSerialized]
		private List<int> m_referencedReportItemPositionsInTransformedExpression;

		// Token: 0x04001CDC RID: 7388
		[NonSerialized]
		private List<int> m_referencedReportItemPositionsInOriginalText;

		// Token: 0x04001CDD RID: 7389
		[NonSerialized]
		private List<string> m_referencedVariables;

		// Token: 0x04001CDE RID: 7390
		[NonSerialized]
		private List<int> m_referencedVariablePositions;

		// Token: 0x04001CDF RID: 7391
		[NonSerialized]
		private List<string> m_referencedParameters;

		// Token: 0x04001CE0 RID: 7392
		[NonSerialized]
		private string m_simpleParameterName;

		// Token: 0x04001CE1 RID: 7393
		[NonSerialized]
		private bool m_hasDynamicParameterReference;

		// Token: 0x04001CE2 RID: 7394
		[NonSerialized]
		private List<string> m_referencedDataSets;

		// Token: 0x04001CE3 RID: 7395
		[NonSerialized]
		private List<string> m_referencedDataSources;

		// Token: 0x04001CE4 RID: 7396
		[NonSerialized]
		private List<ScopeReference> m_referencedScopes;

		// Token: 0x04001CE5 RID: 7397
		[NonSerialized]
		private List<DataAggregateInfo> m_aggregates;

		// Token: 0x04001CE6 RID: 7398
		[NonSerialized]
		private List<RunningValueInfo> m_runningValues;

		// Token: 0x04001CE7 RID: 7399
		[NonSerialized]
		private List<LookupInfo> m_lookups;

		// Token: 0x04001CE8 RID: 7400
		[NonSerialized]
		private int m_compileTimeID = -1;

		// Token: 0x04001CE9 RID: 7401
		[NonSerialized]
		private Hashtable m_referencedFieldProperties;

		// Token: 0x04001CEA RID: 7402
		[NonSerialized]
		private bool m_dynamicFieldReferences;

		// Token: 0x04001CEB RID: 7403
		[NonSerialized]
		private bool m_referencedOverallPageGlobals;

		// Token: 0x04001CEC RID: 7404
		[NonSerialized]
		private bool m_hasAnyFieldReferences;

		// Token: 0x04001CED RID: 7405
		[NonSerialized]
		private bool m_referencedPageGlobals;

		// Token: 0x04001CEE RID: 7406
		[NonSerialized]
		private List<int> m_meDotValuePositionsInOriginalText;

		// Token: 0x04001CEF RID: 7407
		[NonSerialized]
		private List<int> m_meDotValuePositionsInTranformedExpr;

		// Token: 0x04001CF0 RID: 7408
		[NonSerialized]
		private bool m_nullLevelInExpr;

		// Token: 0x04001CF1 RID: 7409
		[NonSerialized]
		private int m_id = int.MinValue;

		// Token: 0x04001CF2 RID: 7410
		[NonSerialized]
		private ExpressionStructureInfo m_expressionStructureInformation;

		// Token: 0x04001CF3 RID: 7411
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ExpressionInfo.GetDeclaration();

		// Token: 0x02000979 RID: 2425
		internal enum Types
		{
			// Token: 0x040040E1 RID: 16609
			Expression,
			// Token: 0x040040E2 RID: 16610
			Field,
			// Token: 0x040040E3 RID: 16611
			Aggregate,
			// Token: 0x040040E4 RID: 16612
			Constant,
			// Token: 0x040040E5 RID: 16613
			Token,
			// Token: 0x040040E6 RID: 16614
			Lookup_OneValue,
			// Token: 0x040040E7 RID: 16615
			Lookup_MultiValue,
			// Token: 0x040040E8 RID: 16616
			RdlFunction,
			// Token: 0x040040E9 RID: 16617
			ScopedFieldReference,
			// Token: 0x040040EA RID: 16618
			Literal
		}

		// Token: 0x0200097A RID: 2426
		private class TransformedExprSpecialFunctionInfo
		{
			// Token: 0x06008077 RID: 32887 RVA: 0x00211221 File Offset: 0x0020F421
			internal TransformedExprSpecialFunctionInfo(int position, string functionID, ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType functionType, int indexIntoCollection)
			{
				this.FunctionType = functionType;
				this.Position = position;
				this.FunctionID = functionID;
				this.IndexIntoCollection = indexIntoCollection;
			}

			// Token: 0x06008078 RID: 32888 RVA: 0x00211246 File Offset: 0x0020F446
			internal object PublishClone(AutomaticSubtotalContext context)
			{
				ExpressionInfo.TransformedExprSpecialFunctionInfo transformedExprSpecialFunctionInfo = (ExpressionInfo.TransformedExprSpecialFunctionInfo)base.MemberwiseClone();
				transformedExprSpecialFunctionInfo.FunctionID = (string)this.FunctionID.Clone();
				return transformedExprSpecialFunctionInfo;
			}

			// Token: 0x040040EB RID: 16619
			internal ExpressionInfo.TransformedExprSpecialFunctionInfo.SpecialFunctionType FunctionType;

			// Token: 0x040040EC RID: 16620
			internal int Position;

			// Token: 0x040040ED RID: 16621
			internal string FunctionID;

			// Token: 0x040040EE RID: 16622
			internal int IndexIntoCollection;

			// Token: 0x02000D37 RID: 3383
			internal enum SpecialFunctionType
			{
				// Token: 0x040050A5 RID: 20645
				Aggregate,
				// Token: 0x040050A6 RID: 20646
				RunningValue,
				// Token: 0x040050A7 RID: 20647
				Lookup
			}
		}
	}
}
