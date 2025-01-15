using System;
using System.IO;
using System.Reflection;
using System.Resources;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.DeltaLake;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library.DeltaLake
{
	// Token: 0x02001EE1 RID: 7905
	public sealed class DeltaLakeModule : Module45
	{
		// Token: 0x17002C15 RID: 11285
		// (get) Token: 0x06010A9B RID: 68251 RVA: 0x00395FBF File Offset: 0x003941BF
		public override string Name
		{
			get
			{
				return "DeltaLake";
			}
		}

		// Token: 0x17002C16 RID: 11286
		// (get) Token: 0x06010A9C RID: 68252 RVA: 0x00395FC6 File Offset: 0x003941C6
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "DeltaLake.Table";
						}
						throw new InvalidOperationException();
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17002C17 RID: 11287
		// (get) Token: 0x06010A9D RID: 68253 RVA: 0x00396001 File Offset: 0x00394201
		public override ResourceManager DocumentationResources
		{
			get
			{
				return Resources.ResourceManager;
			}
		}

		// Token: 0x06010A9E RID: 68254 RVA: 0x00396008 File Offset: 0x00394208
		protected override RecordValue GetModuleExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new DeltaLakeModule.DeltaLake.TableFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x06010A9F RID: 68255 RVA: 0x00396039 File Offset: 0x00394239
		private static void HookNewtonsoft()
		{
			if (!DeltaLakeModule.newtonsoftHooked)
			{
				AppDomain.CurrentDomain.AssemblyResolve += DeltaLakeModule.ResolveNewtonsoftHandler;
				DeltaLakeModule.newtonsoftHooked = true;
			}
		}

		// Token: 0x06010AA0 RID: 68256 RVA: 0x0039605E File Offset: 0x0039425E
		private static Assembly ResolveNewtonsoftHandler(object sender, ResolveEventArgs args)
		{
			if (args.Name.IndexOf("newtonsoft.json", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				return DeltaLakeModule.LoadAssembly(Path.Combine(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Cdm"), "Newtonsoft.Json.dll"));
			}
			return null;
		}

		// Token: 0x06010AA1 RID: 68257 RVA: 0x003960A0 File Offset: 0x003942A0
		private static Assembly LoadAssembly(string assemblyLocation)
		{
			Assembly assembly;
			try
			{
				assembly = Assembly.LoadFile(assemblyLocation);
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				assembly = null;
			}
			return assembly;
		}

		// Token: 0x040063A7 RID: 25511
		private Keys exportKeys;

		// Token: 0x040063A8 RID: 25512
		public const string DataSourceName = "DeltaLake.Table";

		// Token: 0x040063A9 RID: 25513
		private const string CdmDllFolderPath = "Cdm";

		// Token: 0x040063AA RID: 25514
		private const string NewtonsoftAssembly = "Newtonsoft.Json.dll";

		// Token: 0x040063AB RID: 25515
		private static bool newtonsoftHooked;

		// Token: 0x02001EE2 RID: 7906
		private enum Exports
		{
			// Token: 0x040063AD RID: 25517
			DeltaLakeTable,
			// Token: 0x040063AE RID: 25518
			Count
		}

		// Token: 0x02001EE3 RID: 7907
		public static class DeltaLake
		{
			// Token: 0x02001EE4 RID: 7908
			public class TableFunctionValue : NativeFunctionValue2<TableValue, TableValue, Value>
			{
				// Token: 0x06010AA3 RID: 68259 RVA: 0x003960E4 File Offset: 0x003942E4
				public TableFunctionValue(IEngineHost host)
					: base(TypeValue.Any, 1, "directory", TypeValue.Table, "options", DeltaLakeModule.DeltaLake.TableFunctionValue.optionsType)
				{
					this.host = host;
				}

				// Token: 0x06010AA4 RID: 68260 RVA: 0x00396110 File Offset: 0x00394310
				public override TableValue TypedInvoke(TableValue directory, Value options)
				{
					DeltaLakeModule.HookNewtonsoft();
					RecordValue recordValue = DeltaLakeOptions.OptionRecord.ValidateOptions(options, "DeltaLake.Table", false, false);
					return DeltaSource.New(new MashupStorage(this.host, directory, recordValue), null).CreateTable(null);
				}

				// Token: 0x040063AF RID: 25519
				private static readonly TypeValue optionsType = DeltaLakeOptions.OptionRecord.CreateRecordType().Nullable;

				// Token: 0x040063B0 RID: 25520
				private readonly IEngineHost host;
			}
		}
	}
}
