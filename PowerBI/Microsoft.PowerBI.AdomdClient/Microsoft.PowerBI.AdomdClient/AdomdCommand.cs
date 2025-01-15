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
		// Token: 0x06000535 RID: 1333 RVA: 0x0001FA04 File Offset: 0x0001DC04
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

		// Token: 0x06000536 RID: 1334 RVA: 0x0001FA5A File Offset: 0x0001DC5A
		public AdomdCommand(string commandText)
			: this()
		{
			this.commandText = commandText;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001FA69 File Offset: 0x0001DC69
		public AdomdCommand(string commandText, AdomdConnection connection)
			: this(commandText)
		{
			this.connection = connection;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001FA7C File Offset: 0x0001DC7C
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

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001FBA0 File Offset: 0x0001DDA0
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0001FBA8 File Offset: 0x0001DDA8
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

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001FBC7 File Offset: 0x0001DDC7
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0001FBCF File Offset: 0x0001DDCF
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

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001FBD8 File Offset: 0x0001DDD8
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0001FBE0 File Offset: 0x0001DDE0
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

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001FBFF File Offset: 0x0001DDFF
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0001FC08 File Offset: 0x0001DE08
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

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001FC45 File Offset: 0x0001DE45
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0001FC50 File Offset: 0x0001DE50
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0001FC9E File Offset: 0x0001DE9E
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0001FCA6 File Offset: 0x0001DEA6
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

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001FCAF File Offset: 0x0001DEAF
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0001FCB7 File Offset: 0x0001DEB7
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

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
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

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0001FCDC File Offset: 0x0001DEDC
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

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001FCF7 File Offset: 0x0001DEF7
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0001FCFE File Offset: 0x0001DEFE
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

		// Token: 0x0600054B RID: 1355 RVA: 0x0001FD05 File Offset: 0x0001DF05
		public void Cancel()
		{
			if (this.connection != null && this.connection.State == ConnectionState.Open)
			{
				AdomdConnection.CancelCommand(this.connection);
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001FD28 File Offset: 0x0001DF28
		public AdomdParameter CreateParameter()
		{
			return new AdomdParameter();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001FD2F File Offset: 0x0001DF2F
		public int ExecuteNonQuery()
		{
			this.CheckCanExecute();
			this.connection.IExecuteProvider.ExecuteAny(this, this.Properties, this.PrivateParameters);
			this.Connection.OpenedReader = null;
			this.connection.MarkCacheNeedsCheckForValidness();
			return 1;
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001FD6C File Offset: 0x0001DF6C
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001FD74 File Offset: 0x0001DF74
		internal IDataReaderConsumer DataReaderConsumer { get; set; }

		// Token: 0x06000550 RID: 1360 RVA: 0x0001FD7D File Offset: 0x0001DF7D
		public AdomdDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001FD88 File Offset: 0x0001DF88
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

		// Token: 0x06000552 RID: 1362 RVA: 0x0001FDF3 File Offset: 0x0001DFF3
		public object ExecuteScalar()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001FDFA File Offset: 0x0001DFFA
		public void Prepare()
		{
			this.CheckCanExecute();
			this.connection.IExecuteProvider.Prepare(this, this.Properties, this.PrivateParameters);
			this.Connection.OpenedReader = null;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001FE2C File Offset: 0x0001E02C
		public CellSet ExecuteCellSet()
		{
			this.CheckCanExecute();
			MDDatasetFormatter mddatasetFormatter = this.connection.IExecuteProvider.ExecuteMultidimensional(this, this.Properties, this.PrivateParameters);
			this.Connection.OpenedReader = null;
			return new CellSet(this.connection, mddatasetFormatter);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001FE78 File Offset: 0x0001E078
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

		// Token: 0x06000556 RID: 1366 RVA: 0x00020048 File Offset: 0x0001E248
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

		// Token: 0x06000557 RID: 1367 RVA: 0x00020168 File Offset: 0x0001E368
		public AdomdCommand Clone()
		{
			return new AdomdCommand(this);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00020170 File Offset: 0x0001E370
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00020178 File Offset: 0x0001E378
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x00020180 File Offset: 0x0001E380
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

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0002018E File Offset: 0x0001E38E
		IDataParameterCollection IDbCommand.Parameters
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00020196 File Offset: 0x0001E396
		IDbDataParameter IDbCommand.CreateParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0002019E File Offset: 0x0001E39E
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteReader();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000201A6 File Offset: 0x0001E3A6
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000201AF File Offset: 0x0001E3AF
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x000201B8 File Offset: 0x0001E3B8
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

		// Token: 0x06000561 RID: 1377 RVA: 0x00020224 File Offset: 0x0001E424
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

		// Token: 0x06000562 RID: 1378 RVA: 0x00020284 File Offset: 0x0001E484
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

		// Token: 0x06000563 RID: 1379 RVA: 0x000202E5 File Offset: 0x0001E4E5
		private static bool IsMdx(string statement)
		{
			statement = statement.Trim();
			return statement.Length == 0 || statement[0] != '<';
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00020307 File Offset: 0x0001E507
		private IDataParameterCollection PrivateParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0002030F File Offset: 0x0001E50F
		string ICommandContentProvider.CommandText
		{
			get
			{
				return this.CommandText;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00020317 File Offset: 0x0001E517
		Stream ICommandContentProvider.CommandStream
		{
			get
			{
				return this.CommandStream;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0002031F File Offset: 0x0001E51F
		bool ICommandContentProvider.IsContentMdx
		{
			get
			{
				return this.CommandText != null && AdomdCommand.IsMdx(this.CommandText);
			}
		}

		// Token: 0x040003F7 RID: 1015
		private const string timeoutPropName = "Timeout";

		// Token: 0x040003F8 RID: 1016
		private const string ActivityIDPropertyName = "DbpropMsmdActivityID";

		// Token: 0x040003F9 RID: 1017
		private const string RequestPriorityPropertyName = "RequestPriority";

		// Token: 0x040003FA RID: 1018
		private CommandType commandType = CommandType.Text;

		// Token: 0x040003FB RID: 1019
		private string commandText;

		// Token: 0x040003FC RID: 1020
		private Stream commandStream;

		// Token: 0x040003FD RID: 1021
		private int timeOut;

		// Token: 0x040003FE RID: 1022
		private AdomdConnection connection;

		// Token: 0x040003FF RID: 1023
		private AdomdParameterCollection parameters;

		// Token: 0x04000400 RID: 1024
		private AdomdPropertyCollection commandProperties;

		// Token: 0x04000401 RID: 1025
		private AdomdTransaction transaction;

		// Token: 0x04000402 RID: 1026
		private Guid activityID = Guid.Empty;

		// Token: 0x04000403 RID: 1027
		private RequestPriorities requestPriority;
	}
}
