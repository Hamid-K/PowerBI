using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CB9 RID: 7353
	internal class FirewallGroupEnforcer
	{
		// Token: 0x0600B72A RID: 46890 RVA: 0x00252AC4 File Offset: 0x00250CC4
		public FirewallGroupEnforcer(IFirewallRuleService firewallRuleService, IResourcePathService resourcePathService, Func<IPartitionKey, string> getDisplayName, IPartitionKey partitionKey, FirewallGroup2 firewallGroup, bool allowUpgrade)
		{
			this.syncRoot = new object();
			this.firewallRuleService = firewallRuleService;
			this.resourcePathService = resourcePathService;
			this.getDisplayName = getDisplayName;
			this.partitionKey = partitionKey;
			this.resourceGroups = new FirewallGroupEnforcer.ResourceGroups(this);
			this.firewallGroup = firewallGroup;
			this.allowUpgrade = allowUpgrade;
		}

		// Token: 0x17002D8F RID: 11663
		// (get) Token: 0x0600B72B RID: 46891 RVA: 0x00252B1B File Offset: 0x00250D1B
		public IPartitionKey PartitionKey
		{
			get
			{
				return this.partitionKey;
			}
		}

		// Token: 0x17002D90 RID: 11664
		// (get) Token: 0x0600B72C RID: 46892 RVA: 0x00252B23 File Offset: 0x00250D23
		// (set) Token: 0x0600B72D RID: 46893 RVA: 0x00252B2B File Offset: 0x00250D2B
		public bool AllowUpgrade
		{
			get
			{
				return this.allowUpgrade;
			}
			set
			{
				this.allowUpgrade = value;
			}
		}

		// Token: 0x17002D91 RID: 11665
		// (get) Token: 0x0600B72E RID: 46894 RVA: 0x00252B34 File Offset: 0x00250D34
		public FirewallGroup2 CurrentGroup
		{
			get
			{
				object obj = this.syncRoot;
				FirewallGroup2 firewallGroup;
				lock (obj)
				{
					firewallGroup = this.firewallGroup;
				}
				return firewallGroup;
			}
		}

		// Token: 0x17002D92 RID: 11666
		// (get) Token: 0x0600B72F RID: 46895 RVA: 0x00252B78 File Offset: 0x00250D78
		public IEnumerable<IResource> Resources
		{
			get
			{
				object obj = this.syncRoot;
				IEnumerable<IResource> enumerable;
				lock (obj)
				{
					enumerable = this.resourceGroups.Resources.ToArray<IResource>();
				}
				return enumerable;
			}
		}

		// Token: 0x0600B730 RID: 46896 RVA: 0x00252BC4 File Offset: 0x00250DC4
		public bool TryAddResource(IResource resource)
		{
			object obj = this.syncRoot;
			bool flag2;
			lock (obj)
			{
				using (FirewallGroupEnforcer.ResourceGroups.Mark mark = this.resourceGroups.NewMark())
				{
					FirewallGroup2 firewallGroup = this.firewallRuleService.CreateFirewallGroup(resource);
					FirewallGroup2 firewallGroup2;
					if (!this.resourceGroups.TryAdd(resource, firewallGroup))
					{
						flag2 = true;
					}
					else if (!this.resourceGroups.TryCombineGroups(this.partitionKey, out firewallGroup2))
					{
						flag2 = false;
					}
					else
					{
						this.SetGroupCore(firewallGroup2);
						mark.Commit();
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600B731 RID: 46897 RVA: 0x00252C74 File Offset: 0x00250E74
		public bool TryGetResource(IResource resource, out FirewallGroup2 group)
		{
			object obj = this.syncRoot;
			bool flag2;
			lock (obj)
			{
				flag2 = this.resourceGroups.TryGet(resource, out group);
			}
			return flag2;
		}

		// Token: 0x0600B732 RID: 46898 RVA: 0x00252CC0 File Offset: 0x00250EC0
		public bool TryUpdateResource(IResource resource, FirewallGroup2 group)
		{
			object obj = this.syncRoot;
			bool flag2;
			lock (obj)
			{
				using (FirewallGroupEnforcer.ResourceGroups.Mark mark = this.resourceGroups.NewMark())
				{
					FirewallGroup2 firewallGroup;
					if (!this.resourceGroups.TryUpdate(this.partitionKey, resource, group, out firewallGroup))
					{
						flag2 = false;
					}
					else
					{
						this.SetGroupCore(firewallGroup);
						mark.Commit();
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x0600B733 RID: 46899 RVA: 0x00252D50 File Offset: 0x00250F50
		private void SetGroupCore(FirewallGroup2 newGroup)
		{
			if (this.allowUpgrade)
			{
				this.firewallGroup = newGroup;
				return;
			}
			if (this.firewallGroup.IsSameAs(newGroup))
			{
				return;
			}
			if (this.firewallGroup.GroupType == FirewallGroupType2.None)
			{
				throw this.BuildFlowException(FirewallStrings.FirewallFlow_NoGroupsAllowed(this.getDisplayName(this.partitionKey)));
			}
			throw this.BuildFlowException(FirewallStrings.FirewallFlow_DeviationFromGroup(this.getDisplayName(this.partitionKey), this.firewallGroup, newGroup));
		}

		// Token: 0x0600B734 RID: 46900 RVA: 0x00252DC9 File Offset: 0x00250FC9
		private FirewallFlowException2 BuildFlowException(string message)
		{
			return new FirewallFlowException2(this.Resources.ToArray<IResource>(), message);
		}

		// Token: 0x04005D7B RID: 23931
		private readonly object syncRoot;

		// Token: 0x04005D7C RID: 23932
		private readonly IFirewallRuleService firewallRuleService;

		// Token: 0x04005D7D RID: 23933
		private readonly IResourcePathService resourcePathService;

		// Token: 0x04005D7E RID: 23934
		private readonly Func<IPartitionKey, string> getDisplayName;

		// Token: 0x04005D7F RID: 23935
		private readonly IPartitionKey partitionKey;

		// Token: 0x04005D80 RID: 23936
		private readonly FirewallGroupEnforcer.ResourceGroups resourceGroups;

		// Token: 0x04005D81 RID: 23937
		private FirewallGroup2 firewallGroup;

		// Token: 0x04005D82 RID: 23938
		private bool allowUpgrade;

		// Token: 0x02001CBA RID: 7354
		private class ResourceGroups
		{
			// Token: 0x0600B735 RID: 46901 RVA: 0x00252DDC File Offset: 0x00250FDC
			public ResourceGroups(FirewallGroupEnforcer firewallGroupEnforcer)
			{
				this.firewallGroupEnforcer = firewallGroupEnforcer;
				this.resourceGroups = new List<FirewallGroupEnforcer.ResourceGroups.ResourceGroup>();
			}

			// Token: 0x17002D93 RID: 11667
			// (get) Token: 0x0600B736 RID: 46902 RVA: 0x00252DF6 File Offset: 0x00250FF6
			public int Count
			{
				get
				{
					return this.resourceGroups.Count;
				}
			}

			// Token: 0x17002D94 RID: 11668
			// (get) Token: 0x0600B737 RID: 46903 RVA: 0x00252E03 File Offset: 0x00251003
			public IEnumerable<IResource> Resources
			{
				get
				{
					foreach (FirewallGroupEnforcer.ResourceGroups.ResourceGroup resourceGroup in this.resourceGroups)
					{
						if (resourceGroup.Resource != null)
						{
							yield return resourceGroup.Resource;
						}
					}
					List<FirewallGroupEnforcer.ResourceGroups.ResourceGroup>.Enumerator enumerator = default(List<FirewallGroupEnforcer.ResourceGroups.ResourceGroup>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x17002D95 RID: 11669
			// (get) Token: 0x0600B738 RID: 46904 RVA: 0x00252E13 File Offset: 0x00251013
			public IEnumerable<FirewallGroup2> FirewallGroups
			{
				get
				{
					foreach (FirewallGroupEnforcer.ResourceGroups.ResourceGroup resourceGroup in this.resourceGroups)
					{
						yield return resourceGroup.FirewallGroup;
					}
					List<FirewallGroupEnforcer.ResourceGroups.ResourceGroup>.Enumerator enumerator = default(List<FirewallGroupEnforcer.ResourceGroups.ResourceGroup>.Enumerator);
					yield break;
					yield break;
				}
			}

			// Token: 0x0600B739 RID: 46905 RVA: 0x00252E23 File Offset: 0x00251023
			public FirewallGroupEnforcer.ResourceGroups.Mark NewMark()
			{
				return new FirewallGroupEnforcer.ResourceGroups.Mark(this);
			}

			// Token: 0x0600B73A RID: 46906 RVA: 0x00252E2C File Offset: 0x0025102C
			public bool TryAdd(IResource resource, FirewallGroup2 firewallGroup)
			{
				foreach (FirewallGroupEnforcer.ResourceGroups.ResourceGroup resourceGroup in this.resourceGroups)
				{
					if (resourceGroup.Resource != null && this.firewallGroupEnforcer.resourcePathService.StartsWith(resourceGroup.Resource, resource))
					{
						return false;
					}
				}
				for (int i = this.resourceGroups.Count - 1; i >= 0; i--)
				{
					FirewallGroupEnforcer.ResourceGroups.ResourceGroup resourceGroup2 = this.resourceGroups[i];
					if (resourceGroup2.Resource != null && this.firewallGroupEnforcer.resourcePathService.StartsWith(resource, resourceGroup2.Resource))
					{
						this.resourceGroups.RemoveAt(i);
					}
				}
				this.resourceGroups.Add(new FirewallGroupEnforcer.ResourceGroups.ResourceGroup(firewallGroup, resource));
				return true;
			}

			// Token: 0x0600B73B RID: 46907 RVA: 0x00252F0C File Offset: 0x0025110C
			public bool TryGet(IResource resource, out FirewallGroup2 group)
			{
				for (int i = 0; i < this.resourceGroups.Count; i++)
				{
					IResource resource2 = this.resourceGroups[i].Resource;
					if (resource2 != null && resource2.Kind == resource.Kind && resource2.Path == resource.Path)
					{
						group = this.resourceGroups[i].FirewallGroup;
						return true;
					}
				}
				group = null;
				return false;
			}

			// Token: 0x0600B73C RID: 46908 RVA: 0x00252F84 File Offset: 0x00251184
			public bool TryUpdate(IPartitionKey partitionKey, IResource resource, FirewallGroup2 firewallGroup, out FirewallGroup2 newGroup)
			{
				for (int i = 0; i < this.resourceGroups.Count; i++)
				{
					IResource resource2 = this.resourceGroups[i].Resource;
					if (resource2 != null && resource2.Kind == resource.Kind && resource2.Path == resource.Path)
					{
						this.resourceGroups[i] = new FirewallGroupEnforcer.ResourceGroups.ResourceGroup(firewallGroup, resource);
						return this.TryCombineGroups(partitionKey, out newGroup);
					}
				}
				newGroup = null;
				return false;
			}

			// Token: 0x0600B73D RID: 46909 RVA: 0x00253004 File Offset: 0x00251204
			public bool TryCombineGroups(IPartitionKey partitionKey, out FirewallGroup2 firewallGroup)
			{
				if (this.Empty())
				{
					firewallGroup = FirewallGroup2.None;
					return true;
				}
				if (this.One())
				{
					firewallGroup = this.GetOne();
					return true;
				}
				if (this.AllSame())
				{
					firewallGroup = this.GetSame();
					return true;
				}
				if (!this.AnyUnclassified())
				{
					throw this.firewallGroupEnforcer.BuildFlowException(FirewallStrings.FirewallFlow_CantCreateGroup(this.firewallGroupEnforcer.getDisplayName(partitionKey)));
				}
				if (this.AllTrusted())
				{
					firewallGroup = new FirewallGroup2(FirewallGroupType2.MultipleUnclassified, true);
					return true;
				}
				firewallGroup = null;
				return false;
			}

			// Token: 0x0600B73E RID: 46910 RVA: 0x00253087 File Offset: 0x00251287
			private FirewallGroup2 GetOne()
			{
				if (this.Count != 1)
				{
					throw new InvalidOperationException();
				}
				return this.FirewallGroups.First<FirewallGroup2>();
			}

			// Token: 0x0600B73F RID: 46911 RVA: 0x002530A3 File Offset: 0x002512A3
			private bool Empty()
			{
				return this.Count == 0;
			}

			// Token: 0x0600B740 RID: 46912 RVA: 0x002530AE File Offset: 0x002512AE
			private bool One()
			{
				return this.Count == 1;
			}

			// Token: 0x0600B741 RID: 46913 RVA: 0x002530B9 File Offset: 0x002512B9
			private bool AnyUnclassified()
			{
				return this.Any(FirewallGroupType2.MultipleUnclassified) || this.Any(FirewallGroupType2.SingleUnclassified);
			}

			// Token: 0x0600B742 RID: 46914 RVA: 0x002530D0 File Offset: 0x002512D0
			private bool AllSame()
			{
				if (this.Empty())
				{
					throw new InvalidOperationException();
				}
				FirewallGroup2 firewallGroup = null;
				foreach (FirewallGroup2 firewallGroup2 in this.FirewallGroups)
				{
					if (firewallGroup == null)
					{
						firewallGroup = firewallGroup2;
					}
					else if (!firewallGroup.IsSameAs(firewallGroup2))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600B743 RID: 46915 RVA: 0x00253140 File Offset: 0x00251340
			private bool Any(FirewallGroupType2 groupType)
			{
				using (IEnumerator<FirewallGroup2> enumerator = this.FirewallGroups.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.GroupType == groupType)
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x0600B744 RID: 46916 RVA: 0x00253194 File Offset: 0x00251394
			private FirewallGroup2 GetSame()
			{
				if (this.Empty())
				{
					throw new InvalidOperationException();
				}
				if (!this.AllSame())
				{
					throw new InvalidOperationException();
				}
				FirewallGroup2 firewallGroup = this.FirewallGroups.First<FirewallGroup2>();
				bool flag = this.AllTrusted();
				return new FirewallGroup2(firewallGroup.GroupType, flag, firewallGroup.Resource);
			}

			// Token: 0x0600B745 RID: 46917 RVA: 0x002531E4 File Offset: 0x002513E4
			private bool AllTrusted()
			{
				using (IEnumerator<FirewallGroup2> enumerator = this.FirewallGroups.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsTrusted)
						{
							return false;
						}
					}
				}
				return true;
			}

			// Token: 0x04005D83 RID: 23939
			private readonly FirewallGroupEnforcer firewallGroupEnforcer;

			// Token: 0x04005D84 RID: 23940
			private List<FirewallGroupEnforcer.ResourceGroups.ResourceGroup> resourceGroups;

			// Token: 0x02001CBB RID: 7355
			public struct Mark : IDisposable
			{
				// Token: 0x0600B746 RID: 46918 RVA: 0x00253238 File Offset: 0x00251438
				public Mark(FirewallGroupEnforcer.ResourceGroups resourceGroups)
				{
					this.resourceGroups = resourceGroups;
					this.list = new List<FirewallGroupEnforcer.ResourceGroups.ResourceGroup>(this.resourceGroups.resourceGroups);
				}

				// Token: 0x0600B747 RID: 46919 RVA: 0x00253257 File Offset: 0x00251457
				public void Commit()
				{
					this.resourceGroups = null;
					this.list = null;
				}

				// Token: 0x0600B748 RID: 46920 RVA: 0x00253267 File Offset: 0x00251467
				public void Dispose()
				{
					if (this.resourceGroups != null)
					{
						this.resourceGroups.resourceGroups = this.list;
						this.resourceGroups = null;
						this.list = null;
					}
				}

				// Token: 0x04005D85 RID: 23941
				private FirewallGroupEnforcer.ResourceGroups resourceGroups;

				// Token: 0x04005D86 RID: 23942
				private List<FirewallGroupEnforcer.ResourceGroups.ResourceGroup> list;
			}

			// Token: 0x02001CBC RID: 7356
			private class ResourceGroup
			{
				// Token: 0x0600B749 RID: 46921 RVA: 0x00253290 File Offset: 0x00251490
				public ResourceGroup(FirewallGroup2 firewallGroup)
					: this(firewallGroup, null)
				{
				}

				// Token: 0x0600B74A RID: 46922 RVA: 0x0025329A File Offset: 0x0025149A
				public ResourceGroup(FirewallGroup2 firewallGroup, IResource resource)
				{
					this.firewallGroup = firewallGroup;
					this.resource = resource;
				}

				// Token: 0x17002D96 RID: 11670
				// (get) Token: 0x0600B74B RID: 46923 RVA: 0x002532B0 File Offset: 0x002514B0
				public IResource Resource
				{
					get
					{
						return this.resource;
					}
				}

				// Token: 0x17002D97 RID: 11671
				// (get) Token: 0x0600B74C RID: 46924 RVA: 0x002532B8 File Offset: 0x002514B8
				public FirewallGroup2 FirewallGroup
				{
					get
					{
						return this.firewallGroup;
					}
				}

				// Token: 0x04005D87 RID: 23943
				private readonly FirewallGroup2 firewallGroup;

				// Token: 0x04005D88 RID: 23944
				private readonly IResource resource;
			}
		}
	}
}
