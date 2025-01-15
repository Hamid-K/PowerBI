using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.PostgreSQL
{
	// Token: 0x0200053D RID: 1341
	internal sealed class PostgreSQLModule : Module
	{
		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06002B3D RID: 11069 RVA: 0x00083258 File Offset: 0x00081458
		public override string Name
		{
			get
			{
				return "PostgreSQL";
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x0008325F File Offset: 0x0008145F
		public override Keys ExportKeys
		{
			get
			{
				if (PostgreSQLModule.exportKeys == null)
				{
					PostgreSQLModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "PostgreSQL.Database";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return PostgreSQLModule.exportKeys;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x00083297 File Offset: 0x00081497
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { PostgreSQLModule.resourceKindInfo };
			}
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000832A8 File Offset: 0x000814A8
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new PostgreSQLModule.DatabaseFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x040012BF RID: 4799
		public const string PostgreSQLDatabase = "PostgreSQL.Database";

		// Token: 0x040012C0 RID: 4800
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			Options.NavigationPropertyNameGeneratorOption,
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x040012C1 RID: 4801
		private static readonly ResourceKindInfo resourceKindInfo = new DatabaseResourceKindInfo("PostgreSQL", null, true, false, true, new AuthenticationInfo[] { ResourceHelpers.SqlAuth }, null, null, new DataSourceLocationFactory[] { PostgreSqlDataSourceLocation.Factory });

		// Token: 0x040012C2 RID: 4802
		private static Keys exportKeys;

		// Token: 0x0200053E RID: 1342
		private enum Exports
		{
			// Token: 0x040012C4 RID: 4804
			Database,
			// Token: 0x040012C5 RID: 4805
			Count
		}

		// Token: 0x0200053F RID: 1343
		private sealed class DatabaseFunctionValue : NativeFunctionValue3<TableValue, TextValue, TextValue, Value>
		{
			// Token: 0x06002B43 RID: 11075 RVA: 0x000833A8 File Offset: 0x000815A8
			public DatabaseFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 2, "server", TypeValue.Text, "database", TypeValue.Text, "options", PostgreSQLModule.DatabaseFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06002B44 RID: 11076 RVA: 0x000833E8 File Offset: 0x000815E8
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
				return PostgreSQLEnvironment.Create(this.host, server.String, database.String, options).CreateTable();
			}

			// Token: 0x1700103B RID: 4155
			// (get) Token: 0x06002B45 RID: 11077 RVA: 0x00083258 File Offset: 0x00081458
			public override string PrimaryResourceKind
			{
				get
				{
					return "PostgreSQL";
				}
			}

			// Token: 0x06002B46 RID: 11078 RVA: 0x0008343E File Offset: 0x0008163E
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				return StaticAnalysisResolver.TryGetServerDatabaseLocation<PostgreSqlDataSourceLocation>(expression, out location, out foundOptions, out unknownOptions);
			}

			// Token: 0x040012C6 RID: 4806
			private static readonly TypeValue optionsType = PostgreSQLModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x040012C7 RID: 4807
			private IEngineHost host;
		}
	}
}
