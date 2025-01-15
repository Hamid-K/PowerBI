using System;
using System.Collections.Generic;
using System.Globalization;
using antlr;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000D1 RID: 209
	internal abstract class TSql90ParserBaseInternal : TSql80ParserBaseInternal
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		protected TSql90ParserBaseInternal(TokenBuffer tokenBuf, int k)
			: base(tokenBuf, k)
		{
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0001FEEE File Offset: 0x0001E0EE
		protected TSql90ParserBaseInternal(ParserSharedInputState state, int k)
			: base(state, k)
		{
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0001FEF8 File Offset: 0x0001E0F8
		protected TSql90ParserBaseInternal(TokenStream lexer, int k)
			: base(lexer, k)
		{
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0001FF02 File Offset: 0x0001E102
		public TSql90ParserBaseInternal(bool initialQuotedIdentifiersOn)
			: base(initialQuotedIdentifiersOn)
		{
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0001FF0C File Offset: 0x0001E10C
		protected static AuthenticationTypes AggregateAuthenticationType(AuthenticationTypes current, AuthenticationTypes newOption, IToken token)
		{
			AuthenticationTypes authenticationTypes = current | newOption;
			if (authenticationTypes == current)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
			return authenticationTypes;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0001FF29 File Offset: 0x0001E129
		protected static void CheckForFormatFileOptionInOpenRowsetBulk(int encounteredOptions, TSqlFragment relatedFragment)
		{
			if ((encounteredOptions & 3670272) == 0)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46082", relatedFragment, TSqlParserResource.SQL46082Message, new string[0]);
			}
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0001FF4C File Offset: 0x0001E14C
		protected static PortTypes AggregatePortType(PortTypes current, PortTypes newOption, IToken token)
		{
			PortTypes portTypes = current | newOption;
			if (portTypes == current)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
			return portTypes;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0001FF69 File Offset: 0x0001E169
		protected static void CheckCertificateOptionDupication(CertificateOptionKinds current, CertificateOptionKinds newOption, IToken token)
		{
			if ((current & newOption) == newOption)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0001FF78 File Offset: 0x0001E178
		protected static void CheckIfEndpointOptionAllowed(EndpointProtocolOptions current, EndpointProtocolOptions newOption, EndpointProtocol protocol, IToken token)
		{
			if ((current & newOption) == newOption)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
			if ((protocol == EndpointProtocol.Tcp && (newOption & EndpointProtocolOptions.TcpOptions) != newOption) || (protocol == EndpointProtocol.Http && (newOption & EndpointProtocolOptions.HttpOptions) != newOption))
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0001FFAC File Offset: 0x0001E1AC
		protected static void CheckIfPayloadOptionAllowed(PayloadOptionKinds current, PayloadOptionKinds newOption, EndpointType endpointType, IToken token)
		{
			if (endpointType == EndpointType.TSql)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
			if ((endpointType == EndpointType.Soap && (newOption & PayloadOptionKinds.SoapOptions) != newOption) || (endpointType == EndpointType.DatabaseMirroring && (newOption & PayloadOptionKinds.DatabaseMirroringOptions) != newOption) || (endpointType == EndpointType.ServiceBroker && (newOption & PayloadOptionKinds.ServiceBrokerOptions) != newOption))
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
			if ((current & newOption) == newOption && newOption != PayloadOptionKinds.WebMethod)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00020008 File Offset: 0x0001E208
		protected static SecurityObjectKind ParseSecurityObjectKind(Identifier identifier)
		{
			string text;
			if ((text = identifier.Value.ToUpperInvariant()) != null)
			{
				if (<PrivateImplementationDetails>{BC0BDBD1-A382-4890-888D-B78DC4BCB031}.$$method0x60009e3-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(14);
					dictionary.Add("ASSEMBLY", 0);
					dictionary.Add("CERTIFICATE", 1);
					dictionary.Add("CONTRACT", 2);
					dictionary.Add("DATABASE", 3);
					dictionary.Add("ENDPOINT", 4);
					dictionary.Add("LOGIN", 5);
					dictionary.Add("OBJECT", 6);
					dictionary.Add("ROLE", 7);
					dictionary.Add("ROUTE", 8);
					dictionary.Add("SCHEMA", 9);
					dictionary.Add("SERVER", 10);
					dictionary.Add("SERVICE", 11);
					dictionary.Add("TYPE", 12);
					dictionary.Add("USER", 13);
					<PrivateImplementationDetails>{BC0BDBD1-A382-4890-888D-B78DC4BCB031}.$$method0x60009e3-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{BC0BDBD1-A382-4890-888D-B78DC4BCB031}.$$method0x60009e3-1.TryGetValue(text, ref num))
				{
					switch (num)
					{
					case 0:
						return SecurityObjectKind.Assembly;
					case 1:
						return SecurityObjectKind.Certificate;
					case 2:
						return SecurityObjectKind.Contract;
					case 3:
						return SecurityObjectKind.Database;
					case 4:
						return SecurityObjectKind.Endpoint;
					case 5:
						return SecurityObjectKind.Login;
					case 6:
						return SecurityObjectKind.Object;
					case 7:
						return SecurityObjectKind.Role;
					case 8:
						return SecurityObjectKind.Route;
					case 9:
						return SecurityObjectKind.Schema;
					case 10:
						return SecurityObjectKind.Server;
					case 11:
						return SecurityObjectKind.Service;
					case 12:
						return SecurityObjectKind.Type;
					case 13:
						return SecurityObjectKind.User;
					}
				}
			}
			throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(identifier);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002016C File Offset: 0x0001E36C
		protected static SecurityObjectKind ParseSecurityObjectKind(Identifier identifier1, Identifier identifier2)
		{
			string text;
			if ((text = identifier1.Value.ToUpperInvariant()) != null)
			{
				if (<PrivateImplementationDetails>{BC0BDBD1-A382-4890-888D-B78DC4BCB031}.$$method0x60009e4-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(7);
					dictionary.Add("APPLICATION", 0);
					dictionary.Add("ASYMMETRIC", 1);
					dictionary.Add("AVAILABILITY", 2);
					dictionary.Add("FULLTEXT", 3);
					dictionary.Add("MESSAGE", 4);
					dictionary.Add("SERVER", 5);
					dictionary.Add("SYMMETRIC", 6);
					<PrivateImplementationDetails>{BC0BDBD1-A382-4890-888D-B78DC4BCB031}.$$method0x60009e4-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{BC0BDBD1-A382-4890-888D-B78DC4BCB031}.$$method0x60009e4-1.TryGetValue(text, ref num))
				{
					switch (num)
					{
					case 0:
						TSql80ParserBaseInternal.Match(identifier2, "ROLE");
						return SecurityObjectKind.ApplicationRole;
					case 1:
						TSql80ParserBaseInternal.Match(identifier2, "KEY");
						return SecurityObjectKind.AsymmetricKey;
					case 2:
						TSql80ParserBaseInternal.Match(identifier2, "GROUP");
						return SecurityObjectKind.AvailabilityGroup;
					case 3:
						if (TSql80ParserBaseInternal.TryMatch(identifier2, "CATALOG"))
						{
							return SecurityObjectKind.FullTextCatalog;
						}
						TSql80ParserBaseInternal.Match(identifier2, "STOPLIST");
						return SecurityObjectKind.FullTextStopList;
					case 4:
						TSql80ParserBaseInternal.Match(identifier2, "TYPE");
						return SecurityObjectKind.MessageType;
					case 5:
						TSql80ParserBaseInternal.Match(identifier2, "ROLE");
						return SecurityObjectKind.ServerRole;
					case 6:
						TSql80ParserBaseInternal.Match(identifier2, "KEY");
						return SecurityObjectKind.SymmetricKey;
					}
				}
			}
			throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(identifier1);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000202A4 File Offset: 0x0001E4A4
		protected static SecurityObjectKind ParseSecurityObjectKind(Identifier identifier1, Identifier identifier2, Identifier identifier3)
		{
			string text;
			if ((text = identifier1.Value.ToUpperInvariant()) != null)
			{
				if (text == "XML")
				{
					TSql80ParserBaseInternal.Match(identifier2, "SCHEMA");
					TSql80ParserBaseInternal.Match(identifier3, "COLLECTION");
					return SecurityObjectKind.XmlSchemaCollection;
				}
				if (text == "REMOTE")
				{
					TSql80ParserBaseInternal.Match(identifier2, "SERVICE");
					TSql80ParserBaseInternal.Match(identifier3, "BINDING");
					return SecurityObjectKind.RemoteServiceBinding;
				}
				if (text == "SEARCH")
				{
					TSql80ParserBaseInternal.Match(identifier2, "PROPERTY");
					TSql80ParserBaseInternal.Match(identifier3, "LIST");
					return SecurityObjectKind.SearchPropertyList;
				}
			}
			throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(identifier1);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002033A File Offset: 0x0001E53A
		protected static bool IsXml(Identifier identifier)
		{
			return string.Equals(identifier.Value, "XML", 5);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002034D File Offset: 0x0001E54D
		protected static bool IsSys(Identifier identifier)
		{
			return string.Equals(identifier.Value, "SYS", 5);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00020360 File Offset: 0x0001E560
		protected bool IsStatementIsNext()
		{
			return this.LA(1) != 56 || base.NextTokenMatches("CONVERSATION", 2);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002037C File Offset: 0x0001E57C
		public static string Unquote(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			int num = value.IndexOf('\'');
			int num2 = value.LastIndexOf('\'');
			string text = value;
			if (num == -1 || num2 == num)
			{
				return text;
			}
			if (num < 2 && num2 != num && num2 == value.Length - 1)
			{
				if (num == 1)
				{
					if (value.get_Chars(0) == 'N')
					{
						text = value.Substring(num + 1, num2 - num - 1);
					}
				}
				else
				{
					text = value.Substring(num + 1, num2 - num);
				}
			}
			return text;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x000203F4 File Offset: 0x0001E5F4
		protected static EncryptionAlgorithmPreference RecognizeAesOrRc4(Identifier id, IToken tokenForError)
		{
			string text = TSql90ParserBaseInternal.Unquote(id.Value);
			if (string.Equals(text, "AES", 5))
			{
				return EncryptionAlgorithmPreference.Aes;
			}
			if (string.Equals(text, "RC4", 5))
			{
				return EncryptionAlgorithmPreference.Rc4;
			}
			throw new TSqlParseErrorException(TSql80ParserBaseInternal.GetUnexpectedTokenError(tokenForError));
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00020438 File Offset: 0x0001E638
		protected static AuthenticationProtocol RecognizeAuthenticationProtocol(Identifier id, IToken tokenForError)
		{
			string text = TSql90ParserBaseInternal.Unquote(id.Value);
			if (string.Equals(text, "NTLM", 5))
			{
				return AuthenticationProtocol.WindowsNtlm;
			}
			if (string.Equals(text, "KERBEROS", 5))
			{
				return AuthenticationProtocol.WindowsKerberos;
			}
			if (string.Equals(text, "NEGOTIATE", 5))
			{
				return AuthenticationProtocol.WindowsNegotiate;
			}
			throw new TSqlParseErrorException(TSql80ParserBaseInternal.GetUnexpectedTokenError(tokenForError));
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002048C File Offset: 0x0001E68C
		protected static void RecognizeAlterLoginSecAdminPasswordOption(IToken token, PasswordAlterPrincipalOption astNode)
		{
			if (TSql80ParserBaseInternal.TryMatch(token, "MUST_CHANGE"))
			{
				if (astNode.MustChange)
				{
					throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
				}
				astNode.MustChange = true;
			}
			else if (TSql80ParserBaseInternal.TryMatch(token, "HASHED"))
			{
				if (astNode.Hashed)
				{
					throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
				}
				astNode.Hashed = true;
			}
			else
			{
				TSql80ParserBaseInternal.Match(token, "UNLOCK");
				if (astNode.Unlock)
				{
					throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
				}
				astNode.Unlock = true;
			}
			TSql80ParserBaseInternal.UpdateTokenInfo(astNode, token);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002050B File Offset: 0x0001E70B
		protected static TValue EnableDisableMatcher<TValue>(IToken token, TValue enableValue, TValue disableValue)
		{
			if (TSql80ParserBaseInternal.TryMatch(token, "ENABLE"))
			{
				return enableValue;
			}
			TSql80ParserBaseInternal.Match(token, "DISABLE");
			return disableValue;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00020528 File Offset: 0x0001E728
		protected static void AddConstraintToComputedColumn(ConstraintDefinition constraint, ColumnDefinition column)
		{
			bool flag = false;
			if (constraint is NullableConstraintDefinition)
			{
				NullableConstraintDefinition nullableConstraintDefinition = (NullableConstraintDefinition)constraint;
				flag = nullableConstraintDefinition.Nullable;
			}
			if ((!column.IsPersisted && !(constraint is UniqueConstraintDefinition)) || (column.IsPersisted && flag))
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46011", constraint, TSqlParserResource.SQL46011Message, new string[0]);
			}
			TSql80ParserBaseInternal.AddAndUpdateTokenInfo<ConstraintDefinition>(column, column.Constraints, constraint);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002058B File Offset: 0x0001E78B
		protected static IndexAffectingStatement GetAlterIndexStatementKind(AlterIndexStatement alterIndex)
		{
			if (alterIndex.AlterIndexType == AlterIndexType.Reorganize)
			{
				return IndexAffectingStatement.AlterIndexReorganize;
			}
			if (alterIndex.Partition != null && !alterIndex.Partition.All)
			{
				return IndexAffectingStatement.AlterIndexRebuildOnePartition;
			}
			return IndexAffectingStatement.AlterIndexRebuildAllPartitions;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000205B0 File Offset: 0x0001E7B0
		protected static void CheckForDistinctInWindowedAggregate(FunctionCall functionCall, IToken distinctToken)
		{
			if (functionCall.UniqueRowFilter == UniqueRowFilter.Distinct && functionCall.OverClause != null && distinctToken != null)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46086", distinctToken, TSqlParserResource.SQL46086Message, new string[0]);
			}
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x000205DC File Offset: 0x0001E7DC
		protected Literal CreateIntLiteralFromNumericToken(IToken token, int textOffset, int textLength)
		{
			IntegerLiteral integerLiteral = base.FragmentFactory.CreateFragment<IntegerLiteral>();
			TSql80ParserBaseInternal.UpdateTokenInfo(integerLiteral, token);
			integerLiteral.Value = token.getText().Substring(textOffset, textLength);
			return integerLiteral;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00020610 File Offset: 0x0001E810
		protected bool SplitNumericIntoIpParts(IToken token, out Literal frag1, out Literal frag2)
		{
			string text = token.getText();
			int length = text.Length;
			int num = text.IndexOf('.');
			if (num == 0)
			{
				frag1 = null;
				frag2 = this.CreateIntLiteralFromNumericToken(token, 1, length - 1);
				return false;
			}
			if (num == length - 1)
			{
				frag1 = this.CreateIntLiteralFromNumericToken(token, 0, num);
				frag2 = null;
				return false;
			}
			frag1 = this.CreateIntLiteralFromNumericToken(token, 0, num);
			frag2 = this.CreateIntLiteralFromNumericToken(token, num + 1, length - num - 1);
			return true;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002067C File Offset: 0x0001E87C
		protected Literal GetIPv4FragmentFromDotNumberNumeric(IToken token)
		{
			Literal literal;
			Literal literal2;
			this.SplitNumericIntoIpParts(token, out literal, out literal2);
			if (literal != null || literal2 == null)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
			return literal2;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000206A4 File Offset: 0x0001E8A4
		protected Literal GetIPv4FragmentFromNumberDotNumeric(IToken token)
		{
			Literal literal;
			Literal literal2;
			this.SplitNumericIntoIpParts(token, out literal, out literal2);
			if (literal == null || literal2 != null)
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
			return literal;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000206CB File Offset: 0x0001E8CB
		protected void GetIPv4FragmentsFromNumberDotNumberNumeric(IToken token, out Literal frag1, out Literal frag2)
		{
			if (!this.SplitNumericIntoIpParts(token, out frag1, out frag2))
			{
				throw TSql80ParserBaseInternal.GetUnexpectedTokenErrorException(token);
			}
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000206E0 File Offset: 0x0001E8E0
		protected static void CheckDmlTriggerActionDuplication(int current, TriggerAction vTriggerAction)
		{
			if ((current & (1 << (int)vTriggerAction.TriggerActionType)) != 0)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46090", vTriggerAction, TSqlParserResource.SQL46090Message, new string[] { vTriggerAction.TriggerActionType.ToString() });
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00020727 File Offset: 0x0001E927
		protected static void UpdateDmlTriggerActionEncounteredOptions(ref int encountered, TriggerAction vTriggerAction)
		{
			encountered |= 1 << (int)vTriggerAction.TriggerActionType;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002073C File Offset: 0x0001E93C
		protected static void ThrowIfInvalidListenerPortValue(Literal value)
		{
			int num;
			if (!int.TryParse(value.Value, 7, CultureInfo.InvariantCulture, ref num) || num > 32767 || num < 1024)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46087", value, TSqlParserResource.SQL46087Message, new string[] { value.Value });
			}
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00020790 File Offset: 0x0001E990
		protected static void ThrowIfMaxdopValueOutOfRange(Literal value)
		{
			int num;
			if (!int.TryParse(value.Value, 7, CultureInfo.InvariantCulture, ref num) || num > 32767 || num < 0)
			{
				TSql80ParserBaseInternal.ThrowParseErrorException("SQL46091", value, TSqlParserResource.SQL46091Message, new string[] { value.Value });
			}
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000207E0 File Offset: 0x0001E9E0
		protected EventTypeContainer CreateEventTypeContainer(EventNotificationEventType eventTypeValue, IToken token)
		{
			EventTypeContainer eventTypeContainer = base.FragmentFactory.CreateFragment<EventTypeContainer>();
			eventTypeContainer.EventType = eventTypeValue;
			TSql80ParserBaseInternal.UpdateTokenInfo(eventTypeContainer, token);
			return eventTypeContainer;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00020808 File Offset: 0x0001EA08
		protected EventGroupContainer CreateEventGroupContainer(EventNotificationEventGroup eventGroupValue, IToken token)
		{
			EventGroupContainer eventGroupContainer = base.FragmentFactory.CreateFragment<EventGroupContainer>();
			eventGroupContainer.EventGroup = eventGroupValue;
			TSql80ParserBaseInternal.UpdateTokenInfo(eventGroupContainer, token);
			return eventGroupContainer;
		}

		// Token: 0x04000723 RID: 1827
		protected const int BulkInsertOptionsProhibitedInOpenRowset = 34866;

		// Token: 0x04000724 RID: 1828
		private const int CheckForFormatFileOptionInOpenRowsetBulkMask = 3670272;
	}
}
