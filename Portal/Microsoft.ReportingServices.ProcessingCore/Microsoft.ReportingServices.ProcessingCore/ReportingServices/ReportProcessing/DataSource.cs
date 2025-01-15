using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006D6 RID: 1750
	[Serializable]
	internal sealed class DataSource : IProcessingDataSource
	{
		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x06005E96 RID: 24214 RVA: 0x00180753 File Offset: 0x0017E953
		// (set) Token: 0x06005E97 RID: 24215 RVA: 0x0018075B File Offset: 0x0017E95B
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17002137 RID: 8503
		// (get) Token: 0x06005E98 RID: 24216 RVA: 0x00180764 File Offset: 0x0017E964
		// (set) Token: 0x06005E99 RID: 24217 RVA: 0x0018076C File Offset: 0x0017E96C
		public bool Transaction
		{
			get
			{
				return this.m_transaction;
			}
			set
			{
				this.m_transaction = value;
			}
		}

		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x06005E9A RID: 24218 RVA: 0x00180775 File Offset: 0x0017E975
		// (set) Token: 0x06005E9B RID: 24219 RVA: 0x0018077D File Offset: 0x0017E97D
		public string Type
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

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x06005E9C RID: 24220 RVA: 0x00180786 File Offset: 0x0017E986
		// (set) Token: 0x06005E9D RID: 24221 RVA: 0x0018078E File Offset: 0x0017E98E
		internal ExpressionInfo ConnectStringExpression
		{
			get
			{
				return this.m_connectString;
			}
			set
			{
				this.m_connectString = value;
			}
		}

		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x06005E9E RID: 24222 RVA: 0x00180797 File Offset: 0x0017E997
		// (set) Token: 0x06005E9F RID: 24223 RVA: 0x0018079F File Offset: 0x0017E99F
		public bool IntegratedSecurity
		{
			get
			{
				return this.m_integratedSecurity;
			}
			set
			{
				this.m_integratedSecurity = value;
			}
		}

		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x06005EA0 RID: 24224 RVA: 0x001807A8 File Offset: 0x0017E9A8
		// (set) Token: 0x06005EA1 RID: 24225 RVA: 0x001807B0 File Offset: 0x0017E9B0
		public string Prompt
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

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x06005EA2 RID: 24226 RVA: 0x001807B9 File Offset: 0x0017E9B9
		// (set) Token: 0x06005EA3 RID: 24227 RVA: 0x001807C1 File Offset: 0x0017E9C1
		public string DataSourceReference
		{
			get
			{
				return this.m_dataSourceReference;
			}
			set
			{
				this.m_dataSourceReference = value;
			}
		}

		// Token: 0x1700213D RID: 8509
		// (get) Token: 0x06005EA4 RID: 24228 RVA: 0x001807CA File Offset: 0x0017E9CA
		// (set) Token: 0x06005EA5 RID: 24229 RVA: 0x001807D2 File Offset: 0x0017E9D2
		internal DataSetList DataSets
		{
			get
			{
				return this.m_dataSets;
			}
			set
			{
				this.m_dataSets = value;
			}
		}

		// Token: 0x1700213E RID: 8510
		// (get) Token: 0x06005EA6 RID: 24230 RVA: 0x001807DB File Offset: 0x0017E9DB
		// (set) Token: 0x06005EA7 RID: 24231 RVA: 0x001807E3 File Offset: 0x0017E9E3
		public Guid ID
		{
			get
			{
				return this.m_ID;
			}
			set
			{
				this.m_ID = value;
			}
		}

		// Token: 0x1700213F RID: 8511
		// (get) Token: 0x06005EA8 RID: 24232 RVA: 0x001807EC File Offset: 0x0017E9EC
		internal DataSourceExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17002140 RID: 8512
		// (get) Token: 0x06005EA9 RID: 24233 RVA: 0x001807F4 File Offset: 0x0017E9F4
		// (set) Token: 0x06005EAA RID: 24234 RVA: 0x001807FC File Offset: 0x0017E9FC
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

		// Token: 0x17002141 RID: 8513
		// (get) Token: 0x06005EAB RID: 24235 RVA: 0x00180805 File Offset: 0x0017EA05
		// (set) Token: 0x06005EAC RID: 24236 RVA: 0x0018080D File Offset: 0x0017EA0D
		internal bool IsComplex
		{
			get
			{
				return this.m_isComplex;
			}
			set
			{
				this.m_isComplex = value;
			}
		}

		// Token: 0x17002142 RID: 8514
		// (get) Token: 0x06005EAD RID: 24237 RVA: 0x00180816 File Offset: 0x0017EA16
		// (set) Token: 0x06005EAE RID: 24238 RVA: 0x0018081E File Offset: 0x0017EA1E
		internal StringList ParameterNames
		{
			get
			{
				return this.m_parameterNames;
			}
			set
			{
				this.m_parameterNames = value;
			}
		}

		// Token: 0x17002143 RID: 8515
		// (get) Token: 0x06005EAF RID: 24239 RVA: 0x00180827 File Offset: 0x0017EA27
		// (set) Token: 0x06005EB0 RID: 24240 RVA: 0x0018082F File Offset: 0x0017EA2F
		public string SharedDataSourceReferencePath
		{
			get
			{
				return this.m_sharedDataSourceReferencePath;
			}
			set
			{
				this.m_sharedDataSourceReferencePath = value;
			}
		}

		// Token: 0x06005EB1 RID: 24241 RVA: 0x00180838 File Offset: 0x0017EA38
		internal void Initialize(InitializationContext context)
		{
			context.ObjectType = ObjectType.DataSource;
			context.ObjectName = this.m_name;
			this.InternalInitialize(context);
			if (this.m_dataSets != null)
			{
				for (int i = 0; i < this.m_dataSets.Count; i++)
				{
					Global.Tracer.Assert(this.m_dataSets[i] != null);
					this.m_dataSets[i].Initialize(context);
				}
			}
		}

		// Token: 0x06005EB2 RID: 24242 RVA: 0x001808AC File Offset: 0x0017EAAC
		internal string ResolveConnectionString(ReportProcessing.ReportProcessingContext pc, out DataSourceInfo dataSourceInfo)
		{
			dataSourceInfo = null;
			string text;
			if (pc.DataSourceInfos != null)
			{
				if (Guid.Empty != this.ID)
				{
					dataSourceInfo = pc.DataSourceInfos.GetByID(this.ID);
				}
				if (dataSourceInfo == null)
				{
					dataSourceInfo = pc.DataSourceInfos.GetByName(this.Name, pc.ReportContext);
				}
				if (dataSourceInfo == null)
				{
					throw new DataSourceNotFoundException(this.Name);
				}
				text = dataSourceInfo.GetConnectionString(pc.DataProtection);
				if (!dataSourceInfo.IsReference && text == null)
				{
					text = this.EvaluateConnectStringExpression(pc);
				}
			}
			else
			{
				if (this.DataSourceReference != null)
				{
					throw new DataSourceNotFoundException(this.Name);
				}
				text = this.EvaluateConnectStringExpression(pc);
			}
			return text;
		}

		// Token: 0x06005EB3 RID: 24243 RVA: 0x0018095C File Offset: 0x0017EB5C
		private void InternalInitialize(InitializationContext context)
		{
			context.ExprHostBuilder.DataSourceStart(this.m_name);
			if (this.m_connectString != null)
			{
				this.m_connectString.Initialize("ConnectString", context);
				context.ExprHostBuilder.DataSourceConnectString(this.m_connectString);
			}
			this.m_exprHostID = context.ExprHostBuilder.DataSourceEnd();
		}

		// Token: 0x06005EB4 RID: 24244 RVA: 0x001809B8 File Offset: 0x0017EBB8
		private void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.DataSourceHostsRemotable[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06005EB5 RID: 24245 RVA: 0x00180A08 File Offset: 0x0017EC08
		private string EvaluateConnectStringExpression(ReportProcessing.ProcessingContext processingContext)
		{
			if (this.m_connectString == null)
			{
				return null;
			}
			if (ExpressionInfo.Types.Constant == this.m_connectString.Type)
			{
				return this.m_connectString.Value;
			}
			Global.Tracer.Assert(processingContext.ReportRuntime != null);
			if (processingContext.ReportRuntime.ReportExprHost != null)
			{
				this.SetExprHost(processingContext.ReportRuntime.ReportExprHost, processingContext.ReportObjectModel);
			}
			StringResult stringResult = processingContext.ReportRuntime.EvaluateConnectString(this);
			if (stringResult.ErrorOccurred)
			{
				throw new ReportProcessingException(ErrorCode.rsDataSourceConnectStringProcessingError, new object[] { this.m_name });
			}
			return stringResult.Value;
		}

		// Token: 0x06005EB6 RID: 24246 RVA: 0x00180AA4 File Offset: 0x0017ECA4
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Transaction, Token.Boolean),
				new MemberInfo(MemberName.Type, Token.String),
				new MemberInfo(MemberName.ConnectString, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IntegratedSecurity, Token.Boolean),
				new MemberInfo(MemberName.Prompt, Token.String),
				new MemberInfo(MemberName.DataSourceReference, Token.String),
				new MemberInfo(MemberName.DataSets, ObjectType.DataSetList),
				new MemberInfo(MemberName.ID, Token.Guid),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.SharedDataSourceReferencePath, Token.String)
			});
		}

		// Token: 0x04003040 RID: 12352
		private string m_name;

		// Token: 0x04003041 RID: 12353
		private bool m_transaction;

		// Token: 0x04003042 RID: 12354
		private string m_type;

		// Token: 0x04003043 RID: 12355
		private ExpressionInfo m_connectString;

		// Token: 0x04003044 RID: 12356
		private bool m_integratedSecurity;

		// Token: 0x04003045 RID: 12357
		private string m_prompt;

		// Token: 0x04003046 RID: 12358
		private string m_dataSourceReference;

		// Token: 0x04003047 RID: 12359
		private DataSetList m_dataSets;

		// Token: 0x04003048 RID: 12360
		private Guid m_ID = Guid.Empty;

		// Token: 0x04003049 RID: 12361
		private int m_exprHostID = -1;

		// Token: 0x0400304A RID: 12362
		private string m_sharedDataSourceReferencePath;

		// Token: 0x0400304B RID: 12363
		[NonSerialized]
		private DataSourceExprHost m_exprHost;

		// Token: 0x0400304C RID: 12364
		[NonSerialized]
		private bool m_isComplex;

		// Token: 0x0400304D RID: 12365
		[NonSerialized]
		private StringList m_parameterNames;
	}
}
