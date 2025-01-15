using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public class Lookup : IXmlSerializable
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00005D92 File Offset: 0x00003F92
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00005D9A File Offset: 0x00003F9A
		public string Name { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00005DA3 File Offset: 0x00003FA3
		// (set) Token: 0x06000151 RID: 337 RVA: 0x00005DAB File Offset: 0x00003FAB
		public string Description { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005DB4 File Offset: 0x00003FB4
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00005DBC File Offset: 0x00003FBC
		public List<string> Domains { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005DC5 File Offset: 0x00003FC5
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005DCD File Offset: 0x00003FCD
		public List<string> ExactMatchDomains { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005DD6 File Offset: 0x00003FD6
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005DDE File Offset: 0x00003FDE
		public SignatureGenerator SignatureGenerator { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005DE7 File Offset: 0x00003FE7
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00005DEF File Offset: 0x00003FEF
		public TunerSettings TunerSettings { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005DF8 File Offset: 0x00003FF8
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005E00 File Offset: 0x00004000
		public Comparer Comparer { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005E09 File Offset: 0x00004009
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005E11 File Offset: 0x00004011
		internal int LookupId { get; set; }

		// Token: 0x0600015E RID: 350 RVA: 0x00005E1C File Offset: 0x0000401C
		internal Lookup(Lookup lookup)
		{
			this.Name = ((lookup.Name != null) ? ((string)lookup.Name.Clone()) : null);
			this.Description = ((lookup.Description != null) ? ((string)lookup.Description.Clone()) : null);
			this.Domains = new List<string>(lookup.Domains);
			this.ExactMatchDomains = new List<string>(lookup.ExactMatchDomains);
			this.SignatureGenerator = new SignatureGenerator(lookup.SignatureGenerator);
			this.TunerSettings = new TunerSettings(lookup.TunerSettings);
			this.Comparer = new Comparer(lookup.Comparer);
			this.LookupId = lookup.LookupId;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005ED2 File Offset: 0x000040D2
		public Lookup()
		{
			this.Domains = new List<string>();
			this.ExactMatchDomains = new List<string>();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005EF0 File Offset: 0x000040F0
		public static Lookup Create(FuzzyComparisonType comparisonType, double threshold, string[] domains, string[] exactMatchDomains)
		{
			if (domains == null || domains.Length == 0)
			{
				throw new Exception("Must define at least one domain to lookup on.");
			}
			if (threshold < 0.0 || threshold > 1.0)
			{
				throw new Exception("MinimumQuerySimilarity must be between 0.0 and 1.0.");
			}
			Lookup lookup = new Lookup();
			if (comparisonType == FuzzyComparisonType.Jaccard)
			{
				lookup.SignatureGenerator = SignatureGenerator.Create(FuzzyIndexType.LocalitySensitiveHashing);
				lookup.Comparer = new Comparer
				{
					AssemblyName = Assembly.GetExecutingAssembly().GetName().FullName,
					TypeName = typeof(JaccardComparer).FullName
				};
			}
			else if (FuzzyComparisonType.LeftJaccardContainment == comparisonType)
			{
				lookup.SignatureGenerator = SignatureGenerator.Create(FuzzyIndexType.PrefixFiltering);
				lookup.SignatureGenerator.Properties.Add(new Property
				{
					Name = "Threshold",
					DataType = typeof(double),
					Value = threshold
				});
				lookup.Comparer = new Comparer
				{
					AssemblyName = Assembly.GetExecutingAssembly().GetName().FullName,
					TypeName = typeof(LeftJaccardContainmentComparer).FullName
				};
			}
			lookup.Domains = new List<string>(domains);
			if (exactMatchDomains != null)
			{
				lookup.ExactMatchDomains = new List<string>(exactMatchDomains);
			}
			return lookup;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000601E File Offset: 0x0000421E
		internal void Validate()
		{
			if (this.Domains == null || this.Domains.Count == 0)
			{
				throw new Exception("Must define at least on domain to lookup on.");
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006040 File Offset: 0x00004240
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006044 File Offset: 0x00004244
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:Lookup", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				this.Description = xmlNode.Attributes.GetNamedItemOrDefault("description", null);
				this.Domains = Enumerable.ToList<string>(Enumerable.Select<DomainName, string>(xmlNode.SelectNodes("ns:Domains/ns:Domain", xmlNamespaceManager).ReadXmlObjects<DomainName>(), (DomainName dn) => dn.Name));
				this.ExactMatchDomains = Enumerable.ToList<string>(Enumerable.Select<DomainName, string>(xmlNode.SelectNodes("ns:ExactMatchDomains/ns:Domain", xmlNamespaceManager).ReadXmlObjects<DomainName>(), (DomainName dn) => dn.Name));
				this.SignatureGenerator = xmlNode.SelectSingleNode("/*/ns:SignatureGenerator", xmlNamespaceManager).ReadXmlObject<SignatureGenerator>();
				this.TunerSettings = xmlNode.SelectSingleNode("/*/ns:TunerSettings", xmlNamespaceManager).ReadXmlObject<TunerSettings>();
				this.Comparer = xmlNode.SelectSingleNode("/*/ns:Comparer", xmlNamespaceManager).ReadXmlObject<Comparer>();
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006174 File Offset: 0x00004374
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Lookup");
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.Description))
			{
				writer.WriteAttributeString("description", this.Description);
			}
			writer.WriteStartElement("Domains");
			foreach (string text in this.Domains)
			{
				writer.WriteElementString("Domain", text);
			}
			writer.WriteEndElement();
			if (this.ExactMatchDomains.Count > 0)
			{
				writer.WriteStartElement("ExactMatchDomains");
				foreach (string text2 in this.ExactMatchDomains)
				{
					writer.WriteElementString("Domain", text2);
				}
				writer.WriteEndElement();
			}
			if (this.SignatureGenerator != null)
			{
				this.SignatureGenerator.WriteXml(writer);
			}
			if (this.TunerSettings != null)
			{
				this.TunerSettings.WriteXml(writer);
			}
			if (this.Comparer != null)
			{
				this.Comparer.WriteXml(writer);
			}
			writer.WriteEndElement();
		}
	}
}
