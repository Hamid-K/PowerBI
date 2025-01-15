using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CB5 RID: 7349
	internal class FirewallFlowEnforcer
	{
		// Token: 0x0600B6F9 RID: 46841 RVA: 0x002522DF File Offset: 0x002504DF
		public FirewallFlowEnforcer()
		{
			this.flows = new Dictionary<IPartitionKey, FirewallFlowEnforcer.Flow>(PartitionKeyEqualityComparer.Instance);
		}

		// Token: 0x0600B6FA RID: 46842 RVA: 0x002522F7 File Offset: 0x002504F7
		public IEnumerable<IPartitionKey> GetFlows()
		{
			return this.flows.Keys;
		}

		// Token: 0x0600B6FB RID: 46843 RVA: 0x00252304 File Offset: 0x00250504
		public IEnumerable<IPartitionKey> GetFlows(FirewallFlow firewallFlow)
		{
			foreach (KeyValuePair<IPartitionKey, FirewallFlowEnforcer.Flow> keyValuePair in this.flows)
			{
				if (keyValuePair.Value.FirewallFlow == firewallFlow)
				{
					yield return keyValuePair.Key;
				}
			}
			Dictionary<IPartitionKey, FirewallFlowEnforcer.Flow>.Enumerator enumerator = default(Dictionary<IPartitionKey, FirewallFlowEnforcer.Flow>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x17002D87 RID: 11655
		// (get) Token: 0x0600B6FC RID: 46844 RVA: 0x0025231B File Offset: 0x0025051B
		public bool HasFlows
		{
			get
			{
				return this.flows.Count > 0;
			}
		}

		// Token: 0x17002D88 RID: 11656
		// (get) Token: 0x0600B6FD RID: 46845 RVA: 0x0025232C File Offset: 0x0025052C
		public bool IsComplete
		{
			get
			{
				this.VerifyInitialized();
				using (Dictionary<IPartitionKey, FirewallFlowEnforcer.Flow>.ValueCollection.Enumerator enumerator = this.flows.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.FirewallFlow == FirewallFlow.Blocked)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		// Token: 0x17002D89 RID: 11657
		// (get) Token: 0x0600B6FE RID: 46846 RVA: 0x00252394 File Offset: 0x00250594
		public FirewallGroup2 OutflowGroup
		{
			get
			{
				this.VerifyInitialized();
				return this.outflowGroup;
			}
		}

		// Token: 0x0600B6FF RID: 46847 RVA: 0x002523A2 File Offset: 0x002505A2
		public void AddFlow(IPartitionKey partitionKey, FirewallGroup2 firewallGroup)
		{
			if (this.initialized)
			{
				throw new InvalidOperationException();
			}
			this.flows.Add(partitionKey, new FirewallFlowEnforcer.Flow(firewallGroup));
		}

		// Token: 0x0600B700 RID: 46848 RVA: 0x002523C4 File Offset: 0x002505C4
		public void InitializeFlows()
		{
			if (this.initialized)
			{
				throw new InvalidOperationException();
			}
			FirewallFlowEnforcer.FlowBuilder flowBuilder = new FirewallFlowEnforcer.FlowBuilder(this.flows.Values);
			this.outflowGroup = flowBuilder.OutflowGroup;
			this.initialized = true;
		}

		// Token: 0x0600B701 RID: 46849 RVA: 0x00252404 File Offset: 0x00250604
		public bool TryGetFlow(IPartitionKey partitionKey, out FirewallFlow firewallFlow)
		{
			this.VerifyInitialized();
			FirewallFlowEnforcer.Flow flow;
			if (this.flows.TryGetValue(partitionKey, out flow))
			{
				firewallFlow = flow.FirewallFlow;
				return true;
			}
			firewallFlow = FirewallFlow.Unknown;
			return false;
		}

		// Token: 0x0600B702 RID: 46850 RVA: 0x00252435 File Offset: 0x00250635
		private void VerifyInitialized()
		{
			if (!this.initialized)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04005D6D RID: 23917
		private readonly Dictionary<IPartitionKey, FirewallFlowEnforcer.Flow> flows;

		// Token: 0x04005D6E RID: 23918
		private FirewallGroup2 outflowGroup;

		// Token: 0x04005D6F RID: 23919
		private bool initialized;

		// Token: 0x02001CB6 RID: 7350
		private class FlowBuilder
		{
			// Token: 0x0600B703 RID: 46851 RVA: 0x00252445 File Offset: 0x00250645
			public FlowBuilder(IEnumerable<FirewallFlowEnforcer.Flow> inflows)
			{
				this.outflow = this.AssignFlows(inflows);
			}

			// Token: 0x17002D8A RID: 11658
			// (get) Token: 0x0600B704 RID: 46852 RVA: 0x0025245A File Offset: 0x0025065A
			public FirewallGroup2 OutflowGroup
			{
				get
				{
					return this.outflow;
				}
			}

			// Token: 0x0600B705 RID: 46853 RVA: 0x00252464 File Offset: 0x00250664
			private FirewallGroup2 AssignFlows(IEnumerable<FirewallFlowEnforcer.Flow> inflows)
			{
				this.remainingInflows = new List<FirewallFlowEnforcer.Flow>(inflows);
				FirewallGroup2 firewallGroup = this.AssignFlowsCore();
				if (this.remainingInflows.Count != 0)
				{
					throw new InvalidOperationException();
				}
				using (IEnumerator<FirewallFlowEnforcer.Flow> enumerator = inflows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.FirewallFlow == FirewallFlow.Unknown)
						{
							throw new InvalidOperationException();
						}
					}
				}
				return firewallGroup;
			}

			// Token: 0x0600B706 RID: 46854 RVA: 0x002524D8 File Offset: 0x002506D8
			private FirewallGroup2 AssignFlowsCore()
			{
				if (this.Empty())
				{
					return FirewallGroup2.None;
				}
				if (this.AllSame())
				{
					FirewallGroup2 same = this.GetSame();
					this.Allow();
					return same;
				}
				this.AllowNone();
				if (this.AllSame())
				{
					FirewallGroup2 same2 = this.GetSame();
					this.Allow();
					return same2;
				}
				if (this.AnyUnclassified())
				{
					if (this.AllTrusted())
					{
						FirewallGroup2 firewallGroup = new FirewallGroup2(FirewallGroupType2.MultipleUnclassified, true);
						this.Allow();
						return firewallGroup;
					}
					this.Block();
					return new FirewallGroup2(FirewallGroupType2.MultipleUnclassified, false);
				}
				else
				{
					this.LimitPublic();
					if (this.AllSame())
					{
						FirewallGroup2 same3 = this.GetSame();
						this.Allow();
						return same3;
					}
					this.LimitOrg();
					this.LimitNamedPrivate();
					this.LimitSeparatePrivate();
					this.AllowCombinedPrivate();
					return new FirewallGroup2(FirewallGroupType2.CombinedPrivate, true);
				}
			}

			// Token: 0x0600B707 RID: 46855 RVA: 0x00252589 File Offset: 0x00250789
			private void LimitPublic()
			{
				this.Limit(FirewallGroupType2.Public);
			}

			// Token: 0x0600B708 RID: 46856 RVA: 0x00252592 File Offset: 0x00250792
			private void LimitSeparatePrivate()
			{
				this.Limit(FirewallGroupType2.SeparatePrivate);
			}

			// Token: 0x0600B709 RID: 46857 RVA: 0x0025259B File Offset: 0x0025079B
			private void LimitNamedPrivate()
			{
				this.Limit(FirewallGroupType2.Named);
			}

			// Token: 0x0600B70A RID: 46858 RVA: 0x002525A4 File Offset: 0x002507A4
			private void LimitOrg()
			{
				this.Limit(FirewallGroupType2.Organizational);
			}

			// Token: 0x0600B70B RID: 46859 RVA: 0x002525AD File Offset: 0x002507AD
			private void Limit(FirewallGroupType2 groupType)
			{
				this.SetFlowForGroupType(groupType, FirewallFlow.Limited);
			}

			// Token: 0x0600B70C RID: 46860 RVA: 0x002525B7 File Offset: 0x002507B7
			private void Block()
			{
				this.SetFlowForAll(FirewallFlow.Blocked);
			}

			// Token: 0x0600B70D RID: 46861 RVA: 0x002525C0 File Offset: 0x002507C0
			private void Allow()
			{
				this.SetFlowForAll(FirewallFlow.Allowed);
			}

			// Token: 0x0600B70E RID: 46862 RVA: 0x002525C9 File Offset: 0x002507C9
			private void AllowCombinedPrivate()
			{
				this.SetFlowForGroupType(FirewallGroupType2.CombinedPrivate, FirewallFlow.Allowed);
			}

			// Token: 0x0600B70F RID: 46863 RVA: 0x002525D3 File Offset: 0x002507D3
			private void AllowNone()
			{
				this.SetFlowForGroupType(FirewallGroupType2.None, FirewallFlow.Allowed);
			}

			// Token: 0x0600B710 RID: 46864 RVA: 0x002525DD File Offset: 0x002507DD
			private void AllowTrusted()
			{
				this.SetFlowForTrusted(true, FirewallFlow.Allowed);
			}

			// Token: 0x0600B711 RID: 46865 RVA: 0x002525E7 File Offset: 0x002507E7
			private bool AllSeparatePrivate()
			{
				return this.All(FirewallGroupType2.SeparatePrivate);
			}

			// Token: 0x0600B712 RID: 46866 RVA: 0x002525F0 File Offset: 0x002507F0
			private bool AllTrusted()
			{
				using (List<FirewallFlowEnforcer.Flow>.Enumerator enumerator = this.remainingInflows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.FirewallGroup.IsTrusted)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x0600B713 RID: 46867 RVA: 0x00252650 File Offset: 0x00250850
			private bool AnyUnclassified()
			{
				return this.Any(FirewallGroupType2.MultipleUnclassified) || this.Any(FirewallGroupType2.SingleUnclassified);
			}

			// Token: 0x0600B714 RID: 46868 RVA: 0x00252664 File Offset: 0x00250864
			private bool Empty()
			{
				return this.remainingInflows.Count == 0;
			}

			// Token: 0x0600B715 RID: 46869 RVA: 0x00252674 File Offset: 0x00250874
			private bool AllSame()
			{
				if (this.Empty())
				{
					throw new InvalidOperationException();
				}
				FirewallGroup2 firewallGroup = this.remainingInflows[0].FirewallGroup;
				for (int i = 1; i < this.remainingInflows.Count; i++)
				{
					FirewallGroup2 firewallGroup2 = this.remainingInflows[i].FirewallGroup;
					if (!firewallGroup.IsSameAs(firewallGroup2))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600B716 RID: 46870 RVA: 0x002526D8 File Offset: 0x002508D8
			private bool All(FirewallGroupType2 groupType)
			{
				using (List<FirewallFlowEnforcer.Flow>.Enumerator enumerator = this.remainingInflows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.FirewallGroup.GroupType != groupType)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x0600B717 RID: 46871 RVA: 0x00252738 File Offset: 0x00250938
			private bool Any(FirewallGroupType2 groupType)
			{
				using (List<FirewallFlowEnforcer.Flow>.Enumerator enumerator = this.remainingInflows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.FirewallGroup.GroupType == groupType)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x0600B718 RID: 46872 RVA: 0x00252798 File Offset: 0x00250998
			private void SetFlowForTrusted(bool isTrusted, FirewallFlow firewallFlow)
			{
				for (int i = this.remainingInflows.Count - 1; i >= 0; i--)
				{
					FirewallFlowEnforcer.Flow flow = this.remainingInflows[i];
					if (flow.FirewallGroup.IsTrusted == isTrusted)
					{
						this.remainingInflows.RemoveAt(i);
						this.SetFlowForOne(flow, firewallFlow);
					}
				}
			}

			// Token: 0x0600B719 RID: 46873 RVA: 0x002527EC File Offset: 0x002509EC
			private void SetFlowForGroupType(FirewallGroupType2 groupType, FirewallFlow firewallFlow)
			{
				for (int i = this.remainingInflows.Count - 1; i >= 0; i--)
				{
					FirewallFlowEnforcer.Flow flow = this.remainingInflows[i];
					if (flow.FirewallGroup.GroupType == groupType)
					{
						this.remainingInflows.RemoveAt(i);
						this.SetFlowForOne(flow, firewallFlow);
					}
				}
			}

			// Token: 0x0600B71A RID: 46874 RVA: 0x00252840 File Offset: 0x00250A40
			private void SetFlowForAll(FirewallFlow firewallFlow)
			{
				for (int i = this.remainingInflows.Count - 1; i >= 0; i--)
				{
					FirewallFlowEnforcer.Flow flow = this.remainingInflows[i];
					this.remainingInflows.RemoveAt(i);
					this.SetFlowForOne(flow, firewallFlow);
				}
			}

			// Token: 0x0600B71B RID: 46875 RVA: 0x00252886 File Offset: 0x00250A86
			private void SetFlowForOne(FirewallFlowEnforcer.Flow flow, FirewallFlow firewallFlow)
			{
				if (flow.FirewallGroup.IsTrusted && firewallFlow == FirewallFlow.Limited)
				{
					firewallFlow = FirewallFlow.Allowed;
				}
				flow.FirewallFlow = firewallFlow;
			}

			// Token: 0x0600B71C RID: 46876 RVA: 0x002528A4 File Offset: 0x00250AA4
			public FirewallGroup2 GetSame()
			{
				if (this.Empty())
				{
					throw new InvalidOperationException();
				}
				if (!this.AllSame())
				{
					throw new InvalidOperationException();
				}
				FirewallGroup2 firewallGroup = this.remainingInflows[0].FirewallGroup;
				bool flag = this.AllTrusted();
				return new FirewallGroup2(firewallGroup.GroupType, flag, firewallGroup.Resource, firewallGroup.GroupName);
			}

			// Token: 0x04005D70 RID: 23920
			private List<FirewallFlowEnforcer.Flow> remainingInflows;

			// Token: 0x04005D71 RID: 23921
			private FirewallGroup2 outflow;
		}

		// Token: 0x02001CB7 RID: 7351
		private class Flow
		{
			// Token: 0x0600B71D RID: 46877 RVA: 0x002528FE File Offset: 0x00250AFE
			public Flow(FirewallGroup2 firewallGroup)
			{
				this.firewallGroup = firewallGroup;
				this.firewallFlow = FirewallFlow.Unknown;
			}

			// Token: 0x17002D8B RID: 11659
			// (get) Token: 0x0600B71E RID: 46878 RVA: 0x00252914 File Offset: 0x00250B14
			public FirewallGroup2 FirewallGroup
			{
				get
				{
					return this.firewallGroup;
				}
			}

			// Token: 0x17002D8C RID: 11660
			// (get) Token: 0x0600B71F RID: 46879 RVA: 0x0025291C File Offset: 0x00250B1C
			// (set) Token: 0x0600B720 RID: 46880 RVA: 0x00252924 File Offset: 0x00250B24
			public FirewallFlow FirewallFlow
			{
				get
				{
					return this.firewallFlow;
				}
				set
				{
					this.firewallFlow = value;
				}
			}

			// Token: 0x04005D72 RID: 23922
			private readonly FirewallGroup2 firewallGroup;

			// Token: 0x04005D73 RID: 23923
			private FirewallFlow firewallFlow;
		}
	}
}
