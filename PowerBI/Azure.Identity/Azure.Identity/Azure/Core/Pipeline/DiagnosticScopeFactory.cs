using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Azure.Core.Pipeline
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	[Nullable(0)]
	internal class DiagnosticScopeFactory
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003998 File Offset: 0x00001B98
		public DiagnosticScopeFactory(string clientNamespace, [Nullable(2)] string resourceProviderNamespace, bool isActivityEnabled, bool suppressNestedClientActivities = true, bool isStable = false)
		{
			this._resourceProviderNamespace = resourceProviderNamespace;
			this.IsActivityEnabled = isActivityEnabled;
			this._suppressNestedClientActivities = suppressNestedClientActivities;
			this._isStable = isStable;
			if (this.IsActivityEnabled)
			{
				Dictionary<string, DiagnosticListener> dictionary = LazyInitializer.EnsureInitialized<Dictionary<string, DiagnosticListener>>(ref DiagnosticScopeFactory._listeners);
				Dictionary<string, DiagnosticListener> dictionary2 = dictionary;
				lock (dictionary2)
				{
					if (!dictionary.TryGetValue(clientNamespace, out this._source))
					{
						this._source = new DiagnosticListener(clientNamespace);
						dictionary[clientNamespace] = this._source;
					}
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003A2C File Offset: 0x00001C2C
		public bool IsActivityEnabled { get; }

		// Token: 0x06000083 RID: 131 RVA: 0x00003A34 File Offset: 0x00001C34
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The DiagnosticScope constructor is marked as RequiresUnreferencedCode because of the usage of the diagnosticSourceArgs parameter. Since we are passing in null here we can suppress this warning.")]
		public DiagnosticScope CreateScope(string name, ActivityKind kind = 0)
		{
			if (this._source == null)
			{
				return default(DiagnosticScope);
			}
			DiagnosticScope diagnosticScope = new DiagnosticScope(name, this._source, null, this.GetActivitySource(this._source.Name, name), kind, this._suppressNestedClientActivities);
			if (this._resourceProviderNamespace != null)
			{
				diagnosticScope.AddAttribute("az.namespace", this._resourceProviderNamespace);
			}
			return diagnosticScope;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003A98 File Offset: 0x00001C98
		[return: Nullable(2)]
		private ActivitySource GetActivitySource(string ns, string name)
		{
			if (!(this._isStable | ActivityExtensions.SupportsActivitySource))
			{
				return null;
			}
			int num = name.IndexOf(".", StringComparison.OrdinalIgnoreCase);
			string text = ns + "." + ((num < 0) ? name : name.Substring(0, num));
			return DiagnosticScopeFactory.ActivitySources.GetOrAdd(text, (string n) => new ActivitySource(n, ""));
		}

		// Token: 0x0400004B RID: 75
		[Nullable(new byte[] { 2, 1, 1 })]
		private static Dictionary<string, DiagnosticListener> _listeners;

		// Token: 0x0400004C RID: 76
		[Nullable(2)]
		private readonly string _resourceProviderNamespace;

		// Token: 0x0400004D RID: 77
		[Nullable(2)]
		private readonly DiagnosticListener _source;

		// Token: 0x0400004E RID: 78
		private readonly bool _suppressNestedClientActivities;

		// Token: 0x0400004F RID: 79
		private readonly bool _isStable;

		// Token: 0x04000050 RID: 80
		[Nullable(new byte[] { 1, 1, 2 })]
		private static readonly ConcurrentDictionary<string, ActivitySource> ActivitySources = new ConcurrentDictionary<string, ActivitySource>();
	}
}
