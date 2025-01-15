using System;
using System.Linq;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000C4 RID: 196
	internal class EnclaveIdentity
	{
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0002CF83 File Offset: 0x0002B183
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x0002CF8B File Offset: 0x0002B18B
		private int Size { get; set; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0002CF94 File Offset: 0x0002B194
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0002CF9C File Offset: 0x0002B19C
		public uint EnclaveSvn { get; set; }

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0002CFA5 File Offset: 0x0002B1A5
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x0002CFAD File Offset: 0x0002B1AD
		public uint SecureKernelSvn { get; set; }

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0002CFB6 File Offset: 0x0002B1B6
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0002CFBE File Offset: 0x0002B1BE
		public uint PlatformSvn { get; set; }

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0002CFC7 File Offset: 0x0002B1C7
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x0002CFCF File Offset: 0x0002B1CF
		public uint Flags { get; set; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0002CFD8 File Offset: 0x0002B1D8
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x0002CFE0 File Offset: 0x0002B1E0
		public uint SigningLevel { get; set; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0002CFE9 File Offset: 0x0002B1E9
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x0002CFF1 File Offset: 0x0002B1F1
		public uint Reserved { get; set; }

		// Token: 0x06000E00 RID: 3584 RVA: 0x0002CFFC File Offset: 0x0002B1FC
		public EnclaveIdentity()
		{
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0002D060 File Offset: 0x0002B260
		public EnclaveIdentity(byte[] payload)
		{
			this.Size = payload.Length;
			int num = 0;
			int imageEnclaveLongIdLength = EnclaveIdentity.ImageEnclaveLongIdLength;
			this.OwnerId = payload.Skip(num).Take(imageEnclaveLongIdLength).ToArray<byte>();
			num += imageEnclaveLongIdLength;
			int imageEnclaveLongIdLength2 = EnclaveIdentity.ImageEnclaveLongIdLength;
			this.UniqueId = payload.Skip(num).Take(imageEnclaveLongIdLength2).ToArray<byte>();
			num += imageEnclaveLongIdLength2;
			int imageEnclaveLongIdLength3 = EnclaveIdentity.ImageEnclaveLongIdLength;
			this.AuthorId = payload.Skip(num).Take(imageEnclaveLongIdLength3).ToArray<byte>();
			num += imageEnclaveLongIdLength3;
			int imageEnclaveShortIdLength = EnclaveIdentity.ImageEnclaveShortIdLength;
			this.FamilyId = payload.Skip(num).Take(imageEnclaveShortIdLength).ToArray<byte>();
			num += imageEnclaveShortIdLength;
			int imageEnclaveShortIdLength2 = EnclaveIdentity.ImageEnclaveShortIdLength;
			this.ImageId = payload.Skip(num).Take(imageEnclaveShortIdLength2).ToArray<byte>();
			num += imageEnclaveShortIdLength2;
			this.EnclaveSvn = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.SecureKernelSvn = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.PlatformSvn = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.Flags = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.SigningLevel = BitConverter.ToUInt32(payload, num);
			num += 4;
			this.Reserved = BitConverter.ToUInt32(payload, num);
			num += 4;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0002D1E4 File Offset: 0x0002B3E4
		public int GetSizeInPayload()
		{
			return EnclaveIdentity.ImageEnclaveLongIdLength * 3 + EnclaveIdentity.ImageEnclaveShortIdLength * 2 + 24;
		}

		// Token: 0x0400060F RID: 1551
		private static readonly int ImageEnclaveLongIdLength = 32;

		// Token: 0x04000610 RID: 1552
		private static readonly int ImageEnclaveShortIdLength = 16;

		// Token: 0x04000611 RID: 1553
		public byte[] OwnerId = new byte[EnclaveIdentity.ImageEnclaveLongIdLength];

		// Token: 0x04000612 RID: 1554
		public byte[] UniqueId = new byte[EnclaveIdentity.ImageEnclaveLongIdLength];

		// Token: 0x04000613 RID: 1555
		public byte[] AuthorId = new byte[EnclaveIdentity.ImageEnclaveLongIdLength];

		// Token: 0x04000614 RID: 1556
		public byte[] FamilyId = new byte[EnclaveIdentity.ImageEnclaveShortIdLength];

		// Token: 0x04000615 RID: 1557
		public byte[] ImageId = new byte[EnclaveIdentity.ImageEnclaveShortIdLength];
	}
}
