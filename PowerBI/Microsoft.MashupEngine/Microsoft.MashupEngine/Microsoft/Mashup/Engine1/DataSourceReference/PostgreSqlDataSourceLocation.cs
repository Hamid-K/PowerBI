using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.PostgreSQL;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018FF RID: 6399
	internal sealed class PostgreSqlDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A31D RID: 41757 RVA: 0x0021C9F4 File Offset: 0x0021ABF4
		public PostgreSqlDataSourceLocation()
		{
			base.Protocol = "postgresql";
		}

		// Token: 0x170029B5 RID: 10677
		// (get) Token: 0x0600A31E RID: 41758 RVA: 0x00083258 File Offset: 0x00081458
		public override string ResourceKind
		{
			get
			{
				return "PostgreSQL";
			}
		}

		// Token: 0x170029B6 RID: 10678
		// (get) Token: 0x0600A31F RID: 41759 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x0600A320 RID: 41760 RVA: 0x00219500 File Offset: 0x00217700
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveServer(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A321 RID: 41761 RVA: 0x0021CA08 File Offset: 0x0021AC08
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = PostgreSQLModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return base.ServerDatabaseFormula("PostgreSQL.Database", recordValue);
		}

		// Token: 0x0600A322 RID: 41762 RVA: 0x0021950A File Offset: 0x0021770A
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetDatabaseResource(out resource);
		}

		// Token: 0x040054ED RID: 21741
		public static readonly DataSourceLocationFactory Factory = new PostgreSqlDataSourceLocation.DslFactory();

		// Token: 0x040054EE RID: 21742
		private const string PostgreSQL_Database_Function = "PostgreSQL.Database";

		// Token: 0x02001900 RID: 6400
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029B7 RID: 10679
			// (get) Token: 0x0600A324 RID: 41764 RVA: 0x0021CA60 File Offset: 0x0021AC60
			public override string Protocol
			{
				get
				{
					return "postgresql";
				}
			}

			// Token: 0x0600A325 RID: 41765 RVA: 0x0021CA67 File Offset: 0x0021AC67
			public override IDataSourceLocation New()
			{
				return new PostgreSqlDataSourceLocation();
			}

			// Token: 0x0600A326 RID: 41766 RVA: 0x0021CA6E File Offset: 0x0021AC6E
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateDatabaseLocation("postgresql", resourcePath, out location);
			}
		}
	}
}
