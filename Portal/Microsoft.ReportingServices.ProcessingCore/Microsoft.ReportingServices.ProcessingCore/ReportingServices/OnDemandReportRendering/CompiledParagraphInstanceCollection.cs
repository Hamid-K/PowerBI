using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000327 RID: 807
	public sealed class CompiledParagraphInstanceCollection : ReportElementInstanceCollectionBase<CompiledParagraphInstance>, IList<ICompiledParagraphInstance>, ICollection<ICompiledParagraphInstance>, IEnumerable<ICompiledParagraphInstance>, IEnumerable
	{
		// Token: 0x06001E34 RID: 7732 RVA: 0x00075E73 File Offset: 0x00074073
		internal CompiledParagraphInstanceCollection(CompiledRichTextInstance compiledRichTextInstance)
		{
			this.m_compiledRichTextInstance = compiledRichTextInstance;
			this.m_compiledParagraphInstances = new List<CompiledParagraphInstance>();
		}

		// Token: 0x17001102 RID: 4354
		public override CompiledParagraphInstance this[int i]
		{
			get
			{
				return this.m_compiledParagraphInstances[i];
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x00075E9B File Offset: 0x0007409B
		public override int Count
		{
			get
			{
				return this.m_compiledParagraphInstances.Count;
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00075EA8 File Offset: 0x000740A8
		int IList<ICompiledParagraphInstance>.IndexOf(ICompiledParagraphInstance item)
		{
			return this.m_compiledParagraphInstances.IndexOf((CompiledParagraphInstance)item);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00075EBB File Offset: 0x000740BB
		void IList<ICompiledParagraphInstance>.Insert(int index, ICompiledParagraphInstance item)
		{
			this.m_compiledParagraphInstances.Insert(index, (CompiledParagraphInstance)item);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00075ECF File Offset: 0x000740CF
		void IList<ICompiledParagraphInstance>.RemoveAt(int index)
		{
			this.m_compiledParagraphInstances.RemoveAt(index);
		}

		// Token: 0x17001104 RID: 4356
		ICompiledParagraphInstance IList<ICompiledParagraphInstance>.this[int index]
		{
			get
			{
				return this.m_compiledParagraphInstances[index];
			}
			set
			{
				this.m_compiledParagraphInstances[index] = (CompiledParagraphInstance)value;
			}
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x00075EFF File Offset: 0x000740FF
		void ICollection<ICompiledParagraphInstance>.Add(ICompiledParagraphInstance item)
		{
			this.m_compiledParagraphInstances.Add((CompiledParagraphInstance)item);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x00075F12 File Offset: 0x00074112
		void ICollection<ICompiledParagraphInstance>.Clear()
		{
			this.m_compiledParagraphInstances.Clear();
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x00075F1F File Offset: 0x0007411F
		bool ICollection<ICompiledParagraphInstance>.Contains(ICompiledParagraphInstance item)
		{
			return this.m_compiledParagraphInstances.Contains((CompiledParagraphInstance)item);
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x00075F34 File Offset: 0x00074134
		void ICollection<ICompiledParagraphInstance>.CopyTo(ICompiledParagraphInstance[] array, int arrayIndex)
		{
			CompiledParagraphInstance[] array2 = new CompiledParagraphInstance[array.Length];
			this.m_compiledParagraphInstances.CopyTo(array2, arrayIndex);
			for (int i = 0; i < array2.Length; i++)
			{
				array[i] = array2[i];
			}
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x00075F6B File Offset: 0x0007416B
		int ICollection<ICompiledParagraphInstance>.Count
		{
			get
			{
				return this.m_compiledParagraphInstances.Count;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x00075F78 File Offset: 0x00074178
		bool ICollection<ICompiledParagraphInstance>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x00075F7B File Offset: 0x0007417B
		bool ICollection<ICompiledParagraphInstance>.Remove(ICompiledParagraphInstance item)
		{
			return this.m_compiledParagraphInstances.Remove((CompiledParagraphInstance)item);
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x00075F8E File Offset: 0x0007418E
		IEnumerator<ICompiledParagraphInstance> IEnumerable<ICompiledParagraphInstance>.GetEnumerator()
		{
			foreach (CompiledParagraphInstance compiledParagraphInstance in this.m_compiledParagraphInstances)
			{
				yield return compiledParagraphInstance;
			}
			List<CompiledParagraphInstance>.Enumerator enumerator = default(List<CompiledParagraphInstance>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x00075F9D File Offset: 0x0007419D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.m_compiledParagraphInstances).GetEnumerator();
		}

		// Token: 0x04000F89 RID: 3977
		private CompiledRichTextInstance m_compiledRichTextInstance;

		// Token: 0x04000F8A RID: 3978
		private List<CompiledParagraphInstance> m_compiledParagraphInstances;
	}
}
