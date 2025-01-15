using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OleDb
{
	// Token: 0x0200058B RID: 1419
	public class OleDbModule : Module
	{
		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x00088E34 File Offset: 0x00087034
		public override string Name
		{
			get
			{
				return "OleDb";
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x00088E3B File Offset: 0x0008703B
		public override string Location
		{
			get
			{
				return typeof(OleDbModule).Assembly.Location;
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x00088E51 File Offset: 0x00087051
		public override Keys ExportKeys
		{
			get
			{
				if (OleDbModule.exportKeys == null)
				{
					OleDbModule.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "OleDb.Query";
						}
						if (index != 1)
						{
							throw new InvalidOperationException();
						}
						return "OleDb.DataSource";
					});
				}
				return OleDbModule.exportKeys;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x00088E89 File Offset: 0x00087089
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { OleDbModule.resourceKindInfo };
			}
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x00088E9C File Offset: 0x0008709C
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new OleDbModule.QueryFunctionValue(host);
				}
				if (index != 1)
				{
					throw new InvalidOperationException();
				}
				return new OleDbModule.DataSourceFunctionValue(host);
			});
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x00088ED0 File Offset: 0x000870D0
		private static bool TryGetLocation(Value argument, out IDataSourceLocation location)
		{
			if (!argument.IsText && !argument.IsRecord)
			{
				location = null;
				return false;
			}
			Dictionary<string, string> dictionary = ConnectionStringHandler.HandleFormatExceptions<Dictionary<string, string>>("OLE DB", argument, () => OleDbEnvironment.ConnectionString.GetKeyValuePairs(OleDbEnvironment.ConnectionString.GetString(argument)));
			OleDbDataSourceLocation oleDbDataSourceLocation = new OleDbDataSourceLocation();
			oleDbDataSourceLocation.Options = dictionary.ToDictionary((KeyValuePair<string, string> kvp) => kvp.Key, (KeyValuePair<string, string> kvp) => kvp.Value);
			location = oleDbDataSourceLocation;
			return true;
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x00088F78 File Offset: 0x00087178
		private static IResource CreateResource(IEngineHost host, Value connectionAttributes)
		{
			IExtensibilityService extensibilityService = host.QueryService<IExtensibilityService>();
			if (extensibilityService != null && extensibilityService.CurrentResource != null && extensibilityService.ImpersonateResource)
			{
				return extensibilityService.CurrentResource;
			}
			IResource resource;
			try
			{
				resource = Resource.New("OleDb", OleDbEnvironment.ConnectionString.GetString(connectionAttributes));
			}
			catch (FormatException ex)
			{
				Strings.GenericProviders_InvalidConnectionString(ex.Message);
				throw ValueException.NewExpressionError(string.Format(CultureInfo.CurrentCulture, "{0}: {1}", "OLE DB", ex.Message), connectionAttributes, null);
			}
			return resource;
		}

		// Token: 0x04001393 RID: 5011
		public const string DataSourceName = "OLE DB";

		// Token: 0x04001394 RID: 5012
		private static readonly Keys QueryOptionKeys = Keys.New("Query");

		// Token: 0x04001395 RID: 5013
		internal static readonly OptionRecordDefinition DataSourceOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			Options.NavigationPropertyNameGeneratorOption,
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, "True"),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("SqlCompatibleWindowsAuth", NullableTypeValue.Logical)
		});

		// Token: 0x04001396 RID: 5014
		internal static readonly OptionRecordDefinition QueryOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("SqlCompatibleWindowsAuth", NullableTypeValue.Logical)
		});

		// Token: 0x04001397 RID: 5015
		private static readonly ResourceKindInfo resourceKindInfo = new GenericProviderResourceKindInfo("OleDb", Strings.OleDbChallengeTitle, OleDbEnvironment.ConnectionString, new AuthenticationInfo[]
		{
			new UsernamePasswordAuthenticationInfo
			{
				Description = Strings.OleDbSqlAuth
			},
			new ImplicitAuthenticationInfo
			{
				Label = Strings.GenericProvidersNoneAuthLabel,
				Description = Strings.OleDbNoneAuth
			},
			new WindowsAuthenticationInfo
			{
				Description = Strings.OleDbWindowsAuth,
				SupportsAlternateCredentials = true
			}
		}, new DataSourceLocationFactory[] { OleDbDataSourceLocation.Factory });

		// Token: 0x04001398 RID: 5016
		private static Keys exportKeys;

		// Token: 0x0200058C RID: 1420
		private enum Exports
		{
			// Token: 0x0400139A RID: 5018
			Query,
			// Token: 0x0400139B RID: 5019
			DataSource,
			// Token: 0x0400139C RID: 5020
			Count
		}

		// Token: 0x0200058D RID: 1421
		private sealed class QueryFunctionValue : NativeFunctionValue3<TableValue, Value, TextValue, Value>
		{
			// Token: 0x06002CFD RID: 11517 RVA: 0x0008919C File Offset: 0x0008739C
			public QueryFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 2, "connectionString", TypeValue.Any, "query", TypeValue.Text, "options", OleDbModule.QueryFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06002CFE RID: 11518 RVA: 0x000891DC File Offset: 0x000873DC
			public override TableValue TypedInvoke(Value connectionAttributes, TextValue query, Value options)
			{
				IResource resource = OleDbModule.CreateResource(this.host, connectionAttributes);
				RecordValue recordValue = RecordValue.New(OleDbModule.QueryOptionKeys, new Value[] { query });
				return new OleDbEnvironment(this.host, resource, connectionAttributes, options.IsNull ? recordValue : options.Concatenate(recordValue), OleDbModule.QueryOptionRecord.Concatenate(GenericDbEnvironment.NativeQueryOptionRecord)).CreateTable();
			}

			// Token: 0x17001099 RID: 4249
			// (get) Token: 0x06002CFF RID: 11519 RVA: 0x00088E34 File Offset: 0x00087034
			public override string PrimaryResourceKind
			{
				get
				{
					return "OleDb";
				}
			}

			// Token: 0x06002D00 RID: 11520 RVA: 0x00089240 File Offset: 0x00087440
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues != null && argumentValues.Length >= 2 && argumentValues.Length <= 3)
				{
					Keys empty = Keys.Empty;
					if (argumentValues[0].IsRecord)
					{
						argumentValues[0] = ExpressionAnalysis.RemovePlaceholders(argumentValues[0].AsRecord, out empty);
					}
					if (empty.Length == 0 && OleDbModule.TryGetLocation(argumentValues[0], out location))
					{
						IExpression expression2;
						if (argumentValues.Length >= 3)
						{
							expression2 = ((IInvocationExpression)expression).Arguments[2];
						}
						else
						{
							IExpression @null = ConstantExpressionSyntaxNode.Null;
							expression2 = @null;
						}
						bool? flag;
						StaticAnalysisResolver.HandleOptions(expression2, location, out foundOptions, out unknownOptions, out flag);
						if (argumentValues[1].IsText)
						{
							location.Query = argumentValues[1].AsString;
						}
						else
						{
							unknownOptions = Keys.New(unknownOptions.Concat(StaticAnalysisResolver.NativeQueryKeys).ToArray<string>());
						}
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x0400139D RID: 5021
			private static readonly TypeValue optionsType = OleDbModule.QueryOptionRecord.CreateRecordType().Nullable;

			// Token: 0x0400139E RID: 5022
			private readonly IEngineHost host;
		}

		// Token: 0x0200058E RID: 1422
		public sealed class DataSourceFunctionValue : NativeFunctionValue2<TableValue, Value, Value>
		{
			// Token: 0x06002D02 RID: 11522 RVA: 0x00089325 File Offset: 0x00087525
			public DataSourceFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "connectionString", TypeValue.Any, "options", OleDbModule.DataSourceFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06002D03 RID: 11523 RVA: 0x00089350 File Offset: 0x00087550
			public override TableValue TypedInvoke(Value connectionAttributes, Value options)
			{
				IResource resource = OleDbModule.CreateResource(this.host, connectionAttributes);
				return new OleDbDataSource(this.host, resource, connectionAttributes, options, OleDbModule.DataSourceOptionRecord).CreateTable();
			}

			// Token: 0x1700109A RID: 4250
			// (get) Token: 0x06002D04 RID: 11524 RVA: 0x00088E34 File Offset: 0x00087034
			public override string PrimaryResourceKind
			{
				get
				{
					return "OleDb";
				}
			}

			// Token: 0x06002D05 RID: 11525 RVA: 0x00089384 File Offset: 0x00087584
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				if (expression.Kind == ExpressionKind.Invocation)
				{
					IInvocationExpression invocationExpression = (IInvocationExpression)expression;
					if (invocationExpression.Arguments.Count >= 1 && invocationExpression.Arguments.Count <= 2)
					{
						Value value = ExpressionAnalysis.GetValue(invocationExpression.Arguments[0]);
						Keys empty = Keys.Empty;
						if (value.IsRecord)
						{
							value = ExpressionAnalysis.RemovePlaceholders(value.AsRecord, out empty);
						}
						if (empty.Length == 0 && OleDbModule.TryGetLocation(value, out location))
						{
							IExpression expression2;
							if (invocationExpression.Arguments.Count >= 2)
							{
								expression2 = invocationExpression.Arguments[1];
							}
							else
							{
								IExpression @null = ConstantExpressionSyntaxNode.Null;
								expression2 = @null;
							}
							bool? flag;
							StaticAnalysisResolver.HandleOptions(expression2, location, out foundOptions, out unknownOptions, out flag);
							return true;
						}
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x0400139F RID: 5023
			private static readonly TypeValue optionsType = OleDbModule.DataSourceOptionRecord.CreateRecordType().Nullable;

			// Token: 0x040013A0 RID: 5024
			private readonly IEngineHost host;
		}
	}
}
