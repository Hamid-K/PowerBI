using System;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;

namespace Microsoft.Mashup.Engine1.Library._Module
{
	// Token: 0x02000960 RID: 2400
	internal sealed class ModuleModule : Module
	{
		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x000E64CF File Offset: 0x000E46CF
		public override string Name
		{
			get
			{
				return "Module";
			}
		}

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06004473 RID: 17523 RVA: 0x000E64D6 File Offset: 0x000E46D6
		public override Keys ExportKeys
		{
			get
			{
				if (ModuleModule.exportKeys == null)
				{
					ModuleModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Module.Versions";
						}
						throw new InvalidOperationException();
					});
				}
				return ModuleModule.exportKeys;
			}
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x000E6510 File Offset: 0x000E4710
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new ModuleModule.VersionsFunctionValue(host);
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x04002475 RID: 9333
		private static Keys exportKeys;

		// Token: 0x02000961 RID: 2401
		private enum Exports
		{
			// Token: 0x04002477 RID: 9335
			Module_Versions,
			// Token: 0x04002478 RID: 9336
			Count
		}

		// Token: 0x02000962 RID: 2402
		private class VersionsFunctionValue : NativeFunctionValue0<RecordValue>
		{
			// Token: 0x06004476 RID: 17526 RVA: 0x000E6541 File Offset: 0x000E4741
			public VersionsFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Record)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x06004477 RID: 17527 RVA: 0x000E6558 File Offset: 0x000E4758
			public override RecordValue TypedInvoke()
			{
				RecordBuilder recordBuilder = new RecordBuilder(2);
				ExtensionModule extensionModule = this.engineHost.QueryService<ExtensionModule>();
				if (extensionModule != null && extensionModule.Version != null)
				{
					recordBuilder.Add(extensionModule.Name, TextValue.New(extensionModule.Version), TypeValue.Text);
				}
				recordBuilder.Add("Core", TextValue.New(ModuleModule.VersionsFunctionValue.CoreVersion), TypeValue.Text);
				return recordBuilder.ToRecord();
			}

			// Token: 0x170015E1 RID: 5601
			// (get) Token: 0x06004478 RID: 17528 RVA: 0x000E65C4 File Offset: 0x000E47C4
			private static string CoreVersion
			{
				get
				{
					if (ModuleModule.VersionsFunctionValue.coreVersion == null)
					{
						try
						{
							ModuleModule.VersionsFunctionValue.coreVersion = FileVersionInfo.GetVersionInfo(typeof(Module).Assembly.Location).FileVersion;
						}
						catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
						{
							ModuleModule.VersionsFunctionValue.coreVersion = "2.100.0.0";
						}
					}
					return ModuleModule.VersionsFunctionValue.coreVersion;
				}
			}

			// Token: 0x04002479 RID: 9337
			private static string coreVersion;

			// Token: 0x0400247A RID: 9338
			private readonly IEngineHost engineHost;
		}
	}
}
