using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x0200192F RID: 6447
	public class DocumentAnalyzer : IDocumentAnalyzer
	{
		// Token: 0x0600A3DF RID: 41951 RVA: 0x0021E788 File Offset: 0x0021C988
		public DocumentAnalyzer(IEngineHost engineHost, Func<IEngineHost, IDocumentEvaluator<IPreviewValueSource>> evaluatorCtor, Func<IPartitionKey, IEnumerable<IEngineHost>> additionalServicesFactory = null)
		{
			this.engineHost = engineHost;
			this.evaluatorCtor = evaluatorCtor;
			this.additionalServicesFactory = additionalServicesFactory;
		}

		// Token: 0x0600A3E0 RID: 41952 RVA: 0x0021E7A8 File Offset: 0x0021C9A8
		public IEvaluation BeginAnalyzeDocumentPartitions(DocumentEvaluationConfig config, IPartitionedDocument document, IEnumerable<IPartitionKey> partitionKeys, IDocumentAnalysisInfo documentInfo, Action<Exception> callback)
		{
			IFirewallPlanCreator firewallPlanCreator = this.engineHost.QueryService<IFirewallPlanCreator>();
			IFirewallPlanMinimizer firewallPlanMinimizer = this.engineHost.QueryService<IFirewallPlanMinimizer>();
			IFirewallPlan firewallPlan = firewallPlanCreator.CreatePlanForPartitions(document, partitionKeys);
			firewallPlan = firewallPlanMinimizer.GroupPlanForPartitions(firewallPlan, document, partitionKeys);
			Dictionary<IPartitionKey, IFirewallPartitionPlan> dictionary = new Dictionary<IPartitionKey, IFirewallPartitionPlan>(PartitionKeyEqualityComparer.Instance);
			List<IPartitionKey> list = new List<IPartitionKey>(partitionKeys);
			foreach (IPartitionKey partitionKey4 in partitionKeys)
			{
				dictionary.Add(partitionKey4, null);
			}
			foreach (IFirewallPartitionPlan firewallPartitionPlan in firewallPlan.PartitionPlans)
			{
				if (!dictionary.ContainsKey(firewallPartitionPlan.PartitionKey))
				{
					list.Add(firewallPartitionPlan.PartitionKey);
				}
				dictionary[firewallPartitionPlan.PartitionKey] = firewallPartitionPlan;
			}
			IResourcePathService resourcePathService = this.engineHost.QueryService<IResourcePathService>();
			CompositeEvaluation analysisEvaluation = new CompositeEvaluation();
			InvokeManyAction<Exception> invokeManyAction = new InvokeManyAction<Exception>();
			OnManyInvokedAction<Exception> onManyInvokedAction = new OnManyInvokedAction<Exception>();
			Dictionary<IPartitionKey, InvokeManyAction<Exception>> dictionary2 = new Dictionary<IPartitionKey, InvokeManyAction<Exception>>(PartitionKeyEqualityComparer.Instance);
			foreach (IPartitionKey partitionKey2 in list)
			{
				InvokeManyAction<Exception> invokeManyAction2 = new InvokeManyAction<Exception>();
				dictionary2.Add(partitionKey2, invokeManyAction2);
			}
			using (List<IPartitionKey>.Enumerator enumerator3 = list.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					IPartitionKey partitionKey = enumerator3.Current;
					OnManyInvokedAction<Exception> onManyInvokedAction2 = new OnManyInvokedAction<Exception>();
					InvokeManyAction<Exception> partitionComplete = dictionary2[partitionKey];
					partitionComplete.AddOutput(onManyInvokedAction.NewInput());
					invokeManyAction.AddOutput(onManyInvokedAction2.NewInput());
					IFirewallPartitionPlan firewallPartitionPlan2 = dictionary[partitionKey];
					foreach (IPartitionKey partitionKey3 in firewallPartitionPlan2.Inputs)
					{
						InvokeManyAction<Exception> invokeManyAction3;
						if (dictionary[partitionKey3].EvaluationOrder < firewallPartitionPlan2.EvaluationOrder && dictionary2.TryGetValue(partitionKey3, out invokeManyAction3))
						{
							invokeManyAction3.AddOutput(onManyInvokedAction2.NewInput());
						}
					}
					onManyInvokedAction2.OnManyInvoked(delegate(IEnumerable<Exception> exceptions)
					{
						Exception ex = exceptions.Where((Exception e) => e is OperationCanceledException).FirstOrDefault<Exception>();
						if (ex != null)
						{
							partitionComplete.InvokeMany(ex);
							return;
						}
						analysisEvaluation.Add(this.BeginAnalyzeDocumentPartition(firewallPlanCreator, firewallPlanMinimizer, resourcePathService, config, document, partitionKey, documentInfo, new Action<Exception>(partitionComplete.InvokeMany)));
					});
				}
			}
			invokeManyAction.AddOutput(onManyInvokedAction.NewInput());
			onManyInvokedAction.OnManyInvoked(delegate(IEnumerable<Exception> exceptions)
			{
				callback(exceptions.Where((Exception e) => e is OperationCanceledException).FirstOrDefault<Exception>());
			});
			invokeManyAction.InvokeMany(null);
			return analysisEvaluation;
		}

		// Token: 0x0600A3E1 RID: 41953 RVA: 0x0021EAB4 File Offset: 0x0021CCB4
		private IEvaluation BeginAnalyzeDocumentPartition(IFirewallPlanCreator firewallPlanCreator, IFirewallPlanMinimizer firewallPlanMinimizer, IResourcePathService resourcePathService, DocumentEvaluationConfig config, IPartitionedDocument document, IPartitionKey partitionKey, IDocumentAnalysisInfo documentInfo, Action<Exception> callback)
		{
			IPartitionAnalysisInfo partitionInfo = documentInfo.GetPartitionInfo(partitionKey);
			EvaluationResult2<IPreviewValueSource> evaluationResult;
			DateTime dateTime;
			bool flag;
			if (partitionInfo.TryGetComplete() && partitionInfo.TryGetPreviewValue(out evaluationResult, out dateTime, out flag) && !(evaluationResult.Exception is OperationCanceledException))
			{
				callback(null);
				return new EmptyEvaluation();
			}
			IFirewallPlan firewallPlan = null;
			if (config.enableFirewall)
			{
				IPartitionKey[] array = new IPartitionKey[] { partitionKey };
				firewallPlan = firewallPlanCreator.CreatePlanForPartitions(document, array);
				firewallPlan = firewallPlanMinimizer.GroupPlanForPartitions(firewallPlan, document, array);
				FirewallPlanAnnotator.AnnotateFirewallPlan(firewallPlan, documentInfo);
				firewallPlan = firewallPlanMinimizer.TrimPlanForPartition(firewallPlan, document, partitionKey);
			}
			DocumentAnalyzer.AccessRecorder recorder = new DocumentAnalyzer.AccessRecorder(resourcePathService, partitionKey);
			IEngineHost engineHost = new CompositeEngineHost(new IEngineHost[]
			{
				new SimpleEngineHost<IReportCultureAccess>(recorder),
				new SimpleEngineHost<IReportPartitionResources>(recorder),
				new SimpleEngineHost<IReportResourceAccess>(recorder),
				new SimpleEngineHost<IReportStaleness>(recorder),
				new SimpleEngineHost<IReportSampling>(recorder),
				this.engineHost
			});
			if (this.additionalServicesFactory != null)
			{
				List<IEngineHost> list = this.additionalServicesFactory(partitionKey).ToList<IEngineHost>();
				list.Add(engineHost);
				engineHost = new CompositeEngineHost(list.ToArray());
			}
			IDocumentEvaluator<IPreviewValueSource> documentEvaluator = this.evaluatorCtor(engineHost);
			DocumentEvaluationParameters documentEvaluationParameters = new DocumentEvaluationParameters
			{
				config = config,
				document = document,
				partitionKey = partitionKey,
				firewallPlan = firewallPlan,
				reportRelationships = true
			};
			documentEvaluationParameters = documentEvaluationParameters.ReferencePartition(partitionKey).SkipAndTake(new int?(0), new int?(1000));
			partitionInfo.SetStart();
			return documentEvaluator.BeginGetResult(documentEvaluationParameters, delegate(EvaluationResult2<IPreviewValueSource> result)
			{
				this.Analyze(partitionInfo, recorder, result, callback);
			});
		}

		// Token: 0x0600A3E2 RID: 41954 RVA: 0x0021EC80 File Offset: 0x0021CE80
		private void Analyze(IPartitionAnalysisInfo partitionInfo, DocumentAnalyzer.AccessRecorder recorder, EvaluationResult2<IPreviewValueSource> result, Action<Exception> callback)
		{
			partitionInfo.SetPreviewValue(result, () => recorder.Staleness, () => recorder.SamplingWasUsed);
			if (recorder.CultureWasAccessed)
			{
				partitionInfo.SetCultureAccessed();
			}
			if (recorder.Resources != null)
			{
				partitionInfo.SetResources(recorder.Resources ?? new IResource[0]);
			}
			if (recorder.ResourcesAccessed != null)
			{
				partitionInfo.SetResourcesAccessed(recorder.ResourcesAccessed);
			}
			partitionInfo.SetComplete();
			callback(null);
		}

		// Token: 0x04005542 RID: 21826
		private const int PartitionAnalysisRowCount = 1000;

		// Token: 0x04005543 RID: 21827
		private readonly IEngineHost engineHost;

		// Token: 0x04005544 RID: 21828
		private readonly Func<IEngineHost, IDocumentEvaluator<IPreviewValueSource>> evaluatorCtor;

		// Token: 0x04005545 RID: 21829
		private readonly Func<IPartitionKey, IEnumerable<IEngineHost>> additionalServicesFactory;

		// Token: 0x02001930 RID: 6448
		private class AccessRecorder : IReportCultureAccess, IReportPartitionResources, IReportResourceAccess, IReportStaleness, IReportSampling
		{
			// Token: 0x0600A3E3 RID: 41955 RVA: 0x0021ED20 File Offset: 0x0021CF20
			public AccessRecorder(IResourcePathService resourcePathService, IPartitionKey partitionKey)
			{
				this.partitionKey = partitionKey;
				this.resourcesAccessed = new ResourcesAccessed(resourcePathService);
				this.staleSince = DateTime.UtcNow;
			}

			// Token: 0x170029E5 RID: 10725
			// (get) Token: 0x0600A3E4 RID: 41956 RVA: 0x0021ED46 File Offset: 0x0021CF46
			public bool CultureWasAccessed
			{
				get
				{
					return this.cultureAccessed;
				}
			}

			// Token: 0x170029E6 RID: 10726
			// (get) Token: 0x0600A3E5 RID: 41957 RVA: 0x0021ED4E File Offset: 0x0021CF4E
			public bool SamplingWasUsed
			{
				get
				{
					return this.samplingUsed;
				}
			}

			// Token: 0x170029E7 RID: 10727
			// (get) Token: 0x0600A3E6 RID: 41958 RVA: 0x0021ED56 File Offset: 0x0021CF56
			public IEnumerable<IResource> Resources
			{
				get
				{
					return this.resources;
				}
			}

			// Token: 0x170029E8 RID: 10728
			// (get) Token: 0x0600A3E7 RID: 41959 RVA: 0x0021ED5E File Offset: 0x0021CF5E
			public IEnumerable<IResource> ResourcesAccessed
			{
				get
				{
					return this.resourcesAccessed;
				}
			}

			// Token: 0x170029E9 RID: 10729
			// (get) Token: 0x0600A3E8 RID: 41960 RVA: 0x0021ED66 File Offset: 0x0021CF66
			public DateTime Staleness
			{
				get
				{
					return this.staleSince;
				}
			}

			// Token: 0x0600A3E9 RID: 41961 RVA: 0x0021ED6E File Offset: 0x0021CF6E
			public void CultureAccessed()
			{
				this.cultureAccessed = true;
			}

			// Token: 0x0600A3EA RID: 41962 RVA: 0x0021ED77 File Offset: 0x0021CF77
			public void SamplingUsed()
			{
				this.samplingUsed = true;
			}

			// Token: 0x0600A3EB RID: 41963 RVA: 0x0021ED80 File Offset: 0x0021CF80
			public void PartitionResources(IPartitionKey partitionKey, IEnumerable<IResource> resources)
			{
				if (this.partitionKey.Equals(partitionKey))
				{
					this.resources = resources;
				}
			}

			// Token: 0x0600A3EC RID: 41964 RVA: 0x0021ED97 File Offset: 0x0021CF97
			public void ResourceAccessed(IResource resource)
			{
				this.resourcesAccessed.Add(resource);
			}

			// Token: 0x0600A3ED RID: 41965 RVA: 0x0021EDA5 File Offset: 0x0021CFA5
			public void StaleSince(DateTime staleSince)
			{
				if (staleSince < this.staleSince)
				{
					this.staleSince = staleSince;
				}
			}

			// Token: 0x04005546 RID: 21830
			private readonly IPartitionKey partitionKey;

			// Token: 0x04005547 RID: 21831
			private bool cultureAccessed;

			// Token: 0x04005548 RID: 21832
			private bool samplingUsed;

			// Token: 0x04005549 RID: 21833
			private IEnumerable<IResource> resources;

			// Token: 0x0400554A RID: 21834
			private ResourcesAccessed resourcesAccessed;

			// Token: 0x0400554B RID: 21835
			private DateTime staleSince;
		}
	}
}
