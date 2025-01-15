using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Drda;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018D7 RID: 6359
	internal sealed class Db2DataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A224 RID: 41508 RVA: 0x0021A240 File Offset: 0x00218440
		public Db2DataSourceLocation()
		{
			base.Protocol = "db2";
		}

		// Token: 0x17002977 RID: 10615
		// (get) Token: 0x0600A225 RID: 41509 RVA: 0x00133498 File Offset: 0x00131698
		public override string ResourceKind
		{
			get
			{
				return "DB2";
			}
		}

		// Token: 0x17002978 RID: 10616
		// (get) Token: 0x0600A226 RID: 41510 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x0600A227 RID: 41511 RVA: 0x00219500 File Offset: 0x00217700
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveServer(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A228 RID: 41512 RVA: 0x0021A254 File Offset: 0x00218454
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = MsDb2Module.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return base.ServerDatabaseFormula("DB2.Database", recordValue);
		}

		// Token: 0x0600A229 RID: 41513 RVA: 0x0021950A File Offset: 0x0021770A
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetDatabaseResource(out resource);
		}

		// Token: 0x040054B4 RID: 21684
		public static readonly DataSourceLocationFactory Factory = new Db2DataSourceLocation.DslFactory();

		// Token: 0x040054B5 RID: 21685
		private const string DB2_Database_Function = "DB2.Database";

		// Token: 0x020018D8 RID: 6360
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002979 RID: 10617
			// (get) Token: 0x0600A22B RID: 41515 RVA: 0x0021A2AC File Offset: 0x002184AC
			public override string Protocol
			{
				get
				{
					return "db2";
				}
			}

			// Token: 0x0600A22C RID: 41516 RVA: 0x0021A2B3 File Offset: 0x002184B3
			public override IDataSourceLocation New()
			{
				return new Db2DataSourceLocation();
			}

			// Token: 0x0600A22D RID: 41517 RVA: 0x0021A2BA File Offset: 0x002184BA
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateDatabaseLocation("db2", resourcePath, out location);
			}
		}
	}
}
