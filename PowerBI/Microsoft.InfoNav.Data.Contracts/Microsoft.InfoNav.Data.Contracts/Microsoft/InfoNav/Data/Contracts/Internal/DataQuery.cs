using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000183 RID: 387
	[DataContract(Name = "DataQuery", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class DataQuery : IEquatable<DataQuery>
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00014436 File Offset: 0x00012636
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x0001443E File Offset: 0x0001263E
		[DataMember(Name = "Commands", IsRequired = true, EmitDefaultValue = false, Order = 10)]
		public IList<QueryCommand> Commands { get; set; }

		// Token: 0x06000A27 RID: 2599 RVA: 0x00014447 File Offset: 0x00012647
		public static bool IsRawDataRequest(DataQuery dataQuery)
		{
			return DataQuery.IsScriptVisual(dataQuery) || DataQuery.IsExportDataQuery(dataQuery);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00014459 File Offset: 0x00012659
		public static bool IsScriptVisual(DataQuery dataQuery)
		{
			return !(dataQuery == null) && dataQuery.Commands != null && dataQuery.Commands.Count == 2 && !(dataQuery.Commands[1].ScriptVisualCommand == null);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00014498 File Offset: 0x00012698
		public static QueryCommand GetScriptVisualQueryCommand(DataQuery dataQuery)
		{
			DataQuery.ValidateCommandAvailability(dataQuery, 1);
			return dataQuery.Commands[1];
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x000144AD File Offset: 0x000126AD
		public static ScriptVisualCommand GetScriptVisualCommand(DataQuery dataQuery)
		{
			return DataQuery.GetScriptVisualQueryCommand(dataQuery).ScriptVisualCommand;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x000144BA File Offset: 0x000126BA
		public static SemanticQueryDataShapeCommand GetSemanticQueryDataShapeCommand(DataQuery dataQuery)
		{
			DataQuery.ValidateCommandAvailability(dataQuery, 0);
			return dataQuery.Commands[0].SemanticQueryDataShapeCommand;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x000144D4 File Offset: 0x000126D4
		public static ExportDataCommand GetExportDataQueryCommand(DataQuery dataQuery)
		{
			DataQuery.ValidateCommandAvailability(dataQuery, 1);
			return dataQuery.Commands[1].ExportDataCommand;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x000144EE File Offset: 0x000126EE
		public static bool IsExportDataQuery(DataQuery dataQuery)
		{
			return !(dataQuery == null) && dataQuery.Commands != null && dataQuery.Commands.Count == 2 && !(dataQuery.Commands[1].ExportDataCommand == null);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00014530 File Offset: 0x00012730
		public static bool TryValidate(DataQuery dataQuery, out IEnumerable<ValidationResult> validationResults)
		{
			validationResults = null;
			if (dataQuery == null)
			{
				validationResults = DataQuery.CreateValidationResult("There is no DataQuery");
			}
			else if (dataQuery.Commands == null || dataQuery.Commands.Count == 0)
			{
				validationResults = DataQuery.CreateValidationResult("There are no QueryCommands in this DataQuery");
			}
			else if (dataQuery.Commands.Count > 2)
			{
				validationResults = DataQuery.CreateValidationResult("Detected more than two QueryCommand objects");
			}
			else
			{
				QueryCommand queryCommand = dataQuery.Commands[0];
				if (queryCommand == null || queryCommand.SemanticQueryDataShapeCommand == null)
				{
					validationResults = DataQuery.CreateValidationResult("Missing Semantic Query in the first QueryCommand in this DataQuery");
				}
				else if (queryCommand.ScriptVisualCommand != null)
				{
					validationResults = DataQuery.CreateValidationResult("An extraneous ScriptVisualCommand detected next to a semantic query command");
				}
				else if (queryCommand.ExportDataCommand != null)
				{
					validationResults = DataQuery.CreateValidationResult("An extraneous ExportDataCommand detected next to a semantic query command");
				}
				else if (dataQuery.Commands.Count > 1)
				{
					if (dataQuery.Commands.Count == 2)
					{
						QueryCommand queryCommand2 = dataQuery.Commands[1];
						if (queryCommand2 == null)
						{
							validationResults = DataQuery.CreateValidationResult("The second QueryCommand is missing");
						}
						else if (queryCommand2.SemanticQueryDataShapeCommand != null)
						{
							validationResults = DataQuery.CreateValidationResult("An extraneous Semantic Query detected in the second QueryCommand in this DataQuery");
						}
						else if (queryCommand2.ScriptVisualCommand != null)
						{
							if (ScriptVisualCommand.TryValidate(queryCommand2.ScriptVisualCommand, out validationResults))
							{
							}
						}
						else if (queryCommand2.ExportDataCommand != null)
						{
							if (ExportDataCommand.TryValidate(queryCommand2.ExportDataCommand, out validationResults))
							{
							}
						}
						else
						{
							validationResults = DataQuery.CreateValidationResult("The second QueryCommand is with null ScriptVisualCommand and null ExportDataCommand");
						}
					}
					else
					{
						validationResults = DataQuery.CreateValidationResult("Detected more than one QueryCommand object and the count does not match a ScriptVisualCommand or ExportDataCommand");
					}
				}
			}
			return validationResults == null;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000146CF File Offset: 0x000128CF
		public static DataQuery Create(SemanticQueryDataShapeCommand semanticQueryDataShapeCommand)
		{
			return new DataQuery
			{
				Commands = new List<QueryCommand>
				{
					new QueryCommand
					{
						SemanticQueryDataShapeCommand = semanticQueryDataShapeCommand
					}
				}
			};
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000146F3 File Offset: 0x000128F3
		public static IEnumerable<DataQuery> Create(IEnumerable<SemanticQueryDataShapeCommand> semanticQueryDataShapeCommands)
		{
			return semanticQueryDataShapeCommands.Select((SemanticQueryDataShapeCommand semanticQueryDataShapeCommand) => DataQuery.Create(semanticQueryDataShapeCommand));
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001471A File Offset: 0x0001291A
		private static IEnumerable<ValidationResult> CreateValidationResult(string errorMessage)
		{
			return new ValidationResult[]
			{
				new ValidationResult(errorMessage)
			};
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001472C File Offset: 0x0001292C
		private static void ValidateCommandAvailability(DataQuery dataQuery, int expectedPositionInCommandsArray)
		{
			if (dataQuery == null)
			{
				Contract.ExceptValue("dataQuery");
			}
			if (dataQuery.Commands == null || dataQuery.Commands.Count == 0 || dataQuery.Commands.Count <= expectedPositionInCommandsArray)
			{
				Contract.ExceptEmpty("dataQuery.Commands");
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001477C File Offset: 0x0001297C
		public bool Equals(DataQuery other)
		{
			bool? flag = Util.AreEqual<DataQuery>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			flag = Util.AreEqual<IList<QueryCommand>>(this.Commands, other.Commands);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Commands.SequenceEqual(other.Commands);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000147D6 File Offset: 0x000129D6
		public override bool Equals(object other)
		{
			return this.Equals(other as DataQuery);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000147E4 File Offset: 0x000129E4
		public override int GetHashCode()
		{
			if (this.Commands == null)
			{
				return 0;
			}
			return Hashing.CombineHash<QueryCommand>(this.Commands, null);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000147FC File Offset: 0x000129FC
		public static bool operator ==(DataQuery left, DataQuery right)
		{
			bool? flag = Util.AreEqual<DataQuery>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00014829 File Offset: 0x00012A29
		public static bool operator !=(DataQuery left, DataQuery right)
		{
			return !(left == right);
		}

		// Token: 0x0400059F RID: 1439
		public const int SemanticQueryDataShapeCommandPositionInDataQuery = 0;

		// Token: 0x040005A0 RID: 1440
		public const int ScriptVisualCommandPositionInDataQuery = 1;

		// Token: 0x040005A1 RID: 1441
		public const int ExportDataCommandPositionInDataQuery = 1;

		// Token: 0x040005A2 RID: 1442
		public const int MaximumNumberOfCommands = 2;

		// Token: 0x040005A3 RID: 1443
		public const int ScriptVisualCommandCount = 2;

		// Token: 0x040005A4 RID: 1444
		public const int ExportDataCommandCount = 2;
	}
}
