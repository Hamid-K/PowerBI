using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000011 RID: 17
	internal sealed class TraceRequest : IXmlSerializable
	{
		// Token: 0x06000030 RID: 48 RVA: 0x0000281F File Offset: 0x00000A1F
		internal TraceRequest()
		{
			this._traceEvents = new List<TraceEvent>();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002832 File Offset: 0x00000A32
		internal TraceRequest(List<TraceEvent> traceEvents)
		{
			this._traceEvents = traceEvents;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002841 File Offset: 0x00000A41
		internal IList<TraceEvent> TraceEvents
		{
			get
			{
				return this._traceEvents.AsReadOnly();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000284E File Offset: 0x00000A4E
		private static Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002855 File Offset: 0x00000A55
		private XmlSerializer GetTraceEventSerializer()
		{
			if (this._traceEventSerializer == null)
			{
				this._traceEventSerializer = new XmlSerializer(typeof(TraceEvent), this.GetNamespace());
			}
			return this._traceEventSerializer;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002880 File Offset: 0x00000A80
		private static XmlWriterSettings CreateXmlWriterSettings(Encoding encoding)
		{
			return new XmlWriterSettings
			{
				CheckCharacters = false,
				Encoding = encoding
			};
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002895 File Offset: 0x00000A95
		private string GetNamespace()
		{
			return "http://schemas.microsoft.com/sqlserver/reporting/2011/01/logclienttraceevents";
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000289C File Offset: 0x00000A9C
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028A4 File Offset: 0x00000AA4
		public void ReadXml(XmlReader reader)
		{
			foreach (TraceEvent traceEvent in this.ReadTraceEvents(reader))
			{
				this._traceEvents.Add(traceEvent);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028F8 File Offset: 0x00000AF8
		internal IEnumerable<TraceEvent> ReadTraceEvents(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					while ("TraceEvent".Equals(reader.Name, StringComparison.Ordinal))
					{
						TraceEvent traceEvent = this.GetTraceEventSerializer().Deserialize(reader) as TraceEvent;
						yield return traceEvent;
					}
				}
			}
			yield break;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002910 File Offset: 0x00000B10
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("ClientRequest", this.GetNamespace());
			if (this.TraceEvents != null && this.TraceEvents.Count > 0)
			{
				writer.WriteStartElement("TraceEvents");
				foreach (TraceEvent traceEvent in this.TraceEvents)
				{
					this.GetTraceEventSerializer().Serialize(writer, traceEvent);
				}
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void WriteXml(Stream stream)
		{
			XmlWriterSettings xmlWriterSettings = TraceRequest.CreateXmlWriterSettings(TraceRequest.Encoding);
			using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
			{
				xmlWriter.WriteStartDocument();
				this.WriteXml(xmlWriter);
				xmlWriter.WriteEndDocument();
			}
		}

		// Token: 0x0400008D RID: 141
		private readonly List<TraceEvent> _traceEvents;

		// Token: 0x0400008E RID: 142
		private XmlSerializer _traceEventSerializer;

		// Token: 0x0400008F RID: 143
		internal const string NAMESPACE2011 = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/logclienttraceevents";

		// Token: 0x04000090 RID: 144
		internal const string NAMESPACE = "xmlns";

		// Token: 0x04000091 RID: 145
		internal const string CLIENTREQUEST = "ClientRequest";

		// Token: 0x04000092 RID: 146
		internal const string TRACEEVENTS = "TraceEvents";
	}
}
