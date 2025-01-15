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
		// Token: 0x0600052B RID: 1323 RVA: 0x0001F834 File Offset: 0x0001DA34
		public SoapFormatter(XmlaClient theClient)
		{
			this.client = theClient;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001F843 File Offset: 0x0001DA43
		public ResultsetFormatter ReadResponse(XmlReader reader)
		{
			return this.ReadResponse(reader, InlineErrorHandlingType.StoreInCell);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001F850 File Offset: 0x0001DA50
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

		// Token: 0x0600052E RID: 1326 RVA: 0x0001F94C File Offset: 0x0001DB4C
		public ResultsetFormatter ReadDiscoverResponse(XmlReader reader, InlineErrorHandlingType inlineErrorHandling, DataTable inTable, Dictionary<string, bool> columnsToConvertTimeFor)
		{
			return this.ReadDiscoverResponse(reader, inlineErrorHandling, inTable, false, columnsToConvertTimeFor);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001F95C File Offset: 0x0001DB5C
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

		// Token: 0x06000530 RID: 1328 RVA: 0x0001FA18 File Offset: 0x0001DC18
		internal static MDDatasetFormatter ReadDataSetResponse(XmlReader reader)
		{
			if (!XmlaClient.IsDatasetResponseS(reader))
			{
				throw new InvalidOperationException(XmlaSR.SoapFormatter_ResponseIsNotDataset);
			}
			return SoapFormatter.ReadDataSetResponsePrivate(reader);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001FA34 File Offset: 0x0001DC34
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

		// Token: 0x06000532 RID: 1330 RVA: 0x0001FA88 File Offset: 0x0001DC88
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

		// Token: 0x06000533 RID: 1331 RVA: 0x0001FAF0 File Offset: 0x0001DCF0
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

		// Token: 0x06000534 RID: 1332 RVA: 0x0001FB48 File Offset: 0x0001DD48
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

		// Token: 0x06000535 RID: 1333 RVA: 0x0001FB98 File Offset: 0x0001DD98
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

		// Token: 0x04000400 RID: 1024
		private XmlaClient client;
	}
}
