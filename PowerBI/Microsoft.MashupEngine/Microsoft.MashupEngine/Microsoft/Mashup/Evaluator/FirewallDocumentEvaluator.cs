using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CAF RID: 7343
	internal class FirewallDocumentEvaluator : IDocumentEvaluator, IDocumentEvaluator<IPreviewValueSource>, IDocumentEvaluator<IDataReaderSource>, IDocumentEvaluator<IStreamSource>
	{
		// Token: 0x0600B6E6 RID: 46822 RVA: 0x00251C8B File Offset: 0x0024FE8B
		public FirewallDocumentEvaluator(IEngineHost engineHost, IEngine engine, Func<IEngineHost, IEngine, IDocumentEvaluator> evaluatorCtor)
		{
			this.engineHost = engineHost;
			this.engine = engine;
			this.evaluatorCtor = evaluatorCtor;
		}

		// Token: 0x0600B6E7 RID: 46823 RVA: 0x00251CA8 File Offset: 0x0024FEA8
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IPreviewValueSource>> callback)
		{
			IEngineHost originalEngineHost = this.engineHost;
			this.engineHost = PartitionTraitTrackingService.WrapTrackingService(this.engineHost);
			return this.BeginGetResultInternal<IPreviewValueSource>(parameters, delegate(EvaluationResult2<IPreviewValueSource> result)
			{
				this.engineHost = originalEngineHost;
				callback(result);
			});
		}

		// Token: 0x0600B6E8 RID: 46824 RVA: 0x00251CF9 File Offset: 0x0024FEF9
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IDataReaderSource>> callback)
		{
			return this.BeginGetResultInternal<IDataReaderSource>(parameters, callback);
		}

		// Token: 0x0600B6E9 RID: 46825 RVA: 0x00251D03 File Offset: 0x0024FF03
		public IEvaluation BeginGetResult(DocumentEvaluationParameters parameters, Action<EvaluationResult2<IStreamSource>> callback)
		{
			return this.BeginGetResultInternal<IStreamSource>(parameters, callback);
		}

		// Token: 0x0600B6EA RID: 46826 RVA: 0x00251D10 File Offset: 0x0024FF10
		private IEvaluation BeginGetResultInternal<T>(DocumentEvaluationParameters parameters, Action<EvaluationResult2<T>> callback) where T : IDisposable
		{
			IHostTrace hostTrace = EvaluatorTracing.CreatePerformanceTrace("FirewallDocumentEvaluator/GetResult<" + typeof(T).FullName + ">", this.engineHost, TraceEventType.Information, null);
			parameters.SetUiCulture();
			List<DocumentEvaluationParameters> list = new List<DocumentEvaluationParameters>();
			foreach (IFirewallPartitionPlan firewallPartitionPlan in parameters.firewallPlan.PartitionPlans.OrderBy((IFirewallPartitionPlan p) => p.EvaluationOrder))
			{
				if (!firewallPartitionPlan.PartitionKey.Equals(parameters.partitionKey))
				{
					list.Add(this.CreatePartitionParameters(parameters, firewallPartitionPlan.PartitionKey));
				}
			}
			DocumentEvaluationParameters documentEvaluationParameters = this.CreatePartitionParameters(parameters, parameters.partitionKey);
			documentEvaluationParameters.expression = parameters.expression;
			documentEvaluationParameters.executeAction = parameters.executeAction;
			list.Add(documentEvaluationParameters);
			Firewall firewall = null;
			IEvaluation evaluation2;
			try
			{
				firewall = new Firewall(this.engineHost, this.engine, this.evaluatorCtor, list, parameters.firewallPlan, parameters.partitionKey);
				firewall.CreatePartitions();
				foreach (KeyValuePair<IPartitionKey, FirewallPartition> keyValuePair in firewall.Partitions)
				{
					using (IHostTrace hostTrace2 = EvaluatorTracing.CreateTrace("Firewall/Partition", this.engineHost, TraceEventType.Information, null))
					{
						hostTrace2.Add("PartitionKey", keyValuePair.Value.PartitionKey.ToSerializedString(), true);
						IHostTrace hostTrace3 = hostTrace2;
						string text = "AccessedResources";
						bool flag = true;
						FirewallGroupEnforcer groupEnforcer = keyValuePair.Value.GroupEnforcer;
						hostTrace3.AddValues(text, flag, (groupEnforcer != null) ? groupEnforcer.Resources : null);
						IHostTrace hostTrace4 = hostTrace2;
						string text2 = "FirewallGroup";
						FirewallGroup2 firewallGroup = keyValuePair.Value.FirewallGroup;
						hostTrace4.Add(text2, (firewallGroup != null) ? firewallGroup.ToString() : null, true);
						IHostTrace hostTrace5 = hostTrace2;
						string text3 = "PartitionInputs";
						bool flag2 = true;
						FirewallFlowEnforcer flowEnforcer = keyValuePair.Value.FlowEnforcer;
						IEnumerable<string> enumerable;
						if (flowEnforcer == null)
						{
							enumerable = null;
						}
						else
						{
							IEnumerable<IPartitionKey> flows = flowEnforcer.GetFlows();
							if (flows == null)
							{
								enumerable = null;
							}
							else
							{
								enumerable = flows.Select((IPartitionKey p) => p.ToSerializedString());
							}
						}
						hostTrace5.AddValues(text3, flag2, enumerable);
					}
				}
				IReportPartitionResources reportPartitionResources = this.engineHost.QueryService<IReportPartitionResources>();
				FirewallDocumentEvaluator.Evaluation<T> evaluation = new FirewallDocumentEvaluator.Evaluation<T>(firewall, hostTrace, reportPartitionResources, firewall.GetPartition(parameters.partitionKey), callback);
				firewall.BeginBufferPartitions(new Action<Exception>(evaluation.OnBufferComplete));
				evaluation2 = evaluation;
			}
			catch (Exception ex)
			{
				using (hostTrace)
				{
					if (!SafeExceptions.TraceIsSafeException(hostTrace, ex))
					{
						throw;
					}
					if (firewall != null)
					{
						firewall.Dispose();
					}
				}
				callback(new EvaluationResult2<T>(ex.ToCallbackException()));
				evaluation2 = new EmptyEvaluation();
			}
			return evaluation2;
		}

		// Token: 0x0600B6EB RID: 46827 RVA: 0x00252040 File Offset: 0x00250240
		private DocumentEvaluationParameters CreatePartitionParameters(DocumentEvaluationParameters parameters, IPartitionKey partitionKey)
		{
			List<PackageEdit> list = new List<PackageEdit>();
			foreach (IFirewallPartitionPlan firewallPartitionPlan in parameters.firewallPlan.PartitionPlans)
			{
				if (!firewallPartitionPlan.PartitionKey.Equals(partitionKey))
				{
					string text = string.Format(CultureInfo.InvariantCulture, "Value.Firewall({0})", this.engine.EscapeString(firewallPartitionPlan.PartitionKey.ToSerializedString()));
					list.AddRange(parameters.document.ReplacePartition(firewallPartitionPlan.PartitionKey, SegmentedString.New(text)));
				}
			}
			string text2;
			list.AddRange(parameters.document.ReferencePartition(partitionKey, out text2));
			parameters = parameters.Clone();
			parameters.document = new EditsPartitionedDocument(this.engine, parameters.document, list);
			parameters.partitionKey = partitionKey;
			parameters.expression = text2;
			parameters.executeAction = false;
			return parameters;
		}

		// Token: 0x04005D57 RID: 23895
		private const string partitionReferenceTemplate = "Value.Firewall({0})";

		// Token: 0x04005D58 RID: 23896
		private readonly IEngine engine;

		// Token: 0x04005D59 RID: 23897
		private readonly Func<IEngineHost, IEngine, IDocumentEvaluator> evaluatorCtor;

		// Token: 0x04005D5A RID: 23898
		private IEngineHost engineHost;

		// Token: 0x02001CB0 RID: 7344
		private sealed class Evaluation<T> : IEvaluation where T : IDisposable
		{
			// Token: 0x0600B6EC RID: 46828 RVA: 0x00252130 File Offset: 0x00250330
			public Evaluation(Firewall firewall, IHostTrace trace, IReportPartitionResources resources, FirewallPartition rootPartition, Action<EvaluationResult2<T>> callback)
			{
				this.firewall = firewall;
				this.trace = trace;
				this.resources = resources;
				this.rootPartition = rootPartition;
				this.callback = callback;
			}

			// Token: 0x0600B6ED RID: 46829 RVA: 0x0000336E File Offset: 0x0000156E
			public void Cancel()
			{
			}

			// Token: 0x0600B6EE RID: 46830 RVA: 0x00252160 File Offset: 0x00250360
			public void OnBufferComplete(Exception exception)
			{
				if (exception != null)
				{
					this.firewall.Dispose();
					this.trace.Dispose();
					this.callback(new EvaluationResult2<T>(exception));
					return;
				}
				this.rootPartition.BeginGetResult<T>(delegate(EvaluationResult2<T> result)
				{
					this.trace.Suspend();
					this.callback.InvokeThenOnDispose(result, delegate
					{
						this.ReportPartitionResources();
						try
						{
							T result;
							if (result.Exception == null)
							{
								result = result.Result;
								result.Dispose();
							}
						}
						finally
						{
							this.firewall.Dispose();
							this.trace.Dispose();
						}
					});
				});
			}

			// Token: 0x0600B6EF RID: 46831 RVA: 0x002521B0 File Offset: 0x002503B0
			private void ReportPartitionResources()
			{
				IEnumerable<IResource> enumerable;
				if (this.rootPartition.TryGetResources(out enumerable) && this.resources != null)
				{
					this.resources.PartitionResources(this.rootPartition.PartitionKey, enumerable);
				}
			}

			// Token: 0x04005D5B RID: 23899
			private readonly Firewall firewall;

			// Token: 0x04005D5C RID: 23900
			private readonly IHostTrace trace;

			// Token: 0x04005D5D RID: 23901
			private readonly IReportPartitionResources resources;

			// Token: 0x04005D5E RID: 23902
			private readonly FirewallPartition rootPartition;

			// Token: 0x04005D5F RID: 23903
			private readonly Action<EvaluationResult2<T>> callback;
		}
	}
}
