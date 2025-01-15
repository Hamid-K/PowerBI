using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;

namespace System.Diagnostics
{
	// Token: 0x0200002C RID: 44
	internal sealed class LegacyPropagator : DistributedContextPropagator
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000060FD File Offset: 0x000042FD
		internal static DistributedContextPropagator Instance { get; } = new LegacyPropagator();

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00006104 File Offset: 0x00004304
		public override IReadOnlyCollection<string> Fields { get; } = new ReadOnlyCollection<string>(new string[] { "traceparent", "Request-Id", "tracestate", "baggage", "Correlation-Context" });

		// Token: 0x0600017F RID: 383 RVA: 0x0000610C File Offset: 0x0000430C
		public override void Inject(Activity activity, object carrier, DistributedContextPropagator.PropagatorSetterCallback setter)
		{
			if (activity == null || setter == null)
			{
				return;
			}
			string id = activity.Id;
			if (id == null)
			{
				return;
			}
			if (activity.IdFormat == ActivityIdFormat.W3C)
			{
				setter(carrier, "traceparent", id);
				if (!string.IsNullOrEmpty(activity.TraceStateString))
				{
					setter(carrier, "tracestate", activity.TraceStateString);
				}
			}
			else
			{
				setter(carrier, "Request-Id", id);
			}
			DistributedContextPropagator.InjectBaggage(carrier, activity.Baggage, setter);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000617C File Offset: 0x0000437C
		public override void ExtractTraceIdAndState(object carrier, DistributedContextPropagator.PropagatorGetterCallback getter, out string traceId, out string traceState)
		{
			if (getter == null)
			{
				traceId = null;
				traceState = null;
				return;
			}
			IEnumerable<string> enumerable;
			getter(carrier, "traceparent", out traceId, out enumerable);
			if (traceId == null)
			{
				getter(carrier, "Request-Id", out traceId, out enumerable);
			}
			getter(carrier, "tracestate", out traceState, out enumerable);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000061C8 File Offset: 0x000043C8
		public override IEnumerable<KeyValuePair<string, string>> ExtractBaggage(object carrier, DistributedContextPropagator.PropagatorGetterCallback getter)
		{
			if (getter == null)
			{
				return null;
			}
			string text;
			IEnumerable<string> enumerable;
			getter(carrier, "baggage", out text, out enumerable);
			IEnumerable<KeyValuePair<string, string>> enumerable2 = null;
			if (text == null || !LegacyPropagator.TryExtractBaggage(text, out enumerable2))
			{
				getter(carrier, "Correlation-Context", out text, out enumerable);
				if (text != null)
				{
					LegacyPropagator.TryExtractBaggage(text, out enumerable2);
				}
			}
			return enumerable2;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006218 File Offset: 0x00004418
		internal static bool TryExtractBaggage(string baggageString, out IEnumerable<KeyValuePair<string, string>> baggage)
		{
			baggage = null;
			List<KeyValuePair<string, string>> list = null;
			if (string.IsNullOrEmpty(baggageString))
			{
				return true;
			}
			int num = 0;
			for (;;)
			{
				if (num >= baggageString.Length || (baggageString[num] != ' ' && baggageString[num] != '\t'))
				{
					if (num >= baggageString.Length)
					{
						break;
					}
					int num2 = num;
					while (num < baggageString.Length && baggageString[num] != ' ' && baggageString[num] != '\t' && baggageString[num] != '=')
					{
						num++;
					}
					if (num >= baggageString.Length)
					{
						break;
					}
					int num3 = num;
					if (baggageString[num] != '=')
					{
						while (num < baggageString.Length && (baggageString[num] == ' ' || baggageString[num] == '\t'))
						{
							num++;
						}
						if (num >= baggageString.Length || baggageString[num] != '=')
						{
							break;
						}
					}
					num++;
					while (num < baggageString.Length && (baggageString[num] == ' ' || baggageString[num] == '\t'))
					{
						num++;
					}
					if (num >= baggageString.Length)
					{
						break;
					}
					int num4 = num;
					while (num < baggageString.Length && baggageString[num] != ' ' && baggageString[num] != '\t' && baggageString[num] != ',' && baggageString[num] != ';')
					{
						num++;
					}
					if (num2 < num3 && num4 < num)
					{
						if (list == null)
						{
							list = new List<KeyValuePair<string, string>>();
						}
						list.Insert(0, new KeyValuePair<string, string>(WebUtility.UrlDecode(baggageString.Substring(num2, num3 - num2)).Trim(DistributedContextPropagator.s_trimmingSpaceCharacters), WebUtility.UrlDecode(baggageString.Substring(num4, num - num4)).Trim(DistributedContextPropagator.s_trimmingSpaceCharacters)));
					}
					while (num < baggageString.Length && baggageString[num] != ',')
					{
						num++;
					}
					num++;
					if (num >= baggageString.Length)
					{
						break;
					}
				}
				else
				{
					num++;
				}
			}
			baggage = list;
			return list != null;
		}
	}
}
