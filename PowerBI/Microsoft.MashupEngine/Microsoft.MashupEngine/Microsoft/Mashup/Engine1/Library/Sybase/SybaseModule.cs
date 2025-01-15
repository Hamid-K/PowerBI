using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Sybase
{
	// Token: 0x02000372 RID: 882
	internal sealed class SybaseModule : Module
	{
		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x00050F81 File Offset: 0x0004F181
		public override string Name
		{
			get
			{
				return "Sybase";
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00050F88 File Offset: 0x0004F188
		public override Keys ExportKeys
		{
			get
			{
				if (SybaseModule.exportKeys == null)
				{
					SybaseModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Sybase.Database";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return SybaseModule.exportKeys;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x00050FC0 File Offset: 0x0004F1C0
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { SybaseModule.resourceKindInfo };
			}
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00050FD0 File Offset: 0x0004F1D0
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new SybaseModule.DatabaseFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x04000B59 RID: 2905
		public const string SybaseDatabase = "Sybase.Database";

		// Token: 0x04000B5A RID: 2906
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			Options.NavigationPropertyNameGeneratorOption,
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x04000B5B RID: 2907
		private static readonly ResourceKindInfo resourceKindInfo = new DatabaseResourceKindInfo("Sybase", null, true, false, true, new AuthenticationInfo[]
		{
			ResourceHelpers.WindowsAuthAlternateCredentials,
			ResourceHelpers.SqlAuth
		}, null, null, new DataSourceLocationFactory[] { SybaseDataSourceLocation.Factory });

		// Token: 0x04000B5C RID: 2908
		private static Keys exportKeys;

		// Token: 0x02000373 RID: 883
		private enum Exports
		{
			// Token: 0x04000B5E RID: 2910
			Database,
			// Token: 0x04000B5F RID: 2911
			Count
		}

		// Token: 0x02000374 RID: 884
		private sealed class DatabaseFunctionValue : NativeFunctionValue3<TableValue, TextValue, TextValue, Value>
		{
			// Token: 0x06001F51 RID: 8017 RVA: 0x000510D8 File Offset: 0x0004F2D8
			public DatabaseFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 2, "server", TypeValue.Text, "database", TypeValue.Text, "options", SybaseModule.DatabaseFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06001F52 RID: 8018 RVA: 0x00051118 File Offset: 0x0004F318
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
				return SybaseEnvironment.Create(this.host, server.String, database.String, options).CreateTable();
			}

			// Token: 0x17000DCD RID: 3533
			// (get) Token: 0x06001F53 RID: 8019 RVA: 0x00050F81 File Offset: 0x0004F181
			public override string PrimaryResourceKind
			{
				get
				{
					return "Sybase";
				}
			}

			// Token: 0x06001F54 RID: 8020 RVA: 0x0005116E File Offset: 0x0004F36E
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				return StaticAnalysisResolver.TryGetServerDatabaseLocation<SybaseDataSourceLocation>(expression, out location, out foundOptions, out unknownOptions);
			}

			// Token: 0x04000B60 RID: 2912
			private static readonly TypeValue optionsType = SybaseModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x04000B61 RID: 2913
			private readonly IEngineHost host;
		}
	}
}
