using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BE5 RID: 3045
	internal class ExchangeModule : Module
	{
		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06005301 RID: 21249 RVA: 0x00118D3D File Offset: 0x00116F3D
		public override string Name
		{
			get
			{
				return "Exchange";
			}
		}

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x00118D44 File Offset: 0x00116F44
		public override Keys ExportKeys
		{
			get
			{
				if (ExchangeModule.exportKeys == null)
				{
					ExchangeModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Exchange.Contents";
						}
						throw new InvalidOperationException();
					});
				}
				return ExchangeModule.exportKeys;
			}
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x00118D7C File Offset: 0x00116F7C
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { ExchangeModule.resourceKindInfo };
			}
		}

		// Token: 0x06005304 RID: 21252 RVA: 0x00118D8C File Offset: 0x00116F8C
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new ExchangeModule.ContentsFunctionValue(host);
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x04002DD6 RID: 11734
		public const string DataSourceNameString = "Exchange";

		// Token: 0x04002DD7 RID: 11735
		private static readonly ResourceKindInfo resourceKindInfo = new ExchangeResourceKindInfo();

		// Token: 0x04002DD8 RID: 11736
		private static Keys exportKeys;

		// Token: 0x02000BE6 RID: 3046
		private enum Exports
		{
			// Token: 0x04002DDA RID: 11738
			Contents,
			// Token: 0x04002DDB RID: 11739
			Count
		}

		// Token: 0x02000BE7 RID: 3047
		private sealed class ContentsFunctionValue : NativeFunctionValue1<TableValue, Value>
		{
			// Token: 0x06005307 RID: 21255 RVA: 0x00118DC9 File Offset: 0x00116FC9
			public ContentsFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 0, "mailboxAddress", NullableTypeValue.Text)
			{
				this.host = host;
			}

			// Token: 0x06005308 RID: 21256 RVA: 0x00118DE8 File Offset: 0x00116FE8
			public override TableValue TypedInvoke(Value mailboxAddressValue)
			{
				IResource resource = Resource.New("Exchange", mailboxAddressValue.IsNull ? "Exchange" : mailboxAddressValue.AsString);
				ResourceCredentialCollection resourceCredentialCollection = HostResourcePermissionService.VerifyPermissionAndGetCredentials(this.host, resource, null);
				ExchangeCredentialAdornment exchangeCredentialAdornment;
				IResourceCredential credential = ExchangeHelper.GetCredential(this.host, resource, resourceCredentialCollection, out exchangeCredentialAdornment);
				BasicAuthCredential basicAuthCredential = credential as BasicAuthCredential;
				string mailbox;
				if (mailboxAddressValue.IsNull)
				{
					if (string.IsNullOrEmpty(exchangeCredentialAdornment.EmailAddress))
					{
						throw DataSourceException.NewInvalidCredentialsError(this.host, resource, Strings.Resource_EmailAddressAndMailboxNull, null, null);
					}
					mailbox = exchangeCredentialAdornment.EmailAddress;
				}
				else
				{
					mailbox = mailboxAddressValue.AsString;
				}
				if (basicAuthCredential != null)
				{
					if (string.IsNullOrEmpty(basicAuthCredential.Username))
					{
						basicAuthCredential = new BasicAuthCredential(exchangeCredentialAdornment.EmailAddress, basicAuthCredential.Password);
						credential = basicAuthCredential;
					}
					if (mailbox != exchangeCredentialAdornment.EmailAddress && exchangeCredentialAdornment.EmailAddress != basicAuthCredential.Username)
					{
						throw DataSourceException.NewInvalidCredentialsError(this.host, resource, Strings.Resource_ExchangeCredental_Invalid, Strings.Resource_ExchangeCredental_Invalid, null);
					}
				}
				EngineAutodiscoverService engineAutodiscoverService = new EngineAutodiscoverService(this.host, resource, credential, exchangeCredentialAdornment, resourceCredentialCollection.GetHash(), mailbox);
				ExchangeUserSettings exchangeUserSettings = engineAutodiscoverService.GetUserSettings();
				Uri serverAddress;
				try
				{
					serverAddress = new Uri(exchangeUserSettings.EwsUrl);
				}
				catch (UriFormatException ex)
				{
					throw DataSourceException.NewInvalidCredentialsError(this.host, resource, Strings.Resource_ExchangeCredental_Invalid, Strings.Resource_ExchangeCredental_Invalid, ex);
				}
				IExchangeService exchangeService = this.host.Hook(() => new EngineExchangeService(resource, credential, this.host, mailbox, serverAddress, exchangeUserSettings.ExchangeVersion));
				IExchangeService exchangeService2 = new ExchangeCachingService(this.host, exchangeService, resourceCredentialCollection.GetHash(), mailbox);
				IValueReference[] array = new IValueReference[ExchangeHelper.Views.Length];
				for (int i = 0; i < array.Length; i++)
				{
					string text = ExchangeHelper.Views[i];
					ExchangeCatalog exchangeCatalog = ExchangeHelper.GetExchangeCatalog(exchangeUserSettings.ExchangeVersion, text);
					array[i] = this.GetExchangeCategoryEntry(text, new ExchangeTableValue(exchangeUserSettings.ExchangeVersion, exchangeService2, this.host, resource, exchangeCatalog, mailbox));
				}
				return new ExchangeConnectionTestingTableValue(new ListTableValue(ListValue.New(array), NavigationTableServices.DefaultTypeValue));
			}

			// Token: 0x06005309 RID: 21257 RVA: 0x00119064 File Offset: 0x00117264
			private RecordValue GetExchangeCategoryEntry(string name, Value value)
			{
				return RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
				{
					TextValue.New(name),
					value
				});
			}

			// Token: 0x17001994 RID: 6548
			// (get) Token: 0x0600530A RID: 21258 RVA: 0x00118D3D File Offset: 0x00116F3D
			public override string PrimaryResourceKind
			{
				get
				{
					return "Exchange";
				}
			}

			// Token: 0x0600530B RID: 21259 RVA: 0x00119084 File Offset: 0x00117284
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				location = new EwsDataSourceLocation();
				if (argumentValues != null && argumentValues.Length == 1 && argumentValues[0].IsText)
				{
					location.Address["emailAddress"] = argumentValues[0].AsString;
				}
				foundOptions = RecordValue.Empty;
				unknownOptions = Keys.Empty;
				return true;
			}

			// Token: 0x04002DDC RID: 11740
			private readonly IEngineHost host;
		}
	}
}
