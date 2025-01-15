using System;
using System.Collections.Generic;
using System.Globalization;
using antlr;
using Microsoft.Mashup.ScriptDom.ScriptGenerator;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000041 RID: 65
	internal abstract class OptionsHelper<OptionType> where OptionType : struct, IConvertible
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00005B70 File Offset: 0x00003D70
		protected void AddOptionMapping(OptionType option, string identifier, SqlVersionFlags validVersions)
		{
			OptionsHelper<OptionType>.OptionInfo optionInfo = new OptionsHelper<OptionType>.OptionInfo(option, identifier, validVersions);
			this._optionToOptionInfo.Add(option, optionInfo);
			this._stringToOptionInfo.Add(identifier, optionInfo);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005BA0 File Offset: 0x00003DA0
		protected void AddOptionMapping(OptionType option, TSqlTokenType tokenId, SqlVersionFlags validVersions)
		{
			OptionsHelper<OptionType>.OptionInfo optionInfo = new OptionsHelper<OptionType>.OptionInfo(option, tokenId, validVersions);
			this._optionToOptionInfo.Add(option, optionInfo);
			this._stringToOptionInfo.Add(ScriptGeneratorSupporter.GetLowerCase(tokenId), optionInfo);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005BD5 File Offset: 0x00003DD5
		protected void AddOptionMapping(OptionType option, string identifier)
		{
			this.AddOptionMapping(option, identifier, SqlVersionFlags.TSqlAll);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005BE1 File Offset: 0x00003DE1
		protected void AddOptionMapping(OptionType option, TSqlTokenType tokenId)
		{
			this.AddOptionMapping(option, tokenId, SqlVersionFlags.TSqlAll);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005BED File Offset: 0x00003DED
		internal bool IsValidKeyword(IToken token)
		{
			return this._stringToOptionInfo.ContainsKey(token.getText());
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005C00 File Offset: 0x00003E00
		internal SqlVersionFlags MapSqlVersionToSqlVersionFlags(SqlVersion sqlVersion)
		{
			switch (sqlVersion)
			{
			case SqlVersion.Sql90:
				return SqlVersionFlags.TSql90;
			case SqlVersion.Sql80:
				return SqlVersionFlags.TSql80;
			case SqlVersion.Sql100:
				return SqlVersionFlags.TSql100;
			case SqlVersion.Sql110:
				return SqlVersionFlags.TSql110;
			default:
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SqlScriptGeneratorResource.UnknownEnumValue, new object[] { sqlVersion, "SqlVersion" }), "sqlVersion");
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005C64 File Offset: 0x00003E64
		internal OptionType ParseOption(IToken token, SqlVersionFlags version)
		{
			OptionsHelper<OptionType>.OptionInfo optionInfo;
			if (this._stringToOptionInfo.TryGetValue(token.getText(), ref optionInfo) && optionInfo.IsValidIn(version))
			{
				return optionInfo.Value;
			}
			throw this.GetMatchingException(token);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005C9D File Offset: 0x00003E9D
		internal bool TryParseOption(IToken token, SqlVersionFlags version, out OptionType returnValue)
		{
			return this.TryParseOption(token.getText(), version, out returnValue);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00005CB0 File Offset: 0x00003EB0
		internal bool TryParseOption(string tokenString, SqlVersionFlags version, out OptionType returnValue)
		{
			OptionsHelper<OptionType>.OptionInfo optionInfo;
			if (this._stringToOptionInfo.TryGetValue(tokenString, ref optionInfo) && optionInfo.IsValidIn(version))
			{
				returnValue = optionInfo.Value;
				return true;
			}
			returnValue = default(OptionType);
			return false;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005CEC File Offset: 0x00003EEC
		internal OptionType ParseOption(IToken token)
		{
			return this.ParseOption(token, SqlVersionFlags.TSqlAll);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005CF7 File Offset: 0x00003EF7
		internal bool TryParseOption(IToken token, out OptionType returnValue)
		{
			return this.TryParseOption(token, SqlVersionFlags.TSqlAll, out returnValue);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005D03 File Offset: 0x00003F03
		protected virtual TSqlParseErrorException GetMatchingException(IToken token)
		{
			return TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00005D0C File Offset: 0x00003F0C
		internal void GenerateSourceForOption(ScriptWriter writer, OptionType option)
		{
			OptionsHelper<OptionType>.OptionInfo optionInfo;
			if (this._optionToOptionInfo.TryGetValue(option, ref optionInfo))
			{
				optionInfo.GenerateSource(writer);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005D30 File Offset: 0x00003F30
		internal bool TryGenerateSourceForOption(ScriptWriter writer, OptionType option)
		{
			OptionsHelper<OptionType>.OptionInfo optionInfo;
			if (this._optionToOptionInfo.TryGetValue(option, ref optionInfo))
			{
				optionInfo.GenerateSource(writer);
				return true;
			}
			return false;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005D58 File Offset: 0x00003F58
		internal void GenerateCommaSeparatedFlagOptions(ScriptWriter writer, OptionType options)
		{
			bool flag = true;
			long num = options.ToInt64(CultureInfo.InvariantCulture.NumberFormat);
			foreach (object obj in Enum.GetValues(typeof(OptionType)))
			{
				OptionType optionType = (OptionType)((object)obj);
				long num2 = optionType.ToInt64(CultureInfo.InvariantCulture.NumberFormat);
				if (!optionType.Equals(default(OptionType)) && (num & num2) == num2)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						writer.AddKeyword(TSqlTokenType.Comma);
						writer.AddToken(ScriptGeneratorSupporter.CreateWhitespaceToken(1));
					}
					this.GenerateSourceForOption(writer, optionType);
				}
			}
		}

		// Token: 0x04000120 RID: 288
		private Dictionary<OptionType, OptionsHelper<OptionType>.OptionInfo> _optionToOptionInfo = new Dictionary<OptionType, OptionsHelper<OptionType>.OptionInfo>();

		// Token: 0x04000121 RID: 289
		private Dictionary<string, OptionsHelper<OptionType>.OptionInfo> _stringToOptionInfo = new Dictionary<string, OptionsHelper<OptionType>.OptionInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x02000042 RID: 66
		private class OptionInfo
		{
			// Token: 0x060001B4 RID: 436 RVA: 0x00005E5B File Offset: 0x0000405B
			public OptionInfo(OptionType optionValue, TSqlTokenType tokenId, SqlVersionFlags appliesToVersion)
			{
				this._optionValue = optionValue;
				this._tokenId = tokenId;
				this._identifier = null;
				this._validVersions = appliesToVersion;
			}

			// Token: 0x060001B5 RID: 437 RVA: 0x00005E7F File Offset: 0x0000407F
			public OptionInfo(OptionType optionValue, string identifier, SqlVersionFlags validVersions)
			{
				this._optionValue = optionValue;
				this._tokenId = TSqlTokenType.None;
				this._identifier = identifier;
				this._validVersions = validVersions;
			}

			// Token: 0x060001B6 RID: 438 RVA: 0x00005EA3 File Offset: 0x000040A3
			public void GenerateSource(ScriptWriter writer)
			{
				if (this._identifier != null)
				{
					writer.AddIdentifierWithCasing(this._identifier);
					return;
				}
				writer.AddKeyword(this._tokenId);
			}

			// Token: 0x060001B7 RID: 439 RVA: 0x00005EC6 File Offset: 0x000040C6
			public bool IsValidIn(SqlVersionFlags version)
			{
				return (this._validVersions & version) != SqlVersionFlags.None;
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x060001B8 RID: 440 RVA: 0x00005ED6 File Offset: 0x000040D6
			public OptionType Value
			{
				get
				{
					return this._optionValue;
				}
			}

			// Token: 0x04000122 RID: 290
			private readonly OptionType _optionValue;

			// Token: 0x04000123 RID: 291
			private readonly TSqlTokenType _tokenId;

			// Token: 0x04000124 RID: 292
			private readonly string _identifier;

			// Token: 0x04000125 RID: 293
			private readonly SqlVersionFlags _validVersions;
		}
	}
}
