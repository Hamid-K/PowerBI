using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.AzureBlobs
{
	// Token: 0x02000EEA RID: 3818
	internal sealed class AzureBlobsModule : Module
	{
		// Token: 0x17001D7B RID: 7547
		// (get) Token: 0x0600654F RID: 25935 RVA: 0x0015BBD5 File Offset: 0x00159DD5
		public override string Name
		{
			get
			{
				return "AzureBlob";
			}
		}

		// Token: 0x17001D7C RID: 7548
		// (get) Token: 0x06006550 RID: 25936 RVA: 0x0015BBDC File Offset: 0x00159DDC
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
							return "AzureStorage.Blobs";
						}
						if (index != 1)
						{
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
						return "AzureStorage.BlobContents";
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17001D7D RID: 7549
		// (get) Token: 0x06006551 RID: 25937 RVA: 0x0015BC17 File Offset: 0x00159E17
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { AzureBlobsModule.resourceKindInfo };
			}
		}

		// Token: 0x06006552 RID: 25938 RVA: 0x0015BC28 File Offset: 0x00159E28
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new AzureBlobsModule.AccountContainersFunctionValue(hostEnvironment);
				}
				if (index != 1)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				return new AzureBlobsModule.ContentsFunctionValue(hostEnvironment);
			});
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x0015BC5C File Offset: 0x00159E5C
		private static string CreateBlobAuthDiscoverEndpoint(string resourceUrl)
		{
			Uri uri = new Uri(resourceUrl);
			return string.Format(CultureInfo.CurrentCulture, "https://{0}/?restype=service&comp=properties", uri.Host);
		}

		// Token: 0x0400377B RID: 14203
		private const string Containers = "AzureStorage.Blobs";

		// Token: 0x0400377C RID: 14204
		private const string Contents = "AzureStorage.BlobContents";

		// Token: 0x0400377D RID: 14205
		public const string DataSourceNameString = "Azure Blob Storage";

		// Token: 0x0400377E RID: 14206
		public const int defaultBlockSize = 4194304;

		// Token: 0x0400377F RID: 14207
		public const int defaultRequestSize = 4194304;

		// Token: 0x04003780 RID: 14208
		public const int defaultConcurrentRequests = 16;

		// Token: 0x04003781 RID: 14209
		public static readonly OptionRecordDefinition SupportedOptions = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("BlockSize", NullableTypeValue.Number, NumberValue.New(4194304), OptionItemOption.None, null, null),
			new OptionItem("RequestSize", NullableTypeValue.Number, NumberValue.New(4194304), OptionItemOption.None, null, null),
			new OptionItem("ConcurrentRequests", NullableTypeValue.Number, NumberValue.New(16), OptionItemOption.None, null, "Stream")
		});

		// Token: 0x04003782 RID: 14210
		private static readonly KeyValuePair<string, string>[] aadAuthHeaders = new KeyValuePair<string, string>[]
		{
			new KeyValuePair<string, string>("x-ms-version", BlobsHelper.October2021Version.String),
			new KeyValuePair<string, string>("Authorization", null)
		};

		// Token: 0x04003783 RID: 14211
		private static readonly ResourceKindInfo resourceKindInfo = new SasResourceKindInfo("AzureBlobs", null, new AuthenticationInfo[]
		{
			ResourceHelpers.AnonymousAuth,
			new KeyAuthenticationInfo(),
			new AadAuthenticationInfo
			{
				ClientApplicationType = OAuthClientApplicationType.Required,
				Label = Strings.Microsoft_OAuth,
				ProviderFactory = new OAuthFactory((OAuthServices services, string url) => ResourceHelpers.AadForStorage(services, AzureBlobsModule.CreateBlobAuthDiscoverEndpoint(url), AzureBlobsModule.aadAuthHeaders), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, ResourceHelpers.AadForStorage(services, AzureBlobsModule.CreateBlobAuthDiscoverEndpoint(url), AzureBlobsModule.aadAuthHeaders)))
			},
			ResourceHelpers.SasAuthInfo
		}, null, false, false, false, new string[] { "ServiceAccessToken", "CapacityId" }, new DataSourceLocationFactory[] { AzureBlobsDataSourceLocation.Factory });

		// Token: 0x04003784 RID: 14212
		private Keys exportKeys;

		// Token: 0x02000EEB RID: 3819
		private enum Exports
		{
			// Token: 0x04003786 RID: 14214
			Containers,
			// Token: 0x04003787 RID: 14215
			Contents,
			// Token: 0x04003788 RID: 14216
			Count
		}

		// Token: 0x02000EEC RID: 3820
		public class AccountContainersFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x06006556 RID: 25942 RVA: 0x0015BDDB File Offset: 0x00159FDB
			public AccountContainersFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles), 1, "account", TypeValue.Text, "options", AzureBlobsModule.AccountContainersFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x17001D7E RID: 7550
			// (get) Token: 0x06006557 RID: 25943 RVA: 0x0015BE05 File Offset: 0x0015A005
			protected virtual string ResourceKind
			{
				get
				{
					return "AzureBlobs";
				}
			}

			// Token: 0x17001D7F RID: 7551
			// (get) Token: 0x06006558 RID: 25944 RVA: 0x0015BE0C File Offset: 0x0015A00C
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return new FunctionTypeIdentity(typeof(AzureBlobsModule.AccountContainersFunctionValue));
				}
			}

			// Token: 0x06006559 RID: 25945 RVA: 0x0015BE20 File Offset: 0x0015A020
			public override TableValue TypedInvoke(TextValue account, Value options)
			{
				OptionsRecord optionsRecord = AzureBlobsModule.SupportedOptions.CreateOptions("AzureStorage.Blobs", options);
				bool flag;
				AzureUtilities.ValidateParameters(account, out flag);
				if (flag)
				{
					return new ContainerTableValue(this.host, AzureBlobsService.GetHttpUri(account, null), this.ResourceKind, optionsRecord, RecordValue.Empty, false, false);
				}
				return new ContainersTableValue(this.host, AzureBlobsService.GetHttpUri(account, null), this.ResourceKind, optionsRecord, false);
			}

			// Token: 0x17001D80 RID: 7552
			// (get) Token: 0x0600655A RID: 25946 RVA: 0x0015BE84 File Offset: 0x0015A084
			public override string PrimaryResourceKind
			{
				get
				{
					return this.ResourceKind;
				}
			}

			// Token: 0x0600655B RID: 25947 RVA: 0x0015BE8C File Offset: 0x0015A08C
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (!AzureBlobsModule.AccountContainersFunctionValue.pattern.TryMatch(expression, out dictionary) || !dictionary.TryGetConstant("account", out value))
				{
					location = null;
					foundOptions = null;
					unknownOptions = null;
					return false;
				}
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
				AzureUtilities.ValidateParameters(value.AsText, null);
				IResource resource = Resource.New(this.ResourceKind, AzureBlobsService.GetHttpUri(value.AsText, null).String);
				if (!this.TryCreateLocation(resource, out location))
				{
					return false;
				}
				if (location.TrySetAddressString("container", dictionary, "container"))
				{
					location.TrySetAddressString("name", dictionary, "name");
				}
				return true;
			}

			// Token: 0x0600655C RID: 25948 RVA: 0x0015BF65 File Offset: 0x0015A165
			protected virtual bool TryCreateLocation(IResource resource, out IDataSourceLocation location)
			{
				return AzureBlobsDataSourceLocation.Factory.TryCreateFromResource(resource, false, out location);
			}

			// Token: 0x04003789 RID: 14217
			private static readonly TypeValue optionsType = AzureBlobsModule.SupportedOptions.CreateRecordType().Nullable;

			// Token: 0x0400378A RID: 14218
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__account, _o_options)", "__func(__account, _o_options){[Name=__container]}[Data]", "__func(__account, _o_options){[Name=__container]}[Data]{[#\"Folder Path\"=__folder,Name=__name]}[Content]" });

			// Token: 0x0400378B RID: 14219
			private readonly IEngineHost host;
		}

		// Token: 0x02000EED RID: 3821
		public class ContentsFunctionValue : NativeFunctionValue2<BinaryValue, TextValue, Value>
		{
			// Token: 0x0600655E RID: 25950 RVA: 0x0015BFB2 File Offset: 0x0015A1B2
			public ContentsFunctionValue(IEngineHost host)
				: base(TypeValue.Binary, 1, "url", TypeValue.Text, "options", AzureBlobsModule.ContentsFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x17001D81 RID: 7553
			// (get) Token: 0x0600655F RID: 25951 RVA: 0x0015BE05 File Offset: 0x0015A005
			protected virtual string ResourceKind
			{
				get
				{
					return "AzureBlobs";
				}
			}

			// Token: 0x17001D82 RID: 7554
			// (get) Token: 0x06006560 RID: 25952 RVA: 0x0015BFDB File Offset: 0x0015A1DB
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return new FunctionTypeIdentity(typeof(AzureBlobsModule.ContentsFunctionValue));
				}
			}

			// Token: 0x06006561 RID: 25953 RVA: 0x0015BFEC File Offset: 0x0015A1EC
			public override BinaryValue TypedInvoke(TextValue url, Value options)
			{
				OptionsRecord optionsRecord = AzureBlobsModule.SupportedOptions.CreateOptions("AzureStorage.BlobContents", options);
				IResource resource = Resource.New(this.ResourceKind, url.AsString);
				return BlobBinaryValue.New(this.host, resource, url, optionsRecord, null, null, null, null);
			}

			// Token: 0x17001D83 RID: 7555
			// (get) Token: 0x06006562 RID: 25954 RVA: 0x0015C036 File Offset: 0x0015A236
			public override string PrimaryResourceKind
			{
				get
				{
					return this.ResourceKind;
				}
			}

			// Token: 0x06006563 RID: 25955 RVA: 0x0015C040 File Offset: 0x0015A240
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (AzureBlobsModule.ContentsFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("url", out value))
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
					IResource resource = Resource.New(this.ResourceKind, value.AsString);
					if (this.TryCreateLocation(resource, out location))
					{
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x06006564 RID: 25956 RVA: 0x0015BF65 File Offset: 0x0015A165
			protected virtual bool TryCreateLocation(IResource resource, out IDataSourceLocation location)
			{
				return AzureBlobsDataSourceLocation.Factory.TryCreateFromResource(resource, false, out location);
			}

			// Token: 0x0400378C RID: 14220
			private static readonly TypeValue optionsType = AzureBlobsModule.SupportedOptions.CreateRecordType().Nullable;

			// Token: 0x0400378D RID: 14221
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__url, _o_options)" });

			// Token: 0x0400378E RID: 14222
			protected readonly IEngineHost host;
		}
	}
}
