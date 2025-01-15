using System;
using System.Collections;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200210F RID: 8463
	public abstract class OpenXmlElementList : IEnumerable<OpenXmlElement>, IEnumerable
	{
		// Token: 0x0600D115 RID: 53525
		public abstract OpenXmlElement GetItem(int index);

		// Token: 0x17003284 RID: 12932
		// (get) Token: 0x0600D116 RID: 53526
		public abstract int Count { get; }

		// Token: 0x17003285 RID: 12933
		public virtual OpenXmlElement this[int i]
		{
			get
			{
				return this.GetItem(i);
			}
		}

		// Token: 0x0600D118 RID: 53528 RVA: 0x00299EDC File Offset: 0x002980DC
		public T First<T>() where T : OpenXmlElement
		{
			foreach (OpenXmlElement openXmlElement in this)
			{
				if (openXmlElement is T)
				{
					return (T)((object)openXmlElement);
				}
			}
			return default(T);
		}

		// Token: 0x0600D119 RID: 53529 RVA: 0x00299F3C File Offset: 0x0029813C
		public IEnumerable<T> OfType<T>() where T : OpenXmlElement
		{
			foreach (OpenXmlElement item in this)
			{
				if (item is T)
				{
					yield return (T)((object)item);
				}
			}
			yield break;
		}

		// Token: 0x0600D11A RID: 53530
		public abstract IEnumerator<OpenXmlElement> GetEnumerator();

		// Token: 0x0600D11B RID: 53531 RVA: 0x00299F59 File Offset: 0x00298159
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
