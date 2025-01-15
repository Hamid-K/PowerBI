using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000040 RID: 64
	internal sealed class RelationshipEndCollection : IList<IRelationshipEnd>, ICollection<IRelationshipEnd>, IEnumerable<IRelationshipEnd>, IEnumerable
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0000E703 File Offset: 0x0000C903
		public int Count
		{
			get
			{
				return this.KeysInDefOrder.Count;
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000E710 File Offset: 0x0000C910
		public void Add(IRelationshipEnd end)
		{
			SchemaElement schemaElement = end as SchemaElement;
			if (!RelationshipEndCollection.IsEndValid(end))
			{
				return;
			}
			if (!this.ValidateUniqueName(schemaElement, end.Name))
			{
				return;
			}
			this.EndLookup.Add(end.Name, end);
			this.KeysInDefOrder.Add(end.Name);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000E760 File Offset: 0x0000C960
		private static bool IsEndValid(IRelationshipEnd end)
		{
			return !string.IsNullOrEmpty(end.Name);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000E770 File Offset: 0x0000C970
		private bool ValidateUniqueName(SchemaElement end, string name)
		{
			if (this.EndLookup.ContainsKey(name))
			{
				end.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.EndNameAlreadyDefinedDuplicate(name));
				return false;
			}
			return true;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000E792 File Offset: 0x0000C992
		public bool Remove(IRelationshipEnd end)
		{
			if (!RelationshipEndCollection.IsEndValid(end))
			{
				return false;
			}
			this.KeysInDefOrder.Remove(end.Name);
			return this.EndLookup.Remove(end.Name);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000E7C1 File Offset: 0x0000C9C1
		public bool Contains(string name)
		{
			return this.EndLookup.ContainsKey(name);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000E7CF File Offset: 0x0000C9CF
		public bool Contains(IRelationshipEnd end)
		{
			return this.Contains(end.Name);
		}

		// Token: 0x170002F5 RID: 757
		public IRelationshipEnd this[int index]
		{
			get
			{
				return this.EndLookup[this.KeysInDefOrder[index]];
			}
			set
			{
				throw EntityUtil.NotSupported();
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000E7FD File Offset: 0x0000C9FD
		public IEnumerator<IRelationshipEnd> GetEnumerator()
		{
			return new RelationshipEndCollection.Enumerator(this.EndLookup, this.KeysInDefOrder);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000E810 File Offset: 0x0000CA10
		public bool TryGetEnd(string name, out IRelationshipEnd end)
		{
			return this.EndLookup.TryGetValue(name, out end);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000E81F File Offset: 0x0000CA1F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new RelationshipEndCollection.Enumerator(this.EndLookup, this.KeysInDefOrder);
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0000E832 File Offset: 0x0000CA32
		private Dictionary<string, IRelationshipEnd> EndLookup
		{
			get
			{
				if (this._endLookup == null)
				{
					this._endLookup = new Dictionary<string, IRelationshipEnd>(StringComparer.Ordinal);
				}
				return this._endLookup;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0000E852 File Offset: 0x0000CA52
		private List<string> KeysInDefOrder
		{
			get
			{
				if (this._keysInDefOrder == null)
				{
					this._keysInDefOrder = new List<string>();
				}
				return this._keysInDefOrder;
			}
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000E86D File Offset: 0x0000CA6D
		public void Clear()
		{
			this.EndLookup.Clear();
			this.KeysInDefOrder.Clear();
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0000E885 File Offset: 0x0000CA85
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000E888 File Offset: 0x0000CA88
		int IList<IRelationshipEnd>.IndexOf(IRelationshipEnd end)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000E88F File Offset: 0x0000CA8F
		void IList<IRelationshipEnd>.Insert(int index, IRelationshipEnd end)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000E896 File Offset: 0x0000CA96
		void IList<IRelationshipEnd>.RemoveAt(int index)
		{
			throw EntityUtil.NotSupported();
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000E8A0 File Offset: 0x0000CAA0
		public void CopyTo(IRelationshipEnd[] ends, int index)
		{
			foreach (IRelationshipEnd relationshipEnd in this)
			{
				ends[index++] = relationshipEnd;
			}
		}

		// Token: 0x04000685 RID: 1669
		private Dictionary<string, IRelationshipEnd> _endLookup;

		// Token: 0x04000686 RID: 1670
		private List<string> _keysInDefOrder;

		// Token: 0x0200029B RID: 667
		private sealed class Enumerator : IEnumerator<IRelationshipEnd>, IDisposable, IEnumerator
		{
			// Token: 0x06001C14 RID: 7188 RVA: 0x0004E487 File Offset: 0x0004C687
			public Enumerator(Dictionary<string, IRelationshipEnd> data, List<string> keysInDefOrder)
			{
				this._Enumerator = keysInDefOrder.GetEnumerator();
				this._Data = data;
			}

			// Token: 0x06001C15 RID: 7189 RVA: 0x0004E4A2 File Offset: 0x0004C6A2
			public void Reset()
			{
				((IEnumerator)this._Enumerator).Reset();
			}

			// Token: 0x170007CA RID: 1994
			// (get) Token: 0x06001C16 RID: 7190 RVA: 0x0004E4B4 File Offset: 0x0004C6B4
			public IRelationshipEnd Current
			{
				get
				{
					return this._Data[this._Enumerator.Current];
				}
			}

			// Token: 0x170007CB RID: 1995
			// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0004E4CC File Offset: 0x0004C6CC
			object IEnumerator.Current
			{
				get
				{
					return this._Data[this._Enumerator.Current];
				}
			}

			// Token: 0x06001C18 RID: 7192 RVA: 0x0004E4E4 File Offset: 0x0004C6E4
			public bool MoveNext()
			{
				return this._Enumerator.MoveNext();
			}

			// Token: 0x06001C19 RID: 7193 RVA: 0x0004E4F1 File Offset: 0x0004C6F1
			public void Dispose()
			{
			}

			// Token: 0x04000F6C RID: 3948
			private List<string>.Enumerator _Enumerator;

			// Token: 0x04000F6D RID: 3949
			private Dictionary<string, IRelationshipEnd> _Data;
		}
	}
}
