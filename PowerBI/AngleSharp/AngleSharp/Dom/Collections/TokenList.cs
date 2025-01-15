using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Collections
{
	// Token: 0x02000406 RID: 1030
	internal class TokenList : ITokenList, IEnumerable<string>, IEnumerable, IBindable
	{
		// Token: 0x14000140 RID: 320
		// (add) Token: 0x060020DC RID: 8412 RVA: 0x000581DC File Offset: 0x000563DC
		// (remove) Token: 0x060020DD RID: 8413 RVA: 0x00058214 File Offset: 0x00056414
		public event Action<string> Changed;

		// Token: 0x060020DE RID: 8414 RVA: 0x00058249 File Offset: 0x00056449
		internal TokenList(string value)
		{
			this._tokens = new List<string>();
			this.Update(value);
		}

		// Token: 0x17000A4F RID: 2639
		public string this[int index]
		{
			get
			{
				return this._tokens[index];
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x00058271 File Offset: 0x00056471
		public int Length
		{
			get
			{
				return this._tokens.Count;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00058280 File Offset: 0x00056480
		public void Update(string value)
		{
			this._tokens.Clear();
			if (!string.IsNullOrEmpty(value))
			{
				string[] array = value.SplitSpaces();
				for (int i = 0; i < array.Length; i++)
				{
					if (!this._tokens.Contains(array[i]))
					{
						this._tokens.Add(array[i]);
					}
				}
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000582D3 File Offset: 0x000564D3
		public bool Contains(string token)
		{
			return this._tokens.Contains(token);
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000582E4 File Offset: 0x000564E4
		public void Remove(params string[] tokens)
		{
			bool flag = false;
			foreach (string text in tokens)
			{
				if (this._tokens.Contains(text))
				{
					this._tokens.Remove(text);
					flag = true;
				}
			}
			if (flag)
			{
				this.RaiseChanged();
			}
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00058330 File Offset: 0x00056530
		public void Add(params string[] tokens)
		{
			bool flag = false;
			foreach (string text in tokens)
			{
				if (!this._tokens.Contains(text))
				{
					this._tokens.Add(text);
					flag = true;
				}
			}
			if (flag)
			{
				this.RaiseChanged();
			}
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x00058378 File Offset: 0x00056578
		public bool Toggle(string token, bool force = false)
		{
			bool flag = this._tokens.Contains(token);
			if (flag && force)
			{
				return true;
			}
			if (flag)
			{
				this._tokens.Remove(token);
			}
			else
			{
				this._tokens.Add(token);
			}
			this.RaiseChanged();
			return !flag;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000583C1 File Offset: 0x000565C1
		private void RaiseChanged()
		{
			Action<string> changed = this.Changed;
			if (changed == null)
			{
				return;
			}
			changed(this.ToString());
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000583D9 File Offset: 0x000565D9
		public IEnumerator<string> GetEnumerator()
		{
			return this._tokens.GetEnumerator();
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000583EB File Offset: 0x000565EB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000583F3 File Offset: 0x000565F3
		public override string ToString()
		{
			return string.Join(" ", this._tokens);
		}

		// Token: 0x04000D25 RID: 3365
		private readonly List<string> _tokens;
	}
}
