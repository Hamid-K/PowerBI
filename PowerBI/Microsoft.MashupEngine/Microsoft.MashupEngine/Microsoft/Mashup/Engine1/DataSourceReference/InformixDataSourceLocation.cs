using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Drda;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018EE RID: 6382
	internal sealed class InformixDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A2B8 RID: 41656 RVA: 0x0021B703 File Offset: 0x00219903
		public InformixDataSourceLocation()
		{
			base.Protocol = "informix";
		}

		// Token: 0x1700299A RID: 10650
		// (get) Token: 0x0600A2B9 RID: 41657 RVA: 0x0013154E File Offset: 0x0012F74E
		public override string ResourceKind
		{
			get
			{
				return "Informix";
			}
		}

		// Token: 0x1700299B RID: 10651
		// (get) Token: 0x0600A2BA RID: 41658 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x0600A2BB RID: 41659 RVA: 0x00219500 File Offset: 0x00217700
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveServer(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A2BC RID: 41660 RVA: 0x0021B718 File Offset: 0x00219918
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = InformixModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return base.ServerDatabaseFormula("Informix.Database", recordValue);
		}

		// Token: 0x0600A2BD RID: 41661 RVA: 0x0021950A File Offset: 0x0021770A
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetDatabaseResource(out resource);
		}

		// Token: 0x040054D5 RID: 21717
		public static readonly DataSourceLocationFactory Factory = new InformixDataSourceLocation.DslFactory();

		// Token: 0x040054D6 RID: 21718
		private const string Informix_Database_Function = "Informix.Database";

		// Token: 0x020018EF RID: 6383
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x1700299C RID: 10652
			// (get) Token: 0x0600A2BF RID: 41663 RVA: 0x0021B770 File Offset: 0x00219970
			public override string Protocol
			{
				get
				{
					return "informix";
				}
			}

			// Token: 0x0600A2C0 RID: 41664 RVA: 0x0021B777 File Offset: 0x00219977
			public override IDataSourceLocation New()
			{
				return new InformixDataSourceLocation();
			}

			// Token: 0x0600A2C1 RID: 41665 RVA: 0x0021B77E File Offset: 0x0021997E
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateDatabaseLocation("informix", resourcePath, out location);
			}
		}
	}
}
