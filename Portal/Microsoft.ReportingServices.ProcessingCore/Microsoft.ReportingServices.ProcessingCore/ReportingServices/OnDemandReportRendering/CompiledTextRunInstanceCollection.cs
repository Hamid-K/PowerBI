using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000328 RID: 808
	public sealed class CompiledTextRunInstanceCollection : ReportElementInstanceCollectionBase<CompiledTextRunInstance>, IList<ICompiledTextRunInstance>, ICollection<ICompiledTextRunInstance>, IEnumerable<ICompiledTextRunInstance>, IEnumerable
	{
		// Token: 0x06001E45 RID: 7749 RVA: 0x00075FAA File Offset: 0x000741AA
		internal CompiledTextRunInstanceCollection(CompiledRichTextInstance compiledRichTextInstance)
		{
			this.m_compiledRichTextInstance = compiledRichTextInstance;
			this.m_compiledTextRunInstances = new List<CompiledTextRunInstance>();
		}

		// Token: 0x17001107 RID: 4359
		public override CompiledTextRunInstance this[int i]
		{
			get
			{
				return this.m_compiledTextRunInstances[i];
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x00075FD2 File Offset: 0x000741D2
		public override int Count
		{
			get
			{
				return this.m_compiledTextRunInstances.Count;
			}
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00075FDF File Offset: 0x000741DF
		int IList<ICompiledTextRunInstance>.IndexOf(ICompiledTextRunInstance item)
		{
			return this.m_compiledTextRunInstances.IndexOf((CompiledTextRunInstance)item);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00075FF2 File Offset: 0x000741F2
		void IList<ICompiledTextRunInstance>.Insert(int index, ICompiledTextRunInstance item)
		{
			this.m_compiledTextRunInstances.Insert(index, (CompiledTextRunInstance)item);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00076006 File Offset: 0x00074206
		void IList<ICompiledTextRunInstance>.RemoveAt(int index)
		{
			this.m_compiledTextRunInstances.RemoveAt(index);
		}

		// Token: 0x17001109 RID: 4361
		ICompiledTextRunInstance IList<ICompiledTextRunInstance>.this[int index]
		{
			get
			{
				return this.m_compiledTextRunInstances[index];
			}
			set
			{
				this.m_compiledTextRunInstances[index] = (CompiledTextRunInstance)value;
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x00076036 File Offset: 0x00074236
		void ICollection<ICompiledTextRunInstance>.Add(ICompiledTextRunInstance item)
		{
			this.m_compiledTextRunInstances.Add((CompiledTextRunInstance)item);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00076049 File Offset: 0x00074249
		void ICollection<ICompiledTextRunInstance>.Clear()
		{
			this.m_compiledTextRunInstances.Clear();
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x00076056 File Offset: 0x00074256
		bool ICollection<ICompiledTextRunInstance>.Contains(ICompiledTextRunInstance item)
		{
			return this.m_compiledTextRunInstances.Contains((CompiledTextRunInstance)item);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0007606C File Offset: 0x0007426C
		void ICollection<ICompiledTextRunInstance>.CopyTo(ICompiledTextRunInstance[] array, int arrayIndex)
		{
			CompiledTextRunInstance[] array2 = new CompiledTextRunInstance[array.Length];
			this.m_compiledTextRunInstances.CopyTo(array2, arrayIndex);
			for (int i = 0; i < array2.Length; i++)
			{
				array[i] = array2[i];
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x000760A3 File Offset: 0x000742A3
		int ICollection<ICompiledTextRunInstance>.Count
		{
			get
			{
				return this.m_compiledTextRunInstances.Count;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x000760B0 File Offset: 0x000742B0
		bool ICollection<ICompiledTextRunInstance>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x000760B3 File Offset: 0x000742B3
		bool ICollection<ICompiledTextRunInstance>.Remove(ICompiledTextRunInstance item)
		{
			return this.m_compiledTextRunInstances.Remove((CompiledTextRunInstance)item);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000760C6 File Offset: 0x000742C6
		IEnumerator<ICompiledTextRunInstance> IEnumerable<ICompiledTextRunInstance>.GetEnumerator()
		{
			foreach (CompiledTextRunInstance compiledTextRunInstance in this.m_compiledTextRunInstances)
			{
				yield return compiledTextRunInstance;
			}
			List<CompiledTextRunInstance>.Enumerator enumerator = default(List<CompiledTextRunInstance>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x000760D5 File Offset: 0x000742D5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.m_compiledTextRunInstances).GetEnumerator();
		}

		// Token: 0x04000F8B RID: 3979
		private CompiledRichTextInstance m_compiledRichTextInstance;

		// Token: 0x04000F8C RID: 3980
		private List<CompiledTextRunInstance> m_compiledTextRunInstances;
	}
}
