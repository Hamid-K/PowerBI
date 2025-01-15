using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000CA3 RID: 3235
	internal abstract class DrdaModule<T, TDSR> : Module where TDSR : IDataSourceLocation, new()
	{
		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x0600576B RID: 22379
		protected abstract string FunctionName { get; }

		// Token: 0x17001A56 RID: 6742
		// (get) Token: 0x0600576C RID: 22380
		protected abstract TypeValue OptionsType { get; }

		// Token: 0x17001A57 RID: 6743
		// (get) Token: 0x0600576D RID: 22381
		protected abstract ResourceKindInfo ResourceKindInfo { get; }

		// Token: 0x0600576E RID: 22382
		public abstract DbEnvironment CreateDbEnvironment(IEngineHost host, string server, string database, Value options);

		// Token: 0x17001A58 RID: 6744
		// (get) Token: 0x0600576F RID: 22383 RVA: 0x00130358 File Offset: 0x0012E558
		public override Keys ExportKeys
		{
			get
			{
				if (DrdaModule<T, TDSR>.exportKeys == null)
				{
					DrdaModule<T, TDSR>.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return this.FunctionName;
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return DrdaModule<T, TDSR>.exportKeys;
			}
		}

		// Token: 0x17001A59 RID: 6745
		// (get) Token: 0x06005770 RID: 22384 RVA: 0x0013037D File Offset: 0x0012E57D
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { this.ResourceKindInfo };
			}
		}

		// Token: 0x06005771 RID: 22385 RVA: 0x00130390 File Offset: 0x0012E590
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new DrdaModule<T, TDSR>.DatabaseFunctionValue(hostEnvironment, this);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x0400316B RID: 12651
		private static Keys exportKeys;

		// Token: 0x02000CA4 RID: 3236
		private enum Exports
		{
			// Token: 0x0400316D RID: 12653
			Database,
			// Token: 0x0400316E RID: 12654
			Count
		}

		// Token: 0x02000CA5 RID: 3237
		private sealed class DatabaseFunctionValue : NativeFunctionValue3<TableValue, TextValue, TextValue, Value>
		{
			// Token: 0x06005774 RID: 22388 RVA: 0x001303E4 File Offset: 0x0012E5E4
			public DatabaseFunctionValue(IEngineHost host, DrdaModule<T, TDSR> drdaModule)
				: base(TypeValue.Table, 2, "server", TypeValue.Text, "database", TypeValue.Text, "options", drdaModule.OptionsType)
			{
				this.host = host;
				this.drdaModule = drdaModule;
			}

			// Token: 0x06005775 RID: 22389 RVA: 0x0013042C File Offset: 0x0012E62C
			public override TableValue TypedInvoke(TextValue server, TextValue database, Value options)
			{
				if (server.Length <= 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_ServerCannotBeEmpty, server, null);
				}
				if (database.Length <= 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_DatabaseCannotBeEmpty, server, null);
				}
				return this.drdaModule.CreateDbEnvironment(this.host, server.String, database.String, options).CreateTable();
			}

			// Token: 0x17001A5A RID: 6746
			// (get) Token: 0x06005776 RID: 22390 RVA: 0x00130488 File Offset: 0x0012E688
			public override string PrimaryResourceKind
			{
				get
				{
					return this.drdaModule.ResourceKindInfo.Kind;
				}
			}

			// Token: 0x06005777 RID: 22391 RVA: 0x0013049A File Offset: 0x0012E69A
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				return StaticAnalysisResolver.TryGetServerDatabaseLocation<TDSR>(expression, out location, out foundOptions, out unknownOptions);
			}

			// Token: 0x0400316F RID: 12655
			private readonly IEngineHost host;

			// Token: 0x04003170 RID: 12656
			private readonly DrdaModule<T, TDSR> drdaModule;
		}
	}
}
