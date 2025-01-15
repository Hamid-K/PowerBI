using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x0200048B RID: 1163
	internal sealed class SapBusinessWarehouseModule : Module
	{
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x000700A8 File Offset: 0x0006E2A8
		public override string Name
		{
			get
			{
				return "SapBusinessWarehouse";
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x000700AF File Offset: 0x0006E2AF
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(5, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "SapBusinessWarehouse.Cubes";
						case 1:
							return SapBusinessWarehouseExecutionMode.Type.GetName();
						case 2:
							return SapBusinessWarehouseExecutionMode.DataStream.GetName();
						case 3:
							return SapBusinessWarehouseExecutionMode.BasXml.GetName();
						case 4:
							return SapBusinessWarehouseExecutionMode.BasXmlGzip.GetName();
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x000700EA File Offset: 0x0006E2EA
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { SapBusinessWarehouseResourceKindInfo.Instance };
			}
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000700FC File Offset: 0x0006E2FC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new SapBusinessWarehouseModule.CubesFunctionValue(host);
				case 1:
					return SapBusinessWarehouseExecutionMode.Type;
				case 2:
					return SapBusinessWarehouseExecutionMode.DataStream;
				case 3:
					return SapBusinessWarehouseExecutionMode.BasXml;
				case 4:
					return SapBusinessWarehouseExecutionMode.BasXmlGzip;
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x04001015 RID: 4117
		public const string DataSourceName = "SAP Business Warehouse";

		// Token: 0x04001016 RID: 4118
		public const string CubesFunctionName = "SapBusinessWarehouse.Cubes";

		// Token: 0x04001017 RID: 4119
		public const int DefaultBatchSize = 50000;

		// Token: 0x04001018 RID: 4120
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Culture", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SAP"),
			new OptionItem("LanguageCode", NullableTypeValue.Text),
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "MDX"),
			new OptionItem("ScaleMeasures", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, "SAPBW"),
			new OptionItem("EnableStructures", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, "SAPBW"),
			new OptionItem("Implementation", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SAPBW"),
			new OptionItem("ExecutionMode", SapBusinessWarehouseExecutionMode.Type.Nullable, SapBusinessWarehouseExecutionMode.BasXmlGzip, OptionItemOption.None, null, "SAPBW"),
			new OptionItem("BatchSize", NullableTypeValue.Int32, NumberValue.New(50000), OptionItemOption.None, null, "SAPBW")
		});

		// Token: 0x04001019 RID: 4121
		private Keys exportKeys;

		// Token: 0x0200048C RID: 1164
		private enum Exports
		{
			// Token: 0x0400101B RID: 4123
			Cubes,
			// Token: 0x0400101C RID: 4124
			SapBusinessWarehouseExecutionMode_Type,
			// Token: 0x0400101D RID: 4125
			SapBusinessWarehouseExecutionMode_DataStream,
			// Token: 0x0400101E RID: 4126
			SapBusinessWarehouseExecutionMode_BasXml,
			// Token: 0x0400101F RID: 4127
			SapBusinessWarehouseExecutionMode_BasXmlGzip,
			// Token: 0x04001020 RID: 4128
			Count
		}

		// Token: 0x0200048D RID: 1165
		private sealed class CubesFunctionValue : NativeFunctionValue5<TableValue, TextValue, TextValue, TextValue, Value, Value>
		{
			// Token: 0x060026AC RID: 9900 RVA: 0x0007023C File Offset: 0x0006E43C
			public CubesFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 3, "server", TypeValue.Text, "systemNumberOrSystemId", TypeValue.Text, "clientId", TypeValue.Text, "optionsOrLogonGroup", TypeValue.Any, "options", SapBusinessWarehouseModule.CubesFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x17000F69 RID: 3945
			// (get) Token: 0x060026AD RID: 9901 RVA: 0x000700A8 File Offset: 0x0006E2A8
			public override string PrimaryResourceKind
			{
				get
				{
					return "SapBusinessWarehouse";
				}
			}

			// Token: 0x060026AE RID: 9902 RVA: 0x00070290 File Offset: 0x0006E490
			public override TableValue TypedInvoke(TextValue server, TextValue systemNumberOrSystemId, TextValue clientId, Value optionsOrLogonGroup, Value options)
			{
				Value value = Value.Null;
				SapBwRouterString routerString = null;
				SapBwOlapDataSourceLocation location;
				if (optionsOrLogonGroup.IsNull || optionsOrLogonGroup.IsRecord)
				{
					if (optionsOrLogonGroup.IsRecord)
					{
						value = optionsOrLogonGroup;
					}
					else if (options.IsRecord)
					{
						value = options;
					}
					location = this.GetApplicationHostLocation(server, systemNumberOrSystemId, clientId, optionsOrLogonGroup, out routerString);
				}
				else
				{
					value = options;
					location = this.GetLoadBalancedLocation(server, systemNumberOrSystemId, clientId, optionsOrLogonGroup, options, out routerString);
					string asString = optionsOrLogonGroup.AsString;
				}
				IResource resource;
				location.TryGetResource(out resource);
				SapBwOptions sapBwOptions = SapBwOptions.New(this.host, "SAP Business Warehouse", value);
				Func<SapBwService> func = () => new SapBwMicrosoftService(this.host, resource, sapBwOptions, location, routerString, this.host.Hook(() => SapBwMicrosoftProviderFactoryService.Instance));
				ISapBwService sapBwService;
				Func<SapBwService> func2;
				if (sapBwOptions.IsVersion2)
				{
					sapBwService = func();
					func2 = null;
				}
				else
				{
					ISapBwLegacyServiceFactory sapBwLegacyServiceFactory = this.host.Hook(() => null);
					ISapBwService sapBwService2;
					if (sapBwLegacyServiceFactory != null)
					{
						sapBwService2 = sapBwLegacyServiceFactory.CreateTestService(this.host, resource, sapBwOptions, location, routerString, this.host.Hook(() => null));
					}
					else
					{
						ISapBwService sapBwService3 = new SapBwSapClientService();
						sapBwService2 = sapBwService3;
					}
					sapBwService = sapBwService2;
					func2 = func;
				}
				string text;
				TableValue tableValue = (sapBwOptions.TryGetQuery(out text) ? new SapBwNativeQueryTableValue(this.host, text, sapBwService) : SapBwCatalogsTableValue.New(sapBwService));
				return new SapBwTestConnectionTableValue(sapBwService, func2, tableValue);
			}

			// Token: 0x060026AF RID: 9903 RVA: 0x00070438 File Offset: 0x0006E638
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				SapBwRouterString sapBwRouterString = null;
				Dictionary<string, IExpression> dictionary;
				Value value;
				Value value2;
				Value value3;
				if (!SapBusinessWarehouseModule.CubesFunctionValue.pattern.TryMatch(expression, out dictionary) || !dictionary.TryGetConstant("server", out value) || !dictionary.TryGetConstant("systemNumberOrSystemId", out value2) || !dictionary.TryGetConstant("clientId", out value3))
				{
					location = null;
					foundOptions = null;
					unknownOptions = null;
					return false;
				}
				Value value4;
				SapBwOlapDataSourceLocation sapBwOlapDataSourceLocation;
				string text;
				if (dictionary.TryGetConstant("optionsOrLogonGroup", out value4))
				{
					sapBwOlapDataSourceLocation = ((value4.IsNull || value4.IsRecord) ? this.GetApplicationHostLocation(value, value2, value3, null, out sapBwRouterString) : this.GetLoadBalancedLocation(value, value2, value3, value4, null, out sapBwRouterString));
					text = (value4.IsRecord ? "optionsOrLogonGroup" : "options");
				}
				else
				{
					sapBwOlapDataSourceLocation = this.GetApplicationHostLocation(value, value2, value3, null, out sapBwRouterString);
					text = "optionsOrLogonGroup";
				}
				Value value5;
				if (dictionary.TryGetConstant("database", out value5))
				{
					sapBwOlapDataSourceLocation.Database = value5.AsString;
				}
				if (dictionary.TryGetConstant("cube", out value5))
				{
					sapBwOlapDataSourceLocation.Cube = value5.AsString;
				}
				IExpression @null;
				if (!dictionary.TryGetValue(text, out @null))
				{
					@null = ConstantExpressionSyntaxNode.Null;
				}
				location = sapBwOlapDataSourceLocation;
				bool? flag;
				StaticAnalysisResolver.HandleOptions(@null, location, out foundOptions, out unknownOptions, out flag);
				Value value6;
				if (sapBwRouterString != null && !foundOptions.TryGetValue("Implementation", out value6))
				{
					location = null;
					foundOptions = null;
					unknownOptions = null;
					return false;
				}
				return true;
			}

			// Token: 0x060026B0 RID: 9904 RVA: 0x00070588 File Offset: 0x0006E788
			private SapBwOlapDataSourceLocation GetApplicationHostLocation(Value server, Value systemNumber, Value clientId, Value options, out SapBwRouterString routerString)
			{
				SapBwOlapDataSourceLocation.IsValidServerOrRouterString(SapBwConnectionType.ApplicationHostBased, server.AsString, null, true, out routerString);
				SapBusinessWarehouseModule.CubesFunctionValue.Validate(new Func<string, bool>(SapBwOlapDataSourceLocation.IsValidSystemNumber), Strings.SapBwInvalidSystemNumber, systemNumber);
				SapBusinessWarehouseModule.CubesFunctionValue.Validate(new Func<string, bool>(SapBwOlapDataSourceLocation.IsValidClientId), Strings.SapBwInvalidClientId, clientId);
				SapBwOlapDataSourceLocation sapBwOlapDataSourceLocation = new SapBwOlapDataSourceLocation
				{
					Server = server.AsString,
					SystemNumber = systemNumber.AsString,
					ClientId = clientId.AsString
				};
				string text;
				if (options != null && this.TryExtractQuery(options, out text))
				{
					sapBwOlapDataSourceLocation.Query = text;
				}
				return sapBwOlapDataSourceLocation;
			}

			// Token: 0x060026B1 RID: 9905 RVA: 0x00070620 File Offset: 0x0006E820
			private SapBwOlapDataSourceLocation GetLoadBalancedLocation(Value server, Value systemId, Value clientId, Value logonGroup, Value options, out SapBwRouterString routerString)
			{
				SapBwOlapDataSourceLocation.IsValidServerOrRouterString(SapBwConnectionType.LoadBalanced, server.AsString, logonGroup.AsString, true, out routerString);
				SapBusinessWarehouseModule.CubesFunctionValue.Validate(new Func<string, bool>(SapBwOlapDataSourceLocation.IsValidSystemId), Strings.SapBwInvalidSystemId, systemId);
				SapBusinessWarehouseModule.CubesFunctionValue.Validate(new Func<string, bool>(SapBwOlapDataSourceLocation.IsValidClientId), Strings.SapBwInvalidClientId, clientId);
				SapBusinessWarehouseModule.CubesFunctionValue.Validate(new Func<string, bool>(SapBwOlapDataSourceLocation.IsValidGroup), Strings.SapBwInvalidLogonGroup, logonGroup);
				SapBwOlapDataSourceLocation sapBwOlapDataSourceLocation = new SapBwOlapDataSourceLocation
				{
					Server = server.AsString,
					SystemId = systemId.AsString,
					ClientId = clientId.AsString,
					LogonGroup = logonGroup.AsString
				};
				string text;
				if (options != null && this.TryExtractQuery(options, out text))
				{
					sapBwOlapDataSourceLocation.Query = text;
				}
				return sapBwOlapDataSourceLocation;
			}

			// Token: 0x060026B2 RID: 9906 RVA: 0x000706E8 File Offset: 0x0006E8E8
			private bool TryExtractQuery(Value options, out string query)
			{
				return SapBwOptions.New(this.host, "SAP Business Warehouse", options).TryGetQuery(out query);
			}

			// Token: 0x060026B3 RID: 9907 RVA: 0x00070701 File Offset: 0x0006E901
			private static void Validate(Func<string, bool> isValid, string message, Value value)
			{
				if (!isValid(value.AsString))
				{
					throw ValueException.NewExpressionError(message, value, null);
				}
			}

			// Token: 0x04001021 RID: 4129
			private static readonly TypeValue optionsType = SapBusinessWarehouseModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x04001022 RID: 4130
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__server, __systemNumberOrSystemId, __clientId, _o_optionsOrLogonGroup){[Name=__database]}[Data]{[Id=__cube]}[Data]", "__func(__server, __systemNumberOrSystemId, __clientId, _o_optionsOrLogonGroup){[Name=__database]}[Data]", "__func(__server, __systemNumberOrSystemId, __clientId, _o_optionsOrLogonGroup)", "__func(__server, __systemNumberOrSystemId, __clientId, _o_optionsOrLogonGroup, _o_options){[Name=__database]}[Data]{[Id=__cube]}[Data]", "__func(__server, __systemNumberOrSystemId, __clientId, _o_optionsOrLogonGroup, _o_options){[Name=__database]}[Data]", "__func(__server, __systemNumberOrSystemId, __clientId, _o_optionsOrLogonGroup, _o_options)" });

			// Token: 0x04001023 RID: 4131
			private readonly IEngineHost host;
		}
	}
}
