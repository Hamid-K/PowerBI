using System;
using System.CodeDom.Compiler;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000682 RID: 1666
	internal abstract class ExpressionParser
	{
		// Token: 0x06005B35 RID: 23349 RVA: 0x001774C3 File Offset: 0x001756C3
		internal ExpressionParser(ErrorContext errorContext)
		{
			this.m_errorContext = errorContext;
		}

		// Token: 0x06005B36 RID: 23350
		internal abstract CodeDomProvider GetCodeCompiler();

		// Token: 0x06005B37 RID: 23351
		internal abstract string GetCompilerArguments();

		// Token: 0x06005B38 RID: 23352
		internal abstract ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context);

		// Token: 0x06005B39 RID: 23353
		internal abstract ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, ExpressionParser.DetectionFlags flag, out bool reportParameterReferenced, out string reportParameterName, out bool userCollectionReferenced);

		// Token: 0x06005B3A RID: 23354
		internal abstract ExpressionInfo ParseExpression(string expression, ExpressionParser.ExpressionContext context, out bool userCollectionReferenced);

		// Token: 0x06005B3B RID: 23355
		internal abstract void ConvertField2ComplexExpr(ref ExpressionInfo expression);

		// Token: 0x06005B3C RID: 23356 RVA: 0x001774D2 File Offset: 0x001756D2
		internal void ResetValueReferencedFlag()
		{
			this.m_valueReferenced = false;
		}

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x06005B3D RID: 23357
		internal abstract bool BodyRefersToReportItems { get; }

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x06005B3E RID: 23358
		internal abstract bool PageSectionRefersToReportItems { get; }

		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x06005B3F RID: 23359
		internal abstract int NumberOfAggregates { get; }

		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x06005B40 RID: 23360
		internal abstract int LastID { get; }

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x06005B41 RID: 23361 RVA: 0x001774DB File Offset: 0x001756DB
		internal bool ValueReferenced
		{
			get
			{
				return this.m_valueReferenced;
			}
		}

		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x06005B42 RID: 23362 RVA: 0x001774E3 File Offset: 0x001756E3
		internal bool ValueReferencedGlobal
		{
			get
			{
				return this.m_valueReferencedGlobal;
			}
		}

		// Token: 0x06005B43 RID: 23363 RVA: 0x001774EC File Offset: 0x001756EC
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
				return ExpressionParser.Restrictions.SortExpression;
			case ExpressionParser.ExpressionType.DataSetFilters:
				return ExpressionParser.Restrictions.AggregateParameterInBody;
			case ExpressionParser.ExpressionType.DataRegionFilters:
				return ExpressionParser.Restrictions.AggregateParameterInBody;
			case ExpressionParser.ExpressionType.GroupingFilters:
				return ExpressionParser.Restrictions.SortExpression;
			case ExpressionParser.ExpressionType.FieldValue:
				return ExpressionParser.Restrictions.FieldValue;
			default:
				Global.Tracer.Assert(false);
				return ExpressionParser.Restrictions.None;
			}
		}

		// Token: 0x06005B44 RID: 23364 RVA: 0x0017756D File Offset: 0x0017576D
		protected void SetValueReferenced()
		{
			this.m_valueReferenced = true;
			this.m_valueReferencedGlobal = true;
		}

		// Token: 0x04002F79 RID: 12153
		protected ErrorContext m_errorContext;

		// Token: 0x04002F7A RID: 12154
		private bool m_valueReferenced;

		// Token: 0x04002F7B RID: 12155
		private bool m_valueReferencedGlobal;

		// Token: 0x02000CA6 RID: 3238
		[Flags]
		internal enum DetectionFlags
		{
			// Token: 0x04004D61 RID: 19809
			ParameterReference = 1,
			// Token: 0x04004D62 RID: 19810
			UserReference = 2
		}

		// Token: 0x02000CA7 RID: 3239
		internal enum ExpressionType
		{
			// Token: 0x04004D64 RID: 19812
			General,
			// Token: 0x04004D65 RID: 19813
			ReportParameter,
			// Token: 0x04004D66 RID: 19814
			ReportLanguage,
			// Token: 0x04004D67 RID: 19815
			QueryParameter,
			// Token: 0x04004D68 RID: 19816
			GroupExpression,
			// Token: 0x04004D69 RID: 19817
			SortExpression,
			// Token: 0x04004D6A RID: 19818
			DataSetFilters,
			// Token: 0x04004D6B RID: 19819
			DataRegionFilters,
			// Token: 0x04004D6C RID: 19820
			GroupingFilters,
			// Token: 0x04004D6D RID: 19821
			FieldValue
		}

		// Token: 0x02000CA8 RID: 3240
		internal enum ConstantType
		{
			// Token: 0x04004D6F RID: 19823
			String,
			// Token: 0x04004D70 RID: 19824
			Boolean,
			// Token: 0x04004D71 RID: 19825
			Integer
		}

		// Token: 0x02000CA9 RID: 3241
		internal enum RecursiveFlags
		{
			// Token: 0x04004D73 RID: 19827
			Simple,
			// Token: 0x04004D74 RID: 19828
			Recursive
		}

		// Token: 0x02000CAA RID: 3242
		internal struct ExpressionContext
		{
			// Token: 0x06008CBA RID: 36026 RVA: 0x0023CA75 File Offset: 0x0023AC75
			internal ExpressionContext(ExpressionParser.ExpressionType expressionType, ExpressionParser.ConstantType constantType, LocationFlags location, ObjectType objectType, string objectName, string propertyName, string dataSetName, bool parseExtended)
			{
				this.m_expressionType = expressionType;
				this.m_constantType = constantType;
				this.m_location = location;
				this.m_objectType = objectType;
				this.m_objectName = objectName;
				this.m_propertyName = propertyName;
				this.m_dataSetName = dataSetName;
				this.m_parseExtended = parseExtended;
			}

			// Token: 0x17002B3F RID: 11071
			// (get) Token: 0x06008CBB RID: 36027 RVA: 0x0023CAB4 File Offset: 0x0023ACB4
			internal ExpressionParser.ExpressionType ExpressionType
			{
				get
				{
					return this.m_expressionType;
				}
			}

			// Token: 0x17002B40 RID: 11072
			// (get) Token: 0x06008CBC RID: 36028 RVA: 0x0023CABC File Offset: 0x0023ACBC
			internal ExpressionParser.ConstantType ConstantType
			{
				get
				{
					return this.m_constantType;
				}
			}

			// Token: 0x17002B41 RID: 11073
			// (get) Token: 0x06008CBD RID: 36029 RVA: 0x0023CAC4 File Offset: 0x0023ACC4
			internal LocationFlags Location
			{
				get
				{
					return this.m_location;
				}
			}

			// Token: 0x17002B42 RID: 11074
			// (get) Token: 0x06008CBE RID: 36030 RVA: 0x0023CACC File Offset: 0x0023ACCC
			internal ObjectType ObjectType
			{
				get
				{
					return this.m_objectType;
				}
			}

			// Token: 0x17002B43 RID: 11075
			// (get) Token: 0x06008CBF RID: 36031 RVA: 0x0023CAD4 File Offset: 0x0023ACD4
			internal string ObjectName
			{
				get
				{
					return this.m_objectName;
				}
			}

			// Token: 0x17002B44 RID: 11076
			// (get) Token: 0x06008CC0 RID: 36032 RVA: 0x0023CADC File Offset: 0x0023ACDC
			internal string PropertyName
			{
				get
				{
					return this.m_propertyName;
				}
			}

			// Token: 0x17002B45 RID: 11077
			// (get) Token: 0x06008CC1 RID: 36033 RVA: 0x0023CAE4 File Offset: 0x0023ACE4
			internal string DataSetName
			{
				get
				{
					return this.m_dataSetName;
				}
			}

			// Token: 0x17002B46 RID: 11078
			// (get) Token: 0x06008CC2 RID: 36034 RVA: 0x0023CAEC File Offset: 0x0023ACEC
			internal bool ParseExtended
			{
				get
				{
					return this.m_parseExtended;
				}
			}

			// Token: 0x04004D75 RID: 19829
			private ExpressionParser.ExpressionType m_expressionType;

			// Token: 0x04004D76 RID: 19830
			private ExpressionParser.ConstantType m_constantType;

			// Token: 0x04004D77 RID: 19831
			private LocationFlags m_location;

			// Token: 0x04004D78 RID: 19832
			private ObjectType m_objectType;

			// Token: 0x04004D79 RID: 19833
			private string m_objectName;

			// Token: 0x04004D7A RID: 19834
			private string m_propertyName;

			// Token: 0x04004D7B RID: 19835
			private string m_dataSetName;

			// Token: 0x04004D7C RID: 19836
			private bool m_parseExtended;
		}

		// Token: 0x02000CAB RID: 3243
		[Flags]
		protected enum GrammarFlags
		{
			// Token: 0x04004D7E RID: 19838
			DenyAggregates = 1,
			// Token: 0x04004D7F RID: 19839
			DenyRunningValue = 2,
			// Token: 0x04004D80 RID: 19840
			DenyRowNumber = 4,
			// Token: 0x04004D81 RID: 19841
			DenyFields = 8,
			// Token: 0x04004D82 RID: 19842
			DenyReportItems = 16,
			// Token: 0x04004D83 RID: 19843
			DenyPageGlobals = 32,
			// Token: 0x04004D84 RID: 19844
			DenyPostSortAggregate = 64,
			// Token: 0x04004D85 RID: 19845
			DenyPrevious = 128,
			// Token: 0x04004D86 RID: 19846
			DenyDataSets = 256,
			// Token: 0x04004D87 RID: 19847
			DenyDataSources = 512
		}

		// Token: 0x02000CAC RID: 3244
		[Flags]
		protected enum Restrictions
		{
			// Token: 0x04004D89 RID: 19849
			None = 0,
			// Token: 0x04004D8A RID: 19850
			InPageSection = 910,
			// Token: 0x04004D8B RID: 19851
			InBody = 32,
			// Token: 0x04004D8C RID: 19852
			AggregateParameterInPageSection = 135,
			// Token: 0x04004D8D RID: 19853
			AggregateParameterInBody = 151,
			// Token: 0x04004D8E RID: 19854
			ReportParameter = 927,
			// Token: 0x04004D8F RID: 19855
			ReportLanguage = 927,
			// Token: 0x04004D90 RID: 19856
			QueryParameter = 927,
			// Token: 0x04004D91 RID: 19857
			GroupExpression = 147,
			// Token: 0x04004D92 RID: 19858
			SortExpression = 214,
			// Token: 0x04004D93 RID: 19859
			DataSetFilters = 151,
			// Token: 0x04004D94 RID: 19860
			DataRegionFilters = 151,
			// Token: 0x04004D95 RID: 19861
			GroupingFilters = 214,
			// Token: 0x04004D96 RID: 19862
			FieldValue = 183
		}
	}
}
