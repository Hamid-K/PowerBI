using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library.Access;
using Microsoft.Mashup.Engine1.Library.AccessControlEntries;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.ActiveDirectory;
using Microsoft.Mashup.Engine1.Library.AdobeAnalytics;
using Microsoft.Mashup.Engine1.Library.AdoDotNet;
using Microsoft.Mashup.Engine1.Library.AnalysisServices;
using Microsoft.Mashup.Engine1.Library.AzureBlobs;
using Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage;
using Microsoft.Mashup.Engine1.Library.AzureTables;
using Microsoft.Mashup.Engine1.Library.BinaryFormat;
using Microsoft.Mashup.Engine1.Library.CacheManager;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Cdm;
using Microsoft.Mashup.Engine1.Library.Cdpa;
using Microsoft.Mashup.Engine1.Library.Crypto;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Delta;
using Microsoft.Mashup.Engine1.Library.DeltaLake;
using Microsoft.Mashup.Engine1.Library.Diagnostics;
using Microsoft.Mashup.Engine1.Library.Drda;
using Microsoft.Mashup.Engine1.Library.Environment;
using Microsoft.Mashup.Engine1.Library.Essbase;
using Microsoft.Mashup.Engine1.Library.Excel;
using Microsoft.Mashup.Engine1.Library.ExcelTableInference;
using Microsoft.Mashup.Engine1.Library.Exchange;
using Microsoft.Mashup.Engine1.Library.Extensibility;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.FuzzyGroup;
using Microsoft.Mashup.Engine1.Library.FuzzyMatching;
using Microsoft.Mashup.Engine1.Library.GoogleAnalytics;
using Microsoft.Mashup.Engine1.Library.Graph;
using Microsoft.Mashup.Engine1.Library.Hdfs;
using Microsoft.Mashup.Engine1.Library.HDInsight;
using Microsoft.Mashup.Engine1.Library.Html;
using Microsoft.Mashup.Engine1.Library.HtmlTable;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Lineage;
using Microsoft.Mashup.Engine1.Library.Lines;
using Microsoft.Mashup.Engine1.Library.MQ;
using Microsoft.Mashup.Engine1.Library.MySQL;
using Microsoft.Mashup.Engine1.Library.Normalization;
using Microsoft.Mashup.Engine1.Library.OAuthLibrary;
using Microsoft.Mashup.Engine1.Library.OData;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Library.OleDb;
using Microsoft.Mashup.Engine1.Library.Oracle;
using Microsoft.Mashup.Engine1.Library.Package;
using Microsoft.Mashup.Engine1.Library.PageReaderConformance;
using Microsoft.Mashup.Engine1.Library.ParallelEvaluation;
using Microsoft.Mashup.Engine1.Library.Parquet;
using Microsoft.Mashup.Engine1.Library.Pdf;
using Microsoft.Mashup.Engine1.Library.PostgreSQL;
using Microsoft.Mashup.Engine1.Library.RData;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse;
using Microsoft.Mashup.Engine1.Library.SapHana;
using Microsoft.Mashup.Engine1.Library.SharePoint;
using Microsoft.Mashup.Engine1.Library.Spatial;
using Microsoft.Mashup.Engine1.Library.Sql;
using Microsoft.Mashup.Engine1.Library.SqlDatabase;
using Microsoft.Mashup.Engine1.Library.SqlExpression;
using Microsoft.Mashup.Engine1.Library.Swagger;
using Microsoft.Mashup.Engine1.Library.Sybase;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Library.Teradata;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Library.Variable;
using Microsoft.Mashup.Engine1.Library.Web;
using Microsoft.Mashup.Engine1.Library.WebBrowserContents;
using Microsoft.Mashup.Engine1.Library.Xml;
using Microsoft.Mashup.Engine1.Library._Module;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.Salesforce;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x02000250 RID: 592
	public static class Modules
	{
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x00031F90 File Offset: 0x00030190
		public static Microsoft.Mashup.Engine1.Runtime.Module All
		{
			get
			{
				if (Modules.all == null)
				{
					object obj = Modules.lockObject;
					lock (obj)
					{
						if (Modules.all == null)
						{
							Modules.all = Linker.Link((from module in Modules.CoreModules.Concat(Modules.LegacyModules)
								select Modules.DisableModule(module) into module
								where module != null
								select module).ToList<Microsoft.Mashup.Engine1.Runtime.Module>(), delegate(IError entry)
							{
								throw new InvalidOperationException();
							}, LinkOptions.None);
							Modules.all = new Modules.RenamedModule(Modules.all, "Legacy");
						}
					}
				}
				return Modules.all;
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x0600195B RID: 6491 RVA: 0x0003207C File Offset: 0x0003027C
		private static Microsoft.Mashup.Engine1.Runtime.Module CoreModule
		{
			get
			{
				if (Modules.coreModule == null)
				{
					Modules.coreModule = Linker.Link((from module in Modules.CoreModules
						select Modules.DisableModule(module) into module
						where module != null
						select module).ToList<Microsoft.Mashup.Engine1.Runtime.Module>(), delegate(IError entry)
					{
						throw new InvalidOperationException();
					}, LinkOptions.None);
					Modules.coreModule = new Modules.RenamedModule(Modules.coreModule, "Core");
				}
				return Modules.coreModule;
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00032128 File Offset: 0x00030328
		private static Microsoft.Mashup.Engine1.Runtime.Module[] CoreModules
		{
			get
			{
				if (Modules.coreModules == null)
				{
					Modules.coreModules = Modules.WrapModules(Modules.GetCoreModules());
				}
				return Modules.coreModules;
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00032145 File Offset: 0x00030345
		private static Microsoft.Mashup.Engine1.Runtime.Module[] LegacyModules
		{
			get
			{
				if (Modules.legacyModules == null)
				{
					Modules.legacyModules = Modules.WrapModules(Modules.GetLegacyModules());
				}
				return Modules.legacyModules;
			}
		}

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00032162 File Offset: 0x00030362
		private static Microsoft.Mashup.Engine1.Runtime.Module[] AdditionalModules
		{
			get
			{
				if (Modules.additionalModules == null)
				{
					Modules.additionalModules = Modules.WrapModules(Modules.GetAdditionalModules());
				}
				return Modules.additionalModules;
			}
		}

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00032180 File Offset: 0x00030380
		private static Dictionary<string, Microsoft.Mashup.Engine1.Runtime.Module> KnownModules
		{
			get
			{
				if (Modules.knownModules == null)
				{
					Modules.knownModules = new Dictionary<string, Microsoft.Mashup.Engine1.Runtime.Module>(Modules.LegacyModules.Length + Modules.AdditionalModules.Length + 1);
					Modules.knownModules.Add("Core", Modules.CoreModule);
					foreach (Microsoft.Mashup.Engine1.Runtime.Module module in Modules.LegacyModules)
					{
						Modules.knownModules.Add(module.Name, module);
					}
					foreach (Microsoft.Mashup.Engine1.Runtime.Module module2 in Modules.AdditionalModules)
					{
						Modules.knownModules.Add(module2.Name, module2);
					}
				}
				return Modules.knownModules;
			}
		}

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x00032220 File Offset: 0x00030420
		private static IDictionary<string, Microsoft.Mashup.Engine1.Runtime.Module> CurrentModules
		{
			get
			{
				if (Modules.currentModules == null)
				{
					Modules.currentModules = new Dictionary<string, Microsoft.Mashup.Engine1.Runtime.Module>(Modules.KnownModules);
					foreach (string text in Modules.removedModules)
					{
						Modules.currentModules.Remove(text);
					}
					foreach (string text2 in Modules.disabledModules)
					{
						Modules.currentModules[text2] = Modules.DisableModule(Modules.currentModules[text2]);
					}
				}
				return Modules.currentModules;
			}
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x000322E0 File Offset: 0x000304E0
		public static Microsoft.Mashup.Engine1.Runtime.Module Action
		{
			get
			{
				return Modules.actionModule;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x000322E7 File Offset: 0x000304E7
		public static Microsoft.Mashup.Engine1.Runtime.Module Extensibility
		{
			get
			{
				return Modules.extensibilityModule;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x000322EE File Offset: 0x000304EE
		public static ICollection<string> DisabledModules
		{
			get
			{
				return Modules.disabledModules;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x000322F5 File Offset: 0x000304F5
		public static ICollection<string> RemovedModules
		{
			get
			{
				return Modules.removedModules;
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000322FC File Offset: 0x000304FC
		private static Microsoft.Mashup.Engine1.Runtime.Module WrapModule(Microsoft.Mashup.Engine1.Runtime.Module module, ResourceManager resources = null)
		{
			return new SerializationApplyingModule(new DocumentationApplyingModule(module, resources));
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0003230A File Offset: 0x0003050A
		private static Microsoft.Mashup.Engine1.Runtime.Module DisableModule(Microsoft.Mashup.Engine1.Runtime.Module module)
		{
			if (module.Name == null)
			{
				return module;
			}
			if (Modules.removedModules.ContainsUnsafe(module.Name))
			{
				return null;
			}
			if (Modules.disabledModules.ContainsUnsafe(module.Name))
			{
				return new DisabledModule(module);
			}
			return module;
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00032344 File Offset: 0x00030544
		private static Microsoft.Mashup.Engine1.Runtime.Module[] WrapModules(Microsoft.Mashup.Engine1.Runtime.Module[] module)
		{
			Microsoft.Mashup.Engine1.Runtime.Module[] array = new Microsoft.Mashup.Engine1.Runtime.Module[module.Length];
			for (int i = 0; i < module.Length; i++)
			{
				array[i] = Modules.WrapModule(module[i], null);
			}
			return array;
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x00032378 File Offset: 0x00030578
		private static Microsoft.Mashup.Engine1.Runtime.Module[] GetCoreModules()
		{
			return new Microsoft.Mashup.Engine1.Runtime.Module[]
			{
				Modules.Compile("// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n    \r\nsection List;\r\n\r\nshared List.NonNullCount = (list as list) as number => List.Count(List.RemoveNulls(list));\r\n\r\nshared List.MatchesAll = (list as list, condition as function) as logical => not List.MatchesAny(list, (item) => not condition(item));\r\n\r\nshared List.MatchesAny = (list as list, condition as function) as logical => not List.IsEmpty(List.Select(list, condition));\r\n\r\nshared List.Range = (list as list, offset as number, optional count as number) as list =>\r\nlet\r\n    skippedInput = List.Skip(list, offset)\r\nin\r\n    if (count = null) then skippedInput\r\n    else List.FirstN(skippedInput, count);\r\n\r\nshared List.RemoveItems = (list1 as list, list2 as list) as list =>\r\n    List.Select(list1, (x) => not List.Contains(list2, x));\r\n\r\nshared List.ReplaceValue = (\r\n    list as list,\r\n    oldValue,\r\n    newValue,\r\n    replacer as function\r\n) as list =>\r\n    let\r\n        f = (val) => try replacer(val, oldValue, newValue) otherwise val\r\n    in\r\n        List.Transform(list, f);\r\n\r\nshared List.FindText = (list as list, text as text) as list =>\r\n    List.Select(list, each ContainsTextWithin(_, {text}));\r\n\r\nshared List.RemoveLastN = (list as list, optional countOrCondition) as list => \r\n    List.Reverse(List.Skip(List.Reverse(list), countOrCondition));\r\n\r\nshared List.RemoveFirstN = List.Skip;\r\n\r\nContainsTextWithin = (value as any, strings as list) as logical =>\r\n    if (value is text) then\r\n    (\r\n        not List.IsEmpty(List.Select(strings, each Text.Contains(value, _)))\r\n    )\r\n    else if (value is list) then\r\n    (\r\n        not List.IsEmpty(List.Select(value, each @ContainsTextWithin(_, strings)))\r\n    )\r\n    else if (value is record) then\r\n    (\r\n        not List.IsEmpty(List.Select(Table.ToRecords(Record.ToTable(value)), each @ContainsTextWithin([Value], strings)))\r\n    )\r\n    else\r\n    (\r\n        false\r\n    );\r\n\r\n    "),
				Linker.Link(new Microsoft.Mashup.Engine1.Runtime.Module[]
				{
					Modules.Compile("// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n\r\nsection Binary;\r\n\r\nHandlers.FromBinary = (binary as nullable binary) as record =>\r\n[\r\n    GetExpression = () => TableModule!Value.Expression(binary),\r\n    GetLength = () => Binary.Length(binary),\r\n    GetStream = () => binary,\r\n\r\n    OnRange = (offset, count) => Binary.Range(binary, offset, count),\r\n    OnEnd = () => Binary.End(binary),\r\n    OnInvoke = (function, arguments, index) => Function.Invoke(function, List.ReplaceRange(arguments, index, 1, {binary})),\r\n\r\n    OnReplace = (value) => ValueAction.Replace(binary, value)\r\n];\r\n\r\nshared Binary.View = (\r\n    binary as nullable binary,\r\n    handlers as record\r\n) as binary =>\r\n    let\r\n        defaultHandlers = if (binary <> null) then Handlers.FromBinary(binary) else [],\r\n\r\n        // NOTE: Do not automatically forward Value.Expression to the binary as it breaks encapsulation\r\n        defaultHandlersWithoutExpression =\r\n            if (defaultHandlers[GetExpression]? <> null) then defaultHandlers & [GetExpression = () => null]\r\n            else defaultHandlers,\r\n\r\n        viewHandlers = defaultHandlersWithoutExpression & handlers,\r\n        view = LibraryModule!Binary.FromHandlers(viewHandlers)\r\n    in\r\n        view;\r\n\r\nshared Binary.ViewFunction = Value.ViewFunction;\r\n\r\nshared Binary.ViewError = Value.ViewError;\r\n\r\n    "),
					Modules.actionModule
				}, new Action<IError>(Modules.ThrowOnCompileError), LinkOptions.ExportFirstModule | LinkOptions.IgnoreUnresolvedImports),
				Linker.Link(new Microsoft.Mashup.Engine1.Runtime.Module[]
				{
					Modules.Compile("// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n\r\nsection Table;\r\n\r\n//\r\n// Parameter helpers\r\n//\r\n\r\nColumnsSelector.CreateColumnsList = (value) =>\r\n    if (value is text) then {value}\r\n    else value;\r\n\r\nColumnsSelector.CreateListKeySelector = (value) =>\r\n    if (value is text) then each Record.Field(_, value)\r\n    else each Record.SelectFields(_, value);\r\n\r\nTableColumnTransformOperations.CreateTransformOperationsList = (value) =>\r\n    if (value is list and List.Count(value) = 2 and value{0} is text and value{1} is function) then {value}\r\n    else value;\r\n\r\nTableEquationCriteria.CreateListEquationCriteria = (value) =>\r\n    if (value is function) then value\r\n    else if (TableEquationCriterion.IsTableEquationCriterion(value)) then TableEquationCriterion.CreateListEquationCriterion(value)\r\n    else if (value is list and not List.IsEmpty(value) and value{0} is list) then List.Transform(value, TableEquationCriterion.CreateListEquationCriterion)\r\n    else ColumnsSelector.CreateListKeySelector(value);\r\n\r\nTableEquationCriterion.CreateListEquationCriterion = (value) =>\r\n    {ColumnsSelector.CreateListKeySelector(value{0}), value{1}};\r\n\r\nTableEquationCriterion.IsTableEquationCriterion = (value) =>\r\n    value is list and List.Count(value) = 2\r\n        and value{0} is text\r\n        and value{1} is function;\r\n\r\nTableEquationCriterion.Default = (row) =>\r\n    each try Record.SelectFields(_, Record.FieldNames(row)) otherwise _;\r\n\r\nTableRowReplacementOperation.CreateListReplacementOperation = (value) =>\r\n    value;\r\n\r\nTableRowReplacementOperation.IsTableRowReplacementOperation = (value) =>\r\n    value is list and List.Count(value) = 2\r\n        and value{0} is record\r\n        and value{1} is record;\r\n\r\nTableRowReplacementOperations.CreateListReplacementOperations = (value) =>\r\n    if (TableRowReplacementOperation.IsTableRowReplacementOperation(value)) then {TableRowReplacementOperation.CreateListReplacementOperation(value)}\r\n    else List.Transform(value, each TableRowReplacementOperation.CreateListReplacementOperation(_));\r\n\r\nRank.NoFunction = (x) =>\r\n    if x is function then error [Reason = \"Expression.Error\", Message = LibraryModule!UICulture.GetString(\"ComparisonCriteria_NoFunctions\"), Detail = x]\r\n    else x;\r\n\r\nRank.MakeComparisonItem = (item) =>\r\n    if item is text then {item, Order.Ascending}\r\n    else if item is list then\r\n        if List.Count(item) = 1 then {item{0}, Order.Ascending}\r\n        else if List.Count(item) = 2 then item\r\n        else Rank.NoFunction(item)\r\n    else Rank.NoFunction(item);\r\n\r\nRank.MakeComparisonCriteria = (c) =>\r\n    if c is text then {{c, Order.Ascending}}\r\n    else if c is list and c{1}? is number then {Rank.MakeComparisonItem(c)}\r\n    else if c is list then List.Transform(c, Rank.MakeComparisonItem)\r\n    else c;\r\n\r\nRank.ValidateComparison = (c) =>\r\n    if c is list and List.Count(c) > 0 and\r\n            List.AllTrue(List.Transform(c, (e) => e is list and List.Count(e) = 2 and e{0} is text and (e{1} = Order.Ascending or e{1} = Order.Descending)))\r\n        then c\r\n        else error [Reason = \"Expression.Error\", Message = LibraryModule!UICulture.GetString(\"TableSortInvalidSortCriteria\"), Detail = c];\r\n\r\n//\r\n// Information\r\n//\r\n\r\nshared Table.ColumnCount = (table as table) as number =>\r\n    List.Count(Table.ColumnNames(table));\r\n\r\n//\r\n// Row selection and access\r\n//\r\n\r\nshared Table.AlternateRows = TableModule!Function.PreserveTableLineage(\r\n    (table as table, offset as number, skip as number, take as number) as table =>\r\n        Table.FromRecords(List.Alternate(Table.ToRecords(table), skip, take, offset), Value.Type(table)));\r\n\r\nshared Table.InsertRows = (table as table, offset as number, rows as list) as table =>\r\n    Table.ReplaceRows(table, offset, 0, rows);\r\n\r\nshared Table.LastN = TableModule!Function.PreserveTableLineage(\r\n    (table as table, countOrCondition) as table =>\r\n        Table.FromRecords(List.LastN(Table.ToRecords(table), countOrCondition), Value.Type(table)));\r\n\r\nshared Table.Last = (table as table, optional default) =>\r\n    List.Last(Table.ToRecords(table), default);\r\n\r\nshared Table.MatchesAllRows = (table as table, condition as function) as logical =>\r\n    List.MatchesAll(Table.ToRecords(table), condition);\r\n\r\nshared Table.MatchesAnyRows = (table as table, condition as function) as logical =>\r\n    List.MatchesAny(Table.ToRecords(table), condition);\r\n\r\nshared Table.Partition = (table as table, column as text, groups as number, hash as function) as list => \r\n    List.Generate(\r\n        () => 0,\r\n        (i) => i < groups,\r\n        (i) => i + 1, \r\n        (i) => Table.SelectRows(table, (row) => Number.Mod(hash(Record.Field(row, column)), groups) = i));\r\n\r\nshared Table.Range = (table as table, offset as number, optional count as nullable number) as table =>\r\nlet\r\n    skippedInput = Table.Skip(table, offset)\r\nin\r\n    if (count = null) then skippedInput\r\n    else Table.FirstN(skippedInput, count);\r\n\r\nshared Table.RemoveRows = (table as table, offset as number, optional count as nullable number) as table =>\r\n    Table.ReplaceRows(table, offset, if (count = null) then 1 else count, {});\r\n\r\nshared Table.Repeat = (table as table, count as number) as table =>\r\n    Table.Combine(List.Repeat({table}, count));\r\n\r\nshared Table.ReplaceRows = TableModule!Function.PreserveTableLineage(\r\n    (table as table, offset as number, count as number, rows as list) as table =>\r\n        Table.FromRecords(List.ReplaceRange(Table.ToRecords(table), offset, count, rows), Type.ReplaceTableKeys(Value.Type(table), {})));\r\n\r\nshared Table.ReverseRows = TableModule!Function.PreserveTableLineage(\r\n    (table as table) as table =>\r\n        Table.FromRecords(List.Reverse(Table.ToRecords(table)), Value.Type(table)));\r\n\r\n//\r\n// Column selection and access\r\n//\r\n\r\nshared Table.HasColumns = (table as table, columns) as logical =>\r\n    List.ContainsAll(Table.ColumnNames(table), ColumnsSelector.CreateColumnsList(columns));\r\n\r\nshared Table.PrefixColumns = (table as table, prefix as text) as table =>\r\nlet\r\n    names = Table.ColumnNames(table),\r\n    renames = List.Transform(names, each {_, prefix & \".\" & _})\r\nin\r\n    Table.RenameColumns(table, renames);\r\n\r\nshared Table.ColumnsOfType = (table as table, listOfTypes as list) as list =>\r\nlet \r\n    columnsInfo = Record.ToTable(Type.RecordFields(Type.TableRow(Value.Type(table))))\r\nin\r\n    Table.SelectRows(columnsInfo, each\r\n        List.MatchesAny(listOfTypes,\r\n            (typeToCompare) => Type.Is([Value][Type], typeToCompare)))[Name];\r\n\r\n//\r\n// Transformation\r\n//\r\n\r\nshared Table.AddColumn =\r\n    (\r\n        table as table,\r\n        newColumnName as text,\r\n        columnGenerator as function,\r\n        optional columnType as nullable type\r\n    ) as table =>\r\n    let\r\n        newColumnType = if (columnType <> null) then columnType else Type.FunctionReturn(Value.Type(columnGenerator))\r\n    in\r\n        TableModule!Table.AddColumns(table, {newColumnName}, each {columnGenerator(_)}, {newColumnType});\r\n\r\nshared Table.DuplicateColumn = (\r\n    table as table,\r\n    columnName as text,\r\n    newColumnName as text,\r\n    optional columnType as nullable type\r\n) as table =>\r\nlet\r\n    newColumnType = if (columnType <> null) then columnType else Type.TableColumn(Value.Type(table), columnName)\r\nin\r\n    Table.AddColumn(table, newColumnName, each Record.Field(_, columnName), newColumnType);\r\n\r\nshared Table.FillUp = (table as table, columns as list) as table =>\r\n    Table.ReverseRows(Table.FillDown(Table.ReverseRows(table), columns));\r\n\r\nshared Table.RemoveLastN = (table as table, optional countOrCondition) as table => \r\n    Table.ReverseRows(Table.Skip(Table.ReverseRows(table), countOrCondition));\r\n\r\nshared Table.RemoveFirstN = Table.Skip;\r\n\r\nshared Table.ExpandListColumn = (table as table, column as text) as table =>\r\n        TableModule!Table.ExpandListColumn(table, column, /* singleOrDefault: */ false);\r\n\r\nshared Table.ExpandTableColumn = (\r\n    table as table,\r\n    column as text,\r\n    columnNames as list,\r\n    optional newColumnNames as nullable list\r\n) as table =>\r\nlet\r\n    newColumnNamesToUse =\r\n        if (newColumnNames <> null and List.Count(newColumnNames) <> List.Count(columnNames)) then\r\n            error [Reason = \"Expression.Error\", Message = LibraryModule!UICulture.GetString(\"TableExpandTableColumn_ColumnAndNewColumnNamesMustHaveSameCount\")]\r\n        else newColumnNames\r\nin\r\n    Table.ExpandRecordColumn(Table.ExpandListColumn(table, column), column, columnNames, newColumnNamesToUse);\r\n\r\nTable.UniqueName = (\r\n    table as table\r\n) =>\r\n    \"_\" & Text.Combine(Table.ColumnNames(table)); // TODO: More efficient unique name\r\n\r\nshared Table.TransformRows = (table as table, transform as function) as list =>\r\n    List.Transform(Table.ToRecords(table), transform);\r\n\r\nshared Table.Transpose = TableModule!Function.PreserveTableLineage(\r\n    (table as table, optional columns) as table =>\r\n        Table.FromColumns(Table.ToRows(table), columns));\r\n\r\nshared Table.DemoteHeaders = TableModule!Function.PreserveTableLineage(\r\n    (table as table) as table => \r\n        let\r\n            Result = Table.FromRows(List.Combine({{Table.ColumnNames(table)}, Table.ToRows(table)})),\r\n            WithInferType = Result meta [ShouldInferTableType = null]\r\n        in\r\n            WithInferType);\r\n\r\nshared Table.ToRows = (table as table) as list =>\r\n    List.Transform(Table.ToRecords(table), (record) => Record.FieldValues(record));\r\n\r\nshared Table.ToColumns = (table as table) as list =>\r\n    List.Transform(Table.ColumnNames(table), (column) => Table.Column(table, column));\r\n\r\nValidateOptions = (\r\n    options as nullable record,\r\n    functionName as text,\r\n    validOptions as list\r\n) as record =>\r\n    let\r\n        optionsRecord = options ?? [],\r\n        badOption = List.First(Record.FieldNames(Record.RemoveFields(optionsRecord, validOptions, MissingField.Ignore))),\r\n        validatedOptions = if badOption = null then optionsRecord else\r\n            error [Reason = \"Expression.Error\", Message = LibraryModule!UICulture.GetString(\"InvalidOption\", {badOption, functionName, Text.Combine(List.Sort(validOptions), \", \")})]\r\n    in\r\n        validatedOptions;\r\n\r\nshared Table.CombineColumnsToRecord = (\r\n        table as table,\r\n        newColumnName as text,\r\n        sourceColumns as list,\r\n        optional options as record\r\n    ) as table =>\r\n        let\r\n            validatedOptions = ValidateOptions(options, \"Table.CombineColumnsToRecord\", {\"DisplayNameColumn\", \"TypeName\"}),\r\n            uniqueName = Table.UniqueName(table),\r\n            tableType = Value.Type(table),\r\n            displayNameColumn = validatedOptions[DisplayNameColumn]?,\r\n            dni = Expression.Identifier(displayNameColumn),\r\n            getDisplayName = (v) => let dn = Record.FieldOrDefault(Value.Metadata(v), \"Documentation.DisplayName\", v) in\r\n                if dn is text or dn is null or dn is binary or dn is action or dn is function or dn is list or dn is table or dn is record or dn is type then dn else try Text.From(dn) otherwise dn,\r\n            valueMeta = if displayNameColumn = null then \"\"\r\n                else \" meta [Documentation.DisplayName = getDisplayName(row[\" & dni & \"])]\",\r\n            typeName = validatedOptions[TypeName]?,\r\n            typeMeta = if typeName = null then [] else [Documentation.TypeName = typeName],\r\n            addTypeName = if typeName = null then (x) => x\r\n                else (x) => \"let v = \" & x & \" in Value.ReplaceType(v, Value.Type(v) meta [Documentation.TypeName = \" & Expression.Constant(typeName) & \"])\"\r\n        in\r\n            Table.RenameColumns(\r\n                Table.RemoveColumns(\r\n                    Table.ReorderColumns(\r\n                        Table.AddColumn(\r\n                            table,\r\n                            uniqueName,\r\n                            Expression.Evaluate(\r\n                                \"(row) => \" & addTypeName(\"[\" & Text.Combine(\r\n                                    List.Transform(\r\n                                        sourceColumns,\r\n                                        (c) => Expression.Identifier(c) & \" = row[\" & Expression.Identifier(c) & \"]\"),\r\n                                    \",\") & \"] \" & valueMeta),\r\n                                [Value.Type = Value.Type, Value.ReplaceType = Value.ReplaceType, getDisplayName = getDisplayName]),\r\n                            Type.ForRecord(\r\n                                List.Accumulate(\r\n                                    sourceColumns,\r\n                                    [],\r\n                                    (r, c) => Record.AddField(\r\n                                        r,\r\n                                        c,\r\n                                        [Type = Type.TableColumn(tableType, c), Optional = false])),\r\n                                false)\r\n                                meta typeMeta),\r\n                        {uniqueName, List.Last(sourceColumns) ?? List.Last(Table.ColumnNames(table))}),\r\n                    if displayNameColumn = null then sourceColumns else List.Distinct(sourceColumns & {displayNameColumn})),\r\n                {uniqueName, newColumnName});\r\n\r\n//\r\n// Membership\r\n//\r\n\r\nGetListEquationCriteria = (equationCriteria) => \r\n    if (equationCriteria <> null) then TableEquationCriteria.CreateListEquationCriteria(equationCriteria)\r\n    else null;\r\n\r\nGetTableContainsEquationCriteria = (row, equationCriteria) => \r\n    if (equationCriteria <> null) then TableEquationCriteria.CreateListEquationCriteria(equationCriteria)\r\n    else TableEquationCriterion.Default(row);\r\n\r\nshared Table.Contains = (\r\n    table as table,\r\n    row as record,\r\n    optional equationCriteria\r\n) as logical =>\r\n    List.Contains(Table.ToRecords(table), row, GetTableContainsEquationCriteria(row, equationCriteria));\r\n\r\nshared Table.ContainsAll = (\r\n    table as table,\r\n    rows as list,\r\n    optional equationCriteria\r\n) as logical =>\r\n    List.AllTrue(List.Transform(rows, each Table.Contains(table, _, equationCriteria)));\r\n\r\nshared Table.ContainsAny = (\r\n    table as table,\r\n    rows as list,\r\n    optional equationCriteria\r\n) as logical =>\r\n    List.AnyTrue(List.Transform(rows, each Table.Contains(table, _, equationCriteria)));\r\n\r\nshared Table.IsDistinct = (table as table, optional comparisonCriteria) as logical =>\r\n    List.IsDistinct(Table.ToRecords(table), GetListEquationCriteria(comparisonCriteria));\r\n\r\nshared Table.PositionOf = (\r\n    table as table,\r\n    row as record,\r\n    optional occurrence,\r\n    optional equationCriteria\r\n) =>\r\n    List.PositionOf(Table.ToRecords(table), row, occurrence, GetListEquationCriteria(equationCriteria));\r\n\r\nshared Table.PositionOfAny = (\r\n    table as table,\r\n    rows as list,\r\n    optional occurrence as number,\r\n    optional equationCriteria\r\n) =>\r\n    List.PositionOfAny(Table.ToRecords(table), rows, occurrence, GetListEquationCriteria(equationCriteria));\r\n\r\nshared Table.RemoveMatchingRows = TableModule!Function.PreserveTableLineage(\r\n    (\r\n        table as table,\r\n        rows as list,\r\n        optional equationCriteria\r\n    ) as table =>\r\n        Table.FromRecords(List.RemoveMatchingItems(Table.ToRecords(table), rows, GetListEquationCriteria(equationCriteria)), Value.Type(table)));\r\n\r\nshared Table.ReplaceMatchingRows = TableModule!Function.PreserveTableLineage(\r\n    (\r\n        table as table,\r\n        replacements as list,\r\n        optional equationCriteria\r\n    ) as table =>\r\n        Table.FromRecords(List.ReplaceMatchingItems(\r\n            Table.ToRecords(table),\r\n            TableRowReplacementOperations.CreateListReplacementOperations(replacements),\r\n            GetListEquationCriteria(equationCriteria)), Value.Type(table)));\r\n\r\n//\r\n// Comparison\r\n//\r\n\r\nshared Table.Max = (\r\n    table as table,\r\n    comparisonCriteria,\r\n    optional default\r\n) =>\r\n    Table.First(TableModule!Table.SortDescending(table, comparisonCriteria), default);\r\n\r\nshared Table.MaxN = (\r\n    table as table,\r\n    comparisonCriteria,\r\n    countOrCondition\r\n) as table =>\r\n    Table.FirstN(TableModule!Table.SortDescending(table, comparisonCriteria), countOrCondition);\r\n\r\nshared Table.Min = (\r\n    table as table,\r\n    comparisonCriteria,\r\n    optional default\r\n) =>\r\n    Table.First(Table.Sort(table, comparisonCriteria), default);\r\n\r\nshared Table.MinN = (\r\n    table as table,\r\n    comparisonCriteria,\r\n    countOrCondition\r\n) as table =>\r\n    Table.FirstN(Table.Sort(table, comparisonCriteria), countOrCondition);\r\n\r\n//\r\n// Other\r\n//\r\n\r\nshared Table.FindText = TableModule!Function.PreserveTableLineage(\r\n    (table as table, text as text) as table =>\r\n        Table.FromRecords(List.FindText(Table.ToRecords(table), text), Value.Type(table)));\r\n\r\nshared Replacer.ReplaceValue = (value, old, new) =>\r\n    if (value = old) then new\r\n    else value;\r\n\r\nshared Replacer.ReplaceText =\r\n    Text.Replace;\r\n\r\nshared Table.ReplaceValue =\r\n    (\r\n        table as table,\r\n        oldValue,\r\n        newValue,\r\n        replacer as function,\r\n        columnsToSearch as list\r\n    ) as table =>\r\n        if (oldValue is function or newValue is function) then\r\n            Table.ReplaceValueSlow(table, oldValue, newValue, replacer, columnsToSearch, replacer = Replacer.ReplaceValue)\r\n        else\r\n            if replacer = Replacer.ReplaceValue then\r\n                Table.TransformColumns(table,\r\n                    List.Transform(columnsToSearch, (column) => { column,\r\n                        Value.ReplaceType((value) => replacer(value, oldValue, newValue),\r\n                            Type.ForFunction([ReturnType = Type.Union({Value.Type(newValue),\r\n                                Record.FieldOrDefault(Type.RecordFields(Type.TableRow(Value.Type(table))), column, [Type=type any])[Type]}),\r\n                                Parameters = [value = type any]], 1))}))\r\n            else\r\n                Table.TransformColumns(table,\r\n                    List.Transform(columnsToSearch, (column) => { column,\r\n                        Value.ReplaceType((value) => replacer(value, oldValue, newValue),\r\n                            Type.ForFunction([ReturnType = Type.FunctionReturn(Value.Type(replacer)), Parameters = [value = type any]], 1))}));\r\n\r\nTable.ReplaceValueSlow = (\r\n    table as table,\r\n    oldValue,\r\n    newValue,\r\n    replacer as function,\r\n    columnsToSearch as list,\r\n    usingReplacerReplaceValueFunction as logical // TODO (Bug 11397502): Remove need for parameter\r\n) as table =>\r\n    let\r\n        newType =\r\n            if not usingReplacerReplaceValueFunction then\r\n                Type.FunctionReturn(Value.Type(replacer))\r\n            else if newValue is function then\r\n                Type.FunctionReturn(Value.Type(newValue))\r\n            else\r\n                Value.Type(newValue),\r\n        getValueAsFunction = (searchValue) =>\r\n            if searchValue is function then\r\n                (row, value) => searchValue(row)\r\n            else\r\n                (row, value) => searchValue,\r\n        oldValueFn = getValueAsFunction(oldValue),\r\n        newValueFn = getValueAsFunction(newValue),\r\n        tableType = Value.Type(table),\r\n        fieldReplacer = (row, value) =>\r\n            try replacer(value, oldValueFn(row, value), newValueFn(row, value)) otherwise value,\r\n        pairs = List.Transform(columnsToSearch,\r\n            (columnName) as list =>\r\n            let\r\n                currentType = Type.TableColumn(tableType, columnName)\r\n            in\r\n                {columnName,\r\n                 Value.ReplaceType(fieldReplacer,\r\n                    Type.ForFunction([ReturnType = Type.Union({currentType, newType}),\r\n                                      Parameters = [row = type record, value = type any]], 2))})\r\n    in\r\n        // If any of the columns don't exist, then force a top-level error. (The else clause will never be executed.)\r\n        // Construct an empty table with the same type to avoid triggering folding.\r\n        if Table.SelectColumns(#table(Value.Type(table), {}), columnsToSearch, MissingField.Erro[...string is too long...]"),
					Modules.actionModule,
					Modules.dataSourceModule,
					Modules.deltaModule
				}, new Action<IError>(Modules.ThrowOnCompileError), LinkOptions.ExportFirstModule | LinkOptions.IgnoreUnresolvedImports),
				Modules.Compile("// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n    \r\nsection Text;\r\n\r\nshared RelativePosition.Type = Value.RemoveMetadata(Number.Type) meta [\r\n    Documentation.Name = \"RelativePosition.Type\",\r\n    Documentation.Description = LibraryModule!UICulture.GetString(\"RelativePositionTypeDescription\"),\r\n    Documentation.AllowedValues = { RelativePosition.FromStart, RelativePosition.FromEnd }\r\n];\r\nshared RelativePosition.FromStart = 0 meta [Documentation.Name = \"RelativePosition.FromStart\"];\r\nshared RelativePosition.FromEnd = 1 meta [Documentation.Name = \"RelativePosition.FromEnd\"];\r\n\r\nshared Text.AfterDelimiter = (text as nullable text, delimiter as text, optional index as any) =>\r\n    let\r\n        IndexAsList = GetDelimiterIndexAsList(index),\r\n        Index = IndexAsList{0},\r\n        IndexFrom = IndexAsList{1},\r\n        Parts = Splitter.SplitTextByDelimiter(delimiter, QuoteStyle.None)(text),\r\n        PartsToSkip = if IndexFrom = RelativePosition.FromEnd then List.Max({List.Count(Parts) - (Index + 1), 0}) else Index + 1,\r\n        RelevantParts = List.Skip(Parts, PartsToSkip),\r\n        Combined = Combiner.CombineTextByDelimiter(delimiter, QuoteStyle.None)(RelevantParts)\r\n    in\r\n        Combined;\r\n\r\nshared Text.BeforeDelimiter = (text as nullable text, delimiter as text, optional index as any) =>\r\n    let\r\n        IndexAsList = GetDelimiterIndexAsList(index),\r\n        Index = IndexAsList{0},\r\n        IndexFrom = IndexAsList{1},\r\n        Parts = Splitter.SplitTextByDelimiter(delimiter, QuoteStyle.None)(text),\r\n        PartsToTake = if IndexFrom = RelativePosition.FromEnd then List.Max({List.Count(Parts) - (Index + 1), 0}) else Index + 1,\r\n        RelevantParts = List.FirstN(Parts, PartsToTake),\r\n        Combined = Combiner.CombineTextByDelimiter(delimiter, QuoteStyle.None)(RelevantParts)\r\n    in\r\n        Combined;\r\n\r\nshared Text.BetweenDelimiters = (text as nullable text, startDelimiter as text, endDelimiter as text, optional startIndex as any, optional endIndex as any) =>\r\n    let\r\n        EndIndexFrom = GetDelimiterIndexAsList(endIndex){1},\r\n        Result = if EndIndexFrom = RelativePosition.FromStart then \r\n            Text.BeforeDelimiter(Text.AfterDelimiter(text, startDelimiter, startIndex), endDelimiter, endIndex)\r\n        else\r\n            Text.AfterDelimiter(Text.BeforeDelimiter(text, startDelimiter, startIndex), endDelimiter, endIndex)\r\n    in\r\n        Result;\r\n\r\nGetDelimiterIndexAsList = (index as any) as list =>\r\n    if index = null then\r\n        {0, RelativePosition.FromStart}\r\n    else if index is number then\r\n        {index, RelativePosition.FromStart}\r\n    else if index is list and List.Count(index) = 2 and index{0} is number and index{1} = RelativePosition.FromStart or index{1} = RelativePosition.FromEnd then\r\n        index\r\n    else\r\n        error Error.Record(\"Expression.Error\", LibraryModule!UICulture.GetString(\"InvalidDelimiterIndexParameter\"), index);\r\n\r\n    "),
				Modules.Compile("// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n    \r\nsection Date;\r\n\r\nshared Date.IsInPreviousDay = (dateTime) as nullable logical =>\r\n    Date.IsInPreviousNDays(dateTime, 1);\r\n\r\nshared Date.IsInPreviousNDays = (dateTime, days as number) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.AddDays(DateTime.FixedLocalNow(), -days)) and\r\n        Date.From(dateTime) < Date.From(DateTime.FixedLocalNow());\r\n        \r\nshared Date.IsInCurrentDay = (dateTime) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(DateTime.FixedLocalNow()) and\r\n        Date.From(dateTime) < Date.From(Date.AddDays(DateTime.FixedLocalNow(), 1));\r\n\r\nshared Date.IsInNextDay = (dateTime) as nullable logical =>\r\n    Date.IsInNextNDays(dateTime, 1);\r\n\r\nshared Date.IsInNextNDays = (dateTime, days as number) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.AddDays(DateTime.FixedLocalNow(), 1)) and \r\n        Date.From(dateTime) < Date.From(Date.AddDays(DateTime.FixedLocalNow(), days + 1));\r\n            \r\nshared Date.IsInPreviousWeek = (dateTime) as nullable logical =>\r\n    Date.IsInPreviousNWeeks(dateTime, 1);\r\n\r\nshared Date.IsInPreviousNWeeks = (dateTime, weeks as number) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.StartOfWeek(Date.AddWeeks(DateTime.FixedLocalNow(), -weeks))) and\r\n        Date.From(dateTime) < Date.From(Date.StartOfWeek(DateTime.FixedLocalNow()));\r\n\r\nshared Date.IsInCurrentWeek = (dateTime) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.StartOfWeek(DateTime.FixedLocalNow())) and\r\n        Date.From(dateTime) < Date.From(Date.AddDays(Date.EndOfWeek(DateTime.FixedLocalNow()), 1));\r\n\r\nshared Date.IsInNextWeek = (dateTime) as nullable logical =>\r\n    Date.IsInNextNWeeks(dateTime, 1);\r\n\r\nshared Date.IsInNextNWeeks = (dateTime, weeks as number) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.AddDays(Date.EndOfWeek(DateTime.FixedLocalNow()), 1)) and\r\n        Date.From(dateTime) < Date.From(Date.AddDays(Date.EndOfWeek(Date.AddWeeks(DateTime.FixedLocalNow(), weeks)), 1));\r\n\r\nshared Date.IsInPreviousMonth = (dateTime) as nullable logical =>\r\n    Date.IsInPreviousNMonths(dateTime, 1);\r\n\r\nshared Date.IsInPreviousNMonths = (dateTime, months as number) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.StartOfMonth(Date.AddMonths(DateTime.FixedLocalNow(), -months))) and\r\n        Date.From(dateTime) < Date.From(Date.StartOfMonth(DateTime.FixedLocalNow()));\r\n\r\nshared Date.IsInCurrentMonth = (dateTime) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.StartOfMonth(DateTime.FixedLocalNow())) and\r\n        Date.From(dateTime) < Date.From(Date.AddDays(Date.EndOfMonth(DateTime.FixedLocalNow()), 1));\r\n\r\nshared Date.IsInNextMonth = (dateTime) as nullable logical =>\r\n    Date.IsInNextNMonths(dateTime, 1);\r\n\r\nshared Date.IsInNextNMonths = (dateTime, months as number) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.AddDays(Date.EndOfMonth(DateTime.FixedLocalNow()), 1)) and\r\n        Date.From(dateTime) < Date.From(Date.AddDays(Date.EndOfMonth(Date.AddMonths(DateTime.FixedLocalNow(), months)), 1));\r\n\r\nshared Date.IsInPreviousQuarter = (dateTime) as nullable logical =>\r\n    Date.IsInPreviousNQuarters(dateTime, 1);\r\n\r\nshared Date.IsInPreviousNQuarters = (dateTime, quarters as number) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.StartOfQuarter(Date.AddQuarters(DateTime.FixedLocalNow(), -quarters))) and\r\n        Date.From(dateTime) < Date.From(Date.StartOfQuarter(DateTime.FixedLocalNow()));\r\n\r\nshared Date.IsInCurrentQuarter = (dateTime) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.StartOfQuarter(DateTime.FixedLocalNow())) and\r\n        Date.From(dateTime) < Date.From(Date.AddDays(Date.EndOfQuarter(DateTime.FixedLocalNow()), 1));\r\n\r\nshared Date.IsInNextQuarter = (dateTime) as nullable logical =>\r\n    Date.IsInNextNQuarters(dateTime, 1);\r\n\r\nshared Date.IsInNextNQuarters = (dateTime, quarters as number) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.AddDays(Date.EndOfQuarter(DateTime.FixedLocalNow()), 1)) and\r\n        Date.From(dateTime) < Date.From(Date.AddDays(Date.EndOfQuarter(Date.AddQuarters(DateTime.FixedLocalNow(), quarters)), 1));\r\n\r\nshared Date.IsInPreviousYear = (dateTime) as nullable logical =>\r\n    Date.IsInPreviousNYears(dateTime, 1);\r\n\r\nshared Date.IsInPreviousNYears = (dateTime, years as number) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.StartOfYear(Date.AddYears(DateTime.FixedLocalNow(), -years))) and\r\n        Date.From(dateTime) < Date.From(Date.StartOfYear(DateTime.FixedLocalNow()));\r\n\r\nshared Date.IsInCurrentYear = (dateTime) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.StartOfYear(DateTime.FixedLocalNow())) and\r\n        Date.From(dateTime) < Date.From(Date.AddDays(Date.EndOfYear(DateTime.FixedLocalNow()), 1));\r\n\r\nshared Date.IsInNextYear = (dateTime) as nullable logical =>\r\n    Date.IsInNextNYears(dateTime, 1);\r\n\r\nshared Date.IsInNextNYears = (dateTime, years as number) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.AddDays(Date.EndOfYear(DateTime.FixedLocalNow()), 1)) and\r\n        Date.From(dateTime) < Date.From(Date.AddDays(Date.EndOfYear(Date.AddYears(DateTime.FixedLocalNow(), years)), 1));\r\n\r\nshared Date.IsInYearToDate = (dateTime) as nullable logical =>\r\n    Date.From(dateTime) >= Date.From(Date.StartOfYear(DateTime.FixedLocalNow())) and\r\n        Date.From(dateTime) < Date.From(Date.AddDays(Date.EndOfDay(DateTime.FixedLocalNow()), 1));\r\n\r\nshared DateTime.IsInPreviousSecond = (dateTime) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfSecond() - #duration(0, 0, 0, 1) and\r\n        DateTime.From(dateTime) < StartOfSecond();\r\n\r\nshared DateTime.IsInPreviousNSeconds = (dateTime, seconds as number) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfSecond() - #duration(0, 0, 0, seconds) and\r\n        DateTime.From(dateTime) < StartOfSecond();\r\n\r\nshared DateTime.IsInNextSecond = (dateTime) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfSecond() + #duration(0, 0, 0, 1) and\r\n        DateTime.From(dateTime) < StartOfSecond() + #duration(0, 0, 0, 2);\r\n\r\nshared DateTime.IsInNextNSeconds = (dateTime, seconds as number) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfSecond() + #duration(0, 0, 0, 1) and\r\n        DateTime.From(dateTime) < StartOfSecond() + #duration(0, 0, 0, seconds + 1);\r\n\r\nshared DateTime.IsInCurrentSecond = (dateTime) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfSecond() and\r\n        DateTime.From(dateTime) < StartOfSecond() + #duration(0, 0, 0, 1);\r\n\r\nshared DateTime.IsInPreviousMinute = (dateTime) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfMinute() - #duration(0, 0, 1, 0) and\r\n        DateTime.From(dateTime) < StartOfMinute();\r\n\r\nshared DateTime.IsInPreviousNMinutes = (dateTime, minutes as number) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfMinute() - #duration(0, 0, minutes, 0) and\r\n        DateTime.From(dateTime) < StartOfMinute();\r\n\r\nshared DateTime.IsInNextMinute = (dateTime) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfMinute() + #duration(0, 0, 1, 0) and \r\n        DateTime.From(dateTime) < StartOfMinute() + #duration(0, 0, 2, 0);\r\n\r\nshared DateTime.IsInNextNMinutes = (dateTime, minutes as number) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfMinute() + #duration(0, 0, 1, 0) and \r\n        DateTime.From(dateTime) < StartOfMinute() + #duration(0, 0, minutes + 1, 0);\r\n\r\nshared DateTime.IsInCurrentMinute = (dateTime) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfMinute() and\r\n        DateTime.From(dateTime) < StartOfMinute() + #duration(0, 0, 1, 0);\r\n\r\nshared DateTime.IsInPreviousHour = (dateTime) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfHour() - #duration(0, 1, 0, 0) and\r\n        DateTime.From(dateTime) < StartOfHour();\r\n\r\nshared DateTime.IsInPreviousNHours = (dateTime, hours as number) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfHour() - #duration(0, hours, 0, 0) and\r\n        DateTime.From(dateTime) < StartOfHour();\r\n\r\nshared DateTime.IsInNextHour = (dateTime) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfHour() + #duration(0, 1, 0, 0) and \r\n        DateTime.From(dateTime) < StartOfHour() + #duration(0, 2, 0, 0);\r\n\r\nshared DateTime.IsInNextNHours = (dateTime, hours as number) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfHour() + #duration(0, 1, 0, 0) and \r\n        DateTime.From(dateTime) < StartOfHour() + #duration(0, hours + 1, 0, 0);\r\n\r\nshared DateTime.IsInCurrentHour = (dateTime) as nullable logical =>\r\n    DateTime.From(dateTime) >= StartOfHour() and\r\n        DateTime.From(dateTime) < StartOfHour() + #duration(0, 1, 0, 0);\r\n\r\nshared Date.MonthName = (date, optional culture as text) as nullable text => \r\n    DateTimeZone.ToText(DateTimeZone.From(date), \"MMMM\", culture);\r\n\r\nshared Date.DayOfWeekName = (date, optional culture as text) as nullable text => \r\n    DateTimeZone.ToText(DateTimeZone.From(date), \"dddd\", culture);\r\n\r\nStartOfHour = () as nullable datetime => DateTime.From(Time.StartOfHour(DateTime.FixedLocalNow()));\r\n\r\nStartOfMinute = () as nullable datetime => DateTime.From(Time.StartOfHour(DateTime.FixedLocalNow()) + #duration(0, 0, Time.Minute(DateTime.FixedLocalNow()), 0));\r\n\r\nStartOfSecond = () as nullable datetime => DateTime.From(Time.StartOfHour(DateTime.FixedLocalNow()) + #duration(0, 0, Time.Minute(DateTime.FixedLocalNow()), Number.RoundDown(Time.Second(DateTime.FixedLocalNow()))));\r\n    "),
				Modules.Compile("// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n\r\nsection SqlExpression;\r\n\r\nshared SqlExpression.SchemaFrom = (schema) =>\r\nlet\r\n    NullNativeExpressions = Table.TransformColumns(schema, {{\"NativeDefaultExpression\", each null}, {\"NativeExpression\", each null}}),\r\n    RemoveNativeType = Table.RemoveColumns(NullNativeExpressions, {\"NativeTypeName\"}),\r\n    AddNativeType = Table.AddColumn(RemoveNativeType, \"NativeTypeName\", each\r\n        if ([Kind] = \"text\") then\r\n            if ([IsVariableLength] = false) then \"nchar\"\r\n            else \"nvarchar\"\r\n        else if ([Kind] = \"number\") then\r\n            if ([TypeName] = \"Decimal.Type\") then \"decimal\"\r\n            else if ([TypeName] = \"Currency.Type\") then \"money\"\r\n            else if ([TypeName] = \"Int64.Type\") then \"bigint\"\r\n            else if ([TypeName] = \"Int32.Type\") then \"int\"\r\n            else if ([TypeName] = \"Int16.Type\") then \"smallint\"\r\n            else if ([TypeName] = \"Int8.Type\") then \"tinyint\"\r\n            else \"double\"\r\n        else if ([Kind] = \"date\") then \"date\"\r\n        else if ([Kind] = \"datetime\") then \"datetime2\"\r\n        else if ([Kind] = \"datetimezone\") then \"datetimeoffset\"\r\n        else if ([Kind] = \"time\") then \"time\"\r\n        else if ([Kind] = \"logical\") then \"bit\"\r\n        else if ([Kind] = \"binary\") then\r\n            if ([IsVariableLength] = false) then \"binary\"\r\n            else \"varbinary\"\r\n        else null),\r\n    Reorder = Table.ReorderColumns(AddNativeType, Table.ColumnNames(schema))\r\nin\r\n    Reorder;\r\n\r\n    "),
				new CapabilityModule(),
				new LanguageLibraryModule(),
				new Library.LibraryModule(),
				new ValueFirewallModule(),
				new AccessControlEntriesModule(),
				new BinaryFormatModule(),
				new CubeModule(),
				new CubeParametersModule(),
				new DiagnosticsModule(),
				new ExcelModule(),
				new GraphModule(),
				new JsonModule(),
				new LineageModule(),
				new LinesModule(),
				new ModuleModule(),
				new NormalizationModule(),
				new PackageModule(),
				new PageReaderConformanceModule(),
				new RDataModule(),
				new SqlExpressionModule(),
				new TableModule(),
				new UriModule(),
				new VariableModule(),
				new XmlModule()
			};
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00032508 File Offset: 0x00030708
		public static bool AddOptionalTestModule(Microsoft.Mashup.Engine1.Runtime.Module module)
		{
			object obj = Modules.lockObject;
			bool flag2;
			lock (obj)
			{
				if (Modules.CurrentModules.ContainsKey(module.Name))
				{
					flag2 = false;
				}
				else
				{
					Modules.CurrentModules[module.Name] = module;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0003256C File Offset: 0x0003076C
		public static Microsoft.Mashup.Engine1.Runtime.Module AllWithOptional()
		{
			object obj = Modules.lockObject;
			Microsoft.Mashup.Engine1.Runtime.Module module;
			lock (obj)
			{
				module = Linker.Link(Modules.CurrentModules.Values.ToList<Microsoft.Mashup.Engine1.Runtime.Module>(), delegate(IError entry)
				{
					throw new InvalidOperationException();
				}, LinkOptions.None);
			}
			return module;
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000325DC File Offset: 0x000307DC
		public static void InitializeBuiltinResources()
		{
			object obj = Modules.lockObject;
			List<Microsoft.Mashup.Engine1.Runtime.Module> list;
			lock (obj)
			{
				list = Modules.KnownModules.Values.ToList<Microsoft.Mashup.Engine1.Runtime.Module>();
			}
			using (ResourceKinds.Operations operations = ResourceKinds.OperationSet())
			{
				foreach (Microsoft.Mashup.Engine1.Runtime.Module module in list)
				{
					foreach (ResourceKindInfo resourceKindInfo in module.DataSources)
					{
						operations.AddResourceKind(resourceKindInfo, module.Name);
					}
				}
			}
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000326B0 File Offset: 0x000308B0
		public static RecordValue GetLibrary(IEngineHost host, IEnumerable<string> additionalModules = null)
		{
			IModule[] array;
			if (additionalModules != null)
			{
				object obj = Modules.lockObject;
				lock (obj)
				{
					array = new IModule[additionalModules.Count<string>() + 1];
					int num = 0;
					array[num++] = Modules.All;
					using (IEnumerator<string> enumerator = additionalModules.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							Microsoft.Mashup.Engine1.Runtime.Module module;
							if (!Modules.CurrentModules.TryGetValue(text, out module))
							{
								throw new InvalidOperationException(Strings.ModuleNameNotFound(text));
							}
							array[num++] = module;
						}
						goto IL_00A0;
					}
				}
			}
			array = new IModule[] { Modules.All };
			IL_00A0:
			return LanguageLibrary.LinkLibrary(host, array);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00032780 File Offset: 0x00030980
		public static bool TryGetModule(string moduleName, out Microsoft.Mashup.Engine1.Runtime.Module module)
		{
			object obj = Modules.lockObject;
			bool flag2;
			lock (obj)
			{
				if (moduleName == "Legacy")
				{
					module = Modules.All;
					flag2 = true;
				}
				else
				{
					flag2 = Modules.CurrentModules.TryGetValue(moduleName, out module);
				}
			}
			return flag2;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x000327E0 File Offset: 0x000309E0
		public static bool TryLoadDllExtension(IEngine engine, string path, out string moduleName, out Exception error)
		{
			bool flag;
			try
			{
				ExternalModule externalModule = Modules.LoadExtension<ExternalModule>(path, out error);
				if (externalModule == null)
				{
					moduleName = null;
					flag = false;
				}
				else
				{
					object obj = Modules.lockObject;
					lock (obj)
					{
						Microsoft.Mashup.Engine1.Runtime.Module module = Modules.WrapModule(externalModule, externalModule.DocumentationResources);
						if (!Modules.TryAddOptionalModule(engine, module, out error))
						{
							error = new InvalidOperationException(Strings.ModuleNameDuplicate(externalModule.Name));
							moduleName = null;
							flag = false;
						}
						else
						{
							error = null;
							moduleName = externalModule.Name;
							Modules.externalModules[moduleName] = path;
							flag = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				error = ex;
				moduleName = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000328A4 File Offset: 0x00030AA4
		public static global::System.Reflection.Assembly LoadAssembly(string fileName, out Exception error)
		{
			Dictionary<string, object> dictionary = Modules.assemblies;
			global::System.Reflection.Assembly assembly;
			lock (dictionary)
			{
				object obj;
				if (!Modules.assemblies.TryGetValue(fileName, out obj))
				{
					try
					{
						global::System.Reflection.Assembly executingAssembly = global::System.Reflection.Assembly.GetExecutingAssembly();
						string text = Path.Combine(Path.GetDirectoryName(executingAssembly.Location), fileName);
						AssemblyName assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(text));
						if (!Modules.IsShadowCopyAppDomain)
						{
							assemblyName.CodeBase = text;
						}
						assemblyName.SetPublicKeyToken(executingAssembly.GetName().GetPublicKeyToken());
						obj = global::System.Reflection.Assembly.Load(assemblyName);
					}
					catch (Exception ex)
					{
						if (!SafeExceptions.IsSafeException(ex))
						{
							throw;
						}
						obj = ex;
					}
					Modules.assemblies.Add(fileName, obj);
				}
				error = obj as Exception;
				assembly = ((error == null) ? ((global::System.Reflection.Assembly)obj) : null);
			}
			return assembly;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00032980 File Offset: 0x00030B80
		public static TExtension LoadExtension<TExtension>(string fileName, out Exception error)
		{
			global::System.Reflection.Assembly assembly = Modules.LoadAssembly(fileName, out error);
			if (assembly == null)
			{
				return default(TExtension);
			}
			try
			{
				Type type = (from t in assembly.GetExportedTypes()
					where typeof(TExtension).IsAssignableFrom(t)
					orderby t.FullName
					select t).FirstOrDefault<Type>();
				if (type != null)
				{
					error = null;
					return (TExtension)((object)type.GetConstructor(Type.EmptyTypes).Invoke(new object[0]));
				}
				error = new InvalidOperationException();
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				error = ex;
			}
			return default(TExtension);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x00032A5C File Offset: 0x00030C5C
		public static bool TryDelayedLoadExtension(IEngine engine, IModule moduleInfo, ILibraryService libraryService, out Exception error)
		{
			object obj = Modules.lockObject;
			bool flag2;
			lock (obj)
			{
				Microsoft.Mashup.Engine1.Runtime.Module module = Modules.CreateDelayLoadedExtension(engine, moduleInfo, libraryService);
				flag2 = Modules.TryAddOptionalModule(engine, module, out error);
			}
			return flag2;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x00032AA8 File Offset: 0x00030CA8
		public static bool TryCompileDataSource(IEngine engine, ISectionDocument document, IModule library, ILibraryService libraryService, CompileOptions options, Action<IError> log, out IModule module)
		{
			bool flag;
			try
			{
				module = ExtensionModule.Compile(engine, document, library, libraryService, options);
				flag = true;
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				log(SourceErrors.Generic(ex.Message));
				module = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x00032B00 File Offset: 0x00030D00
		public static bool TryLoadExtension(IEngine engine, string moduleSource, ILibraryService libraryService, out string moduleName, out Exception error)
		{
			bool flag2;
			try
			{
				Microsoft.Mashup.Engine1.Runtime.Module module = ExtensionModule.Compile(engine, moduleSource, libraryService, CompileOptions.Debug);
				object obj = Modules.lockObject;
				lock (obj)
				{
					if (!Modules.TryAddOptionalModule(engine, module, out error))
					{
						moduleName = null;
						flag2 = false;
					}
					else
					{
						moduleName = module.Name;
						flag2 = true;
					}
				}
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
				error = ex;
				moduleName = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x00032B88 File Offset: 0x00030D88
		public static bool TryReplaceExtension(IEngine engine, IModule moduleInfo, ILibraryService libraryService, bool delayLoad, out Exception error)
		{
			bool flag2;
			try
			{
				object obj = Modules.lockObject;
				lock (obj)
				{
					Microsoft.Mashup.Engine1.Runtime.Module module = null;
					string name = moduleInfo.Name;
					if (!Modules.CurrentModules.TryGetValue(name, out module))
					{
						error = new InvalidOperationException(Strings.ModuleNameNotFound(name));
						return false;
					}
					Microsoft.Mashup.Engine1.Runtime.Module module2;
					if (delayLoad)
					{
						module2 = Modules.CreateDelayLoadedExtension(engine, moduleInfo, libraryService);
					}
					else
					{
						string source = libraryService.GetSource(name);
						module2 = ExtensionModule.Compile(engine, source, libraryService, CompileOptions.Debug);
					}
					using (ResourceKinds.OperationSet())
					{
						Modules.RemoveOptionalModule(engine, module);
						if (!Modules.TryAddOptionalModule(engine, module2, out error))
						{
							Exception ex;
							Modules.TryAddOptionalModule(engine, module, out ex);
							return false;
						}
					}
				}
				error = null;
				flag2 = true;
			}
			catch (Exception ex2)
			{
				if (!SafeExceptions.IsSafeException(ex2))
				{
					throw;
				}
				error = ex2;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00032C8C File Offset: 0x00030E8C
		public static bool UnloadExtension(IEngine engine, string moduleName)
		{
			object obj = Modules.lockObject;
			bool flag2;
			lock (obj)
			{
				Microsoft.Mashup.Engine1.Runtime.Module module;
				if (Modules.CurrentModules.TryGetValue(moduleName, out module))
				{
					Modules.RemoveOptionalModule(engine, module);
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00032CE4 File Offset: 0x00030EE4
		public static IEnumerable<KeyValuePair<string, string>> GetDllExtensions()
		{
			object obj = Modules.lockObject;
			IEnumerable<KeyValuePair<string, string>> enumerable;
			lock (obj)
			{
				enumerable = Modules.externalModules.ToList<KeyValuePair<string, string>>();
			}
			return enumerable;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00032D2C File Offset: 0x00030F2C
		public static IEnumerable<string> GetModuleNames()
		{
			object obj = Modules.lockObject;
			IEnumerable<string> enumerable;
			lock (obj)
			{
				enumerable = Modules.CurrentModules.Keys.ToArray<string>();
			}
			return enumerable;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x00032D78 File Offset: 0x00030F78
		public static IEnumerable<IModule> GetBuiltinModules()
		{
			object obj = Modules.lockObject;
			IEnumerable<IModule> enumerable;
			lock (obj)
			{
				List<IModule> list = new List<IModule>(Modules.LegacyModules.Length + Modules.AdditionalModules.Length + 1 - Modules.removedModules.Count);
				list.Add(Modules.CoreModule);
				foreach (Microsoft.Mashup.Engine1.Runtime.Module module in Modules.LegacyModules.Concat(Modules.AdditionalModules))
				{
					if (!Modules.removedModules.ContainsUnsafe(module.Name))
					{
						list.Add(module);
					}
				}
				enumerable = list;
			}
			return enumerable;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x00032E40 File Offset: 0x00031040
		public static void EnsureLoaded(IEngineHost engineHost, string moduleName)
		{
			Modules.DelayedLoadModule delayedLoadModule = null;
			object obj = Modules.lockObject;
			lock (obj)
			{
				Microsoft.Mashup.Engine1.Runtime.Module module;
				if (Modules.CurrentModules.TryGetValue(moduleName, out module))
				{
					delayedLoadModule = module as Modules.DelayedLoadModule;
				}
			}
			if (delayedLoadModule != null)
			{
				delayedLoadModule.LoadModule(engineHost);
			}
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x00032E9C File Offset: 0x0003109C
		public static Microsoft.Mashup.Engine1.Runtime.Module Compile(string source)
		{
			return Engine.Instance.Compile(source, RecordValue.Empty, CompileOptions.None, new Action<IError>(Modules.ThrowOnCompileError));
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00032EBB File Offset: 0x000310BB
		public static Microsoft.Mashup.Engine1.Runtime.Module DelayLoadingModule(IModule definitions, Func<IEngineHost, IModule> moduleLoader)
		{
			return new Modules.DelayedLoadModule(definitions, moduleLoader);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00032EC4 File Offset: 0x000310C4
		public static Microsoft.Mashup.Engine1.Runtime.Module OverrideEngineHostModule(Microsoft.Mashup.Engine1.Runtime.Module module, Func<IEngineHost, IEngineHost> binder)
		{
			return new Modules.OverrideEngineHostBindingModule(module, binder);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x00032ECD File Offset: 0x000310CD
		public static Microsoft.Mashup.Engine1.Runtime.Module InternalizeModule(Microsoft.Mashup.Engine1.Runtime.Module module, string newName = null)
		{
			return new Modules.InternalizedModule(module, newName ?? module.Name);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00032EE0 File Offset: 0x000310E0
		private static Microsoft.Mashup.Engine1.Runtime.Module CreateDelayLoadedExtension(IEngine engine, IModule moduleInfo, ILibraryService libraryService)
		{
			return Modules.DelayLoadingModule(moduleInfo, delegate(IEngineHost engineHost)
			{
				libraryService = engineHost.QueryService<ILibraryService>() ?? libraryService;
				string source = libraryService.GetSource(moduleInfo.Name);
				ExtensionModule extensionModule = ExtensionModule.Compile(engine, source, libraryService, CompileOptions.Debug);
				Exception ex;
				Modules.TryRegisterDataSources(engine, extensionModule, out ex);
				return extensionModule;
			});
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00032F20 File Offset: 0x00031120
		private static bool TryAddOptionalModule(IEngine engine, Microsoft.Mashup.Engine1.Runtime.Module module, out Exception error)
		{
			Microsoft.Mashup.Engine1.Runtime.Module module2;
			if (Modules.CurrentModules.TryGetValue(module.Name, out module2))
			{
				error = new InvalidOperationException(Strings.ModuleNameDuplicate(module.Name));
				return false;
			}
			if (!Modules.TryRegisterDataSources(engine, module, out error))
			{
				return false;
			}
			Modules.CurrentModules[module.Name] = module;
			Modules.KnownModules[module.Name] = module;
			error = null;
			return true;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00032F8C File Offset: 0x0003118C
		private static void ThrowOnCompileError(IError entry)
		{
			throw new InvalidOperationException(entry.Location.Range.ToString() + " " + entry.Message);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00032FC8 File Offset: 0x000311C8
		private static bool TryRegisterDataSources(IEngine engine, Microsoft.Mashup.Engine1.Runtime.Module module, out Exception error)
		{
			bool flag = module is Modules.DelayedLoadModule;
			ResourceKindInfo[] dataSources = module.DataSources;
			for (int i = 0; i < dataSources.Length; i++)
			{
				if (!(flag ? engine.TryDelayedRegisterResourceKind(dataSources[i], module.Name, out error) : engine.TryRegisterResourceKind(dataSources[i], module.Name, out error)))
				{
					for (int j = 0; j < i; j++)
					{
						engine.UnregisterResourceKind(dataSources[j].Kind);
					}
					return false;
				}
			}
			error = null;
			return true;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0003303C File Offset: 0x0003123C
		private static void RemoveOptionalModule(IEngine engine, Microsoft.Mashup.Engine1.Runtime.Module module)
		{
			if (Modules.currentModules != null)
			{
				Modules.currentModules.Remove(module.Name);
			}
			if (Modules.knownModules != null)
			{
				Modules.knownModules.Remove(module.Name);
			}
			Modules.externalModules.Remove(module.Name);
			Modules.UnregisterDataSources(engine, module.DataSources);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00033098 File Offset: 0x00031298
		private static void UnregisterDataSources(IEngine engine, ResourceKindInfo[] dataSources)
		{
			foreach (ResourceKindInfo resourceKindInfo in dataSources)
			{
				engine.UnregisterResourceKind(resourceKindInfo.Kind);
			}
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x000330C8 File Offset: 0x000312C8
		private static Microsoft.Mashup.Engine1.Runtime.Module[] GetLegacyModules()
		{
			return new Microsoft.Mashup.Engine1.Runtime.Module[]
			{
				new AccessModule(),
				new ActiveDirectoryModule(),
				new AdobeAnalyticsModule(),
				new AdoDotNetModule(),
				new AnalysisServicesModule(),
				new AzureBlobsModule(),
				new AzureDataLakeStorageModule(),
				new AzureTablesModule(),
				new CdpaModule(),
				new InformixModule(),
				new MsDb2Module(),
				new EssbaseModule(),
				new ExcelInteropModule(),
				new ExchangeModule(),
				new FileModule(),
				new GoogleAnalyticsModule(),
				new HdfsModule(),
				new HDInsightModule(),
				new HtmlModule(),
				new MqModule(),
				new MySQLModule(),
				new ODataModule(),
				new OdbcModule(),
				new OleDbModule(),
				new OracleModule(),
				new PostgreSQLModule(),
				new SalesforceModule(),
				new SapBusinessWarehouseModule(),
				new SapHanaModule(),
				new SharePointModule(),
				new SpatialModule(),
				new SqlModule(),
				new SybaseModule(),
				new TeradataModule(),
				new WebModule(),
				new WebBrowserContentsModule(),
				new CdmModule(),
				new DeltaLakeModule(),
				new FuzzyGroupingModule(),
				new FuzzyMatchingModule(),
				new HtmlTableModule(),
				new ParquetModule(),
				new PdfModule()
			};
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00033258 File Offset: 0x00031458
		private static Microsoft.Mashup.Engine1.Runtime.Module[] GetAdditionalModules()
		{
			return new Microsoft.Mashup.Engine1.Runtime.Module[]
			{
				Modules.actionModule,
				new CacheManagerModule(),
				new CryptoModule(),
				Modules.dataSourceModule,
				Modules.deltaModule,
				new Microsoft.Mashup.Engine1.Library.Environment.EnvironmentModule(),
				new ExcelTableInferenceModule(),
				Modules.extensibilityModule,
				new OAuthModule(),
				new ParallelEvaluationModule(),
				new ResourceModule(),
				new SqlDatabaseModule(),
				new OpenApiModule()
			};
		}

		// Token: 0x040006D2 RID: 1746
		private static readonly Dictionary<string, object> assemblies = new Dictionary<string, object>();

		// Token: 0x040006D3 RID: 1747
		private static readonly Keys OptionalModulesKeys = Keys.New("ModuleName", "Version", "FileName");

		// Token: 0x040006D4 RID: 1748
		private static readonly Keys ExtensionExportKeys = Keys.New("ModuleName", "Name", "Value", "Publish");

		// Token: 0x040006D5 RID: 1749
		private static readonly Keys ExtensionResourceKeys = Keys.New("ModuleName", "ResourceKind", "Label", "ResourceRecord");

		// Token: 0x040006D6 RID: 1750
		private static readonly object lockObject = new object();

		// Token: 0x040006D7 RID: 1751
		private static Microsoft.Mashup.Engine1.Runtime.Module[] coreModules;

		// Token: 0x040006D8 RID: 1752
		private static Microsoft.Mashup.Engine1.Runtime.Module[] legacyModules;

		// Token: 0x040006D9 RID: 1753
		private static Microsoft.Mashup.Engine1.Runtime.Module[] additionalModules;

		// Token: 0x040006DA RID: 1754
		private static Dictionary<string, string> externalModules = new Dictionary<string, string>();

		// Token: 0x040006DB RID: 1755
		private static Dictionary<string, Microsoft.Mashup.Engine1.Runtime.Module> knownModules;

		// Token: 0x040006DC RID: 1756
		private static Dictionary<string, Microsoft.Mashup.Engine1.Runtime.Module> currentModules;

		// Token: 0x040006DD RID: 1757
		private static Modules.ModuleNameCollection disabledModules = new Modules.ModuleNameCollection();

		// Token: 0x040006DE RID: 1758
		private static Modules.ModuleNameCollection removedModules = new Modules.ModuleNameCollection();

		// Token: 0x040006DF RID: 1759
		private static Microsoft.Mashup.Engine1.Runtime.Module coreModule;

		// Token: 0x040006E0 RID: 1760
		private static Microsoft.Mashup.Engine1.Runtime.Module all;

		// Token: 0x040006E1 RID: 1761
		private static Microsoft.Mashup.Engine1.Runtime.Module dataSourceModule = new DataSourceModule();

		// Token: 0x040006E2 RID: 1762
		private static Microsoft.Mashup.Engine1.Runtime.Module deltaModule = new DeltaModule();

		// Token: 0x040006E3 RID: 1763
		private static Microsoft.Mashup.Engine1.Runtime.Module extensibilityModule = new ExtensibilityModule();

		// Token: 0x040006E4 RID: 1764
		private static Microsoft.Mashup.Engine1.Runtime.Module libraryModule = new Library.LibraryModule();

		// Token: 0x040006E5 RID: 1765
		private static Microsoft.Mashup.Engine1.Runtime.Module actionModule = new Modules.RenamedModule(Linker.Link(new Microsoft.Mashup.Engine1.Runtime.Module[]
		{
			new ActionModule(),
			Linker.Link(new Microsoft.Mashup.Engine1.Runtime.Module[]
			{
				Modules.Compile("// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n\r\nsection ActionModule;\r\n\r\n// TODO (Versions): Remove this function after updating DFE\r\nshared ValueAction.BeginTransaction = (values as record) => let\r\n    field = Record.ToTable(values){0},\r\n    createVersion = Action.Sequence(\r\n    {\r\n        () => TableAction.InsertRows(Value.Versions(field[Value]), #table({\"Version\"}, {{Text.NewGuid()}})),\r\n            (newVersion) => Action.Return(Record.FromTable(#table({\"Name\", \"Value\"}, {{field[Name], newVersion{0}[Data]}})))\r\n    })\r\nin\r\n    if (Record.FieldCount(values) = 1) then createVersion\r\n    else [Reason = \"Expression.Error\", Message = LibraryModule!UICulture.GetString(\"ValueAction_Transaction_NotSupported\"), Detail = values];\r\n\r\n// TODO (Versions): Remove this function after updating DFE\r\nshared ValueAction.CommitTransaction = (values as record) => let\r\n    field = Record.ToTable(values){0},\r\n    publishVersion = Action.Sequence(\r\n    {\r\n        () => TableAction.UpdateRows(\r\n                Table.SelectRows(Value.Versions(field[Value]), each [Version] = Value.VersionIdentity(field[Value])),\r\n                {{\"Published\", each true}}),\r\n        Action.DoNothing\r\n    })\r\nin\r\n    if (Record.FieldCount(values) = 1) then publishVersion\r\n    else [Reason = \"Expression.Error\", Message = LibraryModule!UICulture.GetString(\"ValueAction_Transaction_NotSupported\"), Detail = values];\r\n\r\nHandlers.FromAction = (action as nullable action) as record =>\r\n[\r\n    GetExpression = () => Action!Value.Expression(action),\r\n\r\n    OnBind = (binding) => Action.Sequence({action, binding}),\r\n    OnExecute = () => action,\r\n\r\n    OnInvoke = (function, arguments, index) => Function.Invoke(function, List.ReplaceRange(arguments, index, 1, {action}))\r\n];\r\n\r\nshared Action.View = (\r\n    action as nullable action,\r\n    handlers as record\r\n) as action =>\r\n    let\r\n        defaultHandlers = if (action <> null) then Handlers.FromAction(action) else [],\r\n\r\n        // NOTE: Do not automatically forward Value.Expression to the action as it breaks encapsulation\r\n        defaultHandlersWithoutExpression =\r\n            if (defaultHandlers[GetExpression]? <> null) then defaultHandlers & [GetExpression = () => null]\r\n            else defaultHandlers,\r\n\r\n        viewHandlers = defaultHandlersWithoutExpression & handlers,\r\n        view = Action!Action.FromHandlers(viewHandlers)\r\n    in\r\n        view;\r\n\r\nshared Action.ViewFunction = Value.ViewFunction;\r\n\r\nshared Action.ViewError = Value.ViewError;\r\n\r\n    "),
				Modules.libraryModule
			}, new Action<IError>(Modules.ThrowOnCompileError), LinkOptions.ExportFirstModule | LinkOptions.IgnoreUnresolvedImports)
		}, new Action<IError>(Modules.ThrowOnCompileError), LinkOptions.IgnoreUnresolvedImports), "Action");

		// Token: 0x040006E6 RID: 1766
		private static bool IsShadowCopyAppDomain = AppDomain.CurrentDomain.SetupInformation.ShadowCopyFiles == "true";

		// Token: 0x02000251 RID: 593
		private sealed class DelayedLoadModule : Microsoft.Mashup.Engine1.Runtime.Module
		{
			// Token: 0x06001987 RID: 6535 RVA: 0x00033411 File Offset: 0x00031611
			public DelayedLoadModule(IModule definition, Func<IEngineHost, IModule> moduleLoader)
			{
				this.definition = definition;
				this.exportKeys = Keys.New(definition.Exports.ToArray<string>());
				this.moduleLoader = moduleLoader;
				this.syncRoot = new object();
			}

			// Token: 0x17000CB7 RID: 3255
			// (get) Token: 0x06001988 RID: 6536 RVA: 0x00033448 File Offset: 0x00031648
			public override string Name
			{
				get
				{
					return this.definition.Name;
				}
			}

			// Token: 0x17000CB8 RID: 3256
			// (get) Token: 0x06001989 RID: 6537 RVA: 0x00033455 File Offset: 0x00031655
			public override string Version
			{
				get
				{
					return this.definition.Version;
				}
			}

			// Token: 0x17000CB9 RID: 3257
			// (get) Token: 0x0600198A RID: 6538 RVA: 0x00033462 File Offset: 0x00031662
			public override Keys ExportKeys
			{
				get
				{
					return this.exportKeys;
				}
			}

			// Token: 0x17000CBA RID: 3258
			// (get) Token: 0x0600198B RID: 6539 RVA: 0x0003346A File Offset: 0x0003166A
			public override ResourceKindInfo[] DataSources
			{
				get
				{
					return this.dataSources ?? this.definition.DataSources;
				}
			}

			// Token: 0x0600198C RID: 6540 RVA: 0x00033484 File Offset: 0x00031684
			protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
			{
				IValueReference exports = new DelayedValue(() => this.GetExports(environment, hostEnvironment));
				return RecordValue.New(this.exportKeys, (int i) => exports.Value.AsRecord["Shared"].AsRecord[this.exportKeys[i]]);
			}

			// Token: 0x0600198D RID: 6541 RVA: 0x000334DA File Offset: 0x000316DA
			private RecordValue GetExports(RecordValue environment, IEngineHost hostEnvironment)
			{
				return this.LoadModule(hostEnvironment).Link(environment, hostEnvironment);
			}

			// Token: 0x0600198E RID: 6542 RVA: 0x000334EC File Offset: 0x000316EC
			public Microsoft.Mashup.Engine1.Runtime.Module LoadModule(IEngineHost engineHost)
			{
				object obj = this.syncRoot;
				Microsoft.Mashup.Engine1.Runtime.Module module2;
				lock (obj)
				{
					Microsoft.Mashup.Engine1.Runtime.Module module = this.moduleLoader(engineHost) as Microsoft.Mashup.Engine1.Runtime.Module;
					if (module == null)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.ModuleLoadFailed(this.Name), null, null);
					}
					if (this.dataSources == null)
					{
						this.dataSources = module.DataSources;
					}
					module2 = module;
				}
				return module2;
			}

			// Token: 0x040006E7 RID: 1767
			private readonly IModule definition;

			// Token: 0x040006E8 RID: 1768
			private readonly Keys exportKeys;

			// Token: 0x040006E9 RID: 1769
			private readonly Func<IEngineHost, IModule> moduleLoader;

			// Token: 0x040006EA RID: 1770
			private readonly object syncRoot;

			// Token: 0x040006EB RID: 1771
			private ResourceKindInfo[] dataSources;
		}

		// Token: 0x02000253 RID: 595
		private sealed class OverrideEngineHostBindingModule : DelegatingModule
		{
			// Token: 0x06001992 RID: 6546 RVA: 0x000335B8 File Offset: 0x000317B8
			public OverrideEngineHostBindingModule(Microsoft.Mashup.Engine1.Runtime.Module module, Func<IEngineHost, IEngineHost> binder)
				: base(module)
			{
				this.binder = binder;
			}

			// Token: 0x06001993 RID: 6547 RVA: 0x000335C8 File Offset: 0x000317C8
			public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
			{
				return base.Link(environment, this.binder(hostEnvironment));
			}

			// Token: 0x040006F0 RID: 1776
			private readonly Func<IEngineHost, IEngineHost> binder;
		}

		// Token: 0x02000254 RID: 596
		private sealed class ModuleNameCollection : ICollection<string>, IEnumerable<string>, IEnumerable
		{
			// Token: 0x17000CBB RID: 3259
			// (get) Token: 0x06001994 RID: 6548 RVA: 0x000335E0 File Offset: 0x000317E0
			public int Count
			{
				get
				{
					object lockObject = Modules.lockObject;
					int count;
					lock (lockObject)
					{
						count = this.values.Count;
					}
					return count;
				}
			}

			// Token: 0x17000CBC RID: 3260
			// (get) Token: 0x06001995 RID: 6549 RVA: 0x00002105 File Offset: 0x00000305
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001996 RID: 6550 RVA: 0x00033628 File Offset: 0x00031828
			public void Add(string item)
			{
				object lockObject = Modules.lockObject;
				lock (lockObject)
				{
					if (this.values.Add(item))
					{
						this.ResetModules();
					}
				}
			}

			// Token: 0x06001997 RID: 6551 RVA: 0x00033678 File Offset: 0x00031878
			public void Clear()
			{
				object lockObject = Modules.lockObject;
				lock (lockObject)
				{
					if (this.Count > 0)
					{
						this.ResetModules();
					}
					this.values.Clear();
				}
			}

			// Token: 0x06001998 RID: 6552 RVA: 0x000336CC File Offset: 0x000318CC
			public bool Contains(string item)
			{
				object lockObject = Modules.lockObject;
				bool flag2;
				lock (lockObject)
				{
					flag2 = this.values.Contains(item);
				}
				return flag2;
			}

			// Token: 0x06001999 RID: 6553 RVA: 0x00033714 File Offset: 0x00031914
			public bool ContainsUnsafe(string item)
			{
				return this.values.Contains(item);
			}

			// Token: 0x0600199A RID: 6554 RVA: 0x00033724 File Offset: 0x00031924
			public void CopyTo(string[] array, int arrayIndex)
			{
				object lockObject = Modules.lockObject;
				lock (lockObject)
				{
					this.values.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x0600199B RID: 6555 RVA: 0x0003376C File Offset: 0x0003196C
			public IEnumerator<string> GetEnumerator()
			{
				object lockObject = Modules.lockObject;
				IEnumerator<string> enumerator;
				lock (lockObject)
				{
					enumerator = this.values.ToList<string>().GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x0600199C RID: 6556 RVA: 0x000337BC File Offset: 0x000319BC
			public bool Remove(string item)
			{
				object lockObject = Modules.lockObject;
				bool flag3;
				lock (lockObject)
				{
					bool flag2 = this.values.Remove(item);
					if (flag2)
					{
						this.ResetModules();
					}
					flag3 = flag2;
				}
				return flag3;
			}

			// Token: 0x0600199D RID: 6557 RVA: 0x0003380C File Offset: 0x00031A0C
			IEnumerator IEnumerable.GetEnumerator()
			{
				object lockObject = Modules.lockObject;
				IEnumerator enumerator;
				lock (lockObject)
				{
					enumerator = this.values.ToList<string>().GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x0600199E RID: 6558 RVA: 0x0003385C File Offset: 0x00031A5C
			private void ResetModules()
			{
				Modules.coreModule = null;
				Modules.all = null;
				Modules.currentModules = null;
			}

			// Token: 0x040006F1 RID: 1777
			private readonly HashSet<string> values = new HashSet<string>();
		}

		// Token: 0x02000255 RID: 597
		private sealed class InternalizedModule : DelegatingModule
		{
			// Token: 0x060019A0 RID: 6560 RVA: 0x00033883 File Offset: 0x00031A83
			public InternalizedModule(Microsoft.Mashup.Engine1.Runtime.Module module, string name)
				: base(module)
			{
				this.name = name;
			}

			// Token: 0x17000CBD RID: 3261
			// (get) Token: 0x060019A1 RID: 6561 RVA: 0x00033893 File Offset: 0x00031A93
			public override string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000CBE RID: 3262
			// (get) Token: 0x060019A2 RID: 6562 RVA: 0x0003389B File Offset: 0x00031A9B
			public override Keys ExportKeys
			{
				get
				{
					return Keys.Empty;
				}
			}

			// Token: 0x17000CBF RID: 3263
			// (get) Token: 0x060019A3 RID: 6563 RVA: 0x000338A2 File Offset: 0x00031AA2
			public override Keys SectionKeys
			{
				get
				{
					return base.ExportKeys;
				}
			}

			// Token: 0x060019A4 RID: 6564 RVA: 0x000338AC File Offset: 0x00031AAC
			public override RecordValue Link(RecordValue environment, IEngineHost hostEnvironment)
			{
				RecordValue recordValue = base.Link(environment, hostEnvironment);
				return RecordValue.New(Linker.LinkedKeys, new Value[]
				{
					Value.Null,
					RecordValue.Empty,
					recordValue["Shared"]
				});
			}

			// Token: 0x040006F2 RID: 1778
			private readonly string name;
		}

		// Token: 0x02000256 RID: 598
		private sealed class RenamedModule : DelegatingModule
		{
			// Token: 0x060019A5 RID: 6565 RVA: 0x000338F0 File Offset: 0x00031AF0
			public RenamedModule(Microsoft.Mashup.Engine1.Runtime.Module module, string name)
				: base(module)
			{
				this.name = name;
			}

			// Token: 0x17000CC0 RID: 3264
			// (get) Token: 0x060019A6 RID: 6566 RVA: 0x00033900 File Offset: 0x00031B00
			public override string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x040006F3 RID: 1779
			private readonly string name;
		}
	}
}
