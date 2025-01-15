using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x02000074 RID: 116
	internal sealed class DictionaryEncodingCoordinator
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x00009C78 File Offset: 0x00007E78
		internal DictionaryEncodingCoordinator(string dictionaryIdPrefix, int dictionaryCapacity, HashSet<string> idExcludeList, IReadOnlyDictionary<string, ISet<string>> calculationsWithSharedValues)
		{
			this._dictionaryIdPrefix = dictionaryIdPrefix;
			this._dictionaryCapacity = dictionaryCapacity;
			this._idExcludeList = idExcludeList;
			this._calculationsWithSharedValues = calculationsWithSharedValues;
			this._dictionaryEncodingsByOwnerId = new Dictionary<string, DsrValuesDictionaryBuilder>(StringComparer.Ordinal);
			this._dictionaryEncodings = new List<DsrValuesDictionaryBuilder>();
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00009CB8 File Offset: 0x00007EB8
		internal IReadOnlyList<DsrValuesDictionaryBuilder> DictionaryEncodings
		{
			get
			{
				return this._dictionaryEncodings;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00009CC0 File Offset: 0x00007EC0
		internal bool TryAddNewDictionary(string ownerId, Type ownerType, out string dictId)
		{
			dictId = null;
			bool flag = ConceptualTypeConverter.IsVariant(ownerType);
			if ((!(ownerType == DictionaryEncodingCoordinator.StringType) && !flag) || (this._idExcludeList != null && this._idExcludeList.Contains(ownerId)))
			{
				return false;
			}
			this.FindOrAddDictionary(ownerId, flag, out dictId);
			return true;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00009D0C File Offset: 0x00007F0C
		private void FindOrAddDictionary(string ownerId, bool isVariant, out string dictId)
		{
			ISet<string> set = null;
			IReadOnlyDictionary<string, ISet<string>> calculationsWithSharedValues = this._calculationsWithSharedValues;
			bool flag = calculationsWithSharedValues != null && calculationsWithSharedValues.TryGetValue(ownerId, out set);
			DsrValuesDictionaryBuilder dsrValuesDictionaryBuilder = null;
			object obj;
			if (flag)
			{
				Dictionary<ISet<string>, DsrValuesDictionaryBuilder> dictionaryEncodingsByCalculationSet = this._dictionaryEncodingsByCalculationSet;
				obj = dictionaryEncodingsByCalculationSet != null && dictionaryEncodingsByCalculationSet.TryGetValue(set, out dsrValuesDictionaryBuilder);
			}
			else
			{
				obj = 0;
			}
			object obj2 = obj;
			if (obj2 == null || dsrValuesDictionaryBuilder.IsVariant != isVariant)
			{
				dsrValuesDictionaryBuilder = new DsrValuesDictionaryBuilder(isVariant, this.GetNewIdentifier(), this._dictionaryCapacity);
				this._dictionaryEncodings.Add(dsrValuesDictionaryBuilder);
			}
			dictId = dsrValuesDictionaryBuilder.Id;
			this._dictionaryEncodingsByOwnerId[ownerId] = dsrValuesDictionaryBuilder;
			if (obj2 == 0 && flag && flag)
			{
				Util.AddToLazyDictionary<ISet<string>, DsrValuesDictionaryBuilder>(ref this._dictionaryEncodingsByCalculationSet, set, dsrValuesDictionaryBuilder, null);
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00009DA8 File Offset: 0x00007FA8
		internal bool TryGetOrAddValue(string ownerId, object value, out int idx)
		{
			idx = -1;
			if (value == null)
			{
				return false;
			}
			DsrValuesDictionaryBuilder dsrValuesDictionaryBuilder;
			if (!this._dictionaryEncodingsByOwnerId.TryGetValue(ownerId, out dsrValuesDictionaryBuilder))
			{
				return false;
			}
			if (dsrValuesDictionaryBuilder.IsVariant)
			{
				Type type = value.GetType();
				if (type != DictionaryEncodingCoordinator.StringType && type != DictionaryEncodingCoordinator.DateTimeType)
				{
					return false;
				}
			}
			return dsrValuesDictionaryBuilder.TryGetOrAdd(value, out idx);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00009E02 File Offset: 0x00008002
		private string GetNewIdentifier()
		{
			return StringUtil.FormatInvariant("{0}{1}", new object[]
			{
				this._dictionaryIdPrefix,
				this.DictionaryEncodings.Count
			});
		}

		// Token: 0x040001B4 RID: 436
		private static readonly Type StringType = typeof(string);

		// Token: 0x040001B5 RID: 437
		private static readonly Type DateTimeType = typeof(DateTime);

		// Token: 0x040001B6 RID: 438
		private readonly string _dictionaryIdPrefix;

		// Token: 0x040001B7 RID: 439
		private readonly Dictionary<string, DsrValuesDictionaryBuilder> _dictionaryEncodingsByOwnerId;

		// Token: 0x040001B8 RID: 440
		private readonly List<DsrValuesDictionaryBuilder> _dictionaryEncodings;

		// Token: 0x040001B9 RID: 441
		private readonly int _dictionaryCapacity;

		// Token: 0x040001BA RID: 442
		private readonly HashSet<string> _idExcludeList;

		// Token: 0x040001BB RID: 443
		private readonly IReadOnlyDictionary<string, ISet<string>> _calculationsWithSharedValues;

		// Token: 0x040001BC RID: 444
		private Dictionary<ISet<string>, DsrValuesDictionaryBuilder> _dictionaryEncodingsByCalculationSet;
	}
}
