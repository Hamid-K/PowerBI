using System;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	public class TunerSettings : IXmlSerializable
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00008197 File Offset: 0x00006397
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000819F File Offset: 0x0000639F
		public bool Enabled { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001BF RID: 447 RVA: 0x000081A8 File Offset: 0x000063A8
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x000081B0 File Offset: 0x000063B0
		public double MinQueryThreshold { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x000081B9 File Offset: 0x000063B9
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x000081C1 File Offset: 0x000063C1
		public FuzzyComparisonType ComparisonType { get; set; }

		// Token: 0x060001C3 RID: 451 RVA: 0x000081CA File Offset: 0x000063CA
		public TunerSettings(TunerSettings tunerSettings)
		{
			this.Enabled = tunerSettings.Enabled;
			this.MinQueryThreshold = tunerSettings.MinQueryThreshold;
			this.ComparisonType = tunerSettings.ComparisonType;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000081F6 File Offset: 0x000063F6
		public TunerSettings()
		{
			this.Enabled = false;
			this.MinQueryThreshold = 0.8;
			this.ComparisonType = FuzzyComparisonType.Jaccard;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000821B File Offset: 0x0000641B
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008220 File Offset: 0x00006420
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			if (reader.ReadToFollowing("TunerSettings"))
			{
				if (reader.MoveToAttribute("enabled"))
				{
					this.Enabled = bool.Parse(reader.Value);
				}
				if (reader.MoveToAttribute("minQueryThreshold"))
				{
					this.MinQueryThreshold = double.Parse(reader.Value, CultureInfo.InvariantCulture);
				}
				if (reader.MoveToAttribute("comparisonType"))
				{
					this.ComparisonType = (FuzzyComparisonType)Enum.Parse(typeof(FuzzyComparisonType), reader.Value);
				}
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000082B0 File Offset: 0x000064B0
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("TunerSettings");
			writer.WriteAttributeString("enabled", this.Enabled.ToString());
			writer.WriteAttributeString("minQueryThreshold", this.MinQueryThreshold.ToString(CultureInfo.InvariantCulture));
			writer.WriteAttributeString("comparisonType", this.ComparisonType.ToString());
			writer.WriteEndElement();
		}
	}
}
