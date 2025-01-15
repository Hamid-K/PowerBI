using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public class RSException : Exception
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x000068C2 File Offset: 0x00004AC2
		public override string Message
		{
			get
			{
				return RSException.TrimExtraLength(base.Message);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000068D0 File Offset: 0x00004AD0
		public override string ToString()
		{
			if (this.m_toString == null)
			{
				this.m_toString = RSException.TrimExtraLength(base.ToString());
				Type type = base.GetType();
				this.m_toString = ((type != null) ? type.ToString() : null) + ": " + this.Message;
				for (Exception ex = base.InnerException; ex != null; ex = ex.InnerException)
				{
					string[] array = new string[5];
					array[0] = this.m_toString;
					array[1] = " ---> ";
					int num = 2;
					Type type2 = ex.GetType();
					array[num] = ((type2 != null) ? type2.ToString() : null);
					array[3] = ": ";
					array[4] = ex.Message;
					this.m_toString = string.Concat(array);
				}
				this.m_toString = RSException.TrimExtraLength(this.m_toString);
			}
			return this.m_toString;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00006993 File Offset: 0x00004B93
		public RSException(ErrorCode errorCode, string localizedMessage, Exception innerException, RSTrace tracer, string additionalTraceMessage, params object[] exceptionData)
			: this(errorCode, localizedMessage, innerException, tracer, additionalTraceMessage, TraceLevel.Error, exceptionData)
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000069A8 File Offset: 0x00004BA8
		public RSException(ErrorCode errorCode, string localizedMessage, Exception innerException, RSTrace tracer, string additionalTraceMessage, TraceLevel traceLevel, params object[] exceptionData)
		{
			this.m_ActorUri = "";
			this.m_ErrorCode = ErrorCode.rsInternalError;
			this.m_ProductName = "change this";
			this.m_ProductVersion = "1.0";
			this.m_ProductLocaleID = 1033;
			this.m_CountryLocaleID = 1033;
			this.m_traceLevel = TraceLevel.Error;
			base..ctor(StringUtils.RemoveControlCharacters(localizedMessage), innerException);
			this.m_ErrorCode = errorCode;
			this.m_ProductLocaleID = CultureInfo.CurrentCulture.LCID;
			this.m_CountryLocaleID = CultureInfo.InstalledUICulture.LCID;
			this.m_OS = Microsoft.ReportingServices.Diagnostics.Utilities.OperatingSystem.OsIndependent;
			this.m_AdditionalTraceMessage = additionalTraceMessage;
			this.m_tracer = tracer;
			this.m_traceLevel = traceLevel;
			this.m_exceptionData = exceptionData;
			this.Trace();
			this.OnExceptionCreated();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00006A64 File Offset: 0x00004C64
		public RSException(RSException inner)
		{
			this.m_ActorUri = "";
			this.m_ErrorCode = ErrorCode.rsInternalError;
			this.m_ProductName = "change this";
			this.m_ProductVersion = "1.0";
			this.m_ProductLocaleID = 1033;
			this.m_CountryLocaleID = 1033;
			this.m_traceLevel = TraceLevel.Error;
			base..ctor(StringUtils.RemoveControlCharacters(inner.Message), inner);
			this.m_ErrorCode = inner.m_ErrorCode;
			this.m_ActorUri = inner.m_ActorUri;
			this.m_ProductName = inner.m_ProductName;
			this.m_ProductVersion = inner.m_ProductVersion;
			this.m_ProductLocaleID = inner.m_ProductLocaleID;
			this.m_CountryLocaleID = inner.m_CountryLocaleID;
			this.m_OS = inner.m_OS;
			this.m_AdditionalTraceMessage = StringUtils.RemoveControlCharacters(inner.m_AdditionalTraceMessage);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00006B30 File Offset: 0x00004D30
		public void Trace()
		{
			if (this.m_tracer == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (this.TraceFullException)
			{
				stringBuilder.AppendFormat("Throwing {0}: {1}, {2};", base.GetType().FullName, this.m_AdditionalTraceMessage, base.ToString());
			}
			else
			{
				stringBuilder.AppendFormat("Throwing {0}: {1}, {2};", base.GetType().FullName, this.m_AdditionalTraceMessage, base.Message);
			}
			this.m_tracer.TraceException(this.m_traceLevel, (3000 > stringBuilder.Length) ? stringBuilder.ToString() : stringBuilder.ToString(0, 3000));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00006BCE File Offset: 0x00004DCE
		public void SetExceptionProperties(string actorUri, string productName, string productVersion)
		{
			this.m_ActorUri = actorUri;
			this.m_ProductName = productName;
			this.m_ProductVersion = productVersion;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00006BE8 File Offset: 0x00004DE8
		public string ExceptionLevelHelpLink
		{
			get
			{
				return this.CreateHelpLink(typeof(ErrorStrings).FullName, this.Code.ToString());
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00006C20 File Offset: 0x00004E20
		public bool SkipTopLevelMessage
		{
			get
			{
				RSException ex = base.InnerException as RSException;
				return ex != null && ex.Code == this.Code;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00006C4C File Offset: 0x00004E4C
		public ErrorCode Code
		{
			get
			{
				return this.m_ErrorCode;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00006C54 File Offset: 0x00004E54
		public string ActorUri
		{
			get
			{
				return this.m_ActorUri;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00006C5C File Offset: 0x00004E5C
		protected RSException(SerializationInfo info, StreamingContext context)
		{
			this.m_ActorUri = "";
			this.m_ErrorCode = ErrorCode.rsInternalError;
			this.m_ProductName = "change this";
			this.m_ProductVersion = "1.0";
			this.m_ProductLocaleID = 1033;
			this.m_CountryLocaleID = 1033;
			this.m_traceLevel = TraceLevel.Error;
			base..ctor(info, context);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00006CB8 File Offset: 0x00004EB8
		protected RSException(SerializationInfo info, StreamingContext context, ErrorCode errorCode)
		{
			this.m_ActorUri = "";
			this.m_ErrorCode = ErrorCode.rsInternalError;
			this.m_ProductName = "change this";
			this.m_ProductVersion = "1.0";
			this.m_ProductLocaleID = 1033;
			this.m_CountryLocaleID = 1033;
			this.m_traceLevel = TraceLevel.Error;
			base..ctor(info, context);
			this.m_ErrorCode = errorCode;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00006D1A File Offset: 0x00004F1A
		internal List<RSException.AdditionalMessage> AdditionalMessages
		{
			get
			{
				return this.GetAdditionalMessages();
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00006D22 File Offset: 0x00004F22
		protected virtual List<RSException.AdditionalMessage> GetAdditionalMessages()
		{
			return null;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00006D25 File Offset: 0x00004F25
		protected virtual bool TraceFullException
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00006D28 File Offset: 0x00004F28
		protected virtual XmlNode AddMoreInformationForThis(XmlDocument doc, XmlNode parent, StringBuilder errorMsgBuilder)
		{
			return this.AddMoreInformationForException(doc, parent, this, errorMsgBuilder);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00006D34 File Offset: 0x00004F34
		private XmlNode AddMoreInformationForException(XmlDocument doc, XmlNode parent, Exception e, StringBuilder errorMsgBuilder)
		{
			XmlNode xmlNode = RSException.CreateMoreInfoNode(e.Source, doc, parent);
			if (xmlNode != null)
			{
				string text = null;
				RSException ex = e as RSException;
				if (ex != null)
				{
					text = ex.Code.ToString();
				}
				string text2 = this.CreateHelpLink(typeof(ErrorStrings).FullName, text);
				string text3 = RSException.AddMessageToMoreInfoNode(doc, xmlNode, text, e.Message, text2);
				RSException.BuildExceptionMessage(e, text3, errorMsgBuilder);
			}
			return xmlNode;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00006DA8 File Offset: 0x00004FA8
		protected static XmlNode CreateMoreInfoNode(string source, XmlDocument doc, XmlNode parent)
		{
			XmlNode xmlNode = SoapUtil.CreateMoreInfoNode(doc);
			XmlNode xmlNode2 = SoapUtil.CreateMoreInfoSourceNode(doc);
			xmlNode2.InnerText = source;
			xmlNode.AppendChild(xmlNode2);
			if (parent != null)
			{
				parent.AppendChild(xmlNode);
			}
			return xmlNode;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00006DE0 File Offset: 0x00004FE0
		protected virtual void AddWarnings(XmlDocument doc, XmlNode parent)
		{
			RSException ex = base.InnerException as RSException;
			if (ex != null)
			{
				ex.AddWarnings(doc, parent);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00006E04 File Offset: 0x00005004
		protected static string AddMessageToMoreInfoNode(XmlDocument doc, XmlNode moreInfoNode, string errCode, string message, string helpLink)
		{
			XmlNode xmlNode = SoapUtil.CreateMoreInfoMessageNode(doc);
			string text = SoapUtil.RemoveInvalidXmlChars(message);
			xmlNode.InnerText = text;
			if (errCode != null)
			{
				XmlAttribute xmlAttribute = SoapUtil.CreateErrorCodeAttr(doc);
				xmlAttribute.Value = errCode;
				xmlNode.Attributes.Append(xmlAttribute);
				XmlAttribute xmlAttribute2 = SoapUtil.CreateHelpLinkTagAttr(doc);
				xmlAttribute2.Value = helpLink;
				xmlNode.Attributes.Append(xmlAttribute2);
			}
			moreInfoNode.AppendChild(xmlNode);
			return text;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00006E6C File Offset: 0x0000506C
		protected static void AddWarningNode(XmlDocument doc, XmlNode parent, string code, string severity, string objectName, string objectType, string message)
		{
			XmlNode xmlNode = SoapUtil.CreateWarningNode(doc);
			XmlNode xmlNode2 = SoapUtil.CreateWarningCodeNode(doc);
			xmlNode2.InnerText = code;
			xmlNode.AppendChild(xmlNode2);
			XmlNode xmlNode3 = SoapUtil.CreateWarningSeverityNode(doc);
			xmlNode3.InnerText = severity;
			xmlNode.AppendChild(xmlNode3);
			XmlNode xmlNode4 = SoapUtil.CreateWarningObjectNameNode(doc);
			xmlNode4.InnerText = objectName;
			xmlNode.AppendChild(xmlNode4);
			XmlNode xmlNode5 = SoapUtil.CreateWarningObjectTypeNode(doc);
			xmlNode5.InnerText = objectType;
			xmlNode.AppendChild(xmlNode5);
			XmlNode xmlNode6 = SoapUtil.CreateWarningMessageNode(doc);
			xmlNode6.InnerText = SoapUtil.RemoveInvalidXmlChars(message);
			xmlNode.AppendChild(xmlNode6);
			parent.AppendChild(xmlNode);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00006F04 File Offset: 0x00005104
		protected string CreateHelpLink(string messageSource, string id)
		{
			return string.Format(CultureInfo.CurrentCulture, "https://go.microsoft.com/fwlink/?LinkId=20476&EvtSrc={0}&EvtID={1}&ProdName=Microsoft%20SQL%20Server%20Reporting%20Services&ProdVer={2}", messageSource, id, this.m_ProductVersion);
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00006F1D File Offset: 0x0000511D
		public static string ErrorNotVisibleOnRemoteBrowsers
		{
			get
			{
				return ErrorStrings.rsErrorNotVisibleToRemoteBrowsers;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x00006F24 File Offset: 0x00005124
		public string AdditionalTraceMessage
		{
			get
			{
				return this.m_AdditionalTraceMessage;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00006F2C File Offset: 0x0000512C
		public object[] ExceptionData
		{
			get
			{
				return this.m_exceptionData;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00006F34 File Offset: 0x00005134
		private static void BuildExceptionMessage(Exception e, string filteredMsg, StringBuilder errorMsgBuilder)
		{
			if (e != null)
			{
				errorMsgBuilder.Append(" ---> " + e.GetType().FullName);
				if (!string.IsNullOrEmpty(filteredMsg))
				{
					errorMsgBuilder.Append(": " + filteredMsg);
				}
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00006F70 File Offset: 0x00005170
		internal bool ContainsErrorCode(ErrorCode code)
		{
			for (RSException ex = this; ex != null; ex = ex.InnerException as RSException)
			{
				if (code == ex.Code)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00006F9C File Offset: 0x0000519C
		private static string TrimExtraLength(string input)
		{
			return input.Substring(0, Math.Min(3000, input.Length));
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00006FB5 File Offset: 0x000051B5
		internal string ProductName
		{
			get
			{
				return this.m_ProductName;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x00006FBD File Offset: 0x000051BD
		internal string ProductVersion
		{
			get
			{
				return this.m_ProductVersion;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00006FC5 File Offset: 0x000051C5
		internal string OperatingSystem
		{
			get
			{
				return this.m_OS.ToString();
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x00006FD8 File Offset: 0x000051D8
		internal int CountryLocaleID
		{
			get
			{
				return this.m_CountryLocaleID;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00006FE0 File Offset: 0x000051E0
		internal XmlNode DetailsAsXml(bool enableRemoteErrors, out StringBuilder errorMsgBuilder)
		{
			string text = SoapUtil.RemoveInvalidXmlChars(this.Message);
			errorMsgBuilder = new StringBuilder();
			errorMsgBuilder.Append(text);
			return this.ToXml(new XmlDocument
			{
				XmlResolver = null
			}, enableRemoteErrors, text, errorMsgBuilder);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00007020 File Offset: 0x00005220
		protected XmlNode ToXml(XmlDocument doc, bool enableRemoteErrors, string detailedMsg, StringBuilder errorMsgBuilder)
		{
			XmlNode xmlNode = SoapUtil.CreateExceptionDetailsNode(doc, this.Code.ToString(), detailedMsg, this.ExceptionLevelHelpLink, this.m_ProductName, this.m_ProductVersion, this.m_ProductLocaleID, this.m_OS.ToString(), this.m_CountryLocaleID);
			this.AddMoreInformation(doc, xmlNode, enableRemoteErrors, errorMsgBuilder);
			this.AddWarningsInternal(doc, xmlNode);
			return xmlNode;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000708C File Offset: 0x0000528C
		internal void AddMoreInformation(XmlDocument doc, XmlNode parent, bool enableRemoteErrors, StringBuilder errorMsgBuilder)
		{
			Exception ex = this;
			XmlNode xmlNode = parent;
			if (this.SkipTopLevelMessage)
			{
				ex = base.InnerException;
			}
			while (ex != null)
			{
				RSException ex2 = ex as RSException;
				if (ex2 != null)
				{
					xmlNode = ex2.AddMoreInformationForThis(doc, xmlNode, errorMsgBuilder);
				}
				else
				{
					if (!enableRemoteErrors)
					{
						Exception ex3 = new Exception(RSException.ErrorNotVisibleOnRemoteBrowsers);
						xmlNode = this.AddMoreInformationForException(doc, xmlNode, ex3, errorMsgBuilder);
						return;
					}
					xmlNode = this.AddMoreInformationForException(doc, xmlNode, ex, errorMsgBuilder);
				}
				ex = ex.InnerException;
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000070FC File Offset: 0x000052FC
		internal void AddWarningsInternal(XmlDocument doc, XmlNode parent)
		{
			XmlNode xmlNode = SoapUtil.CreateWarningNode(doc);
			parent.AppendChild(xmlNode);
			this.AddWarnings(doc, xmlNode);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000420 RID: 1056 RVA: 0x00007120 File Offset: 0x00005320
		// (remove) Token: 0x06000421 RID: 1057 RVA: 0x00007154 File Offset: 0x00005354
		public static event EventHandler<RSExceptionCreatedEventArgs> ExceptionCreated;

		// Token: 0x06000422 RID: 1058 RVA: 0x00007188 File Offset: 0x00005388
		private void OnExceptionCreated()
		{
			EventHandler<RSExceptionCreatedEventArgs> exceptionCreated = RSException.ExceptionCreated;
			if (exceptionCreated != null)
			{
				RSExceptionCreatedEventArgs rsexceptionCreatedEventArgs = new RSExceptionCreatedEventArgs(this);
				exceptionCreated(this, rsexceptionCreatedEventArgs);
			}
		}

		// Token: 0x0400001A RID: 26
		private const int ExceptionMessageLimit = 3000;

		// Token: 0x0400001B RID: 27
		private string m_toString;

		// Token: 0x0400001C RID: 28
		private string m_ActorUri;

		// Token: 0x0400001D RID: 29
		private ErrorCode m_ErrorCode;

		// Token: 0x0400001E RID: 30
		private string m_ProductName;

		// Token: 0x0400001F RID: 31
		private string m_ProductVersion;

		// Token: 0x04000020 RID: 32
		private int m_ProductLocaleID;

		// Token: 0x04000021 RID: 33
		private int m_CountryLocaleID;

		// Token: 0x04000022 RID: 34
		private OperatingSystem m_OS;

		// Token: 0x04000023 RID: 35
		private string m_AdditionalTraceMessage;

		// Token: 0x04000024 RID: 36
		private RSTrace m_tracer;

		// Token: 0x04000025 RID: 37
		private TraceLevel m_traceLevel;

		// Token: 0x04000026 RID: 38
		private object[] m_exceptionData;

		// Token: 0x020000D8 RID: 216
		public sealed class AdditionalMessage
		{
			// Token: 0x1700020A RID: 522
			// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000086B0 File Offset: 0x000068B0
			// (set) Token: 0x060004B6 RID: 1206 RVA: 0x000086B8 File Offset: 0x000068B8
			internal string Code { get; private set; }

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000086C1 File Offset: 0x000068C1
			// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000086C9 File Offset: 0x000068C9
			internal string Severity { get; private set; }

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000086D2 File Offset: 0x000068D2
			// (set) Token: 0x060004BA RID: 1210 RVA: 0x000086DA File Offset: 0x000068DA
			internal string Message { get; private set; }

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x060004BB RID: 1211 RVA: 0x000086E3 File Offset: 0x000068E3
			// (set) Token: 0x060004BC RID: 1212 RVA: 0x000086EB File Offset: 0x000068EB
			internal string ObjectType { get; private set; }

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x060004BD RID: 1213 RVA: 0x000086F4 File Offset: 0x000068F4
			// (set) Token: 0x060004BE RID: 1214 RVA: 0x000086FC File Offset: 0x000068FC
			internal string ObjectName { get; private set; }

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x060004BF RID: 1215 RVA: 0x00008705 File Offset: 0x00006905
			// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000870D File Offset: 0x0000690D
			internal string PropertyName { get; private set; }

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00008716 File Offset: 0x00006916
			// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000871E File Offset: 0x0000691E
			internal string[] AffectedItems { get; private set; }

			// Token: 0x060004C3 RID: 1219 RVA: 0x00008727 File Offset: 0x00006927
			internal AdditionalMessage(string code, string severity, string message, string objectType = null, string objectName = null, string propertyName = null, string[] affectedItems = null)
			{
				this.Code = code;
				this.Severity = severity;
				this.Message = message;
				this.ObjectType = objectType;
				this.ObjectName = objectName;
				this.PropertyName = propertyName;
				this.AffectedItems = affectedItems;
			}
		}
	}
}
