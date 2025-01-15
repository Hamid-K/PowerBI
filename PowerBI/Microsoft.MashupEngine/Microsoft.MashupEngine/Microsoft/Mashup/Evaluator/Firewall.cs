using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CAC RID: 7340
	internal class Firewall : IDisposable
	{
		// Token: 0x0600B6C5 RID: 46789 RVA: 0x00251358 File Offset: 0x0024F558
		public Firewall(IEngineHost engineHost, IEngine engine, Func<IEngineHost, IEngine, IDocumentEvaluator> evaluatorCtor, IEnumerable<DocumentEvaluationParameters> partitionParameters, IFirewallPlan firewallPlan, IPartitionKey rootPartitionKey)
		{
			this.engineHost = engineHost;
			this.engine = engine;
			this.evaluatorCtor = evaluatorCtor;
			this.partitionParameters = partitionParameters;
			this.firewallPlan = firewallPlan;
			this.rootPartitionKey = rootPartitionKey;
			this.firewallRuleService = this.engineHost.QueryService<IFirewallRuleService>();
			this.resourcePathService = this.engineHost.QueryService<IResourcePathService>();
			IPartitionDisplayNameService partitionDisplayNameService = this.engineHost.QueryService<IPartitionDisplayNameService>();
			this.getDisplayName = ((partitionDisplayNameService != null) ? new Func<IPartitionKey, string>(partitionDisplayNameService.GetDisplayNameForPartition) : Firewall.defaultGetDisplayName);
			this.valueBuffer = this.engineHost.QueryService<IValueBufferingService>().CreateBuffer();
		}

		// Token: 0x17002D7E RID: 11646
		// (get) Token: 0x0600B6C6 RID: 46790 RVA: 0x002513F9 File Offset: 0x0024F5F9
		public IEngineHost EngineHost
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x17002D7F RID: 11647
		// (get) Token: 0x0600B6C7 RID: 46791 RVA: 0x00251401 File Offset: 0x0024F601
		public IEngine Engine
		{
			get
			{
				return this.engine;
			}
		}

		// Token: 0x17002D80 RID: 11648
		// (get) Token: 0x0600B6C8 RID: 46792 RVA: 0x00251409 File Offset: 0x0024F609
		public IEnumerable<DocumentEvaluationParameters> PartitionParameters
		{
			get
			{
				return this.partitionParameters;
			}
		}

		// Token: 0x17002D81 RID: 11649
		// (get) Token: 0x0600B6C9 RID: 46793 RVA: 0x00251411 File Offset: 0x0024F611
		public Dictionary<IPartitionKey, FirewallPartition> Partitions
		{
			get
			{
				return this.partitions;
			}
		}

		// Token: 0x0600B6CA RID: 46794 RVA: 0x00251419 File Offset: 0x0024F619
		public string GetDisplayName(IPartitionKey partitionKey)
		{
			return this.getDisplayName(partitionKey);
		}

		// Token: 0x17002D82 RID: 11650
		// (get) Token: 0x0600B6CB RID: 46795 RVA: 0x00251427 File Offset: 0x0024F627
		public IValueBuffer ValueBuffer
		{
			get
			{
				return this.valueBuffer;
			}
		}

		// Token: 0x17002D83 RID: 11651
		// (get) Token: 0x0600B6CC RID: 46796 RVA: 0x0025142F File Offset: 0x0024F62F
		internal IFirewallPlan FirewallPlan
		{
			get
			{
				return this.firewallPlan;
			}
		}

		// Token: 0x17002D84 RID: 11652
		// (get) Token: 0x0600B6CD RID: 46797 RVA: 0x00251437 File Offset: 0x0024F637
		internal IPartitionKey RootPartitionKey
		{
			get
			{
				return this.rootPartitionKey;
			}
		}

		// Token: 0x17002D85 RID: 11653
		// (get) Token: 0x0600B6CE RID: 46798 RVA: 0x0025143F File Offset: 0x0024F63F
		private IFunctionValue AddExpressionFunction
		{
			get
			{
				if (this.addExpressionFunction == null)
				{
					this.addExpressionFunction = this.CompileFunction("(value, memberName, optional letName) => let\r\n            MakeNavigationStep = (collection, name) => if name = null then collection else [\r\n                Kind = \"FieldAccess\",\r\n                IsOptional = false,\r\n                Expression = [\r\n                    Kind = \"ElementAccess\",\r\n                    Collection = collection,\r\n                    Key = [Kind = \"Constant\", Value = [Name = name]],\r\n                    IsOptional = false\r\n                ],\r\n                MemberName = \"Data\"\r\n            ],\r\n            ModelStorage = [\r\n                Kind = \"Invocation\",\r\n                Function = [Kind = \"Identifier\", Name = \"Pipeline.DefaultModelStorage\", IsInclusive = false],\r\n                Arguments = {}\r\n            ]\r\n        in\r\n            if (value is table) then\r\n                Table.View(null, [\r\n                    GetType = () => Value.Type(value),\r\n                    GetRows = () => value,\r\n                    GetRowCount = () => Table.RowCount(value),\r\n                    GetExpression = () => MakeNavigationStep(MakeNavigationStep(ModelStorage, memberName), letName)\r\n                ])\r\n            else\r\n                value");
				}
				return this.addExpressionFunction;
			}
		}

		// Token: 0x0600B6CF RID: 46799 RVA: 0x00251460 File Offset: 0x0024F660
		public void CreatePartitions()
		{
			if (this.partitions != null)
			{
				return;
			}
			this.partitions = new Dictionary<IPartitionKey, FirewallPartition>(PartitionKeyEqualityComparer.Instance);
			foreach (IFirewallPartitionPlan firewallPartitionPlan in this.firewallPlan.PartitionPlans)
			{
				if (firewallPartitionPlan.PartitionKey != this.rootPartitionKey && firewallPartitionPlan.Exception != null)
				{
					throw firewallPartitionPlan.Exception;
				}
			}
			foreach (IFirewallPartitionPlan firewallPartitionPlan2 in this.firewallPlan.PartitionPlans.OrderBy((IFirewallPartitionPlan p) => p.EvaluationOrder))
			{
				FirewallPartition firewallPartition = this.CreatePartition(firewallPartitionPlan2);
				this.partitions.Add(firewallPartitionPlan2.PartitionKey, firewallPartition);
			}
			bool flag = false;
			using (Dictionary<IPartitionKey, FirewallPartition>.ValueCollection.Enumerator enumerator2 = this.partitions.Values.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (!enumerator2.Current.FlowEnforcer.IsComplete)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				throw this.BuildRuleException(Array.Empty<IResource>());
			}
		}

		// Token: 0x0600B6D0 RID: 46800 RVA: 0x002515BC File Offset: 0x0024F7BC
		public void BeginBufferPartitions(Action<Exception> callback)
		{
			HashSet<IPartitionKey> hashSet = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			foreach (FirewallPartition firewallPartition in this.partitions.Values)
			{
				foreach (IPartitionKey partitionKey in firewallPartition.FlowEnforcer.GetFlows(FirewallFlow.Limited))
				{
					hashSet.Add(partitionKey);
				}
			}
			List<FirewallPartition> list = new List<FirewallPartition>();
			foreach (IFirewallPartitionPlan firewallPartitionPlan in this.firewallPlan.PartitionPlans.OrderBy((IFirewallPartitionPlan p) => p.EvaluationOrder))
			{
				if (hashSet.Contains(firewallPartitionPlan.PartitionKey))
				{
					list.Add(this.partitions[firewallPartitionPlan.PartitionKey]);
				}
			}
			this.BufferNextPartition(list, 0, callback);
		}

		// Token: 0x0600B6D1 RID: 46801 RVA: 0x002516F8 File Offset: 0x0024F8F8
		public FirewallPartition GetPartition(IPartitionKey partitionKey)
		{
			this.CreatePartitions();
			return this.partitions[partitionKey];
		}

		// Token: 0x0600B6D2 RID: 46802 RVA: 0x0025170C File Offset: 0x0024F90C
		public IValue GetValue(FirewallPartition fromPartition, string partitionKeyString)
		{
			IPartitionKey partitionKey;
			try
			{
				partitionKey = partitionKeyString.ToPartitionKey(fromPartition.PartitionKey.PartitioningScheme);
			}
			catch (ArgumentException)
			{
				throw new FirewallException2(Strings.FirewallFlow_NoMatch(partitionKeyString), null);
			}
			FirewallFlow firewallFlow;
			if (!fromPartition.FlowEnforcer.TryGetFlow(partitionKey, out firewallFlow))
			{
				throw new FirewallException2(FirewallStrings.FirewallFlow_IllegalReference(this.getDisplayName(fromPartition.PartitionKey), this.getDisplayName(partitionKey)), null);
			}
			fromPartition.OtherPartitionAccessed(partitionKey);
			FirewallPartition firewallPartition;
			if (!this.partitions.TryGetValue(partitionKey, out firewallPartition))
			{
				throw new FirewallException2(FirewallStrings.FirewallFlow_IllegalReference(this.getDisplayName(fromPartition.PartitionKey), this.getDisplayName(partitionKey)), null);
			}
			if (firewallFlow == FirewallFlow.Allowed)
			{
				return firewallPartition.GetValue();
			}
			if (firewallFlow != FirewallFlow.Limited)
			{
				throw new InvalidOperationException();
			}
			return this.AddStagingLocation(firewallPartition.GetBufferedValue(), partitionKey);
		}

		// Token: 0x0600B6D3 RID: 46803 RVA: 0x002517E8 File Offset: 0x0024F9E8
		public IDocumentEvaluator CreateEvaluator(IEngineHost engineHost)
		{
			return this.evaluatorCtor(engineHost, this.engine);
		}

		// Token: 0x0600B6D4 RID: 46804 RVA: 0x002517FC File Offset: 0x0024F9FC
		public FirewallRuleException2 BuildRuleException(params IResource[] failedResources)
		{
			return new FirewallRuleException2(this.AllResources.Concat(failedResources).ToArray<IResource>(), Strings.FirewallRuleRequired);
		}

		// Token: 0x0600B6D5 RID: 46805 RVA: 0x00251819 File Offset: 0x0024FA19
		public void Dispose()
		{
			if (this.valueBuffer != null)
			{
				this.valueBuffer.Dispose();
				this.valueBuffer = null;
			}
		}

		// Token: 0x17002D86 RID: 11654
		// (get) Token: 0x0600B6D6 RID: 46806 RVA: 0x00251838 File Offset: 0x0024FA38
		private IEnumerable<IResource> AllResources
		{
			get
			{
				HashSet<IResource> hashSet = new HashSet<IResource>();
				foreach (FirewallPartition firewallPartition in this.partitions.Values)
				{
					hashSet.UnionWith(firewallPartition.GroupEnforcer.Resources);
				}
				foreach (IFirewallPartitionPlan firewallPartitionPlan in this.firewallPlan.PartitionPlans)
				{
					hashSet.UnionWith(firewallPartitionPlan.Resources);
				}
				return hashSet;
			}
		}

		// Token: 0x0600B6D7 RID: 46807 RVA: 0x002518EC File Offset: 0x0024FAEC
		private FirewallPartition CreatePartition(IFirewallPartitionPlan partitionPlan)
		{
			FirewallFlowEnforcer firewallFlowEnforcer = new FirewallFlowEnforcer();
			if (partitionPlan.Inputs.Any<IPartitionKey>())
			{
				foreach (IPartitionKey partitionKey in partitionPlan.Inputs)
				{
					if (partitionPlan.IsCyclic)
					{
						firewallFlowEnforcer.AddFlow(partitionKey, FirewallGroup2.None);
					}
					else
					{
						FirewallPartition firewallPartition = this.partitions[partitionKey];
						firewallFlowEnforcer.AddFlow(partitionKey, firewallPartition.FirewallGroup);
					}
				}
			}
			firewallFlowEnforcer.InitializeFlows();
			bool flag = !partitionPlan.IsCyclic && !partitionPlan.Inputs.Any((IPartitionKey i) => this.partitions[i].AccessesDataSources);
			FirewallGroupEnforcer firewallGroupEnforcer = new FirewallGroupEnforcer(this.firewallRuleService, this.resourcePathService, this.getDisplayName, partitionPlan.PartitionKey, FirewallGroup2.None, flag);
			if (flag)
			{
				this.AddResources(partitionPlan.Resources, firewallGroupEnforcer);
				if (!partitionPlan.PartitionKey.Equals(this.rootPartitionKey))
				{
					firewallGroupEnforcer.AllowUpgrade = false;
				}
			}
			return new FirewallPartition(this, firewallGroupEnforcer, firewallFlowEnforcer, partitionPlan.PartitionKey, partitionPlan.IsCyclic);
		}

		// Token: 0x0600B6D8 RID: 46808 RVA: 0x00251A0C File Offset: 0x0024FC0C
		private void AddResources(IEnumerable<IResource> resources, FirewallGroupEnforcer groupEnforcer)
		{
			foreach (IResource resource in resources)
			{
				if (!groupEnforcer.TryAddResource(resource))
				{
					throw this.BuildRuleException(new IResource[] { resource });
				}
			}
		}

		// Token: 0x0600B6D9 RID: 46809 RVA: 0x00251A68 File Offset: 0x0024FC68
		private void BufferNextPartition(List<FirewallPartition> partitionsToBuffer, int nextPartition, Action<Exception> callback)
		{
			if (nextPartition == partitionsToBuffer.Count)
			{
				callback(null);
				return;
			}
			partitionsToBuffer[nextPartition].BeginBufferValue(delegate(Exception exception)
			{
				if (exception != null)
				{
					callback(exception);
					return;
				}
				this.BufferNextPartition(partitionsToBuffer, nextPartition + 1, callback);
			});
		}

		// Token: 0x0600B6DA RID: 46810 RVA: 0x00251ADC File Offset: 0x0024FCDC
		private IFunctionValue CompileFunction(string expression)
		{
			Action<IError> action = delegate(IError e)
			{
				throw new InvalidOperationException(e.Message);
			};
			ITokens tokens = this.engine.Tokenize(expression);
			IExpressionDocument expressionDocument = (IExpressionDocument)this.engine.Parse(tokens, new TextDocumentHost(expression), action);
			IRecordValue library = this.engine.GetLibrary(this.EngineHost, null);
			IModule module = this.engine.Compile(expressionDocument, library, CompileOptions.None, action);
			IAssembly assembly = this.engine.Assemble(new IModule[] { module }, library, this.EngineHost, action);
			return this.Engine.Invoke(assembly.Function, Array.Empty<IValue>()).AsFunction;
		}

		// Token: 0x0600B6DB RID: 46811 RVA: 0x00251B90 File Offset: 0x0024FD90
		private IValue AddStagingLocation(IValue value, IPartitionKey partitionKey)
		{
			IMemberLetPartitionKey memberLetPartitionKey = partitionKey as IMemberLetPartitionKey;
			if (value.IsTable && memberLetPartitionKey != null)
			{
				ITextValue textValue = this.engine.Text(memberLetPartitionKey.Member);
				IValue value2;
				if (memberLetPartitionKey.Lets.Count != 1)
				{
					value2 = this.engine.Null;
				}
				else
				{
					IValue value3 = this.engine.Text(memberLetPartitionKey.Lets[0]);
					value2 = value3;
				}
				IValue value4 = value2;
				value = this.engine.Invoke(this.AddExpressionFunction, new IValue[] { value, textValue, value4 });
			}
			return value;
		}

		// Token: 0x04005D41 RID: 23873
		private const string AddExpression = "(value, memberName, optional letName) => let\r\n            MakeNavigationStep = (collection, name) => if name = null then collection else [\r\n                Kind = \"FieldAccess\",\r\n                IsOptional = false,\r\n                Expression = [\r\n                    Kind = \"ElementAccess\",\r\n                    Collection = collection,\r\n                    Key = [Kind = \"Constant\", Value = [Name = name]],\r\n                    IsOptional = false\r\n                ],\r\n                MemberName = \"Data\"\r\n            ],\r\n            ModelStorage = [\r\n                Kind = \"Invocation\",\r\n                Function = [Kind = \"Identifier\", Name = \"Pipeline.DefaultModelStorage\", IsInclusive = false],\r\n                Arguments = {}\r\n            ]\r\n        in\r\n            if (value is table) then\r\n                Table.View(null, [\r\n                    GetType = () => Value.Type(value),\r\n                    GetRows = () => value,\r\n                    GetRowCount = () => Table.RowCount(value),\r\n                    GetExpression = () => MakeNavigationStep(MakeNavigationStep(ModelStorage, memberName), letName)\r\n                ])\r\n            else\r\n                value";

		// Token: 0x04005D42 RID: 23874
		private static readonly Func<IPartitionKey, string> defaultGetDisplayName = (IPartitionKey partitionKey) => partitionKey.ToString();

		// Token: 0x04005D43 RID: 23875
		private readonly IEngineHost engineHost;

		// Token: 0x04005D44 RID: 23876
		private readonly IEngine engine;

		// Token: 0x04005D45 RID: 23877
		private readonly Func<IEngineHost, IEngine, IDocumentEvaluator> evaluatorCtor;

		// Token: 0x04005D46 RID: 23878
		private readonly IEnumerable<DocumentEvaluationParameters> partitionParameters;

		// Token: 0x04005D47 RID: 23879
		private readonly IFirewallPlan firewallPlan;

		// Token: 0x04005D48 RID: 23880
		private readonly IPartitionKey rootPartitionKey;

		// Token: 0x04005D49 RID: 23881
		private readonly IFirewallRuleService firewallRuleService;

		// Token: 0x04005D4A RID: 23882
		private readonly IResourcePathService resourcePathService;

		// Token: 0x04005D4B RID: 23883
		private readonly Func<IPartitionKey, string> getDisplayName;

		// Token: 0x04005D4C RID: 23884
		private IValueBuffer valueBuffer;

		// Token: 0x04005D4D RID: 23885
		private Dictionary<IPartitionKey, FirewallPartition> partitions;

		// Token: 0x04005D4E RID: 23886
		private IFunctionValue addExpressionFunction;
	}
}
