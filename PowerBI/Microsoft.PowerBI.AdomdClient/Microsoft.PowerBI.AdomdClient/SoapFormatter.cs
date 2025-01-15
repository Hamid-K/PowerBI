using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000053 RID: 83
	internal class SoapFormatter
	{
		// Token: 0x0600051E RID: 1310 RVA: 0x0001F504 File Offset: 0x0001D704
		public SoapFormatter(XmlaClient theClient)
		{
			this.client = theClient;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001F513 File Offset: 0x0001D713
		public ResultsetFormatter ReadResponse(XmlReader reader)
		{
			return this.ReadResponse(reader, InlineErrorHandlingType.StoreInCell);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001F520 File Offset: 0x0001D720
		public ResultsetFormatter ReadResponse(XmlReader reader, InlineErrorHandlingType inlineErrorHandling)
		{
			ResultsetFormatter resultsetFormatter2;
			try
			{
				ResultsetFormatter resultsetFormatter = null;
				if (XmlaClient.IsExecuteResponseS(reader))
				{
					resultsetFormatter = SoapFormatter.ReadExecuteResponsePrivate(reader, inlineErrorHandling);
				}
				else if (XmlaClient.IsDiscoverResponseS(reader))
				{
					resultsetFormatter = SoapFormatter.ReadDiscoverResponsePrivate(reader, inlineErrorHandling, null, false, null);
				}
				else
				{
					if (!XmlaClient.IsEmptyResultS(reader))
					{
						throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected execute or discover response, or empty result, got {0}", reader.Name));
					}
					XmlaClient.ReadEmptyRootS(reader);
				}
				resultsetFormatter2 = resultsetFormatter;
			}
			catch (XmlException ex)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.client != null)
				{
					this.client.Disconnect(false);
				}
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch (AdomdUnknownResponseException)
			{
				throw;
			}
			catch
			{
				if (this.client != null)
				{
					this.client.Disconnect(false);
				}
				throw;
			}
			finally
			{
				this.EndReceival(reader);
			}
			return resultsetFormatter2;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001F61C File Offset: 0x0001D81C
		public ResultsetFormatter ReadDiscoverResponse(XmlReader reader, InlineErrorHandlingType inlineErrorHandling, DataTable inTable, Dictionary<string, bool> columnsToConvertTimeFor)
		{
			return this.ReadDiscoverResponse(reader, inlineErrorHandling, inTable, false, columnsToConvertTimeFor);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0001F62C File Offset: 0x0001D82C
		public ResultsetFormatter ReadDiscoverResponse(XmlReader reader, InlineErrorHandlingType inlineErrorHandling, DataTable inTable, bool schemaOnly, Dictionary<string, bool> columnsToConvertTimeFor)
		{
			ResultsetFormatter resultsetFormatter;
			try
			{
				resultsetFormatter = SoapFormatter.ReadDiscoverResponsePrivate(reader, inlineErrorHandling, inTable, schemaOnly, columnsToConvertTimeFor);
			}
			catch (XmlException ex)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.client != null)
				{
					this.client.Disconnect(false);
				}
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch (AdomdUnknownResponseException)
			{
				throw;
			}
			catch (AdomdErrorResponseException)
			{
				throw;
			}
			catch
			{
				if (this.client != null)
				{
					this.client.Disconnect(false);
				}
				throw;
			}
			finally
			{
				this.EndReceival(reader);
			}
			return resultsetFormatter;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001F6E8 File Offset: 0x0001D8E8
		internal static MDDatasetFormatter ReadDataSetResponse(XmlReader reader)
		{
			if (!XmlaClient.IsDatasetResponseS(reader))
			{
				throw new InvalidOperationException(XmlaSR.SoapFormatter_ResponseIsNotDataset);
			}
			return SoapFormatter.ReadDataSetResponsePrivate(reader);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0001F704 File Offset: 0x0001D904
		private void EndReceival(XmlReader reader)
		{
			if (!(reader is XmlaReader) || (reader is XmlaReader && !((XmlaReader)reader).IsReaderDetached))
			{
				if (this.client != null && this.client.IsConnected)
				{
					this.client.EndReceival();
					return;
				}
			}
			else
			{
				reader.Close();
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0001F758 File Offset: 0x0001D958
		private static ResultsetFormatter ReadExecuteResponsePrivate(XmlReader reader, InlineErrorHandlingType inlineErrorHandling)
		{
			XmlaClient.StartExecuteResponseS(reader);
			if (XmlaClient.IsDatasetResponseS(reader))
			{
				return SoapFormatter.ReadDataSetResponsePrivate(reader);
			}
			if (XmlaClient.IsRowsetResponseS(reader))
			{
				return SoapFormatter.ReadRowsetResponsePrivate(reader, inlineErrorHandling, null, false, null);
			}
			if (!XmlaClient.IsEmptyResultS(reader))
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format("Expected dataset, rowset, or empty result, got {0}", reader.Name));
			}
			XmlaClient.ReadEmptyRootS(reader);
			XmlaClient.EndExecuteResponseS(reader);
			return null;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001F7C0 File Offset: 0x0001D9C0
		private static ResultsetFormatter ReadDiscoverResponsePrivate(XmlReader reader, InlineErrorHandlingType inlineErrorHandling, DataTable inTable, bool schemaOnly, Dictionary<string, bool> columnsToConvertTimeFor)
		{
			XmlaClient.StartDiscoverResponseS(reader);
			if (XmlaClient.IsRowsetResponseS(reader))
			{
				return SoapFormatter.ReadRowsetResponsePrivate(reader, inlineErrorHandling, inTable, schemaOnly, columnsToConvertTimeFor);
			}
			if (!XmlaClient.IsEmptyResultS(reader))
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format("Expected rowset or empty result, got {0}", reader.Name));
			}
			XmlaClient.ReadEmptyRootS(reader);
			XmlaClient.EndDiscoverResponseS(reader);
			return null;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001F818 File Offset: 0x0001DA18
		private static ResultsetFormatter ReadRowsetResponsePrivate(XmlReader reader, InlineErrorHandlingType inlineErrorHandling, DataTable inTable, bool schemaOnly, Dictionary<string, bool> columnsToConvertTimeFor)
		{
			RowsetFormatter rowsetFormatter = new RowsetFormatter();
			if (reader.IsEmptyElement)
			{
				reader.Skip();
			}
			else
			{
				XmlaClient.StartRowsetResponseS(reader);
				bool flag = reader.LocalName == "schema";
				rowsetFormatter.ReadRowset(reader, inlineErrorHandling, inTable, flag, schemaOnly, columnsToConvertTimeFor);
				XmlaClient.EndRowsetResponseS(reader);
			}
			return rowsetFormatter;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001F868 File Offset: 0x0001DA68
		private static MDDatasetFormatter ReadDataSetResponsePrivate(XmlReader reader)
		{
			MDDatasetFormatter mddatasetFormatter = new MDDatasetFormatter();
			if (reader.IsEmptyElement)
			{
				reader.Skip();
			}
			else
			{
				XmlaClient.StartDatasetResponseS(reader);
				mddatasetFormatter.ReadMDDataset(reader);
				XmlaClient.EndDatasetResponseS(reader);
			}
			return mddatasetFormatter;
		}

		// Token: 0x040003F3 RID: 1011
		private XmlaClient client;
	}
}
