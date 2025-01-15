using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200061C RID: 1564
	[Serializable]
	internal sealed class ParameterDef : ParameterBase, IParameterDef
	{
		// Token: 0x17001F64 RID: 8036
		// (get) Token: 0x060055D0 RID: 21968 RVA: 0x00169BA0 File Offset: 0x00167DA0
		// (set) Token: 0x060055D1 RID: 21969 RVA: 0x00169BA8 File Offset: 0x00167DA8
		internal ExpressionInfoList DefaultExpressions
		{
			get
			{
				return this.m_defaultExpressions;
			}
			set
			{
				this.m_defaultExpressions = value;
			}
		}

		// Token: 0x17001F65 RID: 8037
		// (get) Token: 0x060055D2 RID: 21970 RVA: 0x00169BB1 File Offset: 0x00167DB1
		// (set) Token: 0x060055D3 RID: 21971 RVA: 0x00169BB9 File Offset: 0x00167DB9
		internal ParameterDataSource ValidValuesDataSource
		{
			get
			{
				return this.m_validValuesDataSource;
			}
			set
			{
				this.m_validValuesDataSource = value;
			}
		}

		// Token: 0x17001F66 RID: 8038
		// (get) Token: 0x060055D4 RID: 21972 RVA: 0x00169BC2 File Offset: 0x00167DC2
		// (set) Token: 0x060055D5 RID: 21973 RVA: 0x00169BCA File Offset: 0x00167DCA
		internal ExpressionInfoList ValidValuesValueExpressions
		{
			get
			{
				return this.m_validValuesValueExpressions;
			}
			set
			{
				this.m_validValuesValueExpressions = value;
			}
		}

		// Token: 0x17001F67 RID: 8039
		// (get) Token: 0x060055D6 RID: 21974 RVA: 0x00169BD3 File Offset: 0x00167DD3
		// (set) Token: 0x060055D7 RID: 21975 RVA: 0x00169BDB File Offset: 0x00167DDB
		internal ExpressionInfoList ValidValuesLabelExpressions
		{
			get
			{
				return this.m_validValuesLabelExpressions;
			}
			set
			{
				this.m_validValuesLabelExpressions = value;
			}
		}

		// Token: 0x17001F68 RID: 8040
		// (get) Token: 0x060055D8 RID: 21976 RVA: 0x00169BE4 File Offset: 0x00167DE4
		// (set) Token: 0x060055D9 RID: 21977 RVA: 0x00169BEC File Offset: 0x00167DEC
		internal ParameterDataSource DefaultDataSource
		{
			get
			{
				return this.m_defaultDataSource;
			}
			set
			{
				this.m_defaultDataSource = value;
			}
		}

		// Token: 0x17001F69 RID: 8041
		// (get) Token: 0x060055DA RID: 21978 RVA: 0x00169BF5 File Offset: 0x00167DF5
		// (set) Token: 0x060055DB RID: 21979 RVA: 0x00169BFD File Offset: 0x00167DFD
		internal ParameterDefList DependencyList
		{
			get
			{
				return this.m_dependencyList;
			}
			set
			{
				this.m_dependencyList = value;
			}
		}

		// Token: 0x17001F6A RID: 8042
		// (get) Token: 0x060055DC RID: 21980 RVA: 0x00169C06 File Offset: 0x00167E06
		// (set) Token: 0x060055DD RID: 21981 RVA: 0x00169C0E File Offset: 0x00167E0E
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

		// Token: 0x17001F6B RID: 8043
		// (get) Token: 0x060055DE RID: 21982 RVA: 0x00169C17 File Offset: 0x00167E17
		internal ReportParamExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001F6C RID: 8044
		// (get) Token: 0x060055DF RID: 21983 RVA: 0x00169C1F File Offset: 0x00167E1F
		// (set) Token: 0x060055E0 RID: 21984 RVA: 0x00169C27 File Offset: 0x00167E27
		public override string Prompt
		{
			get
			{
				return this.m_prompt;
			}
			set
			{
				this.m_prompt = value;
			}
		}

		// Token: 0x17001F6D RID: 8045
		// (get) Token: 0x060055E1 RID: 21985 RVA: 0x00169C30 File Offset: 0x00167E30
		int IParameterDef.DefaultValuesExpressionCount
		{
			get
			{
				if (this.DefaultExpressions == null)
				{
					return 0;
				}
				return this.DefaultExpressions.Count;
			}
		}

		// Token: 0x17001F6E RID: 8046
		// (get) Token: 0x060055E2 RID: 21986 RVA: 0x00169C47 File Offset: 0x00167E47
		int IParameterDef.ValidValuesValueExpressionCount
		{
			get
			{
				if (this.ValidValuesValueExpressions == null)
				{
					return 0;
				}
				return this.ValidValuesValueExpressions.Count;
			}
		}

		// Token: 0x17001F6F RID: 8047
		// (get) Token: 0x060055E3 RID: 21987 RVA: 0x00169C5E File Offset: 0x00167E5E
		int IParameterDef.ValidValuesLabelExpressionCount
		{
			get
			{
				if (this.ValidValuesLabelExpressions == null)
				{
					return 0;
				}
				return this.ValidValuesLabelExpressions.Count;
			}
		}

		// Token: 0x060055E4 RID: 21988 RVA: 0x00169C75 File Offset: 0x00167E75
		bool IParameterDef.HasDefaultValuesExpressions()
		{
			return this.DefaultExpressions != null;
		}

		// Token: 0x060055E5 RID: 21989 RVA: 0x00169C80 File Offset: 0x00167E80
		bool IParameterDef.HasValidValuesValueExpressions()
		{
			return this.ValidValuesValueExpressions != null;
		}

		// Token: 0x060055E6 RID: 21990 RVA: 0x00169C8B File Offset: 0x00167E8B
		bool IParameterDef.HasValidValuesLabelExpressions()
		{
			return this.ValidValuesLabelExpressions != null;
		}

		// Token: 0x060055E7 RID: 21991 RVA: 0x00169C96 File Offset: 0x00167E96
		bool IParameterDef.HasDefaultValuesDataSource()
		{
			return this.DefaultDataSource != null;
		}

		// Token: 0x060055E8 RID: 21992 RVA: 0x00169CA1 File Offset: 0x00167EA1
		bool IParameterDef.HasValidValuesDataSource()
		{
			return this.ValidValuesDataSource != null;
		}

		// Token: 0x17001F70 RID: 8048
		// (get) Token: 0x060055E9 RID: 21993 RVA: 0x00169CAC File Offset: 0x00167EAC
		string IParameterDef.Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17001F71 RID: 8049
		// (get) Token: 0x060055EA RID: 21994 RVA: 0x00169CB4 File Offset: 0x00167EB4
		ObjectType IParameterDef.ParameterObjectType
		{
			get
			{
				return ObjectType.ReportParameter;
			}
		}

		// Token: 0x17001F72 RID: 8050
		// (get) Token: 0x060055EB RID: 21995 RVA: 0x00169CB8 File Offset: 0x00167EB8
		DataType IParameterDef.DataType
		{
			get
			{
				return base.DataType;
			}
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x00169CC0 File Offset: 0x00167EC0
		bool IParameterDef.ValidateValueForNull(object newValue, ErrorContext errorContext, string parameterValueProperty)
		{
			return ParameterBase.ValidateValueForNull(newValue, base.Nullable, errorContext, ObjectType.ReportParameter, base.Name, parameterValueProperty);
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x00169CD8 File Offset: 0x00167ED8
		bool IParameterDef.ValidateValueForBlank(object newValue, ErrorContext errorContext, string parameterValueProperty)
		{
			return base.ValidateValueForBlank(newValue, errorContext, parameterValueProperty);
		}

		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x060055EE RID: 21998 RVA: 0x00169CE3 File Offset: 0x00167EE3
		bool IParameterDef.MultiValue
		{
			get
			{
				return base.MultiValue;
			}
		}

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x060055EF RID: 21999 RVA: 0x00169CEB File Offset: 0x00167EEB
		IParameterDataSource IParameterDef.DefaultDataSource
		{
			get
			{
				return this.DefaultDataSource;
			}
		}

		// Token: 0x17001F75 RID: 8053
		// (get) Token: 0x060055F0 RID: 22000 RVA: 0x00169CF3 File Offset: 0x00167EF3
		IParameterDataSource IParameterDef.ValidValuesDataSource
		{
			get
			{
				return this.ValidValuesDataSource;
			}
		}

		// Token: 0x17001F76 RID: 8054
		// (get) Token: 0x060055F1 RID: 22001 RVA: 0x00169CFB File Offset: 0x00167EFB
		bool IParameterDef.UseAllValidValues
		{
			get
			{
				return base.UseAllValidValues;
			}
		}

		// Token: 0x060055F2 RID: 22002 RVA: 0x00169D04 File Offset: 0x00167F04
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.ReportParameterStart(base.Name);
			if (this.m_defaultExpressions != null)
			{
				for (int i = this.m_defaultExpressions.Count - 1; i >= 0; i--)
				{
					context.ExprHostBuilder.ReportParameterDefaultValue(this.m_defaultExpressions[i]);
				}
			}
			if (this.m_validValuesValueExpressions != null)
			{
				context.ExprHostBuilder.ReportParameterValidValuesStart();
				for (int j = this.m_validValuesValueExpressions.Count - 1; j >= 0; j--)
				{
					ExpressionInfo expressionInfo = this.m_validValuesValueExpressions[j];
					if (expressionInfo != null)
					{
						context.ExprHostBuilder.ReportParameterValidValue(expressionInfo);
					}
				}
				context.ExprHostBuilder.ReportParameterValidValuesEnd();
			}
			if (this.m_validValuesLabelExpressions != null)
			{
				context.ExprHostBuilder.ReportParameterValidValueLabelsStart();
				for (int k = this.m_validValuesLabelExpressions.Count - 1; k >= 0; k--)
				{
					ExpressionInfo expressionInfo2 = this.m_validValuesLabelExpressions[k];
					if (expressionInfo2 != null)
					{
						context.ExprHostBuilder.ReportParameterValidValueLabel(expressionInfo2);
					}
				}
				context.ExprHostBuilder.ReportParameterValidValueLabelsEnd();
			}
			this.ExprHostID = context.ExprHostBuilder.ReportParameterEnd();
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x00169E1C File Offset: 0x0016801C
		internal void SetExprHost(ReportExprHost reportExprHost, ObjectModel reportObjectModel)
		{
			Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
			if (this.ExprHostID >= 0)
			{
				this.m_exprHost = reportExprHost.ReportParameterHostsRemotable[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_exprHost.ValidValuesHost != null)
				{
					this.m_exprHost.ValidValuesHost.SetReportObjectModel(reportObjectModel);
				}
				if (this.m_exprHost.ValidValueLabelsHost != null)
				{
					this.m_exprHost.ValidValueLabelsHost.SetReportObjectModel(reportObjectModel);
				}
			}
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x00169EAC File Offset: 0x001680AC
		internal void Parse(string name, List<string> defaultValues, string type, string nullable, string prompt, string promptUser, string allowBlank, string multiValue, string usedInQuery, bool hidden, ErrorContext errorContext, CultureInfo language, string useAllValidValues)
		{
			base.Parse(name, defaultValues, type, nullable, prompt, promptUser, allowBlank, multiValue, usedInQuery, hidden, errorContext, language, useAllValidValues);
			if (hidden)
			{
				this.m_prompt = "";
			}
			else if (prompt == null)
			{
				this.m_prompt = name + ":";
			}
			else
			{
				this.m_prompt = prompt;
			}
			if (this.m_validValuesValueExpressions != null && DataType.Boolean != base.DataType)
			{
				for (int i = this.m_validValuesValueExpressions.Count - 1; i >= 0; i--)
				{
					ExpressionInfo expressionInfo = this.m_validValuesValueExpressions[i];
					if (expressionInfo == null && base.MultiValue)
					{
						this.m_validValuesValueExpressions.RemoveAt(i);
					}
					else if (expressionInfo != null && ExpressionInfo.Types.Constant == expressionInfo.Type)
					{
						object obj;
						if (!ParameterBase.CastFromString(expressionInfo.Value, out obj, base.DataType, language))
						{
							if (errorContext == null)
							{
								throw new ReportParameterTypeMismatchException(name);
							}
							errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, base.ParameterObjectType, name, "ValidValue", Array.Empty<string>());
						}
						else
						{
							base.ValidateValue(obj, errorContext, base.ParameterObjectType, "ValidValue");
						}
					}
				}
			}
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x00169FC4 File Offset: 0x001681C4
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.ParameterBase, new MemberInfoList
			{
				new MemberInfo(MemberName.ValidValuesDataSource, ObjectType.ParameterDataSource),
				new MemberInfo(MemberName.ValidValuesValueExpression, ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.ValidValuesLabelExpression, ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.DefaultValueDataSource, ObjectType.ParameterDataSource),
				new MemberInfo(MemberName.ExpressionList, ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.DependencyList, ObjectType.ParameterDefList),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x04002D82 RID: 11650
		private ParameterDataSource m_validValuesDataSource;

		// Token: 0x04002D83 RID: 11651
		private ExpressionInfoList m_validValuesValueExpressions;

		// Token: 0x04002D84 RID: 11652
		private ExpressionInfoList m_validValuesLabelExpressions;

		// Token: 0x04002D85 RID: 11653
		private ParameterDataSource m_defaultDataSource;

		// Token: 0x04002D86 RID: 11654
		private ExpressionInfoList m_defaultExpressions;

		// Token: 0x04002D87 RID: 11655
		private ParameterDefList m_dependencyList;

		// Token: 0x04002D88 RID: 11656
		private int m_exprHostID = -1;

		// Token: 0x04002D89 RID: 11657
		private string m_prompt;

		// Token: 0x04002D8A RID: 11658
		[NonSerialized]
		private ReportParamExprHost m_exprHost;
	}
}
