using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Microsoft.Analytics.Modules.R.ErrorHandling.RException.Primitives;

namespace Microsoft.MetaAnalytics.RDataSupport
{
	// Token: 0x02000170 RID: 368
	public class RReader
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x0000B664 File Offset: 0x00009864
		private RReader(MultiEncodingBinaryReader readStream)
		{
			if (readStream is AsciiBinaryReader)
			{
				this.integerReader = () => ((AsciiBinaryReader)this.reader).ReadInteger();
			}
			else
			{
				this.integerReader = new Func<int?>(this.ReadBinaryInteger);
			}
			this.reader = readStream;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0000B6B8 File Offset: 0x000098B8
		public static RObject ReadSerialized(string filename)
		{
			RObject robject;
			using (FileStream fileStream = File.Open(filename, FileMode.Open))
			{
				robject = RReader.ReadSerialized(fileStream);
			}
			return robject;
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0000B6F4 File Offset: 0x000098F4
		public static RObject ReadSerialized(Stream stream)
		{
			return RReader.ConstructFromStream(stream).ReadObject();
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0000B704 File Offset: 0x00009904
		public static KeyValuePair<string, RObject>[] ReadWorkspace(byte[] binary, bool compressed = true)
		{
			KeyValuePair<string, RObject>[] array2;
			using (Stream stream = (compressed ? new GZipStream(new MemoryStream(binary, false), CompressionMode.Decompress) : new MemoryStream(binary, false)))
			{
				byte[] array = new byte[5];
				stream.Read(array, 0, 5);
				if (!array.SequenceEqual(RConstants.WorkspaceHeader))
				{
					throw new NotValidRDataException();
				}
				array2 = RReader.ConstructFromStream(stream).ReadAttributeList();
			}
			return array2;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0000B778 File Offset: 0x00009978
		public static KeyValuePair<string, RObject>[] ReadWorkspace(string filename)
		{
			KeyValuePair<string, RObject>[] array2;
			using (GZipStream gzipStream = new GZipStream(File.Open(filename, FileMode.Open), CompressionMode.Decompress))
			{
				byte[] array = new byte[5];
				gzipStream.Read(array, 0, 5);
				if (!array.SequenceEqual(RConstants.WorkspaceHeader))
				{
					throw new NotValidRDataException();
				}
				array2 = RReader.ConstructFromStream(gzipStream).ReadAttributeList();
			}
			return array2;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0000B7E0 File Offset: 0x000099E0
		private static RReader ConstructFromStream(Stream stream)
		{
			byte[] array = new byte[2];
			stream.Read(array, 0, 2);
			if (array[1] == 13)
			{
				array[1] = (byte)stream.ReadByte();
			}
			if (array[1] != 10)
			{
				throw new NotValidRDataException();
			}
			byte b = array[0];
			MultiEncodingBinaryReader multiEncodingBinaryReader;
			if (b != 65)
			{
				if (b != 66)
				{
					if (b != 88)
					{
						throw new NotValidRDataException();
					}
					multiEncodingBinaryReader = new BigEndianBinaryReader(stream);
				}
				else
				{
					multiEncodingBinaryReader = new MultiEncodingBinaryReader(stream);
				}
			}
			else
			{
				multiEncodingBinaryReader = new AsciiBinaryReader(stream);
			}
			if (multiEncodingBinaryReader.ReadInt32() != 2)
			{
				throw new NotValidRDataException();
			}
			multiEncodingBinaryReader.ReadInt32();
			multiEncodingBinaryReader.ReadInt32();
			return new RReader(multiEncodingBinaryReader);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0000B874 File Offset: 0x00009A74
		private string ReadSymbolOrString()
		{
			RTypeTag rtypeTag = this.reader.ReadInt32();
			RType rtype = rtypeTag.GetRType();
			if (rtype == RType.SYMSXP)
			{
				string text = this.ReadSymbolOrString();
				this.symbols.Add(text);
				return text;
			}
			if (rtype == RType.CHARSXP)
			{
				return this.reader.ReadString(this.GetStringEncodingFromRTypeTag(rtypeTag));
			}
			if (rtype != RType.SYMBOLREF)
			{
				throw new NotImplementedTypeParserException(rtypeTag.GetRType().ToString());
			}
			int length = rtypeTag.GetLength();
			if (length <= 0)
			{
				throw new NotValidRDataException();
			}
			return this.symbols[length - 1];
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0000B914 File Offset: 0x00009B14
		private MultiEncodingBinaryReader.EncodingOptions GetStringEncodingFromRTypeTag(RTypeTag typeTag)
		{
			RTypeTag.REncodingOptions encoding = typeTag.GetEncoding();
			if (encoding != RTypeTag.REncodingOptions.Latin1 && encoding == RTypeTag.REncodingOptions.UTF8)
			{
				return MultiEncodingBinaryReader.EncodingOptions.UTF8;
			}
			return MultiEncodingBinaryReader.EncodingOptions.Latin1;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0000B934 File Offset: 0x00009B34
		private KeyValuePair<string, RObject>[] ReadAttributeList()
		{
			List<KeyValuePair<string, RObject>> list = new List<KeyValuePair<string, RObject>>();
			RTypeTag rtypeTag = this.reader.ReadInt32();
			while (rtypeTag.GetRType() == RType.LISTSXP)
			{
				bool tagFlag = rtypeTag.GetTagFlag();
				string text = null;
				if (tagFlag)
				{
					text = this.ReadSymbolOrString();
				}
				RObject robject = this.ReadObject();
				if (robject != null)
				{
					list.Add(new KeyValuePair<string, RObject>(text, robject));
				}
				rtypeTag = this.reader.ReadInt32();
			}
			if (rtypeTag.GetRType() != RType.EMPTYTAIL && rtypeTag.GetRType() != RType.STRSXP)
			{
				throw new NotImplementedTypeParserException(rtypeTag.GetRType().ToString());
			}
			return list.ToArray();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0000B9DC File Offset: 0x00009BDC
		private RObject ReadObject()
		{
			RTypeTag rtypeTag = this.reader.ReadInt32();
			bool flag = false;
			RType rtype = rtypeTag.GetRType();
			RObject robject;
			if (rtype <= RType.VECSXP)
			{
				if (rtype == RType.SYMSXP)
				{
					string text = this.ReadSymbolOrString();
					this.symbols.Add(text);
					return new RObject<string>(new string[] { text }, false, false);
				}
				switch (rtype)
				{
				case RType.LANGSXP:
					robject = new RObject<RObject>(null, true, false)
					{
						Attributes = this.ReadAttributeList()
					};
					goto IL_036B;
				case RType.SPECIALSXP:
				case RType.BUILTINSXP:
				case RType.CHARSXP:
				case (RType)11:
				case (RType)12:
					break;
				case RType.LGLSXP:
				{
					int num = this.reader.ReadInt32();
					bool?[] array = new bool?[num];
					for (int i = 0; i < num; i++)
					{
						int? num2 = this.integerReader();
						if (num2 != null)
						{
							int valueOrDefault = num2.GetValueOrDefault();
							if (valueOrDefault > 1)
							{
								throw new NotImplementedTypeParserException(typeof(bool).ToString());
							}
							bool?[] array2 = array;
							int num3 = i;
							int? num4 = num2;
							int num5 = 0;
							array2[num3] = new bool?(!((num4.GetValueOrDefault() == num5) & (num4 != null)));
						}
						else
						{
							array[i] = null;
							flag = true;
						}
					}
					robject = new RObject<bool?>(array, flag, false);
					goto IL_036B;
				}
				case RType.INTSXP:
				{
					int num = this.reader.ReadInt32();
					int?[] array3 = new int?[num];
					for (int j = 0; j < num; j++)
					{
						int? num6 = this.integerReader();
						if (!flag && num6 == null)
						{
							flag = true;
						}
						array3[j] = num6;
					}
					robject = new RObject<int?>(array3, flag, false);
					goto IL_036B;
				}
				case RType.REALSXP:
				{
					int num = this.reader.ReadInt32();
					double?[] array4 = new double?[num];
					for (int k = 0; k < num; k++)
					{
						double num7 = this.reader.ReadDouble();
						if (!double.IsNaN(num7))
						{
							array4[k] = new double?(num7);
						}
						else
						{
							flag = true;
						}
					}
					robject = new RObject<double?>(array4, flag, false);
					goto IL_036B;
				}
				case RType.CPLXSXP:
				{
					int num = this.reader.ReadInt32();
					Complex[] array5 = new Complex[num];
					for (int l = 0; l < num; l++)
					{
						array5[l].re = this.reader.ReadDouble();
						array5[l].im = this.reader.ReadDouble();
					}
					robject = new RObject<Complex>(array5, true, false);
					goto IL_036B;
				}
				case RType.STRSXP:
				{
					int num = this.reader.ReadInt32();
					string[] array6 = new string[num];
					for (int m = 0; m < num; m++)
					{
						array6[m] = this.ReadSymbolOrString();
					}
					robject = new RObject<string>(array6, true, false);
					goto IL_036B;
				}
				default:
					if (rtype == RType.VECSXP)
					{
						int num = this.reader.ReadInt32();
						List<RObject> list = new List<RObject>();
						for (int n = 0; n < num; n++)
						{
							list.Add(this.ReadObject());
						}
						robject = new RObject<RObject>(list.ToArray(), true, false);
						goto IL_036B;
					}
					break;
				}
			}
			else
			{
				if (rtype == RType.EXTPTRSXP)
				{
					this.reader.ReadInt64();
					robject = null;
					goto IL_036B;
				}
				if (rtype == RType.RAWSXP)
				{
					int num = this.reader.ReadInt32();
					robject = new RObject<byte>(this.reader.ReadBytes(num), false, true);
					goto IL_036B;
				}
				if (rtype == RType.EMPTYTAIL)
				{
					return null;
				}
			}
			throw new NotImplementedTypeParserException(rtypeTag.GetRType().ToString());
			IL_036B:
			if (rtypeTag.GetAttributeFlag())
			{
				robject.Attributes = this.ReadAttributeList();
			}
			return robject;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000BD6C File Offset: 0x00009F6C
		private int? ReadBinaryInteger()
		{
			int num = this.reader.ReadInt32();
			if (num == -2147483648)
			{
				return null;
			}
			return new int?(num);
		}

		// Token: 0x0400042F RID: 1071
		private readonly MultiEncodingBinaryReader reader;

		// Token: 0x04000430 RID: 1072
		private readonly List<string> symbols = new List<string>();

		// Token: 0x04000431 RID: 1073
		private readonly Func<int?> integerReader;
	}
}
