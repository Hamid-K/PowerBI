using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class DistributedContextPropagator
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000171 RID: 369
		public abstract IReadOnlyCollection<string> Fields { get; }

		// Token: 0x06000172 RID: 370
		[NullableContext(2)]
		public abstract void Inject(Activity activity, object carrier, DistributedContextPropagator.PropagatorSetterCallback setter);

		// Token: 0x06000173 RID: 371
		[NullableContext(2)]
		public abstract void ExtractTraceIdAndState(object carrier, DistributedContextPropagator.PropagatorGetterCallback getter, out string traceId, out string traceState);

		// Token: 0x06000174 RID: 372
		[NullableContext(2)]
		[return: Nullable(new byte[] { 2, 0, 1, 2 })]
		public abstract IEnumerable<KeyValuePair<string, string>> ExtractBaggage(object carrier, DistributedContextPropagator.PropagatorGetterCallback getter);

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006001 File Offset: 0x00004201
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00006008 File Offset: 0x00004208
		public static DistributedContextPropagator Current
		{
			get
			{
				return DistributedContextPropagator.s_current;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				DistributedContextPropagator.s_current = value;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000601F File Offset: 0x0000421F
		public static DistributedContextPropagator CreateDefaultPropagator()
		{
			return LegacyPropagator.Instance;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006026 File Offset: 0x00004226
		public static DistributedContextPropagator CreatePassThroughPropagator()
		{
			return PassThroughPropagator.Instance;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000602D File Offset: 0x0000422D
		public static DistributedContextPropagator CreateNoOutputPropagator()
		{
			return NoOutputPropagator.Instance;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006034 File Offset: 0x00004234
		internal static void InjectBaggage(object carrier, IEnumerable<KeyValuePair<string, string>> baggage, DistributedContextPropagator.PropagatorSetterCallback setter)
		{
			using (IEnumerator<KeyValuePair<string, string>> enumerator = baggage.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					StringBuilder stringBuilder = new StringBuilder();
					do
					{
						KeyValuePair<string, string> keyValuePair = enumerator.Current;
						stringBuilder.Append(WebUtility.UrlEncode(keyValuePair.Key)).Append('=').Append(WebUtility.UrlEncode(keyValuePair.Value))
							.Append(", ");
					}
					while (enumerator.MoveNext());
					setter(carrier, "Correlation-Context", stringBuilder.ToString(0, stringBuilder.Length - 2));
				}
			}
		}

		// Token: 0x04000086 RID: 134
		private static DistributedContextPropagator s_current = DistributedContextPropagator.CreateDefaultPropagator();

		// Token: 0x04000087 RID: 135
		internal const string TraceParent = "traceparent";

		// Token: 0x04000088 RID: 136
		internal const string RequestId = "Request-Id";

		// Token: 0x04000089 RID: 137
		internal const string TraceState = "tracestate";

		// Token: 0x0400008A RID: 138
		internal const string Baggage = "baggage";

		// Token: 0x0400008B RID: 139
		internal const string CorrelationContext = "Correlation-Context";

		// Token: 0x0400008C RID: 140
		internal const char Space = ' ';

		// Token: 0x0400008D RID: 141
		internal const char Tab = '\t';

		// Token: 0x0400008E RID: 142
		internal const char Comma = ',';

		// Token: 0x0400008F RID: 143
		internal const char Semicolon = ';';

		// Token: 0x04000090 RID: 144
		internal const string CommaWithSpace = ", ";

		// Token: 0x04000091 RID: 145
		internal static readonly char[] s_trimmingSpaceCharacters = new char[] { ' ', '\t' };

		// Token: 0x02000083 RID: 131
		// (Invoke) Token: 0x06000350 RID: 848
		[NullableContext(0)]
		public delegate void PropagatorGetterCallback(object carrier, [Nullable(1)] string fieldName, out string fieldValue, [Nullable(new byte[] { 2, 1 })] out IEnumerable<string> fieldValues);

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x06000354 RID: 852
		[NullableContext(0)]
		public delegate void PropagatorSetterCallback([Nullable(2)] object carrier, string fieldName, string fieldValue);
	}
}
