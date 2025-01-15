using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Xml;
using Microsoft.AnalysisServices.Hosting;
using Microsoft.AnalysisServices.Interop;
using Microsoft.AnalysisServices.Network;
using Microsoft.AnalysisServices.Runtime;
using Microsoft.AnalysisServices.Security;
using Microsoft.AnalysisServices.Sspi;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000059 RID: 89
	internal class XmlaClient
	{
		// Token: 0x060003BE RID: 958 RVA: 0x00015C90 File Offset: 0x00013E90
		static XmlaClient()
		{
			ServicePointManager.DefaultConnectionLimit = 1000;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00015E22 File Offset: 0x00014022
		public XmlaClient()
			: this(null, new StringCollection())
		{
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00015E30 File Offset: 0x00014030
		internal XmlaClient(IConnectivityOwner owner)
			: this(owner, new StringCollection())
		{
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00015E40 File Offset: 0x00014040
		internal XmlaClient(IConnectivityOwner owner, StringCollection log)
		{
			this.owner = owner;
			ServicePointManager.UseNagleAlgorithm = false;
			this.captureLog = log;
			this.namespacesManager = new NamespacesMgr();
			this.nameTable = XmlaConstants.GetNameTable();
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00015E93 File Offset: 0x00014093
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00015E9B File Offset: 0x0001409B
		public string SessionID
		{
			get
			{
				return this.sessionID;
			}
			set
			{
				this.sessionID = value;
				if (this.xmlaStream != null)
				{
					this.xmlaStream.SessionID = this.sessionID;
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00015EBD File Offset: 0x000140BD
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00015EC5 File Offset: 0x000140C5
		public bool IsCompressionEnabled
		{
			get
			{
				return this.isCompressionEnabled;
			}
			set
			{
				this.isCompressionEnabled = value;
				if (this.xmlaStream != null)
				{
					this.xmlaStream.IsCompressionEnabled = value;
				}
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00015EE2 File Offset: 0x000140E2
		public ConnectionInfo ConnectionInfo
		{
			get
			{
				return this.connInfo;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00015EEA File Offset: 0x000140EA
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x00015EF2 File Offset: 0x000140F2
		internal bool SupportsActivityIDAndRequestID
		{
			get
			{
				return this.supportsActivityIDAndRequestID;
			}
			set
			{
				this.supportsActivityIDAndRequestID = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00015EFB File Offset: 0x000140FB
		internal bool SupportsCurrentActivityID
		{
			get
			{
				return this.supportsCurrentActivityID;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00015F03 File Offset: 0x00014103
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00015F0B File Offset: 0x0001410B
		internal bool SupportsApplicationContext
		{
			get
			{
				return this.supportsApplicationContext;
			}
			set
			{
				this.supportsApplicationContext = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00015F14 File Offset: 0x00014114
		internal bool IsConnected
		{
			get
			{
				return this.connected;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00015F1C File Offset: 0x0001411C
		internal bool IsReaderDetached
		{
			get
			{
				XmlaReader xmlaReader = this.reader as XmlaReader;
				return xmlaReader != null && xmlaReader.IsReaderDetached;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00015F40 File Offset: 0x00014140
		public void Connect(ConnectionInfo connectionInfo, bool beginSession)
		{
			if (this.connected)
			{
				throw new InvalidOperationException(XmlaSR.AlreadyConnected);
			}
			if (connectionInfo == null)
			{
				throw new ArgumentNullException("connectionInfo");
			}
			if (connectionInfo.IsPaaSInfrastructure || connectionInfo.IsEmbedded)
			{
				this.supportsActivityIDAndRequestID = true;
				this.supportsCurrentActivityID = true;
				this.supportsApplicationContext = true;
			}
			bool flag = this.captureXml;
			this.captureXml = false;
			bool flag2 = false;
			try
			{
				try
				{
					if (connectionInfo.IsLinkFile())
					{
						using (UserContext userContext = IdentityResolver.Resolve(connectionInfo))
						{
							userContext.ExecuteInUserContext(delegate
							{
								connectionInfo.ResolveLinkFileDataSource(this.owner);
								if (connectionInfo.UseEU)
								{
									if (!this.ConnectionInfo.AllowDelegation)
									{
										throw new ConnectionException(XmlaSR.ConnectionString_LinkFileCannotDelegate);
									}
									connectionInfo.TryAddEffectiveUserName();
								}
							});
						}
					}
					if (connectionInfo.IsAsAzure)
					{
						using (UserContext userContext2 = IdentityResolver.Resolve(connectionInfo))
						{
							userContext2.ExecuteInUserContext(delegate
							{
								connectionInfo.HandleAsAzureRedirection(this.owner);
							});
						}
					}
					this.OpenConnection(connectionInfo, out flag2);
					ConnectionInfo connectionInfo2 = this.connInfo;
					try
					{
						this.connInfo = connectionInfo;
						if (beginSession)
						{
							string text = string.Empty;
							if (flag2)
							{
								text = this.GetSessionToken(connectionInfo.ExtendedProperties, false);
							}
							this.CreateSession(connectionInfo.ExtendedProperties, false, text);
						}
					}
					finally
					{
						this.connInfo = connectionInfo2;
					}
					this.connInfo = connectionInfo;
					this.xmlaStream.IsCompressionEnabled = this.isCompressionEnabled;
					this.userOpened = true;
				}
				catch (IOException ex)
				{
					this.CloseAll();
					throw new ConnectionException(XmlaSR.CannotConnect, ex);
				}
				catch (XmlException ex2)
				{
					this.CloseAll();
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex2);
				}
				catch (ConnectionException ex3)
				{
					if (ex3.Message == XmlaSR.ConnectionBroken)
					{
						throw new ConnectionException(XmlaSR.CannotConnect, ex3.InnerException);
					}
					throw;
				}
				catch
				{
					this.CloseAll();
					throw;
				}
				finally
				{
					this.captureXml = flag;
				}
				if (!this.connInfo.IsLightweightConnection && !this.connInfo.IsForSqlBrowser)
				{
					List<string> list = new List<string>();
					if (!this.supportsCurrentActivityID)
					{
						list.Add("DbpropMsmdCurrentActivityID");
					}
					if (!this.supportsActivityIDAndRequestID)
					{
						list.Add("DbpropMsmdActivityID");
					}
					if (!this.supportsApplicationContext)
					{
						list.Add("ApplicationContext");
					}
					if (list.Count > 0)
					{
						foreach (string text2 in this.SupportsProperties(list))
						{
							if (text2 == "DbpropMsmdCurrentActivityID")
							{
								this.supportsCurrentActivityID = true;
								this.supportsActivityIDAndRequestID = true;
							}
							else if (text2 == "DbpropMsmdActivityID")
							{
								this.supportsActivityIDAndRequestID = true;
							}
							else if (text2 == "ApplicationContext")
							{
								this.supportsApplicationContext = true;
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001630C File Offset: 0x0001450C
		public void Connect(ConnectionInfo connectionInfo, string sessionID)
		{
			if (string.IsNullOrEmpty(sessionID))
			{
				this.sessionID = null;
				this.Connect(connectionInfo, true);
				return;
			}
			this.sessionID = sessionID;
			this.Connect(connectionInfo, false);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00016335 File Offset: 0x00014535
		public void CreateSession(ListDictionary properties, bool sendNamespaceCompatibility)
		{
			this.CreateSession(properties, sendNamespaceCompatibility, string.Empty);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00016344 File Offset: 0x00014544
		public XmlWriter StartMessage(string action)
		{
			return this.StartMessage(action, false, false, false);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00016350 File Offset: 0x00014550
		public void EndMessage()
		{
			this.VerifyIfCanWrite();
			if (!this.captureXml)
			{
				try
				{
					this.writer.WriteEndElement();
					this.writer.WriteEndElement();
				}
				catch (IOException ex)
				{
					this.CloseAll();
					throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
				}
				catch
				{
					this.HandleMessageCreationException();
					throw;
				}
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000163BC File Offset: 0x000145BC
		internal static XmlReader GetReaderToReturnToPublic(XmlReader reader)
		{
			XmlaReader xmlaReader = reader as XmlaReader;
			if (xmlaReader != null && !xmlaReader.IsReaderDetached)
			{
				xmlaReader.DetachReader();
			}
			return reader;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000163E2 File Offset: 0x000145E2
		public void EndReceival()
		{
			this.EndReceival(true);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000163EC File Offset: 0x000145EC
		public void EndReceival(bool closeReader)
		{
			try
			{
				if (this.xmlaStream != null && this.connected && this.connectionState == ConnectionState.Fetching)
				{
					this.xmlaStream.Skip();
					this.connectionState = ConnectionState.Open;
				}
				object obj = this.lockForCloseAll;
				lock (obj)
				{
					if (this.reader != null)
					{
						if (closeReader)
						{
							this.reader.Close();
						}
						this.reader = null;
					}
				}
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.CloseAll();
				throw;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000164A4 File Offset: 0x000146A4
		public void CancelCommand(string sessionID)
		{
			if (sessionID == null || sessionID.Length == 0)
			{
				throw new ArgumentException(XmlaSR.Cancel_SessionIDNotSpecified);
			}
			this.CheckConnection();
			string text = this.SessionID;
			try
			{
				this.SessionID = sessionID;
				this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				this.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("Cancel", "http://schemas.microsoft.com/analysisservices/2003/engine");
				this.writer.WriteEndElement();
				this.WriteEndCommand(this.ConnectionInfo.ExtendedProperties, dictionary, null);
				this.EndMessage();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			finally
			{
				this.SessionID = text;
			}
			this.SendExecuteAndReadResponse(false, true);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00016584 File Offset: 0x00014784
		public void EndSession(ListDictionary properties)
		{
			if (this.SessionID == null || this.captureXml)
			{
				return;
			}
			IDictionary dictionary = null;
			this.PopulateCommandProperties(ref dictionary, null, false);
			this.StartRequest("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
			try
			{
				this.writer.WriteStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
				this.writer.WriteStartElement("Header");
				this.writer.WriteStartElement("EndSession", "urn:schemas-microsoft-com:xml-analysis");
				this.writer.WriteAttributeString("soap", "mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
				this.writer.WriteAttributeString("SessionId", this.SessionID);
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				this.writer.WriteStartElement("Body");
				this.writer.WriteStartElement("Execute", "urn:schemas-microsoft-com:xml-analysis");
				this.writer.WriteStartElement("Command");
				this.writer.WriteElementString("Statement", string.Empty);
				this.writer.WriteEndElement();
				this.WriteProperties(properties, dictionary);
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(true, true);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00016718 File Offset: 0x00014918
		public void Disconnect(bool endSession)
		{
			try
			{
				if (this.SessionID != null && endSession)
				{
					if (this.connected && this.connInfo != null)
					{
						int num = -1;
						try
						{
							if (this.xmlaStream.CanTimeout)
							{
								num = this.xmlaStream.ReadTimeout;
								this.xmlaStream.ReadTimeout = ClientFeaturesManager.GetEndSessionTimeout();
							}
							this.EndSession(this.connInfo.ExtendedProperties);
						}
						catch (ConnectionException)
						{
						}
						catch (OperationException)
						{
						}
						catch (ResponseFormatException)
						{
						}
						catch (SocketException)
						{
						}
						catch (XmlException)
						{
						}
						catch (IOException)
						{
						}
						catch (Exception)
						{
							throw;
						}
						finally
						{
							if (this.xmlaStream != null && this.xmlaStream.CanTimeout)
							{
								this.xmlaStream.ReadTimeout = num;
							}
						}
					}
					this.SessionID = null;
				}
			}
			finally
			{
				this.userOpened = false;
				this.CloseAll();
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00016844 File Offset: 0x00014A44
		public void CloseAll()
		{
			object obj = this.lockForCloseAll;
			lock (obj)
			{
				this.connected = false;
				this.connectionState = ConnectionState.Closed;
				try
				{
					if (this.tcpClient != null)
					{
						try
						{
							if (this.networkStream != null)
							{
								this.networkStream.Close();
							}
							this.tcpClient.Close();
						}
						catch (SocketException)
						{
						}
						catch (IOException)
						{
						}
						this.networkStream = null;
						this.tcpClient = null;
					}
					if (this.xmlaStream != null)
					{
						this.xmlaStream.Dispose();
						this.xmlaStream = null;
					}
					if (this.writer != null)
					{
						try
						{
							this.writer.Close();
						}
						catch (ObjectDisposedException)
						{
						}
						catch (InvalidOperationException)
						{
						}
						this.writer = null;
					}
					if (this.reader != null)
					{
						try
						{
							XmlaReader xmlaReader = this.reader as XmlaReader;
							if (xmlaReader != null)
							{
								xmlaReader.CloseWithoutEndReceival();
							}
							else
							{
								this.reader.Close();
							}
						}
						catch (InvalidOperationException)
						{
						}
						this.reader = null;
					}
				}
				catch (SocketException)
				{
				}
				catch (XmlException)
				{
				}
				catch (IOException)
				{
				}
				catch (WebException)
				{
				}
				catch (Win32Exception)
				{
				}
				catch (COMException)
				{
				}
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000169D0 File Offset: 0x00014BD0
		internal static bool CheckForSoapFault(XmlReader reader, XmlaResult xmlaResult, bool throwIfError)
		{
			if (!reader.IsStartElement("Fault", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				return false;
			}
			reader.ReadStartElement();
			XmlaClient.ReadFaultBody(reader, xmlaResult.Messages);
			reader.ReadEndElement();
			if (throwIfError)
			{
				throw XmlaResultCollection.ExceptionOnError(xmlaResult);
			}
			return true;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00016A0C File Offset: 0x00014C0C
		internal static bool CheckForException(XmlReader reader, XmlaResult xmlaResult, bool throwIfError)
		{
			if (!reader.IsStartElement("Exception", "urn:schemas-microsoft-com:xml-analysis:exception"))
			{
				return false;
			}
			reader.Skip();
			try
			{
				while (!reader.IsStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/") && !reader.IsStartElement("Messages", "urn:schemas-microsoft-com:xml-analysis:exception"))
				{
					reader.ReadEndElement();
				}
			}
			catch (XmlException ex)
			{
				throw new ResponseFormatException(XmlaSR.AfterExceptionAllTagsShouldCloseUntilMessagesSection, ex);
			}
			if (!reader.IsStartElement("Messages", "urn:schemas-microsoft-com:xml-analysis:exception"))
			{
				throw new ResponseFormatException(XmlaSR.AfterExceptionAllTagsShouldCloseUntilMessagesSection, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis:exception", "Messages", reader.Name));
			}
			if (xmlaResult == null)
			{
				xmlaResult = new XmlaResult();
			}
			XmlaClient.ReadXmlaMessages(reader, xmlaResult.Messages);
			if (!xmlaResult.ContainsErrors)
			{
				throw new ResponseFormatException(XmlaSR.ExceptionRequiresXmlaErrorsInMessagesSection, "No errors in XMLA result");
			}
			if (throwIfError)
			{
				throw XmlaResultCollection.ExceptionOnError(xmlaResult);
			}
			return true;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00016AF8 File Offset: 0x00014CF8
		internal static bool CheckForRowsetError(XmlReader reader, XmlaResult xmlaResult, bool throwIfError)
		{
			return XmlaClient.CheckForInlineError(reader, xmlaResult, throwIfError, "urn:schemas-microsoft-com:xml-analysis:exception");
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00016B07 File Offset: 0x00014D07
		internal static XmlaError CheckAndGetRowsetError(XmlReader reader, bool throwIfError)
		{
			return XmlaClient.CheckAndGetInlineError(reader, throwIfError, "urn:schemas-microsoft-com:xml-analysis:exception");
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00016B15 File Offset: 0x00014D15
		internal static XmlaError CheckAndGetDatasetError(XmlReader reader)
		{
			return XmlaClient.CheckAndGetInlineError(reader, false, "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00016B23 File Offset: 0x00014D23
		internal static bool CheckForMessages(XmlReader reader, ref XmlaMessageCollection xmlaMessages)
		{
			if (reader.IsStartElement("Messages", "urn:schemas-microsoft-com:xml-analysis:exception"))
			{
				if (xmlaMessages == null)
				{
					xmlaMessages = new XmlaMessageCollection();
				}
				XmlaClient.ReadXmlaMessages(reader, xmlaMessages);
				return true;
			}
			return false;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00016B50 File Offset: 0x00014D50
		internal static XmlaResult ReadToXmlaResponse(XmlReader reader)
		{
			new XmlaResultCollection();
			reader.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
			if (reader.IsStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				reader.Skip();
			}
			reader.ReadStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
			XmlaResult xmlaResult = new XmlaResult();
			if (XmlaClient.CheckForSoapFault(reader, xmlaResult, false))
			{
				return xmlaResult;
			}
			return null;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00016BB0 File Offset: 0x00014DB0
		internal static XmlaResultCollection ReadResponse(XmlReader reader, bool skipResults, bool throwIfError)
		{
			XmlaResultCollection xmlaResultCollection = new XmlaResultCollection();
			XmlaResult xmlaResult = new XmlaResult();
			reader.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
			if (reader.IsStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				reader.Skip();
			}
			reader.ReadStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
			if (XmlaClient.CheckForSoapFault(reader, xmlaResult, false))
			{
				xmlaResultCollection.Add(xmlaResult);
			}
			else
			{
				XmlaClient.ReadExecuteResponsePrivate(reader, true, xmlaResultCollection, xmlaResult);
			}
			XmlaClient.CheckEndElement(reader, "Body", "http://schemas.xmlsoap.org/soap/envelope/");
			reader.ReadEndElement();
			XmlaClient.CheckEndElement(reader, "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
			reader.ReadEndElement();
			if (throwIfError && xmlaResultCollection.ContainsErrors)
			{
				throw XmlaResultCollection.ExceptionOnError(xmlaResultCollection);
			}
			return xmlaResultCollection;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00016C5C File Offset: 0x00014E5C
		internal static bool IsMultipleResult(XmlReader reader)
		{
			return reader.IsStartElement("results", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00016C6E File Offset: 0x00014E6E
		internal static bool IsAffectedObjects(XmlReader reader)
		{
			return reader.IsStartElement("AffectedObjects", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00016C80 File Offset: 0x00014E80
		internal static bool IsEmptyResultS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:empty");
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00016C9B File Offset: 0x00014E9B
		internal static bool IsExecuteResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00016CB6 File Offset: 0x00014EB6
		internal static bool IsDiscoverResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00016CD1 File Offset: 0x00014ED1
		internal static bool IsDatasetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00016CEC File Offset: 0x00014EEC
		internal static bool IsRowsetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00016D07 File Offset: 0x00014F07
		internal static bool IsMultipleResultResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("results", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00016D22 File Offset: 0x00014F22
		internal static bool IsAffectedObjectsResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("AffectedObjects", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00016D40 File Offset: 0x00014F40
		internal static void ReadUptoRoot(XmlReader reader)
		{
			if (XmlaClient.IsExecuteResponseS(reader))
			{
				XmlaClient.StartExecuteResponseS(reader);
			}
			else if (XmlaClient.IsDiscoverResponseS(reader))
			{
				XmlaClient.StartDiscoverResponseS(reader);
			}
			else if (!XmlaClient.IsEmptyResultS(reader) && !XmlaClient.IsRootElementS(reader))
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected execute response, discover response, empty result, or root, got {0}", reader.Name));
			}
			if (!reader.EOF)
			{
				reader.MoveToContent();
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00016DAC File Offset: 0x00014FAC
		internal static void ReadEmptyRootS(XmlReader reader)
		{
			XmlaResult xmlaResult = new XmlaResult();
			XmlaClient.ReadEmptyRoot(reader, xmlaResult, true);
			if (xmlaResult.ContainsErrors)
			{
				throw XmlaResultCollection.ExceptionOnError(xmlaResult);
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00016DD6 File Offset: 0x00014FD6
		internal static void StartElementS(XmlReader reader, string element, string xmlNamespace)
		{
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement(element, xmlNamespace);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00016DE9 File Offset: 0x00014FE9
		internal static void EndElementS(XmlReader reader, string element, string xmlNamespace)
		{
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.ReadEndElementS(reader, element, xmlNamespace);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00016DFC File Offset: 0x00014FFC
		internal static void StartExecuteResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("return", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00016E30 File Offset: 0x00015030
		internal static void EndExecuteResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.ReadEndElementS(reader, "return", "urn:schemas-microsoft-com:xml-analysis");
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.ReadEndElementS(reader, "ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00016E64 File Offset: 0x00015064
		internal static void StartDiscoverResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("return", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00016E98 File Offset: 0x00015098
		internal static void EndDiscoverResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.ReadEndElementS(reader, "return", "urn:schemas-microsoft-com:xml-analysis");
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.ReadEndElementS(reader, "DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00016ECC File Offset: 0x000150CC
		internal static void StartDatasetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00016EE7 File Offset: 0x000150E7
		internal static void EndDatasetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.SkipXmlaMessages(reader);
			XmlaClient.ReadEndElementS(reader, "root", "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00016F08 File Offset: 0x00015108
		internal static void StartRowsetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00016F23 File Offset: 0x00015123
		internal static void EndRowsetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.SkipXmlaMessages(reader);
			XmlaClient.ReadEndElementS(reader, "root", "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00016F44 File Offset: 0x00015144
		internal static void ReadEndElementS(XmlReader reader, string name, string ns)
		{
			XmlaClient.CheckForException(reader, null, true);
			if (reader.MoveToContent() != XmlNodeType.EndElement || reader.LocalName != name || reader.NamespaceURI != ns)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", ns, name, reader.Name));
			}
			reader.ReadEndElement();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00016FA8 File Offset: 0x000151A8
		internal static bool IsRootElementS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:empty") || reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset") || reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00016FF4 File Offset: 0x000151F4
		internal void CheckConnection()
		{
			if (this.connected || this.captureXml)
			{
				this.CheckIfReaderDetached();
				return;
			}
			if (this.userOpened)
			{
				throw new ConnectionException(XmlaSR.NotConnected);
			}
			throw new InvalidOperationException(XmlaSR.NotConnected);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001702C File Offset: 0x0001522C
		internal XmlWriter StartRequest(string action)
		{
			this.CheckConnection();
			if (this.writer != null)
			{
				throw new InvalidOperationException(XmlaSR.XmlaClient_StartRequest_ThereIsAnotherPendingRequest);
			}
			if (this.reader != null)
			{
				throw new InvalidOperationException(XmlaSR.XmlaClient_StartRequest_ThereIsAnotherPendingResponse);
			}
			XmlWriter xmlWriter;
			try
			{
				if (this.captureXml)
				{
					this.logEntry = new StringWriter(CultureInfo.InvariantCulture);
					XmlTextWriter xmlTextWriter = (this.writer = new XmlTextWriter(this.logEntry));
					xmlTextWriter.Formatting = Formatting.Indented;
					xmlTextWriter.Indentation = 2;
				}
				else
				{
					this.xmlaStream.WriteSoapActionHeader(action);
					if (this.xmlaStream.GetRequestDataType() == XmlaDataType.BinaryXml)
					{
						throw new NotSupportedException();
					}
					XmlTextWriter xmlTextWriter2 = (this.writer = new XmlTextWriter(this.xmlaStream, this.connInfo.CharacterEncoding));
					xmlTextWriter2.Formatting = Formatting.Indented;
					xmlTextWriter2.Indentation = 2;
				}
				this.connectionState = ConnectionState.Executing;
				xmlWriter = this.writer;
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			return xmlWriter;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00017138 File Offset: 0x00015338
		internal XmlReader EndRequest()
		{
			return this.EndRequest(false);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00017144 File Offset: 0x00015344
		internal XmlReader EndRequest(bool useBinaryXml)
		{
			this.VerifyIfCanWrite(useBinaryXml);
			if (XmlaClient.TRACESWITCH.TraceVerbose)
			{
				Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{XmlaClient}::EndRequest]: ")
					.Append("..  ")
					.Append("useBinaryXml: ")
					.Append(useBinaryXml)
					.Append("; ")
					.ToString());
			}
			try
			{
				object obj = this.lockForCloseAll;
				lock (obj)
				{
					if (this.writer != null)
					{
						this.writer.Flush();
						this.writer.Close();
						this.writer = null;
					}
					if (useBinaryXml)
					{
						this.xmlaStream.Flush();
						this.xmlaStream.Close();
					}
				}
				if (this.captureXml)
				{
					this.captureLog.Add(this.logEntry.ToString());
					this.logEntry.Close();
					this.logEntry = null;
					this.connectionState = ConnectionState.Open;
					return null;
				}
				this.WriteEndOfMessage();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			XmlReader xmlReader2;
			try
			{
				XmlaDataType responseDataType = this.xmlaStream.GetResponseDataType();
				if (XmlaClient.TRACESWITCH.TraceVerbose)
				{
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{XmlaClient}::EndRequest]: ")
						.Append("..  ")
						.Append("useBinaryXml: ")
						.Append(useBinaryXml)
						.Append("; ")
						.Append("xmlaStreamResponseDataType: ")
						.Append(responseDataType)
						.Append("; ")
						.Append("xmlaStream Type: ")
						.Append(this.xmlaStream.GetType().FullName)
						.Append("; ")
						.ToString());
				}
				bool flag2 = false;
				XmlReader xmlReader;
				switch (responseDataType)
				{
				case XmlaDataType.TextXml:
				case XmlaDataType.CompressedXml:
					xmlReader = new XmlTextReader(new StreamReader(this.xmlaStream, Encoding.UTF8, true), this.nameTable)
					{
						DtdProcessing = DtdProcessing.Prohibit,
						WhitespaceHandling = WhitespaceHandling.None
					};
					break;
				case XmlaDataType.BinaryXml:
				case XmlaDataType.CompressedBinaryXml:
					xmlReader = new SqlXml(this.xmlaStream).CreateReader();
					flag2 = !xmlReader.CanReadBinaryContent;
					break;
				default:
					this.CloseAll();
					throw new ConnectionException(XmlaSR.ConnectionBroken);
				}
				this.reader = new XmlaReader(xmlReader, this, this.namespacesManager, flag2);
				this.CheckAndGetHttpStreamSoapFault();
				xmlReader2 = this.reader;
			}
			catch (ConnectionException)
			{
				throw;
			}
			catch (IOException ex2)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch
			{
				this.CloseAll();
				throw;
			}
			return xmlReader2;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00017490 File Offset: 0x00015690
		internal void WriteStartCommand(ref IDictionary commandProperties)
		{
			this.CheckConnection();
			try
			{
				if (!this.captureXml)
				{
					this.PopulateCommandProperties(ref commandProperties, null, true);
					if (commandProperties == null)
					{
						commandProperties = new ListDictionary();
					}
					this.writer.WriteStartElement("Execute", "urn:schemas-microsoft-com:xml-analysis");
					this.writer.WriteStartElement("Command");
				}
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00017520 File Offset: 0x00015720
		internal void WriteEndCommand(IDictionary connectionProperties, IDictionary commandProperties, IDataParameterCollection parameters)
		{
			this.CheckConnection();
			try
			{
				if (!this.captureXml)
				{
					this.writer.WriteEndElement();
					this.WriteProperties(connectionProperties, commandProperties);
					this.WriteParameters(parameters);
					this.writer.WriteEndElement();
				}
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001759C File Offset: 0x0001579C
		internal XmlaResultCollection SendExecuteAndReadResponse(bool skipResults, bool throwIfError)
		{
			return this.SendExecuteAndReadResponse(skipResults, throwIfError, false);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000175A8 File Offset: 0x000157A8
		internal XmlaResultCollection SendExecuteAndReadResponse(bool skipResults, bool throwIfError, bool useBinaryXml)
		{
			if (XmlaClient.TRACESWITCH.TraceVerbose)
			{
				Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{XmlaClient}::SendExecuteAndReadResponse]: ")
					.Append("..  ")
					.Append("useBinaryXml: ")
					.Append(useBinaryXml)
					.Append("; ")
					.Append("skipResults: ")
					.Append(skipResults)
					.Append("; ")
					.Append("throwIfError: ")
					.Append(throwIfError)
					.Append("; ")
					.ToString());
			}
			this.EndRequest(useBinaryXml);
			if (this.captureXml)
			{
				return null;
			}
			XmlaResultCollection xmlaResultCollection2;
			try
			{
				XmlaResultCollection xmlaResultCollection = new XmlaResultCollection();
				XmlaResult xmlaResult = new XmlaResult();
				this.reader.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
				if (this.reader.IsStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/"))
				{
					this.reader.Skip();
				}
				this.reader.ReadStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
				if (XmlaClient.CheckForSoapFault(this.reader, xmlaResult, false))
				{
					xmlaResultCollection.Add(xmlaResult);
				}
				else
				{
					XmlaClient.ReadExecuteResponsePrivate(this.reader, skipResults, xmlaResultCollection, xmlaResult);
				}
				XmlaClient.CheckEndElement(this.reader, "Body", "http://schemas.xmlsoap.org/soap/envelope/");
				this.reader.ReadEndElement();
				XmlaClient.CheckEndElement(this.reader, "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
				this.reader.ReadEndElement();
				if (throwIfError && xmlaResultCollection.ContainsErrors)
				{
					throw XmlaResultCollection.ExceptionOnError(xmlaResultCollection);
				}
				xmlaResultCollection2 = xmlaResultCollection;
			}
			catch (XmlException ex)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch (ResponseFormatException)
			{
				throw;
			}
			catch (OperationException)
			{
				throw;
			}
			catch
			{
				this.CloseAll();
				throw;
			}
			finally
			{
				if (this.connected)
				{
					this.EndReceival(true);
				}
			}
			return xmlaResultCollection2;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001780C File Offset: 0x00015A0C
		internal void HandleMessageCreationException()
		{
			if (this.captureXml)
			{
				this.connectionState = ConnectionState.Closed;
				if (this.writer != null)
				{
					this.writer.Close();
					this.writer = null;
				}
				if (this.logEntry != null)
				{
					this.logEntry.Close();
					this.logEntry = null;
					return;
				}
			}
			else
			{
				this.CloseAll();
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00017863 File Offset: 0x00015A63
		private protected static bool CheckForError(XmlReader reader, XmlaResult xmlaResult, bool throwIfError)
		{
			return XmlaClient.CheckForSoapFault(reader, xmlaResult, throwIfError) || XmlaClient.CheckForException(reader, xmlaResult, throwIfError) || XmlaClient.CheckForRowsetError(reader, xmlaResult, throwIfError) || XmlaClient.CheckForDatasetError(reader, xmlaResult, throwIfError);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001788D File Offset: 0x00015A8D
		private protected static bool CheckForMessages(XmlReader reader, XmlaMessageCollection xmlaMessages)
		{
			return XmlaClient.CheckForMessages(reader, ref xmlaMessages);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00017898 File Offset: 0x00015A98
		private protected static void ReadXmlaMessages(XmlReader reader, XmlaMessageCollection xmlaMessages)
		{
			int num = 0;
			reader.ReadStartElement("Messages", "urn:schemas-microsoft-com:xml-analysis:exception");
			while (reader.IsStartElement())
			{
				if (reader.LocalName == "Error")
				{
					xmlaMessages.Add(XmlaClient.ReadXmlaError(reader));
				}
				else
				{
					if (!(reader.LocalName == "Warning"))
					{
						throw new ResponseFormatException(XmlaSR.UnrecognizedElementInMessagesSection(reader.Name), XmlaSR.UnrecognizedElementInMessagesSection(reader.Name));
					}
					xmlaMessages.Add(XmlaClient.ReadXmlaWarning(reader));
				}
				num++;
			}
			reader.ReadEndElement();
			if (num == 0)
			{
				throw new ResponseFormatException(XmlaSR.MessagesSectionIsEmpty, XmlaSR.MessagesSectionIsEmpty);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001793A File Offset: 0x00015B3A
		private protected static void CheckEndElement(XmlReader reader, string localname)
		{
			reader.MoveToContent();
			if (reader.LocalName != localname)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected end of {0} element, got {1}", localname, reader.Name));
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00017974 File Offset: 0x00015B74
		private protected static void CheckEndElement(XmlReader reader, string localname, string ns)
		{
			reader.MoveToContent();
			if (reader.LocalName != localname || reader.NamespaceURI != ns)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Exected end of {0}:{1} element, got {2}", ns, localname, reader.Name));
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000179C8 File Offset: 0x00015BC8
		private protected virtual void WriteProperties(IDictionary connectionProperties, IDictionary commandProperties)
		{
			this.writer.WriteStartElement("Properties");
			this.writer.WriteStartElement("PropertyList");
			if (connectionProperties != null && connectionProperties.Count > 0)
			{
				foreach (object obj in connectionProperties)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value != null && (commandProperties == null || commandProperties.Count <= 0 || !commandProperties.Contains(dictionaryEntry.Key)))
					{
						this.WriteVersionSafeProperty(dictionaryEntry);
					}
				}
			}
			if (commandProperties != null && commandProperties.Count > 0)
			{
				foreach (object obj2 in commandProperties)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (dictionaryEntry2.Value != null)
					{
						this.WriteVersionSafeProperty(dictionaryEntry2);
					}
				}
			}
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00017ADC File Offset: 0x00015CDC
		private protected virtual void WriteXmlaProperty(DictionaryEntry entry)
		{
			this.writer.WriteElementString((string)entry.Key, FormattersHelpers.ConvertToXml(entry.Value));
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00017B04 File Offset: 0x00015D04
		private protected XmlReader Discover(string requestType, string requestNamespace, ListDictionary properties, IDictionary restrictions, bool sendNamespacesCompatibility, IDictionary requestProperties)
		{
			this.CheckConnection();
			try
			{
				this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Discover\"", false, sendNamespacesCompatibility, false);
				this.WriteStartDiscover(requestType, requestNamespace);
				this.WriteRestrictions(restrictions);
				this.WriteEndDiscover(properties, requestProperties);
				this.EndMessage();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			return this.SendMessage(true, false, sendNamespacesCompatibility);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00017B8C File Offset: 0x00015D8C
		private protected void WriteStartDiscover(string requestType, string requestNamespace)
		{
			this.CheckConnection();
			try
			{
				this.writer.WriteStartElement("Discover", "urn:schemas-microsoft-com:xml-analysis");
				if (requestNamespace == null || requestNamespace.Length == 0)
				{
					this.writer.WriteElementString("RequestType", requestType);
				}
				else
				{
					this.writer.WriteStartElement("RequestType");
					this.writer.WriteAttributeString("xmlns", "rt", null, requestNamespace);
					this.writer.WriteString("rt:" + requestType);
					this.writer.WriteEndElement();
				}
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00017C54 File Offset: 0x00015E54
		private protected void WriteEndDiscover(ListDictionary properties)
		{
			this.WriteEndDiscover(properties, null);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00017C60 File Offset: 0x00015E60
		private protected void WriteEndDiscover(ListDictionary properties, IDictionary requestProperties)
		{
			this.CheckConnection();
			try
			{
				IDictionary dictionary = new ListDictionary();
				foreach (object obj in properties.Keys)
				{
					dictionary.Add(obj, properties[obj]);
				}
				this.PopulateCommandProperties(ref dictionary, requestProperties, false);
				this.WriteProperties(dictionary, requestProperties);
				this.writer.WriteEndElement();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00017D1C File Offset: 0x00015F1C
		private protected XmlReader SendMessage(bool endReceivalIfException, bool readSession, bool readNamespaceCompatibility)
		{
			if (XmlaClient.TRACESWITCH.TraceVerbose)
			{
				StackTrace stackTrace = new StackTrace();
				MethodBase method = stackTrace.GetFrame(1).GetMethod();
				Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append(' ')
					.Append("{XmlaClient}::SendMessage]: ")
					.Append('[')
					.Append('(')
					.Append(stackTrace.FrameCount)
					.Append(')')
					.Append(' ')
					.Append(base.GetType().Equals(method.DeclaringType) ? "." : method.DeclaringType.FullName)
					.Append("::")
					.Append(method.Name)
					.Append("] ")
					.Append("Start")
					.Append("..  ")
					.Append("endReceivalIfException: ")
					.Append(endReceivalIfException)
					.Append(", ")
					.Append("readSession: ")
					.Append(readSession)
					.Append(", ")
					.Append("readNamespaceCompatibility: ")
					.Append(readNamespaceCompatibility)
					.Append("; ")
					.ToString());
			}
			this.EndRequest();
			if (this.captureXml)
			{
				if (XmlaClient.TRACESWITCH.TraceVerbose)
				{
					StackTrace stackTrace2 = new StackTrace();
					MethodBase method2 = stackTrace2.GetFrame(1).GetMethod();
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append(' ')
						.Append("{XmlaClient}::SendMessage]: ")
						.Append('[')
						.Append('(')
						.Append(stackTrace2.FrameCount)
						.Append(')')
						.Append(' ')
						.Append(base.GetType().Equals(method2.DeclaringType) ? "." : method2.DeclaringType.FullName)
						.Append("::")
						.Append(method2.Name)
						.Append("] ")
						.Append("Finish. captureXml is true, return null")
						.Append("..  ")
						.Append("endReceivalIfException: ")
						.Append(endReceivalIfException)
						.Append(", ")
						.Append("readSession: ")
						.Append(readSession)
						.Append(", ")
						.Append("readNamespaceCompatibility: ")
						.Append(readNamespaceCompatibility)
						.Append("; ")
						.ToString());
				}
				return null;
			}
			XmlReader xmlReader;
			try
			{
				this.reader.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
				this.ReadEnvelopeHeader(this.reader, readSession, readNamespaceCompatibility);
				this.reader.ReadStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
				XmlaClient.CheckForError(this.reader, new XmlaResult(), true);
				if (XmlaClient.TRACESWITCH.TraceVerbose)
				{
					StackTrace stackTrace3 = new StackTrace();
					MethodBase method3 = stackTrace3.GetFrame(1).GetMethod();
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append(' ')
						.Append("{XmlaClient}::SendMessage]: ")
						.Append('[')
						.Append('(')
						.Append(stackTrace3.FrameCount)
						.Append(')')
						.Append(' ')
						.Append(base.GetType().Equals(method3.DeclaringType) ? "." : method3.DeclaringType.FullName)
						.Append("::")
						.Append(method3.Name)
						.Append("] ")
						.Append("Finish. Execution succeeded. retValType: ")
						.Append(this.reader.GetType().FullName)
						.Append("..  ")
						.Append("endReceivalIfException: ")
						.Append(endReceivalIfException)
						.Append(", ")
						.Append("readSession: ")
						.Append(readSession)
						.Append(", ")
						.Append("readNamespaceCompatibility: ")
						.Append(readNamespaceCompatibility)
						.Append("; ")
						.ToString());
				}
				xmlReader = this.reader;
			}
			catch (XmlException ex)
			{
				if (endReceivalIfException)
				{
					this.EndReceival(true);
				}
				if (XmlaClient.TRACESWITCH.TraceVerbose)
				{
					StackTrace stackTrace4 = new StackTrace();
					MethodBase method4 = stackTrace4.GetFrame(1).GetMethod();
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append(' ')
						.Append("{XmlaClient}::SendMessage]: ")
						.Append('[')
						.Append('(')
						.Append(stackTrace4.FrameCount)
						.Append(')')
						.Append(' ')
						.Append(base.GetType().Equals(method4.DeclaringType) ? "." : method4.DeclaringType.FullName)
						.Append("::")
						.Append(method4.Name)
						.Append("] ")
						.Append("Finish. Failed with exception: {")
						.Append(ex)
						.Append('}')
						.Append("..  ")
						.Append("endReceivalIfException: ")
						.Append(endReceivalIfException)
						.Append(", ")
						.Append("readSession: ")
						.Append(readSession)
						.Append(", ")
						.Append("readNamespaceCompatibility: ")
						.Append(readNamespaceCompatibility)
						.Append("; ")
						.ToString());
				}
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				this.CloseAll();
				if (XmlaClient.TRACESWITCH.TraceVerbose)
				{
					StackTrace stackTrace5 = new StackTrace();
					MethodBase method5 = stackTrace5.GetFrame(1).GetMethod();
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append(' ')
						.Append("{XmlaClient}::SendMessage]: ")
						.Append('[')
						.Append('(')
						.Append(stackTrace5.FrameCount)
						.Append(')')
						.Append(' ')
						.Append(base.GetType().Equals(method5.DeclaringType) ? "." : method5.DeclaringType.FullName)
						.Append("::")
						.Append(method5.Name)
						.Append("] ")
						.Append("Finish. Failed with exception: {")
						.Append(ex2)
						.Append('}')
						.Append("..  ")
						.Append("endReceivalIfException: ")
						.Append(endReceivalIfException)
						.Append(", ")
						.Append("readSession: ")
						.Append(readSession)
						.Append(", ")
						.Append("readNamespaceCompatibility: ")
						.Append(readNamespaceCompatibility)
						.Append("; ")
						.ToString());
				}
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch (ResponseFormatException ex3)
			{
				if (endReceivalIfException)
				{
					this.EndReceival(true);
				}
				if (XmlaClient.TRACESWITCH.TraceVerbose)
				{
					StackTrace stackTrace6 = new StackTrace();
					MethodBase method6 = stackTrace6.GetFrame(1).GetMethod();
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append(' ')
						.Append("{XmlaClient}::SendMessage]: ")
						.Append('[')
						.Append('(')
						.Append(stackTrace6.FrameCount)
						.Append(')')
						.Append(' ')
						.Append(base.GetType().Equals(method6.DeclaringType) ? "." : method6.DeclaringType.FullName)
						.Append("::")
						.Append(method6.Name)
						.Append("] ")
						.Append("Finish. Failed with exception: {")
						.Append(ex3)
						.Append('}')
						.Append("..  ")
						.Append("endReceivalIfException: ")
						.Append(endReceivalIfException)
						.Append(", ")
						.Append("readSession: ")
						.Append(readSession)
						.Append(", ")
						.Append("readNamespaceCompatibility: ")
						.Append(readNamespaceCompatibility)
						.Append("; ")
						.ToString());
				}
				throw;
			}
			catch (OperationException ex4)
			{
				if (endReceivalIfException)
				{
					this.EndReceival(true);
				}
				if (XmlaClient.TRACESWITCH.TraceVerbose)
				{
					StackTrace stackTrace7 = new StackTrace();
					MethodBase method7 = stackTrace7.GetFrame(1).GetMethod();
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append(' ')
						.Append("{XmlaClient}::SendMessage]: ")
						.Append('[')
						.Append('(')
						.Append(stackTrace7.FrameCount)
						.Append(')')
						.Append(' ')
						.Append(base.GetType().Equals(method7.DeclaringType) ? "." : method7.DeclaringType.FullName)
						.Append("::")
						.Append(method7.Name)
						.Append("] ")
						.Append("Finish. Failed with exception: {")
						.Append(ex4)
						.Append('}')
						.Append("..  ")
						.Append("endReceivalIfException: ")
						.Append(endReceivalIfException)
						.Append(", ")
						.Append("readSession: ")
						.Append(readSession)
						.Append(", ")
						.Append("readNamespaceCompatibility: ")
						.Append(readNamespaceCompatibility)
						.Append("; ")
						.ToString());
				}
				throw;
			}
			catch (Exception ex5)
			{
				this.CloseAll();
				if (XmlaClient.TRACESWITCH.TraceVerbose)
				{
					StackTrace stackTrace8 = new StackTrace();
					MethodBase method8 = stackTrace8.GetFrame(1).GetMethod();
					Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append(' ')
						.Append("{XmlaClient}::SendMessage]: ")
						.Append('[')
						.Append('(')
						.Append(stackTrace8.FrameCount)
						.Append(')')
						.Append(' ')
						.Append(base.GetType().Equals(method8.DeclaringType) ? "." : method8.DeclaringType.FullName)
						.Append("::")
						.Append(method8.Name)
						.Append("] ")
						.Append("Finish. Failed with exception: {")
						.Append(ex5)
						.Append('}')
						.Append("..  ")
						.Append("endReceivalIfException: ")
						.Append(endReceivalIfException)
						.Append(", ")
						.Append("readSession: ")
						.Append(readSession)
						.Append(", ")
						.Append("readNamespaceCompatibility: ")
						.Append(readNamespaceCompatibility)
						.Append("; ")
						.ToString());
				}
				throw;
			}
			return xmlReader;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001885C File Offset: 0x00016A5C
		private static bool CheckForDatasetError(XmlReader reader, XmlaResult xmlaResult, bool throwIfError)
		{
			return XmlaClient.CheckForInlineError(reader, xmlaResult, throwIfError, "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001886C File Offset: 0x00016A6C
		private static bool CheckForInlineError(XmlReader reader, XmlaResult xmlaResult, bool throwIfError, string errorNamespace)
		{
			if (!reader.IsStartElement("Error", errorNamespace))
			{
				return false;
			}
			XmlaError xmlaError = XmlaClient.ReadInlineError(reader, errorNamespace);
			xmlaResult.Messages.Add(xmlaError);
			if (throwIfError)
			{
				throw XmlaResultCollection.ExceptionOnError(xmlaResult);
			}
			return true;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000188A8 File Offset: 0x00016AA8
		private static XmlaError CheckAndGetInlineError(XmlReader reader, bool throwIfError, string errorNamespace)
		{
			if (!reader.IsStartElement("Error", errorNamespace))
			{
				return null;
			}
			XmlaError xmlaError = XmlaClient.ReadInlineError(reader, errorNamespace);
			if (throwIfError)
			{
				throw XmlaResultCollection.ExceptionOnError(new XmlaResult
				{
					Messages = { xmlaError }
				});
			}
			return xmlaError;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000188E8 File Offset: 0x00016AE8
		private static XmlaError ReadInlineError(XmlReader reader, string errorNamespace)
		{
			if (reader.IsEmptyElement)
			{
				throw new ResponseFormatException(XmlaSR.ErrorCodeIsMissingFromDatasetError, "Empty Error element");
			}
			reader.ReadStartElement();
			string text = null;
			string text2 = null;
			string text3 = null;
			int num = 0;
			bool flag = false;
			XmlaMessageLocation xmlaMessageLocation = null;
			while (reader.IsStartElement())
			{
				if ("ErrorCode".Equals(reader.LocalName) && errorNamespace.Equals(reader.NamespaceURI))
				{
					text = reader.ReadElementString();
				}
				else if ("Description".Equals(reader.LocalName) && errorNamespace.Equals(reader.NamespaceURI))
				{
					text2 = reader.ReadElementString();
					XmlaReader xmlaReader = reader as XmlaReader;
					if (!xmlaReader.HasExtendedErrorInfoBeenRead)
					{
						text2 += xmlaReader.GetExtendedErrorInfo();
					}
				}
				else if ("Location".Equals(reader.LocalName) && "http://schemas.microsoft.com/analysisservices/2003/engine".Equals(reader.NamespaceURI))
				{
					xmlaMessageLocation = XmlaClient.ReadXmlaMessageLocation(reader);
				}
				else if ("CallStack".Equals(reader.LocalName) && "http://schemas.microsoft.com/analysisservices/2011/engine/300".Equals(reader.NamespaceURI))
				{
					text3 = reader.ReadElementString();
				}
				else if ("errortype".Equals(reader.LocalName) && "http://schemas.microsoft.com/analysisservices/2018/engine/800".Equals(reader.NamespaceURI))
				{
					num = XmlConvert.ToInt32(reader.ReadElementContentAsString());
				}
				else if ("isprimary".Equals(reader.LocalName) && "http://schemas.microsoft.com/analysisservices/2018/engine/800".Equals(reader.NamespaceURI))
				{
					flag = XmlConvert.ToBoolean(reader.ReadElementContentAsString());
				}
				else
				{
					reader.Skip();
				}
			}
			reader.ReadEndElement();
			if (text == null)
			{
				throw new ResponseFormatException(XmlaSR.ErrorCodeIsMissingFromDatasetError, "Missing error code");
			}
			return new XmlaError((int)XmlConvert.ToUInt32(text), text2, null, null, xmlaMessageLocation, text3, num, flag);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00018AA4 File Offset: 0x00016CA4
		private static XmlaMessageLocation ReadXmlaMessageLocation(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				return null;
			}
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			XmlaLocationReference xmlaLocationReference = null;
			XmlaLocationReference xmlaLocationReference2 = null;
			reader.ReadStartElement();
			XmlaClient.ReadPosition(reader, "Start", ref num, ref num3);
			XmlaClient.ReadPosition(reader, "End", ref num2, ref num4);
			int num5 = XmlaClient.ReadIntElementIfAny(reader, "LineOffset", "http://schemas.microsoft.com/analysisservices/2003/engine");
			int num6 = XmlaClient.ReadIntElementIfAny(reader, "TextLength", "http://schemas.microsoft.com/analysisservices/2003/engine");
			if (reader.IsStartElement("SourceObject", "http://schemas.microsoft.com/analysisservices/2010/engine/200"))
			{
				xmlaLocationReference = XmlaClient.ReadXmlaLocationReference(reader);
			}
			if (reader.IsStartElement("DependsOnObject", "http://schemas.microsoft.com/analysisservices/2010/engine/200"))
			{
				xmlaLocationReference2 = XmlaClient.ReadXmlaLocationReference(reader);
			}
			long num7 = XmlaClient.ReadLongElementIfAny(reader, "RowNumber", "http://schemas.microsoft.com/analysisservices/2010/engine/200");
			reader.ReadEndElement();
			return new XmlaMessageLocation(num, num3, num2, num4, num5, num6, xmlaLocationReference, xmlaLocationReference2, num7);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00018B7C File Offset: 0x00016D7C
		private static XmlaLocationReference ReadXmlaLocationReference(XmlReader reader)
		{
			reader.ReadStartElement();
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			string text7 = null;
			string text8 = null;
			string text9 = null;
			string text10 = null;
			string text11 = null;
			string text12 = null;
			string text13 = null;
			bool flag;
			do
			{
				flag = false;
				if (reader.IsStartElement("Dimension", "http://schemas.microsoft.com/analysisservices/2010/engine/200"))
				{
					text = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("Hierarchy", "http://schemas.microsoft.com/analysisservices/2011/engine/300"))
				{
					text2 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("Attribute", "http://schemas.microsoft.com/analysisservices/2010/engine/200"))
				{
					text3 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("Cube", "http://schemas.microsoft.com/analysisservices/2010/engine/200"))
				{
					text4 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("MeasureGroup", "http://schemas.microsoft.com/analysisservices/2010/engine/200"))
				{
					text5 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("MemberName", "http://schemas.microsoft.com/analysisservices/2010/engine/200"))
				{
					text6 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("Role", "http://schemas.microsoft.com/analysisservices/2011/engine/300/300"))
				{
					text7 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("RoleName", "http://schemas.microsoft.com/analysisservices/2013/engine/500/500"))
				{
					text13 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("TableName", "http://schemas.microsoft.com/analysisservices/2013/engine/500/500"))
				{
					text8 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("ColumnName", "http://schemas.microsoft.com/analysisservices/2013/engine/500/500"))
				{
					text9 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("PartitionName", "http://schemas.microsoft.com/analysisservices/2013/engine/500/500"))
				{
					text10 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("MeasureName", "http://schemas.microsoft.com/analysisservices/2013/engine/500/500"))
				{
					text11 = reader.ReadElementString();
					flag = true;
				}
				else if (reader.IsStartElement("CalculationItemName", "http://schemas.microsoft.com/analysisservices/2013/engine/500/500"))
				{
					text12 = reader.ReadElementString();
					flag = true;
				}
			}
			while (flag);
			reader.ReadEndElement();
			return new XmlaLocationReference(text, text2, text3, text4, text5, text6, text7, text8, text9, text10, text11, text12, text13);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00018D84 File Offset: 0x00016F84
		private static void ReadPosition(XmlReader reader, string positionName, ref int line, ref int column)
		{
			if (reader.IsStartElement(positionName, "http://schemas.microsoft.com/analysisservices/2003/engine"))
			{
				if (reader.IsEmptyElement)
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Empty {0} element", positionName));
				}
				reader.ReadStartElement();
				line = XmlaClient.ReadIntElementIfAny(reader, "Line", "http://schemas.microsoft.com/analysisservices/2003/engine");
				column = XmlaClient.ReadIntElementIfAny(reader, "Column", "http://schemas.microsoft.com/analysisservices/2003/engine");
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00018DF2 File Offset: 0x00016FF2
		private static int ReadIntElementIfAny(XmlReader reader, string elementName, string elementNamespace)
		{
			if (!reader.IsStartElement(elementName, elementNamespace))
			{
				return -1;
			}
			if (reader.IsEmptyElement)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Empty {0}:{1} element", elementNamespace, elementName));
			}
			return XmlConvert.ToInt32(reader.ReadElementString(elementName, elementNamespace));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00018E31 File Offset: 0x00017031
		private static long ReadLongElementIfAny(XmlReader reader, string elementName, string elementNamespace)
		{
			if (!reader.IsStartElement(elementName, elementNamespace))
			{
				return -1L;
			}
			if (reader.IsEmptyElement)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected non-empty {0}:{1} element, but it is empty", elementNamespace, elementName));
			}
			return XmlConvert.ToInt64(reader.ReadElementString(elementName, elementNamespace));
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00018E74 File Offset: 0x00017074
		private static void ReadFaultBody(XmlReader reader, XmlaMessageCollection xmlaMessages)
		{
			string text = null;
			while (!XmlaClient.IsStartDetailElement(reader) && reader.NodeType != XmlNodeType.EndElement)
			{
				if (reader.IsStartElement("faultstring"))
				{
					text = reader.ReadElementString();
				}
				else
				{
					reader.Skip();
				}
			}
			bool flag = false;
			if (XmlaClient.IsStartDetailElement(reader) && !reader.IsEmptyElement)
			{
				reader.ReadStartElement();
				while (reader.IsStartElement())
				{
					if (reader.LocalName == "Error")
					{
						flag = true;
						xmlaMessages.Add(XmlaClient.ReadXmlaError(reader));
					}
					else if (reader.LocalName == "Warning")
					{
						xmlaMessages.Add(XmlaClient.ReadXmlaWarning(reader));
					}
					else
					{
						reader.Skip();
					}
				}
				XmlaClient.CheckEndElement(reader, "detail");
				if (reader.NamespaceURI != "" && reader.NamespaceURI != "http://schemas.xmlsoap.org/soap/envelope/")
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Got {0}", reader.Name));
				}
				reader.ReadEndElement();
			}
			while (reader.IsStartElement())
			{
				reader.Skip();
			}
			if (!flag)
			{
				if (text == null)
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, "Missing fault string");
				}
				xmlaMessages.Add(new XmlaError(0, text, null, null, null));
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00018FB0 File Offset: 0x000171B0
		private static bool IsStartDetailElement(XmlReader reader)
		{
			return reader.IsStartElement() && reader.LocalName == "detail" && (reader.NamespaceURI == "" || reader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/");
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00019000 File Offset: 0x00017200
		private static XmlaError ReadXmlaError(XmlReader reader)
		{
			int num = (int)XmlConvert.ToUInt32(reader.GetAttribute("ErrorCode"));
			string attribute = reader.GetAttribute("Source");
			string attribute2 = reader.GetAttribute("HelpFile");
			string text = null;
			int num2 = 0;
			bool flag = false;
			XmlaMessageLocation xmlaMessageLocation = null;
			string text2 = reader.GetAttribute("Description");
			XmlaReader xmlaReader = reader as XmlaReader;
			if (!xmlaReader.HasExtendedErrorInfoBeenRead)
			{
				text2 += xmlaReader.GetExtendedErrorInfo();
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
			}
			else
			{
				reader.ReadStartElement();
				bool flag2 = true;
				if ("Location".Equals(reader.LocalName) && "http://schemas.microsoft.com/analysisservices/2003/engine".Equals(reader.NamespaceURI))
				{
					xmlaMessageLocation = XmlaClient.ReadXmlaMessageLocation(reader);
					flag2 = false;
				}
				if ("CallStack".Equals(reader.LocalName) && "http://schemas.microsoft.com/analysisservices/2011/engine/300".Equals(reader.NamespaceURI))
				{
					text = reader.ReadElementContentAsString();
					flag2 = false;
				}
				if ("errortype".Equals(reader.LocalName) && "http://schemas.microsoft.com/analysisservices/2018/engine/800".Equals(reader.NamespaceURI))
				{
					num2 = XmlConvert.ToInt32(reader.ReadElementContentAsString());
					flag2 = false;
				}
				if ("isprimary".Equals(reader.LocalName) && "http://schemas.microsoft.com/analysisservices/2018/engine/800".Equals(reader.NamespaceURI))
				{
					flag = XmlConvert.ToBoolean(reader.ReadElementContentAsString());
					flag2 = false;
				}
				if (flag2)
				{
					reader.Skip();
				}
				reader.ReadEndElement();
			}
			return new XmlaError(num, text2, attribute, attribute2, xmlaMessageLocation, text, num2, flag);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001916C File Offset: 0x0001736C
		private static XmlaWarning ReadXmlaWarning(XmlReader reader)
		{
			int num = (int)XmlConvert.ToUInt32(reader.GetAttribute("WarningCode"));
			string attribute = reader.GetAttribute("Description");
			string attribute2 = reader.GetAttribute("Source");
			string attribute3 = reader.GetAttribute("HelpFile");
			XmlaMessageLocation xmlaMessageLocation = null;
			if (reader.IsEmptyElement)
			{
				reader.Skip();
			}
			else
			{
				reader.ReadStartElement();
				if (reader.IsStartElement("Location", "http://schemas.microsoft.com/analysisservices/2003/engine"))
				{
					xmlaMessageLocation = XmlaClient.ReadXmlaMessageLocation(reader);
				}
				else
				{
					reader.Skip();
				}
				reader.ReadEndElement();
			}
			return new XmlaWarning(num, attribute, attribute2, attribute3, xmlaMessageLocation);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000191F8 File Offset: 0x000173F8
		private void CheckAndGetHttpStreamSoapFault()
		{
			HttpStream httpStream = this.xmlaStream as HttpStream;
			if (httpStream == null || httpStream.StreamException == null)
			{
				return;
			}
			if (!this.reader.IsStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				throw new ConnectionException(XmlaSR.ConnectionBroken, httpStream.StreamException);
			}
			this.reader.ReadStartElement();
			if (this.reader.IsStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				this.reader.Skip();
			}
			if (!this.reader.IsStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				throw new ConnectionException(XmlaSR.ConnectionBroken, httpStream.StreamException);
			}
			this.reader.ReadStartElement();
			try
			{
				if (!XmlaClient.CheckForSoapFault(this.reader, new XmlaResult(), true))
				{
					throw new ConnectionException(XmlaSR.ConnectionBroken, httpStream.StreamException);
				}
			}
			catch (OperationException)
			{
				throw;
			}
			catch (XmlException ex)
			{
				throw new ResponseFormatException(ex);
			}
			throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, httpStream.StreamException.InnerException);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00019308 File Offset: 0x00017508
		private static void SkipXmlaMessages(XmlReader reader)
		{
			if (reader.IsStartElement("Messages", "urn:schemas-microsoft-com:xml-analysis:exception"))
			{
				XmlaMessageCollection xmlaMessageCollection = new XmlaMessageCollection();
				XmlaClient.ReadXmlaMessages(reader, xmlaMessageCollection);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00019334 File Offset: 0x00017534
		private static bool IsCompressionDesired(ConnectionInfo connectionInfo)
		{
			TransportCompression transportCompression = connectionInfo.TransportCompression;
			return (transportCompression == TransportCompression.Default || transportCompression == TransportCompression.Compressed) && CompressedStream.IsCompressionAvailable;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00019358 File Offset: 0x00017558
		internal static bool IsBinaryDesired(ConnectionInfo connectionInfo)
		{
			bool flag;
			return XmlaClient.hasRuntimeSupportForBinaryXml && ClientFeaturesManager.CheckIfBinaryXmlaIsEnabled(out flag) && (flag || (connectionInfo.IsBinarySupported && (connectionInfo.ProtocolFormat == ProtocolFormat.Default || connectionInfo.ProtocolFormat == ProtocolFormat.Binary)));
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001939B File Offset: 0x0001759B
		private static XmlaDataType GetDesiredRequestType(ConnectionInfo connectionInfo)
		{
			if (XmlaClient.IsCompressionDesired(connectionInfo))
			{
				return XmlaDataType.CompressedXml;
			}
			return XmlaDataType.TextXml;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000193A8 File Offset: 0x000175A8
		private static XmlaDataType GetDesiredResponseType(ConnectionInfo connectionInfo)
		{
			bool flag = XmlaClient.IsCompressionDesired(connectionInfo);
			bool flag2 = XmlaClient.IsBinaryDesired(connectionInfo);
			if (flag && flag2)
			{
				return XmlaDataType.CompressedBinaryXml;
			}
			if (flag)
			{
				return XmlaDataType.CompressedXml;
			}
			if (flag2)
			{
				return XmlaDataType.BinaryXml;
			}
			return XmlaDataType.TextXml;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000193D8 File Offset: 0x000175D8
		private static bool HasRuntimeSupportForBinaryXmlImpl()
		{
			Version version;
			return !FrameworkRuntimeHelper.IsNetCoreDomain || (FrameworkRuntimeHelper.TryGetRuntimeVersion(out version) && (version.Major != 6 || !(version < new Version("6.0.15"))) && (version.Major != 7 || !(version < new Version("7.0.4"))));
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00019430 File Offset: 0x00017630
		private static void ReadExecuteResponsePrivate(XmlReader reader, bool skipResults, XmlaResultCollection results, XmlaResult xmlaResult)
		{
			reader.ReadStartElement("ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
			reader.ReadStartElement("return", "urn:schemas-microsoft-com:xml-analysis");
			if (reader.IsStartElement("results", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults"))
			{
				XmlaClient.ReadMultipleResults(reader, results, skipResults);
			}
			else
			{
				XmlaClient.ReadRoot(reader, xmlaResult, skipResults);
				results.Add(xmlaResult);
			}
			XmlaClient.CheckEndElement(reader, "return", "urn:schemas-microsoft-com:xml-analysis");
			reader.ReadEndElement();
			XmlaClient.CheckEndElement(reader, "ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
			reader.ReadEndElement();
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000194B4 File Offset: 0x000176B4
		private static void ReadMultipleResults(XmlReader reader, XmlaResultCollection results, bool skipResults)
		{
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			string text = (XmlaClient.IsAffectedObjects(reader) ? "AffectedObjects" : "results");
			reader.ReadStartElement(text, "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
			while (reader.IsStartElement())
			{
				XmlaResult xmlaResult = new XmlaResult();
				XmlaClient.ReadRoot(reader, xmlaResult, skipResults);
				results.Add(xmlaResult);
			}
			XmlaClient.CheckEndElement(reader, text, "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
			reader.ReadEndElement();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00019524 File Offset: 0x00017724
		private static void ReadRoot(XmlReader reader, XmlaResult xmlaResult, bool skipResults)
		{
			if (reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:empty"))
			{
				XmlaClient.ReadEmptyRoot(reader, xmlaResult, skipResults);
				return;
			}
			if (reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset"))
			{
				XmlaClient.ReadRowsetRoot(reader, xmlaResult, skipResults);
				return;
			}
			if (reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:mddataset"))
			{
				XmlaClient.ReadDatasetRoot(reader, xmlaResult, skipResults);
				return;
			}
			throw new ResponseFormatException(XmlaSR.UnexpectedElement(reader.LocalName, reader.NamespaceURI), "Expected root element");
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000195A0 File Offset: 0x000177A0
		private static void ReadEmptyRoot(XmlReader reader, XmlaResult xmlaResult, bool skipResults)
		{
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				if (!skipResults)
				{
					xmlaResult.SetValue(string.Empty);
					return;
				}
			}
			else
			{
				reader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:empty");
				XmlaClient.CheckAndSkipXsdSchema(reader);
				if (!XmlaClient.CheckForException(reader, xmlaResult, false))
				{
					XmlaClient.CheckForMessages(reader, xmlaResult.Messages);
				}
				if (!XmlaClient.IsEndElement(reader, "root", "urn:schemas-microsoft-com:xml-analysis:empty"))
				{
					throw new ResponseFormatException(XmlaSR.EmptyRootIsNotEmpty, string.Format(CultureInfo.InvariantCulture, "Expected end of {0}:{1} element, got {2}", "urn:schemas-microsoft-com:xml-analysis:empty", "root", reader.Name));
				}
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001963A File Offset: 0x0001783A
		private static bool IsEndElement(XmlReader reader, string localName, string ns)
		{
			reader.MoveToContent();
			return reader.NodeType == XmlNodeType.EndElement && reader.LocalName == localName && reader.NamespaceURI == ns;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00019669 File Offset: 0x00017869
		private static bool CheckAndSkipXsdSchema(XmlReader reader)
		{
			if (reader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
			{
				reader.Skip();
				return true;
			}
			return false;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00019686 File Offset: 0x00017886
		private static void ReadRowsetRoot(XmlReader reader, XmlaResult xmlaResult, bool skipResults)
		{
			if (skipResults)
			{
				reader.Skip();
				return;
			}
			xmlaResult.SetValue(reader.ReadInnerXml());
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001969E File Offset: 0x0001789E
		private static void ReadDatasetRoot(XmlReader reader, XmlaResult xmlaResult, bool skipResults)
		{
			if (skipResults)
			{
				reader.Skip();
				return;
			}
			xmlaResult.SetValue(reader.ReadInnerXml());
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000196B8 File Offset: 0x000178B8
		private static void CheckAndSkipEmptyElement(XmlReader reader, string localname, string ns)
		{
			if (!reader.IsStartElement(localname, ns))
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1} element, got {2}", ns, localname, reader.Name));
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			if (!XmlaClient.IsEndElement(reader, localname, ns))
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected end of {0}:{1} element, got {2}", ns, localname, reader.Name));
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00019738 File Offset: 0x00017938
		private static TcpClient GetTcpClient(IConnectivityOwner owner, ConnectionInfo connectionInfo)
		{
			int instancePort = XmlaClient.GetInstancePort(owner, connectionInfo);
			string text = connectionInfo.Location;
			if (text == null)
			{
				text = (connectionInfo.IsServerLocal ? "localhost" : connectionInfo.Server);
			}
			try
			{
				if (string.Compare(text, Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(NetworkHelper.GetComputerName(ComputerNameFormat.ComputerNameNetBIOS), NetworkHelper.GetComputerName(ComputerNameFormat.ComputerNamePhysicalNetBIOS)) == 0)
				{
					text = "localhost";
				}
			}
			catch (Win32Exception ex)
			{
				throw new ConnectionException(XmlaSR.CannotConnect, ex);
			}
			TcpClient tcpClient = null;
			try
			{
				IPAddress ipaddress;
				if (IPAddress.TryParse(text, out ipaddress))
				{
					tcpClient = new TcpClient(ipaddress.AddressFamily);
					tcpClient.Connect(ipaddress, instancePort);
				}
				else
				{
					tcpClient = new TcpClient(text, instancePort);
				}
			}
			catch (ArgumentNullException ex2)
			{
				throw new ConnectionException(XmlaSR.ConnectionString_Invalid, ex2);
			}
			catch (ArgumentOutOfRangeException ex3)
			{
				throw new ConnectionException(XmlaSR.ConnectionString_Invalid, ex3);
			}
			catch (ArgumentException ex4)
			{
				throw new ConnectionException(connectionInfo.IsForSqlBrowser ? XmlaSR.CannotConnectToRedirector : XmlaSR.CannotConnect, ex4);
			}
			catch (SocketException ex5)
			{
				throw new ConnectionException(connectionInfo.IsForSqlBrowser ? XmlaSR.CannotConnectToRedirector : XmlaSR.CannotConnect, ex5);
			}
			tcpClient.NoDelay = true;
			int tcpStreamBufferSize = ClientFeaturesManager.GetTcpStreamBufferSize();
			tcpClient.ReceiveBufferSize = tcpStreamBufferSize;
			tcpClient.SendBufferSize = tcpStreamBufferSize;
			return tcpClient;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001988C File Offset: 0x00017A8C
		private static int GetInstancePort(IConnectivityOwner owner, ConnectionInfo connectionInfo)
		{
			if (connectionInfo.InstanceName == null)
			{
				if (connectionInfo.Port == null)
				{
					return 2383;
				}
				try
				{
					return int.Parse(connectionInfo.Port, CultureInfo.InvariantCulture);
				}
				catch (FormatException ex)
				{
					throw new ConnectionException(XmlaSR.ConnectionString_Invalid, ex);
				}
				catch (OverflowException ex2)
				{
					throw new ConnectionException(XmlaSR.ConnectionString_Invalid, ex2);
				}
			}
			ConnectionInfo connectionInfo2 = connectionInfo.CloneForInstanceLookup();
			XmlaClient xmlaClient = new XmlaClient(owner);
			xmlaClient.Connect(connectionInfo2, false);
			try
			{
				ListDictionary listDictionary = new ListDictionary();
				listDictionary.Add("INSTANCE_NAME", connectionInfo.InstanceName);
				XmlReader xmlReader = xmlaClient.Discover("DISCOVER_INSTANCES", null, connectionInfo2.ExtendedProperties, listDictionary, false, null);
				xmlReader.ReadStartElement("DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
				xmlReader.ReadStartElement("return");
				xmlReader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
				if (!xmlReader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1} element, got {2}", "http://www.w3.org/2001/XMLSchema", "schema", xmlReader.Name));
				}
				xmlReader.Skip();
				if (xmlReader.IsStartElement("row", "urn:schemas-microsoft-com:xml-analysis:rowset"))
				{
					xmlReader.ReadStartElement();
					while (xmlReader.IsStartElement())
					{
						if (xmlReader.IsStartElement("INSTANCE_PORT_NUMBER", "urn:schemas-microsoft-com:xml-analysis:rowset"))
						{
							int num;
							if (int.TryParse(xmlReader.ReadElementString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
							{
								return num;
							}
							break;
						}
						else
						{
							xmlReader.Skip();
						}
					}
				}
				throw new ConnectionException(XmlaSR.Instance_NotFound(connectionInfo.InstanceName, connectionInfo.Server));
			}
			finally
			{
				xmlaClient.Disconnect(false);
			}
			int num2;
			return num2;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00019A64 File Offset: 0x00017C64
		private static string CalculateNTAuthenticationSPN(ConnectionInfo connectionInfo)
		{
			if (connectionInfo.IsSchannelSspi())
			{
				return connectionInfo.Server;
			}
			if (connectionInfo.InstanceName != null)
			{
				string server = connectionInfo.Server;
				if (string.Compare(server, "localhost", StringComparison.OrdinalIgnoreCase) == 0 || connectionInfo.IsServerLocal || string.Compare(server, Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return null;
				}
				return string.Format("{0}/{1}:{2}", "MSOLAPSvc.3", server, connectionInfo.InstanceName);
			}
			else
			{
				string text = (connectionInfo.IsServerLocal ? Environment.MachineName : connectionInfo.Server);
				string text2;
				ushort num;
				if (connectionInfo.IsForSqlBrowser)
				{
					text2 = "MSOLAPDisco.3";
					num = 0;
				}
				else
				{
					text2 = "MSOLAPSvc.3";
					num = (string.IsNullOrEmpty(connectionInfo.Port) ? 0 : ushort.Parse(connectionInfo.Port, CultureInfo.InvariantCulture));
				}
				uint num2 = 0U;
				int num3 = NativeMethods.DsMakeSpn(text2, text, null, num, null, ref num2, null);
				if (num3 == 87)
				{
					return string.Empty;
				}
				if (num3 != 111)
				{
					throw new Win32Exception(num3);
				}
				StringBuilder stringBuilder = new StringBuilder((int)num2, (int)num2);
				num3 = NativeMethods.DsMakeSpn(text2, text, null, num, null, ref num2, stringBuilder);
				if (num3 != 0)
				{
					throw new Win32Exception(num3);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00019B78 File Offset: 0x00017D78
		private static NTAuthenticationConfiguration BuildNTAuthenticationConfiguration(ConnectionInfo connectionInfo)
		{
			NTAuthenticationConfiguration ntauthenticationConfiguration = new NTAuthenticationConfiguration(connectionInfo.IsSchannelSspi(), connectionInfo.ImpersonationLevel, connectionInfo.ProtectionLevel, connectionInfo.Certificate);
			if (ntauthenticationConfiguration.IsSChannel)
			{
				switch (ntauthenticationConfiguration.ImpersonationLevel)
				{
				case ImpersonationLevel.Anonymous:
					if (!string.IsNullOrEmpty(ntauthenticationConfiguration.CertificateThumbprint))
					{
						throw new ConnectionException(XmlaSR.Authentication_Sspi_SchannelAnonymousAmbiguity, null, ConnectionExceptionCause.AuthenticationFailed);
					}
					break;
				case ImpersonationLevel.Identify:
				case ImpersonationLevel.Impersonate:
					break;
				case ImpersonationLevel.Delegate:
					throw new ConnectionException(XmlaSR.Authentication_Sspi_SchannelCantDelegate, null, ConnectionExceptionCause.AuthenticationFailed);
				default:
					throw new ConnectionException(XmlaSR.Authentication_Sspi_SchannelUnsupportedImpersonationLevel, null, ConnectionExceptionCause.AuthenticationFailed);
				}
				ProtectionLevel protectionLevel = ntauthenticationConfiguration.ProtectionLevel;
				if (protectionLevel <= ProtectionLevel.Integrity)
				{
					throw new ConnectionException(XmlaSR.Authentication_Sspi_SchannelSupportsOnlyPrivacyLevel, null, ConnectionExceptionCause.AuthenticationFailed);
				}
				if (protectionLevel != ProtectionLevel.Privacy)
				{
					throw new ConnectionException(XmlaSR.Authentication_Sspi_SchannelUnsupportedProtectionLevel, null, ConnectionExceptionCause.AuthenticationFailed);
				}
			}
			return ntauthenticationConfiguration;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00019C32 File Offset: 0x00017E32
		private static string EscapeXMLString(string xmlString)
		{
			return SecurityElement.Escape(xmlString);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00019C3C File Offset: 0x00017E3C
		private void OpenConnection(ConnectionInfo connectionInfo, out bool isSessionTokenNeeded)
		{
			using (UserContext userContext = IdentityResolver.Resolve(connectionInfo))
			{
				isSessionTokenNeeded = userContext.ExecuteInUserContext<bool>(() => this.OpenConnectionAndCheckIfSessionTokenNeeded(connectionInfo));
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00019C9C File Offset: 0x00017E9C
		private bool OpenConnectionAndCheckIfSessionTokenNeeded(ConnectionInfo connectionInfo)
		{
			bool flag = false;
			switch (connectionInfo.ConnectionType)
			{
			case ConnectionType.Native:
			{
				ConnectionException ex = null;
				SecurityMode[] array;
				if (!connectionInfo.IsSchannelSspi())
				{
					(array = new SecurityMode[1])[0] = SecurityMode.Block;
				}
				else
				{
					(array = new SecurityMode[1])[0] = SecurityMode.Stream;
				}
				foreach (SecurityMode securityMode in array)
				{
					ex = null;
					try
					{
						this.OpenTcpConnection(connectionInfo, securityMode);
						break;
					}
					catch (ConnectionException ex)
					{
					}
					catch (Win32Exception ex2)
					{
						ex = new ConnectionException(XmlaSR.Authentication_Failed, ex2, ConnectionExceptionCause.AuthenticationFailed);
					}
				}
				if (ex != null)
				{
					throw ex;
				}
				break;
			}
			case ConnectionType.Http:
				this.OpenHttpConnection(connectionInfo, out flag);
				break;
			case ConnectionType.LocalServer:
				this.OpenLocalServerConnection(connectionInfo);
				break;
			case ConnectionType.LocalCube:
				this.OpenLocalCubeConnection(connectionInfo);
				break;
			case ConnectionType.Wcf:
				this.OpenWcfConnection(connectionInfo);
				break;
			default:
				throw new NotImplementedException();
			}
			return flag;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00019D7C File Offset: 0x00017F7C
		private void OpenTcpConnection(ConnectionInfo connectionInfo, SecurityMode securityMode)
		{
			DateTime dateTime = ((connectionInfo.ConnectTimeout > 0) ? DateTime.Now.AddSeconds((double)connectionInfo.ConnectTimeout) : DateTime.MaxValue);
			this.tcpClient = XmlaClient.GetTcpClient(this.owner, connectionInfo);
			try
			{
				if (dateTime != DateTime.MaxValue)
				{
					DateTime now = DateTime.Now;
					if (now >= dateTime)
					{
						throw new ConnectionException(XmlaSR.XmlaClient_ConnectTimedOut);
					}
					int num = (int)dateTime.Subtract(now).TotalMilliseconds;
					this.SetReadWriteTimeouts(num, num);
				}
				this.networkStream = new BufferedStream(this.tcpClient.GetStream(), ClientFeaturesManager.GetTcpStreamBufferSize());
				TcpStream tcpStream = new TcpStream(this.networkStream, connectionInfo.PacketSize, XmlaClient.GetDesiredRequestType(connectionInfo), XmlaClient.GetDesiredResponseType(connectionInfo));
				CompressedStream compressedStream = new CompressedStream(tcpStream, connectionInfo.CompressionLevel);
				this.xmlaStream = compressedStream;
				if (!connectionInfo.IsSspiAnonymous && connectionInfo.ProtectionLevel != ProtectionLevel.None)
				{
					this.connected = true;
					this.connInfo = connectionInfo;
					Microsoft.AnalysisServices.Sspi.SecurityContext securityContext = this.Authenticate(connectionInfo, dateTime, securityMode);
					switch (connectionInfo.ProtectionLevel)
					{
					case ProtectionLevel.Connection:
						securityContext.Dispose();
						break;
					case ProtectionLevel.Integrity:
						compressedStream.SetBaseXmlaStream(new TcpSignedStream(tcpStream, securityContext));
						break;
					case ProtectionLevel.Privacy:
						compressedStream.SetBaseXmlaStream(new TcpEncryptedStream(tcpStream, securityContext));
						break;
					}
				}
				if (connectionInfo.ConnectionType == ConnectionType.Native && !string.IsNullOrEmpty(connectionInfo.AuthenticationScheme))
				{
					this.SendExtAuth(connectionInfo);
				}
				this.SetReadWriteTimeouts(0, 0);
				this.connected = true;
			}
			catch
			{
				this.connected = false;
				this.connInfo = null;
				if (this.tcpClient != null)
				{
					this.tcpClient.Close();
					this.tcpClient = null;
				}
				throw;
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00019F38 File Offset: 0x00018138
		private void OpenHttpConnection(ConnectionInfo connectionInfo, out bool isSessionTokenNeeded)
		{
			try
			{
				int num = ((connectionInfo.Timeout > 0) ? (connectionInfo.Timeout * 1000) : (-1));
				HttpStream httpStream = HttpStream.Create(this.owner, connectionInfo, XmlaClient.GetDesiredRequestType(connectionInfo), XmlaClient.GetDesiredResponseType(connectionInfo), num, out isSessionTokenNeeded);
				this.xmlaStream = new CompressedStream(httpStream, connectionInfo.CompressionLevel);
				this.connected = true;
			}
			catch (UriFormatException ex)
			{
				throw new XmlaStreamException(ex);
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00019FAC File Offset: 0x000181AC
		private void OpenLocalServerConnection(ConnectionInfo connectionInfo)
		{
			this.xmlaStream = new LocalServerStream();
			this.connected = true;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00019FC0 File Offset: 0x000181C0
		private void OpenLocalCubeConnection(ConnectionInfo connectionInfo)
		{
			if (connectionInfo.RestrictedClient)
			{
				throw new InvalidOperationException(XmlaSR.XmlaClient_CannotConnectToLocalCubeWithRestictedClient);
			}
			this.xmlaStream = LocalCubeStream.Create(connectionInfo);
			this.connected = true;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00019FE8 File Offset: 0x000181E8
		private void OpenWcfConnection(ConnectionInfo connectionInfo)
		{
			string text;
			string text2;
			string text3;
			string text4;
			string text5;
			try
			{
				SPConnectivityManager.ConnectToFileInLegacyFarm(connectionInfo.Server, connectionInfo.DataSourceVersion, out text, out text2, out text3, out text4, out text5);
			}
			catch (Exception ex)
			{
				if (ex.InnerException == null)
				{
					throw new ConnectionException(XmlaSR.CannotConnect);
				}
				throw new ConnectionException(XmlaSR.CannotConnect, ex.InnerException);
			}
			bool flag = true;
			if (string.IsNullOrEmpty(connectionInfo.DataSourceVersion))
			{
				flag = false;
				if (string.IsNullOrEmpty(text))
				{
					throw new ConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo);
				}
				connectionInfo.DataSourceVersion = text;
			}
			connectionInfo.SetCatalog(text3);
			WcfStream wcfStream = new WcfStream(connectionInfo.Server, text5, flag, text4, text2, XmlaClient.GetDesiredRequestType(connectionInfo), XmlaClient.GetDesiredResponseType(connectionInfo), connectionInfo.ApplicationName);
			this.xmlaStream = new CompressedStream(wcfStream, connectionInfo.CompressionLevel);
			this.connected = true;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001A0C0 File Offset: 0x000182C0
		private Microsoft.AnalysisServices.Sspi.SecurityContext Authenticate(ConnectionInfo connectionInfo, DateTime timeout, SecurityMode securityMode)
		{
			Microsoft.AnalysisServices.Sspi.SecurityContext securityContext;
			try
			{
				string text = XmlaClient.CalculateNTAuthenticationSPN(connectionInfo);
				NTAuthenticationConfiguration ntauthenticationConfiguration = XmlaClient.BuildNTAuthenticationConfiguration(connectionInfo);
				XmlaResult xmlaResult = new XmlaResult();
				using (NTAuthenticationSession ntauthenticationSession = new NTAuthenticationSession(securityMode, text, connectionInfo.Sspi, ntauthenticationConfiguration))
				{
					SecurityBuffer securityBuffer = ntauthenticationSession.GetFirstOutgoingToken();
					while (!ntauthenticationSession.IsHandshakeComplete || (securityBuffer != null && securityBuffer.Size > 0))
					{
						if (timeout != DateTime.MaxValue)
						{
							DateTime now = DateTime.Now;
							if (now >= timeout)
							{
								throw new ConnectionException(XmlaSR.XmlaClient_ConnectTimedOut);
							}
							int num = (int)timeout.Subtract(now).TotalMilliseconds;
							this.SetReadWriteTimeouts(num, num);
						}
						this.StartRequest(null);
						this.writer.WriteStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
						this.writer.WriteStartElement("Body");
						this.writer.WriteStartElement("Authenticate", "http://schemas.microsoft.com/analysisservices/2003/ext");
						if (ntauthenticationConfiguration.IsSChannel)
						{
							if (securityMode == SecurityMode.Stream)
							{
								this.writer.WriteElementString("ClientVersion", "1");
							}
							this.writer.WriteElementString("AuthProtocol", "Schannel");
						}
						this.writer.WriteElementString("SspiHandshake", Convert.ToBase64String(securityBuffer.Buffer, securityBuffer.Offset, securityBuffer.Size));
						this.writer.WriteEndElement();
						this.writer.WriteEndElement();
						this.writer.WriteEndElement();
						this.EndRequest();
						this.reader.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
						this.ReadEnvelopeHeader(this.reader, false, false);
						this.reader.ReadStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
						XmlaClient.CheckForError(this.reader, xmlaResult, true);
						this.reader.ReadStartElement("AuthenticateResponse", "http://schemas.microsoft.com/analysisservices/2003/ext");
						this.reader.ReadStartElement("return", "http://schemas.microsoft.com/analysisservices/2003/ext");
						this.reader.MoveToContent();
						object obj;
						if (this.reader.IsEmptyElement)
						{
							obj = string.Empty;
							this.reader.Skip();
						}
						else
						{
							obj = this.reader.ReadElementContentAsObject("SspiHandshake", "http://schemas.microsoft.com/analysisservices/2003/ext");
						}
						byte[] array = ((obj is string) ? Convert.FromBase64String((string)obj) : ((byte[])obj));
						this.reader.ReadEndElement();
						this.reader.ReadEndElement();
						this.reader.ReadEndElement();
						this.reader.ReadEndElement();
						this.EndReceival(true);
						if (ntauthenticationSession.IsHandshakeComplete && array != null && array.Length != 0)
						{
							throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, "Non-empty SSPI token value");
						}
						if (!ntauthenticationSession.IsHandshakeComplete)
						{
							securityBuffer = ntauthenticationSession.GetNextOutgoingToken(array);
						}
						else
						{
							securityBuffer = null;
						}
					}
					securityContext = ntauthenticationSession.ObtainSecurityContext();
				}
			}
			catch (SspiAuthenticationException ex)
			{
				switch (ex.ErrorType)
				{
				case SspiAuthenticationError.InvalidPackageName:
					throw new ConnectionException(XmlaSR.Authentication_Sspi_PackageNotFound(ex.GetPackageName()), ex, ConnectionExceptionCause.AuthenticationFailed);
				case SspiAuthenticationError.MissingCapability:
					throw new ConnectionException(XmlaSR.Authentication_Sspi_PackageDoesntSupportCapability(ex.GetPackageName(), ex.GetMissingCapabilities().ToString()), ex, ConnectionExceptionCause.AuthenticationFailed);
				case SspiAuthenticationError.RequirementNotObtained:
					throw new ConnectionException(XmlaSR.Authentication_Sspi_FlagNotEstablished(ex.GetRequirementsNotObtained().ToString()), ex, ConnectionExceptionCause.AuthenticationFailed);
				}
				throw new ConnectionException(XmlaSR.Authentication_Failed, ex, ConnectionExceptionCause.AuthenticationFailed);
			}
			catch (Win32Exception ex2)
			{
				throw new ConnectionException(XmlaSR.Authentication_Failed, ex2, ConnectionExceptionCause.AuthenticationFailed);
			}
			return securityContext;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001A478 File Offset: 0x00018678
		private void SendExtAuth(ConnectionInfo connectionInfo)
		{
			this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"", true, true, false);
			XmlaResult xmlaResult = new XmlaResult();
			try
			{
				this.writer.WriteStartElement("Execute", "urn:schemas-microsoft-com:xml-analysis");
				this.writer.WriteStartElement("Command");
				this.writer.WriteStartElement("ExtAuth", "http://schemas.microsoft.com/analysisservices/2003/engine");
				this.WriteIfNonEmptyElement("AuthenticationScheme", connectionInfo.AuthenticationScheme);
				this.writer.WriteStartElement("ExtAuthInfo");
				if (!string.IsNullOrEmpty(connectionInfo.ExtAuthInfo))
				{
					this.writer.WriteString(connectionInfo.ExtAuthInfo);
				}
				else if (connectionInfo.AuthenticationScheme.Equals("ActAs", StringComparison.OrdinalIgnoreCase))
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("<Properties>");
					this.WriteIfNonEmptyElement(stringBuilder, "IdentityProvider", XmlaClient.EscapeXMLString(connectionInfo.IdentityProvider));
					this.WriteIfNonEmptyElement(stringBuilder, "UserName", XmlaClient.EscapeXMLString(connectionInfo.UserID));
					this.WriteIfNonEmptyElement(stringBuilder, "BypassAuthorization", XmlaClient.EscapeXMLString(connectionInfo.BypassAuthorization));
					if (connectionInfo.RestrictCatalog.Equals("true", StringComparison.OrdinalIgnoreCase))
					{
						this.WriteIfNonEmptyElement(stringBuilder, "RestrictCatalog", XmlaClient.EscapeXMLString(connectionInfo.Catalog));
					}
					if (!string.IsNullOrEmpty(connectionInfo.RestrictRoles))
					{
						this.WriteIfNonEmptyElement(stringBuilder, "RestrictRoles", XmlaClient.EscapeXMLString(connectionInfo.RestrictRoles));
					}
					this.WriteIfNonEmptyElement(stringBuilder, "AccessMode", XmlaClient.EscapeXMLString(connectionInfo.AccessMode));
					stringBuilder.Append("</Properties>");
					this.writer.WriteString(stringBuilder.ToString());
				}
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				this.WriteProperties(null, null);
				this.writer.WriteEndElement();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			this.EndMessage();
			this.EndRequest();
			try
			{
				this.reader.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
				this.ReadEnvelopeHeader(this.reader, false, true);
				this.reader.ReadStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
				XmlaClient.CheckForError(this.reader, xmlaResult, true);
				this.reader.ReadStartElement("ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
				this.reader.ReadStartElement("return", "urn:schemas-microsoft-com:xml-analysis");
				if (!XmlaClient.IsEmptyResultS(this.reader))
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis:empty", "root", this.reader.Name));
				}
				this.reader.Skip();
				this.reader.ReadEndElement();
				this.reader.ReadEndElement();
				this.reader.ReadEndElement();
				XmlaClient.CheckEndElement(this.reader, "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
			}
			catch (XmlException ex2)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex2);
			}
			catch (IOException ex3)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex3);
			}
			catch (ResponseFormatException)
			{
				throw;
			}
			catch (OperationException)
			{
				throw;
			}
			catch
			{
				this.CloseAll();
				throw;
			}
			finally
			{
				if (this.connected)
				{
					this.EndReceival(true);
				}
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001A85C File Offset: 0x00018A5C
		private void CreateSession(ListDictionary properties, bool sendNamespaceCompatibility, string sessionToken)
		{
			this.SessionID = null;
			IDictionary dictionary = null;
			this.PopulateCommandProperties(ref dictionary, null, false);
			this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"", true, sendNamespaceCompatibility, false);
			XmlaResult xmlaResult = new XmlaResult();
			try
			{
				this.writer.WriteStartElement("Execute", "urn:schemas-microsoft-com:xml-analysis");
				this.writer.WriteStartElement("Command");
				if (string.IsNullOrEmpty(sessionToken))
				{
					this.writer.WriteElementString("Statement", string.Empty);
				}
				else
				{
					this.writer.WriteStartElement("ExtAuth");
					this.writer.WriteStartElement("AuthenticationScheme");
					this.writer.WriteString("DelegateToken");
					this.writer.WriteEndElement();
					this.writer.WriteStartElement("ExtAuthInfo");
					this.writer.WriteString(sessionToken);
					this.writer.WriteEndElement();
					this.writer.WriteEndElement();
				}
				this.writer.WriteEndElement();
				this.WriteProperties(properties, dictionary);
				this.writer.WriteEndElement();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			this.EndMessage();
			this.EndRequest();
			try
			{
				this.reader.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
				this.ReadEnvelopeHeader(this.reader, true, sendNamespaceCompatibility);
				this.reader.ReadStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
				XmlaClient.CheckForError(this.reader, xmlaResult, true);
				this.reader.ReadStartElement("ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
				this.reader.ReadStartElement("return", "urn:schemas-microsoft-com:xml-analysis");
				if (!XmlaClient.IsEmptyResultS(this.reader))
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis:empty", "root", this.reader.Name));
				}
				this.reader.Skip();
				this.reader.ReadEndElement();
				this.reader.ReadEndElement();
				this.reader.ReadEndElement();
				XmlaClient.CheckEndElement(this.reader, "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
			}
			catch (XmlException ex2)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex2);
			}
			catch (IOException ex3)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex3);
			}
			catch (ResponseFormatException)
			{
				throw;
			}
			catch (OperationException)
			{
				throw;
			}
			catch
			{
				this.CloseAll();
				throw;
			}
			finally
			{
				if (this.connected)
				{
					this.EndReceival(true);
				}
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001AB84 File Offset: 0x00018D84
		private string GetSessionToken(ListDictionary properties, bool sendNamespaceCompatibility)
		{
			string text;
			try
			{
				IDictionary dictionary = null;
				this.PopulateCommandProperties(ref dictionary, null, false);
				this.xmlaStream.IsSessionTokenNeeded = true;
				this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"", false, sendNamespaceCompatibility, true);
				new XmlaResult();
				try
				{
					this.writer.WriteStartElement("Execute", "urn:schemas-microsoft-com:xml-analysis");
					this.writer.WriteStartElement("Command");
					this.writer.WriteElementString("Statement", string.Empty);
					this.writer.WriteEndElement();
					this.WriteProperties(properties, dictionary);
					this.writer.WriteEndElement();
				}
				catch (IOException ex)
				{
					this.CloseAll();
					throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
				}
				catch
				{
					this.HandleMessageCreationException();
					throw;
				}
				this.EndMessage();
				try
				{
					this.EndRequest();
					this.reader.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
					text = this.GetTokenFromEnvelopeHeader(this.reader);
				}
				catch (WebException ex2)
				{
					HttpWebResponse httpWebResponse = ex2.Response as HttpWebResponse;
					if (httpWebResponse == null || httpWebResponse.StatusCode != HttpStatusCode.BadRequest)
					{
						throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
					}
					text = string.Empty;
				}
				catch (XmlException ex3)
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex3);
				}
				catch (IOException ex4)
				{
					this.CloseAll();
					throw new ConnectionException(XmlaSR.ConnectionBroken, ex4);
				}
				catch (ResponseFormatException)
				{
					throw;
				}
				catch (OperationException)
				{
					throw;
				}
				catch
				{
					this.CloseAll();
					throw;
				}
				finally
				{
					if (this.connected)
					{
						this.EndReceival(true);
					}
				}
			}
			catch
			{
				if (this.xmlaStream == null)
				{
					throw;
				}
				text = string.Empty;
			}
			finally
			{
				if (this.xmlaStream != null)
				{
					this.xmlaStream.IsSessionTokenNeeded = false;
				}
			}
			return text;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001AE14 File Offset: 0x00019014
		private void WriteIfNonEmptyElement(string elementName, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.writer.WriteStartElement(elementName);
				this.writer.WriteString(value);
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001AE41 File Offset: 0x00019041
		private void WriteIfNonEmptyElement(StringBuilder sb, string elementName, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				sb.AppendFormat("<{0}>{1}</{0}>", elementName, value);
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001AE59 File Offset: 0x00019059
		private void VerifyIfCanWrite()
		{
			this.VerifyIfCanWrite(false);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001AE62 File Offset: 0x00019062
		private void VerifyIfCanWrite(bool useBinaryXml)
		{
			this.CheckConnection();
			if (this.reader != null)
			{
				throw new InvalidOperationException(XmlaSR.XmlaClient_SendRequest_ThereIsAnotherPendingResponse);
			}
			if (!useBinaryXml && this.writer == null)
			{
				throw new InvalidOperationException(XmlaSR.XmlaClient_SendRequest_NoRequestWasCreated);
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001AE94 File Offset: 0x00019094
		private XmlWriter StartMessage(string action, bool addCreateSession, bool sendNamespaceCompatibility, bool addGetSessionToken)
		{
			this.StartRequest(action);
			if (!this.captureXml)
			{
				try
				{
					this.writer.WriteStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
					this.writer.WriteStartElement("Header");
					if (this.SessionID != null)
					{
						this.WriteSessionId();
					}
					else
					{
						if (addGetSessionToken)
						{
							this.WriteBeginGetSessionToken();
						}
						if (addCreateSession)
						{
							this.WriteBeginSession();
						}
						this.WriteVersionHeader();
					}
					if (sendNamespaceCompatibility)
					{
						this.writer.WriteRaw("<NamespaceCompatibility xmlns=\"http://schemas.microsoft.com/analysisservices/2003/xmla\" mustUnderstand=\"0\"/>");
					}
					this.writer.WriteEndElement();
					this.writer.WriteStartElement("Body");
				}
				catch (IOException ex)
				{
					this.CloseAll();
					throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
				}
				catch
				{
					this.HandleMessageCreationException();
					throw;
				}
			}
			return this.writer;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001AF70 File Offset: 0x00019170
		private void WriteStatement(string command)
		{
			this.CheckConnection();
			try
			{
				this.writer.WriteStartElement("Statement");
				this.writer.WriteString(command);
				this.writer.WriteEndElement();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001AFE4 File Offset: 0x000191E4
		private void WriteCommandContent(string command)
		{
			try
			{
				this.writer.WriteRaw(command);
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001B038 File Offset: 0x00019238
		private void WriteEndOfMessage()
		{
			this.WriteEndOfMessage(false);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001B044 File Offset: 0x00019244
		private void WriteEndOfMessage(bool callBaseDirect)
		{
			if (XmlaClient.TRACESWITCH.TraceVerbose)
			{
				Trace.WriteLine(new StringBuilder().Append("[").Append(base.GetType().Name).Append("{XmlaClient}::WriteEndOfMessage]: ")
					.Append("..  ")
					.Append("callBaseDirect: ")
					.Append(callBaseDirect)
					.Append("; ")
					.ToString());
			}
			if (callBaseDirect)
			{
				((CompressedStream)this.xmlaStream).BaseXmlaStream.WriteEndOfMessage();
			}
			else
			{
				this.xmlaStream.WriteEndOfMessage();
			}
			this.connectionState = ConnectionState.Fetching;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001B0E0 File Offset: 0x000192E0
		private void PopulateCommandProperties(ref IDictionary commandProperties, IDictionary requestProperties, bool isCommand)
		{
			if (!this.SupportsApplicationContext && commandProperties != null)
			{
				object obj = "ApplicationContext";
				if (commandProperties.Contains(obj))
				{
					commandProperties.Remove(obj);
				}
			}
			if (this.SupportsActivityIDAndRequestID)
			{
				if (commandProperties == null)
				{
					commandProperties = new ListDictionary();
				}
				bool flag = this.SupportsCurrentActivityID;
				commandProperties["DbpropMsmdActivityID"] = this.connInfo.ClientActivityID;
				this.xmlaStream.ActivityID = (Guid)commandProperties["DbpropMsmdActivityID"];
				Guid guid;
				if (this.connInfo.IsPaaSInfrastructure)
				{
					if (this.connInfo.ConnectionActivityId != Guid.Empty)
					{
						guid = this.connInfo.ConnectionActivityId;
					}
					else
					{
						guid = this.connInfo.ClientActivityID;
					}
				}
				else
				{
					guid = Guid.NewGuid();
				}
				commandProperties["DbpropMsmdRequestID"] = guid;
				this.xmlaStream.RequestID = guid;
				if (flag)
				{
					Guid currentActivityID = this.connInfo.CurrentActivityID;
					commandProperties["DbpropMsmdCurrentActivityID"] = currentActivityID;
					this.xmlaStream.CurrentActivityID = currentActivityID;
				}
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001B1F8 File Offset: 0x000193F8
		private IList<string> SupportsProperties(IList<string> propertyList)
		{
			List<string> list = new List<string>();
			try
			{
				XmlReader xmlReader = this.Discover("DISCOVER_PROPERTIES", null, this.ConnectionInfo.ExtendedProperties, null, false, null);
				xmlReader.ReadStartElement("DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
				xmlReader.ReadStartElement("return");
				xmlReader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
				if (!xmlReader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", "http://www.w3.org/2001/XMLSchema", "schema", xmlReader.Name));
				}
				IDictionary<string, string> dictionary = this.ParseRowsetSchema(xmlReader);
				if (xmlReader.IsStartElement("row", "urn:schemas-microsoft-com:xml-analysis:rowset") && dictionary.ContainsKey("PropertyName"))
				{
					do
					{
						XmlReader xmlReader2 = xmlReader.ReadSubtree();
						xmlReader2.ReadToFollowing(dictionary["PropertyName"]);
						string text = xmlReader2.ReadElementContentAsString();
						list.Add(text);
						xmlReader2.Close();
					}
					while (xmlReader.ReadToNextSibling("row"));
				}
			}
			finally
			{
				this.EndReceival(true);
			}
			return list;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001B308 File Offset: 0x00019508
		private IDictionary<string, string> ParseRowsetSchema(XmlReader reader)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			reader.ReadStartElement("schema", "http://www.w3.org/2001/XMLSchema");
			while (reader.IsStartElement())
			{
				if (!reader.MoveToFirstAttribute() || reader.Value != "row")
				{
					reader.Skip();
				}
				else
				{
					reader.ReadStartElement("complexType", "http://www.w3.org/2001/XMLSchema");
					reader.ReadStartElement("sequence", "http://www.w3.org/2001/XMLSchema");
					while (reader.IsStartElement())
					{
						string text = string.Empty;
						string text2 = string.Empty;
						for (int i = 0; i < reader.AttributeCount; i++)
						{
							reader.MoveToAttribute(i);
							if (reader.LocalName == "field")
							{
								text = reader.Value;
							}
							else if (reader.LocalName == "name")
							{
								text2 = reader.Value;
							}
						}
						if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
						{
							dictionary.Add(text, text2);
						}
						reader.Skip();
					}
					reader.ReadEndElement();
					reader.ReadEndElement();
				}
			}
			reader.ReadEndElement();
			return dictionary;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001B414 File Offset: 0x00019614
		private void WriteVersionSafeProperty(DictionaryEntry propertyEntry)
		{
			string text = (string)propertyEntry.Key;
			if (text.Equals("DbpropMsmdCurrentActivityID", StringComparison.OrdinalIgnoreCase))
			{
				if (this.SupportsCurrentActivityID)
				{
					this.writer.WriteElementString(text, FormattersHelpers.ConvertToXml(propertyEntry.Value));
					return;
				}
			}
			else if (text.Equals("DbpropMsmdRequestID", StringComparison.OrdinalIgnoreCase))
			{
				if (this.SupportsActivityIDAndRequestID)
				{
					this.writer.WriteElementString(text, FormattersHelpers.ConvertToXml(propertyEntry.Value));
					return;
				}
			}
			else if (text.Equals("DbpropMsmdActivityID", StringComparison.OrdinalIgnoreCase))
			{
				if (this.SupportsActivityIDAndRequestID)
				{
					this.writer.WriteElementString(text, FormattersHelpers.ConvertToXml(propertyEntry.Value));
					return;
				}
			}
			else if (text.Equals("ApplicationContext", StringComparison.OrdinalIgnoreCase))
			{
				if (this.SupportsApplicationContext)
				{
					this.writer.WriteElementString(text, FormattersHelpers.ConvertToXml(propertyEntry.Value));
					return;
				}
			}
			else
			{
				this.writer.WriteElementString(text, FormattersHelpers.ConvertToXml(propertyEntry.Value));
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001B508 File Offset: 0x00019708
		private void WriteParameters(IDataParameterCollection parameters)
		{
			if (parameters != null && parameters.Count > 0)
			{
				this.writer.WriteStartElement("Parameters");
				this.writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
				this.writer.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
				foreach (object obj in parameters)
				{
					IDataParameter dataParameter = (IDataParameter)obj;
					if (dataParameter.Value != null)
					{
						this.writer.WriteStartElement("Parameter");
						this.writer.WriteElementString("Name", dataParameter.ParameterName);
						if (typeof(IDataReader).IsInstanceOfType(dataParameter.Value) || typeof(DataTable).IsInstanceOfType(dataParameter.Value))
						{
							this.WriteTabularParameterValue(dataParameter.Value);
						}
						else
						{
							this.writer.WriteStartElement("Value");
							if (typeof(DBNull) == dataParameter.Value.GetType())
							{
								this.writer.WriteAttributeString("xsi:nil", "true");
							}
							else
							{
								this.writer.WriteAttributeString("xsi:type", XmlaTypeHelper.GetXmlaType(dataParameter.Value.GetType()));
								this.writer.WriteString(FormattersHelpers.ConvertToXml(dataParameter.Value));
							}
							this.writer.WriteEndElement();
						}
						this.writer.WriteEndElement();
					}
				}
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001B6C0 File Offset: 0x000198C0
		private void WriteTabularParameterValue(object value)
		{
			IDataReader dataReader = null;
			if (typeof(DataTable).IsInstanceOfType(value))
			{
				dataReader = ((DataTable)value).CreateDataReader();
			}
			if (typeof(IDataReader).IsInstanceOfType(value))
			{
				dataReader = (IDataReader)value;
			}
			this.writer.WriteStartElement("Value");
			this.writer.WriteAttributeString("xmlns", "urn:schemas-microsoft-com:xml-analysis:rowset");
			this.writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
			this.writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
			this.WriteTabularSchema(dataReader);
			while (dataReader.Read())
			{
				this.writer.WriteStartElement("row");
				for (int i = 0; i < dataReader.FieldCount; i++)
				{
					object value2 = dataReader.GetValue(i);
					if (value2 != null && !dataReader.IsDBNull(i))
					{
						this.writer.WriteStartElement(XmlConvert.EncodeLocalName(dataReader.GetName(i)));
						this.writer.WriteString(FormattersHelpers.ConvertToXml(value2));
						this.writer.WriteEndElement();
					}
				}
				this.writer.WriteEndElement();
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001B7F0 File Offset: 0x000199F0
		private void WriteTabularSchema(IDataReader dataReader)
		{
			DataTable schemaTable = dataReader.GetSchemaTable();
			this.writer.WriteStartElement("schema", "http://www.w3.org/2001/XMLSchema");
			this.writer.WriteAttributeString("targetNamespace", "urn:schemas-microsoft-com:xml-analysis:rowset");
			this.writer.WriteAttributeString("elementFormDefault", "qualified");
			this.writer.WriteAttributeString("xmlns:sql", "urn:schemas-microsoft-com:xml-sql");
			this.writer.WriteStartElement("element", "http://www.w3.org/2001/XMLSchema");
			this.writer.WriteAttributeString("name", "root");
			this.writer.WriteStartElement("complexType", "http://www.w3.org/2001/XMLSchema");
			this.writer.WriteStartElement("sequence", "http://www.w3.org/2001/XMLSchema");
			this.writer.WriteAttributeString("minOccurs", "0");
			this.writer.WriteAttributeString("maxOccurs", "unbounded");
			this.writer.WriteStartElement("element", "http://www.w3.org/2001/XMLSchema");
			this.writer.WriteAttributeString("name", "row");
			this.writer.WriteAttributeString("type", "row");
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
			this.writer.WriteStartElement("simpleType", "http://www.w3.org/2001/XMLSchema");
			this.writer.WriteAttributeString("name", "uuid");
			this.writer.WriteStartElement("restriction", "http://www.w3.org/2001/XMLSchema");
			this.writer.WriteAttributeString("base", "xsd:string");
			this.writer.WriteStartElement("pattern", "http://www.w3.org/2001/XMLSchema");
			this.writer.WriteAttributeString("value", "[0-9a-zA-Z]{8}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{4}-[0-9a-zA-Z]{12}");
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
			this.writer.WriteStartElement("complexType", "http://www.w3.org/2001/XMLSchema");
			this.writer.WriteAttributeString("name", "row");
			this.writer.WriteStartElement("sequence", "http://www.w3.org/2001/XMLSchema");
			for (int i = 0; i < schemaTable.Rows.Count; i++)
			{
				DataRow dataRow = schemaTable.Rows[i];
				this.writer.WriteStartElement("element", "http://www.w3.org/2001/XMLSchema");
				this.writer.WriteAttributeString("sql:field", dataRow["ColumnName"].ToString());
				this.writer.WriteAttributeString("name", XmlConvert.EncodeLocalName(dataRow["ColumnName"].ToString()));
				this.writer.WriteAttributeString("type", XmlaTypeHelper.GetXmlaType((Type)dataRow["DataType"]));
				if ((bool)dataRow["AllowDBNull"])
				{
					this.writer.WriteAttributeString("minOccurs", "0");
				}
				this.writer.WriteEndElement();
			}
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001BB1C File Offset: 0x00019D1C
		private void WriteRestrictions(IDictionary restrictions)
		{
			this.writer.WriteStartElement("Restrictions");
			if (restrictions != null && restrictions.Count > 0)
			{
				this.writer.WriteStartElement("RestrictionList");
				foreach (object obj in restrictions)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (dictionaryEntry.Value != null)
					{
						this.WriteXmlaProperty(dictionaryEntry);
					}
				}
				this.writer.WriteEndElement();
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001BBBC File Offset: 0x00019DBC
		private void SetReadWriteTimeouts(int readTimeoutMilliseconds, int writeTimeoutMilliseconds)
		{
			this.tcpClient.ReceiveTimeout = ((writeTimeoutMilliseconds == -1 || writeTimeoutMilliseconds < 0) ? 0 : writeTimeoutMilliseconds);
			this.tcpClient.SendTimeout = ((readTimeoutMilliseconds == -1 || readTimeoutMilliseconds < 0) ? 0 : readTimeoutMilliseconds);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001BBEC File Offset: 0x00019DEC
		private void CheckIfReaderDetached()
		{
			if (this.IsReaderDetached)
			{
				throw new InvalidOperationException(XmlaSR.ConnectionCannotBeUsedWhileXmlReaderOpened);
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001BC04 File Offset: 0x00019E04
		private void WriteBeginSession()
		{
			this.writer.WriteStartElement("BeginSession", "urn:schemas-microsoft-com:xml-analysis");
			this.writer.WriteAttributeString("soap", "mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
			this.writer.WriteEndElement();
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001BC50 File Offset: 0x00019E50
		private void WriteBeginGetSessionToken()
		{
			this.writer.WriteStartElement("BeginGetSessionToken", "http://schemas.microsoft.com/analysisservices/2003/xmla");
			this.writer.WriteAttributeString("soap", "mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
			this.writer.WriteEndElement();
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001BC9C File Offset: 0x00019E9C
		private void WriteSessionId()
		{
			this.writer.WriteStartElement("XA", "Session", "urn:schemas-microsoft-com:xml-analysis");
			this.writer.WriteAttributeString("soap", "mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
			this.writer.WriteAttributeString("SessionId", this.SessionID);
			this.writer.WriteEndElement();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001BD03 File Offset: 0x00019F03
		private void WriteVersionHeader()
		{
			this.writer.WriteStartElement("Version", "http://schemas.microsoft.com/analysisservices/2003/engine/2");
			this.writer.WriteAttributeString("Sequence", "922");
			this.writer.WriteEndElement();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001BD3C File Offset: 0x00019F3C
		private void ReadEnvelopeHeader(XmlReader reader, bool readSession, bool readNamepsaceCompatibility)
		{
			if (reader.IsStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				reader.ReadStartElement();
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("Session", "urn:schemas-microsoft-com:xml-analysis"))
					{
						if (readSession)
						{
							this.SessionID = reader.GetAttribute("SessionId");
							XmlaClient.CheckAndSkipEmptyElement(reader, "Session", "urn:schemas-microsoft-com:xml-analysis");
						}
						else
						{
							reader.Skip();
						}
					}
					else if (reader.IsStartElement("Session", "http://schemas.xmlsoap.org/soap/envelope/"))
					{
						if (readSession)
						{
							this.SessionID = reader.GetAttribute("SessionId");
							XmlaClient.CheckAndSkipEmptyElement(reader, "Session", "http://schemas.xmlsoap.org/soap/envelope/");
						}
						else
						{
							reader.Skip();
						}
					}
					else if (reader.IsStartElement("NamespaceCompatibility", "http://schemas.microsoft.com/analysisservices/2003/engine"))
					{
						if (readNamepsaceCompatibility)
						{
							this.namespacesManager.PopulateIgnorableNamespaces(reader);
						}
						else
						{
							reader.Skip();
						}
					}
					else
					{
						string attribute = reader.GetAttribute("mustUnderstand");
						if (attribute != null && !(attribute.Trim() == "0"))
						{
							throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Unexpected value of MustUnderstand attribute: '{0}'", attribute));
						}
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001BE70 File Offset: 0x0001A070
		private string GetTokenFromEnvelopeHeader(XmlReader reader)
		{
			string text = string.Empty;
			if (reader.IsStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				reader.ReadStartElement();
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("SessionToken", "http://schemas.microsoft.com/analysisservices/2003/xmla"))
					{
						string text2 = reader.ReadOuterXml();
						int num = text2.IndexOf(">", StringComparison.OrdinalIgnoreCase);
						if (num == -1)
						{
							break;
						}
						int num2 = text2.IndexOf(string.Format(CultureInfo.InvariantCulture, "</{0}>", "SessionToken"), StringComparison.OrdinalIgnoreCase);
						if (num2 != -1)
						{
							text = text2.Substring(num + 1, num2 - num - 1).Trim();
							break;
						}
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0001BF06 File Offset: 0x0001A106
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0001BF0E File Offset: 0x0001A10E
		internal bool CaptureXml
		{
			get
			{
				return this.captureXml;
			}
			set
			{
				this.captureXml = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0001BF17 File Offset: 0x0001A117
		internal StringCollection CaptureLog
		{
			get
			{
				return this.captureLog;
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001BF20 File Offset: 0x0001A120
		public ConnectionState GetConnectionState(bool pingServer)
		{
			if (!this.connected)
			{
				return ConnectionState.Closed;
			}
			if (this.writer != null)
			{
				return ConnectionState.Executing;
			}
			if (this.reader != null)
			{
				return ConnectionState.Fetching;
			}
			if (pingServer)
			{
				try
				{
					string text;
					this.ExecuteStatement(string.Empty, null, out text, true, false);
					return ConnectionState.Open;
				}
				catch (ConnectionException)
				{
					return ConnectionState.Broken;
				}
				return ConnectionState.Open;
			}
			return ConnectionState.Open;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001BF7C File Offset: 0x0001A17C
		public void Connect(string connectionString)
		{
			this.Connect(new ConnectionInfo(connectionString));
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001BF8A File Offset: 0x0001A18A
		public void Connect(string connectionString, bool beginSession)
		{
			this.Connect(new ConnectionInfo(connectionString), beginSession);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001BF99 File Offset: 0x0001A199
		public void Connect(string connectionString, string sessionID)
		{
			this.Connect(new ConnectionInfo(connectionString), sessionID);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public void Connect(ConnectionInfo connectionInfo)
		{
			if (connectionInfo == null)
			{
				throw new ArgumentNullException("connectionInfo");
			}
			this.sessionID = (string.IsNullOrEmpty(connectionInfo.SessionID) ? null : connectionInfo.SessionID);
			bool flag = this.SessionID == null;
			this.Connect(connectionInfo, flag);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001BFF1 File Offset: 0x0001A1F1
		public void Disconnect()
		{
			this.Disconnect(true);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		public void Reconnect()
		{
			if (this.connInfo == null)
			{
				throw new InvalidOperationException(XmlaSR.Reconnect_ConnectionInfoIsMissing);
			}
			if (this.connected)
			{
				this.CloseAll();
			}
			if (this.sessionID == null)
			{
				this.Connect(this.connInfo);
				return;
			}
			this.Connect(this.connInfo, this.sessionID);
			try
			{
				string text;
				this.ExecuteStatement(string.Empty, null, out text, true, false);
			}
			catch (ConnectionException)
			{
				this.Disconnect(false);
				this.Connect(this.connInfo);
			}
			catch (OperationException)
			{
				this.Disconnect(false);
				this.Connect(this.connInfo);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001C0AC File Offset: 0x0001A2AC
		public void ImageLoad(string databaseName, string databaseId, Stream sourceDbStream, bool allowOverwrite, string readWriteMode)
		{
			try
			{
				this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				this.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("ImageLoad", "http://schemas.microsoft.com/analysisservices/2003/engine");
				if (allowOverwrite)
				{
					this.writer.WriteAttributeString("AllowOverwrite", "true");
				}
				this.writer.WriteElementString("DatabaseName", databaseName);
				this.writer.WriteElementString("DatabaseID", databaseId);
				this.writer.WriteStartElement("ReadWriteMode", "http://schemas.microsoft.com/analysisservices/2008/engine/100");
				this.writer.WriteString(readWriteMode);
				this.writer.WriteEndElement();
				this.writer.WriteStartElement("Data");
				byte[] array = new byte[65536];
				int num;
				do
				{
					num = sourceDbStream.Read(array, 0, array.Length);
					if (num != 0)
					{
						this.writer.WriteStartElement("DataBlock");
						this.writer.WriteBase64(array, 0, num);
						this.writer.WriteEndElement();
					}
				}
				while (num != 0);
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				this.WriteEndCommand(this.ConnectionInfo.ExtendedProperties, dictionary, null);
				this.EndMessage();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(false, true);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001C230 File Offset: 0x0001A430
		public void BinaryImageLoad(string databaseName, string databaseId, Stream sourceAbfStream, bool allowOverwrite, string readWriteMode)
		{
			XmlaDataType xmlaDataType = this.xmlaStream.GetRequestDataType();
			TransportCapabilitiesAwareXmlaStream transportCapabilitiesAwareXmlaStream = null;
			try
			{
				this.CheckConnection();
				transportCapabilitiesAwareXmlaStream = (TransportCapabilitiesAwareXmlaStream)((CompressedStream)this.xmlaStream).BaseXmlaStream;
				xmlaDataType = this.xmlaStream.GetRequestDataType();
				transportCapabilitiesAwareXmlaStream.SetRequestDataType(XmlaDataType.BinaryXml);
				this.connectionState = ConnectionState.Executing;
				IDictionary dictionary = null;
				this.PopulateCommandProperties(ref dictionary, null, true);
				this.WriteContent(this.xmlaStream, XmlaClient.preSessionID, this.SessionID, XmlaClient.BinaryXmlTokenType.XmlAttribute);
				this.WriteContent(this.xmlaStream, XmlaClient.preDatabaseName, databaseName, XmlaClient.BinaryXmlTokenType.StringContent);
				this.WriteContent(this.xmlaStream, XmlaClient.preDatabaseID, databaseId, XmlaClient.BinaryXmlTokenType.StringContent);
				this.WriteContent(this.xmlaStream, XmlaClient.preReadWriteMode, readWriteMode, XmlaClient.BinaryXmlTokenType.StringContent);
				this.WriteAbfContent(this.xmlaStream, XmlaClient.preAbfContent, sourceAbfStream, XmlaClient.BinaryXmlTokenType.BinaryContent);
				this.WriteContent(this.xmlaStream, XmlaClient.localeBegin, (string)this.ConnectionInfo.ExtendedProperties["LocaleIdentifier"], XmlaClient.BinaryXmlTokenType.Int32Content);
				this.WriteContent(this.xmlaStream, XmlaClient.localeEnd, null, XmlaClient.BinaryXmlTokenType.StringContent);
				this.WritePropertiesBinaryXmla(this.xmlaStream, dictionary);
				this.WriteContent(this.xmlaStream, XmlaClient.endContent, null, XmlaClient.BinaryXmlTokenType.StringContent);
			}
			catch (IOException ex)
			{
				if (transportCapabilitiesAwareXmlaStream != null)
				{
					transportCapabilitiesAwareXmlaStream.SetRequestDataType(xmlaDataType);
				}
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				if (transportCapabilitiesAwareXmlaStream != null)
				{
					transportCapabilitiesAwareXmlaStream.SetRequestDataType(xmlaDataType);
				}
				this.HandleMessageCreationException();
				throw;
			}
			try
			{
				this.SendExecuteAndReadResponse(false, true, true);
			}
			finally
			{
				if (transportCapabilitiesAwareXmlaStream != null)
				{
					transportCapabilitiesAwareXmlaStream.SetRequestDataType(xmlaDataType);
				}
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001C3E8 File Offset: 0x0001A5E8
		public void ImageSave(string databaseId, Stream targetDbStream)
		{
			try
			{
				this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				this.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("ImageSave", "http://schemas.microsoft.com/analysisservices/2003/engine");
				this.writer.WriteStartElement("Object");
				this.writer.WriteElementString("DatabaseID", databaseId);
				this.writer.WriteEndElement();
				this.writer.WriteElementString("Data", "true");
				this.writer.WriteEndElement();
				this.WriteEndCommand(this.ConnectionInfo.ExtendedProperties, dictionary, null);
				this.EndMessage();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			try
			{
				XmlReader xmlReader = this.SendMessage(true, false, false);
				if (xmlReader != null)
				{
					xmlReader.MoveToContent();
					xmlReader.ReadStartElement("ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
					xmlReader.ReadStartElement("return", "urn:schemas-microsoft-com:xml-analysis");
					xmlReader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
					if (xmlReader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
					{
						xmlReader.Skip();
					}
					byte[] array = new byte[65536];
					while (xmlReader.IsStartElement("row", "urn:schemas-microsoft-com:xml-analysis:rowset"))
					{
						xmlReader.ReadStartElement();
						int num;
						while ((num = xmlReader.ReadElementContentAsBase64(array, 0, array.Length)) > 0)
						{
							targetDbStream.Write(array, 0, num);
						}
						xmlReader.ReadEndElement();
					}
					xmlReader.ReadEndElement();
					xmlReader.ReadEndElement();
					xmlReader.ReadEndElement();
					xmlReader.ReadEndElement();
					XmlaClient.CheckEndElement(xmlReader, "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
				}
			}
			catch (XmlException ex2)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex2);
			}
			catch (IOException ex3)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex3);
			}
			catch (ResponseFormatException)
			{
				throw;
			}
			catch (OperationException)
			{
				throw;
			}
			catch
			{
				this.CloseAll();
				throw;
			}
			finally
			{
				if (this.connected)
				{
					this.EndReceival(true);
				}
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001C684 File Offset: 0x0001A884
		internal void SetConnectionInfo(ConnectionInfo ci)
		{
			this.connInfo = ci;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001C68D File Offset: 0x0001A88D
		internal void CancelSession(string sessionID)
		{
			if (string.IsNullOrEmpty(sessionID))
			{
				throw new ArgumentException(XmlaSR.Cancel_SessionIDNotSpecified);
			}
			this.Cancel(sessionID, null, null, false);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001C6AC File Offset: 0x0001A8AC
		internal void CancelSession(string sessionID, bool cancelAssociated)
		{
			if (string.IsNullOrEmpty(sessionID))
			{
				throw new ArgumentException(XmlaSR.Cancel_SessionIDNotSpecified);
			}
			this.Cancel(sessionID, null, null, cancelAssociated);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001C6CB File Offset: 0x0001A8CB
		internal void CancelSession(int spid, bool cancelAssociated)
		{
			this.Cancel(null, XmlConvert.ToString(spid), null, cancelAssociated);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001C6DC File Offset: 0x0001A8DC
		internal void CancelConnection(int connectionID, bool cancelAssociated)
		{
			this.Cancel(null, null, XmlConvert.ToString(connectionID), cancelAssociated);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001C6F0 File Offset: 0x0001A8F0
		internal void WriteStartBatch(bool transactional, bool processAffected, bool skipVolatileObjects)
		{
			this.CheckConnection();
			try
			{
				this.writer.WriteStartElement("Batch", "http://schemas.microsoft.com/analysisservices/2003/engine");
				if (!transactional)
				{
					this.writer.WriteAttributeString("Transaction", XmlConvert.ToString(transactional));
				}
				if (processAffected)
				{
					this.writer.WriteAttributeString("ProcessAffectedObjects", XmlConvert.ToString(processAffected));
				}
				if (skipVolatileObjects)
				{
					this.writer.WriteAttributeString("SkipVolatileObjects", XmlConvert.ToString(skipVolatileObjects));
				}
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001C7A0 File Offset: 0x0001A9A0
		internal void WriteEndBatch()
		{
			this.CheckConnection();
			try
			{
				this.writer.WriteEndElement();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001C7F8 File Offset: 0x0001A9F8
		internal static XmlaCommandFormat GetCommandFormat(string commandText)
		{
			if (string.IsNullOrEmpty(commandText))
			{
				return XmlaCommandFormat.Unknown;
			}
			int num = 0;
			while (num < commandText.Length && char.IsWhiteSpace(commandText[num]))
			{
				num++;
			}
			if (num >= commandText.Length)
			{
				return XmlaCommandFormat.Unknown;
			}
			char c = commandText[num];
			if (c != '/')
			{
				if (c == '<')
				{
					return XmlaCommandFormat.Xml;
				}
				if (c != '{')
				{
					return XmlaCommandFormat.Unknown;
				}
			}
			return XmlaCommandFormat.Json;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001C857 File Offset: 0x0001AA57
		internal void WriteCommandText(string commandText)
		{
			this.WriteCommandTextImpl(commandText, XmlaClient.GetCommandFormat(commandText));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001C866 File Offset: 0x0001AA66
		internal void WriteCommandTextImpl(string commandText, XmlaCommandFormat format)
		{
			if (format != XmlaCommandFormat.Xml && format - XmlaCommandFormat.Json <= 2)
			{
				this.WriteStatement(commandText);
				return;
			}
			this.WriteCommandContent(commandText);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001C884 File Offset: 0x0001AA84
		private void WriteEndCommand(string properties, bool propertiesXmlIsComplete, string parameters, bool parametersXmlIsComplete, IDictionary commandProperties)
		{
			this.CheckConnection();
			try
			{
				if (!this.captureXml)
				{
					properties = this.PopulateActivityIDAndRequestIDIntoProperties(properties, commandProperties, propertiesXmlIsComplete);
					this.writer.WriteEndElement();
					this.WriteProperties(properties, !propertiesXmlIsComplete);
					if (parameters != null && parameters.Length > 0)
					{
						if (parametersXmlIsComplete)
						{
							this.writer.WriteRaw(parameters);
						}
						else
						{
							this.writer.WriteStartElement("Parameters");
							this.writer.WriteAttributeString("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
							this.writer.WriteAttributeString("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
							this.writer.WriteRaw(parameters);
							this.writer.WriteEndElement();
						}
					}
					this.writer.WriteEndElement();
				}
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001C978 File Offset: 0x0001AB78
		private void WriteProperties(string properties, bool writeListElement)
		{
			this.writer.WriteStartElement("Properties");
			if (properties != null)
			{
				if (writeListElement)
				{
					this.writer.WriteStartElement("PropertyList");
					this.writer.WriteRaw(properties);
					this.writer.WriteEndElement();
				}
				else
				{
					this.writer.WriteRaw(properties);
				}
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001C9DC File Offset: 0x0001ABDC
		private void ExecuteStatement(string statement, string properties, out string result, bool skipResult, bool propertiesXmlIsComplete)
		{
			this.CheckConnection();
			try
			{
				this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				this.WriteStartCommand(ref dictionary);
				this.WriteStatement(statement);
				this.WriteEndCommand(properties, propertiesXmlIsComplete, null, true, dictionary);
				this.EndMessage();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			this.SendMessageAndReturnResult(out result, skipResult);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001CA64 File Offset: 0x0001AC64
		private void Cancel(string sessionID, string spID, string connectionID, bool cancelAssociated)
		{
			this.CheckConnection();
			try
			{
				this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				this.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("Cancel", "http://schemas.microsoft.com/analysisservices/2003/engine");
				if (sessionID != null)
				{
					this.writer.WriteElementString("SessionID", sessionID);
				}
				if (connectionID != null)
				{
					this.writer.WriteElementString("ConnectionID", connectionID);
				}
				if (spID != null)
				{
					this.writer.WriteElementString("SPID", spID);
				}
				if (cancelAssociated)
				{
					this.writer.WriteElementString("CancelAssociated", XmlConvert.ToString(cancelAssociated));
				}
				this.writer.WriteEndElement();
				this.WriteEndCommand(this.ConnectionInfo.ExtendedProperties, dictionary, null);
				this.EndMessage();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(false, true);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001CB60 File Offset: 0x0001AD60
		private void SendMessageAndReturnResult(out string result, bool skipResult)
		{
			result = null;
			try
			{
				XmlReader xmlReader = this.SendMessage(false, false, false);
				if (xmlReader != null)
				{
					if (!skipResult)
					{
						xmlReader.MoveToContent();
						result = xmlReader.ReadInnerXml();
					}
				}
			}
			catch (XmlException ex)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				this.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch (ResponseFormatException)
			{
				throw;
			}
			catch (OperationException)
			{
				throw;
			}
			catch
			{
				this.CloseAll();
				throw;
			}
			finally
			{
				if (this.connected)
				{
					this.EndReceival(true);
				}
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001CC20 File Offset: 0x0001AE20
		private void WritePropertiesBinaryXmla(Stream xmlaStream, IDictionary commandProperties)
		{
			if (this.SupportsActivityIDAndRequestID && commandProperties.Contains("DbpropMsmdActivityID"))
			{
				this.WriteContent(this.xmlaStream, XmlaClient.activityIDBegin, ((Guid)commandProperties["DbpropMsmdActivityID"]).ToString(), XmlaClient.BinaryXmlTokenType.StringContent);
				this.WriteContent(this.xmlaStream, XmlaClient.activityIDEnd, null, XmlaClient.BinaryXmlTokenType.StringContent);
			}
			if (this.SupportsActivityIDAndRequestID && commandProperties.Contains("DbpropMsmdRequestID"))
			{
				this.WriteContent(this.xmlaStream, XmlaClient.requestIDBegin, ((Guid)commandProperties["DbpropMsmdRequestID"]).ToString(), XmlaClient.BinaryXmlTokenType.StringContent);
				this.WriteContent(this.xmlaStream, XmlaClient.requestIDEnd, null, XmlaClient.BinaryXmlTokenType.StringContent);
			}
			if (this.SupportsCurrentActivityID && commandProperties.Contains("DbpropMsmdCurrentActivityID"))
			{
				this.WriteContent(this.xmlaStream, XmlaClient.currentActivityIDBegin, ((Guid)commandProperties["DbpropMsmdCurrentActivityID"]).ToString(), XmlaClient.BinaryXmlTokenType.StringContent);
				this.WriteContent(this.xmlaStream, XmlaClient.currentActivityIDEnd, null, XmlaClient.BinaryXmlTokenType.StringContent);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001CD38 File Offset: 0x0001AF38
		private void WriteContent(Stream xmlaStream, byte[] preContent, string content, XmlaClient.BinaryXmlTokenType tokenType)
		{
			xmlaStream.Write(preContent, 0, preContent.Length);
			if (content != null)
			{
				this.WriteStartToken(xmlaStream, tokenType);
				byte[] array;
				if (tokenType == XmlaClient.BinaryXmlTokenType.Int32Content)
				{
					array = BitConverter.GetBytes(int.Parse(content));
				}
				else
				{
					this.WriteLength(xmlaStream, (ulong)((long)content.Length));
					array = new byte[content.Length * 2];
					Buffer.BlockCopy(content.ToCharArray(), 0, array, 0, array.Length);
				}
				xmlaStream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001CDA8 File Offset: 0x0001AFA8
		private void WriteAbfContent(Stream xmlsStream, byte[] preContent, Stream abfContent, XmlaClient.BinaryXmlTokenType tokenType)
		{
			xmlsStream.Write(preContent, 0, preContent.Length);
			if (abfContent != null)
			{
				this.WriteStartToken(xmlsStream, tokenType);
				this.WriteLength(xmlsStream, (ulong)abfContent.Length);
				byte[] array = new byte[65536];
				abfContent.Position = 0L;
				int num;
				do
				{
					num = abfContent.Read(array, 0, array.Length);
					if (num != 0)
					{
						xmlsStream.Write(array, 0, num);
					}
				}
				while (num != 0);
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001CE0C File Offset: 0x0001B00C
		private void WriteLength(Stream xmlaStream, ulong length)
		{
			int num = 0;
			while ((num <= 4 && length > 0UL) || num == 0)
			{
				byte b = (byte)(length & 127UL);
				length >>= 7;
				if (length > 0UL)
				{
					b |= 128;
				}
				xmlaStream.WriteByte(b);
				num++;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001CE4E File Offset: 0x0001B04E
		private void WriteStartToken(Stream xmlaStream, XmlaClient.BinaryXmlTokenType tokenType)
		{
			switch (tokenType)
			{
			case XmlaClient.BinaryXmlTokenType.XmlAttribute:
				xmlaStream.WriteByte(14);
				return;
			case XmlaClient.BinaryXmlTokenType.BinaryContent:
				xmlaStream.WriteByte(12);
				return;
			case XmlaClient.BinaryXmlTokenType.Int32Content:
				xmlaStream.WriteByte(2);
				return;
			}
			xmlaStream.WriteByte(17);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001CE8C File Offset: 0x0001B08C
		private string PopulateActivityIDAndRequestIDIntoProperties(string properties, IDictionary commandProperties, bool propertiesXmlIsComplete)
		{
			if (this.SupportsActivityIDAndRequestID)
			{
				if (propertiesXmlIsComplete)
				{
					if (string.IsNullOrEmpty(properties))
					{
						properties = string.Format(CultureInfo.InvariantCulture, "<{0}></{0}>", "PropertyList");
					}
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(properties);
					XmlNode xmlNode = xmlDocument.SelectSingleNode(string.Format(CultureInfo.InvariantCulture, "/{0}", "PropertyList"));
					if (xmlNode != null && commandProperties != null && commandProperties.Contains("DbpropMsmdActivityID"))
					{
						XmlElement xmlElement = xmlDocument.CreateElement("DbpropMsmdActivityID");
						xmlElement.InnerText = commandProperties["DbpropMsmdActivityID"].ToString();
						XmlElement xmlElement2 = xmlDocument.CreateElement("DbpropMsmdRequestID");
						xmlElement2.InnerText = commandProperties["DbpropMsmdRequestID"].ToString();
						xmlNode.AppendChild(xmlElement);
						xmlNode.AppendChild(xmlElement2);
						if (this.SupportsCurrentActivityID && commandProperties.Contains("DbpropMsmdCurrentActivityID"))
						{
							XmlElement xmlElement3 = xmlDocument.CreateElement("DbpropMsmdCurrentActivityID");
							xmlElement3.InnerText = commandProperties["DbpropMsmdCurrentActivityID"].ToString();
							xmlNode.AppendChild(xmlElement3);
						}
					}
					return xmlDocument.OuterXml;
				}
				if (commandProperties != null && commandProperties.Contains("DbpropMsmdActivityID"))
				{
					string text = string.Format(CultureInfo.InvariantCulture, "<{0}>{1}</{0}>", "DbpropMsmdActivityID", commandProperties["DbpropMsmdActivityID"].ToString());
					string text2 = string.Format(CultureInfo.InvariantCulture, "<{0}>{1}</{0}>", "DbpropMsmdRequestID", commandProperties["DbpropMsmdRequestID"].ToString());
					properties = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", properties, text, text2);
					if (this.SupportsCurrentActivityID && commandProperties.Contains("DbpropMsmdCurrentActivityID"))
					{
						string text3 = string.Format(CultureInfo.InvariantCulture, "<{0}>{1}</{0}>", "DbpropMsmdCurrentActivityID", commandProperties["DbpropMsmdCurrentActivityID"].ToString());
						properties = string.Format(CultureInfo.InvariantCulture, "{0}{1}", properties, text3);
					}
				}
			}
			return properties;
		}

		// Token: 0x040002AB RID: 683
		protected const string ActivityIDPropertyName = "DbpropMsmdActivityID";

		// Token: 0x040002AC RID: 684
		protected const string RequestIDPropertyName = "DbpropMsmdRequestID";

		// Token: 0x040002AD RID: 685
		protected const string CurrentActivityIDPropertyName = "DbpropMsmdCurrentActivityID";

		// Token: 0x040002AE RID: 686
		protected const string RequestMemoryLimitPropertyName = "DbPropmsmdRequestMemoryLimit";

		// Token: 0x040002AF RID: 687
		protected const string ApplicationContextPropertyName = "ApplicationContext";

		// Token: 0x040002B0 RID: 688
		internal const int ReadStreamBufferSize = 4096;

		// Token: 0x040002B1 RID: 689
		internal const int MaxHttpConnections = 1000;

		// Token: 0x040002B2 RID: 690
		internal const string LocaleIdentifierName = "LocaleIdentifier";

		// Token: 0x040002B3 RID: 691
		private const string PropertyListName = "PropertyList";

		// Token: 0x040002B4 RID: 692
		private const bool BeginSessionOnConnect = true;

		// Token: 0x040002B5 RID: 693
		private const bool EndSessionOnDisconnect = true;

		// Token: 0x040002B6 RID: 694
		private const string VersionSequenceNumber = "922";

		// Token: 0x040002B7 RID: 695
		protected internal static readonly TraceSwitch TRACESWITCH = new TraceSwitch(typeof(XmlaClient).FullName, typeof(XmlaClient).FullName, TraceLevel.Off.ToString());

		// Token: 0x040002B8 RID: 696
		private static bool hasRuntimeSupportForBinaryXml = XmlaClient.HasRuntimeSupportForBinaryXmlImpl();

		// Token: 0x040002B9 RID: 697
		private static readonly byte[] preSessionID = new byte[]
		{
			223, byte.MaxValue, 1, 176, 4, 254, 3, 49, 0, 46,
			0, 48, 0, 253, 6, 85, 0, 84, 0, 70,
			0, 45, 0, 49, 0, 54, 0, 0, 240, 41,
			104, 0, 116, 0, 116, 0, 112, 0, 58, 0,
			47, 0, 47, 0, 115, 0, 99, 0, 104, 0,
			101, 0, 109, 0, 97, 0, 115, 0, 46, 0,
			120, 0, 109, 0, 108, 0, 115, 0, 111, 0,
			97, 0, 112, 0, 46, 0, 111, 0, 114, 0,
			103, 0, 47, 0, 115, 0, 111, 0, 97, 0,
			112, 0, 47, 0, 101, 0, 110, 0, 118, 0,
			101, 0, 108, 0, 111, 0, 112, 0, 101, 0,
			47, 0, 240, 8, 69, 0, 110, 0, 118, 0,
			101, 0, 108, 0, 111, 0, 112, 0, 101, 0,
			239, 1, 0, 2, 248, 1, 240, 5, 120, 0,
			109, 0, 108, 0, 110, 0, 115, 0, 239, 0,
			3, 0, 246, 2, 14, 41, 104, 0, 116, 0,
			116, 0, 112, 0, 58, 0, 47, 0, 47, 0,
			115, 0, 99, 0, 104, 0, 101, 0, 109, 0,
			97, 0, 115, 0, 46, 0, 120, 0, 109, 0,
			108, 0, 115, 0, 111, 0, 97, 0, 112, 0,
			46, 0, 111, 0, 114, 0, 103, 0, 47, 0,
			115, 0, 111, 0, 97, 0, 112, 0, 47, 0,
			101, 0, 110, 0, 118, 0, 101, 0, 108, 0,
			111, 0, 112, 0, 101, 0, 47, 0, 240, 6,
			72, 0, 101, 0, 97, 0, 100, 0, 101, 0,
			114, 0, 239, 1, 0, 4, 245, 248, 3, 240,
			38, 117, 0, 114, 0, 110, 0, 58, 0, 115,
			0, 99, 0, 104, 0, 101, 0, 109, 0, 97,
			0, 115, 0, 45, 0, 109, 0, 105, 0, 99,
			0, 114, 0, 111, 0, 115, 0, 111, 0, 102,
			0, 116, 0, 45, 0, 99, 0, 111, 0, 109,
			0, 58, 0, 120, 0, 109, 0, 108, 0, 45,
			0, 97, 0, 110, 0, 97, 0, 108, 0, 121,
			0, 115, 0, 105, 0, 115, 0, 240, 1, 120,
			0, 240, 7, 83, 0, 101, 0, 115, 0, 115,
			0, 105, 0, 111, 0, 110, 0, 239, 5, 6,
			7, 248, 4, 240, 7, 120, 0, 109, 0, 108,
			0, 110, 0, 115, 0, 58, 0, 120, 0, 239,
			0, 8, 0, 246, 5, 14, 38, 117, 0, 114,
			0, 110, 0, 58, 0, 115, 0, 99, 0, 104,
			0, 101, 0, 109, 0, 97, 0, 115, 0, 45,
			0, 109, 0, 105, 0, 99, 0, 114, 0, 111,
			0, 115, 0, 111, 0, 102, 0, 116, 0, 45,
			0, 99, 0, 111, 0, 109, 0, 58, 0, 120,
			0, 109, 0, 108, 0, 45, 0, 97, 0, 110,
			0, 97, 0, 108, 0, 121, 0, 115, 0, 105,
			0, 115, 0, 240, 9, 83, 0, 101, 0, 115,
			0, 115, 0, 105, 0, 111, 0, 110, 0, 73,
			0, 100, 0, 239, 0, 0, 9, 246, 6
		};

		// Token: 0x040002BA RID: 698
		private static readonly byte[] preDatabaseName = new byte[]
		{
			240, 14, 109, 0, 117, 0, 115, 0, 116, 0,
			85, 0, 110, 0, 100, 0, 101, 0, 114, 0,
			115, 0, 116, 0, 97, 0, 110, 0, 100, 0,
			239, 1, 0, 10, 246, 7, 14, 1, 49, 0,
			245, 247, 247, 240, 4, 66, 0, 111, 0, 100,
			0, 121, 0, 239, 1, 0, 11, 248, 8, 240,
			7, 69, 0, 120, 0, 101, 0, 99, 0, 117,
			0, 116, 0, 101, 0, 239, 5, 0, 12, 248,
			9, 246, 2, 14, 38, 117, 0, 114, 0, 110,
			0, 58, 0, 115, 0, 99, 0, 104, 0, 101,
			0, 109, 0, 97, 0, 115, 0, 45, 0, 109,
			0, 105, 0, 99, 0, 114, 0, 111, 0, 115,
			0, 111, 0, 102, 0, 116, 0, 45, 0, 99,
			0, 111, 0, 109, 0, 58, 0, 120, 0, 109,
			0, 108, 0, 45, 0, 97, 0, 110, 0, 97,
			0, 108, 0, 121, 0, 115, 0, 105, 0, 115,
			0, 240, 7, 67, 0, 111, 0, 109, 0, 109,
			0, 97, 0, 110, 0, 100, 0, 239, 5, 0,
			13, 245, 248, 10, 240, 57, 104, 0, 116, 0,
			116, 0, 112, 0, 58, 0, 47, 0, 47, 0,
			115, 0, 99, 0, 104, 0, 101, 0, 109, 0,
			97, 0, 115, 0, 46, 0, 109, 0, 105, 0,
			99, 0, 114, 0, 111, 0, 115, 0, 111, 0,
			102, 0, 116, 0, 46, 0, 99, 0, 111, 0,
			109, 0, 47, 0, 97, 0, 110, 0, 97, 0,
			108, 0, 121, 0, 115, 0, 105, 0, 115, 0,
			115, 0, 101, 0, 114, 0, 118, 0, 105, 0,
			99, 0, 101, 0, 115, 0, 47, 0, 50, 0,
			48, 0, 48, 0, 51, 0, 47, 0, 101, 0,
			110, 0, 103, 0, 105, 0, 110, 0, 101, 0,
			240, 9, 73, 0, 109, 0, 97, 0, 103, 0,
			101, 0, 76, 0, 111, 0, 97, 0, 100, 0,
			239, 14, 0, 15, 248, 11, 246, 2, 14, 57,
			104, 0, 116, 0, 116, 0, 112, 0, 58, 0,
			47, 0, 47, 0, 115, 0, 99, 0, 104, 0,
			101, 0, 109, 0, 97, 0, 115, 0, 46, 0,
			109, 0, 105, 0, 99, 0, 114, 0, 111, 0,
			115, 0, 111, 0, 102, 0, 116, 0, 46, 0,
			99, 0, 111, 0, 109, 0, 47, 0, 97, 0,
			110, 0, 97, 0, 108, 0, 121, 0, 115, 0,
			105, 0, 115, 0, 115, 0, 101, 0, 114, 0,
			118, 0, 105, 0, 99, 0, 101, 0, 115, 0,
			47, 0, 50, 0, 48, 0, 48, 0, 51, 0,
			47, 0, 101, 0, 110, 0, 103, 0, 105, 0,
			110, 0, 101, 0, 240, 14, 65, 0, 108, 0,
			108, 0, 111, 0, 119, 0, 79, 0, 118, 0,
			101, 0, 114, 0, 119, 0, 114, 0, 105, 0,
			116, 0, 101, 0, 239, 0, 0, 16, 246, 12,
			14, 4, 116, 0, 114, 0, 117, 0, 101, 0,
			240, 12, 68, 0, 97, 0, 116, 0, 97, 0,
			98, 0, 97, 0, 115, 0, 101, 0, 78, 0,
			97, 0, 109, 0, 101, 0, 239, 14, 0, 17,
			245, 248, 13
		};

		// Token: 0x040002BB RID: 699
		private static readonly byte[] preDatabaseID = new byte[]
		{
			247, 240, 10, 68, 0, 97, 0, 116, 0, 97,
			0, 98, 0, 97, 0, 115, 0, 101, 0, 73,
			0, 68, 0, 239, 14, 0, 18, 248, 14
		};

		// Token: 0x040002BC RID: 700
		private static readonly byte[] preReadWriteMode = new byte[]
		{
			247, 240, 61, 104, 0, 116, 0, 116, 0, 112,
			0, 58, 0, 47, 0, 47, 0, 115, 0, 99,
			0, 104, 0, 101, 0, 109, 0, 97, 0, 115,
			0, 46, 0, 109, 0, 105, 0, 99, 0, 114,
			0, 111, 0, 115, 0, 111, 0, 102, 0, 116,
			0, 46, 0, 99, 0, 111, 0, 109, 0, 47,
			0, 97, 0, 110, 0, 97, 0, 108, 0, 121,
			0, 115, 0, 105, 0, 115, 0, 115, 0, 101,
			0, 114, 0, 118, 0, 105, 0, 99, 0, 101,
			0, 115, 0, 47, 0, 50, 0, 48, 0, 48,
			0, 56, 0, 47, 0, 101, 0, 110, 0, 103,
			0, 105, 0, 110, 0, 101, 0, 47, 0, 49,
			0, 48, 0, 48, 0, 240, 13, 82, 0, 101,
			0, 97, 0, 100, 0, 87, 0, 114, 0, 105,
			0, 116, 0, 101, 0, 77, 0, 111, 0, 100,
			0, 101, 0, 239, 19, 0, 20, 248, 15, 246,
			2, 14, 61, 104, 0, 116, 0, 116, 0, 112,
			0, 58, 0, 47, 0, 47, 0, 115, 0, 99,
			0, 104, 0, 101, 0, 109, 0, 97, 0, 115,
			0, 46, 0, 109, 0, 105, 0, 99, 0, 114,
			0, 111, 0, 115, 0, 111, 0, 102, 0, 116,
			0, 46, 0, 99, 0, 111, 0, 109, 0, 47,
			0, 97, 0, 110, 0, 97, 0, 108, 0, 121,
			0, 115, 0, 105, 0, 115, 0, 115, 0, 101,
			0, 114, 0, 118, 0, 105, 0, 99, 0, 101,
			0, 115, 0, 47, 0, 50, 0, 48, 0, 48,
			0, 56, 0, 47, 0, 101, 0, 110, 0, 103,
			0, 105, 0, 110, 0, 101, 0, 47, 0, 49,
			0, 48, 0, 48, 0, 245
		};

		// Token: 0x040002BD RID: 701
		private static readonly byte[] preAbfContent = new byte[]
		{
			247, 240, 4, 68, 0, 97, 0, 116, 0, 97,
			0, 239, 14, 0, 21, 248, 16, 240, 9, 68,
			0, 97, 0, 116, 0, 97, 0, 66, 0, 108,
			0, 111, 0, 99, 0, 107, 0, 239, 14, 0,
			22, 248, 17
		};

		// Token: 0x040002BE RID: 702
		private static readonly byte[] localeBegin = new byte[]
		{
			247, 247, 247, 247, 240, 10, 80, 0, 114, 0,
			111, 0, 112, 0, 101, 0, 114, 0, 116, 0,
			105, 0, 101, 0, 115, 0, 239, 5, 0, 23,
			248, 18, 240, 12, 80, 0, 114, 0, 111, 0,
			112, 0, 101, 0, 114, 0, 116, 0, 121, 0,
			76, 0, 105, 0, 115, 0, 116, 0, 239, 5,
			0, 24, 248, 19, 240, 16, 76, 0, 111, 0,
			99, 0, 97, 0, 108, 0, 101, 0, 73, 0,
			100, 0, 101, 0, 110, 0, 116, 0, 105, 0,
			102, 0, 105, 0, 101, 0, 114, 0, 239, 5,
			0, 25, 248, 20
		};

		// Token: 0x040002BF RID: 703
		private static readonly byte[] localeEnd = new byte[] { 247 };

		// Token: 0x040002C0 RID: 704
		private static readonly byte[] activityIDBegin = new byte[]
		{
			240, 20, 68, 0, 98, 0, 112, 0, 114, 0,
			111, 0, 112, 0, 77, 0, 115, 0, 109, 0,
			100, 0, 65, 0, 99, 0, 116, 0, 105, 0,
			118, 0, 105, 0, 116, 0, 121, 0, 73, 0,
			68, 0, 239, 5, 0, 26, 248, 21
		};

		// Token: 0x040002C1 RID: 705
		private static readonly byte[] activityIDEnd = new byte[] { 247 };

		// Token: 0x040002C2 RID: 706
		private static readonly byte[] requestIDBegin = new byte[]
		{
			240, 19, 68, 0, 98, 0, 112, 0, 114, 0,
			111, 0, 112, 0, 77, 0, 115, 0, 109, 0,
			100, 0, 82, 0, 101, 0, 113, 0, 117, 0,
			101, 0, 115, 0, 116, 0, 73, 0, 68, 0,
			239, 5, 0, 27, 248, 22
		};

		// Token: 0x040002C3 RID: 707
		private static readonly byte[] requestIDEnd = new byte[] { 247 };

		// Token: 0x040002C4 RID: 708
		private static readonly byte[] currentActivityIDBegin = new byte[]
		{
			240, 27, 68, 0, 98, 0, 112, 0, 114, 0,
			111, 0, 112, 0, 77, 0, 115, 0, 109, 0,
			100, 0, 67, 0, 117, 0, 114, 0, 114, 0,
			101, 0, 110, 0, 116, 0, 65, 0, 99, 0,
			116, 0, 105, 0, 118, 0, 105, 0, 116, 0,
			121, 0, 73, 0, 68, 0, 239, 5, 0, 28,
			248, 23
		};

		// Token: 0x040002C5 RID: 709
		private static readonly byte[] currentActivityIDEnd = new byte[] { 247 };

		// Token: 0x040002C6 RID: 710
		private static readonly byte[] endContent = new byte[] { 247, 247, 247, 247, 247 };

		// Token: 0x040002C7 RID: 711
		internal XmlaStream xmlaStream;

		// Token: 0x040002C8 RID: 712
		private protected XmlWriter writer;

		// Token: 0x040002C9 RID: 713
		private protected XmlReader reader;

		// Token: 0x040002CA RID: 714
		private protected bool captureXml;

		// Token: 0x040002CB RID: 715
		private readonly object lockForCloseAll = new object();

		// Token: 0x040002CC RID: 716
		private readonly IConnectivityOwner owner;

		// Token: 0x040002CD RID: 717
		private string sessionID;

		// Token: 0x040002CE RID: 718
		private Guid activityID = Guid.Empty;

		// Token: 0x040002CF RID: 719
		private bool connected;

		// Token: 0x040002D0 RID: 720
		private StringCollection captureLog;

		// Token: 0x040002D1 RID: 721
		private BufferedStream networkStream;

		// Token: 0x040002D2 RID: 722
		private TcpClient tcpClient;

		// Token: 0x040002D3 RID: 723
		private ConnectionInfo connInfo;

		// Token: 0x040002D4 RID: 724
		private StringWriter logEntry;

		// Token: 0x040002D5 RID: 725
		private NamespacesMgr namespacesManager;

		// Token: 0x040002D6 RID: 726
		private NameTable nameTable;

		// Token: 0x040002D7 RID: 727
		private ConnectionState connectionState;

		// Token: 0x040002D8 RID: 728
		private bool userOpened;

		// Token: 0x040002D9 RID: 729
		private bool supportsActivityIDAndRequestID;

		// Token: 0x040002DA RID: 730
		private bool supportsCurrentActivityID;

		// Token: 0x040002DB RID: 731
		private bool supportsApplicationContext;

		// Token: 0x040002DC RID: 732
		private bool isCompressionEnabled;

		// Token: 0x0200018D RID: 397
		private enum BinaryXmlTokenType
		{
			// Token: 0x04000C1C RID: 3100
			XmlAttribute,
			// Token: 0x04000C1D RID: 3101
			BinaryContent,
			// Token: 0x04000C1E RID: 3102
			StringContent,
			// Token: 0x04000C1F RID: 3103
			Int32Content
		}
	}
}
