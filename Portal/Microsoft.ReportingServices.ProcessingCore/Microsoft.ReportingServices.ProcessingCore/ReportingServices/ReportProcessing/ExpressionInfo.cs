using System;
using System.Collections;
using System.Text;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200070B RID: 1803
	[Serializable]
	public class ExpressionInfo
	{
		// Token: 0x060064AB RID: 25771 RVA: 0x0018E1AA File Offset: 0x0018C3AA
		internal ExpressionInfo()
		{
		}

		// Token: 0x060064AC RID: 25772 RVA: 0x0018E1C0 File Offset: 0x0018C3C0
		internal ExpressionInfo(ExpressionInfo.Types type)
		{
			this.m_type = ExpressionInfo.Types.Expression;
		}

		// Token: 0x170023A7 RID: 9127
		// (get) Token: 0x060064AD RID: 25773 RVA: 0x0018E1DD File Offset: 0x0018C3DD
		// (set) Token: 0x060064AE RID: 25774 RVA: 0x0018E1E5 File Offset: 0x0018C3E5
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

		// Token: 0x170023A8 RID: 9128
		// (get) Token: 0x060064AF RID: 25775 RVA: 0x0018E1EE File Offset: 0x0018C3EE
		internal bool IsExpression
		{
			get
			{
				return this.m_type != ExpressionInfo.Types.Constant;
			}
		}

		// Token: 0x170023A9 RID: 9129
		// (get) Token: 0x060064B0 RID: 25776 RVA: 0x0018E1FC File Offset: 0x0018C3FC
		// (set) Token: 0x060064B1 RID: 25777 RVA: 0x0018E204 File Offset: 0x0018C404
		internal string Value
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

		// Token: 0x170023AA RID: 9130
		// (get) Token: 0x060064B2 RID: 25778 RVA: 0x0018E20D File Offset: 0x0018C40D
		// (set) Token: 0x060064B3 RID: 25779 RVA: 0x0018E215 File Offset: 0x0018C415
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

		// Token: 0x170023AB RID: 9131
		// (get) Token: 0x060064B4 RID: 25780 RVA: 0x0018E21E File Offset: 0x0018C41E
		// (set) Token: 0x060064B5 RID: 25781 RVA: 0x0018E226 File Offset: 0x0018C426
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

		// Token: 0x170023AC RID: 9132
		// (get) Token: 0x060064B6 RID: 25782 RVA: 0x0018E22F File Offset: 0x0018C42F
		// (set) Token: 0x060064B7 RID: 25783 RVA: 0x0018E237 File Offset: 0x0018C437
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

		// Token: 0x170023AD RID: 9133
		// (get) Token: 0x060064B8 RID: 25784 RVA: 0x0018E240 File Offset: 0x0018C440
		// (set) Token: 0x060064B9 RID: 25785 RVA: 0x0018E248 File Offset: 0x0018C448
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

		// Token: 0x170023AE RID: 9134
		// (get) Token: 0x060064BA RID: 25786 RVA: 0x0018E251 File Offset: 0x0018C451
		// (set) Token: 0x060064BB RID: 25787 RVA: 0x0018E259 File Offset: 0x0018C459
		internal IntList TransformedExpressionAggregatePositions
		{
			get
			{
				return this.m_transformedExpressionAggregatePositions;
			}
			set
			{
				this.m_transformedExpressionAggregatePositions = value;
			}
		}

		// Token: 0x170023AF RID: 9135
		// (get) Token: 0x060064BC RID: 25788 RVA: 0x0018E262 File Offset: 0x0018C462
		// (set) Token: 0x060064BD RID: 25789 RVA: 0x0018E26A File Offset: 0x0018C46A
		internal StringList TransformedExpressionAggregateIDs
		{
			get
			{
				return this.m_transformedExpressionAggregateIDs;
			}
			set
			{
				this.m_transformedExpressionAggregateIDs = value;
			}
		}

		// Token: 0x170023B0 RID: 9136
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x0018E273 File Offset: 0x0018C473
		// (set) Token: 0x060064BF RID: 25791 RVA: 0x0018E27B File Offset: 0x0018C47B
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

		// Token: 0x170023B1 RID: 9137
		// (get) Token: 0x060064C0 RID: 25792 RVA: 0x0018E284 File Offset: 0x0018C484
		// (set) Token: 0x060064C1 RID: 25793 RVA: 0x0018E28C File Offset: 0x0018C48C
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

		// Token: 0x170023B2 RID: 9138
		// (get) Token: 0x060064C2 RID: 25794 RVA: 0x0018E295 File Offset: 0x0018C495
		internal DataAggregateInfoList Aggregates
		{
			get
			{
				return this.m_aggregates;
			}
		}

		// Token: 0x170023B3 RID: 9139
		// (get) Token: 0x060064C3 RID: 25795 RVA: 0x0018E29D File Offset: 0x0018C49D
		internal RunningValueInfoList RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
		}

		// Token: 0x170023B4 RID: 9140
		// (get) Token: 0x060064C4 RID: 25796 RVA: 0x0018E2A5 File Offset: 0x0018C4A5
		internal Hashtable ReferencedFieldProperties
		{
			get
			{
				return this.m_referencedFieldProperties;
			}
		}

		// Token: 0x170023B5 RID: 9141
		// (get) Token: 0x060064C5 RID: 25797 RVA: 0x0018E2AD File Offset: 0x0018C4AD
		// (set) Token: 0x060064C6 RID: 25798 RVA: 0x0018E2B5 File Offset: 0x0018C4B5
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

		// Token: 0x060064C7 RID: 25799 RVA: 0x0018E2C0 File Offset: 0x0018C4C0
		internal void Initialize(string propertyName, InitializationContext context)
		{
			context.CheckFieldReferences(this.m_referencedFields, propertyName);
			context.CheckReportItemReferences(this.m_referencedReportItems, propertyName);
			context.CheckReportParameterReferences(this.m_referencedParameters, propertyName);
			context.CheckDataSetReference(this.m_referencedDataSets, propertyName);
			context.CheckDataSourceReference(this.m_referencedDataSources, propertyName);
			if ((LocationFlags.InMatrixCellTopLevelItem & context.Location) != (LocationFlags)0 && this.m_referencedFields != null)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsNonAggregateInMatrixCell, Severity.Warning, context.ObjectType, context.ObjectName, propertyName, Array.Empty<string>());
			}
			context.FillInFieldIndex(this);
			context.TransferAggregates(this.m_aggregates, propertyName);
			context.TransferRunningValues(this.m_runningValues, propertyName);
			context.MergeFieldPropertiesIntoDataset(this);
			context.FillInTokenIndex(this);
			this.m_referencedFieldProperties = null;
		}

		// Token: 0x060064C8 RID: 25800 RVA: 0x0018E38C File Offset: 0x0018C58C
		internal void AggregateInitialize(string dataSetName, ObjectType objectType, string objectName, string propertyName, InitializationContext context)
		{
			context.AggregateCheckFieldReferences(this.m_referencedFields, dataSetName, objectType, objectName, propertyName);
			context.AggregateCheckReportItemReferences(this.m_referencedReportItems, objectType, objectName, propertyName);
			context.AggregateCheckDataSetReference(this.m_referencedDataSets, objectType, objectName, propertyName);
			context.AggregateCheckDataSourceReference(this.m_referencedDataSources, objectType, objectName, propertyName);
			context.MergeFieldPropertiesIntoDataset(this);
			context.FillInFieldIndex(this, dataSetName);
			context.ExprHostBuilder.AggregateParamExprAdd(this);
		}

		// Token: 0x060064C9 RID: 25801 RVA: 0x0018E3FC File Offset: 0x0018C5FC
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

		// Token: 0x060064CA RID: 25802 RVA: 0x0018E440 File Offset: 0x0018C640
		internal void GroupExpressionInitialize(InitializationContext context)
		{
			context.CheckFieldReferences(this.m_referencedFields, "Group");
			context.CheckReportItemReferences(this.m_referencedReportItems, "Group");
			context.CheckReportParameterReferences(this.m_referencedParameters, "Group");
			context.CheckDataSetReference(this.m_referencedDataSets, "Group");
			context.CheckDataSourceReference(this.m_referencedDataSources, "Group");
			context.MergeFieldPropertiesIntoDataset(this);
			context.FillInFieldIndex(this);
			context.TransferGroupExpressionRowNumbers(this.m_runningValues);
		}

		// Token: 0x060064CB RID: 25803 RVA: 0x0018E4C4 File Offset: 0x0018C6C4
		internal ExpressionInfo DeepClone(InitializationContext context)
		{
			Global.Tracer.Assert(-1 == this.m_exprHostID);
			ExpressionInfo expressionInfo = new ExpressionInfo();
			expressionInfo.m_type = this.m_type;
			expressionInfo.m_compileTimeID = this.m_compileTimeID;
			expressionInfo.m_stringValue = this.m_stringValue;
			expressionInfo.m_boolValue = this.m_boolValue;
			expressionInfo.m_intValue = this.m_intValue;
			expressionInfo.m_originalText = this.m_originalText;
			expressionInfo.m_referencedFields = this.m_referencedFields;
			expressionInfo.m_referencedParameters = this.m_referencedParameters;
			Global.Tracer.Assert(this.m_referencedReportItems == null);
			if (this.m_aggregates != null)
			{
				int count = this.m_aggregates.Count;
				expressionInfo.m_aggregates = new DataAggregateInfoList(count);
				for (int i = 0; i < count; i++)
				{
					expressionInfo.m_aggregates.Add(this.m_aggregates[i].DeepClone(context));
				}
			}
			if (this.m_runningValues != null)
			{
				int count2 = this.m_runningValues.Count;
				expressionInfo.m_runningValues = new RunningValueInfoList(count2);
				for (int j = 0; j < count2; j++)
				{
					expressionInfo.m_runningValues.Add(this.m_runningValues[j].DeepClone(context));
				}
			}
			if (this.m_transformedExpression != null)
			{
				StringBuilder stringBuilder = new StringBuilder(this.m_transformedExpression);
				if (context.AggregateRewriteMap != null && this.m_transformedExpressionAggregateIDs != null)
				{
					Global.Tracer.Assert(this.m_transformedExpressionAggregatePositions != null && this.m_transformedExpressionAggregateIDs.Count == this.m_transformedExpressionAggregatePositions.Count && this.m_transformedExpression != null);
					int num = 11;
					for (int k = 0; k < this.m_transformedExpressionAggregateIDs.Count; k++)
					{
						string text = this.m_transformedExpressionAggregateIDs[k];
						string text2 = context.AggregateRewriteMap[text] as string;
						int num2 = this.m_transformedExpressionAggregatePositions[k];
						if (text2 != null)
						{
							Global.Tracer.Assert(text != null && text2 != null && text2.Length >= text.Length);
							Global.Tracer.Assert(this.m_transformedExpression.Length > num2 + num);
							stringBuilder.Replace(text, text2, num2 + num, text.Length);
							num += text2.Length - text.Length;
						}
					}
				}
				expressionInfo.m_transformedExpression = stringBuilder.ToString();
			}
			return expressionInfo;
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x0018E734 File Offset: 0x0018C934
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Type, Token.Enum),
				new MemberInfo(MemberName.StringValue, Token.String),
				new MemberInfo(MemberName.BoolValue, Token.Boolean),
				new MemberInfo(MemberName.IntValue, Token.Int32),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.OriginalValue, Token.String)
			});
		}

		// Token: 0x060064CD RID: 25805 RVA: 0x0018E7CE File Offset: 0x0018C9CE
		internal void AddReferencedField(string fieldName)
		{
			if (this.m_referencedFields == null)
			{
				this.m_referencedFields = new StringList();
			}
			this.m_referencedFields.Add(fieldName);
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x0018E7F0 File Offset: 0x0018C9F0
		internal void AddReferencedReportItem(string reportItemName)
		{
			if (this.m_referencedReportItems == null)
			{
				this.m_referencedReportItems = new StringList();
			}
			this.m_referencedReportItems.Add(reportItemName);
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x0018E812 File Offset: 0x0018CA12
		internal void AddReferencedParameter(string parameterName)
		{
			if (this.m_referencedParameters == null)
			{
				this.m_referencedParameters = new StringList();
			}
			this.m_referencedParameters.Add(parameterName);
		}

		// Token: 0x060064D0 RID: 25808 RVA: 0x0018E834 File Offset: 0x0018CA34
		internal void AddReferencedDataSet(string dataSetName)
		{
			if (this.m_referencedDataSets == null)
			{
				this.m_referencedDataSets = new StringList();
			}
			this.m_referencedDataSets.Add(dataSetName);
		}

		// Token: 0x060064D1 RID: 25809 RVA: 0x0018E856 File Offset: 0x0018CA56
		internal void AddReferencedDataSource(string dataSourceName)
		{
			if (this.m_referencedDataSources == null)
			{
				this.m_referencedDataSources = new StringList();
			}
			this.m_referencedDataSources.Add(dataSourceName);
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x0018E878 File Offset: 0x0018CA78
		internal void AddAggregate(DataAggregateInfo aggregate)
		{
			if (this.m_aggregates == null)
			{
				this.m_aggregates = new DataAggregateInfoList();
			}
			this.m_aggregates.Add(aggregate);
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x0018E89A File Offset: 0x0018CA9A
		internal void AddRunningValue(RunningValueInfo runningValue)
		{
			if (this.m_runningValues == null)
			{
				this.m_runningValues = new RunningValueInfoList();
			}
			this.m_runningValues.Add(runningValue);
		}

		// Token: 0x060064D4 RID: 25812 RVA: 0x0018E8BC File Offset: 0x0018CABC
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

		// Token: 0x060064D5 RID: 25813 RVA: 0x0018E918 File Offset: 0x0018CB18
		internal void AddDynamicPropertyReference(string fieldName)
		{
			Global.Tracer.Assert(fieldName != null);
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

		// Token: 0x060064D6 RID: 25814 RVA: 0x0018E970 File Offset: 0x0018CB70
		internal void AddStaticPropertyReference(string fieldName, string propertyName)
		{
			Global.Tracer.Assert(fieldName != null && propertyName != null);
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

		// Token: 0x04003278 RID: 12920
		private ExpressionInfo.Types m_type;

		// Token: 0x04003279 RID: 12921
		private string m_stringValue;

		// Token: 0x0400327A RID: 12922
		private bool m_boolValue;

		// Token: 0x0400327B RID: 12923
		private int m_intValue;

		// Token: 0x0400327C RID: 12924
		private int m_exprHostID = -1;

		// Token: 0x0400327D RID: 12925
		private string m_originalText;

		// Token: 0x0400327E RID: 12926
		[NonSerialized]
		private string m_transformedExpression;

		// Token: 0x0400327F RID: 12927
		[NonSerialized]
		private IntList m_transformedExpressionAggregatePositions;

		// Token: 0x04003280 RID: 12928
		[NonSerialized]
		private StringList m_transformedExpressionAggregateIDs;

		// Token: 0x04003281 RID: 12929
		[NonSerialized]
		private StringList m_referencedFields;

		// Token: 0x04003282 RID: 12930
		[NonSerialized]
		private StringList m_referencedReportItems;

		// Token: 0x04003283 RID: 12931
		[NonSerialized]
		private StringList m_referencedParameters;

		// Token: 0x04003284 RID: 12932
		[NonSerialized]
		private StringList m_referencedDataSets;

		// Token: 0x04003285 RID: 12933
		[NonSerialized]
		private StringList m_referencedDataSources;

		// Token: 0x04003286 RID: 12934
		[NonSerialized]
		private DataAggregateInfoList m_aggregates;

		// Token: 0x04003287 RID: 12935
		[NonSerialized]
		private RunningValueInfoList m_runningValues;

		// Token: 0x04003288 RID: 12936
		[NonSerialized]
		private int m_compileTimeID = -1;

		// Token: 0x04003289 RID: 12937
		[NonSerialized]
		private Hashtable m_referencedFieldProperties;

		// Token: 0x0400328A RID: 12938
		[NonSerialized]
		private bool m_dynamicFieldReferences;

		// Token: 0x02000CDA RID: 3290
		internal enum Types
		{
			// Token: 0x04004F19 RID: 20249
			Expression,
			// Token: 0x04004F1A RID: 20250
			Field,
			// Token: 0x04004F1B RID: 20251
			Aggregate,
			// Token: 0x04004F1C RID: 20252
			Constant,
			// Token: 0x04004F1D RID: 20253
			Token
		}
	}
}
