using System;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x0200062B RID: 1579
	internal sealed class LinqQueryCacheKey : QueryCacheKey
	{
		// Token: 0x06004C23 RID: 19491 RVA: 0x0010C0FC File Offset: 0x0010A2FC
		internal LinqQueryCacheKey(string expressionKey, int parameterCount, string parametersToken, string includePathsToken, MergeOption mergeOption, bool streaming, bool useCSharpNullComparisonBehavior, Type resultType)
		{
			this._expressionKey = expressionKey;
			this._parameterCount = parameterCount;
			this._parametersToken = parametersToken;
			this._includePathsToken = includePathsToken;
			this._mergeOption = mergeOption;
			this._streaming = streaming;
			this._resultType = resultType;
			this._useCSharpNullComparisonBehavior = useCSharpNullComparisonBehavior;
			int num = this._expressionKey.GetHashCode() ^ this._mergeOption.GetHashCode();
			if (this._parametersToken != null)
			{
				num ^= this._parametersToken.GetHashCode();
			}
			if (this._includePathsToken != null)
			{
				num ^= this._includePathsToken.GetHashCode();
			}
			num ^= this._useCSharpNullComparisonBehavior.GetHashCode();
			this._hashCode = num;
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x0010C1AC File Offset: 0x0010A3AC
		public override bool Equals(object otherObject)
		{
			if (typeof(LinqQueryCacheKey) != otherObject.GetType())
			{
				return false;
			}
			LinqQueryCacheKey linqQueryCacheKey = (LinqQueryCacheKey)otherObject;
			return this._parameterCount == linqQueryCacheKey._parameterCount && this._mergeOption == linqQueryCacheKey._mergeOption && this._streaming == linqQueryCacheKey._streaming && this.Equals(linqQueryCacheKey._expressionKey, this._expressionKey) && this.Equals(linqQueryCacheKey._includePathsToken, this._includePathsToken) && this.Equals(linqQueryCacheKey._parametersToken, this._parametersToken) && object.Equals(linqQueryCacheKey._resultType, this._resultType) && object.Equals(linqQueryCacheKey._useCSharpNullComparisonBehavior, this._useCSharpNullComparisonBehavior);
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0010C272 File Offset: 0x0010A472
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06004C26 RID: 19494 RVA: 0x0010C27C File Offset: 0x0010A47C
		public override string ToString()
		{
			return string.Join("|", new string[]
			{
				this._expressionKey,
				this._parametersToken,
				this._includePathsToken,
				Enum.GetName(typeof(MergeOption), this._mergeOption),
				this._useCSharpNullComparisonBehavior.ToString()
			});
		}

		// Token: 0x04001A9B RID: 6811
		private readonly int _hashCode;

		// Token: 0x04001A9C RID: 6812
		private readonly string _expressionKey;

		// Token: 0x04001A9D RID: 6813
		private readonly string _parametersToken;

		// Token: 0x04001A9E RID: 6814
		private readonly int _parameterCount;

		// Token: 0x04001A9F RID: 6815
		private readonly string _includePathsToken;

		// Token: 0x04001AA0 RID: 6816
		private readonly MergeOption _mergeOption;

		// Token: 0x04001AA1 RID: 6817
		private readonly Type _resultType;

		// Token: 0x04001AA2 RID: 6818
		private readonly bool _streaming;

		// Token: 0x04001AA3 RID: 6819
		private readonly bool _useCSharpNullComparisonBehavior;
	}
}
