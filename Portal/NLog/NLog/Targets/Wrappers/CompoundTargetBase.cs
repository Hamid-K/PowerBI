using System;
using System.Collections.Generic;
using System.Text;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000066 RID: 102
	public abstract class CompoundTargetBase : Target
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x00016754 File Offset: 0x00014954
		protected CompoundTargetBase(params Target[] targets)
		{
			this.Targets = new List<Target>(targets);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00016768 File Offset: 0x00014968
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x00016770 File Offset: 0x00014970
		public IList<Target> Targets { get; private set; }

		// Token: 0x060008AE RID: 2222 RVA: 0x0001677C File Offset: 0x0001497C
		public override string ToString()
		{
			string text = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.Append("(");
			foreach (Target target in this.Targets)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(target.ToString());
				text = ", ";
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00016814 File Offset: 0x00014A14
		protected override void Write(LogEventInfo logEvent)
		{
			throw new NotSupportedException("This target must not be invoked in a synchronous way.");
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00016820 File Offset: 0x00014A20
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			AsyncHelpers.ForEachItemInParallel<Target>(this.Targets, asyncContinuation, delegate(Target t, AsyncContinuation c)
			{
				t.Flush(c);
			});
		}
	}
}
