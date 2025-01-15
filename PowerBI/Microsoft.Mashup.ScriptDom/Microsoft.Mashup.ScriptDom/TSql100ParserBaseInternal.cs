using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000D2 RID: 210
	internal abstract class TSql100ParserBaseInternal : TSql90ParserBaseInternal
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x00020830 File Offset: 0x0001EA30
		protected TSql100ParserBaseInternal(TokenBuffer tokenBuf, int k)
			: base(tokenBuf, k)
		{
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002083A File Offset: 0x0001EA3A
		protected TSql100ParserBaseInternal(ParserSharedInputState state, int k)
			: base(state, k)
		{
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00020844 File Offset: 0x0001EA44
		protected TSql100ParserBaseInternal(TokenStream lexer, int k)
			: base(lexer, k)
		{
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002084E File Offset: 0x0001EA4E
		public TSql100ParserBaseInternal(bool initialQuotedIdentifiersOn)
			: base(initialQuotedIdentifiersOn)
		{
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00020858 File Offset: 0x0001EA58
		protected AutoCleanupChangeTrackingOptionDetail CreateAutoCleanupDetail(IToken firstToken, IToken lastToken, ref bool autoCleanupEncountered)
		{
			TSql80ParserBaseInternal.Match(firstToken, "AUTO_CLEANUP");
			if (autoCleanupEncountered)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46050", firstToken, TSqlParserResource.SQL46050Message, new string[] { firstToken.getText() });
			}
			autoCleanupEncountered = true;
			AutoCleanupChangeTrackingOptionDetail autoCleanupChangeTrackingOptionDetail = base.FragmentFactory.CreateFragment<AutoCleanupChangeTrackingOptionDetail>();
			TSql80ParserBaseInternal.UpdateTokenInfo(autoCleanupChangeTrackingOptionDetail, firstToken);
			TSql80ParserBaseInternal.UpdateTokenInfo(autoCleanupChangeTrackingOptionDetail, lastToken);
			autoCleanupChangeTrackingOptionDetail.IsOn = lastToken.Type == 105;
			return autoCleanupChangeTrackingOptionDetail;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000208C4 File Offset: 0x0001EAC4
		protected static SqlDataTypeOption ParseDataType100(string token)
		{
			string text;
			if ((text = token.ToUpperInvariant()) != null)
			{
				if (text == "DATE")
				{
					return SqlDataTypeOption.Date;
				}
				if (text == "TIME")
				{
					return SqlDataTypeOption.Time;
				}
				if (text == "DATETIME2")
				{
					return SqlDataTypeOption.DateTime2;
				}
				if (text == "DATETIMEOFFSET")
				{
					return SqlDataTypeOption.DateTimeOffset;
				}
			}
			return TSql80ParserBaseInternal.ParseDataType(token);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00020923 File Offset: 0x0001EB23
		protected static void CheckBrokerPriorityParameterDuplication(int current, BrokerPriorityParameterType newOption, IToken token)
		{
			if ((current & (1 << (int)newOption)) != 0)
			{
				TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(token);
			}
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00020935 File Offset: 0x0001EB35
		protected static void UpdateBrokerPriorityEncounteredOptions(ref int encountered, BrokerPriorityParameter vBrokerPriorityParameter)
		{
			encountered |= 1 << (int)vBrokerPriorityParameter.ParameterType;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00020947 File Offset: 0x0001EB47
		protected static void CheckBoundingBoxParameterDuplication(int current, BoundingBoxParameterType newOption, IToken token)
		{
			if ((current & (1 << (int)newOption)) != 0)
			{
				TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(token);
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00020959 File Offset: 0x0001EB59
		protected static void UpdateBoundingBoxParameterEncounteredOptions(ref int encountered, BoundingBoxParameter vBoundingBoxParameter)
		{
			encountered |= 1 << (int)vBoundingBoxParameter.Parameter;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002096C File Offset: 0x0001EB6C
		protected static void CheckIfValidSpatialIndexOptionValue(IndexAffectingStatement statement, IndexOption option)
		{
			IndexStateOption indexStateOption = option as IndexStateOption;
			if (indexStateOption != null && indexStateOption.OptionKind == IndexOptionKind.IgnoreDupKey && indexStateOption.OptionState == OptionState.On)
			{
				TSql80ParserBaseInternal.ThrowWrongIndexOptionError(statement, indexStateOption);
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002099C File Offset: 0x0001EB9C
		protected static void SetFileStreamStorageOption(ColumnStorageOptions storageOptions, IToken fileStreamToken, DataTypeReference columnType, IndexAffectingStatement statementType)
		{
			if (statementType != IndexAffectingStatement.AlterTableAddElement && statementType != IndexAffectingStatement.CreateTable)
			{
				TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(fileStreamToken);
				return;
			}
			SqlDataTypeReference sqlDataTypeReference = columnType as SqlDataTypeReference;
			if (sqlDataTypeReference != null && sqlDataTypeReference.SqlDataTypeOption == SqlDataTypeOption.VarBinary && sqlDataTypeReference.Parameters.Count == 1 && sqlDataTypeReference.Parameters[0].LiteralType == LiteralType.Max)
			{
				storageOptions.IsFileStream = true;
				return;
			}
			TSql80ParserBaseInternal.ThrowParseErrorException("SQL46051", fileStreamToken, TSqlParserResource.SQL46051Message, new string[0]);
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00020A0C File Offset: 0x0001EC0C
		protected static void SetSparseStorageOption(ColumnStorageOptions columnStorage, SparseColumnOption option, IToken token, IndexAffectingStatement statementType)
		{
			if (statementType == IndexAffectingStatement.AlterTableAddElement || statementType == IndexAffectingStatement.CreateTable || statementType == IndexAffectingStatement.DeclareTableVariable)
			{
				columnStorage.SparseOption = option;
				return;
			}
			TSql80ParserBaseInternal.ThrowIncorrectSyntaxErrorException(token);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00020A38 File Offset: 0x0001EC38
		protected static void CheckComparisonOperandForIndexFilter(ScalarExpression rightOperand, bool convertAllowed)
		{
			UnaryExpression unaryExpression = rightOperand as UnaryExpression;
			if (unaryExpression != null)
			{
				TSql100ParserBaseInternal.CheckComparisonOperandForIndexFilter(unaryExpression.Expression, convertAllowed);
				return;
			}
			Literal literal = rightOperand as Literal;
			if (literal != null && literal.LiteralType != LiteralType.Max)
			{
				return;
			}
			ParenthesisExpression parenthesisExpression = rightOperand as ParenthesisExpression;
			if (parenthesisExpression != null)
			{
				TSql100ParserBaseInternal.CheckComparisonOperandForIndexFilter(parenthesisExpression.Expression, convertAllowed);
				return;
			}
			if (convertAllowed)
			{
				ConvertCall convertCall = rightOperand as ConvertCall;
				if (convertCall != null)
				{
					TSql100ParserBaseInternal.CheckComparisonOperandForIndexFilter(convertCall.Parameter, false);
					return;
				}
				CastCall castCall = rightOperand as CastCall;
				if (castCall != null)
				{
					TSql100ParserBaseInternal.CheckComparisonOperandForIndexFilter(castCall.Parameter, false);
					return;
				}
			}
			TSql80ParserBaseInternal.ThrowParseErrorException("SQL46059", rightOperand, TSqlParserResource.SQL46059Message, new string[0]);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00020AD4 File Offset: 0x0001ECD4
		protected static void CheckPartitionAllSpecifiedForIndexRebuild(PartitionSpecifier partitionSpecifier, IList<IndexOption> indexOptions)
		{
			if (partitionSpecifier == null)
			{
				foreach (IndexOption indexOption in indexOptions)
				{
					DataCompressionOption dataCompressionOption = indexOption as DataCompressionOption;
					if (dataCompressionOption != null && dataCompressionOption.PartitionRanges.Count > 0)
					{
						TSql80ParserBaseInternal.ThrowParseErrorException("SQL46061", indexOption, TSqlParserResource.SQL46061Message, new string[0]);
					}
				}
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00020B48 File Offset: 0x0001ED48
		protected static void ThrowIfWrongGuidFormat(Literal literal)
		{
			string text = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";
			if (!Regex.IsMatch(literal.Value, text, 512))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46055", literal, TSqlParserResource.SQL46055Message, new string[0]);
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00020B84 File Offset: 0x0001ED84
		protected static void ThrowIfTooLargeAuditFileSize(Literal size, int shift)
		{
			ulong num;
			if (!ulong.TryParse(size.Value, 7, CultureInfo.InvariantCulture, ref num) || num > 18446744073709551615UL >> 20 + shift)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46054", size, TSqlParserResource.SQL46054Message, new string[0]);
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00020BCC File Offset: 0x0001EDCC
		protected static void CheckForCellsPerObjectValueRange(Literal value)
		{
			int num;
			if (!int.TryParse(value.Value, 7, CultureInfo.InvariantCulture, ref num) || num < 1 || num > 8192)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46073", value, TSqlParserResource.SQL46073Message, new string[] { value.Value });
			}
		}
	}
}
