using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200010D RID: 269
	[DebuggerDisplay("{ToString()}")]
	[Serializable]
	public struct Transformation : IMemoryUsage, INullable
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x00031B30 File Offset: 0x0002FD30
		public Transformation Clone(ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			return new Transformation
			{
				From = this.From.Clone(allocator),
				To = this.To.Clone(allocator),
				Type = this.Type,
				Metadata = this.Metadata.Clone(byteAllocator)
			};
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00031B96 File Offset: 0x0002FD96
		public Transformation(BinaryReader r, ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			this = default(Transformation);
			this.Read(r, allocator, byteAllocator);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00031BA8 File Offset: 0x0002FDA8
		public Transformation(TokenSequence from, TokenSequence to)
		{
			this.From = from;
			this.To = to;
			this.Type = TransformationType.Undefined;
			this.Metadata = default(ArraySegment<byte>);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00031BCB File Offset: 0x0002FDCB
		public Transformation(TokenSequence from, TokenSequence to, TransformationType typeId)
		{
			this.From = from;
			this.To = to;
			this.Type = typeId;
			this.Metadata = default(ArraySegment<byte>);
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00031BEE File Offset: 0x0002FDEE
		public bool IsNull
		{
			get
			{
				return this.Type == TransformationType.Null;
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00031BFC File Offset: 0x0002FDFC
		public void Write(XmlWriter writer, IDomainManager domainManager, ITokenIdProvider tokenIdProvider)
		{
			writer.WriteStartElement("Transformation");
			writer.WriteAttributeString("typeId", this.Type.ToString());
			writer.WriteAttributeString("domain", domainManager.GetDomainName(tokenIdProvider.GetDomainId(this.From[0])));
			if (this.Type == TransformationType.EditTransformation)
			{
				EditTransformationMetadata editTransformationMetadata = new EditTransformationMetadata(this.Metadata);
				writer.WriteAttributeString("editDistance", editTransformationMetadata.EditDistance.ToString());
				writer.WriteAttributeString("prefixMatchLength", editTransformationMetadata.PrefixMatchLength.ToString());
				writer.WriteAttributeString("maxLength", editTransformationMetadata.MaxLength.ToString());
			}
			else if (this.Type == TransformationType.PrefixTransformation)
			{
				PrefixTransformationMetadata prefixTransformationMetadata = new PrefixTransformationMetadata(this.Metadata);
				writer.WriteAttributeString("prefixMatchLength", prefixTransformationMetadata.PrefixMatchLength.ToString());
				writer.WriteAttributeString("maxLength", prefixTransformationMetadata.MaxLength.ToString());
			}
			writer.WriteStartElement("From");
			writer.WriteString(this.From.ToVerboseString(tokenIdProvider));
			writer.WriteEndElement();
			writer.WriteStartElement("To");
			writer.WriteString(this.To.ToVerboseString(tokenIdProvider));
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00031D44 File Offset: 0x0002FF44
		public void Write(BinaryWriter w)
		{
			this.From.Write(w);
			this.To.Write(w);
			w.Write((int)this.Type);
			w.Write(this.Metadata.Count);
			if (this.Metadata.Count > 0)
			{
				w.Write(this.Metadata.Array, this.Metadata.Offset, this.Metadata.Count);
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00031DBC File Offset: 0x0002FFBC
		public void Read(BinaryReader r, ISegmentAllocator<int> allocator, ISegmentAllocator<byte> byteAllocator)
		{
			this.From.Read(r, allocator);
			this.To.Read(r, allocator);
			this.Type = (TransformationType)r.ReadInt32();
			int num = r.ReadInt32();
			this.Metadata = byteAllocator.New(num);
			if (num > 0)
			{
				r.BaseStream.Read(this.Metadata.Array, this.Metadata.Offset, num);
			}
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00031E2A File Offset: 0x0003002A
		public static Transformation Parse(SqlString s)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x00031E31 File Offset: 0x00030031
		internal bool IsUnitRule
		{
			get
			{
				return this.From.Count == 1 && this.To.Count == 1;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00031E51 File Offset: 0x00030051
		internal bool IsUnitMulti
		{
			get
			{
				return this.From.Count == 1 && this.To.Count != 1;
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00031E74 File Offset: 0x00030074
		public override string ToString()
		{
			return this.From.ToString() + " => " + this.To.ToString();
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00031EA2 File Offset: 0x000300A2
		public string ToVerboseString(ITokenIdProvider tokenIdProvider)
		{
			return this.From.ToVerboseString(tokenIdProvider) + " => " + this.To.ToVerboseString(tokenIdProvider);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00031EC6 File Offset: 0x000300C6
		public override bool Equals(object obj)
		{
			return obj is Transformation && this.Equals((Transformation)obj);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00031EE0 File Offset: 0x000300E0
		public bool Equals(Transformation t)
		{
			return t.Type == this.Type && t.From.Equals(this.From) && t.To.Equals(this.To) && t.Metadata.DeepEquals(this.Metadata);
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00031F36 File Offset: 0x00030136
		public long MemoryUsage
		{
			get
			{
				return 2L + this.From.MemoryUsage + this.To.MemoryUsage + (long)(16 + this.Metadata.Count);
			}
		}

		// Token: 0x0400044D RID: 1101
		public static readonly Transformation Null = new Transformation
		{
			Type = TransformationType.Null
		};

		// Token: 0x0400044E RID: 1102
		public TokenSequence From;

		// Token: 0x0400044F RID: 1103
		public TokenSequence To;

		// Token: 0x04000450 RID: 1104
		public TransformationType Type;

		// Token: 0x04000451 RID: 1105
		public ArraySegment<byte> Metadata;
	}
}
