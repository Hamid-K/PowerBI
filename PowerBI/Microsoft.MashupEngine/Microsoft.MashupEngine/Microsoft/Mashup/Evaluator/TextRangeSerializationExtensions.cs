using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D74 RID: 7540
	public static class TextRangeSerializationExtensions
	{
		// Token: 0x0600BB51 RID: 47953 RVA: 0x0025EF82 File Offset: 0x0025D182
		public static void WriteSourceLocation(this BinaryWriter writer, SourceLocation location)
		{
			writer.WriteNullableString(location.Document.UniqueID);
			writer.WriteTextRange(location.Range);
		}

		// Token: 0x0600BB52 RID: 47954 RVA: 0x0025EFA4 File Offset: 0x0025D1A4
		public static SourceLocation ReadSourceLocation(this BinaryReader reader)
		{
			string text = reader.ReadNullableString();
			TextRange textRange = reader.ReadTextRange();
			return new SourceLocation(new TextRangeSerializationExtensions.SerializedDocumentHost(text), textRange);
		}

		// Token: 0x0600BB53 RID: 47955 RVA: 0x0025EFC9 File Offset: 0x0025D1C9
		public static void WriteTextRange(this BinaryWriter writer, TextRange textRange)
		{
			writer.WriteTextPosition(textRange.Start);
			writer.WriteTextPosition(textRange.End);
		}

		// Token: 0x0600BB54 RID: 47956 RVA: 0x0025EFE8 File Offset: 0x0025D1E8
		public static TextRange ReadTextRange(this BinaryReader reader)
		{
			TextPosition textPosition = reader.ReadTextPosition();
			TextPosition textPosition2 = reader.ReadTextPosition();
			return new TextRange(textPosition, textPosition2);
		}

		// Token: 0x0600BB55 RID: 47957 RVA: 0x0025F008 File Offset: 0x0025D208
		public static void WriteTextPosition(this BinaryWriter writer, TextPosition textPosition)
		{
			writer.WriteInt32(textPosition.Row);
			writer.WriteInt32(textPosition.Column);
		}

		// Token: 0x0600BB56 RID: 47958 RVA: 0x0025F024 File Offset: 0x0025D224
		public static TextPosition ReadTextPosition(this BinaryReader reader)
		{
			int num = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			return new TextPosition(num, num2);
		}

		// Token: 0x02001D75 RID: 7541
		private sealed class SerializedDocumentHost : IDocumentHost
		{
			// Token: 0x0600BB57 RID: 47959 RVA: 0x0025F044 File Offset: 0x0025D244
			public SerializedDocumentHost(string uniqueID)
			{
				this.uniqueID = uniqueID;
			}

			// Token: 0x17002E3E RID: 11838
			// (get) Token: 0x0600BB58 RID: 47960 RVA: 0x0025F053 File Offset: 0x0025D253
			public string UniqueID
			{
				get
				{
					return this.uniqueID;
				}
			}

			// Token: 0x04005F63 RID: 24419
			private readonly string uniqueID;
		}
	}
}
