using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Internal;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x02000629 RID: 1577
	internal sealed class EntityClientCacheKey : QueryCacheKey
	{
		// Token: 0x06004C19 RID: 19481 RVA: 0x0010BC24 File Offset: 0x00109E24
		internal EntityClientCacheKey(EntityCommand entityCommand)
		{
			this._commandType = entityCommand.CommandType;
			this._eSqlStatement = entityCommand.CommandText;
			this._parametersToken = EntityClientCacheKey.GetParametersToken(entityCommand);
			this._parameterCount = entityCommand.Parameters.Count;
			this._hashCode = this._commandType.GetHashCode() ^ this._eSqlStatement.GetHashCode() ^ this._parametersToken.GetHashCode();
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x0010BC9C File Offset: 0x00109E9C
		public override bool Equals(object otherObject)
		{
			if (typeof(EntityClientCacheKey) != otherObject.GetType())
			{
				return false;
			}
			EntityClientCacheKey entityClientCacheKey = (EntityClientCacheKey)otherObject;
			return this._commandType == entityClientCacheKey._commandType && this._parameterCount == entityClientCacheKey._parameterCount && this.Equals(entityClientCacheKey._eSqlStatement, this._eSqlStatement) && this.Equals(entityClientCacheKey._parametersToken, this._parametersToken);
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x0010BD0D File Offset: 0x00109F0D
		public override int GetHashCode()
		{
			return this._hashCode;
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x0010BD18 File Offset: 0x00109F18
		private static string GetTypeUsageToken(TypeUsage type)
		{
			string text;
			if (type == DbTypeMap.AnsiString)
			{
				text = "AnsiString";
			}
			else if (type == DbTypeMap.AnsiStringFixedLength)
			{
				text = "AnsiStringFixedLength";
			}
			else if (type == DbTypeMap.String)
			{
				text = "String";
			}
			else if (type == DbTypeMap.StringFixedLength)
			{
				text = "StringFixedLength";
			}
			else if (type == DbTypeMap.Xml)
			{
				text = "String";
			}
			else if (TypeSemantics.IsEnumerationType(type))
			{
				text = type.EdmType.FullName;
			}
			else
			{
				text = type.EdmType.Name;
			}
			return text;
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x0010BD9C File Offset: 0x00109F9C
		private static string GetParametersToken(EntityCommand entityCommand)
		{
			if (entityCommand.Parameters == null || entityCommand.Parameters.Count == 0)
			{
				return "@@0";
			}
			Dictionary<string, TypeUsage> parameterTypeUsage = entityCommand.GetParameterTypeUsage();
			if (1 == parameterTypeUsage.Count)
			{
				return "@@1:" + entityCommand.Parameters[0].ParameterName + ":" + EntityClientCacheKey.GetTypeUsageToken(parameterTypeUsage[entityCommand.Parameters[0].ParameterName]);
			}
			StringBuilder stringBuilder = new StringBuilder(entityCommand.Parameters.Count * 20);
			stringBuilder.Append("@@");
			stringBuilder.Append(entityCommand.Parameters.Count);
			stringBuilder.Append(":");
			string text = "";
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in parameterTypeUsage)
			{
				stringBuilder.Append(text);
				stringBuilder.Append(keyValuePair.Key);
				stringBuilder.Append(":");
				stringBuilder.Append(EntityClientCacheKey.GetTypeUsageToken(keyValuePair.Value));
				text = ";";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x0010BED4 File Offset: 0x0010A0D4
		public override string ToString()
		{
			return string.Join("|", new string[]
			{
				Enum.GetName(typeof(CommandType), this._commandType),
				this._eSqlStatement,
				this._parametersToken
			});
		}

		// Token: 0x04001A8D RID: 6797
		private readonly CommandType _commandType;

		// Token: 0x04001A8E RID: 6798
		private readonly string _eSqlStatement;

		// Token: 0x04001A8F RID: 6799
		private readonly string _parametersToken;

		// Token: 0x04001A90 RID: 6800
		private readonly int _parameterCount;

		// Token: 0x04001A91 RID: 6801
		private readonly int _hashCode;
	}
}
