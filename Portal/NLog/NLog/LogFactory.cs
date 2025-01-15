using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NLog.Internal;

namespace NLog
{
	// Token: 0x0200000C RID: 12
	public class LogFactory<T> : LogFactory where T : Logger
	{
		// Token: 0x060001CA RID: 458 RVA: 0x00002E4D File Offset: 0x0000104D
		public new T GetLogger(string name)
		{
			return (T)((object)base.GetLogger(name, typeof(T)));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00002E68 File Offset: 0x00001068
		[MethodImpl(MethodImplOptions.NoInlining)]
		public new T GetCurrentClassLogger()
		{
			string classFullName = StackTraceUsageUtils.GetClassFullName(new StackFrame(1, false));
			return this.GetLogger(classFullName);
		}
	}
}
