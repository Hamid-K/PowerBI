using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200018F RID: 399
	internal sealed class CompositeKeyGroupItem : IGroupItem, IEquatable<IGroupItem>
	{
		// Token: 0x06001550 RID: 5456 RVA: 0x0003BDE9 File Offset: 0x00039FE9
		internal CompositeKeyGroupItem(IEnumerable<KeyValuePair<string, QueryExpression>> keys)
		{
			this._keys = ArgumentValidation.CheckNotNullOrEmpty<KeyValuePair<string, QueryExpression>>(keys, "keys").ToReadOnlyCollection<KeyValuePair<string, QueryExpression>>();
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x0003BE07 File Offset: 0x0003A007
		public ReadOnlyCollection<KeyValuePair<string, QueryExpression>> Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0003BE0F File Offset: 0x0003A00F
		public IEnumerable<KeyValuePair<string, QueryExpression>> GetGroupKeys()
		{
			return this._keys;
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0003BE17 File Offset: 0x0003A017
		public override bool Equals(object obj)
		{
			return this.Equals(obj as IGroupItem);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0003BE28 File Offset: 0x0003A028
		public bool Equals(IGroupItem other)
		{
			CompositeKeyGroupItem compositeKeyGroupItem = other as CompositeKeyGroupItem;
			return compositeKeyGroupItem != null && compositeKeyGroupItem.Keys.SequenceEqual(this.Keys);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0003BE54 File Offset: 0x0003A054
		public override int GetHashCode()
		{
			return this._keys[0].Key.GetHashCode();
		}

		// Token: 0x04000B61 RID: 2913
		private readonly ReadOnlyCollection<KeyValuePair<string, QueryExpression>> _keys;
	}
}
