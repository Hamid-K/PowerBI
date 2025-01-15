using System;
using System.Data;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006D8 RID: 1752
	[Serializable]
	internal sealed class ReportQuery
	{
		// Token: 0x17002165 RID: 8549
		// (get) Token: 0x06005F06 RID: 24326 RVA: 0x001816CF File Offset: 0x0017F8CF
		// (set) Token: 0x06005F07 RID: 24327 RVA: 0x001816D7 File Offset: 0x0017F8D7
		internal CommandType CommandType
		{
			get
			{
				return this.m_commandType;
			}
			set
			{
				this.m_commandType = value;
			}
		}

		// Token: 0x17002166 RID: 8550
		// (get) Token: 0x06005F08 RID: 24328 RVA: 0x001816E0 File Offset: 0x0017F8E0
		// (set) Token: 0x06005F09 RID: 24329 RVA: 0x001816E8 File Offset: 0x0017F8E8
		internal ExpressionInfo CommandText
		{
			get
			{
				return this.m_commandText;
			}
			set
			{
				this.m_commandText = value;
			}
		}

		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x06005F0A RID: 24330 RVA: 0x001816F1 File Offset: 0x0017F8F1
		// (set) Token: 0x06005F0B RID: 24331 RVA: 0x001816F9 File Offset: 0x0017F8F9
		internal ParameterValueList Parameters
		{
			get
			{
				return this.m_queryParameters;
			}
			set
			{
				this.m_queryParameters = value;
			}
		}

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x06005F0C RID: 24332 RVA: 0x00181702 File Offset: 0x0017F902
		// (set) Token: 0x06005F0D RID: 24333 RVA: 0x0018170A File Offset: 0x0017F90A
		internal int TimeOut
		{
			get
			{
				return this.m_timeOut;
			}
			set
			{
				this.m_timeOut = value;
			}
		}

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x06005F0E RID: 24334 RVA: 0x00181713 File Offset: 0x0017F913
		// (set) Token: 0x06005F0F RID: 24335 RVA: 0x0018171B File Offset: 0x0017F91B
		internal string CommandTextValue
		{
			get
			{
				return this.m_commandTextValue;
			}
			set
			{
				this.m_commandTextValue = value;
			}
		}

		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x06005F10 RID: 24336 RVA: 0x00181724 File Offset: 0x0017F924
		// (set) Token: 0x06005F11 RID: 24337 RVA: 0x0018172C File Offset: 0x0017F92C
		internal string RewrittenCommandText
		{
			get
			{
				return this.m_writtenCommandText;
			}
			set
			{
				this.m_writtenCommandText = value;
			}
		}

		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x06005F12 RID: 24338 RVA: 0x00181735 File Offset: 0x0017F935
		// (set) Token: 0x06005F13 RID: 24339 RVA: 0x0018173D File Offset: 0x0017F93D
		internal string DataSourceName
		{
			get
			{
				return this.m_dataSourceName;
			}
			set
			{
				this.m_dataSourceName = value;
			}
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x00181748 File Offset: 0x0017F948
		internal void Initialize(InitializationContext context)
		{
			if (this.m_commandText != null)
			{
				this.m_commandText.Initialize("CommandText", context);
				context.ExprHostBuilder.DataSetQueryCommandText(this.m_commandText);
			}
			if (this.m_queryParameters != null)
			{
				ObjectType objectType = context.ObjectType;
				string objectName = context.ObjectName;
				context.ObjectType = ObjectType.QueryParameter;
				context.ExprHostBuilder.QueryParametersStart();
				for (int i = 0; i < this.m_queryParameters.Count; i++)
				{
					ParameterValue parameterValue = this.m_queryParameters[i];
					context.ObjectName = parameterValue.Name;
					parameterValue.Initialize(context, true);
				}
				context.ExprHostBuilder.QueryParametersEnd();
				context.ObjectType = objectType;
				context.ObjectName = objectName;
			}
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x00181801 File Offset: 0x0017FA01
		internal void SetExprHost(IndexedExprHost queryParamsExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(queryParamsExprHost != null && reportObjectModel != null);
			this.m_queryParamsExprHost = queryParamsExprHost;
			this.m_queryParamsExprHost.SetReportObjectModel(reportObjectModel);
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x0018182C File Offset: 0x0017FA2C
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.CommandType, Token.Enum),
				new MemberInfo(MemberName.CommandText, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.QueryParameters, ObjectType.ParameterValueList),
				new MemberInfo(MemberName.Timeout, Token.Int32),
				new MemberInfo(MemberName.CommandTextValue, Token.String),
				new MemberInfo(MemberName.RewrittenCommandText, Token.String)
			});
		}

		// Token: 0x0400306D RID: 12397
		private CommandType m_commandType = CommandType.Text;

		// Token: 0x0400306E RID: 12398
		private ExpressionInfo m_commandText;

		// Token: 0x0400306F RID: 12399
		private ParameterValueList m_queryParameters;

		// Token: 0x04003070 RID: 12400
		private int m_timeOut;

		// Token: 0x04003071 RID: 12401
		private string m_commandTextValue;

		// Token: 0x04003072 RID: 12402
		private string m_writtenCommandText;

		// Token: 0x04003073 RID: 12403
		[NonSerialized]
		private string m_dataSourceName;

		// Token: 0x04003074 RID: 12404
		[NonSerialized]
		private IndexedExprHost m_queryParamsExprHost;
	}
}
