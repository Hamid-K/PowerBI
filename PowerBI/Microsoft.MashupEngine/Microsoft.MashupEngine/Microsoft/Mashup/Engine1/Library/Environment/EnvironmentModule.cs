using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Environment
{
	// Token: 0x02000C88 RID: 3208
	internal sealed class EnvironmentModule : Module
	{
		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x060056D7 RID: 22231 RVA: 0x0012D5AD File Offset: 0x0012B7AD
		public override string Name
		{
			get
			{
				return "Environment";
			}
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x060056D8 RID: 22232 RVA: 0x0012D5B4 File Offset: 0x0012B7B4
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(3, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Environment.Libraries";
						case 1:
							return "Environment.Configuration";
						case 2:
							return "Environment.FeatureSwitch";
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x060056D9 RID: 22233 RVA: 0x0012D5F0 File Offset: 0x0012B7F0
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new EnvironmentModule.LibrariesFunctionValue(hostEnvironment);
				case 1:
					return new EnvironmentModule.ConfigurationFunctionValue(hostEnvironment);
				case 2:
					return new EnvironmentModule.FeatureSwitchFunctionValue(hostEnvironment);
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x040030E9 RID: 12521
		private static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Culture", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("ExcludeBuiltins", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			new OptionItem("ReturnAad", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x040030EA RID: 12522
		private Keys exportKeys;

		// Token: 0x02000C89 RID: 3209
		private enum Exports
		{
			// Token: 0x040030EC RID: 12524
			Libraries,
			// Token: 0x040030ED RID: 12525
			Configuration,
			// Token: 0x040030EE RID: 12526
			FeatureSwitch,
			// Token: 0x040030EF RID: 12527
			Count
		}

		// Token: 0x02000C8A RID: 3210
		private class LibrariesFunctionValue : NativeFunctionValue1<TableValue, Value>
		{
			// Token: 0x060056DC RID: 22236 RVA: 0x0012D68F File Offset: 0x0012B88F
			public LibrariesFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Table, 0, "options", EnvironmentModule.LibrariesFunctionValue.optionsType)
			{
				this.engineHost = engineHost;
				this.defaultCulture = Culture.GetDefaultCulture(engineHost);
			}

			// Token: 0x060056DD RID: 22237 RVA: 0x0012D6BC File Offset: 0x0012B8BC
			public override TableValue TypedInvoke(Value options)
			{
				IResource resource = Resource.New("Environment", "Environment");
				ResourceCredentialCollection resourceCredentialCollection;
				if (!HostResourcePermissionService.IsResourceAccessPermitted(this.engineHost, resource, out resourceCredentialCollection))
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.IntrospectionNotAvailable, Value.Null, null);
				}
				OptionsRecord optionsRecord = EnvironmentModule.OptionRecord.CreateOptions("Environment", options);
				bool returnAad = optionsRecord.GetBool("ReturnAad", false);
				Value @null;
				if (options.IsNull || !options.TryGetValue("Culture", out @null))
				{
					@null = Value.Null;
				}
				ICulture culture = Culture.GetCulture(this.engineHost, @null, this.defaultCulture);
				TableValue tableValue = null;
				bool @bool = optionsRecord.GetBool("ExcludeBuiltins", false);
				if (!@bool)
				{
					CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
					try
					{
						CultureInfo cultureInfo = ((culture != null) ? culture.GetCultureInfo() : null);
						if (cultureInfo != null)
						{
							Thread.CurrentThread.CurrentUICulture = cultureInfo;
						}
						TableValue tableValue2 = ListValue.New(from m in Modules.GetBuiltinModules()
							select EnvironmentModule.LibrariesFunctionValue.MakeModule(m, returnAad)).ToTable(EnvironmentModule.LibrariesFunctionValue.modulesType);
						RecordValue recordValue = RecordValue.New(EnvironmentModule.LibrariesFunctionValue.resultKeys, new Value[]
						{
							TextValue.New("Builtin"),
							NumberValue.New(0),
							ListValue.New(new Value[] { RecordValue.New(EnvironmentModule.LibrariesFunctionValue.librariesKeys, new Value[]
							{
								TextValue.New("Builtin"),
								Value.Null,
								tableValue2
							}) }).ToTable(EnvironmentModule.LibrariesFunctionValue.librariesType)
						});
						tableValue = ListValue.New(new Value[] { recordValue }).ToTable(EnvironmentModule.LibrariesFunctionValue.resultKeys);
					}
					finally
					{
						Thread.CurrentThread.CurrentUICulture = currentUICulture;
					}
				}
				ILibraryService libraryService = this.engineHost.QueryService<ILibraryService>();
				if (libraryService != null)
				{
					TableValue tableValue3 = (TableValue)libraryService.GetLibraries(culture.Name).GetValue().AsTable;
					if (!@bool)
					{
						tableValue3 = tableValue3.SelectRows(new EnvironmentModule.LibrariesFunctionValue.FilterBuiltinsFunctionValue());
					}
					Grouping grouping = new Grouping(true, EnvironmentModule.LibrariesFunctionValue.resultKeys, Keys.New("Id", "Priority"), new int[] { 0, 1 }, new ColumnConstructor[]
					{
						new ColumnConstructor("Libraries", new EnvironmentModule.LibrariesFunctionValue.CreateLibrariesTableFunctionValue(libraryService, culture.Name, returnAad))
					}, true, null, tableValue3.Type.AsTableType);
					TableValue tableValue4 = tableValue3.Group(grouping);
					if (tableValue == null)
					{
						tableValue = tableValue4;
					}
					else
					{
						tableValue = TableModule.Table.Combine.Invoke(ListValue.New(new Value[] { tableValue, tableValue4 })).AsTable;
					}
				}
				return tableValue;
			}

			// Token: 0x060056DE RID: 22238 RVA: 0x0012D948 File Offset: 0x0012BB48
			private static IValueReference MakeModule(IModule module, bool returnAad)
			{
				return RecordValue.New(EnvironmentModule.LibrariesFunctionValue.modulesKeys, new Value[]
				{
					TextValue.New(module.Name),
					TextValue.NewOrNull(module.Version),
					(RecordValue)module.Metadata,
					Value.Null,
					EnvironmentModule.LibrariesFunctionValue.MakeExports(module),
					EnvironmentModule.LibrariesFunctionValue.MakeDataSources(module, returnAad)
				});
			}

			// Token: 0x060056DF RID: 22239 RVA: 0x0012D9AC File Offset: 0x0012BBAC
			private static TableValue MakeDataSources(IModule module, bool returnAad)
			{
				ResourceKindInfo[] dataSources = module.DataSources;
				Value[] array = new Value[dataSources.Length];
				for (int i = 0; i < array.Length; i++)
				{
					ResourceKindInfo resourceKindInfo = dataSources[i];
					array[i] = RecordValue.New(EnvironmentModule.LibrariesFunctionValue.dataSourcesKeys, new Value[]
					{
						TextValue.New(resourceKindInfo.Kind),
						TextValue.New(resourceKindInfo.Label ?? resourceKindInfo.Kind),
						EnvironmentModule.LibrariesFunctionValue.MakeAuthenticationInfos(resourceKindInfo, returnAad),
						EnvironmentModule.LibrariesFunctionValue.MakeProperties(resourceKindInfo),
						EnvironmentModule.LibrariesFunctionValue.MakeApplicationProperties(resourceKindInfo),
						EnvironmentModule.LibrariesFunctionValue.MakePermissionKinds(resourceKindInfo),
						((RecordValue)resourceKindInfo.ResourceRecord) ?? Value.Null
					});
				}
				return ListValue.New(array).ToTable(EnvironmentModule.LibrariesFunctionValue.dataSourcesKeys);
			}

			// Token: 0x060056E0 RID: 22240 RVA: 0x0012DA64 File Offset: 0x0012BC64
			private static TableValue MakeAuthenticationInfos(ResourceKindInfo resourceKindInfo, bool returnAad)
			{
				return ListValue.New(from info in resourceKindInfo.AuthenticationInfo
					select EnvironmentModule.LibrariesFunctionValue.MakeAuthenticationInfo(info, returnAad) into record
					where record != null
					select record).ToTable(EnvironmentModule.LibrariesFunctionValue.authenticationKeys);
			}

			// Token: 0x060056E1 RID: 22241 RVA: 0x0012DAC8 File Offset: 0x0012BCC8
			private static RecordValue MakeAuthenticationInfo(AuthenticationInfo info, bool returnAad)
			{
				string text;
				switch (info.AuthenticationKind)
				{
				case AuthenticationKind.Implicit:
					text = "Implicit";
					goto IL_008D;
				case AuthenticationKind.UsernamePassword:
					text = "UsernamePassword";
					goto IL_008D;
				case AuthenticationKind.Windows:
					text = "Windows";
					goto IL_008D;
				case AuthenticationKind.WebApi:
					text = "WebApi";
					goto IL_008D;
				case AuthenticationKind.OAuth2:
					text = ((returnAad && info is AadAuthenticationInfo) ? "AAD" : "OAuth");
					goto IL_008D;
				case AuthenticationKind.Exchange:
					text = "UsernamePassword";
					goto IL_008D;
				case AuthenticationKind.Key:
					text = "Key";
					goto IL_008D;
				case AuthenticationKind.Parameterized:
					text = ((ParameterizedAuthenticationInfo)info).Name;
					goto IL_008D;
				}
				return null;
				IL_008D:
				return RecordValue.New(EnvironmentModule.LibrariesFunctionValue.authenticationKeys, new Value[]
				{
					TextValue.New(text),
					EnvironmentModule.LibrariesFunctionValue.MakeProperties(DataSourceProperties.GetAuthenticationProperties(info, true)),
					EnvironmentModule.LibrariesFunctionValue.MakeProperties(info.ApplicationProperties)
				});
			}

			// Token: 0x060056E2 RID: 22242 RVA: 0x0012DB98 File Offset: 0x0012BD98
			private static TableValue MakeProperties(ResourceKindInfo resourceKindInfo)
			{
				return EnvironmentModule.LibrariesFunctionValue.MakeProperties(DataSourceProperties.GetConnectionProperties(resourceKindInfo));
			}

			// Token: 0x060056E3 RID: 22243 RVA: 0x0012DBA5 File Offset: 0x0012BDA5
			private static TableValue MakeApplicationProperties(ResourceKindInfo resourceKindInfo)
			{
				return EnvironmentModule.LibrariesFunctionValue.MakeProperties(resourceKindInfo.ApplicationProperties);
			}

			// Token: 0x060056E4 RID: 22244 RVA: 0x0012DBB4 File Offset: 0x0012BDB4
			private static TableValue MakeProperties(IList<CredentialProperty> properties)
			{
				ListValue listValue;
				if (properties == null)
				{
					listValue = ListValue.Empty;
				}
				else
				{
					listValue = ListValue.New(properties.Select(new Func<CredentialProperty, IValueReference>(EnvironmentModule.LibrariesFunctionValue.MakeProperty)));
				}
				return listValue.ToTable(EnvironmentModule.LibrariesFunctionValue.propertyKeys);
			}

			// Token: 0x060056E5 RID: 22245 RVA: 0x0012DBF0 File Offset: 0x0012BDF0
			private static IValueReference MakeProperty(CredentialProperty property)
			{
				TypeValue typeValue;
				if (property.PropertyType == typeof(string))
				{
					typeValue = (property.IsSecret ? TypeValue.Password : TypeValue.Text);
				}
				else if (property.PropertyType == typeof(bool))
				{
					typeValue = TypeValue.Logical;
				}
				else
				{
					typeValue = TypeValue.Any;
				}
				if (!property.IsRequired)
				{
					typeValue = typeValue.Nullable;
				}
				TextValue textValue = TextValue.New(property.Name);
				TextValue textValue2 = ((property.Label == null) ? textValue : TextValue.New(property.Label));
				return RecordValue.New(EnvironmentModule.LibrariesFunctionValue.propertyKeys, new Value[] { textValue, textValue2, typeValue });
			}

			// Token: 0x060056E6 RID: 22246 RVA: 0x0012DC9E File Offset: 0x0012BE9E
			private static ListValue MakePermissionKinds(ResourceKindInfo resource)
			{
				return ListValue.New(resource.PermissionKinds.Select((QueryPermissionChallengeType p) => TextValue.New(DataSourceProperties.FromQueryPermission(p))));
			}

			// Token: 0x060056E7 RID: 22247 RVA: 0x0012DCD0 File Offset: 0x0012BED0
			private static TableValue MakeExports(IModule module)
			{
				RecordValue exports = Linker.Assemble((Module)module, EngineHost.Empty, delegate(IError e)
				{
				}).Exports;
				Value[] array = new Value[exports.Count];
				for (int i = 0; i < array.Length; i++)
				{
					Value value = exports[i];
					Value value2 = Value.Null;
					Value value3 = Value.Null;
					if (value.IsFunction)
					{
						if (!value.TryGetMetaField("Publish", out value2) || !value2.IsRecord)
						{
							value2 = Value.Null;
						}
						string text = value.AsFunction.PrimaryResourceKind;
						if (text == null && value.TryGetMetaField("DataSource.Kind", out value3) && value3.IsText)
						{
							text = value3.AsString;
						}
						value3 = ((text == null) ? Value.Null : TextValue.New(text));
					}
					array[i] = RecordValue.New(EnvironmentModule.LibrariesFunctionValue.exportKeys, new Value[]
					{
						TextValue.New(exports.Keys[i]),
						value.Type,
						value3,
						value2
					});
				}
				return ListValue.New(array).ToTable(EnvironmentModule.LibrariesFunctionValue.exportKeys);
			}

			// Token: 0x040030F0 RID: 12528
			private const string BuiltinProviderName = "Builtin";

			// Token: 0x040030F1 RID: 12529
			private static readonly TypeValue optionsType = EnvironmentModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x040030F2 RID: 12530
			private static readonly Keys resultKeys = Keys.New("Id", "Priority", "Libraries");

			// Token: 0x040030F3 RID: 12531
			private static readonly Keys modulesKeys = Keys.New(new string[] { "Name", "Version", "Metadata", "Dependencies", "Members", "DataSources" });

			// Token: 0x040030F4 RID: 12532
			private static readonly TableTypeValue modulesType = TableTypeValue.New(RecordTypeValue.New(EnvironmentModule.LibrariesFunctionValue.modulesKeys));

			// Token: 0x040030F5 RID: 12533
			private static readonly Keys librariesKeys = Keys.New("Id", "Status", "Modules");

			// Token: 0x040030F6 RID: 12534
			private static readonly TableTypeValue librariesType = TableTypeValue.New(RecordTypeValue.New(EnvironmentModule.LibrariesFunctionValue.librariesKeys));

			// Token: 0x040030F7 RID: 12535
			private static readonly Keys dataSourcesKeys = Keys.New(new string[] { "DataSourceKind", "Label", "AuthenticationInfos", "Properties", "ApplicationProperties", "PermissionKinds", "DataSourceRecord" });

			// Token: 0x040030F8 RID: 12536
			private static readonly Keys exportKeys = Keys.New("Name", "Type", "DataSourceKind", "Publish");

			// Token: 0x040030F9 RID: 12537
			private static readonly Keys authenticationKeys = Keys.New("Kind", "Properties", "ApplicationProperties");

			// Token: 0x040030FA RID: 12538
			private static readonly Keys propertyKeys = Keys.New("Name", "Label", "PropertyType");

			// Token: 0x040030FB RID: 12539
			private readonly IEngineHost engineHost;

			// Token: 0x040030FC RID: 12540
			private readonly ICulture defaultCulture;

			// Token: 0x02000C8B RID: 3211
			private class FilterBuiltinsFunctionValue : NativeFunctionValue1<LogicalValue, RecordValue>
			{
				// Token: 0x060056E9 RID: 22249 RVA: 0x0012DF53 File Offset: 0x0012C153
				public FilterBuiltinsFunctionValue()
					: base(TypeValue.Logical, "row", TypeValue.Record)
				{
				}

				// Token: 0x060056EA RID: 22250 RVA: 0x0012DF6C File Offset: 0x0012C16C
				public override LogicalValue TypedInvoke(RecordValue row)
				{
					Value value;
					return LogicalValue.New(!row.TryGetValue("Id", out value) || !value.IsText || value.AsString != "Builtin");
				}
			}

			// Token: 0x02000C8C RID: 3212
			private class CreateLibrariesTableFunctionValue : NativeFunctionValue1<TableValue, TableValue>
			{
				// Token: 0x060056EB RID: 22251 RVA: 0x0012DFA8 File Offset: 0x0012C1A8
				public CreateLibrariesTableFunctionValue(ILibraryService libraryService, string culture, bool returnAad)
					: base(TypeValue.Table, "table", TypeValue.Table)
				{
					this.libraryService = libraryService;
					this.culture = culture;
					this.returnAad = returnAad;
				}

				// Token: 0x060056EC RID: 22252 RVA: 0x0012DFD4 File Offset: 0x0012C1D4
				public override TableValue TypedInvoke(TableValue table)
				{
					return new EnvironmentModule.LibrariesFunctionValue.CreateLibrariesTableFunctionValue.CreateLibrariesTableValue(this.libraryService, this.culture, table, this.returnAad);
				}

				// Token: 0x040030FD RID: 12541
				private readonly ILibraryService libraryService;

				// Token: 0x040030FE RID: 12542
				private readonly string culture;

				// Token: 0x040030FF RID: 12543
				private readonly bool returnAad;

				// Token: 0x02000C8D RID: 3213
				private class CreateLibrariesTableValue : TableValue
				{
					// Token: 0x060056ED RID: 22253 RVA: 0x0012DFEE File Offset: 0x0012C1EE
					public CreateLibrariesTableValue(ILibraryService libraryService, string culture, TableValue baseTable, bool returnAad)
					{
						this.libraryService = libraryService;
						this.culture = culture;
						this.baseTable = baseTable;
						this.returnAad = returnAad;
					}

					// Token: 0x17001A3C RID: 6716
					// (get) Token: 0x060056EE RID: 22254 RVA: 0x0012E013 File Offset: 0x0012C213
					public override TypeValue Type
					{
						get
						{
							return EnvironmentModule.LibrariesFunctionValue.librariesType;
						}
					}

					// Token: 0x17001A3D RID: 6717
					// (get) Token: 0x060056EF RID: 22255 RVA: 0x0012E01A File Offset: 0x0012C21A
					private static FunctionValue TransformAadFunctionValue
					{
						get
						{
							if (EnvironmentModule.LibrariesFunctionValue.CreateLibrariesTableFunctionValue.CreateLibrariesTableValue.transformAadFunctionValue == null)
							{
								EnvironmentModule.LibrariesFunctionValue.CreateLibrariesTableFunctionValue.CreateLibrariesTableValue.transformAadFunctionValue = LanguageLibrary.Evaluate("(t) => Table.TransformColumns(t, {{\"AuthenticationInfos\", (a) => Table.TransformColumns(a, {{\"Kind\", (k) => if k = \"AAD\" then \"OAuth\" else k}})}})", LibraryHelper.StandardLibrary).AsFunction;
							}
							return EnvironmentModule.LibrariesFunctionValue.CreateLibrariesTableFunctionValue.CreateLibrariesTableValue.transformAadFunctionValue;
						}
					}

					// Token: 0x060056F0 RID: 22256 RVA: 0x0012E041 File Offset: 0x0012C241
					public override IEnumerator<IValueReference> GetEnumerator()
					{
						using (IEnumerator<IValueReference> enumerator = this.baseTable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								IValueReference row = enumerator.Current;
								yield return RecordValue.New(EnvironmentModule.LibrariesFunctionValue.librariesKeys, delegate(int idx)
								{
									switch (idx)
									{
									case 0:
										return row.Value["LibraryId"];
									case 1:
										return row.Value["Status"];
									case 2:
										return this.GetLibraries(row, row.Value["FullIdentifier"]);
									default:
										throw new InvalidOperationException();
									}
								});
							}
						}
						IEnumerator<IValueReference> enumerator = null;
						yield break;
						yield break;
					}

					// Token: 0x060056F1 RID: 22257 RVA: 0x0012E050 File Offset: 0x0012C250
					private Value GetLibraries(IValueReference row, Value libraryIdentifier)
					{
						if (libraryIdentifier.IsNull)
						{
							return Value.Null;
						}
						string libraryId = libraryIdentifier.AsText.String;
						return TableModule.Table.FromRecords.Invoke(ListValue.New(new Value[] { RecordValue.New(EnvironmentModule.LibrariesFunctionValue.modulesKeys, delegate(int index)
						{
							switch (index)
							{
							case 0:
								return row.Value["Name"];
							case 1:
								return row.Value["Version"];
							case 2:
								return row.Value["Metadata"];
							case 3:
								return row.Value["Dependencies"];
							case 4:
								return (TableValue)this.libraryService.GetLibraryExports(this.culture, libraryId).GetValue().AsTable;
							case 5:
								return this.GetDataSources(libraryId);
							default:
								throw new InvalidOperationException();
							}
						}) }));
					}

					// Token: 0x060056F2 RID: 22258 RVA: 0x0012E0C0 File Offset: 0x0012C2C0
					private Value GetDataSources(string libraryId)
					{
						Value value = (TableValue)this.libraryService.GetLibraryDataSources(this.culture, libraryId).GetValue().AsTable;
						if (!this.returnAad)
						{
							value = EnvironmentModule.LibrariesFunctionValue.CreateLibrariesTableFunctionValue.CreateLibrariesTableValue.TransformAadFunctionValue.Invoke(value);
						}
						return value;
					}

					// Token: 0x04003100 RID: 12544
					private const string transformAad = "(t) => Table.TransformColumns(t, {{\"AuthenticationInfos\", (a) => Table.TransformColumns(a, {{\"Kind\", (k) => if k = \"AAD\" then \"OAuth\" else k}})}})";

					// Token: 0x04003101 RID: 12545
					private static FunctionValue transformAadFunctionValue;

					// Token: 0x04003102 RID: 12546
					private readonly ILibraryService libraryService;

					// Token: 0x04003103 RID: 12547
					private readonly string culture;

					// Token: 0x04003104 RID: 12548
					private readonly TableValue baseTable;

					// Token: 0x04003105 RID: 12549
					private readonly bool returnAad;
				}
			}
		}

		// Token: 0x02000C94 RID: 3220
		private sealed class ConfigurationFunctionValue : NativeFunctionValue0<RecordValue>
		{
			// Token: 0x06005707 RID: 22279 RVA: 0x0012E3C1 File Offset: 0x0012C5C1
			public ConfigurationFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Record)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x06005708 RID: 22280 RVA: 0x0012E3D8 File Offset: 0x0012C5D8
			public override RecordValue TypedInvoke()
			{
				IConfigurationPropertyService configurationPropertyService = this.engineHost.QueryService<IConfigurationPropertyService>();
				if (configurationPropertyService == null)
				{
					return RecordValue.Empty;
				}
				RecordBuilder recordBuilder = new RecordBuilder(configurationPropertyService.Values.Count);
				foreach (KeyValuePair<string, object> keyValuePair in configurationPropertyService.Values)
				{
					recordBuilder.Add(keyValuePair.Key, ValueMarshaller.MarshalFromClr(keyValuePair.Value), TypeValue.Any);
				}
				return recordBuilder.ToRecord();
			}

			// Token: 0x04003115 RID: 12565
			private readonly IEngineHost engineHost;
		}

		// Token: 0x02000C95 RID: 3221
		private sealed class FeatureSwitchFunctionValue : NativeFunctionValue2<Value, TextValue, Value>
		{
			// Token: 0x06005709 RID: 22281 RVA: 0x0012E46C File Offset: 0x0012C66C
			public FeatureSwitchFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Any, 1, "name", TypeValue.Text, "default", TypeValue.Any)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x0600570A RID: 22282 RVA: 0x0012E498 File Offset: 0x0012C698
			public override Value TypedInvoke(TextValue name, Value @default)
			{
				object obj;
				if (this.engineHost.TryGetConfigurationProperty(name.String, out obj))
				{
					return ValueMarshaller.MarshalFromClr(obj);
				}
				return @default;
			}

			// Token: 0x04003116 RID: 12566
			private readonly IEngineHost engineHost;
		}
	}
}
