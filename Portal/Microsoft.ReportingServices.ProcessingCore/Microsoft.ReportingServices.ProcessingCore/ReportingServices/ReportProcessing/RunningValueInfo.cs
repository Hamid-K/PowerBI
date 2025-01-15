using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000709 RID: 1801
	[Serializable]
	internal sealed class RunningValueInfo : DataAggregateInfo
	{
		// Token: 0x170023A5 RID: 9125
		// (get) Token: 0x060064A3 RID: 25763 RVA: 0x0018E115 File Offset: 0x0018C315
		// (set) Token: 0x060064A4 RID: 25764 RVA: 0x0018E11D File Offset: 0x0018C31D
		internal string Scope
		{
			get
			{
				return this.m_scope;
			}
			set
			{
				this.m_scope = value;
			}
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x0018E128 File Offset: 0x0018C328
		internal new RunningValueInfo DeepClone(InitializationContext context)
		{
			RunningValueInfo runningValueInfo = new RunningValueInfo();
			base.DeepCloneInternal(runningValueInfo, context);
			runningValueInfo.m_scope = context.EscalateScope(this.m_scope);
			return runningValueInfo;
		}

		// Token: 0x060064A6 RID: 25766 RVA: 0x0018E158 File Offset: 0x0018C358
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.DataAggregateInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.Scope, Token.String)
			});
		}

		// Token: 0x04003276 RID: 12918
		private string m_scope;
	}
}
