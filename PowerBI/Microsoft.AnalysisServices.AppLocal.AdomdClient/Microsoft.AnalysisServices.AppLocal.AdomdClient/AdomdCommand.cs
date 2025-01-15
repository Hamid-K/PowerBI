using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000056 RID: 86
	public sealed class AdomdCommand : Component, IDbCommand, IDisposable, ICloneable, ICommandContentProvider
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x0001FD34 File Offset: 0x0001DF34
		public AdomdCommand()
		{
			this.commandStream = null;
			this.commandText = null;
			this.timeOut = 0;
			this.connection = null;
			this.parameters = null;
			this.commandProperties = null;
			this.transaction = null;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001FD8A File Offset: 0x0001DF8A
		public AdomdCommand(string commandText)
			: this()
		{
			this.commandText = commandText;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001FD99 File Offset: 0x0001DF99
		public AdomdCommand(string commandText, AdomdConnection connection)
			: this(commandText)
		{
			this.connection = connection;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001FDAC File Offset: 0x0001DFAC
		private AdomdCommand(AdomdCommand originalCommand)
		{
			this.Connection = originalCommand.Connection;
			this.CommandText = originalCommand.CommandText;
			this.CommandStream = originalCommand.CommandStream;
			this.CommandTimeout = originalCommand.CommandTimeout;
			this.CommandType = originalCommand.CommandType;
			if (originalCommand.Parameters.Count > 0)
			{
				AdomdParameterCollection adomdParameterCollection = this.Parameters;
				foreach (object obj in ((IEnumerable)originalCommand.Parameters))
				{
					AdomdParameter adomdParameter = (AdomdParameter)obj;
					adomdParameterCollection.Add(adomdParameter.Clone());
				}
			}
			if (originalCommand.Properties.Count > 0)
			{
				AdomdPropertyCollection properties = this.Properties;
				foreach (AdomdProperty adomdProperty in originalCommand.Properties)
				{
					properties.Add(new AdomdProperty(adomdProperty.Name, adomdProperty.Namespace, adomdProperty.Value));
				}
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001FED0 File Offset: 0x0001E0D0
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0001FED8 File Offset: 0x0001E0D8
		[Browsable(false)]
		public Stream CommandStream
		{
			get
			{
				return this.commandStream;
			}
			set
			{
				if (value != null && !value.CanRead)
				{
					throw new ArgumentException(SR.Command_CommandStreamDoesNotSupportReadingFrom);
				}
				this.commandStream = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0001FEF7 File Offset: 0x0001E0F7
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x0001FEFF File Offset: 0x0001E0FF
		public string CommandText
		{
			get
			{
				return this.commandText;
			}
			set
			{
				this.commandText = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001FF08 File Offset: 0x0001E108
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0001FF10 File Offset: 0x0001E110
		public Guid ActivityID
		{
			get
			{
				return this.activityID;
			}
			set
			{
				this.activityID = value;
				this.AddCommandProperty("DbpropMsmdActivityID", this.activityID);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001FF2F File Offset: 0x0001E12F
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0001FF38 File Offset: 0x0001E138
		public RequestPriorities RequestPriority
		{
			get
			{
				return this.requestPriority;
			}
			set
			{
				this.requestPriority = value;
				int num = 2;
				RequestPriorities requestPriorities = this.requestPriority;
				if (requestPriorities != RequestPriorities.Normal)
				{
					if (requestPriorities == RequestPriorities.Low)
					{
						num = 1;
					}
				}
				else
				{
					num = 2;
				}
				this.AddCommandProperty("RequestPriority", num);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001FF75 File Offset: 0x0001E175
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001FF80 File Offset: 0x0001E180
		public int CommandTimeout
		{
			get
			{
				return this.timeOut;
			}
			set
			{
				this.timeOut = value;
				if (this.timeOut < 0)
				{
					throw new ArgumentException(SR.Command_InvalidTimeout(this.timeOut.ToString(CultureInfo.CurrentCulture)));
				}
				this.AddCommandProperty("Timeout", this.timeOut);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001FFCE File Offset: 0x0001E1CE
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0001FFD6 File Offset: 0x0001E1D6
		[Browsable(false)]
		public CommandType CommandType
		{
			get
			{
				return this.commandType;
			}
			set
			{
				this.commandType = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0001FFDF File Offset: 0x0001E1DF
		// (set) Token: 0x06000553 RID: 1363 RVA: 0x0001FFE7 File Offset: 0x0001E1E7
		public AdomdConnection Connection
		{
			get
			{
				return this.connection;
			}
			set
			{
				this.connection = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		[Browsable(false)]
		public AdomdParameterCollection Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new AdomdParameterCollection(this);
				}
				return this.parameters;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0002000C File Offset: 0x0001E20C
		[Browsable(false)]
		public AdomdPropertyCollection Properties
		{
			get
			{
				if (this.commandProperties == null)
				{
					this.commandProperties = new AdomdPropertyCollection();
				}
				return this.commandProperties;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x00020027 File Offset: 0x0001E227
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x0002002E File Offset: 0x0001E22E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public UpdateRowSource UpdatedRowSource
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00020035 File Offset: 0x0001E235
		public void Cancel()
		{
			if (this.connection != null && this.connection.State == ConnectionState.Open)
			{
				AdomdConnection.CancelCommand(this.connection);
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00020058 File Offset: 0x0001E258
		public AdomdParameter CreateParameter()
		{
			return new AdomdParameter();
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002005F File Offset: 0x0001E25F
		public int ExecuteNonQuery()
		{
			this.CheckCanExecute();
			this.connection.IExecuteProvider.ExecuteAny(this, this.Properties, this.PrivateParameters);
			this.Connection.OpenedReader = null;
			this.connection.MarkCacheNeedsCheckForValidness();
			return 1;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0002009C File Offset: 0x0001E29C
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x000200A4 File Offset: 0x0001E2A4
		internal IDataReaderConsumer DataReaderConsumer { get; set; }

		// Token: 0x0600055D RID: 1373 RVA: 0x000200AD File Offset: 0x0001E2AD
		public AdomdDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000200B8 File Offset: 0x0001E2B8
		public AdomdDataReader ExecuteReader(CommandBehavior behavior)
		{
			if ((behavior & CommandBehavior.SingleRow) == CommandBehavior.SingleRow)
			{
				throw new NotSupportedException();
			}
			this.CheckCanExecute();
			AdomdDataReader adomdDataReader = AdomdDataReader.CreateInstance(this.connection.IExecuteProvider.ExecuteTabular(behavior, this, this.Properties, this.PrivateParameters), behavior, this.Connection);
			if (this.DataReaderConsumer != null)
			{
				this.DataReaderConsumer.SetDataReader(adomdDataReader);
			}
			this.Connection.OpenedReader = adomdDataReader;
			return adomdDataReader;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00020123 File Offset: 0x0001E323
		public object ExecuteScalar()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0002012A File Offset: 0x0001E32A
		public void Prepare()
		{
			this.CheckCanExecute();
			this.connection.IExecuteProvider.Prepare(this, this.Properties, this.PrivateParameters);
			this.Connection.OpenedReader = null;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0002015C File Offset: 0x0001E35C
		public CellSet ExecuteCellSet()
		{
			this.CheckCanExecute();
			MDDatasetFormatter mddatasetFormatter = this.connection.IExecuteProvider.ExecuteMultidimensional(this, this.Properties, this.PrivateParameters);
			this.Connection.OpenedReader = null;
			return new CellSet(this.connection, mddatasetFormatter);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000201A8 File Offset: 0x0001E3A8
		public object Execute()
		{
			this.CheckCanExecute();
			XmlaReader xmlaReader = null;
			xmlaReader = this.connection.IExecuteProvider.Execute(this, this.Properties, this.PrivateParameters);
			this.Connection.OpenedReader = null;
			if (xmlaReader == null)
			{
				this.connection.MarkCacheNeedsCheckForValidness();
				return null;
			}
			object obj2;
			try
			{
				object obj = null;
				if (XmlaClient.IsExecuteResponseS(xmlaReader))
				{
					XmlaClient.StartExecuteResponseS(xmlaReader);
					if (XmlaClient.IsDatasetResponseS(xmlaReader))
					{
						MDDatasetFormatter mddatasetFormatter = SoapFormatter.ReadDataSetResponse(xmlaReader);
						if (mddatasetFormatter != null)
						{
							obj = new CellSet(this.connection, mddatasetFormatter);
						}
					}
					else if (XmlaClient.IsRowsetResponseS(xmlaReader))
					{
						obj = AdomdDataReader.CreateInstance(xmlaReader, CommandBehavior.Default, this.connection);
					}
					else if (XmlaClient.IsEmptyResultS(xmlaReader))
					{
						this.connection.MarkCacheNeedsCheckForValidness();
						XmlaClient.ReadEmptyRootS(xmlaReader);
					}
					else
					{
						if (!XmlaClient.IsMultipleResult(xmlaReader) && !XmlaClient.IsAffectedObjects(xmlaReader))
						{
							this.connection.MarkCacheNeedsCheckForValidness();
							throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected dataset, rowset, empty or multiple results, got {0}", xmlaReader.Name));
						}
						this.connection.MarkCacheNeedsCheckForValidness();
						XmlaClient.ReadMultipleResults(xmlaReader);
					}
				}
				if (!(obj is AdomdDataReader))
				{
					xmlaReader.Close();
				}
				else
				{
					this.Connection.OpenedReader = obj;
				}
				obj2 = obj;
			}
			catch (AdomdConnectionException)
			{
				throw;
			}
			catch (AdomdException)
			{
				if (xmlaReader != null)
				{
					xmlaReader.Close();
				}
				throw;
			}
			catch (XmlException ex)
			{
				if (xmlaReader != null)
				{
					xmlaReader.Close();
				}
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.connection != null)
				{
					this.connection.Close(false);
				}
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch
			{
				if (this.connection != null)
				{
					this.connection.Close(false);
				}
				throw;
			}
			return obj2;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00020378 File Offset: 0x0001E578
		public XmlReader ExecuteXmlReader()
		{
			this.CheckCanExecute();
			XmlaReader xmlaReader = this.connection.IExecuteProvider.Execute(this, this.Properties, this.PrivateParameters);
			this.Connection.OpenedReader = null;
			if (xmlaReader == null)
			{
				return null;
			}
			XmlReader xmlReader;
			try
			{
				XmlaClient.ReadUptoRoot(xmlaReader);
				if (!XmlaClient.IsRowsetResponseS(xmlaReader) && !XmlaClient.IsDatasetResponseS(xmlaReader))
				{
					this.connection.MarkCacheNeedsCheckForValidness();
				}
				xmlaReader.MaskEndOfStream = true;
				xmlaReader.SkipElements = false;
				this.Connection.OpenedReader = xmlaReader;
				xmlReader = xmlaReader;
			}
			catch (AdomdConnectionException)
			{
				throw;
			}
			catch (AdomdException)
			{
				if (xmlaReader != null)
				{
					xmlaReader.Close();
				}
				throw;
			}
			catch (XmlException ex)
			{
				if (xmlaReader != null)
				{
					xmlaReader.Close();
				}
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				if (this.connection != null)
				{
					this.connection.Close(false);
				}
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch
			{
				if (this.connection != null)
				{
					this.connection.Close(false);
				}
				throw;
			}
			return xmlReader;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00020498 File Offset: 0x0001E698
		public AdomdCommand Clone()
		{
			return new AdomdCommand(this);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000204A0 File Offset: 0x0001E6A0
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x000204A8 File Offset: 0x0001E6A8
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x000204B0 File Offset: 0x0001E6B0
		IDbConnection IDbCommand.Connection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (AdomdConnection)value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000204BE File Offset: 0x0001E6BE
		IDataParameterCollection IDbCommand.Parameters
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000204C6 File Offset: 0x0001E6C6
		IDbDataParameter IDbCommand.CreateParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x000204CE File Offset: 0x0001E6CE
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteReader();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000204D6 File Offset: 0x0001E6D6
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x000204DF File Offset: 0x0001E6DF
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x000204E8 File Offset: 0x0001E6E8
		IDbTransaction IDbCommand.Transaction
		{
			get
			{
				return this.transaction;
			}
			set
			{
				if (value == null)
				{
					this.transaction = null;
					return;
				}
				if (!(value is AdomdTransaction))
				{
					throw new ArgumentException(SR.Command_OnlyAdomdTransactionObjectIsSupported, "value");
				}
				AdomdTransaction adomdTransaction = value as AdomdTransaction;
				if (adomdTransaction.IsCompleted)
				{
					throw new InvalidOperationException(SR.Command_OnlyActiveTransactionCanBeAssigned);
				}
				if (adomdTransaction.Connection != this.connection)
				{
					throw new InvalidOperationException(SR.Command_OnlyTransactionAssociatedWithTheSameConnectionCanBeAssigned);
				}
				this.transaction = adomdTransaction;
			}
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00020554 File Offset: 0x0001E754
		private void AddCommandProperty(string propKey, object propValue)
		{
			AdomdProperty adomdProperty = new AdomdProperty(propKey, propValue);
			if (this.commandProperties == null)
			{
				this.commandProperties = new AdomdPropertyCollection();
			}
			else
			{
				int num = this.commandProperties.InternalCollection.IndexOf(adomdProperty);
				if (num != -1)
				{
					this.commandProperties.InternalCollection.RemoveAt(num);
				}
			}
			this.commandProperties.Add(adomdProperty);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x000205B4 File Offset: 0x0001E7B4
		private void CheckCanExecute()
		{
			if (this.connection == null)
			{
				throw new InvalidOperationException(SR.Command_ConnectionIsNotSet);
			}
			AdomdUtils.CheckConnectionOpened(this.connection);
			if (this.CommandText == null && this.CommandStream == null)
			{
				throw new InvalidOperationException(SR.Command_CommandTextCommandStreamNotSet);
			}
			if (this.CommandStream != null && this.CommandText != null)
			{
				throw new InvalidOperationException(SR.Command_CommandTextCommandStreamBothSet);
			}
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00020615 File Offset: 0x0001E815
		private static bool IsMdx(string statement)
		{
			statement = statement.Trim();
			return statement.Length == 0 || statement[0] != '<';
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00020637 File Offset: 0x0001E837
		private IDataParameterCollection PrivateParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0002063F File Offset: 0x0001E83F
		string ICommandContentProvider.CommandText
		{
			get
			{
				return this.CommandText;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00020647 File Offset: 0x0001E847
		Stream ICommandContentProvider.CommandStream
		{
			get
			{
				return this.CommandStream;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0002064F File Offset: 0x0001E84F
		bool ICommandContentProvider.IsContentMdx
		{
			get
			{
				return this.CommandText != null && AdomdCommand.IsMdx(this.CommandText);
			}
		}

		// Token: 0x04000404 RID: 1028
		private const string timeoutPropName = "Timeout";

		// Token: 0x04000405 RID: 1029
		private const string ActivityIDPropertyName = "DbpropMsmdActivityID";

		// Token: 0x04000406 RID: 1030
		private const string RequestPriorityPropertyName = "RequestPriority";

		// Token: 0x04000407 RID: 1031
		private CommandType commandType = CommandType.Text;

		// Token: 0x04000408 RID: 1032
		private string commandText;

		// Token: 0x04000409 RID: 1033
		private Stream commandStream;

		// Token: 0x0400040A RID: 1034
		private int timeOut;

		// Token: 0x0400040B RID: 1035
		private AdomdConnection connection;

		// Token: 0x0400040C RID: 1036
		private AdomdParameterCollection parameters;

		// Token: 0x0400040D RID: 1037
		private AdomdPropertyCollection commandProperties;

		// Token: 0x0400040E RID: 1038
		private AdomdTransaction transaction;

		// Token: 0x0400040F RID: 1039
		private Guid activityID = Guid.Empty;

		// Token: 0x04000410 RID: 1040
		private RequestPriorities requestPriority;
	}
}
