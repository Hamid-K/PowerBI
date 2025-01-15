using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Teradata
{
	// Token: 0x020002D8 RID: 728
	internal sealed class TeradataModule : Module
	{
		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x00047D96 File Offset: 0x00045F96
		public override string Name
		{
			get
			{
				return "Teradata";
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x00047D9D File Offset: 0x00045F9D
		public override Keys ExportKeys
		{
			get
			{
				if (TeradataModule.exportKeys == null)
				{
					TeradataModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Teradata.Database";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return TeradataModule.exportKeys;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x00047DD5 File Offset: 0x00045FD5
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { TeradataModule.resourceKindInfo };
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00047DE8 File Offset: 0x00045FE8
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new TeradataModule.DatabaseFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x040009CD RID: 2509
		public const string TeradataDatabase = "Teradata.Database";

		// Token: 0x040009CE RID: 2510
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			Options.NavigationPropertyNameGeneratorOption,
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x040009CF RID: 2511
		private static readonly ParameterizedAuthenticationInfo teradataLdap = new ParameterizedAuthenticationInfo("Ldap", null)
		{
			Label = Strings.Auth_Ldap,
			Properties = new CredentialProperty[]
			{
				new CredentialProperty
				{
					Name = "Username",
					Label = Strings.Auth_Username,
					IsRequired = true,
					IsSecret = false,
					PropertyType = typeof(string)
				},
				new CredentialProperty
				{
					Name = "Password",
					Label = Strings.Auth_Password,
					IsRequired = true,
					IsSecret = true,
					PropertyType = typeof(string)
				}
			}
		};

		// Token: 0x040009D0 RID: 2512
		private static readonly ResourceKindInfo resourceKindInfo = new DatabaseResourceKindInfo("Teradata", null, true, false, true, new AuthenticationInfo[]
		{
			ResourceHelpers.WindowsAuthAlternateCredentials,
			ResourceHelpers.SqlAuth,
			TeradataModule.teradataLdap
		}, null, null, new DataSourceLocationFactory[] { TeradataDataSourceLocation.Factory });

		// Token: 0x040009D1 RID: 2513
		private static Keys exportKeys;

		// Token: 0x020002D9 RID: 729
		private enum Exports
		{
			// Token: 0x040009D3 RID: 2515
			Database,
			// Token: 0x040009D4 RID: 2516
			Count
		}

		// Token: 0x020002DA RID: 730
		private sealed class DatabaseFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x06001D02 RID: 7426 RVA: 0x00047FA8 File Offset: 0x000461A8
			public DatabaseFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "server", TypeValue.Text, "options", TeradataModule.DatabaseFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06001D03 RID: 7427 RVA: 0x00047FD1 File Offset: 0x000461D1
			public override TableValue TypedInvoke(TextValue server, Value options)
			{
				if (server.Length <= 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_ServerCannotBeEmpty, server, null);
				}
				return TeradataEnvironment.Create(this.host, server.String, options).CreateTable();
			}

			// Token: 0x17000D5D RID: 3421
			// (get) Token: 0x06001D04 RID: 7428 RVA: 0x00047D96 File Offset: 0x00045F96
			public override string PrimaryResourceKind
			{
				get
				{
					return "Teradata";
				}
			}

			// Token: 0x06001D05 RID: 7429 RVA: 0x00048000 File Offset: 0x00046200
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				return StaticAnalysisResolver.TryGetServerLocation<TeradataDataSourceLocation>(expression, out location, out foundOptions, out unknownOptions);
			}

			// Token: 0x040009D5 RID: 2517
			private static readonly TypeValue optionsType = TeradataModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x040009D6 RID: 2518
			private readonly IEngineHost host;
		}
	}
}
