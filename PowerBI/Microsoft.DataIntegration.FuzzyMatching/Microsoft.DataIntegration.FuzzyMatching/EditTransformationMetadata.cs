using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000F7 RID: 247
	[Serializable]
	public struct EditTransformationMetadata : IBinarySerialize, IXmlSerializable
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002DB18 File Offset: 0x0002BD18
		public float EditDistance
		{
			get
			{
				return (float)this.Numerator / (float)this.Denominator;
			}
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002DB2C File Offset: 0x0002BD2C
		public EditTransformationMetadata(BinaryReader r)
		{
			this.Numerator = (this.Denominator = (this.PrefixMatchLength = (this.MaxLength = 0)));
			this.Read(r);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002DB64 File Offset: 0x0002BD64
		public EditTransformationMetadata(XmlReader r)
		{
			this.Numerator = (this.Denominator = (this.PrefixMatchLength = (this.MaxLength = 0)));
			this.ReadXml(r);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002DB9C File Offset: 0x0002BD9C
		public EditTransformationMetadata(ArraySegment<byte> metadata)
		{
			this.Numerator = (this.Denominator = (this.PrefixMatchLength = (this.MaxLength = 0)));
			this.Read(metadata);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002DBD4 File Offset: 0x0002BDD4
		public void Write(ArraySegment<byte> metadata)
		{
			int offset = metadata.Offset;
			metadata.Array[offset++] = BitOperations.GetUpperByte(this.Numerator);
			metadata.Array[offset++] = BitOperations.GetLowerByte(this.Numerator);
			metadata.Array[offset++] = BitOperations.GetUpperByte(this.Denominator);
			metadata.Array[offset++] = BitOperations.GetLowerByte(this.Denominator);
			metadata.Array[offset++] = BitOperations.GetUpperByte(this.PrefixMatchLength);
			metadata.Array[offset++] = BitOperations.GetLowerByte(this.PrefixMatchLength);
			metadata.Array[offset++] = BitOperations.GetUpperByte(this.MaxLength);
			metadata.Array[offset++] = BitOperations.GetLowerByte(this.MaxLength);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002DCAC File Offset: 0x0002BEAC
		public void Read(ArraySegment<byte> metadata)
		{
			int offset = metadata.Offset;
			this.Numerator = (short)(((int)metadata.Array[offset++] << 8) | (int)metadata.Array[offset++]);
			this.Denominator = (short)(((int)metadata.Array[offset++] << 8) | (int)metadata.Array[offset++]);
			this.PrefixMatchLength = (short)(((int)metadata.Array[offset++] << 8) | (int)metadata.Array[offset++]);
			this.MaxLength = (short)(((int)metadata.Array[offset++] << 8) | (int)metadata.Array[offset++]);
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002DD51 File Offset: 0x0002BF51
		public void Write(BinaryWriter w)
		{
			w.Write(this.Numerator);
			w.Write(this.Denominator);
			w.Write(this.PrefixMatchLength);
			w.Write(this.MaxLength);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002DD83 File Offset: 0x0002BF83
		public void Read(BinaryReader r)
		{
			this.Numerator = r.ReadInt16();
			this.Denominator = r.ReadInt16();
			this.PrefixMatchLength = r.ReadInt16();
			this.MaxLength = r.ReadInt16();
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002DDB8 File Offset: 0x0002BFB8
		public override string ToString()
		{
			return string.Format("<EditTransformation similarity=\"{0:F4}\" prefixMatchLength=\"{1}\" maxLength=\"{2}\" numerator=\"{3}\" denominator=\"{4}\"/>", new object[]
			{
				1.0 - (double)this.EditDistance,
				this.PrefixMatchLength,
				this.MaxLength,
				this.Numerator,
				this.Denominator
			});
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002DE26 File Offset: 0x0002C026
		public XmlSchema GetSchema()
		{
			return null;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002DE2C File Offset: 0x0002C02C
		public void ReadXml(XmlReader reader)
		{
			if (reader.ReadToFollowing("EditTransformation"))
			{
				if (reader.MoveToAttribute("numerator"))
				{
					this.Numerator = short.Parse(reader.Value);
				}
				if (reader.MoveToAttribute("denominator"))
				{
					this.Denominator = short.Parse(reader.Value);
				}
				if (reader.MoveToAttribute("prefixMatchLength"))
				{
					this.PrefixMatchLength = short.Parse(reader.Value);
				}
				if (reader.MoveToAttribute("maxLength"))
				{
					this.MaxLength = short.Parse(reader.Value);
				}
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002DEC0 File Offset: 0x0002C0C0
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("EditTransformation");
			writer.WriteAttributeString("numerator", this.Numerator.ToString());
			writer.WriteAttributeString("denominator", this.Denominator.ToString());
			writer.WriteAttributeString("prefixMatchLength", this.PrefixMatchLength.ToString());
			writer.WriteAttributeString("maxLength", this.MaxLength.ToString());
			writer.WriteEndElement();
		}

		// Token: 0x040003DB RID: 987
		public const int Length = 8;

		// Token: 0x040003DC RID: 988
		public short Numerator;

		// Token: 0x040003DD RID: 989
		public short Denominator;

		// Token: 0x040003DE RID: 990
		public short PrefixMatchLength;

		// Token: 0x040003DF RID: 991
		public short MaxLength;
	}
}
