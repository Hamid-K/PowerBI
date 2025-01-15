using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.PowerBIPipeline;
using Microsoft.PowerBI.ReportServer.WebApi.Properties;

namespace Microsoft.PowerBI.ReportServer.WebApi.Excel
{
	// Token: 0x02000037 RID: 55
	public static class ExcelGenerator
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00006E30 File Offset: 0x00005030
		public static async Task CreateTableAsync(Stream outputStream, Stream rawDataStream, ExcelStreamWriterMetadata tableMetadata)
		{
			try
			{
				Stream localExcelFileStream = outputStream;
				using (PowerBIRawDataClientPipeline exploreServicesPipeline = new PowerBIRawDataClientPipeline(rawDataStream, (IPageReader reader) => new PageReaderView(reader, tableMetadata.PrimarySelectsMap, null), null))
				{
					IRawDataPageReader pageReader = exploreServicesPipeline.PageReader;
					localExcelFileStream.Write(Resources.Template, 0, Resources.Template.Length);
					using (ZipArchive archive = new ZipArchive(localExcelFileStream, ZipArchiveMode.Update, true))
					{
						ZipArchiveEntry zipArchiveEntry = archive.CreateEntry("xl/worksheets/sheet1.xml", CompressionLevel.Fastest);
						ZipArchiveEntry zipArchiveEntry2 = archive.CreateEntry("xl/tables/table1.xml", CompressionLevel.Fastest);
						ZipArchiveEntry entry = archive.GetEntry("xl/styles.xml");
						using (Stream sheetStream = zipArchiveEntry.Open())
						{
							using (Stream tableStream = zipArchiveEntry2.Open())
							{
								using (Stream styleStream = entry.Open())
								{
									using (ExcelStreamWriter resultWriter = new ExcelStreamWriter(sheetStream, tableStream, styleStream, tableMetadata))
									{
										await resultWriter.WriteTableAsync(pageReader);
									}
									ExcelStreamWriter resultWriter = null;
								}
								Stream styleStream = null;
							}
							Stream tableStream = null;
						}
						Stream sheetStream = null;
					}
					ZipArchive archive = null;
					if (localExcelFileStream.CanSeek)
					{
						long length = localExcelFileStream.Length;
					}
				}
				PowerBIRawDataClientPipeline exploreServicesPipeline = null;
				localExcelFileStream = null;
			}
			catch (Exception ex)
			{
				Logger.Error(ex, "Error generating xlsx from rawDataStream.", Array.Empty<object>());
				throw ex;
			}
		}
	}
}
