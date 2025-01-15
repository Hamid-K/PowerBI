using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Azure.Common;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.CommonUtilities
{
	// Token: 0x0200002F RID: 47
	[BlockServiceProvider(typeof(ISampleModelInfo))]
	public sealed class ANSampleModelInfo : Block, ISampleModelInfo
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000E008 File Offset: 0x0000C208
		public ANSampleModelInfo()
			: base(typeof(ANSampleModelInfo).Name)
		{
			this.modelProperties = new List<SampleModelProperty>();
			this.sampleModelMonikers = new List<DatabaseMoniker>();
			this.modelDictionary = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.fileToModelMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			this.modelNames = new List<string>();
			this.modelFiles = new List<string>();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000E078 File Offset: 0x0000C278
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			this.configurationManager = this.configurationManagerFactory.GetConfigurationManager();
			this.configurationManager.Subscribe(new Type[] { typeof(AnalyticsSampleModelCollectionConfiguration) }, new CcsEventHandler(this.OnConfigurationChange));
			return BlockInitializationStatus.Done;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E0CC File Offset: 0x0000C2CC
		private void OnConfigurationChange(IConfigurationContainer configurationContainer)
		{
			this.modelProperties.Clear();
			this.sampleModelMonikers.Clear();
			this.modelDictionary.Clear();
			this.fileToModelMapping.Clear();
			this.modelNames.Clear();
			this.modelFiles.Clear();
			AnalyticsSampleModelCollectionConfiguration configuration = configurationContainer.GetConfiguration<AnalyticsSampleModelCollectionConfiguration>();
			this.sampleModelCollectionConfiguration = configuration.SampleModelCollection;
			foreach (SampleModelConfiguration sampleModelConfiguration in this.sampleModelCollectionConfiguration)
			{
				ExtendedDiagnostics.EnsureStringNotNullOrEmpty(sampleModelConfiguration.ABFFileName, "model.ABFFileName");
				ExtendedDiagnostics.EnsureStringNotNullOrEmpty(sampleModelConfiguration.DatabaseName, "model.DatabaseName");
				ExtendedDiagnostics.EnsureArgumentIsNotNegative(sampleModelConfiguration.ModelMaxMemoryMB, "model.ModelMaxMemoryMB");
				ExtendedDiagnostics.EnsureArgumentIsNotNegative(sampleModelConfiguration.ModelProcessLimitMB, "model.ModelProcessLimitMB");
				ExtendedDiagnostics.EnsureArgumentIsBetween(sampleModelConfiguration.ModelMaxCPU, 0, 100, "model.ModelMaxCPU");
				string text = (string.IsNullOrEmpty(sampleModelConfiguration.TestQuery) ? string.Empty : sampleModelConfiguration.TestQuery);
				string text2 = (string.IsNullOrEmpty(sampleModelConfiguration.TestQueryResult) ? string.Empty : sampleModelConfiguration.TestQueryResult);
				string text3 = (string.IsNullOrEmpty(sampleModelConfiguration.TestUtterance) ? string.Empty : sampleModelConfiguration.TestUtterance);
				this.modelProperties.Add(new SampleModelProperty(sampleModelConfiguration.ABFFileName, sampleModelConfiguration.DatabaseName, sampleModelConfiguration.ModelMaxMemoryMB, sampleModelConfiguration.ModelMaxCPU, sampleModelConfiguration.ModelProcessLimitMB, text, text2, text3));
				DatabaseMoniker databaseMoniker = new DatabaseMoniker(SampleModelConstants.VIRTUAL_SERVER_NAME, sampleModelConfiguration.DatabaseName);
				this.modelNames.Add(sampleModelConfiguration.DatabaseName);
				this.modelFiles.Add(sampleModelConfiguration.ABFFileName);
				this.sampleModelMonikers.Add(databaseMoniker);
				this.modelDictionary.Add(databaseMoniker.DatabaseName);
				this.modelDictionary.Add(databaseMoniker.FullName);
				this.fileToModelMapping.Add(sampleModelConfiguration.ABFFileName, sampleModelConfiguration.DatabaseName);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		protected override void OnStop()
		{
			this.configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigurationChange));
			base.OnStop();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E2EF File Offset: 0x0000C4EF
		public IEnumerable<DatabaseMoniker> GetSampleModelMonikers()
		{
			return this.sampleModelMonikers;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E2F7 File Offset: 0x0000C4F7
		public IEnumerable<string> GetSampleModelNames()
		{
			return this.modelFiles;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E2FF File Offset: 0x0000C4FF
		public IEnumerable<SampleModelProperty> GetSampleModelProperties()
		{
			return this.modelProperties;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E307 File Offset: 0x0000C507
		public bool IsSampleModel(string modelName)
		{
			return this.modelDictionary.Contains(modelName);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E318 File Offset: 0x0000C518
		public bool TryGetSampleModelName(string inputModelName, out string modelName)
		{
			modelName = this.modelNames.FirstOrDefault((string m) => string.Compare(inputModelName, m, StringComparison.OrdinalIgnoreCase) == 0);
			return !string.IsNullOrEmpty(modelName);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000E355 File Offset: 0x0000C555
		public bool IsSampleModel(DatabaseMoniker moniker)
		{
			return string.Compare(SampleModelConstants.VIRTUAL_SERVER_NAME, moniker.VirtualServerName, StringComparison.OrdinalIgnoreCase) == 0 && this.modelDictionary.Contains(moniker.DatabaseName);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000E380 File Offset: 0x0000C580
		public string GetModelName(string fileName)
		{
			string text;
			this.fileToModelMapping.TryGetValue(fileName, out text);
			return text;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000E39D File Offset: 0x0000C59D
		public bool IsSampleModelVirtualServer(string virtualServer)
		{
			return string.Compare(SampleModelConstants.VIRTUAL_SERVER_NAME, virtualServer, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x04000081 RID: 129
		private List<DatabaseMoniker> sampleModelMonikers;

		// Token: 0x04000082 RID: 130
		private HashSet<string> modelDictionary;

		// Token: 0x04000083 RID: 131
		private Dictionary<string, string> fileToModelMapping;

		// Token: 0x04000084 RID: 132
		private List<SampleModelProperty> modelProperties;

		// Token: 0x04000085 RID: 133
		private List<string> modelNames;

		// Token: 0x04000086 RID: 134
		private List<string> modelFiles;

		// Token: 0x04000087 RID: 135
		private IConfigurationManager configurationManager;

		// Token: 0x04000088 RID: 136
		private IEnumerable<SampleModelConfiguration> sampleModelCollectionConfiguration;

		// Token: 0x04000089 RID: 137
		[BlockServiceDependency]
		private readonly IConfigurationManagerFactory configurationManagerFactory;
	}
}
