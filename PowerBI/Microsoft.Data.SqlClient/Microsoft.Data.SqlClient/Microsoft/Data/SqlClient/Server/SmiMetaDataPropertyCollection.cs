using System;
using System.Collections.Generic;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient.Server
{
	// Token: 0x02000126 RID: 294
	internal class SmiMetaDataPropertyCollection
	{
		// Token: 0x060016E9 RID: 5865 RVA: 0x00061E60 File Offset: 0x00060060
		private static SmiMetaDataPropertyCollection CreateEmptyInstance()
		{
			SmiMetaDataPropertyCollection smiMetaDataPropertyCollection = new SmiMetaDataPropertyCollection();
			smiMetaDataPropertyCollection.SetReadOnly();
			return smiMetaDataPropertyCollection;
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00061E7C File Offset: 0x0006007C
		internal SmiMetaDataPropertyCollection()
		{
			this._properties = new SmiMetaDataProperty[3];
			this._isReadOnly = false;
			this._properties[0] = SmiMetaDataPropertyCollection.s_emptyDefaultFields;
			this._properties[1] = SmiMetaDataPropertyCollection.s_emptySortOrder;
			this._properties[2] = SmiMetaDataPropertyCollection.s_emptyUniqueKey;
		}

		// Token: 0x17000940 RID: 2368
		internal SmiMetaDataProperty this[SmiPropertySelector key]
		{
			get
			{
				return this._properties[(int)key];
			}
			set
			{
				this.EnsureWritable();
				SmiMetaDataProperty[] properties = this._properties;
				if (value == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
				}
				properties[(int)key] = value;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00061EF0 File Offset: 0x000600F0
		internal bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00061EF8 File Offset: 0x000600F8
		internal IEnumerable<SmiMetaDataProperty> Values
		{
			get
			{
				return new List<SmiMetaDataProperty>(this._properties);
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00061F05 File Offset: 0x00060105
		internal void SetReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00061F0E File Offset: 0x0006010E
		private void EnsureWritable()
		{
			if (this.IsReadOnly)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.InvalidSmiCall);
			}
		}

		// Token: 0x0400095C RID: 2396
		private const int SelectorCount = 3;

		// Token: 0x0400095D RID: 2397
		private readonly SmiMetaDataProperty[] _properties;

		// Token: 0x0400095E RID: 2398
		private bool _isReadOnly;

		// Token: 0x0400095F RID: 2399
		private static readonly SmiDefaultFieldsProperty s_emptyDefaultFields = new SmiDefaultFieldsProperty(new List<bool>());

		// Token: 0x04000960 RID: 2400
		private static readonly SmiOrderProperty s_emptySortOrder = new SmiOrderProperty(new List<SmiOrderProperty.SmiColumnOrder>());

		// Token: 0x04000961 RID: 2401
		private static readonly SmiUniqueKeyProperty s_emptyUniqueKey = new SmiUniqueKeyProperty(new List<bool>());

		// Token: 0x04000962 RID: 2402
		internal static readonly SmiMetaDataPropertyCollection s_emptyInstance = SmiMetaDataPropertyCollection.CreateEmptyInstance();
	}
}
