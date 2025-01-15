using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Azure.Core.Pipeline
{
	// Token: 0x020000A4 RID: 164
	[NullableContext(1)]
	[Nullable(0)]
	internal class ClientDiagnostics : DiagnosticScopeFactory
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x0000FCAF File Offset: 0x0000DEAF
		public ClientDiagnostics(ClientOptions options, bool? suppressNestedClientActivities = null)
			: this(options.GetType().Namespace, ClientDiagnostics.GetResourceProviderNamespace(options.GetType().Assembly), options.Diagnostics, suppressNestedClientActivities)
		{
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000FCD9 File Offset: 0x0000DED9
		public ClientDiagnostics(string optionsNamespace, [Nullable(2)] string providerNamespace, DiagnosticsOptions diagnosticsOptions, bool? suppressNestedClientActivities = null)
			: base(optionsNamespace, providerNamespace, diagnosticsOptions.IsDistributedTracingEnabled, suppressNestedClientActivities.GetValueOrDefault(true), true)
		{
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000FCF2 File Offset: 0x0000DEF2
		internal static HttpMessageSanitizer CreateMessageSanitizer(DiagnosticsOptions diagnostics)
		{
			return new HttpMessageSanitizer(diagnostics.LoggedQueryParameters.ToArray<string>(), diagnostics.LoggedHeaderNames.ToArray<string>(), "REDACTED");
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000FD14 File Offset: 0x0000DF14
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
