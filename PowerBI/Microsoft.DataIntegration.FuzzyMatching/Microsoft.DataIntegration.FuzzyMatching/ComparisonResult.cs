using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000B2 RID: 178
	[Serializable]
	public class ComparisonResult : IXmlSerializable
	{
		// Token: 0x060006D2 RID: 1746 RVA: 0x0001E7E9 File Offset: 0x0001C9E9
		public ComparisonResult()
		{
			this.LeftTransformationsApplied = new ArraySegmentBuilder<TransformationMatch>();
			this.RightTransformationsApplied = new ArraySegmentBuilder<TransformationMatch>();
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001E807 File Offset: 0x0001CA07
		public ComparisonResult Clone()
		{
			return this.Clone(HeapSegmentAllocator<WeightedTransformationMatch>.Instance, HeapSegmentAllocator<TransformationMatch>.Instance, HeapSegmentAllocator<int>.Instance, HeapSegmentAllocator<byte>.Instance);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001E823 File Offset: 0x0001CA23
		public ComparisonResult Clone(ISegmentAllocator<WeightedTransformationMatch> weightedtranMatchAllocator, ISegmentAllocator<TransformationMatch> tranMatchAllocator, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			return this.Clone(weightedtranMatchAllocator, tranMatchAllocator, intAllocator, byteAllocator, false);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001E834 File Offset: 0x0001CA34
		public ComparisonResult Clone(ISegmentAllocator<WeightedTransformationMatch> weightedtranMatchAllocator, ISegmentAllocator<TransformationMatch> tranMatchAllocator, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator, bool shallow)
		{
			ComparisonResult comparisonResult = new ComparisonResult
			{
				Similarity = this.Similarity,
				NumeratorWeight = this.NumeratorWeight,
				DenominatorWeight = this.DenominatorWeight,
				TotalLeftWeight = this.TotalLeftWeight,
				TotalRightWeight = this.TotalRightWeight
			};
			if (!shallow)
			{
				comparisonResult.LeftRecordContext = ((this.LeftRecordContext != null) ? this.LeftRecordContext.Clone(intAllocator, byteAllocator) : null);
				comparisonResult.RightRecordContext = ((this.RightRecordContext != null) ? this.RightRecordContext.Clone(intAllocator, byteAllocator) : null);
				comparisonResult.LeftTransformationsApplied = this.LeftTransformationsApplied.Clone(tranMatchAllocator, intAllocator, byteAllocator);
				comparisonResult.RightTransformationsApplied = this.RightTransformationsApplied.Clone(tranMatchAllocator, intAllocator, byteAllocator);
			}
			return comparisonResult;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001E8F0 File Offset: 0x0001CAF0
		public void Reset()
		{
			this.Similarity = 0.0;
			this.NumeratorWeight = (this.DenominatorWeight = 0.0);
			this.TotalLeftWeight = (this.TotalRightWeight = 0.0);
			this.LeftTransformationsApplied.Reset();
			this.RightTransformationsApplied.Reset();
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0001E952 File Offset: 0x0001CB52
		public bool IsNull
		{
			get
			{
				return this.Similarity == double.MinValue;
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001E965 File Offset: 0x0001CB65
		public static ComparisonResult Parse(SqlString s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001E96C File Offset: 0x0001CB6C
		public void Write(BinaryWriter w)
		{
			w.Write(this.Similarity);
			if (this.Similarity > 0.0)
			{
				w.Write(this.NumeratorWeight);
				w.Write(this.DenominatorWeight);
				w.Write(this.TotalLeftWeight);
				w.Write(this.TotalRightWeight);
				byte b = (byte)(((this.LeftRecordContext != null) ? 1 : 0) | ((this.RightRecordContext != null) ? 2 : 0) | ((this.LeftTransformationsApplied.Count > 0) ? 4 : 0) | ((this.RightTransformationsApplied.Count > 0) ? 8 : 0));
				w.Write(b);
				if ((b & 1) != 0)
				{
					this.LeftRecordContext.Write(w);
				}
				if ((b & 2) != 0)
				{
					this.RightRecordContext.Write(w);
				}
				if ((b & 4) != 0)
				{
					this.LeftTransformationsApplied.Write(w);
				}
				if ((b & 8) != 0)
				{
					this.RightTransformationsApplied.Write(w);
				}
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001EA58 File Offset: 0x0001CC58
		public void Read(BinaryReader r, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.Similarity = r.ReadDouble();
			if (this.Similarity > 0.0)
			{
				this.NumeratorWeight = r.ReadDouble();
				this.DenominatorWeight = r.ReadDouble();
				this.TotalLeftWeight = r.ReadDouble();
				this.TotalRightWeight = r.ReadDouble();
				byte b = r.ReadByte();
				if ((b & 1) != 0)
				{
					this.LeftRecordContext = new RecordContext(r, intAllocator, byteAllocator);
				}
				if ((b & 2) != 0)
				{
					this.RightRecordContext = new RecordContext(r, intAllocator, byteAllocator);
				}
				if ((b & 4) != 0)
				{
					this.LeftTransformationsApplied = new ArraySegmentBuilder<TransformationMatch>();
					this.LeftTransformationsApplied.Read(r, intAllocator, byteAllocator);
				}
				if ((b & 8) != 0)
				{
					this.RightTransformationsApplied = new ArraySegmentBuilder<TransformationMatch>();
					this.RightTransformationsApplied.Read(r, intAllocator, byteAllocator);
				}
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001EB1C File Offset: 0x0001CD1C
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001EB1F File Offset: 0x0001CD1F
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001EB26 File Offset: 0x0001CD26
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001EB2D File Offset: 0x0001CD2D
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001EB38 File Offset: 0x0001CD38
		public string ToXml(IDomainManager domainManager, ITokenIdProvider tokenIdProvider, bool verbose)
		{
			StringBuilder stringBuilder = new StringBuilder();
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.CheckCharacters = false;
			xmlWriterSettings.Indent = true;
			xmlWriterSettings.ConformanceLevel = 1;
			XmlWriter xmlWriter = XmlWriter.Create(new StringWriter(stringBuilder), xmlWriterSettings);
			xmlWriter.WriteStartElement("ComparisonResult");
			xmlWriter.WriteAttributeString("similarity", this.Similarity.ToString());
			if (verbose)
			{
				ComparisonResult.CreateWeightedTokenSequenceXml(xmlWriter, "LeftTokens", this.LeftRecordContext.TokenSequence, domainManager, tokenIdProvider);
				if (this.RightRecordContext != null)
				{
					ComparisonResult.CreateWeightedTokenSequenceXml(xmlWriter, "RightTokens", this.RightRecordContext.TokenSequence, domainManager, tokenIdProvider);
				}
				if (this.LeftRecordContext.TransformationMatchList.Count > 0)
				{
					ComparisonResult.CreateTranMatchListXml(xmlWriter, "LeftTransformations", this.LeftRecordContext.TransformationMatchList, domainManager, tokenIdProvider);
				}
				if (this.LeftTransformationsApplied.Count > 0)
				{
					ComparisonResult.CreateTranMatchListXml(xmlWriter, "LeftTransformationsApplied", this.LeftTransformationsApplied, domainManager, tokenIdProvider);
				}
				if (this.RightTransformationsApplied.Count > 0)
				{
					ComparisonResult.CreateTranMatchListXml(xmlWriter, "RightTransformationsApplied", this.RightTransformationsApplied, domainManager, tokenIdProvider);
				}
			}
			xmlWriter.WriteEndElement();
			xmlWriter.Flush();
			return stringBuilder.ToString();
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001EC50 File Offset: 0x0001CE50
		private static void CreateWeightedTokenSequenceXml(XmlWriter writer, string elementName, WeightedTokenSequence tokenSequence, IDomainManager domainManager, ITokenIdProvider tokenIdProvider)
		{
			writer.WriteStartElement(elementName);
			for (int i = 0; i < tokenSequence.Count; i++)
			{
				writer.WriteStartElement("Token");
				writer.WriteAttributeString("domain", domainManager.GetDomainName(tokenIdProvider.GetDomainId(tokenSequence.Tokens[i])));
				writer.WriteAttributeString("id", tokenSequence.Tokens[i].ToString());
				writer.WriteAttributeString("weight", tokenSequence.Weights.Array[tokenSequence.Weights.Offset + i].ToString());
				writer.WriteString(tokenIdProvider.GetToken(tokenSequence.Tokens[i]).ToString());
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001ED30 File Offset: 0x0001CF30
		private static void CreateTranMatchListXml(XmlWriter writer, string elementName, IEnumerable<TransformationMatch> tranMatchList, IDomainManager domainManager, ITokenIdProvider tokenIdProvider)
		{
			writer.WriteStartElement(elementName);
			foreach (TransformationMatch transformationMatch in tranMatchList)
			{
				Transformation transformation = transformationMatch.Transformation;
				transformation.Write(writer, domainManager, tokenIdProvider);
			}
			writer.WriteEndElement();
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001ED90 File Offset: 0x0001CF90
		private static void CreateTranMatchListXml(XmlWriter writer, string elementName, IEnumerable<WeightedTransformationMatch> tranMatchList, IDomainManager domainManager, ITokenIdProvider tokenIdProvider)
		{
			writer.WriteStartElement(elementName);
			foreach (WeightedTransformationMatch weightedTransformationMatch in tranMatchList)
			{
				TransformationMatch transformationMatch = (TransformationMatch)weightedTransformationMatch;
				Transformation transformation = transformationMatch.Transformation;
				transformation.Write(writer, domainManager, tokenIdProvider);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0400028B RID: 651
		public static readonly ComparisonResult Empty = new ComparisonResult();

		// Token: 0x0400028C RID: 652
		public double Similarity;

		// Token: 0x0400028D RID: 653
		public double NumeratorWeight;

		// Token: 0x0400028E RID: 654
		public double DenominatorWeight;

		// Token: 0x0400028F RID: 655
		public double TotalLeftWeight;

		// Token: 0x04000290 RID: 656
		public double TotalRightWeight;

		// Token: 0x04000291 RID: 657
		public RecordContext LeftRecordContext;

		// Token: 0x04000292 RID: 658
		public RecordContext RightRecordContext;

		// Token: 0x04000293 RID: 659
		public ArraySegmentBuilder<TransformationMatch> LeftTransformationsApplied;

		// Token: 0x04000294 RID: 660
		public ArraySegmentBuilder<TransformationMatch> RightTransformationsApplied;
	}
}
