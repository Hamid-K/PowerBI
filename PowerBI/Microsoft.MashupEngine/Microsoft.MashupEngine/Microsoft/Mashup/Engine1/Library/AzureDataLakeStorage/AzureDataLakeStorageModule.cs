using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.AzureBlobs;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000EE4 RID: 3812
	internal sealed class AzureDataLakeStorageModule : Module
	{
		// Token: 0x17001D73 RID: 7539
		// (get) Token: 0x06006534 RID: 25908 RVA: 0x0015B73B File Offset: 0x0015993B
		public override string Name
		{
			get
			{
				return "AzureDataLakeStorage";
			}
		}

		// Token: 0x17001D74 RID: 7540
		// (get) Token: 0x06006535 RID: 25909 RVA: 0x0015B742 File Offset: 0x00159942
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
							return "AzureStorage.DataLake";
						}
						if (index == 1)
						{
							return "AzureStorage.DataLakeContents";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17001D75 RID: 7541
		// (get) Token: 0x06006536 RID: 25910 RVA: 0x0015B77D File Offset: 0x0015997D
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { AzureDataLakeStorageModule.resourceKindInfo };
			}
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x0015B790 File Offset: 0x00159990
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new AzureDataLakeStorageModule.DataLakeFunctionValue(hostEnvironment);
				}
				if (index == 1)
				{
					return new AzureDataLakeStorageModule.DataLakeContentsFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x0015B7C4 File Offset: 0x001599C4
		private static string CreateAdlsAuthDiscoverEndpoint(string resourceUrl)
		{
			Uri uri = new Uri(resourceUrl);
			return string.Format(CultureInfo.CurrentCulture, "https://{0}/?resource=account", uri.Host);
		}

		// Token: 0x0400376D RID: 14189
		private const string DataLakeContents = "AzureStorage.DataLakeContents";

		// Token: 0x0400376E RID: 14190
		private const string DataLake = "AzureStorage.DataLake";

		// Token: 0x0400376F RID: 14191
		public static readonly OptionRecordDefinition SupportedOptions = AzureBlobsModule.SupportedOptions.Concatenate(new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.New(false), OptionItemOption.None, null, "Directory"),
			new OptionItem("IsOneLake", NullableTypeValue.Logical, LogicalValue.New(false), OptionItemOption.ExcludeFromOptionType, null, null)
		}));

		// Token: 0x04003770 RID: 14192
		private static readonly ResourceKindInfo resourceKindInfo = new SasResourceKindInfo("AzureDataLakeStorage", null, new AuthenticationInfo[]
		{
			ResourceHelpers.AnonymousAuth,
			new KeyAuthenticationInfo(),
			new AadAuthenticationInfo
			{
				ClientApplicationType = OAuthClientApplicationType.Required,
				Label = Strings.Microsoft_OAuth,
				ProviderFactory = new OAuthFactory((OAuthServices services, string url) => ResourceHelpers.AadForStorage(services, AzureDataLakeStorageModule.CreateAdlsAuthDiscoverEndpoint(url), null), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, ResourceHelpers.AadForStorage(services, AzureDataLakeStorageModule.CreateAdlsAuthDiscoverEndpoint(url), null)))
			},
			ResourceHelpers.SasAuthInfo
		}, null, false, false, false, new string[] { "ServiceAccessToken", "CapacityId" }, new DataSourceLocationFactory[] { AzureDataLakeStorageDataSourceLocation.Factory });

		// Token: 0x04003771 RID: 14193
		private Keys exportKeys;

		// Token: 0x02000EE5 RID: 3813
		private enum Exports
		{
			// Token: 0x04003773 RID: 14195
			DataLake,
			// Token: 0x04003774 RID: 14196
			DataLakeContents,
			// Token: 0x04003775 RID: 14197
			Count
		}

		// Token: 0x02000EE6 RID: 3814
		public sealed class DataLakeContentsFunctionValue : AzureBlobsModule.ContentsFunctionValue
		{
			// Token: 0x0600653B RID: 25915 RVA: 0x0015B8F1 File Offset: 0x00159AF1
			public DataLakeContentsFunctionValue(IEngineHost host)
				: base(host)
			{
			}

			// Token: 0x17001D76 RID: 7542
			// (get) Token: 0x0600653C RID: 25916 RVA: 0x0015B73B File Offset: 0x0015993B
			protected override string ResourceKind
			{
				get
				{
					return "AzureDataLakeStorage";
				}
			}

			// Token: 0x17001D77 RID: 7543
			// (get) Token: 0x0600653D RID: 25917 RVA: 0x0015B8FA File Offset: 0x00159AFA
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return new FunctionTypeIdentity(typeof(AzureDataLakeStorageModule.DataLakeContentsFunctionValue));
				}
			}

			// Token: 0x0600653E RID: 25918 RVA: 0x0015B90C File Offset: 0x00159B0C
			public override BinaryValue TypedInvoke(TextValue url, Value options)
			{
				OptionsRecord optionsRecord = AzureBlobsModule.SupportedOptions.CreateOptions("AzureStorage.DataLakeContents", options);
				IResource resource = Resource.New(this.ResourceKind, url.AsString);
				return AdlsBinaryValue.New(this.host, resource, url, optionsRecord, false, null);
			}

			// Token: 0x0600653F RID: 25919 RVA: 0x0015B94C File Offset: 0x00159B4C
			protected override bool TryCreateLocation(IResource resource, out IDataSourceLocation location)
			{
				return AzureDataLakeStorageDataSourceLocation.Factory.TryCreateFromResource(resource, false, out location);
			}
		}

		// Token: 0x02000EE7 RID: 3815
		public sealed class DataLakeFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x06006540 RID: 25920 RVA: 0x0015B95B File Offset: 0x00159B5B
			public DataLakeFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles), 1, "endpoint", TypeValue.Text, "options", AzureDataLakeStorageModule.SupportedOptions.CreateRecordType().Nullable)
			{
				this.host = host;
			}

			// Token: 0x17001D78 RID: 7544
			// (get) Token: 0x06006541 RID: 25921 RVA: 0x0015B73B File Offset: 0x0015993B
			public string ResourceKind
			{
				get
				{
					return "AzureDataLakeStorage";
				}
			}

			// Token: 0x17001D79 RID: 7545
			// (get) Token: 0x06006542 RID: 25922 RVA: 0x0015B98F File Offset: 0x00159B8F
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return new FunctionTypeIdentity(typeof(AzureDataLakeStorageModule.DataLakeFunctionValue));
				}
			}

			// Token: 0x06006543 RID: 25923 RVA: 0x0015B9A0 File Offset: 0x00159BA0
			public override TableValue TypedInvoke(TextValue url, Value options)
			{
				url = this.ValidateAndUpdateParameters(url);
				OptionsRecord optionsRecord = AzureDataLakeStorageModule.SupportedOptions.CreateOptions("AzureStorage.DataLake", options);
				bool @bool = optionsRecord.GetBool("HierarchicalNavigation", false);
				return AdlsFileSystemsTableValue.CreateView(this.host, url, this.ResourceKind, optionsRecord, @bool);
			}

			// Token: 0x17001D7A RID: 7546
			// (get) Token: 0x06006544 RID: 25924 RVA: 0x0015B9E8 File Offset: 0x00159BE8
			public override string PrimaryResourceKind
			{
				get
				{
					return this.ResourceKind;
				}
			}

			// Token: 0x06006545 RID: 25925 RVA: 0x0015B9F0 File Offset: 0x00159BF0
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (AzureDataLakeStorageModule.DataLakeFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("endpoint", out value) && value.IsText)
				{
					foundOptions = RecordValue.Empty;
					unknownOptions = Keys.Empty;
					IExpression expression2;
					if (dictionary.TryGetValue("options", out expression2))
					{
						Value value2 = ExpressionAnalysis.GetValue(expression2);
						if (value2.IsRecord)
						{
							foundOptions = ExpressionAnalysis.RemovePlaceholders(value2.AsRecord, out unknownOptions);
						}
					}
					value = this.ValidateAndUpdateParameters(value.AsText);
					string @string = AdlsHelper.GetHttpUri(value.AsText, null).String;
					IResource resource = Resource.New(this.ResourceKind, @string);
					return AzureDataLakeStorageDataSourceLocation.Factory.TryCreateFromResource(resource, false, out location);
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x06006546 RID: 25926 RVA: 0x0015BAB8 File Offset: 0x00159CB8
			private TextValue ValidateAndUpdateParameters(TextValue url)
			{
				if (!url.String.Contains("."))
				{
					throw ValueException.NewDataFormatError<Message0>(Strings.AzureInvalidAccountURL, url, null);
				}
				if (!url.String.EndsWith("/", StringComparison.Ordinal))
				{
					url = TextValue.New(url.String + "/");
				}
				bool flag;
				AzureUtilities.ValidateParameters(url, null, AzureUtilities.Validation.AllowDirectory, out flag);
				return url;
			}

			// Token: 0x04003776 RID: 14198
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__endpoint, _o_options)" });

			// Token: 0x04003777 RID: 14199
			private readonly IEngineHost host;
		}
	}
}
