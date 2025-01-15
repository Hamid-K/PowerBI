using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x0200190D RID: 6413
	internal sealed class SqlDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A38D RID: 41869 RVA: 0x0021D94A File Offset: 0x0021BB4A
		public SqlDataSourceLocation()
		{
			base.Protocol = "tds";
		}

		// Token: 0x170029D9 RID: 10713
		// (get) Token: 0x0600A38E RID: 41870 RVA: 0x0005DF3D File Offset: 0x0005C13D
		public override string ResourceKind
		{
			get
			{
				return "SQL";
			}
		}

		// Token: 0x170029DA RID: 10714
		// (get) Token: 0x0600A38F RID: 41871 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x0600A390 RID: 41872 RVA: 0x0021D960 File Offset: 0x0021BB60
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			resolvedLocation = base.Clone();
			Match match = SqlDataSourceLocation.SqlServerHostNameRegex.Match(base.Address.GetStringOrNull("server"));
			if (match.Success)
			{
				string value = match.Groups["host"].Value;
				string value2 = match.Groups["instance"].Value;
				string value3 = match.Groups["port"].Value;
				string text;
				if (base.TryResolveHostName(value, getHostEntry, out text))
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(text);
					if (!string.IsNullOrEmpty(value2))
					{
						stringBuilder.Append('\\');
						stringBuilder.Append(value2);
					}
					if (!string.IsNullOrEmpty(value3) && value3 != "1433")
					{
						stringBuilder.Append(",");
						stringBuilder.Append(value3);
					}
					resolvedLocation.Address["server"] = stringBuilder.ToString();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600A391 RID: 41873 RVA: 0x0021DA5C File Offset: 0x0021BC5C
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			string stringOrNull = base.Address.GetStringOrNull("server");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			RecordValue recordValue = null;
			try
			{
				recordValue = SqlModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			if (base.Address.GetStringOrNull("database") == null && base.Query == null)
			{
				return DataSourceLocation.FormatInvocation("Sql.Databases", 1, new object[]
				{
					TextValue.New(stringOrNull),
					recordValue
				});
			}
			return base.ServerDatabaseFormula("Sql.Database", recordValue);
		}

		// Token: 0x0600A392 RID: 41874 RVA: 0x0021950A File Offset: 0x0021770A
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetDatabaseResource(out resource);
		}

		// Token: 0x04005506 RID: 21766
		public static readonly DataSourceLocationFactory Factory = new SqlDataSourceLocation.DslFactory();

		// Token: 0x04005507 RID: 21767
		private const string Sql_Database_Function = "Sql.Database";

		// Token: 0x04005508 RID: 21768
		private const string Sql_Databases_Function = "Sql.Databases";

		// Token: 0x04005509 RID: 21769
		private static readonly Regex SqlServerHostNameRegex = new Regex("\\s*(?<host>[^\\\\,\\s]+)\\s*((,\\s*(?<port>[0-9^\\s]+))|(\\\\\\s*(?<instance>[^\\s]+)))?\\s*");

		// Token: 0x0200190E RID: 6414
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029DB RID: 10715
			// (get) Token: 0x0600A394 RID: 41876 RVA: 0x0021DB1F File Offset: 0x0021BD1F
			public override string Protocol
			{
				get
				{
					return "tds";
				}
			}

			// Token: 0x0600A395 RID: 41877 RVA: 0x0021DB26 File Offset: 0x0021BD26
			public override IDataSourceLocation New()
			{
				return new SqlDataSourceLocation();
			}

			// Token: 0x0600A396 RID: 41878 RVA: 0x0021DB2D File Offset: 0x0021BD2D
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateDatabaseLocation("tds", resourcePath, out location);
			}
		}
	}
}
