using System;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;

[assembly: AssemblyVersion("6.0.0.1")]
[assembly: AllowPartiallyTrustedCallers]
[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
[assembly: AssemblyDefaultAlias("System.Diagnostics.DiagnosticSource")]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: AssemblyMetadata(".NETFrameworkAssembly", "")]
[assembly: AssemblyMetadata("Serviceable", "True")]
[assembly: AssemblyMetadata("PreferInbox", "True")]
[assembly: AssemblyMetadata("IsTrimmable", "True")]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyCopyright("© Microsoft Corporation. All rights reserved.")]
[assembly: AssemblyDescription("Provides Classes that allow you to decouple code logging rich (unserializable) diagnostics/telemetry (e.g. framework) from code that consumes it (e.g. tools)\r\n\r\nCommonly Used Types:\r\nSystem.Diagnostics.DiagnosticListener\r\nSystem.Diagnostics.DiagnosticSource")]
[assembly: AssemblyFileVersion("6.0.1523.11507")]
[assembly: AssemblyInformationalVersion("6.0.15+5edef4b20babd4c3ddac7460e536f86fd0f2d724")]
[assembly: AssemblyProduct("Microsoft® .NET")]
[assembly: AssemblyTitle("System.Diagnostics.DiagnosticSource")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/dotnet/runtime")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[module: NullablePublicOnly(false)]
