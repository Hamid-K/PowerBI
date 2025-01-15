using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Internal.Fakeables;
using NLog.Layouts;
using NLog.Targets;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D1 RID: 209
	[LayoutRenderer("log4jxmlevent")]
	[ThreadSafe]
	[MutableUnsafe]
	public class Log4JXmlEventLayoutRenderer : LayoutRenderer, IUsesStackTrace, IIncludeContext
	{
		// Token: 0x06000CA0 RID: 3232 RVA: 0x000203CD File Offset: 0x0001E5CD
		public Log4JXmlEventLayoutRenderer()
			: this(LogFactory.CurrentAppDomain)
		{
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000203DC File Offset: 0x0001E5DC
		public Log4JXmlEventLayoutRenderer(IAppDomain appDomain)
		{
			this.IncludeNLogData = true;
			this.NdcItemSeparator = " ";
			this.NdlcItemSeparator = " ";
			this.AppInfo = string.Format(CultureInfo.InvariantCulture, "{0}({1})", new object[]
			{
				appDomain.FriendlyName,
				ProcessIDHelper.Instance.CurrentProcessID
			});
			this.Parameters = new List<NLogViewerParameterInfo>();
			try
			{
				this._machineName = EnvironmentHelper.GetMachineName();
				if (string.IsNullOrEmpty(this._machineName))
				{
					InternalLogger.Info("MachineName is not available.");
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Error getting machine name.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
				this._machineName = string.Empty;
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000204A4 File Offset: 0x0001E6A4
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			this._xmlWriterSettings = new XmlWriterSettings
			{
				Indent = this.IndentXml,
				ConformanceLevel = ConformanceLevel.Fragment,
				IndentChars = "  "
			};
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000204D5 File Offset: 0x0001E6D5
		// (set) Token: 0x06000CA4 RID: 3236 RVA: 0x000204DD File Offset: 0x0001E6DD
		[DefaultValue(true)]
		public bool IncludeNLogData { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000204E6 File Offset: 0x0001E6E6
		// (set) Token: 0x06000CA6 RID: 3238 RVA: 0x000204EE File Offset: 0x0001E6EE
		public bool IndentXml { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x000204F7 File Offset: 0x0001E6F7
		// (set) Token: 0x06000CA8 RID: 3240 RVA: 0x000204FF File Offset: 0x0001E6FF
		public string AppInfo { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x00020508 File Offset: 0x0001E708
		// (set) Token: 0x06000CAA RID: 3242 RVA: 0x00020510 File Offset: 0x0001E710
		public bool IncludeCallSite { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x00020519 File Offset: 0x0001E719
		// (set) Token: 0x06000CAC RID: 3244 RVA: 0x00020521 File Offset: 0x0001E721
		public bool IncludeSourceInfo { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0002052A File Offset: 0x0001E72A
		// (set) Token: 0x06000CAE RID: 3246 RVA: 0x00020532 File Offset: 0x0001E732
		public bool IncludeMdc { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002053B File Offset: 0x0001E73B
		// (set) Token: 0x06000CB0 RID: 3248 RVA: 0x00020543 File Offset: 0x0001E743
		public bool IncludeMdlc { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002054C File Offset: 0x0001E74C
		// (set) Token: 0x06000CB2 RID: 3250 RVA: 0x00020554 File Offset: 0x0001E754
		public bool IncludeNdlc { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0002055D File Offset: 0x0001E75D
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x00020565 File Offset: 0x0001E765
		[DefaultValue(" ")]
		public string NdlcItemSeparator { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002056E File Offset: 0x0001E76E
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x00020576 File Offset: 0x0001E776
		public bool IncludeAllProperties { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002057F File Offset: 0x0001E77F
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x00020587 File Offset: 0x0001E787
		public bool IncludeNdc { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00020590 File Offset: 0x0001E790
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x00020598 File Offset: 0x0001E798
		[DefaultValue(" ")]
		public string NdcItemSeparator { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x000205A1 File Offset: 0x0001E7A1
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x000205A9 File Offset: 0x0001E7A9
		public Layout LoggerName { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x000205B2 File Offset: 0x0001E7B2
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				if (this.IncludeSourceInfo)
				{
					return StackTraceUsage.WithSource;
				}
				if (this.IncludeCallSite)
				{
					return StackTraceUsage.WithoutSource;
				}
				return StackTraceUsage.None;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x000205C9 File Offset: 0x0001E7C9
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x000205D1 File Offset: 0x0001E7D1
		internal IList<NLogViewerParameterInfo> Parameters { get; set; }

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000205DA File Offset: 0x0001E7DA
		internal void AppendToStringBuilder(StringBuilder sb, LogEventInfo logEvent)
		{
			this.Append(sb, logEvent);
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000205E4 File Offset: 0x0001E7E4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, this._xmlWriterSettings))
			{
				xmlWriter.WriteStartElement("log4j", "event", Log4JXmlEventLayoutRenderer.dummyNamespace);
				xmlWriter.WriteAttributeSafeString("xmlns", "nlog", null, Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
				xmlWriter.WriteAttributeSafeString("logger", (this.LoggerName != null) ? this.LoggerName.Render(logEvent) : logEvent.LoggerName);
				xmlWriter.WriteAttributeSafeString("level", logEvent.Level.Name.ToUpperInvariant());
				xmlWriter.WriteAttributeSafeString("timestamp", Convert.ToString((long)(logEvent.TimeStamp.ToUniversalTime() - Log4JXmlEventLayoutRenderer.log4jDateBase).TotalMilliseconds, CultureInfo.InvariantCulture));
				xmlWriter.WriteAttributeSafeString("thread", AsyncHelpers.GetManagedThreadId().ToString(CultureInfo.InvariantCulture));
				xmlWriter.WriteElementSafeString("log4j", "message", Log4JXmlEventLayoutRenderer.dummyNamespace, logEvent.FormattedMessage);
				if (logEvent.Exception != null)
				{
					xmlWriter.WriteElementSafeString("log4j", "throwable", Log4JXmlEventLayoutRenderer.dummyNamespace, logEvent.Exception.ToString());
				}
				this.AppendNdc(xmlWriter);
				Log4JXmlEventLayoutRenderer.AppendException(logEvent, xmlWriter);
				this.AppendCallSite(logEvent, xmlWriter);
				this.AppendProperties(xmlWriter);
				this.AppendMdlc(xmlWriter);
				if (this.IncludeAllProperties)
				{
					this.AppendProperties("log4j", xmlWriter, logEvent);
				}
				this.AppendParameters(logEvent, xmlWriter);
				xmlWriter.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
				xmlWriter.WriteAttributeSafeString("name", "log4japp");
				xmlWriter.WriteAttributeSafeString("value", this.AppInfo);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
				xmlWriter.WriteAttributeSafeString("name", "log4jmachinename");
				xmlWriter.WriteAttributeSafeString("value", this._machineName);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				stringBuilder.Replace(Log4JXmlEventLayoutRenderer.dummyNamespaceRemover, string.Empty);
				stringBuilder.Replace(Log4JXmlEventLayoutRenderer.dummyNLogNamespaceRemover, string.Empty);
				stringBuilder.CopyTo(builder);
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00020824 File Offset: 0x0001EA24
		private void AppendMdlc(XmlWriter xtw)
		{
			if (this.IncludeMdlc)
			{
				foreach (string text in MappedDiagnosticsLogicalContext.GetNames())
				{
					string text2 = XmlHelper.XmlConvertToString(MappedDiagnosticsLogicalContext.GetObject(text));
					if (text2 != null)
					{
						xtw.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
						xtw.WriteAttributeSafeString("name", text);
						xtw.WriteAttributeSafeString("value", text2);
						xtw.WriteEndElement();
					}
				}
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000208B4 File Offset: 0x0001EAB4
		private void AppendNdc(XmlWriter xtw)
		{
			string text = null;
			if (this.IncludeNdc)
			{
				text = string.Join(this.NdcItemSeparator, NestedDiagnosticsContext.GetAllMessages());
			}
			if (this.IncludeNdlc)
			{
				if (text != null)
				{
					text += this.NdcItemSeparator;
				}
				text += string.Join(this.NdlcItemSeparator, NestedDiagnosticsLogicalContext.GetAllMessages());
			}
			if (text != null)
			{
				xtw.WriteElementSafeString("log4j", "NDC", Log4JXmlEventLayoutRenderer.dummyNamespace, text);
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00020924 File Offset: 0x0001EB24
		private static void AppendException(LogEventInfo logEvent, XmlWriter xtw)
		{
			if (logEvent.Exception != null)
			{
				xtw.WriteStartElement("log4j", "throwable", Log4JXmlEventLayoutRenderer.dummyNamespace);
				xtw.WriteSafeCData(logEvent.Exception.ToString());
				xtw.WriteEndElement();
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002095C File Offset: 0x0001EB5C
		private void AppendParameters(LogEventInfo logEvent, XmlWriter xtw)
		{
			int num = 0;
			for (;;)
			{
				int num2 = num;
				IList<NLogViewerParameterInfo> parameters = this.Parameters;
				int? num3 = ((parameters != null) ? new int?(parameters.Count) : null);
				if (!((num2 < num3.GetValueOrDefault()) & (num3 != null)))
				{
					break;
				}
				NLogViewerParameterInfo nlogViewerParameterInfo = this.Parameters[num];
				if (!string.IsNullOrEmpty((nlogViewerParameterInfo != null) ? nlogViewerParameterInfo.Name : null))
				{
					Layout layout = nlogViewerParameterInfo.Layout;
					string text = ((layout != null) ? layout.Render(logEvent) : null) ?? string.Empty;
					if (nlogViewerParameterInfo.IncludeEmptyValue || !string.IsNullOrEmpty(text))
					{
						xtw.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
						xtw.WriteAttributeSafeString("name", nlogViewerParameterInfo.Name);
						xtw.WriteAttributeSafeString("value", text);
						xtw.WriteEndElement();
					}
				}
				num++;
			}
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00020A34 File Offset: 0x0001EC34
		private void AppendProperties(XmlWriter xtw)
		{
			xtw.WriteStartElement("log4j", "properties", Log4JXmlEventLayoutRenderer.dummyNamespace);
			if (this.IncludeMdc)
			{
				foreach (string text in MappedDiagnosticsContext.GetNames())
				{
					string text2 = XmlHelper.XmlConvertToString(MappedDiagnosticsContext.GetObject(text));
					if (text2 != null)
					{
						xtw.WriteStartElement("log4j", "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
						xtw.WriteAttributeSafeString("name", text);
						xtw.WriteAttributeSafeString("value", text2);
						xtw.WriteEndElement();
					}
				}
			}
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00020AD8 File Offset: 0x0001ECD8
		private void AppendCallSite(LogEventInfo logEvent, XmlWriter xtw)
		{
			if ((this.IncludeCallSite || this.IncludeSourceInfo) && logEvent.CallSiteInformation != null)
			{
				MethodBase callerStackFrameMethod = logEvent.CallSiteInformation.GetCallerStackFrameMethod(0);
				string callerClassName = logEvent.CallSiteInformation.GetCallerClassName(callerStackFrameMethod, true, true, true);
				string callerMemberName = logEvent.CallSiteInformation.GetCallerMemberName(callerStackFrameMethod, true, true, true);
				xtw.WriteStartElement("log4j", "locationInfo", Log4JXmlEventLayoutRenderer.dummyNamespace);
				if (!string.IsNullOrEmpty(callerClassName))
				{
					xtw.WriteAttributeSafeString("class", callerClassName);
				}
				xtw.WriteAttributeSafeString("method", callerMemberName);
				if (this.IncludeSourceInfo)
				{
					xtw.WriteAttributeSafeString("file", logEvent.CallSiteInformation.GetCallerFilePath(0));
					xtw.WriteAttributeSafeString("line", logEvent.CallSiteInformation.GetCallerLineNumber(0).ToString(CultureInfo.InvariantCulture));
				}
				xtw.WriteEndElement();
				if (this.IncludeNLogData)
				{
					xtw.WriteElementSafeString("nlog", "eventSequenceNumber", Log4JXmlEventLayoutRenderer.dummyNLogNamespace, logEvent.SequenceID.ToString(CultureInfo.InvariantCulture));
					xtw.WriteStartElement("nlog", "locationInfo", Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
					Type type = ((callerStackFrameMethod != null) ? callerStackFrameMethod.DeclaringType : null);
					if (type != null)
					{
						xtw.WriteAttributeSafeString("assembly", type.GetAssembly().FullName);
					}
					xtw.WriteEndElement();
					xtw.WriteStartElement("nlog", "properties", Log4JXmlEventLayoutRenderer.dummyNLogNamespace);
					this.AppendProperties("nlog", xtw, logEvent);
					xtw.WriteEndElement();
				}
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00020C54 File Offset: 0x0001EE54
		private void AppendProperties(string prefix, XmlWriter xtw, LogEventInfo logEvent)
		{
			if (logEvent.HasProperties)
			{
				foreach (KeyValuePair<object, object> keyValuePair in logEvent.Properties)
				{
					string text = XmlHelper.XmlConvertToString(keyValuePair.Key);
					if (!string.IsNullOrEmpty(text))
					{
						string text2 = XmlHelper.XmlConvertToString(keyValuePair.Value);
						if (text2 != null)
						{
							xtw.WriteStartElement(prefix, "data", Log4JXmlEventLayoutRenderer.dummyNamespace);
							xtw.WriteAttributeSafeString("name", text);
							xtw.WriteAttributeSafeString("value", text2);
							xtw.WriteEndElement();
						}
					}
				}
			}
		}

		// Token: 0x04000329 RID: 809
		private static readonly DateTime log4jDateBase = new DateTime(1970, 1, 1);

		// Token: 0x0400032A RID: 810
		private static readonly string dummyNamespace = "http://nlog-project.org/dummynamespace/" + Guid.NewGuid();

		// Token: 0x0400032B RID: 811
		private static readonly string dummyNamespaceRemover = " xmlns:log4j=\"" + Log4JXmlEventLayoutRenderer.dummyNamespace + "\"";

		// Token: 0x0400032C RID: 812
		private static readonly string dummyNLogNamespace = "http://nlog-project.org/dummynamespace/" + Guid.NewGuid();

		// Token: 0x0400032D RID: 813
		private static readonly string dummyNLogNamespaceRemover = " xmlns:nlog=\"" + Log4JXmlEventLayoutRenderer.dummyNLogNamespace + "\"";

		// Token: 0x0400033B RID: 827
		private readonly string _machineName;

		// Token: 0x0400033C RID: 828
		private XmlWriterSettings _xmlWriterSettings;
	}
}
