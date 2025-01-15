using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Azure.Core.Pipeline
{
	// Token: 0x020000A8 RID: 168
	[NullableContext(1)]
	[Nullable(0)]
	internal class DiagnosticScopeFactory
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x00010088 File Offset: 0x0000E288
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001011C File Offset: 0x0000E31C
		public bool IsActivityEnabled { get; }

		// Token: 0x06000541 RID: 1345 RVA: 0x00010124 File Offset: 0x0000E324
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "The DiagnosticScope constructor is marked as RequiresUnreferencedCode because of the usage of the diagnosticSourceArgs parameter. Since we are passing in null here we can suppress this warning.")]
		public DiagnosticScope CreateScope(string name, ActivityKind kind = ActivityKind.Internal)
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

		// Token: 0x06000542 RID: 1346 RVA: 0x00010188 File Offset: 0x0000E388
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

		// Token: 0x04000227 RID: 551
		[Nullable(new byte[] { 2, 1, 1 })]
		private static Dictionary<string, DiagnosticListener> _listeners;

		// Token: 0x04000228 RID: 552
		[Nullable(2)]
		private readonly string _resourceProviderNamespace;

		// Token: 0x04000229 RID: 553
		[Nullable(2)]
		private readonly DiagnosticListener _source;

		// Token: 0x0400022A RID: 554
		private readonly bool _suppressNestedClientActivities;

		// Token: 0x0400022B RID: 555
		private readonly bool _isStable;

		// Token: 0x0400022C RID: 556
		[Nullable(new byte[] { 1, 1, 2 })]
		private static readonly ConcurrentDictionary<string, ActivitySource> ActivitySources = new ConcurrentDictionary<string, ActivitySource>();
	}
}
