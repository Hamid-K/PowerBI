using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x02000047 RID: 71
	[Serializable]
	internal class MemoryManager : IMemoryLimit, IMemoryUsage
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000131EB File Offset: 0x000113EB
		public int Count
		{
			get
			{
				return this.m_managedObjects.Count;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000131F8 File Offset: 0x000113F8
		public void Add<T>(double weight, T memoryObject) where T : IMemoryLimit, IMemoryUsage
		{
			this.m_managedObjects.Add(new MemoryManager.MemoryObject(weight, memoryObject));
			this.m_totalWeight += weight;
			this.UpdateMemoryLimits();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00013228 File Offset: 0x00011428
		public void Remove<T>(T memoryObject) where T : IMemoryLimit, IMemoryUsage
		{
			foreach (MemoryManager.MemoryObject memoryObject2 in this.m_managedObjects)
			{
				if (memoryObject2.m_object == memoryObject)
				{
					this.m_managedObjects.Remove(memoryObject2);
					this.m_totalWeight -= memoryObject2.Weight;
					if (this.Count == 0)
					{
						this.m_totalWeight = 0.0;
					}
					this.UpdateMemoryLimits();
					break;
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000132C4 File Offset: 0x000114C4
		protected void UpdateMemoryLimits()
		{
			foreach (MemoryManager.MemoryObject memoryObject in this.m_managedObjects)
			{
				memoryObject.MemoryLimit = (long)((double)this.MemoryLimit * (memoryObject.Weight / this.m_totalWeight));
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0001332C File Offset: 0x0001152C
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00013334 File Offset: 0x00011534
		public long MemoryLimit
		{
			get
			{
				return this.m_memLimit;
			}
			set
			{
				if (value >= 0L)
				{
					this.m_memLimit = value;
				}
				else
				{
					MemoryManager.MemoryLimitBehavior memoryLimitBehavior = (MemoryManager.MemoryLimitBehavior)value;
					if (memoryLimitBehavior != MemoryManager.MemoryLimitBehavior.DynamicPhysical)
					{
						if (memoryLimitBehavior != MemoryManager.MemoryLimitBehavior.UnlimitedVirtual)
						{
							this.m_memLimit = 1152921504606846975L;
						}
						else
						{
							this.m_memLimit = 1152921504606846975L;
						}
					}
					else
					{
						this.m_memLimit = 1152921504606846975L;
					}
				}
				this.UpdateMemoryLimits();
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00013394 File Offset: 0x00011594
		public long MemoryUsage
		{
			get
			{
				long num = 0L;
				foreach (MemoryManager.MemoryObject memoryObject in this.m_managedObjects)
				{
					num += memoryObject.MemoryUsage;
				}
				return num;
			}
		}

		// Token: 0x04000060 RID: 96
		public const long MaxMemoryPerProcess = 1152921504606846975L;

		// Token: 0x04000061 RID: 97
		private double m_totalWeight;

		// Token: 0x04000062 RID: 98
		private long m_memLimit = 1152921504606846975L;

		// Token: 0x04000063 RID: 99
		private List<MemoryManager.MemoryObject> m_managedObjects = new List<MemoryManager.MemoryObject>();

		// Token: 0x020000D4 RID: 212
		public enum MemoryLimitBehavior
		{
			// Token: 0x04000205 RID: 517
			ZeroMemoryUsage,
			// Token: 0x04000206 RID: 518
			UnlimitedVirtual = -1,
			// Token: 0x04000207 RID: 519
			DynamicPhysical = -2
		}

		// Token: 0x020000D5 RID: 213
		[Serializable]
		protected class MemoryObject : IMemoryLimit, IMemoryUsage
		{
			// Token: 0x060008A3 RID: 2211 RVA: 0x0002C374 File Offset: 0x0002A574
			public MemoryObject(double weight, object obj)
			{
				this.Weight = weight;
				this.m_object = obj;
				this.m_memLimit = obj as IMemoryLimit;
				this.m_memUsage = obj as IMemoryUsage;
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0002C3A2 File Offset: 0x0002A5A2
			// (set) Token: 0x060008A5 RID: 2213 RVA: 0x0002C3AF File Offset: 0x0002A5AF
			public long MemoryLimit
			{
				get
				{
					return this.m_memLimit.MemoryLimit;
				}
				set
				{
					this.m_memLimit.MemoryLimit = value;
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0002C3BD File Offset: 0x0002A5BD
			public long MemoryUsage
			{
				get
				{
					return this.m_memUsage.MemoryUsage;
				}
			}

			// Token: 0x04000208 RID: 520
			public double Weight;

			// Token: 0x04000209 RID: 521
			private IMemoryLimit m_memLimit;

			// Token: 0x0400020A RID: 522
			private IMemoryUsage m_memUsage;

			// Token: 0x0400020B RID: 523
			public object m_object;
		}
	}
}
