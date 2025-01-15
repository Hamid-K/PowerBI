using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200061F RID: 1567
	internal class OdbcOptions
	{
		// Token: 0x06003111 RID: 12561 RVA: 0x00095327 File Offset: 0x00093527
		private OdbcOptions(RecordValue record, RecordValue sqlCapabilities)
		{
			this.record = record;
			this.sqlCapabilities = sqlCapabilities;
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x00095340 File Offset: 0x00093540
		public int? MaxParameters
		{
			get
			{
				return OdbcOptions.GetInt32(this.sqlCapabilities, "MaxParameters", null);
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x00095366 File Offset: 0x00093566
		public bool SupportsTop
		{
			get
			{
				return OdbcOptions.GetBool(this.sqlCapabilities, "SupportsTop", false);
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x00095379 File Offset: 0x00093579
		public bool SupportsDerivedTable
		{
			get
			{
				return OdbcOptions.GetBool(this.sqlCapabilities, "SupportsDerivedTable", false);
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x0009538C File Offset: 0x0009358C
		public Odbc32.SQL_SP? SupportedPredicates
		{
			get
			{
				int? @int = OdbcOptions.GetInt32(this.sqlCapabilities, "SupportedPredicates", null);
				if (@int == null)
				{
					return null;
				}
				return new Odbc32.SQL_SP?((Odbc32.SQL_SP)@int.GetValueOrDefault());
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x000953D4 File Offset: 0x000935D4
		public Odbc32.SQL_AF? SupportedAggregateFunctions
		{
			get
			{
				int? @int = OdbcOptions.GetInt32(this.sqlCapabilities, "SupportedAggregateFunctions", null);
				if (@int == null)
				{
					return null;
				}
				return new Odbc32.SQL_AF?((Odbc32.SQL_AF)@int.GetValueOrDefault());
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x0009541C File Offset: 0x0009361C
		public Odbc32.SQL_GB? GroupByCapabilities
		{
			get
			{
				int? @int = OdbcOptions.GetInt32(this.sqlCapabilities, "GroupByCapabilities", null);
				if (@int == null)
				{
					return null;
				}
				return new Odbc32.SQL_GB?((Odbc32.SQL_GB)@int.GetValueOrDefault());
			}
		}

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x00095464 File Offset: 0x00093664
		public Odbc32.SQL_SRJO? SupportedSql92Joins
		{
			get
			{
				int? @int = OdbcOptions.GetInt32(this.sqlCapabilities, "SupportedSql92Joins", null);
				if (@int == null)
				{
					return null;
				}
				return new Odbc32.SQL_SRJO?((Odbc32.SQL_SRJO)@int.GetValueOrDefault());
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x000954AC File Offset: 0x000936AC
		public Odbc32.SQL_SVE? SupportedValueExpressions
		{
			get
			{
				int? @int = OdbcOptions.GetInt32(this.sqlCapabilities, "SupportedValueExpressions", null);
				if (@int == null)
				{
					return null;
				}
				return new Odbc32.SQL_SVE?((Odbc32.SQL_SVE)@int.GetValueOrDefault());
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x000954F2 File Offset: 0x000936F2
		public bool SupportsStringLiterals
		{
			get
			{
				return OdbcOptions.GetBool(this.sqlCapabilities, "SupportsStringLiterals", false);
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x00095505 File Offset: 0x00093705
		public bool SupportsOdbcDateLiterals
		{
			get
			{
				return OdbcOptions.GetBool(this.sqlCapabilities, "SupportsOdbcDateLiterals", false);
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x00095518 File Offset: 0x00093718
		public bool SupportsOdbcTimeLiterals
		{
			get
			{
				return OdbcOptions.GetBool(this.sqlCapabilities, "SupportsOdbcTimeLiterals", false);
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x0009552B File Offset: 0x0009372B
		public bool SupportsOdbcTimestampLiterals
		{
			get
			{
				return OdbcOptions.GetBool(this.sqlCapabilities, "SupportsOdbcTimestampLiterals", false);
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x0009553E File Offset: 0x0009373E
		public bool SupportsNumericLiterals
		{
			get
			{
				return OdbcOptions.GetBool(this.sqlCapabilities, "SupportsNumericLiterals", false);
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x00095551 File Offset: 0x00093751
		public bool PrepareStatements
		{
			get
			{
				return OdbcOptions.GetBool(this.sqlCapabilities, "PrepareStatements", false);
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x00095564 File Offset: 0x00093764
		public string LimitClauseLocation
		{
			get
			{
				return OdbcOptions.GetString(this.sqlCapabilities, "LimitClauseLocation", null);
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x00095578 File Offset: 0x00093778
		public Odbc32.SQL_RETURN_ESCAPE_CLAUSE? ReturnEscapeClause
		{
			get
			{
				int? @int = OdbcOptions.GetInt32(this.sqlCapabilities, "ReturnEscapeClause", null);
				if (@int == null)
				{
					return null;
				}
				return new Odbc32.SQL_RETURN_ESCAPE_CLAUSE?((Odbc32.SQL_RETURN_ESCAPE_CLAUSE)@int.GetValueOrDefault());
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x000955C0 File Offset: 0x000937C0
		public LimitClauseKind.LimitClauseKindType LimitClauseKind
		{
			get
			{
				return (LimitClauseKind.LimitClauseKindType)OdbcOptions.GetInt32(this.sqlCapabilities, "LimitClauseKind", new int?(0)).Value;
			}
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06003123 RID: 12579 RVA: 0x000955EB File Offset: 0x000937EB
		public string Sql92Translation
		{
			get
			{
				return OdbcOptions.GetString(this.sqlCapabilities, "Sql92Translation", null);
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x000955FE File Offset: 0x000937FE
		public bool SupportsLimitZero
		{
			get
			{
				return OdbcOptions.GetBool(this.sqlCapabilities, "SupportsLimitZero", false);
			}
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x00095611 File Offset: 0x00093811
		public string NativeQuerySchemaStrategy
		{
			get
			{
				return OdbcOptions.GetString(this.sqlCapabilities, "NativeQuerySchemaStrategy", null);
			}
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x00095624 File Offset: 0x00093824
		public bool TryGetOptionalCapability(string capability, out Value value)
		{
			Value value2;
			if (this.sqlCapabilities.TryGetValue("Optional", out value2) && value2.IsRecord && value2.TryGetValue(capability, out value))
			{
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x00095660 File Offset: 0x00093860
		public Odbc32.SQL_SC? Sql92Conformance
		{
			get
			{
				int? @int = OdbcOptions.GetInt32(this.sqlCapabilities, "Sql92Conformance", null);
				if (@int != null)
				{
					switch (@int.Value)
					{
					case 0:
					case 1:
					case 2:
					case 4:
					case 8:
						return new Odbc32.SQL_SC?((Odbc32.SQL_SC)@int.Value);
					}
					throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("Sql92Conformance"), this.sqlCapabilities["Sql92Conformance"], null);
				}
				return null;
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x000956FC File Offset: 0x000938FC
		public IDictionary<string, string> StringLiteralEscapeCharacters
		{
			get
			{
				if (this.stringLiteralEscapeCharacters == null)
				{
					this.stringLiteralEscapeCharacters = new Dictionary<string, string>();
					bool flag = false;
					bool flag2 = false;
					Value value;
					if (this.sqlCapabilities.TryGetValue("StringLiteralEscapeCharacters", out value) && !value.IsNull)
					{
						if (value.IsList)
						{
							using (IEnumerator<IValueReference> enumerator = value.AsList.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									IValueReference valueReference = enumerator.Current;
									Value value2 = (Value)valueReference;
									if (value2.IsText && !flag)
									{
										flag2 = true;
										string text = value2.AsText.ToString();
										try
										{
											this.stringLiteralEscapeCharacters.Add(text, text + text);
											continue;
										}
										catch (ArgumentException)
										{
											throw ValueException.DuplicateField(text);
										}
									}
									if (value2.IsList && !flag2)
									{
										flag = true;
										ListValue asList = value2.AsList;
										if (asList.Count == 2 && asList.Item0.IsText && asList.Item1.IsText)
										{
											string text2 = asList.Item0.AsText.ToString();
											string text3 = asList.Item1.AsText.ToString();
											try
											{
												this.stringLiteralEscapeCharacters.Add(text2, text3);
												continue;
											}
											catch (ArgumentException)
											{
												throw ValueException.DuplicateField(text2);
											}
										}
										throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("StringLiteralEscapeCharacters"), value, null);
									}
									throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("StringLiteralEscapeCharacters"), value, null);
								}
								goto IL_0175;
							}
						}
						throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("StringLiteralEscapeCharacters"), value, null);
					}
				}
				IL_0175:
				return this.stringLiteralEscapeCharacters;
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000958D0 File Offset: 0x00093AD0
		public bool UseEmbeddedDriver
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "UseEmbeddedDriver", false);
			}
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000958E3 File Offset: 0x00093AE3
		public bool UseSoftNumbers
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "SoftNumbers", false);
			}
		}

		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000958F6 File Offset: 0x00093AF6
		public RecordValue SQLGetInfo
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "SQLGetInfo", RecordValue.Empty).AsRecord;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x00095912 File Offset: 0x00093B12
		public Value ImplicitTypeConversions
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "ImplicitTypeConversions", Value.Null);
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x00095929 File Offset: 0x00093B29
		public bool HideNativeQuery
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "HideNativeQuery", false);
			}
		}

		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x0009593C File Offset: 0x00093B3C
		public Value SQLGetTypeInfo
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "SQLGetTypeInfo", Value.Null);
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x00095953 File Offset: 0x00093B53
		public bool TolerateConcatOverflow
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "TolerateConcatOverflow", false);
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x00095966 File Offset: 0x00093B66
		public Value OnError
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "OnError", Value.Null);
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x0009597D File Offset: 0x00093B7D
		public RecordValue AstVisitor
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "AstVisitor", RecordValue.Empty).AsRecord;
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x00095999 File Offset: 0x00093B99
		public RecordValue SQLGetFunctions
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "SQLGetFunctions", RecordValue.Empty).AsRecord;
			}
		}

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x06003133 RID: 12595 RVA: 0x000959B5 File Offset: 0x00093BB5
		public Value CredentialConnectionString
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "CredentialConnectionString", Value.Null);
			}
		}

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x000959CC File Offset: 0x00093BCC
		public bool ClientConnectionPooling
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "ClientConnectionPooling", false);
			}
		}

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x000959DF File Offset: 0x00093BDF
		public bool CreateNavigationProperties
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "CreateNavigationProperties", true);
			}
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x000959F2 File Offset: 0x00093BF2
		public bool HierarchicalNavigation
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "HierarchicalNavigation", false);
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x00095A05 File Offset: 0x00093C05
		public int? ConnectionTimeout
		{
			get
			{
				return OdbcOptions.GetSeconds(OdbcOptions.GetTimeSpan(this.record, "ConnectionTimeout"));
			}
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x00095A1C File Offset: 0x00093C1C
		public int? CommandTimeout
		{
			get
			{
				return OdbcOptions.GetSeconds(OdbcOptions.GetTimeSpan(this.record, "CommandTimeout"));
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x00095A33 File Offset: 0x00093C33
		public Value SQLColumns
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "SQLColumns", Value.Null);
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x00095A4A File Offset: 0x00093C4A
		public Value SQLTables
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "SQLTables", Value.Null);
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x00095A61 File Offset: 0x00093C61
		public bool SqlCompatibleWindowsAuth
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "SqlCompatibleWindowsAuth", true);
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x00095A74 File Offset: 0x00093C74
		public Value CredentialHandler
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "CredentialHandler", Value.Null);
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x0600313D RID: 12605 RVA: 0x00095A8B File Offset: 0x00093C8B
		public RecordValue DefaultTypeParameters
		{
			get
			{
				return OdbcOptions.GetValueOrDefault(this.record, "DefaultTypeParameters", RecordValue.Empty).AsRecord;
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x0600313E RID: 12606 RVA: 0x00095AA7 File Offset: 0x00093CA7
		public bool SupportsIncrementalNavigation
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "SupportsIncrementalNavigation", false);
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x00095ABA File Offset: 0x00093CBA
		public bool CancelQueryExplicitly
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "CancelQueryExplicitly", false);
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x00095ACD File Offset: 0x00093CCD
		public OdbcTableTypes TableTypes
		{
			get
			{
				return OdbcTableTypes.LoadFrom(OdbcOptions.GetValueOrDefault(this.record, "TableTypes", Value.Null));
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x00095AE9 File Offset: 0x00093CE9
		public string SetUserQuery
		{
			get
			{
				return OdbcOptions.GetString(this.record, "SetUserQuery", null);
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x00095AFC File Offset: 0x00093CFC
		public string ClearUserQuery
		{
			get
			{
				return OdbcOptions.GetString(this.record, "ClearUserQuery", null);
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x00095B10 File Offset: 0x00093D10
		public IDictionary<string, string> UserContextCredentialProperties
		{
			get
			{
				if (this.userContextCredentialProperties == null)
				{
					RecordValue asRecord = OdbcOptions.GetValueOrDefault(this.record, "UserContextCredentialProperties", RecordValue.Empty).AsRecord;
					this.userContextCredentialProperties = new Dictionary<string, string>(asRecord.Count);
					for (int i = 0; i < asRecord.Count; i++)
					{
						this.userContextCredentialProperties[asRecord.Keys[i]] = asRecord[i].AsString;
					}
				}
				return this.userContextCredentialProperties;
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x00095B8B File Offset: 0x00093D8B
		public bool TryRecoverDateDiff
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "TryRecoverDateDiff", false);
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x00095B9E File Offset: 0x00093D9E
		public bool TryRecoverCoalesce
		{
			get
			{
				return OdbcOptions.GetBool(this.record, "TryRecoverCoalesce", false);
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x00095BB4 File Offset: 0x00093DB4
		public int? FractionalSecondsScale
		{
			get
			{
				int? @int = OdbcOptions.GetInt32(this.sqlCapabilities, "FractionalSecondsScale", null);
				if (@int != null && (@int.Value > 9 || @int.Value < 1))
				{
					throw ValueException.NewExpressionError<Message1>(Strings.OdbcDataSourceInvalidSqlCapability("FractionalSecondsScale"), null, null);
				}
				return @int;
			}
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x00095C0C File Offset: 0x00093E0C
		public static OdbcOptions CreateDataSourceOptionsFromValue(Value options, IEngineHost host)
		{
			return OdbcOptions.CreateDataSourceOptionsFromValue(options, OdbcOptions.IsPrivileged(host));
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x00095C1C File Offset: 0x00093E1C
		public static OdbcOptions CreateDataSourceOptionsFromValue(Value options, bool isPrivileged)
		{
			HashSet<string> hashSet = new HashSet<string>(OdbcOptions.baseSupportedSqlCapabilities);
			HashSet<string> hashSet2 = new HashSet<string>(OdbcOptions.baseSupportedOptions);
			OdbcOptions.AddExtensionsSupportedOptions(isPrivileged, hashSet2);
			OdbcOptions.AddExtensionsSqlCapabilities(isPrivileged, hashSet);
			return OdbcOptions.CreateFromValue(options, hashSet, hashSet2);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x00095C58 File Offset: 0x00093E58
		public static OdbcOptions CreateQueryOptionsFromValue(Value options, IEngineHost host)
		{
			HashSet<string> hashSet = new HashSet<string> { "ClientConnectionPooling", "SQLGetInfo", "HideNativeQuery", "ConnectionTimeout", "CommandTimeout", "SqlCompatibleWindowsAuth" };
			OdbcOptions.AddExtensionsSupportedOptions(OdbcOptions.IsPrivileged(host), hashSet);
			return OdbcOptions.CreateFromValue(options, new HashSet<string>(), hashSet);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x00095CCB File Offset: 0x00093ECB
		private static bool IsPrivileged(IEngineHost host)
		{
			return host.QueryService<IExtensibilityService>() != null;
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x00095CD8 File Offset: 0x00093ED8
		private static OdbcOptions CreateFromValue(Value options, HashSet<string> supportedSqlCapabilities, HashSet<string> supportedOptions)
		{
			if (options.IsNull)
			{
				return OdbcOptions.Empty;
			}
			RecordValue asRecord = options.AsRecord;
			OdbcOptions.EnsureSupported(asRecord, supportedOptions, Strings.OdbcDataSourceUnknownNamedOption);
			Value value;
			RecordValue recordValue;
			if (asRecord.TryGetValue("SqlCapabilities", out value) && !value.IsNull)
			{
				recordValue = value.AsRecord;
				OdbcOptions.EnsureSupported(recordValue, supportedSqlCapabilities, Strings.OdbcDataSourceUnknownSqlCapability);
			}
			else
			{
				recordValue = RecordValue.Empty;
			}
			return new OdbcOptions(asRecord, recordValue);
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x00095D48 File Offset: 0x00093F48
		public bool TryGetConnectionStringAdornment(ConnectionStringHandler connectionStringHandler, out ConnectionStringAdornment credentials)
		{
			if (this.CredentialConnectionString != null && !this.CredentialConnectionString.IsNull)
			{
				string @string = connectionStringHandler.GetString(this.CredentialConnectionString);
				credentials = new ConnectionStringAdornment(@string);
				return true;
			}
			credentials = null;
			return false;
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x00095D88 File Offset: 0x00093F88
		private static void EnsureSupported(RecordValue options, HashSet<string> supported, string errorMessage)
		{
			ListValue listValue = ListValue.New(options.Keys.Where((string key) => !supported.Contains(key)).Select(new Func<string, TextValue>(TextValue.New)).Cast<IValueReference>());
			if (!listValue.IsEmpty)
			{
				throw ValueException.NewExpressionError(errorMessage, listValue, null);
			}
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x00095DE8 File Offset: 0x00093FE8
		private static void AddExtensionsSupportedOptions(bool isPrivileged, HashSet<string> supportedOptions)
		{
			if (isPrivileged)
			{
				supportedOptions.Add("UseEmbeddedDriver");
				supportedOptions.Add("CredentialConnectionString");
				supportedOptions.Add("ClientConnectionPooling");
				supportedOptions.Add("AstVisitor");
				supportedOptions.Add("OnError");
				supportedOptions.Add("SQLColumns");
				supportedOptions.Add("SQLTables");
				supportedOptions.Add("CredentialHandler");
				supportedOptions.Add("CancelQueryExplicitly");
				supportedOptions.Add("TryRecoverDateDiff");
				supportedOptions.Add("TryRecoverCoalesce");
				supportedOptions.Add("TableTypes");
				supportedOptions.Add("SetUserQuery");
				supportedOptions.Add("ClearUserQuery");
				supportedOptions.Add("UserContextCredentialProperties");
				supportedOptions.Add("SQLGetTypeInfo");
			}
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x00095EBC File Offset: 0x000940BC
		private static void AddExtensionsSqlCapabilities(bool isPrivileged, HashSet<string> sqlCapabilities)
		{
			if (isPrivileged)
			{
				sqlCapabilities.Add("SupportsStringLiterals");
				sqlCapabilities.Add("StringLiteralEscapeCharacters");
				sqlCapabilities.Add("LimitClauseLocation");
				sqlCapabilities.Add("SupportsLimitZero");
				sqlCapabilities.Add("NativeQuerySchemaStrategy");
				sqlCapabilities.Add("MaxParameters");
				sqlCapabilities.Add("Sql92Translation");
			}
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x00095F20 File Offset: 0x00094120
		private static string[] GetStringArray(RecordValue record, string key, string[] defaultValue)
		{
			Value value;
			if (record.TryGetValue(key, out value) && !value.IsNull)
			{
				return value.AsList.Select((IValueReference i) => i.Value.AsString).ToArray<string>();
			}
			return defaultValue;
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x00095F74 File Offset: 0x00094174
		private static bool GetBool(RecordValue record, string key, bool defaultValue)
		{
			Value value;
			if (record.TryGetValue(key, out value) && !value.IsNull)
			{
				return value.AsBoolean;
			}
			return defaultValue;
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x00095F9C File Offset: 0x0009419C
		private static int? GetInt32(RecordValue record, string key, int? defaultValue = null)
		{
			Value value;
			if (record.TryGetValue(key, out value) && !value.IsNull)
			{
				return new int?(value.AsInteger32);
			}
			return defaultValue;
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x00095FCC File Offset: 0x000941CC
		private static TimeSpan? GetTimeSpan(RecordValue record, string key)
		{
			Value value;
			if (record.TryGetValue(key, out value) && !value.IsNull)
			{
				return new TimeSpan?(value.AsDuration.AsClrTimeSpan);
			}
			return null;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x00096008 File Offset: 0x00094208
		private static int? GetSeconds(TimeSpan? timeSpan)
		{
			if (timeSpan != null)
			{
				int num = (int)Math.Round(timeSpan.Value.TotalSeconds);
				if (num >= 0)
				{
					return new int?(num);
				}
			}
			return null;
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x00096048 File Offset: 0x00094248
		private static string GetString(RecordValue record, string key, string defaultValue)
		{
			Value value;
			if (record.TryGetValue(key, out value) && !value.IsNull)
			{
				return value.AsString;
			}
			return defaultValue;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x00096070 File Offset: 0x00094270
		private static Value GetValueOrDefault(RecordValue record, string key, Value defaultValue)
		{
			Value value;
			if (record.TryGetValue(key, out value) && !value.IsNull)
			{
				return value;
			}
			return defaultValue;
		}

		// Token: 0x040015B5 RID: 5557
		public static readonly OdbcOptions Empty = new OdbcOptions(RecordValue.Empty, RecordValue.Empty);

		// Token: 0x040015B6 RID: 5558
		public const string ClientConnectionPoolingKey = "ClientConnectionPooling";

		// Token: 0x040015B7 RID: 5559
		public const string SqlCapabilitiesKey = "SqlCapabilities";

		// Token: 0x040015B8 RID: 5560
		public const string SupportsTopKey = "SupportsTop";

		// Token: 0x040015B9 RID: 5561
		public const string LimitClauseLocationKey = "LimitClauseLocation";

		// Token: 0x040015BA RID: 5562
		public const string LimitClauseKey = "LimitClause";

		// Token: 0x040015BB RID: 5563
		public const string SupportsDerivedTableKey = "SupportsDerivedTable";

		// Token: 0x040015BC RID: 5564
		public const string SupportedPredicatesKey = "SupportedPredicates";

		// Token: 0x040015BD RID: 5565
		public const string SupportedAggregateFunctionsKey = "SupportedAggregateFunctions";

		// Token: 0x040015BE RID: 5566
		public const string GroupByCapabilitiesKey = "GroupByCapabilities";

		// Token: 0x040015BF RID: 5567
		public const string Sql92ConformanceKey = "Sql92Conformance";

		// Token: 0x040015C0 RID: 5568
		public const string SupportedSql92JoinsKey = "SupportedSql92Joins";

		// Token: 0x040015C1 RID: 5569
		public const string SupportedValueExpressionsKey = "SupportedValueExpressions";

		// Token: 0x040015C2 RID: 5570
		public const string FractionalSecondsScaleKey = "FractionalSecondsScale";

		// Token: 0x040015C3 RID: 5571
		public const string CredentialConnectionStringKey = "CredentialConnectionString";

		// Token: 0x040015C4 RID: 5572
		public const string CreateNavigationPropertiesKey = "CreateNavigationProperties";

		// Token: 0x040015C5 RID: 5573
		public const string HierarchicalNavigationKey = "HierarchicalNavigation";

		// Token: 0x040015C6 RID: 5574
		public const string ConnectionTimeoutKey = "ConnectionTimeout";

		// Token: 0x040015C7 RID: 5575
		public const string CommandTimeoutKey = "CommandTimeout";

		// Token: 0x040015C8 RID: 5576
		public const string UseEmbeddedDriverKey = "UseEmbeddedDriver";

		// Token: 0x040015C9 RID: 5577
		public const string SqlGetInfoKey = "SQLGetInfo";

		// Token: 0x040015CA RID: 5578
		public const string SqlGetTypeInfoKey = "SQLGetTypeInfo";

		// Token: 0x040015CB RID: 5579
		public const string SqlGetFunctionsKey = "SQLGetFunctions";

		// Token: 0x040015CC RID: 5580
		public const string SqlColumnsKey = "SQLColumns";

		// Token: 0x040015CD RID: 5581
		public const string SqlTablesKey = "SQLTables";

		// Token: 0x040015CE RID: 5582
		public const string CredentialHandlerKey = "CredentialHandler";

		// Token: 0x040015CF RID: 5583
		public const string DefaultTypeParametersKey = "DefaultTypeParameters";

		// Token: 0x040015D0 RID: 5584
		public const string RetryCountKey = "RetryCount";

		// Token: 0x040015D1 RID: 5585
		public const string TableTypesKey = "TableTypes";

		// Token: 0x040015D2 RID: 5586
		public const string SetUserQueryKey = "SetUserQuery";

		// Token: 0x040015D3 RID: 5587
		public const string ClearUserQueryKey = "ClearUserQuery";

		// Token: 0x040015D4 RID: 5588
		public const string UserContextCredentialPropertiesKey = "UserContextCredentialProperties";

		// Token: 0x040015D5 RID: 5589
		public const string SqlApiSqlBindCol = "SQL_API_SQLBINDCOL";

		// Token: 0x040015D6 RID: 5590
		public const string SupportsIncrementalNavigationKey = "SupportsIncrementalNavigation";

		// Token: 0x040015D7 RID: 5591
		public const string SoftNumbersKey = "SoftNumbers";

		// Token: 0x040015D8 RID: 5592
		public const string HideNativeQueryKey = "HideNativeQuery";

		// Token: 0x040015D9 RID: 5593
		public const string ImplicitTypeConversionsKey = "ImplicitTypeConversions";

		// Token: 0x040015DA RID: 5594
		public const string MaxParametersKey = "MaxParameters";

		// Token: 0x040015DB RID: 5595
		public const string SupportsStringLiteralsKey = "SupportsStringLiterals";

		// Token: 0x040015DC RID: 5596
		public const string SupportsNumericLiteralKey = "SupportsNumericLiterals";

		// Token: 0x040015DD RID: 5597
		public const string StringLiteralEscapeCharactersKey = "StringLiteralEscapeCharacters";

		// Token: 0x040015DE RID: 5598
		public const string TolerateConcatOverflowKey = "TolerateConcatOverflow";

		// Token: 0x040015DF RID: 5599
		public const string AstVisitorKey = "AstVisitor";

		// Token: 0x040015E0 RID: 5600
		public const string SupportsOdbcDateLiteralsKey = "SupportsOdbcDateLiterals";

		// Token: 0x040015E1 RID: 5601
		public const string SupportsOdbcTimeLiteralsKey = "SupportsOdbcTimeLiterals";

		// Token: 0x040015E2 RID: 5602
		public const string SupportsOdbcTimestampLiteralsKey = "SupportsOdbcTimestampLiterals";

		// Token: 0x040015E3 RID: 5603
		public const string OnErrorKey = "OnError";

		// Token: 0x040015E4 RID: 5604
		public const string PrepareStatementsKey = "PrepareStatements";

		// Token: 0x040015E5 RID: 5605
		public const string ReturnEscapeClauseKey = "ReturnEscapeClause";

		// Token: 0x040015E6 RID: 5606
		public const string LimitClauseKindKey = "LimitClauseKind";

		// Token: 0x040015E7 RID: 5607
		public const string SupportsLimitZeroKey = "SupportsLimitZero";

		// Token: 0x040015E8 RID: 5608
		public const string NativeQuerySchemaStrategyKey = "NativeQuerySchemaStrategy";

		// Token: 0x040015E9 RID: 5609
		public const string Sql92TranslationKey = "Sql92Translation";

		// Token: 0x040015EA RID: 5610
		public const string CancelQueryExplicitlyKey = "CancelQueryExplicitly";

		// Token: 0x040015EB RID: 5611
		public const string TryRecoverDateDiffKey = "TryRecoverDateDiff";

		// Token: 0x040015EC RID: 5612
		public const string TryRecoverCoalesceKey = "TryRecoverCoalesce";

		// Token: 0x040015ED RID: 5613
		public const string OptionalKey = "Optional";

		// Token: 0x040015EE RID: 5614
		private static readonly HashSet<string> baseSupportedSqlCapabilities = new HashSet<string>
		{
			"SupportsTop", "SupportsDerivedTable", "SupportedPredicates", "Sql92Conformance", "SupportedAggregateFunctions", "GroupByCapabilities", "SupportedSql92Joins", "FractionalSecondsScale", "SupportsNumericLiterals", "SupportsOdbcDateLiterals",
			"SupportsOdbcTimeLiterals", "SupportsOdbcTimestampLiterals", "PrepareStatements", "ReturnEscapeClause", "LimitClauseKind", "Optional"
		};

		// Token: 0x040015EF RID: 5615
		private static readonly HashSet<string> baseSupportedOptions = new HashSet<string>
		{
			"ClientConnectionPooling", "CreateNavigationProperties", "HierarchicalNavigation", "ConnectionTimeout", "CommandTimeout", "SqlCapabilities", "SoftNumbers", "SQLGetInfo", "ImplicitTypeConversions", "HideNativeQuery",
			"TolerateConcatOverflow", "SQLGetFunctions", "SupportsIncrementalNavigation", "SqlCompatibleWindowsAuth", "DefaultTypeParameters"
		};

		// Token: 0x040015F0 RID: 5616
		private readonly RecordValue record;

		// Token: 0x040015F1 RID: 5617
		private readonly RecordValue sqlCapabilities;

		// Token: 0x040015F2 RID: 5618
		private IDictionary<string, string> stringLiteralEscapeCharacters;

		// Token: 0x040015F3 RID: 5619
		private IDictionary<string, string> userContextCredentialProperties;
	}
}
