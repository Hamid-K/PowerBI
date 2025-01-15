using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using Microsoft.Lucia.Core;
using Microsoft.Lucia.Core.Packaging;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000050 RID: 80
	[ImmutableObject(true)]
	internal sealed class DataIndexReader : IDataIndexReader
	{
		// Token: 0x0600025F RID: 607 RVA: 0x00007CDE File Offset: 0x00005EDE
		internal DataIndexReader(Lazy<INaturalLanguageServicesFactory> serviceFactory, string workingDirectoryRoot, Version dataIndexVersion)
		{
			this.m_serviceFactory = serviceFactory;
			this.m_workingDirectoryRoot = workingDirectoryRoot;
			this.m_dataIndexVersion = dataIndexVersion;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00007CFC File Offset: 0x00005EFC
		public bool TryRead(Stream stream, IDatabaseContext databaseContext, CancellationToken token, out OpenDataIndexResult result)
		{
			using (DataIndexPackageReader dataIndexPackageReader = this.CreatePackageReader(stream, databaseContext, token))
			{
				DataIndexVersion dataIndexVersion;
				if (dataIndexPackageReader != null && dataIndexPackageReader.TryReadVersion(out dataIndexVersion) && dataIndexVersion.CompatibleWith(this.m_dataIndexVersion))
				{
					result = this.GetDataIndex(dataIndexPackageReader, databaseContext, token);
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00007D64 File Offset: 0x00005F64
		private DataIndexPackageReader CreatePackageReader(Stream stream, IDatabaseContext databaseContext, CancellationToken token)
		{
			DataIndexPackageReader dataIndexPackageReader;
			try
			{
				token.ThrowIfCancellationRequested();
				dataIndexPackageReader = DataIndexPackage.CreateReader(stream);
			}
			catch (Exception ex)
			{
				throw new DataIndexException("DataIndexReader: Failed to create package reader: " + databaseContext.DatabaseName, ex);
			}
			return dataIndexPackageReader;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00007DAC File Offset: 0x00005FAC
		private OpenDataIndexResult GetDataIndex(DataIndexPackageReader reader, IDatabaseContext databaseContext, CancellationToken token)
		{
			OpenDataIndexResult openDataIndexResult;
			try
			{
				token.ThrowIfCancellationRequested();
				openDataIndexResult = this.m_serviceFactory.Value.CreateManagementService(null, LinguisticSchemaServicesBuilderOptions.None).OpenDataIndex(reader, databaseContext, LuciaUtils.CreateNewLuciaDataIndexWorkingDirectory(this.m_workingDirectoryRoot), token);
			}
			catch (Exception ex)
			{
				throw new DataIndexException("DataIndexReader: Failed to open DataIndex: " + databaseContext.DatabaseName, ex);
			}
			return openDataIndexResult;
		}

		// Token: 0x040000F2 RID: 242
		private readonly Lazy<INaturalLanguageServicesFactory> m_serviceFactory;

		// Token: 0x040000F3 RID: 243
		private readonly string m_workingDirectoryRoot;

		// Token: 0x040000F4 RID: 244
		private readonly Version m_dataIndexVersion;
	}
}
