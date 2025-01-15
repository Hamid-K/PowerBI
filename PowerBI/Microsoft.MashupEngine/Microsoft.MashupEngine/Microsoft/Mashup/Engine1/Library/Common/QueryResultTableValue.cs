using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001115 RID: 4373
	internal class QueryResultTableValue : TableValue, IQueryResultValue, IQueryResultTableValue
	{
		// Token: 0x06007261 RID: 29281 RVA: 0x001897DC File Offset: 0x001879DC
		public QueryResultTableValue(Query originalQuery, EnvironmentBase environment, IExpression syntaxTree, IEngineHost host)
			: this(originalQuery, environment, syntaxTree, host, null)
		{
		}

		// Token: 0x06007262 RID: 29282 RVA: 0x001897EA File Offset: 0x001879EA
		public QueryResultTableValue(EnvironmentBase environment, Identifier identifier, IEngineHost host, TableTypeValue type)
			: this(null, environment, null, host, null)
		{
			this.identifier = identifier;
			this.type = type;
		}

		// Token: 0x06007263 RID: 29283 RVA: 0x00189806 File Offset: 0x00187A06
		private QueryResultTableValue(Query originalQuery, EnvironmentBase environment, IExpression syntaxTree, IEngineHost host, ValueBuilderBase valueBuilder)
		{
			this.originalQuery = originalQuery;
			this.environment = environment;
			this.host = host;
			this.syntaxTree = syntaxTree;
			this.identifier = string.Empty;
			this.valueBuilder = valueBuilder;
		}

		// Token: 0x06007264 RID: 29284 RVA: 0x00183D6D File Offset: 0x00181F6D
		public override bool TryGetProcessor(out QueryProcessor processor)
		{
			processor = QueryResultTableValue.queryProcessor;
			return true;
		}

		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x06007265 RID: 29285 RVA: 0x00189843 File Offset: 0x00187A43
		public override long LargeCount
		{
			get
			{
				return this.ValueBuilder.CreateCountOverEnumerator();
			}
		}

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x06007266 RID: 29286 RVA: 0x00189850 File Offset: 0x00187A50
		public EnvironmentBase Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x06007267 RID: 29287 RVA: 0x00189858 File Offset: 0x00187A58
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x06007268 RID: 29288 RVA: 0x00189860 File Offset: 0x00187A60
		public Identifier Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x06007269 RID: 29289 RVA: 0x00189868 File Offset: 0x00187A68
		public IExpression SyntaxTree
		{
			get
			{
				if (this.syntaxTree == null)
				{
					this.syntaxTree = new ConstantExpressionSyntaxNode(this);
				}
				return this.syntaxTree;
			}
		}

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x0600726A RID: 29290 RVA: 0x00189884 File Offset: 0x00187A84
		public override TypeValue Type
		{
			get
			{
				if (this.type != null)
				{
					return this.type;
				}
				return this.ValueBuilder.Type;
			}
		}

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x0600726B RID: 29291 RVA: 0x001898A0 File Offset: 0x00187AA0
		public override Query Query
		{
			get
			{
				return new QueryResultQuery(this);
			}
		}

		// Token: 0x0600726C RID: 29292 RVA: 0x001898A8 File Offset: 0x00187AA8
		public override TableValue Optimize()
		{
			return new QueryTableValue(this.Query).Optimize();
		}

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x0600726D RID: 29293 RVA: 0x001898BA File Offset: 0x00187ABA
		public ValueBuilderBase ValueBuilder
		{
			get
			{
				if (this.valueBuilder == null)
				{
					this.valueBuilder = this.environment.Compile(this.originalQuery, this.SyntaxTree);
				}
				return this.valueBuilder;
			}
		}

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x0600726E RID: 29294 RVA: 0x001898E7 File Offset: 0x00187AE7
		public EnvironmentStatementBuilder StatementBuilder
		{
			get
			{
				if (this.statementBuilder == null)
				{
					this.statementBuilder = this.environment.NewStatementBuilder(this, this.ValueBuilder, this.SyntaxTree);
				}
				return this.statementBuilder;
			}
		}

		// Token: 0x0600726F RID: 29295 RVA: 0x00189915 File Offset: 0x00187B15
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			this.Environment.ReportFoldingFailure();
			return this.ValueBuilder.GetEnumerator();
		}

		// Token: 0x06007270 RID: 29296 RVA: 0x00189930 File Offset: 0x00187B30
		public override IPageReader GetReader()
		{
			IPageReader pageReader;
			if (this.ValueBuilder.TryGetReader(out pageReader))
			{
				return pageReader;
			}
			return base.GetReader();
		}

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x06007271 RID: 29297 RVA: 0x00189954 File Offset: 0x00187B54
		public override IQueryDomain QueryDomain
		{
			get
			{
				return ExpressionQueryDomain.Instance;
			}
		}

		// Token: 0x06007272 RID: 29298 RVA: 0x0018995C File Offset: 0x00187B5C
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			if (function.Equals(CapabilityModule.DirectQueryCapabilities.From))
			{
				result = this.Environment.GetDirectQueryCapabilities();
				return true;
			}
			if (function.Equals(Library._Value.VersionIdentity))
			{
				DbEnvironment dbEnvironment = this.Environment as DbEnvironment;
				if (dbEnvironment.TransactionInfo != null)
				{
					result = TextValue.New(dbEnvironment.TransactionInfo.Identity);
					return true;
				}
			}
			if (function.Equals(Library._Value.Versions) && arguments.Length == 1)
			{
				DbEnvironment dbEnvironment2 = this.Environment as DbEnvironment;
				TableValue tableValue;
				if (dbEnvironment2 != null && new DbValueVersions(dbEnvironment2, delegate(DbEnvironment versionEnv)
				{
					if (this.identifier != string.Empty)
					{
						return new QueryResultTableValue(versionEnv, this.identifier, versionEnv.Host, this.type);
					}
					return new QueryResultTableValue(this.originalQuery, versionEnv, this.syntaxTree, versionEnv.Host);
				}).TryCreateTable(out tableValue))
				{
					result = tableValue;
					return true;
				}
			}
			if (function.Equals(QueryResultTableValue.ExtractionFunction) && arguments.Length == 1)
			{
				result = this;
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x04003F15 RID: 16149
		public static readonly ExternalQueryProcessor queryProcessor = new ExternalQueryProcessor();

		// Token: 0x04003F16 RID: 16150
		public static readonly FunctionValue ExtractionFunction = FoldableFunctionValue.New(new QueryResultTableValue.QueryResultExtractionFunctionValue());

		// Token: 0x04003F17 RID: 16151
		private readonly Query originalQuery;

		// Token: 0x04003F18 RID: 16152
		private readonly EnvironmentBase environment;

		// Token: 0x04003F19 RID: 16153
		private readonly IEngineHost host;

		// Token: 0x04003F1A RID: 16154
		private readonly Identifier identifier;

		// Token: 0x04003F1B RID: 16155
		private IExpression syntaxTree;

		// Token: 0x04003F1C RID: 16156
		private readonly TableTypeValue type;

		// Token: 0x04003F1D RID: 16157
		private ValueBuilderBase valueBuilder;

		// Token: 0x04003F1E RID: 16158
		private EnvironmentStatementBuilder statementBuilder;

		// Token: 0x02001116 RID: 4374
		private class QueryResultExtractionFunctionValue : NativeFunctionValue1<TableValue, TableValue>, IOpaqueFunctionValue, IFunctionValue, IValue
		{
			// Token: 0x06007275 RID: 29301 RVA: 0x0004DA23 File Offset: 0x0004BC23
			public QueryResultExtractionFunctionValue()
				: base(TypeValue.Table, "table", TypeValue.Table)
			{
			}

			// Token: 0x06007276 RID: 29302 RVA: 0x0000A6A5 File Offset: 0x000088A5
			public override TableValue TypedInvoke(TableValue table)
			{
				return table;
			}
		}
	}
}
