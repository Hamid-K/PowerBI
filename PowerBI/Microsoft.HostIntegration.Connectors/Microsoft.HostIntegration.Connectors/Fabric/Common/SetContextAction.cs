using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003D9 RID: 985
	internal class SetContextAction : IFailPointAction
	{
		// Token: 0x06002291 RID: 8849 RVA: 0x0006AB40 File Offset: 0x00068D40
		public SetContextAction(string action)
		{
			this.m_action = action;
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x0006AB4F File Offset: 0x00068D4F
		public void Invoke(FailPointContext context)
		{
			context["action"] = this.m_action;
		}

		// Token: 0x040015B9 RID: 5561
		public const string ActionProperty = "action";

		// Token: 0x040015BA RID: 5562
		private string m_action;
	}
}
