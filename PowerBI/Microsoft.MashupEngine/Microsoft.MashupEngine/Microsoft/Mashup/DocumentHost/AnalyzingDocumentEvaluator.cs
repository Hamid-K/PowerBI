using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x02001929 RID: 6441
	public class AnalyzingDocumentEvaluator : IDocumentEvaluator, IDocumentEvaluator<IPreviewValueSource>, IDocumentEvaluator<IDataReaderSource>, IDocumentEvaluator<IStreamSource>
	{
		// Token: 0x0600A3C5 RID: 41925 RVA: 0x0021E203 File Offset: 0x0021C403
		public AnalyzingDocumentEvaluator(IEngineHost engineHost, IDocumentAnalyzer analyzer, Func<IEngineHost, IDocumentEvaluator> evaluatorCtor)
		{
			this.engineHost = engineHost;
			this.analyzer = analyzer;
			this.evaluatorCtor = evaluatorCtor;
		}

		// Token: 0x0600A3C6 RID: 41926 RVA: 0x0021E220 File Offset: 0x0021C420
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IPreviewValueSource>> callback)
		{
			return this.BeginGetResult<IPreviewValueSource>(parameters, callback);
		}

		// Token: 0x0600A3C7 RID: 41927 RVA: 0x0021E22A File Offset: 0x0021C42A
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IDataReaderSource>> callback)
		{
			return this.BeginGetResult<IDataReaderSource>(parameters, callback);
		}

		// Token: 0x0600A3C8 RID: 41928 RVA: 0x0021E234 File Offset: 0x0021C434
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IStreamSource>> callback)
		{
			return this.BeginGetResult<IStreamSource>(parameters, callback);
		}

		// Token: 0x0600A3C9 RID: 41929 RVA: 0x0021E240 File Offset: 0x0021C440
		private IEvaluation BeginGetResult<T>(DocumentEvaluationParameters parameters, Action<EvaluationResult2<T>> callback)
		{
			parameters.SetUiCulture();
			parameters = parameters.Clone();
			IDocumentEvaluator<T> evaluator = (IDocumentEvaluator<T>)this.evaluatorCtor(this.engineHost);
			if (parameters.config.enableFirewall && parameters.firewallPlan == null)
			{
				IFirewallPlanCreator firewallPlanCreator = this.engineHost.QueryService<IFirewallPlanCreator>();
				IFirewallPlanMinimizer minimizer = this.engineHost.QueryService<IFirewallPlanMinimizer>();
				IPartitionKey[] array = new IPartitionKey[] { parameters.partitionKey };
				parameters.firewallPlan = firewallPlanCreator.CreatePlanForPartitions(parameters.document, array);
				parameters.firewallPlan = minimizer.GroupPlanForPartitions(parameters.firewallPlan, parameters.document, array);
				IPartitionKey[] array2 = (from p in parameters.firewallPlan.PartitionPlans
					select p.PartitionKey into p
					where !p.Equals(parameters.partitionKey)
					select p).ToArray<IPartitionKey>();
				CompositeEvaluation evaluation = new CompositeEvaluation();
				IDocumentAnalysisInfo documentInfo = new AnalyzingDocumentEvaluator.FirewallDocumentAnalysisInfo();
				evaluation.Add(this.analyzer.BeginAnalyzeDocumentPartitions(parameters.config, parameters.document, array2, documentInfo, delegate(Exception exception)
				{
					if (exception != null)
					{
						documentInfo.Dispose();
						callback(new EvaluationResult2<T>(exception));
						return;
					}
					FirewallPlanAnnotator.AnnotateFirewallPlan(parameters.firewallPlan, documentInfo);
					documentInfo.Dispose();
					parameters.firewallPlan = minimizer.TrimPlanForPartition(parameters.firewallPlan, parameters.document, parameters.partitionKey);
					evaluation.Add(evaluator.BeginGetResult(parameters, callback));
				}));
				return evaluation;
			}
			return evaluator.BeginGetResult(parameters, callback);
		}

		// Token: 0x04005532 RID: 21810
		private readonly IEngineHost engineHost;

		// Token: 0x04005533 RID: 21811
		private readonly IDocumentAnalyzer analyzer;

		// Token: 0x04005534 RID: 21812
		private readonly Func<IEngineHost, IDocumentEvaluator> evaluatorCtor;

		// Token: 0x0200192A RID: 6442
		private class FirewallDocumentAnalysisInfo : IDocumentAnalysisInfo, IDisposable
		{
			// Token: 0x0600A3CA RID: 41930 RVA: 0x0021E43C File Offset: 0x0021C63C
			public IPartitionAnalysisInfo GetPartitionInfo(IPartitionKey partitionKey)
			{
				Dictionary<IPartitionKey, IPartitionAnalysisInfo> dictionary = this.partitions;
				IPartitionAnalysisInfo partitionAnalysisInfo2;
				lock (dictionary)
				{
					IPartitionAnalysisInfo partitionAnalysisInfo;
					if (!this.partitions.TryGetValue(partitionKey, out partitionAnalysisInfo))
					{
						partitionAnalysisInfo = new AnalyzingDocumentEvaluator.FirewallDocumentAnalysisInfo.FirewallPartitionAnalysisInfo();
						this.partitions.Add(partitionKey, partitionAnalysisInfo);
					}
					partitionAnalysisInfo2 = partitionAnalysisInfo;
				}
				return partitionAnalysisInfo2;
			}

			// Token: 0x0600A3CB RID: 41931 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x04005535 RID: 21813
			private readonly Dictionary<IPartitionKey, IPartitionAnalysisInfo> partitions = new Dictionary<IPartitionKey, IPartitionAnalysisInfo>(PartitionKeyEqualityComparer.Instance);

			// Token: 0x0200192B RID: 6443
			private class FirewallPartitionAnalysisInfo : IPartitionAnalysisInfo
			{
				// Token: 0x0600A3CD RID: 41933 RVA: 0x0000336E File Offset: 0x0000156E
				public void SetStart()
				{
				}

				// Token: 0x0600A3CE RID: 41934 RVA: 0x0021E4B4 File Offset: 0x0021C6B4
				public void SetPreviewValue(EvaluationResult2<IPreviewValueSource> result, Func<DateTime> getStaleSince, Func<bool> getSampled)
				{
					try
					{
						using (result.Result)
						{
							string value = result.Result.Value;
						}
						getStaleSince();
						getSampled();
					}
					catch (Exception ex)
					{
						using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("FirewallPartitionAnalysisInfo/SetPreviewValue", null, TraceEventType.Information, null))
						{
							if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
							{
								throw;
							}
						}
					}
				}

				// Token: 0x0600A3CF RID: 41935 RVA: 0x0021E544 File Offset: 0x0021C744
				public void SetResources(IEnumerable<IResource> resources)
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						this.resources = resources.ToArray<IResource>();
					}
				}

				// Token: 0x0600A3D0 RID: 41936 RVA: 0x0000336E File Offset: 0x0000156E
				public void SetResourcesAccessed(IEnumerable<IResource> resources)
				{
				}

				// Token: 0x0600A3D1 RID: 41937 RVA: 0x0000336E File Offset: 0x0000156E
				public void SetCultureAccessed()
				{
				}

				// Token: 0x0600A3D2 RID: 41938 RVA: 0x0021E58C File Offset: 0x0021C78C
				public void SetComplete()
				{
					object obj = this.syncRoot;
					lock (obj)
					{
						this.complete = true;
					}
				}

				// Token: 0x0600A3D3 RID: 41939 RVA: 0x0021E5D0 File Offset: 0x0021C7D0
				public bool TryGetPreviewValue(out EvaluationResult2<IPreviewValueSource> result, out DateTime staleSince, out bool sampled)
				{
					result = default(EvaluationResult2<IPreviewValueSource>);
					staleSince = default(DateTime);
					sampled = false;
					return false;
				}

				// Token: 0x0600A3D4 RID: 41940 RVA: 0x0021E5E4 File Offset: 0x0021C7E4
				public bool TryGetResources(out IEnumerable<IResource> resources)
				{
					object obj = this.syncRoot;
					bool flag2;
					lock (obj)
					{
						resources = this.resources;
						flag2 = resources != null;
					}
					return flag2;
				}

				// Token: 0x0600A3D5 RID: 41941 RVA: 0x000E6755 File Offset: 0x000E4955
				public bool TryGetResourcesAccessed(out IEnumerable<IResource> resources)
				{
					resources = null;
					return false;
				}

				// Token: 0x0600A3D6 RID: 41942 RVA: 0x0021E630 File Offset: 0x0021C830
				public bool TryGetComplete()
				{
					object obj = this.syncRoot;
					bool flag2;
					lock (obj)
					{
						flag2 = this.complete;
					}
					return flag2;
				}

				// Token: 0x04005536 RID: 21814
				private readonly object syncRoot = new object();

				// Token: 0x04005537 RID: 21815
				private bool complete;

				// Token: 0x04005538 RID: 21816
				private IEnumerable<IResource> resources;
			}
		}
	}
}
