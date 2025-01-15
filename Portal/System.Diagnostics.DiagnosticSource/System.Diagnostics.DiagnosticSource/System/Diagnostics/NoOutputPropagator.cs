using System;
using System.Collections.Generic;

namespace System.Diagnostics
{
	// Token: 0x0200002D RID: 45
	internal sealed class NoOutputPropagator : DistributedContextPropagator
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00006440 File Offset: 0x00004640
		internal static DistributedContextPropagator Instance { get; } = new NoOutputPropagator();

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006447 File Offset: 0x00004647
		public override IReadOnlyCollection<string> Fields { get; } = LegacyPropagator.Instance.Fields;

		// Token: 0x06000187 RID: 391 RVA: 0x0000644F File Offset: 0x0000464F
		public override void Inject(Activity activity, object carrier, DistributedContextPropagator.PropagatorSetterCallback setter)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006451 File Offset: 0x00004651
		public override void ExtractTraceIdAndState(object carrier, DistributedContextPropagator.PropagatorGetterCallback getter, out string traceId, out string traceState)
		{
			LegacyPropagator.Instance.ExtractTraceIdAndState(carrier, getter, out traceId, out traceState);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006462 File Offset: 0x00004662
		public override IEnumerable<KeyValuePair<string, string>> ExtractBaggage(object carrier, DistributedContextPropagator.PropagatorGetterCallback getter)
		{
			return LegacyPropagator.Instance.ExtractBaggage(carrier, getter);
		}
	}
}
