using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006DB RID: 1755
	[Serializable]
	internal class ParameterValue
	{
		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x06005F30 RID: 24368 RVA: 0x00181AF7 File Offset: 0x0017FCF7
		// (set) Token: 0x06005F31 RID: 24369 RVA: 0x00181AFF File Offset: 0x0017FCFF
		internal string Name
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

		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x06005F32 RID: 24370 RVA: 0x00181B08 File Offset: 0x0017FD08
		// (set) Token: 0x06005F33 RID: 24371 RVA: 0x00181B10 File Offset: 0x0017FD10
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x06005F34 RID: 24372 RVA: 0x00181B19 File Offset: 0x0017FD19
		// (set) Token: 0x06005F35 RID: 24373 RVA: 0x00181B21 File Offset: 0x0017FD21
		internal ExpressionInfo Omit
		{
			get
			{
				return this.m_omit;
			}
			set
			{
				this.m_omit = value;
			}
		}

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x06005F36 RID: 24374 RVA: 0x00181B2A File Offset: 0x0017FD2A
		// (set) Token: 0x06005F37 RID: 24375 RVA: 0x00181B32 File Offset: 0x0017FD32
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

		// Token: 0x17002179 RID: 8569
		// (get) Token: 0x06005F38 RID: 24376 RVA: 0x00181B3B File Offset: 0x0017FD3B
		// (set) Token: 0x06005F39 RID: 24377 RVA: 0x00181B43 File Offset: 0x0017FD43
		internal ParamExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
			set
			{
				this.m_exprHost = value;
			}
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x00181B4C File Offset: 0x0017FD4C
		internal void Initialize(InitializationContext context, bool queryParam)
		{
			if (this.m_value != null)
			{
				this.m_value.Initialize("Value", context);
				if (!queryParam)
				{
					context.ExprHostBuilder.GenericValue(this.m_value);
				}
				else
				{
					context.ExprHostBuilder.QueryParameterValue(this.m_value);
				}
			}
			if (this.m_omit != null)
			{
				this.m_omit.Initialize("Omit", context);
				context.ExprHostBuilder.ParameterOmit(this.m_omit);
			}
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x00181BC6 File Offset: 0x0017FDC6
		internal void SetExprHost(IList<ParamExprHost> paramExprHosts, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				Global.Tracer.Assert(paramExprHosts != null && reportObjectModel != null);
				this.m_exprHost = paramExprHosts[this.m_exprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x00181C04 File Offset: 0x0017FE04
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.Value, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.Omit, ObjectType.ExpressionInfo)
			});
		}

		// Token: 0x0400307D RID: 12413
		private string m_name;

		// Token: 0x0400307E RID: 12414
		private ExpressionInfo m_value;

		// Token: 0x0400307F RID: 12415
		private ExpressionInfo m_omit;

		// Token: 0x04003080 RID: 12416
		private int m_exprHostID = -1;

		// Token: 0x04003081 RID: 12417
		[NonSerialized]
		private ParamExprHost m_exprHost;
	}
}
