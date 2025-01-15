using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x02001917 RID: 6423
	internal static class StaticAnalysisResolver
	{
		// Token: 0x0600A3BD RID: 41917 RVA: 0x0021DD8C File Offset: 0x0021BF8C
		public static bool TryGetServerDatabaseLocation<T>(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions) where T : IDataSourceLocation, new()
		{
			int num;
			Dictionary<string, IExpression> dictionary;
			Value value;
			Value value2;
			if (StaticAnalysisResolver.serverDatabasePattern.TryMatch(expression, out num, out dictionary) && dictionary.TryGetConstant("server", out value) && dictionary.TryGetConstant("database", out value2))
			{
				IExpression @null;
				if (!dictionary.TryGetValue("options", out @null))
				{
					@null = ConstantExpressionSyntaxNode.Null;
				}
				bool? flag;
				if (StaticAnalysisResolver.TryGetRelationalLocation<T>(value, value2, @null, out location, out foundOptions, out unknownOptions, out flag) && (flag != null || num <= 0))
				{
					if (flag != null && num > 0)
					{
						bool? flag2 = flag;
						bool flag3 = num == 2;
						if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
						{
							return false;
						}
					}
					if (location.TrySetAddressString("object", dictionary, "item"))
					{
						location.TrySetAddressString("schema", dictionary, "schema");
					}
					return true;
				}
				return false;
			}
			location = null;
			foundOptions = null;
			unknownOptions = null;
			return false;
		}

		// Token: 0x0600A3BE RID: 41918 RVA: 0x0021DE68 File Offset: 0x0021C068
		public static bool TryGetServerLocation<T>(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions) where T : IDataSourceLocation, new()
		{
			int num;
			Dictionary<string, IExpression> dictionary;
			Value value;
			if (StaticAnalysisResolver.serverPattern.TryMatch(expression, out num, out dictionary) && dictionary.TryGetConstant("server", out value))
			{
				IExpression @null;
				if (!dictionary.TryGetValue("options", out @null))
				{
					@null = ConstantExpressionSyntaxNode.Null;
				}
				bool? flag;
				if (StaticAnalysisResolver.TryGetRelationalLocation<T>(value, @null, out location, out foundOptions, out unknownOptions, out flag) && (flag != null || num <= 0))
				{
					if (flag != null && num > 0)
					{
						bool? flag2 = flag;
						bool flag3 = num == 2;
						if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
						{
							return false;
						}
					}
					if (location.TrySetAddressString("object", dictionary, "item"))
					{
						location.TrySetAddressString("schema", dictionary, "schema");
					}
					return true;
				}
				return false;
			}
			location = null;
			foundOptions = null;
			unknownOptions = null;
			return false;
		}

		// Token: 0x0600A3BF RID: 41919 RVA: 0x0021DF2C File Offset: 0x0021C12C
		public static bool TryGetRelationalLocation<T>(Value server, Value database, IExpression options, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions, out bool? hierarchicalNavigation) where T : IDataSourceLocation, new()
		{
			if (!server.IsText || !database.IsText)
			{
				location = null;
				foundOptions = null;
				unknownOptions = null;
				hierarchicalNavigation = null;
				return false;
			}
			location = new T();
			location.Address["server"] = server.AsString;
			location.Address["database"] = database.AsString;
			StaticAnalysisResolver.HandleOptions(options, location, out foundOptions, out unknownOptions, out hierarchicalNavigation);
			return true;
		}

		// Token: 0x0600A3C0 RID: 41920 RVA: 0x0021DFA8 File Offset: 0x0021C1A8
		public static bool TryGetRelationalLocation<T>(Value server, IExpression options, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions, out bool? hierarchicalNavigation) where T : IDataSourceLocation, new()
		{
			if (!server.IsText)
			{
				location = null;
				foundOptions = null;
				unknownOptions = null;
				hierarchicalNavigation = null;
				return false;
			}
			location = new T();
			location.Address["server"] = server.AsString;
			StaticAnalysisResolver.HandleOptions(options, location, out foundOptions, out unknownOptions, out hierarchicalNavigation);
			return true;
		}

		// Token: 0x0600A3C1 RID: 41921 RVA: 0x0021E004 File Offset: 0x0021C204
		public static void HandleOptions(IExpression options, IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions, out bool? hierarchicalNavigation)
		{
			RecordValue record = ExpressionAnalysis.GetRecord(options);
			if (record == null)
			{
				foundOptions = null;
				unknownOptions = StaticAnalysisResolver.NativeQueryKeys;
				hierarchicalNavigation = null;
				return;
			}
			foundOptions = ExpressionAnalysis.RemovePlaceholders(record, out unknownOptions);
			hierarchicalNavigation = new bool?(false);
			KeysBuilder keysBuilder = new KeysBuilder(record.Count);
			RecordBuilder recordBuilder = new RecordBuilder(record.Count);
			for (int i = 0; i < record.Count; i++)
			{
				string text = record.Keys[i];
				Value value = record[i];
				if (ExpressionAnalysis.IsPlaceholder(value))
				{
					keysBuilder.Add(text);
				}
				else if (text != "Query")
				{
					recordBuilder.Add(text, value, value.Type);
				}
				else if (value.IsText)
				{
					location.Query = value.AsString;
				}
				else if (!value.IsNull)
				{
					keysBuilder.Add(text);
				}
				if (text == "HierarchicalNavigation")
				{
					hierarchicalNavigation = (value.IsLogical ? new bool?(value.AsBoolean) : null);
				}
			}
			foundOptions = recordBuilder.ToRecord();
			unknownOptions = keysBuilder.ToKeys();
		}

		// Token: 0x04005510 RID: 21776
		public static readonly Keys NativeQueryKeys = Keys.New("Query");

		// Token: 0x04005511 RID: 21777
		private static readonly ExpressionPattern serverDatabasePattern = new ExpressionPattern(new string[] { "__func(__server, __database, _o_options)", "__func(__server, __database, _o_options){[Schema=_o_schema, Item=__item]}[Data]", "__func(__server, __database, _o_options){[Schema=_o_schema]}[Data]{[Name=__item]}[Data]" });

		// Token: 0x04005512 RID: 21778
		private static readonly ExpressionPattern serverPattern = new ExpressionPattern(new string[] { "__func(__server, _o_options)", "__func(__server, _o_options){[Schema=_o_schema, Item=__item]}[Data]", "__func(__server, _o_options){[Schema=_o_schema]}[Data]{[Name=__item]}[Data]" });

		// Token: 0x04005513 RID: 21779
		private const int databaseOnlyIndex = 0;

		// Token: 0x04005514 RID: 21780
		private const int hierarchicalIndex = 2;
	}
}
