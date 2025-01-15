using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.MySQL;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018F5 RID: 6389
	internal sealed class MySqlDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A2DC RID: 41692 RVA: 0x0021BB4B File Offset: 0x00219D4B
		public MySqlDataSourceLocation()
		{
			base.Protocol = "mysql";
		}

		// Token: 0x170029A4 RID: 10660
		// (get) Token: 0x0600A2DD RID: 41693 RVA: 0x000E046E File Offset: 0x000DE66E
		public override string ResourceKind
		{
			get
			{
				return "MySql";
			}
		}

		// Token: 0x170029A5 RID: 10661
		// (get) Token: 0x0600A2DE RID: 41694 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x0600A2DF RID: 41695 RVA: 0x00219500 File Offset: 0x00217700
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveServer(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A2E0 RID: 41696 RVA: 0x0021BB60 File Offset: 0x00219D60
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = MySQLModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return base.ServerDatabaseFormula("MySQL.Database", recordValue);
		}

		// Token: 0x0600A2E1 RID: 41697 RVA: 0x0021950A File Offset: 0x0021770A
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetDatabaseResource(out resource);
		}

		// Token: 0x040054DE RID: 21726
		public static readonly DataSourceLocationFactory Factory = new MySqlDataSourceLocation.DslFactory();

		// Token: 0x040054DF RID: 21727
		private const string MySQL_Database_Function = "MySQL.Database";

		// Token: 0x020018F6 RID: 6390
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029A6 RID: 10662
			// (get) Token: 0x0600A2E3 RID: 41699 RVA: 0x0021BBB8 File Offset: 0x00219DB8
			public override string Protocol
			{
				get
				{
					return "mysql";
				}
			}

			// Token: 0x0600A2E4 RID: 41700 RVA: 0x0021BBBF File Offset: 0x00219DBF
			public override IDataSourceLocation New()
			{
				return new MySqlDataSourceLocation();
			}

			// Token: 0x0600A2E5 RID: 41701 RVA: 0x0021BBC6 File Offset: 0x00219DC6
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateDatabaseLocation("mysql", resourcePath, out location);
			}
		}
	}
}
