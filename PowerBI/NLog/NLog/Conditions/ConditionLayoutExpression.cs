using System;
using NLog.Layouts;

namespace NLog.Conditions
{
	// Token: 0x020001A5 RID: 421
	internal sealed class ConditionLayoutExpression : ConditionExpression
	{
		// Token: 0x060012FC RID: 4860 RVA: 0x00033948 File Offset: 0x00031B48
		public ConditionLayoutExpression(Layout layout)
		{
			this.Layout = layout;
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x00033957 File Offset: 0x00031B57
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x0003395F File Offset: 0x00031B5F
		public Layout Layout { get; private set; }

		// Token: 0x060012FF RID: 4863 RVA: 0x00033968 File Offset: 0x00031B68
		public override string ToString()
		{
			return this.Layout.ToString();
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00033975 File Offset: 0x00031B75
		protected override object EvaluateNode(LogEventInfo context)
		{
			return this.Layout.Render(context);
		}
	}
}
