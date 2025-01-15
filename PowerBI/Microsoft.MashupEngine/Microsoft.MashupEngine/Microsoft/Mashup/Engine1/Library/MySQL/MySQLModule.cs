using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MySQL
{
	// Token: 0x02000914 RID: 2324
	internal sealed class MySQLModule : Module
	{
		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x06004266 RID: 16998 RVA: 0x000E012C File Offset: 0x000DE32C
		public override string Name
		{
			get
			{
				return "MySQL";
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x000E0133 File Offset: 0x000DE333
		public override Keys ExportKeys
		{
			get
			{
				if (MySQLModule.exportKeys == null)
				{
					MySQLModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "MySQL.Database";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return MySQLModule.exportKeys;
			}
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06004268 RID: 17000 RVA: 0x000E016B File Offset: 0x000DE36B
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { MySQLModule.resourceKindInfo };
			}
		}

		// Token: 0x06004269 RID: 17001 RVA: 0x000E017C File Offset: 0x000DE37C
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new MySQLModule.DatabaseFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x0600426A RID: 17002 RVA: 0x000E01B0 File Offset: 0x000DE3B0
		private static bool TryConvertEncoding(Value optionValue, out object value)
		{
			if (optionValue.IsNumber)
			{
				NumberValue asNumber = optionValue.AsNumber;
				int num;
				if (asNumber.TryGetInt32(out num))
				{
					if (num == TextEncoding.Utf8.AsNumber.ToInt32())
					{
						value = "utf8mb4";
						return true;
					}
					if (num == TextEncoding.Utf16.AsNumber.ToInt32())
					{
						value = "utf16";
						return true;
					}
					if (num == TextEncoding.Ascii.AsNumber.ToInt32())
					{
						value = "ascii";
						return true;
					}
					if (num == TextEncoding.Windows.AsNumber.ToInt32())
					{
						value = "latin1";
						return true;
					}
					throw ValueException.NewDataSourceError<Message2>(DataSourceException.DataSourceMessage("MySQL", Strings.UnsupportedQueryOption("Encoding", TextEncoding.Type.GetName(asNumber))), asNumber, null);
				}
			}
			value = null;
			return false;
		}

		// Token: 0x040022CE RID: 8910
		public const string MySQLDatabase = "MySQL.Database";

		// Token: 0x040022CF RID: 8911
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Encoding", TextEncoding.Type.Nullable, Value.Null, OptionItemOption.None, new TryConvertOption(MySQLModule.TryConvertEncoding), "MySQL"),
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			Options.NavigationPropertyNameGeneratorOption,
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration, DurationValue.New(TimeSpan.FromSeconds(60.0)), OptionItemOption.None, null, null),
			new OptionItem("TreatTinyAsBoolean", NullableTypeValue.Logical),
			new OptionItem("OldGuids", NullableTypeValue.Logical),
			new OptionItem("ReturnSingleDatabase", NullableTypeValue.Logical),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x040022D0 RID: 8912
		private static readonly ResourceKindInfo resourceKindInfo = new DatabaseResourceKindInfo("MySql", null, true, false, true, new AuthenticationInfo[]
		{
			ResourceHelpers.WindowsAuthAlternateCredentials,
			ResourceHelpers.SqlAuth
		}, null, new string[] { "EffectiveUserName" }, new DataSourceLocationFactory[] { MySqlDataSourceLocation.Factory });

		// Token: 0x040022D1 RID: 8913
		private static Keys exportKeys;

		// Token: 0x02000915 RID: 2325
		private enum Exports
		{
			// Token: 0x040022D3 RID: 8915
			Database,
			// Token: 0x040022D4 RID: 8916
			Count
		}

		// Token: 0x02000916 RID: 2326
		private sealed class DatabaseFunctionValue : NativeFunctionValue3<TableValue, TextValue, TextValue, Value>
		{
			// Token: 0x0600426D RID: 17005 RVA: 0x000E03D8 File Offset: 0x000DE5D8
			public DatabaseFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 2, "server", TypeValue.Text, "database", TypeValue.Text, "options", MySQLModule.DatabaseFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x0600426E RID: 17006 RVA: 0x000E0418 File Offset: 0x000DE618
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
				return MySQLEnvironment.Create(this.host, server.String, database.String, options).CreateTable();
			}

			// Token: 0x1700152D RID: 5421
			// (get) Token: 0x0600426F RID: 17007 RVA: 0x000E046E File Offset: 0x000DE66E
			public override string PrimaryResourceKind
			{
				get
				{
					return "MySql";
				}
			}

			// Token: 0x06004270 RID: 17008 RVA: 0x000E0475 File Offset: 0x000DE675
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				return StaticAnalysisResolver.TryGetServerDatabaseLocation<MySqlDataSourceLocation>(expression, out location, out foundOptions, out unknownOptions);
			}

			// Token: 0x040022D5 RID: 8917
			private static readonly TypeValue optionsType = MySQLModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x040022D6 RID: 8918
			private readonly IEngineHost host;
		}
	}
}
