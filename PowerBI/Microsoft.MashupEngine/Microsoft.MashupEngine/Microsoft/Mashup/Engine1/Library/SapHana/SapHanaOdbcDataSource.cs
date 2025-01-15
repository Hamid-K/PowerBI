using System;
using System.Data;
using System.Text;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb.Serialization;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200045D RID: 1117
	internal sealed class SapHanaOdbcDataSource : OdbcDataSource
	{
		// Token: 0x06002582 RID: 9602 RVA: 0x0006BF24 File Offset: 0x0006A124
		public SapHanaOdbcDataSource(IEngineHost host, IResource resource, IOdbcService service, ConnectionStringHandler connectionStringHandler, Value sourceConnectionValue, OdbcOptions options, OdbcExceptionHandler odbcExceptionHandler, SapHanaAdditionalTracesProvider additionalTracesProvider, bool enableBindColumn = false)
			: base(host, service, connectionStringHandler, sourceConnectionValue, options, odbcExceptionHandler, new Action<IHostTrace>(additionalTracesProvider.GetAdditionalTraces))
		{
			this.tracer = new Tracer(host, "Engine/IO/SapHana/", resource, null, null);
			this.enableBindColumn = enableBindColumn;
			this.additionalTracesProvider = additionalTracesProvider;
		}

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06002583 RID: 9603 RVA: 0x0006BF72 File Offset: 0x0006A172
		public SapHanaFlavor SapHanaFlavor
		{
			get
			{
				if (this.sapHanaFlavor == null)
				{
					this.sapHanaFlavor = new SapHanaFlavor?(this.GetSapHanaFlavor());
				}
				return this.sapHanaFlavor.Value;
			}
		}

		// Token: 0x06002584 RID: 9604 RVA: 0x0006BFA0 File Offset: 0x0006A1A0
		private SapHanaFlavor GetSapHanaFlavor()
		{
			Version version;
			if (this.TryGetSapHanaVersion(out version) && version.Major < 4)
			{
				return SapHanaFlavor.OnPremise;
			}
			SapHanaFlavor sapHanaFlavor;
			try
			{
				sapHanaFlavor = base.ConnectForMetadata<SapHanaFlavor>(delegate(IOdbcConnection connection)
				{
					using (IPageReader pageReader = connection.ExecuteDirect("select COUNT(DISTINCT USER_NAME) from effective_privileges where USER_NAME = current_user AND SCHEMA_NAME = '_SYS_BI' AND PRIVILEGE = 'SELECT' AND OBJECT_TYPE = 'SCHEMA'", EmptyArray<OdbcParameter>.Instance, RowRange.All, OdbcStatementRegistrar.DummyRegistrar))
					{
						using (IDataReader dataReader = new PageReaderDataReader(pageReader, new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties), new Func<ISerializedException, Exception>(PageExceptionSerializer.GetExceptionFromProperties)))
						{
							if (dataReader.Read())
							{
								return (dataReader.GetInt64(0) == 0L) ? SapHanaFlavor.Datasphere : SapHanaFlavor.Cloud;
							}
						}
					}
					return SapHanaFlavor.CloudOrDatasphere;
				});
			}
			catch (OdbcException ex)
			{
				OdbcException ex3;
				OdbcException ex2 = ex3;
				OdbcException ex = ex2;
				this.tracer.Trace("DetermineHanaFlavor/Exception", delegate(IHostTrace trace)
				{
					trace.Add(ex, true);
				});
				sapHanaFlavor = SapHanaFlavor.CloudOrDatasphere;
			}
			finally
			{
				this.tracer.Trace("DetermineHanaFlavor", delegate(IHostTrace trace)
				{
					trace.Add("HanaFlavor", this.sapHanaFlavor.ToString(), false);
				});
			}
			return sapHanaFlavor;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x0006C054 File Offset: 0x0006A254
		protected override OdbcFetchPlanFactory GetFetchPlanFactory()
		{
			if (!this.enableBindColumn)
			{
				return OdbcFetchPlanFactory.BindColumnNotSupportedInstance;
			}
			if (this.sapHanaFetchPlanFactory == null)
			{
				if (!this.versionRetrieved)
				{
					return new SapHanaOdbcDataSource.SapHanaFetchPlanFactory(this.tracer, this.additionalTracesProvider, true);
				}
				this.sapHanaFetchPlanFactory = new SapHanaOdbcDataSource.SapHanaFetchPlanFactory(this.tracer, this.additionalTracesProvider, this.version.Major < 2);
			}
			return this.sapHanaFetchPlanFactory;
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x0006C0C0 File Offset: 0x0006A2C0
		public bool TryGetSapHanaVersion(out Version sapHanaVersion)
		{
			if (!this.versionRetrieved)
			{
				string versionString;
				if (base.TryGetVersion(out versionString))
				{
					this.version = SapHanaOdbcDataSource.ParseVersionOrNull(versionString);
				}
				if (this.version == null)
				{
					this.tracer.Trace("ParseHanaVersion", delegate(IHostTrace trace)
					{
						trace.Add("Unable to parse HANA version string", versionString, false);
					});
				}
				this.versionRetrieved = true;
			}
			sapHanaVersion = this.version;
			return sapHanaVersion != null;
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x0006C13C File Offset: 0x0006A33C
		private static Version ParseVersionOrNull(string input)
		{
			Scanner scanner = new Scanner(input);
			try
			{
				int num = SapHanaOdbcDataSource.ParsePart(scanner);
				if (scanner.HasMore)
				{
					int num2 = SapHanaOdbcDataSource.ParsePart(scanner);
					if (scanner.HasMore)
					{
						int num3 = SapHanaOdbcDataSource.ParsePart(scanner);
						if (scanner.HasMore)
						{
							int num4 = SapHanaOdbcDataSource.ParsePart(scanner);
							return new Version(num, num2, num3, num4);
						}
						return new Version(num, num2, num3);
					}
				}
			}
			catch (FormatException)
			{
			}
			return null;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x0006C1B8 File Offset: 0x0006A3B8
		private static int ParsePart(Scanner scanner)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (scanner.HasMore)
			{
				char c = scanner.Pop();
				if (char.IsNumber(c))
				{
					stringBuilder.Append(c);
				}
				else
				{
					if (c != '.' && c != ' ' && c != '-')
					{
						throw new FormatException();
					}
					break;
				}
			}
			int num;
			if (int.TryParse(stringBuilder.ToString(), out num))
			{
				return num;
			}
			throw new FormatException();
		}

		// Token: 0x04000F73 RID: 3955
		private readonly Tracer tracer;

		// Token: 0x04000F74 RID: 3956
		private readonly bool enableBindColumn;

		// Token: 0x04000F75 RID: 3957
		private readonly SapHanaAdditionalTracesProvider additionalTracesProvider;

		// Token: 0x04000F76 RID: 3958
		private bool versionRetrieved;

		// Token: 0x04000F77 RID: 3959
		private Version version;

		// Token: 0x04000F78 RID: 3960
		private SapHanaFlavor? sapHanaFlavor;

		// Token: 0x04000F79 RID: 3961
		private OdbcFetchPlanFactory sapHanaFetchPlanFactory;

		// Token: 0x0200045E RID: 1118
		internal sealed class SapHanaFetchPlanFactory : OdbcFetchPlanFactory
		{
			// Token: 0x0600258A RID: 9610 RVA: 0x0006C238 File Offset: 0x0006A438
			public SapHanaFetchPlanFactory(Tracer tracer, SapHanaAdditionalTracesProvider additionalTracesProvider, bool allowPartialBinding)
				: base(true)
			{
				this.tracer = tracer;
				this.additionalTracesProvider = additionalTracesProvider;
				this.allowPartialBinding = allowPartialBinding;
			}

			// Token: 0x17000F22 RID: 3874
			// (get) Token: 0x0600258B RID: 9611 RVA: 0x0006C256 File Offset: 0x0006A456
			public override int MaxCellByteLength
			{
				get
				{
					return 24576;
				}
			}

			// Token: 0x0600258C RID: 9612 RVA: 0x0006C260 File Offset: 0x0006A460
			public override void ColumnNotBoundHandler(OdbcPageReaderColumnInfo columnInfo, long maxDataLength)
			{
				if (!this.allowPartialBinding)
				{
					string message = Strings.SapHanaColumnBindingError(columnInfo.Name, columnInfo.TypeMap.SqlType, maxDataLength, this.MaxCellByteLength);
					this.tracer.Trace("SQLBindCol", delegate(IHostTrace trace)
					{
						trace.Add("Message", message, true);
						trace.Add("ColumnName", columnInfo.Name, true);
						trace.Add("DataType", columnInfo.TypeMap.SqlType.ToString(), false);
						trace.Add("IsColumnBound", columnInfo.IsColumnBound, false);
						trace.Add("BoundCellLength", columnInfo.BoundCellLength, false);
						trace.Add("BoundColumnLength", columnInfo.BoundColumnLength, false);
						trace.Add("MaxDataLength", maxDataLength, false);
					});
					throw ValueException.NewDataSourceError(message, Value.Null, null);
				}
			}

			// Token: 0x0600258D RID: 9613 RVA: 0x0006C2FD File Offset: 0x0006A4FD
			public override bool GetUseMultipleRowFetch(bool allowBind, OdbcStatementHandle statement)
			{
				if (this.additionalTracesProvider != null)
				{
					this.additionalTracesProvider.UseMultipleRowFetch = new bool?(allowBind);
				}
				return allowBind;
			}

			// Token: 0x0600258E RID: 9614 RVA: 0x0006C319 File Offset: 0x0006A519
			public override OdbcFetchPlan BuildFetchPlan(OdbcStatementHandle statement, int maxRowCount, bool allowBind, OdbcPageReaderColumnInfo[] columnInfos)
			{
				return new SapHanaOdbcDataSource.SapHanaFetchPlanFactory.SapHanaFetchPlan(columnInfos, this.GetUseMultipleRowFetch(allowBind, statement), maxRowCount, this.MaxCellByteLength, this.tracer, this.allowPartialBinding);
			}

			// Token: 0x04000F7A RID: 3962
			private readonly Tracer tracer;

			// Token: 0x04000F7B RID: 3963
			private readonly bool allowPartialBinding;

			// Token: 0x04000F7C RID: 3964
			private SapHanaAdditionalTracesProvider additionalTracesProvider;

			// Token: 0x0200045F RID: 1119
			private sealed class SapHanaFetchPlan : OdbcFetchPlan
			{
				// Token: 0x0600258F RID: 9615 RVA: 0x0006C33D File Offset: 0x0006A53D
				public SapHanaFetchPlan(OdbcPageReaderColumnInfo[] columnInfos, bool useMultipleRowFetch, int maxRowCount, int maxCellByteLength, Tracer tracer, bool allowPartialBinding)
					: base(columnInfos, useMultipleRowFetch, maxRowCount, maxCellByteLength)
				{
					this.tracer = tracer;
					this.allowPartialBinding = allowPartialBinding;
				}

				// Token: 0x06002590 RID: 9616 RVA: 0x0006C35C File Offset: 0x0006A55C
				public override void BindColumnFailureHandler(OdbcPageReaderColumnInfo columnInfo, Exception exception)
				{
					if (!this.allowPartialBinding)
					{
						string message = Strings.SapHanaOdbcColumnBindingError(columnInfo.Name, columnInfo.TypeMap.SqlType, exception.Message);
						this.tracer.Trace("SQLBindCol", delegate(IHostTrace trace)
						{
							trace.Add("Message", message, true);
							trace.Add("ColumnName", columnInfo.Name, true);
							trace.Add("DataType", columnInfo.TypeMap.SqlType.ToString(), false);
							trace.Add("IsColumnBound", columnInfo.IsColumnBound, false);
							trace.Add("BoundCellLength", columnInfo.BoundCellLength, false);
							trace.Add("BoundColumnLength", columnInfo.BoundColumnLength, false);
							trace.Add(exception, true);
						});
						throw ValueException.NewDataSourceError(message, Value.Null, exception);
					}
				}

				// Token: 0x04000F7D RID: 3965
				private readonly Tracer tracer;

				// Token: 0x04000F7E RID: 3966
				private readonly bool allowPartialBinding;
			}
		}
	}
}
