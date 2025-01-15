using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Extensibility;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x02001702 RID: 5890
	internal sealed class ExtensionModule : Module
	{
		// Token: 0x060095A5 RID: 38309 RVA: 0x001EF424 File Offset: 0x001ED624
		private ExtensionModule(IEngine engine, ISectionDocument document, IModule library, ILibraryService libraryService, CompileOptions compileOptions)
		{
			this.engine = engine;
			this.libraryService = libraryService;
			this.library = library;
			this.lockObject = new object();
			this.compileOptions = compileOptions;
			this.innerModule = ExtensionModule.WrapForExtensibility(this, engine, document, compileOptions, out this.dataSourceFunctions);
			if (library != null)
			{
				this.module = engine.Link(new IModule[] { this.innerModule, library }, new Action<IError>(ExtensionModule.CompilationErrorHandler), LinkOptions.ExportFirstModule | LinkOptions.ExplicitEnvironment);
			}
			else
			{
				this.module = engine.Link(new IModule[] { this.innerModule }, new Action<IError>(ExtensionModule.CompilationErrorHandler), LinkOptions.ExportFirstModule);
			}
			ExtensionModule.TryGetDynamicModuleDataSourceKind(document.Section, out this.dynamicModuleDataSourceKind);
		}

		// Token: 0x1700271F RID: 10015
		// (get) Token: 0x060095A6 RID: 38310 RVA: 0x001EF4E1 File Offset: 0x001ED6E1
		public override string Name
		{
			get
			{
				return this.module.Name;
			}
		}

		// Token: 0x17002720 RID: 10016
		// (get) Token: 0x060095A7 RID: 38311 RVA: 0x000020FA File Offset: 0x000002FA
		public override string Location
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002721 RID: 10017
		// (get) Token: 0x060095A8 RID: 38312 RVA: 0x001EF4EE File Offset: 0x001ED6EE
		public override string Version
		{
			get
			{
				return this.module.Version;
			}
		}

		// Token: 0x17002722 RID: 10018
		// (get) Token: 0x060095A9 RID: 38313 RVA: 0x001EF4FB File Offset: 0x001ED6FB
		public override Keys ExportKeys
		{
			get
			{
				return (Keys)this.module.Exports;
			}
		}

		// Token: 0x17002723 RID: 10019
		// (get) Token: 0x060095AA RID: 38314 RVA: 0x001EF50D File Offset: 0x001ED70D
		public IEngine Engine
		{
			get
			{
				return this.engine;
			}
		}

		// Token: 0x060095AB RID: 38315 RVA: 0x001EF518 File Offset: 0x001ED718
		public static bool TryGetModuleName(IEngineHost engineHost, out string moduleName)
		{
			InvocationEngineHost invocationEngineHost = engineHost as InvocationEngineHost;
			ExtensionModule extensionModule;
			if (invocationEngineHost != null)
			{
				extensionModule = invocationEngineHost.EngineHost.QueryService<ExtensionModule>();
			}
			else
			{
				extensionModule = engineHost.QueryService<ExtensionModule>();
			}
			if (extensionModule == null || extensionModule.Name == null)
			{
				moduleName = null;
				return false;
			}
			moduleName = extensionModule.Name;
			return true;
		}

		// Token: 0x060095AC RID: 38316 RVA: 0x001EF560 File Offset: 0x001ED760
		private static void CompilationErrorHandler(IError error)
		{
			if (error.Location != null)
			{
				throw new InvalidOperationException(error.Location.Range.ToString() + " " + error.Message);
			}
			throw new InvalidOperationException(error.Message);
		}

		// Token: 0x060095AD RID: 38317 RVA: 0x001EF5B0 File Offset: 0x001ED7B0
		private static bool TryGetDynamicModuleDataSourceKind(ISection section, out string dynamicModuleDataSourceKind)
		{
			if (section.Attribute != null)
			{
				IExpression value = section.Attribute.Members.FirstOrDefault((VariableInitializer vi) => vi.Name == "DynamicMemberGenerator").Value;
				string memberName;
				if (value != null && value.TryGetStringConstant(out memberName))
				{
					ISectionMember sectionMember = section.Members.FirstOrDefault((ISectionMember s) => s.Name == memberName);
					if (sectionMember != null && sectionMember.Attribute != null)
					{
						IExpression value2 = sectionMember.Attribute.Members.FirstOrDefault((VariableInitializer m) => m.Name == "DataSource.Kind").Value;
						if (value2 != null && value2.TryGetStringConstant(out dynamicModuleDataSourceKind))
						{
							return true;
						}
					}
				}
			}
			dynamicModuleDataSourceKind = null;
			return false;
		}

		// Token: 0x060095AE RID: 38318 RVA: 0x001EF687 File Offset: 0x001ED887
		public static ExtensionModule Compile(IEngine engine, string source, ILibraryService libraryService, CompileOptions compileOptions)
		{
			return ExtensionModule.Compile(engine, engine.Parse(source, new Action<IError>(ExtensionModule.CompilationErrorHandler)), libraryService, compileOptions, null);
		}

		// Token: 0x060095AF RID: 38319 RVA: 0x001EF6A8 File Offset: 0x001ED8A8
		public static ExtensionModule Compile(IEngine engine, IDocument document, ILibraryService libraryService, CompileOptions compileOptions, IModule moduleInfo = null)
		{
			if (document.Kind == DocumentKind.Expression)
			{
				ExtensionModule.ExtensionInfo extensionInfo = ExtensionModule.UnderstandExpressionExtension(engine, (IExpressionDocument)document, libraryService);
				document = ExtensionModule.WrapExpressionExtension(engine, extensionInfo, (IExpressionDocument)document);
			}
			return new ExtensionModule(engine, (ISectionDocument)document, null, libraryService, compileOptions);
		}

		// Token: 0x060095B0 RID: 38320 RVA: 0x001EF6EA File Offset: 0x001ED8EA
		public static ExtensionModule Compile(IEngine engine, ISectionDocument dataSourceDocument, IModule library, ILibraryService libraryService, CompileOptions compileOptions)
		{
			return new ExtensionModule(engine, dataSourceDocument, library, libraryService, compileOptions);
		}

		// Token: 0x060095B1 RID: 38321 RVA: 0x001EF6F8 File Offset: 0x001ED8F8
		public static ISectionDocument WrapExpressionExtension(IEngine engine, IExpressionDocument document, ILibraryService libraryService, out Exception error)
		{
			ISectionDocument sectionDocument;
			try
			{
				error = null;
				ExtensionModule.ExtensionInfo extensionInfo = ExtensionModule.UnderstandExpressionExtension(engine, document, libraryService);
				sectionDocument = ExtensionModule.WrapExpressionExtension(engine, extensionInfo, document);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				error = ex;
				sectionDocument = null;
			}
			return sectionDocument;
		}

		// Token: 0x060095B2 RID: 38322 RVA: 0x001EF740 File Offset: 0x001ED940
		public ILibraryService GetLibraryService(IEngineHost engineHost)
		{
			if (engineHost == null)
			{
				return this.libraryService;
			}
			return engineHost.QueryService<ILibraryService>() ?? this.libraryService;
		}

		// Token: 0x060095B3 RID: 38323 RVA: 0x001EF75C File Offset: 0x001ED95C
		private static ExtensionModule.ExtensionInfo UnderstandExpressionExtension(IEngine engine, IExpressionDocument document, ILibraryService libraryService)
		{
			ExtensionModule.ExtensionHost extensionHost = new ExtensionModule.ExtensionHost(engine, libraryService);
			RecordValue recordValue = Modules.GetLibrary(extensionHost, Extension.Modules);
			IModule module = engine.Compile(document, RecordValue.Empty, CompileOptions.None, new Action<IError>(ExtensionModule.CompilationErrorHandler));
			IAssembly assembly = engine.Assemble(new IModule[] { module }, recordValue, extensionHost, new Action<IError>(ExtensionModule.CompilationErrorHandler));
			RecordValue recordValue2 = (RecordValue)engine.Invoke(assembly.Function, Array.Empty<IValue>()).AsRecord;
			Dictionary<string, string> dictionary = new Dictionary<string, string>(recordValue2.Count);
			KeysBuilder keysBuilder = new KeysBuilder(recordValue2.Count);
			for (int i = 0; i < recordValue2.Count; i++)
			{
				Value value = recordValue2[i];
				Value value2;
				Value value3;
				if (value.IsFunction && value.TryGetMetaField("DataSource.Kind", out value2) && value2.IsText && value.TryGetMetaField("Publish", out value3))
				{
					dictionary.Add(recordValue2.Keys[i], value2.AsString);
					if (value3.IsRecord)
					{
						keysBuilder.Add(recordValue2.Keys[i]);
					}
				}
			}
			string text = null;
			Value value4;
			if (recordValue2.MetaValue.TryGetValue("Name", out value4) && value4.IsText)
			{
				text = value4.AsString;
			}
			return new ExtensionModule.ExtensionInfo(text, null, recordValue2.Keys, keysBuilder.ToKeys(), dictionary);
		}

		// Token: 0x060095B4 RID: 38324 RVA: 0x001EF8C4 File Offset: 0x001EDAC4
		private static ISectionDocument WrapExpressionExtension(IEngine engine, ExtensionModule.ExtensionInfo info, IExpressionDocument document)
		{
			List<ISectionMember> list = new List<ISectionMember>(1 + 2 * info.Exports.Length + info.Published.Length);
			string text = Guid.NewGuid().ToString();
			Identifier identifier = Identifier.New(text + ":base");
			IExpression expression = new ExclusiveIdentifierExpressionSyntaxNode(identifier);
			HashSet<string> hashSet = new HashSet<string>();
			list.Add(new ModuleMemberSyntaxNode(null, false, identifier, document.Expression, TokenRange.Null));
			foreach (string text2 in info.Exports)
			{
				Identifier identifier2 = Identifier.New(text2);
				IRecordExpression recordExpression = null;
				string text3 = null;
				string text4;
				if (info.TryGetResourceKind(text2, out text4))
				{
					ArrayBuilder<VariableInitializer> arrayBuilder = new ArrayBuilder<VariableInitializer>(2);
					arrayBuilder.Add(new VariableInitializer("DataSource.Kind", engine.ConstantExpression(TextValue.New(text4))));
					if (info.Published.Contains(text2))
					{
						text3 = text + ":publish:" + text2;
						arrayBuilder.Add(new VariableInitializer("Publish", engine.ConstantExpression(TextValue.New(text3))));
					}
					recordExpression = new RecordExpressionSyntaxNode(arrayBuilder.ToArray());
					if (hashSet.Add(text4))
					{
						Identifier identifier3 = Identifier.New(text4);
						list.Add(new ModuleMemberSyntaxNode(null, false, identifier3, new RequiredFieldAccessExpressionSyntaxNode(new RequiredFieldAccessExpressionSyntaxNode(new InvocationExpressionSyntaxNode1(ExtensionModule.valueMetadataFunction, expression), Identifier.New("DataSource.Kind")), identifier3), TokenRange.Null));
					}
				}
				IExpression expression2 = new RequiredFieldAccessExpressionSyntaxNode(expression, identifier2);
				list.Add(new ModuleMemberSyntaxNode(recordExpression, true, identifier2, expression2, TokenRange.Null));
				if (text3 != null)
				{
					IExpression expression3 = new RequiredFieldAccessExpressionSyntaxNode(new InvocationExpressionSyntaxNode1(ExtensionModule.valueMetadataFunction, expression2), ExtensionModule.publishIdentifier);
					list.Add(new ModuleMemberSyntaxNode(null, false, Identifier.New(text3), expression3, TokenRange.Null));
				}
			}
			return new ModuleDocumentSyntaxNode(document.Host, document.Tokens, new ModuleSyntaxNode(null, Identifier.New(info.Name), list, TokenRange.Null), TokenRange.Null);
		}

		// Token: 0x17002724 RID: 10020
		// (get) Token: 0x060095B5 RID: 38325 RVA: 0x001EFAF8 File Offset: 0x001EDCF8
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				if (this.dataSources == null)
				{
					object obj = this.lockObject;
					lock (obj)
					{
						if (this.dataSources == null)
						{
							this.dataSources = this.GetDataSources();
						}
					}
				}
				return this.dataSources;
			}
		}

		// Token: 0x17002725 RID: 10021
		// (get) Token: 0x060095B6 RID: 38326 RVA: 0x001EFB54 File Offset: 0x001EDD54
		public override ResourceKindInfo DynamicModuleDataSource
		{
			get
			{
				return this.DataSources.FirstOrDefault((ResourceKindInfo info) => info.Kind == this.dynamicModuleDataSourceKind);
			}
		}

		// Token: 0x17002726 RID: 10022
		// (get) Token: 0x060095B7 RID: 38327 RVA: 0x001EFB6D File Offset: 0x001EDD6D
		public override RecordValue Metadata
		{
			get
			{
				return (RecordValue)this.module.Metadata;
			}
		}

		// Token: 0x060095B8 RID: 38328 RVA: 0x001EFB80 File Offset: 0x001EDD80
		public IAssembly RelinkDataSourceFunctions(IEngineHost engineHost, params string[] functionNames)
		{
			RecordValue recordValue = this.GetLibrary(engineHost);
			IModule module = this.engine.Compile(new ExpressionDocumentSyntaxNode(new ListExpressionSyntaxNode(functionNames.Select(delegate(string name)
			{
				if (name != null)
				{
					return new SectionIdentifierExpressionSyntaxNode(this.Name, name, TokenRange.Null);
				}
				return ConstantExpressionSyntaxNode.Null;
			}).ToList<IExpression>())), RecordValue.Empty, this.compileOptions, new Action<IError>(ExtensionModule.CompilationErrorHandler));
			return this.engine.Assemble(new IModule[] { module, this.module }, recordValue, engineHost, new Action<IError>(ExtensionModule.CompilationErrorHandler));
		}

		// Token: 0x060095B9 RID: 38329 RVA: 0x001EFC08 File Offset: 0x001EDE08
		public RecordValue RelinkAuthRecord(IEngineHost engineHost, string resourceKind)
		{
			engineHost = new ExtensionModule.ExtensionModuleEngineHost(engineHost, this);
			RecordValue recordValue = this.GetLibrary(engineHost);
			IModule module = this.engine.Compile(new ExpressionDocumentSyntaxNode(new RequiredFieldAccessExpressionSyntaxNode(new SectionIdentifierExpressionSyntaxNode(this.Name, resourceKind, TokenRange.Null), "Authentication")), RecordValue.Empty, this.compileOptions, new Action<IError>(ExtensionModule.CompilationErrorHandler));
			IAssembly assembly = this.engine.Assemble(new IModule[] { module, this.module }, recordValue, engineHost, new Action<IError>(ExtensionModule.CompilationErrorHandler));
			return (RecordValue)this.engine.Invoke(assembly.Function, Array.Empty<IValue>()).AsRecord;
		}

		// Token: 0x060095BA RID: 38330 RVA: 0x001EFCC8 File Offset: 0x001EDEC8
		private ResourceKindInfo[] GetDataSources()
		{
			if (this.dataSourceFunctions == null)
			{
				return EmptyArray<ResourceKindInfo>.Instance;
			}
			Dictionary<string, List<VariableInitializer>> dictionary = new Dictionary<string, List<VariableInitializer>>(this.dataSourceFunctions.Count);
			foreach (KeyValuePair<string, string> keyValuePair in this.dataSourceFunctions)
			{
				List<VariableInitializer> list;
				if (!dictionary.TryGetValue(keyValuePair.Value, out list))
				{
					list = new List<VariableInitializer>();
					dictionary[keyValuePair.Value] = list;
				}
				list.Add(new VariableInitializer(ExtensionModule.DecodeFunctionName(keyValuePair.Key), new SectionIdentifierExpressionSyntaxNode(this.Name, keyValuePair.Key, TokenRange.Null)));
			}
			VariableInitializer[] array = new VariableInitializer[dictionary.Count];
			int num = 0;
			foreach (KeyValuePair<string, List<VariableInitializer>> keyValuePair2 in dictionary)
			{
				array[num] = new VariableInitializer(keyValuePair2.Key, new ListExpressionSyntaxNode(new IExpression[]
				{
					new SectionIdentifierExpressionSyntaxNode(this.Name, keyValuePair2.Key, TokenRange.Null),
					new RecordExpressionSyntaxNode(keyValuePair2.Value.ToArray())
				}));
				num++;
			}
			IModule module = this.engine.Compile(new ExpressionDocumentSyntaxNode(new RecordExpressionSyntaxNode(array)), RecordValue.Empty, this.compileOptions, new Action<IError>(ExtensionModule.CompilationErrorHandler));
			IEngineHost engineHost = new ExtensionModule.ExtensionHost(EngineHost.Empty, this);
			RecordValue recordValue = this.GetLibrary(EngineHost.Empty);
			IModule[] array3;
			if (this.library != null)
			{
				IModule[] array2 = new IModule[3];
				array2[0] = module;
				array2[1] = this.innerModule;
				array3 = array2;
				array2[2] = this.library;
			}
			else
			{
				IModule[] array4 = new IModule[2];
				array4[0] = module;
				array3 = array4;
				array4[1] = this.innerModule;
			}
			IModule[] array5 = array3;
			IAssembly assembly = this.engine.Assemble(array5, recordValue, engineHost, new Action<IError>(ExtensionModule.CompilationErrorHandler));
			IRecordValue asRecord = this.engine.Invoke(assembly.Function, Array.Empty<IValue>()).AsRecord;
			return ExtensibilityModule.GetDataSources(this, (RecordValue)asRecord);
		}

		// Token: 0x060095BB RID: 38331 RVA: 0x001EFF0C File Offset: 0x001EE10C
		private RecordValue GetLibrary(IEngineHost engineHost)
		{
			if (this.library == null)
			{
				return (RecordValue)this.engine.GetLibrary(engineHost, Extension.Modules);
			}
			return RecordValue.Empty;
		}

		// Token: 0x060095BC RID: 38332 RVA: 0x001EFF32 File Offset: 0x001EE132
		public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
		{
			hostEnvironment = new ExtensionModule.ExtensionHost(hostEnvironment, this);
			return ((Module)this.module).Link(environment, hostEnvironment);
		}

		// Token: 0x060095BD RID: 38333 RVA: 0x001EFF50 File Offset: 0x001EE150
		private static Module WrapForExtensibility(ExtensionModule extensionModule, IEngine engine, ISectionDocument document, CompileOptions compileOptions, out Dictionary<string, string> dataAccessFunctions)
		{
			dataAccessFunctions = new Dictionary<string, string>();
			List<ISectionMember> list = new List<ISectionMember>(2 * document.Section.Members.Count);
			string text = document.Section.SectionName;
			foreach (ISectionMember sectionMember in document.Section.Members)
			{
				string text2 = null;
				IExpression expression = sectionMember.Value;
				IExpression expression2;
				IExpression expression3;
				if (ExtensionModule.TryGetScopedInvocation(expression, out expression2, out expression3, out text2))
				{
					expression = expression2;
				}
				Value @null = Value.Null;
				VariableInitializer variableInitializer = default(VariableInitializer);
				if (sectionMember.Attribute != null)
				{
					VariableInitializer variableInitializer2 = sectionMember.Attribute.Members.FirstOrDefault((VariableInitializer vi) => vi.Name == "DataSource.Kind");
					if (variableInitializer2.Name != null && variableInitializer2.Value.TryGetConstant(out @null) && @null.IsText)
					{
						variableInitializer = sectionMember.Attribute.Members.FirstOrDefault((VariableInitializer vi) => vi.Name == "Publish");
						text2 = "Extension.DataSource";
					}
				}
				if (text2 != null)
				{
					string text3 = ExtensionModule.EncodeFunctionName(sectionMember.Name);
					list.Add(new ModuleMemberSyntaxNode(sectionMember.Attribute, false, text3, expression, sectionMember.Range));
					string text4 = null;
					IExpression expression4 = engine.ConstantExpression(engine.Null);
					if (expression3 != null)
					{
						string text5 = ExtensionModule.EncodeFunctionName(sectionMember.Name);
						list.Add(new ModuleMemberSyntaxNode(null, false, text5, expression3, expression3.Range));
						if (text2 == "Extension.DataSource")
						{
							text4 = text5;
						}
						else
						{
							expression4 = new SectionIdentifierExpressionSyntaxNode(text, text5, TokenRange.Null);
						}
					}
					Value value;
					IExpression expression5;
					if (variableInitializer.Name != null && variableInitializer.Value.TryGetConstant(out value) && value.IsText)
					{
						expression5 = new SectionIdentifierExpressionSyntaxNode(text, value.AsString, TokenRange.Null);
					}
					else
					{
						expression5 = engine.ConstantExpression(engine.Null);
					}
					FunctionValue functionValue = ExtensibilityModule.MakeExtensionFunctionInfo(extensionModule, @null.IsText ? @null.AsString : null, sectionMember.Name, text3, text4, text2);
					list.Add(new ModuleMemberSyntaxNode(sectionMember.Attribute, sectionMember.Export, sectionMember.Name, new InvocationExpressionSyntaxNodeN(new SectionIdentifierExpressionSyntaxNode(Modules.Extensibility.Name, "DataSource.Function", TokenRange.Null), new IExpression[]
					{
						engine.ConstantExpression(functionValue),
						new SectionIdentifierExpressionSyntaxNode(text, text3, TokenRange.Null),
						expression4,
						expression5
					}), sectionMember.Range));
					if (@null.IsText)
					{
						dataAccessFunctions[text3] = @null.AsText.String;
					}
				}
				else
				{
					list.Add(sectionMember);
				}
			}
			if (dataAccessFunctions.Count == 0)
			{
				dataAccessFunctions = null;
			}
			document = new ModuleDocumentSyntaxNode(document.Host, document.Tokens, new ModuleSyntaxNode(document.Section.Attribute, document.Section.SectionName, list, document.Section.Range), document.Range);
			IModule module = engine.Compile(document, RecordValue.Empty, compileOptions, new Action<IError>(ExtensionModule.CompilationErrorHandler));
			return (Module)engine.Link(new IModule[]
			{
				module,
				Modules.Extensibility
			}, new Action<IError>(ExtensionModule.CompilationErrorHandler), LinkOptions.ExportFirstModule);
		}

		// Token: 0x060095BE RID: 38334 RVA: 0x001F0320 File Offset: 0x001EE520
		private static string EncodeFunctionName(string name)
		{
			return Guid.NewGuid().ToString() + ":" + name;
		}

		// Token: 0x060095BF RID: 38335 RVA: 0x001F034B File Offset: 0x001EE54B
		private static string DecodeFunctionName(string name)
		{
			return name.Substring(name.IndexOf(':') + 1);
		}

		// Token: 0x060095C0 RID: 38336 RVA: 0x001F0360 File Offset: 0x001EE560
		private static bool TryGetScopedInvocation(IExpression expression, out IExpression newExpression, out IExpression handler, out string invocationKind)
		{
			newExpression = null;
			handler = null;
			invocationKind = null;
			if (expression.Kind != ExpressionKind.Function)
			{
				return false;
			}
			IFunctionExpression functionExpression = (IFunctionExpression)expression;
			if (functionExpression.Expression.Kind != ExpressionKind.Invocation)
			{
				return false;
			}
			IInvocationExpression invocationExpression = (IInvocationExpression)functionExpression.Expression;
			Identifier identifier;
			if (invocationExpression.Arguments.Count != 2 || !invocationExpression.Function.TryGetIdentifier(out identifier) || (identifier != "Extension.InvokeWithCredentials" && identifier != "Extension.InvokeWithPermissions") || invocationExpression.Arguments[0].Kind != ExpressionKind.Function || invocationExpression.Arguments[1].Kind != ExpressionKind.Function)
			{
				return false;
			}
			IFunctionExpression functionExpression2 = (IFunctionExpression)invocationExpression.Arguments[0];
			if (functionExpression2.FunctionType.Parameters.Count != 1)
			{
				return false;
			}
			IFunctionExpression functionExpression3 = (IFunctionExpression)invocationExpression.Arguments[1];
			if (functionExpression3.FunctionType.Parameters.Count != 0)
			{
				return false;
			}
			newExpression = new FunctionExpressionSyntaxNode(functionExpression.FunctionType, functionExpression3.Expression, functionExpression3.Range);
			handler = new FunctionExpressionSyntaxNode(functionExpression.FunctionType, functionExpression2, functionExpression2.Range);
			invocationKind = identifier.Name;
			return true;
		}

		// Token: 0x04004F8A RID: 20362
		public const string PublishKey = "Publish";

		// Token: 0x04004F8B RID: 20363
		public const string DataSourceKindKey = "DataSource.Kind";

		// Token: 0x04004F8C RID: 20364
		public const string NameKey = "Name";

		// Token: 0x04004F8D RID: 20365
		private static readonly Identifier publishIdentifier = Identifier.New("Publish");

		// Token: 0x04004F8E RID: 20366
		private static readonly IExpression valueMetadataFunction = new ExclusiveIdentifierExpressionSyntaxNode(Identifier.New("Value.Metadata"));

		// Token: 0x04004F8F RID: 20367
		private readonly object lockObject;

		// Token: 0x04004F90 RID: 20368
		private readonly IEngine engine;

		// Token: 0x04004F91 RID: 20369
		private readonly ILibraryService libraryService;

		// Token: 0x04004F92 RID: 20370
		private readonly IModule library;

		// Token: 0x04004F93 RID: 20371
		private readonly CompileOptions compileOptions;

		// Token: 0x04004F94 RID: 20372
		private readonly IModule innerModule;

		// Token: 0x04004F95 RID: 20373
		private readonly Dictionary<string, string> dataSourceFunctions;

		// Token: 0x04004F96 RID: 20374
		private readonly IModule module;

		// Token: 0x04004F97 RID: 20375
		private readonly string dynamicModuleDataSourceKind;

		// Token: 0x04004F98 RID: 20376
		private ResourceKindInfo[] dataSources;

		// Token: 0x02001703 RID: 5891
		private class ExtensionInfo : IModule
		{
			// Token: 0x060095C4 RID: 38340 RVA: 0x001F0505 File Offset: 0x001EE705
			public ExtensionInfo(string name, string version, Keys exports, Keys published, Dictionary<string, string> resourceMap)
			{
				this.name = name;
				this.version = version;
				this.exports = exports;
				this.published = published;
				this.resourceMap = resourceMap;
			}

			// Token: 0x17002727 RID: 10023
			// (get) Token: 0x060095C5 RID: 38341 RVA: 0x001F0532 File Offset: 0x001EE732
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17002728 RID: 10024
			// (get) Token: 0x060095C6 RID: 38342 RVA: 0x001F053A File Offset: 0x001EE73A
			public string Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x17002729 RID: 10025
			// (get) Token: 0x060095C7 RID: 38343 RVA: 0x001F0542 File Offset: 0x001EE742
			public IKeys Exports
			{
				get
				{
					return this.exports;
				}
			}

			// Token: 0x1700272A RID: 10026
			// (get) Token: 0x060095C8 RID: 38344 RVA: 0x001F054A File Offset: 0x001EE74A
			public IKeys Published
			{
				get
				{
					return this.published;
				}
			}

			// Token: 0x1700272B RID: 10027
			// (get) Token: 0x060095C9 RID: 38345 RVA: 0x00019E61 File Offset: 0x00018061
			public IRecordValue Metadata
			{
				get
				{
					return RecordValue.Empty;
				}
			}

			// Token: 0x1700272C RID: 10028
			// (get) Token: 0x060095CA RID: 38346 RVA: 0x000091AE File Offset: 0x000073AE
			public ResourceKindInfo[] DataSources
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x1700272D RID: 10029
			// (get) Token: 0x060095CB RID: 38347 RVA: 0x000091AE File Offset: 0x000073AE
			public ResourceKindInfo DynamicModuleDataSource
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x060095CC RID: 38348 RVA: 0x001F0552 File Offset: 0x001EE752
			public bool TryGetResourceKind(string export, out string resourceKind)
			{
				return this.resourceMap.TryGetValue(export, out resourceKind);
			}

			// Token: 0x04004F99 RID: 20377
			private readonly string name;

			// Token: 0x04004F9A RID: 20378
			private readonly string version;

			// Token: 0x04004F9B RID: 20379
			private readonly Keys exports;

			// Token: 0x04004F9C RID: 20380
			private readonly Keys published;

			// Token: 0x04004F9D RID: 20381
			private readonly Dictionary<string, string> resourceMap;
		}

		// Token: 0x02001704 RID: 5892
		private class ExtensionHost : IEngineHost
		{
			// Token: 0x060095CD RID: 38349 RVA: 0x001F0561 File Offset: 0x001EE761
			public ExtensionHost(IEngine engine, ILibraryService libraryService)
			{
				this.engineHost = EngineHost.Empty;
				this.engine = engine;
				this.libraryService = libraryService;
			}

			// Token: 0x060095CE RID: 38350 RVA: 0x001F0582 File Offset: 0x001EE782
			public ExtensionHost(IEngineHost engineHost, ExtensionModule extension)
			{
				this.engineHost = engineHost;
				this.engine = extension.Engine;
				this.extension = extension;
				this.libraryService = extension.GetLibraryService(engineHost);
			}

			// Token: 0x060095CF RID: 38351 RVA: 0x001F05B4 File Offset: 0x001EE7B4
			public T QueryService<T>() where T : class
			{
				if (typeof(T) == typeof(IEngine))
				{
					return (T)((object)this.engine);
				}
				if (typeof(T) == typeof(ExtensionModule))
				{
					return (T)((object)this.extension);
				}
				if (typeof(T) == typeof(ILibraryService))
				{
					return (T)((object)this.libraryService);
				}
				return this.engineHost.QueryService<T>();
			}

			// Token: 0x04004F9E RID: 20382
			private readonly IEngineHost engineHost;

			// Token: 0x04004F9F RID: 20383
			private readonly IEngine engine;

			// Token: 0x04004FA0 RID: 20384
			private readonly ExtensionModule extension;

			// Token: 0x04004FA1 RID: 20385
			private readonly ILibraryService libraryService;
		}

		// Token: 0x02001705 RID: 5893
		private sealed class ExtensionModuleEngineHost : IEngineHost
		{
			// Token: 0x060095D0 RID: 38352 RVA: 0x001F0641 File Offset: 0x001EE841
			public ExtensionModuleEngineHost(IEngineHost engineHost, ExtensionModule module)
			{
				this.engineHost = engineHost;
				this.module = module;
			}

			// Token: 0x060095D1 RID: 38353 RVA: 0x001F0657 File Offset: 0x001EE857
			T IEngineHost.QueryService<T>()
			{
				if (typeof(T) == typeof(ExtensionModule))
				{
					return (T)((object)this.module);
				}
				return this.engineHost.QueryService<T>();
			}

			// Token: 0x04004FA2 RID: 20386
			private readonly IEngineHost engineHost;

			// Token: 0x04004FA3 RID: 20387
			private readonly ExtensionModule module;
		}
	}
}
