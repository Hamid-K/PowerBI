using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x02000400 RID: 1024
	internal sealed class OptionsCollection : IHtmlOptionsCollection, IHtmlCollection<IHtmlOptionElement>, IEnumerable<IHtmlOptionElement>, IEnumerable
	{
		// Token: 0x06002092 RID: 8338 RVA: 0x00056AE4 File Offset: 0x00054CE4
		public OptionsCollection(IElement parent)
		{
			this._parent = parent;
			this._options = this.GetOptions();
		}

		// Token: 0x17000A3E RID: 2622
		public IHtmlOptionElement this[int index]
		{
			get
			{
				return this.GetOptionAt(index);
			}
		}

		// Token: 0x17000A3F RID: 2623
		public IHtmlOptionElement this[string name]
		{
			get
			{
				if (!string.IsNullOrEmpty(name))
				{
					foreach (IHtmlOptionElement htmlOptionElement in this._options)
					{
						if (htmlOptionElement.Id.Is(name))
						{
							return htmlOptionElement;
						}
					}
					return this._parent.Children[name] as IHtmlOptionElement;
				}
				return null;
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x00056B84 File Offset: 0x00054D84
		// (set) Token: 0x06002096 RID: 8342 RVA: 0x00056BE0 File Offset: 0x00054DE0
		public int SelectedIndex
		{
			get
			{
				int num = 0;
				using (IEnumerator<IHtmlOptionElement> enumerator = this._options.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsSelected)
						{
							return num;
						}
						num++;
					}
				}
				return -1;
			}
			set
			{
				int num = 0;
				foreach (IHtmlOptionElement htmlOptionElement in this._options)
				{
					htmlOptionElement.IsSelected = num++ == value;
				}
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x00056C34 File Offset: 0x00054E34
		public int Length
		{
			get
			{
				return this._options.Count<IHtmlOptionElement>();
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00056C41 File Offset: 0x00054E41
		public IHtmlOptionElement GetOptionAt(int index)
		{
			return this._options.GetItemByIndex(index);
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x00056C50 File Offset: 0x00054E50
		public void SetOptionAt(int index, IHtmlOptionElement value)
		{
			IHtmlOptionElement optionAt = this.GetOptionAt(index);
			if (optionAt != null)
			{
				this._parent.ReplaceChild(value, optionAt);
				return;
			}
			this._parent.AppendChild(value);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00056C84 File Offset: 0x00054E84
		public void Add(IHtmlOptionElement element, IHtmlElement before = null)
		{
			this._parent.InsertBefore(element, before);
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x00056C84 File Offset: 0x00054E84
		public void Add(IHtmlOptionsGroupElement element, IHtmlElement before = null)
		{
			this._parent.InsertBefore(element, before);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00056C94 File Offset: 0x00054E94
		public void Remove(int index)
		{
			if (index >= 0 && index < this.Length)
			{
				IHtmlOptionElement optionAt = this.GetOptionAt(index);
				this._parent.RemoveChild(optionAt);
			}
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x00056CC3 File Offset: 0x00054EC3
		private IEnumerable<IHtmlOptionElement> GetOptions()
		{
			foreach (INode child in this._parent.ChildNodes)
			{
				IHtmlOptionsGroupElement htmlOptionsGroupElement = child as IHtmlOptionsGroupElement;
				if (htmlOptionsGroupElement != null)
				{
					foreach (INode node in htmlOptionsGroupElement.ChildNodes)
					{
						IHtmlOptionElement htmlOptionElement = node as IHtmlOptionElement;
						if (htmlOptionElement != null)
						{
							yield return htmlOptionElement;
						}
					}
					IEnumerator<INode> enumerator2 = null;
				}
				else if (child is IHtmlOptionElement)
				{
					yield return (IHtmlOptionElement)child;
				}
				child = null;
			}
			IEnumerator<INode> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x00056CD3 File Offset: 0x00054ED3
		public IEnumerator<IHtmlOptionElement> GetEnumerator()
		{
			return this._options.GetEnumerator();
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x00056CE0 File Offset: 0x00054EE0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000D1C RID: 3356
		private readonly IElement _parent;

		// Token: 0x04000D1D RID: 3357
		private readonly IEnumerable<IHtmlOptionElement> _options;
	}
}
