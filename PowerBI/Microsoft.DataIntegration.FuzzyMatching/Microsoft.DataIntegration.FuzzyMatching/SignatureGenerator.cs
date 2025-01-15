using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	public class SignatureGenerator : ObjectDefinition, IXmlSerializable
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000855C File Offset: 0x0000675C
		public SignatureGenerator(SignatureGenerator signatureGenerator)
			: base(signatureGenerator)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008565 File Offset: 0x00006765
		public SignatureGenerator()
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000856D File Offset: 0x0000676D
		public SignatureGenerator(XmlReader reader)
			: this()
		{
			this.ReadXml(reader);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000857C File Offset: 0x0000677C
		internal SignatureGeneratorPool ObjectPool
		{
			get
			{
				if (this.m_objectPool == null)
				{
					this.m_objectPool = new SignatureGeneratorPool
					{
						SignatureGenerator = this
					};
				}
				return this.m_objectPool;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000085A0 File Offset: 0x000067A0
		public override string ToString()
		{
			StringWriter stringWriter = new StringWriter();
			this.WriteXml(XmlWriter.Create(stringWriter));
			return stringWriter.ToString();
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000085C5 File Offset: 0x000067C5
		public static SignatureGenerator Parse(string xml)
		{
			SignatureGenerator signatureGenerator = new SignatureGenerator();
			signatureGenerator.ReadXml(XmlReader.Create(new StringReader(xml)));
			return signatureGenerator;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000085E0 File Offset: 0x000067E0
		public static SignatureGenerator Create(FuzzyIndexType indexType)
		{
			if (FuzzyIndexType.LocalitySensitiveHashing == indexType)
			{
				return new SignatureGenerator
				{
					TypeName = "Microsoft.DataIntegration.FuzzyMatching.LshSignatureGenerator"
				};
			}
			if (FuzzyIndexType.PrefixFiltering == indexType)
			{
				return new SignatureGenerator
				{
					TypeName = "Microsoft.DataIntegration.FuzzyMatching.PrefixSignatureGenerator"
				};
			}
			if (FuzzyIndexType.InvertedIndex == indexType)
			{
				return new SignatureGenerator
				{
					TypeName = "Microsoft.DataIntegration.FuzzyMatching.InvertedIndexSignatureGenerator"
				};
			}
			if (FuzzyIndexType.InvertedIndexWithoutClustering == indexType)
			{
				SignatureGenerator signatureGenerator = new SignatureGenerator();
				signatureGenerator.TypeName = "Microsoft.DataIntegration.FuzzyMatching.InvertedIndexSignatureGenerator";
				signatureGenerator.Properties.Add(new Property
				{
					Name = "UseClustering",
					Value = "false",
					DataType = typeof(bool)
				});
				return signatureGenerator;
			}
			if (FuzzyIndexType.Identity == indexType)
			{
				return new SignatureGenerator
				{
					TypeName = "Microsoft.DataIntegration.FuzzyMatching.IdentitySignatureGenerator"
				};
			}
			if (FuzzyIndexType.Exact == indexType)
			{
				return new SignatureGenerator
				{
					TypeName = "Microsoft.DataIntegration.FuzzyMatching.ExactMatchSignatureGenerator"
				};
			}
			throw new ArgumentException();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000086A6 File Offset: 0x000068A6
		public static SignatureGenerator Create(FuzzyIndexType indexType, double threshold)
		{
			SignatureGenerator signatureGenerator = SignatureGenerator.Create(indexType);
			signatureGenerator.Properties.Add(new Property
			{
				Name = "Threshold",
				DataType = typeof(double),
				Value = threshold
			});
			return signatureGenerator;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000086E8 File Offset: 0x000068E8
		public static SignatureGenerator Create(FuzzyIndexType indexType, int lshNumHashtables, int lshNumDimensions)
		{
			if (indexType != FuzzyIndexType.LocalitySensitiveHashing)
			{
				throw new ArgumentException("indexType must be FuzzyIndexType.LocalitySensitiveHashing.");
			}
			if (lshNumHashtables <= 0 || lshNumDimensions <= 0)
			{
				throw new Exception("NumLshHashtables and NumLshDimensions must be greater than or equal to zero.");
			}
			SignatureGenerator signatureGenerator = new SignatureGenerator();
			signatureGenerator.TypeName = "Microsoft.DataIntegration.FuzzyMatching.LshSignatureGenerator";
			signatureGenerator.Properties.Add(new Property
			{
				Name = "NumHashtables",
				DataType = typeof(int),
				Value = lshNumHashtables
			});
			signatureGenerator.Properties.Add(new Property
			{
				Name = "DimensionsPerSignature",
				DataType = typeof(int),
				Value = lshNumDimensions
			});
			return signatureGenerator;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008795 File Offset: 0x00006995
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008798 File Offset: 0x00006998
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			if (reader.ReadToFollowing("SignatureGenerator"))
			{
				if (reader.MoveToAttribute("name"))
				{
					base.Name = reader.Value;
				}
				if (reader.MoveToAttribute("assemblyName"))
				{
					base.AssemblyName = reader.Value;
				}
				if (reader.MoveToAttribute("typeName"))
				{
					base.TypeName = reader.Value;
				}
				if (reader.ReadToFollowing("Properties"))
				{
					CollectionSerialization.ReadXml<Property>(reader.ReadSubtree(), "Properties", base.Properties);
				}
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008828 File Offset: 0x00006A28
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("SignatureGenerator");
			if (!string.IsNullOrEmpty(base.Name))
			{
				writer.WriteAttributeString("name", base.Name);
			}
			if (!string.IsNullOrEmpty(base.AssemblyName))
			{
				writer.WriteAttributeString("assemblyName", base.AssemblyName);
			}
			if (!string.IsNullOrEmpty(base.TypeName))
			{
				writer.WriteAttributeString("typeName", base.TypeName);
			}
			CollectionSerialization.WriteXml<Property>(writer, "Properties", base.Properties);
			writer.WriteEndElement();
		}

		// Token: 0x04000079 RID: 121
		[NonSerialized]
		private SignatureGeneratorPool m_objectPool;
	}
}
