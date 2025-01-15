using System;
using System.Globalization;
using System.IO;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x0200000E RID: 14
	internal class MessageFormatterFactory
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002884 File Offset: 0x00000A84
		internal static IMessageReader CreateReader(Stream s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			BinaryReader binaryReader = new BinaryReader(s, MessageUtil.StringEncoding);
			IMessageReader messageReader;
			try
			{
				uint num = binaryReader.ReadUInt32();
				if (num != 1179510781U)
				{
					throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Stream is not a valid package.", Array.Empty<object>()));
				}
				string text = binaryReader.ReadString();
				int num2 = binaryReader.ReadInt32();
				int num3 = binaryReader.ReadInt32();
				messageReader = MessageFormatterFactory.InternalCreateReader(binaryReader, num, text, num2, num3);
			}
			catch (IOException)
			{
				throw;
			}
			catch (NotSupportedException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new IOException("header", ex);
			}
			return messageReader;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002938 File Offset: 0x00000B38
		internal static IMessageWriter CreateWriter(Stream s, string format, int majorVersion, int minorVersion)
		{
			return MessageFormatterFactory.CreateWriter(s, format, majorVersion, minorVersion, true);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002944 File Offset: 0x00000B44
		internal static IMessageWriter CreateWriter(Stream s, string format, int majorVersion, int minorVersion, bool writePackagePrefix)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (string.IsNullOrEmpty(format))
			{
				throw new ArgumentException("format");
			}
			BinaryWriter binaryWriter = new BinaryWriter(s, MessageUtil.StringEncoding);
			if (writePackagePrefix)
			{
				binaryWriter.Write(1179510781U);
				binaryWriter.Write(format);
				binaryWriter.Write(majorVersion);
				binaryWriter.Write(minorVersion);
			}
			return MessageFormatterFactory.InternalCreateWriter(binaryWriter, format, majorVersion, minorVersion);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029AB File Offset: 0x00000BAB
		private static IMessageReader InternalCreateReader(BinaryReader reader, uint marker, string format, int majorVersion, int minorVersion)
		{
			if (format.Equals("Progressive", StringComparison.Ordinal) && majorVersion == 1 && minorVersion == 0)
			{
				return new ProgressiveReader(reader);
			}
			throw MessageFormatterFactory.NotSupportedException(format, majorVersion, minorVersion);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029D3 File Offset: 0x00000BD3
		private static IMessageWriter InternalCreateWriter(BinaryWriter writer, string format, int majorVersion, int minorVersion)
		{
			if (format.Equals("Progressive", StringComparison.Ordinal) && majorVersion == 1 && minorVersion == 0)
			{
				return new ProgressiveWriter(writer);
			}
			throw MessageFormatterFactory.NotSupportedException(format, majorVersion, minorVersion);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000029F9 File Offset: 0x00000BF9
		private static NotSupportedException NotSupportedException(string format, int majorVersion, int minorVersion)
		{
			return new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "{0} {1}.{2}", format, majorVersion, minorVersion));
		}

		// Token: 0x0400001F RID: 31
		internal const string ProgressivePackagingMimeType = "application/progressive-report";

		// Token: 0x04000020 RID: 32
		internal const uint FileMarker = 1179510781U;
	}
}
