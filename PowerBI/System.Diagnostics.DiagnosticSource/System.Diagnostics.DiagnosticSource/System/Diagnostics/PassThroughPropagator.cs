using System;
using System.Collections.Generic;

namespace System.Diagnostics
{
	// Token: 0x0200002E RID: 46
	internal sealed class PassThroughPropagator : DistributedContextPropagator
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00006494 File Offset: 0x00004694
		internal static DistributedContextPropagator Instance { get; } = new PassThroughPropagator();

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000649B File Offset: 0x0000469B
		public override IReadOnlyCollection<string> Fields { get; } = LegacyPropagator.Instance.Fields;

		// Token: 0x0600018E RID: 398 RVA: 0x000064A4 File Offset: 0x000046A4
		public override void Inject(Activity activity, object carrier, DistributedContextPropagator.PropagatorSetterCallback setter)
		{
			if (setter == null)
			{
				return;
			}
			string text;
			string text2;
			bool flag;
			IEnumerable<KeyValuePair<string, string>> enumerable;
			PassThroughPropagator.GetRootId(out text, out text2, out flag, out enumerable);
			if (text == null)
			{
				return;
			}
			setter(carrier, flag ? "traceparent" : "Request-Id", text);
			if (!string.IsNullOrEmpty(text2))
			{
				setter(carrier, "tracestate", text2);
			}
			if (enumerable != null)
			{
				DistributedContextPropagator.InjectBaggage(carrier, enumerable, setter);
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000064FD File Offset: 0x000046FD
		public override void ExtractTraceIdAndState(object carrier, DistributedContextPropagator.PropagatorGetterCallback getter, out string traceId, out string traceState)
		{
			LegacyPropagator.Instance.ExtractTraceIdAndState(carrier, getter, out traceId, out traceState);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000650E File Offset: 0x0000470E
		public override IEnumerable<KeyValuePair<string, string>> ExtractBaggage(object carrier, DistributedContextPropagator.PropagatorGetterCallback getter)
		{
			return LegacyPropagator.Instance.ExtractBaggage(carrier, getter);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000651C File Offset: 0x0000471C
		private static void GetRootId(out string parentId, out string traceState, out bool isW3c, out IEnumerable<KeyValuePair<string, string>> baggage)
		{
			Activity activity = Activity.Current;
			for (;;)
			{
				Activity activity2 = ((activity != null) ? activity.Parent : null);
				if (activity2 == null)
				{
					break;
				}
				activity = activity2;
			}
			traceState = ((activity != null) ? activity.TraceStateString : null);
			parentId = ((activity != null) ? activity.ParentId : null) ?? ((activity != null) ? activity.Id : null);
			ActivityContext activityContext;
			isW3c = parentId != null && Activity.TryConvertIdToContext(parentId, traceState, out activityContext);
			baggage = ((activity != null) ? activity.Baggage : null);
		}
	}
}
