using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006E9 RID: 1769
	[Serializable]
	internal sealed class ActiveXControl : ReportItem
	{
		// Token: 0x060060D6 RID: 24790 RVA: 0x001856CD File Offset: 0x001838CD
		internal ActiveXControl(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x060060D7 RID: 24791 RVA: 0x001856D6 File Offset: 0x001838D6
		internal ActiveXControl(int id, ReportItem parent)
			: base(id, parent)
		{
			this.m_parameters = new ParameterValueList();
		}

		// Token: 0x1700221B RID: 8731
		// (get) Token: 0x060060D8 RID: 24792 RVA: 0x001856EB File Offset: 0x001838EB
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.ActiveXControl;
			}
		}

		// Token: 0x1700221C RID: 8732
		// (get) Token: 0x060060D9 RID: 24793 RVA: 0x001856EF File Offset: 0x001838EF
		// (set) Token: 0x060060DA RID: 24794 RVA: 0x001856F7 File Offset: 0x001838F7
		internal string ClassID
		{
			get
			{
				return this.m_classID;
			}
			set
			{
				this.m_classID = value;
			}
		}

		// Token: 0x1700221D RID: 8733
		// (get) Token: 0x060060DB RID: 24795 RVA: 0x00185700 File Offset: 0x00183900
		// (set) Token: 0x060060DC RID: 24796 RVA: 0x00185708 File Offset: 0x00183908
		internal string CodeBase
		{
			get
			{
				return this.m_codeBase;
			}
			set
			{
				this.m_codeBase = value;
			}
		}

		// Token: 0x1700221E RID: 8734
		// (get) Token: 0x060060DD RID: 24797 RVA: 0x00185711 File Offset: 0x00183911
		// (set) Token: 0x060060DE RID: 24798 RVA: 0x00185719 File Offset: 0x00183919
		internal ParameterValueList Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x060060DF RID: 24799 RVA: 0x00185724 File Offset: 0x00183924
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.ExprHostBuilder.ActiveXControlStart(this.m_name);
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, false, false);
			}
			if (this.m_parameters != null)
			{
				for (int i = 0; i < this.m_parameters.Count; i++)
				{
					ParameterValue parameterValue = this.m_parameters[i];
					context.ExprHostBuilder.ActiveXControlParameterStart();
					parameterValue.Initialize(context, false);
					parameterValue.ExprHostID = context.ExprHostBuilder.ActiveXControlParameterEnd();
				}
			}
			base.ExprHostID = context.ExprHostBuilder.ActiveXControlEnd();
			return true;
		}

		// Token: 0x060060E0 RID: 24800 RVA: 0x001857E0 File Offset: 0x001839E0
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.ActiveXControlHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_exprHost.ParameterHostsRemotable != null)
				{
					Global.Tracer.Assert(this.m_parameters != null);
					for (int i = this.m_parameters.Count - 1; i >= 0; i--)
					{
						this.m_parameters[i].SetExprHost(this.m_exprHost.ParameterHostsRemotable, reportObjectModel);
					}
				}
			}
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x00185888 File Offset: 0x00183A88
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, new MemberInfoList
			{
				new MemberInfo(MemberName.ClassID, Token.String),
				new MemberInfo(MemberName.CodeBase, Token.String),
				new MemberInfo(MemberName.Parameters, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ParameterValueList)
			});
		}

		// Token: 0x0400311F RID: 12575
		private string m_classID;

		// Token: 0x04003120 RID: 12576
		private string m_codeBase;

		// Token: 0x04003121 RID: 12577
		private ParameterValueList m_parameters;

		// Token: 0x04003122 RID: 12578
		[NonSerialized]
		private ActiveXControlExprHost m_exprHost;
	}
}
