using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.ModelParameters
{
	// Token: 0x020000ED RID: 237
	public class ParameterMappings
	{
		// Token: 0x0600063C RID: 1596 RVA: 0x0000D0AD File Offset: 0x0000B2AD
		public ParameterMappings()
		{
			this._parameterMappings = new Dictionary<string, ParameterMapping>(ParameterMappings.ParameterNameComparer);
			this._mappingComparer = ParameterMappingComparer.Instance;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
		public int Count
		{
			get
			{
				return this._parameterMappings.Count;
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000D0DD File Offset: 0x0000B2DD
		public IEnumerator<ParameterMapping> GetEnumerator()
		{
			foreach (KeyValuePair<string, ParameterMapping> keyValuePair in this._parameterMappings)
			{
				yield return keyValuePair.Value;
			}
			IEnumerator<KeyValuePair<string, ParameterMapping>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000D0EC File Offset: 0x0000B2EC
		public bool HasMappedParameter(string parameterName)
		{
			return this._parameterMappings.ContainsKey(parameterName);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		public ISet<ResolvedQueryLiteralExpression> GetMappedValues(string parameterName)
		{
			ParameterMapping parameterMapping;
			if (this._parameterMappings.TryGetValue(parameterName, out parameterMapping))
			{
				return parameterMapping.Values;
			}
			return null;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000D124 File Offset: 0x0000B324
		public bool GetIsListType(string parameterName)
		{
			ParameterMapping parameterMapping;
			return this._parameterMappings.TryGetValue(parameterName, out parameterMapping) && parameterMapping.IsListType;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000D149 File Offset: 0x0000B349
		public override int GetHashCode()
		{
			return Hashing.CombineHash<string>(this._parameterMappings.Keys.AsList<string>(), null);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000D161 File Offset: 0x0000B361
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ParameterMappings);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000D16F File Offset: 0x0000B36F
		public bool Equals(ParameterMappings pm)
		{
			return pm != null && (this == pm || this._parameterMappings.DictionaryEquals(pm._parameterMappings, this._mappingComparer));
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0000D193 File Offset: 0x0000B393
		public static bool operator ==(ParameterMappings lhs, ParameterMappings rhs)
		{
			if (lhs == null)
			{
				return rhs == null;
			}
			return lhs.Equals(rhs);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000D1A6 File Offset: 0x0000B3A6
		public static bool operator !=(ParameterMappings lhs, ParameterMappings rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040002A7 RID: 679
		public static readonly StringComparer ParameterNameComparer = StringComparer.OrdinalIgnoreCase;

		// Token: 0x040002A8 RID: 680
		public static readonly ParameterMappings Empty = new ParameterMappings();

		// Token: 0x040002A9 RID: 681
		protected IDictionary<string, ParameterMapping> _parameterMappings;

		// Token: 0x040002AA RID: 682
		private ParameterMappingComparer _mappingComparer;
	}
}
