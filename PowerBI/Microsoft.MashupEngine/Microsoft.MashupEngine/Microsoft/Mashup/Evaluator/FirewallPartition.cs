using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CC0 RID: 7360
	internal class FirewallPartition : IEngineHost, IFirewallService, IResourcePermissionService, ITraitPrivacyService, IEvaluationConstants
	{
		// Token: 0x0600B761 RID: 46945 RVA: 0x00253660 File Offset: 0x00251860
		public FirewallPartition(Firewall firewall, FirewallGroupEnforcer groupEnforcer, FirewallFlowEnforcer flowEnforcer, IPartitionKey partitionKey, bool isCyclic)
		{
			this.firewall = firewall;
			this.evaluationConstants = this.firewall.EngineHost.QueryService<IEvaluationConstants>();
			this.tracedConstants = new EvaluationConstant[]
			{
				new EvaluationConstant("PartitionKey", partitionKey.ToString(), true)
			};
			this.groupEnforcer = groupEnforcer;
			this.flowEnforcer = flowEnforcer;
			this.partitionKey = partitionKey;
			if (firewall.RootPartitionKey.ToSerializedString() != partitionKey.ToSerializedString())
			{
				this.traitTrackingService = new PartitionTraitTrackingService(this.firewall.EngineHost);
			}
			this.isCyclic = isCyclic;
			if (this.isCyclic)
			{
				string firewallFlow_Cyclic_Reference = Strings.FirewallFlow_Cyclic_Reference;
				IRecordValue recordValue = this.firewall.Engine.ExceptionRecord(this.firewall.Engine.Text("Expression.Error"), this.firewall.Engine.Text(firewallFlow_Cyclic_Reference), this.firewall.Engine.Null);
				this.result = new EvaluationResult2<IValue>(this.firewall.Engine.Exception(recordValue));
			}
		}

		// Token: 0x17002D9C RID: 11676
		// (get) Token: 0x0600B762 RID: 46946 RVA: 0x00253773 File Offset: 0x00251973
		public IPartitionKey PartitionKey
		{
			get
			{
				return this.partitionKey;
			}
		}

		// Token: 0x17002D9D RID: 11677
		// (get) Token: 0x0600B763 RID: 46947 RVA: 0x0025377B File Offset: 0x0025197B
		public FirewallGroup2 FirewallGroup
		{
			get
			{
				if (this.flowEnforcer.HasFlows)
				{
					return this.flowEnforcer.OutflowGroup;
				}
				return this.groupEnforcer.CurrentGroup;
			}
		}

		// Token: 0x17002D9E RID: 11678
		// (get) Token: 0x0600B764 RID: 46948 RVA: 0x002537A1 File Offset: 0x002519A1
		public bool IsCyclic
		{
			get
			{
				return this.isCyclic;
			}
		}

		// Token: 0x17002D9F RID: 11679
		// (get) Token: 0x0600B765 RID: 46949 RVA: 0x002537A9 File Offset: 0x002519A9
		public FirewallGroupEnforcer GroupEnforcer
		{
			get
			{
				return this.groupEnforcer;
			}
		}

		// Token: 0x17002DA0 RID: 11680
		// (get) Token: 0x0600B766 RID: 46950 RVA: 0x002537B1 File Offset: 0x002519B1
		public FirewallFlowEnforcer FlowEnforcer
		{
			get
			{
				return this.flowEnforcer;
			}
		}

		// Token: 0x17002DA1 RID: 11681
		// (get) Token: 0x0600B767 RID: 46951 RVA: 0x002537BC File Offset: 0x002519BC
		public bool AccessesDataSources
		{
			get
			{
				if (this.FlowEnforcer.HasFlows)
				{
					if (this.FlowEnforcer.OutflowGroup.Equals(FirewallGroup2.None))
					{
						return false;
					}
				}
				else if (this.GroupEnforcer.CurrentGroup.Equals(FirewallGroup2.None))
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x17002DA2 RID: 11682
		// (get) Token: 0x0600B768 RID: 46952 RVA: 0x00253809 File Offset: 0x00251A09
		public Guid ActivityId
		{
			get
			{
				return this.evaluationConstants.ActivityId;
			}
		}

		// Token: 0x17002DA3 RID: 11683
		// (get) Token: 0x0600B769 RID: 46953 RVA: 0x00253816 File Offset: 0x00251A16
		public string CorrelationId
		{
			get
			{
				return this.evaluationConstants.CorrelationId;
			}
		}

		// Token: 0x17002DA4 RID: 11684
		// (get) Token: 0x0600B76A RID: 46954 RVA: 0x00253824 File Offset: 0x00251A24
		public IEnumerable<EvaluationConstant> TracedConstants
		{
			get
			{
				if (this.evaluationConstants.TracedConstants == null)
				{
					return this.tracedConstants;
				}
				return this.evaluationConstants.TracedConstants.Concat(this.tracedConstants);
			}
		}

		// Token: 0x0600B76B RID: 46955 RVA: 0x0025385D File Offset: 0x00251A5D
		public bool TryGetResources(out IEnumerable<IResource> resources)
		{
			if (!this.flowEnforcer.HasFlows)
			{
				resources = this.GroupEnforcer.Resources;
				return true;
			}
			resources = null;
			return false;
		}

		// Token: 0x0600B76C RID: 46956 RVA: 0x0025387F File Offset: 0x00251A7F
		public IValue GetValue(string partitionKeyString)
		{
			return this.firewall.GetValue(this, partitionKeyString);
		}

		// Token: 0x0600B76D RID: 46957 RVA: 0x00253890 File Offset: 0x00251A90
		public IValue GetValue()
		{
			if (this.result.Result == null)
			{
				using (ManualResetEvent waitHandle = new ManualResetEvent(false))
				{
					this.BeginGetResult<IValue>(delegate(EvaluationResult2<IValue> result)
					{
						this.result = result;
						waitHandle.Set();
					});
					waitHandle.WaitOne();
				}
			}
			return this.result.Result;
		}

		// Token: 0x0600B76E RID: 46958 RVA: 0x00253914 File Offset: 0x00251B14
		public IValue GetBufferedValue()
		{
			IValue value;
			using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("FirewallPartition/GetBufferedValue", null, TraceEventType.Information, null))
			{
				string serializedPartitionKey = this.partitionKey.ToSerializedString();
				IDataReaderSource dataReaderSource = this.firewall.ValueBuffer.GetDataReaderSource(serializedPartitionKey);
				ValueShape valueShape = dataReaderSource.TableSource.ValueShape;
				ITableValue tableValue = this.firewall.Engine.Table(this.firewall.EngineHost, delegate
				{
					IDataReaderSource dataReaderSource2;
					try
					{
						dataReaderSource2 = dataReaderSource ?? this.firewall.ValueBuffer.GetDataReaderSource(serializedPartitionKey);
					}
					finally
					{
						dataReaderSource = null;
					}
					return dataReaderSource2;
				});
				try
				{
					IKeys keys = tableValue.Type.AsTableType.ItemType.Fields.Keys;
					switch (valueShape)
					{
					case ValueShape.Record:
						value = tableValue[0].AsRecord;
						break;
					case ValueShape.List:
						value = tableValue[keys[0]];
						break;
					case ValueShape.Primitive:
						value = tableValue[keys[0]][0];
						break;
					default:
						value = tableValue;
						break;
					}
				}
				catch (IOException ex)
				{
					throw new HostingException(Strings.Firewall_Buffering_Failed(this.firewall.GetDisplayName(this.partitionKey), ex.Message), "BufferingError");
				}
				catch (Exception ex2)
				{
					if (!SafeExceptions.TraceIsSafeException(hostTrace, ex2))
					{
						throw;
					}
					throw new FirewallException2(Strings.Firewall_Buffering_Failed(this.firewall.GetDisplayName(this.partitionKey), ex2.Message), ex2);
				}
			}
			return value;
		}

		// Token: 0x0600B76F RID: 46959 RVA: 0x00253ACC File Offset: 0x00251CCC
		public IEvaluation BeginGetResult<T>(Action<EvaluationResult2<T>> callback)
		{
			if (this.isCyclic)
			{
				callback(new EvaluationResult2<T>(this.result.Exception));
				return new EmptyEvaluation();
			}
			IDocumentEvaluator<T> documentEvaluator = (IDocumentEvaluator<T>)this.firewall.CreateEvaluator(this);
			DocumentEvaluationParameters documentEvaluationParameters = this.firewall.PartitionParameters.Where((DocumentEvaluationParameters p) => p.partitionKey.Equals(this.partitionKey)).Single<DocumentEvaluationParameters>();
			return documentEvaluator.BeginGetResult(documentEvaluationParameters, callback);
		}

		// Token: 0x0600B770 RID: 46960 RVA: 0x00253B38 File Offset: 0x00251D38
		public void BeginBufferValue(Action<Exception> callback)
		{
			IHostProgress hostProgress = this.GetHostProgress();
			Action<int> writeCallback = delegate(int size)
			{
				hostProgress.RecordBytesRead((long)size);
			};
			this.BeginGetResult<IDataReaderSource>(delegate(EvaluationResult2<IDataReaderSource> result)
			{
				using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("FirewallDocumentEvaluator/BeginBufferValue/callback", null, TraceEventType.Information, null))
				{
					Exception ex = null;
					try
					{
						try
						{
							using (IDataReaderSource dataReaderSource = result.Result)
							{
								this.firewall.ValueBuffer.SetDataReaderSource(this.partitionKey.ToSerializedString(), dataReaderSource, writeCallback);
							}
						}
						catch (ValueException2 valueException)
						{
							this.firewall.ValueBuffer.SetException(this.partitionKey.ToSerializedString(), valueException, writeCallback);
						}
					}
					catch (HostingException ex2)
					{
						ex = new HostingException(Strings.Firewall_Buffering_Failed(this.firewall.GetDisplayName(this.partitionKey), ex2.Message), ex2.Reason);
					}
					catch (ValueBufferingException ex3)
					{
						ex = new FirewallException2(Strings.Firewall_Buffering_Failed(this.firewall.GetDisplayName(this.partitionKey), ex3.InnerException.Message), ex3.InnerException);
					}
					catch (Exception ex4)
					{
						if (!SafeExceptions.TraceIsSafeException(hostTrace, ex4))
						{
							throw;
						}
						ex = ex4;
					}
					if (ex != null)
					{
						ex = ex.ToCallbackException();
					}
					callback(ex);
				}
			});
		}

		// Token: 0x0600B771 RID: 46961 RVA: 0x0000336E File Offset: 0x0000156E
		public void OtherPartitionAccessed(IPartitionKey otherPartitionKey)
		{
		}

		// Token: 0x0600B772 RID: 46962 RVA: 0x00253B8A File Offset: 0x00251D8A
		public bool IsResourceAccessPermitted(IResource resource, out ResourceCredentialCollection credentials)
		{
			if (!this.GetCredentialService().TryGetCredentials(resource, out credentials))
			{
				credentials = null;
				return false;
			}
			if (!this.groupEnforcer.TryAddResource(resource))
			{
				throw this.firewall.BuildRuleException(new IResource[] { resource });
			}
			return true;
		}

		// Token: 0x0600B773 RID: 46963 RVA: 0x00253BC8 File Offset: 0x00251DC8
		public void VerifyPrivacyTrait(IResource resource, IValue traits)
		{
			FirewallGroup2 firewallGroup;
			if (!this.FlowEnforcer.HasFlows && this.groupEnforcer.TryGetResource(resource, out firewallGroup))
			{
				FirewallGroup2 firewallGroup2 = this.firewall.EngineHost.QueryService<IFirewallRuleService>().UpdateFirewallGroup(resource, firewallGroup, traits);
				if (this.groupEnforcer.TryUpdateResource(resource, firewallGroup2))
				{
					return;
				}
			}
			throw this.firewall.BuildRuleException(new IResource[] { resource });
		}

		// Token: 0x0600B774 RID: 46964 RVA: 0x00253C30 File Offset: 0x00251E30
		T IEngineHost.QueryService<T>()
		{
			if (typeof(T) == typeof(IResourcePermissionService) || typeof(T) == typeof(IFirewallService) || typeof(T) == typeof(IEvaluationConstants) || typeof(T) == typeof(ITraitPrivacyService))
			{
				return (T)((object)this);
			}
			if (typeof(T) == typeof(ITraitTrackingService) && this.traitTrackingService != null)
			{
				return (T)((object)this.traitTrackingService);
			}
			return this.firewall.EngineHost.QueryService<T>();
		}

		// Token: 0x0600B775 RID: 46965 RVA: 0x00253CF0 File Offset: 0x00251EF0
		private IHostProgress GetHostProgress()
		{
			IProgressService progressService = this.firewall.EngineHost.QueryService<IProgressService>();
			if (progressService != null)
			{
				return progressService.GetHostProgress("Staging", this.firewall.GetDisplayName(this.partitionKey));
			}
			return FirewallPartition.NoHostProgress.Instance;
		}

		// Token: 0x0600B776 RID: 46966 RVA: 0x00253D33 File Offset: 0x00251F33
		private ICredentialService GetCredentialService()
		{
			return this.firewall.EngineHost.QueryService<ICredentialService>();
		}

		// Token: 0x0600B777 RID: 46967 RVA: 0x00253D45 File Offset: 0x00251F45
		public void AddTrait(IRecordValue trait)
		{
			this.traitTrackingService.AddTrait(trait);
		}

		// Token: 0x0600B778 RID: 46968 RVA: 0x00253D53 File Offset: 0x00251F53
		public IRecordValue[] GetTraits()
		{
			return this.traitTrackingService.GetTraits();
		}

		// Token: 0x04005D93 RID: 23955
		private readonly Firewall firewall;

		// Token: 0x04005D94 RID: 23956
		private readonly FirewallGroupEnforcer groupEnforcer;

		// Token: 0x04005D95 RID: 23957
		private readonly FirewallFlowEnforcer flowEnforcer;

		// Token: 0x04005D96 RID: 23958
		private readonly IPartitionKey partitionKey;

		// Token: 0x04005D97 RID: 23959
		private readonly ITraitTrackingService traitTrackingService;

		// Token: 0x04005D98 RID: 23960
		private readonly bool isCyclic;

		// Token: 0x04005D99 RID: 23961
		private EvaluationResult2<IValue> result;

		// Token: 0x04005D9A RID: 23962
		private IEvaluationConstants evaluationConstants;

		// Token: 0x04005D9B RID: 23963
		private EvaluationConstant[] tracedConstants;

		// Token: 0x02001CC1 RID: 7361
		private class NoHostProgress : IHostProgress
		{
			// Token: 0x0600B77A RID: 46970 RVA: 0x0000336E File Offset: 0x0000156E
			public void StartRequest()
			{
			}

			// Token: 0x0600B77B RID: 46971 RVA: 0x0000336E File Offset: 0x0000156E
			public void StopRequest()
			{
			}

			// Token: 0x0600B77C RID: 46972 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordRowRead()
			{
			}

			// Token: 0x0600B77D RID: 46973 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordRowsRead(long rowsRead)
			{
			}

			// Token: 0x0600B77E RID: 46974 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordRowWritten()
			{
			}

			// Token: 0x0600B77F RID: 46975 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordRowsWritten(long rowsWritten)
			{
			}

			// Token: 0x0600B780 RID: 46976 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordBytesRead(long bytesRead)
			{
			}

			// Token: 0x0600B781 RID: 46977 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordBytesWritten(long bytesWritten)
			{
			}

			// Token: 0x0600B782 RID: 46978 RVA: 0x0000336E File Offset: 0x0000156E
			public void RecordPercentComplete(int percent)
			{
			}

			// Token: 0x04005D9C RID: 23964
			public static readonly IHostProgress Instance = new FirewallPartition.NoHostProgress();
		}
	}
}
