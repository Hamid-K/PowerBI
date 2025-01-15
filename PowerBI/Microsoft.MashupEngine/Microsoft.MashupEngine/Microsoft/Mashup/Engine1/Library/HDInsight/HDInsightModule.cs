using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library.AzureBlobs;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.HDInsight
{
	// Token: 0x02000AD6 RID: 2774
	internal sealed class HDInsightModule : Module
	{
		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x06004D5A RID: 19802 RVA: 0x00100388 File Offset: 0x000FE588
		public override string Name
		{
			get
			{
				return "HdInsight";
			}
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x06004D5B RID: 19803 RVA: 0x0010038F File Offset: 0x000FE58F
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
							return "HdInsight.Files";
						case 1:
							return "HdInsight.Contents";
						case 2:
							return "HdInsight.Containers";
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x06004D5C RID: 19804 RVA: 0x001003CA File Offset: 0x000FE5CA
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { HDInsightModule.resourceKindInfo };
			}
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x001003DC File Offset: 0x000FE5DC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new HDInsightModule.ContainerFilesFunctionValue(hostEnvironment);
				case 1:
					return new HDInsightModule.AccountContentsFunctionValue(hostEnvironment);
				case 2:
					return new HDInsightModule.AccountContainersFunctionValue(hostEnvironment);
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x04002945 RID: 10565
		public const string HdInsightFiles = "HdInsight.Files";

		// Token: 0x04002946 RID: 10566
		public const string HdInsightContents = "HdInsight.Contents";

		// Token: 0x04002947 RID: 10567
		public const string HdInsightContainers = "HdInsight.Containers";

		// Token: 0x04002948 RID: 10568
		public const string DataSourceNameString = "Azure HDInsight";

		// Token: 0x04002949 RID: 10569
		private static readonly OptionRecordDefinition SupportedOptions = AzureBlobsModule.SupportedOptions;

		// Token: 0x0400294A RID: 10570
		private static readonly ResourceKindInfo resourceKindInfo = new UriResourceKindInfo("HDInsight", null, new AuthenticationInfo[]
		{
			ResourceHelpers.AnonymousAuth,
			new KeyAuthenticationInfo()
		}, null, false, false, false, null, new DataSourceLocationFactory[] { HdInsightDataSourceLocation.Factory });

		// Token: 0x0400294B RID: 10571
		private Keys exportKeys;

		// Token: 0x02000AD7 RID: 2775
		private enum Exports
		{
			// Token: 0x0400294D RID: 10573
			Files,
			// Token: 0x0400294E RID: 10574
			Contents,
			// Token: 0x0400294F RID: 10575
			Containers,
			// Token: 0x04002950 RID: 10576
			Count
		}

		// Token: 0x02000AD8 RID: 2776
		private sealed class AccountContainersFunctionValue : NativeFunctionValue1<TableValue, TextValue>
		{
			// Token: 0x06004D60 RID: 19808 RVA: 0x00100460 File Offset: 0x000FE660
			public AccountContainersFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles), 1, "account", TypeValue.Text)
			{
				this.host = host;
			}

			// Token: 0x06004D61 RID: 19809 RVA: 0x00100480 File Offset: 0x000FE680
			public override TableValue TypedInvoke(TextValue account)
			{
				OptionsRecord optionsRecord = HDInsightModule.SupportedOptions.CreateOptions("HdInsight.Containers", Value.Null);
				bool flag;
				AzureUtilities.ValidateParameters(account, out flag);
				if (flag)
				{
					return new ContainerTableValue(this.host, AzureBlobsService.GetHttpUri(account, null), "HDInsight", optionsRecord, RecordValue.Empty, false, false);
				}
				return new ContainersTableValue(this.host, AzureBlobsService.GetHttpUri(account, null), "HDInsight", optionsRecord, false);
			}

			// Token: 0x1700183E RID: 6206
			// (get) Token: 0x06004D62 RID: 19810 RVA: 0x001004E6 File Offset: 0x000FE6E6
			public override string PrimaryResourceKind
			{
				get
				{
					return "HDInsight";
				}
			}

			// Token: 0x06004D63 RID: 19811 RVA: 0x001004F0 File Offset: 0x000FE6F0
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues.Length == 1 && argumentValues[0].IsText)
				{
					AzureUtilities.ValidateParameters(argumentValues[0].AsText, null);
					string @string = AzureBlobsService.GetHttpUri(argumentValues[0].AsText, null).String;
					location = new HdInsightDataSourceLocation();
					location.Address["url"] = @string;
					foundOptions = RecordValue.New(Keys.New("HierarchicalNavigation"), new Value[] { Value.Null });
					unknownOptions = Keys.Empty;
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04002951 RID: 10577
			private readonly IEngineHost host;
		}

		// Token: 0x02000AD9 RID: 2777
		private sealed class AccountContentsFunctionValue : NativeFunctionValue1<TableValue, TextValue>
		{
			// Token: 0x06004D64 RID: 19812 RVA: 0x00100583 File Offset: 0x000FE783
			public AccountContentsFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles), 1, "account", TypeValue.Text)
			{
				this.host = host;
			}

			// Token: 0x06004D65 RID: 19813 RVA: 0x001005A4 File Offset: 0x000FE7A4
			public override TableValue TypedInvoke(TextValue account)
			{
				OptionsRecord optionsRecord = HDInsightModule.SupportedOptions.CreateOptions("HdInsight.Contents", Value.Null);
				bool flag;
				AzureUtilities.ValidateParameters(account, out flag);
				if (flag)
				{
					return new ContainerTableValue(this.host, AzureBlobsService.GetHttpUri(account, null), "HDInsight", optionsRecord, RecordValue.Empty, false, false);
				}
				return new ContainersTableValue(this.host, AzureBlobsService.GetHttpUri(account, null), "HDInsight", optionsRecord, true);
			}

			// Token: 0x1700183F RID: 6207
			// (get) Token: 0x06004D66 RID: 19814 RVA: 0x001004E6 File Offset: 0x000FE6E6
			public override string PrimaryResourceKind
			{
				get
				{
					return "HDInsight";
				}
			}

			// Token: 0x06004D67 RID: 19815 RVA: 0x0010060C File Offset: 0x000FE80C
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues.Length == 1 && argumentValues[0].IsText)
				{
					AzureUtilities.ValidateParameters(argumentValues[0].AsText, null);
					string @string = AzureBlobsService.GetHttpUri(argumentValues[0].AsText, null).String;
					location = new HdInsightDataSourceLocation();
					location.Address["url"] = @string;
					foundOptions = RecordValue.New(Keys.New("HierarchicalNavigation"), new Value[] { LogicalValue.False });
					unknownOptions = Keys.Empty;
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04002952 RID: 10578
			private readonly IEngineHost host;
		}

		// Token: 0x02000ADA RID: 2778
		private sealed class ContainerFilesFunctionValue : NativeFunctionValue2<TableValue, TextValue, TextValue>
		{
			// Token: 0x06004D68 RID: 19816 RVA: 0x0010069F File Offset: 0x000FE89F
			public ContainerFilesFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles), 2, "account", TypeValue.Text, "containerName", TypeValue.Text)
			{
				this.host = host;
			}

			// Token: 0x06004D69 RID: 19817 RVA: 0x001006CC File Offset: 0x000FE8CC
			public override TableValue TypedInvoke(TextValue account, TextValue containerName)
			{
				OptionsRecord optionsRecord = HDInsightModule.SupportedOptions.CreateOptions("HdInsight.Files", Value.Null);
				AzureUtilities.ValidateParameters(account, containerName);
				return new ContainerTableValue(this.host, AzureBlobsService.GetHttpUri(account, containerName), "HDInsight", optionsRecord, RecordValue.Empty, false, false);
			}

			// Token: 0x17001840 RID: 6208
			// (get) Token: 0x06004D6A RID: 19818 RVA: 0x001004E6 File Offset: 0x000FE6E6
			public override string PrimaryResourceKind
			{
				get
				{
					return "HDInsight";
				}
			}

			// Token: 0x06004D6B RID: 19819 RVA: 0x00100714 File Offset: 0x000FE914
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues.Length == 2 && argumentValues[0].IsText && argumentValues[1].IsText)
				{
					AzureUtilities.ValidateParameters(argumentValues[0].AsText, argumentValues[1].AsText);
					string @string = AzureBlobsService.GetHttpUri(argumentValues[0].AsText, argumentValues[1].AsText).String;
					location = new HdInsightDataSourceLocation();
					location.Address["url"] = @string;
					foundOptions = RecordValue.New(Keys.New("HierarchicalNavigation"), new Value[] { LogicalValue.True });
					unknownOptions = Keys.Empty;
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04002953 RID: 10579
			private readonly IEngineHost host;
		}
	}
}
