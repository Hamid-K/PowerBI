using System;
using System.IO;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019EC RID: 6636
	internal static class IErrorSerializationExtensions
	{
		// Token: 0x0600A7EB RID: 42987 RVA: 0x0022B848 File Offset: 0x00229A48
		public static void WriteIError(this BinaryWriter writer, IError error)
		{
			writer.WriteInt32((int)error.Kind);
			writer.WriteSourceLocation(error.Location);
			writer.WriteNullableString(error.Message);
			IDuplicateIdentifierError duplicateIdentifierError = error as IDuplicateIdentifierError;
			if (duplicateIdentifierError != null)
			{
				writer.WriteInt32(1);
				writer.WriteNullableString(duplicateIdentifierError.Name);
				return;
			}
			IUnknownIdentifierError unknownIdentifierError = error as IUnknownIdentifierError;
			if (unknownIdentifierError != null)
			{
				writer.WriteInt32(2);
				writer.WriteNullableString(unknownIdentifierError.Section);
				writer.WriteNullableString(unknownIdentifierError.Name);
				return;
			}
			writer.WriteInt32(0);
		}

		// Token: 0x0600A7EC RID: 42988 RVA: 0x0022B8C8 File Offset: 0x00229AC8
		public static IError ReadIError(this BinaryReader reader)
		{
			ErrorKind errorKind = (ErrorKind)reader.ReadInt32();
			SourceLocation sourceLocation = reader.ReadSourceLocation();
			string text = reader.ReadNullableString();
			switch (reader.ReadInt32())
			{
			case 0:
				return SerializedSourceError.Create(errorKind, sourceLocation, text);
			case 1:
			{
				string text2 = reader.ReadNullableString();
				return SerializedSourceError.Create(errorKind, sourceLocation, text, text2);
			}
			case 2:
			{
				string text3 = reader.ReadNullableString();
				string text4 = reader.ReadNullableString();
				return SerializedSourceError.Create(errorKind, sourceLocation, text, text3, text4);
			}
			default:
				throw new InvalidOperationException("Unknown error kind.");
			}
		}

		// Token: 0x020019ED RID: 6637
		private enum ErrorType
		{
			// Token: 0x0400576F RID: 22383
			Other,
			// Token: 0x04005770 RID: 22384
			DuplicateIdentifier,
			// Token: 0x04005771 RID: 22385
			UnknownIdentifier
		}
	}
}
