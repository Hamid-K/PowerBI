using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018CF RID: 6351
	internal sealed class AzureDataLakeStorageDataSourceLocation : AzureBlobsDataSourceLocation
	{
		// Token: 0x0600A1F2 RID: 41458 RVA: 0x00219C5B File Offset: 0x00217E5B
		public AzureDataLakeStorageDataSourceLocation()
		{
			base.Protocol = "azure-data-lake-storage";
		}

		// Token: 0x17002967 RID: 10599
		// (get) Token: 0x0600A1F3 RID: 41459 RVA: 0x00219C6E File Offset: 0x00217E6E
		protected override string ContentsFunction
		{
			get
			{
				return "AzureStorage.DataLakeContents";
			}
		}

		// Token: 0x17002968 RID: 10600
		// (get) Token: 0x0600A1F4 RID: 41460 RVA: 0x00219C75 File Offset: 0x00217E75
		protected override string ContainersFunction
		{
			get
			{
				return "AzureStorage.DataLake";
			}
		}

		// Token: 0x17002969 RID: 10601
		// (get) Token: 0x0600A1F5 RID: 41461 RVA: 0x0015B73B File Offset: 0x0015993B
		public override string ResourceKind
		{
			get
			{
				return "AzureDataLakeStorage";
			}
		}

		// Token: 0x0600A1F6 RID: 41462 RVA: 0x00219C7C File Offset: 0x00217E7C
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			IFormulaCreationResult formulaCreationResult;
			if (base.TryCreateFormulaWithUrl(optionsJson, out formulaCreationResult))
			{
				return formulaCreationResult;
			}
			return this.CreateFormulaWithServer(optionsJson);
		}

		// Token: 0x0600A1F7 RID: 41463 RVA: 0x00219CA0 File Offset: 0x00217EA0
		public override bool TryGetResource(out IResource resource)
		{
			string stringOrNull = base.Address.GetStringOrNull("url");
			if (stringOrNull != null)
			{
				resource = Resource.New(this.ResourceKind, stringOrNull);
				return true;
			}
			string stringOrNull2 = base.Address.GetStringOrNull("server");
			if (string.IsNullOrEmpty(stringOrNull2))
			{
				resource = null;
				return false;
			}
			string stringOrNull3 = base.Address.GetStringOrNull("path");
			string absoluteUri = DataSourceLocation.GetAzureServerUrl(stringOrNull2, stringOrNull3).Uri.AbsoluteUri;
			resource = Resource.New(this.ResourceKind, absoluteUri);
			return true;
		}

		// Token: 0x0600A1F8 RID: 41464 RVA: 0x00219D24 File Offset: 0x00217F24
		private IFormulaCreationResult CreateFormulaWithServer(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = AzureDataLakeStorageModule.SupportedOptions.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			string stringOrNull = base.Address.GetStringOrNull("server");
			if (stringOrNull == null)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			string stringOrNull2 = base.Address.GetStringOrNull("path");
			string absoluteUri = DataSourceLocation.GetAzureServerUrl(stringOrNull, stringOrNull2).Uri.AbsoluteUri;
			return new FormulaCreationResult(ExpressionBuilder.Instance.Invoke(this.ContainersFunction, 1, new object[] { absoluteUri, recordValue }));
		}

		// Token: 0x040054AA RID: 21674
		public new static readonly DataSourceLocationFactory Factory = new AzureDataLakeStorageDataSourceLocation.DslFactory();

		// Token: 0x040054AB RID: 21675
		private const string AzureStorage_DataLake_Function = "AzureStorage.DataLake";

		// Token: 0x040054AC RID: 21676
		private const string AzureStorage_DataLakeContents_Function = "AzureStorage.DataLakeContents";

		// Token: 0x020018D0 RID: 6352
		private new sealed class DslFactory : AzureBlobsDataSourceLocation.DslFactory
		{
			// Token: 0x1700296A RID: 10602
			// (get) Token: 0x0600A1FA RID: 41466 RVA: 0x00219DD8 File Offset: 0x00217FD8
			public override string Protocol
			{
				get
				{
					return "azure-data-lake-storage";
				}
			}

			// Token: 0x0600A1FB RID: 41467 RVA: 0x00219DDF File Offset: 0x00217FDF
			public override IDataSourceLocation New()
			{
				return new AzureDataLakeStorageDataSourceLocation();
			}

			// Token: 0x0600A1FC RID: 41468 RVA: 0x00219DE6 File Offset: 0x00217FE6
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return base.TryCreateWithUrl(resourcePath, out location) || this.TryCreateWithServer(resourcePath, out location);
			}

			// Token: 0x0600A1FD RID: 41469 RVA: 0x00219DFC File Offset: 0x00217FFC
			private bool TryCreateWithServer(string resourcePath, out IDataSourceLocation location)
			{
				location = DataSourceLocationFactory.New(this.Protocol);
				Uri uri;
				if (!Uri.TryCreate(resourcePath, UriKind.Absolute, out uri))
				{
					location = null;
					return false;
				}
				location.Address["server"] = uri.Host;
				location.Address["path"] = uri.AbsolutePath;
				return true;
			}
		}
	}
}
