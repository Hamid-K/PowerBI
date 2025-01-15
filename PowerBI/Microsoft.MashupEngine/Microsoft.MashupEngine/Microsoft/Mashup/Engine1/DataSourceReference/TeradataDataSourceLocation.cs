using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Teradata;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x02001911 RID: 6417
	internal sealed class TeradataDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A3A2 RID: 41890 RVA: 0x0021DBC4 File Offset: 0x0021BDC4
		public TeradataDataSourceLocation()
		{
			base.Protocol = "teradata";
		}

		// Token: 0x170029DF RID: 10719
		// (get) Token: 0x0600A3A3 RID: 41891 RVA: 0x00047D96 File Offset: 0x00045F96
		public override string ResourceKind
		{
			get
			{
				return "Teradata";
			}
		}

		// Token: 0x170029E0 RID: 10720
		// (get) Token: 0x0600A3A4 RID: 41892 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x0600A3A5 RID: 41893 RVA: 0x00219500 File Offset: 0x00217700
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveServer(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A3A6 RID: 41894 RVA: 0x0021DBD8 File Offset: 0x0021BDD8
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = TeradataModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			string stringOrNull = base.Address.GetStringOrNull("database");
			string text = base.Address.GetStringOrNull("schema");
			if (!string.IsNullOrEmpty(stringOrNull))
			{
				if (!string.IsNullOrEmpty(text))
				{
					return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
				}
				text = stringOrNull;
			}
			return base.ServerFormula("Teradata.Database", text, recordValue);
		}

		// Token: 0x0600A3A7 RID: 41895 RVA: 0x0021950A File Offset: 0x0021770A
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetDatabaseResource(out resource);
		}

		// Token: 0x0400550C RID: 21772
		public static readonly DataSourceLocationFactory Factory = new TeradataDataSourceLocation.DslFactory();

		// Token: 0x0400550D RID: 21773
		private const string Teradata_Database_Function = "Teradata.Database";

		// Token: 0x02001912 RID: 6418
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029E1 RID: 10721
			// (get) Token: 0x0600A3A9 RID: 41897 RVA: 0x0021DC70 File Offset: 0x0021BE70
			public override string Protocol
			{
				get
				{
					return "teradata";
				}
			}

			// Token: 0x0600A3AA RID: 41898 RVA: 0x0021DC77 File Offset: 0x0021BE77
			public override IDataSourceLocation New()
			{
				return new TeradataDataSourceLocation();
			}

			// Token: 0x0600A3AB RID: 41899 RVA: 0x0021DC7E File Offset: 0x0021BE7E
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateDatabaseLocation("teradata", resourcePath, out location);
			}
		}
	}
}
