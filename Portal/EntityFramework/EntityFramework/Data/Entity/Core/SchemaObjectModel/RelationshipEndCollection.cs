using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200030A RID: 778
	internal sealed class RelationshipEndCollection : IList<IRelationshipEnd>, ICollection<IRelationshipEnd>, IEnumerable<IRelationshipEnd>, IEnumerable
	{
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x060024D1 RID: 9425 RVA: 0x000688A9 File Offset: 0x00066AA9
		public int Count
		{
			get
			{
				return this.KeysInDefOrder.Count;
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000688B8 File Offset: 0x00066AB8
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

		// Token: 0x060024D3 RID: 9427 RVA: 0x00068908 File Offset: 0x00066B08
		private static bool IsEndValid(IRelationshipEnd end)
		{
			return !string.IsNullOrEmpty(end.Name);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00068918 File Offset: 0x00066B18
		private bool ValidateUniqueName(SchemaElement end, string name)
		{
			if (this.EndLookup.ContainsKey(name))
			{
				end.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.EndNameAlreadyDefinedDuplicate(name));
				return false;
			}
			return true;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x0006893A File Offset: 0x00066B3A
		public bool Remove(IRelationshipEnd end)
		{
			if (!RelationshipEndCollection.IsEndValid(end))
			{
				return false;
			}
			this.KeysInDefOrder.Remove(end.Name);
			return this.EndLookup.Remove(end.Name);
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x00068969 File Offset: 0x00066B69
		public bool Contains(string name)
		{
			return this.EndLookup.ContainsKey(name);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00068977 File Offset: 0x00066B77
		public bool Contains(IRelationshipEnd end)
		{
			return this.Contains(end.Name);
		}

		// Token: 0x170007D4 RID: 2004
		public IRelationshipEnd this[int index]
		{
			get
			{
				return this.EndLookup[this.KeysInDefOrder[index]];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x000689A5 File Offset: 0x00066BA5
		public IEnumerator<IRelationshipEnd> GetEnumerator()
		{
			return new RelationshipEndCollection.Enumerator(this.EndLookup, this.KeysInDefOrder);
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000689B8 File Offset: 0x00066BB8
		public bool TryGetEnd(string name, out IRelationshipEnd end)
		{
			return this.EndLookup.TryGetValue(name, out end);
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000689C7 File Offset: 0x00066BC7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new RelationshipEndCollection.Enumerator(this.EndLookup, this.KeysInDefOrder);
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060024DD RID: 9437 RVA: 0x000689DA File Offset: 0x00066BDA
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

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x000689FA File Offset: 0x00066BFA
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

		// Token: 0x060024DF RID: 9439 RVA: 0x00068A15 File Offset: 0x00066C15
		public void Clear()
		{
			this.EndLookup.Clear();
			this.KeysInDefOrder.Clear();
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00068A2D File Offset: 0x00066C2D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00068A30 File Offset: 0x00066C30
		int IList<IRelationshipEnd>.IndexOf(IRelationshipEnd end)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x00068A37 File Offset: 0x00066C37
		void IList<IRelationshipEnd>.Insert(int index, IRelationshipEnd end)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x00068A3E File Offset: 0x00066C3E
		void IList<IRelationshipEnd>.RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x00068A48 File Offset: 0x00066C48
		public void CopyTo(IRelationshipEnd[] ends, int index)
		{
			foreach (IRelationshipEnd relationshipEnd in this)
			{
				ends[index++] = relationshipEnd;
			}
		}

		// Token: 0x04000D0B RID: 3339
		private Dictionary<string, IRelationshipEnd> _endLookup;

		// Token: 0x04000D0C RID: 3340
		private List<string> _keysInDefOrder;

		// Token: 0x020009B4 RID: 2484
		private sealed class Enumerator : IEnumerator<IRelationshipEnd>, IDisposable, IEnumerator
		{
			// Token: 0x06005F25 RID: 24357 RVA: 0x00147572 File Offset: 0x00145772
			public Enumerator(Dictionary<string, IRelationshipEnd> data, List<string> keysInDefOrder)
			{
				this._Enumerator = keysInDefOrder.GetEnumerator();
				this._Data = data;
			}

			// Token: 0x06005F26 RID: 24358 RVA: 0x0014758D File Offset: 0x0014578D
			public void Reset()
			{
				((IEnumerator)this._Enumerator).Reset();
			}

			// Token: 0x17001074 RID: 4212
			// (get) Token: 0x06005F27 RID: 24359 RVA: 0x0014759F File Offset: 0x0014579F
			public IRelationshipEnd Current
			{
				get
				{
					return this._Data[this._Enumerator.Current];
				}
			}

			// Token: 0x17001075 RID: 4213
			// (get) Token: 0x06005F28 RID: 24360 RVA: 0x001475B7 File Offset: 0x001457B7
			object IEnumerator.Current
			{
				get
				{
					return this._Data[this._Enumerator.Current];
				}
			}

			// Token: 0x06005F29 RID: 24361 RVA: 0x001475CF File Offset: 0x001457CF
			public bool MoveNext()
			{
				return this._Enumerator.MoveNext();
			}

			// Token: 0x06005F2A RID: 24362 RVA: 0x001475DC File Offset: 0x001457DC
			public void Dispose()
			{
			}

			// Token: 0x040027F5 RID: 10229
			private List<string>.Enumerator _Enumerator;

			// Token: 0x040027F6 RID: 10230
			private readonly Dictionary<string, IRelationshipEnd> _Data;
		}
	}
}
