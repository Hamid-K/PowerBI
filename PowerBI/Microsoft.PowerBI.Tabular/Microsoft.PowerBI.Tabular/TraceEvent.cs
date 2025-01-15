using System;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000DD RID: 221
	[Guid("91A789BC-E474-4cc1-BD9C-94AD212B3F52")]
	public sealed class TraceEvent : ICloneable, IXmlSerializable
	{
		// Token: 0x06000E9A RID: 3738 RVA: 0x0007067F File Offset: 0x0006E87F
		object ICloneable.Clone()
		{
			return this.CopyTo(new TraceEvent());
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0007068C File Offset: 0x0006E88C
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00070690 File Offset: 0x0006E890
		void IXmlSerializable.WriteXml(XmlWriter xmlWriter)
		{
			if (xmlWriter == null)
			{
				throw new ArgumentNullException("xmlWriter");
			}
			if (this.eventID != TraceEventClass.NotAvailable)
			{
				xmlWriter.WriteElementString("EventID", XmlConvert.ToString((int)this.eventID));
			}
			if (this.columns.Count > 0)
			{
				xmlWriter.WriteStartElement("Columns");
				int i = 0;
				int count = this.columns.Count;
				while (i < count)
				{
					xmlWriter.WriteElementString("ColumnID", XmlConvert.ToString((int)this.columns[i]));
					i++;
				}
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0007071C File Offset: 0x0006E91C
		void IXmlSerializable.ReadXml(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				throw new ArgumentNullException("xmlReader");
			}
			this.eventID = TraceEventClass.NotAvailable;
			this.columns.Clear();
			xmlReader.ReadStartElement("Event", "http://schemas.microsoft.com/analysisservices/2003/engine");
			if (xmlReader.IsStartElement("EventID", "http://schemas.microsoft.com/analysisservices/2003/engine"))
			{
				this.EventID = (TraceEventClass)XmlConvert.ToInt32(xmlReader.ReadElementString("EventID"));
			}
			if (xmlReader.IsStartElement("Columns", "http://schemas.microsoft.com/analysisservices/2003/engine"))
			{
				xmlReader.ReadStartElement("Columns");
				while (xmlReader.IsStartElement("ColumnID", "http://schemas.microsoft.com/analysisservices/2003/engine"))
				{
					this.columns.Add((TraceColumn)XmlConvert.ToInt32(xmlReader.ReadElementString("ColumnID")));
				}
				xmlReader.ReadEndElement();
			}
			xmlReader.ReadEndElement();
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000707DA File Offset: 0x0006E9DA
		public TraceEvent()
		{
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x000707ED File Offset: 0x0006E9ED
		public TraceEvent(TraceEventClass eventClass)
		{
			this.eventID = eventClass;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00070807 File Offset: 0x0006EA07
		// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x0007080F File Offset: 0x0006EA0F
		public TraceEventClass EventID
		{
			get
			{
				return this.eventID;
			}
			set
			{
				if (this.eventID != value)
				{
					if (this.owningCollection != null)
					{
						throw new InvalidOperationException(SR.PropertyCannotBeChangedForObjectInCollection("EventID", typeof(TraceEvent).Name));
					}
					this.eventID = value;
				}
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00070848 File Offset: 0x0006EA48
		public TraceColumnCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00070850 File Offset: 0x0006EA50
		public TraceEvent CopyTo(TraceEvent obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (obj == this)
			{
				throw new InvalidOperationException(SR.Copy_DestinationIsSelf);
			}
			obj.EventID = this.EventID;
			this.Columns.CopyTo(obj.Columns);
			return obj;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0007088D File Offset: 0x0006EA8D
		public TraceEvent Clone()
		{
			return this.CopyTo(new TraceEvent());
		}

		// Token: 0x040001AF RID: 431
		internal TraceEventCollection owningCollection;

		// Token: 0x040001B0 RID: 432
		private TraceEventClass eventID;

		// Token: 0x040001B1 RID: 433
		private TraceColumnCollection columns = new TraceColumnCollection();
	}
}
