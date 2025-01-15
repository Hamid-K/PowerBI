using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;

namespace DocumentFormat.OpenXml
{
	// Token: 0x0200213B RID: 8507
	[DebuggerDisplay("{InnerText}")]
	internal class ListValue<T> : OpenXmlSimpleType where T : OpenXmlSimpleType, new()
	{
		// Token: 0x0600D329 RID: 54057 RVA: 0x0029D744 File Offset: 0x0029B944
		public ListValue()
		{
		}

		// Token: 0x0600D32A RID: 54058 RVA: 0x0029E8D0 File Offset: 0x0029CAD0
		public ListValue(IEnumerable<T> list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			this._list = new ObservableCollection<T>();
			this._list.CollectionChanged += this.CollectionChanged;
			foreach (T t in list)
			{
				this._list.Add(t.Clone() as T);
			}
		}

		// Token: 0x0600D32B RID: 54059 RVA: 0x0029E96C File Offset: 0x0029CB6C
		public ListValue(ListValue<T> source)
			: this(source.Items)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
		}

		// Token: 0x17003300 RID: 13056
		// (get) Token: 0x0600D32C RID: 54060 RVA: 0x0029E988 File Offset: 0x0029CB88
		public override bool HasValue
		{
			get
			{
				if (this._list == null && !string.IsNullOrEmpty(base.TextValue))
				{
					this.TryParse();
				}
				return this._list != null && this._list.Count > 0;
			}
		}

		// Token: 0x17003301 RID: 13057
		// (get) Token: 0x0600D32D RID: 54061 RVA: 0x0029E9C0 File Offset: 0x0029CBC0
		public ICollection<T> Items
		{
			get
			{
				if (this._list == null)
				{
					if (!string.IsNullOrEmpty(base.TextValue))
					{
						this.Parse();
					}
					else
					{
						this._list = new ObservableCollection<T>();
						this._list.CollectionChanged += this.CollectionChanged;
					}
				}
				return this._list;
			}
		}

		// Token: 0x0600D32E RID: 54062 RVA: 0x0029EA14 File Offset: 0x0029CC14
		internal override void Parse()
		{
			this._list = new ObservableCollection<T>();
			this._list.CollectionChanged += this.CollectionChanged;
			if (!string.IsNullOrEmpty(base.TextValue))
			{
				string[] array = base.TextValue.Split(null, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					T t = new T();
					t.InnerText = text;
					this._list.Add(t);
				}
			}
		}

		// Token: 0x0600D32F RID: 54063 RVA: 0x0029EA98 File Offset: 0x0029CC98
		internal override bool TryParse()
		{
			if (!string.IsNullOrEmpty(base.TextValue))
			{
				string[] array = base.TextValue.Split(null, StringSplitOptions.RemoveEmptyEntries);
				ObservableCollection<T> observableCollection = new ObservableCollection<T>();
				foreach (string text in array)
				{
					T t = new T();
					t.InnerText = text;
					observableCollection.Add(t);
				}
				this._list = observableCollection;
				this._list.CollectionChanged += this.CollectionChanged;
				return true;
			}
			return false;
		}

		// Token: 0x17003302 RID: 13058
		// (get) Token: 0x0600D330 RID: 54064 RVA: 0x0029EB20 File Offset: 0x0029CD20
		// (set) Token: 0x0600D331 RID: 54065 RVA: 0x0029EBC0 File Offset: 0x0029CDC0
		public override string InnerText
		{
			get
			{
				if (base.TextValue == null && this._list != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					string text = string.Empty;
					foreach (T t in this._list)
					{
						if (t != null)
						{
							stringBuilder.Append(text);
							stringBuilder.Append(t.ToString());
							text = " ";
						}
					}
					base.TextValue = stringBuilder.ToString();
				}
				return base.TextValue;
			}
			set
			{
				base.TextValue = value;
				this._list = null;
			}
		}

		// Token: 0x0600D332 RID: 54066 RVA: 0x0029EBD0 File Offset: 0x0029CDD0
		internal override OpenXmlSimpleType CloneImp()
		{
			return new ListValue<T>(this);
		}

		// Token: 0x0600D333 RID: 54067 RVA: 0x0029EBD8 File Offset: 0x0029CDD8
		private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			base.TextValue = null;
		}

		// Token: 0x0600D334 RID: 54068 RVA: 0x0029EBE4 File Offset: 0x0029CDE4
		internal override IEnumerable<OpenXmlSimpleType> GetListItems()
		{
			foreach (T item in this.Items)
			{
				yield return item;
			}
			yield break;
		}

		// Token: 0x04006985 RID: 27013
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private const string _listSeparator = " ";

		// Token: 0x04006986 RID: 27014
		private ObservableCollection<T> _list;
	}
}
