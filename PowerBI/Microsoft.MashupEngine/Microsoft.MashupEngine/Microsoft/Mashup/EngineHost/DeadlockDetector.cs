using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200195D RID: 6493
	public class DeadlockDetector<P, R>
	{
		// Token: 0x0600A497 RID: 42135 RVA: 0x002214BA File Offset: 0x0021F6BA
		public DeadlockDetector()
		{
			this.available = new Dictionary<R, int>();
			this.active = new Dictionary<P, Dictionary<R, int>>();
			this.pending = new Dictionary<P, Dictionary<R, int>>();
		}

		// Token: 0x0600A498 RID: 42136 RVA: 0x002214E4 File Offset: 0x0021F6E4
		private DeadlockDetector(DeadlockDetector<P, R> detector)
		{
			this.available = new Dictionary<R, int>(detector.available);
			this.active = new Dictionary<P, Dictionary<R, int>>(detector.active.Count);
			foreach (KeyValuePair<P, Dictionary<R, int>> keyValuePair in detector.active)
			{
				this.active.Add(keyValuePair.Key, new Dictionary<R, int>(keyValuePair.Value));
			}
			this.pending = new Dictionary<P, Dictionary<R, int>>(detector.pending.Count);
			foreach (KeyValuePair<P, Dictionary<R, int>> keyValuePair2 in detector.pending)
			{
				this.pending.Add(keyValuePair2.Key, new Dictionary<R, int>(keyValuePair2.Value));
			}
		}

		// Token: 0x0600A499 RID: 42137 RVA: 0x002215EC File Offset: 0x0021F7EC
		public void SetAvailability(R resource, int? count)
		{
			if (count != null)
			{
				this.available[resource] = count.Value;
				return;
			}
			this.available.Remove(resource);
		}

		// Token: 0x0600A49A RID: 42138 RVA: 0x00221618 File Offset: 0x0021F818
		public void Requests(P process, R resource, int count = 1)
		{
			DeadlockDetector<P, R>.RequiresPositive(count);
			DeadlockDetector<P, R>.AddOne(this.pending, process, resource, count);
		}

		// Token: 0x0600A49B RID: 42139 RVA: 0x00221630 File Offset: 0x0021F830
		public void Cancels(P process)
		{
			foreach (KeyValuePair<R, int> keyValuePair in this.GetPending(process))
			{
				this.Cancels(process, keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<R, int> keyValuePair2 in this.GetActive(process))
			{
				this.Releases(process, keyValuePair2.Key, keyValuePair2.Value);
			}
		}

		// Token: 0x0600A49C RID: 42140 RVA: 0x002216D8 File Offset: 0x0021F8D8
		public void Cancels(P process, R resource, int count = 1)
		{
			DeadlockDetector<P, R>.RequiresPositive(count);
			DeadlockDetector<P, R>.RemoveOne(this.pending, process, resource, count);
		}

		// Token: 0x0600A49D RID: 42141 RVA: 0x002216EE File Offset: 0x0021F8EE
		public void Gets(P process, R resource, int count = 1)
		{
			DeadlockDetector<P, R>.RequiresPositive(count);
			DeadlockDetector<P, R>.RemoveOne(this.pending, process, resource, count);
			this.Acquires(process, resource, count);
		}

		// Token: 0x0600A49E RID: 42142 RVA: 0x00221710 File Offset: 0x0021F910
		public void Acquires(P process, R resource, int count = 1)
		{
			DeadlockDetector<P, R>.RequiresPositive(count);
			int num;
			if (this.available.TryGetValue(resource, out num))
			{
				this.available[resource] = num - count;
			}
			DeadlockDetector<P, R>.AddOne(this.active, process, resource, count);
		}

		// Token: 0x0600A49F RID: 42143 RVA: 0x00221750 File Offset: 0x0021F950
		public void Releases(P process, R resource, int count = 1)
		{
			DeadlockDetector<P, R>.RequiresPositive(count);
			int num;
			if (this.available.TryGetValue(resource, out num))
			{
				this.available[resource] = num + count;
			}
			DeadlockDetector<P, R>.RemoveOne(this.active, process, resource, count);
		}

		// Token: 0x0600A4A0 RID: 42144 RVA: 0x00221790 File Offset: 0x0021F990
		public IEnumerable<KeyValuePair<R, int>> GetActive(P process)
		{
			Dictionary<R, int> dictionary;
			if (this.active.TryGetValue(process, out dictionary))
			{
				return dictionary.ToArray<KeyValuePair<R, int>>();
			}
			return Enumerable.Empty<KeyValuePair<R, int>>();
		}

		// Token: 0x0600A4A1 RID: 42145 RVA: 0x002217BC File Offset: 0x0021F9BC
		public IEnumerable<KeyValuePair<R, int>> GetPending(P process)
		{
			Dictionary<R, int> dictionary;
			if (this.pending.TryGetValue(process, out dictionary))
			{
				return dictionary.ToArray<KeyValuePair<R, int>>();
			}
			return Enumerable.Empty<KeyValuePair<R, int>>();
		}

		// Token: 0x0600A4A2 RID: 42146 RVA: 0x002217E8 File Offset: 0x0021F9E8
		public bool IsDeadlocked()
		{
			HashSet<P> hashSet = new HashSet<P>(this.pending.Keys.Union(this.active.Keys));
			Dictionary<R, int> available = new Dictionary<R, int>(this.available);
			Func<KeyValuePair<R, int>, bool> <>9__0;
			Func<KeyValuePair<R, int>, bool> <>9__1;
			for (;;)
			{
				bool flag = false;
				P p = default(P);
				foreach (P p2 in hashSet)
				{
					IEnumerable<KeyValuePair<R, int>> dictionaryOrEmpty = DeadlockDetector<P, R>.GetDictionaryOrEmpty(this.pending, p2);
					Func<KeyValuePair<R, int>, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (KeyValuePair<R, int> resource) => available.ContainsKey(resource.Key));
					}
					IEnumerable<KeyValuePair<R, int>> enumerable = dictionaryOrEmpty.Where(func);
					Func<KeyValuePair<R, int>, bool> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (KeyValuePair<R, int> resource) => resource.Value <= available[resource.Key]);
					}
					if (enumerable.All(func2))
					{
						flag = true;
						p = p2;
						break;
					}
				}
				if (!flag)
				{
					break;
				}
				hashSet.Remove(p);
				foreach (KeyValuePair<R, int> keyValuePair in DeadlockDetector<P, R>.GetDictionaryOrEmpty(this.active, p))
				{
					if (available.ContainsKey(keyValuePair.Key))
					{
						Dictionary<R, int> dictionary = available;
						R key = keyValuePair.Key;
						dictionary[key] += keyValuePair.Value;
					}
				}
			}
			return hashSet.Any<P>();
		}

		// Token: 0x0600A4A3 RID: 42147 RVA: 0x00221984 File Offset: 0x0021FB84
		public P PickVictim()
		{
			IEnumerable<P> enumerable = this.pending.Keys.Intersect(this.active.Keys);
			int num = int.MaxValue;
			P p = this.pending.FirstOrDefault<KeyValuePair<P, Dictionary<R, int>>>().Key;
			foreach (P p2 in enumerable)
			{
				int num2 = this.active[p2].Sum((KeyValuePair<R, int> kvp) => kvp.Value);
				if (num2 < num)
				{
					num = num2;
					p = p2;
				}
			}
			return p;
		}

		// Token: 0x0600A4A4 RID: 42148 RVA: 0x00221A3C File Offset: 0x0021FC3C
		public DeadlockDetector<P, R> Clone()
		{
			return new DeadlockDetector<P, R>(this);
		}

		// Token: 0x0600A4A5 RID: 42149 RVA: 0x00221A44 File Offset: 0x0021FC44
		private static void AddOne(Dictionary<P, Dictionary<R, int>> dict, P process, R resource, int count = 1)
		{
			Dictionary<R, int> dictionary;
			if (!dict.TryGetValue(process, out dictionary))
			{
				dictionary = new Dictionary<R, int>();
				dict.Add(process, dictionary);
			}
			int num;
			dictionary.TryGetValue(resource, out num);
			dictionary[resource] = num + count;
		}

		// Token: 0x0600A4A6 RID: 42150 RVA: 0x00221A80 File Offset: 0x0021FC80
		private static void RemoveOne(Dictionary<P, Dictionary<R, int>> dict, P process, R resource, int count = 1)
		{
			Dictionary<R, int> dictionary;
			int num;
			if (dict.TryGetValue(process, out dictionary) && dictionary.TryGetValue(resource, out num))
			{
				if (num > count)
				{
					dictionary[resource] = num - count;
					return;
				}
				dictionary.Remove(resource);
				if (dictionary.Count == 0)
				{
					dict.Remove(process);
				}
			}
		}

		// Token: 0x0600A4A7 RID: 42151 RVA: 0x00221ACC File Offset: 0x0021FCCC
		private static IEnumerable<KeyValuePair<R, int>> GetDictionaryOrEmpty(Dictionary<P, Dictionary<R, int>> dict, P process)
		{
			Dictionary<R, int> dictionary;
			if (dict.TryGetValue(process, out dictionary))
			{
				return dictionary;
			}
			return Enumerable.Empty<KeyValuePair<R, int>>();
		}

		// Token: 0x0600A4A8 RID: 42152 RVA: 0x00221AEB File Offset: 0x0021FCEB
		private static void RequiresPositive(int i)
		{
			if (i < 1)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x040055C0 RID: 21952
		private readonly Dictionary<R, int> available;

		// Token: 0x040055C1 RID: 21953
		private readonly Dictionary<P, Dictionary<R, int>> active;

		// Token: 0x040055C2 RID: 21954
		private readonly Dictionary<P, Dictionary<R, int>> pending;
	}
}
