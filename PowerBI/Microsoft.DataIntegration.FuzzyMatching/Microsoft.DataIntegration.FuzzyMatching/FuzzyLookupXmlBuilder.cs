using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public class FuzzyLookupXmlBuilder
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x000073A4 File Offset: 0x000055A4
		internal static XmlSchemaSet LoadXmlSchemaSet()
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			xmlSchemaSet.XmlResolver = null;
			xmlSchemaSet.Add(XmlSchema.Read(new StringReader(Resources.Common_xsd), null));
			xmlSchemaSet.Add(XmlSchema.Read(new StringReader(Resources.RowsetManager_xsd), null));
			xmlSchemaSet.Add(XmlSchema.Read(new StringReader(Resources.DomainManager_xsd), null));
			xmlSchemaSet.Add(XmlSchema.Read(new StringReader(Resources.ComparisonDefinition_xsd), null));
			xmlSchemaSet.Add(XmlSchema.Read(new StringReader(Resources.IndexDefinition_xsd), null));
			xmlSchemaSet.Add(XmlSchema.Read(new StringReader(Resources.QueryDefinition_xsd), null));
			xmlSchemaSet.Add(XmlSchema.Read(new StringReader(Resources.Results_xsd), null));
			xmlSchemaSet.Add(XmlSchema.Read(new StringReader(Resources.Configuration_xsd), null));
			return xmlSchemaSet;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007475 File Offset: 0x00005675
		public static XmlReader CreateValidatingXmlReader(Stream xmlStream)
		{
			return XmlReader.Create(xmlStream, FuzzyLookupXmlBuilder.s_xmlReaderSettings);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007482 File Offset: 0x00005682
		public static XmlReader CreateValidatingXmlReader(XmlReader reader)
		{
			return XmlReader.Create(reader, FuzzyLookupXmlBuilder.s_xmlReaderSettings);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000748F File Offset: 0x0000568F
		public static void CreateDomainManager(XmlReader reader, out DomainManager domainManager)
		{
			FuzzyLookupXmlBuilder.CreateDomainManager(reader, null, out domainManager);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000749C File Offset: 0x0000569C
		public static object CreateObject(Stream xmlStream)
		{
			XmlReader xmlReader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(xmlStream);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlReader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode;
			object obj;
			if ((xmlNode = xmlDocument.SelectSingleNode("/ns:DomainManager", xmlNamespaceManager)) != null)
			{
				RowsetManager rowsetManager = null;
				XmlNode xmlNode2;
				if ((xmlNode2 = xmlDocument.SelectSingleNode("/ns:DomainManager/ns:RowsetManager", xmlNamespaceManager)) != null)
				{
					rowsetManager = new RowsetManager(new XmlNodeReader(xmlNode2));
				}
				DomainManager domainManager;
				FuzzyLookupXmlBuilder.CreateDomainManager(new XmlNodeReader(xmlNode), rowsetManager, out domainManager);
				obj = domainManager;
			}
			else if ((xmlNode = xmlDocument.SelectSingleNode("/ns:RowsetManager", xmlNamespaceManager)) != null)
			{
				obj = new RowsetManager(new XmlNodeReader(xmlNode));
			}
			else if ((xmlNode = xmlDocument.SelectSingleNode("/ns:RecordBinding", xmlNamespaceManager)) != null)
			{
				obj = new RecordBinding(new XmlNodeReader(xmlNode));
			}
			else if ((xmlNode = xmlDocument.SelectSingleNode("/ns:SignatureGenerator", xmlNamespaceManager)) != null)
			{
				obj = new SignatureGenerator(new XmlNodeReader(xmlNode));
			}
			else if ((xmlNode = xmlDocument.SelectSingleNode("/ns:Comparer", xmlNamespaceManager)) != null)
			{
				obj = new Comparer(new XmlNodeReader(xmlNode));
			}
			else
			{
				if ((xmlNode = xmlDocument.SelectSingleNode("/ns:Tokenizer", xmlNamespaceManager)) == null)
				{
					throw new Exception("Unsupported XML object type.");
				}
				obj = new Tokenizer(new XmlNodeReader(xmlNode)).CreateInstance();
			}
			return obj;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000075C1 File Offset: 0x000057C1
		public static void CreateDomainManager(XmlReader domainManagerXml, IRowsetManager rowsetManager, out DomainManager domainManager)
		{
			if (rowsetManager == null)
			{
				rowsetManager = new RowsetManager();
			}
			domainManager = new DomainManager();
			FuzzyLookupXmlBuilder.PopulateDomainManager(domainManager, domainManagerXml, rowsetManager);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000075E0 File Offset: 0x000057E0
		internal static void PopulateDomainManager(DomainManager domainManager, XmlReader domainManagerXml, IRowsetManager rowsetManager)
		{
			domainManagerXml = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(domainManagerXml);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(domainManagerXml);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode;
			if ((xmlNode = xmlDocument.SelectSingleNode("/*/ns:DomainManager", xmlNamespaceManager)) == null && (xmlNode = xmlDocument.SelectSingleNode("/ns:DomainManager", xmlNamespaceManager)) == null)
			{
				throw new ArgumentException("Must specify a DomainManager XML element.");
			}
			DomainManagerDefinition domainManagerDefinition = new DomainManagerDefinition(new XmlNodeReader(xmlNode));
			using (ConnectionManager connectionManager = new ConnectionManager(rowsetManager.Connections))
			{
				domainManager.Name = domainManagerDefinition.Name;
				if (domainManagerDefinition.TokenIdProvider != null)
				{
					domainManager.TokenIdProvider = (ITokenIdProvider)domainManagerDefinition.TokenIdProvider.CreateInstance();
				}
				ITokenIdProvider tokenIdProvider = domainManager.TokenIdProvider;
				Property.SetProperties(domainManager, domainManagerDefinition.Properties);
				RowsetDistributor rowsetDistributor = new RowsetDistributor(rowsetManager);
				RowsetDistributor rowsetDistributor2 = new RowsetDistributor(rowsetManager);
				foreach (Domain domain in domainManagerDefinition.Domains)
				{
					domainManager.CreateDomain(domain.Name);
					IRecordTokenizer recordTokenizer = (IRecordTokenizer)domain.Tokenizer.CreateInstance();
					domainManager.SetTokenizer(domain.Name, recordTokenizer);
					foreach (TransformationProvider transformationProvider in domain.RightTransformationProviders)
					{
						ITransformationProvider transformationProvider2 = FuzzyLookupXmlBuilder.CreateTransformationProvider(domainManager, rowsetDistributor, transformationProvider, domain.Name);
						domainManager[domain.Name].RightTransformationProvider.Add(transformationProvider2);
					}
					foreach (TransformationProvider transformationProvider3 in domain.LeftTransformationProviders)
					{
						ITransformationProvider transformationProvider4 = FuzzyLookupXmlBuilder.CreateTransformationProvider(domainManager, rowsetDistributor2, transformationProvider3, domain.Name);
						domainManager[domain.Name].LeftTransformationProvider.Add(transformationProvider4);
					}
					foreach (TransformationProvider transformationProvider5 in domain.PairSpecificTransformationProviders)
					{
						IPairSpecificTransformationProvider pairSpecificTransformationProvider = FuzzyLookupXmlBuilder.CreatePairSpecificTransformationProvider(domainManager, rowsetDistributor2, transformationProvider5, domain.Name);
						domainManager[domain.Name].PairSpecificTransformationProvider.Add(pairSpecificTransformationProvider);
					}
				}
				foreach (Domain domain2 in domainManagerDefinition.Domains)
				{
					ITokenWeightProvider tokenWeightProvider = (ITokenWeightProvider)domain2.TokenWeightProvider.CreateInstance();
					if (tokenWeightProvider is IProviderInitialize)
					{
						(tokenWeightProvider as IProviderInitialize).Initialize(domainManager, domain2.Name);
					}
					if (tokenWeightProvider is IRowsetConsumer && !(tokenWeightProvider is TokenWeightProviderReference))
					{
						(tokenWeightProvider as IRowsetConsumer).RequestRowsets(rowsetDistributor2);
					}
					domainManager.SetTokenWeightProvider(domain2.Name, tokenWeightProvider);
				}
				rowsetDistributor.DistributeRowsets(connectionManager, domainManager, tokenIdProvider);
				rowsetDistributor2.DistributeRowsets(connectionManager, domainManager, tokenIdProvider);
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007968 File Offset: 0x00005B68
		private static ITransformationProvider CreateTransformationProvider(DomainManager domainManager, RowsetDistributor rowsetDistributor, TransformationProvider tranProviderDef, string domainName)
		{
			ITransformationProvider transformationProvider = (ITransformationProvider)tranProviderDef.CreateInstance();
			if (transformationProvider is IProviderInitialize)
			{
				(transformationProvider as IProviderInitialize).Initialize(domainManager, domainName);
			}
			if (transformationProvider is IRowsetConsumer && !(transformationProvider is TransformationProviderReference))
			{
				(transformationProvider as IRowsetConsumer).RequestRowsets(rowsetDistributor);
			}
			TransformationFilterAggregator transformationFilterAggregator = new TransformationFilterAggregator();
			foreach (TransformationFilter transformationFilter in tranProviderDef.TransformationFilters)
			{
				ITransformationFilter transformationFilter2 = FuzzyLookupXmlBuilder.CreateTransformationFilter(domainManager, rowsetDistributor, transformationFilter, domainName);
				transformationFilterAggregator.Add(transformationFilter2);
			}
			if (transformationFilterAggregator.Count > 1)
			{
				(transformationProvider as ITransformationFiltering).TransformationFilter = transformationFilterAggregator;
			}
			else if (transformationFilterAggregator.Count == 1)
			{
				(transformationProvider as ITransformationFiltering).TransformationFilter = transformationFilterAggregator[0];
			}
			return transformationProvider;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007A40 File Offset: 0x00005C40
		private static IPairSpecificTransformationProvider CreatePairSpecificTransformationProvider(DomainManager domainManager, RowsetDistributor rowsetDistributor, TransformationProvider tranProviderDef, string domainName)
		{
			IPairSpecificTransformationProvider pairSpecificTransformationProvider = (IPairSpecificTransformationProvider)tranProviderDef.CreateInstance();
			if (pairSpecificTransformationProvider is IProviderInitialize)
			{
				(pairSpecificTransformationProvider as IProviderInitialize).Initialize(domainManager, domainName);
			}
			if (pairSpecificTransformationProvider is IRowsetConsumer && !(pairSpecificTransformationProvider is TransformationProviderReference))
			{
				(pairSpecificTransformationProvider as IRowsetConsumer).RequestRowsets(rowsetDistributor);
			}
			TransformationFilterAggregator transformationFilterAggregator = new TransformationFilterAggregator();
			foreach (TransformationFilter transformationFilter in tranProviderDef.TransformationFilters)
			{
				ITransformationFilter transformationFilter2 = FuzzyLookupXmlBuilder.CreateTransformationFilter(domainManager, rowsetDistributor, transformationFilter, domainName);
				transformationFilterAggregator.Add(transformationFilter2);
			}
			if (transformationFilterAggregator.Count > 1)
			{
				(pairSpecificTransformationProvider as ITransformationFiltering).TransformationFilter = transformationFilterAggregator;
			}
			else if (transformationFilterAggregator.Count == 1)
			{
				(pairSpecificTransformationProvider as ITransformationFiltering).TransformationFilter = transformationFilterAggregator[0];
			}
			return pairSpecificTransformationProvider;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00007B18 File Offset: 0x00005D18
		private static ITransformationFilter CreateTransformationFilter(DomainManager domainManager, RowsetDistributor rowsetDistributor, TransformationFilter tranFilterDef, string domainName)
		{
			ITransformationFilter transformationFilter = (ITransformationFilter)tranFilterDef.CreateInstance();
			if (transformationFilter is IProviderInitialize)
			{
				(transformationFilter as IProviderInitialize).Initialize(domainManager, domainName);
			}
			if (!(transformationFilter is TransformationFilterReference) && transformationFilter is IRowsetConsumer)
			{
				(transformationFilter as IRowsetConsumer).RequestRowsets(rowsetDistributor);
			}
			return transformationFilter;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007B64 File Offset: 0x00005D64
		public static void CreateIndex(XmlReader reader, DomainManager domainManager, out FuzzyLookup fuzzyLookup)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			RowsetManager rowsetManager = new RowsetManager(new XmlNodeReader(xmlDocument));
			FuzzyLookupDefinition fuzzyLookupDefinition = new FuzzyLookupDefinition(new XmlNodeReader(xmlDocument.SelectSingleNode("/*/ns:FuzzyLookup", xmlNamespaceManager)));
			using (ConnectionManager connectionManager = new ConnectionManager(rowsetManager.Connections))
			{
				IRowsetDefinition rowsetDefinition = null;
				rowsetManager.Rowsets.TryGetItem(fuzzyLookupDefinition.ReferenceRowsetName, out rowsetDefinition);
				if (rowsetDefinition != null)
				{
					rowsetManager.GetRecordBinding(fuzzyLookupDefinition.ReferenceRowsetName, connectionManager);
				}
				if (fuzzyLookupDefinition.KeyColumns.Count == 0 && rowsetDefinition.RidColumnName != null)
				{
					fuzzyLookupDefinition.KeyColumns.Add(new Column
					{
						Name = rowsetDefinition.RidColumnName
					});
				}
				IFuzzyLookupStateManager fuzzyLookupStateManager = fuzzyLookupDefinition.StateManager.CreateInstance() as IFuzzyLookupStateManager;
				if (fuzzyLookupStateManager is IFuzzyLookupStateManagerInitialize)
				{
					(fuzzyLookupStateManager as IFuzzyLookupStateManagerInitialize).Initialize(fuzzyLookupDefinition, connectionManager);
				}
				fuzzyLookup = new FuzzyLookup(domainManager, fuzzyLookupDefinition, fuzzyLookupStateManager);
				fuzzyLookup.Statistics.EnableTimers = false;
				RowsetDistributor rowsetDistributor = new RowsetDistributor(rowsetManager);
				((IRowsetConsumer)fuzzyLookup).RequestRowsets(rowsetDistributor);
				rowsetDistributor.DistributeRowsets(connectionManager, domainManager, domainManager.TokenIdProvider);
			}
		}

		// Token: 0x04000065 RID: 101
		internal static readonly XmlReaderSettings s_xmlReaderSettings = new XmlReaderSettings
		{
			ConformanceLevel = 0,
			Schemas = FuzzyLookupXmlBuilder.LoadXmlSchemaSet(),
			ValidationType = 4,
			ValidationFlags = 7
		};

		// Token: 0x02000126 RID: 294
		private class RowsetDataDrivenProviderInfo
		{
			// Token: 0x04000491 RID: 1169
			public List<IRecordUpdate> FirstPassProviders = new List<IRecordUpdate>();

			// Token: 0x04000492 RID: 1170
			public bool NeedSecondPassForTokenWeights;

			// Token: 0x04000493 RID: 1171
			public List<IRecordUpdate> SecondPassProviders = new List<IRecordUpdate>();
		}
	}
}
