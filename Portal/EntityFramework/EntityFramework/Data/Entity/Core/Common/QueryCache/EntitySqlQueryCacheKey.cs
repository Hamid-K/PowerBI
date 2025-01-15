using System;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x0200062A RID: 1578
	internal sealed class EntitySqlQueryCacheKey : QueryCacheKey
	{
		// Token: 0x06004C1F RID: 19487 RVA: 0x0010BF20 File Offset: 0x0010A120
		internal EntitySqlQueryCacheKey(string defaultContainerName, string eSqlStatement, int parameterCount, string parametersToken, string includePathsToken, MergeOption mergeOption, bool streaming, Type resultType)
		{
			this._defaultContainer = defaultContainerName;
			this._eSqlStatement = eSqlStatement;
			this._parameterCount = parameterCount;
			this._parametersToken = parametersToken;
			this._includePathsToken = includePathsToken;
			this._mergeOption = mergeOption;
			this._streaming = streaming;
			this._resultType = resultType;
			int num = this._eSqlStatement.GetHashCode() ^ this._mergeOption.GetHashCode();
			if (this._parametersToken != null)
			{
				num ^= this._parametersToken.GetHashCode();
			}
			if (this._includePathsToken != null)
			{
				num ^= this._includePathsToken.GetHashCode();
			}
			if (this._defaultContainer != null)
			{
				num ^= this._defaultContainer.GetHashCode();
			}
			this._hashCode = num;
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x0010BFD8 File Offset: 0x0010A1D8
		public override bool Equals(object otherObject)
		{
			if (typeof(EntitySqlQueryCacheKey) != otherObject.GetType())
			{
				return false;
			}
			EntitySqlQueryCacheKey entitySqlQueryCacheKey = (EntitySqlQueryCacheKey)otherObject;
			return this._parameterCount == entitySqlQueryCacheKey._parameterCount && this._mergeOption == entitySqlQueryCacheKey._mergeOption && this._streaming == entitySqlQueryCacheKey._streaming && this.Equals(entitySqlQueryCacheKey._defaultContainer, this._defaultContainer) && this.Equals(entitySqlQueryCacheKey._eSqlStatement, this._eSqlStatement) && this.Equals(entitySqlQueryCacheKey._includePathsToken, this._includePathsToken) && this.Equals(entitySqlQueryCacheKey._parametersToken, this._parametersToken) && object.Equals(entitySqlQueryCacheKey._resultType, this._resultType);
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x0010C092 File Offset: 0x0010A292
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x0010C09C File Offset: 0x0010A29C
		public override string ToString()
		{
			return string.Join("|", new string[]
			{
				this._defaultContainer,
				this._eSqlStatement,
				this._parametersToken,
				this._includePathsToken,
				Enum.GetName(typeof(MergeOption), this._mergeOption)
			});
		}

		// Token: 0x04001A92 RID: 6802
		private readonly int _hashCode;

		// Token: 0x04001A93 RID: 6803
		private readonly string _defaultContainer;

		// Token: 0x04001A94 RID: 6804
		private readonly string _eSqlStatement;

		// Token: 0x04001A95 RID: 6805
		private readonly string _parametersToken;

		// Token: 0x04001A96 RID: 6806
		private readonly int _parameterCount;

		// Token: 0x04001A97 RID: 6807
		private readonly string _includePathsToken;

		// Token: 0x04001A98 RID: 6808
		private readonly MergeOption _mergeOption;

		// Token: 0x04001A99 RID: 6809
		private readonly Type _resultType;

		// Token: 0x04001A9A RID: 6810
		private readonly bool _streaming;
	}
}
