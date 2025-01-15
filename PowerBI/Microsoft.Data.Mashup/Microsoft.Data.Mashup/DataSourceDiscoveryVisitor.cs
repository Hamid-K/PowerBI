using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.EngineHost;
using Microsoft.Mashup.EngineHost.Services;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000013 RID: 19
	internal sealed class DataSourceDiscoveryVisitor : CoordinateTrackingVisitor
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00004E14 File Offset: 0x00003014
		public DataSourceDiscoveryVisitor(MashupDiscoveryOptions options, MashupPartitionCoordinateType coordinateType, IEvaluationConstants evaluationConstants)
			: base(coordinateType)
		{
			this.options = options;
			this.evaluationConstants = evaluationConstants;
			this.engine = MashupEngines.Version1;
			this.placeholder = this.engine.Binary(new byte[0], null);
			this.library = LibraryService.LegacyLibrary.CurrentLibrary;
			this.modules = LibraryService.LegacyLibrary.GetModules();
			this.modules.Add("Core");
			if ((this.options & MashupDiscoveryOptions.ReportMetadata) == MashupDiscoveryOptions.ReportMetadata)
			{
				this.metadata = new Dictionary<IExpression, IExpression>();
			}
			IEngineHost engineHost = MashupProviderFactory.MakeEngineHost();
			this.resourceAccess = this.engine.Function(engineHost, FunctionHandle.ResourceAccess);
			if (!this.library.Keys.Contains("Resource.Access"))
			{
				this.library = this.library.Concatenate(this.engine.Record(this.engine.Keys(new string[] { "Resource.Access" }), new IValue[] { this.resourceAccess })).AsRecord;
			}
			this.databaseFunctions = new IFunctionValue[]
			{
				this.library["Sql.Databases"].AsFunction,
				this.library["AnalysisServices.Databases"].AsFunction
			};
			this.dataSourceFunctions = new Dictionary<IFunctionValue, string>();
			for (int i = 0; i < this.library.Keys.Length; i++)
			{
				if (this.library[i].IsFunction && this.library[i].AsFunction.IsResourceAccessFunction)
				{
					this.dataSourceFunctions.Add(this.library[i].AsFunction, this.library.Keys[i]);
				}
			}
			this.nativeQueryFunction = this.library["Value.NativeQuery"].AsFunction;
			this.discoveries = new Dictionary<IExpression, MashupDiscovery>();
			this.nativeQueries = new List<IInvocationExpression>();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005007 File Offset: 0x00003207
		public HashSet<string> Modules
		{
			get
			{
				return this.modules;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000500F File Offset: 0x0000320F
		public bool HasUnboundNativeQuery
		{
			get
			{
				return this.unboundNativeQuery;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005017 File Offset: 0x00003217
		public Dictionary<IExpression, MashupDiscovery> Discoveries
		{
			get
			{
				return this.discoveries;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000501F File Offset: 0x0000321F
		public bool IgnoreNativeQueries
		{
			get
			{
				return (this.options & MashupDiscoveryOptions.IgnoreNativeQueries) > MashupDiscoveryOptions.None;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005030 File Offset: 0x00003230
		public void VisitDocuments(IEnumerable<IDocument> documents)
		{
			this.bindings = DataSourceDiscoveryVisitor.BindingVisitor.GetBindings(this.library, documents);
			this.valueInterpreter = new DataSourceDiscoveryVisitor.ValueInterpreter(this.engine, this.library, this.placeholder, this.bindings, this.evaluationConstants);
			this.boundExpressions = new HashSet<IExpression>(this.bindings.Values);
			this.discoveredNavigationRoots = new List<DataSourceDiscoveryVisitor.ResolverData>();
			foreach (IDocument document in documents)
			{
				this.currentDocument = document;
				this.VisitDocument(document);
			}
			if (this.discoveredNavigationRoots.Count > 0)
			{
				foreach (KeyValuePair<IExpression, MashupDiscovery> keyValuePair in DataSourceDiscoveryVisitor.NavigationResolverVisitor.Resolve(this, documents))
				{
					this.AddDiscovery(keyValuePair.Key, this.StripExtension(keyValuePair.Value));
				}
			}
			if (this.nativeQueries.Count > 0 && !this.IgnoreNativeQueries)
			{
				this.AnalyzeNativeQueries();
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005158 File Offset: 0x00003358
		private void AddDiscovery(IExpression expression, MashupDiscovery discovery)
		{
			if (this.IgnoreNativeQueries)
			{
				discovery = discovery.RemoveQuery();
			}
			IExpression expression2;
			IRecordValue recordValue;
			string text;
			if (this.metadata != null && this.metadata.TryGetValue(expression, out expression2) && Util.TryGetRecord(expression2, out recordValue) && Util.TryConvertJson(recordValue, out text))
			{
				discovery = discovery.AddMetadata(text);
			}
			this.discoveries.Add(expression, discovery);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000051B7 File Offset: 0x000033B7
		protected override void VisitBinary(IBinaryExpression binary)
		{
			if (binary.Operator == BinaryOperator2.MetadataAdd && this.metadata != null)
			{
				this.metadata[binary.Left] = binary.Right;
			}
			base.VisitBinary(binary);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000051EC File Offset: 0x000033EC
		protected override void VisitInvocation(IInvocationExpression invocation)
		{
			bool flag;
			IInvocationExpression invocationExpression;
			MashupDiscovery mashupDiscovery = this.AnalyzeInvocation(invocation, out flag, out invocationExpression);
			if (mashupDiscovery != null)
			{
				if (invocationExpression != null && (flag || (this.options & MashupDiscoveryOptions.ReportNavigationSteps) != MashupDiscoveryOptions.None))
				{
					this.discoveredNavigationRoots.Add(new DataSourceDiscoveryVisitor.ResolverData(mashupDiscovery, invocation, invocationExpression));
					return;
				}
				this.AddDiscovery(invocation, this.StripExtension(mashupDiscovery));
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000523C File Offset: 0x0000343C
		private MashupDiscovery StripExtension(MashupDiscovery discovered)
		{
			if (discovered.Kind == MashupDiscoveryKind.DataSource && (this.options & MashupDiscoveryOptions.ReportExtensionDSR) == MashupDiscoveryOptions.None && this.engine.IsExtensionResourceKind(discovered.DataSourceReference.ResourceKindInfo.Kind))
			{
				IResource nonNormalizedResource = discovered.DataSourceReference.NonNormalizedResource;
				DataSourceReference dataSourceReference = new DataSourceReference(nonNormalizedResource.Kind, nonNormalizedResource.NonNormalizedPath, discovered.DataSourceReference.Query);
				discovered = new MashupDiscovery(discovered.Kind, discovered.FunctionName, dataSourceReference, discovered.Coordinate, null, true, null);
			}
			return discovered;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000052C0 File Offset: 0x000034C0
		private MashupDiscovery AnalyzeInvocation(IInvocationExpression invocation, out bool isDatabaseFunction, out IInvocationExpression resolved)
		{
			isDatabaseFunction = false;
			resolved = null;
			IValue value = this.valueInterpreter.Interpret(invocation.Function);
			if (value == this.placeholder || !value.IsFunction)
			{
				base.VisitInvocation(invocation);
				return null;
			}
			base.VisitListElements(invocation.Arguments);
			if (value == this.nativeQueryFunction)
			{
				this.nativeQueries.Add(invocation);
				return null;
			}
			if (!value.AsFunction.IsResourceAccessFunction)
			{
				return null;
			}
			resolved = this.SafeCreateInvocation(value.AsFunction, invocation.Arguments);
			MashupDiscovery mashupDiscovery = null;
			if (resolved != null)
			{
				mashupDiscovery = this.SafeCreateDataSource(this.dataSourceFunctions[value.AsFunction], resolved, null);
			}
			if (mashupDiscovery == null)
			{
				base.Visit(invocation.Function);
			}
			else
			{
				isDatabaseFunction = mashupDiscovery.Kind == MashupDiscoveryKind.DataSource && this.databaseFunctions.Contains(value.AsFunction);
			}
			return mashupDiscovery;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005398 File Offset: 0x00003598
		private void AnalyzeNativeQueries()
		{
			DataSourceDiscoveryVisitor.DataSourceLocator dataSourceLocator = new DataSourceDiscoveryVisitor.DataSourceLocator(this.bindings, this.discoveries);
			List<IExpression> list = new List<IExpression>();
			foreach (IInvocationExpression invocationExpression in this.nativeQueries)
			{
				if (invocationExpression.Arguments.Count != 0)
				{
					IValue value = ((invocationExpression.Arguments.Count > 1) ? this.valueInterpreter.Interpret(invocationExpression.Arguments[1]) : this.placeholder);
					KeyValuePair<IExpression, MashupDiscovery> keyValuePair = dataSourceLocator.Locate(invocationExpression.Arguments[0]);
					MashupDiscovery mashupDiscovery = null;
					if (keyValuePair.Key == null || keyValuePair.Value.Kind != MashupDiscoveryKind.DataSource)
					{
						this.unboundNativeQuery = true;
					}
					else if (value.IsText)
					{
						mashupDiscovery = keyValuePair.Value.AddQuery(value.AsString);
					}
					else
					{
						mashupDiscovery = keyValuePair.Value.SetUnknownQuery();
					}
					if (mashupDiscovery != null)
					{
						list.Add(keyValuePair.Key);
						this.discoveries.Add(invocationExpression, mashupDiscovery);
					}
				}
			}
			foreach (IExpression expression in list)
			{
				this.discoveries.Remove(expression);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000550C File Offset: 0x0000370C
		protected override void VisitIdentifier(IIdentifierExpression identifier)
		{
			MashupDiscoveryKind? mashupDiscoveryKind = null;
			string text = null;
			IExpression expression;
			if (!this.bindings.TryGetValue(identifier, out expression))
			{
				mashupDiscoveryKind = new MashupDiscoveryKind?(MashupDiscoveryKind.UnknownFunction);
				text = identifier.Name.Name;
			}
			else if (expression != null)
			{
				IValue value = this.valueInterpreter.Interpret(expression);
				if (value.IsFunction && value.AsFunction.IsResourceAccessFunction)
				{
					mashupDiscoveryKind = new MashupDiscoveryKind?(MashupDiscoveryKind.UnknownCallSite);
					text = this.dataSourceFunctions[value.AsFunction];
				}
				else if (value == this.nativeQueryFunction)
				{
					this.unboundNativeQuery = true;
				}
			}
			if (mashupDiscoveryKind != null)
			{
				this.AddDiscovery(identifier, new MashupDiscovery(mashupDiscoveryKind.Value, text, null, base.CurrentCoordinate, null, true, null));
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000055C4 File Offset: 0x000037C4
		protected override void VisitLet(ILetExpression let)
		{
			for (int i = 0; i < let.Variables.Count; i++)
			{
				if (this.boundExpressions.Contains(let.Variables[i].Value))
				{
					using (base.NewScope(let.Variables[i].Name))
					{
						this.VisitInitializer(let.Variables[i]);
					}
				}
			}
			this.VisitExpression(let.Expression);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005668 File Offset: 0x00003868
		protected override void VisitExports(IExportsExpression expression)
		{
			if (string.Equals(expression.Name, "Shared", StringComparison.Ordinal))
			{
				this.AddDiscovery(expression, new MashupDiscovery(MashupDiscoveryKind.Unsupported, "#shared", null, base.CurrentCoordinate, null, true, null));
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000056A9 File Offset: 0x000038A9
		protected override Exception NewDepthLimitExceededException()
		{
			return new MashupException(ProviderErrorStrings.Expression_TooDeep);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000056B8 File Offset: 0x000038B8
		private DataSourceDiscoveryVisitor.InvocationExpression SafeCreateInvocation(IFunctionValue function, IList<IExpression> arguments)
		{
			DataSourceDiscoveryVisitor.InvocationExpression invocationExpression;
			try
			{
				invocationExpression = this.CreateInvocation(function, arguments);
			}
			catch (ValueException2)
			{
				invocationExpression = null;
			}
			return invocationExpression;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000056E8 File Offset: 0x000038E8
		private DataSourceDiscoveryVisitor.InvocationExpression CreateInvocation(IFunctionValue function, IList<IExpression> arguments)
		{
			IFunctionTypeValue asFunctionType = function.Type.AsFunctionType;
			int parameterCount = asFunctionType.ParameterCount;
			if (arguments.Count < asFunctionType.Min || arguments.Count > parameterCount)
			{
				return null;
			}
			IValue[] array = new IValue[parameterCount];
			for (int i = 0; i < array.Length; i++)
			{
				if (i < arguments.Count)
				{
					array[i] = this.valueInterpreter.Interpret(arguments[i]);
				}
				else
				{
					array[i] = this.engine.Null;
				}
			}
			return new DataSourceDiscoveryVisitor.InvocationExpression(this.engine, function, array);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005774 File Offset: 0x00003974
		private MashupDiscovery SafeCreateDataSource(string dataSourceFunctionName, IExpression expression, MashupPartitionCoordinate coordinate = null)
		{
			MashupDiscovery mashupDiscovery;
			try
			{
				mashupDiscovery = this.CreateDataSource(dataSourceFunctionName, expression, coordinate ?? base.CurrentCoordinate);
			}
			catch (Exception ex)
			{
				if (!ProviderTracing.TraceIsSafeException("DataSourceDiscoverVisitor/SafeCreateDataSource", ex, this.evaluationConstants, null))
				{
					throw;
				}
				mashupDiscovery = null;
			}
			return mashupDiscovery;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000057C4 File Offset: 0x000039C4
		private MashupDiscovery CreateDataSource(string dataSourceFunctionName, IExpression expression, MashupPartitionCoordinate coordinate)
		{
			IDataSourceLocation dataSourceLocation;
			IRecordValue recordValue;
			IKeys keys;
			bool flag;
			if (!this.engine.TryGetLocation(expression, false, out dataSourceLocation, out recordValue, out keys, out flag))
			{
				return null;
			}
			DataSourceReference dataSourceReference = new DataSourceReference(dataSourceLocation);
			string text = null;
			bool flag2 = true;
			if ((this.options & MashupDiscoveryOptions.ReportOptionsRecord) == MashupDiscoveryOptions.ReportOptionsRecord && recordValue != null && recordValue.IsRecord && Util.TryConvertJson(recordValue, out text))
			{
				flag2 = keys == null || keys.Length > 0;
			}
			return new MashupDiscovery(flag ? MashupDiscoveryKind.UnknownNativeQuery : MashupDiscoveryKind.DataSource, dataSourceFunctionName, dataSourceReference, coordinate, text, flag2, null);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005840 File Offset: 0x00003A40
		private MashupDiscovery LegacyCreateNavigatedDataSource(string dataSourceFunctionName, IExpression expression, MashupPartitionCoordinate coordinate)
		{
			IElementAccessExpression elementAccessExpression = expression as IElementAccessExpression;
			IInvocationExpression invocationExpression = expression as IInvocationExpression;
			if (elementAccessExpression != null)
			{
				invocationExpression = elementAccessExpression.Collection as IInvocationExpression;
			}
			if (invocationExpression == null)
			{
				return null;
			}
			IConstantExpression2 constantExpression = invocationExpression.Function as IConstantExpression2;
			if (constantExpression == null || !constantExpression.Value.IsFunction)
			{
				return null;
			}
			MashupDiscovery mashupDiscovery = this.SafeCreateDataSource(dataSourceFunctionName, invocationExpression, coordinate);
			if (mashupDiscovery == null)
			{
				return null;
			}
			if (elementAccessExpression == null)
			{
				return mashupDiscovery;
			}
			if (!this.databaseFunctions.Contains(constantExpression.Value.AsFunction) || mashupDiscovery.DataSourceReference.Query != null || mashupDiscovery.DataSourceReference.Location == null)
			{
				return null;
			}
			IRecordExpression recordExpression = elementAccessExpression.Key as IRecordExpression;
			if (recordExpression == null || recordExpression.Members.Count != 1)
			{
				return null;
			}
			VariableInitializer variableInitializer = recordExpression.Members.FirstOrDefault((VariableInitializer initializer) => initializer.Name == "Name");
			if (variableInitializer.Name == null && variableInitializer.Value == null)
			{
				return null;
			}
			IConstantExpression2 constantExpression2 = variableInitializer.Value as IConstantExpression2;
			if (constantExpression2 == null || !constantExpression2.Value.IsText)
			{
				return null;
			}
			IDataSourceLocation dataSourceLocation = this.engine.NewLocation(mashupDiscovery.DataSourceReference.Location.Protocol, null, mashupDiscovery.DataSourceReference.Location.Address, null);
			dataSourceLocation.Address["database"] = constantExpression2.Value.AsString;
			return new MashupDiscovery(mashupDiscovery.Kind, mashupDiscovery.FunctionName, new DataSourceReference(dataSourceLocation), coordinate, mashupDiscovery.Options, mashupDiscovery.HasUnknownOptions, null);
		}

		// Token: 0x04000055 RID: 85
		private const string NavigationTableNameColumn = "Name";

		// Token: 0x04000056 RID: 86
		private const string Sql_Databases_Function = "Sql.Databases";

		// Token: 0x04000057 RID: 87
		private const string AnalysisServices_Databases_Function = "AnalysisServices.Databases";

		// Token: 0x04000058 RID: 88
		private const string Value_NativeQuery_Function = "Value.NativeQuery";

		// Token: 0x04000059 RID: 89
		private const string Resource_Access_Function = "Resource.Access";

		// Token: 0x0400005A RID: 90
		private const string Shared = "Shared";

		// Token: 0x0400005B RID: 91
		private readonly MashupDiscoveryOptions options;

		// Token: 0x0400005C RID: 92
		private readonly IEvaluationConstants evaluationConstants;

		// Token: 0x0400005D RID: 93
		private readonly IEngine engine;

		// Token: 0x0400005E RID: 94
		private readonly IValue placeholder;

		// Token: 0x0400005F RID: 95
		private readonly IRecordValue library;

		// Token: 0x04000060 RID: 96
		private readonly HashSet<string> modules;

		// Token: 0x04000061 RID: 97
		private readonly IValue resourceAccess;

		// Token: 0x04000062 RID: 98
		private readonly IFunctionValue[] databaseFunctions;

		// Token: 0x04000063 RID: 99
		private readonly IFunctionValue nativeQueryFunction;

		// Token: 0x04000064 RID: 100
		private readonly Dictionary<IFunctionValue, string> dataSourceFunctions;

		// Token: 0x04000065 RID: 101
		private readonly Dictionary<IExpression, MashupDiscovery> discoveries;

		// Token: 0x04000066 RID: 102
		private readonly Dictionary<IExpression, IExpression> metadata;

		// Token: 0x04000067 RID: 103
		private readonly List<IInvocationExpression> nativeQueries;

		// Token: 0x04000068 RID: 104
		private Dictionary<IExpression, IExpression> bindings;

		// Token: 0x04000069 RID: 105
		private HashSet<IExpression> boundExpressions;

		// Token: 0x0400006A RID: 106
		private DataSourceDiscoveryVisitor.ValueInterpreter valueInterpreter;

		// Token: 0x0400006B RID: 107
		private List<DataSourceDiscoveryVisitor.ResolverData> discoveredNavigationRoots;

		// Token: 0x0400006C RID: 108
		private IDocument currentDocument;

		// Token: 0x0400006D RID: 109
		private bool unboundNativeQuery;

		// Token: 0x0200005C RID: 92
		private class InvocationExpression : IInvocationExpression, IExpression, ISyntaxNode
		{
			// Token: 0x06000405 RID: 1029 RVA: 0x0000F398 File Offset: 0x0000D598
			public InvocationExpression(IEngine engine, IFunctionValue function, IValue[] arguments)
			{
				this.function = engine.ConstantExpression(function);
				this.arguments = new IExpression[arguments.Length];
				for (int i = 0; i < arguments.Length; i++)
				{
					this.arguments[i] = DataSourceDiscoveryVisitor.InvocationExpression.StripPlaceholders(engine, arguments[i]);
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000F3E5 File Offset: 0x0000D5E5
			public IExpression Function
			{
				get
				{
					return this.function;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000F3ED File Offset: 0x0000D5ED
			public IList<IExpression> Arguments
			{
				get
				{
					return this.arguments;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000F3F5 File Offset: 0x0000D5F5
			public ExpressionKind Kind
			{
				get
				{
					return ExpressionKind.Invocation;
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
			public TokenRange Range
			{
				get
				{
					return TokenRange.Null;
				}
			}

			// Token: 0x0600040A RID: 1034 RVA: 0x0000F3FF File Offset: 0x0000D5FF
			private static IExpression StripPlaceholders(IEngine engine, IValue value)
			{
				return engine.ConstantExpression(value);
			}

			// Token: 0x040001F8 RID: 504
			private readonly IExpression function;

			// Token: 0x040001F9 RID: 505
			private readonly IExpression[] arguments;
		}

		// Token: 0x0200005D RID: 93
		private class ElementAccessExpression : IElementAccessExpression, IExpression, ISyntaxNode
		{
			// Token: 0x0600040B RID: 1035 RVA: 0x0000F408 File Offset: 0x0000D608
			public ElementAccessExpression(IExpression collection, bool isOptional, IExpression key)
			{
				this.collection = collection;
				this.isOptional = isOptional;
				this.key = key;
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000F425 File Offset: 0x0000D625
			public IExpression Collection
			{
				get
				{
					return this.collection;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000F42D File Offset: 0x0000D62D
			public bool IsOptional
			{
				get
				{
					return this.isOptional;
				}
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000F435 File Offset: 0x0000D635
			public IExpression Key
			{
				get
				{
					return this.key;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000F43D File Offset: 0x0000D63D
			public ExpressionKind Kind
			{
				get
				{
					return ExpressionKind.ElementAccess;
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000F440 File Offset: 0x0000D640
			public TokenRange Range
			{
				get
				{
					return TokenRange.Null;
				}
			}

			// Token: 0x040001FA RID: 506
			private readonly IExpression collection;

			// Token: 0x040001FB RID: 507
			private readonly bool isOptional;

			// Token: 0x040001FC RID: 508
			private readonly IExpression key;
		}

		// Token: 0x0200005E RID: 94
		private class FieldAccessExpression : IFieldAccessExpression, IExpression, ISyntaxNode
		{
			// Token: 0x06000411 RID: 1041 RVA: 0x0000F447 File Offset: 0x0000D647
			public FieldAccessExpression(IExpression expression, bool isOptional, Identifier memberName)
			{
				this.expression = expression;
				this.isOptional = isOptional;
				this.memberName = memberName;
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000F464 File Offset: 0x0000D664
			public ExpressionKind Kind
			{
				get
				{
					return ExpressionKind.FieldAccess;
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000F467 File Offset: 0x0000D667
			public IExpression Expression
			{
				get
				{
					return this.expression;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000F46F File Offset: 0x0000D66F
			public bool IsOptional
			{
				get
				{
					return this.isOptional;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000F477 File Offset: 0x0000D677
			public Identifier MemberName
			{
				get
				{
					return this.memberName;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000F47F File Offset: 0x0000D67F
			public TokenRange Range
			{
				get
				{
					return TokenRange.Null;
				}
			}

			// Token: 0x040001FD RID: 509
			private readonly IExpression expression;

			// Token: 0x040001FE RID: 510
			private readonly bool isOptional;

			// Token: 0x040001FF RID: 511
			private readonly Identifier memberName;
		}

		// Token: 0x0200005F RID: 95
		private class RecordExpression : IRecordExpression, IExpression, ISyntaxNode, IDeclarator
		{
			// Token: 0x06000417 RID: 1047 RVA: 0x0000F486 File Offset: 0x0000D686
			public RecordExpression(IEnumerable<VariableInitializer> members)
			{
				this.members = members.ToList<VariableInitializer>();
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000F49A File Offset: 0x0000D69A
			public ExpressionKind Kind
			{
				get
				{
					return ExpressionKind.Record;
				}
			}

			// Token: 0x1700011A RID: 282
			public Identifier this[int index]
			{
				get
				{
					return this.members[index].Name;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000F4C1 File Offset: 0x0000D6C1
			public int Count
			{
				get
				{
					return this.members.Count;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000F4CE File Offset: 0x0000D6CE
			public Identifier Identifier
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000F4D1 File Offset: 0x0000D6D1
			public IList<VariableInitializer> Members
			{
				get
				{
					return this.members;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000F4D9 File Offset: 0x0000D6D9
			public TokenRange Range
			{
				get
				{
					return TokenRange.Null;
				}
			}

			// Token: 0x04000200 RID: 512
			private readonly List<VariableInitializer> members;
		}

		// Token: 0x02000060 RID: 96
		private class BindingVisitor : ScopedReadOnlyAstVisitor<IExpression>
		{
			// Token: 0x0600041E RID: 1054 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
			public static Dictionary<IExpression, IExpression> GetBindings(IRecordValue library, IEnumerable<IDocument> documents)
			{
				DataSourceDiscoveryVisitor.BindingVisitor bindingVisitor = new DataSourceDiscoveryVisitor.BindingVisitor();
				bindingVisitor.Visit(library, documents);
				return bindingVisitor.bindings;
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
			private void Visit(IRecordValue library, IEnumerable<IDocument> documents)
			{
				using (ScopedReadOnlyAstVisitor<IExpression>.EnvironmentScope environmentScope = base.EnterScope())
				{
					for (int i = 0; i < library.Keys.Length; i++)
					{
						environmentScope.Add(library.Keys[i], new DataSourceDiscoveryVisitor.BindingVisitor.LibraryMember(library[i]));
					}
					foreach (IDocument document in documents)
					{
						if (document.Kind == DocumentKind.Section)
						{
							ISectionDocument sectionDocument = (ISectionDocument)document;
							DataSourceDiscoveryVisitor.BindingVisitor.SectionBinding sectionBinding = new DataSourceDiscoveryVisitor.BindingVisitor.SectionBinding();
							foreach (ISectionMember sectionMember in sectionDocument.Section.Members)
							{
								if (sectionMember.Export)
								{
									environmentScope.Add(sectionMember.Name, sectionMember.Value);
								}
								if (!sectionBinding.Members.ContainsKey(sectionMember.Name))
								{
									sectionBinding.Members.Add(sectionMember.Name, sectionMember.Value);
								}
							}
							this.sectionBindings.Add(sectionDocument.Section.SectionName, sectionBinding);
						}
					}
					foreach (IDocument document2 in documents)
					{
						this.VisitDocument(document2);
					}
				}
			}

			// Token: 0x06000420 RID: 1056 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
			protected override IList<IExpression> CreateBindings(IDeclarator members)
			{
				return new IExpression[members.Count];
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x0000F6D0 File Offset: 0x0000D8D0
			private IList<IExpression> CreateBindings(IList<VariableInitializer> initializers)
			{
				IExpression[] array = new IExpression[initializers.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = initializers[i].Value;
				}
				return array;
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x0000F70C File Offset: 0x0000D90C
			private IList<IExpression> CreateBindings(IList<ISectionMember> initializers)
			{
				IExpression[] array = new IExpression[initializers.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = initializers[i].Value;
				}
				return array;
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x0000F743 File Offset: 0x0000D943
			protected override void VisitLet(ILetExpression let)
			{
				base.VisitLet(let, this.CreateBindings(let.Variables));
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x0000F758 File Offset: 0x0000D958
			protected override void VisitRecord(IRecordExpression record)
			{
				base.VisitRecord(record, null, this.CreateBindings(record.Members));
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x0000F76E File Offset: 0x0000D96E
			protected override void VisitModule(ISection module)
			{
				base.VisitModule(module, this.CreateBindings(module.Members));
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x0000F784 File Offset: 0x0000D984
			protected override void VisitIdentifier(IIdentifierExpression identifier)
			{
				IExpression expression;
				if (base.TryGetValue(identifier.Name, identifier.IsInclusive, out expression))
				{
					this.bindings[identifier] = expression;
				}
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
			protected override void VisitSectionIdentifier(ISectionIdentifierExpression sectionIdentifier)
			{
				DataSourceDiscoveryVisitor.BindingVisitor.SectionBinding sectionBinding;
				IExpression expression;
				if (this.sectionBindings.TryGetValue(sectionIdentifier.Section, out sectionBinding) && sectionBinding.Members.TryGetValue(sectionIdentifier.Name, out expression))
				{
					this.bindings[sectionIdentifier] = expression;
				}
			}

			// Token: 0x06000428 RID: 1064 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
			protected override Exception NewDepthLimitExceededException()
			{
				return new MashupException(ProviderErrorStrings.Expression_TooDeep);
			}

			// Token: 0x04000201 RID: 513
			private readonly Dictionary<IExpression, IExpression> bindings = new Dictionary<IExpression, IExpression>();

			// Token: 0x04000202 RID: 514
			private readonly Dictionary<Identifier, DataSourceDiscoveryVisitor.BindingVisitor.SectionBinding> sectionBindings = new Dictionary<Identifier, DataSourceDiscoveryVisitor.BindingVisitor.SectionBinding>();

			// Token: 0x02000093 RID: 147
			private class SectionBinding
			{
				// Token: 0x17000143 RID: 323
				// (get) Token: 0x06000523 RID: 1315 RVA: 0x00012D14 File Offset: 0x00010F14
				public Dictionary<Identifier, IExpression> Members
				{
					get
					{
						return this.members;
					}
				}

				// Token: 0x040002B8 RID: 696
				private readonly Dictionary<Identifier, IExpression> members = new Dictionary<Identifier, IExpression>();
			}

			// Token: 0x02000094 RID: 148
			private class LibraryMember : IConstantExpression2, IExpression, ISyntaxNode
			{
				// Token: 0x06000525 RID: 1317 RVA: 0x00012D2F File Offset: 0x00010F2F
				public LibraryMember(IValue value)
				{
					this.value = value;
				}

				// Token: 0x17000144 RID: 324
				// (get) Token: 0x06000526 RID: 1318 RVA: 0x00012D3E File Offset: 0x00010F3E
				public IValue Value
				{
					get
					{
						return this.value;
					}
				}

				// Token: 0x17000145 RID: 325
				// (get) Token: 0x06000527 RID: 1319 RVA: 0x00012D46 File Offset: 0x00010F46
				public ExpressionKind Kind
				{
					get
					{
						return ExpressionKind.Constant;
					}
				}

				// Token: 0x17000146 RID: 326
				// (get) Token: 0x06000528 RID: 1320 RVA: 0x00012D49 File Offset: 0x00010F49
				public TokenRange Range
				{
					get
					{
						return TokenRange.Null;
					}
				}

				// Token: 0x040002B9 RID: 697
				private readonly IValue value;
			}
		}

		// Token: 0x02000061 RID: 97
		private abstract class BindingInterpreter : ReadOnlyAstVisitor
		{
			// Token: 0x0600042A RID: 1066 RVA: 0x0000F822 File Offset: 0x0000DA22
			protected BindingInterpreter(Dictionary<IExpression, IExpression> bindings)
			{
				this.bindings = bindings;
			}

			// Token: 0x0600042B RID: 1067
			protected abstract void BindFailure();

			// Token: 0x0600042C RID: 1068 RVA: 0x0000F831 File Offset: 0x0000DA31
			protected override void VisitIdentifier(IIdentifierExpression identifier)
			{
				this.InterpretIdentifier(identifier);
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x0000F83A File Offset: 0x0000DA3A
			protected override void VisitSectionIdentifier(ISectionIdentifierExpression identifier)
			{
				this.InterpretIdentifier(identifier);
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x0000F843 File Offset: 0x0000DA43
			protected override Exception NewDepthLimitExceededException()
			{
				return new MashupException(ProviderErrorStrings.Expression_TooDeep);
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x0000F850 File Offset: 0x0000DA50
			private void InterpretIdentifier(IExpression identifier)
			{
				IExpression expression;
				if (this.bindings.TryGetValue(identifier, out expression) && expression != null && this.currentDepth < 8)
				{
					this.currentDepth++;
					this.VisitExpression(expression);
					this.currentDepth--;
					return;
				}
				this.BindFailure();
			}

			// Token: 0x04000203 RID: 515
			private const int DepthLimit = 8;

			// Token: 0x04000204 RID: 516
			private readonly Dictionary<IExpression, IExpression> bindings;

			// Token: 0x04000205 RID: 517
			private int currentDepth;
		}

		// Token: 0x02000062 RID: 98
		private class ValueInterpreter : DataSourceDiscoveryVisitor.BindingInterpreter
		{
			// Token: 0x06000430 RID: 1072 RVA: 0x0000F8A3 File Offset: 0x0000DAA3
			public ValueInterpreter(IEngine engine, IRecordValue library, IValue placeholder, Dictionary<IExpression, IExpression> bindings, IEvaluationConstants evaluationConstants)
				: base(bindings)
			{
				this.engine = engine;
				this.library = library;
				this.placeholder = placeholder;
				this.evaluationConstants = evaluationConstants;
				this.values = new List<IValue>();
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x0000F8D5 File Offset: 0x0000DAD5
			public IValue Interpret(IExpression expression)
			{
				this.values.Clear();
				base.Visit(expression);
				return this.Pop();
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x0000F8EF File Offset: 0x0000DAEF
			protected override void BindFailure()
			{
				this.value = this.placeholder;
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x0000F900 File Offset: 0x0000DB00
			protected override void VisitBinary(IBinaryExpression binary)
			{
				BinaryOperator2 @operator = binary.Operator;
				if (@operator != BinaryOperator2.MetadataAdd)
				{
					if (@operator == BinaryOperator2.Concatenate)
					{
						this.VisitExpression(binary.Left);
						IValue value = this.Pop();
						this.VisitExpression(binary.Right);
						IValue value2 = this.Pop();
						if (value.IsText && value2.IsText)
						{
							this.value = this.engine.Text(value.AsString + value2.AsString);
							return;
						}
					}
					this.value = this.placeholder;
					return;
				}
				this.VisitExpression(binary.Left);
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x0000F992 File Offset: 0x0000DB92
			protected override void VisitConstant(IConstantExpression2 constant)
			{
				this.value = constant.Value;
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
			protected override void VisitElementAccess(IElementAccessExpression elementAccess)
			{
				this.VisitExpression(elementAccess.Collection);
				IValue value = this.Pop();
				this.VisitExpression(elementAccess.Key);
				IValue value2 = this.Pop();
				int num = ((value2.IsNumber && value2.AsNumber.IsInteger32) ? value2.AsNumber.AsInteger32 : (-1));
				if (value.IsList && num >= 0 && num < value.AsList.Count)
				{
					this.value = value.AsList[num];
					return;
				}
				this.value = this.placeholder;
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x0000FA30 File Offset: 0x0000DC30
			protected override void VisitExpression(IExpression expression)
			{
				int count = this.values.Count;
				this.value = null;
				switch (expression.Kind)
				{
				case ExpressionKind.Exports:
				case ExpressionKind.Function:
				case ExpressionKind.MultiFieldRecordProjection:
				case ExpressionKind.NotImplemented:
				case ExpressionKind.Throw:
				case ExpressionKind.TryCatch:
				case ExpressionKind.Unary:
				case ExpressionKind.Verbatim:
				case ExpressionKind.Type:
				case ExpressionKind.RecordType:
				case ExpressionKind.ListType:
				case ExpressionKind.TableType:
				case ExpressionKind.NullableType:
				case ExpressionKind.FunctionType:
					this.value = this.placeholder;
					goto IL_009D;
				}
				base.VisitExpression(expression);
				IL_009D:
				int num = this.values.Count - count;
				this.values.RemoveRange(count, num);
				this.values.Add(this.value ?? this.placeholder);
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x0000FB10 File Offset: 0x0000DD10
			protected override void VisitFieldAccess(IFieldAccessExpression fieldAccess)
			{
				this.VisitExpression(fieldAccess.Expression);
				IValue value = this.Pop();
				if (value.IsRecord && value.AsRecord.TryGetValue(fieldAccess.MemberName, out value))
				{
					this.value = value;
					return;
				}
				this.value = this.placeholder;
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x0000FB68 File Offset: 0x0000DD68
			protected override void VisitIf(IIfExpression @if)
			{
				this.VisitExpression(@if.Condition);
				IValue value = this.Pop();
				if (value.IsLogical)
				{
					this.VisitExpression(value.AsBoolean ? @if.TrueCase : @if.FalseCase);
					return;
				}
				this.value = this.placeholder;
			}

			// Token: 0x06000439 RID: 1081 RVA: 0x0000FBBC File Offset: 0x0000DDBC
			protected override void VisitInvocation(IInvocationExpression invocation)
			{
				this.VisitExpression(invocation.Function);
				IValue value = this.Pop();
				IListValue listValue = this.VisitList(invocation.Arguments);
				if (value.IsFunction && this.IsFunctionAllowed(value.AsFunction) && !listValue.GetEnumerable().Any((IValueReference2 e) => e.Value == this.placeholder))
				{
					try
					{
						IValue[] array = (from a in listValue.GetEnumerable()
							select a.Value).ToArray<IValue>();
						this.value = this.engine.Invoke(value.AsFunction, array);
						return;
					}
					catch (Exception ex) when (ProviderTracing.TraceIsSafeException("DataSourceDiscoverVisitor/ValueInterpreter/VisitInvocation", ex, this.evaluationConstants, null))
					{
					}
				}
				this.value = this.placeholder;
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x0000FCAC File Offset: 0x0000DEAC
			protected override void VisitLet(ILetExpression let)
			{
				this.VisitExpression(let.Expression);
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x0000FCBA File Offset: 0x0000DEBA
			protected override void VisitList(IListExpression list)
			{
				this.value = this.VisitList(list.Members);
			}

			// Token: 0x0600043C RID: 1084 RVA: 0x0000FCD0 File Offset: 0x0000DED0
			private IListValue VisitList(IList<IExpression> list)
			{
				int count = this.values.Count;
				base.VisitListElements(list);
				int num = this.values.Count - count;
				return this.engine.List(this.values.GetRange(count, num).ToArray());
			}

			// Token: 0x0600043D RID: 1085 RVA: 0x0000FD1C File Offset: 0x0000DF1C
			protected override void VisitRecord(IRecordExpression record)
			{
				int count = this.values.Count;
				base.VisitRecord(record);
				int num = this.values.Count - count;
				try
				{
					IKeys keys = this.engine.Keys(record.Members.Select((VariableInitializer i) => i.Name.Name).ToArray<string>());
					this.value = this.engine.Record(keys, this.values.GetRange(count, num).ToArray());
				}
				catch (ValueException2)
				{
					this.value = this.placeholder;
				}
			}

			// Token: 0x0600043E RID: 1086 RVA: 0x0000FDCC File Offset: 0x0000DFCC
			private bool IsFunctionAllowed(IValue function)
			{
				foreach (string text in DataSourceDiscoveryVisitor.ValueInterpreter.allowedFunctions)
				{
					IValue value;
					if (this.library.TryGetValue(text, out value) && value == function)
					{
						return true;
					}
				}
				foreach (string text2 in DataSourceDiscoveryVisitor.ValueInterpreter.allowedConstructors)
				{
					IValue value2;
					if (this.engine.TryParseSourceValue(text2, out value2) && object.Equals(value2, function))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600043F RID: 1087 RVA: 0x0000FE40 File Offset: 0x0000E040
			private IValue Pop()
			{
				if (this.values.Count == 0)
				{
					throw new InvalidOperationException();
				}
				IValue value = this.values[this.values.Count - 1];
				this.values.RemoveAt(this.values.Count - 1);
				return value;
			}

			// Token: 0x04000206 RID: 518
			private static readonly string[] allowedFunctions = new string[] { "Uri.Combine" };

			// Token: 0x04000207 RID: 519
			private static readonly string[] allowedConstructors = new string[] { "#date", "#datetime", "#datetimezone", "#time", "#duration" };

			// Token: 0x04000208 RID: 520
			private readonly IEngine engine;

			// Token: 0x04000209 RID: 521
			private readonly IRecordValue library;

			// Token: 0x0400020A RID: 522
			private readonly IValue placeholder;

			// Token: 0x0400020B RID: 523
			private readonly List<IValue> values;

			// Token: 0x0400020C RID: 524
			private readonly IEvaluationConstants evaluationConstants;

			// Token: 0x0400020D RID: 525
			private IValue value;
		}

		// Token: 0x02000063 RID: 99
		private class DataSourceLocator : DataSourceDiscoveryVisitor.BindingInterpreter
		{
			// Token: 0x06000442 RID: 1090 RVA: 0x0000FEF3 File Offset: 0x0000E0F3
			public DataSourceLocator(Dictionary<IExpression, IExpression> bindings, Dictionary<IExpression, MashupDiscovery> discoveries)
				: base(bindings)
			{
				this.discoveries = discoveries;
			}

			// Token: 0x06000443 RID: 1091 RVA: 0x0000FF03 File Offset: 0x0000E103
			public KeyValuePair<IExpression, MashupDiscovery> Locate(IExpression expression)
			{
				this.foundExpression = null;
				this.foundDiscovery = null;
				this.failed = false;
				base.Visit(expression);
				return new KeyValuePair<IExpression, MashupDiscovery>(this.foundExpression, this.foundDiscovery);
			}

			// Token: 0x06000444 RID: 1092 RVA: 0x0000FF34 File Offset: 0x0000E134
			protected override void VisitExpression(IExpression expression)
			{
				if (this.failed)
				{
					return;
				}
				MashupDiscovery mashupDiscovery;
				if (!this.discoveries.TryGetValue(expression, out mashupDiscovery) || mashupDiscovery.Kind != MashupDiscoveryKind.DataSource)
				{
					base.VisitExpression(expression);
					return;
				}
				if (this.foundExpression != null)
				{
					this.failed = true;
					return;
				}
				this.foundExpression = expression;
				this.foundDiscovery = mashupDiscovery;
			}

			// Token: 0x06000445 RID: 1093 RVA: 0x0000FF88 File Offset: 0x0000E188
			protected override void BindFailure()
			{
				this.failed = true;
			}

			// Token: 0x0400020E RID: 526
			private readonly Dictionary<IExpression, MashupDiscovery> discoveries;

			// Token: 0x0400020F RID: 527
			private IExpression foundExpression;

			// Token: 0x04000210 RID: 528
			private MashupDiscovery foundDiscovery;

			// Token: 0x04000211 RID: 529
			private bool failed;
		}

		// Token: 0x02000064 RID: 100
		private class NavigationResolverVisitor : ReadOnlyAstVisitor
		{
			// Token: 0x06000446 RID: 1094 RVA: 0x0000FF91 File Offset: 0x0000E191
			private NavigationResolverVisitor(IEngine engine, Dictionary<IExpression, DataSourceDiscoveryVisitor.ResolverData> navigationExpressions, Dictionary<IExpression, IExpression> bindings, DataSourceDiscoveryVisitor.ValueInterpreter valueInterpreter)
			{
				this.engine = engine;
				this.navigationExpressions = navigationExpressions;
				this.bindings = bindings;
				this.valueInterpreter = valueInterpreter;
				this.visitedExpressions = new HashSet<IExpression>();
			}

			// Token: 0x06000447 RID: 1095 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
			public static IEnumerable<KeyValuePair<IExpression, MashupDiscovery>> Resolve(DataSourceDiscoveryVisitor dataSourceDiscoveryVisitor, IEnumerable<IDocument> documents)
			{
				List<DataSourceDiscoveryVisitor.ResolverData> discoveredNavigationRoots = dataSourceDiscoveryVisitor.discoveredNavigationRoots;
				Dictionary<IExpression, DataSourceDiscoveryVisitor.ResolverData> dictionary = discoveredNavigationRoots.ToDictionary((DataSourceDiscoveryVisitor.ResolverData data) => data.Expression);
				DataSourceDiscoveryVisitor.NavigationResolverVisitor navigationResolverVisitor = new DataSourceDiscoveryVisitor.NavigationResolverVisitor(dataSourceDiscoveryVisitor.engine, dictionary, dataSourceDiscoveryVisitor.bindings, dataSourceDiscoveryVisitor.valueInterpreter);
				foreach (IDocument document in documents)
				{
					navigationResolverVisitor.VisitDocument(document);
				}
				foreach (KeyValuePair<IExpression, IExpression> keyValuePair in dataSourceDiscoveryVisitor.bindings)
				{
					DataSourceDiscoveryVisitor.ResolverData resolverData;
					if (keyValuePair.Value != null && dictionary.TryGetValue(keyValuePair.Value, out resolverData))
					{
						resolverData.IncrementCount();
					}
				}
				return from kvp in discoveredNavigationRoots.SelectMany((DataSourceDiscoveryVisitor.ResolverData data) => data.CreateDataSources(dataSourceDiscoveryVisitor))
					select (kvp);
			}

			// Token: 0x06000448 RID: 1096 RVA: 0x00010114 File Offset: 0x0000E314
			protected override void VisitExpression(IExpression expression)
			{
				if (this.visitedExpressions.Add(expression))
				{
					base.VisitExpression(expression);
				}
			}

			// Token: 0x06000449 RID: 1097 RVA: 0x0001012C File Offset: 0x0000E32C
			protected override void VisitBinary(IBinaryExpression binary)
			{
				base.VisitBinary(binary);
				DataSourceDiscoveryVisitor.ResolverData resolverData;
				if (binary.Operator == BinaryOperator2.MetadataAdd && this.navigationExpressions.TryGetValue(binary.Left, out resolverData))
				{
					this.navigationExpressions[binary] = resolverData;
				}
			}

			// Token: 0x0600044A RID: 1098 RVA: 0x0001016C File Offset: 0x0000E36C
			protected override void VisitElementAccess(IElementAccessExpression elementAccess)
			{
				base.VisitElementAccess(elementAccess);
				IExpression expression = this.ResolveBinding(elementAccess.Collection);
				DataSourceDiscoveryVisitor.ResolverData resolverData;
				if (this.navigationExpressions.TryGetValue(expression, out resolverData))
				{
					IValue value = this.valueInterpreter.Interpret(elementAccess.Key);
					if (value.IsNumber || value.IsRecord)
					{
						IExpression expression2;
						if (value.IsRecord)
						{
							IRecordValue recordValue = value.AsRecord;
							expression2 = new DataSourceDiscoveryVisitor.RecordExpression(recordValue.Keys.Select((string k) => new VariableInitializer(k, this.engine.ConstantExpression(recordValue[k]))));
						}
						else
						{
							expression2 = this.engine.ConstantExpression(value);
						}
						IElementAccessExpression elementAccessExpression = new DataSourceDiscoveryVisitor.ElementAccessExpression(resolverData.ResolvedExpression, elementAccess.IsOptional, expression2);
						DataSourceDiscoveryVisitor.ResolverData resolverData2 = resolverData.AddNavigation(elementAccess, elementAccessExpression);
						this.navigationExpressions.Add(elementAccess, resolverData2);
					}
				}
			}

			// Token: 0x0600044B RID: 1099 RVA: 0x00010244 File Offset: 0x0000E444
			protected override void VisitFieldAccess(IFieldAccessExpression fieldAccess)
			{
				base.VisitFieldAccess(fieldAccess);
				IExpression expression = this.ResolveBinding(fieldAccess.Expression);
				DataSourceDiscoveryVisitor.ResolverData resolverData;
				if (this.navigationExpressions.TryGetValue(expression, out resolverData))
				{
					IFieldAccessExpression fieldAccessExpression = new DataSourceDiscoveryVisitor.FieldAccessExpression(resolverData.ResolvedExpression, fieldAccess.IsOptional, fieldAccess.MemberName);
					DataSourceDiscoveryVisitor.ResolverData resolverData2 = resolverData.AddNavigation(fieldAccess, fieldAccessExpression);
					this.navigationExpressions.Add(fieldAccess, resolverData2);
				}
			}

			// Token: 0x0600044C RID: 1100 RVA: 0x000102A4 File Offset: 0x0000E4A4
			private IExpression ResolveBinding(IExpression expression)
			{
				IExpression expression2;
				if (this.bindings.TryGetValue(expression, out expression2) && expression2 != null)
				{
					this.VisitExpression(expression2);
					return expression2;
				}
				return expression;
			}

			// Token: 0x04000212 RID: 530
			private readonly IEngine engine;

			// Token: 0x04000213 RID: 531
			private readonly Dictionary<IExpression, DataSourceDiscoveryVisitor.ResolverData> navigationExpressions;

			// Token: 0x04000214 RID: 532
			private readonly Dictionary<IExpression, IExpression> bindings;

			// Token: 0x04000215 RID: 533
			private readonly DataSourceDiscoveryVisitor.ValueInterpreter valueInterpreter;

			// Token: 0x04000216 RID: 534
			private readonly HashSet<IExpression> visitedExpressions;
		}

		// Token: 0x02000065 RID: 101
		private class ResolverData
		{
			// Token: 0x0600044D RID: 1101 RVA: 0x000102CE File Offset: 0x0000E4CE
			public ResolverData(MashupDiscovery discovered, IExpression expression, IExpression resolvedExpression)
			{
				this.discovered = discovered;
				this.expression = expression;
				this.resolvedExpression = resolvedExpression;
				this.navigatesTo = new Dictionary<IExpression, DataSourceDiscoveryVisitor.ResolverData>();
				this.instanceCount = 0;
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x0600044E RID: 1102 RVA: 0x000102FD File Offset: 0x0000E4FD
			public MashupDiscovery RootDiscovery
			{
				get
				{
					return this.discovered;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x0600044F RID: 1103 RVA: 0x00010305 File Offset: 0x0000E505
			public IExpression Expression
			{
				get
				{
					return this.expression;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001030D File Offset: 0x0000E50D
			public IExpression ResolvedExpression
			{
				get
				{
					return this.resolvedExpression;
				}
			}

			// Token: 0x06000451 RID: 1105 RVA: 0x00010315 File Offset: 0x0000E515
			public void IncrementCount()
			{
				this.instanceCount++;
			}

			// Token: 0x06000452 RID: 1106 RVA: 0x00010328 File Offset: 0x0000E528
			public DataSourceDiscoveryVisitor.ResolverData AddNavigation(IExpression expression, IExpression resolvedExpression)
			{
				DataSourceDiscoveryVisitor.ResolverData resolverData = new DataSourceDiscoveryVisitor.ResolverData(this.discovered, expression, resolvedExpression);
				this.navigatesTo.Add(expression, resolverData);
				return resolverData;
			}

			// Token: 0x06000453 RID: 1107 RVA: 0x00010351 File Offset: 0x0000E551
			public IEnumerable<KeyValuePair<IExpression, MashupDiscovery>> CreateDataSources(DataSourceDiscoveryVisitor visitor)
			{
				bool legacyBehavior = (visitor.options & MashupDiscoveryOptions.ReportNavigationSteps) == MashupDiscoveryOptions.None;
				bool flag = (visitor.options & MashupDiscoveryOptions.MultipleNavigationSteps) > MashupDiscoveryOptions.None;
				IEnumerable<KeyValuePair<IExpression, MashupDiscovery>> enumerable = this.navigatesTo.Values.SelectMany((DataSourceDiscoveryVisitor.ResolverData resolverData) => resolverData.CreateDataSources(visitor));
				bool includeCurrent = this.navigatesTo.Count < this.instanceCount || this.navigatesTo.Count == 0 || flag;
				if (legacyBehavior)
				{
					if (!includeCurrent)
					{
						List<KeyValuePair<IExpression, MashupDiscovery>> list = new List<KeyValuePair<IExpression, MashupDiscovery>>();
						foreach (KeyValuePair<IExpression, MashupDiscovery> keyValuePair in enumerable)
						{
							if (keyValuePair.Value == null)
							{
								includeCurrent = true;
								break;
							}
							list.Add(keyValuePair);
						}
						enumerable = list;
					}
					if (includeCurrent)
					{
						enumerable = Enumerable.Empty<KeyValuePair<IExpression, MashupDiscovery>>();
					}
				}
				foreach (KeyValuePair<IExpression, MashupDiscovery> keyValuePair2 in enumerable)
				{
					if (keyValuePair2.Value != null)
					{
						yield return keyValuePair2;
					}
					else
					{
						includeCurrent = true;
					}
				}
				IEnumerator<KeyValuePair<IExpression, MashupDiscovery>> enumerator2 = null;
				if (includeCurrent)
				{
					MashupDiscovery mashupDiscovery;
					if (legacyBehavior)
					{
						mashupDiscovery = visitor.LegacyCreateNavigatedDataSource(this.RootDiscovery.FunctionName, this.ResolvedExpression, this.RootDiscovery.Coordinate);
					}
					else
					{
						mashupDiscovery = visitor.SafeCreateDataSource(this.RootDiscovery.FunctionName, this.ResolvedExpression, this.RootDiscovery.Coordinate);
					}
					yield return new KeyValuePair<IExpression, MashupDiscovery>(this.Expression, mashupDiscovery);
				}
				yield break;
				yield break;
			}

			// Token: 0x04000217 RID: 535
			private readonly MashupDiscovery discovered;

			// Token: 0x04000218 RID: 536
			private readonly IExpression expression;

			// Token: 0x04000219 RID: 537
			private readonly IExpression resolvedExpression;

			// Token: 0x0400021A RID: 538
			private readonly Dictionary<IExpression, DataSourceDiscoveryVisitor.ResolverData> navigatesTo;

			// Token: 0x0400021B RID: 539
			private int instanceCount;
		}
	}
}
