using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	public class Domain : IXmlSerializable, IName
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000057D2 File Offset: 0x000039D2
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000057DA File Offset: 0x000039DA
		public string Name { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000057E3 File Offset: 0x000039E3
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000057EB File Offset: 0x000039EB
		public string Description { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000057F4 File Offset: 0x000039F4
		// (set) Token: 0x06000130 RID: 304 RVA: 0x000057FC File Offset: 0x000039FC
		public Tokenizer Tokenizer { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005805 File Offset: 0x00003A05
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000580D File Offset: 0x00003A0D
		public TokenWeightProvider TokenWeightProvider { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005816 File Offset: 0x00003A16
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000581E File Offset: 0x00003A1E
		public List<TransformationProvider> LeftTransformationProviders { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005827 File Offset: 0x00003A27
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0000582F File Offset: 0x00003A2F
		public List<TransformationProvider> RightTransformationProviders { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005838 File Offset: 0x00003A38
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005840 File Offset: 0x00003A40
		public List<TransformationProvider> PairSpecificTransformationProviders { get; set; }

		// Token: 0x06000139 RID: 313 RVA: 0x00005849 File Offset: 0x00003A49
		public Domain()
		{
			this.Tokenizer = Tokenizer.Default;
			this.TokenWeightProvider = TokenWeightProvider.Default;
			this.LeftTransformationProviders = new List<TransformationProvider>();
			this.RightTransformationProviders = new List<TransformationProvider>();
			this.PairSpecificTransformationProviders = new List<TransformationProvider>();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005888 File Offset: 0x00003A88
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000588C File Offset: 0x00003A8C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:Domain", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				this.Description = xmlNode.Attributes.GetNamedItemOrDefault("description", null);
				XmlNode xmlNode2;
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Tokenizer", xmlNamespaceManager)) != null)
				{
					this.Tokenizer = new Tokenizer();
					this.Tokenizer.ReadXml(new XmlNodeReader(xmlNode2));
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:TokenWeightProvider", xmlNamespaceManager)) != null)
				{
					this.TokenWeightProvider = new TokenWeightProvider();
					this.TokenWeightProvider.ReadXml(new XmlNodeReader(xmlNode2));
				}
				foreach (object obj in xmlNode.SelectNodes("//ns:LeftTransformationProviders/ns:TransformationProvider", xmlNamespaceManager))
				{
					XmlNode xmlNode3 = (XmlNode)obj;
					TransformationProvider transformationProvider = new TransformationProvider();
					transformationProvider.ReadXml(new XmlNodeReader(xmlNode3));
					this.LeftTransformationProviders.Add(transformationProvider);
				}
				foreach (object obj2 in xmlNode.SelectNodes("//ns:RightTransformationProviders/ns:TransformationProvider", xmlNamespaceManager))
				{
					XmlNode xmlNode4 = (XmlNode)obj2;
					TransformationProvider transformationProvider2 = new TransformationProvider();
					transformationProvider2.ReadXml(new XmlNodeReader(xmlNode4));
					this.RightTransformationProviders.Add(transformationProvider2);
				}
				foreach (object obj3 in xmlNode.SelectNodes("//ns:PairSpecificTransformationProviders/ns:TransformationProvider", xmlNamespaceManager))
				{
					XmlNode xmlNode5 = (XmlNode)obj3;
					TransformationProvider transformationProvider3 = new TransformationProvider();
					transformationProvider3.ReadXml(new XmlNodeReader(xmlNode5));
					this.PairSpecificTransformationProviders.Add(transformationProvider3);
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005A98 File Offset: 0x00003C98
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("Domain");
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.Description))
			{
				writer.WriteAttributeString("description", this.Description);
			}
			if (this.Tokenizer != null)
			{
				this.Tokenizer.WriteXml(writer);
			}
			if (this.TokenWeightProvider != null)
			{
				this.TokenWeightProvider.WriteXml(writer);
			}
			CollectionSerialization.WriteXml<TransformationProvider>(writer, "LeftTransformationProviders", this.LeftTransformationProviders);
			CollectionSerialization.WriteXml<TransformationProvider>(writer, "RightTransformationProviders", this.RightTransformationProviders);
			CollectionSerialization.WriteXml<TransformationProvider>(writer, "PairSpecificTransformationProviders", this.PairSpecificTransformationProviders);
			writer.WriteEndElement();
		}
	}
}
