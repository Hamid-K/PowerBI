using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001091 RID: 4241
	internal class DbStatementBuilder : EnvironmentStatementBuilder
	{
		// Token: 0x06006F03 RID: 28419 RVA: 0x0017EFF7 File Offset: 0x0017D1F7
		public DbStatementBuilder(TableValue table, DbEnvironment environment, DbValueBuilder valueBuilder, IExpression syntaxTree)
			: base(table, environment, valueBuilder, syntaxTree)
		{
		}

		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x06006F04 RID: 28420 RVA: 0x0017F004 File Offset: 0x0017D204
		public DbEnvironment DbEnvironment
		{
			get
			{
				return (DbEnvironment)base.Environment;
			}
		}

		// Token: 0x06006F05 RID: 28421 RVA: 0x0017F014 File Offset: 0x0017D214
		protected override ActionValue InsertRowsNonFolding(Query rowsToInsert, bool countOnly)
		{
			IConstantExpression constant = base.SyntaxTree as IConstantExpression;
			IBulkCopy bulkCopy;
			if (countOnly && constant != null && this.DbEnvironment.BulkInsertMinimumSize != -1 && this.DbEnvironment.TryGetBulkCopy(constant.Value.AsTable, out bulkCopy))
			{
				return new DbStatementBuilder.WithExpressionActionValue(ActionValue.New(delegate
				{
					Value value;
					using (IPageReader reader = new QueryTableValue(rowsToInsert).GetReader())
					{
						using (IPageReader pageReader = new ThrowingPageReader(reader))
						{
							using (PageReaderBuffer pageReaderBuffer = new PageReaderBuffer(pageReader))
							{
								bool flag = false;
								using (IPageReader bufferedPageReader2 = pageReaderBuffer.GetPageReader())
								{
									flag = DataReaderTableValue.New(this.DbEnvironment.Host.QueryService<ILifetimeService>(), () => new PageReaderDataReaderSource(bufferedPageReader2)).HasAtLeastNElements(this.DbEnvironment.BulkInsertMinimumSize);
									pageReaderBuffer.StopBuffering();
								}
								using (IPageReader bufferedPageReader = pageReaderBuffer.GetPageReader())
								{
									long num;
									if (flag && bulkCopy.TryCopyFrom(bufferedPageReader, out num))
									{
										value = new CountAndTypeTableValue(num, constant.Value.AsTable.Type.AsTableType);
									}
									else
									{
										TableValue tableValue = DataReaderTableValue.New(this.DbEnvironment.Host.QueryService<ILifetimeService>(), () => new PageReaderDataReaderSource(bufferedPageReader));
										value = this.<>n__0(tableValue.Query, countOnly).Execute();
									}
								}
							}
						}
					}
					return value;
				}), () => this.<>n__0(rowsToInsert, countOnly).Expression);
			}
			return base.InsertRowsNonFolding(rowsToInsert, countOnly);
		}

		// Token: 0x02001092 RID: 4242
		private sealed class WithExpressionActionValue : ActionValue
		{
			// Token: 0x06006F07 RID: 28423 RVA: 0x0017F0CD File Offset: 0x0017D2CD
			public WithExpressionActionValue(ActionValue action, Func<IExpression> getExpression)
			{
				this.action = action;
				this.getExpression = getExpression;
			}

			// Token: 0x17001F4E RID: 8014
			// (get) Token: 0x06006F08 RID: 28424 RVA: 0x0017F0E3 File Offset: 0x0017D2E3
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = this.getExpression();
					}
					return this.expression;
				}
			}

			// Token: 0x06006F09 RID: 28425 RVA: 0x0017F104 File Offset: 0x0017D304
			public override ActionValue ExecuteBindings()
			{
				return this.action.ExecuteBindings();
			}

			// Token: 0x06006F0A RID: 28426 RVA: 0x0017F111 File Offset: 0x0017D311
			public override Value Execute()
			{
				return this.action.Execute();
			}

			// Token: 0x04003D93 RID: 15763
			private readonly ActionValue action;

			// Token: 0x04003D94 RID: 15764
			private readonly Func<IExpression> getExpression;

			// Token: 0x04003D95 RID: 15765
			private IExpression expression;
		}
	}
}
