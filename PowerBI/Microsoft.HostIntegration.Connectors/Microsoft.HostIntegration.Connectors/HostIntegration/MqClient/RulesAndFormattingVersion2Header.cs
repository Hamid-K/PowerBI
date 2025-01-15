using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;
using Microsoft.HostIntegration.Nls;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B35 RID: 2869
	public class RulesAndFormattingVersion2Header : MqUnstructuredHeader
	{
		// Token: 0x170015DF RID: 5599
		// (get) Token: 0x06005AA8 RID: 23208 RVA: 0x0017596B File Offset: 0x00173B6B
		// (set) Token: 0x06005AA9 RID: 23209 RVA: 0x00175973 File Offset: 0x00173B73
		public RulesAndFormattingVersion2Flag Flags { get; set; }

		// Token: 0x170015E0 RID: 5600
		// (get) Token: 0x06005AAA RID: 23210 RVA: 0x0017597C File Offset: 0x00173B7C
		// (set) Token: 0x06005AAB RID: 23211 RVA: 0x00175984 File Offset: 0x00173B84
		public int CcsidForNamesAndValues
		{
			get
			{
				return this.ccsidForNamesAndValues;
			}
			set
			{
				this.ChangesMade(true);
				if (value != 1200 && value != 13488 && value != 17584 && value != 1208)
				{
					throw new CustomMqClientException(SR.CcsidNamesValuesInvalid(value));
				}
				this.ccsidForNamesAndValues = value;
			}
		}

		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06005AAC RID: 23212 RVA: 0x001759D0 File Offset: 0x00173BD0
		// (set) Token: 0x06005AAD RID: 23213 RVA: 0x001759D8 File Offset: 0x00173BD8
		public NumericEncoding NumericEncodingForLengths
		{
			get
			{
				return this.numericEncodingForLengths;
			}
			set
			{
				this.ChangesMade(true);
				this.numericEncodingForLengths = value;
			}
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06005AAE RID: 23214 RVA: 0x001759E8 File Offset: 0x00173BE8
		// (set) Token: 0x06005AAF RID: 23215 RVA: 0x001759F0 File Offset: 0x00173BF0
		public Rf2hFolderCollection Folders { get; private set; }

		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06005AB0 RID: 23216 RVA: 0x001759F9 File Offset: 0x00173BF9
		// (set) Token: 0x06005AB1 RID: 23217 RVA: 0x00175A01 File Offset: 0x00173C01
		internal Rf2hHeaderPropertyCollection Properties { get; private set; }

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06005AB2 RID: 23218 RVA: 0x001753EE File Offset: 0x001735EE
		public override int AsciiStructId
		{
			get
			{
				return 541607506;
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06005AB3 RID: 23219 RVA: 0x001753F5 File Offset: 0x001735F5
		public override int EbcdicStructId
		{
			get
			{
				return 1086899929;
			}
		}

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06005AB4 RID: 23220 RVA: 0x0003A22E File Offset: 0x0003842E
		public override int MinimumVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x0003A22E File Offset: 0x0003842E
		public override int MaximumVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06005AB6 RID: 23222 RVA: 0x00175A0C File Offset: 0x00173C0C
		public RulesAndFormattingVersion2Header()
			: base(MqHeaderType.RulesAndFormattingVersion2, OrderedMqHeaderType.RulesAndFormattingVersion2, "Rules and Formatting (Version 2) Header", "MQHRF2", 36)
		{
			this.Folders = new Rf2hFolderCollection(this);
			this.Properties = new Rf2hHeaderPropertyCollection(this);
			this.Flags = RulesAndFormattingVersion2Flag.None;
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x00175A64 File Offset: 0x00173C64
		internal unsafe override void GenerateBytes(byte[] buffer, int offset, string format, bool littleEndian, int ccsid, int numericEncodingValue, int ccsidValue)
		{
			HisEncoding encoding = HisEncoding.GetEncoding(ccsid);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				*(ptr3++) = 541607506;
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, 2);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, this.SendLength);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, numericEncodingValue);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, ccsidValue);
				byte* ptr4 = (byte*)ptr3;
				int num = offset + (int)((long)(ptr4 - ptr2));
				ConversionHelpers.MoveStringToBufferSingleByte(buffer, num, format, 8, true, encoding);
				ptr3 += 2;
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, (int)this.Flags);
				*(ptr3++) = ConversionHelpers.ChangeEndiannessIfNeeded(littleEndian, this.CcsidForNamesAndValues);
				if (this.preparedBytes != null)
				{
					ptr4 = (byte*)ptr3;
					num = offset + (int)((long)(ptr4 - ptr2));
					Buffer.BlockCopy(this.preparedBytes, 0, buffer, num, this.preparedBytes.Length);
				}
			}
		}

		// Token: 0x06005AB8 RID: 23224 RVA: 0x00175B4C File Offset: 0x00173D4C
		internal unsafe override bool TryExtract(byte[] buffer, int numberOfBytesAvailable, int offset, bool truncationInEffect, ref int ccsidToUse, ref int numericEncodingToUse, out string nextFormat)
		{
			nextFormat = null;
			bool flag = NumericEncoding.EncodingValueIsLittleEndian(numericEncodingToUse);
			HisEncoding encoding = HisEncoding.GetEncoding(ccsidToUse);
			this.numericEncodingForLengths = NumericEncoding.GetInstance(numericEncodingToUse);
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				int* ptr3 = (int*)ptr2;
				ptr3 += 2;
				int num = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
				if (numberOfBytesAvailable - offset < num)
				{
					if (truncationInEffect)
					{
						return false;
					}
					throw new InvalidOperationException("Not enough bytes for all of the header!");
				}
				else
				{
					int num2 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
					int num3 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
					byte* ptr4 = (byte*)ptr3;
					int num4 = offset + (int)((long)(ptr4 - ptr2));
					nextFormat = ConversionHelpers.GetStringOrNull(buffer, num4, 8, encoding);
					ptr4 += 8;
					ptr3 = (int*)ptr4;
					this.Flags = (RulesAndFormattingVersion2Flag)ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
					this.ccsidForNamesAndValues = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
					int num5;
					if (!(flag ? RulesAndFormattingVersion2Header.ibmToWindowsCodePagesLittleEndian : RulesAndFormattingVersion2Header.ibmToWindowsCodePagesBigEndian).TryGetValue(this.ccsidForNamesAndValues, out num5))
					{
						throw new InvalidOperationException("Code Page for Folder is not allowed!");
					}
					ptr4 = (byte*)ptr3;
					num4 = offset + (int)((long)(ptr4 - ptr2));
					this.preparedBytes = new byte[num - base.SendLength];
					Array.Copy(buffer, num4, this.preparedBytes, 0, this.preparedBytes.Length);
					base.Bytes = this.preparedBytes;
					this.firstUsageWasOld = new bool?(false);
					if (base.Bytes.Length != 0)
					{
						int i = base.Bytes.Length;
						Encoding encoding2 = Encoding.GetEncoding(num5);
						while (i > 0)
						{
							if (i < 4)
							{
								throw new InvalidOperationException("Not enough bytes for length of a folder!");
							}
							int num6 = ConversionHelpers.ExtractIntFromAddress(ref ptr3, flag);
							i -= 4;
							if (num6 != 0)
							{
								if (num6 % 4 != 0)
								{
									throw new InvalidOperationException("Length of folder string is not multiple of 4!");
								}
								if (num6 > i)
								{
									throw new InvalidOperationException("Not enough bytes for folder string!");
								}
								ptr4 = (byte*)ptr3;
								this.Folders.Add(Rf2hFolder.GetInstance(encoding2.GetString(ptr4, num6)));
								i -= num6;
								ptr4 += num6;
								ptr3 = (int*)ptr4;
							}
						}
					}
					ptr = null;
					this.firstUsageWasOld = null;
					numericEncodingToUse = num2;
					ccsidToUse = num3;
					return true;
				}
			}
		}

		// Token: 0x06005AB9 RID: 23225 RVA: 0x00175D48 File Offset: 0x00173F48
		internal override MqHeader GenerateCopy(bool deepCopy)
		{
			if (!deepCopy)
			{
				return this;
			}
			RulesAndFormattingVersion2Header rulesAndFormattingVersion2Header = new RulesAndFormattingVersion2Header();
			rulesAndFormattingVersion2Header.Flags = this.Flags;
			rulesAndFormattingVersion2Header.CcsidForNamesAndValues = this.CcsidForNamesAndValues;
			rulesAndFormattingVersion2Header.NumericEncodingForLengths = this.NumericEncodingForLengths;
			foreach (Rf2hFolder rf2hFolder in this.Folders)
			{
				rulesAndFormattingVersion2Header.Folders.Add(rf2hFolder.GenerateCopy());
			}
			if (base.Bytes != null)
			{
				byte[] array = new byte[base.Bytes.Length];
				Array.Copy(base.Bytes, array, base.Bytes.Length);
				rulesAndFormattingVersion2Header.Bytes = array;
			}
			rulesAndFormattingVersion2Header.firstUsageWasOld = null;
			return rulesAndFormattingVersion2Header;
		}

		// Token: 0x06005ABA RID: 23226 RVA: 0x00175E0C File Offset: 0x0017400C
		internal override void Prepare()
		{
			this.preparedBytes = ((this.firstUsageWasOld != null && !this.firstUsageWasOld.Value) ? this.ConvertStructureToBytes() : base.Bytes);
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x00175E3C File Offset: 0x0017403C
		private byte[] ConvertStructureToBytes()
		{
			if (this.Folders.Count == 0)
			{
				return null;
			}
			List<byte[]> list = new List<byte[]>(this.Folders.Count);
			int num = 0;
			foreach (Rf2hFolder rf2hFolder in this.Folders)
			{
				byte[] array = this.ConvertFolder(rf2hFolder.CompleteString);
				list.Add(array);
				num += array.Length;
			}
			byte[] array2 = new byte[num];
			int num2 = 0;
			foreach (byte[] array3 in list)
			{
				Buffer.BlockCopy(array3, 0, array2, num2, array3.Length);
				num2 += array3.Length;
			}
			return array2;
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x00175F20 File Offset: 0x00174120
		protected override void BytesBeingSet()
		{
			this.ChangesMade(true);
		}

		// Token: 0x06005ABD RID: 23229 RVA: 0x00175F2C File Offset: 0x0017412C
		private byte[] ConvertFolder(string folder)
		{
			if (string.IsNullOrWhiteSpace(folder))
			{
				throw new CustomMqClientException(SR.FolderEmpty);
			}
			byte[] bytes = Encoding.UTF8.GetBytes(folder);
			int num = bytes.Length % 4;
			int num2 = ((num == 0) ? 0 : (4 - num));
			int num3 = bytes.Length + num2;
			byte[] array = new byte[num3 + 4];
			array[0] = (byte)(num3 & 255);
			array[1] = (byte)((num3 >> 8) & 255);
			array[2] = (byte)((num3 >> 16) & 255);
			array[3] = (byte)(num3 >> 24);
			Buffer.BlockCopy(bytes, 0, array, 4, bytes.Length);
			if (num2 != 0)
			{
				int num4 = array.Length - 1;
				for (int i = 0; i < num2; i++)
				{
					array[num4--] = 32;
				}
			}
			return array;
		}

		// Token: 0x06005ABE RID: 23230 RVA: 0x00175FE0 File Offset: 0x001741E0
		internal void ChangesMade(bool toOldProperty)
		{
			if (this.firstUsageWasOld == null)
			{
				this.firstUsageWasOld = new bool?(toOldProperty);
				return;
			}
			if (this.firstUsageWasOld.Value != toOldProperty)
			{
				throw new CustomMqClientException(SR.MixedUsageOfRulesAndFormattingHeader);
			}
		}

		// Token: 0x04004775 RID: 18293
		private const int NameValueCcsidUcs2 = 1200;

		// Token: 0x04004776 RID: 18294
		private const int NameValueCcsidUcs220 = 13488;

		// Token: 0x04004777 RID: 18295
		private const int NameValueCcsidUcs221 = 17584;

		// Token: 0x04004778 RID: 18296
		private const int NameValueCcsidUtf8 = 1208;

		// Token: 0x04004779 RID: 18297
		private const int NameValueCcsidUcs2WindowsBigEndian = 1201;

		// Token: 0x0400477A RID: 18298
		private const int NameValueCcsidUcs220WindowsBigEndian = 1201;

		// Token: 0x0400477B RID: 18299
		private const int NameValueCcsidUcs221WindowsBigEndian = 1201;

		// Token: 0x0400477C RID: 18300
		private const int NameValueCcsidUtf8WindowsBigEndian = 65001;

		// Token: 0x0400477D RID: 18301
		private const int NameValueCcsidUcs2WindowsLittleEndian = 1200;

		// Token: 0x0400477E RID: 18302
		private const int NameValueCcsidUcs220WindowsLittleEndian = 1200;

		// Token: 0x0400477F RID: 18303
		private const int NameValueCcsidUcs221WindowsLittleEndian = 1200;

		// Token: 0x04004780 RID: 18304
		private const int NameValueCcsidUtf8WindowsLittleEndian = 65001;

		// Token: 0x04004781 RID: 18305
		private static Dictionary<int, int> ibmToWindowsCodePagesBigEndian = new Dictionary<int, int>
		{
			{ 1200, 1201 },
			{ 13488, 1201 },
			{ 17584, 1201 },
			{ 1208, 65001 }
		};

		// Token: 0x04004782 RID: 18306
		private static Dictionary<int, int> ibmToWindowsCodePagesLittleEndian = new Dictionary<int, int>
		{
			{ 1200, 1200 },
			{ 13488, 1200 },
			{ 17584, 1200 },
			{ 1208, 65001 }
		};

		// Token: 0x04004784 RID: 18308
		private int ccsidForNamesAndValues = 1208;

		// Token: 0x04004785 RID: 18309
		private NumericEncoding numericEncodingForLengths = NumericEncoding.NativeWindowsEncoding;

		// Token: 0x04004787 RID: 18311
		internal bool? firstUsageWasOld;
	}
}
