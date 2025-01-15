using System;
using System.Collections.Generic;
using NLog.Config;
using NLog.Filters;
using NLog.Targets;

namespace NLog.Internal
{
	// Token: 0x02000148 RID: 328
	[NLogConfigurationItem]
	internal class TargetWithFilterChain
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x00028B19 File Offset: 0x00026D19
		public TargetWithFilterChain(Target target, IList<Filter> filterChain, FilterResult defaultResult)
		{
			this.Target = target;
			this.FilterChain = filterChain;
			this.DefaultResult = defaultResult;
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00028B36 File Offset: 0x00026D36
		public Target Target { get; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00028B3E File Offset: 0x00026D3E
		public IList<Filter> FilterChain { get; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00028B46 File Offset: 0x00026D46
		public FilterResult DefaultResult { get; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00028B4E File Offset: 0x00026D4E
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00028B56 File Offset: 0x00026D56
		public TargetWithFilterChain NextInChain { get; set; }

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00028B60 File Offset: 0x00026D60
		public StackTraceUsage GetStackTraceUsage()
		{
			StackTraceUsage? stackTraceUsage = this._stackTraceUsage;
			if (stackTraceUsage == null)
			{
				return StackTraceUsage.None;
			}
			return stackTraceUsage.GetValueOrDefault();
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00028B88 File Offset: 0x00026D88
		internal StackTraceUsage PrecalculateStackTraceUsage()
		{
			StackTraceUsage stackTraceUsage = StackTraceUsage.None;
			if (this.Target != null)
			{
				stackTraceUsage = this.Target.StackTraceUsage;
			}
			if (this.NextInChain != null && stackTraceUsage != StackTraceUsage.WithSource)
			{
				StackTraceUsage stackTraceUsage2 = this.NextInChain.PrecalculateStackTraceUsage();
				if (stackTraceUsage2 > stackTraceUsage)
				{
					stackTraceUsage = stackTraceUsage2;
				}
			}
			this._stackTraceUsage = new StackTraceUsage?(stackTraceUsage);
			return stackTraceUsage;
		}

		// Token: 0x0400043A RID: 1082
		private StackTraceUsage? _stackTraceUsage;
	}
}
