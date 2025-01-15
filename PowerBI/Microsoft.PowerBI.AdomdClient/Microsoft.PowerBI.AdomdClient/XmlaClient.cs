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
using Microsoft.AnalysisServices.AdomdClient.Hosting;
using Microsoft.AnalysisServices.AdomdClient.Interop;
using Microsoft.AnalysisServices.AdomdClient.Network;
using Microsoft.AnalysisServices.AdomdClient.Runtime;
using Microsoft.AnalysisServices.AdomdClient.Security;
using Microsoft.AnalysisServices.AdomdClient.Sspi;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200003D RID: 61
	internal class XmlaClient
	{
		// Token: 0x06000311 RID: 785 RVA: 0x00012ADC File Offset: 0x00010CDC
		static XmlaClient()
		{
			ServicePointManager.DefaultConnectionLimit = 1000;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00012C6E File Offset: 0x00010E6E
		public XmlaClient()
			: this(null, new StringCollection())
		{
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00012C7C File Offset: 0x00010E7C
		internal XmlaClient(IConnectivityOwner owner)
			: this(owner, new StringCollection())
		{
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00012C8C File Offset: 0x00010E8C
		internal XmlaClient(IConnectivityOwner owner, StringCollection log)
		{
			this.owner = owner;
			ServicePointManager.UseNagleAlgorithm = false;
			this.captureLog = log;
			this.namespacesManager = new NamespacesMgr();
			this.nameTable = XmlaConstants.GetNameTable();
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00012CDF File Offset: 0x00010EDF
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00012CE7 File Offset: 0x00010EE7
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00012D09 File Offset: 0x00010F09
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00012D11 File Offset: 0x00010F11
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00012D2E File Offset: 0x00010F2E
		public ConnectionInfo ConnectionInfo
		{
			get
			{
				return this.connInfo;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00012D36 File Offset: 0x00010F36
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00012D3E File Offset: 0x00010F3E
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00012D47 File Offset: 0x00010F47
		internal bool SupportsCurrentActivityID
		{
			get
			{
				return this.supportsCurrentActivityID;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00012D4F File Offset: 0x00010F4F
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00012D57 File Offset: 0x00010F57
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00012D60 File Offset: 0x00010F60
		internal bool IsConnected
		{
			get
			{
				return this.connected;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00012D68 File Offset: 0x00010F68
		internal bool IsReaderDetached
		{
			get
			{
				XmlaReader xmlaReader = this.reader as XmlaReader;
				return xmlaReader != null && xmlaReader.IsReaderDetached;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00012D8C File Offset: 0x00010F8C
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
										throw new AdomdConnectionException(XmlaSR.ConnectionString_LinkFileCannotDelegate, null);
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
					throw new AdomdConnectionException(XmlaSR.CannotConnect, ex);
				}
				catch (XmlException ex2)
				{
					this.CloseAll();
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex2);
				}
				catch (AdomdConnectionException ex3)
				{
					if (ex3.Message == XmlaSR.ConnectionBroken)
					{
						throw new AdomdConnectionException(XmlaSR.CannotConnect, ex3.InnerException);
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

		// Token: 0x06000322 RID: 802 RVA: 0x00013158 File Offset: 0x00011358
		public void CreateSession(ListDictionary properties, bool sendNamespaceCompatibility)
		{
			this.CreateSession(properties, sendNamespaceCompatibility, string.Empty);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00013167 File Offset: 0x00011367
		public XmlWriter StartMessage(string action)
		{
			return this.StartMessage(action, false, false, false);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00013174 File Offset: 0x00011374
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
					throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
				}
				catch
				{
					this.HandleMessageCreationException();
					throw;
				}
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000131E0 File Offset: 0x000113E0
		internal static XmlReader GetReaderToReturnToPublic(XmlReader reader)
		{
			XmlaReader xmlaReader = reader as XmlaReader;
			if (xmlaReader != null && !xmlaReader.IsReaderDetached)
			{
				xmlaReader.DetachReader();
			}
			return reader;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00013206 File Offset: 0x00011406
		public void EndReceival()
		{
			this.EndReceival(true);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00013210 File Offset: 0x00011410
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.CloseAll();
				throw;
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000132C8 File Offset: 0x000114C8
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
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

		// Token: 0x06000329 RID: 809 RVA: 0x000133A8 File Offset: 0x000115A8
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(true, true);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001353C File Offset: 0x0001173C
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
						catch (AdomdConnectionException)
						{
						}
						catch (AdomdErrorResponseException)
						{
						}
						catch (AdomdUnknownResponseException)
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

		// Token: 0x0600032B RID: 811 RVA: 0x00013668 File Offset: 0x00011868
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

		// Token: 0x0600032C RID: 812 RVA: 0x000137F4 File Offset: 0x000119F4
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

		// Token: 0x0600032D RID: 813 RVA: 0x00013830 File Offset: 0x00011A30
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
				throw new AdomdUnknownResponseException(XmlaSR.AfterExceptionAllTagsShouldCloseUntilMessagesSection, ex);
			}
			if (!reader.IsStartElement("Messages", "urn:schemas-microsoft-com:xml-analysis:exception"))
			{
				throw new AdomdUnknownResponseException(XmlaSR.AfterExceptionAllTagsShouldCloseUntilMessagesSection, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis:exception", "Messages", reader.Name));
			}
			if (xmlaResult == null)
			{
				xmlaResult = new XmlaResult();
			}
			XmlaClient.ReadXmlaMessages(reader, xmlaResult.Messages);
			if (!xmlaResult.ContainsErrors)
			{
				throw new AdomdUnknownResponseException(XmlaSR.ExceptionRequiresXmlaErrorsInMessagesSection, "No errors in XMLA result");
			}
			if (throwIfError)
			{
				throw XmlaResultCollection.ExceptionOnError(xmlaResult);
			}
			return true;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001391C File Offset: 0x00011B1C
		internal static bool CheckForRowsetError(XmlReader reader, XmlaResult xmlaResult, bool throwIfError)
		{
			return XmlaClient.CheckForInlineError(reader, xmlaResult, throwIfError, "urn:schemas-microsoft-com:xml-analysis:exception");
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001392B File Offset: 0x00011B2B
		internal static XmlaError CheckAndGetRowsetError(XmlReader reader, bool throwIfError)
		{
			return XmlaClient.CheckAndGetInlineError(reader, throwIfError, "urn:schemas-microsoft-com:xml-analysis:exception");
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00013939 File Offset: 0x00011B39
		internal static XmlaError CheckAndGetDatasetError(XmlReader reader)
		{
			return XmlaClient.CheckAndGetInlineError(reader, false, "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00013947 File Offset: 0x00011B47
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

		// Token: 0x06000332 RID: 818 RVA: 0x00013974 File Offset: 0x00011B74
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

		// Token: 0x06000333 RID: 819 RVA: 0x000139D4 File Offset: 0x00011BD4
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

		// Token: 0x06000334 RID: 820 RVA: 0x00013A80 File Offset: 0x00011C80
		internal static bool IsMultipleResult(XmlReader reader)
		{
			return reader.IsStartElement("results", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00013A92 File Offset: 0x00011C92
		internal static bool IsAffectedObjects(XmlReader reader)
		{
			return reader.IsStartElement("AffectedObjects", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00013AA4 File Offset: 0x00011CA4
		internal static bool IsEmptyResultS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:empty");
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00013ABF File Offset: 0x00011CBF
		internal static bool IsExecuteResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00013ADA File Offset: 0x00011CDA
		internal static bool IsDiscoverResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00013AF5 File Offset: 0x00011CF5
		internal static bool IsDatasetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00013B10 File Offset: 0x00011D10
		internal static bool IsRowsetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00013B2B File Offset: 0x00011D2B
		internal static bool IsMultipleResultResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("results", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00013B46 File Offset: 0x00011D46
		internal static bool IsAffectedObjectsResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("AffectedObjects", "http://schemas.microsoft.com/analysisservices/2003/xmla-multipleresults");
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00013B64 File Offset: 0x00011D64
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
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected execute response, discover response, empty result, or root, got {0}", reader.Name));
			}
			if (!reader.EOF)
			{
				reader.MoveToContent();
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00013BD0 File Offset: 0x00011DD0
		internal static void ReadEmptyRootS(XmlReader reader)
		{
			XmlaResult xmlaResult = new XmlaResult();
			XmlaClient.ReadEmptyRoot(reader, xmlaResult, true);
			if (xmlaResult.ContainsErrors)
			{
				throw XmlaResultCollection.ExceptionOnError(xmlaResult);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00013BFA File Offset: 0x00011DFA
		internal static void StartElementS(XmlReader reader, string element, string xmlNamespace)
		{
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement(element, xmlNamespace);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00013C0D File Offset: 0x00011E0D
		internal static void EndElementS(XmlReader reader, string element, string xmlNamespace)
		{
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.ReadEndElementS(reader, element, xmlNamespace);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00013C20 File Offset: 0x00011E20
		internal static void StartExecuteResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("return", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00013C54 File Offset: 0x00011E54
		internal static void EndExecuteResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.ReadEndElementS(reader, "return", "urn:schemas-microsoft-com:xml-analysis");
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.ReadEndElementS(reader, "ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00013C88 File Offset: 0x00011E88
		internal static void StartDiscoverResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("return", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00013CBC File Offset: 0x00011EBC
		internal static void EndDiscoverResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.ReadEndElementS(reader, "return", "urn:schemas-microsoft-com:xml-analysis");
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.ReadEndElementS(reader, "DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00013CF0 File Offset: 0x00011EF0
		internal static void StartDatasetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00013D0B File Offset: 0x00011F0B
		internal static void EndDatasetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.SkipXmlaMessages(reader);
			XmlaClient.ReadEndElementS(reader, "root", "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00013D2C File Offset: 0x00011F2C
		internal static void StartRowsetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			reader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00013D47 File Offset: 0x00011F47
		internal static void EndRowsetResponseS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			XmlaClient.SkipXmlaMessages(reader);
			XmlaClient.ReadEndElementS(reader, "root", "urn:schemas-microsoft-com:xml-analysis:rowset");
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00013D68 File Offset: 0x00011F68
		internal static void ReadEndElementS(XmlReader reader, string name, string ns)
		{
			XmlaClient.CheckForException(reader, null, true);
			if (reader.MoveToContent() != XmlNodeType.EndElement || reader.LocalName != name || reader.NamespaceURI != ns)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", ns, name, reader.Name));
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00013DCC File Offset: 0x00011FCC
		internal static bool IsRootElementS(XmlReader reader)
		{
			XmlaClient.CheckForException(reader, null, true);
			return reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:empty") || reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset") || reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00013E18 File Offset: 0x00012018
		internal void CheckConnection()
		{
			if (this.connected || this.captureXml)
			{
				this.CheckIfReaderDetached();
				return;
			}
			if (this.userOpened)
			{
				throw new AdomdConnectionException(XmlaSR.NotConnected, null);
			}
			throw new InvalidOperationException(XmlaSR.NotConnected);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00013E64 File Offset: 0x00012064
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			return xmlWriter;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00013F70 File Offset: 0x00012170
		internal XmlReader EndRequest()
		{
			return this.EndRequest(false);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00013F7C File Offset: 0x0001217C
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
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
					throw new AdomdConnectionException(XmlaSR.ConnectionBroken, null);
				}
				this.reader = new XmlaReader(xmlReader, this, this.namespacesManager, flag2);
				this.CheckAndGetHttpStreamSoapFault();
				xmlReader2 = this.reader;
			}
			catch (AdomdConnectionException)
			{
				throw;
			}
			catch (IOException ex2)
			{
				this.CloseAll();
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch
			{
				this.CloseAll();
				throw;
			}
			return xmlReader2;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000142D4 File Offset: 0x000124D4
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00014364 File Offset: 0x00012564
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000143E0 File Offset: 0x000125E0
		internal XmlaResultCollection SendExecuteAndReadResponse(bool skipResults, bool throwIfError)
		{
			return this.SendExecuteAndReadResponse(skipResults, throwIfError, false);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000143EC File Offset: 0x000125EC
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
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
			}
			catch (IOException ex2)
			{
				this.CloseAll();
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

		// Token: 0x06000353 RID: 851 RVA: 0x00014650 File Offset: 0x00012850
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

		// Token: 0x06000354 RID: 852 RVA: 0x000146A7 File Offset: 0x000128A7
		private protected static bool CheckForError(XmlReader reader, XmlaResult xmlaResult, bool throwIfError)
		{
			return XmlaClient.CheckForSoapFault(reader, xmlaResult, throwIfError) || XmlaClient.CheckForException(reader, xmlaResult, throwIfError) || XmlaClient.CheckForRowsetError(reader, xmlaResult, throwIfError) || XmlaClient.CheckForDatasetError(reader, xmlaResult, throwIfError);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000146D1 File Offset: 0x000128D1
		private protected static bool CheckForMessages(XmlReader reader, XmlaMessageCollection xmlaMessages)
		{
			return XmlaClient.CheckForMessages(reader, ref xmlaMessages);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000146DC File Offset: 0x000128DC
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
						throw new AdomdUnknownResponseException(XmlaSR.UnrecognizedElementInMessagesSection(reader.Name), XmlaSR.UnrecognizedElementInMessagesSection(reader.Name));
					}
					xmlaMessages.Add(XmlaClient.ReadXmlaWarning(reader));
				}
				num++;
			}
			reader.ReadEndElement();
			if (num == 0)
			{
				throw new AdomdUnknownResponseException(XmlaSR.MessagesSectionIsEmpty, XmlaSR.MessagesSectionIsEmpty);
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001477E File Offset: 0x0001297E
		private protected static void CheckEndElement(XmlReader reader, string localname)
		{
			reader.MoveToContent();
			if (reader.LocalName != localname)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected end of {0} element, got {1}", localname, reader.Name));
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000147B8 File Offset: 0x000129B8
		private protected static void CheckEndElement(XmlReader reader, string localname, string ns)
		{
			reader.MoveToContent();
			if (reader.LocalName != localname || reader.NamespaceURI != ns)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Exected end of {0}:{1} element, got {2}", ns, localname, reader.Name));
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001480C File Offset: 0x00012A0C
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

		// Token: 0x0600035A RID: 858 RVA: 0x00014920 File Offset: 0x00012B20
		private protected virtual void WriteXmlaProperty(DictionaryEntry entry)
		{
			this.writer.WriteElementString((string)entry.Key, FormattersHelpers.ConvertToXml(entry.Value));
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00014948 File Offset: 0x00012B48
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			return this.SendMessage(true, false, sendNamespacesCompatibility);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000149D0 File Offset: 0x00012BD0
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00014A98 File Offset: 0x00012C98
		private protected void WriteEndDiscover(ListDictionary properties)
		{
			this.WriteEndDiscover(properties, null);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00014AA4 File Offset: 0x00012CA4
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00014B60 File Offset: 0x00012D60
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
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex);
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
			}
			catch (AdomdUnknownResponseException ex3)
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
			catch (AdomdErrorResponseException ex4)
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

		// Token: 0x06000360 RID: 864 RVA: 0x000156A0 File Offset: 0x000138A0
		private static bool CheckForDatasetError(XmlReader reader, XmlaResult xmlaResult, bool throwIfError)
		{
			return XmlaClient.CheckForInlineError(reader, xmlaResult, throwIfError, "urn:schemas-microsoft-com:xml-analysis:mddataset");
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000156B0 File Offset: 0x000138B0
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

		// Token: 0x06000362 RID: 866 RVA: 0x000156EC File Offset: 0x000138EC
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

		// Token: 0x06000363 RID: 867 RVA: 0x0001572C File Offset: 0x0001392C
		private static XmlaError ReadInlineError(XmlReader reader, string errorNamespace)
		{
			if (reader.IsEmptyElement)
			{
				throw new AdomdUnknownResponseException(XmlaSR.ErrorCodeIsMissingFromDatasetError, "Empty Error element");
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
				throw new AdomdUnknownResponseException(XmlaSR.ErrorCodeIsMissingFromDatasetError, "Missing error code");
			}
			return new XmlaError((int)XmlConvert.ToUInt32(text), text2, null, null, xmlaMessageLocation, text3, num, flag);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000158E8 File Offset: 0x00013AE8
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

		// Token: 0x06000365 RID: 869 RVA: 0x000159C0 File Offset: 0x00013BC0
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

		// Token: 0x06000366 RID: 870 RVA: 0x00015BC8 File Offset: 0x00013DC8
		private static void ReadPosition(XmlReader reader, string positionName, ref int line, ref int column)
		{
			if (reader.IsStartElement(positionName, "http://schemas.microsoft.com/analysisservices/2003/engine"))
			{
				if (reader.IsEmptyElement)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Empty {0} element", positionName));
				}
				reader.ReadStartElement();
				line = XmlaClient.ReadIntElementIfAny(reader, "Line", "http://schemas.microsoft.com/analysisservices/2003/engine");
				column = XmlaClient.ReadIntElementIfAny(reader, "Column", "http://schemas.microsoft.com/analysisservices/2003/engine");
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00015C36 File Offset: 0x00013E36
		private static int ReadIntElementIfAny(XmlReader reader, string elementName, string elementNamespace)
		{
			if (!reader.IsStartElement(elementName, elementNamespace))
			{
				return -1;
			}
			if (reader.IsEmptyElement)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Empty {0}:{1} element", elementNamespace, elementName));
			}
			return XmlConvert.ToInt32(reader.ReadElementString(elementName, elementNamespace));
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00015C75 File Offset: 0x00013E75
		private static long ReadLongElementIfAny(XmlReader reader, string elementName, string elementNamespace)
		{
			if (!reader.IsStartElement(elementName, elementNamespace))
			{
				return -1L;
			}
			if (reader.IsEmptyElement)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected non-empty {0}:{1} element, but it is empty", elementNamespace, elementName));
			}
			return XmlConvert.ToInt64(reader.ReadElementString(elementName, elementNamespace));
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00015CB8 File Offset: 0x00013EB8
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
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Got {0}", reader.Name));
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
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "Missing fault string");
				}
				xmlaMessages.Add(new XmlaError(0, text, null, null, null));
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00015DF4 File Offset: 0x00013FF4
		private static bool IsStartDetailElement(XmlReader reader)
		{
			return reader.IsStartElement() && reader.LocalName == "detail" && (reader.NamespaceURI == "" || reader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/");
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00015E44 File Offset: 0x00014044
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

		// Token: 0x0600036C RID: 876 RVA: 0x00015FB0 File Offset: 0x000141B0
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

		// Token: 0x0600036D RID: 877 RVA: 0x0001603C File Offset: 0x0001423C
		private void CheckAndGetHttpStreamSoapFault()
		{
			HttpStream httpStream = this.xmlaStream as HttpStream;
			if (httpStream == null || httpStream.StreamException == null)
			{
				return;
			}
			if (!this.reader.IsStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, httpStream.StreamException);
			}
			this.reader.ReadStartElement();
			if (this.reader.IsStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				this.reader.Skip();
			}
			if (!this.reader.IsStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/"))
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, httpStream.StreamException);
			}
			this.reader.ReadStartElement();
			try
			{
				if (!XmlaClient.CheckForSoapFault(this.reader, new XmlaResult(), true))
				{
					throw new AdomdConnectionException(XmlaSR.ConnectionBroken, httpStream.StreamException);
				}
			}
			catch (AdomdErrorResponseException)
			{
				throw;
			}
			catch (XmlException ex)
			{
				throw new AdomdUnknownResponseException(ex);
			}
			throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, httpStream.StreamException.InnerException);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001614C File Offset: 0x0001434C
		private static void SkipXmlaMessages(XmlReader reader)
		{
			if (reader.IsStartElement("Messages", "urn:schemas-microsoft-com:xml-analysis:exception"))
			{
				XmlaMessageCollection xmlaMessageCollection = new XmlaMessageCollection();
				XmlaClient.ReadXmlaMessages(reader, xmlaMessageCollection);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00016178 File Offset: 0x00014378
		private static bool IsCompressionDesired(ConnectionInfo connectionInfo)
		{
			TransportCompression transportCompression = connectionInfo.TransportCompression;
			return (transportCompression == TransportCompression.Default || transportCompression == TransportCompression.Compressed) && CompressedStream.IsCompressionAvailable;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001619C File Offset: 0x0001439C
		internal static bool IsBinaryDesired(ConnectionInfo connectionInfo)
		{
			bool flag;
			return XmlaClient.hasRuntimeSupportForBinaryXml && ClientFeaturesManager.CheckIfBinaryXmlaIsEnabled(out flag) && (flag || (connectionInfo.IsBinarySupported && (connectionInfo.ProtocolFormat == ProtocolFormat.Default || connectionInfo.ProtocolFormat == ProtocolFormat.Binary)));
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000161DF File Offset: 0x000143DF
		private static XmlaDataType GetDesiredRequestType(ConnectionInfo connectionInfo)
		{
			if (XmlaClient.IsCompressionDesired(connectionInfo))
			{
				return XmlaDataType.CompressedXml;
			}
			return XmlaDataType.TextXml;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000161EC File Offset: 0x000143EC
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

		// Token: 0x06000373 RID: 883 RVA: 0x0001621C File Offset: 0x0001441C
		private static bool HasRuntimeSupportForBinaryXmlImpl()
		{
			Version version;
			return !FrameworkRuntimeHelper.IsNetCoreDomain || (FrameworkRuntimeHelper.TryGetRuntimeVersion(out version) && (version.Major != 6 || !(version < new Version("6.0.15"))) && (version.Major != 7 || !(version < new Version("7.0.4"))));
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00016274 File Offset: 0x00014474
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

		// Token: 0x06000375 RID: 885 RVA: 0x000162F8 File Offset: 0x000144F8
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

		// Token: 0x06000376 RID: 886 RVA: 0x00016368 File Offset: 0x00014568
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
			throw new AdomdUnknownResponseException(XmlaSR.UnexpectedElement(reader.LocalName, reader.NamespaceURI), "Expected root element");
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000163E4 File Offset: 0x000145E4
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
					throw new AdomdUnknownResponseException(XmlaSR.EmptyRootIsNotEmpty, string.Format(CultureInfo.InvariantCulture, "Expected end of {0}:{1} element, got {2}", "urn:schemas-microsoft-com:xml-analysis:empty", "root", reader.Name));
				}
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001647E File Offset: 0x0001467E
		private static bool IsEndElement(XmlReader reader, string localName, string ns)
		{
			reader.MoveToContent();
			return reader.NodeType == XmlNodeType.EndElement && reader.LocalName == localName && reader.NamespaceURI == ns;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000164AD File Offset: 0x000146AD
		private static bool CheckAndSkipXsdSchema(XmlReader reader)
		{
			if (reader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
			{
				reader.Skip();
				return true;
			}
			return false;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000164CA File Offset: 0x000146CA
		private static void ReadRowsetRoot(XmlReader reader, XmlaResult xmlaResult, bool skipResults)
		{
			if (skipResults)
			{
				reader.Skip();
				return;
			}
			xmlaResult.SetValue(reader.ReadInnerXml());
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000164E2 File Offset: 0x000146E2
		private static void ReadDatasetRoot(XmlReader reader, XmlaResult xmlaResult, bool skipResults)
		{
			if (skipResults)
			{
				reader.Skip();
				return;
			}
			xmlaResult.SetValue(reader.ReadInnerXml());
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000164FC File Offset: 0x000146FC
		private static void CheckAndSkipEmptyElement(XmlReader reader, string localname, string ns)
		{
			if (!reader.IsStartElement(localname, ns))
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1} element, got {2}", ns, localname, reader.Name));
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			if (!XmlaClient.IsEndElement(reader, localname, ns))
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected end of {0}:{1} element, got {2}", ns, localname, reader.Name));
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001657C File Offset: 0x0001477C
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
				throw new AdomdConnectionException(XmlaSR.CannotConnect, ex);
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
				throw new AdomdConnectionException(XmlaSR.ConnectionString_Invalid, ex2);
			}
			catch (ArgumentOutOfRangeException ex3)
			{
				throw new AdomdConnectionException(XmlaSR.ConnectionString_Invalid, ex3);
			}
			catch (ArgumentException ex4)
			{
				throw new AdomdConnectionException(connectionInfo.IsForSqlBrowser ? XmlaSR.CannotConnectToRedirector : XmlaSR.CannotConnect, ex4);
			}
			catch (SocketException ex5)
			{
				throw new AdomdConnectionException(connectionInfo.IsForSqlBrowser ? XmlaSR.CannotConnectToRedirector : XmlaSR.CannotConnect, ex5);
			}
			tcpClient.NoDelay = true;
			int tcpStreamBufferSize = ClientFeaturesManager.GetTcpStreamBufferSize();
			tcpClient.ReceiveBufferSize = tcpStreamBufferSize;
			tcpClient.SendBufferSize = tcpStreamBufferSize;
			return tcpClient;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000166D0 File Offset: 0x000148D0
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
					throw new AdomdConnectionException(XmlaSR.ConnectionString_Invalid, ex);
				}
				catch (OverflowException ex2)
				{
					throw new AdomdConnectionException(XmlaSR.ConnectionString_Invalid, ex2);
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
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1} element, got {2}", "http://www.w3.org/2001/XMLSchema", "schema", xmlReader.Name));
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
				throw new AdomdConnectionException(XmlaSR.Instance_NotFound(connectionInfo.InstanceName, connectionInfo.Server), null);
			}
			finally
			{
				xmlaClient.Disconnect(false);
			}
			int num2;
			return num2;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000168B4 File Offset: 0x00014AB4
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

		// Token: 0x06000380 RID: 896 RVA: 0x000169C8 File Offset: 0x00014BC8
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
						throw new AdomdConnectionException(XmlaSR.Authentication_Sspi_SchannelAnonymousAmbiguity, null, ConnectionExceptionCause.AuthenticationFailed);
					}
					break;
				case ImpersonationLevel.Identify:
				case ImpersonationLevel.Impersonate:
					break;
				case ImpersonationLevel.Delegate:
					throw new AdomdConnectionException(XmlaSR.Authentication_Sspi_SchannelCantDelegate, null, ConnectionExceptionCause.AuthenticationFailed);
				default:
					throw new AdomdConnectionException(XmlaSR.Authentication_Sspi_SchannelUnsupportedImpersonationLevel, null, ConnectionExceptionCause.AuthenticationFailed);
				}
				ProtectionLevel protectionLevel = ntauthenticationConfiguration.ProtectionLevel;
				if (protectionLevel <= ProtectionLevel.Integrity)
				{
					throw new AdomdConnectionException(XmlaSR.Authentication_Sspi_SchannelSupportsOnlyPrivacyLevel, null, ConnectionExceptionCause.AuthenticationFailed);
				}
				if (protectionLevel != ProtectionLevel.Privacy)
				{
					throw new AdomdConnectionException(XmlaSR.Authentication_Sspi_SchannelUnsupportedProtectionLevel, null, ConnectionExceptionCause.AuthenticationFailed);
				}
			}
			return ntauthenticationConfiguration;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00016A82 File Offset: 0x00014C82
		private static string EscapeXMLString(string xmlString)
		{
			return SecurityElement.Escape(xmlString);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00016A8C File Offset: 0x00014C8C
		private void OpenConnection(ConnectionInfo connectionInfo, out bool isSessionTokenNeeded)
		{
			using (UserContext userContext = IdentityResolver.Resolve(connectionInfo))
			{
				isSessionTokenNeeded = userContext.ExecuteInUserContext<bool>(() => this.OpenConnectionAndCheckIfSessionTokenNeeded(connectionInfo));
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00016AEC File Offset: 0x00014CEC
		private bool OpenConnectionAndCheckIfSessionTokenNeeded(ConnectionInfo connectionInfo)
		{
			bool flag = false;
			switch (connectionInfo.ConnectionType)
			{
			case ConnectionType.Native:
			{
				AdomdConnectionException ex = null;
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
					catch (AdomdConnectionException ex)
					{
					}
					catch (Win32Exception ex2)
					{
						ex = new AdomdConnectionException(XmlaSR.Authentication_Failed, ex2, ConnectionExceptionCause.AuthenticationFailed);
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

		// Token: 0x06000384 RID: 900 RVA: 0x00016BCC File Offset: 0x00014DCC
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
						throw new AdomdConnectionException(XmlaSR.XmlaClient_ConnectTimedOut, null);
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
					Microsoft.AnalysisServices.AdomdClient.Sspi.SecurityContext securityContext = this.Authenticate(connectionInfo, dateTime, securityMode);
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

		// Token: 0x06000385 RID: 901 RVA: 0x00016D94 File Offset: 0x00014F94
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

		// Token: 0x06000386 RID: 902 RVA: 0x00016E08 File Offset: 0x00015008
		private void OpenLocalServerConnection(ConnectionInfo connectionInfo)
		{
			this.xmlaStream = new LocalServerStream();
			this.connected = true;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00016E1C File Offset: 0x0001501C
		private void OpenLocalCubeConnection(ConnectionInfo connectionInfo)
		{
			if (connectionInfo.RestrictedClient)
			{
				throw new InvalidOperationException(XmlaSR.XmlaClient_CannotConnectToLocalCubeWithRestictedClient);
			}
			this.xmlaStream = LocalCubeStream.Create(connectionInfo);
			this.connected = true;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00016E44 File Offset: 0x00015044
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
					throw new AdomdConnectionException(XmlaSR.CannotConnect, null);
				}
				throw new AdomdConnectionException(XmlaSR.CannotConnect, ex.InnerException);
			}
			bool flag = true;
			if (string.IsNullOrEmpty(connectionInfo.DataSourceVersion))
			{
				flag = false;
				if (string.IsNullOrEmpty(text))
				{
					throw new AdomdConnectionException(XmlaSR.Connect_RedirectorDidntReturnDatabaseInfo, null);
				}
				connectionInfo.DataSourceVersion = text;
			}
			connectionInfo.SetCatalog(text3);
			WcfStream wcfStream = new WcfStream(connectionInfo.Server, text5, flag, text4, text2, XmlaClient.GetDesiredRequestType(connectionInfo), XmlaClient.GetDesiredResponseType(connectionInfo), connectionInfo.ApplicationName);
			this.xmlaStream = new CompressedStream(wcfStream, connectionInfo.CompressionLevel);
			this.connected = true;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00016F30 File Offset: 0x00015130
		private Microsoft.AnalysisServices.AdomdClient.Sspi.SecurityContext Authenticate(ConnectionInfo connectionInfo, DateTime timeout, SecurityMode securityMode)
		{
			Microsoft.AnalysisServices.AdomdClient.Sspi.SecurityContext securityContext;
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
								throw new AdomdConnectionException(XmlaSR.XmlaClient_ConnectTimedOut, null);
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
							throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, "Non-empty SSPI token value");
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
					throw new AdomdConnectionException(XmlaSR.Authentication_Sspi_PackageNotFound(ex.GetPackageName()), ex, ConnectionExceptionCause.AuthenticationFailed);
				case SspiAuthenticationError.MissingCapability:
					throw new AdomdConnectionException(XmlaSR.Authentication_Sspi_PackageDoesntSupportCapability(ex.GetPackageName(), ex.GetMissingCapabilities().ToString()), ex, ConnectionExceptionCause.AuthenticationFailed);
				case SspiAuthenticationError.RequirementNotObtained:
					throw new AdomdConnectionException(XmlaSR.Authentication_Sspi_FlagNotEstablished(ex.GetRequirementsNotObtained().ToString()), ex, ConnectionExceptionCause.AuthenticationFailed);
				}
				throw new AdomdConnectionException(XmlaSR.Authentication_Failed, ex, ConnectionExceptionCause.AuthenticationFailed);
			}
			catch (Win32Exception ex2)
			{
				throw new AdomdConnectionException(XmlaSR.Authentication_Failed, ex2, ConnectionExceptionCause.AuthenticationFailed);
			}
			return securityContext;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000172F4 File Offset: 0x000154F4
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
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
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis:empty", "root", this.reader.Name));
				}
				this.reader.Skip();
				this.reader.ReadEndElement();
				this.reader.ReadEndElement();
				this.reader.ReadEndElement();
				XmlaClient.CheckEndElement(this.reader, "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
			}
			catch (XmlException ex2)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex2);
			}
			catch (IOException ex3)
			{
				this.CloseAll();
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex3);
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

		// Token: 0x0600038B RID: 907 RVA: 0x000176D8 File Offset: 0x000158D8
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
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
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", "urn:schemas-microsoft-com:xml-analysis:empty", "root", this.reader.Name));
				}
				this.reader.Skip();
				this.reader.ReadEndElement();
				this.reader.ReadEndElement();
				this.reader.ReadEndElement();
				XmlaClient.CheckEndElement(this.reader, "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
			}
			catch (XmlException ex2)
			{
				throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex2);
			}
			catch (IOException ex3)
			{
				this.CloseAll();
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex3);
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

		// Token: 0x0600038C RID: 908 RVA: 0x00017A00 File Offset: 0x00015C00
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
					throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
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
						throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex2);
					}
					text = string.Empty;
				}
				catch (XmlException ex3)
				{
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, ex3);
				}
				catch (IOException ex4)
				{
					this.CloseAll();
					throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex4);
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

		// Token: 0x0600038D RID: 909 RVA: 0x00017C90 File Offset: 0x00015E90
		private void WriteIfNonEmptyElement(string elementName, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.writer.WriteStartElement(elementName);
				this.writer.WriteString(value);
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00017CBD File Offset: 0x00015EBD
		private void WriteIfNonEmptyElement(StringBuilder sb, string elementName, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				sb.AppendFormat("<{0}>{1}</{0}>", elementName, value);
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00017CD5 File Offset: 0x00015ED5
		private void VerifyIfCanWrite()
		{
			this.VerifyIfCanWrite(false);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00017CDE File Offset: 0x00015EDE
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

		// Token: 0x06000391 RID: 913 RVA: 0x00017D10 File Offset: 0x00015F10
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
					throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
				}
				catch
				{
					this.HandleMessageCreationException();
					throw;
				}
			}
			return this.writer;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00017DEC File Offset: 0x00015FEC
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
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00017E60 File Offset: 0x00016060
		private void WriteCommandContent(string command)
		{
			try
			{
				this.writer.WriteRaw(command);
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00017EB4 File Offset: 0x000160B4
		private void WriteEndOfMessage()
		{
			this.WriteEndOfMessage(false);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00017EC0 File Offset: 0x000160C0
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

		// Token: 0x06000396 RID: 918 RVA: 0x00017F5C File Offset: 0x0001615C
		private Guid GetCorrelationProperty(IDictionary properties, object propertyKey)
		{
			if (properties[propertyKey] is AdomdProperty)
			{
				return (Guid)((AdomdProperty)properties[propertyKey]).Value;
			}
			return (Guid)properties[propertyKey];
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00017F90 File Offset: 0x00016190
		private void PopulateCommandProperties(ref IDictionary commandProperties, IDictionary requestProperties, bool isCommand)
		{
			if (!this.SupportsApplicationContext && commandProperties != null)
			{
				object obj;
				if (commandProperties is AdomdPropertyCollectionInternal)
				{
					obj = new XmlaPropertyKey("ApplicationContext", null);
				}
				else
				{
					obj = "ApplicationContext";
				}
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
				object obj2 = null;
				object obj3 = null;
				object obj4;
				if (commandProperties is AdomdPropertyCollectionInternal)
				{
					obj4 = new XmlaPropertyKey("DbpropMsmdActivityID", null);
					obj2 = new XmlaPropertyKey("DbpropMsmdRequestID", null);
					obj3 = new XmlaPropertyKey("DbpropMsmdCurrentActivityID", null);
				}
				else
				{
					obj4 = "DbpropMsmdActivityID";
					obj2 = "DbpropMsmdRequestID";
					obj3 = "DbpropMsmdCurrentActivityID";
				}
				object obj5 = null;
				if (requestProperties != null)
				{
					if (requestProperties is AdomdPropertyCollectionInternal)
					{
						obj5 = new XmlaPropertyKey("DbpropMsmdRequestID", null);
					}
					else
					{
						obj5 = "DbpropMsmdRequestID";
					}
				}
				if (isCommand)
				{
					if (!commandProperties.Contains(obj4))
					{
						commandProperties.Add(obj4, this.connInfo.ClientActivityID);
					}
					if (flag && !commandProperties.Contains(obj3))
					{
						commandProperties.Add(obj3, this.connInfo.CurrentActivityID);
					}
				}
				else
				{
					commandProperties[obj4] = this.connInfo.ClientActivityID;
					if (flag)
					{
						commandProperties[obj3] = this.connInfo.CurrentActivityID;
					}
				}
				try
				{
					this.xmlaStream.ActivityID = this.GetCorrelationProperty(commandProperties, obj4);
				}
				catch
				{
					this.xmlaStream.ActivityID = Guid.Empty;
				}
				if (flag)
				{
					try
					{
						this.xmlaStream.CurrentActivityID = this.GetCorrelationProperty(commandProperties, obj3);
					}
					catch
					{
						this.xmlaStream.CurrentActivityID = Guid.Empty;
					}
				}
				if (!this.connInfo.IsPaaSInfrastructure)
				{
					if (!commandProperties.Contains(obj2))
					{
						Guid guid = Guid.NewGuid();
						commandProperties[obj2] = guid;
						this.xmlaStream.RequestID = guid;
						return;
					}
				}
				else
				{
					try
					{
						Guid guid2 = Guid.NewGuid();
						if (requestProperties != null && requestProperties.Contains(obj5))
						{
							guid2 = this.GetCorrelationProperty(requestProperties, obj5);
						}
						else if (commandProperties.Contains(obj2))
						{
							guid2 = this.GetCorrelationProperty(commandProperties, obj2);
						}
						else if (this.connInfo.ConnectionActivityId != Guid.Empty)
						{
							guid2 = this.connInfo.ConnectionActivityId;
						}
						else
						{
							guid2 = this.connInfo.ClientActivityID;
						}
						commandProperties[obj2] = guid2;
						this.xmlaStream.RequestID = guid2;
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00018234 File Offset: 0x00016434
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
					throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Expected {0}:{1}, got {2}", "http://www.w3.org/2001/XMLSchema", "schema", xmlReader.Name));
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

		// Token: 0x06000399 RID: 921 RVA: 0x00018344 File Offset: 0x00016544
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

		// Token: 0x0600039A RID: 922 RVA: 0x00018450 File Offset: 0x00016650
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

		// Token: 0x0600039B RID: 923 RVA: 0x00018544 File Offset: 0x00016744
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

		// Token: 0x0600039C RID: 924 RVA: 0x000186FC File Offset: 0x000168FC
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

		// Token: 0x0600039D RID: 925 RVA: 0x0001882C File Offset: 0x00016A2C
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

		// Token: 0x0600039E RID: 926 RVA: 0x00018B58 File Offset: 0x00016D58
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

		// Token: 0x0600039F RID: 927 RVA: 0x00018BF8 File Offset: 0x00016DF8
		private void SetReadWriteTimeouts(int readTimeoutMilliseconds, int writeTimeoutMilliseconds)
		{
			this.tcpClient.ReceiveTimeout = ((writeTimeoutMilliseconds == -1 || writeTimeoutMilliseconds < 0) ? 0 : writeTimeoutMilliseconds);
			this.tcpClient.SendTimeout = ((readTimeoutMilliseconds == -1 || readTimeoutMilliseconds < 0) ? 0 : readTimeoutMilliseconds);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00018C28 File Offset: 0x00016E28
		private void CheckIfReaderDetached()
		{
			if (this.IsReaderDetached)
			{
				throw new InvalidOperationException(XmlaSR.ConnectionCannotBeUsedWhileXmlReaderOpened);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00018C40 File Offset: 0x00016E40
		private void WriteBeginSession()
		{
			this.writer.WriteStartElement("BeginSession", "urn:schemas-microsoft-com:xml-analysis");
			this.writer.WriteAttributeString("soap", "mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
			this.writer.WriteEndElement();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00018C8C File Offset: 0x00016E8C
		private void WriteBeginGetSessionToken()
		{
			this.writer.WriteStartElement("BeginGetSessionToken", "http://schemas.microsoft.com/analysisservices/2003/xmla");
			this.writer.WriteAttributeString("soap", "mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
			this.writer.WriteEndElement();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00018CD8 File Offset: 0x00016ED8
		private void WriteSessionId()
		{
			this.writer.WriteStartElement("XA", "Session", "urn:schemas-microsoft-com:xml-analysis");
			this.writer.WriteAttributeString("soap", "mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
			this.writer.WriteAttributeString("SessionId", this.SessionID);
			this.writer.WriteEndElement();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00018D3F File Offset: 0x00016F3F
		private void WriteVersionHeader()
		{
			this.writer.WriteStartElement("Version", "http://schemas.microsoft.com/analysisservices/2003/engine/2");
			this.writer.WriteAttributeString("Sequence", "922");
			this.writer.WriteEndElement();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00018D78 File Offset: 0x00016F78
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
							throw new AdomdUnknownResponseException(XmlaSR.UnknownServerResponseFormat, string.Format(CultureInfo.InvariantCulture, "Unexpected value of MustUnderstand attribute: '{0}'", attribute));
						}
						reader.Skip();
					}
				}
				reader.ReadEndElement();
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00018EAC File Offset: 0x000170AC
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

		// Token: 0x060003A7 RID: 935 RVA: 0x00018F44 File Offset: 0x00017144
		internal XmlReader DiscoverWithCreateSession(string discoverType, ListDictionary properties, bool sendNamespacesCompatibility)
		{
			this.CheckConnection();
			try
			{
				this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Discover\"", true, sendNamespacesCompatibility, false);
				this.WriteStartDiscover(discoverType, null);
				this.WriteRestrictions(null);
				this.WriteEndDiscover(properties);
				this.EndMessage();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			return XmlaClient.GetReaderToReturnToPublic(this.SendMessage(true, true, sendNamespacesCompatibility));
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00018FCC File Offset: 0x000171CC
		internal static void ReadExecuteResponse(XmlReader reader)
		{
			XmlaResultCollection xmlaResultCollection = new XmlaResultCollection();
			XmlaResult xmlaResult = new XmlaResult();
			XmlaClient.ReadExecuteResponsePrivate(reader, true, xmlaResultCollection, xmlaResult);
			if (xmlaResultCollection.ContainsErrors)
			{
				throw XmlaResultCollection.ExceptionOnError(xmlaResultCollection);
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00019000 File Offset: 0x00017200
		internal static void ReadMultipleResults(XmlReader reader)
		{
			XmlaResultCollection xmlaResultCollection = new XmlaResultCollection();
			XmlaClient.ReadMultipleResults(reader, xmlaResultCollection, true);
			if (xmlaResultCollection.ContainsErrors)
			{
				throw XmlaResultCollection.ExceptionOnError(xmlaResultCollection);
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001902C File Offset: 0x0001722C
		internal XmlReader ExecuteStream(Stream stream, IDictionary connectionProperties, IDictionary commandProperties, IDataParameterCollection parameters, bool appendStatementTags)
		{
			this.CheckConnection();
			try
			{
				if (!this.captureXml)
				{
					this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
					this.WriteStartCommand(ref commandProperties);
					if (appendStatementTags)
					{
						this.writer.WriteStartElement("Statement");
					}
					StreamReader streamReader = new StreamReader(stream);
					char[] array = new char[4096];
					int num;
					while ((num = streamReader.Read(array, 0, 4096)) > 0)
					{
						this.writer.WriteRaw(array, 0, num);
					}
					if (appendStatementTags)
					{
						this.writer.WriteEndElement();
					}
					this.WriteEndCommand(connectionProperties, commandProperties, parameters);
					this.EndMessage();
				}
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			if (!this.captureXml)
			{
				return XmlaClient.GetReaderToReturnToPublic(this.SendMessage(true, false, false));
			}
			return null;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00019118 File Offset: 0x00017318
		internal XmlReader Discover(string requestType, ListDictionary properties, IDictionary restrictions)
		{
			return XmlaClient.GetReaderToReturnToPublic(this.Discover(requestType, null, properties, restrictions, false, null));
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001912B File Offset: 0x0001732B
		internal XmlReader Discover(string requestType, string requestNamespace, ListDictionary properties, IDictionary restrictions)
		{
			return this.Discover(requestType, requestNamespace, properties, restrictions, null);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00019139 File Offset: 0x00017339
		internal XmlReader Discover(string requestType, string requestNamespace, ListDictionary properties, IDictionary restrictions, IDictionary requestProperties)
		{
			return XmlaClient.GetReaderToReturnToPublic(this.Discover(requestType, requestNamespace, properties, restrictions, false, requestProperties));
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001914E File Offset: 0x0001734E
		internal XmlReader Discover(string requestType, ListDictionary properties, IDictionary restrictions, bool sendNamespacesCompatibility)
		{
			return XmlaClient.GetReaderToReturnToPublic(this.Discover(requestType, null, properties, restrictions, sendNamespacesCompatibility, null));
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00019162 File Offset: 0x00017362
		internal static bool IsTypeSupportedForParameters(Type type)
		{
			return typeof(IDataReader).IsAssignableFrom(type) || typeof(DataTable).IsAssignableFrom(type) || XmlaTypeHelper.IsXmlaSupportedType(type);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00019190 File Offset: 0x00017390
		internal XmlReader ExecuteStatement(string statement, IDictionary connectionProperties, IDictionary commandProperties, IDataParameterCollection parameters, bool isMdx)
		{
			this.CheckConnection();
			try
			{
				this.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				this.WriteStartCommand(ref commandProperties);
				if (isMdx)
				{
					this.WriteStatement(statement);
				}
				else
				{
					this.WriteCommandContent(statement);
				}
				this.WriteEndCommand(connectionProperties, commandProperties, parameters);
				this.EndMessage();
			}
			catch (IOException ex)
			{
				this.CloseAll();
				throw new AdomdConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				this.HandleMessageCreationException();
				throw;
			}
			return XmlaClient.GetReaderToReturnToPublic(this.SendMessage(true, false, false));
		}

		// Token: 0x0400025A RID: 602
		protected const string ActivityIDPropertyName = "DbpropMsmdActivityID";

		// Token: 0x0400025B RID: 603
		protected const string RequestIDPropertyName = "DbpropMsmdRequestID";

		// Token: 0x0400025C RID: 604
		protected const string CurrentActivityIDPropertyName = "DbpropMsmdCurrentActivityID";

		// Token: 0x0400025D RID: 605
		protected const string RequestMemoryLimitPropertyName = "DbPropmsmdRequestMemoryLimit";

		// Token: 0x0400025E RID: 606
		protected const string ApplicationContextPropertyName = "ApplicationContext";

		// Token: 0x0400025F RID: 607
		internal const int ReadStreamBufferSize = 4096;

		// Token: 0x04000260 RID: 608
		internal const int MaxHttpConnections = 1000;

		// Token: 0x04000261 RID: 609
		internal const string LocaleIdentifierName = "LocaleIdentifier";

		// Token: 0x04000262 RID: 610
		private const string PropertyListName = "PropertyList";

		// Token: 0x04000263 RID: 611
		private const string VersionSequenceNumber = "922";

		// Token: 0x04000264 RID: 612
		protected internal static readonly TraceSwitch TRACESWITCH = new TraceSwitch(typeof(XmlaClient).FullName, typeof(XmlaClient).FullName, TraceLevel.Off.ToString());

		// Token: 0x04000265 RID: 613
		private static bool hasRuntimeSupportForBinaryXml = XmlaClient.HasRuntimeSupportForBinaryXmlImpl();

		// Token: 0x04000266 RID: 614
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

		// Token: 0x04000267 RID: 615
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

		// Token: 0x04000268 RID: 616
		private static readonly byte[] preDatabaseID = new byte[]
		{
			247, 240, 10, 68, 0, 97, 0, 116, 0, 97,
			0, 98, 0, 97, 0, 115, 0, 101, 0, 73,
			0, 68, 0, 239, 14, 0, 18, 248, 14
		};

		// Token: 0x04000269 RID: 617
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

		// Token: 0x0400026A RID: 618
		private static readonly byte[] preAbfContent = new byte[]
		{
			247, 240, 4, 68, 0, 97, 0, 116, 0, 97,
			0, 239, 14, 0, 21, 248, 16, 240, 9, 68,
			0, 97, 0, 116, 0, 97, 0, 66, 0, 108,
			0, 111, 0, 99, 0, 107, 0, 239, 14, 0,
			22, 248, 17
		};

		// Token: 0x0400026B RID: 619
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

		// Token: 0x0400026C RID: 620
		private static readonly byte[] localeEnd = new byte[] { 247 };

		// Token: 0x0400026D RID: 621
		private static readonly byte[] activityIDBegin = new byte[]
		{
			240, 20, 68, 0, 98, 0, 112, 0, 114, 0,
			111, 0, 112, 0, 77, 0, 115, 0, 109, 0,
			100, 0, 65, 0, 99, 0, 116, 0, 105, 0,
			118, 0, 105, 0, 116, 0, 121, 0, 73, 0,
			68, 0, 239, 5, 0, 26, 248, 21
		};

		// Token: 0x0400026E RID: 622
		private static readonly byte[] activityIDEnd = new byte[] { 247 };

		// Token: 0x0400026F RID: 623
		private static readonly byte[] requestIDBegin = new byte[]
		{
			240, 19, 68, 0, 98, 0, 112, 0, 114, 0,
			111, 0, 112, 0, 77, 0, 115, 0, 109, 0,
			100, 0, 82, 0, 101, 0, 113, 0, 117, 0,
			101, 0, 115, 0, 116, 0, 73, 0, 68, 0,
			239, 5, 0, 27, 248, 22
		};

		// Token: 0x04000270 RID: 624
		private static readonly byte[] requestIDEnd = new byte[] { 247 };

		// Token: 0x04000271 RID: 625
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

		// Token: 0x04000272 RID: 626
		private static readonly byte[] currentActivityIDEnd = new byte[] { 247 };

		// Token: 0x04000273 RID: 627
		private static readonly byte[] endContent = new byte[] { 247, 247, 247, 247, 247 };

		// Token: 0x04000274 RID: 628
		internal XmlaStream xmlaStream;

		// Token: 0x04000275 RID: 629
		private protected XmlWriter writer;

		// Token: 0x04000276 RID: 630
		private protected XmlReader reader;

		// Token: 0x04000277 RID: 631
		private protected bool captureXml;

		// Token: 0x04000278 RID: 632
		private readonly object lockForCloseAll = new object();

		// Token: 0x04000279 RID: 633
		private readonly IConnectivityOwner owner;

		// Token: 0x0400027A RID: 634
		private string sessionID;

		// Token: 0x0400027B RID: 635
		private Guid activityID = Guid.Empty;

		// Token: 0x0400027C RID: 636
		private bool connected;

		// Token: 0x0400027D RID: 637
		private StringCollection captureLog;

		// Token: 0x0400027E RID: 638
		private BufferedStream networkStream;

		// Token: 0x0400027F RID: 639
		private TcpClient tcpClient;

		// Token: 0x04000280 RID: 640
		private ConnectionInfo connInfo;

		// Token: 0x04000281 RID: 641
		private StringWriter logEntry;

		// Token: 0x04000282 RID: 642
		private NamespacesMgr namespacesManager;

		// Token: 0x04000283 RID: 643
		private NameTable nameTable;

		// Token: 0x04000284 RID: 644
		private ConnectionState connectionState;

		// Token: 0x04000285 RID: 645
		private bool userOpened;

		// Token: 0x04000286 RID: 646
		private bool supportsActivityIDAndRequestID;

		// Token: 0x04000287 RID: 647
		private bool supportsCurrentActivityID;

		// Token: 0x04000288 RID: 648
		private bool supportsApplicationContext;

		// Token: 0x04000289 RID: 649
		private bool isCompressionEnabled;
	}
}
