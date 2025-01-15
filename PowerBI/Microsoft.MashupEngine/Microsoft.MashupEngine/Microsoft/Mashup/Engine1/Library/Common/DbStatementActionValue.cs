using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001090 RID: 4240
	internal class DbStatementActionValue : ActionValue
	{
		// Token: 0x06006EFC RID: 28412 RVA: 0x0017ED13 File Offset: 0x0017CF13
		private DbStatementActionValue(DbEnvironment environment, Value target, string statement, bool countOnly, Value parameters, OutputParameterCollection outputParameters, string statementType)
		{
			this.environment = environment;
			this.target = target;
			this.statement = statement;
			this.countOnly = countOnly;
			this.parameters = parameters;
			this.outputParameters = outputParameters;
			this.statementType = statementType;
		}

		// Token: 0x06006EFD RID: 28413 RVA: 0x0017ED50 File Offset: 0x0017CF50
		public static ActionValue New(DbEnvironment environment, Query targetQuery, DbStatementPlan statementPlan, string statementType, OutputParameterCollection outputParameters = null)
		{
			return new DbStatementActionValue(environment, new QueryTableValue(targetQuery), statementPlan.ExternalStatement, statementPlan.CountOnly, Value.Null, outputParameters, statementType);
		}

		// Token: 0x06006EFE RID: 28414 RVA: 0x0017ED72 File Offset: 0x0017CF72
		public static ActionValue New(DbEnvironment environment, Value targetValue, string statement, bool countOnly, Value parameters, string statementType)
		{
			return new DbStatementActionValue(environment, targetValue, statement, countOnly, parameters, null, statementType);
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x06006EFF RID: 28415 RVA: 0x0017ED84 File Offset: 0x0017CF84
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					if (this.parameters.IsNull)
					{
						this.expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(ActionModule.ValueAction.NativeStatement), new ConstantExpressionSyntaxNode(this.target), new ConstantExpressionSyntaxNode(TextValue.New(this.statement)));
					}
					else
					{
						this.expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(ActionModule.ValueAction.NativeStatement), new IExpression[]
						{
							new ConstantExpressionSyntaxNode(this.target),
							new ConstantExpressionSyntaxNode(TextValue.New(this.statement)),
							new ConstantExpressionSyntaxNode(this.parameters)
						});
					}
				}
				return this.expression;
			}
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x0017EE2B File Offset: 0x0017D02B
		public override Value Execute()
		{
			return this.environment.ConvertDbExceptions<TableValue>(delegate
			{
				DataTable dataTable = new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				};
				using (DbConnection dbConnection = this.environment.CreateConnection())
				{
					dbConnection.Open(this.environment);
					using (DbCommand dbCommand = this.environment.ParameterTypeMap.AddParameters(dbConnection.CreateCommand(), this.parameters))
					{
						dbCommand.CommandText = this.statement;
						TracingDbCommand.TryAddTracer(dbCommand, delegate(IHostTrace trace)
						{
							trace.Add("StatementType", this.statementType, false);
							trace.Add("CountOnly", this.countOnly, false);
						});
						if (this.countOnly)
						{
							int num = dbCommand.ExecuteNonQuery();
							dataTable.Columns.Add("Value", typeof(int));
							dataTable.Rows.Add(new object[] { num });
						}
						else if (this.outputParameters != null)
						{
							this.outputParameters.AddParametersToDbCommand(dbCommand);
							dbCommand.ExecuteNonQuery();
							this.outputParameters.FillDataTable(dbCommand, dataTable);
						}
						else
						{
							using (DbDataReaderWithTableSchema dbDataReaderWithTableSchema = this.environment.CreateReaderWrapper(null, false).Wrap(dbCommand.ExecuteReader().WithTableSchema()))
							{
								dataTable.Load(dbDataReaderWithTableSchema);
							}
						}
					}
					dbConnection.Close();
				}
				return DataReaderTableValue.New(dataTable);
			});
		}

		// Token: 0x04003D8B RID: 15755
		private readonly DbEnvironment environment;

		// Token: 0x04003D8C RID: 15756
		private readonly Value target;

		// Token: 0x04003D8D RID: 15757
		private readonly string statement;

		// Token: 0x04003D8E RID: 15758
		private readonly bool countOnly;

		// Token: 0x04003D8F RID: 15759
		private readonly Value parameters;

		// Token: 0x04003D90 RID: 15760
		private readonly string statementType;

		// Token: 0x04003D91 RID: 15761
		private IExpression expression;

		// Token: 0x04003D92 RID: 15762
		private OutputParameterCollection outputParameters;
	}
}
