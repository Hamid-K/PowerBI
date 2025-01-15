using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000B6 RID: 182
	[Serializable]
	public class RecordContext : IMemoryUsage, IXmlSerializable
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x0001F18F File Offset: 0x0001D38F
		// (set) Token: 0x060006F0 RID: 1776 RVA: 0x0001F197 File Offset: 0x0001D397
		public ArraySegmentBuilder<WeightedTransformationMatch> TransformationMatchList { get; internal set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001F1A0 File Offset: 0x0001D3A0
		public long MemoryUsage
		{
			get
			{
				return this.TokenSequence.MemoryUsage + this.TransformationMatchList.MemoryUsage + this.ExactMatchTokenSequence.MemoryUsage;
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001F1C5 File Offset: 0x0001D3C5
		public RecordContext()
		{
			this.TransformationMatchList = new ArraySegmentBuilder<WeightedTransformationMatch>();
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001F1D8 File Offset: 0x0001D3D8
		internal RecordContext(bool fast)
		{
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001F1E0 File Offset: 0x0001D3E0
		public RecordContext(BinaryReader r, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.TransformationMatchList = new ArraySegmentBuilder<WeightedTransformationMatch>();
			this.Read(r, intAllocator, byteAllocator);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001F1FC File Offset: 0x0001D3FC
		public RecordContext Clone(ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			return new RecordContext(true)
			{
				TokenSequence = this.TokenSequence.Clone(intAllocator),
				TransformationMatchList = this.TransformationMatchList.Clone(intAllocator, byteAllocator),
				ExactMatchTokenSequence = this.ExactMatchTokenSequence.Clone(intAllocator)
			};
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001F24B File Offset: 0x0001D44B
		public void Reset()
		{
			this.TokenSequence.Clear();
			this.TransformationMatchList.Reset();
			this.ExactMatchTokenSequence.Clear();
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001F26E File Offset: 0x0001D46E
		public void Write(BinaryWriter w)
		{
			this.TokenSequence.Write(w);
			this.TransformationMatchList.Write(w);
			this.ExactMatchTokenSequence.Write(w);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001F294 File Offset: 0x0001D494
		public void Read(BinaryReader r, ISegmentAllocator<int> intAllocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.TokenSequence = default(WeightedTokenSequence);
			this.ExactMatchTokenSequence = default(TokenSequence);
			this.TokenSequence.Read(r, intAllocator);
			this.TransformationMatchList.Read(r, intAllocator, byteAllocator);
			this.ExactMatchTokenSequence.Read(r, intAllocator);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001F2E1 File Offset: 0x0001D4E1
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001F2E4 File Offset: 0x0001D4E4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001F2EB File Offset: 0x0001D4EB
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040002A9 RID: 681
		public WeightedTokenSequence TokenSequence;

		// Token: 0x040002AB RID: 683
		public TokenSequence ExactMatchTokenSequence;
	}
}
