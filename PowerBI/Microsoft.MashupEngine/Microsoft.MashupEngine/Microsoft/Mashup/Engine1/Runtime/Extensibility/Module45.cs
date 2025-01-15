using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x02001714 RID: 5908
	public abstract class Module45 : Module
	{
		// Token: 0x17002747 RID: 10055
		// (get) Token: 0x06009637 RID: 38455 RVA: 0x000020FA File Offset: 0x000002FA
		public virtual ResourceManager DocumentationResources
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002748 RID: 10056
		// (get) Token: 0x06009638 RID: 38456 RVA: 0x001F1B57 File Offset: 0x001EFD57
		public override string Location
		{
			get
			{
				return base.GetType().Assembly.Location;
			}
		}

		// Token: 0x17002749 RID: 10057
		// (get) Token: 0x06009639 RID: 38457 RVA: 0x001F1B69 File Offset: 0x001EFD69
		public virtual string AssemblyName
		{
			get
			{
				return "Microsoft.MashupEngine.Library45.dll";
			}
		}

		// Token: 0x1700274A RID: 10058
		// (get) Token: 0x0600963A RID: 38458 RVA: 0x001F1B70 File Offset: 0x001EFD70
		private Module45 UnderlyingModule
		{
			get
			{
				if (this.underlyingModule == null)
				{
					Func<Module45> func = this.LoadModuleConstructor();
					this.underlyingModule = ((func != null) ? func() : this);
				}
				return this.underlyingModule;
			}
		}

		// Token: 0x0600963B RID: 38459 RVA: 0x001F1BA4 File Offset: 0x001EFDA4
		private Func<Module45> LoadModuleConstructor()
		{
			Dictionary<string, Func<Module45>> dictionary = Module45.moduleCtors;
			Func<Module45> func2;
			lock (dictionary)
			{
				Func<Module45> func;
				if (!Module45.moduleCtors.TryGetValue(this.Name, out func))
				{
					Exception ex;
					global::System.Reflection.Assembly assembly = Modules.LoadAssembly(this.AssemblyName, out ex);
					if (assembly != null)
					{
						try
						{
							func = Expression.Lambda<Func<Module45>>(Expression.New(assembly.GetType(base.GetType().FullName, false)), Array.Empty<ParameterExpression>()).Compile();
							func();
						}
						catch (Exception ex2)
						{
							if (!SafeExceptions.IsSafeException(ex2))
							{
								throw;
							}
							func = null;
						}
						Module45.moduleCtors.Add(this.Name, func);
					}
				}
				func2 = func;
			}
			return func2;
		}

		// Token: 0x0600963C RID: 38460 RVA: 0x001F1C6C File Offset: 0x001EFE6C
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			RecordValue exports = this.UnderlyingModule.GetModuleExports(environment, host);
			if (this.UnderlyingModule.DocumentationResources != null)
			{
				return RecordValue.New(this.ExportKeys, (int i) => exports[i].AddHelp(exports.Keys[i], this.UnderlyingModule.DocumentationResources));
			}
			return exports;
		}

		// Token: 0x0600963D RID: 38461 RVA: 0x001F1CC4 File Offset: 0x001EFEC4
		protected virtual RecordValue GetModuleExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index < 0 || index > this.ExportKeys.Length)
				{
					throw new InvalidOperationException();
				}
				return new Module45.UnavailableFunctionValue(host);
			});
		}

		// Token: 0x04004FDE RID: 20446
		private const string ProviderDownloadLink = "https://go.microsoft.com/fwlink/?LinkId=528259";

		// Token: 0x04004FDF RID: 20447
		private const string ProviderLibraryName = "Microsoft .NET Framework 4.5";

		// Token: 0x04004FE0 RID: 20448
		private static Dictionary<string, Func<Module45>> moduleCtors = new Dictionary<string, Func<Module45>>();

		// Token: 0x04004FE1 RID: 20449
		private Module45 underlyingModule;

		// Token: 0x02001715 RID: 5909
		private sealed class UnavailableFunctionValue : NativeFunctionValue
		{
			// Token: 0x06009640 RID: 38464 RVA: 0x001F1D08 File Offset: 0x001EFF08
			public UnavailableFunctionValue(IEngineHost engineHost)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x1700274B RID: 10059
			// (get) Token: 0x06009641 RID: 38465 RVA: 0x001F1D17 File Offset: 0x001EFF17
			public override TypeValue Type
			{
				get
				{
					return Module45.UnavailableFunctionValue.type;
				}
			}

			// Token: 0x06009642 RID: 38466 RVA: 0x001F1D1E File Offset: 0x001EFF1E
			public override Value Invoke()
			{
				return this.Fail();
			}

			// Token: 0x06009643 RID: 38467 RVA: 0x001F1D1E File Offset: 0x001EFF1E
			public override Value Invoke(Value arg0)
			{
				return this.Fail();
			}

			// Token: 0x06009644 RID: 38468 RVA: 0x001F1D1E File Offset: 0x001EFF1E
			public override Value Invoke(Value arg0, Value arg1)
			{
				return this.Fail();
			}

			// Token: 0x06009645 RID: 38469 RVA: 0x001F1D1E File Offset: 0x001EFF1E
			public override Value Invoke(Value arg0, Value arg1, Value arg2)
			{
				return this.Fail();
			}

			// Token: 0x06009646 RID: 38470 RVA: 0x001F1D1E File Offset: 0x001EFF1E
			public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3)
			{
				return this.Fail();
			}

			// Token: 0x06009647 RID: 38471 RVA: 0x001F1D1E File Offset: 0x001EFF1E
			public override Value Invoke(Value arg0, Value arg1, Value arg2, Value arg3, Value arg4)
			{
				return this.Fail();
			}

			// Token: 0x06009648 RID: 38472 RVA: 0x001F1D1E File Offset: 0x001EFF1E
			public override Value Invoke(params Value[] args)
			{
				return this.Fail();
			}

			// Token: 0x06009649 RID: 38473 RVA: 0x001F1D26 File Offset: 0x001EFF26
			private Value Fail()
			{
				throw DataSourceException.NewMissingClientLibraryError<Message2>(this.engineHost, Strings.NewerCLRNeeded("Microsoft .NET Framework 4.5", "https://go.microsoft.com/fwlink/?LinkId=528259"), null, "Microsoft .NET Framework 4.5", "https://go.microsoft.com/fwlink/?LinkId=528259", null);
			}

			// Token: 0x04004FE2 RID: 20450
			private static readonly TypeValue type = FunctionTypeValue.New(TypeValue.Null, RecordValue.Empty, 0);

			// Token: 0x04004FE3 RID: 20451
			private readonly IEngineHost engineHost;
		}
	}
}
