using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Oracle;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018FD RID: 6397
	internal sealed class OracleDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A312 RID: 41746 RVA: 0x0021C8CC File Offset: 0x0021AACC
		public OracleDataSourceLocation()
		{
			base.Protocol = "oracle";
		}

		// Token: 0x170029B2 RID: 10674
		// (get) Token: 0x0600A313 RID: 41747 RVA: 0x00086B9F File Offset: 0x00084D9F
		public override string ResourceKind
		{
			get
			{
				return "Oracle";
			}
		}

		// Token: 0x170029B3 RID: 10675
		// (get) Token: 0x0600A314 RID: 41748 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x0600A315 RID: 41749 RVA: 0x0021C8E0 File Offset: 0x0021AAE0
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			resolvedLocation = base.Clone();
			string[] array = base.Address.GetStringOrNull("server").Split(new char[] { '/' });
			string text;
			if (array.Length <= 2 && base.TryResolveHostName(array[0], getHostEntry, out text))
			{
				if (array.Length == 2)
				{
					text = text + "/" + array[1];
				}
				resolvedLocation.Address["server"] = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600A316 RID: 41750 RVA: 0x0021C954 File Offset: 0x0021AB54
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			bool stringOrNull = base.Address.GetStringOrNull("database") != null;
			string stringOrNull2 = base.Address.GetStringOrNull("schema");
			if (stringOrNull)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			RecordValue recordValue = null;
			try
			{
				recordValue = OracleModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return base.ServerFormula("Oracle.Database", stringOrNull2, recordValue);
		}

		// Token: 0x0600A317 RID: 41751 RVA: 0x0021950A File Offset: 0x0021770A
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetDatabaseResource(out resource);
		}

		// Token: 0x040054EB RID: 21739
		public static readonly DataSourceLocationFactory Factory = new OracleDataSourceLocation.DslFactory();

		// Token: 0x040054EC RID: 21740
		private const string Oracle_Database_Function = "Oracle.Database";

		// Token: 0x020018FE RID: 6398
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029B4 RID: 10676
			// (get) Token: 0x0600A319 RID: 41753 RVA: 0x0021C9D8 File Offset: 0x0021ABD8
			public override string Protocol
			{
				get
				{
					return "oracle";
				}
			}

			// Token: 0x0600A31A RID: 41754 RVA: 0x0021C9DF File Offset: 0x0021ABDF
			public override IDataSourceLocation New()
			{
				return new OracleDataSourceLocation();
			}

			// Token: 0x0600A31B RID: 41755 RVA: 0x0021C9E6 File Offset: 0x0021ABE6
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateDatabaseLocation("oracle", resourcePath, out location);
			}
		}
	}
}
