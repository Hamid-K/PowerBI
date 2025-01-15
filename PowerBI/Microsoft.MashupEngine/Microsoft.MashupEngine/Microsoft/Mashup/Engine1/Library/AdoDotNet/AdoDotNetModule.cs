using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdoDotNet
{
	// Token: 0x02000F42 RID: 3906
	internal class AdoDotNetModule : Module
	{
		// Token: 0x17001DE3 RID: 7651
		// (get) Token: 0x06006746 RID: 26438 RVA: 0x0016368C File Offset: 0x0016188C
		public override string Name
		{
			get
			{
				return "AdoDotNet";
			}
		}

		// Token: 0x17001DE4 RID: 7652
		// (get) Token: 0x06006747 RID: 26439 RVA: 0x00163693 File Offset: 0x00161893
		public override string Location
		{
			get
			{
				return typeof(AdoDotNetModule).Assembly.Location;
			}
		}

		// Token: 0x17001DE5 RID: 7653
		// (get) Token: 0x06006748 RID: 26440 RVA: 0x001636A9 File Offset: 0x001618A9
		public override Keys ExportKeys
		{
			get
			{
				if (AdoDotNetModule.exportKeys == null)
				{
					AdoDotNetModule.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "AdoDotNet.Query";
						}
						if (index != 1)
						{
							throw new InvalidOperationException();
						}
						return "AdoDotNet.DataSource";
					});
				}
				return AdoDotNetModule.exportKeys;
			}
		}

		// Token: 0x17001DE6 RID: 7654
		// (get) Token: 0x06006749 RID: 26441 RVA: 0x001636E1 File Offset: 0x001618E1
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { AdoDotNetResourceKindInfo.Instance };
			}
		}

		// Token: 0x0600674A RID: 26442 RVA: 0x001636F4 File Offset: 0x001618F4
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new AdoDotNetModule.QueryFunctionValue(host);
				}
				if (index != 1)
				{
					throw new InvalidOperationException();
				}
				return new AdoDotNetModule.DataSourceFunctionValue(host);
			});
		}

		// Token: 0x0600674B RID: 26443 RVA: 0x00163728 File Offset: 0x00161928
		private static bool ConvertTypeMap(Value option, out object value)
		{
			DbCommandTypeMap dbCommandTypeMap;
			if (DbCommandTypeMap.TryFromValue(option, out dbCommandTypeMap))
			{
				value = dbCommandTypeMap;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x0600674C RID: 26444 RVA: 0x00163748 File Offset: 0x00161948
		private static bool TryGetLocation(Value[] args, out IDataSourceLocation location)
		{
			if (!args[0].IsText || (!args[1].IsText && !args[1].IsRecord))
			{
				location = null;
				return false;
			}
			Dictionary<string, string> dictionary = ConnectionStringHandler.HandleFormatExceptions<Dictionary<string, string>>("ADO.NET", args[1], () => AdoDotNetEnvironment.ConnectionString.GetKeyValuePairs(AdoDotNetEnvironment.ConnectionString.GetString(args[1])));
			AdoDotNetDataSourceLocation adoDotNetDataSourceLocation = new AdoDotNetDataSourceLocation();
			adoDotNetDataSourceLocation.Options = dictionary.ToDictionary((KeyValuePair<string, string> kvp) => kvp.Key, (KeyValuePair<string, string> kvp) => kvp.Value);
			location = adoDotNetDataSourceLocation;
			location.Address["provider"] = args[0].AsString;
			return true;
		}

		// Token: 0x0600674D RID: 26445 RVA: 0x00163824 File Offset: 0x00161A24
		private static IResource GetResource(IEngineHost host, Value providerName, Value connectionAttributes)
		{
			IExtensibilityService extensibilityService = host.QueryService<IExtensibilityService>();
			if (extensibilityService != null && extensibilityService.CurrentResource != null && extensibilityService.ImpersonateResource)
			{
				return extensibilityService.CurrentResource;
			}
			string message;
			try
			{
				string @string = AdoDotNetEnvironment.ConnectionString.GetString(connectionAttributes);
				IResource resource;
				if (AdoDotNetResourceKindInfo.Instance.TryCreateResource(providerName.AsString, @string, out resource, out message))
				{
					return resource;
				}
			}
			catch (FormatException ex)
			{
				message = ex.Message;
			}
			throw ValueException.NewExpressionError<Message2>(DataSourceException.DataSourceMessage("ADO.NET", message), connectionAttributes, null);
		}

		// Token: 0x040038C3 RID: 14531
		public const string ResourceKind = "AdoDotNet";

		// Token: 0x040038C4 RID: 14532
		public const string DataSourceName = "ADO.NET";

		// Token: 0x040038C5 RID: 14533
		internal static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("SqlCompatibleWindowsAuth", NullableTypeValue.Logical),
			new OptionItem("TypeMap", NullableTypeValue.List, Value.Null, OptionItemOption.None, new TryConvertOption(AdoDotNetModule.ConvertTypeMap), null)
		});

		// Token: 0x040038C6 RID: 14534
		private static readonly Keys QueryOptionKeys = Keys.New("Query");

		// Token: 0x040038C7 RID: 14535
		private static Keys exportKeys;

		// Token: 0x02000F43 RID: 3907
		private enum Exports
		{
			// Token: 0x040038C9 RID: 14537
			Query,
			// Token: 0x040038CA RID: 14538
			DataSource,
			// Token: 0x040038CB RID: 14539
			Count
		}

		// Token: 0x02000F44 RID: 3908
		private sealed class QueryFunctionValue : NativeFunctionValue4<TableValue, TextValue, Value, TextValue, Value>
		{
			// Token: 0x06006750 RID: 26448 RVA: 0x00163924 File Offset: 0x00161B24
			public QueryFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 3, "providerName", TypeValue.Text, "connectionString", TypeValue.Any, "query", TypeValue.Text, "options", AdoDotNetModule.QueryFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x17001DE7 RID: 7655
			// (get) Token: 0x06006751 RID: 26449 RVA: 0x0016368C File Offset: 0x0016188C
			public override string PrimaryResourceKind
			{
				get
				{
					return "AdoDotNet";
				}
			}

			// Token: 0x06006752 RID: 26450 RVA: 0x0016396C File Offset: 0x00161B6C
			public override TableValue TypedInvoke(TextValue providerName, Value connectionProperties, TextValue query, Value options)
			{
				IResource resource = AdoDotNetModule.GetResource(this.host, providerName, connectionProperties);
				RecordValue recordValue = RecordValue.New(AdoDotNetModule.QueryOptionKeys, new Value[] { query });
				return new AdoDotNetEnvironment(this.host, resource, providerName.AsString, connectionProperties, options.IsNull ? recordValue : options.Concatenate(recordValue)).CreateTable();
			}

			// Token: 0x06006753 RID: 26451 RVA: 0x001639C8 File Offset: 0x00161BC8
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues != null && argumentValues.Length >= 3 && argumentValues.Length <= 4 && AdoDotNetModule.TryGetLocation(argumentValues, out location))
				{
					Value value = ((argumentValues.Length > 3) ? argumentValues[3] : Value.Null);
					if (value.IsRecord)
					{
						foundOptions = ExpressionAnalysis.RemovePlaceholders(value.AsRecord, out unknownOptions);
					}
					else
					{
						foundOptions = RecordValue.Empty;
						unknownOptions = Keys.Empty;
					}
					if (argumentValues[2].IsText)
					{
						location.Query = argumentValues[2].AsString;
					}
					else
					{
						unknownOptions = Keys.New(unknownOptions.Concat(StaticAnalysisResolver.NativeQueryKeys).ToArray<string>());
					}
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x040038CC RID: 14540
			private static readonly TypeValue optionsType = AdoDotNetModule.OptionRecord.Remove(new string[] { "TypeMap" }).CreateRecordType().Nullable;

			// Token: 0x040038CD RID: 14541
			private readonly IEngineHost host;
		}

		// Token: 0x02000F45 RID: 3909
		private sealed class DataSourceFunctionValue : NativeFunctionValue3<TableValue, TextValue, Value, Value>
		{
			// Token: 0x06006755 RID: 26453 RVA: 0x00163A9C File Offset: 0x00161C9C
			public DataSourceFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 2, "providerName", TypeValue.Text, "connectionString", TypeValue.Any, "options", AdoDotNetModule.DataSourceFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x17001DE8 RID: 7656
			// (get) Token: 0x06006756 RID: 26454 RVA: 0x0016368C File Offset: 0x0016188C
			public override string PrimaryResourceKind
			{
				get
				{
					return "AdoDotNet";
				}
			}

			// Token: 0x06006757 RID: 26455 RVA: 0x00163ADC File Offset: 0x00161CDC
			public override TableValue TypedInvoke(TextValue providerName, Value connectionProperties, Value options)
			{
				IResource resource = AdoDotNetModule.GetResource(this.host, providerName, connectionProperties);
				return new AdoDotNetEnvironment(this.host, resource, providerName.AsString, connectionProperties, options).CreateTable();
			}

			// Token: 0x06006758 RID: 26456 RVA: 0x00163B10 File Offset: 0x00161D10
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues != null && argumentValues.Length >= 2 && argumentValues.Length <= 3 && AdoDotNetModule.TryGetLocation(argumentValues, out location))
				{
					Value value = ((argumentValues.Length > 2) ? argumentValues[2] : Value.Null);
					if (value.IsRecord)
					{
						foundOptions = ExpressionAnalysis.RemovePlaceholders(value.AsRecord, out unknownOptions);
					}
					else
					{
						foundOptions = RecordValue.Empty;
						unknownOptions = Keys.Empty;
					}
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x040038CE RID: 14542
			private static readonly TypeValue optionsType = AdoDotNetModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x040038CF RID: 14543
			private readonly IEngineHost host;
		}
	}
}
