using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Sybase;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x0200190F RID: 6415
	internal sealed class SybaseDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A398 RID: 41880 RVA: 0x0021DB3B File Offset: 0x0021BD3B
		public SybaseDataSourceLocation()
		{
			base.Protocol = "sybase";
		}

		// Token: 0x170029DC RID: 10716
		// (get) Token: 0x0600A399 RID: 41881 RVA: 0x00050F81 File Offset: 0x0004F181
		public override string ResourceKind
		{
			get
			{
				return "Sybase";
			}
		}

		// Token: 0x170029DD RID: 10717
		// (get) Token: 0x0600A39A RID: 41882 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x0600A39B RID: 41883 RVA: 0x0021DB50 File Offset: 0x0021BD50
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = SybaseModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return base.ServerDatabaseFormula("Sybase.Database", recordValue);
		}

		// Token: 0x0600A39C RID: 41884 RVA: 0x0021950A File Offset: 0x0021770A
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetDatabaseResource(out resource);
		}

		// Token: 0x0400550A RID: 21770
		public static readonly DataSourceLocationFactory Factory = new SybaseDataSourceLocation.DslFactory();

		// Token: 0x0400550B RID: 21771
		private const string Sybase_Database_Function = "Sybase.Database";

		// Token: 0x02001910 RID: 6416
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029DE RID: 10718
			// (get) Token: 0x0600A39E RID: 41886 RVA: 0x0021DBA8 File Offset: 0x0021BDA8
			public override string Protocol
			{
				get
				{
					return "sybase";
				}
			}

			// Token: 0x0600A39F RID: 41887 RVA: 0x0021DBAF File Offset: 0x0021BDAF
			public override IDataSourceLocation New()
			{
				return new SybaseDataSourceLocation();
			}

			// Token: 0x0600A3A0 RID: 41888 RVA: 0x0021DBB6 File Offset: 0x0021BDB6
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateDatabaseLocation("sybase", resourcePath, out location);
			}
		}
	}
}
