using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.CSharp;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000202 RID: 514
	public class ExtendedCSharpCodeProvider
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x0002FD76 File Offset: 0x0002DF76
		public ExtendedCSharpCodeProvider(string source, string[] references, string informationalTag = "")
		{
			this.m_source = source;
			this.m_references = references;
			this.m_informationalTag = informationalTag;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsDebugging()
		{
			return false;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0002FD94 File Offset: 0x0002DF94
		public Assembly BuildAssembly(ExtendedCSharpCodeProviderBuildOptions options, string outputAssembly = null)
		{
			Assembly compiledAssembly;
			try
			{
				ExtendedCSharpCodeProvider.s_buildAssemblySemaphore.Wait();
				using (CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", "v4.0" } }))
				{
					bool flag = this.IsDebugging();
					bool flag2 = options.HasFlag(ExtendedCSharpCodeProviderBuildOptions.CompileAssemblyToDisk);
					CompilerParameters compilerParameters = new CompilerParameters
					{
						GenerateInMemory = (!flag && !flag2),
						IncludeDebugInformation = flag,
						WarningLevel = 0,
						CompilerOptions = "/nologo /nostdlib /optimize",
						OutputAssembly = outputAssembly,
						TempFiles = new TempFileCollection(Path.GetTempPath(), flag)
					};
					compilerParameters.ReferencedAssemblies.AddRange(this.m_references);
					try
					{
						CompilerResults compilerResults = csharpCodeProvider.CompileAssemblyFromSource(compilerParameters, new string[] { this.m_source });
						if (compilerResults.Errors.HasErrors)
						{
							string errorsInfo = ExtendedCSharpCodeProvider.GetErrorsInfo(compilerResults.Errors);
							throw new CodeGenerationException("AssemblyBuilder failed to compile assembly. Output: " + errorsInfo);
						}
						compiledAssembly = compilerResults.CompiledAssembly;
					}
					catch (ExecutionEngineException ex)
					{
						throw new ExecutionEngineException(new StringBuilder().Append('[').Append(base.GetType().Name).Append(']')
							.Append(' ')
							.Append("FAILURE")
							.Append(' ')
							.Append("InformationalTag ")
							.Append('\'')
							.Append(this.m_informationalTag)
							.Append('\'')
							.Append(' ')
							.Append("Compiler options: ")
							.Append("GenerateInMemory=")
							.Append(compilerParameters.GenerateInMemory)
							.Append("; ")
							.Append("IncludeDebugInformation=")
							.Append(compilerParameters.IncludeDebugInformation)
							.Append("; ")
							.Append("CompilerOptions=")
							.Append(compilerParameters.CompilerOptions)
							.Append("; ")
							.Append("TempFiles=")
							.Append('{')
							.Append("TempDir=")
							.Append(compilerParameters.TempFiles.TempDir)
							.Append(", ")
							.Append("KeepFiles=")
							.Append(compilerParameters.TempFiles.KeepFiles)
							.Append('}')
							.Append("; ")
							.Append("references=")
							.Append('[')
							.Append(string.Join(", ", this.m_references))
							.Append(']')
							.ToString(), ex);
					}
				}
			}
			finally
			{
				ExtendedCSharpCodeProvider.s_buildAssemblySemaphore.Release();
			}
			return compiledAssembly;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00030058 File Offset: 0x0002E258
		private static string GetErrorsInfo(CompilerErrorCollection compilerErrorCollection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format(CultureInfo.CurrentCulture, "There are {0} errors: ", new object[] { compilerErrorCollection.Count }));
			foreach (object obj in compilerErrorCollection)
			{
				stringBuilder.AppendLine(obj.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000562 RID: 1378
		private const string c_enableCodeGenDebuggingTweakName = "Microsoft.Cloud.Platform.Utils.EnableCodeGenDebugging";

		// Token: 0x04000563 RID: 1379
		private static Tweak<bool> sm_enableCodeGenDebuggingTweak = Anchor.Tweaks.RegisterTweak<bool>("Microsoft.Cloud.Platform.Utils.EnableCodeGenDebugging", "When set, generated code will write the source and PDB files to disk for debugging.", false);

		// Token: 0x04000564 RID: 1380
		private static readonly SemaphoreSlim s_buildAssemblySemaphore = new SemaphoreSlim(2);

		// Token: 0x04000565 RID: 1381
		private string m_informationalTag;

		// Token: 0x04000566 RID: 1382
		private string m_source;

		// Token: 0x04000567 RID: 1383
		private string[] m_references;
	}
}
