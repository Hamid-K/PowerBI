using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Excel;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Access
{
	// Token: 0x02001233 RID: 4659
	internal sealed class AccessModule : Module
	{
		// Token: 0x170021AA RID: 8618
		// (get) Token: 0x06007B25 RID: 31525 RVA: 0x001A917A File Offset: 0x001A737A
		public override string Name
		{
			get
			{
				return "Access";
			}
		}

		// Token: 0x170021AB RID: 8619
		// (get) Token: 0x06007B26 RID: 31526 RVA: 0x001A9181 File Offset: 0x001A7381
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
							return "Access.Database";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06007B27 RID: 31527 RVA: 0x001A91BC File Offset: 0x001A73BC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new AccessModule.DatabaseFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x04004435 RID: 17461
		public const string DatabaseFunctionName = "Access.Database";

		// Token: 0x04004436 RID: 17462
		public const string DataSourceNameString = "Microsoft Access";

		// Token: 0x04004437 RID: 17463
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, "False"),
			Options.NavigationPropertyNameGeneratorOption
		});

		// Token: 0x04004438 RID: 17464
		private Keys exportKeys;

		// Token: 0x02001234 RID: 4660
		private enum Exports
		{
			// Token: 0x0400443A RID: 17466
			Database,
			// Token: 0x0400443B RID: 17467
			Count
		}

		// Token: 0x02001235 RID: 4661
		public sealed class DatabaseFunctionValue : NativeFunctionValue2<TableValue, BinaryValue, Value>
		{
			// Token: 0x06007B2A RID: 31530 RVA: 0x001A9233 File Offset: 0x001A7433
			public DatabaseFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "database", TypeValue.Binary, "options", AccessModule.DatabaseFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06007B2B RID: 31531 RVA: 0x001A925C File Offset: 0x001A745C
			public override TableValue TypedInvoke(BinaryValue database, Value options)
			{
				AceSourceFile aceSourceFile = new AceSourceFile(this.host, database, ".accdb", true);
				TableValue tableValue;
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Access/FromDatabase", TraceEventType.Information, null))
				{
					try
					{
						AccessEnvironment accessEnvironment = AccessEnvironment.Create(this.host, aceSourceFile, options);
						hostTrace.AddResource(accessEnvironment.Resource);
						tableValue = accessEnvironment.CreateTable();
					}
					catch (Exception ex)
					{
						if (SafeExceptions.TraceIsSafeException(hostTrace, ex))
						{
							string fileName = Path.GetFileName(aceSourceFile.Path);
							AceSourceFile.ThrowIfProviderMissing(ex, "Microsoft Access", fileName, aceSourceFile.IsTempFile);
						}
						throw;
					}
				}
				return tableValue;
			}

			// Token: 0x0400443C RID: 17468
			private static readonly TypeValue optionsType = AccessModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x0400443D RID: 17469
			private readonly IEngineHost host;
		}
	}
}
