using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.AnalysisServices;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018CA RID: 6346
	internal sealed class AnalysisServicesDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A1CC RID: 41420 RVA: 0x00219218 File Offset: 0x00217418
		public AnalysisServicesDataSourceLocation()
		{
			base.Protocol = "analysis-services";
		}

		// Token: 0x1700295C RID: 10588
		// (get) Token: 0x0600A1CD RID: 41421 RVA: 0x00160F2B File Offset: 0x0015F12B
		public override string ResourceKind
		{
			get
			{
				return "AnalysisServices";
			}
		}

		// Token: 0x1700295D RID: 10589
		// (get) Token: 0x0600A1CE RID: 41422 RVA: 0x0021922B File Offset: 0x0021742B
		// (set) Token: 0x0600A1CF RID: 41423 RVA: 0x0021923D File Offset: 0x0021743D
		public string Server
		{
			get
			{
				return base.Address.GetStringOrNull("server");
			}
			set
			{
				base.Address["server"] = value;
			}
		}

		// Token: 0x1700295E RID: 10590
		// (get) Token: 0x0600A1D0 RID: 41424 RVA: 0x00219250 File Offset: 0x00217450
		// (set) Token: 0x0600A1D1 RID: 41425 RVA: 0x00219262 File Offset: 0x00217462
		public string Database
		{
			get
			{
				return base.Address.GetStringOrNull("database");
			}
			set
			{
				base.Address["database"] = value;
			}
		}

		// Token: 0x1700295F RID: 10591
		// (get) Token: 0x0600A1D2 RID: 41426 RVA: 0x00219275 File Offset: 0x00217475
		// (set) Token: 0x0600A1D3 RID: 41427 RVA: 0x00219287 File Offset: 0x00217487
		public string Model
		{
			get
			{
				return base.Address.GetStringOrNull("model");
			}
			set
			{
				base.Address["model"] = value;
			}
		}

		// Token: 0x17002960 RID: 10592
		// (get) Token: 0x0600A1D4 RID: 41428 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x0600A1D5 RID: 41429 RVA: 0x002192A4 File Offset: 0x002174A4
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			string server = this.Server;
			string database = this.Database;
			string model = this.Model;
			string query = base.Query;
			RecordValue recordValue = null;
			try
			{
				recordValue = ((database == null) ? AnalysisServicesModule.DatabasesOptionRecord.FromJson(optionsJson) : AnalysisServicesModule.DatabaseOptionRecord.FromJson(optionsJson));
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			if (server != null)
			{
				if (database == null && model == null && query == null)
				{
					return DataSourceLocation.FormatInvocation("AnalysisServices.Databases", 1, new object[]
					{
						TextValue.New(server),
						recordValue
					});
				}
				if (database != null && model == null && query == null)
				{
					return DataSourceLocation.FormatInvocation("AnalysisServices.Database", 2, new object[]
					{
						TextValue.New(server),
						TextValue.New(database),
						recordValue
					});
				}
				if (database != null && model == null && query != null)
				{
					recordValue = recordValue ?? RecordValue.Empty;
					recordValue = recordValue.Concatenate(RecordValue.New(Keys.New("Query"), new Value[] { TextValue.New(base.Query) })).AsRecord;
					return DataSourceLocation.FormatInvocation("AnalysisServices.Database", 3, new object[]
					{
						TextValue.New(server),
						TextValue.New(database),
						recordValue
					});
				}
				if (database != null && model != null && query == null)
				{
					ExpressionBuilder instance = ExpressionBuilder.Instance;
					return new FormulaCreationResult(instance.Let(new VariableInitializer[]
					{
						instance.Declare("Source", instance.Invoke("AnalysisServices.Database", 2, new object[] { server, database, recordValue }), true),
						instance.Declare("ExpandModels", instance.Invoke("Table.ExpandTableColumn", new object[]
						{
							Identifier.New("Source"),
							"Data",
							instance.List(new object[] { "Id", "Data" }),
							instance.List(new object[] { "CubeId", "CubeData" })
						}), true),
						instance.Declare("Cube", instance.Navigate(instance.Identifier("ExpandModels"), "CubeId", model, "CubeData"), true)
					}));
				}
			}
			return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
		}

		// Token: 0x0600A1D6 RID: 41430 RVA: 0x00219500 File Offset: 0x00217700
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveServer(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A1D7 RID: 41431 RVA: 0x0021950A File Offset: 0x0021770A
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetDatabaseResource(out resource);
		}

		// Token: 0x0400549F RID: 21663
		public static readonly DataSourceLocationFactory Factory = new AnalysisServicesDataSourceLocation.DslFactory();

		// Token: 0x020018CB RID: 6347
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002961 RID: 10593
			// (get) Token: 0x0600A1D9 RID: 41433 RVA: 0x0021951F File Offset: 0x0021771F
			public override string Protocol
			{
				get
				{
					return "analysis-services";
				}
			}

			// Token: 0x0600A1DA RID: 41434 RVA: 0x00219526 File Offset: 0x00217726
			public override IDataSourceLocation New()
			{
				return new AnalysisServicesDataSourceLocation();
			}

			// Token: 0x0600A1DB RID: 41435 RVA: 0x0021952D File Offset: 0x0021772D
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateDatabaseLocation("analysis-services", resourcePath, out location);
			}
		}
	}
}
