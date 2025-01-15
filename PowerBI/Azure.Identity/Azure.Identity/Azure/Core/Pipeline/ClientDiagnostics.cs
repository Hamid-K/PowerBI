using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	internal class ClientDiagnostics : DiagnosticScopeFactory
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003662 File Offset: 0x00001862
		public ClientDiagnostics(ClientOptions options, bool? suppressNestedClientActivities = null)
			: this(options.GetType().Namespace, ClientDiagnostics.GetResourceProviderNamespace(options.GetType().Assembly), options.Diagnostics, suppressNestedClientActivities)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000368C File Offset: 0x0000188C
		public ClientDiagnostics(string optionsNamespace, [Nullable(2)] string providerNamespace, DiagnosticsOptions diagnosticsOptions, bool? suppressNestedClientActivities = null)
			: base(optionsNamespace, providerNamespace, diagnosticsOptions.IsDistributedTracingEnabled, suppressNestedClientActivities.GetValueOrDefault(true), true)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000036A5 File Offset: 0x000018A5
		internal static HttpMessageSanitizer CreateMessageSanitizer(DiagnosticsOptions diagnostics)
		{
			return new HttpMessageSanitizer(diagnostics.LoggedQueryParameters.ToArray<string>(), diagnostics.LoggedHeaderNames.ToArray<string>(), "REDACTED");
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000036C8 File Offset: 0x000018C8
		[return: Nullable(2)]
		internal static string GetResourceProviderNamespace(Assembly assembly)
		{
			foreach (CustomAttributeData customAttributeData in assembly.GetCustomAttributesData())
			{
				if (customAttributeData.AttributeType.FullName == "Azure.Core.AzureResourceProviderNamespaceAttribute")
				{
					return customAttributeData.ConstructorArguments.Single<CustomAttributeTypedArgument>().Value as string;
				}
			}
			return null;
		}
	}
}
