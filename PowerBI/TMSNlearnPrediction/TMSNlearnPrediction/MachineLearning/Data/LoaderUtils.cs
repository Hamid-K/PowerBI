using System;
using System.IO;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000A6 RID: 166
	public static class LoaderUtils
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00012D00 File Offset: 0x00010F00
		public static void SaveLoader(IDataLoader loader, IFileHandle file)
		{
			Contracts.CheckValue<IDataLoader>(loader, "loader");
			Contracts.CheckParam(file != null && file.CanWrite, "file");
			using (Stream stream = file.CreateWriteStream())
			{
				using (RepositoryWriter repositoryWriter = RepositoryWriter.CreateNew(stream, true))
				{
					ModelSaveContext.SaveModel<IDataLoader>(repositoryWriter, loader, "DataLoaderModel");
					repositoryWriter.Commit();
				}
			}
		}
	}
}
