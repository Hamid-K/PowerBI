using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001E0 RID: 480
	internal class SalesforceModule : Module
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00013AF7 File Offset: 0x00011CF7
		public override string Name
		{
			get
			{
				return "Salesforce";
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x00013AFE File Offset: 0x00011CFE
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Salesforce.Data";
						}
						if (index != 1)
						{
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
						return "Salesforce.Reports";
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00013B39 File Offset: 0x00011D39
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { SalesforceModule.resourceKindInfo };
			}
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00013B4C File Offset: 0x00011D4C
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new SalesforceModule.SalesforceDataFunctionValue(hostEnvironment);
				}
				if (index != 1)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				return new SalesforceModule.SalesforceReportsFunctionValue(hostEnvironment);
			});
		}

		// Token: 0x040005BB RID: 1467
		private const string DataSourceName = "Salesforce.com";

		// Token: 0x040005BC RID: 1468
		private const string DefaultLoginUrl = "https://login.salesforce.com";

		// Token: 0x040005BD RID: 1469
		public const string SalesforceData = "Salesforce.Data";

		// Token: 0x040005BE RID: 1470
		public const string SalesforceReports = "Salesforce.Reports";

		// Token: 0x040005BF RID: 1471
		public static readonly OptionRecordDefinition SalesforceDataOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, "False"),
			new OptionItem("ApiVersion", NullableTypeValue.Number, Value.Null, OptionItemOption.None, null, "Salesforce"),
			new OptionItem("Timeout", NullableTypeValue.Duration)
		});

		// Token: 0x040005C0 RID: 1472
		public static readonly OptionRecordDefinition SalesforceReportsOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("ApiVersion", NullableTypeValue.Number, Value.Null, OptionItemOption.None, null, "Salesforce"),
			new OptionItem("Timeout", NullableTypeValue.Duration)
		});

		// Token: 0x040005C1 RID: 1473
		private static readonly ResourceKindInfo resourceKindInfo = new UriResourceKindInfo("Salesforce", Strings.SalesforceChallengeTitle, new AuthenticationInfo[]
		{
			new OAuth2AuthenticationInfo
			{
				Label = Strings.Salesforce_OAuth,
				ClientApplicationType = OAuthClientApplicationType.Required,
				ProviderFactory = new OAuthFactory((OAuthServices services, OAuthClientApplication app, string url) => new SalesforceOAuthProvider(services, app, url))
			}
		}, null, false, false, false, null, new DataSourceLocationFactory[] { SalesforceDataSourceLocation.Factory });

		// Token: 0x040005C2 RID: 1474
		private Keys exportKeys;

		// Token: 0x020001E1 RID: 481
		protected enum Exports
		{
			// Token: 0x040005C4 RID: 1476
			Data,
			// Token: 0x040005C5 RID: 1477
			Reports,
			// Token: 0x040005C6 RID: 1478
			Count
		}

		// Token: 0x020001E2 RID: 482
		private sealed class SalesforceDataFunctionValue : NativeFunctionValue2<TableValue, Value, Value>
		{
			// Token: 0x06000995 RID: 2453 RVA: 0x00013C9F File Offset: 0x00011E9F
			public SalesforceDataFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 0, "loginUrl", NullableTypeValue.Any, "options", SalesforceModule.SalesforceDataFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06000996 RID: 2454 RVA: 0x00013CC8 File Offset: 0x00011EC8
			public override TableValue TypedInvoke(Value loginUrl, Value options)
			{
				string text;
				ValueException ex;
				if (!this.FixParameters(loginUrl, options, out text, out options, out ex))
				{
					throw ex;
				}
				OptionsRecord optionsRecord = SalesforceModule.SalesforceDataOptionRecord.CreateOptions("Salesforce.com", options);
				SalesforceDataLoader salesforceDataLoader = new SalesforceDataLoader(this.host, text, optionsRecord);
				return new SalesforceCatalogTableValue(this.host, salesforceDataLoader, optionsRecord);
			}

			// Token: 0x170002C8 RID: 712
			// (get) Token: 0x06000997 RID: 2455 RVA: 0x00013AF7 File Offset: 0x00011CF7
			public override string PrimaryResourceKind
			{
				get
				{
					return "Salesforce";
				}
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x00013D14 File Offset: 0x00011F14
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues != null)
				{
					Value value = ((argumentValues.Length != 0) ? argumentValues[0] : Value.Null);
					Value value2 = ((argumentValues.Length > 1) ? argumentValues[1] : Value.Null);
					string text;
					Value value3;
					ValueException ex;
					if (value != null && this.FixParameters(value, value2, out text, out value3, out ex))
					{
						IResource resource = Resource.New("Salesforce", text);
						location = new SalesforceDataSourceLocation
						{
							LoginServer = resource.Path,
							Class = "object"
						};
						if (value3.IsRecord)
						{
							foundOptions = ExpressionAnalysis.RemovePlaceholders(value3.AsRecord, out unknownOptions);
						}
						else
						{
							foundOptions = RecordValue.Empty;
							unknownOptions = Keys.Empty;
						}
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x06000999 RID: 2457 RVA: 0x00013DC4 File Offset: 0x00011FC4
			private bool FixParameters(Value arg0, Value arg1, out string loginUrl, out Value options, out ValueException error)
			{
				loginUrl = null;
				options = null;
				error = null;
				if (arg0.IsRecord && !arg1.IsNull)
				{
					error = ValueException.NewExpressionError<Message1>(Strings.ValueException_InvalidArguments_Generic("Salesforce.Data"), arg1, null);
					return false;
				}
				if (arg0.IsNull || arg0.IsRecord)
				{
					loginUrl = "https://login.salesforce.com";
					options = (arg0.IsRecord ? arg0 : arg1);
					return true;
				}
				bool flag;
				try
				{
					loginUrl = arg0.AsText.AsString;
					options = arg1;
					flag = true;
				}
				catch (ValueException ex)
				{
					error = ex;
					flag = false;
				}
				return flag;
			}

			// Token: 0x040005C7 RID: 1479
			private static readonly TypeValue optionsType = SalesforceModule.SalesforceDataOptionRecord.CreateRecordType().Nullable;

			// Token: 0x040005C8 RID: 1480
			private readonly IEngineHost host;
		}

		// Token: 0x020001E3 RID: 483
		private sealed class SalesforceReportsFunctionValue : NativeFunctionValue2<TableValue, Value, Value>
		{
			// Token: 0x0600099B RID: 2459 RVA: 0x00013E72 File Offset: 0x00012072
			public SalesforceReportsFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 0, "loginUrl", NullableTypeValue.Text, "options", SalesforceModule.SalesforceReportsFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x0600099C RID: 2460 RVA: 0x00013E9C File Offset: 0x0001209C
			public override TableValue TypedInvoke(Value loginUrl, Value options)
			{
				string text;
				if (loginUrl.IsText)
				{
					text = loginUrl.AsString;
				}
				else
				{
					text = "https://login.salesforce.com";
				}
				OptionsRecord optionsRecord = SalesforceModule.SalesforceReportsOptionRecord.CreateOptions("Salesforce.com", options);
				SalesforceDataLoader salesforceDataLoader = new SalesforceDataLoader(this.host, text, optionsRecord);
				return new SalesforceReportsTableValue(this.host, salesforceDataLoader);
			}

			// Token: 0x170002C9 RID: 713
			// (get) Token: 0x0600099D RID: 2461 RVA: 0x00013AF7 File Offset: 0x00011CF7
			public override string PrimaryResourceKind
			{
				get
				{
					return "Salesforce";
				}
			}

			// Token: 0x0600099E RID: 2462 RVA: 0x00013EEC File Offset: 0x000120EC
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues != null)
				{
					string text = null;
					if (argumentValues.Length == 0 || argumentValues[0].IsNull)
					{
						text = "https://login.salesforce.com";
					}
					else if (argumentValues[0] != null && argumentValues[0].IsText)
					{
						text = argumentValues[0].AsString;
					}
					if (text != null)
					{
						IResource resource = Resource.New("Salesforce", text);
						location = new SalesforceDataSourceLocation
						{
							LoginServer = resource.Path,
							Class = "report-detail"
						};
						if (argumentValues.Length > 1 && argumentValues[1].IsRecord)
						{
							foundOptions = ExpressionAnalysis.RemovePlaceholders(argumentValues[1].AsRecord, out unknownOptions);
						}
						else
						{
							foundOptions = RecordValue.Empty;
							unknownOptions = Keys.Empty;
						}
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x040005C9 RID: 1481
			private static readonly TypeValue optionsType = SalesforceModule.SalesforceReportsOptionRecord.CreateRecordType().Nullable;

			// Token: 0x040005CA RID: 1482
			private readonly IEngineHost host;
		}
	}
}
